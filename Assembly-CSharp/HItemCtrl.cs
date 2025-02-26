using System;
using System.Collections.Generic;
using Illusion;
using IllusionUtility.GetUtility;
using Manager;
using UnityEngine;
using UnityEx;

// Token: 0x02000A9F RID: 2719
public class HItemCtrl
{
	// Token: 0x06005003 RID: 20483 RVA: 0x001ECD74 File Offset: 0x001EB174
	public void HItemInit(Transform _hitemPlace)
	{
		this.lstParent = Singleton<Manager.Resources>.Instance.HSceneTable.lstHItemObjInfo;
		this.BaseRacs = Singleton<Manager.Resources>.Instance.HSceneTable.lstHItemBase;
		this.hitemPlace = _hitemPlace;
	}

	// Token: 0x06005004 RID: 20484 RVA: 0x001ECDA8 File Offset: 0x001EB1A8
	public bool LoadItem(int _mode, int _id, GameObject _boneMale, GameObject _boneFemale, GameObject _boneMale1, GameObject _boneFemale1)
	{
		this.ReleaseItem();
		List<HItemCtrl.ListItem> list = null;
		this.itemObj.Clear();
		for (int i = 0; i < this.lstParent[_mode].Count; i++)
		{
			if (this.lstParent[_mode][i].ContainsKey(_id))
			{
				list = this.lstParent[_mode][i][_id];
				foreach (HItemCtrl.ListItem listItem in list)
				{
					HItemCtrl.Item item = new HItemCtrl.Item();
					if (GlobalMethod.AssetFileExist(listItem.pathAssetObject, listItem.nameObject, listItem.nameManifest))
					{
						item.itemName = listItem.Name;
						item.objItem = CommonLib.LoadAsset<GameObject>(listItem.pathAssetObject, listItem.nameObject, true, listItem.nameManifest);
						item.transItem = item.objItem.transform;
						Singleton<HSceneManager>.Instance.hashUseAssetBundle.Add(listItem.pathAssetObject);
						this.LoadAnimation(item, listItem);
						this.lstItem.Add(item);
					}
				}
			}
		}
		if (list == null)
		{
			return false;
		}
		for (int j = 0; j < list.Count; j++)
		{
			if (this.lstItem.Count <= j)
			{
				break;
			}
			if (!(this.lstItem[j].objItem == null))
			{
				foreach (HItemCtrl.ParentInfo parentInfo in list[j].lstParent)
				{
					GameObject gameObject = null;
					if (parentInfo.numToWhomParent == 0)
					{
						if (_boneMale != null)
						{
							gameObject = _boneMale.transform.FindLoop(parentInfo.nameParent);
						}
					}
					else if (parentInfo.numToWhomParent == 1)
					{
						if (_boneFemale != null)
						{
							gameObject = _boneFemale.transform.FindLoop(parentInfo.nameParent);
						}
					}
					else if (parentInfo.numToWhomParent == 2)
					{
						if (_boneMale1 != null)
						{
							gameObject = _boneMale1.transform.FindLoop(parentInfo.nameParent);
						}
					}
					else if (parentInfo.numToWhomParent == 3)
					{
						if (_boneFemale1 != null)
						{
							gameObject = _boneFemale1.transform.FindLoop(parentInfo.nameParent);
						}
					}
					else
					{
						int num = parentInfo.numToWhomParent - 4;
						if (this.lstItem.Count > num && this.lstItem[num].objItem)
						{
							gameObject = this.lstItem[num].transItem.FindLoop(parentInfo.nameParent);
						}
					}
					GameObject gameObject2;
					if (parentInfo.nameSelf != string.Empty)
					{
						gameObject2 = this.lstItem[j].transItem.FindLoop(parentInfo.nameSelf);
					}
					else
					{
						gameObject2 = this.lstItem[j].objItem;
					}
					if (!(gameObject == null) && !(gameObject2 == null))
					{
						HItemCtrl.ChildInfo childInfo = new HItemCtrl.ChildInfo();
						childInfo.objChild = gameObject2;
						childInfo.transChild = gameObject2.transform;
						childInfo.oldParent = gameObject2.transform.parent;
						this.lstItem[j].lstChild.Add(childInfo);
						if (parentInfo.isParentMode)
						{
							childInfo.transChild.SetParent(gameObject.transform, false);
							childInfo.transChild.localPosition = Vector3.zero;
							childInfo.transChild.localRotation = Quaternion.identity;
						}
						else
						{
							childInfo.transChild.SetParent(this.hitemPlace, false);
							childInfo.transChild.position = gameObject.transform.position;
							childInfo.transChild.rotation = gameObject.transform.rotation;
						}
						if (!parentInfo.isParentScale)
						{
							this.itemObj.Add(new UnityEx.ValueTuple<Transform, Transform, bool>(childInfo.transChild, gameObject.transform, parentInfo.isParentMode));
						}
					}
				}
			}
		}
		GC.Collect();
		return true;
	}

	// Token: 0x06005005 RID: 20485 RVA: 0x001ED260 File Offset: 0x001EB660
	public void ParentScaleReject()
	{
		if (this.itemObj.Count <= 0)
		{
			return;
		}
		for (int i = 0; i < this.itemObj.Count; i++)
		{
			if (this.itemObj[i].Item3)
			{
				Vector3 lossyScale = this.itemObj[i].Item1.lossyScale;
				Vector3 localScale = this.itemObj[i].Item1.localScale;
				this.itemObj[i].Item1.localScale = new Vector3(localScale.x / lossyScale.x, localScale.y / lossyScale.y, localScale.z / lossyScale.z);
			}
			else
			{
				this.itemObj[i].Item1.position = this.itemObj[i].Item2.position;
				this.itemObj[i].Item1.rotation = this.itemObj[i].Item2.rotation;
			}
		}
	}

	// Token: 0x06005006 RID: 20486 RVA: 0x001ED3A4 File Offset: 0x001EB7A4
	public bool ReleaseItem()
	{
		for (int i = 0; i < this.lstItem.Count; i++)
		{
			if (!(this.lstItem[i].objItem == null))
			{
				for (int j = 0; j < this.lstItem[i].lstChild.Count; j++)
				{
					HItemCtrl.ChildInfo childInfo = this.lstItem[i].lstChild[j];
					if (childInfo.objChild && childInfo.oldParent)
					{
						childInfo.transChild.SetParent(childInfo.oldParent, false);
					}
				}
				UnityEngine.Object.Destroy(this.lstItem[i].objItem);
				this.lstItem[i].objItem = null;
				this.lstItem[i].animItem = null;
			}
		}
		this.lstItem.Clear();
		this.itemObj.Clear();
		return true;
	}

	// Token: 0x06005007 RID: 20487 RVA: 0x001ED4B0 File Offset: 0x001EB8B0
	public bool setTransform(Transform _transform)
	{
		if (_transform == null)
		{
			return false;
		}
		foreach (HItemCtrl.Item item in this.lstItem)
		{
			if (!(item.objItem == null))
			{
				if (!(item.transItem.parent != null))
				{
					item.transItem.position = _transform.position;
					item.transItem.rotation = _transform.rotation;
				}
			}
		}
		return true;
	}

	// Token: 0x06005008 RID: 20488 RVA: 0x001ED568 File Offset: 0x001EB968
	public bool setTransform(Vector3 pos, Vector3 rot)
	{
		foreach (HItemCtrl.Item item in this.lstItem)
		{
			if (!(item.objItem == null))
			{
				if (!(item.transItem.parent != null))
				{
					item.transItem.position = pos;
					item.transItem.rotation = Quaternion.Euler(rot);
				}
			}
		}
		return true;
	}

	// Token: 0x06005009 RID: 20489 RVA: 0x001ED60C File Offset: 0x001EBA0C
	public void syncPlay(AnimatorStateInfo _ai)
	{
		foreach (HItemCtrl.Item item in this.lstItem)
		{
			if (!(item.animItem == null))
			{
				item.animItem.Play(_ai.shortNameHash, 0, _ai.normalizedTime);
			}
		}
	}

	// Token: 0x0600500A RID: 20490 RVA: 0x001ED694 File Offset: 0x001EBA94
	public void syncPlay(int _nameHash, float _fnormalizedTime)
	{
		foreach (HItemCtrl.Item item in this.lstItem)
		{
			if (!(item.animItem == null))
			{
				item.animItem.Play(_nameHash, 0, _fnormalizedTime);
			}
		}
	}

	// Token: 0x0600500B RID: 20491 RVA: 0x001ED710 File Offset: 0x001EBB10
	public void Update()
	{
		foreach (HItemCtrl.Item item in this.lstItem)
		{
			if (!(item.animItem == null))
			{
				item.animItem.Update(0f);
			}
		}
	}

	// Token: 0x0600500C RID: 20492 RVA: 0x001ED78C File Offset: 0x001EBB8C
	public void setPlay(string _strAnmName)
	{
		foreach (HItemCtrl.Item item in this.lstItem)
		{
			if (!(item.animItem == null))
			{
				item.animItem.Play(_strAnmName, 0);
			}
		}
	}

	// Token: 0x0600500D RID: 20493 RVA: 0x001ED804 File Offset: 0x001EBC04
	public void setPlay(string _strAnmName, float normalizeTime)
	{
		foreach (HItemCtrl.Item item in this.lstItem)
		{
			if (!(item.animItem == null))
			{
				item.animItem.Play(_strAnmName, 0, normalizeTime);
			}
		}
	}

	// Token: 0x0600500E RID: 20494 RVA: 0x001ED880 File Offset: 0x001EBC80
	public bool setPlay(string _strAnmName, int _nObj)
	{
		if (this.lstItem.Count <= _nObj)
		{
			return false;
		}
		if (this.lstItem[_nObj].animItem == null)
		{
			return false;
		}
		this.lstItem[_nObj].animItem.Play(_strAnmName, 0);
		return true;
	}

	// Token: 0x0600500F RID: 20495 RVA: 0x001ED8D8 File Offset: 0x001EBCD8
	public void setAnimatorParamTrigger(string _strAnmName)
	{
		foreach (HItemCtrl.Item item in this.lstItem)
		{
			if (!(item.animItem == null))
			{
				item.animItem.SetTrigger(_strAnmName);
			}
		}
	}

	// Token: 0x06005010 RID: 20496 RVA: 0x001ED950 File Offset: 0x001EBD50
	public void setAnimatorParamResetTrigger(string _strAnmName)
	{
		foreach (HItemCtrl.Item item in this.lstItem)
		{
			if (!(item.animItem == null))
			{
				item.animItem.ResetTrigger(_strAnmName);
			}
		}
	}

	// Token: 0x06005011 RID: 20497 RVA: 0x001ED9C8 File Offset: 0x001EBDC8
	public void setAnimatorParamBool(string _strAnmName, bool _bFlag)
	{
		foreach (HItemCtrl.Item item in this.lstItem)
		{
			if (!(item.animItem == null))
			{
				item.animItem.SetBool(_strAnmName, _bFlag);
			}
		}
	}

	// Token: 0x06005012 RID: 20498 RVA: 0x001EDA40 File Offset: 0x001EBE40
	public void setAnimatorParamFloat(string _strAnmName, float _fValue)
	{
		foreach (HItemCtrl.Item item in this.lstItem)
		{
			if (!(item.animItem == null))
			{
				item.animItem.SetFloat(_strAnmName, _fValue);
			}
		}
	}

	// Token: 0x06005013 RID: 20499 RVA: 0x001EDAB8 File Offset: 0x001EBEB8
	public GameObject GetItem()
	{
		if (this.lstItem.Count < 1)
		{
			return null;
		}
		return this.lstItem[0].objItem;
	}

	// Token: 0x06005014 RID: 20500 RVA: 0x001EDADE File Offset: 0x001EBEDE
	public List<HItemCtrl.Item> GetItems()
	{
		return this.lstItem;
	}

	// Token: 0x06005015 RID: 20501 RVA: 0x001EDAE6 File Offset: 0x001EBEE6
	public List<Dictionary<int, List<HItemCtrl.ListItem>>>[] GetListItemInfos()
	{
		return this.lstParent;
	}

	// Token: 0x06005016 RID: 20502 RVA: 0x001EDAF0 File Offset: 0x001EBEF0
	private bool LoadAnimation(HItemCtrl.Item _item, HItemCtrl.ListItem _info)
	{
		if (_item.objItem == null)
		{
			return false;
		}
		_item.animItem = _item.objItem.GetComponent<Animator>();
		if (_item.animItem == null)
		{
			_item.animItem = _item.objItem.GetComponentInChildren<Animator>();
			if (_item.animItem == null)
			{
				return false;
			}
		}
		if (_info.pathAssetAnimator.IsNullOrEmpty() || _info.nameAnimator.IsNullOrEmpty())
		{
			_item.animItem = null;
			return false;
		}
		foreach (UnityEx.ValueTuple<string, RuntimeAnimatorController> valueTuple in this.BaseRacs)
		{
			if (!(valueTuple.Item1 != _info.nameAnimatorBase))
			{
				this.rac[0] = valueTuple.Item2;
			}
		}
		_item.animItem.runtimeAnimatorController = this.rac[0];
		if (_item.animItem.runtimeAnimatorController == null)
		{
			_item.animItem = null;
		}
		this.rac[1] = CommonLib.LoadAsset<RuntimeAnimatorController>(_info.pathAssetAnimator, _info.nameAnimator, false, string.Empty);
		_item.animItem.runtimeAnimatorController = Illusion.Utils.Animator.SetupAnimatorOverrideController(_item.animItem.runtimeAnimatorController, this.rac[1]);
		return true;
	}

	// Token: 0x04004902 RID: 18690
	private List<HItemCtrl.Item> lstItem = new List<HItemCtrl.Item>();

	// Token: 0x04004903 RID: 18691
	private List<Dictionary<int, List<HItemCtrl.ListItem>>>[] lstParent = new List<Dictionary<int, List<HItemCtrl.ListItem>>>[]
	{
		new List<Dictionary<int, List<HItemCtrl.ListItem>>>(),
		new List<Dictionary<int, List<HItemCtrl.ListItem>>>(),
		new List<Dictionary<int, List<HItemCtrl.ListItem>>>(),
		new List<Dictionary<int, List<HItemCtrl.ListItem>>>(),
		new List<Dictionary<int, List<HItemCtrl.ListItem>>>(),
		new List<Dictionary<int, List<HItemCtrl.ListItem>>>()
	};

	// Token: 0x04004904 RID: 18692
	private List<UnityEx.ValueTuple<Transform, Transform, bool>> itemObj = new List<UnityEx.ValueTuple<Transform, Transform, bool>>();

	// Token: 0x04004905 RID: 18693
	private Transform hitemPlace;

	// Token: 0x04004906 RID: 18694
	private List<UnityEx.ValueTuple<string, RuntimeAnimatorController>> BaseRacs = new List<UnityEx.ValueTuple<string, RuntimeAnimatorController>>();

	// Token: 0x04004907 RID: 18695
	private RuntimeAnimatorController[] rac = new RuntimeAnimatorController[2];

	// Token: 0x02000AA0 RID: 2720
	public class ChildInfo
	{
		// Token: 0x04004908 RID: 18696
		public GameObject objChild;

		// Token: 0x04004909 RID: 18697
		public Transform transChild;

		// Token: 0x0400490A RID: 18698
		public Transform oldParent;
	}

	// Token: 0x02000AA1 RID: 2721
	public class Item
	{
		// Token: 0x0400490B RID: 18699
		public string itemName;

		// Token: 0x0400490C RID: 18700
		public GameObject objItem;

		// Token: 0x0400490D RID: 18701
		public Transform transItem;

		// Token: 0x0400490E RID: 18702
		public Animator animItem;

		// Token: 0x0400490F RID: 18703
		public List<HItemCtrl.ChildInfo> lstChild = new List<HItemCtrl.ChildInfo>();
	}

	// Token: 0x02000AA2 RID: 2722
	public class ParentInfo
	{
		// Token: 0x04004910 RID: 18704
		public bool isParentMode;

		// Token: 0x04004911 RID: 18705
		public int numToWhomParent;

		// Token: 0x04004912 RID: 18706
		public string nameParent;

		// Token: 0x04004913 RID: 18707
		public string nameSelf;

		// Token: 0x04004914 RID: 18708
		public bool isParentScale;
	}

	// Token: 0x02000AA3 RID: 2723
	public class ListItem
	{
		// Token: 0x04004915 RID: 18709
		public string Name;

		// Token: 0x04004916 RID: 18710
		public int itemkind;

		// Token: 0x04004917 RID: 18711
		public int itemID;

		// Token: 0x04004918 RID: 18712
		public string nameManifest;

		// Token: 0x04004919 RID: 18713
		public string pathAssetObject;

		// Token: 0x0400491A RID: 18714
		public string nameObject;

		// Token: 0x0400491B RID: 18715
		public string pathAssetAnimatorBase;

		// Token: 0x0400491C RID: 18716
		public string nameAnimatorBase;

		// Token: 0x0400491D RID: 18717
		public string pathAssetAnimator;

		// Token: 0x0400491E RID: 18718
		public string nameAnimator;

		// Token: 0x0400491F RID: 18719
		public List<HItemCtrl.ParentInfo> lstParent = new List<HItemCtrl.ParentInfo>();
	}
}
