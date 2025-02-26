using System;
using System.Collections;
using System.Collections.Generic;
using AIChara;
using Manager;
using UnityEngine;

// Token: 0x02000AA4 RID: 2724
public class HitObjectCtrl
{
	// Token: 0x0600501C RID: 20508 RVA: 0x001EDCF8 File Offset: 0x001EC0F8
	public IEnumerator HitObjInit(int Sex, GameObject _objBody, ChaControl _custom)
	{
		if (_objBody == null)
		{
			yield break;
		}
		this.ReleaseObject();
		while (!Singleton<Manager.Resources>.Instance.HSceneTable.endHLoad)
		{
			yield return null;
		}
		if (!Singleton<Manager.Resources>.Instance.HSceneTable.lstHitObject.ContainsKey(Sex))
		{
			yield break;
		}
		if (HitObjectCtrl.lstHitObject == null)
		{
			HitObjectCtrl.lstHitObject = new List<string>();
		}
		HitObjectCtrl.lstHitObject = Singleton<Manager.Resources>.Instance.HSceneTable.lstHitObject[Sex];
		int nX = HitObjectCtrl.lstHitObject.Count;
		GameObject obj = null;
		for (int i = 0; i < nX; i += 3)
		{
			GameObject objParent = this.GetObjParent(_objBody.transform, HitObjectCtrl.lstHitObject[i]);
			if (Singleton<Manager.Resources>.Instance.HSceneTable.DicHitObject.TryGetValue(Sex, out this.tmpDic))
			{
				if (this.tmpDic.TryGetValue(this.id, out this.tmpLst))
				{
					if (this.tmpLst.TryGetValue(HitObjectCtrl.lstHitObject[i + 2], out obj))
					{
						EliminateScale[] scale = obj.GetComponentsInChildren<EliminateScale>(true);
						foreach (EliminateScale eliminateScale in scale)
						{
							eliminateScale.custom = _custom;
						}
						if (objParent != null && obj != null)
						{
							obj.transform.SetParent(objParent.transform, false);
							obj.transform.localPosition = Vector3.zero;
							obj.transform.localRotation = Quaternion.identity;
						}
						this.lstObject.Add(obj);
					}
				}
			}
		}
		this.isInit = true;
		yield break;
	}

	// Token: 0x0600501D RID: 20509 RVA: 0x001EDD28 File Offset: 0x001EC128
	private GameObject GetObjParent(Transform objTop, string name)
	{
		this.getChild = objTop.GetComponentsInChildren<Transform>();
		for (int i = 0; i < this.getChild.Length; i++)
		{
			if (!(this.getChild[i].name != name))
			{
				return this.getChild[i].gameObject;
			}
		}
		return null;
	}

	// Token: 0x0600501E RID: 20510 RVA: 0x001EDD88 File Offset: 0x001EC188
	public void HitObjLoadExcel(string _file)
	{
		this.lstInfo = new List<HitObjectCtrl.CollisionInfo>();
		this.atariName = new List<string>();
		if (_file == string.Empty)
		{
			return;
		}
		if (!Singleton<Manager.Resources>.Instance.HSceneTable.DicLstHitObjInfo.TryGetValue(_file, out this.lstInfo))
		{
			this.lstInfo = new List<HitObjectCtrl.CollisionInfo>();
		}
		if (!Singleton<Manager.Resources>.Instance.HSceneTable.HitObjAtariName.TryGetValue(_file, out this.atariName))
		{
			this.atariName = new List<string>();
		}
	}

	// Token: 0x0600501F RID: 20511 RVA: 0x001EDE14 File Offset: 0x001EC214
	public bool setActiveObject(bool val)
	{
		for (int i = 0; i < this.lstObject.Count; i++)
		{
			if (!(this.lstObject[i] == null) && this.lstObject[i].activeSelf != val)
			{
				this.lstObject[i].SetActive(val);
			}
		}
		this.isActive = val;
		return true;
	}

	// Token: 0x06005020 RID: 20512 RVA: 0x001EDE8C File Offset: 0x001EC28C
	public bool ReleaseObject()
	{
		for (int i = 0; i < this.lstObject.Count; i++)
		{
			if (!(this.lstObject[i] == null))
			{
				this.lstObject[i].SetActive(false);
				this.lstObject[i].transform.SetParent(this.Place, false);
			}
		}
		this.isInit = false;
		this.isActive = true;
		return true;
	}

	// Token: 0x06005021 RID: 20513 RVA: 0x001EDF10 File Offset: 0x001EC310
	public bool Proc(Animator _anim)
	{
		if (_anim == null || _anim.runtimeAnimatorController == null)
		{
			this.Visible(false);
			return false;
		}
		this.ai = _anim.GetCurrentAnimatorStateInfo(0);
		for (int i = 0; i < this.lstInfo.Count; i++)
		{
			if (this.ai.IsName(this.lstInfo[i].nameAnimation.ToString()))
			{
				this.isActive = true;
				foreach (GameObject gameObject in this.lstObject)
				{
					for (int j = 0; j < this.atariName.Count; j++)
					{
						if (!(gameObject.name != this.atariName[j]))
						{
							if (gameObject.activeSelf != this.lstInfo[i].lstIsActive[j])
							{
								gameObject.SetActive(this.lstInfo[i].lstIsActive[j]);
							}
						}
					}
				}
				return true;
			}
		}
		this.Visible(false);
		return false;
	}

	// Token: 0x06005022 RID: 20514 RVA: 0x001EE088 File Offset: 0x001EC488
	private bool Visible(bool _visible)
	{
		if (this.isActive == _visible)
		{
			return false;
		}
		for (int i = 0; i < this.lstObject.Count; i++)
		{
			this.lstObject[i].SetActive(_visible);
		}
		this.isActive = _visible;
		return false;
	}

	// Token: 0x04004920 RID: 18720
	public bool isInit;

	// Token: 0x04004921 RID: 18721
	public bool isActive = true;

	// Token: 0x04004922 RID: 18722
	public int id = -1;

	// Token: 0x04004923 RID: 18723
	public Transform Place;

	// Token: 0x04004924 RID: 18724
	private List<string> atariName = new List<string>();

	// Token: 0x04004925 RID: 18725
	private List<GameObject> lstObject = new List<GameObject>();

	// Token: 0x04004926 RID: 18726
	private List<HitObjectCtrl.CollisionInfo> lstInfo = new List<HitObjectCtrl.CollisionInfo>();

	// Token: 0x04004927 RID: 18727
	private AnimatorStateInfo ai;

	// Token: 0x04004928 RID: 18728
	private Dictionary<int, Dictionary<string, GameObject>> tmpDic = new Dictionary<int, Dictionary<string, GameObject>>();

	// Token: 0x04004929 RID: 18729
	private Dictionary<string, GameObject> tmpLst = new Dictionary<string, GameObject>();

	// Token: 0x0400492A RID: 18730
	private string pathAsset;

	// Token: 0x0400492B RID: 18731
	private static List<string> lstHitObject;

	// Token: 0x0400492C RID: 18732
	private Transform[] getChild;

	// Token: 0x02000AA5 RID: 2725
	public struct CollisionInfo
	{
		// Token: 0x0400492D RID: 18733
		public string nameAnimation;

		// Token: 0x0400492E RID: 18734
		public List<bool> lstIsActive;
	}
}
