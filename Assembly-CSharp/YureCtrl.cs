using System;
using System.Collections.Generic;
using AIChara;
using Manager;
using UniRx;
using UnityEngine;

// Token: 0x02000AB9 RID: 2745
public class YureCtrl : MonoBehaviour
{
	// Token: 0x06005086 RID: 20614 RVA: 0x001F6BB0 File Offset: 0x001F4FB0
	private void Start()
	{
		for (int i = 0; i < 2; i++)
		{
			this.aBreastShape[i].MemberInit();
			this.aBreastShapeEnable[i].MemberInit();
		}
	}

	// Token: 0x06005087 RID: 20615 RVA: 0x001F6BF1 File Offset: 0x001F4FF1
	private void LateUpdate()
	{
		if (!this.isInit)
		{
			return;
		}
		if (this.chaFemale)
		{
			this.Proc(this.chaFemale.getAnimatorStateInfo(0));
		}
	}

	// Token: 0x06005088 RID: 20616 RVA: 0x001F6C22 File Offset: 0x001F5022
	public bool Release()
	{
		this.isInit = false;
		if (this.lstInfo != null)
		{
			this.lstInfo.Clear();
		}
		return true;
	}

	// Token: 0x06005089 RID: 20617 RVA: 0x001F6C44 File Offset: 0x001F5044
	public bool Load(int _motionId, int category)
	{
		this.isInit = false;
		for (int i = 0; i < this.aIsActive.Length; i++)
		{
			this.aIsActive[i] = true;
		}
		for (int j = 0; j < 2; j++)
		{
			this.aBreastShape[j].MemberInit();
		}
		if (this.chaFemale)
		{
			for (int k = 0; k < this.aIsActive.Length; k++)
			{
				this.chaFemale.playDynamicBoneBust((ChaControlDefine.DynamicBoneKind)k, true);
			}
			Observable.EveryEndOfFrame().Take(1).Subscribe(delegate(long _)
			{
				DynamicBone_Ver02 dynamicBoneBustAndHip = this.chaFemale.GetDynamicBoneBustAndHip(ChaControlDefine.DynamicBoneKind.BreastL);
				dynamicBoneBustAndHip.enabled = false;
				dynamicBoneBustAndHip = this.chaFemale.GetDynamicBoneBustAndHip(ChaControlDefine.DynamicBoneKind.BreastR);
				dynamicBoneBustAndHip.enabled = false;
				dynamicBoneBustAndHip = this.chaFemale.GetDynamicBoneBustAndHip(ChaControlDefine.DynamicBoneKind.HipL);
				dynamicBoneBustAndHip.enabled = false;
				dynamicBoneBustAndHip = this.chaFemale.GetDynamicBoneBustAndHip(ChaControlDefine.DynamicBoneKind.HipR);
				dynamicBoneBustAndHip.enabled = false;
			});
			for (int l = 0; l < ChaFileDefine.cf_BustShapeMaskID.Length; l++)
			{
				int id = l;
				this.chaFemale.DisableShapeBodyID(2, id, false);
			}
		}
		Dictionary<int, List<YureCtrl.Info>> dictionary = null;
		List<YureCtrl.Info> list = null;
		if (Singleton<Manager.Resources>.Instance.HSceneTable.DicDicYure.TryGetValue(category, out dictionary))
		{
			dictionary.TryGetValue(_motionId, out list);
		}
		if (list != null)
		{
			this.lstInfo = new List<YureCtrl.Info>(list);
		}
		else
		{
			this.lstInfo = new List<YureCtrl.Info>();
		}
		this.isInit = true;
		return true;
	}

	// Token: 0x0600508A RID: 20618 RVA: 0x001F6D74 File Offset: 0x001F5174
	public bool Proc(AnimatorStateInfo _ai)
	{
		if (!this.isInit)
		{
			return false;
		}
		YureCtrl.Info info = null;
		if (this.lstInfo != null)
		{
			for (int i = 0; i < this.lstInfo.Count; i++)
			{
				if (_ai.IsName(this.lstInfo[i].nameAnimation))
				{
					if (this.lstInfo[i].nFemale == this.femaleID)
					{
						info = this.lstInfo[i];
						break;
					}
				}
			}
		}
		if (info != null)
		{
			this.Active(info.aIsActive);
			this.Shape(info.aBreastShape);
			return true;
		}
		this.Active(this.aYureEnableActive);
		this.Shape(this.aBreastShapeEnable);
		return false;
	}

	// Token: 0x0600508B RID: 20619 RVA: 0x001F6E48 File Offset: 0x001F5248
	private void Active(bool[] _aIsActive)
	{
		for (int i = 0; i < this.aIsActive.Length; i++)
		{
			if (this.aIsActive[i] != _aIsActive[i])
			{
				this.chaFemale.playDynamicBoneBust((ChaControlDefine.DynamicBoneKind)i, _aIsActive[i]);
				this.aIsActive[i] = _aIsActive[i];
			}
		}
	}

	// Token: 0x0600508C RID: 20620 RVA: 0x001F6EA0 File Offset: 0x001F52A0
	private void Shape(YureCtrl.BreastShapeInfo[] _shapeInfo)
	{
		for (int i = 0; i < 2; i++)
		{
			int lr = i;
			YureCtrl.BreastShapeInfo breastShapeInfo = _shapeInfo[i];
			YureCtrl.BreastShapeInfo breastShapeInfo2 = this.aBreastShape[i];
			if (breastShapeInfo.breast != breastShapeInfo2.breast)
			{
				for (int j = 0; j < ChaFileDefine.cf_BustShapeMaskID.Length - 1; j++)
				{
					int num = j;
					if (breastShapeInfo.breast[num] != breastShapeInfo2.breast[num])
					{
						if (breastShapeInfo.breast[num])
						{
							this.chaFemale.DisableShapeBodyID(lr, num, false);
						}
						else
						{
							this.chaFemale.DisableShapeBodyID(lr, num, true);
						}
					}
				}
				breastShapeInfo2.breast = breastShapeInfo.breast;
			}
			if (breastShapeInfo.nip != breastShapeInfo2.nip)
			{
				if (breastShapeInfo.nip)
				{
					this.chaFemale.DisableShapeBodyID(lr, 7, false);
				}
				else
				{
					this.chaFemale.DisableShapeBodyID(lr, 7, true);
				}
				breastShapeInfo2.nip = breastShapeInfo.nip;
			}
			this.aBreastShape[i] = breastShapeInfo2;
		}
	}

	// Token: 0x0600508D RID: 20621 RVA: 0x001F6FD4 File Offset: 0x001F53D4
	public void ResetShape()
	{
		if (this.chaFemale == null)
		{
			return;
		}
		for (int i = 0; i < ChaFileDefine.cf_BustShapeMaskID.Length; i++)
		{
			int id = i;
			this.chaFemale.DisableShapeBodyID(2, id, false);
		}
		for (int j = 0; j < this.aIsActive.Length; j++)
		{
			this.aIsActive[j] = true;
			this.chaFemale.playDynamicBoneBust((ChaControlDefine.DynamicBoneKind)j, true);
		}
	}

	// Token: 0x04004A18 RID: 18968
	public List<YureCtrl.Info> lstInfo = new List<YureCtrl.Info>();

	// Token: 0x04004A19 RID: 18969
	public ChaControl chaFemale;

	// Token: 0x04004A1A RID: 18970
	public int femaleID;

	// Token: 0x04004A1B RID: 18971
	public bool isInit;

	// Token: 0x04004A1C RID: 18972
	[Tooltip("動いているかの確認用")]
	public bool[] aIsActive = new bool[]
	{
		true,
		true,
		true,
		true
	};

	// Token: 0x04004A1D RID: 18973
	[Tooltip("動いているかの確認用")]
	public YureCtrl.BreastShapeInfo[] aBreastShape = new YureCtrl.BreastShapeInfo[2];

	// Token: 0x04004A1E RID: 18974
	private bool[] aYureEnableActive = new bool[]
	{
		true,
		true,
		true,
		true
	};

	// Token: 0x04004A1F RID: 18975
	private YureCtrl.BreastShapeInfo[] aBreastShapeEnable = new YureCtrl.BreastShapeInfo[2];

	// Token: 0x04004A20 RID: 18976
	public YureCtrl.YureCtrlMapH CtrlMapH = new YureCtrl.YureCtrlMapH();

	// Token: 0x02000ABA RID: 2746
	[Serializable]
	public struct BreastShapeInfo
	{
		// Token: 0x0600508F RID: 20623 RVA: 0x001F70A9 File Offset: 0x001F54A9
		public void MemberInit()
		{
			this.breast = new bool[]
			{
				true,
				true,
				true,
				true,
				true,
				true,
				true
			};
			this.nip = true;
		}

		// Token: 0x04004A21 RID: 18977
		public bool[] breast;

		// Token: 0x04004A22 RID: 18978
		public bool nip;
	}

	// Token: 0x02000ABB RID: 2747
	[Serializable]
	public class Info
	{
		// Token: 0x04004A23 RID: 18979
		public string nameAnimation = string.Empty;

		// Token: 0x04004A24 RID: 18980
		public bool[] aIsActive = new bool[4];

		// Token: 0x04004A25 RID: 18981
		public YureCtrl.BreastShapeInfo[] aBreastShape = new YureCtrl.BreastShapeInfo[2];

		// Token: 0x04004A26 RID: 18982
		public int nFemale;
	}

	// Token: 0x02000ABC RID: 2748
	[Serializable]
	public class YureCtrlMapH
	{
		// Token: 0x06005092 RID: 20626 RVA: 0x001F7154 File Offset: 0x001F5554
		public int AddChaInit(ChaControl female)
		{
			int num = this.aBreastShapeEnable.Count;
			if (this.chaFemale.ContainsKey(num))
			{
				int num2 = 0;
				while (this.chaFemale.ContainsKey(num2))
				{
					num2++;
				}
				num = num2;
			}
			this.chaFemale.Add(num, female);
			this.aBreastShapeEnable.Add(num, new YureCtrl.BreastShapeInfo[2]);
			this.aBreastShape.Add(num, new YureCtrl.BreastShapeInfo[2]);
			for (int i = 0; i < 2; i++)
			{
				this.aBreastShape[num][i].MemberInit();
				this.aBreastShapeEnable[num][i].MemberInit();
			}
			this.aYureEnableActive.Add(num, new bool[]
			{
				true,
				true,
				true,
				true
			});
			return num;
		}

		// Token: 0x06005093 RID: 20627 RVA: 0x001F722C File Offset: 0x001F562C
		public void RemoveChaInit(int ID)
		{
			if (this.lstInfo.ContainsKey(ID))
			{
				this.lstInfo[ID] = null;
				this.lstInfo.Remove(ID);
			}
			if (this.chaFemale.ContainsKey(ID))
			{
				this.chaFemale[ID] = null;
				this.chaFemale.Remove(ID);
			}
			if (this.isInit.ContainsKey(ID))
			{
				this.isInit.Remove(ID);
			}
			if (this.aIsActive.ContainsKey(ID))
			{
				this.aIsActive[ID] = null;
				this.aIsActive.Remove(ID);
			}
			if (this.aBreastShape.ContainsKey(ID))
			{
				this.aBreastShape[ID] = null;
				this.aBreastShape.Remove(ID);
			}
			if (this.aYureEnableActive.ContainsKey(ID))
			{
				this.aYureEnableActive[ID] = null;
				this.aYureEnableActive.Remove(ID);
			}
			if (this.aBreastShapeEnable.ContainsKey(ID))
			{
				this.aBreastShapeEnable[ID] = null;
				this.aBreastShapeEnable.Remove(ID);
			}
		}

		// Token: 0x06005094 RID: 20628 RVA: 0x001F735C File Offset: 0x001F575C
		public bool AddLoadInfo(int _motionId, int category, int _addID)
		{
			if (!this.isInit.ContainsKey(_addID))
			{
				this.isInit.Add(_addID, false);
			}
			else
			{
				this.isInit[_addID] = false;
			}
			if (!this.lstInfo.ContainsKey(_addID))
			{
				this.lstInfo.Add(_addID, new List<YureCtrl.Info>());
			}
			else
			{
				this.lstInfo[_addID] = new List<YureCtrl.Info>();
			}
			if (!this.aIsActive.ContainsKey(_addID))
			{
				this.aIsActive.Add(_addID, new bool[4]);
			}
			for (int i = 0; i < this.aIsActive[_addID].Length; i++)
			{
				this.aIsActive[_addID][i] = true;
			}
			if (!this.aBreastShape.ContainsKey(_addID))
			{
				this.aBreastShape.Add(_addID, new YureCtrl.BreastShapeInfo[2]);
			}
			for (int j = 0; j < 2; j++)
			{
				this.aBreastShape[_addID][j].MemberInit();
			}
			if (this.chaFemale[_addID])
			{
				this.chaFemale[_addID].playDynamicBoneBust(0, true);
				this.chaFemale[_addID].playDynamicBoneBust(1, true);
				Observable.EveryEndOfFrame().Take(1).Subscribe(delegate(long _)
				{
					DynamicBone_Ver02 dynamicBoneBustAndHip = this.chaFemale[_addID].GetDynamicBoneBustAndHip(ChaControlDefine.DynamicBoneKind.BreastL);
					dynamicBoneBustAndHip.enabled = false;
					dynamicBoneBustAndHip = this.chaFemale[_addID].GetDynamicBoneBustAndHip(ChaControlDefine.DynamicBoneKind.BreastR);
					dynamicBoneBustAndHip.enabled = false;
				});
				for (int k = 0; k < ChaFileDefine.cf_BustShapeMaskID.Length; k++)
				{
					int id = k;
					this.chaFemale[_addID].DisableShapeBodyID(2, id, false);
				}
			}
			Dictionary<int, List<YureCtrl.Info>> dictionary = null;
			Singleton<Manager.Resources>.Instance.HSceneTable.DicDicYure.TryGetValue(category, out dictionary);
			if (dictionary != null)
			{
				List<YureCtrl.Info> value = new List<YureCtrl.Info>();
				dictionary.TryGetValue(_motionId, out value);
				this.lstInfo[_addID] = value;
			}
			this.isInit[_addID] = true;
			return true;
		}

		// Token: 0x06005095 RID: 20629 RVA: 0x001F75BC File Offset: 0x001F59BC
		public bool Proc(AnimatorStateInfo _ai, int _id)
		{
			if (!this.isInit[_id])
			{
				return false;
			}
			YureCtrl.Info info = null;
			if (this.lstInfo != null && this.lstInfo[_id] != null)
			{
				for (int i = 0; i < this.lstInfo[_id].Count; i++)
				{
					if (_ai.IsName(this.lstInfo[_id][i].nameAnimation))
					{
						if (this.lstInfo[_id][i].nFemale == 0)
						{
							info = this.lstInfo[_id][i];
							break;
						}
					}
				}
			}
			if (info != null)
			{
				this.Active(info.aIsActive, _id);
				this.Shape(info.aBreastShape, _id);
				return true;
			}
			this.Active(this.aYureEnableActive[_id], _id);
			this.Shape(this.aBreastShapeEnable[_id], _id);
			return false;
		}

		// Token: 0x06005096 RID: 20630 RVA: 0x001F76C8 File Offset: 0x001F5AC8
		private void Active(bool[] _aIsActive, int _id)
		{
			for (int i = 0; i < this.aIsActive[_id].Length; i++)
			{
				if (this.aIsActive[_id][i] != _aIsActive[i])
				{
					this.chaFemale[_id].playDynamicBoneBust((ChaControlDefine.DynamicBoneKind)i, _aIsActive[i]);
					this.aIsActive[_id][i] = _aIsActive[i];
				}
			}
		}

		// Token: 0x06005097 RID: 20631 RVA: 0x001F7738 File Offset: 0x001F5B38
		private void Shape(YureCtrl.BreastShapeInfo[] _shapeInfo, int _id)
		{
			for (int i = 0; i < 2; i++)
			{
				int lr = i;
				YureCtrl.BreastShapeInfo breastShapeInfo = _shapeInfo[i];
				YureCtrl.BreastShapeInfo breastShapeInfo2 = this.aBreastShape[_id][i];
				if (breastShapeInfo.breast != breastShapeInfo2.breast)
				{
					for (int j = 0; j < ChaFileDefine.cf_BustShapeMaskID.Length - 1; j++)
					{
						int num = j;
						if (breastShapeInfo.breast[num] != breastShapeInfo2.breast[num])
						{
							if (breastShapeInfo.breast[num])
							{
								this.chaFemale[_id].DisableShapeBodyID(lr, num, false);
							}
							else
							{
								this.chaFemale[_id].DisableShapeBodyID(lr, num, true);
							}
						}
					}
					breastShapeInfo2.breast = breastShapeInfo.breast;
				}
				if (breastShapeInfo.nip != breastShapeInfo2.nip)
				{
					if (breastShapeInfo.nip)
					{
						this.chaFemale[_id].DisableShapeBodyID(lr, 7, false);
					}
					else
					{
						this.chaFemale[_id].DisableShapeBodyID(lr, 7, true);
					}
					breastShapeInfo2.nip = breastShapeInfo.nip;
				}
				this.aBreastShape[_id][i] = breastShapeInfo2;
			}
		}

		// Token: 0x06005098 RID: 20632 RVA: 0x001F7890 File Offset: 0x001F5C90
		public void ResetShape(int _id)
		{
			if (this.chaFemale == null)
			{
				return;
			}
			for (int i = 0; i < ChaFileDefine.cf_BustShapeMaskID.Length; i++)
			{
				int id = i;
				if (this.chaFemale.ContainsKey(_id))
				{
					this.chaFemale[_id].DisableShapeBodyID(2, id, false);
				}
			}
			if (this.aIsActive.ContainsKey(_id))
			{
				for (int j = 0; j < this.aIsActive[_id].Length; j++)
				{
					this.aIsActive[_id][j] = true;
					if (this.chaFemale.ContainsKey(_id))
					{
						this.chaFemale[_id].playDynamicBoneBust((ChaControlDefine.DynamicBoneKind)j, true);
					}
				}
			}
		}

		// Token: 0x04004A27 RID: 18983
		public Dictionary<int, List<YureCtrl.Info>> lstInfo = new Dictionary<int, List<YureCtrl.Info>>();

		// Token: 0x04004A28 RID: 18984
		public Dictionary<int, ChaControl> chaFemale = new Dictionary<int, ChaControl>();

		// Token: 0x04004A29 RID: 18985
		[HideInInspector]
		public int femaleID;

		// Token: 0x04004A2A RID: 18986
		public Dictionary<int, bool> isInit = new Dictionary<int, bool>();

		// Token: 0x04004A2B RID: 18987
		public Dictionary<int, bool[]> aIsActive = new Dictionary<int, bool[]>();

		// Token: 0x04004A2C RID: 18988
		public Dictionary<int, YureCtrl.BreastShapeInfo[]> aBreastShape = new Dictionary<int, YureCtrl.BreastShapeInfo[]>();

		// Token: 0x04004A2D RID: 18989
		private Dictionary<int, bool[]> aYureEnableActive = new Dictionary<int, bool[]>();

		// Token: 0x04004A2E RID: 18990
		private Dictionary<int, YureCtrl.BreastShapeInfo[]> aBreastShapeEnable = new Dictionary<int, YureCtrl.BreastShapeInfo[]>();
	}
}
