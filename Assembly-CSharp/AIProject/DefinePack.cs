using System;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000F60 RID: 3936
	public class DefinePack : SerializedScriptableObject
	{
		// Token: 0x17001B1E RID: 6942
		// (get) Token: 0x060082B4 RID: 33460 RVA: 0x003701D0 File Offset: 0x0036E5D0
		public DefinePack.AssetBundleManifestsDefine ABManifests
		{
			[CompilerGenerated]
			get
			{
				return this._abManifests;
			}
		}

		// Token: 0x17001B1F RID: 6943
		// (get) Token: 0x060082B5 RID: 33461 RVA: 0x003701D8 File Offset: 0x0036E5D8
		public DefinePack.AssetBundleDirectoriesDefine ABDirectories
		{
			[CompilerGenerated]
			get
			{
				return this._abDirectories;
			}
		}

		// Token: 0x17001B20 RID: 6944
		// (get) Token: 0x060082B6 RID: 33462 RVA: 0x003701E0 File Offset: 0x0036E5E0
		public DefinePack.AssetBundlePathsDefine ABPaths
		{
			[CompilerGenerated]
			get
			{
				return this._abPaths;
			}
		}

		// Token: 0x17001B21 RID: 6945
		// (get) Token: 0x060082B7 RID: 33463 RVA: 0x003701E8 File Offset: 0x0036E5E8
		public DefinePack.SceneNameGroup SceneNames
		{
			[CompilerGenerated]
			get
			{
				return this._sceneNames;
			}
		}

		// Token: 0x17001B22 RID: 6946
		// (get) Token: 0x060082B8 RID: 33464 RVA: 0x003701F0 File Offset: 0x0036E5F0
		public DefinePack.AnimatorStateNameGroup AnimatorState
		{
			[CompilerGenerated]
			get
			{
				return this._animatorState;
			}
		}

		// Token: 0x17001B23 RID: 6947
		// (get) Token: 0x060082B9 RID: 33465 RVA: 0x003701F8 File Offset: 0x0036E5F8
		public DefinePack.BasicAnimatorParameter AnimatorParameter
		{
			[CompilerGenerated]
			get
			{
				return this._animatorParameter;
			}
		}

		// Token: 0x17001B24 RID: 6948
		// (get) Token: 0x060082BA RID: 33466 RVA: 0x00370200 File Offset: 0x0036E600
		public DefinePack.MapGroup MapDefines
		{
			[CompilerGenerated]
			get
			{
				return this._mapDefines;
			}
		}

		// Token: 0x17001B25 RID: 6949
		// (get) Token: 0x060082BB RID: 33467 RVA: 0x00370208 File Offset: 0x0036E608
		public DefinePack.ItemBoxCapacity ItemBoxCapacityDefines
		{
			[CompilerGenerated]
			get
			{
				return this._itemBoxCapacityDefines;
			}
		}

		// Token: 0x17001B26 RID: 6950
		// (get) Token: 0x060082BC RID: 33468 RVA: 0x00370210 File Offset: 0x0036E610
		public DefinePack.MinimapUI MinimapUIDefine
		{
			[CompilerGenerated]
			get
			{
				return this._minimapUIDefine;
			}
		}

		// Token: 0x17001B27 RID: 6951
		// (get) Token: 0x060082BD RID: 33469 RVA: 0x00370218 File Offset: 0x0036E618
		public DefinePack.MapLes MapLesDefine
		{
			[CompilerGenerated]
			get
			{
				return this._MapLes;
			}
		}

		// Token: 0x17001B28 RID: 6952
		// (get) Token: 0x060082BE RID: 33470 RVA: 0x00370220 File Offset: 0x0036E620
		public DefinePack.RecyclingSetting Recycling
		{
			[CompilerGenerated]
			get
			{
				return this._recycling;
			}
		}

		// Token: 0x04006927 RID: 26919
		[SerializeField]
		private DefinePack.AssetBundleManifestsDefine _abManifests;

		// Token: 0x04006928 RID: 26920
		[SerializeField]
		private DefinePack.AssetBundleDirectoriesDefine _abDirectories;

		// Token: 0x04006929 RID: 26921
		[SerializeField]
		private DefinePack.AssetBundlePathsDefine _abPaths;

		// Token: 0x0400692A RID: 26922
		[SerializeField]
		[Header("Scene Name")]
		private DefinePack.SceneNameGroup _sceneNames;

		// Token: 0x0400692B RID: 26923
		[SerializeField]
		[Header("Animator State")]
		private DefinePack.AnimatorStateNameGroup _animatorState;

		// Token: 0x0400692C RID: 26924
		[SerializeField]
		[Header("Animator Parameter")]
		private DefinePack.BasicAnimatorParameter _animatorParameter;

		// Token: 0x0400692D RID: 26925
		[SerializeField]
		[Header("Map")]
		private DefinePack.MapGroup _mapDefines;

		// Token: 0x0400692E RID: 26926
		[SerializeField]
		[Header("ItemBoxCapacity")]
		private DefinePack.ItemBoxCapacity _itemBoxCapacityDefines;

		// Token: 0x0400692F RID: 26927
		[SerializeField]
		[Header("MinimapUIDefine")]
		private DefinePack.MinimapUI _minimapUIDefine;

		// Token: 0x04006930 RID: 26928
		[SerializeField]
		[Header("MapLesbianDefine")]
		private DefinePack.MapLes _MapLes;

		// Token: 0x04006931 RID: 26929
		[SerializeField]
		[Header("RecyclingSetting")]
		private DefinePack.RecyclingSetting _recycling;

		// Token: 0x02000F61 RID: 3937
		[Serializable]
		public class AssetBundleManifestsDefine
		{
			// Token: 0x17001B29 RID: 6953
			// (get) Token: 0x060082C0 RID: 33472 RVA: 0x00370267 File Offset: 0x0036E667
			public string Default
			{
				[CompilerGenerated]
				get
				{
					return this._default;
				}
			}

			// Token: 0x17001B2A RID: 6954
			// (get) Token: 0x060082C1 RID: 33473 RVA: 0x0037026F File Offset: 0x0036E66F
			public string Add05
			{
				[CompilerGenerated]
				get
				{
					return this._add05;
				}
			}

			// Token: 0x17001B2B RID: 6955
			// (get) Token: 0x060082C2 RID: 33474 RVA: 0x00370277 File Offset: 0x0036E677
			public string Add07
			{
				[CompilerGenerated]
				get
				{
					return this._add07;
				}
			}

			// Token: 0x17001B2C RID: 6956
			// (get) Token: 0x060082C3 RID: 33475 RVA: 0x0037027F File Offset: 0x0036E67F
			public string Add12
			{
				[CompilerGenerated]
				get
				{
					return this._add12;
				}
			}

			// Token: 0x17001B2D RID: 6957
			// (get) Token: 0x060082C4 RID: 33476 RVA: 0x00370287 File Offset: 0x0036E687
			public string Add19
			{
				[CompilerGenerated]
				get
				{
					return this._add19;
				}
			}

			// Token: 0x04006932 RID: 26930
			[SerializeField]
			private string _default = string.Empty;

			// Token: 0x04006933 RID: 26931
			[SerializeField]
			private string _add05 = string.Empty;

			// Token: 0x04006934 RID: 26932
			[SerializeField]
			private string _add07 = string.Empty;

			// Token: 0x04006935 RID: 26933
			[SerializeField]
			private string _add12 = string.Empty;

			// Token: 0x04006936 RID: 26934
			[SerializeField]
			private string _add19 = string.Empty;
		}

		// Token: 0x02000F62 RID: 3938
		[Serializable]
		public class AssetBundleDirectoriesDefine
		{
			// Token: 0x17001B2E RID: 6958
			// (get) Token: 0x060082C6 RID: 33478 RVA: 0x003705DC File Offset: 0x0036E9DC
			public string InputIconList
			{
				[CompilerGenerated]
				get
				{
					return this._inputIconList;
				}
			}

			// Token: 0x17001B2F RID: 6959
			// (get) Token: 0x060082C7 RID: 33479 RVA: 0x003705E4 File Offset: 0x0036E9E4
			public string ActionIconList
			{
				[CompilerGenerated]
				get
				{
					return this._actionIconList;
				}
			}

			// Token: 0x17001B30 RID: 6960
			// (get) Token: 0x060082C8 RID: 33480 RVA: 0x003705EC File Offset: 0x0036E9EC
			public string ActorIconList
			{
				[CompilerGenerated]
				get
				{
					return this._actorIconList;
				}
			}

			// Token: 0x17001B31 RID: 6961
			// (get) Token: 0x060082C9 RID: 33481 RVA: 0x003705F4 File Offset: 0x0036E9F4
			public string WeatherIconList
			{
				[CompilerGenerated]
				get
				{
					return this._weatherIconList;
				}
			}

			// Token: 0x17001B32 RID: 6962
			// (get) Token: 0x060082CA RID: 33482 RVA: 0x003705FC File Offset: 0x0036E9FC
			public string EquipItemIconList
			{
				[CompilerGenerated]
				get
				{
					return this._equipItemIconList;
				}
			}

			// Token: 0x17001B33 RID: 6963
			// (get) Token: 0x060082CB RID: 33483 RVA: 0x00370604 File Offset: 0x0036EA04
			public string StatusIconList
			{
				[CompilerGenerated]
				get
				{
					return this._statusIconList;
				}
			}

			// Token: 0x17001B34 RID: 6964
			// (get) Token: 0x060082CC RID: 33484 RVA: 0x0037060C File Offset: 0x0036EA0C
			public string SickIconList
			{
				[CompilerGenerated]
				get
				{
					return this._sickIconList;
				}
			}

			// Token: 0x17001B35 RID: 6965
			// (get) Token: 0x060082CD RID: 33485 RVA: 0x00370614 File Offset: 0x0036EA14
			public string LoadingSpriteList
			{
				[CompilerGenerated]
				get
				{
					return this._loadingSpriteList;
				}
			}

			// Token: 0x17001B36 RID: 6966
			// (get) Token: 0x060082CE RID: 33486 RVA: 0x0037061C File Offset: 0x0036EA1C
			public string SearchEquipItemObjList
			{
				[CompilerGenerated]
				get
				{
					return this._searchEquipItemObjList;
				}
			}

			// Token: 0x17001B37 RID: 6967
			// (get) Token: 0x060082CF RID: 33487 RVA: 0x00370624 File Offset: 0x0036EA24
			public string CommonEquipItemObjList
			{
				[CompilerGenerated]
				get
				{
					return this._commonEquipItemObjList;
				}
			}

			// Token: 0x17001B38 RID: 6968
			// (get) Token: 0x060082D0 RID: 33488 RVA: 0x0037062C File Offset: 0x0036EA2C
			public string AccessoryItem
			{
				[CompilerGenerated]
				get
				{
					return this._accessoryItem;
				}
			}

			// Token: 0x17001B39 RID: 6969
			// (get) Token: 0x060082D1 RID: 33489 RVA: 0x00370634 File Offset: 0x0036EA34
			public string ActionPointPrefabList
			{
				[CompilerGenerated]
				get
				{
					return this._actionPointPrefabList;
				}
			}

			// Token: 0x17001B3A RID: 6970
			// (get) Token: 0x060082D2 RID: 33490 RVA: 0x0037063C File Offset: 0x0036EA3C
			public string BasePointPrefabList
			{
				[CompilerGenerated]
				get
				{
					return this._basePointPrefabList;
				}
			}

			// Token: 0x17001B3B RID: 6971
			// (get) Token: 0x060082D3 RID: 33491 RVA: 0x00370644 File Offset: 0x0036EA44
			public string DevicePointPrefabList
			{
				[CompilerGenerated]
				get
				{
					return this._devicePointPrefabList;
				}
			}

			// Token: 0x17001B3C RID: 6972
			// (get) Token: 0x060082D4 RID: 33492 RVA: 0x0037064C File Offset: 0x0036EA4C
			public string FarmPointPrefabList
			{
				[CompilerGenerated]
				get
				{
					return this._farmPointPrefabList;
				}
			}

			// Token: 0x17001B3D RID: 6973
			// (get) Token: 0x060082D5 RID: 33493 RVA: 0x00370654 File Offset: 0x0036EA54
			public string ShipPointPrefabList
			{
				[CompilerGenerated]
				get
				{
					return this._shipPointPrefabList;
				}
			}

			// Token: 0x17001B3E RID: 6974
			// (get) Token: 0x060082D6 RID: 33494 RVA: 0x0037065C File Offset: 0x0036EA5C
			public string PlayerActionPointList
			{
				[CompilerGenerated]
				get
				{
					return this._playerActionPointList;
				}
			}

			// Token: 0x17001B3F RID: 6975
			// (get) Token: 0x060082D7 RID: 33495 RVA: 0x00370664 File Offset: 0x0036EA64
			public string AgentActionPointList
			{
				[CompilerGenerated]
				get
				{
					return this._agentActionPointList;
				}
			}

			// Token: 0x17001B40 RID: 6976
			// (get) Token: 0x060082D8 RID: 33496 RVA: 0x0037066C File Offset: 0x0036EA6C
			public string MerchantActionPointList
			{
				[CompilerGenerated]
				get
				{
					return this._merchantActionPointList;
				}
			}

			// Token: 0x17001B41 RID: 6977
			// (get) Token: 0x060082D9 RID: 33497 RVA: 0x00370674 File Offset: 0x0036EA74
			public string LightSwitchPointList
			{
				[CompilerGenerated]
				get
				{
					return this._lightSwitchPointList;
				}
			}

			// Token: 0x17001B42 RID: 6978
			// (get) Token: 0x060082DA RID: 33498 RVA: 0x0037067C File Offset: 0x0036EA7C
			public string EventPointList
			{
				[CompilerGenerated]
				get
				{
					return this._eventPointList;
				}
			}

			// Token: 0x17001B43 RID: 6979
			// (get) Token: 0x060082DB RID: 33499 RVA: 0x00370684 File Offset: 0x0036EA84
			public string StoryPointList
			{
				[CompilerGenerated]
				get
				{
					return this._storyPointList;
				}
			}

			// Token: 0x17001B44 RID: 6980
			// (get) Token: 0x060082DC RID: 33500 RVA: 0x0037068C File Offset: 0x0036EA8C
			public string MapList
			{
				[CompilerGenerated]
				get
				{
					return this._mapList;
				}
			}

			// Token: 0x17001B45 RID: 6981
			// (get) Token: 0x060082DD RID: 33501 RVA: 0x00370694 File Offset: 0x0036EA94
			public string ChunkList
			{
				[CompilerGenerated]
				get
				{
					return this._chunkList;
				}
			}

			// Token: 0x17001B46 RID: 6982
			// (get) Token: 0x060082DE RID: 33502 RVA: 0x0037069C File Offset: 0x0036EA9C
			public string WaypointList
			{
				[CompilerGenerated]
				get
				{
					return this._waypointList;
				}
			}

			// Token: 0x17001B47 RID: 6983
			// (get) Token: 0x060082DF RID: 33503 RVA: 0x003706A4 File Offset: 0x0036EAA4
			public string AreaGroupList
			{
				[CompilerGenerated]
				get
				{
					return this._AreaGroupList;
				}
			}

			// Token: 0x17001B48 RID: 6984
			// (get) Token: 0x060082E0 RID: 33504 RVA: 0x003706AC File Offset: 0x0036EAAC
			public string PlantItemList
			{
				[CompilerGenerated]
				get
				{
					return this._plantItemList;
				}
			}

			// Token: 0x17001B49 RID: 6985
			// (get) Token: 0x060082E1 RID: 33505 RVA: 0x003706B4 File Offset: 0x0036EAB4
			public string IvyFilterList
			{
				[CompilerGenerated]
				get
				{
					return this._ivyFilterList;
				}
			}

			// Token: 0x17001B4A RID: 6986
			// (get) Token: 0x060082E2 RID: 33506 RVA: 0x003706BC File Offset: 0x0036EABC
			public string EventItemList
			{
				[CompilerGenerated]
				get
				{
					return this._eventItemList;
				}
			}

			// Token: 0x17001B4B RID: 6987
			// (get) Token: 0x060082E3 RID: 33507 RVA: 0x003706C4 File Offset: 0x0036EAC4
			public string EventParticleList
			{
				[CompilerGenerated]
				get
				{
					return this._eventParticleList;
				}
			}

			// Token: 0x17001B4C RID: 6988
			// (get) Token: 0x060082E4 RID: 33508 RVA: 0x003706CC File Offset: 0x0036EACC
			public string PopupInfoList
			{
				[CompilerGenerated]
				get
				{
					return this._popupInfoList;
				}
			}

			// Token: 0x17001B4D RID: 6989
			// (get) Token: 0x060082E5 RID: 33509 RVA: 0x003706D4 File Offset: 0x0036EAD4
			public string AreaOpenStateList
			{
				[CompilerGenerated]
				get
				{
					return this._areaOpenStateList;
				}
			}

			// Token: 0x17001B4E RID: 6990
			// (get) Token: 0x060082E6 RID: 33510 RVA: 0x003706DC File Offset: 0x0036EADC
			public string TimeRelationInfoList
			{
				[CompilerGenerated]
				get
				{
					return this._timeRelationInfoList;
				}
			}

			// Token: 0x17001B4F RID: 6991
			// (get) Token: 0x060082E7 RID: 33511 RVA: 0x003706E4 File Offset: 0x0036EAE4
			public string FoodInfo
			{
				[CompilerGenerated]
				get
				{
					return this._foodInfo;
				}
			}

			// Token: 0x17001B50 RID: 6992
			// (get) Token: 0x060082E8 RID: 33512 RVA: 0x003706EC File Offset: 0x0036EAEC
			public string DrinkInfo
			{
				[CompilerGenerated]
				get
				{
					return this._drinkInfo;
				}
			}

			// Token: 0x17001B51 RID: 6993
			// (get) Token: 0x060082E9 RID: 33513 RVA: 0x003706F4 File Offset: 0x0036EAF4
			public string ItemList
			{
				[CompilerGenerated]
				get
				{
					return this._itemList;
				}
			}

			// Token: 0x17001B52 RID: 6994
			// (get) Token: 0x060082EA RID: 33514 RVA: 0x003706FC File Offset: 0x0036EAFC
			public string GatheringTable
			{
				[CompilerGenerated]
				get
				{
					return this._gatheringTable;
				}
			}

			// Token: 0x17001B53 RID: 6995
			// (get) Token: 0x060082EB RID: 33515 RVA: 0x00370704 File Offset: 0x0036EB04
			public string FrogItemTable
			{
				[CompilerGenerated]
				get
				{
					return this._frogItemTable;
				}
			}

			// Token: 0x17001B54 RID: 6996
			// (get) Token: 0x060082EC RID: 33516 RVA: 0x0037070C File Offset: 0x0036EB0C
			public string ComCamera
			{
				[CompilerGenerated]
				get
				{
					return this._comCamera;
				}
			}

			// Token: 0x17001B55 RID: 6997
			// (get) Token: 0x060082ED RID: 33517 RVA: 0x00370714 File Offset: 0x0036EB14
			public string MapActionVoiceInfoList
			{
				[CompilerGenerated]
				get
				{
					return this._mapActionVoiceInfoList;
				}
			}

			// Token: 0x17001B56 RID: 6998
			// (get) Token: 0x060082EE RID: 33518 RVA: 0x0037071C File Offset: 0x0036EB1C
			public string DefaultFemaleFootStepSEInfoList
			{
				[CompilerGenerated]
				get
				{
					return this._defaultFemaleFootStepSEInfoList;
				}
			}

			// Token: 0x17001B57 RID: 6999
			// (get) Token: 0x060082EF RID: 33519 RVA: 0x00370724 File Offset: 0x0036EB24
			public string DefaultMaleFootStepSEInfoList
			{
				[CompilerGenerated]
				get
				{
					return this._defaultMaleFootStepSEInfoList;
				}
			}

			// Token: 0x17001B58 RID: 7000
			// (get) Token: 0x060082F0 RID: 33520 RVA: 0x0037072C File Offset: 0x0036EB2C
			public string MapBGMInfoList
			{
				[CompilerGenerated]
				get
				{
					return this._mapBGMInfoList;
				}
			}

			// Token: 0x17001B59 RID: 7001
			// (get) Token: 0x060082F1 RID: 33521 RVA: 0x00370734 File Offset: 0x0036EB34
			public string EnviroSEInfoList
			{
				[CompilerGenerated]
				get
				{
					return this._enviroSEInfoList;
				}
			}

			// Token: 0x17001B5A RID: 7002
			// (get) Token: 0x060082F2 RID: 33522 RVA: 0x0037073C File Offset: 0x0036EB3C
			public string ActorAnimatorList
			{
				[CompilerGenerated]
				get
				{
					return this._actorAnimatorList;
				}
			}

			// Token: 0x17001B5B RID: 7003
			// (get) Token: 0x060082F3 RID: 33523 RVA: 0x00370744 File Offset: 0x0036EB44
			public string PlayerFemaleAnimeInfo
			{
				[CompilerGenerated]
				get
				{
					return this._playerFemaleAnimeInfo;
				}
			}

			// Token: 0x17001B5C RID: 7004
			// (get) Token: 0x060082F4 RID: 33524 RVA: 0x0037074C File Offset: 0x0036EB4C
			public string PlayerMaleAnimeInfo
			{
				[CompilerGenerated]
				get
				{
					return this._playerMaleAnimeInfo;
				}
			}

			// Token: 0x17001B5D RID: 7005
			// (get) Token: 0x060082F5 RID: 33525 RVA: 0x00370754 File Offset: 0x0036EB54
			public string AgentPhase
			{
				[CompilerGenerated]
				get
				{
					return this._agentPhase;
				}
			}

			// Token: 0x17001B5E RID: 7006
			// (get) Token: 0x060082F6 RID: 33526 RVA: 0x0037075C File Offset: 0x0036EB5C
			public string AgentPersonalityMotivation
			{
				[CompilerGenerated]
				get
				{
					return this._agentPersonalityMotivation;
				}
			}

			// Token: 0x17001B5F RID: 7007
			// (get) Token: 0x060082F7 RID: 33527 RVA: 0x00370764 File Offset: 0x0036EB64
			public string LifestyleTable
			{
				[CompilerGenerated]
				get
				{
					return this._lifestyleTable;
				}
			}

			// Token: 0x17001B60 RID: 7008
			// (get) Token: 0x060082F8 RID: 33528 RVA: 0x0037076C File Offset: 0x0036EB6C
			public string FlavorPickSkillTable
			{
				[CompilerGenerated]
				get
				{
					return this._flavorPickSkillTable;
				}
			}

			// Token: 0x17001B61 RID: 7009
			// (get) Token: 0x060082F9 RID: 33529 RVA: 0x00370774 File Offset: 0x0036EB74
			public string FlavorPickHSkillTable
			{
				[CompilerGenerated]
				get
				{
					return this._flavorPickHSkillTable;
				}
			}

			// Token: 0x17001B62 RID: 7010
			// (get) Token: 0x060082FA RID: 33530 RVA: 0x0037077C File Offset: 0x0036EB7C
			public string AgentDesire
			{
				[CompilerGenerated]
				get
				{
					return this._agentDesire;
				}
			}

			// Token: 0x17001B63 RID: 7011
			// (get) Token: 0x060082FB RID: 33531 RVA: 0x00370784 File Offset: 0x0036EB84
			public string AgentCommunicationFlags
			{
				[CompilerGenerated]
				get
				{
					return this._agentCommunicationFlags;
				}
			}

			// Token: 0x17001B64 RID: 7012
			// (get) Token: 0x060082FC RID: 33532 RVA: 0x0037078C File Offset: 0x0036EB8C
			public string AgentAnimeInfo
			{
				[CompilerGenerated]
				get
				{
					return this._agentAnimeInfo;
				}
			}

			// Token: 0x17001B65 RID: 7013
			// (get) Token: 0x060082FD RID: 33533 RVA: 0x00370794 File Offset: 0x0036EB94
			public string AgentLocomotionBreath
			{
				[CompilerGenerated]
				get
				{
					return this._agentLocomotionBreath;
				}
			}

			// Token: 0x17001B66 RID: 7014
			// (get) Token: 0x060082FE RID: 33534 RVA: 0x0037079C File Offset: 0x0036EB9C
			public string GravurePoseInfo
			{
				[CompilerGenerated]
				get
				{
					return this._gravurePoseInfo;
				}
			}

			// Token: 0x17001B67 RID: 7015
			// (get) Token: 0x060082FF RID: 33535 RVA: 0x003707A4 File Offset: 0x0036EBA4
			public string SurpriseItemInfo
			{
				[CompilerGenerated]
				get
				{
					return this._surpriseItemInfo;
				}
			}

			// Token: 0x17001B68 RID: 7016
			// (get) Token: 0x06008300 RID: 33536 RVA: 0x003707AC File Offset: 0x0036EBAC
			public string AgentActionResult
			{
				[CompilerGenerated]
				get
				{
					return this._agentActionResult;
				}
			}

			// Token: 0x17001B69 RID: 7017
			// (get) Token: 0x06008301 RID: 33537 RVA: 0x003707B4 File Offset: 0x0036EBB4
			public string AgentSituationResult
			{
				[CompilerGenerated]
				get
				{
					return this._agentSituationResult;
				}
			}

			// Token: 0x17001B6A RID: 7018
			// (get) Token: 0x06008302 RID: 33538 RVA: 0x003707BC File Offset: 0x0036EBBC
			public string ActorVanishList
			{
				[CompilerGenerated]
				get
				{
					return this._actorVanishList;
				}
			}

			// Token: 0x17001B6B RID: 7019
			// (get) Token: 0x06008303 RID: 33539 RVA: 0x003707C4 File Offset: 0x0036EBC4
			public string BehaviorTree
			{
				[CompilerGenerated]
				get
				{
					return this._behaviorTree;
				}
			}

			// Token: 0x17001B6C RID: 7020
			// (get) Token: 0x06008304 RID: 33540 RVA: 0x003707CC File Offset: 0x0036EBCC
			public string TutorialBehaviorTree
			{
				[CompilerGenerated]
				get
				{
					return this._tutrialBehaviorTree;
				}
			}

			// Token: 0x17001B6D RID: 7021
			// (get) Token: 0x06008305 RID: 33541 RVA: 0x003707D4 File Offset: 0x0036EBD4
			public string MerchantAnimeInfo
			{
				[CompilerGenerated]
				get
				{
					return this._merchantAnimeInfo;
				}
			}

			// Token: 0x17001B6E RID: 7022
			// (get) Token: 0x06008306 RID: 33542 RVA: 0x003707DC File Offset: 0x0036EBDC
			public string MerchantBehaviorTree
			{
				[CompilerGenerated]
				get
				{
					return this._merchantBehaviorTree;
				}
			}

			// Token: 0x17001B6F RID: 7023
			// (get) Token: 0x06008307 RID: 33543 RVA: 0x003707E4 File Offset: 0x0036EBE4
			public string MapIKList
			{
				[CompilerGenerated]
				get
				{
					return this._mapIKList;
				}
			}

			// Token: 0x17001B70 RID: 7024
			// (get) Token: 0x06008308 RID: 33544 RVA: 0x003707EC File Offset: 0x0036EBEC
			public string MinimapIconNameList
			{
				[CompilerGenerated]
				get
				{
					return this._minimapIconNameList;
				}
			}

			// Token: 0x17001B71 RID: 7025
			// (get) Token: 0x06008309 RID: 33545 RVA: 0x003707F4 File Offset: 0x0036EBF4
			public string VanishCameraList
			{
				[CompilerGenerated]
				get
				{
					return this._vanishCameraList;
				}
			}

			// Token: 0x17001B72 RID: 7026
			// (get) Token: 0x0600830A RID: 33546 RVA: 0x003707FC File Offset: 0x0036EBFC
			public string ExpList
			{
				[CompilerGenerated]
				get
				{
					return this._expList;
				}
			}

			// Token: 0x17001B73 RID: 7027
			// (get) Token: 0x0600830B RID: 33547 RVA: 0x00370804 File Offset: 0x0036EC04
			public string ActionExpList
			{
				[CompilerGenerated]
				get
				{
					return this._actionExpList;
				}
			}

			// Token: 0x17001B74 RID: 7028
			// (get) Token: 0x0600830C RID: 33548 RVA: 0x0037080C File Offset: 0x0036EC0C
			public string ActionExpKeyFrameList
			{
				[CompilerGenerated]
				get
				{
					return this._actionExpKeyFrameList;
				}
			}

			// Token: 0x17001B75 RID: 7029
			// (get) Token: 0x0600830D RID: 33549 RVA: 0x00370814 File Offset: 0x0036EC14
			public string ActionBustCtrlList
			{
				[CompilerGenerated]
				get
				{
					return this._actionBustCtrlList;
				}
			}

			// Token: 0x17001B76 RID: 7030
			// (get) Token: 0x0600830E RID: 33550 RVA: 0x0037081C File Offset: 0x0036EC1C
			public string ActionCameraData
			{
				[CompilerGenerated]
				get
				{
					return this._actionCameraData;
				}
			}

			// Token: 0x17001B77 RID: 7031
			// (get) Token: 0x0600830F RID: 33551 RVA: 0x00370824 File Offset: 0x0036EC24
			public string ActionByproductList
			{
				[CompilerGenerated]
				get
				{
					return this._actionByproductList;
				}
			}

			// Token: 0x17001B78 RID: 7032
			// (get) Token: 0x06008310 RID: 33552 RVA: 0x0037082C File Offset: 0x0036EC2C
			public string EnviroInfoList
			{
				[CompilerGenerated]
				get
				{
					return this._enviroInfoList;
				}
			}

			// Token: 0x17001B79 RID: 7033
			// (get) Token: 0x06008311 RID: 33553 RVA: 0x00370834 File Offset: 0x0036EC34
			public string RecyclingInfoList
			{
				[CompilerGenerated]
				get
				{
					return this._recyclingInfoList;
				}
			}

			// Token: 0x04006937 RID: 26935
			[SerializeField]
			private string _inputIconList = string.Empty;

			// Token: 0x04006938 RID: 26936
			[SerializeField]
			private string _actionIconList = string.Empty;

			// Token: 0x04006939 RID: 26937
			[SerializeField]
			private string _actorIconList = string.Empty;

			// Token: 0x0400693A RID: 26938
			[SerializeField]
			private string _weatherIconList = string.Empty;

			// Token: 0x0400693B RID: 26939
			[SerializeField]
			private string _equipItemIconList = string.Empty;

			// Token: 0x0400693C RID: 26940
			[SerializeField]
			private string _statusIconList = string.Empty;

			// Token: 0x0400693D RID: 26941
			[SerializeField]
			private string _sickIconList = string.Empty;

			// Token: 0x0400693E RID: 26942
			[SerializeField]
			private string _loadingSpriteList;

			// Token: 0x0400693F RID: 26943
			[SerializeField]
			private string _searchEquipItemObjList = string.Empty;

			// Token: 0x04006940 RID: 26944
			[SerializeField]
			private string _commonEquipItemObjList = string.Empty;

			// Token: 0x04006941 RID: 26945
			[SerializeField]
			private string _accessoryItem = string.Empty;

			// Token: 0x04006942 RID: 26946
			[SerializeField]
			private string _actionPointPrefabList = string.Empty;

			// Token: 0x04006943 RID: 26947
			[SerializeField]
			private string _basePointPrefabList = string.Empty;

			// Token: 0x04006944 RID: 26948
			[SerializeField]
			private string _devicePointPrefabList = string.Empty;

			// Token: 0x04006945 RID: 26949
			[SerializeField]
			private string _farmPointPrefabList = string.Empty;

			// Token: 0x04006946 RID: 26950
			[SerializeField]
			private string _shipPointPrefabList = string.Empty;

			// Token: 0x04006947 RID: 26951
			[SerializeField]
			private string _playerActionPointList = string.Empty;

			// Token: 0x04006948 RID: 26952
			[SerializeField]
			private string _agentActionPointList = string.Empty;

			// Token: 0x04006949 RID: 26953
			[SerializeField]
			private string _merchantActionPointList = string.Empty;

			// Token: 0x0400694A RID: 26954
			[SerializeField]
			private string _lightSwitchPointList = string.Empty;

			// Token: 0x0400694B RID: 26955
			[SerializeField]
			private string _eventPointList = string.Empty;

			// Token: 0x0400694C RID: 26956
			[SerializeField]
			private string _storyPointList = string.Empty;

			// Token: 0x0400694D RID: 26957
			[SerializeField]
			private string _mapList = string.Empty;

			// Token: 0x0400694E RID: 26958
			[SerializeField]
			private string _chunkList = string.Empty;

			// Token: 0x0400694F RID: 26959
			[SerializeField]
			private string _waypointList = string.Empty;

			// Token: 0x04006950 RID: 26960
			[SerializeField]
			private string _AreaGroupList = string.Empty;

			// Token: 0x04006951 RID: 26961
			[SerializeField]
			private string _plantItemList = string.Empty;

			// Token: 0x04006952 RID: 26962
			[SerializeField]
			private string _ivyFilterList = string.Empty;

			// Token: 0x04006953 RID: 26963
			[SerializeField]
			private string _eventItemList = string.Empty;

			// Token: 0x04006954 RID: 26964
			[SerializeField]
			private string _eventParticleList = string.Empty;

			// Token: 0x04006955 RID: 26965
			[SerializeField]
			private string _popupInfoList = string.Empty;

			// Token: 0x04006956 RID: 26966
			[SerializeField]
			private string _areaOpenStateList = string.Empty;

			// Token: 0x04006957 RID: 26967
			[SerializeField]
			private string _timeRelationInfoList = string.Empty;

			// Token: 0x04006958 RID: 26968
			[SerializeField]
			private string _foodInfo = string.Empty;

			// Token: 0x04006959 RID: 26969
			[SerializeField]
			private string _drinkInfo = string.Empty;

			// Token: 0x0400695A RID: 26970
			[SerializeField]
			private string _itemList = string.Empty;

			// Token: 0x0400695B RID: 26971
			[SerializeField]
			private string _gatheringTable = string.Empty;

			// Token: 0x0400695C RID: 26972
			[SerializeField]
			private string _frogItemTable = string.Empty;

			// Token: 0x0400695D RID: 26973
			[SerializeField]
			private string _comCamera = string.Empty;

			// Token: 0x0400695E RID: 26974
			[SerializeField]
			private string _mapActionVoiceInfoList = string.Empty;

			// Token: 0x0400695F RID: 26975
			[SerializeField]
			private string _defaultFemaleFootStepSEInfoList = string.Empty;

			// Token: 0x04006960 RID: 26976
			[SerializeField]
			private string _defaultMaleFootStepSEInfoList = string.Empty;

			// Token: 0x04006961 RID: 26977
			[SerializeField]
			private string _mapBGMInfoList = string.Empty;

			// Token: 0x04006962 RID: 26978
			[SerializeField]
			private string _enviroSEInfoList = string.Empty;

			// Token: 0x04006963 RID: 26979
			[SerializeField]
			private string _actorAnimatorList = string.Empty;

			// Token: 0x04006964 RID: 26980
			[SerializeField]
			private string _playerFemaleAnimeInfo = string.Empty;

			// Token: 0x04006965 RID: 26981
			[SerializeField]
			private string _playerMaleAnimeInfo = string.Empty;

			// Token: 0x04006966 RID: 26982
			[SerializeField]
			private string _agentPhase = string.Empty;

			// Token: 0x04006967 RID: 26983
			[SerializeField]
			private string _agentPersonalityMotivation = string.Empty;

			// Token: 0x04006968 RID: 26984
			[SerializeField]
			private string _lifestyleTable = string.Empty;

			// Token: 0x04006969 RID: 26985
			[SerializeField]
			private string _flavorPickSkillTable = string.Empty;

			// Token: 0x0400696A RID: 26986
			[SerializeField]
			private string _flavorPickHSkillTable = string.Empty;

			// Token: 0x0400696B RID: 26987
			[SerializeField]
			private string _agentDesire = string.Empty;

			// Token: 0x0400696C RID: 26988
			[SerializeField]
			private string _agentCommunicationFlags = string.Empty;

			// Token: 0x0400696D RID: 26989
			[SerializeField]
			private string _agentAnimeInfo = string.Empty;

			// Token: 0x0400696E RID: 26990
			[SerializeField]
			private string _agentLocomotionBreath = string.Empty;

			// Token: 0x0400696F RID: 26991
			[SerializeField]
			private string _gravurePoseInfo = string.Empty;

			// Token: 0x04006970 RID: 26992
			[SerializeField]
			private string _surpriseItemInfo = string.Empty;

			// Token: 0x04006971 RID: 26993
			[SerializeField]
			private string _agentActionResult = string.Empty;

			// Token: 0x04006972 RID: 26994
			[SerializeField]
			private string _agentSituationResult = string.Empty;

			// Token: 0x04006973 RID: 26995
			[SerializeField]
			private string _actorVanishList = string.Empty;

			// Token: 0x04006974 RID: 26996
			[SerializeField]
			private string _behaviorTree = string.Empty;

			// Token: 0x04006975 RID: 26997
			[SerializeField]
			private string _tutrialBehaviorTree = string.Empty;

			// Token: 0x04006976 RID: 26998
			[SerializeField]
			private string _merchantAnimeInfo = string.Empty;

			// Token: 0x04006977 RID: 26999
			[SerializeField]
			private string _merchantBehaviorTree = string.Empty;

			// Token: 0x04006978 RID: 27000
			[SerializeField]
			private string _mapIKList = string.Empty;

			// Token: 0x04006979 RID: 27001
			[SerializeField]
			private string _minimapIconNameList = string.Empty;

			// Token: 0x0400697A RID: 27002
			[SerializeField]
			private string _vanishCameraList = string.Empty;

			// Token: 0x0400697B RID: 27003
			[SerializeField]
			private string _expList = string.Empty;

			// Token: 0x0400697C RID: 27004
			[SerializeField]
			private string _actionExpList = string.Empty;

			// Token: 0x0400697D RID: 27005
			[SerializeField]
			private string _actionExpKeyFrameList = string.Empty;

			// Token: 0x0400697E RID: 27006
			[SerializeField]
			private string _actionBustCtrlList = string.Empty;

			// Token: 0x0400697F RID: 27007
			[SerializeField]
			private string _actionCameraData = string.Empty;

			// Token: 0x04006980 RID: 27008
			[SerializeField]
			private string _actionByproductList = string.Empty;

			// Token: 0x04006981 RID: 27009
			[SerializeField]
			private string _enviroInfoList = string.Empty;

			// Token: 0x04006982 RID: 27010
			[SerializeField]
			private string _recyclingInfoList = string.Empty;
		}

		// Token: 0x02000F63 RID: 3939
		[Serializable]
		public class AssetBundlePathsDefine
		{
			// Token: 0x17001B7A RID: 7034
			// (get) Token: 0x06008313 RID: 33555 RVA: 0x003708C8 File Offset: 0x0036ECC8
			public string MapScene
			{
				[CompilerGenerated]
				get
				{
					return this._mapScene;
				}
			}

			// Token: 0x17001B7B RID: 7035
			// (get) Token: 0x06008314 RID: 33556 RVA: 0x003708D0 File Offset: 0x0036ECD0
			public string MapScenePrefab
			{
				[CompilerGenerated]
				get
				{
					return this._mapScenePrefab;
				}
			}

			// Token: 0x17001B7C RID: 7036
			// (get) Token: 0x06008315 RID: 33557 RVA: 0x003708D8 File Offset: 0x0036ECD8
			public string MapScenePrefabAdd05
			{
				[CompilerGenerated]
				get
				{
					return this._mapScenePrefabAdd05;
				}
			}

			// Token: 0x17001B7D RID: 7037
			// (get) Token: 0x06008316 RID: 33558 RVA: 0x003708E0 File Offset: 0x0036ECE0
			public string MapScenePrefabAdd07
			{
				[CompilerGenerated]
				get
				{
					return this._mapScenePrefabAdd07;
				}
			}

			// Token: 0x17001B7E RID: 7038
			// (get) Token: 0x06008317 RID: 33559 RVA: 0x003708E8 File Offset: 0x0036ECE8
			public string MapScenePrefabAdd12
			{
				[CompilerGenerated]
				get
				{
					return this._mapScenePrefabAdd12;
				}
			}

			// Token: 0x17001B7F RID: 7039
			// (get) Token: 0x06008318 RID: 33560 RVA: 0x003708F0 File Offset: 0x0036ECF0
			public string MapScenePrefabAdd19
			{
				[CompilerGenerated]
				get
				{
					return this._mapScenePrefabAdd19;
				}
			}

			// Token: 0x17001B80 RID: 7040
			// (get) Token: 0x06008319 RID: 33561 RVA: 0x003708F8 File Offset: 0x0036ECF8
			public string MapDebug
			{
				[CompilerGenerated]
				get
				{
					return this._mapDebug;
				}
			}

			// Token: 0x17001B81 RID: 7041
			// (get) Token: 0x0600831A RID: 33562 RVA: 0x00370900 File Offset: 0x0036ED00
			public string ActorPrefab
			{
				[CompilerGenerated]
				get
				{
					return this._actorPrefab;
				}
			}

			// Token: 0x17001B82 RID: 7042
			// (get) Token: 0x0600831B RID: 33563 RVA: 0x00370908 File Offset: 0x0036ED08
			public string Agent
			{
				[CompilerGenerated]
				get
				{
					return this._agent;
				}
			}

			// Token: 0x17001B83 RID: 7043
			// (get) Token: 0x0600831C RID: 33564 RVA: 0x00370910 File Offset: 0x0036ED10
			public string Camera
			{
				[CompilerGenerated]
				get
				{
					return this._camera;
				}
			}

			// Token: 0x17001B84 RID: 7044
			// (get) Token: 0x0600831D RID: 33565 RVA: 0x00370918 File Offset: 0x0036ED18
			public string CameraAdd05
			{
				[CompilerGenerated]
				get
				{
					return this._cameraAdd05;
				}
			}

			// Token: 0x04006983 RID: 27011
			[SerializeField]
			private string _mapScene = string.Empty;

			// Token: 0x04006984 RID: 27012
			[SerializeField]
			private string _mapScenePrefab = string.Empty;

			// Token: 0x04006985 RID: 27013
			[SerializeField]
			private string _mapScenePrefabAdd05 = string.Empty;

			// Token: 0x04006986 RID: 27014
			[SerializeField]
			private string _mapScenePrefabAdd07 = string.Empty;

			// Token: 0x04006987 RID: 27015
			[SerializeField]
			private string _mapScenePrefabAdd12 = string.Empty;

			// Token: 0x04006988 RID: 27016
			[SerializeField]
			private string _mapScenePrefabAdd19 = string.Empty;

			// Token: 0x04006989 RID: 27017
			[SerializeField]
			private string _mapDebug = string.Empty;

			// Token: 0x0400698A RID: 27018
			[SerializeField]
			private string _actorPrefab = string.Empty;

			// Token: 0x0400698B RID: 27019
			[SerializeField]
			private string _agent = string.Empty;

			// Token: 0x0400698C RID: 27020
			[SerializeField]
			private string _camera = string.Empty;

			// Token: 0x0400698D RID: 27021
			[SerializeField]
			private string _cameraAdd05 = string.Empty;
		}

		// Token: 0x02000F64 RID: 3940
		[Serializable]
		public class SceneNameGroup
		{
			// Token: 0x17001B85 RID: 7045
			// (get) Token: 0x0600831F RID: 33567 RVA: 0x00370996 File Offset: 0x0036ED96
			public string LogoScene
			{
				[CompilerGenerated]
				get
				{
					return this._logoScene;
				}
			}

			// Token: 0x17001B86 RID: 7046
			// (get) Token: 0x06008320 RID: 33568 RVA: 0x0037099E File Offset: 0x0036ED9E
			public string TitleScene
			{
				[CompilerGenerated]
				get
				{
					return this._titleScene;
				}
			}

			// Token: 0x17001B87 RID: 7047
			// (get) Token: 0x06008321 RID: 33569 RVA: 0x003709A6 File Offset: 0x0036EDA6
			public string MapScene
			{
				[CompilerGenerated]
				get
				{
					return this._mapScene;
				}
			}

			// Token: 0x17001B88 RID: 7048
			// (get) Token: 0x06008322 RID: 33570 RVA: 0x003709AE File Offset: 0x0036EDAE
			public string MapUIScene
			{
				[CompilerGenerated]
				get
				{
					return this._mapUIScene;
				}
			}

			// Token: 0x17001B89 RID: 7049
			// (get) Token: 0x06008323 RID: 33571 RVA: 0x003709B6 File Offset: 0x0036EDB6
			public string HScene
			{
				[CompilerGenerated]
				get
				{
					return this._hScene;
				}
			}

			// Token: 0x17001B8A RID: 7050
			// (get) Token: 0x06008324 RID: 33572 RVA: 0x003709BE File Offset: 0x0036EDBE
			public string MapShortcutScene
			{
				[CompilerGenerated]
				get
				{
					return this._mapShortcutScene;
				}
			}

			// Token: 0x17001B8B RID: 7051
			// (get) Token: 0x06008325 RID: 33573 RVA: 0x003709C6 File Offset: 0x0036EDC6
			public string ConfigScene
			{
				[CompilerGenerated]
				get
				{
					return this._configScene;
				}
			}

			// Token: 0x17001B8C RID: 7052
			// (get) Token: 0x06008326 RID: 33574 RVA: 0x003709CE File Offset: 0x0036EDCE
			public string DialogScene
			{
				[CompilerGenerated]
				get
				{
					return this._dialogScene;
				}
			}

			// Token: 0x17001B8D RID: 7053
			// (get) Token: 0x06008327 RID: 33575 RVA: 0x003709D6 File Offset: 0x0036EDD6
			public string ExitScene
			{
				[CompilerGenerated]
				get
				{
					return this._exitScene;
				}
			}

			// Token: 0x0400698E RID: 27022
			[SerializeField]
			private string _logoScene = string.Empty;

			// Token: 0x0400698F RID: 27023
			[SerializeField]
			private string _titleScene = string.Empty;

			// Token: 0x04006990 RID: 27024
			[SerializeField]
			private string _mapScene = string.Empty;

			// Token: 0x04006991 RID: 27025
			[SerializeField]
			private string _mapUIScene = string.Empty;

			// Token: 0x04006992 RID: 27026
			[SerializeField]
			private string _hScene = string.Empty;

			// Token: 0x04006993 RID: 27027
			[SerializeField]
			private string _mapShortcutScene = string.Empty;

			// Token: 0x04006994 RID: 27028
			[SerializeField]
			private string _configScene = string.Empty;

			// Token: 0x04006995 RID: 27029
			[SerializeField]
			private string _dialogScene = string.Empty;

			// Token: 0x04006996 RID: 27030
			[SerializeField]
			private string _exitScene = string.Empty;
		}

		// Token: 0x02000F65 RID: 3941
		[Serializable]
		public struct PlayerDefine
		{
			// Token: 0x04006997 RID: 27031
			public string walk;

			// Token: 0x04006998 RID: 27032
			public string run;
		}

		// Token: 0x02000F66 RID: 3942
		[Serializable]
		public class AnimatorStateNameGroup
		{
			// Token: 0x17001B8E RID: 7054
			// (get) Token: 0x06008329 RID: 33577 RVA: 0x00370A1D File Offset: 0x0036EE1D
			public PlayState.AnimStateInfo IdleStateInfo
			{
				[CompilerGenerated]
				get
				{
					return this._idleStateInfo;
				}
			}

			// Token: 0x17001B8F RID: 7055
			// (get) Token: 0x0600832A RID: 33578 RVA: 0x00370A25 File Offset: 0x0036EE25
			public PlayState.AnimStateInfo TurnStateInfo
			{
				[CompilerGenerated]
				get
				{
					return this._turnStateInfo;
				}
			}

			// Token: 0x17001B90 RID: 7056
			// (get) Token: 0x0600832B RID: 33579 RVA: 0x00370A2D File Offset: 0x0036EE2D
			public PlayState.AnimStateInfo FastTurnStateInfo
			{
				[CompilerGenerated]
				get
				{
					return this._fastTurnStateInfo;
				}
			}

			// Token: 0x17001B91 RID: 7057
			// (get) Token: 0x0600832C RID: 33580 RVA: 0x00370A35 File Offset: 0x0036EE35
			public string FastTurnAssetBundleName
			{
				[CompilerGenerated]
				get
				{
					return this._fastTurnAssetBundleName;
				}
			}

			// Token: 0x17001B92 RID: 7058
			// (get) Token: 0x0600832D RID: 33581 RVA: 0x00370A3D File Offset: 0x0036EE3D
			public string FastTurnAnimatorName
			{
				[CompilerGenerated]
				get
				{
					return this._fastTurnAnimatorName;
				}
			}

			// Token: 0x17001B93 RID: 7059
			// (get) Token: 0x0600832E RID: 33582 RVA: 0x00370A45 File Offset: 0x0036EE45
			public string IdleState
			{
				[CompilerGenerated]
				get
				{
					return this._idleState;
				}
			}

			// Token: 0x17001B94 RID: 7060
			// (get) Token: 0x0600832F RID: 33583 RVA: 0x00370A4D File Offset: 0x0036EE4D
			public string TurnState
			{
				[CompilerGenerated]
				get
				{
					return this._turnState;
				}
			}

			// Token: 0x17001B95 RID: 7061
			// (get) Token: 0x06008330 RID: 33584 RVA: 0x00370A55 File Offset: 0x0036EE55
			public string HousingAnimationDefault
			{
				[CompilerGenerated]
				get
				{
					return this._housingAnimationDefault;
				}
			}

			// Token: 0x17001B96 RID: 7062
			// (get) Token: 0x06008331 RID: 33585 RVA: 0x00370A5D File Offset: 0x0036EE5D
			public int OnbuStateID
			{
				[CompilerGenerated]
				get
				{
					return this._onbuStateID;
				}
			}

			// Token: 0x17001B97 RID: 7063
			// (get) Token: 0x06008332 RID: 33586 RVA: 0x00370A65 File Offset: 0x0036EE65
			public string MerchantIdleState
			{
				[CompilerGenerated]
				get
				{
					return this._merchantIdleState;
				}
			}

			// Token: 0x04006999 RID: 27033
			[SerializeField]
			private PlayState.AnimStateInfo _idleStateInfo;

			// Token: 0x0400699A RID: 27034
			[SerializeField]
			private PlayState.AnimStateInfo _turnStateInfo;

			// Token: 0x0400699B RID: 27035
			[SerializeField]
			private PlayState.AnimStateInfo _fastTurnStateInfo;

			// Token: 0x0400699C RID: 27036
			[SerializeField]
			private string _fastTurnAssetBundleName = string.Empty;

			// Token: 0x0400699D RID: 27037
			[SerializeField]
			private string _fastTurnAnimatorName = string.Empty;

			// Token: 0x0400699E RID: 27038
			[SerializeField]
			private string _idleState = string.Empty;

			// Token: 0x0400699F RID: 27039
			[SerializeField]
			private string _turnState;

			// Token: 0x040069A0 RID: 27040
			[SerializeField]
			private string _housingAnimationDefault = string.Empty;

			// Token: 0x040069A1 RID: 27041
			[SerializeField]
			private int _onbuStateID;

			// Token: 0x040069A2 RID: 27042
			[SerializeField]
			private string _merchantIdleState = string.Empty;
		}

		// Token: 0x02000F67 RID: 3943
		[Serializable]
		public class BasicAnimatorParameter
		{
			// Token: 0x17001B98 RID: 7064
			// (get) Token: 0x06008334 RID: 33588 RVA: 0x00370AAC File Offset: 0x0036EEAC
			public string ForwardMove
			{
				[CompilerGenerated]
				get
				{
					return this._forwardMove;
				}
			}

			// Token: 0x17001B99 RID: 7065
			// (get) Token: 0x06008335 RID: 33589 RVA: 0x00370AB4 File Offset: 0x0036EEB4
			public string HeightParameterName
			{
				[CompilerGenerated]
				get
				{
					return this._heightParameterName;
				}
			}

			// Token: 0x17001B9A RID: 7066
			// (get) Token: 0x06008336 RID: 33590 RVA: 0x00370ABC File Offset: 0x0036EEBC
			public string Height1ParameterName
			{
				[CompilerGenerated]
				get
				{
					return this._height1ParameterName;
				}
			}

			// Token: 0x17001B9B RID: 7067
			// (get) Token: 0x06008337 RID: 33591 RVA: 0x00370AC4 File Offset: 0x0036EEC4
			public string DirectionParameterName
			{
				[CompilerGenerated]
				get
				{
					return this._directionParameterName;
				}
			}

			// Token: 0x17001B9C RID: 7068
			// (get) Token: 0x06008338 RID: 33592 RVA: 0x00370ACC File Offset: 0x0036EECC
			public string SpeedParameterName
			{
				[CompilerGenerated]
				get
				{
					return this._speedParameterName;
				}
			}

			// Token: 0x040069A3 RID: 27043
			[SerializeField]
			private string _forwardMove = string.Empty;

			// Token: 0x040069A4 RID: 27044
			[SerializeField]
			private string _heightParameterName = string.Empty;

			// Token: 0x040069A5 RID: 27045
			[SerializeField]
			private string _height1ParameterName = string.Empty;

			// Token: 0x040069A6 RID: 27046
			[SerializeField]
			private string _directionParameterName = string.Empty;

			// Token: 0x040069A7 RID: 27047
			[SerializeField]
			private string _speedParameterName = string.Empty;
		}

		// Token: 0x02000F68 RID: 3944
		[Serializable]
		public class MapGroup
		{
			// Token: 0x17001B9D RID: 7069
			// (get) Token: 0x0600833A RID: 33594 RVA: 0x00370C54 File Offset: 0x0036F054
			public float WorldSize
			{
				[CompilerGenerated]
				get
				{
					return this._worldSize;
				}
			}

			// Token: 0x17001B9E RID: 7070
			// (get) Token: 0x0600833B RID: 33595 RVA: 0x00370C5C File Offset: 0x0036F05C
			public int AgentDefaultNum
			{
				[CompilerGenerated]
				get
				{
					return this._agentDefaultNum;
				}
			}

			// Token: 0x17001B9F RID: 7071
			// (get) Token: 0x0600833C RID: 33596 RVA: 0x00370C64 File Offset: 0x0036F064
			public int AgentMax
			{
				[CompilerGenerated]
				get
				{
					return this._agentMax;
				}
			}

			// Token: 0x17001BA0 RID: 7072
			// (get) Token: 0x0600833D RID: 33597 RVA: 0x00370C6C File Offset: 0x0036F06C
			public LayerMask CharaLayer
			{
				[CompilerGenerated]
				get
				{
					return this._charaLayer;
				}
			}

			// Token: 0x17001BA1 RID: 7073
			// (get) Token: 0x0600833E RID: 33598 RVA: 0x00370C74 File Offset: 0x0036F074
			public LayerMask MapLayer
			{
				[CompilerGenerated]
				get
				{
					return this._mapLayer;
				}
			}

			// Token: 0x17001BA2 RID: 7074
			// (get) Token: 0x0600833F RID: 33599 RVA: 0x00370C7C File Offset: 0x0036F07C
			public LayerMask AreaDetectionLayer
			{
				[CompilerGenerated]
				get
				{
					return this._areaDetectionLayer;
				}
			}

			// Token: 0x17001BA3 RID: 7075
			// (get) Token: 0x06008340 RID: 33600 RVA: 0x00370C84 File Offset: 0x0036F084
			public LayerMask RoofLayer
			{
				[CompilerGenerated]
				get
				{
					return this._roofLayer;
				}
			}

			// Token: 0x17001BA4 RID: 7076
			// (get) Token: 0x06008341 RID: 33601 RVA: 0x00370C8C File Offset: 0x0036F08C
			public LayerMask SELayer
			{
				[CompilerGenerated]
				get
				{
					return this._seLayer;
				}
			}

			// Token: 0x17001BA5 RID: 7077
			// (get) Token: 0x06008342 RID: 33602 RVA: 0x00370C94 File Offset: 0x0036F094
			public LayerMask HLayer
			{
				[CompilerGenerated]
				get
				{
					return this._hLayer;
				}
			}

			// Token: 0x17001BA6 RID: 7078
			// (get) Token: 0x06008343 RID: 33603 RVA: 0x00370C9C File Offset: 0x0036F09C
			public LayerMask EnvLightCulMask
			{
				[CompilerGenerated]
				get
				{
					return this._envLightCulMask;
				}
			}

			// Token: 0x17001BA7 RID: 7079
			// (get) Token: 0x06008344 RID: 33604 RVA: 0x00370CA4 File Offset: 0x0036F0A4
			public LayerMask EnvLightCulMaskCustom
			{
				[CompilerGenerated]
				get
				{
					return this._envLightCulMaskCustom;
				}
			}

			// Token: 0x17001BA8 RID: 7080
			// (get) Token: 0x06008345 RID: 33605 RVA: 0x00370CAC File Offset: 0x0036F0AC
			public string OnbuMeshTag
			{
				[CompilerGenerated]
				get
				{
					return this._onbuMeshTag;
				}
			}

			// Token: 0x17001BA9 RID: 7081
			// (get) Token: 0x06008346 RID: 33606 RVA: 0x00370CB4 File Offset: 0x0036F0B4
			public string[] FloorTypeHMeshTag
			{
				[CompilerGenerated]
				get
				{
					return this._floorTypeHMeshTag;
				}
			}

			// Token: 0x17001BAA RID: 7082
			// (get) Token: 0x06008347 RID: 33607 RVA: 0x00370CBC File Offset: 0x0036F0BC
			public string[] LesTypeHMeshTag
			{
				[CompilerGenerated]
				get
				{
					return this._lesTypeHMeshTag;
				}
			}

			// Token: 0x17001BAB RID: 7083
			// (get) Token: 0x06008348 RID: 33608 RVA: 0x00370CC4 File Offset: 0x0036F0C4
			public string[] MerchantHMeshTag
			{
				[CompilerGenerated]
				get
				{
					return this._merchantHMeshTag;
				}
			}

			// Token: 0x17001BAC RID: 7084
			// (get) Token: 0x06008349 RID: 33609 RVA: 0x00370CCC File Offset: 0x0036F0CC
			public EventType PlayerEventMask
			{
				[CompilerGenerated]
				get
				{
					return this._playerEventMask;
				}
			}

			// Token: 0x17001BAD RID: 7085
			// (get) Token: 0x0600834A RID: 33610 RVA: 0x00370CD4 File Offset: 0x0036F0D4
			public int DefaultMotivation
			{
				[CompilerGenerated]
				get
				{
					return this._defaultMotivation;
				}
			}

			// Token: 0x17001BAE RID: 7086
			// (get) Token: 0x0600834B RID: 33611 RVA: 0x00370CDC File Offset: 0x0036F0DC
			public int ItemStackUpperLimit
			{
				[CompilerGenerated]
				get
				{
					return this._itemStackUpperLimit;
				}
			}

			// Token: 0x17001BAF RID: 7087
			// (get) Token: 0x0600834C RID: 33612 RVA: 0x00370CE4 File Offset: 0x0036F0E4
			public int ItemSlotMax
			{
				[CompilerGenerated]
				get
				{
					return this._itemSlotMax;
				}
			}

			// Token: 0x17001BB0 RID: 7088
			// (get) Token: 0x0600834D RID: 33613 RVA: 0x00370CEC File Offset: 0x0036F0EC
			public string NavMeshTargetName
			{
				[CompilerGenerated]
				get
				{
					return this._navMeshTargetName;
				}
			}

			// Token: 0x17001BB1 RID: 7089
			// (get) Token: 0x0600834E RID: 33614 RVA: 0x00370CF4 File Offset: 0x0036F0F4
			public string CommandTargetName
			{
				[CompilerGenerated]
				get
				{
					return this._commandTargetName;
				}
			}

			// Token: 0x17001BB2 RID: 7090
			// (get) Token: 0x0600834F RID: 33615 RVA: 0x00370CFC File Offset: 0x0036F0FC
			public string DoorOpenCommandTargetName
			{
				[CompilerGenerated]
				get
				{
					return this._doorOpenCommandTargetName;
				}
			}

			// Token: 0x17001BB3 RID: 7091
			// (get) Token: 0x06008350 RID: 33616 RVA: 0x00370D04 File Offset: 0x0036F104
			public string DoorCloseCommandTargetName
			{
				[CompilerGenerated]
				get
				{
					return this._doorCloseCommandTargetName;
				}
			}

			// Token: 0x17001BB4 RID: 7092
			// (get) Token: 0x06008351 RID: 33617 RVA: 0x00370D0C File Offset: 0x0036F10C
			public string BasePointLabelTargetName
			{
				[CompilerGenerated]
				get
				{
					return this._basePointLabelTargetName;
				}
			}

			// Token: 0x17001BB5 RID: 7093
			// (get) Token: 0x06008352 RID: 33618 RVA: 0x00370D14 File Offset: 0x0036F114
			public string BasePointWarpTargetName
			{
				[CompilerGenerated]
				get
				{
					return this._basePointWarpTargetName;
				}
			}

			// Token: 0x17001BB6 RID: 7094
			// (get) Token: 0x06008353 RID: 33619 RVA: 0x00370D1C File Offset: 0x0036F11C
			public string HousingCenterTargetName
			{
				[CompilerGenerated]
				get
				{
					return this._housingCenterTargetName;
				}
			}

			// Token: 0x17001BB7 RID: 7095
			// (get) Token: 0x06008354 RID: 33620 RVA: 0x00370D24 File Offset: 0x0036F124
			public string DevicePointLabelTargetName
			{
				[CompilerGenerated]
				get
				{
					return this._devicePointLabelTargetName;
				}
			}

			// Token: 0x17001BB8 RID: 7096
			// (get) Token: 0x06008355 RID: 33621 RVA: 0x00370D2C File Offset: 0x0036F12C
			public string DevicePointPivotTargetName
			{
				[CompilerGenerated]
				get
				{
					return this._devicePointPivotTargetName;
				}
			}

			// Token: 0x17001BB9 RID: 7097
			// (get) Token: 0x06008356 RID: 33622 RVA: 0x00370D34 File Offset: 0x0036F134
			public string[] DevicePointRecoveryTargetNames
			{
				[CompilerGenerated]
				get
				{
					return this._devicePointRecoveryTargetNames;
				}
			}

			// Token: 0x17001BBA RID: 7098
			// (get) Token: 0x06008357 RID: 33623 RVA: 0x00370D3C File Offset: 0x0036F13C
			public string DevicePointPlayerRecoveryTargetName
			{
				[CompilerGenerated]
				get
				{
					return this._devicePointPlayerRecoveryTargetName;
				}
			}

			// Token: 0x17001BBB RID: 7099
			// (get) Token: 0x06008358 RID: 33624 RVA: 0x00370D44 File Offset: 0x0036F144
			public string FarmPointLabelTargetName
			{
				[CompilerGenerated]
				get
				{
					return this._farmPointLabelTargetName;
				}
			}

			// Token: 0x17001BBC RID: 7100
			// (get) Token: 0x06008359 RID: 33625 RVA: 0x00370D4C File Offset: 0x0036F14C
			public string CraftPointLabelTargetName
			{
				[CompilerGenerated]
				get
				{
					return this._craftPointLabelTargetName;
				}
			}

			// Token: 0x17001BBD RID: 7101
			// (get) Token: 0x0600835A RID: 33626 RVA: 0x00370D54 File Offset: 0x0036F154
			public string EventPointLabelTargetName
			{
				[CompilerGenerated]
				get
				{
					return this._eventPointLabelTargetName;
				}
			}

			// Token: 0x17001BBE RID: 7102
			// (get) Token: 0x0600835B RID: 33627 RVA: 0x00370D5C File Offset: 0x0036F15C
			public string PetHomePointLabelTargetName
			{
				[CompilerGenerated]
				get
				{
					return this._petHomePointLabelTargetName;
				}
			}

			// Token: 0x17001BBF RID: 7103
			// (get) Token: 0x0600835C RID: 33628 RVA: 0x00370D64 File Offset: 0x0036F164
			public string JukeBoxPointLabelTargetName
			{
				[CompilerGenerated]
				get
				{
					return this._jukeBoxPointLabelTargetName;
				}
			}

			// Token: 0x17001BC0 RID: 7104
			// (get) Token: 0x0600835D RID: 33629 RVA: 0x00370D6C File Offset: 0x0036F16C
			public string JukeBoxSoundRootTargetName
			{
				[CompilerGenerated]
				get
				{
					return this._jukeBoxSoundRootTargetName;
				}
			}

			// Token: 0x17001BC1 RID: 7105
			// (get) Token: 0x0600835E RID: 33630 RVA: 0x00370D74 File Offset: 0x0036F174
			public string LightPointLabelTargetName
			{
				[CompilerGenerated]
				get
				{
					return this._lightPointLabelTargetName;
				}
			}

			// Token: 0x17001BC2 RID: 7106
			// (get) Token: 0x0600835F RID: 33631 RVA: 0x00370D7C File Offset: 0x0036F17C
			public string ShipPointLabelTargetName
			{
				[CompilerGenerated]
				get
				{
					return this._shipPointLabelTargetName;
				}
			}

			// Token: 0x17001BC3 RID: 7107
			// (get) Token: 0x06008360 RID: 33632 RVA: 0x00370D84 File Offset: 0x0036F184
			public string StealPivotName
			{
				[CompilerGenerated]
				get
				{
					return this._stealPivotName;
				}
			}

			// Token: 0x17001BC4 RID: 7108
			// (get) Token: 0x06008361 RID: 33633 RVA: 0x00370D8C File Offset: 0x0036F18C
			public int ShadowDistance
			{
				[CompilerGenerated]
				get
				{
					return this._shadowDistance;
				}
			}

			// Token: 0x040069A8 RID: 27048
			[SerializeField]
			[Range(0.1f, 10f)]
			private float _worldSize = 1f;

			// Token: 0x040069A9 RID: 27049
			[SerializeField]
			private int _agentDefaultNum;

			// Token: 0x040069AA RID: 27050
			[SerializeField]
			private int _agentMax;

			// Token: 0x040069AB RID: 27051
			[SerializeField]
			private LayerMask _charaLayer = 0;

			// Token: 0x040069AC RID: 27052
			[SerializeField]
			private LayerMask _mapLayer = 0;

			// Token: 0x040069AD RID: 27053
			[SerializeField]
			private LayerMask _areaDetectionLayer = 0;

			// Token: 0x040069AE RID: 27054
			[SerializeField]
			private LayerMask _roofLayer = 0;

			// Token: 0x040069AF RID: 27055
			[SerializeField]
			private LayerMask _seLayer = 0;

			// Token: 0x040069B0 RID: 27056
			[SerializeField]
			private LayerMask _hLayer = 0;

			// Token: 0x040069B1 RID: 27057
			[SerializeField]
			private LayerMask _envLightCulMask = -1;

			// Token: 0x040069B2 RID: 27058
			[SerializeField]
			private LayerMask _envLightCulMaskCustom = -1;

			// Token: 0x040069B3 RID: 27059
			[SerializeField]
			private string _onbuMeshTag = string.Empty;

			// Token: 0x040069B4 RID: 27060
			[SerializeField]
			private string[] _floorTypeHMeshTag;

			// Token: 0x040069B5 RID: 27061
			[SerializeField]
			private string[] _lesTypeHMeshTag;

			// Token: 0x040069B6 RID: 27062
			[SerializeField]
			private string[] _merchantHMeshTag;

			// Token: 0x040069B7 RID: 27063
			[SerializeField]
			[EnumMask]
			private EventType _playerEventMask;

			// Token: 0x040069B8 RID: 27064
			[SerializeField]
			private int _defaultMotivation;

			// Token: 0x040069B9 RID: 27065
			[SerializeField]
			private int _itemStackUpperLimit;

			// Token: 0x040069BA RID: 27066
			[SerializeField]
			private int _itemSlotMax = 1;

			// Token: 0x040069BB RID: 27067
			[SerializeField]
			private string _navMeshTargetName = string.Empty;

			// Token: 0x040069BC RID: 27068
			[SerializeField]
			private string _commandTargetName = string.Empty;

			// Token: 0x040069BD RID: 27069
			[SerializeField]
			private string _doorOpenCommandTargetName = string.Empty;

			// Token: 0x040069BE RID: 27070
			[SerializeField]
			private string _doorCloseCommandTargetName = string.Empty;

			// Token: 0x040069BF RID: 27071
			[SerializeField]
			private string _basePointLabelTargetName = string.Empty;

			// Token: 0x040069C0 RID: 27072
			[SerializeField]
			private string _basePointWarpTargetName = string.Empty;

			// Token: 0x040069C1 RID: 27073
			[SerializeField]
			private string _housingCenterTargetName = string.Empty;

			// Token: 0x040069C2 RID: 27074
			[SerializeField]
			private string _devicePointLabelTargetName = string.Empty;

			// Token: 0x040069C3 RID: 27075
			[SerializeField]
			private string _devicePointPivotTargetName = string.Empty;

			// Token: 0x040069C4 RID: 27076
			[SerializeField]
			private string[] _devicePointRecoveryTargetNames = new string[]
			{
				string.Empty
			};

			// Token: 0x040069C5 RID: 27077
			[SerializeField]
			private string _devicePointPlayerRecoveryTargetName = string.Empty;

			// Token: 0x040069C6 RID: 27078
			[SerializeField]
			private string _farmPointLabelTargetName = string.Empty;

			// Token: 0x040069C7 RID: 27079
			[SerializeField]
			private string _craftPointLabelTargetName = string.Empty;

			// Token: 0x040069C8 RID: 27080
			[SerializeField]
			private string _eventPointLabelTargetName = string.Empty;

			// Token: 0x040069C9 RID: 27081
			[SerializeField]
			private string _petHomePointLabelTargetName = string.Empty;

			// Token: 0x040069CA RID: 27082
			[SerializeField]
			private string _jukeBoxPointLabelTargetName = string.Empty;

			// Token: 0x040069CB RID: 27083
			[SerializeField]
			private string _jukeBoxSoundRootTargetName = string.Empty;

			// Token: 0x040069CC RID: 27084
			[SerializeField]
			private string _lightPointLabelTargetName = string.Empty;

			// Token: 0x040069CD RID: 27085
			[SerializeField]
			private string _shipPointLabelTargetName = string.Empty;

			// Token: 0x040069CE RID: 27086
			[SerializeField]
			private string _stealPivotName = string.Empty;

			// Token: 0x040069CF RID: 27087
			[SerializeField]
			private int _shadowDistance = 400;
		}

		// Token: 0x02000F69 RID: 3945
		[Serializable]
		public class ItemBoxCapacity
		{
			// Token: 0x17001BC5 RID: 7109
			// (get) Token: 0x06008363 RID: 33635 RVA: 0x00370DB2 File Offset: 0x0036F1B2
			public int StorageCapacity
			{
				[CompilerGenerated]
				get
				{
					return this._storageCapacity;
				}
			}

			// Token: 0x17001BC6 RID: 7110
			// (get) Token: 0x06008364 RID: 33636 RVA: 0x00370DBA File Offset: 0x0036F1BA
			public int PantryCapacity
			{
				[CompilerGenerated]
				get
				{
					return this._pantryCapacity;
				}
			}

			// Token: 0x040069D0 RID: 27088
			[SerializeField]
			private int _storageCapacity = 200;

			// Token: 0x040069D1 RID: 27089
			[SerializeField]
			private int _pantryCapacity = 200;
		}

		// Token: 0x02000F6A RID: 3946
		[Serializable]
		public class MinimapUI
		{
			// Token: 0x17001BC7 RID: 7111
			// (get) Token: 0x06008366 RID: 33638 RVA: 0x00370E16 File Offset: 0x0036F216
			public int BaseIconID
			{
				[CompilerGenerated]
				get
				{
					return this._baseIconID;
				}
			}

			// Token: 0x17001BC8 RID: 7112
			// (get) Token: 0x06008367 RID: 33639 RVA: 0x00370E1E File Offset: 0x0036F21E
			public int DeviceIconID
			{
				[CompilerGenerated]
				get
				{
					return this._deviceIconID;
				}
			}

			// Token: 0x17001BC9 RID: 7113
			// (get) Token: 0x06008368 RID: 33640 RVA: 0x00370E26 File Offset: 0x0036F226
			public int FarmIconID
			{
				[CompilerGenerated]
				get
				{
					return this._farmIconID;
				}
			}

			// Token: 0x17001BCA RID: 7114
			// (get) Token: 0x06008369 RID: 33641 RVA: 0x00370E2E File Offset: 0x0036F22E
			public int EventIconID
			{
				[CompilerGenerated]
				get
				{
					return this._eventIconID;
				}
			}

			// Token: 0x17001BCB RID: 7115
			// (get) Token: 0x0600836A RID: 33642 RVA: 0x00370E36 File Offset: 0x0036F236
			public int ChickenIconID
			{
				[CompilerGenerated]
				get
				{
					return this._chickenIconID;
				}
			}

			// Token: 0x17001BCC RID: 7116
			// (get) Token: 0x0600836B RID: 33643 RVA: 0x00370E3E File Offset: 0x0036F23E
			public int DragDeskIconID
			{
				[CompilerGenerated]
				get
				{
					return this._dragDeskIconID;
				}
			}

			// Token: 0x17001BCD RID: 7117
			// (get) Token: 0x0600836C RID: 33644 RVA: 0x00370E46 File Offset: 0x0036F246
			public int PetUnionIconID
			{
				[CompilerGenerated]
				get
				{
					return this._petUnionIconID;
				}
			}

			// Token: 0x17001BCE RID: 7118
			// (get) Token: 0x0600836D RID: 33645 RVA: 0x00370E4E File Offset: 0x0036F24E
			public int RecycleIconID
			{
				[CompilerGenerated]
				get
				{
					return this._recycleIconID;
				}
			}

			// Token: 0x17001BCF RID: 7119
			// (get) Token: 0x0600836E RID: 33646 RVA: 0x00370E56 File Offset: 0x0036F256
			public int JukeIconID
			{
				[CompilerGenerated]
				get
				{
					return this._jukeIconID;
				}
			}

			// Token: 0x040069D2 RID: 27090
			[SerializeField]
			private int _baseIconID = -1;

			// Token: 0x040069D3 RID: 27091
			[SerializeField]
			private int _deviceIconID = -1;

			// Token: 0x040069D4 RID: 27092
			[SerializeField]
			private int _farmIconID = -1;

			// Token: 0x040069D5 RID: 27093
			[SerializeField]
			private int _eventIconID = -1;

			// Token: 0x040069D6 RID: 27094
			[SerializeField]
			private int _chickenIconID = -1;

			// Token: 0x040069D7 RID: 27095
			[SerializeField]
			private int _dragDeskIconID = -1;

			// Token: 0x040069D8 RID: 27096
			[SerializeField]
			private int _petUnionIconID = -1;

			// Token: 0x040069D9 RID: 27097
			[SerializeField]
			private int _recycleIconID = -1;

			// Token: 0x040069DA RID: 27098
			[SerializeField]
			private int _jukeIconID = -1;
		}

		// Token: 0x02000F6B RID: 3947
		[Serializable]
		public class MapLes
		{
			// Token: 0x17001BD0 RID: 7120
			// (get) Token: 0x06008370 RID: 33648 RVA: 0x00370E66 File Offset: 0x0036F266
			public float LoopChangeTime
			{
				[CompilerGenerated]
				get
				{
					return this._loopChangeTime;
				}
			}

			// Token: 0x17001BD1 RID: 7121
			// (get) Token: 0x06008371 RID: 33649 RVA: 0x00370E6E File Offset: 0x0036F26E
			public string MotionParameterName
			{
				[CompilerGenerated]
				get
				{
					return this._motionParameterName;
				}
			}

			// Token: 0x040069DB RID: 27099
			[SerializeField]
			private float _loopChangeTime;

			// Token: 0x040069DC RID: 27100
			[SerializeField]
			private string _motionParameterName;
		}

		// Token: 0x02000F6C RID: 3948
		[Serializable]
		public class RecyclingSetting
		{
			// Token: 0x17001BD2 RID: 7122
			// (get) Token: 0x06008373 RID: 33651 RVA: 0x00370EA1 File Offset: 0x0036F2A1
			public int DecidedItemCapacity
			{
				[CompilerGenerated]
				get
				{
					return this._decidedItemCapacity;
				}
			}

			// Token: 0x17001BD3 RID: 7123
			// (get) Token: 0x06008374 RID: 33652 RVA: 0x00370EA9 File Offset: 0x0036F2A9
			public int CreateItemCapacity
			{
				[CompilerGenerated]
				get
				{
					return this._createItemCapacity;
				}
			}

			// Token: 0x17001BD4 RID: 7124
			// (get) Token: 0x06008375 RID: 33653 RVA: 0x00370EB1 File Offset: 0x0036F2B1
			public float ItemCreateTime
			{
				[CompilerGenerated]
				get
				{
					return this._itemCreateTime;
				}
			}

			// Token: 0x17001BD5 RID: 7125
			// (get) Token: 0x06008376 RID: 33654 RVA: 0x00370EB9 File Offset: 0x0036F2B9
			public int NeedNumber
			{
				[CompilerGenerated]
				get
				{
					return this._needNumber;
				}
			}

			// Token: 0x040069DD RID: 27101
			[SerializeField]
			private int _decidedItemCapacity = 100;

			// Token: 0x040069DE RID: 27102
			[SerializeField]
			private int _createItemCapacity = 100;

			// Token: 0x040069DF RID: 27103
			[SerializeField]
			private float _itemCreateTime = 10f;

			// Token: 0x040069E0 RID: 27104
			[SerializeField]
			private int _needNumber = 10;
		}
	}
}
