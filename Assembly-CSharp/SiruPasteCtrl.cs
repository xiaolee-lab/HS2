using System;
using System.Collections.Generic;
using System.Text;
using AIChara;
using AIProject;
using Manager;
using UnityEngine;

// Token: 0x02000AB6 RID: 2742
public class SiruPasteCtrl : MonoBehaviour
{
	// Token: 0x0600507F RID: 20607 RVA: 0x001F677C File Offset: 0x001F4B7C
	public bool Init(ChaControl _female)
	{
		this.abName = new StringBuilder();
		this.asset = new StringBuilder();
		this.Release();
		this.chaFemale = _female;
		return true;
	}

	// Token: 0x06005080 RID: 20608 RVA: 0x001F67A2 File Offset: 0x001F4BA2
	public void Release()
	{
		this.lstPaste.Clear();
	}

	// Token: 0x06005081 RID: 20609 RVA: 0x001F67B0 File Offset: 0x001F4BB0
	public bool Load(string _assetpath, string _file, int _id)
	{
		this.lstPaste.Clear();
		if (_file == string.Empty)
		{
			return false;
		}
		List<string> assetBundleNameListFromPath = CommonLib.GetAssetBundleNameListFromPath(_assetpath, false);
		assetBundleNameListFromPath.Sort();
		this.asset.Clear();
		this.asset.Append(_file);
		this.excelData = null;
		for (int i = 0; i < assetBundleNameListFromPath.Count; i++)
		{
			this.abName.Clear();
			this.abName.Append(assetBundleNameListFromPath[i]);
			if (GlobalMethod.AssetFileExist(this.abName.ToString(), this.asset.ToString(), string.Empty))
			{
				this.excelData = CommonLib.LoadAsset<ExcelData>(this.abName.ToString(), this.asset.ToString(), false, string.Empty);
				Singleton<HSceneManager>.Instance.hashUseAssetBundle.Add(this.abName.ToString());
				if (!(this.excelData == null))
				{
					int j = 1;
					while (j < this.excelData.MaxCell)
					{
						this.row = this.excelData.list[j++].list;
						int num = 1;
						SiruPasteCtrl.PasteInfo pasteInfo = new SiruPasteCtrl.PasteInfo();
						pasteInfo.anim = this.row.GetElement(num++);
						pasteInfo.timing = new SiruPasteCtrl.Timing();
						pasteInfo.timing.normalizeTime = float.Parse(this.row.GetElement(num++));
						this.astr = this.row.GetElement(num++).Split(new char[]
						{
							','
						});
						for (int k = 0; k < this.astr.Length; k++)
						{
							int item = 0;
							if (!int.TryParse(this.astr[k], out item))
							{
								item = 0;
							}
							pasteInfo.timing.lstWhere.Add(item);
						}
						this.lstPaste.Add(pasteInfo);
					}
				}
			}
		}
		this.oldFrame = 0f;
		this.oldHash = 0;
		return true;
	}

	// Token: 0x06005082 RID: 20610 RVA: 0x001F69E0 File Offset: 0x001F4DE0
	public bool Proc(AnimatorStateInfo _ai)
	{
		if (this.oldHash != _ai.shortNameHash)
		{
			this.oldFrame = 0f;
		}
		this.oldHash = _ai.shortNameHash;
		for (int i = 0; i < this.lstPaste.Count; i++)
		{
			this.p = this.lstPaste[i];
			if (_ai.IsName(this.p.anim))
			{
				float num = _ai.normalizedTime % 1f;
				this.ti = this.p.timing;
				if (this.oldFrame <= this.ti.normalizeTime && this.ti.normalizeTime < num)
				{
					for (int j = 0; j < this.ti.lstWhere.Count; j++)
					{
						this.sp = (ChaFileDefine.SiruParts)this.ti.lstWhere[j];
						this.chaFemale.SetSiruFlag(this.sp, (byte)Mathf.Clamp((int)(this.chaFemale.GetSiruFlag(this.sp) + 1), 0, 2));
					}
				}
			}
		}
		this.oldFrame = _ai.normalizedTime;
		return true;
	}

	// Token: 0x04004A08 RID: 18952
	private ExcelData excelData;

	// Token: 0x04004A09 RID: 18953
	private List<string> row = new List<string>();

	// Token: 0x04004A0A RID: 18954
	[SerializeField]
	private List<SiruPasteCtrl.PasteInfo> lstPaste = new List<SiruPasteCtrl.PasteInfo>();

	// Token: 0x04004A0B RID: 18955
	[DisabledGroup("女クラス")]
	[SerializeField]
	private ChaControl chaFemale;

	// Token: 0x04004A0C RID: 18956
	private float oldFrame;

	// Token: 0x04004A0D RID: 18957
	private int oldHash;

	// Token: 0x04004A0E RID: 18958
	private SiruPasteCtrl.PasteInfo p;

	// Token: 0x04004A0F RID: 18959
	private ChaFileDefine.SiruParts sp;

	// Token: 0x04004A10 RID: 18960
	private SiruPasteCtrl.Timing ti;

	// Token: 0x04004A11 RID: 18961
	private StringBuilder abName;

	// Token: 0x04004A12 RID: 18962
	private StringBuilder asset;

	// Token: 0x04004A13 RID: 18963
	private string[] astr;

	// Token: 0x02000AB7 RID: 2743
	[Serializable]
	public class Timing
	{
		// Token: 0x04004A14 RID: 18964
		[Label("タイミング")]
		public float normalizeTime;

		// Token: 0x04004A15 RID: 18965
		public List<int> lstWhere = new List<int>();
	}

	// Token: 0x02000AB8 RID: 2744
	[Serializable]
	public class PasteInfo
	{
		// Token: 0x04004A16 RID: 18966
		[Label("アニメーション名")]
		public string anim = string.Empty;

		// Token: 0x04004A17 RID: 18967
		public SiruPasteCtrl.Timing timing;
	}
}
