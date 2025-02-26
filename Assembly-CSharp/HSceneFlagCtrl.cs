using System;
using System.Collections.Generic;
using AIProject;
using Cinemachine;
using Manager;
using UnityEngine;

// Token: 0x02000AD5 RID: 2773
public class HSceneFlagCtrl : Singleton<HSceneFlagCtrl>
{
	// Token: 0x17000EED RID: 3821
	// (get) Token: 0x0600514F RID: 20815 RVA: 0x00211E68 File Offset: 0x00210268
	// (set) Token: 0x06005150 RID: 20816 RVA: 0x00211E70 File Offset: 0x00210270
	public HScene.AnimationListInfo selectAnimationListInfo
	{
		get
		{
			return this._selectAnimationListInfo;
		}
		set
		{
			this._selectAnimationListInfo = value;
		}
	}

	// Token: 0x17000EEE RID: 3822
	// (get) Token: 0x06005151 RID: 20817 RVA: 0x00211E79 File Offset: 0x00210279
	// (set) Token: 0x06005152 RID: 20818 RVA: 0x00211E81 File Offset: 0x00210281
	public Dictionary<int, int> ChangeParams { get; private set; } = new Dictionary<int, int>();

	// Token: 0x06005153 RID: 20819 RVA: 0x00211E8C File Offset: 0x0021028C
	public void AddOrgasm()
	{
		this.numOrgasmTotal = Mathf.Clamp(this.numOrgasmTotal + 1, 0, 999999);
		if (this.numOrgasmTotal == this.gotoFaintnessCount)
		{
			if (Singleton<HSceneManager>.Instance.isForce)
			{
				this.AddParam(4, 0);
			}
			else
			{
				this.AddParam(6, 0);
			}
		}
	}

	// Token: 0x06005154 RID: 20820 RVA: 0x00211EE8 File Offset: 0x002102E8
	public void AddParam(int ptn, int mode)
	{
		ParameterPacket parameterPacket = new ParameterPacket();
		if (mode == 0)
		{
			if (!Singleton<Manager.Resources>.Instance.HSceneTable.HBaseParamTable.TryGetValue(ptn, out parameterPacket))
			{
				return;
			}
		}
		else if (mode == 1 && !Singleton<Manager.Resources>.Instance.HSceneTable.HactionParamTable.TryGetValue(ptn, out parameterPacket))
		{
			return;
		}
		if (this.endAdd[mode].Contains(ptn))
		{
			return;
		}
		this.endAdd[mode].Add(ptn);
		foreach (KeyValuePair<int, TriThreshold> keyValuePair in parameterPacket.Parameters)
		{
			int num = Mathf.RoundToInt(UnityEngine.Random.Range(keyValuePair.Value.SThreshold, keyValuePair.Value.LThreshold));
			if (!this.ChangeParams.ContainsKey(keyValuePair.Key))
			{
				this.ChangeParams.Add(keyValuePair.Key, num);
			}
			else
			{
				Dictionary<int, int> changeParams;
				int key;
				(changeParams = this.ChangeParams)[key = keyValuePair.Key] = changeParams[key] + num;
			}
		}
	}

	// Token: 0x06005155 RID: 20821 RVA: 0x00212030 File Offset: 0x00210430
	public void AddSkileParam(int id)
	{
		Dictionary<int, float> dictionary = new Dictionary<int, float>();
		if (!Singleton<Manager.Resources>.Instance.HSceneTable.HSkileParamTable.TryGetValue(id, out dictionary))
		{
			return;
		}
		if (this.endAddSkil.Contains(id))
		{
			return;
		}
		this.endAddSkil.Add(id);
		foreach (KeyValuePair<int, float> keyValuePair in dictionary)
		{
			if (keyValuePair.Key != Manager.Resources.HSceneTables.HTagTable["ゲージ"])
			{
				int num = Mathf.RoundToInt(keyValuePair.Value);
				if (!this.ChangeParams.ContainsKey(keyValuePair.Key))
				{
					this.ChangeParams.Add(keyValuePair.Key, num);
				}
				else
				{
					Dictionary<int, int> changeParams;
					int key;
					(changeParams = this.ChangeParams)[key = keyValuePair.Key] = changeParams[key] + num;
				}
			}
		}
	}

	// Token: 0x06005156 RID: 20822 RVA: 0x00212140 File Offset: 0x00210540
	public float SkilChangeSpeed(int id)
	{
		Dictionary<int, float> dictionary = new Dictionary<int, float>();
		if (!Singleton<Manager.Resources>.Instance.HSceneTable.HSkileParamTable.TryGetValue(id, out dictionary))
		{
			return 1f;
		}
		foreach (KeyValuePair<int, float> keyValuePair in dictionary)
		{
			if (keyValuePair.Key == Manager.Resources.HSceneTables.HTagTable["ゲージ"])
			{
				return keyValuePair.Value;
			}
		}
		return 1f;
	}

	// Token: 0x06005157 RID: 20823 RVA: 0x002121EC File Offset: 0x002105EC
	private void Start()
	{
		this.Init();
	}

	// Token: 0x06005158 RID: 20824 RVA: 0x002121F4 File Offset: 0x002105F4
	public void Init()
	{
		this.isNotCtrl = true;
		this.isFaintness = false;
		this.lstSyncAnimLayers[0, 0] = new List<int>();
		this.lstSyncAnimLayers[0, 1] = new List<int>();
		this.lstSyncAnimLayers[1, 0] = new List<int>();
		this.lstSyncAnimLayers[1, 1] = new List<int>();
		this.voice.playShorts = new int[]
		{
			-1,
			-1
		};
	}

	// Token: 0x06005159 RID: 20825 RVA: 0x00212270 File Offset: 0x00210670
	private void OnDisable()
	{
		this.EndProc();
	}

	// Token: 0x0600515A RID: 20826 RVA: 0x00212278 File Offset: 0x00210678
	public void EndProc()
	{
		this.bFutanari = false;
		this.BeforeHWait = true;
		this.nowAnimationInfo = new HScene.AnimationListInfo();
		this.selectAnimationListInfo = null;
		this.feel_f = 0f;
		this.feel_m = 0f;
		this.initiative = 0;
		this.isLeaveItToYou = false;
		this.isAutoActionChange = false;
		this.isGaugeHit = false;
		this.isGaugeHit_M = false;
		this.nowSpeedStateFast = false;
		this.speed = 0f;
		for (int i = 0; i < this.motions.Length; i++)
		{
			this.motions[i] = 0f;
		}
		this.nPlace = 0;
		this.HPointID = 0;
		if (this.nowHPoint != null)
		{
			ChangeHItem componentInChildren = this.nowHPoint.GetComponentInChildren<ChangeHItem>();
			if (componentInChildren != null)
			{
				componentInChildren.ChangeActive(true);
			}
		}
		this.nowHPoint = null;
		this.stopFeelFemal = false;
		this.stopFeelMale = false;
		this.isFaintness = false;
		this.rateTuya = 0f;
		this.rateNip = 0f;
		this.numOrgasm = 0;
		this.numOrgasmTotal = 0;
		this.numOrgasmFemalePlayer = 0;
		this.numSameOrgasm = 0;
		this.numInside = 0;
		this.numOutSide = 0;
		this.numDrink = 0;
		this.numVomit = 0;
		this.numAibu = 0;
		this.numHoushi = 0;
		this.numSonyu = 0;
		this.numLes = 0;
		this.numUrine = 0;
		this.numFaintness = 0;
		this.numKokan = 0;
		this.numAnal = 0;
		this.numLeadFemale = 0;
		this.isJudgeSelect = new List<HSceneFlagCtrl.JudgeSelect>();
		this.isHoushiFinish = false;
		this.isNotCtrl = true;
		this.isPainActionParam = false;
		this.isPainAction = false;
		this.isConstraintAction = false;
		this.nowOrgasm = false;
		this.voice.MemberInit();
		this.ChangeParams.Clear();
		this.endAdd[0].Clear();
		this.endAdd[1].Clear();
		this.endAddSkil.Clear();
		for (int j = 0; j < 2; j++)
		{
			for (int k = 0; k < 2; k++)
			{
				if (this.lstSyncAnimLayers[j, k] != null)
				{
					this.lstSyncAnimLayers[j, k].Clear();
				}
			}
		}
	}

	// Token: 0x0600515B RID: 20827 RVA: 0x002124BC File Offset: 0x002108BC
	public void MapHVoiceInit()
	{
		int agentMax = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.AgentMax;
		if (this.MapHvoices == null)
		{
			this.MapHvoices = new Dictionary<int, HSceneFlagCtrl.VoiceFlag>();
		}
		if (this.MapHVoiceParent == null)
		{
			this.MapHVoiceParent = new GameObject("MapHVoiceCtrls");
			this.MapHVoiceParent.transform.SetParent(base.transform);
		}
		for (int i = 0; i < agentMax; i++)
		{
			if (!this.ctrlMapHVoice.ContainsKey(i) || !(this.ctrlMapHVoice[i] != null))
			{
				this.ctrlMapHVoice.Add(i, new GameObject(string.Format("MapHVoiceCtrl_{0}", i)).AddComponent<HVoiceCtrl>());
				this.ctrlMapHVoice[i].transform.SetParent(this.MapHVoiceParent.transform);
				this.ctrlMapHVoice[i].MapHID = i;
				this.ctrlMapHVoice[i].ctrlFlag = this;
			}
		}
	}

	// Token: 0x0600515C RID: 20828 RVA: 0x002125D8 File Offset: 0x002109D8
	public int AddMapHvoices()
	{
		int num = 0;
		if (this.MapHvoices.ContainsKey(num))
		{
			int num2 = 0;
			while (this.MapHvoices.ContainsKey(num2))
			{
				num2++;
			}
			num = num2;
		}
		this.MapHvoices.Add(num, new HSceneFlagCtrl.VoiceFlag());
		this.MapHvoices[num].MemberInit();
		if (this.MapHVoiceParent == null)
		{
			this.MapHVoiceParent = new GameObject("MapHVoiceCtrls");
			this.MapHVoiceParent.transform.SetParent(base.transform);
		}
		if (!this.ctrlMapHVoice.ContainsKey(num))
		{
			this.ctrlMapHVoice.Add(num, new GameObject(string.Format("MapHVoiceCtrl_{0}", num)).AddComponent<HVoiceCtrl>());
			this.ctrlMapHVoice[num].transform.SetParent(this.MapHVoiceParent.transform);
			this.ctrlMapHVoice[num].MapHID = num;
			this.ctrlMapHVoice[num].ctrlFlag = this;
		}
		return num;
	}

	// Token: 0x0600515D RID: 20829 RVA: 0x002126EC File Offset: 0x00210AEC
	public void RemoveMapHvoices(int mapHID)
	{
		bool flag = this.MapHvoices.ContainsKey(mapHID);
		if (flag)
		{
			for (int i = 0; i < this.MapHvoices[mapHID].lstUseAsset.Count; i++)
			{
				AssetBundleManager.UnloadAssetBundle(this.MapHvoices[mapHID].lstUseAsset[i], true, null, false);
			}
			this.MapHvoices.Remove(mapHID);
		}
	}

	// Token: 0x0600515E RID: 20830 RVA: 0x0021275F File Offset: 0x00210B5F
	public void AddMapSyncAnimLayer(int addID)
	{
		this.lstMapSyncAnimLayers.Add(addID, new List<int>());
	}

	// Token: 0x0600515F RID: 20831 RVA: 0x00212774 File Offset: 0x00210B74
	public void RemoveMapSyncAnimLayer(int mapHID)
	{
		bool flag = this.lstMapSyncAnimLayers.ContainsKey(mapHID);
		if (flag)
		{
			this.lstMapSyncAnimLayers[mapHID].Clear();
			this.lstMapSyncAnimLayers.Remove(mapHID);
		}
	}

	// Token: 0x04004B30 RID: 19248
	public readonly int gotoFaintnessCount = 3;

	// Token: 0x04004B31 RID: 19249
	[Range(0f, 2f)]
	public float speed;

	// Token: 0x04004B32 RID: 19250
	[Tooltip("0:弱ループ 1:強ループ -1:その他")]
	public int loopType;

	// Token: 0x04004B33 RID: 19251
	[Range(0f, 1f)]
	public float[] motions;

	// Token: 0x04004B34 RID: 19252
	[Tooltip("現在の場所を示すID")]
	public int nPlace;

	// Token: 0x04004B35 RID: 19253
	[Tooltip("Hポイントのマーカーを識別するID")]
	public int HPointID;

	// Token: 0x04004B36 RID: 19254
	public HPoint nowHPoint;

	// Token: 0x04004B37 RID: 19255
	[Tooltip("0:なし\u30001:メタボール\u30002:パーティクル")]
	public int semenType = 1;

	// Token: 0x04004B38 RID: 19256
	public bool bFutanari;

	// Token: 0x04004B39 RID: 19257
	public bool BeforeHWait = true;

	// Token: 0x04004B3A RID: 19258
	public float HBeforeHouchiTime;

	// Token: 0x04004B3B RID: 19259
	public float StartHouchiTime;

	// Token: 0x04004B3C RID: 19260
	public bool pointMoveAnimChange;

	// Token: 0x04004B3D RID: 19261
	[RangeIntLabel("夜這いがバレる確率", 0, 100)]
	[Tooltip("0～100％で(小数不可)")]
	public int YobaiBareRate = 50;

	// Token: 0x04004B3E RID: 19262
	public bool nowOrgasm;

	// Token: 0x04004B3F RID: 19263
	public float changeMotionTimeMin;

	// Token: 0x04004B40 RID: 19264
	public float changeMotionTimeMax;

	// Token: 0x04004B41 RID: 19265
	public AnimationCurve changeMotionCurve;

	// Token: 0x04004B42 RID: 19266
	public float changeMotionMinRate;

	// Token: 0x04004B43 RID: 19267
	[Range(0f, 1f)]
	public float feel_f;

	// Token: 0x04004B44 RID: 19268
	[Range(0f, 1f)]
	public float feel_m;

	// Token: 0x04004B45 RID: 19269
	[Range(0f, 1f)]
	public float speedGuageRate = 0.01f;

	// Token: 0x04004B46 RID: 19270
	[Tooltip("ホイール一回でどれだけ回したことにするか")]
	public float wheelActionCount = 0.05f;

	// Token: 0x04004B47 RID: 19271
	[Tooltip("どの汁を制御するか")]
	public string ctrlSiru;

	// Token: 0x04004B48 RID: 19272
	public bool isInsert;

	// Token: 0x04004B49 RID: 19273
	public float changeAutoMotionTimeMin;

	// Token: 0x04004B4A RID: 19274
	public float changeAutoMotionTimeMax;

	// Token: 0x04004B4B RID: 19275
	[Range(0f, 1f)]
	public float guageDecreaseRate;

	// Token: 0x04004B4C RID: 19276
	[Range(0f, 1f)]
	public float feelSpnking;

	// Token: 0x04004B4D RID: 19277
	public float timeMasturbationChangeSpeed;

	// Token: 0x04004B4E RID: 19278
	public List<int>[,] lstSyncAnimLayers = new List<int>[2, 2];

	// Token: 0x04004B4F RID: 19279
	public Dictionary<int, List<int>> lstMapSyncAnimLayers = new Dictionary<int, List<int>>();

	// Token: 0x04004B50 RID: 19280
	public VirtualCameraController cameraCtrl;

	// Token: 0x04004B51 RID: 19281
	public CinemachineVirtualCamera HBeforeCamera;

	// Token: 0x04004B52 RID: 19282
	public int categoryMotionList = -1;

	// Token: 0x04004B53 RID: 19283
	private HScene.AnimationListInfo _selectAnimationListInfo;

	// Token: 0x04004B54 RID: 19284
	[SerializeField]
	public HScene.AnimationListInfo nowAnimationInfo = new HScene.AnimationListInfo();

	// Token: 0x04004B55 RID: 19285
	public bool stopFeelFemal;

	// Token: 0x04004B56 RID: 19286
	public bool stopFeelMale;

	// Token: 0x04004B57 RID: 19287
	public HSceneFlagCtrl.ClickKind click;

	// Token: 0x04004B58 RID: 19288
	public bool isFaintness;

	// Token: 0x04004B59 RID: 19289
	public int initiative;

	// Token: 0x04004B5A RID: 19290
	public bool isLeaveItToYou;

	// Token: 0x04004B5B RID: 19291
	public bool isAutoActionChange;

	// Token: 0x04004B5C RID: 19292
	public bool isGaugeHit;

	// Token: 0x04004B5D RID: 19293
	public bool isGaugeHit_M;

	// Token: 0x04004B5E RID: 19294
	public bool nowSpeedStateFast;

	// Token: 0x04004B5F RID: 19295
	[Range(0f, 1f)]
	public float rateTuya;

	// Token: 0x04004B60 RID: 19296
	[Range(0f, 1f)]
	public float rateNipMax = 0.3f;

	// Token: 0x04004B61 RID: 19297
	[Range(0f, 1f)]
	public float rateNip;

	// Token: 0x04004B62 RID: 19298
	public HSceneFlagCtrl.HousingID[] HousingAreaID;

	// Token: 0x04004B64 RID: 19300
	private List<int>[] endAdd = new List<int>[]
	{
		new List<int>(),
		new List<int>()
	};

	// Token: 0x04004B65 RID: 19301
	private List<int> endAddSkil = new List<int>();

	// Token: 0x04004B66 RID: 19302
	public int numOrgasm;

	// Token: 0x04004B67 RID: 19303
	public int numOrgasmTotal;

	// Token: 0x04004B68 RID: 19304
	public int numOrgasmFemalePlayer;

	// Token: 0x04004B69 RID: 19305
	[Tooltip("通常絶頂回数もカウントされます 同時絶頂したら、こいつも+1、絶頂回数も+1、中出しか外出しも+1")]
	public int numSameOrgasm;

	// Token: 0x04004B6A RID: 19306
	public int numInside;

	// Token: 0x04004B6B RID: 19307
	public int numOutSide;

	// Token: 0x04004B6C RID: 19308
	public int numDrink;

	// Token: 0x04004B6D RID: 19309
	public int numVomit;

	// Token: 0x04004B6E RID: 19310
	public int numAibu;

	// Token: 0x04004B6F RID: 19311
	public int numHoushi;

	// Token: 0x04004B70 RID: 19312
	public int numSonyu;

	// Token: 0x04004B71 RID: 19313
	public int numLes;

	// Token: 0x04004B72 RID: 19314
	public int numMulti;

	// Token: 0x04004B73 RID: 19315
	public int numUrine;

	// Token: 0x04004B74 RID: 19316
	public int numFaintness;

	// Token: 0x04004B75 RID: 19317
	public int numKokan;

	// Token: 0x04004B76 RID: 19318
	public int numAnal;

	// Token: 0x04004B77 RID: 19319
	public int numLeadFemale;

	// Token: 0x04004B78 RID: 19320
	[EnumFlags]
	public List<HSceneFlagCtrl.JudgeSelect> isJudgeSelect = new List<HSceneFlagCtrl.JudgeSelect>();

	// Token: 0x04004B79 RID: 19321
	public bool isHoushiFinish;

	// Token: 0x04004B7A RID: 19322
	public bool isNotCtrl = true;

	// Token: 0x04004B7B RID: 19323
	public bool isPainActionParam;

	// Token: 0x04004B7C RID: 19324
	public bool isPainAction;

	// Token: 0x04004B7D RID: 19325
	public bool isConstraintAction;

	// Token: 0x04004B7E RID: 19326
	public bool isItemRelease;

	// Token: 0x04004B7F RID: 19327
	public bool isFemaleNaked;

	// Token: 0x04004B80 RID: 19328
	public bool isToilet;

	// Token: 0x04004B81 RID: 19329
	public HSceneFlagCtrl.VoiceFlag voice = new HSceneFlagCtrl.VoiceFlag();

	// Token: 0x04004B82 RID: 19330
	public Dictionary<int, HSceneFlagCtrl.VoiceFlag> MapHvoices = new Dictionary<int, HSceneFlagCtrl.VoiceFlag>();

	// Token: 0x04004B83 RID: 19331
	public Dictionary<int, HVoiceCtrl> ctrlMapHVoice = new Dictionary<int, HVoiceCtrl>();

	// Token: 0x04004B84 RID: 19332
	private GameObject MapHVoiceParent;

	// Token: 0x02000AD6 RID: 2774
	public enum ClickKind
	{
		// Token: 0x04004B86 RID: 19334
		None = -1,
		// Token: 0x04004B87 RID: 19335
		FinishBefore,
		// Token: 0x04004B88 RID: 19336
		FinishInSide,
		// Token: 0x04004B89 RID: 19337
		FinishOutSide,
		// Token: 0x04004B8A RID: 19338
		FinishSame,
		// Token: 0x04004B8B RID: 19339
		FinishDrink,
		// Token: 0x04004B8C RID: 19340
		FinishVomit,
		// Token: 0x04004B8D RID: 19341
		RecoverFaintness,
		// Token: 0x04004B8E RID: 19342
		Spnking,
		// Token: 0x04004B8F RID: 19343
		PeepingRestart,
		// Token: 0x04004B90 RID: 19344
		LeaveItToYou,
		// Token: 0x04004B91 RID: 19345
		SceneEnd
	}

	// Token: 0x02000AD7 RID: 2775
	[Flags]
	public enum JudgeSelect
	{
		// Token: 0x04004B93 RID: 19347
		Kiss = 1,
		// Token: 0x04004B94 RID: 19348
		Kokan = 2,
		// Token: 0x04004B95 RID: 19349
		Breast = 4,
		// Token: 0x04004B96 RID: 19350
		Anal = 8,
		// Token: 0x04004B97 RID: 19351
		Pain = 16,
		// Token: 0x04004B98 RID: 19352
		Constraint = 32
	}

	// Token: 0x02000AD8 RID: 2776
	[Serializable]
	public class VoiceFlag
	{
		// Token: 0x06005161 RID: 20833 RVA: 0x00212818 File Offset: 0x00210C18
		public void MemberInit()
		{
			this.playVoices = new bool[2];
			this.playShorts = new int[]
			{
				-1,
				-1
			};
			this.oldFinish = -1;
			this.playStart = -1;
			this.dialog = false;
			this.urines = new bool[2];
			this.sleep = false;
			this.voiceTrs = new Transform[2];
			this.lstUseAsset = new List<string>();
		}

		// Token: 0x04004B99 RID: 19353
		public bool[] playVoices = new bool[2];

		// Token: 0x04004B9A RID: 19354
		public int[] playShorts = new int[]
		{
			-1,
			-1
		};

		// Token: 0x04004B9B RID: 19355
		public int oldFinish = -1;

		// Token: 0x04004B9C RID: 19356
		public int playStart = -1;

		// Token: 0x04004B9D RID: 19357
		public bool dialog;

		// Token: 0x04004B9E RID: 19358
		public bool[] urines = new bool[2];

		// Token: 0x04004B9F RID: 19359
		public bool sleep;

		// Token: 0x04004BA0 RID: 19360
		public Transform[] voiceTrs = new Transform[2];

		// Token: 0x04004BA1 RID: 19361
		public List<string> lstUseAsset = new List<string>();
	}

	// Token: 0x02000AD9 RID: 2777
	[Serializable]
	public struct HousingID
	{
		// Token: 0x04004BA2 RID: 19362
		public int mapID;

		// Token: 0x04004BA3 RID: 19363
		public int[] areaID;
	}
}
