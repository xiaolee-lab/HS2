using System;
using System.Collections.Generic;
using AIChara;
using AIProject;

// Token: 0x020006C7 RID: 1735
[Serializable]
public class YureCtrlEx
{
	// Token: 0x1700060B RID: 1547
	// (get) Token: 0x06002929 RID: 10537 RVA: 0x000F1E26 File Offset: 0x000F0226
	public Dictionary<string, YureCtrl.Info> dicInfo { get; } = new Dictionary<string, YureCtrl.Info>();

	// Token: 0x1700060C RID: 1548
	// (get) Token: 0x0600292A RID: 10538 RVA: 0x000F1E2E File Offset: 0x000F022E
	private static bool[] _defaultYure { get; } = new bool[]
	{
		true,
		true,
		true,
		true,
		true,
		true,
		true
	};

	// Token: 0x0600292B RID: 10539 RVA: 0x000F1E38 File Offset: 0x000F0238
	public bool Init(ChaControl _female)
	{
		this.chara = _female;
		for (int i = 0; i < this._shapeInfo.Length; i++)
		{
			this._shapeInfo[i].MemberInit();
		}
		this.isInit = false;
		return true;
	}

	// Token: 0x0600292C RID: 10540 RVA: 0x000F1E7E File Offset: 0x000F027E
	public bool Release()
	{
		this.isInit = false;
		this.dicInfo.Clear();
		return true;
	}

	// Token: 0x1700060D RID: 1549
	// (get) Token: 0x0600292D RID: 10541 RVA: 0x000F1E93 File Offset: 0x000F0293
	private static int[] _bustIndexes { get; } = new int[]
	{
		2,
		3,
		4,
		5,
		6,
		7,
		8
	};

	// Token: 0x0600292E RID: 10542 RVA: 0x000F1E9C File Offset: 0x000F029C
	public bool Load(string bundle, string asset)
	{
		this.isInit = false;
		this.dicInfo.Clear();
		if (this.chara != null)
		{
			this.ResetShape();
		}
		if (asset.IsNullOrEmpty())
		{
			return false;
		}
		MotionBustCtrlData asset2 = AssetBundleManager.LoadAsset(bundle, asset, typeof(MotionBustCtrlData), null).GetAsset<MotionBustCtrlData>();
		this.LoadParam(asset2.param);
		AssetBundleManager.UnloadAssetBundle(bundle, true, null, false);
		this.isInit = true;
		return true;
	}

	// Token: 0x0600292F RID: 10543 RVA: 0x000F1F18 File Offset: 0x000F0318
	public bool LoadParam(List<MotionBustCtrlData.Param> _param)
	{
		foreach (MotionBustCtrlData.Param param in _param)
		{
			if (!param.State.IsNullOrEmpty())
			{
				string state = param.State;
				YureCtrl.Info info;
				if (!this.dicInfo.TryGetValue(state, out info))
				{
					info = (this.dicInfo[state] = new YureCtrl.Info());
				}
				int num = 0;
				info.aIsActive[0] = (param.Parameters.GetElement(num++) == "1");
				info.aBreastShape[0].MemberInit();
				for (int i = 0; i < YureCtrlEx._bustIndexes.Length; i++)
				{
					info.aBreastShape[0].breast[i] = (param.Parameters.GetElement(num++) == "1");
				}
				info.aBreastShape[0].nip = (param.Parameters.GetElement(num++) == "1");
				info.aIsActive[1] = (param.Parameters.GetElement(num++) == "1");
				info.aBreastShape[1].MemberInit();
				for (int j = 0; j < YureCtrlEx._bustIndexes.Length; j++)
				{
					info.aBreastShape[1].breast[j] = (param.Parameters.GetElement(num++) == "1");
				}
				info.aBreastShape[1].nip = (param.Parameters.GetElement(num++) == "1");
				info.aIsActive[2] = (param.Parameters.GetElement(num++) == "1");
				info.aIsActive[3] = (param.Parameters.GetElement(num++) == "1");
			}
		}
		return true;
	}

	// Token: 0x06002930 RID: 10544 RVA: 0x000F2160 File Offset: 0x000F0560
	public bool Proc(string _animation)
	{
		if (!this.isInit)
		{
			return false;
		}
		YureCtrl.Info info;
		if (!this.dicInfo.TryGetValue(_animation, out info))
		{
			return false;
		}
		this.Active(info);
		this.Shape(info);
		return true;
	}

	// Token: 0x06002931 RID: 10545 RVA: 0x000F21A0 File Offset: 0x000F05A0
	private void Active(YureCtrl.Info info)
	{
		bool[] aIsActive = info.aIsActive;
		for (int i = 0; i < aIsActive.Length; i++)
		{
			this.chara.playDynamicBoneBust(i, aIsActive[i]);
		}
	}

	// Token: 0x06002932 RID: 10546 RVA: 0x000F21D8 File Offset: 0x000F05D8
	private void Shape(YureCtrl.Info info)
	{
		YureCtrl.BreastShapeInfo[] aBreastShape = info.aBreastShape;
		for (int i = 0; i < 2; i++)
		{
			YureCtrl.BreastShapeInfo breastShapeInfo = aBreastShape[i];
			YureCtrl.BreastShapeInfo breastShapeInfo2 = this._shapeInfo[i];
			for (int j = 0; j < breastShapeInfo.breast.Length; j++)
			{
				bool flag = breastShapeInfo.breast[j];
				bool flag2 = breastShapeInfo2.breast[j];
				if (flag != flag2)
				{
					if (flag)
					{
						this.chara.DisableShapeBodyID((i != 0) ? 1 : 0, j, false);
					}
					else
					{
						this.chara.DisableShapeBodyID((i != 0) ? 1 : 0, j, true);
					}
				}
				this._shapeInfo[i].breast[j] = flag;
			}
			if (breastShapeInfo.nip != breastShapeInfo2.nip)
			{
				if (breastShapeInfo.nip)
				{
					this.chara.DisableShapeBodyID((i != 0) ? 1 : 0, 7, false);
				}
				else
				{
					this.chara.DisableShapeBodyID((i != 0) ? 1 : 0, 7, false);
				}
				this._shapeInfo[i].nip = breastShapeInfo.nip;
			}
		}
	}

	// Token: 0x06002933 RID: 10547 RVA: 0x000F2324 File Offset: 0x000F0724
	public void ResetShape()
	{
		for (int i = 0; i < 4; i++)
		{
			this.chara.playDynamicBoneBust(i, true);
		}
		for (int j = 0; j < 2; j++)
		{
			YureCtrl.BreastShapeInfo breastShapeInfo = this._shapeInfo[j];
			for (int k = 0; k < YureCtrlEx._defaultYure.Length; k++)
			{
				bool flag = YureCtrlEx._defaultYure[k];
				bool flag2 = breastShapeInfo.breast[k];
				if (flag != flag2)
				{
					if (flag)
					{
						this.chara.DisableShapeBodyID((j != 0) ? 1 : 0, k, false);
					}
					else
					{
						this.chara.DisableShapeBodyID((j != 0) ? 1 : 0, k, true);
					}
				}
				this._shapeInfo[j].breast[k] = flag;
			}
			if (!breastShapeInfo.nip)
			{
				this.chara.DisableShapeBodyID((j != 0) ? 1 : 0, 7, false);
				this._shapeInfo[j].nip = true;
			}
		}
	}

	// Token: 0x04002A6A RID: 10858
	public bool isInit;

	// Token: 0x04002A6B RID: 10859
	private ChaControl chara;

	// Token: 0x04002A6D RID: 10861
	private const bool _defaultNip = true;

	// Token: 0x04002A6E RID: 10862
	private YureCtrl.BreastShapeInfo[] _shapeInfo = new YureCtrl.BreastShapeInfo[2];
}
