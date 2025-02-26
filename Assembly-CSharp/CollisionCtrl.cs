using System;
using System.Collections.Generic;
using AIChara;
using Manager;
using UnityEngine;

// Token: 0x02000A79 RID: 2681
public class CollisionCtrl : MonoBehaviour
{
	// Token: 0x06004F67 RID: 20327 RVA: 0x001E8386 File Offset: 0x001E6786
	private void Update()
	{
		if (this.chaFemale)
		{
			this.Proc(this.chaFemale.getAnimatorStateInfo(0));
		}
	}

	// Token: 0x06004F68 RID: 20328 RVA: 0x001E83AC File Offset: 0x001E67AC
	public bool Init(ChaControl _female, GameObject _objHitHead, GameObject _objHitBody)
	{
		this.Release();
		this.chaFemale = _female;
		List<GameObject> headCollisionComponent = this.GetHeadCollisionComponent(_objHitHead);
		if (headCollisionComponent != null)
		{
			this.lstObj.AddRange(headCollisionComponent);
		}
		else
		{
			this.lstObj.Add(null);
		}
		if (_objHitBody)
		{
			HitCollision componentInChildren = _objHitBody.GetComponentInChildren<HitCollision>();
			if (componentInChildren)
			{
				this.lstObj.AddRange(componentInChildren.lstObj);
			}
		}
		return true;
	}

	// Token: 0x06004F69 RID: 20329 RVA: 0x001E8420 File Offset: 0x001E6820
	private List<GameObject> GetHeadCollisionComponent(GameObject _objHitHead)
	{
		if (_objHitHead == null)
		{
			return null;
		}
		HitCollision componentInChildren = _objHitHead.GetComponentInChildren<HitCollision>();
		if (componentInChildren == null)
		{
			return null;
		}
		if (componentInChildren.lstObj.Count == 0)
		{
			return null;
		}
		return componentInChildren.lstObj;
	}

	// Token: 0x06004F6A RID: 20330 RVA: 0x001E8468 File Offset: 0x001E6868
	public void Release()
	{
		this.lstObj.Clear();
		this.lstInfo = new List<CollisionCtrl.CollisionInfo>();
		this.isActive = true;
	}

	// Token: 0x06004F6B RID: 20331 RVA: 0x001E8487 File Offset: 0x001E6887
	public void LoadExcel(string _file)
	{
		if (_file == string.Empty)
		{
			return;
		}
		if (!Singleton<Manager.Resources>.Instance.HSceneTable.DicLstCollisionInfo.TryGetValue(_file, out this.lstInfo))
		{
			this.lstInfo = new List<CollisionCtrl.CollisionInfo>();
		}
	}

	// Token: 0x06004F6C RID: 20332 RVA: 0x001E84C8 File Offset: 0x001E68C8
	private bool Proc(AnimatorStateInfo _ai)
	{
		for (int i = 0; i < this.lstInfo.Count; i++)
		{
			if (_ai.IsName(this.lstInfo[i].nameAnimation.ToString()))
			{
				this.Visible(true);
				for (int j = 0; j < this.lstObj.Count; j++)
				{
					if (!(this.lstObj[j] == null))
					{
						if (this.lstObj[j].activeSelf != this.lstInfo[i].lstIsActive[j])
						{
							this.lstObj[j].SetActive(this.lstInfo[i].lstIsActive[j]);
						}
					}
				}
				return true;
			}
		}
		this.Visible(false);
		return false;
	}

	// Token: 0x06004F6D RID: 20333 RVA: 0x001E85C8 File Offset: 0x001E69C8
	private void Visible(bool _visible)
	{
		if (this.isActive == _visible)
		{
			return;
		}
		for (int i = 0; i < this.lstObj.Count; i++)
		{
			if (this.lstObj[i])
			{
				this.lstObj[i].SetActive(_visible);
			}
		}
		this.isActive = _visible;
	}

	// Token: 0x04004867 RID: 18535
	public List<CollisionCtrl.CollisionInfo> lstInfo = new List<CollisionCtrl.CollisionInfo>();

	// Token: 0x04004868 RID: 18536
	public List<GameObject> lstObj = new List<GameObject>();

	// Token: 0x04004869 RID: 18537
	public ChaControl chaFemale;

	// Token: 0x0400486A RID: 18538
	[DisabledGroup("表示")]
	public bool isActive = true;

	// Token: 0x02000A7A RID: 2682
	[Serializable]
	public struct CollisionInfo
	{
		// Token: 0x0400486B RID: 18539
		public string nameAnimation;

		// Token: 0x0400486C RID: 18540
		public List<bool> lstIsActive;
	}
}
