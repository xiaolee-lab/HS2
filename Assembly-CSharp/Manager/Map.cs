using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using AIChara;
using AIProject;
using AIProject.Animal;
using AIProject.Definitions;
using AIProject.MiniGames.Fishing;
using AIProject.Player;
using AIProject.SaveData;
using AIProject.Scene;
using ConfigScene;
using IllusionUtility.GetUtility;
using PlaceholderSoftware.WetStuff;
using ReMotion;
using Sound;
using UniRx;
using UnityEngine;
using UnityEngine.AI;
using UnityEx;
using UnityEx.Misc;

namespace Manager
{
	// Token: 0x020008E9 RID: 2281
	public class Map : Singleton<Map>
	{
		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x06003D0F RID: 15631 RVA: 0x00164C21 File Offset: 0x00163021
		// (set) Token: 0x06003D10 RID: 15632 RVA: 0x00164C29 File Offset: 0x00163029
		public EnvironmentSimulator Simulator { get; private set; }

		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x06003D11 RID: 15633 RVA: 0x00164C32 File Offset: 0x00163032
		// (set) Token: 0x06003D12 RID: 15634 RVA: 0x00164C3A File Offset: 0x0016303A
		public ReadOnlyDictionary<int, Actor> ActorTable { get; set; }

		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x06003D13 RID: 15635 RVA: 0x00164C43 File Offset: 0x00163043
		// (set) Token: 0x06003D14 RID: 15636 RVA: 0x00164C4B File Offset: 0x0016304B
		public List<Actor> Actors { get; private set; } = new List<Actor>();

		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x06003D15 RID: 15637 RVA: 0x00164C54 File Offset: 0x00163054
		// (set) Token: 0x06003D16 RID: 15638 RVA: 0x00164C5C File Offset: 0x0016305C
		public PlayerActor Player { get; private set; }

		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x06003D17 RID: 15639 RVA: 0x00164C65 File Offset: 0x00163065
		// (set) Token: 0x06003D18 RID: 15640 RVA: 0x00164C6D File Offset: 0x0016306D
		public ReadOnlyDictionary<int, AgentActor> AgentTable { get; private set; }

		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x06003D19 RID: 15641 RVA: 0x00164C76 File Offset: 0x00163076
		// (set) Token: 0x06003D1A RID: 15642 RVA: 0x00164C7E File Offset: 0x0016307E
		public IReadOnlyDictionary<int, ChaFile> AgentChaFileTable { get; private set; }

		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x06003D1B RID: 15643 RVA: 0x00164C87 File Offset: 0x00163087
		// (set) Token: 0x06003D1C RID: 15644 RVA: 0x00164C8F File Offset: 0x0016308F
		public MerchantActor Merchant { get; private set; }

		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x06003D1D RID: 15645 RVA: 0x00164C98 File Offset: 0x00163098
		// (set) Token: 0x06003D1E RID: 15646 RVA: 0x00164CA0 File Offset: 0x001630A0
		public AgentActor TutorialAgent { get; set; }

		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x06003D1F RID: 15647 RVA: 0x00164CAC File Offset: 0x001630AC
		public static bool TutorialMode
		{
			get
			{
				int tutorialProgress = Map.GetTutorialProgress();
				return tutorialProgress < 16;
			}
		}

		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x06003D20 RID: 15648 RVA: 0x00164CC4 File Offset: 0x001630C4
		// (set) Token: 0x06003D21 RID: 15649 RVA: 0x00164CCC File Offset: 0x001630CC
		public Dictionary<int, UnityEx.ValueTuple<GameObject, EnvArea3DSE, EnvLineArea3DSE>> EnviroSETable { get; private set; }

		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x06003D22 RID: 15650 RVA: 0x00164CD5 File Offset: 0x001630D5
		// (set) Token: 0x06003D23 RID: 15651 RVA: 0x00164CDD File Offset: 0x001630DD
		public Dictionary<int, System.Tuple<Transform, AudioReverbZone[]>> ReverbTable { get; private set; }

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x06003D24 RID: 15652 RVA: 0x00164CE8 File Offset: 0x001630E8
		public Transform EnviroGroupRoot
		{
			get
			{
				if (this._enviroGroupRoot != null)
				{
					return this._enviroGroupRoot;
				}
				this._enviroGroupRoot = new GameObject("Enviro Group Root").transform;
				this._enviroGroupRoot.SetParent(this._mapRoot, false);
				return this._enviroGroupRoot;
			}
		}

		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x06003D25 RID: 15653 RVA: 0x00164D3A File Offset: 0x0016313A
		// (set) Token: 0x06003D26 RID: 15654 RVA: 0x00164D42 File Offset: 0x00163142
		public Dictionary<int, Transform> EnviroRootElement { get; private set; }

		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x06003D27 RID: 15655 RVA: 0x00164D4B File Offset: 0x0016314B
		public Dictionary<int, UnityEx.ValueTuple<bool, List<Env3DSEPoint>>> HousingEnvSEPointTable { get; } = new Dictionary<int, UnityEx.ValueTuple<bool, List<Env3DSEPoint>>>();

		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x06003D28 RID: 15656 RVA: 0x00164D53 File Offset: 0x00163153
		// (set) Token: 0x06003D29 RID: 15657 RVA: 0x00164D5B File Offset: 0x0016315B
		public Dictionary<int, Dictionary<bool, ForcedHideObject[]>> AreaOpenObjectTable { get; private set; } = new Dictionary<int, Dictionary<bool, ForcedHideObject[]>>();

		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x06003D2A RID: 15658 RVA: 0x00164D64 File Offset: 0x00163164
		// (set) Token: 0x06003D2B RID: 15659 RVA: 0x00164D6C File Offset: 0x0016316C
		public Dictionary<int, Dictionary<int, Dictionary<bool, Dictionary<int, UnityEx.ValueTuple<GameObject, Material, float, Color>[]>>>> TimeRelationObjectTable { get; private set; } = new Dictionary<int, Dictionary<int, Dictionary<bool, Dictionary<int, UnityEx.ValueTuple<GameObject, Material, float, Color>[]>>>>();

		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x06003D2C RID: 15660 RVA: 0x00164D75 File Offset: 0x00163175
		public EnvironmentProfile EnvironmentProfile
		{
			[CompilerGenerated]
			get
			{
				EnvironmentSimulator simulator = this.Simulator;
				return (simulator != null) ? simulator.EnvironmentProfile : null;
			}
		}

		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x06003D2D RID: 15661 RVA: 0x00164D8C File Offset: 0x0016318C
		// (set) Token: 0x06003D2E RID: 15662 RVA: 0x00164D94 File Offset: 0x00163194
		public Transform MapRoot
		{
			get
			{
				return this._mapRoot;
			}
			set
			{
				this._mapRoot = value;
				this._pointAgent = value.GetComponentInChildren<PointManager>(true);
			}
		}

		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x06003D2F RID: 15663 RVA: 0x00164DAA File Offset: 0x001631AA
		// (set) Token: 0x06003D30 RID: 15664 RVA: 0x00164DB2 File Offset: 0x001631B2
		public GameObject WaterObject
		{
			get
			{
				return this._waterObject;
			}
			set
			{
				this._waterObject = value;
			}
		}

		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x06003D31 RID: 15665 RVA: 0x00164DBB File Offset: 0x001631BB
		// (set) Token: 0x06003D32 RID: 15666 RVA: 0x00164DC3 File Offset: 0x001631C3
		public AQUAS_Reflection[] WaterRefrections { get; set; }

		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x06003D33 RID: 15667 RVA: 0x00164DCC File Offset: 0x001631CC
		// (set) Token: 0x06003D34 RID: 15668 RVA: 0x00164DD4 File Offset: 0x001631D4
		public Transform PlayerStartPoint { get; set; }

		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x06003D35 RID: 15669 RVA: 0x00164DDD File Offset: 0x001631DD
		// (set) Token: 0x06003D36 RID: 15670 RVA: 0x00164DE5 File Offset: 0x001631E5
		public Transform ActorRoot { get; set; }

		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x06003D37 RID: 15671 RVA: 0x00164DEE File Offset: 0x001631EE
		// (set) Token: 0x06003D38 RID: 15672 RVA: 0x00164DF6 File Offset: 0x001631F6
		public NavMeshSurface NavMeshSurface { get; set; }

		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x06003D39 RID: 15673 RVA: 0x00164DFF File Offset: 0x001631FF
		// (set) Token: 0x06003D3A RID: 15674 RVA: 0x00164E07 File Offset: 0x00163207
		public GameObject NavMeshSrc { get; set; }

		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x06003D3B RID: 15675 RVA: 0x00164E10 File Offset: 0x00163210
		// (set) Token: 0x06003D3C RID: 15676 RVA: 0x00164E18 File Offset: 0x00163218
		public GameObject ChunkSrc { get; set; }

		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x06003D3D RID: 15677 RVA: 0x00164E21 File Offset: 0x00163221
		public PointManager PointAgent
		{
			[CompilerGenerated]
			get
			{
				return this._pointAgent;
			}
		}

		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x06003D3E RID: 15678 RVA: 0x00164E29 File Offset: 0x00163229
		// (set) Token: 0x06003D3F RID: 15679 RVA: 0x00164E31 File Offset: 0x00163231
		public int MapID { get; private set; }

		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x06003D40 RID: 15680 RVA: 0x00164E3A File Offset: 0x0016323A
		// (set) Token: 0x06003D41 RID: 15681 RVA: 0x00164E42 File Offset: 0x00163242
		public int AccessDeviceID { get; set; } = -1;

		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x06003D42 RID: 15682 RVA: 0x00164E4B File Offset: 0x0016324B
		// (set) Token: 0x06003D43 RID: 15683 RVA: 0x00164E53 File Offset: 0x00163253
		public Dictionary<int, Dictionary<int, Transform>> EventStartPointDic { get; set; } = new Dictionary<int, Dictionary<int, Transform>>();

		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x06003D44 RID: 15684 RVA: 0x00164E5C File Offset: 0x0016325C
		// (set) Token: 0x06003D45 RID: 15685 RVA: 0x00164E64 File Offset: 0x00163264
		public Dictionary<int, string> ChangedCharaFiles { get; set; } = new Dictionary<int, string>();

		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x06003D46 RID: 15686 RVA: 0x00164E6D File Offset: 0x0016326D
		// (set) Token: 0x06003D47 RID: 15687 RVA: 0x00164E75 File Offset: 0x00163275
		public Dictionary<int, Transform> HousingPointTable { get; set; } = new Dictionary<int, Transform>();

		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x06003D48 RID: 15688 RVA: 0x00164E7E File Offset: 0x0016327E
		// (set) Token: 0x06003D49 RID: 15689 RVA: 0x00164E86 File Offset: 0x00163286
		public int HousingID { get; set; }

		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x06003D4A RID: 15690 RVA: 0x00164E8F File Offset: 0x0016328F
		// (set) Token: 0x06003D4B RID: 15691 RVA: 0x00164E97 File Offset: 0x00163297
		public int HousingAreaID { get; set; }

		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x06003D4C RID: 15692 RVA: 0x00164EA0 File Offset: 0x001632A0
		// (set) Token: 0x06003D4D RID: 15693 RVA: 0x00164EA8 File Offset: 0x001632A8
		public Dictionary<int, List<Transform>> HousingRecoveryPointTable { get; set; } = new Dictionary<int, List<Transform>>();

		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x06003D4E RID: 15694 RVA: 0x00164EB1 File Offset: 0x001632B1
		// (set) Token: 0x06003D4F RID: 15695 RVA: 0x00164EB9 File Offset: 0x001632B9
		public Dictionary<int, Dictionary<int, List<WarpPoint>>> WarpPointDic { get; set; } = new Dictionary<int, Dictionary<int, List<WarpPoint>>>();

		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x06003D50 RID: 15696 RVA: 0x00164EC2 File Offset: 0x001632C2
		// (set) Token: 0x06003D51 RID: 15697 RVA: 0x00164ECA File Offset: 0x001632CA
		public FishingManager FishingSystem { get; set; }

		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x06003D52 RID: 15698 RVA: 0x00164ED3 File Offset: 0x001632D3
		public Dictionary<int, Chunk> ChunkTable { get; } = new Dictionary<int, Chunk>();

		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x06003D53 RID: 15699 RVA: 0x00164EDB File Offset: 0x001632DB
		// (set) Token: 0x06003D54 RID: 15700 RVA: 0x00164EE3 File Offset: 0x001632E3
		public Dictionary<int, GameObject> MapGroupObjList { get; private set; } = new Dictionary<int, GameObject>();

		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x06003D55 RID: 15701 RVA: 0x00164EEC File Offset: 0x001632EC
		// (set) Token: 0x06003D56 RID: 15702 RVA: 0x00164EF4 File Offset: 0x001632F4
		public Dictionary<int, UnityEx.ValueTuple<Desire.ActionType, Desire.ActionType, bool>> AgentModeCache { get; set; } = new Dictionary<int, UnityEx.ValueTuple<Desire.ActionType, Desire.ActionType, bool>>();

		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x06003D57 RID: 15703 RVA: 0x00164F00 File Offset: 0x00163300
		public bool IsHour7After
		{
			[CompilerGenerated]
			get
			{
				return this.Simulator.Now.Hour >= 7;
			}
		}

		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x06003D58 RID: 15704 RVA: 0x00164F26 File Offset: 0x00163326
		// (set) Token: 0x06003D59 RID: 15705 RVA: 0x00164F2E File Offset: 0x0016332E
		public List<TimeLinkedLightObject> TimeLinkedLightObjectList { get; private set; } = new List<TimeLinkedLightObject>();

		// Token: 0x06003D5A RID: 15706 RVA: 0x00164F37 File Offset: 0x00163337
		protected override void Awake()
		{
			if (!base.CheckInstance())
			{
				return;
			}
			this.ActorTable = new ReadOnlyDictionary<int, Actor>(this._actorTable);
			this.AgentTable = new ReadOnlyDictionary<int, AgentActor>(this._agentTable);
			this.AgentChaFileTable = this._agentChaFileTable;
		}

		// Token: 0x06003D5B RID: 15707 RVA: 0x00164F73 File Offset: 0x00163373
		private void Start()
		{
		}

		// Token: 0x06003D5C RID: 15708 RVA: 0x00164F78 File Offset: 0x00163378
		private void FixedUpdate()
		{
			LayerMask areaDetectionLayer = Singleton<Resources>.Instance.DefinePack.MapDefines.AreaDetectionLayer;
			foreach (Actor actor in this.Actors)
			{
				RaycastHit raycastHit;
				if (Physics.Raycast(actor.Position + Vector3.up * 5f, Vector3.down, out raycastHit, 1000f, areaDetectionLayer))
				{
					bool flag = false;
					foreach (KeyValuePair<int, Chunk> keyValuePair in this.ChunkTable)
					{
						foreach (MapArea mapArea in keyValuePair.Value.MapAreas)
						{
							if (flag = mapArea.ContainsCollider(raycastHit.collider))
							{
								actor.MapArea = mapArea;
								actor.ChunkID = mapArea.ChunkID;
								actor.AreaID = mapArea.AreaID;
								break;
							}
						}
						if (flag)
						{
							break;
						}
					}
					if (!flag)
					{
						actor.MapArea = null;
					}
				}
				else
				{
					actor.MapArea = null;
				}
			}
			if (this.Player != null)
			{
				this.AreaTypeUpdate(this.Player);
				if (this.Player.MapArea != null)
				{
					this.Player.PlayerData.AreaID = this.Player.MapArea.AreaID;
				}
			}
			if (this.Merchant != null)
			{
				this.AreaTypeUpdate(this.Merchant);
			}
			if (!this._agentTable.IsNullOrEmpty<int, AgentActor>())
			{
				foreach (KeyValuePair<int, AgentActor> keyValuePair2 in this._agentTable)
				{
					if (!(keyValuePair2.Value == null))
					{
						this.AreaTypeUpdate(keyValuePair2.Value);
						if (!(keyValuePair2.Value.MapArea == null))
						{
							keyValuePair2.Value.AgentData.AreaID = keyValuePair2.Value.MapArea.AreaID;
						}
					}
				}
			}
		}

		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x06003D5D RID: 15709 RVA: 0x00165240 File Offset: 0x00163640
		// (set) Token: 0x06003D5E RID: 15710 RVA: 0x00165248 File Offset: 0x00163648
		public bool UpdateTexSetting { get; set; } = true;

		// Token: 0x06003D5F RID: 15711 RVA: 0x00165254 File Offset: 0x00163654
		private void Update()
		{
			GraphicSystem graphicData = Config.GraphicData;
			if (graphicData != null)
			{
				bool selfShadow = graphicData.SelfShadow;
				int qualityLevel = QualitySettings.GetQualityLevel();
				int num = qualityLevel / 2 * 2 + ((!selfShadow) ? 1 : 0);
				if (num != qualityLevel)
				{
					QualitySettings.SetQualityLevel(num);
				}
				if (this.UpdateTexSetting)
				{
					byte mapGraphicQuality = graphicData.MapGraphicQuality;
					if (QualitySettings.masterTextureLimit != (int)mapGraphicQuality)
					{
						QualitySettings.masterTextureLimit = (int)mapGraphicQuality;
					}
				}
			}
			PlayerActor player = this.Player;
			if (player == null)
			{
				return;
			}
			if (this.Simulator != null)
			{
				DecalSettings settings = player.CameraControl.WetDecal.Settings;
				float num2 = 0f;
				Weather weather = this.Simulator.Weather;
				if (weather != Weather.Rain)
				{
					if (weather != Weather.Storm)
					{
						settings.Saturation = Mathf.SmoothDamp(settings.Saturation, 0f, ref num2, 1f);
					}
					else
					{
						settings.Saturation = Mathf.SmoothDamp(settings.Saturation, 1f, ref num2, 0.3f);
					}
				}
				else
				{
					settings.Saturation = Mathf.SmoothDamp(settings.Saturation, 1f, ref num2, 0.5f);
				}
			}
			if (Singleton<Game>.IsInstance() && Singleton<Game>.Instance.Environment != null)
			{
				foreach (KeyValuePair<int, List<AIProject.SaveData.Environment.PlantInfo>> keyValuePair in Singleton<Game>.Instance.Environment.FarmlandTable)
				{
					foreach (AIProject.SaveData.Environment.PlantInfo plantInfo in keyValuePair.Value)
					{
						if (plantInfo != null)
						{
							plantInfo.AddTimer(Time.unscaledDeltaTime);
						}
					}
				}
			}
			if (Singleton<Game>.IsInstance() && Singleton<Game>.Instance.Environment != null)
			{
				Dictionary<int, RecyclingData> recyclingDataTable = Singleton<Game>.Instance.Environment.RecyclingDataTable;
				if (!recyclingDataTable.IsNullOrEmpty<int, RecyclingData>())
				{
					foreach (KeyValuePair<int, RecyclingData> keyValuePair2 in recyclingDataTable)
					{
						RecyclingData value = keyValuePair2.Value;
						if (value != null && value.CreateCountEnabled)
						{
							value.CreateCounter += Time.unscaledDeltaTime;
						}
					}
				}
			}
			if (Singleton<Game>.IsInstance() && Singleton<Game>.Instance.Environment != null)
			{
				Dictionary<int, AIProject.SaveData.Environment.SearchActionInfo> searchActionLockTable = Singleton<Game>.Instance.Environment.SearchActionLockTable;
				foreach (KeyValuePair<int, AIProject.SaveData.Environment.SearchActionInfo> keyValuePair3 in searchActionLockTable)
				{
					AIProject.SaveData.Environment.SearchActionInfo value2 = keyValuePair3.Value;
					if (value2.Count != 0)
					{
						value2.ElapsedTime += Time.unscaledDeltaTime;
						if (value2.ElapsedTime > this.EnvironmentProfile.SearchCoolTimeDuration)
						{
							value2.ElapsedTime = 0f;
							value2.Count = 0;
						}
					}
				}
			}
			float talkLockDuration = Singleton<Resources>.Instance.AgentProfile.TalkLockDuration;
			float effectiveDynamicBoneDistance = Singleton<Resources>.Instance.LocomotionProfile.EffectiveDynamicBoneDistance;
			float charaVisibleDistance = Singleton<Resources>.Instance.LocomotionProfile.CharaVisibleDistance;
			Transform transform = player.CameraControl.transform;
			foreach (int key in this._agentKeys)
			{
				AgentActor agentActor;
				if (this._agentTable.TryGetValue(key, out agentActor))
				{
					if (agentActor.AgentData.LockTalk)
					{
						agentActor.AgentData.TalkElapsedTime += Time.unscaledDeltaTime;
						if (agentActor.AgentData.TalkElapsedTime > talkLockDuration)
						{
							agentActor.AgentData.LockTalk = false;
							agentActor.AgentData.TalkElapsedTime = 0f;
							agentActor.AgentData.TalkMotivation = agentActor.AgentData.StatsTable[5];
						}
					}
					agentActor.AgentData.CallCTCount += Time.unscaledDeltaTime;
					if (agentActor.AgentData.IsPlayerForBirthdayEvent && !player.IsBirthday(null))
					{
						agentActor.AgentData.IsPlayerForBirthdayEvent = false;
					}
					Dictionary<int, AIProject.SaveData.Environment.SearchActionInfo> searchActionLockTable2 = agentActor.AgentData.SearchActionLockTable;
					foreach (KeyValuePair<int, AIProject.SaveData.Environment.SearchActionInfo> keyValuePair4 in searchActionLockTable2)
					{
						AIProject.SaveData.Environment.SearchActionInfo value3 = keyValuePair4.Value;
						if (value3.Count != 0)
						{
							value3.ElapsedTime += Time.deltaTime;
							if (value3.ElapsedTime > this.EnvironmentProfile.SearchCoolTimeDuration)
							{
								value3.ElapsedTime = 0f;
								value3.Count = 0;
							}
						}
					}
					StatusProfile statusProfile = Singleton<Resources>.Instance.StatusProfile;
					if (agentActor.AgentData.ColdLockInfo.Lock)
					{
						SickLockInfo coldLockInfo = agentActor.AgentData.ColdLockInfo;
						coldLockInfo.ElapsedTime += Time.deltaTime;
						if (coldLockInfo.ElapsedTime > statusProfile.ColdLockDuration)
						{
							coldLockInfo.Lock = false;
							coldLockInfo.ElapsedTime = 0f;
						}
					}
					if (agentActor.AgentData.HeatStrokeLockInfo.Lock)
					{
						SickLockInfo heatStrokeLockInfo = agentActor.AgentData.HeatStrokeLockInfo;
						heatStrokeLockInfo.ElapsedTime += Time.deltaTime;
						if (heatStrokeLockInfo.ElapsedTime > statusProfile.HeatStrokeLockDuration)
						{
							heatStrokeLockInfo.Lock = false;
							heatStrokeLockInfo.ElapsedTime = 0f;
						}
					}
					if (agentActor.enabled && agentActor.Controller.enabled && agentActor.AnimationAgent.enabled)
					{
						if (!this._agentVanishAreaList.IsNullOrEmpty<int>())
						{
							agentActor.Visible = !this._agentVanishAreaList.Contains(agentActor.AreaID);
						}
						else
						{
							agentActor.Visible = true;
						}
						agentActor.ChaControl.visibleAll = (agentActor.Visible && agentActor.IsVisible && agentActor.IsVisibleDistanceAll(transform));
						if (!(player.PlayerController.State is Sex))
						{
							bool flag = agentActor.ChaControl.fileGameInfo.flavorState[2] >= Singleton<Resources>.Instance.StatusProfile.LampEquipableBorder;
							bool flag2 = this.Simulator.TimeZone == AIProject.TimeZone.Night;
							agentActor.ChaControl.ShowExtraAccessory(ChaControlDefine.ExtraAccessoryParts.Waist, flag && flag2);
						}
						ItemScrounge itemScrounge = agentActor.AgentData.ItemScrounge;
						itemScrounge.AddTimer(Time.deltaTime);
						if (itemScrounge.isEnd)
						{
							itemScrounge.Reset();
							FlavorSkill.Type[] source = new FlavorSkill.Type[]
							{
								FlavorSkill.Type.Reliability,
								FlavorSkill.Type.Sociability,
								FlavorSkill.Type.Reason
							};
							int num3 = (int)source.Shuffle<FlavorSkill.Type>().First<FlavorSkill.Type>();
							agentActor.AgentData.SetFlavorSkill(num3, agentActor.ChaControl.fileGameInfo.flavorState[num3] - 20);
							FlavorSkill.Type[] source2 = new FlavorSkill.Type[]
							{
								FlavorSkill.Type.Darkness,
								FlavorSkill.Type.Wariness,
								FlavorSkill.Type.Instinct
							};
							int num4 = (int)source2.Shuffle<FlavorSkill.Type>().First<FlavorSkill.Type>();
							agentActor.AgentData.SetFlavorSkill(num4, agentActor.ChaControl.fileGameInfo.flavorState[num4] + 20);
							int num5 = 1;
							agentActor.SetStatus(num5, agentActor.AgentData.StatsTable[num5] - 30f);
							int id = 7;
							agentActor.SetStatus(id, (float)(agentActor.ChaControl.fileGameInfo.morality - 5));
						}
					}
				}
			}
			MerchantActor merchant = this.Merchant;
			if (merchant != null && merchant.enabled && merchant.Controller.enabled && merchant.AnimationMerchant.enabled)
			{
				merchant.ChaControl.visibleAll = (merchant.IsVisible && merchant.IsVisibleDistanceAll(transform));
			}
		}

		// Token: 0x06003D60 RID: 15712 RVA: 0x00165B50 File Offset: 0x00163F50
		public IEnumerator LoadMap(int id)
		{
			this.MapID = id;
			AssetBundleInfo mapInfo = Singleton<Resources>.Instance.Map.MapList[id];
			yield return Singleton<Scene>.Instance.LoadBaseSceneCoroutine(new Scene.Data
			{
				assetBundleName = mapInfo.assetbundle,
				levelName = mapInfo.asset,
				fadeType = Scene.Data.FadeType.None
			});
			yield break;
		}

		// Token: 0x06003D61 RID: 15713 RVA: 0x00165B74 File Offset: 0x00163F74
		public IEnumerator LoadMap(string assetBundleName, string assetName)
		{
			yield return Singleton<Scene>.Instance.LoadBaseSceneCoroutine(new Scene.Data
			{
				assetBundleName = assetBundleName,
				levelName = assetName,
				fadeType = Scene.Data.FadeType.None
			});
			yield break;
		}

		// Token: 0x06003D62 RID: 15714 RVA: 0x00165B98 File Offset: 0x00163F98
		public IEnumerator LoadEnvironment()
		{
			Resources resourceManager = Singleton<Resources>.Instance;
			string mapScenePrefabAdd = resourceManager.DefinePack.ABPaths.MapScenePrefabAdd05;
			string add = resourceManager.DefinePack.ABManifests.Add05;
			GameObject original = CommonLib.LoadAsset<GameObject>(mapScenePrefabAdd, "MapSimulation", false, add);
			MapScene.AddAssetBundlePath(mapScenePrefabAdd, add);
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
			gameObject.transform.SetParent(this._mapRoot);
			this.Simulator = gameObject.GetComponent<EnvironmentSimulator>();
			this.Simulator.EnabledTimeProgression = false;
			string mapScenePrefab = resourceManager.DefinePack.ABPaths.MapScenePrefab;
			string text = "abdata";
			GameObject original2 = CommonLib.LoadAsset<GameObject>(mapScenePrefab, "EnviroSkyGroup", false, text);
			MapScene.AddAssetBundlePath(mapScenePrefab, text);
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(original2, Vector3.zero, Quaternion.identity, this.Simulator.transform);
			EnviroSky instance = EnviroSky.instance;
			this.Simulator.EnviroSky = instance;
			this.Simulator.DefaultEnviroZone = gameObject2.GetComponentInChildren<EnviroZone>(true);
			this.Simulator.EnviroEvents = gameObject2.GetComponentInChildren<EnviroEvents>(true);
			this.Simulator.SkyDomeObject = gameObject2;
			this.Simulator.MapID = this.MapID;
			this.Simulator.OnEnableChangeTimeAsObservable().Subscribe(delegate(bool x)
			{
				this.Simulator.SetTimeProgress(x);
			});
			yield return null;
			yield break;
		}

		// Token: 0x06003D63 RID: 15715 RVA: 0x00165BB4 File Offset: 0x00163FB4
		public IEnumerator LoadDemoMap()
		{
			DefinePack.AssetBundlePathsDefine define = Singleton<Resources>.Instance.DefinePack.ABPaths;
			yield return Singleton<Scene>.Instance.LoadBaseSceneCoroutine(new Scene.Data
			{
				assetBundleName = "map/scene/demo_data.unity3d",
				levelName = "demo_data",
				fadeType = Scene.Data.FadeType.None
			});
			yield break;
		}

		// Token: 0x06003D64 RID: 15716 RVA: 0x00165BC8 File Offset: 0x00163FC8
		public IEnumerator LoadDemoEnvironment(string assetName)
		{
			GameObject original = CommonLib.LoadAsset<GameObject>("map/demo_data.unity3d", assetName, false, string.Empty);
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
			this.DemoLightObject = gameObject;
			gameObject.SetActive(true);
			EnviroSky instance = EnviroSky.instance;
			instance.Weather.weatherPresets.Clear();
			instance.Weather.WeatherPrefabs.Clear();
			EnviroZone component = EnviroSky.instance.GetComponent<EnviroZone>();
			component.zoneWeather.Clear();
			component.enabled = false;
			yield return null;
			yield break;
		}

		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x06003D65 RID: 15717 RVA: 0x00165BEA File Offset: 0x00163FEA
		// (set) Token: 0x06003D66 RID: 15718 RVA: 0x00165BF2 File Offset: 0x00163FF2
		public GameObject DemoLightObject { get; private set; }

		// Token: 0x06003D67 RID: 15719 RVA: 0x00165BFC File Offset: 0x00163FFC
		public IEnumerator LoadNavMeshSource()
		{
			if (this.NavMeshSurface == null)
			{
				yield break;
			}
			AssetBundleInfo assetBundleInfo = Singleton<Resources>.Instance.Map.NavMeshSourceList[this.MapID];
			GameObject original = CommonLib.LoadAsset<GameObject>(assetBundleInfo.assetbundle, assetBundleInfo.asset, false, assetBundleInfo.manifest);
			this.NavMeshSrc = UnityEngine.Object.Instantiate<GameObject>(original, this.NavMeshSurface.transform, false);
			yield break;
		}

		// Token: 0x06003D68 RID: 15720 RVA: 0x00165C18 File Offset: 0x00164018
		public IEnumerator LoadElements()
		{
			DefinePack.AssetBundlePathsDefine define = Singleton<Resources>.Instance.DefinePack.ABPaths;
			PointManager pointAgent = this._mapRoot.GetComponentInChildren<PointManager>(true);
			Transform apRoot = new GameObject("ActionPointRoot").transform;
			apRoot.SetParent(pointAgent.transform, false);
			pointAgent.ActionPointRoot = apRoot;
			AssetBundleInfo info = Singleton<Resources>.Instance.Map.ActionPointGroupTable[this.MapID];
			GameObject gameObject = CommonLib.LoadAsset<GameObject>(info.assetbundle, info.asset, false, info.manifest);
			if (gameObject != null)
			{
				MapScene.AddAssetBundlePath(info);
				UnityEngine.Object.Instantiate<GameObject>(gameObject, apRoot, true);
			}
			Transform bpRoot = new GameObject("BasePointRoot").transform;
			bpRoot.SetParent(pointAgent.transform);
			pointAgent.BasePointRoot = bpRoot;
			AssetBundleInfo info2;
			if (Singleton<Resources>.Instance.Map.BasePointGroupTable.TryGetValue(this.MapID, out info2))
			{
				GameObject gameObject2 = CommonLib.LoadAsset<GameObject>(info2.assetbundle, info2.asset, false, info2.manifest);
				MapScene.AddAssetBundlePath(info2);
				if (gameObject2 != null)
				{
					UnityEngine.Object.Instantiate<GameObject>(gameObject2, bpRoot, true);
				}
			}
			Transform dpRoot = new GameObject("DevicePointRoot").transform;
			dpRoot.SetParent(pointAgent.transform, false);
			pointAgent.DevicePointRoot = dpRoot;
			AssetBundleInfo info3;
			if (Singleton<Resources>.Instance.Map.DevicePointGroupTable.TryGetValue(this.MapID, out info3))
			{
				GameObject gameObject3 = CommonLib.LoadAsset<GameObject>(info3.assetbundle, info3.asset, false, info3.manifest);
				MapScene.AddAssetBundlePath(info3);
				if (gameObject3 != null)
				{
					UnityEngine.Object.Instantiate<GameObject>(gameObject3, dpRoot, true);
				}
			}
			Transform hpRoot = new GameObject("FarmPointRoot").transform;
			hpRoot.SetParent(pointAgent.transform, false);
			pointAgent.HarvestPointRoot = hpRoot;
			AssetBundleInfo info4;
			if (Singleton<Resources>.Instance.Map.HarvestPointGroupTable.TryGetValue(this.MapID, out info4))
			{
				GameObject gameObject4 = CommonLib.LoadAsset<GameObject>(info4.assetbundle, info4.asset, false, info4.manifest);
				MapScene.AddAssetBundlePath(info4);
				if (gameObject4 != null)
				{
					UnityEngine.Object.Instantiate<GameObject>(gameObject4, hpRoot, true);
				}
			}
			Transform shipRoot = new GameObject("ShipPointRoot").transform;
			shipRoot.SetParent(pointAgent.transform, false);
			pointAgent.ShipPointRoot = shipRoot;
			AssetBundleInfo info5;
			if (Singleton<Resources>.Instance.Map.ShipPointGroupTable.TryGetValue(this.MapID, out info5))
			{
				GameObject gameObject5 = CommonLib.LoadAsset<GameObject>(info5.assetbundle, info5.asset, false, info5.manifest);
				MapScene.AddAssetBundlePath(info5);
				if (gameObject5 != null)
				{
					UnityEngine.Object.Instantiate<GameObject>(gameObject5, shipRoot, true);
				}
			}
			Transform lpRoot = new GameObject("LightSwitchRoot").transform;
			lpRoot.SetParent(pointAgent.transform, false);
			pointAgent.LightSwitchPointRoot = lpRoot;
			Dictionary<int, AssetBundleInfo> dictionary;
			if (Singleton<Resources>.Instance.Map.LightSwitchPointGroupTable.TryGetValue(this.MapID, out dictionary) && !dictionary.IsNullOrEmpty<int, AssetBundleInfo>())
			{
				foreach (KeyValuePair<int, AssetBundleInfo> keyValuePair in dictionary)
				{
					AssetBundleInfo value = keyValuePair.Value;
					GameObject gameObject6 = CommonLib.LoadAsset<GameObject>(value.assetbundle, value.asset, false, value.manifest);
					MapScene.AddAssetBundlePath(value);
					if (gameObject6 != null)
					{
						UnityEngine.Object.Instantiate<GameObject>(gameObject6, lpRoot, true);
					}
				}
			}
			AssetBundleInfo info6 = Singleton<Resources>.Instance.Map.ChunkList[this.MapID];
			GameObject original = CommonLib.LoadAsset<GameObject>(info6.assetbundle, info6.asset, false, info6.manifest);
			MapScene.AddAssetBundlePath(info6);
			GameObject gameObject7 = UnityEngine.Object.Instantiate<GameObject>(original);
			this.ChunkSrc = gameObject7;
			GameObject gameObject8 = gameObject7;
			gameObject8.transform.SetParent(this._mapRoot, false);
			AssetBundleInfo info7;
			if (Singleton<Resources>.Instance.Map.SEMeshList.TryGetValue(this.MapID, out info7))
			{
				GameObject original2 = CommonLib.LoadAsset<GameObject>(info7.assetbundle, info7.asset, false, info7.manifest);
				MapScene.AddAssetBundlePath(info7);
				GameObject gameObject9 = UnityEngine.Object.Instantiate<GameObject>(original2);
				gameObject9.transform.SetParent(gameObject8.transform, false);
			}
			Singleton<Resources>.Instance.HSceneTable.SetHmesh(gameObject8.transform);
			AssetBundleInfo info8 = Singleton<Resources>.Instance.Map.CameraColliderList[this.MapID];
			GameObject original3 = CommonLib.LoadAsset<GameObject>(info8.assetbundle, info8.asset, false, info8.manifest ?? string.Empty);
			MapScene.AddAssetBundlePath(info8);
			GameObject gameObject10 = UnityEngine.Object.Instantiate<GameObject>(original3);
			gameObject10.transform.SetParent(gameObject8.transform, false);
			this._mapDataEffect = GameObject.Find("searchpoint_effect");
			yield return null;
			yield break;
		}

		// Token: 0x06003D69 RID: 15721 RVA: 0x00165C34 File Offset: 0x00164034
		public IEnumerator LoadHousingObj(int mapID = 0)
		{
			yield return Singleton<Housing>.Instance.LoadHousing(mapID);
			yield break;
		}

		// Token: 0x06003D6A RID: 15722 RVA: 0x00165C50 File Offset: 0x00164050
		public IEnumerator LoadMerchantPoint()
		{
			PointManager pointAgent = this._mapRoot.GetComponentInChildren<PointManager>(true);
			Transform mpRoot = new GameObject("MerchantPointRoot").transform;
			mpRoot.SetParent(pointAgent.transform, false);
			pointAgent.MerchantPointRoot = mpRoot;
			int chunkID = this.MapID;
			Dictionary<int, AssetBundleInfo> infos = Singleton<Resources>.Instance.Map.MerchantPointGroupTable[chunkID];
			foreach (KeyValuePair<int, AssetBundleInfo> keyValuePair in infos)
			{
				GameObject gameObject = CommonLib.LoadAsset<GameObject>(keyValuePair.Value.assetbundle, keyValuePair.Value.asset, false, keyValuePair.Value.manifest);
				if (!(gameObject == null))
				{
					MapScene.AddAssetBundlePath(keyValuePair.Value);
					UnityEngine.Object.Instantiate<GameObject>(gameObject, mpRoot, true);
				}
			}
			yield return null;
			yield break;
		}

		// Token: 0x06003D6B RID: 15723 RVA: 0x00165C6C File Offset: 0x0016406C
		public IEnumerator LoadEventPoints()
		{
			PointManager pointAgent = this._mapRoot.GetComponentInChildren<PointManager>(true);
			Transform epRoot = new GameObject("Event Point Root").transform;
			epRoot.SetParent(pointAgent.transform, false);
			pointAgent.EventPointRoot = epRoot;
			Dictionary<int, AssetBundleInfo> elementTable;
			if (Singleton<Resources>.Instance.Map.EventPointGroupTable.TryGetValue(this.MapID, out elementTable) && !elementTable.IsNullOrEmpty<int, AssetBundleInfo>())
			{
				foreach (KeyValuePair<int, AssetBundleInfo> keyValuePair in elementTable)
				{
					AssetBundleInfo value = keyValuePair.Value;
					GameObject original = CommonLib.LoadAsset<GameObject>(value.assetbundle, value.asset, false, value.manifest);
					MapScene.AddAssetBundlePath(value);
					UnityEngine.Object.Instantiate<GameObject>(original, epRoot, true);
				}
			}
			this.RefreshEventPointActive();
			yield return null;
			yield break;
		}

		// Token: 0x06003D6C RID: 15724 RVA: 0x00165C88 File Offset: 0x00164088
		public IEnumerator LoadStoryPoints()
		{
			PointManager pointAgent = this._mapRoot.GetComponentInChildren<PointManager>(true);
			if (pointAgent == null)
			{
				yield break;
			}
			Transform spRoot = new GameObject("Story Point Root").transform;
			spRoot.SetParent(pointAgent.transform, false);
			pointAgent.StoryPointRoot = spRoot;
			Dictionary<int, AssetBundleInfo> elementTable;
			if (Singleton<Resources>.Instance.Map.StoryPointGroupTable.TryGetValue(this.MapID, out elementTable) && !elementTable.IsNullOrEmpty<int, AssetBundleInfo>())
			{
				foreach (KeyValuePair<int, AssetBundleInfo> keyValuePair in elementTable)
				{
					AssetBundleInfo value = keyValuePair.Value;
					GameObject original = CommonLib.LoadAsset<GameObject>(value.assetbundle, value.asset, false, value.manifest);
					MapScene.AddAssetBundlePath(value);
					UnityEngine.Object.Instantiate<GameObject>(original, spRoot, true);
				}
			}
			yield return null;
			yield break;
		}

		// Token: 0x06003D6D RID: 15725 RVA: 0x00165CA4 File Offset: 0x001640A4
		public IEnumerator LoadEnviroObject()
		{
			if (!Singleton<Resources>.IsInstance())
			{
				yield break;
			}
			if (!this.EnviroRootElement.IsNullOrEmpty<int, Transform>())
			{
				foreach (KeyValuePair<int, Transform> keyValuePair in this.EnviroRootElement)
				{
					Transform value = keyValuePair.Value;
					if (!(value == null))
					{
						if (value.gameObject.activeSelf)
						{
							value.gameObject.SetActive(false);
						}
					}
				}
			}
			yield return this.LoadEnviroSEObject();
			yield return this.LoadReverbObject();
			yield break;
		}

		// Token: 0x06003D6E RID: 15726 RVA: 0x00165CC0 File Offset: 0x001640C0
		private Transform GetEnviroRootElement(int areaID)
		{
			if (this.EnviroRootElement == null)
			{
				this.EnviroRootElement = new Dictionary<int, Transform>();
			}
			Transform transform = null;
			if (!this.EnviroRootElement.TryGetValue(areaID, out transform) || transform == null)
			{
				transform = (this.EnviroRootElement[areaID] = new GameObject(string.Format("Enviro Root Element {0}", areaID.ToString("00"))).transform);
				transform.SetParent(this.EnviroGroupRoot, false);
			}
			return transform;
		}

		// Token: 0x06003D6F RID: 15727 RVA: 0x00165D40 File Offset: 0x00164140
		public bool ActiveEnvAreaID(int areaID)
		{
			if (this.MapID < 0 || areaID < 0)
			{
				return false;
			}
			if (!Singleton<Resources>.IsInstance())
			{
				return false;
			}
			MapArea mapArea = (!(this.Player != null)) ? null : this.Player.MapArea;
			if (mapArea == null)
			{
				return false;
			}
			int areaID2 = mapArea.AreaID;
			if (areaID == areaID2)
			{
				return true;
			}
			Dictionary<int, Dictionary<int, int[]>> enviroSEAdjacentInfoTable = Singleton<Resources>.Instance.Sound.EnviroSEAdjacentInfoTable;
			Dictionary<int, int[]> dictionary;
			int[] array;
			return !enviroSEAdjacentInfoTable.IsNullOrEmpty<int, Dictionary<int, int[]>>() && (enviroSEAdjacentInfoTable.TryGetValue(this.MapID, out dictionary) && !dictionary.IsNullOrEmpty<int, int[]>() && dictionary.TryGetValue(areaID2, out array) && !array.IsNullOrEmpty<int>()) && array.Contains(areaID);
		}

		// Token: 0x06003D70 RID: 15728 RVA: 0x00165E10 File Offset: 0x00164210
		private IEnumerator LoadEnviroSEObject()
		{
			if (!this.EnviroSETable.IsNullOrEmpty<int, UnityEx.ValueTuple<GameObject, EnvArea3DSE, EnvLineArea3DSE>>())
			{
				foreach (KeyValuePair<int, UnityEx.ValueTuple<GameObject, EnvArea3DSE, EnvLineArea3DSE>> keyValuePair in this.EnviroSETable)
				{
					GameObject item = keyValuePair.Value.Item1;
					if (item != null)
					{
						UnityEngine.Object.Destroy(item);
					}
				}
				this.EnviroSETable.Clear();
			}
			else if (this.EnviroSETable == null)
			{
				this.EnviroSETable = new Dictionary<int, UnityEx.ValueTuple<GameObject, EnvArea3DSE, EnvLineArea3DSE>>();
			}
			int chunkID = this.MapID;
			Dictionary<int, Dictionary<int, AssetBundleInfo>> enviroInfoTable = Singleton<Resources>.Instance.Sound.EnviroSEPrefabInfoTable;
			Dictionary<int, AssetBundleInfo> areaTable;
			if (!enviroInfoTable.TryGetValue(chunkID, out areaTable) || areaTable.IsNullOrEmpty<int, AssetBundleInfo>())
			{
				yield break;
			}
			foreach (KeyValuePair<int, AssetBundleInfo> keyValuePair2 in areaTable)
			{
				AssetBundleInfo value = keyValuePair2.Value;
				GameObject gameObject = CommonLib.LoadAsset<GameObject>(value.assetbundle, value.asset, false, value.manifest);
				if (!(gameObject == null))
				{
					MapScene.AddAssetBundlePath(value);
					Transform enviroRootElement = this.GetEnviroRootElement(keyValuePair2.Key);
					GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject, enviroRootElement, true);
					EnvArea3DSE component = gameObject2.GetComponent<EnvArea3DSE>();
					EnvLineArea3DSE component2 = gameObject2.GetComponent<EnvLineArea3DSE>();
					if (component != null || component2 != null)
					{
						this.EnviroSETable[keyValuePair2.Key] = new UnityEx.ValueTuple<GameObject, EnvArea3DSE, EnvLineArea3DSE>(gameObject2, component, component2);
					}
					else if (gameObject2 != null)
					{
						UnityEngine.Object.Destroy(gameObject2);
					}
				}
			}
			yield return null;
			yield break;
		}

		// Token: 0x06003D71 RID: 15729 RVA: 0x00165E2C File Offset: 0x0016422C
		private IEnumerator LoadReverbObject()
		{
			if (!this.ReverbTable.IsNullOrEmpty<int, System.Tuple<Transform, AudioReverbZone[]>>())
			{
				foreach (KeyValuePair<int, System.Tuple<Transform, AudioReverbZone[]>> keyValuePair in this.ReverbTable)
				{
					Transform item = keyValuePair.Value.Item1;
					if (!(item == null) && !(item.gameObject == null))
					{
						UnityEngine.Object.Destroy(item.gameObject);
					}
				}
				this.ReverbTable.Clear();
			}
			else if (this.ReverbTable == null)
			{
				this.ReverbTable = new Dictionary<int, System.Tuple<Transform, AudioReverbZone[]>>();
			}
			int chunkID = this.MapID;
			Dictionary<int, Dictionary<int, AssetBundleInfo>> reverbInfoTable = Singleton<Resources>.Instance.Sound.ReverbPrefabInfoTable;
			Dictionary<int, AssetBundleInfo> areaTable;
			if (!reverbInfoTable.TryGetValue(chunkID, out areaTable) || areaTable.IsNullOrEmpty<int, AssetBundleInfo>())
			{
				yield break;
			}
			foreach (KeyValuePair<int, AssetBundleInfo> keyValuePair2 in areaTable)
			{
				AssetBundleInfo value = keyValuePair2.Value;
				GameObject gameObject = CommonLib.LoadAsset<GameObject>(value.assetbundle, value.asset, false, value.manifest);
				if (!(gameObject == null))
				{
					MapScene.AddAssetBundlePath(value);
					Transform enviroRootElement = this.GetEnviroRootElement(keyValuePair2.Key);
					GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject, enviroRootElement, true);
					AudioReverbZone[] componentsInChildren = gameObject2.GetComponentsInChildren<AudioReverbZone>(true);
					if (!componentsInChildren.IsNullOrEmpty<AudioReverbZone>())
					{
						this.ReverbTable[keyValuePair2.Key] = new System.Tuple<Transform, AudioReverbZone[]>(gameObject2.transform, componentsInChildren);
					}
					else
					{
						UnityEngine.Object.Destroy(gameObject2);
					}
				}
			}
			yield return null;
			yield break;
		}

		// Token: 0x06003D72 RID: 15730 RVA: 0x00165E48 File Offset: 0x00164248
		public IEnumerator LoadAnimalPoint()
		{
			Transform apRoot = new GameObject("AnimalPointRoot").transform;
			apRoot.SetParent(this._mapRoot, false);
			PointManager pointAgent = this._mapRoot.GetComponentInChildren<PointManager>(true);
			pointAgent.AnimalPointRoot = apRoot;
			int chunkID = this.MapID;
			Dictionary<int, Dictionary<int, AssetBundleInfo>> assetInfoTable = Singleton<Resources>.Instance.AnimalTable.AnimalPointAssetTable;
			Dictionary<int, AssetBundleInfo> table;
			if (assetInfoTable.TryGetValue(chunkID, out table))
			{
				foreach (KeyValuePair<int, AssetBundleInfo> keyValuePair in table)
				{
					AssetBundleInfo value = keyValuePair.Value;
					GameObject gameObject = CommonLib.LoadAsset<GameObject>(value.assetbundle, value.asset, false, value.manifest);
					if (!(gameObject == null))
					{
						MapScene.AddAssetBundlePath(value);
						UnityEngine.Object.Instantiate<GameObject>(gameObject, apRoot, true);
					}
				}
			}
			yield return null;
			yield break;
		}

		// Token: 0x06003D73 RID: 15731 RVA: 0x00165E64 File Offset: 0x00164264
		public void StartSubscribe()
		{
			(from _ in this.Simulator.OnWeatherChangedAsObservable().TakeUntilDestroy(base.gameObject)
			where base.isActiveAndEnabled
			select _).Do(delegate(Weather weather)
			{
				this.RefreshWeather(weather);
			}).OnErrorRetry(delegate(Exception ex)
			{
				if (global::Debug.isDebugBuild)
				{
				}
			}).Subscribe<Weather>();
			(from _ in this.Simulator.OnTimeZoneChangedAsObservable().TakeUntilDestroy(base.gameObject)
			where base.isActiveAndEnabled
			select _).Do(delegate(AIProject.TimeZone timeZone)
			{
				this.RefreshTimeZone(timeZone);
			}).OnErrorRetry(delegate(Exception ex)
			{
				if (global::Debug.isDebugBuild)
				{
				}
			}).Subscribe<AIProject.TimeZone>();
			(from _ in this.Simulator.OnDayAsObservable().TakeUntilDestroy(base.gameObject)
			where base.isActiveAndEnabled
			select _).Do(delegate(TimeSpan timeSpan)
			{
				this.OnElapsedDay(timeSpan);
			}).OnErrorRetry(delegate(Exception ex)
			{
				if (global::Debug.isDebugBuild)
				{
				}
			}).Subscribe<TimeSpan>();
			(from _ in this.Simulator.OnMinuteAsObservable().TakeUntilDestroy(this.Simulator.gameObject)
			where base.isActiveAndEnabled
			select _).Do(delegate(TimeSpan timeSpan)
			{
				this.OnElapsedMinute(timeSpan);
			}).OnErrorRetry(delegate(Exception ex)
			{
				if (global::Debug.isDebugBuild)
				{
				}
			}).Subscribe<TimeSpan>();
			(from _ in this.Simulator.OnMinuteAsObservable().TakeUntilDestroy(this.Simulator.gameObject)
			where base.isActiveAndEnabled
			select _).Do(delegate(TimeSpan timeSpan)
			{
				this.Simulator.OnMinuteUpdate(timeSpan);
			}).OnErrorRetry(delegate(Exception ex)
			{
				if (global::Debug.isDebugBuild)
				{
				}
			}).Subscribe<TimeSpan>();
			(from _ in this.Simulator.OnSecondAsObservable().TakeUntilDestroy(base.gameObject)
			where base.isActiveAndEnabled
			select _).OnErrorRetry(delegate(Exception ex)
			{
				if (global::Debug.isDebugBuild)
				{
				}
			}).Subscribe(delegate(TimeSpan timeSpan)
			{
				this.OnElapsedSecond(timeSpan);
			}, delegate(Exception ex)
			{
			}, delegate()
			{
			});
			(from _ in this.Simulator.OnMapLightTimeZoneChangedAsObservable().TakeUntilDestroy(this.Simulator)
			where this.Simulator.isActiveAndEnabled
			select _).Do(delegate(AIProject.TimeZone timeZone)
			{
				this.RefreshActiveTimeRelationObjects();
			}).OnErrorRetry(delegate(Exception ex)
			{
				if (global::Debug.isDebugBuild)
				{
				}
			}).Subscribe<AIProject.TimeZone>();
			this.Player.OnMapAreaChangedAsObservable().Do(delegate(int areaID)
			{
				this.OnMapAreaChanged(areaID);
				this.ResettingEnviroAreaElement(this.MapID, areaID);
				this.RefreshHousingEnv3DSEPoints(this.MapID, areaID);
			}).Subscribe<int>();
		}

		// Token: 0x06003D74 RID: 15732 RVA: 0x00166174 File Offset: 0x00164574
		private void OnMapAreaChanged(int areaID)
		{
			int mapID = this.MapID;
			Dictionary<int, List<int>> dictionary;
			List<int> list;
			if (Singleton<Resources>.Instance.Map.MapGroupHiddenAreaList.TryGetValue(mapID, out dictionary) && dictionary.TryGetValue(areaID, out list))
			{
				foreach (KeyValuePair<int, GameObject> keyValuePair in this.MapGroupObjList)
				{
					if (!(keyValuePair.Value == null))
					{
						keyValuePair.Value.SetActive(!list.Contains(keyValuePair.Key));
					}
				}
				this.HousingPointTable[0].gameObject.SetActive(!list.Contains(100));
				this.HousingPointTable[1].gameObject.SetActive(!list.Contains(101));
				this.HousingPointTable[2].gameObject.SetActive(!list.Contains(102));
			}
			Dictionary<int, List<int>> dictionary2;
			if (Singleton<Resources>.Instance.Map.AgentHiddenAreaList.TryGetValue(mapID, out dictionary2))
			{
				if (!dictionary2.TryGetValue(areaID, out this._agentVanishAreaList))
				{
					this._agentVanishAreaList = null;
				}
			}
			else
			{
				this._agentVanishAreaList = null;
			}
			ActorCameraControl cameraControl = this.Player.CameraControl;
			int[] source;
			if (Singleton<Resources>.Instance.PlayerProfile.DisableWaterVFXAreaList.TryGetValue(mapID, out source))
			{
				bool enabled = !source.Contains(areaID);
				cameraControl.UnderWaterFX.enabled = enabled;
				cameraControl.UnderWaterBlurFX.enabled = enabled;
			}
			else
			{
				cameraControl.UnderWaterFX.enabled = true;
				cameraControl.UnderWaterBlurFX.enabled = true;
			}
		}

		// Token: 0x06003D75 RID: 15733 RVA: 0x00166344 File Offset: 0x00164744
		public void SetAgentOpenState(int id, bool openState)
		{
			if (Singleton<Game>.IsInstance())
			{
				WorldData worldData = Singleton<Game>.Instance.WorldData;
				Dictionary<int, AgentData> dictionary = (worldData != null) ? worldData.AgentTable : null;
				AgentData agentData;
				if (dictionary != null && dictionary.TryGetValue(id, out agentData) && agentData.OpenState != openState)
				{
					agentData.OpenState = openState;
					PlayerActor player = this.Player;
					player.PlayerController.CommandArea.UpdateCollision(player);
				}
			}
		}

		// Token: 0x06003D76 RID: 15734 RVA: 0x001663B4 File Offset: 0x001647B4
		public bool GetBasePointOpenState(int id, out bool flag)
		{
			WorldData worldData = Singleton<Game>.Instance.WorldData;
			Dictionary<int, Dictionary<int, bool>> dictionary;
			if (worldData == null)
			{
				dictionary = null;
			}
			else
			{
				AIProject.SaveData.Environment environment = worldData.Environment;
				dictionary = ((environment != null) ? environment.BasePointOpenState : null);
			}
			Dictionary<int, Dictionary<int, bool>> dictionary2 = dictionary;
			Dictionary<int, bool> dictionary3;
			if (!dictionary2.TryGetValue(this.MapID, out dictionary3))
			{
				Dictionary<int, bool> dictionary4 = new Dictionary<int, bool>();
				dictionary2[this.MapID] = dictionary4;
				dictionary3 = dictionary4;
				flag = false;
				return false;
			}
			return dictionary3.TryGetValue(id, out flag);
		}

		// Token: 0x06003D77 RID: 15735 RVA: 0x00166420 File Offset: 0x00164820
		public bool SetBaseOpenState(int id, bool openState)
		{
			if (Singleton<Game>.IsInstance())
			{
				WorldData worldData = Singleton<Game>.Instance.WorldData;
				Dictionary<int, Dictionary<int, bool>> dictionary;
				if (worldData == null)
				{
					dictionary = null;
				}
				else
				{
					AIProject.SaveData.Environment environment = worldData.Environment;
					dictionary = ((environment != null) ? environment.BasePointOpenState : null);
				}
				Dictionary<int, Dictionary<int, bool>> dictionary2 = dictionary;
				if (dictionary2 != null)
				{
					Dictionary<int, bool> dictionary3;
					if (!dictionary2.TryGetValue(this.MapID, out dictionary3))
					{
						Dictionary<int, bool> dictionary4 = new Dictionary<int, bool>();
						dictionary2[this.MapID] = dictionary4;
						dictionary3 = dictionary4;
					}
					bool flag;
					dictionary3.TryGetValue(id, out flag);
					if (flag != openState)
					{
						dictionary3[id] = openState;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06003D78 RID: 15736 RVA: 0x001664AC File Offset: 0x001648AC
		public bool SetBaseOpenState(int mapID, int id, bool openState)
		{
			if (Singleton<Game>.IsInstance())
			{
				WorldData worldData = Singleton<Game>.Instance.WorldData;
				Dictionary<int, Dictionary<int, bool>> dictionary;
				if (worldData == null)
				{
					dictionary = null;
				}
				else
				{
					AIProject.SaveData.Environment environment = worldData.Environment;
					dictionary = ((environment != null) ? environment.BasePointOpenState : null);
				}
				Dictionary<int, Dictionary<int, bool>> dictionary2 = dictionary;
				if (dictionary2 != null)
				{
					Dictionary<int, bool> dictionary3;
					if (!dictionary2.TryGetValue(mapID, out dictionary3))
					{
						Dictionary<int, bool> dictionary4 = new Dictionary<int, bool>();
						dictionary2[mapID] = dictionary4;
						dictionary3 = dictionary4;
					}
					bool flag;
					dictionary3.TryGetValue(id, out flag);
					if (flag != openState)
					{
						dictionary3[id] = openState;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06003D79 RID: 15737 RVA: 0x0016652C File Offset: 0x0016492C
		public void InitializeDefaultState()
		{
			this.InitializeDefaultAreaOpenID();
			this.InitializeDefaultTimeRelationStateID();
			this.RefreshTutorialOpenState();
		}

		// Token: 0x06003D7A RID: 15738 RVA: 0x00166540 File Offset: 0x00164940
		public void InitializeDefaultAreaOpenID()
		{
			if (Singleton<Resources>.IsInstance() && Singleton<Game>.IsInstance())
			{
				bool isFreeMode = Game.IsFreeMode;
				AIProject.SaveData.Environment environment = Singleton<Game>.Instance.Environment;
				Dictionary<int, bool> dictionary = (environment != null) ? environment.AreaOpenState : null;
				Dictionary<int, string> areaOpenIDTable = Singleton<Resources>.Instance.Map.AreaOpenIDTable;
				if (dictionary != null && areaOpenIDTable != null)
				{
					foreach (KeyValuePair<int, string> keyValuePair in areaOpenIDTable)
					{
						if (isFreeMode)
						{
							dictionary[keyValuePair.Key] = true;
						}
						else if (!dictionary.ContainsKey(keyValuePair.Key))
						{
							dictionary[keyValuePair.Key] = false;
						}
					}
				}
			}
		}

		// Token: 0x06003D7B RID: 15739 RVA: 0x0016661C File Offset: 0x00164A1C
		public void InitializeDefaultTimeRelationStateID()
		{
			if (Singleton<Resources>.IsInstance() && Singleton<Game>.IsInstance())
			{
				bool isFreeMode = Game.IsFreeMode;
				AIProject.SaveData.Environment environment = Singleton<Game>.Instance.Environment;
				Dictionary<int, bool> dictionary = (environment != null) ? environment.TimeObjOpenState : null;
				Dictionary<int, string> timeRelationObjectIDTable = Singleton<Resources>.Instance.Map.TimeRelationObjectIDTable;
				if (dictionary != null && timeRelationObjectIDTable != null)
				{
					foreach (KeyValuePair<int, string> keyValuePair in timeRelationObjectIDTable)
					{
						if (isFreeMode)
						{
							dictionary[keyValuePair.Key] = true;
						}
						else if (!dictionary.ContainsKey(keyValuePair.Key))
						{
							dictionary[keyValuePair.Key] = false;
						}
					}
				}
			}
		}

		// Token: 0x06003D7C RID: 15740 RVA: 0x001666F8 File Offset: 0x00164AF8
		public void SetOpenAreaState(int id, bool active)
		{
			bool flag = false;
			if (Singleton<Game>.IsInstance())
			{
				Dictionary<int, bool> dictionary = Singleton<Game>.Instance.Environment.AreaOpenState;
				if (dictionary == null)
				{
					Dictionary<int, bool> dictionary2 = new Dictionary<int, bool>();
					Singleton<Game>.Instance.Environment.AreaOpenState = dictionary2;
					dictionary = dictionary2;
				}
				bool flag2;
				flag = (!dictionary.TryGetValue(id, out flag2) || flag2 != active);
				dictionary[id] = active;
			}
			Dictionary<bool, ForcedHideObject[]> dictionary3;
			if (!this.AreaOpenObjectTable.IsNullOrEmpty<int, Dictionary<bool, ForcedHideObject[]>>() && this.AreaOpenObjectTable.TryGetValue(id, out dictionary3) && !dictionary3.IsNullOrEmpty<bool, ForcedHideObject[]>())
			{
				foreach (KeyValuePair<bool, ForcedHideObject[]> keyValuePair in dictionary3)
				{
					if (!keyValuePair.Value.IsNullOrEmpty<ForcedHideObject>())
					{
						bool key = keyValuePair.Key;
						foreach (ForcedHideObject forcedHideObject in keyValuePair.Value)
						{
							if (!(forcedHideObject == null))
							{
								bool active2 = (!key) ? (!active) : active;
								forcedHideObject.SetActive(active2);
							}
						}
					}
				}
			}
			if (this.PointAgent != null)
			{
				EventPoint[] eventPoints = this.PointAgent.EventPoints;
				if (!eventPoints.IsNullOrEmpty<EventPoint>())
				{
					foreach (EventPoint eventPoint in eventPoints)
					{
						if (!(eventPoint == null))
						{
							if (eventPoint.OpenAreaID >= 0)
							{
								if (eventPoint.OpenAreaID == id)
								{
									eventPoint.SetActive(!active);
								}
							}
						}
					}
				}
			}
			List<GameObject> list;
			if (!AreaOpenLinkedObject.Table.IsNullOrEmpty<int, List<GameObject>>() && AreaOpenLinkedObject.Table.TryGetValue(id, out list) && !list.IsNullOrEmpty<GameObject>())
			{
				foreach (GameObject gameObject in list)
				{
					if (!(gameObject == null))
					{
						if (gameObject.activeSelf != active)
						{
							gameObject.SetActive(active);
						}
					}
				}
			}
			if (flag)
			{
				if (Singleton<MapUIContainer>.IsInstance() && Singleton<MapUIContainer>.Instance.MinimapUI != null)
				{
					Singleton<MapUIContainer>.Instance.MinimapUI.RoadNaviMesh.Reflesh();
				}
				if (this.Merchant != null && active)
				{
					this.Merchant.SetOpenAreaID(this);
				}
			}
		}

		// Token: 0x06003D7D RID: 15741 RVA: 0x001669D0 File Offset: 0x00164DD0
		public bool GetOpenAreaState(int id)
		{
			if (!Singleton<Game>.IsInstance())
			{
				return false;
			}
			AIProject.SaveData.Environment environment = Singleton<Game>.Instance.Environment;
			bool? flag;
			if (environment == null)
			{
				flag = null;
			}
			else
			{
				Dictionary<int, bool> areaOpenState = environment.AreaOpenState;
				flag = ((areaOpenState != null) ? new bool?(areaOpenState.IsNullOrEmpty<int, bool>()) : null);
			}
			bool? flag2 = flag;
			bool flag3;
			return flag2 != null && !flag2.Value && Singleton<Game>.Instance.Environment.AreaOpenState.TryGetValue(id, out flag3) && flag3;
		}

		// Token: 0x06003D7E RID: 15742 RVA: 0x00166A6C File Offset: 0x00164E6C
		public void RefreshAreaOpenObject()
		{
			if (this.AreaOpenObjectTable.IsNullOrEmpty<int, Dictionary<bool, ForcedHideObject[]>>())
			{
				return;
			}
			bool flag3;
			if (Singleton<Game>.IsInstance())
			{
				AIProject.SaveData.Environment environment = Singleton<Game>.Instance.Environment;
				bool? flag;
				if (environment == null)
				{
					flag = null;
				}
				else
				{
					Dictionary<int, bool> areaOpenState = environment.AreaOpenState;
					flag = ((areaOpenState != null) ? new bool?(areaOpenState.IsNullOrEmpty<int, bool>()) : null);
				}
				bool? flag2 = flag;
				flag3 = (flag2 == null || flag2.Value);
			}
			else
			{
				flag3 = true;
			}
			bool flag4 = flag3;
			bool isFreeMode = Game.IsFreeMode;
			if (flag4)
			{
				foreach (KeyValuePair<int, Dictionary<bool, ForcedHideObject[]>> keyValuePair in this.AreaOpenObjectTable)
				{
					if (!keyValuePair.Value.IsNullOrEmpty<bool, ForcedHideObject[]>())
					{
						foreach (KeyValuePair<bool, ForcedHideObject[]> keyValuePair2 in keyValuePair.Value)
						{
							if (!keyValuePair2.Value.IsNullOrEmpty<ForcedHideObject>())
							{
								bool flag5 = keyValuePair2.Key;
								if (isFreeMode)
								{
									flag5 = !flag5;
								}
								foreach (ForcedHideObject forcedHideObject in keyValuePair2.Value)
								{
									if (!(forcedHideObject == null))
									{
										forcedHideObject.SetActive(!flag5);
									}
								}
							}
						}
					}
				}
			}
			else
			{
				Dictionary<int, bool> areaOpenState2 = Singleton<Game>.Instance.Environment.AreaOpenState;
				foreach (KeyValuePair<int, Dictionary<bool, ForcedHideObject[]>> keyValuePair3 in this.AreaOpenObjectTable)
				{
					if (!keyValuePair3.Value.IsNullOrEmpty<bool, ForcedHideObject[]>())
					{
						int key = keyValuePair3.Key;
						foreach (KeyValuePair<bool, ForcedHideObject[]> keyValuePair4 in keyValuePair3.Value)
						{
							if (!keyValuePair4.Value.IsNullOrEmpty<ForcedHideObject>())
							{
								bool key2 = keyValuePair4.Key;
								foreach (ForcedHideObject forcedHideObject2 in keyValuePair4.Value)
								{
									if (!(forcedHideObject2 == null))
									{
										bool flag6;
										if (!areaOpenState2.TryGetValue(key, out flag6))
										{
											bool flag7 = isFreeMode;
											areaOpenState2[key] = flag7;
											flag6 = flag7;
										}
										if (!key2)
										{
											flag6 = !flag6;
										}
										forcedHideObject2.SetActive(flag6);
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06003D7F RID: 15743 RVA: 0x00166D9C File Offset: 0x0016519C
		public void RefreshEventPointActive()
		{
			if (this.PointAgent == null)
			{
				return;
			}
			EventPoint[] array = this.PointAgent.EventPoints;
			if (array.IsNullOrEmpty<EventPoint>() && this.PointAgent.EventPointRoot != null)
			{
				array = this.PointAgent.EventPointRoot.GetComponentsInChildren<EventPoint>(true);
			}
			if (array.IsNullOrEmpty<EventPoint>())
			{
				return;
			}
			bool flag3;
			if (Singleton<Game>.IsInstance())
			{
				AIProject.SaveData.Environment environment = Singleton<Game>.Instance.Environment;
				bool? flag;
				if (environment == null)
				{
					flag = null;
				}
				else
				{
					Dictionary<int, bool> areaOpenState = environment.AreaOpenState;
					flag = ((areaOpenState != null) ? new bool?(areaOpenState.IsNullOrEmpty<int, bool>()) : null);
				}
				bool? flag2 = flag;
				flag3 = (flag2 == null || flag2.Value);
			}
			else
			{
				flag3 = true;
			}
			bool flag4 = flag3;
			bool isFreeMode = Game.IsFreeMode;
			if (flag4)
			{
				foreach (EventPoint eventPoint in array)
				{
					if (!(eventPoint == null))
					{
						if (eventPoint.OpenAreaID >= 0)
						{
							if (!eventPoint.gameObject.activeSelf)
							{
								eventPoint.gameObject.SetActive(!isFreeMode);
							}
						}
					}
				}
			}
			else
			{
				Dictionary<int, bool> areaOpenState2 = Singleton<Game>.Instance.Environment.AreaOpenState;
				foreach (EventPoint eventPoint2 in array)
				{
					if (!(eventPoint2 == null))
					{
						if (eventPoint2.OpenAreaID >= 0)
						{
							int openAreaID = eventPoint2.OpenAreaID;
							bool flag5;
							if (!areaOpenState2.TryGetValue(openAreaID, out flag5))
							{
								flag5 = false;
							}
							if (isFreeMode)
							{
								flag5 = true;
							}
							if (eventPoint2.gameObject.activeSelf == flag5)
							{
								eventPoint2.gameObject.SetActive(!flag5);
							}
						}
					}
				}
			}
		}

		// Token: 0x06003D80 RID: 15744 RVA: 0x00166F88 File Offset: 0x00165388
		public void RefreshAreaOpenLinkedObject()
		{
			if (AreaOpenLinkedObject.Table.IsNullOrEmpty<int, List<GameObject>>())
			{
				return;
			}
			bool flag3;
			if (Singleton<Game>.IsInstance())
			{
				AIProject.SaveData.Environment environment = Singleton<Game>.Instance.Environment;
				bool? flag;
				if (environment == null)
				{
					flag = null;
				}
				else
				{
					Dictionary<int, bool> areaOpenState = environment.AreaOpenState;
					flag = ((areaOpenState != null) ? new bool?(areaOpenState.IsNullOrEmpty<int, bool>()) : null);
				}
				bool? flag2 = flag;
				flag3 = (flag2 == null || flag2.Value);
			}
			else
			{
				flag3 = true;
			}
			bool flag4 = flag3;
			bool isFreeMode = Game.IsFreeMode;
			if (flag4)
			{
				foreach (KeyValuePair<int, List<GameObject>> keyValuePair in AreaOpenLinkedObject.Table)
				{
					if (!keyValuePair.Value.IsNullOrEmpty<GameObject>())
					{
						foreach (GameObject gameObject in keyValuePair.Value)
						{
							if (!(gameObject == null))
							{
								if (gameObject.activeSelf)
								{
									gameObject.SetActive(isFreeMode);
								}
							}
						}
					}
				}
			}
			else
			{
				Dictionary<int, bool> areaOpenState2 = Singleton<Game>.Instance.Environment.AreaOpenState;
				foreach (KeyValuePair<int, List<GameObject>> keyValuePair2 in AreaOpenLinkedObject.Table)
				{
					if (!keyValuePair2.Value.IsNullOrEmpty<GameObject>())
					{
						bool flag5;
						if (areaOpenState2.TryGetValue(keyValuePair2.Key, out flag5))
						{
							foreach (GameObject gameObject2 in keyValuePair2.Value)
							{
								if (!(gameObject2 == null))
								{
									if (gameObject2.activeSelf != flag5)
									{
										gameObject2.SetActive(flag5);
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06003D81 RID: 15745 RVA: 0x001671E0 File Offset: 0x001655E0
		public void RefreshTimeOpenLinkedObject()
		{
			if (TimeOpenLinkedObject.Table.IsNullOrEmpty<int, List<TimeOpenLinkedObject>>())
			{
				return;
			}
			bool flag3;
			if (Singleton<Game>.IsInstance())
			{
				AIProject.SaveData.Environment environment = Singleton<Game>.Instance.Environment;
				bool? flag;
				if (environment == null)
				{
					flag = null;
				}
				else
				{
					Dictionary<int, bool> timeObjOpenState = environment.TimeObjOpenState;
					flag = ((timeObjOpenState != null) ? new bool?(timeObjOpenState.IsNullOrEmpty<int, bool>()) : null);
				}
				bool? flag2 = flag;
				flag3 = (flag2 == null || flag2.Value);
			}
			else
			{
				flag3 = true;
			}
			bool flag4 = flag3;
			if (flag4)
			{
				foreach (KeyValuePair<int, List<TimeOpenLinkedObject>> keyValuePair in TimeOpenLinkedObject.Table)
				{
					if (!keyValuePair.Value.IsNullOrEmpty<TimeOpenLinkedObject>())
					{
						foreach (TimeOpenLinkedObject timeOpenLinkedObject in keyValuePair.Value)
						{
							if (!(timeOpenLinkedObject == null) && !(timeOpenLinkedObject.gameObject == null))
							{
								timeOpenLinkedObject.SetActive(!timeOpenLinkedObject.EnableFlag);
							}
						}
					}
				}
			}
			else
			{
				Dictionary<int, bool> timeObjOpenState2 = Singleton<Game>.Instance.Environment.TimeObjOpenState;
				foreach (KeyValuePair<int, List<TimeOpenLinkedObject>> keyValuePair2 in TimeOpenLinkedObject.Table)
				{
					if (!keyValuePair2.Value.IsNullOrEmpty<TimeOpenLinkedObject>())
					{
						bool flag5;
						if (!timeObjOpenState2.TryGetValue(keyValuePair2.Key, out flag5))
						{
							flag5 = false;
						}
						foreach (TimeOpenLinkedObject timeOpenLinkedObject2 in keyValuePair2.Value)
						{
							if (!(timeOpenLinkedObject2 == null) && !(timeOpenLinkedObject2.gameObject == null))
							{
								timeOpenLinkedObject2.SetActive(timeOpenLinkedObject2.EnableFlag == flag5);
							}
						}
					}
				}
			}
		}

		// Token: 0x06003D82 RID: 15746 RVA: 0x00167448 File Offset: 0x00165848
		public void SetTimeRelationAreaOpenState(int areaID, bool active)
		{
			if (!Singleton<Game>.IsInstance())
			{
				return;
			}
			bool flag = false;
			Dictionary<int, bool> dictionary = Singleton<Game>.Instance.Environment.TimeObjOpenState;
			if (dictionary == null)
			{
				Dictionary<int, bool> dictionary2 = new Dictionary<int, bool>();
				Singleton<Game>.Instance.Environment.TimeObjOpenState = dictionary2;
				dictionary = dictionary2;
			}
			bool flag2;
			flag = (!dictionary.TryGetValue(areaID, out flag2) || flag2 != active);
			if (flag)
			{
				dictionary[areaID] = active;
			}
			List<TimeOpenLinkedObject> list;
			if (!TimeOpenLinkedObject.Table.IsNullOrEmpty<int, List<TimeOpenLinkedObject>>() && TimeOpenLinkedObject.Table.TryGetValue(areaID, out list) && !list.IsNullOrEmpty<TimeOpenLinkedObject>())
			{
				foreach (TimeOpenLinkedObject timeOpenLinkedObject in list)
				{
					if (!(timeOpenLinkedObject == null) && !(timeOpenLinkedObject.gameObject == null))
					{
						timeOpenLinkedObject.SetActive(timeOpenLinkedObject.EnableFlag == active);
					}
				}
			}
			if (flag)
			{
				this.RefreshActiveTimeRelationObjects();
			}
		}

		// Token: 0x06003D83 RID: 15747 RVA: 0x00167570 File Offset: 0x00165970
		public bool CanSleepInTime()
		{
			DateTime now = this.Simulator.Now;
			PlayerProfile playerProfile = Singleton<Resources>.Instance.PlayerProfile;
			foreach (EnvironmentSimulator.DateTimeThreshold dateTimeThreshold in playerProfile.CanSleepTime)
			{
				if (dateTimeThreshold.min.Time <= now && dateTimeThreshold.max.Time > now)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003D84 RID: 15748 RVA: 0x001675F4 File Offset: 0x001659F4
		public bool GetTimeObjOpenState(int id)
		{
			if (!Singleton<Game>.IsInstance())
			{
				return false;
			}
			AIProject.SaveData.Environment environment = Singleton<Game>.Instance.Environment;
			Dictionary<int, bool> dictionary = (environment != null) ? environment.TimeObjOpenState : null;
			bool flag;
			return dictionary != null && dictionary.TryGetValue(id, out flag) && flag;
		}

		// Token: 0x06003D85 RID: 15749 RVA: 0x00167644 File Offset: 0x00165A44
		public void RefreshTutorialOpenState()
		{
			if (!Singleton<Game>.IsInstance() || !Singleton<Resources>.IsInstance())
			{
				return;
			}
			bool isFirstGame = Game.IsFirstGame;
			bool isFreeMode = Game.IsFreeMode;
			WorldData worldData = Singleton<Game>.Instance.WorldData;
			if (worldData == null)
			{
				return;
			}
			Dictionary<int, bool> dictionary = worldData.TutorialOpenStateTable;
			if (dictionary == null)
			{
				dictionary = (worldData.TutorialOpenStateTable = new Dictionary<int, bool>());
			}
			ReadOnlyDictionary<int, UnityEx.ValueTuple<string, GameObject[]>> tutorialPrefabTable = Singleton<Resources>.Instance.PopupInfo.TutorialPrefabTable;
			if (tutorialPrefabTable.IsNullOrEmpty<int, UnityEx.ValueTuple<string, GameObject[]>>())
			{
				return;
			}
			foreach (int key in tutorialPrefabTable.Keys)
			{
				if (isFreeMode && isFirstGame)
				{
					dictionary[key] = true;
				}
				else if (!dictionary.ContainsKey(key))
				{
					dictionary[key] = isFreeMode;
				}
			}
		}

		// Token: 0x06003D86 RID: 15750 RVA: 0x0016773C File Offset: 0x00165B3C
		public bool CheckAvailableMapArea(int mapAreaID)
		{
			if (this.MapID != 0)
			{
				return true;
			}
			if (!Singleton<Resources>.IsInstance())
			{
				return false;
			}
			if (!Singleton<Game>.IsInstance())
			{
				return false;
			}
			AIProject.SaveData.Environment environment = Singleton<Game>.Instance.Environment;
			Dictionary<int, bool> dictionary = (environment != null) ? environment.AreaOpenState : null;
			if (dictionary == null)
			{
				return false;
			}
			Dictionary<int, int[]> areaOpenStateMapAreaLinkerTable = Singleton<Resources>.Instance.Map.AreaOpenStateMapAreaLinkerTable;
			int[] array;
			if (!areaOpenStateMapAreaLinkerTable.TryGetValue(mapAreaID, out array))
			{
				return false;
			}
			if (array.IsNullOrEmpty<int>())
			{
				return true;
			}
			bool flag = true;
			foreach (int key in array)
			{
				bool flag2;
				flag = (dictionary.TryGetValue(key, out flag2) && (flag && flag2));
			}
			return flag;
		}

		// Token: 0x06003D87 RID: 15751 RVA: 0x00167800 File Offset: 0x00165C00
		public void ResettingEnviroSEActive(int mapID, int areaID)
		{
			if (mapID < 0 || areaID < 0)
			{
				return;
			}
			if (!Singleton<Resources>.IsInstance())
			{
				return;
			}
			Resources.SoundTable sound = Singleton<Resources>.Instance.Sound;
			if (sound == null)
			{
				return;
			}
			if (this.EnviroSETable.IsNullOrEmpty<int, UnityEx.ValueTuple<GameObject, EnvArea3DSE, EnvLineArea3DSE>>())
			{
				return;
			}
			Dictionary<int, int[]> dictionary = null;
			int[] array = null;
			if (sound.EnviroSEAdjacentInfoTable.TryGetValue(mapID, out dictionary) && dictionary.TryGetValue(areaID, out array) && !array.IsNullOrEmpty<int>())
			{
				List<int> list = ListPool<int>.Get();
				foreach (KeyValuePair<int, UnityEx.ValueTuple<GameObject, EnvArea3DSE, EnvLineArea3DSE>> keyValuePair in this.EnviroSETable)
				{
					list.Add(keyValuePair.Key);
				}
				if (list.Contains(areaID))
				{
					list.Remove(areaID);
				}
				UnityEx.ValueTuple<GameObject, EnvArea3DSE, EnvLineArea3DSE> valueTuple;
				if (this.EnviroSETable.TryGetValue(areaID, out valueTuple) && valueTuple.Item1 != null && !valueTuple.Item1.activeSelf)
				{
					valueTuple.Item1.SetActive(true);
				}
				foreach (int num in array)
				{
					UnityEx.ValueTuple<GameObject, EnvArea3DSE, EnvLineArea3DSE> valueTuple2;
					if (this.EnviroSETable.TryGetValue(num, out valueTuple2) && valueTuple2.Item1 != null && !valueTuple2.Item1.activeSelf)
					{
						valueTuple2.Item1.SetActive(true);
					}
					if (list.Contains(num))
					{
						list.Remove(num);
					}
				}
				foreach (int key in list)
				{
					UnityEx.ValueTuple<GameObject, EnvArea3DSE, EnvLineArea3DSE> valueTuple3;
					if (this.EnviroSETable.TryGetValue(key, out valueTuple3) && valueTuple3.Item1 != null && valueTuple3.Item1.activeSelf)
					{
						valueTuple3.Item1.SetActive(false);
					}
				}
				ListPool<int>.Release(list);
			}
			else
			{
				foreach (KeyValuePair<int, UnityEx.ValueTuple<GameObject, EnvArea3DSE, EnvLineArea3DSE>> keyValuePair2 in this.EnviroSETable)
				{
					GameObject item = keyValuePair2.Value.Item1;
					if (!(item == null))
					{
						if (item.activeSelf)
						{
							item.SetActive(false);
						}
					}
				}
			}
		}

		// Token: 0x06003D88 RID: 15752 RVA: 0x00167ABC File Offset: 0x00165EBC
		public void ResettingEnviroAreaElement(int mapID, int areaID)
		{
			if (mapID < 0 || areaID < 0)
			{
				return;
			}
			if (!Singleton<Resources>.IsInstance())
			{
				return;
			}
			Resources.SoundTable sound = Singleton<Resources>.Instance.Sound;
			Dictionary<int, Dictionary<int, int[]>> dictionary = (sound != null) ? sound.EnviroSEAdjacentInfoTable : null;
			if (dictionary.IsNullOrEmpty<int, Dictionary<int, int[]>>())
			{
				return;
			}
			if (this.EnviroRootElement.IsNullOrEmpty<int, Transform>())
			{
				return;
			}
			Dictionary<int, int[]> dictionary2 = null;
			int[] array = null;
			if (dictionary.TryGetValue(mapID, out dictionary2) && !dictionary2.IsNullOrEmpty<int, int[]>() && dictionary2.TryGetValue(areaID, out array) && !array.IsNullOrEmpty<int>())
			{
				List<int> list = ListPool<int>.Get();
				list.AddRange(array);
				if (!list.Contains(areaID))
				{
					list.Add(areaID);
				}
				List<int> list2 = ListPool<int>.Get();
				list2.AddRange(this.EnviroRootElement.Keys);
				list2.Remove(areaID);
				foreach (int num in list)
				{
					Transform transform;
					if (this.EnviroRootElement.TryGetValue(num, out transform) && transform != null && !transform.gameObject.activeSelf)
					{
						transform.gameObject.SetActive(true);
					}
					if (list2.Contains(num))
					{
						list2.Remove(num);
					}
				}
				foreach (int key in list2)
				{
					Transform transform2;
					if (this.EnviroRootElement.TryGetValue(key, out transform2) && transform2 != null && transform2.gameObject.activeSelf)
					{
						transform2.gameObject.SetActive(false);
					}
				}
				ListPool<int>.Release(list);
				ListPool<int>.Release(list2);
			}
			else
			{
				foreach (KeyValuePair<int, Transform> keyValuePair in this.EnviroRootElement)
				{
					Transform value = keyValuePair.Value;
					if (!(value == null) && !(value.gameObject == null))
					{
						bool flag = areaID == keyValuePair.Key;
						if (value.gameObject.activeSelf != flag)
						{
							value.gameObject.SetActive(flag);
						}
					}
				}
			}
		}

		// Token: 0x06003D89 RID: 15753 RVA: 0x00167D64 File Offset: 0x00166164
		public void RefreshHousingEnv3DSEPoints(int mapID, int areaID)
		{
			if (mapID < 0 || areaID < 0)
			{
				return;
			}
			if (!Singleton<Resources>.IsInstance())
			{
				return;
			}
			Resources.SoundTable sound = Singleton<Resources>.Instance.Sound;
			Dictionary<int, Dictionary<int, int[]>> dictionary = (sound != null) ? sound.EnviroSEAdjacentInfoTable : null;
			if (dictionary.IsNullOrEmpty<int, Dictionary<int, int[]>>())
			{
				return;
			}
			if (this.HousingEnvSEPointTable.IsNullOrEmpty<int, UnityEx.ValueTuple<bool, List<Env3DSEPoint>>>())
			{
				return;
			}
			Dictionary<int, int[]> dictionary2 = null;
			int[] array = null;
			if (dictionary.TryGetValue(mapID, out dictionary2) && !dictionary2.IsNullOrEmpty<int, int[]>() && dictionary2.TryGetValue(areaID, out array) && !array.IsNullOrEmpty<int>())
			{
				List<int> list = ListPool<int>.Get();
				list.AddRange(array);
				if (!list.Contains(areaID))
				{
					list.Add(areaID);
				}
				List<int> list2 = ListPool<int>.Get();
				list2.AddRange(this.HousingEnvSEPointTable.Keys);
				list2.Remove(areaID);
				foreach (int num in list)
				{
					UnityEx.ValueTuple<bool, List<Env3DSEPoint>> value;
					if (this.HousingEnvSEPointTable.TryGetValue(num, out value))
					{
						value.Item1 = true;
						if (!value.Item2.IsNullOrEmpty<Env3DSEPoint>())
						{
							foreach (Env3DSEPoint env3DSEPoint in value.Item2)
							{
								if (!(env3DSEPoint == null))
								{
									env3DSEPoint.enabled = true;
								}
							}
						}
						this.HousingEnvSEPointTable[num] = value;
					}
					if (list2.Contains(num))
					{
						list2.Remove(num);
					}
				}
				foreach (int key in list2)
				{
					UnityEx.ValueTuple<bool, List<Env3DSEPoint>> valueTuple;
					if (this.HousingEnvSEPointTable.TryGetValue(key, out valueTuple))
					{
						valueTuple.Item1 = false;
						if (!valueTuple.Item2.IsNullOrEmpty<Env3DSEPoint>())
						{
							foreach (Env3DSEPoint env3DSEPoint2 in valueTuple.Item2)
							{
								if (!(env3DSEPoint2 == null))
								{
									env3DSEPoint2.enabled = false;
								}
							}
						}
					}
				}
				ListPool<int>.Release(list);
				ListPool<int>.Release(list2);
			}
			else if (!this.HousingEnvSEPointTable.IsNullOrEmpty<int, UnityEx.ValueTuple<bool, List<Env3DSEPoint>>>())
			{
				List<int> list3 = this.HousingEnvSEPointTable.Keys.ToList<int>();
				foreach (int num2 in list3)
				{
					UnityEx.ValueTuple<bool, List<Env3DSEPoint>> value2 = this.HousingEnvSEPointTable[num2];
					value2.Item1 = (num2 == areaID);
					if (!value2.Item2.IsNullOrEmpty<Env3DSEPoint>())
					{
						foreach (Env3DSEPoint env3DSEPoint3 in value2.Item2)
						{
							if (env3DSEPoint3 != null && env3DSEPoint3.enabled != value2.Item1)
							{
								env3DSEPoint3.enabled = value2.Item1;
							}
						}
					}
					this.HousingEnvSEPointTable[num2] = value2;
				}
			}
		}

		// Token: 0x06003D8A RID: 15754 RVA: 0x0016812C File Offset: 0x0016652C
		public static StoryPoint GetStoryPoint(int id)
		{
			if (Singleton<Map>.IsInstance() && Singleton<Map>.Instance._pointAgent != null)
			{
				StoryPoint result = null;
				Singleton<Map>.Instance._pointAgent.StoryPointTable.TryGetValue(id, out result);
				return result;
			}
			return null;
		}

		// Token: 0x06003D8B RID: 15755 RVA: 0x00168178 File Offset: 0x00166578
		public static bool SetTutorialProgress(int number)
		{
			if (!Singleton<Game>.IsInstance())
			{
				return false;
			}
			AIProject.SaveData.Environment environment = Singleton<Game>.Instance.Environment;
			if (environment == null)
			{
				return false;
			}
			int tutorialProgress = environment.TutorialProgress;
			environment.TutorialProgress = Mathf.Max(tutorialProgress, number);
			return tutorialProgress < number;
		}

		// Token: 0x06003D8C RID: 15756 RVA: 0x001681BC File Offset: 0x001665BC
		public static bool ForcedSetTutorialProgress(int number)
		{
			if (!Singleton<Game>.IsInstance())
			{
				return false;
			}
			AIProject.SaveData.Environment environment = Singleton<Game>.Instance.Environment;
			if (environment == null)
			{
				return false;
			}
			environment.TutorialProgress = number;
			return true;
		}

		// Token: 0x06003D8D RID: 15757 RVA: 0x001681F0 File Offset: 0x001665F0
		public static bool ForcedSetTutorialProgressAndUIUpdate(int number)
		{
			if (!Singleton<Game>.IsInstance())
			{
				return false;
			}
			AIProject.SaveData.Environment environment = Singleton<Game>.Instance.Environment;
			if (environment == null)
			{
				return false;
			}
			environment.TutorialProgress = number;
			MapUIContainer.StorySupportUI.Open(number);
			return true;
		}

		// Token: 0x06003D8E RID: 15758 RVA: 0x0016822F File Offset: 0x0016662F
		public static void SetTutorialProgressAndUIUpdate(int number)
		{
			if (Map.SetTutorialProgress(number))
			{
				MapUIContainer.OpenStorySupportUI(number);
			}
		}

		// Token: 0x06003D8F RID: 15759 RVA: 0x00168244 File Offset: 0x00166644
		public static void RefreshStoryUI()
		{
			if (!Singleton<Game>.IsInstance())
			{
				return;
			}
			AIProject.SaveData.Environment environment = Singleton<Game>.Instance.Environment;
			if (environment == null)
			{
				return;
			}
			MapUIContainer.OpenStorySupportUI(environment.TutorialProgress);
		}

		// Token: 0x06003D90 RID: 15760 RVA: 0x0016827C File Offset: 0x0016667C
		public void CheckTutorialState(PlayerActor player)
		{
			if (player == null || !Singleton<Resources>.IsInstance())
			{
				return;
			}
			Resources instance = Singleton<Resources>.Instance;
			if (Map.TutorialMode)
			{
				if (this.ChangeTutorialUI(player))
				{
					return;
				}
				switch (Map.GetTutorialProgress())
				{
				case 3:
				{
					ItemIDKeyPair driftwoodID = instance.CommonDefine.ItemIDDefine.DriftwoodID;
					if (player.PlayerData.ItemList.Exists((StuffItem x) => x.CategoryID == driftwoodID.categoryID && x.ID == driftwoodID.itemID && 0 < x.Count))
					{
						Singleton<Map>.Instance.DestroyTutorialSearchPoint();
						Map.SetTutorialProgress(4);
						player.AddTutorialUI(Popup.Tutorial.Type.Craft, false);
					}
					break;
				}
				case 4:
				{
					FishingDefinePack.ItemIDPair fishingRodID = instance.FishingDefinePack.IDInfo.FishingRod;
					bool flag = player.PlayerData.ItemList.Exists((StuffItem x) => x.CategoryID == fishingRodID.CategoryID && x.ID == fishingRodID.ItemID && 0 < x.Count);
					if (flag)
					{
						Map.SetTutorialProgress(5);
						player.AddTutorialUI(Popup.Tutorial.Type.Equipment, false);
					}
					break;
				}
				case 5:
				{
					int id = player.PlayerData.EquipedFishingItem.ID;
					bool flag2 = id == instance.CommonDefine.ItemIDDefine.RodID.itemID;
					if (flag2)
					{
						Map.SetTutorialProgress(6);
					}
					break;
				}
				case 6:
				{
					List<FishingDefinePack.ItemIDPair> fishList = instance.FishingDefinePack.IDInfo.FishList;
					bool flag3 = false;
					if (!fishList.IsNullOrEmpty<FishingDefinePack.ItemIDPair>())
					{
						using (List<FishingDefinePack.ItemIDPair>.Enumerator enumerator = fishList.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								FishingDefinePack.ItemIDPair data = enumerator.Current;
								if (player.PlayerData.ItemList.Exists((StuffItem x) => x.CategoryID == data.CategoryID && x.ID == data.ItemID && 0 < x.Count))
								{
									flag3 = true;
									break;
								}
							}
						}
						if (flag3)
						{
							Map.SetTutorialProgress(7);
						}
					}
					break;
				}
				case 11:
				{
					bool flag4 = false;
					FishingDefinePack fishingDefinePack = (instance != null) ? instance.FishingDefinePack : null;
					PlayerActor player2 = player;
					List<StuffItem> list;
					if (player2 == null)
					{
						list = null;
					}
					else
					{
						PlayerData playerData = player2.PlayerData;
						list = ((playerData != null) ? playerData.ItemList : null);
					}
					List<StuffItem> list2 = list;
					if (fishingDefinePack != null && list2 != null)
					{
						FishingDefinePack.ItemIDPair idinfo = fishingDefinePack.IDInfo.GrilledFish;
						flag4 = list2.Exists((StuffItem x) => x.CategoryID == idinfo.CategoryID && x.ID == idinfo.ItemID && 0 < x.Count);
						if (flag4)
						{
							Map.SetTutorialProgress(13);
						}
					}
					if (!flag4)
					{
						CommonDefine commonDefine = (!Singleton<Resources>.IsInstance()) ? null : Singleton<Resources>.Instance.CommonDefine;
						if (commonDefine != null && this._pointAgent != null && !commonDefine.Tutorial.KitchenPointIDList.IsNullOrEmpty<int>())
						{
							int[] kitchenPointIDList = commonDefine.Tutorial.KitchenPointIDList;
							for (int i = 0; i < kitchenPointIDList.Length; i++)
							{
								int kitchenID = kitchenPointIDList[i];
								if (this._pointAgent.AppendActionPoints.Exists((ActionPoint x) => x.ID == kitchenID))
								{
									Map.SetTutorialProgress(12);
									break;
								}
							}
						}
					}
					break;
				}
				case 12:
				{
					FishingDefinePack.ItemIDPair idinfo = instance.FishingDefinePack.IDInfo.GrilledFish;
					List<StuffItem> itemList = player.PlayerData.ItemList;
					bool flag5 = itemList.Exists((StuffItem x) => x.CategoryID == idinfo.CategoryID && x.ID == idinfo.ItemID && 0 < x.Count);
					if (flag5)
					{
						Map.SetTutorialProgress(13);
					}
					break;
				}
				}
			}
			if (this.ChangeTutorialUI(player))
			{
				return;
			}
			if (this.MapID != 0)
			{
				return;
			}
			if (Map.GetTutorialProgress() != MapUIContainer.StorySupportUI.Index)
			{
				bool flag6 = Singleton<MapScene>.IsInstance() && Singleton<MapScene>.Instance.IsLoading;
				bool flag7 = !flag6 && Singleton<Scene>.IsInstance() && Singleton<Scene>.Instance.IsNowLoadingFade;
				if (flag6)
				{
					Observable.EveryUpdate().TakeUntilDestroy(Singleton<MapScene>.Instance.gameObject).TakeUntilDestroy(player.gameObject).SkipWhile((long _) => Singleton<MapScene>.IsInstance() && Singleton<MapScene>.Instance.IsLoading).SkipWhile((long _) => player.CameraControl.CinemachineBrain.IsBlending || player.Animation.PlayingInLocoAnimation).Take(1).DelayFrame(15, FrameCountType.Update).Subscribe(delegate(long _)
					{
						Map.RefreshStoryUI();
					});
				}
				else if (flag7)
				{
					Observable.EveryUpdate().TakeUntilDestroy(Singleton<MapScene>.Instance.gameObject).TakeUntilDestroy(player.gameObject).SkipWhile((long _) => Singleton<Scene>.IsInstance() && Singleton<Scene>.Instance.IsNowLoadingFade).SkipWhile((long _) => player.CameraControl.CinemachineBrain.IsBlending || player.Animation.PlayingInLocoAnimation).Take(1).DelayFrame(15, FrameCountType.Update).Subscribe(delegate(long _)
					{
						Map.RefreshStoryUI();
					});
				}
				else
				{
					Observable.EveryUpdate().TakeUntilDestroy(player).Skip(1).SkipWhile((long _) => player.CameraControl.CinemachineBrain.IsBlending || player.Animation.PlayingInLocoAnimation).Take(1).DelayFrame(15, FrameCountType.Update).Subscribe(delegate(long _)
					{
						Map.RefreshStoryUI();
					});
				}
			}
		}

		// Token: 0x06003D91 RID: 15761 RVA: 0x00168874 File Offset: 0x00166C74
		private bool ChangeTutorialUI(PlayerActor player)
		{
			float delayTime = (!Singleton<Resources>.IsInstance()) ? 1f : Singleton<Resources>.Instance.CommonDefine.Tutorial.UIDisplayDelayTime;
			if (!player.TutorialIndexList.IsNullOrEmpty<UnityEx.ValueTuple<Popup.Tutorial.Type, bool>>())
			{
				UnityEx.ValueTuple<Popup.Tutorial.Type, bool> valueTuple = player.TutorialIndexList.Pop<UnityEx.ValueTuple<Popup.Tutorial.Type, bool>>();
				MapUIContainer.TutorialUI.SetCondition(valueTuple.Item1, valueTuple.Item2);
				EventPoint.SetCurrentPlayerStateName();
				player.PlayerController.ChangeState("Idle");
				bool flag = Singleton<MapScene>.IsInstance() && Singleton<MapScene>.Instance.IsLoading;
				bool flag2 = !flag && Singleton<Scene>.IsInstance() && Singleton<Scene>.Instance.IsNowLoadingFade;
				if (flag)
				{
					Observable.EveryUpdate().TakeUntilDestroy(Singleton<MapScene>.Instance.gameObject).TakeUntilDestroy(player.gameObject).SkipWhile((long _) => Singleton<MapScene>.IsInstance() && Singleton<MapScene>.Instance.IsLoading).Take(1).Subscribe(delegate(long _)
					{
						this.DelayChangeTutorialUI(player, delayTime);
					});
				}
				else if (flag2)
				{
					Observable.EveryUpdate().TakeUntilDestroy(Singleton<Scene>.Instance).TakeUntilDestroy(player.gameObject).SkipWhile((long _) => Singleton<Scene>.IsInstance() && Singleton<Scene>.Instance.IsNowLoadingFade).Take(1).Subscribe(delegate(long _)
					{
						this.DelayChangeTutorialUI(player, delayTime);
					});
				}
				else
				{
					this.DelayChangeTutorialUI(player, delayTime);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06003D92 RID: 15762 RVA: 0x00168A34 File Offset: 0x00166E34
		private void DelayChangeTutorialUI(PlayerActor player, float delayTime)
		{
			if (Map.GetTutorialProgress() != MapUIContainer.StorySupportUI.Index)
			{
				MapUIContainer.StorySupportUI.OpenedAction = delegate()
				{
					(from _ in Observable.Timer(TimeSpan.FromSeconds((double)delayTime)).TakeUntilDestroy(player.gameObject)
					where Singleton<Map>.IsInstance()
					select _).Subscribe(delegate(long _)
					{
						player.PlayerController.ChangeState("Tutorial");
					});
					MapUIContainer.StorySupportUI.OpenedAction = null;
				};
				Observable.EveryUpdate().TakeUntilDestroy(player.gameObject).Skip(1).SkipWhile((long _) => player.Animation.PlayingInLocoAnimation || player.CameraControl.CinemachineBrain.IsBlending).Take(1).Subscribe(delegate(long _)
				{
					Map.RefreshStoryUI();
				});
			}
			else
			{
				Observable.EveryUpdate().Skip(1).SkipWhile((long _) => player.Animation.PlayingInLocoAnimation || player.CameraControl.CinemachineBrain.IsBlending).Take(1).Delay(TimeSpan.FromSeconds((double)delayTime)).Subscribe(delegate(long _)
				{
					player.PlayerController.ChangeState("Tutorial");
				});
			}
		}

		// Token: 0x06003D93 RID: 15763 RVA: 0x00168B20 File Offset: 0x00166F20
		public void CheckStoryProgress()
		{
			if (this.MapID != 0)
			{
				return;
			}
			int tutorialProgress = Map.GetTutorialProgress();
			Popup.StorySupport.Type type = (Popup.StorySupport.Type)tutorialProgress;
			switch (type)
			{
			case Popup.StorySupport.Type.GrowGirls3:
			case Popup.StorySupport.Type.ExamineStoryPoint3:
			case Popup.StorySupport.Type.RepairGenerator:
			case Popup.StorySupport.Type.ExamineStoryPoint4:
			{
				EventPoint eventPoint = EventPoint.Get(1, 2);
				if (eventPoint != null)
				{
					switch (eventPoint.LabelIndex)
					{
					case 0:
						if (type != Popup.StorySupport.Type.GrowGirls3)
						{
							Map.ForcedSetTutorialProgressAndUIUpdate(22);
						}
						break;
					case 1:
						if (type != Popup.StorySupport.Type.ExamineStoryPoint3)
						{
							Map.ForcedSetTutorialProgressAndUIUpdate(23);
						}
						break;
					case 2:
						if (type != Popup.StorySupport.Type.RepairGenerator)
						{
							Map.ForcedSetTutorialProgressAndUIUpdate(24);
						}
						break;
					case 3:
						if (type != Popup.StorySupport.Type.ExamineStoryPoint4)
						{
							Map.ForcedSetTutorialProgressAndUIUpdate(25);
						}
						break;
					}
				}
				break;
			}
			default:
				switch (type)
				{
				case Popup.StorySupport.Type.GrowGirls1:
				{
					EventPoint eventPoint2 = EventPoint.Get(1, 0);
					if (eventPoint2 != null && eventPoint2.LabelIndex == 1)
					{
						Map.ForcedSetTutorialProgressAndUIUpdate(17);
					}
					break;
				}
				case Popup.StorySupport.Type.GrowGirls2:
				{
					EventPoint eventPoint3 = EventPoint.Get(1, 1);
					if (eventPoint3 != null && eventPoint3.LabelIndex == 1)
					{
						Map.ForcedSetTutorialProgressAndUIUpdate(20);
					}
					break;
				}
				}
				break;
			}
			bool flag = false;
			if (this.Player != null)
			{
				flag = (this.Player.Mode == Desire.ActionType.Date);
			}
			type = (Popup.StorySupport.Type)Map.GetTutorialProgress();
			if (!flag)
			{
				switch (type)
				{
				case Popup.StorySupport.Type.GrowGirls1:
				case Popup.StorySupport.Type.ExamineStoryPoint1:
					EventPoint.SetTargetID(1, 0);
					break;
				case Popup.StorySupport.Type.ExamineNextStoryPoint1:
				case Popup.StorySupport.Type.GrowGirls2:
				case Popup.StorySupport.Type.ExamineStoryPoint2:
					EventPoint.SetTargetID(1, 1);
					break;
				case Popup.StorySupport.Type.ExamineNextStoryPoint2:
				case Popup.StorySupport.Type.GrowGirls3:
				case Popup.StorySupport.Type.ExamineStoryPoint3:
				case Popup.StorySupport.Type.ExamineStoryPoint4:
					EventPoint.SetTargetID(1, 2);
					break;
				case Popup.StorySupport.Type.RepairGenerator:
					EventPoint.SetTargetID(1, 3);
					break;
				case Popup.StorySupport.Type.ExamineNextStoryPoint3:
				case Popup.StorySupport.Type.RepairShip:
					EventPoint.SetTargetID(1, 6);
					break;
				default:
					EventPoint.SetTargetID(-1, -1);
					break;
				}
			}
			else if (type != Popup.StorySupport.Type.ExamineStoryPoint4)
			{
				EventPoint.SetTargetID(-1, -1);
			}
			else
			{
				EventPoint.SetTargetID(1, 2);
			}
		}

		// Token: 0x06003D94 RID: 15764 RVA: 0x00168D48 File Offset: 0x00167148
		public static int GetTutorialProgress()
		{
			if (Singleton<Game>.IsInstance())
			{
				AIProject.SaveData.Environment environment = Singleton<Game>.Instance.Environment;
				return (environment == null) ? -1 : environment.TutorialProgress;
			}
			return -1;
		}

		// Token: 0x06003D95 RID: 15765 RVA: 0x00168D80 File Offset: 0x00167180
		public static int GetAgentTotalFlavorAdditionAmount()
		{
			if (!Singleton<Map>.IsInstance())
			{
				return 0;
			}
			if (Singleton<Map>.Instance.AgentTable.IsNullOrEmpty<int, AgentActor>())
			{
				return 0;
			}
			int num = 0;
			foreach (KeyValuePair<int, AgentActor> keyValuePair in Singleton<Map>.Instance.AgentTable)
			{
				AgentActor value = keyValuePair.Value;
				if (!(value == null))
				{
					AgentData agentData = value.AgentData;
					if (agentData != null)
					{
						if (agentData.OpenState)
						{
							num += agentData.FlavorAdditionAmount;
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06003D96 RID: 15766 RVA: 0x00168E44 File Offset: 0x00167244
		public static int GetTotalAgentFlavorAdditionAmount()
		{
			if (!Singleton<Map>.IsInstance() || !Singleton<Game>.IsInstance())
			{
				return 0;
			}
			AIProject.SaveData.Environment environment = Singleton<Game>.Instance.Environment;
			if (environment == null)
			{
				return 0;
			}
			return environment.TotalAgentFlavorAdditionAmount;
		}

		// Token: 0x06003D97 RID: 15767 RVA: 0x00168E80 File Offset: 0x00167280
		public IEnumerator SetupPoint()
		{
			this._pointAgent.SetActive(true);
			yield return this._pointAgent.Load();
			if (global::Debug.isDebugBuild)
			{
			}
			yield break;
		}

		// Token: 0x06003D98 RID: 15768 RVA: 0x00168E9C File Offset: 0x0016729C
		public IEnumerator ApplyProfile(WorldData profile, bool existsBackup)
		{
			yield return this.RefreshAsync();
			yield return this.LoadPlayerAsync(profile.PlayerData, existsBackup);
			yield return this.LoadMerchantAsync(profile.MerchantData, existsBackup);
			yield return this.LoadAgents(profile, existsBackup);
			this.RefreshAgentStatus();
			this.RefreshAnimalStatus();
			yield return null;
			this.Simulator.EnabledSky = true;
			this.Simulator.EnviroSky.AssignAndStart(this.Player.Locomotor.gameObject, this.Player.CameraControl.CameraComponent);
			this.Simulator.DefaultEnviroZone.enabled = true;
			yield return null;
			AIProject.SaveData.Environment env = profile.Environment;
			this.Simulator.Now = env.Time.DateTime;
			this.Simulator.InitializeEnviroParticle();
			this.Simulator.RefreshWeather(env.Weather, true);
			this.Simulator.SetTemperatureValue(env.TemperatureValue);
			yield return null;
			this.RefreshActiveTimeRelationObjects();
			yield break;
		}

		// Token: 0x06003D99 RID: 15769 RVA: 0x00168EC8 File Offset: 0x001672C8
		private void LoadPlayer(PlayerData playerInfo)
		{
			DefinePack definePack = Singleton<Resources>.Instance.DefinePack;
			string bundlePath = definePack.ABPaths.ActorPrefab;
			string manifest = definePack.ABManifests.Default;
			GameObject original = CommonLib.LoadAsset<GameObject>(bundlePath, "Player", false, manifest);
			if (!MapScene.AssetBundlePaths.Exists((UnityEx.ValueTuple<string, string> x) => x.Item1 == bundlePath && x.Item2 == manifest))
			{
				MapScene.AssetBundlePaths.Add(new UnityEx.ValueTuple<string, string>(bundlePath, manifest));
			}
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
			gameObject.transform.Initiate(this.ActorRoot);
			PlayerActor player = gameObject.GetComponent<PlayerActor>();
			player.ID = -99;
			Vector3 pos = playerInfo.Position;
			(from _ in Observable.EveryUpdate()
			where player.NavMeshAgent.enabled
			select _).TakeWhile((long _) => !player.NavMeshAgent.isOnNavMesh).Subscribe(delegate(long _)
			{
				player.NavMeshAgent.Warp(pos);
			});
			player.Rotation = playerInfo.Rotation;
			player.PlayerData = playerInfo;
			if (player.PlayerData.CharaFileNames.IsNullOrEmpty<string>())
			{
				player.PlayerData.CharaFileNames[0] = "charaF_20170613163526688";
				player.PlayerData.CharaFileNames[1] = "charaF_20170613163526688";
			}
			player.Relocate();
			gameObject.SetActiveSafe(true);
			this.RegisterPlayer(player);
			this.RegisterActor(-99, player);
		}

		// Token: 0x06003D9A RID: 15770 RVA: 0x00169060 File Offset: 0x00167460
		private IEnumerator LoadPlayerAsync(PlayerData playerInfo, bool existsBackup)
		{
			DefinePack define = Singleton<Resources>.Instance.DefinePack;
			string bundlePath = define.ABPaths.ActorPrefab;
			string manifest = define.ABManifests.Default;
			GameObject playPrefab = CommonLib.LoadAsset<GameObject>(bundlePath, "Player", false, manifest);
			if (!MapScene.AssetBundlePaths.Exists((UnityEx.ValueTuple<string, string> x) => x.Item1 == bundlePath && x.Item2 == manifest))
			{
				MapScene.AssetBundlePaths.Add(new UnityEx.ValueTuple<string, string>(bundlePath, manifest));
			}
			GameObject obj = UnityEngine.Object.Instantiate<GameObject>(playPrefab);
			obj.transform.Initiate(this.ActorRoot);
			PlayerActor player = obj.GetComponent<PlayerActor>();
			player.ID = -99;
			bool addTutorialItems = false;
			if (existsBackup)
			{
				Vector3 pos = playerInfo.Position;
				player.NavMeshAgent.Warp(pos);
				(from _ in Observable.EveryUpdate()
				where player.NavMeshAgent.enabled
				select _).TakeWhile((long _) => !player.NavMeshAgent.isOnNavMesh).Subscribe(delegate(long _)
				{
					player.NavMeshAgent.Warp(pos);
				});
				player.Rotation = playerInfo.Rotation;
			}
			else
			{
				StoryPoint storyPoint = Map.GetStoryPoint(0);
				if (Map.TutorialMode && storyPoint != null)
				{
					player.MapArea = storyPoint.OwnerArea;
					GameObject gameObject = storyPoint.transform.FindLoop("player_point");
					Transform transform = (gameObject != null) ? gameObject.transform : null;
					Actor player3 = player;
					Vector3? vector = (transform != null) ? new Vector3?(transform.position) : null;
					player3.Position = ((vector == null) ? storyPoint.Position : vector.Value);
					Actor player2 = player;
					Quaternion? quaternion = (transform != null) ? new Quaternion?(transform.rotation) : null;
					player2.Rotation = ((quaternion == null) ? storyPoint.Rotation : quaternion.Value);
				}
				else
				{
					player.Position = this.PlayerStartPoint.position;
					player.Rotation = this.PlayerStartPoint.rotation;
					addTutorialItems = true;
				}
			}
			player.PlayerData = playerInfo;
			playerInfo.param.Bind(player);
			if (player.PlayerData.CharaFileNames.IsNullOrEmpty<string>())
			{
				player.PlayerData.CharaFileNames[0] = "charaF_20170613163526688";
				player.PlayerData.CharaFileNames[1] = "charaF_20170613163526688";
			}
			player.Relocate();
			obj.SetActiveSafe(true);
			yield return player.LoadAsync();
			this.RegisterPlayer(player);
			this.RegisterActor(-99, player);
			if (addTutorialItems)
			{
				this.AddTutorialItems();
			}
			yield break;
		}

		// Token: 0x06003D9B RID: 15771 RVA: 0x0016908C File Offset: 0x0016748C
		private IEnumerator LoadAgents(WorldData profile, bool existsBackup)
		{
			DefinePack define = Singleton<Resources>.Instance.DefinePack;
			AgentProfile agentProfile = Singleton<Resources>.Instance.AgentProfile;
			string bundlePath = define.ABPaths.ActorPrefab;
			string manifest = define.ABManifests.Default;
			GameObject agentPrefab = CommonLib.LoadAsset<GameObject>(bundlePath, "Agent", false, manifest);
			if (!MapScene.AssetBundlePaths.Exists((UnityEx.ValueTuple<string, string> x) => x.Item1 == bundlePath && x.Item2 == manifest))
			{
				MapScene.AssetBundlePaths.Add(new UnityEx.ValueTuple<string, string>(bundlePath, manifest));
			}
			int[] chunkKeys = this.ChunkTable.Keys.ToArray<int>();
			int maxCharaNum = Config.GraphicData.MaxCharaNum;
			bool[] charasEnter = Config.GraphicData.CharasEntry;
			ChaFileControl chaFile = new ChaFileControl();
			bool isFreeMode = profile.FreeMode;
			bool isFirstGame = !existsBackup;
			for (int i = 0; i < profile.AgentTable.Count; i++)
			{
				AgentData data = profile.AgentTable[i];
				if (isFreeMode)
				{
					data.OpenState = true;
					data.PlayEnterScene = true;
				}
				if (!charasEnter[i] && (!Map.TutorialMode || i != 0))
				{
					if (i == 0 && isFreeMode && isFirstGame)
					{
						this.FirstAgentDataPositionSetting(data);
					}
				}
				else if (data.OpenState)
				{
					if (!data.CharaFileName.IsNullOrEmpty())
					{
						if (data.MapID != this.MapID)
						{
							ChaFileControl chaFileControl = new ChaFileControl();
							if (chaFileControl.LoadCharaFile(data.CharaFileName, 1, false, true))
							{
								this.SetTableAgentChaFile(i, chaFileControl);
							}
						}
						else if (chaFile.LoadCharaFile(data.CharaFileName, 1, false, true))
						{
							GameObject agentObj = UnityEngine.Object.Instantiate<GameObject>(agentPrefab, this.ActorRoot);
							agentObj.name = string.Format("Heroine_{0}", i.ToString("00"));
							AgentActor agent = agentObj.GetComponent<AgentActor>();
							agent.ID = i;
							agent.AgentData = data;
							data.param.Bind(agent);
							this.RegisterActor(i, agent);
							this.RegisterAgent(i, agent);
							if (existsBackup)
							{
								if (!Map.TutorialMode)
								{
									if (agentProfile.BlackListInSaveAndLoad.Contains(data.ModeType))
									{
										data.ModeType = Desire.ActionType.Normal;
									}
									if (data.ModeType == Desire.ActionType.Date || data.ModeType == Desire.ActionType.Onbu)
									{
										data.ModeType = Desire.ActionType.Normal;
									}
									if (data.PrevMapID != null)
									{
										data.ModeType = Desire.ActionType.Normal;
									}
								}
								agent.Mode = data.ModeType;
								agent.PrevMode = data.PrevMode;
								if (i == 0 && Map.TutorialMode)
								{
									Actor agent6 = agent;
									Desire.ActionType actionType = Desire.ActionType.Idle;
									data.ModeType = actionType;
									actionType = actionType;
									agent.PrevActionMode = actionType;
									actionType = actionType;
									agent.PrevMode = actionType;
									agent6.Mode = actionType;
									agent.PrevActionMode = Desire.ActionType.Idle;
									agent.TutorialType = data.TutorialModeType;
									agent.TutorialMode = true;
									this.TutorialAgent = agent;
									agent.CreateTutorialBehaviorResources();
								}
								Quaternion rotation;
								Vector3 pos;
								if (data.PrevMapID != null)
								{
									if (this.MapID == 0)
									{
										DevicePoint devicePoint = this.PointAgent.DevicePointDic[i];
										pos = devicePoint.RecoverPoints[0].position;
										rotation = devicePoint.RecoverPoints[0].rotation;
									}
									else
									{
										DevicePoint devicePoint2 = this.PointAgent.DevicePoints[0];
										pos = devicePoint2.RecoverPoints[i].position;
										rotation = devicePoint2.RecoverPoints[i].rotation;
									}
									data.PrevMapID = null;
								}
								else
								{
									pos = data.Position;
									rotation = data.Rotation;
								}
								agent.NavMeshAgent.Warp(pos);
								(from _ in Observable.EveryUpdate().TakeUntilDestroy(agent.gameObject)
								where agent.NavMeshAgent.enabled
								select _).TakeWhile((long _) => !agent.NavMeshAgent.isOnNavMesh).Subscribe(delegate(long _)
								{
									agent.NavMeshAgent.Warp(pos);
								});
								agent.Rotation = rotation;
								Chunk chunk;
								this.ChunkTable.TryGetValue(data.ChunkID, out chunk);
								foreach (MapArea mapArea in chunk.MapAreas)
								{
									if (mapArea.AreaID == data.AreaID)
									{
										agent.MapArea = mapArea;
									}
								}
							}
							else
							{
								StoryPoint storyPoint = Map.GetStoryPoint(0);
								if (Map.TutorialMode && i == 0 && storyPoint != null)
								{
									Actor agent2 = agent;
									Desire.ActionType actionType = Desire.ActionType.Idle;
									data.ModeType = actionType;
									actionType = actionType;
									agent.PrevActionMode = actionType;
									actionType = actionType;
									agent.PrevMode = actionType;
									agent2.Mode = actionType;
									AgentActor agent3 = agent;
									AIProject.Definitions.Tutorial.ActionType actionType2 = AIProject.Definitions.Tutorial.ActionType.Idle;
									data.TutorialModeType = actionType2;
									agent3.TutorialType = actionType2;
									agent.MapArea = storyPoint.OwnerArea;
									Transform transform = storyPoint.transform.FindLoop("heroine_point").transform;
									Actor agent4 = agent;
									Vector3? vector = (transform != null) ? new Vector3?(transform.position) : null;
									agent4.Position = ((vector == null) ? (storyPoint.Position + this.Player.Rotation * storyPoint.Forward * 3f) : vector.Value);
									Actor agent5 = agent;
									Quaternion? quaternion = (transform != null) ? new Quaternion?(transform.rotation) : null;
									agent5.Rotation = ((quaternion == null) ? storyPoint.Rotation : quaternion.Value);
									agent.TutorialMode = true;
									this.TutorialAgent = agent;
									agent.CreateTutorialBehaviorResources();
								}
								else if (i == 0)
								{
									if (!this.FirstAgentPositionSetting(agent))
									{
										this.AgentPositionSetting(agent);
									}
								}
								else
								{
									this.AgentPositionSetting(agent);
								}
							}
							agent.Relocate();
							agentObj.SetActiveSafe(true);
							yield return agent.LoadAsync();
							if (!existsBackup)
							{
								agent.SetDefaultImmoral();
								foreach (KeyValuePair<int, float> keyValuePair in agent.ChaControl.fileGameInfo.desireDefVal)
								{
									agent.AgentData.DesireTable[keyValuePair.Key] = keyValuePair.Value;
								}
							}
							yield return null;
						}
					}
				}
			}
			yield return null;
			yield break;
		}

		// Token: 0x06003D9C RID: 15772 RVA: 0x001690B8 File Offset: 0x001674B8
		private bool FirstAgentPositionSetting(AgentActor agent)
		{
			StoryPoint storyPoint = Map.GetStoryPoint(3);
			if (storyPoint == null || agent == null)
			{
				return false;
			}
			if (agent.NavMeshAgent.isActiveAndEnabled)
			{
				agent.NavMeshAgent.Warp(storyPoint.Position);
			}
			else
			{
				agent.Position = storyPoint.Position;
			}
			agent.Rotation = storyPoint.Rotation;
			agent.MapArea = storyPoint.OwnerArea;
			return true;
		}

		// Token: 0x06003D9D RID: 15773 RVA: 0x00169134 File Offset: 0x00167534
		private bool FirstAgentDataPositionSetting(AgentData data)
		{
			if (data == null)
			{
				return false;
			}
			StoryPoint storyPoint = Map.GetStoryPoint(3);
			if (storyPoint == null)
			{
				return false;
			}
			data.Position = storyPoint.Position;
			data.Rotation = storyPoint.Rotation;
			return true;
		}

		// Token: 0x06003D9E RID: 15774 RVA: 0x00169178 File Offset: 0x00167578
		private void AgentPositionSetting(AgentActor agent)
		{
			int key = 0;
			Chunk chunk;
			this.ChunkTable.TryGetValue(key, out chunk);
			List<Waypoint> waypoints = chunk.MapAreas[0].Waypoints;
			List<Waypoint> list = waypoints.FindAll(delegate(Waypoint x)
			{
				Vector3 position = this.Player.Position;
				float num = Vector3.Distance(x.transform.position, position);
				return num > 50f;
			});
			if (global::Debug.isDebugBuild)
			{
			}
			Waypoint element = list.GetElement(UnityEngine.Random.Range(0, list.Count));
			agent.Position = element.transform.position;
			agent.MapArea = chunk.MapAreas[0];
		}

		// Token: 0x06003D9F RID: 15775 RVA: 0x001691F8 File Offset: 0x001675F8
		public void LoadAgentTargetActionPoint()
		{
			using (Dictionary<int, AgentActor>.Enumerator enumerator = this._agentTable.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<int, AgentActor> agent = enumerator.Current;
					ActionPoint actionPoint = this.PointAgent.ActionPoints.FirstOrDefault((ActionPoint x) => x.RegisterID == agent.Value.AgentData.CurrentActionPointID);
					if (actionPoint == null)
					{
						actionPoint = this.PointAgent.AppendActionPoints.Find((ActionPoint x) => x.RegisterID == agent.Value.AgentData.CurrentActionPointID);
					}
					if (actionPoint != null)
					{
						if (!actionPoint.AgentEventType.Contains(AIProject.EventType.Move))
						{
							if (!actionPoint.AgentEventType.Contains(AIProject.EventType.DoorOpen))
							{
								agent.Value.TargetInSightActionPoint = actionPoint;
							}
						}
					}
				}
			}
		}

		// Token: 0x06003DA0 RID: 15776 RVA: 0x001692F0 File Offset: 0x001676F0
		public void LoadAgentTargetActor()
		{
			foreach (KeyValuePair<int, AgentActor> keyValuePair in this._agentTable)
			{
				Actor targetInSightActor;
				if (this._actorTable.TryGetValue(keyValuePair.Value.AgentData.ActionTargetID, out targetInSightActor))
				{
					keyValuePair.Value.TargetInSightActor = targetInSightActor;
				}
			}
		}

		// Token: 0x06003DA1 RID: 15777 RVA: 0x0016937C File Offset: 0x0016777C
		public void RemovePlayer(PlayerActor player)
		{
			this.UnregisterActor(player.ID);
		}

		// Token: 0x06003DA2 RID: 15778 RVA: 0x0016938C File Offset: 0x0016778C
		public AgentActor AddAgent(int id, AgentData agentData)
		{
			if (this._agentPrefab == null)
			{
				DefinePack definePack = Singleton<Resources>.Instance.DefinePack;
				string bundlePath = definePack.ABPaths.ActorPrefab;
				string manifest = definePack.ABManifests.Default;
				this._agentPrefab = CommonLib.LoadAsset<GameObject>(bundlePath, "Agent", false, manifest);
				if (!MapScene.AssetBundlePaths.Exists((UnityEx.ValueTuple<string, string> x) => x.Item1 == bundlePath && x.Item2 == manifest))
				{
					MapScene.AssetBundlePaths.Add(new UnityEx.ValueTuple<string, string>(bundlePath, manifest));
				}
			}
			int[] array = this.ChunkTable.Keys.ToArray<int>();
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this._agentPrefab, this.ActorRoot);
			gameObject.name = string.Format("Heroine_{0}", id.ToString("00"));
			AgentActor component = gameObject.GetComponent<AgentActor>();
			component.ID = id;
			component.AgentData = agentData;
			int num;
			if (Singleton<Resources>.Instance.AgentProfile.DefaultAreaIDTable.TryGetValue(id, out num))
			{
				Actor actor = component;
				int areaID = num;
				agentData.AreaID = areaID;
				actor.AreaID = areaID;
				foreach (KeyValuePair<int, Chunk> keyValuePair in this.ChunkTable)
				{
					foreach (MapArea mapArea in keyValuePair.Value.MapAreas)
					{
						if (mapArea.AreaID == num)
						{
							component.MapArea = mapArea;
						}
					}
				}
			}
			agentData.param.Bind(component);
			this.RegisterActor(id, component);
			this.RegisterAgent(id, component);
			if (Singleton<AnimalManager>.IsInstance())
			{
				Singleton<AnimalManager>.Instance.AddTargetAnimals(component);
			}
			component.Relocate();
			gameObject.SetActiveSafe(true);
			component.Load();
			return component;
		}

		// Token: 0x06003DA3 RID: 15779 RVA: 0x00169594 File Offset: 0x00167994
		public void RemoveAgent(AgentActor agent)
		{
			agent.DisableBehavior();
			agent.BehaviorResources.DisableAllBehaviors();
			foreach (KeyValuePair<int, Dictionary<int, UnityEx.ValueTuple<AudioSource, FadePlayer>>> keyValuePair in agent.Animation.ActionLoopSETable)
			{
				foreach (KeyValuePair<int, UnityEx.ValueTuple<AudioSource, FadePlayer>> keyValuePair2 in keyValuePair.Value)
				{
					if (!(keyValuePair2.Value.Item2 == null))
					{
						keyValuePair2.Value.Item2.Stop(0f);
					}
				}
			}
			agent.Animation.ActionLoopSETable.Clear();
			this.Player.PlayerController.CommandArea.RemoveCommandableObject(agent);
			this.UnregisterAgent(agent.ID);
			this.UnregisterActor(agent.ID);
			this.RemoveTableAgentChaFile(agent.ID);
			foreach (KeyValuePair<int, AgentActor> keyValuePair3 in this.AgentTable)
			{
				if (!(keyValuePair3.Value == agent))
				{
					keyValuePair3.Value.RemoveActor(agent);
				}
			}
			Singleton<Character>.Instance.DeleteChara(agent.ChaControl, false);
			UnityEngine.Object.Destroy(agent.gameObject);
		}

		// Token: 0x06003DA4 RID: 15780 RVA: 0x00169750 File Offset: 0x00167B50
		private IEnumerator LoadMerchantAsync(MerchantData merchantData, bool existsBackup)
		{
			DefinePack define = Singleton<Resources>.Instance.DefinePack;
			string bundlePath = define.ABPaths.ActorPrefab;
			string manifest = define.ABManifests.Default;
			GameObject merchantPrefab = CommonLib.LoadAsset<GameObject>(bundlePath, "Merchant", false, manifest);
			if (!MapScene.AssetBundlePaths.Exists((UnityEx.ValueTuple<string, string> x) => x.Item1 == bundlePath && x.Item2 == manifest))
			{
				MapScene.AssetBundlePaths.Add(new UnityEx.ValueTuple<string, string>(bundlePath, manifest));
			}
			GameObject obj = UnityEngine.Object.Instantiate<GameObject>(merchantPrefab, this.ActorRoot);
			obj.name = "Merchant";
			MerchantActor merchant = obj.GetComponent<MerchantActor>();
			merchant.ID = -90;
			if (merchantData == null)
			{
				merchantData = new MerchantData();
			}
			merchant.SetMerchantPoints(this._pointAgent.MerchantPoints);
			merchant.OpenAreaID = 0;
			if (existsBackup)
			{
				merchant.DataUpdate(merchantData);
			}
			else if (merchant.StartPoint != null)
			{
				Transform basePoint = merchant.StartPoint.GetBasePoint(AIProject.Definitions.Merchant.EventType.Wait);
				merchant.Position = basePoint.position;
				merchant.Rotation = basePoint.rotation;
				merchant.TargetInSightMerchantPoint = merchant.StartPoint;
			}
			else
			{
				int[] source = this.ChunkTable.Keys.ToArray<int>();
				int element = source.GetElement(0);
				Chunk chunk;
				this.ChunkTable.TryGetValue(element, out chunk);
				List<Waypoint> waypoints = chunk.MapAreas[0].Waypoints;
				List<Waypoint> list = waypoints.FindAll(delegate(Waypoint x)
				{
					Vector3 position = this.Player.Position;
					float num = Vector3.Distance(x.transform.position, position);
					return 50f < num;
				});
				Waypoint element2 = list.GetElement(UnityEngine.Random.Range(0, list.Count));
				merchant.Position = element2.transform.position;
			}
			merchant.Relocate();
			merchant.SetActiveSafe(true);
			merchant.MerchantData = merchantData;
			merchantData.param.Bind(merchant);
			yield return merchant.LoadAsync();
			merchant.SetOpenAreaID(this);
			if (!merchant.MerchantData.Unlock && !Map.TutorialMode)
			{
				merchant.MerchantData.Unlock = true;
			}
			this.RegisterMerchant(merchant);
			this.RegisterActor(-90, merchant);
			if (existsBackup)
			{
				merchant.ChangeMap(this.MapID);
				if (this.MapID == 0 && merchantData.ModeType == AIProject.Definitions.Merchant.ActionType.Absent)
				{
					merchant.Hide();
					if (merchant.ExitPoint != null)
					{
						merchant.ExitPoint.HideItemObjects();
					}
				}
			}
			yield break;
		}

		// Token: 0x06003DA5 RID: 15781 RVA: 0x0016977C File Offset: 0x00167B7C
		public IEnumerator LoadAnimals(WorldData profile, bool existsBackup)
		{
			if (this._pointAgent == null || profile == null || !Singleton<AnimalManager>.IsInstance())
			{
				yield break;
			}
			AIProject.SaveData.Environment envData = profile.Environment;
			if (envData == null)
			{
				yield break;
			}
			yield return null;
			Dictionary<int, AIProject.SaveData.Environment.PetHomeInfo> homePetDataTable = envData.PetHomeStateTable;
			IReadOnlyDictionary<int, PetHomePoint> petPointTable = this._pointAgent.PetHomePointTable;
			if (!homePetDataTable.IsNullOrEmpty<int, AIProject.SaveData.Environment.PetHomeInfo>() || petPointTable == null || petPointTable.Count == 0)
			{
				foreach (KeyValuePair<int, AIProject.SaveData.Environment.PetHomeInfo> pair in homePetDataTable)
				{
					AIProject.SaveData.Environment.PetHomeInfo value = pair.Value;
					AIProject.SaveData.AnimalData data = (value != null) ? value.AnimalData : null;
					if (data != null)
					{
						int registerID = pair.Key;
						PetHomePoint habitatPoint;
						if (petPointTable.TryGetValue(registerID, out habitatPoint) && !(habitatPoint == null))
						{
							IPetAnimal petAnimal = null;
							int rootPointID = 0;
							if (!data.InitAnimalTypeID || data.AnimalTypeID < 0)
							{
								data.AnimalTypeID = AIProject.Animal.AnimalData.GetAnimalTypeID(data.AnimalType);
								data.InitAnimalTypeID = true;
							}
							PetHomePoint.HomeKind kind = habitatPoint.Kind;
							if (kind != PetHomePoint.HomeKind.PetMat)
							{
								if (kind == PetHomePoint.HomeKind.FishTank)
								{
									Dictionary<int, Dictionary<int, FishInfo>> dictionary = (!Singleton<Resources>.IsInstance()) ? null : Singleton<Resources>.Instance.Fishing.FishInfoTable;
									Dictionary<int, FishInfo> dictionary2;
									FishInfo fishInfo;
									if (!dictionary.IsNullOrEmpty<int, Dictionary<int, FishInfo>>() && dictionary.TryGetValue(data.ItemCategoryID, out dictionary2) && !dictionary2.IsNullOrEmpty<int, FishInfo>() && dictionary2.TryGetValue(data.ItemID, out fishInfo))
									{
										petAnimal = (Singleton<AnimalManager>.Instance.CreateBase(data.AnimalTypeID, (int)data.BreedingType) as IPetAnimal);
										rootPointID = fishInfo.TankPointID;
									}
								}
							}
							else
							{
								petAnimal = (Singleton<AnimalManager>.Instance.CreateBase(data.AnimalTypeID, (int)data.BreedingType) as IPetAnimal);
							}
							if (petAnimal != null)
							{
								data.AnimalID = petAnimal.AnimalID;
								petAnimal.AnimalData = data;
								IGroundPet groundPet = petAnimal as IGroundPet;
								if (groundPet != null)
								{
									groundPet.ChaseActor = pair.Value.ChaseActor;
								}
								habitatPoint.SetUser(petAnimal);
								habitatPoint.SetRootPoint(rootPointID, petAnimal);
								if (groundPet != null && groundPet.ChaseActor)
								{
									groundPet.Position = data.Position;
									groundPet.Rotation = data.Rotation;
								}
								yield return null;
							}
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x06003DA6 RID: 15782 RVA: 0x001697A0 File Offset: 0x00167BA0
		private void AddTutorialItems()
		{
			List<StuffItem> itemList = this.Player.PlayerData.ItemList;
			FishingDefinePack.ItemIDPair fishingRodID = Singleton<Resources>.Instance.FishingDefinePack.IDInfo.FishingRod;
			if (!itemList.Exists((StuffItem x) => x.CategoryID == fishingRodID.CategoryID && x.ID == fishingRodID.ItemID && 0 < x.Count))
			{
				itemList.AddItem(new StuffItem(fishingRodID.CategoryID, fishingRodID.ItemID, 1));
			}
		}

		// Token: 0x06003DA7 RID: 15783 RVA: 0x00169818 File Offset: 0x00167C18
		public void CreateTutorialLockArea()
		{
			string mapScenePrefab = Singleton<Resources>.Instance.DefinePack.ABPaths.MapScenePrefab;
			string assetName = "p_ai_mi_tutorialmesh00";
			GameObject original = CommonLib.LoadAsset<GameObject>(mapScenePrefab, assetName, false, string.Empty);
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(original);
			gameObject.transform.SetParent(this._mapRoot, false);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.identity;
			gameObject.transform.localScale = Vector3.one;
			this.TutorialLockAreaObject = gameObject;
			MapScene.AddAssetBundlePath(mapScenePrefab, string.Empty);
		}

		// Token: 0x06003DA8 RID: 15784 RVA: 0x001698A9 File Offset: 0x00167CA9
		public void DestroyTutorialLockArea()
		{
			if (this.TutorialLockAreaObject != null)
			{
				UnityEngine.Object.Destroy(this.TutorialLockAreaObject);
				this.TutorialLockAreaObject = null;
			}
		}

		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x06003DA9 RID: 15785 RVA: 0x001698CE File Offset: 0x00167CCE
		// (set) Token: 0x06003DAA RID: 15786 RVA: 0x001698D6 File Offset: 0x00167CD6
		public GameObject TutorialLockAreaObject { get; private set; }

		// Token: 0x06003DAB RID: 15787 RVA: 0x001698E0 File Offset: 0x00167CE0
		public void CreateTutorialSearchPoint()
		{
			if (this._tutorialSearchPointObject != null)
			{
				return;
			}
			string mapScenePrefab = Singleton<Resources>.Instance.DefinePack.ABPaths.MapScenePrefab;
			string assetName = "TutorialSearchPoint_00";
			string @default = Singleton<Resources>.Instance.DefinePack.ABManifests.Default;
			GameObject gameObject = CommonLib.LoadAsset<GameObject>(mapScenePrefab, assetName, false, @default);
			if (gameObject == null)
			{
				return;
			}
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
			gameObject2.transform.SetParent(this._mapRoot, false);
			this._tutorialSearchPointObject = gameObject2;
			MapScene.AddAssetBundlePath(mapScenePrefab, @default);
		}

		// Token: 0x06003DAC RID: 15788 RVA: 0x00169970 File Offset: 0x00167D70
		public void DestroyTutorialSearchPoint()
		{
			if (this._tutorialSearchPointObject != null)
			{
				UnityEngine.Object.Destroy(this._tutorialSearchPointObject);
				this._tutorialSearchPointObject = null;
			}
		}

		// Token: 0x06003DAD RID: 15789 RVA: 0x00169998 File Offset: 0x00167D98
		public void CreateStoryPointEffect()
		{
			if (this.StoryPointEffect != null)
			{
				return;
			}
			string mapScenePrefab = Singleton<Resources>.Instance.DefinePack.ABPaths.MapScenePrefab;
			string assetName = "StoryPointEffect";
			string @default = Singleton<Resources>.Instance.DefinePack.ABManifests.Default;
			GameObject gameObject = CommonLib.LoadAsset<GameObject>(mapScenePrefab, assetName, false, @default);
			if (gameObject == null)
			{
				return;
			}
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
			gameObject2.transform.SetParent(this._pointAgent.transform, false);
			this.StoryPointEffect = gameObject2.GetComponent<StoryPointEffect>();
			if (this.StoryPointEffect == null)
			{
				UnityEngine.Object.Destroy(gameObject2);
			}
			else
			{
				StoryPointEffect.Switch = true;
			}
			MapScene.AddAssetBundlePath(mapScenePrefab, @default);
		}

		// Token: 0x06003DAE RID: 15790 RVA: 0x00169A55 File Offset: 0x00167E55
		public void DestroyStoryPointEffect()
		{
			if (this.StoryPointEffect == null)
			{
				return;
			}
			UnityEngine.Object.Destroy(this.StoryPointEffect.gameObject);
			this.StoryPointEffect = null;
		}

		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x06003DAF RID: 15791 RVA: 0x00169A80 File Offset: 0x00167E80
		// (set) Token: 0x06003DB0 RID: 15792 RVA: 0x00169A88 File Offset: 0x00167E88
		public StoryPointEffect StoryPointEffect { get; private set; }

		// Token: 0x06003DB1 RID: 15793 RVA: 0x00169A91 File Offset: 0x00167E91
		public void RegisterPlayer(PlayerActor player)
		{
			if (this.Player == player)
			{
				return;
			}
			this.Player = player;
		}

		// Token: 0x06003DB2 RID: 15794 RVA: 0x00169AAC File Offset: 0x00167EAC
		public void RegisterActor(int id, Actor actor)
		{
			Actor actor2;
			if (this._actorTable.TryGetValue(id, out actor2))
			{
			}
			this._actorTable[id] = actor;
			if (!this.Actors.Contains(actor))
			{
				this.Actors.Add(actor);
			}
		}

		// Token: 0x06003DB3 RID: 15795 RVA: 0x00169AF6 File Offset: 0x00167EF6
		public void UnregisterActor(int id)
		{
			if (this._actorTable.ContainsKey(id))
			{
				this._actorTable.Remove(id);
			}
		}

		// Token: 0x06003DB4 RID: 15796 RVA: 0x00169B18 File Offset: 0x00167F18
		public void RegisterAgent(int id, AgentActor agent)
		{
			AgentActor agentActor;
			if (this._agentTable.TryGetValue(id, out agentActor))
			{
			}
			this._agentTable[id] = agent;
			if (!this._agentKeys.Contains(id))
			{
				this._agentKeys.Add(id);
			}
		}

		// Token: 0x06003DB5 RID: 15797 RVA: 0x00169B62 File Offset: 0x00167F62
		public void UnregisterAgent(int id)
		{
			if (this._agentKeys.Contains(id))
			{
				this._agentKeys.Remove(id);
			}
			if (this._agentTable.ContainsKey(id))
			{
				this._agentTable.Remove(id);
			}
		}

		// Token: 0x06003DB6 RID: 15798 RVA: 0x00169BA0 File Offset: 0x00167FA0
		public void SetTableAgentChaFile(int id, ChaFile chaFile)
		{
			this._agentChaFileTable[id] = chaFile;
		}

		// Token: 0x06003DB7 RID: 15799 RVA: 0x00169BAF File Offset: 0x00167FAF
		public void RemoveTableAgentChaFile(int id)
		{
			if (this._agentChaFileTable.ContainsKey(id))
			{
				this._agentChaFileTable.Remove(id);
			}
		}

		// Token: 0x06003DB8 RID: 15800 RVA: 0x00169BCF File Offset: 0x00167FCF
		public void RegisterMerchant(MerchantActor merchant)
		{
			if (this.Merchant == merchant)
			{
				return;
			}
			this.Merchant = merchant;
		}

		// Token: 0x06003DB9 RID: 15801 RVA: 0x00169BEC File Offset: 0x00167FEC
		public void ApplyConfig(System.Action onCompleteIn, System.Action onCompleteOut)
		{
			if (Map.TutorialMode)
			{
				System.Action onCompleteOut2 = onCompleteOut;
				if (onCompleteOut2 != null)
				{
					onCompleteOut2();
				}
				return;
			}
			MapUIContainer.StartFade(FadeCanvas.PanelType.Blackout, FadeType.In, 1f, true).Subscribe(delegate(Unit _)
			{
			}, delegate()
			{
				System.Action onCompleteIn2 = onCompleteIn;
				if (onCompleteIn2 != null)
				{
					onCompleteIn2();
				}
				bool[] charasEntry = Config.GraphicData.CharasEntry;
				Dictionary<int, AgentData> agentTable = Singleton<Game>.Instance.WorldData.AgentTable;
				ReadOnlyDictionary<int, AgentActor> agentTable2 = Singleton<Map>.Instance.AgentTable;
				ChaFileControl chaFileControl = new ChaFileControl();
				foreach (KeyValuePair<int, AgentData> keyValuePair in Singleton<Game>.Instance.WorldData.AgentTable)
				{
					string charaFileName = keyValuePair.Value.CharaFileName;
					AgentActor agentActor2;
					if (charasEntry[keyValuePair.Key] && !charaFileName.IsNullOrEmpty() && chaFileControl.LoadCharaFile(charaFileName, 1, false, true) && keyValuePair.Value.MapID == this.MapID)
					{
						if (!agentTable2.ContainsKey(keyValuePair.Key))
						{
							AgentActor agentActor = Singleton<Map>.Instance.AddAgent(keyValuePair.Key, keyValuePair.Value);
							agentActor.RefreshWalkStatus(this.PointAgent.Waypoints);
							Singleton<Map>.Instance.InitSearchActorTargets(agentActor);
							this.Player.PlayerController.CommandArea.AddCommandableObject(agentActor);
							foreach (KeyValuePair<int, AgentActor> keyValuePair2 in Singleton<Map>.Instance.AgentTable)
							{
								if (!(keyValuePair2.Value == agentActor))
								{
									keyValuePair2.Value.AddActor(agentActor);
								}
							}
							agentActor.ActivateNavMeshAgent();
							Transform transform;
							if (this.MapID == 0)
							{
								transform = this.PointAgent.DevicePointDic[keyValuePair.Key].RecoverPoints[0].transform;
							}
							else
							{
								transform = this.PointAgent.DevicePointDic[0].RecoverPoints[keyValuePair.Key].transform;
							}
							agentActor.NavMeshAgent.Warp(transform.position);
							agentActor.Rotation = transform.rotation;
							agentActor.EnableBehavior();
							agentActor.ChangeBehavior(Desire.ActionType.Normal);
						}
					}
					else if (agentTable2.TryGetValue(keyValuePair.Key, out agentActor2))
					{
						agentActor2.DisableBehavior();
						agentActor2.ClearItems();
						agentActor2.ClearParticles();
						Actor.BehaviorSchedule schedule = agentActor2.Schedule;
						schedule.enabled = false;
						agentActor2.Schedule = schedule;
						agentActor2.TargetInSightActor = null;
						if (agentActor2.CurrentPoint != null)
						{
							agentActor2.CurrentPoint.SetActiveMapItemObjs(true);
							agentActor2.CurrentPoint.ReleaseSlot(agentActor2);
							agentActor2.CurrentPoint = null;
						}
						agentActor2.TargetInSightActionPoint = null;
						if (agentActor2.Partner != null)
						{
							if (agentActor2.Partner is PlayerActor)
							{
								PlayerActor playerActor = agentActor2.Partner as PlayerActor;
								agentActor2.ActivateHoldingHands(0, false);
								if (playerActor.PlayerController.State is Normal || playerActor.PlayerController.State is Houchi || playerActor.PlayerController.State is Onbu)
								{
									playerActor.PlayerController.ChangeState("Normal");
									playerActor.Mode = Desire.ActionType.Normal;
									playerActor.Partner = null;
									playerActor.ActivateTransfer();
								}
								else if (playerActor.PlayerController.State is Menu)
								{
									playerActor.PlayerController.ChangeState("Menu");
									playerActor.PlayerController.PrevStateName = "Normal";
									playerActor.Mode = Desire.ActionType.Normal;
									playerActor.Partner = null;
									playerActor.ActivateTransfer();
								}
							}
							else if (agentActor2.Partner is AgentActor)
							{
								AgentActor agentActor3 = agentActor2.Partner as AgentActor;
								agentActor2.StopLesbianSequence();
								agentActor3.StopLesbianSequence();
								agentActor3.Animation.EndIgnoreEvent();
								Game.Expression expression = Singleton<Game>.Instance.GetExpression(agentActor3.ChaControl.fileParam.personality, "標準");
								if (expression != null)
								{
									expression.Change(agentActor3.ChaControl);
								}
								agentActor3.Animation.ResetDefaultAnimatorController();
								agentActor3.ChangeBehavior(Desire.ActionType.Normal);
							}
							else if (agentActor2.Partner is MerchantActor)
							{
								MerchantActor merchantActor = agentActor2.Partner as MerchantActor;
								agentActor2.StopLesbianSequence();
								merchantActor.ResetState();
								merchantActor.ChangeBehavior(merchantActor.LastNormalMode);
							}
							agentActor2.Partner = null;
						}
						if (agentActor2.CommandPartner != null)
						{
							if (agentActor2.CommandPartner is AgentActor)
							{
								AgentActor agentActor4 = agentActor2.CommandPartner as AgentActor;
								agentActor4.ChangeBehavior(Desire.ActionType.Normal);
							}
							else if (agentActor2.CommandPartner is MerchantActor)
							{
								MerchantActor merchantActor2 = agentActor2.CommandPartner as MerchantActor;
								merchantActor2.ChangeBehavior(merchantActor2.LastNormalMode);
							}
							agentActor2.CommandPartner = null;
						}
						agentActor2.ChaControl.chaFile.SaveCharaFile(agentActor2.ChaControl.chaFile.charaFileName, byte.MaxValue, false);
						this.RemoveAgent(agentActor2);
					}
				}
				MapUIContainer.StartFade(FadeCanvas.PanelType.Blackout, FadeType.Out, 1f, true).Subscribe(delegate(Unit __)
				{
				}, delegate()
				{
					System.Action onCompleteOut3 = onCompleteOut;
					if (onCompleteOut3 != null)
					{
						onCompleteOut3();
					}
				});
			});
		}

		// Token: 0x06003DBA RID: 15802 RVA: 0x00169C70 File Offset: 0x00168070
		public void ReleaseComponents()
		{
			if (this.Player != null)
			{
				UnityEngine.Object.Destroy(this.Player.gameObject);
				this.Player = null;
			}
			foreach (KeyValuePair<int, AgentActor> keyValuePair in this._agentTable)
			{
				if (!(keyValuePair.Value == null))
				{
					UnityEngine.Object.Destroy(keyValuePair.Value.gameObject);
				}
			}
			this._agentTable.Clear();
			if (this.Merchant != null)
			{
				UnityEngine.Object.Destroy(this.Merchant.gameObject);
				this.Merchant = null;
			}
			this._actorTable.Clear();
			this._agentKeys.Clear();
			this._agentChaFileTable.Clear();
			if (Singleton<Character>.IsInstance())
			{
				Singleton<Character>.Instance.DeleteCharaAll();
				Singleton<Character>.Instance.EndLoadAssetBundle(false);
			}
			if (Singleton<Housing>.IsInstance())
			{
				Singleton<Housing>.Instance.Release();
			}
			this.HousingPointTable.Clear();
			this.ChunkTable.Clear();
		}

		// Token: 0x06003DBB RID: 15803 RVA: 0x00169DB4 File Offset: 0x001681B4
		private void ReleaseAgents()
		{
			int[] array = this._agentKeys.ToArray();
			foreach (int key in array)
			{
				AgentActor agent;
				if (this._agentTable.TryGetValue(key, out agent))
				{
					this.RemoveAgent(agent);
				}
			}
		}

		// Token: 0x06003DBC RID: 15804 RVA: 0x00169E08 File Offset: 0x00168208
		private void ReleaseMap()
		{
			Singleton<Housing>.Instance.DeleteRoot();
			Singleton<Housing>.Instance.Release();
			this.HousingPointTable.Clear();
			if (this.NavMeshSrc != null)
			{
				UnityEngine.Object.Destroy(this.NavMeshSrc.gameObject);
				this.NavMeshSrc = null;
			}
			if (this.ChunkSrc != null)
			{
				UnityEngine.Object.Destroy(this.ChunkSrc.gameObject);
				this.ChunkSrc = null;
			}
			this._pointAgent.Release();
		}

		// Token: 0x06003DBD RID: 15805 RVA: 0x00169E90 File Offset: 0x00168290
		public void InitSearchActorTargetsAll()
		{
			foreach (KeyValuePair<int, AgentActor> keyValuePair in this.AgentTable)
			{
				this.InitSearchActorTargets(keyValuePair.Value);
			}
		}

		// Token: 0x06003DBE RID: 15806 RVA: 0x00169EF0 File Offset: 0x001682F0
		public void InitSearchActorTargets(AgentActor agent)
		{
			agent.AddActor(this.Player);
			agent.AddActor(this.Merchant);
			foreach (KeyValuePair<int, AgentActor> keyValuePair in this.AgentTable)
			{
				if (!(keyValuePair.Value == agent))
				{
					agent.AddActor(keyValuePair.Value);
				}
			}
		}

		// Token: 0x06003DBF RID: 15807 RVA: 0x00169F80 File Offset: 0x00168380
		private void AreaTypeUpdate(Actor actor)
		{
			if (actor == null)
			{
				return;
			}
			LayerMask roofLayer = Singleton<Resources>.Instance.DefinePack.MapDefines.RoofLayer;
			RaycastHit raycastHit;
			actor.AreaType = ((!Physics.Raycast(actor.Position, Vector3.up, out raycastHit, 1000f, roofLayer)) ? MapArea.AreaType.Normal : MapArea.AreaType.Indoor);
		}

		// Token: 0x06003DC0 RID: 15808 RVA: 0x00169FE0 File Offset: 0x001683E0
		private void OnElapsedDay(TimeSpan timeSpan)
		{
			if (this.Merchant != null)
			{
				this.Merchant.OnDayUpdated(timeSpan);
			}
			List<int> agentKeys = this._agentKeys;
			foreach (int key in agentKeys)
			{
				AgentActor agentActor;
				if (this._agentTable.TryGetValue(key, out agentActor))
				{
					agentActor.OnDayUpdated(timeSpan);
				}
			}
		}

		// Token: 0x06003DC1 RID: 15809 RVA: 0x0016A074 File Offset: 0x00168474
		private void OnElapsedMinute(TimeSpan timeSpan)
		{
			this.Player.OnMinuteUpdated(timeSpan);
			if (this.Merchant != null)
			{
				this.Merchant.OnMinuteUpdated(timeSpan);
			}
			List<int> agentKeys = this._agentKeys;
			foreach (int key in agentKeys)
			{
				AgentActor agentActor;
				if (this._agentTable.TryGetValue(key, out agentActor))
				{
					agentActor.OnMinuteUpdated(timeSpan);
				}
			}
		}

		// Token: 0x06003DC2 RID: 15810 RVA: 0x0016A114 File Offset: 0x00168514
		private void OnElapsedSecond(TimeSpan timeSpan)
		{
			List<int> agentKeys = this._agentKeys;
			foreach (int key in agentKeys)
			{
				AgentActor agentActor;
				if (this._agentTable.TryGetValue(key, out agentActor))
				{
					agentActor.OnSecondUpdated(timeSpan);
				}
			}
		}

		// Token: 0x06003DC3 RID: 15811 RVA: 0x0016A18C File Offset: 0x0016858C
		public void RefreshWeather(Weather weather)
		{
			foreach (KeyValuePair<int, AgentActor> keyValuePair in this._agentTable)
			{
				AgentActor value = keyValuePair.Value;
				this.RefreshAgentLocomotionStatus(value);
			}
		}

		// Token: 0x06003DC4 RID: 15812 RVA: 0x0016A1F0 File Offset: 0x001685F0
		public void RefreshPlayerLocomotionStatus()
		{
		}

		// Token: 0x06003DC5 RID: 15813 RVA: 0x0016A1F4 File Offset: 0x001685F4
		public void RefreshAgentLocomotionStatus(AgentActor agent)
		{
			if (!agent.NavMeshAgent.enabled || agent.NavMeshAgent.isStopped)
			{
				return;
			}
			bool flag = false;
			AnimatorStateInfo currentAnimatorStateInfo = agent.Animation.Animator.GetCurrentAnimatorStateInfo(0);
			foreach (KeyValuePair<int, PlayState> keyValuePair in Singleton<Resources>.Instance.Animation.AgentLocomotionStateTable)
			{
				if (!flag)
				{
					foreach (PlayState.Info info in keyValuePair.Value.MainStateInfo.InStateInfo.StateInfos)
					{
						if (!flag)
						{
							if (currentAnimatorStateInfo.IsName(info.stateName))
							{
								flag = true;
								agent.ActivateTransfer(true);
								agent.AbortCalc();
								agent.AbortPatrol();
								agent.ClearWalkPath();
								agent.StartPatrol();
							}
						}
					}
				}
			}
		}

		// Token: 0x06003DC6 RID: 15814 RVA: 0x0016A314 File Offset: 0x00168714
		public void RefreshTimeZone(AIProject.TimeZone timeZone)
		{
			int[] array = this.AgentTable.Keys.ToArray<int>();
			foreach (int key in array)
			{
				AgentActor agentActor;
				if (this.AgentTable.TryGetValue(key, out agentActor))
				{
					AgentData agentData = agentActor.AgentData;
					if (!agentData.SickState.Enabled && agentData.SickState.UsedMedicine)
					{
						agentData.SickState.UsedMedicine = false;
					}
					float t;
					if (!agentData.StatsTable.TryGetValue(1, out t))
					{
						float num = 50f;
						agentData.StatsTable[1] = num;
						t = num;
					}
					Dictionary<int, Threshold> dictionary;
					Threshold threshold;
					if (agentActor.ChaControl.fileGameInfo.phase < 3)
					{
						agentData.StatsTable[5] = (float)(Singleton<Resources>.Instance.DefinePack.MapDefines.DefaultMotivation + agentActor.ChaControl.fileGameInfo.motivation);
					}
					else if (Singleton<Resources>.Instance.Action.PersonalityMotivation.TryGetValue(agentActor.ChaControl.fileParam.personality, out dictionary) && dictionary.TryGetValue(agentActor.ChaControl.fileGameInfo.phase, out threshold))
					{
						float num2 = threshold.Lerp(t) + (float)agentActor.ChaControl.fileGameInfo.motivation;
						if (agentActor.ChaControl.fileGameInfo.normalSkill.ContainsValue(9))
						{
							num2 += Singleton<Resources>.Instance.StatusProfile.GWifeMotivationBuff;
						}
						if (agentActor.ChaControl.fileGameInfo.normalSkill.ContainsValue(33))
						{
							num2 += Singleton<Resources>.Instance.StatusProfile.ActiveBuffMotivation;
						}
						agentData.StatsTable[5] = num2;
					}
					this.RefreshAgentLocomotionStatus(agentActor);
				}
			}
		}

		// Token: 0x06003DC7 RID: 15815 RVA: 0x0016A4FC File Offset: 0x001688FC
		private IEnumerator RefreshAsync()
		{
			Waypoint[] waypoints = this._pointAgent.Waypoints;
			BasePoint[] basePoints = this._pointAgent.BasePoints;
			DevicePoint[] devicePoints = this._pointAgent.DevicePoints;
			ActionPoint[] actionPoints = this._pointAgent.ActionPoints;
			FarmPoint[] farmPoints = this._pointAgent.FarmPoints;
			ShipPoint[] shipPoints = this._pointAgent.ShipPoints;
			MerchantPoint[] merchantPoints = this._pointAgent.MerchantPoints;
			EventPoint[] eventPoints = this._pointAgent.EventPoints;
			StoryPoint[] storyPoints = this._pointAgent.StoryPoints;
			AnimalActionPoint[] animalActionPoints = this._pointAgent.AnimalActionPoints;
			List<IConnectableObservable<Unit>> streams = ListPool<IConnectableObservable<Unit>>.Get();
			int[] chunkKeys = this.ChunkTable.Keys.ToArray<int>();
			int[] array = chunkKeys;
			for (int i = 0; i < array.Length; i++)
			{
				int key = array[i];
				Chunk chunk;
				if (this.ChunkTable.TryGetValue(key, out chunk))
				{
					IConnectableObservable<Unit> connectableObservable = Observable.FromCoroutine((CancellationToken _) => chunk.Load(waypoints, basePoints, devicePoints, farmPoints, shipPoints, actionPoints, merchantPoints, eventPoints, storyPoints, animalActionPoints), false).PublishLast<Unit>();
					connectableObservable.Connect();
					streams.Add(connectableObservable);
				}
			}
			yield return streams.WhenAll().ToYieldInstruction<Unit>();
			ListPool<IConnectableObservable<Unit>>.Release(streams);
			int ct = 0;
			foreach (Waypoint point in waypoints)
			{
				if (!(point.OwnerArea != null))
				{
					point.SetActiveSafe(false);
					int num;
					ct = (num = ct) + 1;
					if (num > 16)
					{
						ct %= 16;
						yield return null;
					}
				}
			}
			int sleepCount = 0;
			foreach (BasePoint point2 in basePoints)
			{
				if (!(point2.OwnerArea != null))
				{
					point2.SetActiveSafe(false);
					int num;
					sleepCount = (num = sleepCount) + 1;
					if (num > 100)
					{
						sleepCount = 0;
						yield return null;
					}
				}
			}
			foreach (ActionPoint point3 in actionPoints)
			{
				if (!(point3.OwnerArea != null))
				{
					point3.SetActiveSafe(false);
					int num;
					sleepCount = (num = sleepCount) + 1;
					if (num > 100)
					{
						sleepCount = 0;
						yield return null;
					}
				}
			}
			foreach (MerchantPoint point4 in merchantPoints)
			{
				if (!(point4.OwnerArea != null))
				{
					point4.SetActiveSafe(false);
					int num;
					sleepCount = (num = sleepCount) + 1;
					if (num > 100)
					{
						sleepCount = 0;
						yield return null;
					}
				}
			}
			foreach (EventPoint point5 in eventPoints)
			{
				if (!(point5.OwnerArea != null))
				{
					point5.SetActiveSafe(false);
					int num;
					sleepCount = (num = sleepCount) + 1;
					if (num > 100)
					{
						sleepCount = 0;
						yield return null;
					}
				}
			}
			foreach (StoryPoint point6 in storyPoints)
			{
				if (!(point6.OwnerArea != null))
				{
					point6.SetActiveSafe(false);
					int num;
					sleepCount = (num = sleepCount) + 1;
					if (num > 100)
					{
						sleepCount = 0;
						yield return null;
					}
				}
			}
			foreach (AnimalActionPoint point7 in animalActionPoints)
			{
				if (!(point7.OwnerArea != null))
				{
					point7.SetActiveSafe(false);
					int num;
					sleepCount = (num = sleepCount) + 1;
					if (num > 100)
					{
						sleepCount = 0;
						yield return null;
					}
				}
			}
			yield return null;
			sleepCount = 0;
			foreach (Waypoint point8 in waypoints)
			{
				if (!(point8.OwnerArea == null))
				{
					point8.RefreshExistence();
					point8.RefilterToActionPoint(actionPoints);
					point8.RefilterToActionPoint(merchantPoints);
					point8.RefilterToActionPoint(eventPoints);
					point8.RefilterToActionPoint(animalActionPoints);
					int num;
					sleepCount = (num = sleepCount) + 1;
					if (num > 100)
					{
						sleepCount = 0;
						yield return null;
					}
				}
			}
			yield break;
		}

		// Token: 0x06003DC8 RID: 15816 RVA: 0x0016A518 File Offset: 0x00168918
		public void InitActionPoints()
		{
			ActionPoint[] actionPoints = this._pointAgent.ActionPoints;
			foreach (ActionPoint actionPoint in actionPoints)
			{
				actionPoint.Init();
			}
		}

		// Token: 0x06003DC9 RID: 15817 RVA: 0x0016A554 File Offset: 0x00168954
		private void RefreshAgentStatus()
		{
			Waypoint[] waypoints = this._pointAgent.Waypoints;
			foreach (AgentActor agentActor in this._agentTable.Values)
			{
				agentActor.RefreshWalkStatus(waypoints);
			}
		}

		// Token: 0x06003DCA RID: 15818 RVA: 0x0016A5C4 File Offset: 0x001689C4
		private void RefreshAnimalStatus()
		{
			if (Singleton<AnimalManager>.IsInstance())
			{
				Singleton<AnimalManager>.Instance.RefreshStates(this);
			}
		}

		// Token: 0x06003DCB RID: 15819 RVA: 0x0016A5DC File Offset: 0x001689DC
		public void RefreshActiveTimeRelationObjects()
		{
			if (this.Simulator == null)
			{
				return;
			}
			string name = "_EmissionColor";
			string keyword = "_EMISSION";
			AIProject.TimeZone mapLightTimeZone = this.Simulator.MapLightTimeZone;
			Dictionary<int, bool> dictionary = null;
			if (Singleton<Game>.IsInstance())
			{
				AIProject.SaveData.Environment environment = Singleton<Game>.Instance.Environment;
				dictionary = ((environment != null) ? environment.TimeObjOpenState : null);
			}
			int num = (mapLightTimeZone != AIProject.TimeZone.Morning) ? ((mapLightTimeZone != AIProject.TimeZone.Day) ? ((mapLightTimeZone != AIProject.TimeZone.Night) ? -1 : 2) : 1) : 0;
			Dictionary<int, GameObject> dictionary2 = DictionaryPool<int, GameObject>.Get();
			Dictionary<int, GameObject> dictionary3 = DictionaryPool<int, GameObject>.Get();
			foreach (KeyValuePair<int, Dictionary<int, Dictionary<bool, Dictionary<int, UnityEx.ValueTuple<GameObject, Material, float, Color>[]>>>> keyValuePair in this.TimeRelationObjectTable)
			{
				if (!keyValuePair.Value.IsNullOrEmpty<int, Dictionary<bool, Dictionary<int, UnityEx.ValueTuple<GameObject, Material, float, Color>[]>>>())
				{
					int key = keyValuePair.Key;
					foreach (KeyValuePair<int, Dictionary<bool, Dictionary<int, UnityEx.ValueTuple<GameObject, Material, float, Color>[]>>> keyValuePair2 in keyValuePair.Value)
					{
						if (!keyValuePair2.Value.IsNullOrEmpty<bool, Dictionary<int, UnityEx.ValueTuple<GameObject, Material, float, Color>[]>>())
						{
							int key2 = keyValuePair2.Key;
							foreach (KeyValuePair<bool, Dictionary<int, UnityEx.ValueTuple<GameObject, Material, float, Color>[]>> keyValuePair3 in keyValuePair2.Value)
							{
								if (!keyValuePair3.Value.IsNullOrEmpty<int, UnityEx.ValueTuple<GameObject, Material, float, Color>[]>())
								{
									bool key3 = keyValuePair3.Key;
									foreach (KeyValuePair<int, UnityEx.ValueTuple<GameObject, Material, float, Color>[]> keyValuePair4 in keyValuePair3.Value)
									{
										if (!keyValuePair4.Value.IsNullOrEmpty<UnityEx.ValueTuple<GameObject, Material, float, Color>>())
										{
											int key4 = keyValuePair4.Key;
											bool flag = key == num;
											if (key3 && flag)
											{
												bool flag2;
												if (dictionary == null)
												{
													flag = false;
												}
												else if (!dictionary.TryGetValue(key4, out flag2))
												{
													flag2 = false;
												}
												else
												{
													flag = flag2;
												}
											}
											if (key2 != 0)
											{
												if (key2 == 1)
												{
													if (key == num)
													{
														foreach (UnityEx.ValueTuple<GameObject, Material, float, Color> valueTuple in keyValuePair4.Value)
														{
															if (!(valueTuple.Item1 == null) && !(valueTuple.Item2 == null))
															{
																GameObject item = valueTuple.Item1;
																Material item2 = valueTuple.Item2;
																int instanceID = valueTuple.Item1.GetInstanceID();
																if (valueTuple.Item2.HasProperty(name))
																{
																	if (flag)
																	{
																		Color item3 = valueTuple.Item4;
																		float num2 = valueTuple.Item3;
																		num2 = Mathf.Sign(num2) * Mathf.Pow(num2, 1.5f);
																		Color c = Color.white * num2 + item3;
																		if (0f <= c.r && 0f <= c.g && 0f <= c.b)
																		{
																			if (!item2.IsKeywordEnabled(keyword))
																			{
																				item2.EnableKeyword(keyword);
																			}
																			item2.SetVector(name, c);
																		}
																		else if (item2.IsKeywordEnabled(keyword))
																		{
																			item2.DisableKeyword(keyword);
																		}
																	}
																	else if (item2.IsKeywordEnabled(keyword))
																	{
																		item2.DisableKeyword(keyword);
																	}
																}
															}
														}
													}
												}
											}
											else
											{
												foreach (UnityEx.ValueTuple<GameObject, Material, float, Color> valueTuple2 in keyValuePair4.Value)
												{
													if (!(valueTuple2.Item1 == null))
													{
														int instanceID2 = valueTuple2.Item1.GetInstanceID();
														if (flag)
														{
															if (dictionary3.ContainsKey(instanceID2))
															{
																dictionary3.Remove(instanceID2);
															}
															if (!dictionary2.ContainsKey(instanceID2))
															{
																dictionary2[instanceID2] = valueTuple2.Item1;
															}
														}
														else if (!dictionary2.ContainsKey(instanceID2) && !dictionary3.ContainsKey(instanceID2))
														{
															dictionary3[instanceID2] = valueTuple2.Item1;
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			foreach (KeyValuePair<int, GameObject> keyValuePair5 in dictionary2)
			{
				if (keyValuePair5.Value != null && !keyValuePair5.Value.activeSelf)
				{
					keyValuePair5.Value.SetActive(true);
				}
			}
			foreach (KeyValuePair<int, GameObject> keyValuePair6 in dictionary3)
			{
				if (keyValuePair6.Value != null && keyValuePair6.Value.activeSelf)
				{
					keyValuePair6.Value.SetActive(false);
				}
			}
			DictionaryPool<int, GameObject>.Release(dictionary2);
			DictionaryPool<int, GameObject>.Release(dictionary3);
			if (!this.TimeLinkedLightObjectList.IsNullOrEmpty<TimeLinkedLightObject>())
			{
				List<TimeLinkedLightObject> timeLinkedLightObjectList = this.TimeLinkedLightObjectList;
				timeLinkedLightObjectList.RemoveAll((TimeLinkedLightObject x) => x == null);
				foreach (TimeLinkedLightObject timeLinkedLightObject in timeLinkedLightObjectList)
				{
					timeLinkedLightObject.Refresh(mapLightTimeZone);
				}
			}
		}

		// Token: 0x06003DCC RID: 15820 RVA: 0x0016AC64 File Offset: 0x00169064
		public void SetActiveMapEffect(bool active)
		{
			if (this._mapDataEffect == null || this._mapDataEffect.activeSelf == active)
			{
				return;
			}
			this._mapDataEffect.SetActive(active);
		}

		// Token: 0x06003DCD RID: 15821 RVA: 0x0016AC98 File Offset: 0x00169098
		public static PlayerActor GetPlayer()
		{
			if (!Singleton<Map>.IsInstance())
			{
				return null;
			}
			Map instance = Singleton<Map>.Instance;
			PlayerActor player = instance.Player;
			return (!(player != null)) ? null : player;
		}

		// Token: 0x06003DCE RID: 15822 RVA: 0x0016ACD4 File Offset: 0x001690D4
		public static MerchantActor GetMerchant()
		{
			if (!Singleton<Map>.IsInstance())
			{
				return null;
			}
			Map instance = Singleton<Map>.Instance;
			MerchantActor merchant = instance.Merchant;
			return (!(merchant != null)) ? null : merchant;
		}

		// Token: 0x06003DCF RID: 15823 RVA: 0x0016AD10 File Offset: 0x00169110
		public static Camera GetCameraComponent()
		{
			if (!Singleton<Map>.IsInstance())
			{
				return null;
			}
			Map instance = Singleton<Map>.Instance;
			PlayerActor player = instance.Player;
			if (player == null)
			{
				return null;
			}
			ActorCameraControl cameraControl = player.CameraControl;
			if (cameraControl == null)
			{
				return null;
			}
			Camera cameraComponent = cameraControl.CameraComponent;
			return (!(cameraComponent != null)) ? null : cameraComponent;
		}

		// Token: 0x06003DD0 RID: 15824 RVA: 0x0016AD74 File Offset: 0x00169174
		public static Camera GetCameraComponent(PlayerActor player)
		{
			if (player == null)
			{
				return null;
			}
			ActorCameraControl cameraControl = player.CameraControl;
			if (cameraControl == null)
			{
				return null;
			}
			Camera cameraComponent = cameraControl.CameraComponent;
			return (!(cameraComponent != null)) ? null : cameraComponent;
		}

		// Token: 0x06003DD1 RID: 15825 RVA: 0x0016ADC0 File Offset: 0x001691C0
		public static CommandArea GetCommandArea()
		{
			if (!Singleton<Map>.IsInstance())
			{
				return null;
			}
			Map instance = Singleton<Map>.Instance;
			PlayerActor player = instance.Player;
			if (player == null)
			{
				return null;
			}
			PlayerController playerController = player.PlayerController;
			if (playerController == null)
			{
				return null;
			}
			CommandArea commandArea = playerController.CommandArea;
			return (!(commandArea != null)) ? null : commandArea;
		}

		// Token: 0x06003DD2 RID: 15826 RVA: 0x0016AE24 File Offset: 0x00169224
		public static CommandArea GetCommandArea(PlayerActor player)
		{
			if (player == null)
			{
				return null;
			}
			PlayerController playerController = player.PlayerController;
			if (playerController == null)
			{
				return null;
			}
			CommandArea commandArea = playerController.CommandArea;
			return (!(commandArea != null)) ? null : commandArea;
		}

		// Token: 0x06003DD3 RID: 15827 RVA: 0x0016AE70 File Offset: 0x00169270
		public static ActorCameraControl GetCameraControl()
		{
			if (!Singleton<Map>.IsInstance())
			{
				return null;
			}
			Map instance = Singleton<Map>.Instance;
			PlayerActor player = instance.Player;
			if (player == null)
			{
				return null;
			}
			ActorCameraControl cameraControl = player.CameraControl;
			return (!(cameraControl != null)) ? null : cameraControl;
		}

		// Token: 0x06003DD4 RID: 15828 RVA: 0x0016AEC0 File Offset: 0x001692C0
		public static ActorCameraControl GetCameraControl(PlayerActor player)
		{
			if (player == null)
			{
				return null;
			}
			ActorCameraControl cameraControl = player.CameraControl;
			return (!(cameraControl != null)) ? null : cameraControl;
		}

		// Token: 0x06003DD5 RID: 15829 RVA: 0x0016AEF8 File Offset: 0x001692F8
		public static bool FadeStart(float time = -1f)
		{
			if (!Singleton<Map>.IsInstance())
			{
				return false;
			}
			Map instance = Singleton<Map>.Instance;
			PlayerActor player = instance.Player;
			if (player == null)
			{
				return false;
			}
			ActorCameraControl cameraControl = player.CameraControl;
			if (cameraControl == null)
			{
				return false;
			}
			CrossFade crossFade = cameraControl.CrossFade;
			if (crossFade == null)
			{
				return false;
			}
			crossFade.FadeStart(time);
			return true;
		}

		// Token: 0x06003DD6 RID: 15830 RVA: 0x0016AF5E File Offset: 0x0016935E
		public void BuildNavMesh()
		{
			this.NavMeshSurface.BuildNavMesh();
		}

		// Token: 0x06003DD7 RID: 15831 RVA: 0x0016AF6C File Offset: 0x0016936C
		public IEnumerator RebuildNavMeshAsync()
		{
			if (this.NavMeshSurface == null)
			{
				yield break;
			}
			AsyncOperation operation = this.NavMeshSurface.UpdateNavMesh(this.NavMeshSurface.navMeshData);
			yield return operation;
			yield break;
		}

		// Token: 0x06003DD8 RID: 15832 RVA: 0x0016AF88 File Offset: 0x00169388
		public void SetVisibleAll(bool active)
		{
			foreach (KeyValuePair<int, AgentActor> keyValuePair in this.AgentTable)
			{
				keyValuePair.Value.IsVisible = active;
			}
		}

		// Token: 0x06003DD9 RID: 15833 RVA: 0x0016AFE8 File Offset: 0x001693E8
		public void SetVisibleAll(bool active, AgentActor exAgent)
		{
			foreach (KeyValuePair<int, AgentActor> keyValuePair in this.AgentTable)
			{
				if (!(keyValuePair.Value == exAgent))
				{
					keyValuePair.Value.IsVisible = active;
				}
			}
		}

		// Token: 0x06003DDA RID: 15834 RVA: 0x0016B060 File Offset: 0x00169460
		public void EnableEntity()
		{
			PlayerActor player = this.Player;
			player.CameraControl.enabled = true;
			player.ChaControl.visibleAll = true;
			player.ActivateNavMeshAgent();
			foreach (AgentActor agentActor in this.AgentTable.Values)
			{
				UnityEx.ValueTuple<Desire.ActionType, Desire.ActionType, bool> valueTuple = this.AgentModeCache[agentActor.ID];
				if (valueTuple.Item3)
				{
					agentActor.ActivateNavMeshAgent();
					agentActor.NavMeshAgent.Warp(agentActor.Position);
				}
				else if (agentActor.EventKey == AIProject.EventType.Move)
				{
					agentActor.ActivateNavMeshAgent();
					agentActor.SetDefaultStateHousingItem();
					if (agentActor.CurrentPoint != null)
					{
						OffMeshLink component = agentActor.CurrentPoint.GetComponent<OffMeshLink>();
						if (component != null)
						{
							Transform endTransform = component.endTransform;
							agentActor.NavMeshAgent.Warp(endTransform.position);
							agentActor.Rotation = endTransform.rotation;
						}
						agentActor.CurrentPoint.RemoveBookingUser(agentActor);
						agentActor.CurrentPoint.SetActiveMapItemObjs(true);
						agentActor.CurrentPoint.ReleaseSlot(agentActor);
						agentActor.CurrentPoint = null;
					}
					agentActor.EventKey = (AIProject.EventType)0;
					agentActor.TargetInSightActionPoint = null;
					agentActor.Animation.ResetDefaultAnimatorController();
					valueTuple.Item1 = Desire.ActionType.Normal;
					valueTuple.Item2 = Desire.ActionType.Normal;
				}
				else if (agentActor.EventKey == AIProject.EventType.DoorOpen)
				{
					agentActor.ActivateNavMeshAgent();
					agentActor.SetDefaultStateHousingItem();
					if (agentActor.CurrentPoint != null)
					{
						DoorPoint doorPoint = agentActor.CurrentPoint as DoorPoint;
						if (doorPoint != null)
						{
							if (doorPoint.OpenState == DoorPoint.OpenPattern.Close)
							{
								if (doorPoint.OpenType == DoorPoint.OpenTypeState.Right || doorPoint.OpenType == DoorPoint.OpenTypeState.Right90)
								{
									doorPoint.SetOpenState(DoorPoint.OpenPattern.OpenRight, true);
								}
								else
								{
									doorPoint.SetOpenState(DoorPoint.OpenPattern.OpenLeft, true);
								}
							}
							doorPoint.RemoveBookingUser(agentActor);
						}
						agentActor.CurrentPoint.SetActiveMapItemObjs(true);
						agentActor.CurrentPoint.ReleaseSlot(agentActor);
						agentActor.CurrentPoint = null;
					}
					agentActor.EventKey = (AIProject.EventType)0;
					agentActor.TargetInSightActionPoint = null;
					agentActor.Animation.ResetDefaultAnimatorController();
					valueTuple.Item1 = Desire.ActionType.Normal;
					valueTuple.Item2 = Desire.ActionType.Normal;
				}
				else if (agentActor.EventKey == AIProject.EventType.Toilet || agentActor.EventKey == AIProject.EventType.DressIn || agentActor.EventKey == AIProject.EventType.Bath || agentActor.EventKey == AIProject.EventType.DressOut || agentActor.AgentData.PlayedDressIn || valueTuple.Item1 == Desire.ActionType.FoundPeeping)
				{
					agentActor.ActivateNavMeshAgent();
					agentActor.SetDefaultStateHousingItem();
					if (agentActor.CurrentPoint != null)
					{
						agentActor.CurrentPoint.SetActiveMapItemObjs(true);
						agentActor.CurrentPoint.ReleaseSlot(agentActor);
						agentActor.CurrentPoint = null;
					}
					agentActor.ChaControl.ChangeNowCoordinate(true, true);
					agentActor.AgentData.BathCoordinateFileName = null;
					agentActor.ChaControl.SetClothesState(0, 0, true);
					agentActor.ChaControl.SetClothesState(1, 0, true);
					agentActor.ChaControl.SetClothesState(2, 0, true);
					agentActor.ChaControl.SetClothesState(3, 0, true);
					agentActor.AgentData.PlayedDressIn = false;
					agentActor.EventKey = (AIProject.EventType)0;
					agentActor.TargetInSightActionPoint = null;
					agentActor.Animation.ResetDefaultAnimatorController();
					valueTuple.Item1 = Desire.ActionType.Normal;
					valueTuple.Item2 = Desire.ActionType.Normal;
				}
				else if (valueTuple.Item1 == Desire.ActionType.Encounter)
				{
					agentActor.ActivateNavMeshAgent();
					agentActor.EventKey = (AIProject.EventType)0;
					agentActor.TargetInSightActionPoint = null;
					agentActor.Animation.ResetDefaultAnimatorController();
					valueTuple.Item1 = Desire.ActionType.Normal;
					valueTuple.Item2 = Desire.ActionType.Normal;
				}
				agentActor.Controller.enabled = true;
				agentActor.ChaControl.visibleAll = true;
				agentActor.AnimationAgent.enabled = true;
				agentActor.SetActiveOnEquipedItem(false);
				agentActor.EnableBehavior();
				if (valueTuple.Item1 == Desire.ActionType.Normal && valueTuple.Item2 == Desire.ActionType.Normal)
				{
					agentActor.ResetActionFlag();
					if (agentActor.EnabledSchedule)
					{
						agentActor.EnabledSchedule = false;
					}
				}
				agentActor.Mode = valueTuple.Item1;
				agentActor.BehaviorResources.ChangeMode(valueTuple.Item2);
				agentActor.AnimationAgent.EnableItems();
				agentActor.AnimationAgent.EnableParticleRenderer();
			}
			MerchantActor merchant = Singleton<Map>.Instance.Merchant;
			merchant.EnableEntity();
			AnimalBase.CreateDisplay = true;
			AnimalManager instance = Singleton<AnimalManager>.Instance;
			for (int i = 0; i < instance.Animals.Count; i++)
			{
				AnimalBase animalBase = instance.Animals[i];
				if (!(animalBase == null))
				{
					animalBase.BodyEnabled = true;
					animalBase.enabled = true;
				}
			}
			instance.SettingAnimalPointBehavior();
		}

		// Token: 0x06003DDB RID: 15835 RVA: 0x0016B534 File Offset: 0x00169934
		public void DisableEntity()
		{
			PlayerActor player = this.Player;
			player.CameraControl.enabled = false;
			player.ChaControl.visibleAll = false;
			foreach (AgentActor agentActor in this.AgentTable.Values)
			{
				Desire.ActionType mode = agentActor.BehaviorResources.Mode;
				bool enabled = agentActor.NavMeshAgent.enabled;
				this.AgentModeCache[agentActor.ID] = new UnityEx.ValueTuple<Desire.ActionType, Desire.ActionType, bool>(agentActor.Mode, mode, enabled);
				agentActor.SetActiveOnEquipedItem(false);
				agentActor.Controller.enabled = false;
				if (enabled)
				{
					agentActor.NavMeshAgent.enabled = false;
				}
				agentActor.AnimationAgent.enabled = false;
				agentActor.ChaControl.visibleAll = false;
				if (mode == Desire.ActionType.EndTaskMasturbation || mode == Desire.ActionType.EndTaskLesbianH || mode == Desire.ActionType.EndTaskLesbianMerchantH)
				{
					agentActor.BehaviorResources.ChangeMode(Desire.ActionType.Idle);
				}
				agentActor.DisableBehavior();
				agentActor.AnimationAgent.DisableItems();
				agentActor.AnimationAgent.DisableParticleRenderer();
			}
			MerchantActor merchant = Singleton<Map>.Instance.Merchant;
			merchant.DisableEntity();
			AnimalBase.CreateDisplay = false;
			AnimalManager instance = Singleton<AnimalManager>.Instance;
			for (int i = 0; i < instance.Animals.Count; i++)
			{
				AnimalBase animalBase = instance.Animals[i];
				if (!(animalBase == null))
				{
					animalBase.enabled = false;
					animalBase.BodyEnabled = false;
				}
			}
			instance.ClearAnimalPointBehavior();
		}

		// Token: 0x06003DDC RID: 15836 RVA: 0x0016B6DC File Offset: 0x00169ADC
		public void EnableEntity(Actor exActor)
		{
			ReadOnlyDictionary<int, AgentActor> agentTable = Singleton<Map>.Instance.AgentTable;
			if (agentTable.IsNullOrEmpty<int, AgentActor>())
			{
				return;
			}
			foreach (KeyValuePair<int, AgentActor> keyValuePair in agentTable)
			{
				AgentActor value = keyValuePair.Value;
				if (!(value == null))
				{
					if (!(value == exActor))
					{
						value.EnableEntity();
					}
				}
			}
			if (this.Merchant != null && this.Merchant != exActor)
			{
				this.Merchant.EnableEntity();
			}
		}

		// Token: 0x06003DDD RID: 15837 RVA: 0x0016B7A0 File Offset: 0x00169BA0
		public void DisableEntity(Actor exActor)
		{
			ReadOnlyDictionary<int, AgentActor> agentTable = Singleton<Map>.Instance.AgentTable;
			if (agentTable.IsNullOrEmpty<int, AgentActor>())
			{
				return;
			}
			foreach (KeyValuePair<int, AgentActor> keyValuePair in agentTable)
			{
				AgentActor value = keyValuePair.Value;
				if (!(value == null))
				{
					if (!(value == exActor))
					{
						value.DisableEntity();
					}
				}
			}
			if (this.Merchant != null && this.Merchant != exActor)
			{
				this.Merchant.DisableEntity();
			}
		}

		// Token: 0x06003DDE RID: 15838 RVA: 0x0016B864 File Offset: 0x00169C64
		public void ResetAgentsInHousingArea()
		{
			List<AgentActor> list = ListPool<AgentActor>.Get();
			foreach (KeyValuePair<int, AgentActor> keyValuePair in this._agentTable)
			{
				if (keyValuePair.Value.AreaID == this.HousingAreaID)
				{
					list.Add(keyValuePair.Value);
					Actor commandPartner = keyValuePair.Value.CommandPartner;
					if (commandPartner != null && commandPartner.GetType() == typeof(AgentActor) && !list.Contains(commandPartner))
					{
						list.Add(commandPartner as AgentActor);
					}
				}
			}
			List<Transform> source;
			if (this.HousingRecoveryPointTable.TryGetValue(this.HousingID, out source))
			{
				int num = 0;
				foreach (AgentActor agentActor in list)
				{
					Transform element = source.GetElement(num++);
					if (!(element == null))
					{
						agentActor.ActivateNavMeshAgent();
						agentActor.ClearItems();
						agentActor.ClearParticles();
						agentActor.SetActiveOnEquipedItem(false);
						agentActor.AgentData.CarryingItem = null;
						agentActor.ChaControl.ChangeNowCoordinate(true, true);
						agentActor.AgentData.BathCoordinateFileName = null;
						agentActor.ChaControl.SetClothesState(0, 0, true);
						agentActor.ChaControl.SetClothesState(1, 0, true);
						agentActor.ChaControl.SetClothesState(2, 0, true);
						agentActor.ChaControl.SetClothesState(3, 0, true);
						agentActor.ChaControl.SetClothesState(5, 0, true);
						agentActor.AgentData.PlayedDressIn = false;
						agentActor.SetDefaultStateHousingItem();
						if (agentActor.CurrentPoint != null)
						{
							agentActor.CurrentPoint.SetActiveMapItemObjs(true);
							agentActor.CurrentPoint.ReleaseSlot(agentActor);
							agentActor.CurrentPoint = null;
						}
						agentActor.EventKey = (AIProject.EventType)0;
						agentActor.TargetInSightActionPoint = null;
						agentActor.CommandPartner = null;
						agentActor.TargetInSightActor = null;
						agentActor.Animation.ResetDefaultAnimatorController();
						agentActor.ChangeBehavior(Desire.ActionType.Normal);
						agentActor.NavMeshAgent.Warp(element.position);
						agentActor.Rotation = Quaternion.Euler(0f, element.rotation.eulerAngles.y, 0f);
					}
				}
			}
			ListPool<AgentActor>.Release(list);
		}

		// Token: 0x06003DDF RID: 15839 RVA: 0x0016BB1C File Offset: 0x00169F1C
		public void InitCommandable()
		{
			ICommandable[] componentsInChildren = this._pointAgent.GetComponentsInChildren<ICommandable>(true);
			this.Player.PlayerController.CommandArea.SetCommandableObjects(componentsInChildren);
			foreach (int key in this._agentKeys)
			{
				AgentActor command;
				if (this.AgentTable.TryGetValue(key, out command))
				{
					this.AddCommandable(command);
				}
			}
			this.AddCommandable(this.Merchant);
			AnimalManager instance = Singleton<AnimalManager>.Instance;
			foreach (AnimalBase animalBase in instance.Animals)
			{
				if (animalBase != null && animalBase.IsCommandable)
				{
					this.AddCommandable(animalBase);
				}
			}
		}

		// Token: 0x06003DE0 RID: 15840 RVA: 0x0016BC30 File Offset: 0x0016A030
		public void AddCommandable(ICommandable command)
		{
			this.Player.PlayerController.CommandArea.AddCommandableObject(command);
		}

		// Token: 0x06003DE1 RID: 15841 RVA: 0x0016BC48 File Offset: 0x0016A048
		private int UnusedRegID()
		{
			int num = 10000;
			while (Singleton<Game>.Instance.Environment.RegIDList.Contains(num))
			{
				num++;
			}
			return num;
		}

		// Token: 0x06003DE2 RID: 15842 RVA: 0x0016BC84 File Offset: 0x0016A084
		public int RegisterRuntimePoint(Point pt)
		{
			if (pt == null)
			{
				return -1;
			}
			int num;
			if (pt.RegisterID == -1)
			{
				num = this.UnusedRegID();
				pt.RegisterID = num;
			}
			else
			{
				num = pt.RegisterID;
			}
			AIProject.SaveData.Environment environment = Singleton<Game>.Instance.Environment;
			environment.RegIDList.Add(num);
			if (pt is SearchActionPoint)
			{
				AIProject.SaveData.Environment.SearchActionInfo searchActionInfo;
				if (!environment.SearchActionLockTable.TryGetValue(num, out searchActionInfo))
				{
					environment.SearchActionLockTable[num] = new AIProject.SaveData.Environment.SearchActionInfo();
				}
			}
			else if (pt is FarmPoint)
			{
				FarmPoint farmPoint = pt as FarmPoint;
				List<AIProject.SaveData.Environment.PlantInfo> list;
				if (!environment.FarmlandTable.TryGetValue(num, out list))
				{
					List<AIProject.SaveData.Environment.PlantInfo> list2 = new List<AIProject.SaveData.Environment.PlantInfo>();
					environment.FarmlandTable[num] = list2;
					list = list2;
					if (farmPoint.Kind == FarmPoint.FarmKind.Plant && list.Count != farmPoint.HarvestSections.Length)
					{
						list.Clear();
						for (int i = 0; i < farmPoint.HarvestSections.Length; i++)
						{
							list.Add(null);
						}
					}
				}
				foreach (FarmSection farmSection in farmPoint.HarvestSections)
				{
					farmSection.HarvestID = num;
				}
			}
			return num;
		}

		// Token: 0x06003DE3 RID: 15843 RVA: 0x0016BDCC File Offset: 0x0016A1CC
		public void UnregisterRuntimePoint(Point pt, bool removeEntry = true)
		{
			if (pt == null)
			{
				return;
			}
			int registerID = pt.RegisterID;
			AIProject.SaveData.Environment environment = Singleton<Game>.Instance.Environment;
			if (removeEntry)
			{
				if (environment.RegIDList.Remove(registerID))
				{
					environment.SearchActionLockTable.Remove(registerID);
					environment.FarmlandTable.Remove(registerID);
				}
			}
			else if (!this._removeRegIDCache.Contains(registerID))
			{
				this._removeRegIDCache.Add(registerID);
			}
		}

		// Token: 0x06003DE4 RID: 15844 RVA: 0x0016BE4B File Offset: 0x0016A24B
		public void RemoveRegIDCache(int regID)
		{
			if (this._removeRegIDCache.Contains(regID))
			{
				this._removeRegIDCache.Remove(regID);
			}
		}

		// Token: 0x06003DE5 RID: 15845 RVA: 0x0016BE6C File Offset: 0x0016A26C
		public void SeqRemoveCacheRegID()
		{
			AIProject.SaveData.Environment environment = Singleton<Game>.Instance.Environment;
			foreach (int num in this._removeRegIDCache)
			{
				if (environment.RegIDList.Remove(num))
				{
					environment.SearchActionLockTable.Remove(num);
					environment.FarmlandTable.Remove(num);
				}
			}
			this._removeRegIDCache.Clear();
		}

		// Token: 0x06003DE6 RID: 15846 RVA: 0x0016BF04 File Offset: 0x0016A304
		public void AddAppendTargetPoints(ActionPoint[] actionPoints)
		{
			this._pointAgent.AppendActionPoints.Clear();
			this._pointAgent.AppendActionPoints.AddRange(actionPoints);
			int[] array = this.ChunkTable.Keys.ToArray<int>();
			foreach (int key in array)
			{
				Chunk chunk;
				if (this.ChunkTable.TryGetValue(key, out chunk))
				{
					chunk.LoadAppendActionPoints(actionPoints);
				}
			}
			Singleton<Map>.Instance.WarpPointDic.Clear();
			foreach (ActionPoint actionPoint in actionPoints)
			{
				actionPoint.Init();
				this.AddAppendCommandable(actionPoint);
				this.CheckWarpPoint(actionPoint);
			}
			foreach (int key2 in this._agentKeys)
			{
				AgentActor agentActor;
				if (this.AgentTable.TryGetValue(key2, out agentActor))
				{
					agentActor.AgentController.SearchArea.AddPoints(actionPoints);
				}
			}
		}

		// Token: 0x06003DE7 RID: 15847 RVA: 0x0016C03C File Offset: 0x0016A43C
		private void CheckWarpPoint(ActionPoint pt)
		{
			WarpPoint warpPoint = pt as WarpPoint;
			if (warpPoint == null)
			{
				return;
			}
			MapArea ownerArea = warpPoint.OwnerArea;
			if (ownerArea == null)
			{
				return;
			}
			int chunkID = ownerArea.ChunkID;
			Dictionary<int, List<WarpPoint>> dictionary;
			if (!Singleton<Map>.Instance.WarpPointDic.TryGetValue(chunkID, out dictionary))
			{
				Dictionary<int, List<WarpPoint>> dictionary2 = new Dictionary<int, List<WarpPoint>>();
				Singleton<Map>.Instance.WarpPointDic[chunkID] = dictionary2;
				dictionary = dictionary2;
			}
			List<WarpPoint> list;
			if (!dictionary.TryGetValue(warpPoint.TableID, out list))
			{
				List<WarpPoint> list2 = new List<WarpPoint>();
				dictionary[warpPoint.TableID] = list2;
				list = list2;
			}
			if (!list.Contains(warpPoint))
			{
				list.Add(warpPoint);
			}
		}

		// Token: 0x06003DE8 RID: 15848 RVA: 0x0016C0EC File Offset: 0x0016A4EC
		public void RemoveAppendActionPoint(ActionPoint point)
		{
			this._pointAgent.AppendActionPoints.Remove(point);
			int[] array = this.ChunkTable.Keys.ToArray<int>();
			foreach (int key in array)
			{
				Chunk chunk;
				if (this.ChunkTable.TryGetValue(key, out chunk))
				{
					foreach (MapArea mapArea in chunk.MapAreas)
					{
						mapArea.AppendActionPoints.Remove(point);
					}
					chunk.AppendActionPoints.Remove(point);
				}
			}
		}

		// Token: 0x06003DE9 RID: 15849 RVA: 0x0016C194 File Offset: 0x0016A594
		public void RemoveAgentSearchActionPoint(ActionPoint actionPoint)
		{
			foreach (int key in this._agentKeys)
			{
				AgentActor agentActor;
				if (this.AgentTable.TryGetValue(key, out agentActor))
				{
					agentActor.AgentController.SearchArea.RemovePoint(actionPoint);
					agentActor.RemoveActionPoint(actionPoint);
				}
			}
		}

		// Token: 0x06003DEA RID: 15850 RVA: 0x0016C21C File Offset: 0x0016A61C
		public void AddRuntimeFarmPoints(FarmPoint[] farmPoints)
		{
			int[] array = this.ChunkTable.Keys.ToArray<int>();
			foreach (int key in array)
			{
				Chunk chunk;
				if (this.ChunkTable.TryGetValue(key, out chunk))
				{
					chunk.LoadAppendFarmPoints(farmPoints);
				}
			}
			if (this._pointAgent != null)
			{
				this._pointAgent.AddRuntimeFarmPoints(farmPoints);
			}
			foreach (FarmPoint farmPoint in farmPoints)
			{
				this.AddAppendCommandable(farmPoint);
				farmPoint.SetChickenWayPoint();
				farmPoint.CreateChicken();
			}
		}

		// Token: 0x06003DEB RID: 15851 RVA: 0x0016C2CC File Offset: 0x0016A6CC
		public void RemoveRuntimeFarmPoint(FarmPoint point)
		{
			if (point != null)
			{
				point.DestroyChicken();
			}
			int[] array = this.ChunkTable.Keys.ToArray<int>();
			foreach (int key in array)
			{
				Chunk chunk;
				if (this.ChunkTable.TryGetValue(key, out chunk))
				{
					foreach (MapArea mapArea in chunk.MapAreas)
					{
						mapArea.AppendFarmPoints.Remove(point);
					}
					chunk.AppendFarmPoints.Remove(point);
				}
			}
			if (this._pointAgent != null)
			{
				this._pointAgent.RemoveRuntimeFarmPoint(point);
			}
		}

		// Token: 0x06003DEC RID: 15852 RVA: 0x0016C390 File Offset: 0x0016A790
		public void AddPetHomePoints(PetHomePoint[] petHomePoints)
		{
			int[] array = this.ChunkTable.Keys.ToArray<int>();
			foreach (int key in array)
			{
				Chunk chunk;
				if (this.ChunkTable.TryGetValue(key, out chunk) && !(chunk == null))
				{
					chunk.LoadAppendPetHomePoints(petHomePoints);
				}
			}
			foreach (PetHomePoint command in petHomePoints)
			{
				this.AddAppendCommandable(command);
			}
			if (this.PointAgent != null)
			{
				this.PointAgent.AddPetHomePoints(petHomePoints);
			}
			Dictionary<int, AIProject.SaveData.Environment.PetHomeInfo> dictionary;
			if (Singleton<Game>.IsInstance())
			{
				AIProject.SaveData.Environment environment = Singleton<Game>.Instance.Environment;
				dictionary = ((environment != null) ? environment.PetHomeStateTable : null);
			}
			else
			{
				dictionary = null;
			}
			Dictionary<int, AIProject.SaveData.Environment.PetHomeInfo> dictionary2 = dictionary;
			if (dictionary2 != null)
			{
				foreach (PetHomePoint petHomePoint in petHomePoints)
				{
					if (!(petHomePoint == null))
					{
						int registerID = petHomePoint.RegisterID;
						AIProject.SaveData.Environment.PetHomeInfo petHomeInfo;
						if (!dictionary2.TryGetValue(registerID, out petHomeInfo) || petHomeInfo == null)
						{
							PetHomePoint petHomePoint2 = petHomePoint;
							AIProject.SaveData.Environment.PetHomeInfo petHomeInfo2 = new AIProject.SaveData.Environment.PetHomeInfo();
							dictionary2[registerID] = petHomeInfo2;
							petHomeInfo = (petHomePoint2.SaveData = petHomeInfo2);
							petHomeInfo.HousingID = this.HousingID;
							petHomeInfo.AnimalData = null;
							petHomeInfo.ChaseActor = false;
						}
						else
						{
							petHomePoint.SaveData = petHomeInfo;
						}
					}
				}
			}
		}

		// Token: 0x06003DED RID: 15853 RVA: 0x0016C510 File Offset: 0x0016A910
		public void RemovePetHomePoint(PetHomePoint petHomePoint)
		{
			if (petHomePoint == null)
			{
				return;
			}
			int[] array = this.ChunkTable.Keys.ToArray<int>();
			foreach (int key in array)
			{
				Chunk chunk;
				if (this.ChunkTable.TryGetValue(key, out chunk) && !(chunk == null))
				{
					if (!chunk.MapAreas.IsNullOrEmpty<MapArea>())
					{
						foreach (MapArea mapArea in chunk.MapAreas)
						{
							mapArea.AppendPetHomePoints.Remove(petHomePoint);
						}
					}
					chunk.AppendPetHomePoints.Remove(petHomePoint);
				}
			}
			if (this.PointAgent != null)
			{
				this.PointAgent.RemovePetHomePoint(petHomePoint);
			}
			Dictionary<int, AIProject.SaveData.Environment.PetHomeInfo> dictionary;
			if (Singleton<Game>.IsInstance())
			{
				AIProject.SaveData.Environment environment = Singleton<Game>.Instance.Environment;
				dictionary = ((environment != null) ? environment.PetHomeStateTable : null);
			}
			else
			{
				dictionary = null;
			}
			Dictionary<int, AIProject.SaveData.Environment.PetHomeInfo> dictionary2 = dictionary;
			if (!dictionary2.IsNullOrEmpty<int, AIProject.SaveData.Environment.PetHomeInfo>())
			{
				dictionary2.Remove(petHomePoint.RegisterID);
			}
		}

		// Token: 0x06003DEE RID: 15854 RVA: 0x0016C634 File Offset: 0x0016AA34
		public void AddJukePoints(JukePoint[] jukePoints)
		{
			int[] array = this.ChunkTable.Keys.ToArray<int>();
			foreach (int key in array)
			{
				Chunk chunk;
				if (this.ChunkTable.TryGetValue(key, out chunk) && !(chunk == null))
				{
					chunk.LoadAppendJukePoints(jukePoints);
				}
			}
			foreach (JukePoint jukePoint in jukePoints)
			{
				if (!(jukePoint == null))
				{
					jukePoint.SetAreaID();
					this.AddAppendCommandable(jukePoint);
				}
			}
			if (this._pointAgent != null)
			{
				this._pointAgent.AddJukePoints(jukePoints);
			}
		}

		// Token: 0x06003DEF RID: 15855 RVA: 0x0016C6FC File Offset: 0x0016AAFC
		public void RemoveJukePoint(JukePoint point)
		{
			int[] array = this.ChunkTable.Keys.ToArray<int>();
			if (!array.IsNullOrEmpty<int>())
			{
				foreach (int key in array)
				{
					Chunk chunk;
					if (this.ChunkTable.TryGetValue(key, out chunk) && !(chunk == null))
					{
						foreach (MapArea mapArea in chunk.MapAreas)
						{
							mapArea.AppendJukePoints.Remove(point);
						}
						chunk.AppendJukePoints.Remove(point);
					}
				}
			}
			if (this._pointAgent != null)
			{
				this._pointAgent.RemoveJukePoint(point);
			}
			Dictionary<int, string> dictionary = null;
			if (Singleton<Game>.IsInstance())
			{
				if (this.MapID == 0)
				{
					AIProject.SaveData.Environment environment = Singleton<Game>.Instance.Environment;
					dictionary = ((environment != null) ? environment.JukeBoxAudioNameTable : null);
				}
				else
				{
					AIProject.SaveData.Environment environment2 = Singleton<Game>.Instance.Environment;
					Dictionary<int, Dictionary<int, string>> dictionary2 = (environment2 != null) ? environment2.AnotherJukeBoxAudioNameTable : null;
					if (dictionary2 != null && (!dictionary2.TryGetValue(this.MapID, out dictionary) || dictionary == null))
					{
						Dictionary<int, string> dictionary3 = new Dictionary<int, string>();
						dictionary2[this.MapID] = dictionary3;
						dictionary = dictionary3;
					}
				}
			}
			if (!dictionary.IsNullOrEmpty<int, string>() && point != null)
			{
				int areaID = point.AreaID;
				bool flag = true;
				if (this._pointAgent != null)
				{
					IReadOnlyDictionary<int, List<JukePoint>> jukePointTable = this._pointAgent.JukePointTable;
					if (jukePointTable != null && 0 < jukePointTable.Count)
					{
						List<JukePoint> list = null;
						if (jukePointTable.TryGetValue(areaID, out list) && !list.IsNullOrEmpty<JukePoint>())
						{
							list.RemoveAll((JukePoint x) => x == null);
							flag = (list.Count == 0 || !list.Exists((JukePoint x) => x != point));
						}
					}
				}
				if (flag && dictionary.ContainsKey(areaID))
				{
					dictionary.Remove(areaID);
				}
			}
		}

		// Token: 0x06003DF0 RID: 15856 RVA: 0x0016C960 File Offset: 0x0016AD60
		public void AddRuntimeCraftPoints(CraftPoint[] craftPoints)
		{
			List<int> list = this.ChunkTable.Keys.ToList<int>();
			if (!list.IsNullOrEmpty<int>())
			{
				foreach (int key in list)
				{
					Chunk chunk;
					if (this.ChunkTable.TryGetValue(key, out chunk) && !(chunk == null))
					{
						chunk.LoadAppendCraftPoints(craftPoints);
					}
				}
			}
			foreach (CraftPoint craftPoint in craftPoints)
			{
				this.AddAppendCommandable(craftPoint);
				if (craftPoint != null && craftPoint.Kind == CraftPoint.CraftKind.Recycling)
				{
					AIProject.SaveData.Environment environment = (!Singleton<Game>.IsInstance()) ? null : Singleton<Game>.Instance.Environment;
					Dictionary<int, RecyclingData> dictionary = (environment == null) ? null : environment.RecyclingDataTable;
					if (dictionary != null)
					{
						RecyclingData recyclingData = null;
						if (!dictionary.TryGetValue(craftPoint.RegisterID, out recyclingData) || recyclingData == null)
						{
							dictionary[craftPoint.RegisterID] = new RecyclingData();
						}
					}
				}
			}
		}

		// Token: 0x06003DF1 RID: 15857 RVA: 0x0016CAA8 File Offset: 0x0016AEA8
		public void RemoveCraftPoint(CraftPoint point)
		{
			List<int> list = this.ChunkTable.Keys.ToList<int>();
			foreach (int key in list)
			{
				Chunk chunk;
				if (this.ChunkTable.TryGetValue(key, out chunk) && !(chunk == null))
				{
					foreach (MapArea mapArea in chunk.MapAreas)
					{
						mapArea.AppendCraftPoints.Remove(point);
					}
					chunk.AppendCraftPoints.Remove(point);
				}
			}
			if (point != null && point.Kind == CraftPoint.CraftKind.Recycling)
			{
				AIProject.SaveData.Environment environment = (!Singleton<Game>.IsInstance()) ? null : Singleton<Game>.Instance.Environment;
				Dictionary<int, RecyclingData> dictionary = (environment == null) ? null : environment.RecyclingDataTable;
				if (dictionary != null && dictionary.ContainsKey(point.RegisterID))
				{
					dictionary.Remove(point.RegisterID);
				}
			}
		}

		// Token: 0x06003DF2 RID: 15858 RVA: 0x0016CBE4 File Offset: 0x0016AFE4
		public void AddRuntimeLightSwitchPoints(LightSwitchPoint[] lightSwitchPoints)
		{
			List<int> list = this.ChunkTable.Keys.ToList<int>();
			if (!list.IsNullOrEmpty<int>())
			{
				foreach (int key in list)
				{
					Chunk chunk;
					if (this.ChunkTable.TryGetValue(key, out chunk) && !(chunk == null))
					{
						chunk.LoadAppendLightSwitchPoints(lightSwitchPoints);
					}
				}
			}
			if (!lightSwitchPoints.IsNullOrEmpty<LightSwitchPoint>())
			{
				foreach (LightSwitchPoint lightSwitchPoint in lightSwitchPoints)
				{
					this.AddAppendCommandable(lightSwitchPoint);
					lightSwitchPoint.Switch(lightSwitchPoint.IsSwitch());
				}
			}
		}

		// Token: 0x06003DF3 RID: 15859 RVA: 0x0016CCC0 File Offset: 0x0016B0C0
		public void RemoveRuntimeLightSwitchPoint(LightSwitchPoint point)
		{
			List<int> list = this.ChunkTable.Keys.ToList<int>();
			if (!list.IsNullOrEmpty<int>())
			{
				foreach (int key in list)
				{
					Chunk chunk;
					if (this.ChunkTable.TryGetValue(key, out chunk) && !(chunk == null))
					{
						foreach (MapArea mapArea in chunk.MapAreas)
						{
							mapArea.AppendLightSwitchPoints.Remove(point);
						}
						chunk.AppendLightSwitchPoints.Remove(point);
					}
				}
			}
			Dictionary<int, bool> dictionary;
			if (Singleton<Game>.IsInstance())
			{
				AIProject.SaveData.Environment environment = Singleton<Game>.Instance.Environment;
				dictionary = ((environment != null) ? environment.LightObjectSwitchStateTable : null);
			}
			else
			{
				dictionary = null;
			}
			Dictionary<int, bool> dictionary2 = dictionary;
			if (!dictionary2.IsNullOrEmpty<int, bool>() && dictionary2.ContainsKey(point.RegisterID))
			{
				dictionary2.Remove(point.RegisterID);
			}
		}

		// Token: 0x06003DF4 RID: 15860 RVA: 0x0016CDEC File Offset: 0x0016B1EC
		public void AddAppendCommandable(ICommandable command)
		{
			this._appendCommandables.Add(command);
			this.Player.PlayerController.CommandArea.AddCommandableObject(command);
		}

		// Token: 0x06003DF5 RID: 15861 RVA: 0x0016CE10 File Offset: 0x0016B210
		public void RemoveAppendCommandable(ICommandable command)
		{
			this._appendCommandables.Remove(command);
			this.Player.PlayerController.CommandArea.RemoveCommandableObject(command);
		}

		// Token: 0x06003DF6 RID: 15862 RVA: 0x0016CE38 File Offset: 0x0016B238
		public void ClearAppendCommands()
		{
			if (!this._appendCommandables.IsNullOrEmpty<ICommandable>())
			{
				foreach (ICommandable commandable in this._appendCommandables)
				{
					this.Player.PlayerController.CommandArea.RemoveCommandableObject(commandable);
					if (commandable is ActionPoint)
					{
						ActionPoint ap = commandable as ActionPoint;
						foreach (int key in this._agentKeys)
						{
							AgentActor agentActor;
							if (this.AgentTable.TryGetValue(key, out agentActor))
							{
								agentActor.AgentController.SearchArea.RemovePoint(ap);
							}
						}
					}
				}
				this._appendCommandables.Clear();
			}
		}

		// Token: 0x06003DF7 RID: 15863 RVA: 0x0016CF40 File Offset: 0x0016B340
		public void AddAppendHPoints(HPoint[] hPoints)
		{
			this._pointAgent.AppendHPoints.Clear();
			this._pointAgent.AppendHPoints.AddRange(hPoints);
			int[] array = this.ChunkTable.Keys.ToArray<int>();
			foreach (int key in array)
			{
				Chunk chunk;
				if (this.ChunkTable.TryGetValue(key, out chunk))
				{
					chunk.LoadAppendHPoints(hPoints);
				}
			}
		}

		// Token: 0x06003DF8 RID: 15864 RVA: 0x0016CFBC File Offset: 0x0016B3BC
		public List<UnityEx.ValueTuple<int, List<StuffItem>>> GetInventoryList()
		{
			List<UnityEx.ValueTuple<int, List<StuffItem>>> list = ListPool<UnityEx.ValueTuple<int, List<StuffItem>>>.Get();
			WorldData worldData = (!Singleton<Game>.IsInstance()) ? null : Singleton<Game>.Instance.WorldData;
			AIProject.SaveData.Environment environment = (worldData == null) ? null : worldData.Environment;
			DefinePack definePack = (!Singleton<Resources>.IsInstance()) ? null : Singleton<Resources>.Instance.DefinePack;
			if (worldData != null && environment != null)
			{
				int i = 0;
				while (i < 2)
				{
					List<StuffItem> list2;
					int num;
					if (i == 0)
					{
						PlayerData playerData = worldData.PlayerData;
						list2 = playerData.ItemList;
						num = playerData.InventorySlotMax;
						goto IL_CD;
					}
					if (i == 1)
					{
						list2 = environment.ItemListInStorage;
						num = ((!(definePack != null)) ? 0 : definePack.ItemBoxCapacityDefines.StorageCapacity);
						goto IL_CD;
					}
					IL_EB:
					i++;
					continue;
					IL_CD:
					if (0 < num && list2 != null)
					{
						list.Add(new UnityEx.ValueTuple<int, List<StuffItem>>(num, list2));
						goto IL_EB;
					}
					goto IL_EB;
				}
			}
			return list;
		}

		// Token: 0x06003DF9 RID: 15865 RVA: 0x0016D0C3 File Offset: 0x0016B4C3
		public void ReturnInventoryList(List<UnityEx.ValueTuple<int, List<StuffItem>>> list)
		{
			if (list == null)
			{
				return;
			}
			list.Clear();
			ListPool<UnityEx.ValueTuple<int, List<StuffItem>>>.Release(list);
		}

		// Token: 0x06003DFA RID: 15866 RVA: 0x0016D0D8 File Offset: 0x0016B4D8
		public void SendItemListToList(int slotMax, List<StuffItem> sender, List<StuffItem> receiver)
		{
			if (slotMax <= 0 || sender.IsNullOrEmpty<StuffItem>() || receiver == null)
			{
				return;
			}
			for (int i = 0; i < sender.Count; i++)
			{
				StuffItem element = sender.GetElement(i);
				if (element == null || element.Count <= 0)
				{
					sender.RemoveAt(i);
					i--;
				}
				else
				{
					StuffItem stuffItem = new StuffItem(element);
					int num = 0;
					receiver.CanAddItem(slotMax, stuffItem, out num);
					if (0 < num)
					{
						num = Mathf.Min(stuffItem.Count, num);
						receiver.AddItem(stuffItem, num, slotMax);
					}
					element.Count -= num;
					if (element.Count <= 0)
					{
						sender.RemoveAt(i);
						i--;
					}
				}
			}
		}

		// Token: 0x06003DFB RID: 15867 RVA: 0x0016D19C File Offset: 0x0016B59C
		public void WarpToBasePoint(BasePoint basePoint, System.Action onEndFadeIn = null, System.Action onCompleted = null)
		{
			Map.<WarpToBasePoint>c__AnonStorey2F <WarpToBasePoint>c__AnonStorey2F = new Map.<WarpToBasePoint>c__AnonStorey2F();
			<WarpToBasePoint>c__AnonStorey2F.onEndFadeIn = onEndFadeIn;
			<WarpToBasePoint>c__AnonStorey2F.basePoint = basePoint;
			<WarpToBasePoint>c__AnonStorey2F.onCompleted = onCompleted;
			<WarpToBasePoint>c__AnonStorey2F.$this = this;
			if (<WarpToBasePoint>c__AnonStorey2F.basePoint == null)
			{
				return;
			}
			this.IsWarpProc = true;
			MapUIContainer.StartFade(FadeCanvas.PanelType.Blackout, FadeType.In, 1f, true).Subscribe(delegate(Unit _)
			{
			}, delegate()
			{
				System.Action onEndFadeIn2 = <WarpToBasePoint>c__AnonStorey2F.onEndFadeIn;
				if (onEndFadeIn2 != null)
				{
					onEndFadeIn2();
				}
				Transform warpPt = <WarpToBasePoint>c__AnonStorey2F.basePoint.WarpPoint;
				PlayerActor player = <WarpToBasePoint>c__AnonStorey2F.$this.Player;
				player.NavMeshAgent.Warp(warpPt.position);
				Quaternion rotation = warpPt.rotation;
				player.Rotation = rotation;
				Quaternion quaternion = rotation;
				player.CameraControl.XAxisValue = quaternion.eulerAngles.y;
				player.CameraControl.YAxisValue = 0.6f;
				Actor partner = player.Partner;
				if (partner != null)
				{
					AgentActor agentActor = partner as AgentActor;
					if (agentActor != null && agentActor.EventKey == AIProject.EventType.Move)
					{
						agentActor.Animation.StopAllAnimCoroutine();
					}
					Observable.EveryLateUpdate().Skip(2).Take(2).Subscribe(delegate(long _)
					{
						float shapeBodyValue = partner.ChaControl.GetShapeBodyValue(0);
						Vector3 newPosition = player.Position + player.Rotation * Singleton<Resources>.Instance.AgentProfile.GetOffsetInParty(shapeBodyValue);
						partner.NavMeshAgent.Warp(newPosition);
						partner.Rotation = warpPt.rotation;
					});
				}
				MapUIContainer.StartFade(FadeCanvas.PanelType.Blackout, FadeType.Out, 1f, false).Subscribe(delegate(Unit __)
				{
				}, delegate()
				{
					<WarpToBasePoint>c__AnonStorey2F.IsWarpProc = false;
					System.Action onCompleted2 = <WarpToBasePoint>c__AnonStorey2F.onCompleted;
					if (onCompleted2 != null)
					{
						onCompleted2();
					}
				});
			});
		}

		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x06003DFC RID: 15868 RVA: 0x0016D220 File Offset: 0x0016B620
		// (set) Token: 0x06003DFD RID: 15869 RVA: 0x0016D228 File Offset: 0x0016B628
		public bool IsWarpProc { get; private set; }

		// Token: 0x06003DFE RID: 15870 RVA: 0x0016D234 File Offset: 0x0016B634
		public void WarpToPairPoint(WarpPoint warpPoint, System.Action onEndFadeIn = null, System.Action onCompleted = null)
		{
			Map.<WarpToPairPoint>c__AnonStorey31 <WarpToPairPoint>c__AnonStorey = new Map.<WarpToPairPoint>c__AnonStorey31();
			<WarpToPairPoint>c__AnonStorey.onEndFadeIn = onEndFadeIn;
			<WarpToPairPoint>c__AnonStorey.warpPoint = warpPoint;
			<WarpToPairPoint>c__AnonStorey.onCompleted = onCompleted;
			<WarpToPairPoint>c__AnonStorey.$this = this;
			if (<WarpToPairPoint>c__AnonStorey.warpPoint == null)
			{
				return;
			}
			this.IsWarpProc = true;
			MapUIContainer.StartFade(FadeCanvas.PanelType.Blackout, FadeType.In, 1f, true).Subscribe(delegate(Unit _)
			{
			}, delegate()
			{
				System.Action onEndFadeIn2 = <WarpToPairPoint>c__AnonStorey.onEndFadeIn;
				if (onEndFadeIn2 != null)
				{
					onEndFadeIn2();
				}
				Transform warpTrans = <WarpToPairPoint>c__AnonStorey.warpPoint.transform;
				PlayerActor player = <WarpToPairPoint>c__AnonStorey.$this.Player;
				player.NavMeshWarp(warpTrans, 3, 100f);
				player.CameraControl.XAxisValue = warpTrans.eulerAngles.y;
				player.CameraControl.YAxisValue = 0.6f;
				Actor partner = player.Partner;
				if (partner != null)
				{
					AgentActor agentActor = partner as AgentActor;
					if (agentActor != null && agentActor.EventKey == AIProject.EventType.Move)
					{
						agentActor.Animation.StopAllAnimCoroutine();
					}
					Observable.EveryLateUpdate().Skip(2).Take(2).Subscribe(delegate(long _)
					{
						float shapeBodyValue = partner.ChaControl.GetShapeBodyValue(0);
						Vector3 position = player.Position + player.Rotation * Singleton<Resources>.Instance.AgentProfile.GetOffsetInParty(shapeBodyValue);
						partner.NavMeshWarp(position, warpTrans.rotation, 0, 100f);
					});
				}
				MapUIContainer.StartFade(FadeCanvas.PanelType.Blackout, FadeType.Out, 1f, false).Subscribe(delegate(Unit __)
				{
				}, delegate()
				{
					<WarpToPairPoint>c__AnonStorey.IsWarpProc = false;
					System.Action onCompleted2 = <WarpToPairPoint>c__AnonStorey.onCompleted;
					if (onCompleted2 != null)
					{
						onCompleted2();
					}
				});
			});
		}

		// Token: 0x06003DFF RID: 15871 RVA: 0x0016D2B8 File Offset: 0x0016B6B8
		public void ChangeMap(int mapID, System.Action onEndFadeIn = null, System.Action onCompleted = null)
		{
			Singleton<Game>.Instance.WorldData.MapID = mapID;
			Observable.FromCoroutine(() => this.ChangeMapCoroutine(mapID, onEndFadeIn, onCompleted), false).Subscribe<Unit>();
		}

		// Token: 0x06003E00 RID: 15872 RVA: 0x0016D314 File Offset: 0x0016B714
		private IEnumerator ChangeMapCoroutine(int mapID, System.Action onEndFadeIn = null, System.Action onCompleted = null)
		{
			Singleton<Scene>.Instance.loadingPanel.CanvasGroup.alpha = 0f;
			Singleton<Scene>.Instance.loadingPanel.gameObject.SetActive(true);
			Singleton<Scene>.Instance.loadingPanel.Play();
			yield return ObservableEasing.Linear(1f, true).FrameTimeInterval(true).Do(delegate(TimeInterval<float> x)
			{
				Singleton<Scene>.Instance.loadingPanel.CanvasGroup.alpha = x.Value;
			}).ToYieldInstruction<TimeInterval<float>>();
			if (onEndFadeIn != null)
			{
				onEndFadeIn();
			}
			if (Singleton<AnimalManager>.IsInstance())
			{
				Singleton<AnimalManager>.Instance.ReleaseAnimal();
			}
			this.ReleaseMap();
			this.ReleaseAgents();
			this.Player.DisableEntity();
			this.Merchant.StopBehavior();
			this.Merchant.DisableEntity();
			this.Merchant.DeactivateNavMeshElement();
			yield return this.LoadMap(mapID);
			yield return this.LoadNavMeshSource();
			yield return this.LoadElements();
			yield return this.LoadMerchantPoint();
			yield return this.LoadEventPoints();
			yield return this.LoadStoryPoints();
			yield return this.LoadHousingObj(mapID);
			this.SetVanishList();
			this.BuildNavMesh();
			this._pointAgent.CreateHousingWaypoint();
			yield return this.LoadAnimalPoint();
			yield return this.SetupPoint();
			AnimalManager animalManager = Singleton<AnimalManager>.Instance;
			yield return animalManager.SetupPointsAsync(this._pointAgent);
			if (Singleton<SoundPlayer>.IsInstance())
			{
				Singleton<SoundPlayer>.Instance.RefreshJukeBoxTable(this);
			}
			yield return this.RefreshAsync();
			WorldData worldData = Singleton<Game>.Instance.WorldData;
			bool existsBackup = Singleton<Game>.Instance.ExistsBackup(worldData.WorldID);
			yield return this.LoadAgents(worldData, existsBackup);
			this.RefreshAgentStatus();
			this.RefreshAnimalStatus();
			yield return null;
			this.RefreshActiveTimeRelationObjects();
			this.InitActionPoints();
			this.Player.PlayerController.CommandArea.InitCommandStates();
			this.InitCommandable();
			this.AddAppendTargetPoints(Singleton<Housing>.Instance.ActionPoints);
			this.AddRuntimeFarmPoints(Singleton<Housing>.Instance.FarmPoints);
			this.AddPetHomePoints(Singleton<Housing>.Instance.PetHomePoints);
			this.AddJukePoints(Singleton<Housing>.Instance.JukePoints);
			this.AddRuntimeCraftPoints(Singleton<Housing>.Instance.CraftPoints);
			this.AddRuntimeLightSwitchPoints(Singleton<Housing>.Instance.LightSwitchPoints);
			this.AddAppendHPoints(Singleton<Housing>.Instance.HPoints);
			foreach (ActionPoint actionPoint in Singleton<Housing>.Instance.ActionPoints)
			{
				OffMeshLink[] componentsInChildren = actionPoint.GetComponentsInChildren<OffMeshLink>();
				foreach (OffMeshLink offMeshLink in componentsInChildren)
				{
					offMeshLink.UpdatePositions();
				}
			}
			this.LoadAgentTargetActionPoint();
			this.LoadAgentTargetActor();
			WorldData worldData2 = Singleton<Game>.Instance.WorldData;
			bool existsBackup2 = Singleton<Game>.Instance.ExistsBackup(worldData2.WorldID);
			yield return this.LoadAnimals(worldData2, existsBackup2);
			this.RefreshAreaOpenLinkedObject();
			this.RefreshTimeOpenLinkedObject();
			foreach (SoundData soundData in Config.SoundData.Sounds)
			{
				if (soundData != null)
				{
					soundData.Refresh();
				}
			}
			yield return this.LoadEnviroObject();
			MapArea mapArea = this.Player.MapArea;
			int? num = (mapArea != null) ? new int?(mapArea.AreaID) : null;
			this.ResettingEnviroAreaElement(mapID, (num == null) ? 0 : num.Value);
			MapArea mapArea2 = this.Player.MapArea;
			int? num2 = (mapArea2 != null) ? new int?(mapArea2.AreaID) : null;
			this.RefreshHousingEnv3DSEPoints(mapID, (num2 == null) ? 0 : num2.Value);
			yield return new global::WaitForSecondsRealtime(1f);
			WaitForSeconds waitTime = new WaitForSeconds(0.5f);
			while (!this.WaitCompletionAgents())
			{
				yield return waitTime;
			}
			this._agentVanishAreaList = null;
			foreach (AgentActor agentActor in this._agentTable.Values)
			{
				agentActor.ActivateNavMeshAgent();
			}
			yield return null;
			foreach (AgentActor agent in this._agentTable.Values)
			{
				yield return agent.CalculateWaypoints();
			}
			this.Player.ActivateNavMeshAgent();
			this.Merchant.SetMerchantPoints(this._pointAgent.MerchantPoints);
			this.Merchant.ChangeMap(mapID);
			MapUIContainer.InitializeMinimap();
			animalManager.StartAllAnimalCreate();
			yield return Singleton<Resources>.Instance.HSceneTable.LoadHObj();
			if (Singleton<AnimalManager>.IsInstance())
			{
				Singleton<AnimalManager>.Instance.ActiveMapScene = true;
			}
			GC.Collect();
			this.Merchant.EnableEntity();
			this.Merchant.FirstAppear();
			this.Player.EnableEntity();
			Transform startPoint = this._pointAgent.ShipPoints[0].StartPointFromMigrate;
			this.Player.NavMeshAgent.Warp(startPoint.position);
			this.Player.Rotation = startPoint.rotation;
			this.InitSearchActorTargetsAll();
			foreach (KeyValuePair<int, AgentActor> keyValuePair in this._agentTable)
			{
				if (keyValuePair.Value.AgentData.CarryingItem != null && keyValuePair.Value.Mode != Desire.ActionType.SearchEatSpot && keyValuePair.Value.Mode != Desire.ActionType.EndTaskEat && keyValuePair.Value.Mode != Desire.ActionType.EndTaskEatThere)
				{
					keyValuePair.Value.AgentData.CarryingItem = null;
				}
				keyValuePair.Value.EnableBehavior();
				keyValuePair.Value.BehaviorResources.ChangeMode(keyValuePair.Value.Mode);
			}
			this.Player.PlayerController.CommandArea.InitCommandStates();
			this.Player.PlayerController.CommandArea.RefreshCommands();
			yield return ObservableEasing.Linear(1f, true).FrameTimeInterval(true).Do(delegate(TimeInterval<float> x)
			{
				Singleton<Scene>.Instance.loadingPanel.CanvasGroup.alpha = 1f - x.Value;
			}).ToYieldInstruction<TimeInterval<float>>();
			Singleton<Scene>.Instance.loadingPanel.Stop();
			Singleton<Scene>.Instance.loadingPanel.gameObject.SetActive(false);
			while (Time.timeScale == 0f)
			{
				yield return null;
			}
			if (Singleton<Game>.IsInstance())
			{
				AIProject.SaveData.Environment environment = Singleton<Game>.Instance.Environment;
				if (environment != null)
				{
					int num3 = 29;
					if (environment.TutorialProgress == num3)
					{
						Map.ForcedSetTutorialProgressAndUIUpdate(num3 + 1);
					}
				}
			}
			if (onCompleted != null)
			{
				onCompleted();
			}
			yield break;
		}

		// Token: 0x06003E01 RID: 15873 RVA: 0x0016D344 File Offset: 0x0016B744
		public bool WaitCompletionAgents()
		{
			foreach (AgentActor agentActor in this._agentTable.Values)
			{
				if (!agentActor.IsInit)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x06003E02 RID: 15874 RVA: 0x0016D3B4 File Offset: 0x0016B7B4
		public List<Map.VisibleObject> LstMapVanish
		{
			[CompilerGenerated]
			get
			{
				return this.lstMapVanish;
			}
		}

		// Token: 0x06003E03 RID: 15875 RVA: 0x0016D3BC File Offset: 0x0016B7BC
		public void SetVanishList()
		{
			Dictionary<int, List<List<Resources.MapTables.VisibleObjectInfo>>> vanishList = Singleton<Resources>.Instance.Map.VanishList;
			List<List<Resources.MapTables.VisibleObjectInfo>> list;
			if (!Singleton<Resources>.Instance.Map.VanishList.TryGetValue(this.MapID, out list))
			{
				return;
			}
			for (int i = 0; i < list.Count; i++)
			{
				int index = i;
				for (int j = 0; j < list[index].Count; j++)
				{
					int index2 = j;
					List<Map.VisibleObject> list2 = this.lstMapVanish;
					Map.VisibleObject visibleObject = new Map.VisibleObject();
					visibleObject.nameCollider = list[index][index2].nameCollider;
					Map.VisibleObject visibleObject2 = visibleObject;
					GameObject gameObject = GameObject.Find(list[index][index2].nameCollider);
					visibleObject2.collider = ((gameObject != null) ? gameObject.GetComponentInChildren<Collider>() : null);
					visibleObject.vanishObj = GameObject.Find(list[index][index2].VanishObjName);
					list2.Add(visibleObject);
				}
			}
		}

		// Token: 0x04003B1A RID: 15130
		private Dictionary<int, Actor> _actorTable = new Dictionary<int, Actor>();

		// Token: 0x04003B1E RID: 15134
		private Dictionary<int, AgentActor> _agentTable = new Dictionary<int, AgentActor>();

		// Token: 0x04003B1F RID: 15135
		private List<int> _agentKeys = new List<int>();

		// Token: 0x04003B21 RID: 15137
		private Dictionary<int, ChaFile> _agentChaFileTable = new Dictionary<int, ChaFile>();

		// Token: 0x04003B26 RID: 15142
		private Transform _enviroGroupRoot;

		// Token: 0x04003B2B RID: 15147
		[SerializeField]
		private Transform _mapRoot;

		// Token: 0x04003B2C RID: 15148
		[SerializeField]
		private GameObject _waterObject;

		// Token: 0x04003B33 RID: 15155
		[SerializeField]
		private PointManager _pointAgent;

		// Token: 0x04003B40 RID: 15168
		private GameObject _mapDataEffect;

		// Token: 0x04003B41 RID: 15169
		private List<int> _agentVanishAreaList;

		// Token: 0x04003B46 RID: 15174
		private GameObject _agentPrefab;

		// Token: 0x04003B48 RID: 15176
		private GameObject _tutorialSearchPointObject;

		// Token: 0x04003B4A RID: 15178
		private List<int> _removeRegIDCache = new List<int>();

		// Token: 0x04003B4B RID: 15179
		private List<ICommandable> _appendCommandables = new List<ICommandable>();

		// Token: 0x04003B4D RID: 15181
		private List<Map.VisibleObject> lstMapVanish = new List<Map.VisibleObject>();

		// Token: 0x020008EA RID: 2282
		[Serializable]
		public class VisibleObject
		{
			// Token: 0x04003B64 RID: 15204
			public string nameCollider = string.Empty;

			// Token: 0x04003B65 RID: 15205
			public Collider collider;

			// Token: 0x04003B66 RID: 15206
			public float delay;

			// Token: 0x04003B67 RID: 15207
			public bool isVisible = true;

			// Token: 0x04003B68 RID: 15208
			public GameObject vanishObj;
		}
	}
}
