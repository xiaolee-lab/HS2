using System;
using System.Runtime.CompilerServices;
using AIProject.Animal;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000B53 RID: 2899
	public class AnimalDefinePack : ScriptableObject
	{
		// Token: 0x17000FB0 RID: 4016
		// (get) Token: 0x0600566B RID: 22123 RVA: 0x00258F86 File Offset: 0x00257386
		public AnimalDefinePack.AssetBundleNameGroup AssetBundleNames
		{
			[CompilerGenerated]
			get
			{
				return this._assetBundleNames;
			}
		}

		// Token: 0x17000FB1 RID: 4017
		// (get) Token: 0x0600566C RID: 22124 RVA: 0x00258F8E File Offset: 0x0025738E
		public AnimalDefinePack.AnimatorInfoGroup AnimatorInfo
		{
			[CompilerGenerated]
			get
			{
				return this._animatorInfo;
			}
		}

		// Token: 0x17000FB2 RID: 4018
		// (get) Token: 0x0600566D RID: 22125 RVA: 0x00258F96 File Offset: 0x00257396
		public AnimalDefinePack.SystemInfoGroup SystemInfo
		{
			[CompilerGenerated]
			get
			{
				return this._systemInfo;
			}
		}

		// Token: 0x17000FB3 RID: 4019
		// (get) Token: 0x0600566E RID: 22126 RVA: 0x00258F9E File Offset: 0x0025739E
		public AnimalDefinePack.CreateStartTimeInfoGroup CreateStartTimeInfo
		{
			[CompilerGenerated]
			get
			{
				return this._createStartTimeInfo;
			}
		}

		// Token: 0x17000FB4 RID: 4020
		// (get) Token: 0x0600566F RID: 22127 RVA: 0x00258FA6 File Offset: 0x002573A6
		public AnimalDefinePack.NavMeshAgentInfoGroup AgentInfo
		{
			[CompilerGenerated]
			get
			{
				return this._agentInfo;
			}
		}

		// Token: 0x17000FB5 RID: 4021
		// (get) Token: 0x06005670 RID: 22128 RVA: 0x00258FAE File Offset: 0x002573AE
		public AnimalDefinePack.AllAnimalInfoGroup AllAnimalInfo
		{
			[CompilerGenerated]
			get
			{
				return this._allAnimalInfo;
			}
		}

		// Token: 0x17000FB6 RID: 4022
		// (get) Token: 0x06005671 RID: 22129 RVA: 0x00258FB6 File Offset: 0x002573B6
		public AnimalDefinePack.WithActorInfoGroup WithActorInfo
		{
			[CompilerGenerated]
			get
			{
				return this._withActorInfo;
			}
		}

		// Token: 0x17000FB7 RID: 4023
		// (get) Token: 0x06005672 RID: 22130 RVA: 0x00258FBE File Offset: 0x002573BE
		public AnimalDefinePack.GroundAnimalInfoGroup GroundAnimalInfo
		{
			[CompilerGenerated]
			get
			{
				return this._groundAnimalInfo;
			}
		}

		// Token: 0x17000FB8 RID: 4024
		// (get) Token: 0x06005673 RID: 22131 RVA: 0x00258FC6 File Offset: 0x002573C6
		public AnimalDefinePack.GroundWildInfoGroup GroundWildInfo
		{
			[CompilerGenerated]
			get
			{
				return this._groundWildInfo;
			}
		}

		// Token: 0x17000FB9 RID: 4025
		// (get) Token: 0x06005674 RID: 22132 RVA: 0x00258FCE File Offset: 0x002573CE
		public AnimalDefinePack.PetCatInfoGroup PetCatInfo
		{
			[CompilerGenerated]
			get
			{
				return this._petCatInfo;
			}
		}

		// Token: 0x17000FBA RID: 4026
		// (get) Token: 0x06005675 RID: 22133 RVA: 0x00258FD6 File Offset: 0x002573D6
		public AnimalDefinePack.DesireInfoGroup DesireInfo
		{
			[CompilerGenerated]
			get
			{
				return this._desireInfo;
			}
		}

		// Token: 0x17000FBB RID: 4027
		// (get) Token: 0x06005676 RID: 22134 RVA: 0x00258FDE File Offset: 0x002573DE
		public AnimalDefinePack.ChickenCoopWaypointSettings ChickenCoopWaypointSetting
		{
			[CompilerGenerated]
			get
			{
				return this._chickenCoopWaypointSetting;
			}
		}

		// Token: 0x17000FBC RID: 4028
		// (get) Token: 0x06005677 RID: 22135 RVA: 0x00258FE6 File Offset: 0x002573E6
		public AnimalDefinePack.SoundIDInfo SoundID
		{
			[CompilerGenerated]
			get
			{
				return this._soundID;
			}
		}

		// Token: 0x04004FAE RID: 20398
		[SerializeField]
		[Header("Asset Bundle")]
		private AnimalDefinePack.AssetBundleNameGroup _assetBundleNames;

		// Token: 0x04004FAF RID: 20399
		[SerializeField]
		[Header("Animator")]
		private AnimalDefinePack.AnimatorInfoGroup _animatorInfo;

		// Token: 0x04004FB0 RID: 20400
		[SerializeField]
		[Header("System Info")]
		private AnimalDefinePack.SystemInfoGroup _systemInfo;

		// Token: 0x04004FB1 RID: 20401
		[SerializeField]
		[Header("Create Start Time Info")]
		private AnimalDefinePack.CreateStartTimeInfoGroup _createStartTimeInfo;

		// Token: 0x04004FB2 RID: 20402
		[SerializeField]
		[Header("NavMesh Agent Info")]
		private AnimalDefinePack.NavMeshAgentInfoGroup _agentInfo;

		// Token: 0x04004FB3 RID: 20403
		[SerializeField]
		[Header("All Animal Info")]
		private AnimalDefinePack.AllAnimalInfoGroup _allAnimalInfo;

		// Token: 0x04004FB4 RID: 20404
		[SerializeField]
		[Header("WithActor Info")]
		private AnimalDefinePack.WithActorInfoGroup _withActorInfo;

		// Token: 0x04004FB5 RID: 20405
		[SerializeField]
		[Header("Ground Animal Info")]
		private AnimalDefinePack.GroundAnimalInfoGroup _groundAnimalInfo;

		// Token: 0x04004FB6 RID: 20406
		[SerializeField]
		[Header("Ground Wild Animal Info")]
		private AnimalDefinePack.GroundWildInfoGroup _groundWildInfo;

		// Token: 0x04004FB7 RID: 20407
		[SerializeField]
		[Header("Pet Cat Info")]
		private AnimalDefinePack.PetCatInfoGroup _petCatInfo;

		// Token: 0x04004FB8 RID: 20408
		[SerializeField]
		[Header("Desire Info")]
		private AnimalDefinePack.DesireInfoGroup _desireInfo;

		// Token: 0x04004FB9 RID: 20409
		[SerializeField]
		[Header("Chicken Coop Waypoint Setting")]
		private AnimalDefinePack.ChickenCoopWaypointSettings _chickenCoopWaypointSetting;

		// Token: 0x04004FBA RID: 20410
		[SerializeField]
		[Header("Sound ID Info")]
		private AnimalDefinePack.SoundIDInfo _soundID = default(AnimalDefinePack.SoundIDInfo);

		// Token: 0x02000B54 RID: 2900
		[Serializable]
		public class AssetBundleNameGroup
		{
			// Token: 0x17000FBD RID: 4029
			// (get) Token: 0x06005679 RID: 22137 RVA: 0x00259087 File Offset: 0x00257487
			public string AnimalInfoDirectory
			{
				[CompilerGenerated]
				get
				{
					return this._animalInfoDirectory;
				}
			}

			// Token: 0x17000FBE RID: 4030
			// (get) Token: 0x0600567A RID: 22138 RVA: 0x0025908F File Offset: 0x0025748F
			public string PrefabsBundleDirectory
			{
				[CompilerGenerated]
				get
				{
					return this._prefabsBundleDirectory;
				}
			}

			// Token: 0x17000FBF RID: 4031
			// (get) Token: 0x0600567B RID: 22139 RVA: 0x00259097 File Offset: 0x00257497
			public string ActionInfoListBundleDirectory
			{
				[CompilerGenerated]
				get
				{
					return this._actionInfoListBundleDirectory;
				}
			}

			// Token: 0x17000FC0 RID: 4032
			// (get) Token: 0x0600567C RID: 22140 RVA: 0x0025909F File Offset: 0x0025749F
			public string AnimatorListBundleDirectory
			{
				[CompilerGenerated]
				get
				{
					return this._animatorListBundleDirectory;
				}
			}

			// Token: 0x17000FC1 RID: 4033
			// (get) Token: 0x0600567D RID: 22141 RVA: 0x002590A7 File Offset: 0x002574A7
			public string AnimeInfoListBundleDirectory
			{
				[CompilerGenerated]
				get
				{
					return this._animeInfoListBundleDirectory;
				}
			}

			// Token: 0x17000FC2 RID: 4034
			// (get) Token: 0x0600567E RID: 22142 RVA: 0x002590AF File Offset: 0x002574AF
			public string WithActorAnimeInfoListBundleDirectory
			{
				[CompilerGenerated]
				get
				{
					return this._withActorAnimeInfoListBundleDirectory;
				}
			}

			// Token: 0x17000FC3 RID: 4035
			// (get) Token: 0x0600567F RID: 22143 RVA: 0x002590B7 File Offset: 0x002574B7
			public string ModelInfoListBundleDirector
			{
				[CompilerGenerated]
				get
				{
					return this._modelInfoListBundleDirectory;
				}
			}

			// Token: 0x17000FC4 RID: 4036
			// (get) Token: 0x06005680 RID: 22144 RVA: 0x002590BF File Offset: 0x002574BF
			public string LookInfoListBundleDirectory
			{
				[CompilerGenerated]
				get
				{
					return this._lookInfoListBundleDirectory;
				}
			}

			// Token: 0x17000FC5 RID: 4037
			// (get) Token: 0x06005681 RID: 22145 RVA: 0x002590C7 File Offset: 0x002574C7
			public string StateInfoListBundleDirectory
			{
				[CompilerGenerated]
				get
				{
					return this._stateInfoListBundleDirectory;
				}
			}

			// Token: 0x17000FC6 RID: 4038
			// (get) Token: 0x06005682 RID: 22146 RVA: 0x002590CF File Offset: 0x002574CF
			public string DesireInfoListBundleDirectory
			{
				[CompilerGenerated]
				get
				{
					return this._desireInfoListBundleDirectory;
				}
			}

			// Token: 0x17000FC7 RID: 4039
			// (get) Token: 0x06005683 RID: 22147 RVA: 0x002590D7 File Offset: 0x002574D7
			public string PlayerInfoListBundleDirectory
			{
				[CompilerGenerated]
				get
				{
					return this._playerInfoListBundleDirectory;
				}
			}

			// Token: 0x17000FC8 RID: 4040
			// (get) Token: 0x06005684 RID: 22148 RVA: 0x002590DF File Offset: 0x002574DF
			public string AnimalPointPrefabBundleDirectory
			{
				[CompilerGenerated]
				get
				{
					return this._animalPointPrefabBundleDirectory;
				}
			}

			// Token: 0x04004FBB RID: 20411
			[Header("AssetBundle Directory")]
			[SerializeField]
			[Tooltip("動物の色々なリストまとめ")]
			private string _animalInfoDirectory = string.Empty;

			// Token: 0x04004FBC RID: 20412
			[SerializeField]
			[Tooltip("ペットのベースのプレファブのパス")]
			private string _prefabsBundleDirectory = string.Empty;

			// Token: 0x04004FBD RID: 20413
			[SerializeField]
			[Tooltip("行動に関する情報リストのパス")]
			private string _actionInfoListBundleDirectory = string.Empty;

			// Token: 0x04004FBE RID: 20414
			[SerializeField]
			[Tooltip("各動物のアニメーターのパス")]
			private string _animatorListBundleDirectory = string.Empty;

			// Token: 0x04004FBF RID: 20415
			[SerializeField]
			[Tooltip("行動に対するアニメーション名のリストのパス")]
			private string _animeInfoListBundleDirectory = string.Empty;

			// Token: 0x04004FC0 RID: 20416
			[SerializeField]
			[Tooltip("Actor(Player/Agent)と連携されたアニメーション名リストのパス")]
			private string _withActorAnimeInfoListBundleDirectory = string.Empty;

			// Token: 0x04004FC1 RID: 20417
			[SerializeField]
			[Tooltip("モデルデータのリストのパス")]
			private string _modelInfoListBundleDirectory = string.Empty;

			// Token: 0x04004FC2 RID: 20418
			[SerializeField]
			[Tooltip("注視条件情報のリストのパス")]
			private string _lookInfoListBundleDirectory = string.Empty;

			// Token: 0x04004FC3 RID: 20419
			[SerializeField]
			[Tooltip("状態に関する情報のリストのパス")]
			private string _stateInfoListBundleDirectory = string.Empty;

			// Token: 0x04004FC4 RID: 20420
			[SerializeField]
			[Tooltip("欲求に関する情報のリストのパス")]
			private string _desireInfoListBundleDirectory = string.Empty;

			// Token: 0x04004FC5 RID: 20421
			[SerializeField]
			[Tooltip("動物に関係したプレイヤーの設定情報等のリストのパス")]
			private string _playerInfoListBundleDirectory = string.Empty;

			// Token: 0x04004FC6 RID: 20422
			[SerializeField]
			[Tooltip("動物ポイントのプレファブ情報のリストのパス")]
			private string _animalPointPrefabBundleDirectory = string.Empty;
		}

		// Token: 0x02000B55 RID: 2901
		[Serializable]
		public class AnimatorInfoGroup
		{
			// Token: 0x17000FC9 RID: 4041
			// (get) Token: 0x06005686 RID: 22150 RVA: 0x0025911B File Offset: 0x0025751B
			public string LocomotionParamName
			{
				[CompilerGenerated]
				get
				{
					return this._locomotionParamName;
				}
			}

			// Token: 0x17000FCA RID: 4042
			// (get) Token: 0x06005687 RID: 22151 RVA: 0x00259123 File Offset: 0x00257523
			public string AnimationSpeedParamName
			{
				[CompilerGenerated]
				get
				{
					return this._animationSpeedParamName;
				}
			}

			// Token: 0x17000FCB RID: 4043
			// (get) Token: 0x06005688 RID: 22152 RVA: 0x0025912B File Offset: 0x0025752B
			public float BirdAnimationSpeed
			{
				[CompilerGenerated]
				get
				{
					return this._birdAnimationSpeed;
				}
			}

			// Token: 0x17000FCC RID: 4044
			// (get) Token: 0x06005689 RID: 22153 RVA: 0x00259133 File Offset: 0x00257533
			public float ButterflyAnimationSpeed
			{
				[CompilerGenerated]
				get
				{
					return this._butterflyAnimationSpeed;
				}
			}

			// Token: 0x04004FC7 RID: 20423
			[SerializeField]
			[Tooltip("Locomotionのパラメーター名")]
			private string _locomotionParamName = string.Empty;

			// Token: 0x04004FC8 RID: 20424
			[SerializeField]
			[Tooltip("アニメーション速度のパラメーター名")]
			private string _animationSpeedParamName = string.Empty;

			// Token: 0x04004FC9 RID: 20425
			[SerializeField]
			[Tooltip("トリのアニメーション速度(通常１)")]
			private float _birdAnimationSpeed = 1f;

			// Token: 0x04004FCA RID: 20426
			[SerializeField]
			[Tooltip("チョウのアニメーション速度(通常１)")]
			private float _butterflyAnimationSpeed = 1f;
		}

		// Token: 0x02000B56 RID: 2902
		[Serializable]
		public class SystemInfoGroup
		{
			// Token: 0x17000FCD RID: 4045
			// (get) Token: 0x0600568B RID: 22155 RVA: 0x002591C2 File Offset: 0x002575C2
			public int WildGroundMaxNum
			{
				[CompilerGenerated]
				get
				{
					return this._WildGroundMaxNum;
				}
			}

			// Token: 0x17000FCE RID: 4046
			// (get) Token: 0x0600568C RID: 22156 RVA: 0x002591CA File Offset: 0x002575CA
			public int WildCatMaxNum
			{
				[CompilerGenerated]
				get
				{
					return this._wildCatMaxNum;
				}
			}

			// Token: 0x17000FCF RID: 4047
			// (get) Token: 0x0600568D RID: 22157 RVA: 0x002591D2 File Offset: 0x002575D2
			public int WildChickenMaxNum
			{
				[CompilerGenerated]
				get
				{
					return this._wildChickenMaxNum;
				}
			}

			// Token: 0x17000FD0 RID: 4048
			// (get) Token: 0x0600568E RID: 22158 RVA: 0x002591DA File Offset: 0x002575DA
			public int WildMechaMaxNum
			{
				[CompilerGenerated]
				get
				{
					return this._wildMechaMaxNum;
				}
			}

			// Token: 0x17000FD1 RID: 4049
			// (get) Token: 0x0600568F RID: 22159 RVA: 0x002591E2 File Offset: 0x002575E2
			public int WildFrogMaxNum
			{
				[CompilerGenerated]
				get
				{
					return this._wildFrogMaxNum;
				}
			}

			// Token: 0x17000FD2 RID: 4050
			// (get) Token: 0x06005690 RID: 22160 RVA: 0x002591EA File Offset: 0x002575EA
			public int WildButterflyMaxNum
			{
				[CompilerGenerated]
				get
				{
					return this._wildButterflyMaxNum;
				}
			}

			// Token: 0x17000FD3 RID: 4051
			// (get) Token: 0x06005691 RID: 22161 RVA: 0x002591F2 File Offset: 0x002575F2
			public int WildBirdFlockMaxNum
			{
				[CompilerGenerated]
				get
				{
					return this._wildBirdFlockMaxNum;
				}
			}

			// Token: 0x17000FD4 RID: 4052
			// (get) Token: 0x06005692 RID: 22162 RVA: 0x002591FA File Offset: 0x002575FA
			public Vector2 WildCatCreateCoolTime
			{
				[CompilerGenerated]
				get
				{
					return this._wildCatCreateCoolTime;
				}
			}

			// Token: 0x17000FD5 RID: 4053
			// (get) Token: 0x06005693 RID: 22163 RVA: 0x00259202 File Offset: 0x00257602
			public Vector2 WildChickenCreateCoolTime
			{
				[CompilerGenerated]
				get
				{
					return this._wildChickenCreateCoolTime;
				}
			}

			// Token: 0x17000FD6 RID: 4054
			// (get) Token: 0x06005694 RID: 22164 RVA: 0x0025920A File Offset: 0x0025760A
			public float PopDistance
			{
				[CompilerGenerated]
				get
				{
					return this._popDistance;
				}
			}

			// Token: 0x17000FD7 RID: 4055
			// (get) Token: 0x06005695 RID: 22165 RVA: 0x00259212 File Offset: 0x00257612
			public Vector2 PopPointCoolTimeRange
			{
				[CompilerGenerated]
				get
				{
					return this._popPointCoolTimeRange;
				}
			}

			// Token: 0x17000FD8 RID: 4056
			// (get) Token: 0x06005696 RID: 22166 RVA: 0x0025921A File Offset: 0x0025761A
			public float ViewportPointScale
			{
				[CompilerGenerated]
				get
				{
					return this._viewportPointScale;
				}
			}

			// Token: 0x17000FD9 RID: 4057
			// (get) Token: 0x06005697 RID: 22167 RVA: 0x00259222 File Offset: 0x00257622
			public Vector2 FrogCoolTimeRange
			{
				[CompilerGenerated]
				get
				{
					return this._frogCoolTimeRange;
				}
			}

			// Token: 0x04004FCB RID: 20427
			[SerializeField]
			[Tooltip("地面系野生動物同時最大出現数")]
			private int _WildGroundMaxNum = 4;

			// Token: 0x04004FCC RID: 20428
			[SerializeField]
			[Tooltip("野生ネコ同時最大出現数")]
			private int _wildCatMaxNum = 2;

			// Token: 0x04004FCD RID: 20429
			[SerializeField]
			[Tooltip("野生ニワトリ同時最大出現数")]
			private int _wildChickenMaxNum = 2;

			// Token: 0x04004FCE RID: 20430
			[SerializeField]
			[Tooltip("野生メカ同時最大出現数")]
			private int _wildMechaMaxNum = 2;

			// Token: 0x04004FCF RID: 20431
			[SerializeField]
			[Tooltip("野生カエル同時最大出現数")]
			private int _wildFrogMaxNum = 2;

			// Token: 0x04004FD0 RID: 20432
			[SerializeField]
			[Tooltip("野生チョウ同時最大出現数")]
			private int _wildButterflyMaxNum = 3;

			// Token: 0x04004FD1 RID: 20433
			[SerializeField]
			[Tooltip("野生トリの群れ同時最大出現数")]
			private int _wildBirdFlockMaxNum = 1;

			// Token: 0x04004FD2 RID: 20434
			[SerializeField]
			[RangeZeroOver("野生ネコ生成間隔(秒)の範囲")]
			private Vector2 _wildCatCreateCoolTime = Vector2.zero;

			// Token: 0x04004FD3 RID: 20435
			[SerializeField]
			[RangeZeroOver("野生ニワトリ生成間隔(秒)の範囲")]
			private Vector2 _wildChickenCreateCoolTime = Vector2.zero;

			// Token: 0x04004FD4 RID: 20436
			[SerializeField]
			[Min(0f)]
			[Tooltip("Playerからどのくらい離れていたら生成されるかの距離")]
			private float _popDistance = 50f;

			// Token: 0x04004FD5 RID: 20437
			[SerializeField]
			[RangeZeroOver("地面系動物:出現ポイント使用後のクールタイム")]
			private Vector2 _popPointCoolTimeRange = Vector2.zero;

			// Token: 0x04004FD6 RID: 20438
			[SerializeField]
			[Min(0f)]
			[Tooltip("生成時の視界判定時の範囲内のポイントのスケール")]
			private float _viewportPointScale = 0.75f;

			// Token: 0x04004FD7 RID: 20439
			[SerializeField]
			[RangeZeroOver("野生カエル：クールタイム")]
			private Vector2 _frogCoolTimeRange = Vector2.zero;
		}

		// Token: 0x02000B57 RID: 2903
		[Serializable]
		public class ChickenCoopWaypointSettings
		{
			// Token: 0x17000FDA RID: 4058
			// (get) Token: 0x06005699 RID: 22169 RVA: 0x00259286 File Offset: 0x00257686
			public Vector3 Installation
			{
				[CompilerGenerated]
				get
				{
					return this._installation;
				}
			}

			// Token: 0x17000FDB RID: 4059
			// (get) Token: 0x0600569A RID: 22170 RVA: 0x0025928E File Offset: 0x0025768E
			public float SampleDistance
			{
				[CompilerGenerated]
				get
				{
					return this._sampleDistance;
				}
			}

			// Token: 0x17000FDC RID: 4060
			// (get) Token: 0x0600569B RID: 22171 RVA: 0x00259296 File Offset: 0x00257696
			public float ClosestEdgeDistance
			{
				[CompilerGenerated]
				get
				{
					return this._closestEdgeDistance;
				}
			}

			// Token: 0x17000FDD RID: 4061
			// (get) Token: 0x0600569C RID: 22172 RVA: 0x0025929E File Offset: 0x0025769E
			public LayerMask Layer
			{
				[CompilerGenerated]
				get
				{
					return this._layerMask;
				}
			}

			// Token: 0x17000FDE RID: 4062
			// (get) Token: 0x0600569D RID: 22173 RVA: 0x002592A6 File Offset: 0x002576A6
			public string TagName
			{
				[CompilerGenerated]
				get
				{
					return this._tagName;
				}
			}

			// Token: 0x17000FDF RID: 4063
			// (get) Token: 0x0600569E RID: 22174 RVA: 0x002592AE File Offset: 0x002576AE
			public float CanEatEdgeDistance
			{
				[CompilerGenerated]
				get
				{
					return this._canEatEdgeDistance;
				}
			}

			// Token: 0x17000FE0 RID: 4064
			// (get) Token: 0x0600569F RID: 22175 RVA: 0x002592B6 File Offset: 0x002576B6
			public int AgentAreaMask
			{
				[CompilerGenerated]
				get
				{
					return this._agentAreaMask;
				}
			}

			// Token: 0x04004FD8 RID: 20440
			[SerializeField]
			private Vector3 _installation = default(Vector3);

			// Token: 0x04004FD9 RID: 20441
			[SerializeField]
			private float _sampleDistance = 0.25f;

			// Token: 0x04004FDA RID: 20442
			[SerializeField]
			private float _closestEdgeDistance = 1f;

			// Token: 0x04004FDB RID: 20443
			[SerializeField]
			private LayerMask _layerMask = 0;

			// Token: 0x04004FDC RID: 20444
			[SerializeField]
			private string _tagName = string.Empty;

			// Token: 0x04004FDD RID: 20445
			[SerializeField]
			private float _canEatEdgeDistance = 3f;

			// Token: 0x04004FDE RID: 20446
			[SerializeField]
			private int _agentAreaMask;
		}

		// Token: 0x02000B58 RID: 2904
		[Serializable]
		public struct SoundIDInfo
		{
			// Token: 0x17000FE1 RID: 4065
			// (get) Token: 0x060056A0 RID: 22176 RVA: 0x002592BE File Offset: 0x002576BE
			public int MechaStartup
			{
				[CompilerGenerated]
				get
				{
					return this._mechaStartup;
				}
			}

			// Token: 0x17000FE2 RID: 4066
			// (get) Token: 0x060056A1 RID: 22177 RVA: 0x002592C6 File Offset: 0x002576C6
			public int GetCat
			{
				[CompilerGenerated]
				get
				{
					return this._getCat;
				}
			}

			// Token: 0x17000FE3 RID: 4067
			// (get) Token: 0x060056A2 RID: 22178 RVA: 0x002592CE File Offset: 0x002576CE
			public int GetChicken
			{
				[CompilerGenerated]
				get
				{
					return this._getChicken;
				}
			}

			// Token: 0x04004FDF RID: 20447
			[SerializeField]
			private int _mechaStartup;

			// Token: 0x04004FE0 RID: 20448
			[SerializeField]
			private int _getCat;

			// Token: 0x04004FE1 RID: 20449
			[SerializeField]
			private int _getChicken;
		}

		// Token: 0x02000B59 RID: 2905
		[Serializable]
		public class CreateStartTimeInfoGroup
		{
			// Token: 0x17000FE4 RID: 4068
			// (get) Token: 0x060056A4 RID: 22180 RVA: 0x0025932D File Offset: 0x0025772D
			public Vector2 Cat
			{
				[CompilerGenerated]
				get
				{
					return this._cat;
				}
			}

			// Token: 0x17000FE5 RID: 4069
			// (get) Token: 0x060056A5 RID: 22181 RVA: 0x00259335 File Offset: 0x00257735
			public Vector2 Chicken
			{
				[CompilerGenerated]
				get
				{
					return this._chicken;
				}
			}

			// Token: 0x17000FE6 RID: 4070
			// (get) Token: 0x060056A6 RID: 22182 RVA: 0x0025933D File Offset: 0x0025773D
			public Vector2 CatAndChicken
			{
				[CompilerGenerated]
				get
				{
					return this._catAndChicken;
				}
			}

			// Token: 0x17000FE7 RID: 4071
			// (get) Token: 0x060056A7 RID: 22183 RVA: 0x00259345 File Offset: 0x00257745
			public Vector2 Mecha
			{
				[CompilerGenerated]
				get
				{
					return this._mecha;
				}
			}

			// Token: 0x17000FE8 RID: 4072
			// (get) Token: 0x060056A8 RID: 22184 RVA: 0x0025934D File Offset: 0x0025774D
			public Vector2 Frog
			{
				[CompilerGenerated]
				get
				{
					return this._frog;
				}
			}

			// Token: 0x17000FE9 RID: 4073
			// (get) Token: 0x060056A9 RID: 22185 RVA: 0x00259355 File Offset: 0x00257755
			public Vector2 BirdFlock
			{
				[CompilerGenerated]
				get
				{
					return this._birdFlock;
				}
			}

			// Token: 0x04004FE2 RID: 20450
			[SerializeField]
			[RangeZeroOver("ネコの生成開始時間")]
			private Vector2 _cat = Vector2.zero;

			// Token: 0x04004FE3 RID: 20451
			[SerializeField]
			[RangeZeroOver("ニワトリの生成開始時間")]
			private Vector2 _chicken = Vector2.zero;

			// Token: 0x04004FE4 RID: 20452
			[SerializeField]
			[RangeZeroOver("ネコ＆ニワトリの生成開始時間")]
			private Vector2 _catAndChicken = Vector2.zero;

			// Token: 0x04004FE5 RID: 20453
			[SerializeField]
			[RangeZeroOver("メカの生成開始時間")]
			private Vector2 _mecha = Vector2.zero;

			// Token: 0x04004FE6 RID: 20454
			[SerializeField]
			[RangeZeroOver("カエルの生成開始時間")]
			private Vector2 _frog = Vector2.zero;

			// Token: 0x04004FE7 RID: 20455
			[SerializeField]
			[RangeZeroOver("トリの群れの生成開始時間")]
			private Vector2 _birdFlock = Vector2.zero;
		}

		// Token: 0x02000B5A RID: 2906
		[Serializable]
		public class NavMeshAgentInfoGroup
		{
			// Token: 0x17000FEA RID: 4074
			// (get) Token: 0x060056AB RID: 22187 RVA: 0x00259374 File Offset: 0x00257774
			public int GroundAnimalStartPriority
			{
				[CompilerGenerated]
				get
				{
					return this._groundAnimalStartPriority;
				}
			}

			// Token: 0x17000FEB RID: 4075
			// (get) Token: 0x060056AC RID: 22188 RVA: 0x0025937C File Offset: 0x0025777C
			public int PriorityMargin
			{
				[CompilerGenerated]
				get
				{
					return this._priorityMargin;
				}
			}

			// Token: 0x04004FE8 RID: 20456
			[SerializeField]
			[Tooltip("地面系動物の開始優先度")]
			private int _groundAnimalStartPriority = 51;

			// Token: 0x04004FE9 RID: 20457
			[SerializeField]
			[Tooltip("優先度の間隔")]
			private int _priorityMargin = 5;
		}

		// Token: 0x02000B5B RID: 2907
		[Serializable]
		public class AllAnimalInfoGroup
		{
			// Token: 0x17000FEC RID: 4076
			// (get) Token: 0x060056AE RID: 22190 RVA: 0x002593AD File Offset: 0x002577AD
			public float ActionPointSearchedCoolTime
			{
				[CompilerGenerated]
				get
				{
					return this._actionPointSearchedCoolTime;
				}
			}

			// Token: 0x17000FED RID: 4077
			// (get) Token: 0x060056AF RID: 22191 RVA: 0x002593B5 File Offset: 0x002577B5
			public float ActionPointUsedCoolTime
			{
				[CompilerGenerated]
				get
				{
					return this._actionPointUsedCoolTime;
				}
			}

			// Token: 0x17000FEE RID: 4078
			// (get) Token: 0x060056B0 RID: 22192 RVA: 0x002593BD File Offset: 0x002577BD
			public float LovelyMaxParam
			{
				[CompilerGenerated]
				get
				{
					return this._lovelyMaxParam;
				}
			}

			// Token: 0x04004FEA RID: 20458
			[SerializeField]
			[Tooltip("行動ポイント検索後のクールタイム")]
			private float _actionPointSearchedCoolTime = 30f;

			// Token: 0x04004FEB RID: 20459
			[SerializeField]
			[Tooltip("行動ポイント使用後のクールタイム")]
			private float _actionPointUsedCoolTime = 180f;

			// Token: 0x04004FEC RID: 20460
			[SerializeField]
			[Tooltip("なつき度の最大値")]
			private float _lovelyMaxParam = 100f;
		}

		// Token: 0x02000B5C RID: 2908
		[Serializable]
		public class GroundAnimalInfoGroup
		{
			// Token: 0x17000FEF RID: 4079
			// (get) Token: 0x060056B2 RID: 22194 RVA: 0x002593EE File Offset: 0x002577EE
			public float FollowIdleSpace
			{
				[CompilerGenerated]
				get
				{
					return this._followIdleSpace;
				}
			}

			// Token: 0x17000FF0 RID: 4080
			// (get) Token: 0x060056B3 RID: 22195 RVA: 0x002593F6 File Offset: 0x002577F6
			public float FollowStopDistance
			{
				[CompilerGenerated]
				get
				{
					return this._followStopDistance;
				}
			}

			// Token: 0x17000FF1 RID: 4081
			// (get) Token: 0x060056B4 RID: 22196 RVA: 0x002593FE File Offset: 0x002577FE
			public float ForwardAnimationLerpValue
			{
				[CompilerGenerated]
				get
				{
					return this._forwardAnimationLerpValue;
				}
			}

			// Token: 0x04004FED RID: 20461
			[SerializeField]
			[Tooltip("停止距離からどれくらいの距離で追尾再開するか")]
			private float _followIdleSpace = 5f;

			// Token: 0x04004FEE RID: 20462
			[SerializeField]
			[Tooltip("追尾中のターゲットからどのくらい距離を離れて止まるか")]
			private float _followStopDistance = 20f;

			// Token: 0x04004FEF RID: 20463
			[SerializeField]
			[Tooltip("")]
			private float _forwardAnimationLerpValue = 0.5f;
		}

		// Token: 0x02000B5D RID: 2909
		[Serializable]
		public class GroundWildInfoGroup
		{
			// Token: 0x17000FF2 RID: 4082
			// (get) Token: 0x060056B6 RID: 22198 RVA: 0x0025942F File Offset: 0x0025782F
			public float LovelyTime
			{
				[CompilerGenerated]
				get
				{
					return this._lovelyTime;
				}
			}

			// Token: 0x17000FF3 RID: 4083
			// (get) Token: 0x060056B7 RID: 22199 RVA: 0x00259437 File Offset: 0x00257837
			public float BadMoodTime
			{
				[CompilerGenerated]
				get
				{
					return this._badMoodTime;
				}
			}

			// Token: 0x17000FF4 RID: 4084
			// (get) Token: 0x060056B8 RID: 22200 RVA: 0x0025943F File Offset: 0x0025783F
			public float DestroyTimeSeconds
			{
				[CompilerGenerated]
				get
				{
					return this._destroyTimeSeconds;
				}
			}

			// Token: 0x17000FF5 RID: 4085
			// (get) Token: 0x060056B9 RID: 22201 RVA: 0x00259447 File Offset: 0x00257847
			public float DepopCrossFadeDistance
			{
				[CompilerGenerated]
				get
				{
					return this._depopCrossFadeDistance;
				}
			}

			// Token: 0x17000FF6 RID: 4086
			// (get) Token: 0x060056BA RID: 22202 RVA: 0x0025944F File Offset: 0x0025784F
			public float DepopCrossFadeAngle
			{
				[CompilerGenerated]
				get
				{
					return this._depopCrossFadeAngle;
				}
			}

			// Token: 0x04004FF0 RID: 20464
			[SerializeField]
			[Tooltip("懐き状態の時間")]
			private float _lovelyTime = 120f;

			// Token: 0x04004FF1 RID: 20465
			[SerializeField]
			[Tooltip("不機嫌状態のリアルタイムの時間(秒)")]
			private float _badMoodTime = 600f;

			// Token: 0x04004FF2 RID: 20466
			[SerializeField]
			[Tooltip("地面系野生動物が消滅するのにかかる時間(秒)")]
			private float _destroyTimeSeconds = 1440f;

			// Token: 0x04004FF3 RID: 20467
			[SerializeField]
			[Min(0f)]
			[Tooltip("消滅するとき CrossFadeが再生される距離")]
			private float _depopCrossFadeDistance;

			// Token: 0x04004FF4 RID: 20468
			[SerializeField]
			[Min(0f)]
			[MaxValue(180.0)]
			[Tooltip("消滅するとき CrossFadeが再生される角度(0～180)")]
			private float _depopCrossFadeAngle;
		}

		// Token: 0x02000B5E RID: 2910
		[Serializable]
		public class PetCatInfoGroup
		{
			// Token: 0x17000FF7 RID: 4087
			// (get) Token: 0x060056BC RID: 22204 RVA: 0x00259475 File Offset: 0x00257875
			public float LovelyFollowParam
			{
				[CompilerGenerated]
				get
				{
					return this._lovelyFollowParam;
				}
			}

			// Token: 0x17000FF8 RID: 4088
			// (get) Token: 0x060056BD RID: 22205 RVA: 0x0025947D File Offset: 0x0025787D
			public float LonelinessFollowDistance
			{
				[CompilerGenerated]
				get
				{
					return this._lonelinessFollowDistance;
				}
			}

			// Token: 0x04004FF5 RID: 20469
			[SerializeField]
			[Tooltip("追尾状態になる必要なつき度")]
			private float _lovelyFollowParam = 10f;

			// Token: 0x04004FF6 RID: 20470
			[SerializeField]
			[Tooltip("寂しい時、最も懐いているキャラが優先される距離")]
			private float _lonelinessFollowDistance = 300f;
		}

		// Token: 0x02000B5F RID: 2911
		[Serializable]
		public class DesireInfoGroup
		{
			// Token: 0x17000FF9 RID: 4089
			// (get) Token: 0x060056BF RID: 22207 RVA: 0x00259498 File Offset: 0x00257898
			public float MotivationSecondTime
			{
				[CompilerGenerated]
				get
				{
					return this._motivationSecondTime;
				}
			}

			// Token: 0x04004FF7 RID: 20471
			[SerializeField]
			[Tooltip("欲求値がMaxに達した時の制限時間")]
			private float _motivationSecondTime = 300f;
		}

		// Token: 0x02000B60 RID: 2912
		[Serializable]
		public class WithActorInfoGroup
		{
			// Token: 0x17000FFA RID: 4090
			// (get) Token: 0x060056C1 RID: 22209 RVA: 0x002594BE File Offset: 0x002578BE
			public float ActionPointDistance
			{
				[CompilerGenerated]
				get
				{
					return this._actionPointDistance;
				}
			}

			// Token: 0x17000FFB RID: 4091
			// (get) Token: 0x060056C2 RID: 22210 RVA: 0x002594C6 File Offset: 0x002578C6
			public float GetPointDistance
			{
				[CompilerGenerated]
				get
				{
					return this._getPointDistance;
				}
			}

			// Token: 0x04004FF8 RID: 20472
			[SerializeField]
			[Tooltip("アニメーションを再生させるキャラクターからの距離")]
			private float _actionPointDistance = 11.5f;

			// Token: 0x04004FF9 RID: 20473
			[SerializeField]
			[Tooltip("野生動物を捕まえる時のキャラクターからの距離")]
			private float _getPointDistance = 12.5f;
		}
	}
}
