using System;
using System.Collections.Generic;
using AIChara;
using Manager;
using UnityEngine;

// Token: 0x02000ABD RID: 2749
public class YureCtrlMale : MonoBehaviour
{
	// Token: 0x0600509A RID: 20634 RVA: 0x001F7A14 File Offset: 0x001F5E14
	private void Start()
	{
		for (int i = 0; i < 2; i++)
		{
			this.aBreastShape[i].MemberInit();
			this.aBreastShapeEnable[i].MemberInit();
		}
	}

	// Token: 0x0600509B RID: 20635 RVA: 0x001F7A55 File Offset: 0x001F5E55
	private void LateUpdate()
	{
		if (!this.isInit)
		{
			return;
		}
		if (this.chaMale)
		{
			this.Proc(this.chaMale.getAnimatorStateInfo(0));
		}
	}

	// Token: 0x0600509C RID: 20636 RVA: 0x001F7A86 File Offset: 0x001F5E86
	public bool Release()
	{
		this.isInit = false;
		if (this.lstInfo != null)
		{
			this.lstInfo.Clear();
		}
		return true;
	}

	// Token: 0x0600509D RID: 20637 RVA: 0x001F7AA8 File Offset: 0x001F5EA8
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
		if (this.chaMale)
		{
			for (int k = 0; k < this.aIsActive.Length; k++)
			{
				this.chaMale.playDynamicBoneBust((ChaControlDefine.DynamicBoneKind)k, true);
			}
			for (int l = 0; l < ChaFileDefine.cf_BustShapeMaskID.Length; l++)
			{
				int id = l;
				this.chaMale.DisableShapeBodyID(2, id, false);
			}
		}
		Dictionary<int, List<YureCtrlMale.Info>> dictionary = null;
		List<YureCtrlMale.Info> list = null;
		Singleton<Manager.Resources>.Instance.HSceneTable.DicDicYureMale.TryGetValue(category, out dictionary);
		if (dictionary != null)
		{
			dictionary.TryGetValue(_motionId, out list);
		}
		if (list != null)
		{
			this.lstInfo = new List<YureCtrlMale.Info>(list);
		}
		else
		{
			this.lstInfo = new List<YureCtrlMale.Info>();
		}
		this.isInit = true;
		return true;
	}

	// Token: 0x0600509E RID: 20638 RVA: 0x001F7BC0 File Offset: 0x001F5FC0
	public bool Proc(AnimatorStateInfo _ai)
	{
		if (!this.isInit)
		{
			return false;
		}
		YureCtrlMale.Info info = null;
		if (this.lstInfo != null)
		{
			for (int i = 0; i < this.lstInfo.Count; i++)
			{
				if (_ai.IsName(this.lstInfo[i].nameAnimation))
				{
					if (this.lstInfo[i].nMale == this.MaleID)
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

	// Token: 0x0600509F RID: 20639 RVA: 0x001F7C94 File Offset: 0x001F6094
	private void Active(bool[] _aIsActive)
	{
		for (int i = 0; i < this.aIsActive.Length; i++)
		{
			if (this.aIsActive[i] != _aIsActive[i])
			{
				this.chaMale.playDynamicBoneBust((ChaControlDefine.DynamicBoneKind)i, _aIsActive[i]);
				this.aIsActive[i] = _aIsActive[i];
			}
		}
	}

	// Token: 0x060050A0 RID: 20640 RVA: 0x001F7CEC File Offset: 0x001F60EC
	private void Shape(YureCtrlMale.BreastShapeInfo[] _shapeInfo)
	{
		for (int i = 0; i < 2; i++)
		{
			int lr = i;
			YureCtrlMale.BreastShapeInfo breastShapeInfo = _shapeInfo[i];
			YureCtrlMale.BreastShapeInfo breastShapeInfo2 = this.aBreastShape[i];
			if (breastShapeInfo.breast != breastShapeInfo2.breast)
			{
				for (int j = 0; j < ChaFileDefine.cf_BustShapeMaskID.Length - 1; j++)
				{
					int num = j;
					if (breastShapeInfo.breast[num] != breastShapeInfo2.breast[num])
					{
						if (breastShapeInfo.breast[num])
						{
							this.chaMale.DisableShapeBodyID(lr, num, false);
						}
						else
						{
							this.chaMale.DisableShapeBodyID(lr, num, true);
						}
					}
				}
				breastShapeInfo2.breast = breastShapeInfo.breast;
			}
			if (breastShapeInfo.nip != breastShapeInfo2.nip)
			{
				if (breastShapeInfo.nip)
				{
					this.chaMale.DisableShapeBodyID(lr, 7, false);
				}
				else
				{
					this.chaMale.DisableShapeBodyID(lr, 7, true);
				}
				breastShapeInfo2.nip = breastShapeInfo.nip;
			}
			this.aBreastShape[i] = breastShapeInfo2;
		}
	}

	// Token: 0x060050A1 RID: 20641 RVA: 0x001F7E20 File Offset: 0x001F6220
	public void ResetShape()
	{
		if (this.chaMale == null)
		{
			return;
		}
		for (int i = 0; i < ChaFileDefine.cf_BustShapeMaskID.Length; i++)
		{
			int id = i;
			this.chaMale.DisableShapeBodyID(2, id, false);
		}
		for (int j = 0; j < this.aIsActive.Length; j++)
		{
			this.aIsActive[j] = true;
			this.chaMale.playDynamicBoneBust((ChaControlDefine.DynamicBoneKind)j, true);
		}
	}

	// Token: 0x04004A2F RID: 18991
	public List<YureCtrlMale.Info> lstInfo = new List<YureCtrlMale.Info>();

	// Token: 0x04004A30 RID: 18992
	public ChaControl chaMale;

	// Token: 0x04004A31 RID: 18993
	public int MaleID;

	// Token: 0x04004A32 RID: 18994
	public bool isInit;

	// Token: 0x04004A33 RID: 18995
	[Tooltip("動いているかの確認用")]
	public bool[] aIsActive = new bool[]
	{
		true,
		true,
		true,
		true
	};

	// Token: 0x04004A34 RID: 18996
	[Tooltip("動いているかの確認用")]
	public YureCtrlMale.BreastShapeInfo[] aBreastShape = new YureCtrlMale.BreastShapeInfo[2];

	// Token: 0x04004A35 RID: 18997
	private bool[] aYureEnableActive = new bool[]
	{
		true,
		true,
		true,
		true
	};

	// Token: 0x04004A36 RID: 18998
	private YureCtrlMale.BreastShapeInfo[] aBreastShapeEnable = new YureCtrlMale.BreastShapeInfo[2];

	// Token: 0x02000ABE RID: 2750
	[Serializable]
	public struct BreastShapeInfo
	{
		// Token: 0x060050A2 RID: 20642 RVA: 0x001F7E96 File Offset: 0x001F6296
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

		// Token: 0x04004A37 RID: 18999
		public bool[] breast;

		// Token: 0x04004A38 RID: 19000
		public bool nip;
	}

	// Token: 0x02000ABF RID: 2751
	[Serializable]
	public class Info
	{
		// Token: 0x04004A39 RID: 19001
		public string nameAnimation = string.Empty;

		// Token: 0x04004A3A RID: 19002
		public bool[] aIsActive = new bool[4];

		// Token: 0x04004A3B RID: 19003
		public YureCtrlMale.BreastShapeInfo[] aBreastShape = new YureCtrlMale.BreastShapeInfo[2];

		// Token: 0x04004A3C RID: 19004
		public int nMale;
	}
}
