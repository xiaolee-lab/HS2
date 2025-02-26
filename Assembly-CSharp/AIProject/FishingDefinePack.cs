using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using AIProject.MiniGames.Fishing;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000F1F RID: 3871
	public class FishingDefinePack : SerializedScriptableObject
	{
		// Token: 0x1700193E RID: 6462
		// (get) Token: 0x06007F71 RID: 32625 RVA: 0x00363708 File Offset: 0x00361B08
		public FishingDefinePack.AssetBundleNameGroup AssetBundleNames
		{
			[CompilerGenerated]
			get
			{
				return this._assetBundleNames;
			}
		}

		// Token: 0x1700193F RID: 6463
		// (get) Token: 0x06007F72 RID: 32626 RVA: 0x00363710 File Offset: 0x00361B10
		public FishingDefinePack.SystemParamGroup SystemParam
		{
			[CompilerGenerated]
			get
			{
				return this._systemParam;
			}
		}

		// Token: 0x17001940 RID: 6464
		// (get) Token: 0x06007F73 RID: 32627 RVA: 0x00363718 File Offset: 0x00361B18
		public FishingDefinePack.IDGroup IDInfo
		{
			[CompilerGenerated]
			get
			{
				return this._idGroup;
			}
		}

		// Token: 0x17001941 RID: 6465
		// (get) Token: 0x06007F74 RID: 32628 RVA: 0x00363720 File Offset: 0x00361B20
		public FishingDefinePack.LureParamGroup LureParam
		{
			[CompilerGenerated]
			get
			{
				return this._lureParam;
			}
		}

		// Token: 0x17001942 RID: 6466
		// (get) Token: 0x06007F75 RID: 32629 RVA: 0x00363728 File Offset: 0x00361B28
		public FishingDefinePack.FishParamGroup FishParam
		{
			[CompilerGenerated]
			get
			{
				return this._fishParam;
			}
		}

		// Token: 0x17001943 RID: 6467
		// (get) Token: 0x06007F76 RID: 32630 RVA: 0x00363730 File Offset: 0x00361B30
		public FishingDefinePack.UIParamGroup UIParam
		{
			[CompilerGenerated]
			get
			{
				return this._uiParam;
			}
		}

		// Token: 0x17001944 RID: 6468
		// (get) Token: 0x06007F77 RID: 32631 RVA: 0x00363738 File Offset: 0x00361B38
		public FishingDefinePack.PlayerParamGroup PlayerParam
		{
			[CompilerGenerated]
			get
			{
				return this._playerParam;
			}
		}

		// Token: 0x17001945 RID: 6469
		// (get) Token: 0x06007F78 RID: 32632 RVA: 0x00363740 File Offset: 0x00361B40
		public Dictionary<int, float> GetEfcScaleTable
		{
			[CompilerGenerated]
			get
			{
				return this._getEfcScaleTable;
			}
		}

		// Token: 0x17001946 RID: 6470
		// (get) Token: 0x06007F79 RID: 32633 RVA: 0x00363748 File Offset: 0x00361B48
		public Dictionary<SEType, int> SETable
		{
			[CompilerGenerated]
			get
			{
				return this._seTable;
			}
		}

		// Token: 0x04006698 RID: 26264
		[SerializeField]
		private FishingDefinePack.AssetBundleNameGroup _assetBundleNames;

		// Token: 0x04006699 RID: 26265
		[SerializeField]
		private FishingDefinePack.SystemParamGroup _systemParam;

		// Token: 0x0400669A RID: 26266
		[SerializeField]
		private FishingDefinePack.IDGroup _idGroup;

		// Token: 0x0400669B RID: 26267
		[SerializeField]
		private FishingDefinePack.LureParamGroup _lureParam;

		// Token: 0x0400669C RID: 26268
		[SerializeField]
		private FishingDefinePack.FishParamGroup _fishParam;

		// Token: 0x0400669D RID: 26269
		[SerializeField]
		private FishingDefinePack.UIParamGroup _uiParam;

		// Token: 0x0400669E RID: 26270
		[SerializeField]
		private FishingDefinePack.PlayerParamGroup _playerParam;

		// Token: 0x0400669F RID: 26271
		[SerializeField]
		private Dictionary<int, float> _getEfcScaleTable = Enumerable.Range(0, 3).ToDictionary((int x) => x, (int x) => 1f);

		// Token: 0x040066A0 RID: 26272
		[SerializeField]
		private Dictionary<SEType, int> _seTable = Enum.GetValues(typeof(SEType)).Cast<SEType>().ToDictionary((SEType x) => x, (SEType x) => -1);

		// Token: 0x02000F20 RID: 3872
		[Serializable]
		public class AssetBundleNameGroup
		{
			// Token: 0x17001947 RID: 6471
			// (get) Token: 0x06007F7F RID: 32639 RVA: 0x00363773 File Offset: 0x00361B73
			public string FishingInfoListBundleDirectory
			{
				[CompilerGenerated]
				get
				{
					return this._fishingInfoListBundleDirectory;
				}
			}

			// Token: 0x040066A5 RID: 26277
			[SerializeField]
			private string _fishingInfoListBundleDirectory = string.Empty;
		}

		// Token: 0x02000F21 RID: 3873
		[Serializable]
		public class SystemParamGroup
		{
			// Token: 0x17001948 RID: 6472
			// (get) Token: 0x06007F81 RID: 32641 RVA: 0x0036388E File Offset: 0x00361C8E
			public LayerMask FishingLayerMask
			{
				[CompilerGenerated]
				get
				{
					return this._fishingLayerMask;
				}
			}

			// Token: 0x17001949 RID: 6473
			// (get) Token: 0x06007F82 RID: 32642 RVA: 0x00363896 File Offset: 0x00361C96
			public string FishingMeshTagName
			{
				[CompilerGenerated]
				get
				{
					return this._fishingMeshTagName;
				}
			}

			// Token: 0x1700194A RID: 6474
			// (get) Token: 0x06007F83 RID: 32643 RVA: 0x0036389E File Offset: 0x00361C9E
			public LayerMask LureWaterBoxLayerMask
			{
				[CompilerGenerated]
				get
				{
					return this._lureWaterBoxLayerMask;
				}
			}

			// Token: 0x1700194B RID: 6475
			// (get) Token: 0x06007F84 RID: 32644 RVA: 0x003638A6 File Offset: 0x00361CA6
			public string LureWaterBoxTagName
			{
				[CompilerGenerated]
				get
				{
					return this._lureWaterBoxTagName;
				}
			}

			// Token: 0x1700194C RID: 6476
			// (get) Token: 0x06007F85 RID: 32645 RVA: 0x003638AE File Offset: 0x00361CAE
			public LayerMask LuxWaterLayerMask
			{
				[CompilerGenerated]
				get
				{
					return this._luxWaterLayerMask;
				}
			}

			// Token: 0x1700194D RID: 6477
			// (get) Token: 0x06007F86 RID: 32646 RVA: 0x003638B6 File Offset: 0x00361CB6
			public int FishMaxNum
			{
				[CompilerGenerated]
				get
				{
					return this._fishMaxNum;
				}
			}

			// Token: 0x1700194E RID: 6478
			// (get) Token: 0x06007F87 RID: 32647 RVA: 0x003638BE File Offset: 0x00361CBE
			public float SoundRoodDistance
			{
				[CompilerGenerated]
				get
				{
					return this._soundRoodDistance;
				}
			}

			// Token: 0x1700194F RID: 6479
			// (get) Token: 0x06007F88 RID: 32648 RVA: 0x003638C6 File Offset: 0x00361CC6
			public float MoveAreaRadius
			{
				[CompilerGenerated]
				get
				{
					return this._moveAreaRadius;
				}
			}

			// Token: 0x17001950 RID: 6480
			// (get) Token: 0x06007F89 RID: 32649 RVA: 0x003638CE File Offset: 0x00361CCE
			public Vector3 MoveAreaOffsetPosition
			{
				[CompilerGenerated]
				get
				{
					return this._moveAreaOffsetPosition;
				}
			}

			// Token: 0x17001951 RID: 6481
			// (get) Token: 0x06007F8A RID: 32650 RVA: 0x003638D6 File Offset: 0x00361CD6
			public int MaxLevel
			{
				[CompilerGenerated]
				get
				{
					return this._maxLevel;
				}
			}

			// Token: 0x17001952 RID: 6482
			// (get) Token: 0x06007F8B RID: 32651 RVA: 0x003638DE File Offset: 0x00361CDE
			public float DefaultDamage
			{
				[CompilerGenerated]
				get
				{
					return this._defaultDamage;
				}
			}

			// Token: 0x17001953 RID: 6483
			// (get) Token: 0x06007F8C RID: 32652 RVA: 0x003638E6 File Offset: 0x00361CE6
			public float MaxDamage
			{
				[CompilerGenerated]
				get
				{
					return this._maxDamage;
				}
			}

			// Token: 0x17001954 RID: 6484
			// (get) Token: 0x06007F8D RID: 32653 RVA: 0x003638EE File Offset: 0x00361CEE
			public float NormalDamageAngle
			{
				[CompilerGenerated]
				get
				{
					return this._normalDamageAngle;
				}
			}

			// Token: 0x17001955 RID: 6485
			// (get) Token: 0x06007F8E RID: 32654 RVA: 0x003638F6 File Offset: 0x00361CF6
			public float CriticalDamageAngle
			{
				[CompilerGenerated]
				get
				{
					return this._criticalDamageAngle;
				}
			}

			// Token: 0x17001956 RID: 6486
			// (get) Token: 0x06007F8F RID: 32655 RVA: 0x003638FE File Offset: 0x00361CFE
			public float AngryDamageScale
			{
				[CompilerGenerated]
				get
				{
					return this._angryDamageScale;
				}
			}

			// Token: 0x17001957 RID: 6487
			// (get) Token: 0x06007F90 RID: 32656 RVA: 0x00363906 File Offset: 0x00361D06
			public float AngryCountDownScale
			{
				[CompilerGenerated]
				get
				{
					return this._angryCountDownScale;
				}
			}

			// Token: 0x17001958 RID: 6488
			// (get) Token: 0x06007F91 RID: 32657 RVA: 0x0036390E File Offset: 0x00361D0E
			public float FishingTimeLimit
			{
				[CompilerGenerated]
				get
				{
					return this._fishingTimeLimit;
				}
			}

			// Token: 0x17001959 RID: 6489
			// (get) Token: 0x06007F92 RID: 32658 RVA: 0x00363916 File Offset: 0x00361D16
			public int NextExperience
			{
				[CompilerGenerated]
				get
				{
					return this._nextExperience;
				}
			}

			// Token: 0x1700195A RID: 6490
			// (get) Token: 0x06007F93 RID: 32659 RVA: 0x0036391E File Offset: 0x00361D1E
			public float ArrowMaxPower
			{
				[CompilerGenerated]
				get
				{
					return this._arrowMaxPower;
				}
			}

			// Token: 0x1700195B RID: 6491
			// (get) Token: 0x06007F94 RID: 32660 RVA: 0x00363926 File Offset: 0x00361D26
			public float ArrowAddAngle
			{
				[CompilerGenerated]
				get
				{
					return this._arrowAddAngle;
				}
			}

			// Token: 0x1700195C RID: 6492
			// (get) Token: 0x06007F95 RID: 32661 RVA: 0x0036392E File Offset: 0x00361D2E
			public float DeviceArrowPowerScale
			{
				[CompilerGenerated]
				get
				{
					return this._deviceArrowPowerScale;
				}
			}

			// Token: 0x1700195D RID: 6493
			// (get) Token: 0x06007F96 RID: 32662 RVA: 0x00363936 File Offset: 0x00361D36
			public float MouseArrowPowerScale
			{
				[CompilerGenerated]
				get
				{
					return this._mouseArrowPowerScale;
				}
			}

			// Token: 0x1700195E RID: 6494
			// (get) Token: 0x06007F97 RID: 32663 RVA: 0x0036393E File Offset: 0x00361D3E
			public float DistanceToCircle
			{
				[CompilerGenerated]
				get
				{
					return this._distanceToCircle;
				}
			}

			// Token: 0x040066A6 RID: 26278
			[SerializeField]
			private LayerMask _fishingLayerMask = default(LayerMask);

			// Token: 0x040066A7 RID: 26279
			[SerializeField]
			private string _fishingMeshTagName = "Water";

			// Token: 0x040066A8 RID: 26280
			[SerializeField]
			private LayerMask _lureWaterBoxLayerMask = default(LayerMask);

			// Token: 0x040066A9 RID: 26281
			[SerializeField]
			private string _lureWaterBoxTagName = "Water";

			// Token: 0x040066AA RID: 26282
			[SerializeField]
			private LayerMask _luxWaterLayerMask = default(LayerMask);

			// Token: 0x040066AB RID: 26283
			[SerializeField]
			private int _fishMaxNum = 3;

			// Token: 0x040066AC RID: 26284
			[SerializeField]
			private float _soundRoodDistance = 5f;

			// Token: 0x040066AD RID: 26285
			[SerializeField]
			private float _moveAreaRadius = 17.5f;

			// Token: 0x040066AE RID: 26286
			[SerializeField]
			private Vector3 _moveAreaOffsetPosition = Vector3.zero;

			// Token: 0x040066AF RID: 26287
			[SerializeField]
			private int _maxLevel = 100;

			// Token: 0x040066B0 RID: 26288
			[SerializeField]
			private float _defaultDamage = 10f;

			// Token: 0x040066B1 RID: 26289
			[SerializeField]
			private float _maxDamage = 10f;

			// Token: 0x040066B2 RID: 26290
			[SerializeField]
			private float _normalDamageAngle = 90f;

			// Token: 0x040066B3 RID: 26291
			[SerializeField]
			private float _criticalDamageAngle = 30f;

			// Token: 0x040066B4 RID: 26292
			[SerializeField]
			private float _angryDamageScale = 0.1f;

			// Token: 0x040066B5 RID: 26293
			[SerializeField]
			private float _angryCountDownScale = 2f;

			// Token: 0x040066B6 RID: 26294
			[SerializeField]
			private float _fishingTimeLimit = 30f;

			// Token: 0x040066B7 RID: 26295
			[SerializeField]
			private int _nextExperience = 100;

			// Token: 0x040066B8 RID: 26296
			[SerializeField]
			private float _arrowMaxPower = 1f;

			// Token: 0x040066B9 RID: 26297
			[SerializeField]
			private float _arrowAddAngle = 360f;

			// Token: 0x040066BA RID: 26298
			[SerializeField]
			private float _deviceArrowPowerScale = 2.5f;

			// Token: 0x040066BB RID: 26299
			[SerializeField]
			private float _mouseArrowPowerScale = 0.3f;

			// Token: 0x040066BC RID: 26300
			[SerializeField]
			private float _distanceToCircle = 5f;
		}

		// Token: 0x02000F22 RID: 3874
		[Serializable]
		public class IDGroup
		{
			// Token: 0x1700195F RID: 6495
			// (get) Token: 0x06007F99 RID: 32665 RVA: 0x003639B0 File Offset: 0x00361DB0
			public int LureEventItemID
			{
				[CompilerGenerated]
				get
				{
					return this._lureEventItemID;
				}
			}

			// Token: 0x17001960 RID: 6496
			// (get) Token: 0x06007F9A RID: 32666 RVA: 0x003639B8 File Offset: 0x00361DB8
			public int FishItemCategoryID
			{
				[CompilerGenerated]
				get
				{
					return this._fishItemCategoryID;
				}
			}

			// Token: 0x17001961 RID: 6497
			// (get) Token: 0x06007F9B RID: 32667 RVA: 0x003639C0 File Offset: 0x00361DC0
			public FishingDefinePack.ItemIDPair FishingRod
			{
				[CompilerGenerated]
				get
				{
					return this._fishingRod;
				}
			}

			// Token: 0x17001962 RID: 6498
			// (get) Token: 0x06007F9C RID: 32668 RVA: 0x003639C8 File Offset: 0x00361DC8
			public FishingDefinePack.ItemIDPair BrokenFishingRod
			{
				[CompilerGenerated]
				get
				{
					return this._brokenFishingRod;
				}
			}

			// Token: 0x17001963 RID: 6499
			// (get) Token: 0x06007F9D RID: 32669 RVA: 0x003639D0 File Offset: 0x00361DD0
			public FishingDefinePack.ItemIDPair Kotsubuuo
			{
				[CompilerGenerated]
				get
				{
					return this._kotsubuuo;
				}
			}

			// Token: 0x17001964 RID: 6500
			// (get) Token: 0x06007F9E RID: 32670 RVA: 0x003639D8 File Offset: 0x00361DD8
			public List<FishingDefinePack.ItemIDPair> FishList
			{
				[CompilerGenerated]
				get
				{
					return this._fishList;
				}
			}

			// Token: 0x17001965 RID: 6501
			// (get) Token: 0x06007F9F RID: 32671 RVA: 0x003639E0 File Offset: 0x00361DE0
			public FishingDefinePack.ItemIDPair GrilledFish
			{
				[CompilerGenerated]
				get
				{
					return this._grilledFish;
				}
			}

			// Token: 0x040066BD RID: 26301
			[SerializeField]
			private int _lureEventItemID = 3;

			// Token: 0x040066BE RID: 26302
			[SerializeField]
			private int _fishItemCategoryID = 2;

			// Token: 0x040066BF RID: 26303
			[SerializeField]
			private FishingDefinePack.ItemIDPair _fishingRod = default(FishingDefinePack.ItemIDPair);

			// Token: 0x040066C0 RID: 26304
			[SerializeField]
			private FishingDefinePack.ItemIDPair _brokenFishingRod = default(FishingDefinePack.ItemIDPair);

			// Token: 0x040066C1 RID: 26305
			[SerializeField]
			private FishingDefinePack.ItemIDPair _kotsubuuo = default(FishingDefinePack.ItemIDPair);

			// Token: 0x040066C2 RID: 26306
			[SerializeField]
			private List<FishingDefinePack.ItemIDPair> _fishList = new List<FishingDefinePack.ItemIDPair>();

			// Token: 0x040066C3 RID: 26307
			[SerializeField]
			private FishingDefinePack.ItemIDPair _grilledFish = default(FishingDefinePack.ItemIDPair);
		}

		// Token: 0x02000F23 RID: 3875
		[Serializable]
		public class LureParamGroup
		{
			// Token: 0x17001966 RID: 6502
			// (get) Token: 0x06007FA1 RID: 32673 RVA: 0x00363A74 File Offset: 0x00361E74
			public Vector3 DropOffsetPosition
			{
				[CompilerGenerated]
				get
				{
					return this._dropOffsetPosition;
				}
			}

			// Token: 0x17001967 RID: 6503
			// (get) Token: 0x06007FA2 RID: 32674 RVA: 0x00363A7C File Offset: 0x00361E7C
			public float DropOffsetHeight
			{
				[CompilerGenerated]
				get
				{
					return this._dropOffsetHeight;
				}
			}

			// Token: 0x17001968 RID: 6504
			// (get) Token: 0x06007FA3 RID: 32675 RVA: 0x00363A84 File Offset: 0x00361E84
			public float ThrowTime
			{
				[CompilerGenerated]
				get
				{
					return this._throwTime;
				}
			}

			// Token: 0x17001969 RID: 6505
			// (get) Token: 0x06007FA4 RID: 32676 RVA: 0x00363A8C File Offset: 0x00361E8C
			public float ReturnTime
			{
				[CompilerGenerated]
				get
				{
					return this._returnTime;
				}
			}

			// Token: 0x1700196A RID: 6506
			// (get) Token: 0x06007FA5 RID: 32677 RVA: 0x00363A94 File Offset: 0x00361E94
			public float FloatMoveMaxSpeed
			{
				[CompilerGenerated]
				get
				{
					return this._floatMoveMaxSpeed;
				}
			}

			// Token: 0x1700196B RID: 6507
			// (get) Token: 0x06007FA6 RID: 32678 RVA: 0x00363A9C File Offset: 0x00361E9C
			public float MouseAxisScale
			{
				[CompilerGenerated]
				get
				{
					return this._mouseAxisScale;
				}
			}

			// Token: 0x1700196C RID: 6508
			// (get) Token: 0x06007FA7 RID: 32679 RVA: 0x00363AA4 File Offset: 0x00361EA4
			public float WaterDensity
			{
				[CompilerGenerated]
				get
				{
					return this._waterDensity;
				}
			}

			// Token: 0x1700196D RID: 6509
			// (get) Token: 0x06007FA8 RID: 32680 RVA: 0x00363AAC File Offset: 0x00361EAC
			public float Density
			{
				[CompilerGenerated]
				get
				{
					return this._density;
				}
			}

			// Token: 0x1700196E RID: 6510
			// (get) Token: 0x06007FA9 RID: 32681 RVA: 0x00363AB4 File Offset: 0x00361EB4
			public float NormalizedVoxelSize
			{
				[CompilerGenerated]
				get
				{
					return this._normalizedVoxelSize;
				}
			}

			// Token: 0x1700196F RID: 6511
			// (get) Token: 0x06007FAA RID: 32682 RVA: 0x00363ABC File Offset: 0x00361EBC
			public float DragInWater
			{
				[CompilerGenerated]
				get
				{
					return this._dragInWater;
				}
			}

			// Token: 0x17001970 RID: 6512
			// (get) Token: 0x06007FAB RID: 32683 RVA: 0x00363AC4 File Offset: 0x00361EC4
			public float AngularDragInWater
			{
				[CompilerGenerated]
				get
				{
					return this._angularDragInWater;
				}
			}

			// Token: 0x040066C4 RID: 26308
			[SerializeField]
			private Vector3 _dropOffsetPosition = Vector3.zero;

			// Token: 0x040066C5 RID: 26309
			[SerializeField]
			private float _dropOffsetHeight = -0.5f;

			// Token: 0x040066C6 RID: 26310
			[SerializeField]
			private float _throwTime = 0.75f;

			// Token: 0x040066C7 RID: 26311
			[SerializeField]
			private float _returnTime = 0.75f;

			// Token: 0x040066C8 RID: 26312
			[SerializeField]
			private float _floatMoveMaxSpeed = 12.5f;

			// Token: 0x040066C9 RID: 26313
			[SerializeField]
			private float _mouseAxisScale = 1.25f;

			// Token: 0x040066CA RID: 26314
			[SerializeField]
			private float _waterDensity = 1f;

			// Token: 0x040066CB RID: 26315
			[SerializeField]
			private float _density = 0.75f;

			// Token: 0x040066CC RID: 26316
			[SerializeField]
			private float _normalizedVoxelSize = 1f;

			// Token: 0x040066CD RID: 26317
			[SerializeField]
			private float _dragInWater = 1f;

			// Token: 0x040066CE RID: 26318
			[SerializeField]
			private float _angularDragInWater = 1f;
		}

		// Token: 0x02000F24 RID: 3876
		[Serializable]
		public class FishParamGroup
		{
			// Token: 0x17001971 RID: 6513
			// (get) Token: 0x06007FAD RID: 32685 RVA: 0x00363B9A File Offset: 0x00361F9A
			public float CreateOffsetHeight
			{
				[CompilerGenerated]
				get
				{
					return this._createOffsetHeight;
				}
			}

			// Token: 0x17001972 RID: 6514
			// (get) Token: 0x06007FAE RID: 32686 RVA: 0x00363BA2 File Offset: 0x00361FA2
			public float SwimSpeed
			{
				[CompilerGenerated]
				get
				{
					return this._swimSpeed;
				}
			}

			// Token: 0x17001973 RID: 6515
			// (get) Token: 0x06007FAF RID: 32687 RVA: 0x00363BAA File Offset: 0x00361FAA
			public float FollowSpeed
			{
				[CompilerGenerated]
				get
				{
					return this._followSpeed;
				}
			}

			// Token: 0x17001974 RID: 6516
			// (get) Token: 0x06007FB0 RID: 32688 RVA: 0x00363BB2 File Offset: 0x00361FB2
			public float EscapeSpeed
			{
				[CompilerGenerated]
				get
				{
					return this._escapeSpeed;
				}
			}

			// Token: 0x17001975 RID: 6517
			// (get) Token: 0x06007FB1 RID: 32689 RVA: 0x00363BBA File Offset: 0x00361FBA
			public float EscapeFadeTime
			{
				[CompilerGenerated]
				get
				{
					return this._escapeFadeTime;
				}
			}

			// Token: 0x17001976 RID: 6518
			// (get) Token: 0x06007FB2 RID: 32690 RVA: 0x00363BC2 File Offset: 0x00361FC2
			public float SwimDistance
			{
				[CompilerGenerated]
				get
				{
					return this._swimDistance;
				}
			}

			// Token: 0x17001977 RID: 6519
			// (get) Token: 0x06007FB3 RID: 32691 RVA: 0x00363BCA File Offset: 0x00361FCA
			public float SwimStopDistance
			{
				[CompilerGenerated]
				get
				{
					return this._swimStopDistance;
				}
			}

			// Token: 0x17001978 RID: 6520
			// (get) Token: 0x06007FB4 RID: 32692 RVA: 0x00363BD2 File Offset: 0x00361FD2
			public float SwimAddAngle
			{
				[CompilerGenerated]
				get
				{
					return this._swimAddAngle;
				}
			}

			// Token: 0x17001979 RID: 6521
			// (get) Token: 0x06007FB5 RID: 32693 RVA: 0x00363BDA File Offset: 0x00361FDA
			public float FollowAddAngle
			{
				[CompilerGenerated]
				get
				{
					return this._followAddAngle;
				}
			}

			// Token: 0x1700197A RID: 6522
			// (get) Token: 0x06007FB6 RID: 32694 RVA: 0x00363BE2 File Offset: 0x00361FE2
			public float FindAngle
			{
				[CompilerGenerated]
				get
				{
					return this._findAngle;
				}
			}

			// Token: 0x1700197B RID: 6523
			// (get) Token: 0x06007FB7 RID: 32695 RVA: 0x00363BEA File Offset: 0x00361FEA
			public float FindDistance
			{
				[CompilerGenerated]
				get
				{
					return this._findDistance;
				}
			}

			// Token: 0x1700197C RID: 6524
			// (get) Token: 0x06007FB8 RID: 32696 RVA: 0x00363BF2 File Offset: 0x00361FF2
			public float HitDistance
			{
				[CompilerGenerated]
				get
				{
					return this._hitDistance;
				}
			}

			// Token: 0x1700197D RID: 6525
			// (get) Token: 0x06007FB9 RID: 32697 RVA: 0x00363BFA File Offset: 0x00361FFA
			public float ReFindTime
			{
				[CompilerGenerated]
				get
				{
					return this._reFindTime;
				}
			}

			// Token: 0x1700197E RID: 6526
			// (get) Token: 0x06007FBA RID: 32698 RVA: 0x00363C02 File Offset: 0x00362002
			public float DestroyMinTime
			{
				[CompilerGenerated]
				get
				{
					return this._destroyMinTime;
				}
			}

			// Token: 0x1700197F RID: 6527
			// (get) Token: 0x06007FBB RID: 32699 RVA: 0x00363C0A File Offset: 0x0036200A
			public float DestroyMaxTime
			{
				[CompilerGenerated]
				get
				{
					return this._destroyMaxTime;
				}
			}

			// Token: 0x17001980 RID: 6528
			// (get) Token: 0x06007FBC RID: 32700 RVA: 0x00363C12 File Offset: 0x00362012
			public string AnimLoopName
			{
				[CompilerGenerated]
				get
				{
					return this._animLoopName;
				}
			}

			// Token: 0x17001981 RID: 6529
			// (get) Token: 0x06007FBD RID: 32701 RVA: 0x00363C1A File Offset: 0x0036201A
			public string AnimHitName
			{
				[CompilerGenerated]
				get
				{
					return this._animHitName;
				}
			}

			// Token: 0x17001982 RID: 6530
			// (get) Token: 0x06007FBE RID: 32702 RVA: 0x00363C22 File Offset: 0x00362022
			public string AnimAngryName
			{
				[CompilerGenerated]
				get
				{
					return this._animAngryName;
				}
			}

			// Token: 0x17001983 RID: 6531
			// (get) Token: 0x06007FBF RID: 32703 RVA: 0x00363C2A File Offset: 0x0036202A
			public FishingDefinePack.FishHitParamGroup HitParam
			{
				[CompilerGenerated]
				get
				{
					return this._hitParam;
				}
			}

			// Token: 0x040066CF RID: 26319
			[SerializeField]
			private float _createOffsetHeight;

			// Token: 0x040066D0 RID: 26320
			[SerializeField]
			private float _swimSpeed = 3f;

			// Token: 0x040066D1 RID: 26321
			[SerializeField]
			private float _followSpeed = 3.6000001f;

			// Token: 0x040066D2 RID: 26322
			[SerializeField]
			private float _escapeSpeed = 18f;

			// Token: 0x040066D3 RID: 26323
			[SerializeField]
			private float _escapeFadeTime = 1f;

			// Token: 0x040066D4 RID: 26324
			[SerializeField]
			private float _swimDistance = 5f;

			// Token: 0x040066D5 RID: 26325
			[SerializeField]
			private float _swimStopDistance = 2f;

			// Token: 0x040066D6 RID: 26326
			[SerializeField]
			private float _swimAddAngle = 80f;

			// Token: 0x040066D7 RID: 26327
			[SerializeField]
			private float _followAddAngle = 80f;

			// Token: 0x040066D8 RID: 26328
			[SerializeField]
			private float _findAngle = 90f;

			// Token: 0x040066D9 RID: 26329
			[SerializeField]
			private float _findDistance = 10f;

			// Token: 0x040066DA RID: 26330
			[SerializeField]
			private float _hitDistance = 2f;

			// Token: 0x040066DB RID: 26331
			[SerializeField]
			private float _reFindTime = 0.5f;

			// Token: 0x040066DC RID: 26332
			[SerializeField]
			private float _destroyMinTime = 30f;

			// Token: 0x040066DD RID: 26333
			[SerializeField]
			private float _destroyMaxTime = 60f;

			// Token: 0x040066DE RID: 26334
			[SerializeField]
			private string _animLoopName = "shadowfish_loop";

			// Token: 0x040066DF RID: 26335
			[SerializeField]
			private string _animHitName = "shadowfish_hit";

			// Token: 0x040066E0 RID: 26336
			[SerializeField]
			private string _animAngryName = "shadowfish_angry";

			// Token: 0x040066E1 RID: 26337
			[SerializeField]
			private FishingDefinePack.FishHitParamGroup _hitParam;
		}

		// Token: 0x02000F25 RID: 3877
		[Serializable]
		public class FishHitParamGroup
		{
			// Token: 0x17001984 RID: 6532
			// (get) Token: 0x06007FC1 RID: 32705 RVA: 0x00363D2E File Offset: 0x0036212E
			public Vector3 MoveAreaOffsetPosition
			{
				[CompilerGenerated]
				get
				{
					return this._moveAreaOffsetPosition;
				}
			}

			// Token: 0x17001985 RID: 6533
			// (get) Token: 0x06007FC2 RID: 32706 RVA: 0x00363D36 File Offset: 0x00362136
			public float MoveAreaAngle
			{
				[CompilerGenerated]
				get
				{
					return this._moveAreaAngle;
				}
			}

			// Token: 0x17001986 RID: 6534
			// (get) Token: 0x06007FC3 RID: 32707 RVA: 0x00363D3E File Offset: 0x0036213E
			public float MoveAreaMinRadius
			{
				[CompilerGenerated]
				get
				{
					return this._moveAreaMinRadius;
				}
			}

			// Token: 0x17001987 RID: 6535
			// (get) Token: 0x06007FC4 RID: 32708 RVA: 0x00363D46 File Offset: 0x00362146
			public float MoveAreaMaxRadius
			{
				[CompilerGenerated]
				get
				{
					return this._moveAreaMaxRadius;
				}
			}

			// Token: 0x17001988 RID: 6536
			// (get) Token: 0x06007FC5 RID: 32709 RVA: 0x00363D4E File Offset: 0x0036214E
			public float AngryNextMinTime
			{
				[CompilerGenerated]
				get
				{
					return this._angryNextMinTime;
				}
			}

			// Token: 0x17001989 RID: 6537
			// (get) Token: 0x06007FC6 RID: 32710 RVA: 0x00363D56 File Offset: 0x00362156
			public float AngryNextMaxTime
			{
				[CompilerGenerated]
				get
				{
					return this._angryNextMaxTime;
				}
			}

			// Token: 0x1700198A RID: 6538
			// (get) Token: 0x06007FC7 RID: 32711 RVA: 0x00363D5E File Offset: 0x0036215E
			public float AngryMinTimeLimit
			{
				[CompilerGenerated]
				get
				{
					return this._angryMinTimeLimit;
				}
			}

			// Token: 0x1700198B RID: 6539
			// (get) Token: 0x06007FC8 RID: 32712 RVA: 0x00363D66 File Offset: 0x00362166
			public float AngryMaxTimeLimit
			{
				[CompilerGenerated]
				get
				{
					return this._angryMaxTimeLimit;
				}
			}

			// Token: 0x1700198C RID: 6540
			// (get) Token: 0x06007FC9 RID: 32713 RVA: 0x00363D6E File Offset: 0x0036216E
			public float MoveSpeed
			{
				[CompilerGenerated]
				get
				{
					return this._moveSpeed;
				}
			}

			// Token: 0x1700198D RID: 6541
			// (get) Token: 0x06007FCA RID: 32714 RVA: 0x00363D76 File Offset: 0x00362176
			public float FirstAddAngle
			{
				[CompilerGenerated]
				get
				{
					return this._firstAddAngle;
				}
			}

			// Token: 0x1700198E RID: 6542
			// (get) Token: 0x06007FCB RID: 32715 RVA: 0x00363D7E File Offset: 0x0036217E
			public float AddAngle
			{
				[CompilerGenerated]
				get
				{
					return this._addAngle;
				}
			}

			// Token: 0x1700198F RID: 6543
			// (get) Token: 0x06007FCC RID: 32716 RVA: 0x00363D86 File Offset: 0x00362186
			public float NextMinAngle
			{
				[CompilerGenerated]
				get
				{
					return this._nextMinAngle;
				}
			}

			// Token: 0x17001990 RID: 6544
			// (get) Token: 0x06007FCD RID: 32717 RVA: 0x00363D8E File Offset: 0x0036218E
			public float NextMaxAngle
			{
				[CompilerGenerated]
				get
				{
					return this._nextMaxAngle;
				}
			}

			// Token: 0x17001991 RID: 6545
			// (get) Token: 0x06007FCE RID: 32718 RVA: 0x00363D96 File Offset: 0x00362196
			public float AngleMinTimeLimit
			{
				[CompilerGenerated]
				get
				{
					return this._angleMinTimeLimit;
				}
			}

			// Token: 0x17001992 RID: 6546
			// (get) Token: 0x06007FCF RID: 32719 RVA: 0x00363D9E File Offset: 0x0036219E
			public float AngleMaxTimeLimit
			{
				[CompilerGenerated]
				get
				{
					return this._angleMaxTimeLimit;
				}
			}

			// Token: 0x17001993 RID: 6547
			// (get) Token: 0x06007FD0 RID: 32720 RVA: 0x00363DA6 File Offset: 0x003621A6
			public float NextMinRadius
			{
				[CompilerGenerated]
				get
				{
					return this._nextMinRadius;
				}
			}

			// Token: 0x17001994 RID: 6548
			// (get) Token: 0x06007FD1 RID: 32721 RVA: 0x00363DAE File Offset: 0x003621AE
			public float NextMaxRadius
			{
				[CompilerGenerated]
				get
				{
					return this._nextMaxRadius;
				}
			}

			// Token: 0x17001995 RID: 6549
			// (get) Token: 0x06007FD2 RID: 32722 RVA: 0x00363DB6 File Offset: 0x003621B6
			public float RadiusMinSpeed
			{
				[CompilerGenerated]
				get
				{
					return this._radiusMinSpeed;
				}
			}

			// Token: 0x17001996 RID: 6550
			// (get) Token: 0x06007FD3 RID: 32723 RVA: 0x00363DBE File Offset: 0x003621BE
			public float RadiusMaxSpeed
			{
				[CompilerGenerated]
				get
				{
					return this._radiusMaxSpeed;
				}
			}

			// Token: 0x17001997 RID: 6551
			// (get) Token: 0x06007FD4 RID: 32724 RVA: 0x00363DC6 File Offset: 0x003621C6
			public bool ChangeRadiusEasing
			{
				[CompilerGenerated]
				get
				{
					return this._changeRadiusEasing;
				}
			}

			// Token: 0x17001998 RID: 6552
			// (get) Token: 0x06007FD5 RID: 32725 RVA: 0x00363DCE File Offset: 0x003621CE
			public float RadiusMinWaitTimeLimit
			{
				[CompilerGenerated]
				get
				{
					return this._radiusMinWaitTimeLimit;
				}
			}

			// Token: 0x17001999 RID: 6553
			// (get) Token: 0x06007FD6 RID: 32726 RVA: 0x00363DD6 File Offset: 0x003621D6
			public float RadiusMaxWaitTimeLimit
			{
				[CompilerGenerated]
				get
				{
					return this._radiusMaxWaitTimeLimit;
				}
			}

			// Token: 0x040066E2 RID: 26338
			[SerializeField]
			private Vector3 _moveAreaOffsetPosition = Vector3.zero;

			// Token: 0x040066E3 RID: 26339
			[SerializeField]
			private float _moveAreaAngle = 65f;

			// Token: 0x040066E4 RID: 26340
			[SerializeField]
			private float _moveAreaMinRadius = 20f;

			// Token: 0x040066E5 RID: 26341
			[SerializeField]
			private float _moveAreaMaxRadius = 45f;

			// Token: 0x040066E6 RID: 26342
			[SerializeField]
			private float _angryNextMinTime = 10f;

			// Token: 0x040066E7 RID: 26343
			[SerializeField]
			private float _angryNextMaxTime = 20f;

			// Token: 0x040066E8 RID: 26344
			[SerializeField]
			private float _angryMinTimeLimit = 5f;

			// Token: 0x040066E9 RID: 26345
			[SerializeField]
			private float _angryMaxTimeLimit = 6f;

			// Token: 0x040066EA RID: 26346
			[SerializeField]
			private float _moveSpeed = 25f;

			// Token: 0x040066EB RID: 26347
			[SerializeField]
			private float _firstAddAngle = 200f;

			// Token: 0x040066EC RID: 26348
			[SerializeField]
			private float _addAngle = 120f;

			// Token: 0x040066ED RID: 26349
			[SerializeField]
			private float _nextMinAngle = 50f;

			// Token: 0x040066EE RID: 26350
			[SerializeField]
			private float _nextMaxAngle = 170f;

			// Token: 0x040066EF RID: 26351
			[SerializeField]
			private float _angleMinTimeLimit = 1f;

			// Token: 0x040066F0 RID: 26352
			[SerializeField]
			private float _angleMaxTimeLimit = 3f;

			// Token: 0x040066F1 RID: 26353
			[SerializeField]
			private float _nextMinRadius = 3f;

			// Token: 0x040066F2 RID: 26354
			[SerializeField]
			private float _nextMaxRadius = 10f;

			// Token: 0x040066F3 RID: 26355
			[SerializeField]
			private float _radiusMinSpeed = 10f;

			// Token: 0x040066F4 RID: 26356
			[SerializeField]
			private float _radiusMaxSpeed = 25f;

			// Token: 0x040066F5 RID: 26357
			[SerializeField]
			private bool _changeRadiusEasing;

			// Token: 0x040066F6 RID: 26358
			[SerializeField]
			private float _radiusMinWaitTimeLimit = 2f;

			// Token: 0x040066F7 RID: 26359
			[SerializeField]
			private float _radiusMaxWaitTimeLimit = 4f;
		}

		// Token: 0x02000F26 RID: 3878
		[Serializable]
		public class UIParamGroup
		{
			// Token: 0x1700199A RID: 6554
			// (get) Token: 0x06007FD8 RID: 32728 RVA: 0x00363E36 File Offset: 0x00362236
			public float ArrowAnimWaitTimeLimit
			{
				[CompilerGenerated]
				get
				{
					return this._arrowAnimWaitTimeLimit;
				}
			}

			// Token: 0x1700199B RID: 6555
			// (get) Token: 0x06007FD9 RID: 32729 RVA: 0x00363E3E File Offset: 0x0036223E
			public float ArrowAnimFadeInTimeLimit
			{
				[CompilerGenerated]
				get
				{
					return this._arrowAnimFadeInTimeLimit;
				}
			}

			// Token: 0x1700199C RID: 6556
			// (get) Token: 0x06007FDA RID: 32730 RVA: 0x00363E46 File Offset: 0x00362246
			public float ArrowAnimFadeOutTimeLimit
			{
				[CompilerGenerated]
				get
				{
					return this._arrowAnimFadeOutTimeLimit;
				}
			}

			// Token: 0x1700199D RID: 6557
			// (get) Token: 0x06007FDB RID: 32731 RVA: 0x00363E4E File Offset: 0x0036224E
			public float ArrowAnimCancelFadeTimeLimit
			{
				[CompilerGenerated]
				get
				{
					return this._arrowAnimCancelFadeTimeLimit;
				}
			}

			// Token: 0x1700199E RID: 6558
			// (get) Token: 0x06007FDC RID: 32732 RVA: 0x00363E56 File Offset: 0x00362256
			public string[] RarelityLabelList
			{
				[CompilerGenerated]
				get
				{
					return this._rarelityLabelList;
				}
			}

			// Token: 0x1700199F RID: 6559
			// (get) Token: 0x06007FDD RID: 32733 RVA: 0x00363E5E File Offset: 0x0036225E
			public float ExperienceAddParSecond
			{
				[CompilerGenerated]
				get
				{
					return this._experienceAddParSecond;
				}
			}

			// Token: 0x040066F8 RID: 26360
			[SerializeField]
			private float _arrowAnimWaitTimeLimit = 1f;

			// Token: 0x040066F9 RID: 26361
			[SerializeField]
			private float _arrowAnimFadeInTimeLimit = 0.1f;

			// Token: 0x040066FA RID: 26362
			[SerializeField]
			private float _arrowAnimFadeOutTimeLimit = 0.2f;

			// Token: 0x040066FB RID: 26363
			[SerializeField]
			private float _arrowAnimCancelFadeTimeLimit = 0.1f;

			// Token: 0x040066FC RID: 26364
			[SerializeField]
			private string[] _rarelityLabelList = new string[0];

			// Token: 0x040066FD RID: 26365
			[SerializeField]
			private float _experienceAddParSecond = 33f;
		}

		// Token: 0x02000F27 RID: 3879
		[Serializable]
		public class PlayerParamGroup
		{
			// Token: 0x170019A0 RID: 6560
			// (get) Token: 0x06007FDF RID: 32735 RVA: 0x00363EDE File Offset: 0x003622DE
			public float WaitHitMoveHorizontalScale
			{
				[CompilerGenerated]
				get
				{
					return this._waitHitMoveHorizontalScale;
				}
			}

			// Token: 0x170019A1 RID: 6561
			// (get) Token: 0x06007FE0 RID: 32736 RVA: 0x00363EE6 File Offset: 0x003622E6
			public float WaitHitReturnHorizontalScale
			{
				[CompilerGenerated]
				get
				{
					return this._waitHitReturnHorizontalScale;
				}
			}

			// Token: 0x170019A2 RID: 6562
			// (get) Token: 0x06007FE1 RID: 32737 RVA: 0x00363EEE File Offset: 0x003622EE
			public float FishingHorizontalScale
			{
				[CompilerGenerated]
				get
				{
					return this._fishingHorizontalScale;
				}
			}

			// Token: 0x170019A3 RID: 6563
			// (get) Token: 0x06007FE2 RID: 32738 RVA: 0x00363EF6 File Offset: 0x003622F6
			public float RodHitWaitAngleSpeed
			{
				[CompilerGenerated]
				get
				{
					return this._rodHitWaitAngleSpeed;
				}
			}

			// Token: 0x170019A4 RID: 6564
			// (get) Token: 0x06007FE3 RID: 32739 RVA: 0x00363EFE File Offset: 0x003622FE
			public float RodAngleScale
			{
				[CompilerGenerated]
				get
				{
					return this._rodAngleScale;
				}
			}

			// Token: 0x170019A5 RID: 6565
			// (get) Token: 0x06007FE4 RID: 32740 RVA: 0x00363F06 File Offset: 0x00362306
			public string FishingGamePrefabName
			{
				[CompilerGenerated]
				get
				{
					return this._fishingGamePrefabName;
				}
			}

			// Token: 0x170019A6 RID: 6566
			// (get) Token: 0x06007FE5 RID: 32741 RVA: 0x00363F0E File Offset: 0x0036230E
			public string AnimStandbyName
			{
				[CompilerGenerated]
				get
				{
					return this._animStandbyName;
				}
			}

			// Token: 0x170019A7 RID: 6567
			// (get) Token: 0x06007FE6 RID: 32742 RVA: 0x00363F16 File Offset: 0x00362316
			public string PlayerAnimParamName
			{
				[CompilerGenerated]
				get
				{
					return this._playerAnimParamName;
				}
			}

			// Token: 0x170019A8 RID: 6568
			// (get) Token: 0x06007FE7 RID: 32743 RVA: 0x00363F1E File Offset: 0x0036231E
			public string RodAnimParamName
			{
				[CompilerGenerated]
				get
				{
					return this._rodAnimParamName;
				}
			}

			// Token: 0x040066FE RID: 26366
			[SerializeField]
			private float _waitHitMoveHorizontalScale = 3f;

			// Token: 0x040066FF RID: 26367
			[SerializeField]
			private float _waitHitReturnHorizontalScale = 3f;

			// Token: 0x04006700 RID: 26368
			[SerializeField]
			private float _fishingHorizontalScale = 4f;

			// Token: 0x04006701 RID: 26369
			[SerializeField]
			private float _rodHitWaitAngleSpeed = 5f;

			// Token: 0x04006702 RID: 26370
			[SerializeField]
			private float _rodAngleScale = 1.5f;

			// Token: 0x04006703 RID: 26371
			[SerializeField]
			private string _fishingGamePrefabName = string.Empty;

			// Token: 0x04006704 RID: 26372
			[SerializeField]
			private string _animStandbyName = string.Empty;

			// Token: 0x04006705 RID: 26373
			[SerializeField]
			private string _playerAnimParamName = string.Empty;

			// Token: 0x04006706 RID: 26374
			[SerializeField]
			private string _rodAnimParamName = string.Empty;
		}

		// Token: 0x02000F28 RID: 3880
		[Serializable]
		public struct ItemIDPair
		{
			// Token: 0x06007FE8 RID: 32744 RVA: 0x00363F26 File Offset: 0x00362326
			public ItemIDPair(int categoryID, int itemID)
			{
				this.CategoryID = categoryID;
				this.ItemID = itemID;
			}

			// Token: 0x04006707 RID: 26375
			public int CategoryID;

			// Token: 0x04006708 RID: 26376
			public int ItemID;
		}
	}
}
