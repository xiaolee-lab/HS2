using System;
using System.Collections.Generic;
using System.Text;
using AIChara;
using AIProject;
using Manager;
using UnityEngine;

// Token: 0x02000A7B RID: 2683
public class DynamicBoneReferenceCtrl
{
	// Token: 0x06004F6F RID: 20335 RVA: 0x001E8680 File Offset: 0x001E6A80
	public bool Init(ChaControl _female)
	{
		this.chaFemale = _female;
		if (this.chaFemale == null)
		{
			return false;
		}
		DynamicBone_Ver02[] array = new DynamicBone_Ver02[]
		{
			this.chaFemale.GetDynamicBoneBustAndHip(ChaControlDefine.DynamicBoneKind.BreastL),
			this.chaFemale.GetDynamicBoneBustAndHip(ChaControlDefine.DynamicBoneKind.BreastR)
		};
		for (int i = 0; i < 2; i++)
		{
			this.lstsRef[i] = new List<DynamicBoneReferenceCtrl.Reference>();
			this.bonePtns[i] = new DynamicBone_Ver02.BonePtn();
			this.lstsTrans[i] = new List<Transform>();
			if (array[i] != null && array[i].Patterns.Count > 0)
			{
				this.bonePtns[i] = array[i].Patterns[0];
				for (int j = 1; j < this.bonePtns[i].Params.Count; j++)
				{
					this.lstsTrans[i].Add(this.bonePtns[i].Params[j].RefTransform);
				}
			}
		}
		return true;
	}

	// Token: 0x06004F70 RID: 20336 RVA: 0x001E8788 File Offset: 0x001E6B88
	public bool Load(string _assetpath, string _file)
	{
		List<bool>[] array = new List<bool>[2];
		List<bool>[] array2 = new List<bool>[2];
		this.isInit = false;
		for (int i = 0; i < 2; i++)
		{
			this.InitDynamicBoneReferenceBone(this.bonePtns[i], this.lstsTrans[i]);
			if (this.lstsRef[i] != null)
			{
				this.lstsRef[i].Clear();
			}
			array[i] = new List<bool>();
			array[i].Add(false);
			array2[i] = new List<bool>();
			array2[i].Add(false);
		}
		if (_file == string.Empty)
		{
			return false;
		}
		this.sbAssetName.Clear();
		this.sbAssetName.Append(_file.Remove(3, 2));
		List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(_assetpath, false);
		for (int j = 0; j < assetBundleNameListFromPath.Count; j++)
		{
			if (!GlobalMethod.AssetFileExist(assetBundleNameListFromPath[j], this.sbAssetName.ToString(), string.Empty))
			{
				return false;
			}
			ExcelData excelData = CommonLib.LoadAsset<ExcelData>(assetBundleNameListFromPath[j], this.sbAssetName.ToString(), false, string.Empty);
			Singleton<HSceneManager>.Instance.hashUseAssetBundle.Add(assetBundleNameListFromPath[j]);
			if (excelData == null)
			{
				return false;
			}
			int k = 3;
			while (k < excelData.MaxCell)
			{
				int num = k - 3;
				this.row = excelData.list[k++].list;
				int num2 = 2;
				DynamicBoneReferenceCtrl.Reference item;
				item.position.sizeS = new Vector3(float.Parse(this.row.GetElement(num2++)), float.Parse(this.row.GetElement(num2++)), float.Parse(this.row.GetElement(num2++)));
				item.position.sizeM = new Vector3(float.Parse(this.row.GetElement(num2++)), float.Parse(this.row.GetElement(num2++)), float.Parse(this.row.GetElement(num2++)));
				item.position.sizeL = new Vector3(float.Parse(this.row.GetElement(num2++)), float.Parse(this.row.GetElement(num2++)), float.Parse(this.row.GetElement(num2++)));
				item.rotation.sizeS = new Vector3(float.Parse(this.row.GetElement(num2++)), float.Parse(this.row.GetElement(num2++)), float.Parse(this.row.GetElement(num2++)));
				item.rotation.sizeM = new Vector3(float.Parse(this.row.GetElement(num2++)), float.Parse(this.row.GetElement(num2++)), float.Parse(this.row.GetElement(num2++)));
				item.rotation.sizeL = new Vector3(float.Parse(this.row.GetElement(num2++)), float.Parse(this.row.GetElement(num2++)), float.Parse(this.row.GetElement(num2++)));
				item.scale.sizeS = new Vector3(float.Parse(this.row.GetElement(num2++)), float.Parse(this.row.GetElement(num2++)), float.Parse(this.row.GetElement(num2++)));
				item.scale.sizeM = new Vector3(float.Parse(this.row.GetElement(num2++)), float.Parse(this.row.GetElement(num2++)), float.Parse(this.row.GetElement(num2++)));
				item.scale.sizeL = new Vector3(float.Parse(this.row.GetElement(num2++)), float.Parse(this.row.GetElement(num2++)), float.Parse(this.row.GetElement(num2++)));
				array[num / 4].Add(this.row.GetElement(num2++) == "1");
				array2[num / 4].Add(this.row.GetElement(num2++) == "1");
				this.lstsRef[num / 4].Add(item);
			}
		}
		for (int l = 0; l < 2; l++)
		{
			this.SetDynamicBoneRotationCalc(this.bonePtns[l], array[l]);
			this.SetDynamicBoneMoveLimitFlag(this.bonePtns[l], array2[l]);
		}
		this.isInit = true;
		return true;
	}

	// Token: 0x06004F71 RID: 20337 RVA: 0x001E8CC0 File Offset: 0x001E70C0
	public bool Proc()
	{
		if (!this.isInit)
		{
			return false;
		}
		float shapeBodyValue = this.chaFemale.GetShapeBodyValue(1);
		for (int i = 0; i < 2; i++)
		{
			if (this.lstsTrans[i].Count == this.lstsRef[i].Count)
			{
				for (int j = 0; j < this.lstsRef[i].Count; j++)
				{
					if (!(this.lstsTrans[i][j] == null))
					{
						this.lstsTrans[i][j].localPosition = this.lstsRef[i][j].position.Calc(shapeBodyValue);
						this.lstsTrans[i][j].localRotation = Quaternion.Euler(this.lstsRef[i][j].rotation.Calc(shapeBodyValue));
						this.lstsTrans[i][j].localScale = this.lstsRef[i][j].scale.Calc(shapeBodyValue);
					}
				}
			}
		}
		return true;
	}

	// Token: 0x06004F72 RID: 20338 RVA: 0x001E8DF0 File Offset: 0x001E71F0
	public bool InitDynamicBoneReferenceBone()
	{
		if (!this.isInit)
		{
			return false;
		}
		for (int i = 0; i < 2; i++)
		{
			this.InitDynamicBoneReferenceBone(this.bonePtns[i], this.lstsTrans[i]);
		}
		return true;
	}

	// Token: 0x06004F73 RID: 20339 RVA: 0x001E8E34 File Offset: 0x001E7234
	private bool InitDynamicBoneReferenceBone(DynamicBone_Ver02.BonePtn _ptn, List<Transform> _lstTrans)
	{
		if (_ptn == null || _lstTrans == null)
		{
			return false;
		}
		List<bool> lstBool = new List<bool>
		{
			false,
			true,
			false,
			true,
			false
		};
		this.SetDynamicBoneRotationCalc(_ptn, lstBool);
		List<bool> lstBool2 = new List<bool>
		{
			false,
			false,
			false,
			false,
			false
		};
		this.SetDynamicBoneMoveLimitFlag(_ptn, lstBool2);
		foreach (Transform transform in _lstTrans)
		{
			if (!(transform == null))
			{
				transform.localPosition = Vector3.zero;
				transform.localRotation = Quaternion.identity;
				transform.localScale = Vector3.one;
			}
		}
		return true;
	}

	// Token: 0x06004F74 RID: 20340 RVA: 0x001E8F30 File Offset: 0x001E7330
	private bool SetDynamicBoneRotationCalc(DynamicBone_Ver02.BonePtn _ptn, List<bool> _lstBool)
	{
		if (_ptn == null || _lstBool == null)
		{
			return false;
		}
		if (_lstBool.Count != _ptn.Params.Count)
		{
			return false;
		}
		for (int i = 0; i < _ptn.Params.Count; i++)
		{
			_ptn.Params[i].IsRotationCalc = _lstBool[i];
		}
		int num = 0;
		while (num < _ptn.ParticlePtns.Count && num < _lstBool.Count)
		{
			_ptn.ParticlePtns[num].IsRotationCalc = _lstBool[num];
			num++;
		}
		return true;
	}

	// Token: 0x06004F75 RID: 20341 RVA: 0x001E8FD8 File Offset: 0x001E73D8
	private bool SetDynamicBoneMoveLimitFlag(DynamicBone_Ver02.BonePtn _ptn, List<bool> _lstBool)
	{
		if (_ptn == null || _lstBool == null)
		{
			return false;
		}
		if (_lstBool.Count != _ptn.Params.Count)
		{
			return false;
		}
		for (int i = 0; i < _ptn.Params.Count; i++)
		{
			_ptn.Params[i].IsMoveLimit = _lstBool[i];
		}
		int num = 0;
		while (num < _ptn.ParticlePtns.Count && num < _lstBool.Count)
		{
			_ptn.ParticlePtns[num].IsMoveLimit = _lstBool[num];
			num++;
		}
		return true;
	}

	// Token: 0x0400486D RID: 18541
	private bool isInit;

	// Token: 0x0400486E RID: 18542
	private ChaControl chaFemale;

	// Token: 0x0400486F RID: 18543
	private DynamicBone_Ver02.BonePtn[] bonePtns = new DynamicBone_Ver02.BonePtn[2];

	// Token: 0x04004870 RID: 18544
	private List<Transform>[] lstsTrans = new List<Transform>[2];

	// Token: 0x04004871 RID: 18545
	private List<string> row = new List<string>();

	// Token: 0x04004872 RID: 18546
	private StringBuilder sbAssetName = new StringBuilder();

	// Token: 0x04004873 RID: 18547
	private List<DynamicBoneReferenceCtrl.Reference>[] lstsRef = new List<DynamicBoneReferenceCtrl.Reference>[2];

	// Token: 0x02000A7C RID: 2684
	private struct RateInfo
	{
		// Token: 0x06004F76 RID: 20342 RVA: 0x001E9080 File Offset: 0x001E7480
		public Vector3 Calc(float _rate)
		{
			return (_rate < 0.5f) ? Vector3.Lerp(this.sizeS, this.sizeM, Mathf.InverseLerp(0f, 0.5f, _rate)) : Vector3.Lerp(this.sizeM, this.sizeL, Mathf.InverseLerp(0.5f, 1f, _rate));
		}

		// Token: 0x04004874 RID: 18548
		public Vector3 sizeS;

		// Token: 0x04004875 RID: 18549
		public Vector3 sizeM;

		// Token: 0x04004876 RID: 18550
		public Vector3 sizeL;
	}

	// Token: 0x02000A7D RID: 2685
	private struct Reference
	{
		// Token: 0x04004877 RID: 18551
		public DynamicBoneReferenceCtrl.RateInfo position;

		// Token: 0x04004878 RID: 18552
		public DynamicBoneReferenceCtrl.RateInfo rotation;

		// Token: 0x04004879 RID: 18553
		public DynamicBoneReferenceCtrl.RateInfo scale;
	}
}
