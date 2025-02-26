using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AIProject.SaveData;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000F55 RID: 3925
	public class CommonDefine : SerializedScriptableObject
	{
		// Token: 0x17001AA0 RID: 6816
		// (get) Token: 0x0600822B RID: 33323 RVA: 0x0036FA59 File Offset: 0x0036DE59
		public CommonDefine.FileNamesGroup FileNames
		{
			[CompilerGenerated]
			get
			{
				return this._fileNames;
			}
		}

		// Token: 0x17001AA1 RID: 6817
		// (get) Token: 0x0600822C RID: 33324 RVA: 0x0036FA61 File Offset: 0x0036DE61
		public CommonDefine.CommonIconGroup Icon
		{
			[CompilerGenerated]
			get
			{
				return this._icon;
			}
		}

		// Token: 0x17001AA2 RID: 6818
		// (get) Token: 0x0600822D RID: 33325 RVA: 0x0036FA69 File Offset: 0x0036DE69
		public CommonDefine.ItemIDDefines ItemIDDefine
		{
			[CompilerGenerated]
			get
			{
				return this._itemIDDefine;
			}
		}

		// Token: 0x17001AA3 RID: 6819
		// (get) Token: 0x0600822E RID: 33326 RVA: 0x0036FA71 File Offset: 0x0036DE71
		public ItemIconIDDefine ItemIconIDDefine
		{
			[CompilerGenerated]
			get
			{
				return this._itemIconIDDefine;
			}
		}

		// Token: 0x17001AA4 RID: 6820
		// (get) Token: 0x0600822F RID: 33327 RVA: 0x0036FA79 File Offset: 0x0036DE79
		public Dictionary<int, Dictionary<int, int>> SearchItemGradeTable
		{
			[CompilerGenerated]
			get
			{
				return this._searchItemGradeTable;
			}
		}

		// Token: 0x17001AA5 RID: 6821
		// (get) Token: 0x06008230 RID: 33328 RVA: 0x0036FA81 File Offset: 0x0036DE81
		public CommonDefine.ItemAnimGroup ItemAnims
		{
			[CompilerGenerated]
			get
			{
				return this._itemAnims;
			}
		}

		// Token: 0x17001AA6 RID: 6822
		// (get) Token: 0x06008231 RID: 33329 RVA: 0x0036FA89 File Offset: 0x0036DE89
		public Dictionary<int, string[]> AppearCameraInStates
		{
			[CompilerGenerated]
			get
			{
				return this._appearCameraInStates;
			}
		}

		// Token: 0x17001AA7 RID: 6823
		// (get) Token: 0x06008232 RID: 33330 RVA: 0x0036FA91 File Offset: 0x0036DE91
		public Dictionary<int, string[]> OpeningWakeUpCameraInStates
		{
			[CompilerGenerated]
			get
			{
				return this._openingWakeUpCameraInStates;
			}
		}

		// Token: 0x17001AA8 RID: 6824
		// (get) Token: 0x06008233 RID: 33331 RVA: 0x0036FA99 File Offset: 0x0036DE99
		public RarelityProbability ProbRarelityProfile
		{
			[CompilerGenerated]
			get
			{
				return this._probRarelityProfile;
			}
		}

		// Token: 0x17001AA9 RID: 6825
		// (get) Token: 0x06008234 RID: 33332 RVA: 0x0036FAA1 File Offset: 0x0036DEA1
		public WeatherProbability ProbWeatherProfile
		{
			[CompilerGenerated]
			get
			{
				return this._probWeatherProfile;
			}
		}

		// Token: 0x17001AAA RID: 6826
		// (get) Token: 0x06008235 RID: 33333 RVA: 0x0036FAA9 File Offset: 0x0036DEA9
		public SearchAreaProbabilities ProbSearchAreaProfile
		{
			[CompilerGenerated]
			get
			{
				return this._probSearchAreaProfile;
			}
		}

		// Token: 0x17001AAB RID: 6827
		// (get) Token: 0x06008236 RID: 33334 RVA: 0x0036FAB1 File Offset: 0x0036DEB1
		public CommonDefine.TutorialSetting Tutorial
		{
			[CompilerGenerated]
			get
			{
				return this._tutorial;
			}
		}

		// Token: 0x17001AAC RID: 6828
		// (get) Token: 0x06008237 RID: 33335 RVA: 0x0036FAB9 File Offset: 0x0036DEB9
		public IReadOnlyDictionary<int, int> AppearCameraAnimatorIDDic
		{
			[CompilerGenerated]
			get
			{
				return this._appearCameraAnimatorIDDic;
			}
		}

		// Token: 0x17001AAD RID: 6829
		// (get) Token: 0x06008238 RID: 33336 RVA: 0x0036FAC1 File Offset: 0x0036DEC1
		public IReadOnlyDictionary<int, int> OpeningWakeUpCameraAnimatorIDDic
		{
			[CompilerGenerated]
			get
			{
				return this._openingWakeUpCameraAnimatorIDDic;
			}
		}

		// Token: 0x17001AAE RID: 6830
		// (get) Token: 0x06008239 RID: 33337 RVA: 0x0036FAC9 File Offset: 0x0036DEC9
		public CommonDefine.EventStoryInfoGroup EventStoryInfo
		{
			[CompilerGenerated]
			get
			{
				return this._eventStoryInfo;
			}
		}

		// Token: 0x040068A7 RID: 26791
		[SerializeField]
		private CommonDefine.FileNamesGroup _fileNames = new CommonDefine.FileNamesGroup();

		// Token: 0x040068A8 RID: 26792
		[SerializeField]
		private CommonDefine.CommonIconGroup _icon;

		// Token: 0x040068A9 RID: 26793
		[SerializeField]
		private CommonDefine.ItemIDDefines _itemIDDefine;

		// Token: 0x040068AA RID: 26794
		[SerializeField]
		private ItemIconIDDefine _itemIconIDDefine;

		// Token: 0x040068AB RID: 26795
		[SerializeField]
		private Dictionary<int, Dictionary<int, int>> _searchItemGradeTable = new Dictionary<int, Dictionary<int, int>>();

		// Token: 0x040068AC RID: 26796
		[SerializeField]
		private CommonDefine.ItemAnimGroup _itemAnims;

		// Token: 0x040068AD RID: 26797
		[SerializeField]
		private Dictionary<int, string[]> _appearCameraInStates = new Dictionary<int, string[]>();

		// Token: 0x040068AE RID: 26798
		[SerializeField]
		private Dictionary<int, string[]> _openingWakeUpCameraInStates = new Dictionary<int, string[]>();

		// Token: 0x040068AF RID: 26799
		[SerializeField]
		private RarelityProbability _probRarelityProfile;

		// Token: 0x040068B0 RID: 26800
		[SerializeField]
		private WeatherProbability _probWeatherProfile;

		// Token: 0x040068B1 RID: 26801
		[SerializeField]
		private SearchAreaProbabilities _probSearchAreaProfile;

		// Token: 0x040068B2 RID: 26802
		[SerializeField]
		private CommonDefine.TutorialSetting _tutorial;

		// Token: 0x040068B3 RID: 26803
		[SerializeField]
		private Dictionary<int, int> _appearCameraAnimatorIDDic = new Dictionary<int, int>();

		// Token: 0x040068B4 RID: 26804
		[SerializeField]
		private Dictionary<int, int> _openingWakeUpCameraAnimatorIDDic = new Dictionary<int, int>();

		// Token: 0x040068B5 RID: 26805
		[SerializeField]
		private CommonDefine.EventStoryInfoGroup _eventStoryInfo;

		// Token: 0x02000F56 RID: 3926
		[Serializable]
		public class FileNamesGroup
		{
			// Token: 0x17001AAF RID: 6831
			// (get) Token: 0x0600823B RID: 33339 RVA: 0x0036FB29 File Offset: 0x0036DF29
			public string MainCameraName
			{
				[CompilerGenerated]
				get
				{
					return this._mainCameraName;
				}
			}

			// Token: 0x17001AB0 RID: 6832
			// (get) Token: 0x0600823C RID: 33340 RVA: 0x0036FB31 File Offset: 0x0036DF31
			public string NormalCameraName
			{
				[CompilerGenerated]
				get
				{
					return this._normalCameraName;
				}
			}

			// Token: 0x17001AB1 RID: 6833
			// (get) Token: 0x0600823D RID: 33341 RVA: 0x0036FB39 File Offset: 0x0036DF39
			public string ActionCameraFreeLookName
			{
				[CompilerGenerated]
				get
				{
					return this._actionCameraFreeLookName;
				}
			}

			// Token: 0x17001AB2 RID: 6834
			// (get) Token: 0x0600823E RID: 33342 RVA: 0x0036FB41 File Offset: 0x0036DF41
			public string ActionCameraNotMoveName
			{
				[CompilerGenerated]
				get
				{
					return this._actionCameraNotMoveName;
				}
			}

			// Token: 0x17001AB3 RID: 6835
			// (get) Token: 0x0600823F RID: 33343 RVA: 0x0036FB49 File Offset: 0x0036DF49
			public string FishingCamera
			{
				[CompilerGenerated]
				get
				{
					return this._fishingCamera;
				}
			}

			// Token: 0x17001AB4 RID: 6836
			// (get) Token: 0x06008240 RID: 33344 RVA: 0x0036FB51 File Offset: 0x0036DF51
			public string TrialCamera
			{
				[CompilerGenerated]
				get
				{
					return this._trialCamera;
				}
			}

			// Token: 0x040068B6 RID: 26806
			[SerializeField]
			private string _mainCameraName = string.Empty;

			// Token: 0x040068B7 RID: 26807
			[SerializeField]
			private string _normalCameraName = string.Empty;

			// Token: 0x040068B8 RID: 26808
			[SerializeField]
			private string _actionCameraFreeLookName = string.Empty;

			// Token: 0x040068B9 RID: 26809
			[SerializeField]
			private string _actionCameraNotMoveName = string.Empty;

			// Token: 0x040068BA RID: 26810
			[SerializeField]
			private string _fishingCamera = string.Empty;

			// Token: 0x040068BB RID: 26811
			[SerializeField]
			private string _trialCamera = string.Empty;
		}

		// Token: 0x02000F57 RID: 3927
		[Serializable]
		public class CommonIconGroup
		{
			// Token: 0x17001AB5 RID: 6837
			// (get) Token: 0x06008242 RID: 33346 RVA: 0x0036FB77 File Offset: 0x0036DF77
			public CommandTargetSpriteInfo ActionSpriteInfo
			{
				[CompilerGenerated]
				get
				{
					return this._actionSpriteInfo;
				}
			}

			// Token: 0x17001AB6 RID: 6838
			// (get) Token: 0x06008243 RID: 33347 RVA: 0x0036FB7F File Offset: 0x0036DF7F
			public CommandTargetSpriteInfo CharaSpriteInfo
			{
				[CompilerGenerated]
				get
				{
					return this._charaSpriteInfo;
				}
			}

			// Token: 0x17001AB7 RID: 6839
			// (get) Token: 0x06008244 RID: 33348 RVA: 0x0036FB87 File Offset: 0x0036DF87
			public Sprite ActionSprite
			{
				[CompilerGenerated]
				get
				{
					return this._actionSprite;
				}
			}

			// Token: 0x17001AB8 RID: 6840
			// (get) Token: 0x06008245 RID: 33349 RVA: 0x0036FB8F File Offset: 0x0036DF8F
			public Sprite ActionSelectedSprite
			{
				[CompilerGenerated]
				get
				{
					return this._actionSelectedSprite;
				}
			}

			// Token: 0x17001AB9 RID: 6841
			// (get) Token: 0x06008246 RID: 33350 RVA: 0x0036FB97 File Offset: 0x0036DF97
			public Sprite CharaSprite
			{
				[CompilerGenerated]
				get
				{
					return this._charaSprite;
				}
			}

			// Token: 0x17001ABA RID: 6842
			// (get) Token: 0x06008247 RID: 33351 RVA: 0x0036FB9F File Offset: 0x0036DF9F
			public Sprite CharaSelectedSprite
			{
				[CompilerGenerated]
				get
				{
					return this._charaSelectedSprite;
				}
			}

			// Token: 0x17001ABB RID: 6843
			// (get) Token: 0x06008248 RID: 33352 RVA: 0x0036FBA7 File Offset: 0x0036DFA7
			public Texture2D TouchCursorTexture
			{
				[CompilerGenerated]
				get
				{
					return this._touchCursorTexture;
				}
			}

			// Token: 0x17001ABC RID: 6844
			// (get) Token: 0x06008249 RID: 33353 RVA: 0x0036FBAF File Offset: 0x0036DFAF
			public int BaseIconID
			{
				[CompilerGenerated]
				get
				{
					return this._baseIconID;
				}
			}

			// Token: 0x17001ABD RID: 6845
			// (get) Token: 0x0600824A RID: 33354 RVA: 0x0036FBB7 File Offset: 0x0036DFB7
			public int DeviceIconID
			{
				[CompilerGenerated]
				get
				{
					return this._deviceIconID;
				}
			}

			// Token: 0x17001ABE RID: 6846
			// (get) Token: 0x0600824B RID: 33355 RVA: 0x0036FBBF File Offset: 0x0036DFBF
			public int FarmIconID
			{
				[CompilerGenerated]
				get
				{
					return this._farmIconID;
				}
			}

			// Token: 0x17001ABF RID: 6847
			// (get) Token: 0x0600824C RID: 33356 RVA: 0x0036FBC7 File Offset: 0x0036DFC7
			public int ChickenCoopIconID
			{
				[CompilerGenerated]
				get
				{
					return this._chickenCoopIconID;
				}
			}

			// Token: 0x17001AC0 RID: 6848
			// (get) Token: 0x0600824D RID: 33357 RVA: 0x0036FBCF File Offset: 0x0036DFCF
			public int WellIconID
			{
				[CompilerGenerated]
				get
				{
					return this._wellIconID;
				}
			}

			// Token: 0x17001AC1 RID: 6849
			// (get) Token: 0x0600824E RID: 33358 RVA: 0x0036FBD7 File Offset: 0x0036DFD7
			public int CraftIconID
			{
				[CompilerGenerated]
				get
				{
					return this._craftIconID;
				}
			}

			// Token: 0x17001AC2 RID: 6850
			// (get) Token: 0x0600824F RID: 33359 RVA: 0x0036FBDF File Offset: 0x0036DFDF
			public int MedicineCraftIconID
			{
				[CompilerGenerated]
				get
				{
					return this._medicineCraftIconID;
				}
			}

			// Token: 0x17001AC3 RID: 6851
			// (get) Token: 0x06008250 RID: 33360 RVA: 0x0036FBE7 File Offset: 0x0036DFE7
			public int PetCraftIcon
			{
				[CompilerGenerated]
				get
				{
					return this._petCraftIconID;
				}
			}

			// Token: 0x17001AC4 RID: 6852
			// (get) Token: 0x06008251 RID: 33361 RVA: 0x0036FBEF File Offset: 0x0036DFEF
			public int RecyclingCraftIcon
			{
				[CompilerGenerated]
				get
				{
					return this._recyclingCraftIcon;
				}
			}

			// Token: 0x17001AC5 RID: 6853
			// (get) Token: 0x06008252 RID: 33362 RVA: 0x0036FBF7 File Offset: 0x0036DFF7
			public int FishTankIconID
			{
				[CompilerGenerated]
				get
				{
					return this._fishTankIconID;
				}
			}

			// Token: 0x17001AC6 RID: 6854
			// (get) Token: 0x06008253 RID: 33363 RVA: 0x0036FBFF File Offset: 0x0036DFFF
			public int JukeBoxIconID
			{
				[CompilerGenerated]
				get
				{
					return this._jukeBoxIconID;
				}
			}

			// Token: 0x17001AC7 RID: 6855
			// (get) Token: 0x06008254 RID: 33364 RVA: 0x0036FC07 File Offset: 0x0036E007
			public int EventIconID
			{
				[CompilerGenerated]
				get
				{
					return this._eventIconID;
				}
			}

			// Token: 0x17001AC8 RID: 6856
			// (get) Token: 0x06008255 RID: 33365 RVA: 0x0036FC0F File Offset: 0x0036E00F
			public int StoryIconID
			{
				[CompilerGenerated]
				get
				{
					return this._storyIconID;
				}
			}

			// Token: 0x17001AC9 RID: 6857
			// (get) Token: 0x06008256 RID: 33366 RVA: 0x0036FC17 File Offset: 0x0036E017
			public int GuideOKID
			{
				[CompilerGenerated]
				get
				{
					return this._guideOKID;
				}
			}

			// Token: 0x17001ACA RID: 6858
			// (get) Token: 0x06008257 RID: 33367 RVA: 0x0036FC1F File Offset: 0x0036E01F
			public int GuideCancelID
			{
				[CompilerGenerated]
				get
				{
					return this._guideCancelID;
				}
			}

			// Token: 0x17001ACB RID: 6859
			// (get) Token: 0x06008258 RID: 33368 RVA: 0x0036FC27 File Offset: 0x0036E027
			public int GuideScrollID
			{
				[CompilerGenerated]
				get
				{
					return this._guideScrollID;
				}
			}

			// Token: 0x17001ACC RID: 6860
			// (get) Token: 0x06008259 RID: 33369 RVA: 0x0036FC2F File Offset: 0x0036E02F
			public int ShipIconID
			{
				[CompilerGenerated]
				get
				{
					return this._shipIconID;
				}
			}

			// Token: 0x040068BC RID: 26812
			[SerializeField]
			private CommandTargetSpriteInfo _actionSpriteInfo = new CommandTargetSpriteInfo();

			// Token: 0x040068BD RID: 26813
			[SerializeField]
			private CommandTargetSpriteInfo _charaSpriteInfo = new CommandTargetSpriteInfo();

			// Token: 0x040068BE RID: 26814
			[SerializeField]
			private Sprite _actionSprite;

			// Token: 0x040068BF RID: 26815
			[SerializeField]
			private Sprite _actionSelectedSprite;

			// Token: 0x040068C0 RID: 26816
			[SerializeField]
			private Sprite _charaSprite;

			// Token: 0x040068C1 RID: 26817
			[SerializeField]
			private Sprite _charaSelectedSprite;

			// Token: 0x040068C2 RID: 26818
			[SerializeField]
			private Texture2D _touchCursorTexture;

			// Token: 0x040068C3 RID: 26819
			[SerializeField]
			private int _baseIconID;

			// Token: 0x040068C4 RID: 26820
			[SerializeField]
			private int _deviceIconID;

			// Token: 0x040068C5 RID: 26821
			[SerializeField]
			private int _farmIconID;

			// Token: 0x040068C6 RID: 26822
			[SerializeField]
			private int _chickenCoopIconID;

			// Token: 0x040068C7 RID: 26823
			[SerializeField]
			private int _wellIconID;

			// Token: 0x040068C8 RID: 26824
			[SerializeField]
			private int _craftIconID;

			// Token: 0x040068C9 RID: 26825
			[SerializeField]
			private int _medicineCraftIconID;

			// Token: 0x040068CA RID: 26826
			[SerializeField]
			private int _petCraftIconID;

			// Token: 0x040068CB RID: 26827
			[SerializeField]
			private int _recyclingCraftIcon;

			// Token: 0x040068CC RID: 26828
			[SerializeField]
			private int _fishTankIconID;

			// Token: 0x040068CD RID: 26829
			[SerializeField]
			private int _jukeBoxIconID;

			// Token: 0x040068CE RID: 26830
			[SerializeField]
			private int _eventIconID;

			// Token: 0x040068CF RID: 26831
			[SerializeField]
			private int _storyIconID;

			// Token: 0x040068D0 RID: 26832
			[SerializeField]
			private int _guideOKID;

			// Token: 0x040068D1 RID: 26833
			[SerializeField]
			private int _guideCancelID;

			// Token: 0x040068D2 RID: 26834
			[SerializeField]
			private int _guideScrollID;

			// Token: 0x040068D3 RID: 26835
			[SerializeField]
			private int _shipIconID;
		}

		// Token: 0x02000F58 RID: 3928
		[Serializable]
		public class ItemAnimGroup
		{
			// Token: 0x17001ACD RID: 6861
			// (get) Token: 0x0600825B RID: 33371 RVA: 0x0036FCE8 File Offset: 0x0036E0E8
			public int ChestAnimatorID
			{
				[CompilerGenerated]
				get
				{
					return this._chestAnimatorID;
				}
			}

			// Token: 0x17001ACE RID: 6862
			// (get) Token: 0x0600825C RID: 33372 RVA: 0x0036FCF0 File Offset: 0x0036E0F0
			public string ChestDefaultState
			{
				[CompilerGenerated]
				get
				{
					return this._chestDefaultState;
				}
			}

			// Token: 0x17001ACF RID: 6863
			// (get) Token: 0x0600825D RID: 33373 RVA: 0x0036FCF8 File Offset: 0x0036E0F8
			public string[] ChestInStates
			{
				[CompilerGenerated]
				get
				{
					return this._chestInStates;
				}
			}

			// Token: 0x17001AD0 RID: 6864
			// (get) Token: 0x0600825E RID: 33374 RVA: 0x0036FD00 File Offset: 0x0036E100
			public string[] ChestOutStates
			{
				[CompilerGenerated]
				get
				{
					return this._chestOutStates;
				}
			}

			// Token: 0x17001AD1 RID: 6865
			// (get) Token: 0x0600825F RID: 33375 RVA: 0x0036FD08 File Offset: 0x0036E108
			public int DoorAnimatorID
			{
				[CompilerGenerated]
				get
				{
					return this._doorAnimatorID;
				}
			}

			// Token: 0x17001AD2 RID: 6866
			// (get) Token: 0x06008260 RID: 33376 RVA: 0x0036FD10 File Offset: 0x0036E110
			public string DoorDefaultState
			{
				[CompilerGenerated]
				get
				{
					return this._doorDefaultState;
				}
			}

			// Token: 0x17001AD3 RID: 6867
			// (get) Token: 0x06008261 RID: 33377 RVA: 0x0036FD18 File Offset: 0x0036E118
			public string DoorCloseRight
			{
				[CompilerGenerated]
				get
				{
					return this._doorCloseRight;
				}
			}

			// Token: 0x17001AD4 RID: 6868
			// (get) Token: 0x06008262 RID: 33378 RVA: 0x0036FD20 File Offset: 0x0036E120
			public string DoorCloseLeft
			{
				[CompilerGenerated]
				get
				{
					return this._doorCloseLeft;
				}
			}

			// Token: 0x17001AD5 RID: 6869
			// (get) Token: 0x06008263 RID: 33379 RVA: 0x0036FD28 File Offset: 0x0036E128
			public string DoorCloseLoopState
			{
				[CompilerGenerated]
				get
				{
					return this._doorCloseLoopState;
				}
			}

			// Token: 0x17001AD6 RID: 6870
			// (get) Token: 0x06008264 RID: 33380 RVA: 0x0036FD30 File Offset: 0x0036E130
			public string DoorOpenIdleRight
			{
				[CompilerGenerated]
				get
				{
					return this._doorOpenIdleRight;
				}
			}

			// Token: 0x17001AD7 RID: 6871
			// (get) Token: 0x06008265 RID: 33381 RVA: 0x0036FD38 File Offset: 0x0036E138
			public string DoorOpenIdleLeft
			{
				[CompilerGenerated]
				get
				{
					return this._doorOpenIdleLeft;
				}
			}

			// Token: 0x17001AD8 RID: 6872
			// (get) Token: 0x06008266 RID: 33382 RVA: 0x0036FD40 File Offset: 0x0036E140
			public int PodAnimatorID
			{
				[CompilerGenerated]
				get
				{
					return this._podAnimatorID;
				}
			}

			// Token: 0x17001AD9 RID: 6873
			// (get) Token: 0x06008267 RID: 33383 RVA: 0x0036FD48 File Offset: 0x0036E148
			public string[] PodInStates
			{
				[CompilerGenerated]
				get
				{
					return this._podInStates;
				}
			}

			// Token: 0x17001ADA RID: 6874
			// (get) Token: 0x06008268 RID: 33384 RVA: 0x0036FD50 File Offset: 0x0036E150
			public string[] PodOutStates
			{
				[CompilerGenerated]
				get
				{
					return this._podOutStates;
				}
			}

			// Token: 0x17001ADB RID: 6875
			// (get) Token: 0x06008269 RID: 33385 RVA: 0x0036FD58 File Offset: 0x0036E158
			public int AppearCameraAnimatorID
			{
				[CompilerGenerated]
				get
				{
					return this._appearCameraAnimatorID;
				}
			}

			// Token: 0x17001ADC RID: 6876
			// (get) Token: 0x0600826A RID: 33386 RVA: 0x0036FD60 File Offset: 0x0036E160
			public int OpeningWakeUpCameraAnimatorID
			{
				[CompilerGenerated]
				get
				{
					return this._openingWakeUpCameraAnimatorID;
				}
			}

			// Token: 0x040068D4 RID: 26836
			[SerializeField]
			private int _chestAnimatorID;

			// Token: 0x040068D5 RID: 26837
			[SerializeField]
			private string _chestDefaultState = string.Empty;

			// Token: 0x040068D6 RID: 26838
			[SerializeField]
			private string[] _chestInStates = new string[]
			{
				string.Empty
			};

			// Token: 0x040068D7 RID: 26839
			[SerializeField]
			private string[] _chestOutStates = new string[]
			{
				string.Empty
			};

			// Token: 0x040068D8 RID: 26840
			[SerializeField]
			private int _doorAnimatorID;

			// Token: 0x040068D9 RID: 26841
			[SerializeField]
			private string _doorDefaultState = string.Empty;

			// Token: 0x040068DA RID: 26842
			[SerializeField]
			private string _doorCloseRight = string.Empty;

			// Token: 0x040068DB RID: 26843
			[SerializeField]
			private string _doorCloseLeft = string.Empty;

			// Token: 0x040068DC RID: 26844
			[SerializeField]
			private string _doorCloseLoopState = string.Empty;

			// Token: 0x040068DD RID: 26845
			[SerializeField]
			private string _doorOpenIdleRight = string.Empty;

			// Token: 0x040068DE RID: 26846
			[SerializeField]
			private string _doorOpenIdleLeft = string.Empty;

			// Token: 0x040068DF RID: 26847
			[SerializeField]
			private int _podAnimatorID;

			// Token: 0x040068E0 RID: 26848
			[SerializeField]
			private string[] _podInStates = new string[]
			{
				string.Empty
			};

			// Token: 0x040068E1 RID: 26849
			[SerializeField]
			private string[] _podOutStates = new string[]
			{
				string.Empty
			};

			// Token: 0x040068E2 RID: 26850
			[SerializeField]
			private int _appearCameraAnimatorID;

			// Token: 0x040068E3 RID: 26851
			[SerializeField]
			private int _openingWakeUpCameraAnimatorID;
		}

		// Token: 0x02000F59 RID: 3929
		[Serializable]
		public class ItemIDDefines
		{
			// Token: 0x17001ADD RID: 6877
			// (get) Token: 0x0600826C RID: 33388 RVA: 0x0036FEB7 File Offset: 0x0036E2B7
			public ItemIDKeyPair RareGloveID
			{
				[CompilerGenerated]
				get
				{
					return this._rareGloveID;
				}
			}

			// Token: 0x17001ADE RID: 6878
			// (get) Token: 0x0600826D RID: 33389 RVA: 0x0036FEBF File Offset: 0x0036E2BF
			public ItemIDKeyPair SRareGloveID
			{
				[CompilerGenerated]
				get
				{
					return this._sRareGloveID;
				}
			}

			// Token: 0x17001ADF RID: 6879
			// (get) Token: 0x0600826E RID: 33390 RVA: 0x0036FEC7 File Offset: 0x0036E2C7
			public ItemIDKeyPair NetID
			{
				[CompilerGenerated]
				get
				{
					return this._netID;
				}
			}

			// Token: 0x17001AE0 RID: 6880
			// (get) Token: 0x0600826F RID: 33391 RVA: 0x0036FECF File Offset: 0x0036E2CF
			public ItemIDKeyPair RareNetID
			{
				[CompilerGenerated]
				get
				{
					return this._rareNetID;
				}
			}

			// Token: 0x17001AE1 RID: 6881
			// (get) Token: 0x06008270 RID: 33392 RVA: 0x0036FED7 File Offset: 0x0036E2D7
			public ItemIDKeyPair SRareNetID
			{
				[CompilerGenerated]
				get
				{
					return this._sRareNetID;
				}
			}

			// Token: 0x17001AE2 RID: 6882
			// (get) Token: 0x06008271 RID: 33393 RVA: 0x0036FEDF File Offset: 0x0036E2DF
			public ItemIDKeyPair ShovelID
			{
				[CompilerGenerated]
				get
				{
					return this._shovelID;
				}
			}

			// Token: 0x17001AE3 RID: 6883
			// (get) Token: 0x06008272 RID: 33394 RVA: 0x0036FEE7 File Offset: 0x0036E2E7
			public ItemIDKeyPair RareShovelID
			{
				[CompilerGenerated]
				get
				{
					return this._rareShovelID;
				}
			}

			// Token: 0x17001AE4 RID: 6884
			// (get) Token: 0x06008273 RID: 33395 RVA: 0x0036FEEF File Offset: 0x0036E2EF
			public ItemIDKeyPair SRareShovelID
			{
				[CompilerGenerated]
				get
				{
					return this._sRareShovelID;
				}
			}

			// Token: 0x17001AE5 RID: 6885
			// (get) Token: 0x06008274 RID: 33396 RVA: 0x0036FEF7 File Offset: 0x0036E2F7
			public ItemIDKeyPair PickelID
			{
				[CompilerGenerated]
				get
				{
					return this._pickelID;
				}
			}

			// Token: 0x17001AE6 RID: 6886
			// (get) Token: 0x06008275 RID: 33397 RVA: 0x0036FEFF File Offset: 0x0036E2FF
			public ItemIDKeyPair RarePickelID
			{
				[CompilerGenerated]
				get
				{
					return this._rarePickelID;
				}
			}

			// Token: 0x17001AE7 RID: 6887
			// (get) Token: 0x06008276 RID: 33398 RVA: 0x0036FF07 File Offset: 0x0036E307
			public ItemIDKeyPair SRarePickelID
			{
				[CompilerGenerated]
				get
				{
					return this._sRarePickelID;
				}
			}

			// Token: 0x17001AE8 RID: 6888
			// (get) Token: 0x06008277 RID: 33399 RVA: 0x0036FF0F File Offset: 0x0036E30F
			public ItemIDKeyPair RodID
			{
				[CompilerGenerated]
				get
				{
					return this._rodID;
				}
			}

			// Token: 0x17001AE9 RID: 6889
			// (get) Token: 0x06008278 RID: 33400 RVA: 0x0036FF17 File Offset: 0x0036E317
			public ItemIDKeyPair UmbrellaID
			{
				[CompilerGenerated]
				get
				{
					return this._umbrellaID;
				}
			}

			// Token: 0x17001AEA RID: 6890
			// (get) Token: 0x06008279 RID: 33401 RVA: 0x0036FF1F File Offset: 0x0036E31F
			public ItemIDKeyPair HandLampID
			{
				[CompilerGenerated]
				get
				{
					return this._handLampID;
				}
			}

			// Token: 0x17001AEB RID: 6891
			// (get) Token: 0x0600827A RID: 33402 RVA: 0x0036FF27 File Offset: 0x0036E327
			public ItemIDKeyPair TorchID
			{
				[CompilerGenerated]
				get
				{
					return this._torchID;
				}
			}

			// Token: 0x17001AEC RID: 6892
			// (get) Token: 0x0600827B RID: 33403 RVA: 0x0036FF2F File Offset: 0x0036E32F
			public ItemIDKeyPair MaleLampID
			{
				[CompilerGenerated]
				get
				{
					return this._maleLampID;
				}
			}

			// Token: 0x17001AED RID: 6893
			// (get) Token: 0x0600827C RID: 33404 RVA: 0x0036FF37 File Offset: 0x0036E337
			public ItemIDKeyPair FlashlightID
			{
				[CompilerGenerated]
				get
				{
					return this._flashlightID;
				}
			}

			// Token: 0x17001AEE RID: 6894
			// (get) Token: 0x0600827D RID: 33405 RVA: 0x0036FF3F File Offset: 0x0036E33F
			public ItemIDKeyPair DriftwoodID
			{
				[CompilerGenerated]
				get
				{
					return this._driftwoodID;
				}
			}

			// Token: 0x17001AEF RID: 6895
			// (get) Token: 0x0600827E RID: 33406 RVA: 0x0036FF47 File Offset: 0x0036E347
			public ItemIDKeyPair ShansKeyID
			{
				[CompilerGenerated]
				get
				{
					return this._shansKeyID;
				}
			}

			// Token: 0x17001AF0 RID: 6896
			// (get) Token: 0x0600827F RID: 33407 RVA: 0x0036FF4F File Offset: 0x0036E34F
			public ItemIDKeyPair GauzeID
			{
				[CompilerGenerated]
				get
				{
					return this._gauzeID;
				}
			}

			// Token: 0x06008280 RID: 33408 RVA: 0x0036FF58 File Offset: 0x0036E358
			public bool ContainsLightItem(StuffItem item)
			{
				return (this._torchID.categoryID == item.CategoryID && this._torchID.itemID == item.ID) || (this._maleLampID.categoryID == item.CategoryID && this._maleLampID.itemID == item.ID) || (this._flashlightID.categoryID == item.CategoryID && this._flashlightID.itemID == item.ID);
			}

			// Token: 0x17001AF1 RID: 6897
			// (get) Token: 0x06008281 RID: 33409 RVA: 0x0036FFF0 File Offset: 0x0036E3F0
			public int ToolCategoryID
			{
				[CompilerGenerated]
				get
				{
					return this._toolCategoryID;
				}
			}

			// Token: 0x17001AF2 RID: 6898
			// (get) Token: 0x06008282 RID: 33410 RVA: 0x0036FFF8 File Offset: 0x0036E3F8
			public int EquipmentCategoryID
			{
				[CompilerGenerated]
				get
				{
					return this._equipmentCategoryID;
				}
			}

			// Token: 0x17001AF3 RID: 6899
			// (get) Token: 0x06008283 RID: 33411 RVA: 0x00370000 File Offset: 0x0036E400
			public int NormalSkillCategoryID
			{
				[CompilerGenerated]
				get
				{
					return this._normalSkillCategoryID;
				}
			}

			// Token: 0x17001AF4 RID: 6900
			// (get) Token: 0x06008284 RID: 33412 RVA: 0x00370008 File Offset: 0x0036E408
			public int HSkillCategoryID
			{
				[CompilerGenerated]
				get
				{
					return this._hSkillCategoryID;
				}
			}

			// Token: 0x17001AF5 RID: 6901
			// (get) Token: 0x06008285 RID: 33413 RVA: 0x00370010 File Offset: 0x0036E410
			public int[] ExtendItemIDs
			{
				[CompilerGenerated]
				get
				{
					return this._extendItemIDs;
				}
			}

			// Token: 0x17001AF6 RID: 6902
			// (get) Token: 0x06008286 RID: 33414 RVA: 0x00370018 File Offset: 0x0036E418
			public int[] GloveItemIDs
			{
				[CompilerGenerated]
				get
				{
					return this._gloveItemIDs;
				}
			}

			// Token: 0x17001AF7 RID: 6903
			// (get) Token: 0x06008287 RID: 33415 RVA: 0x00370020 File Offset: 0x0036E420
			public int[] NetItemIDs
			{
				[CompilerGenerated]
				get
				{
					return this._netItemIDs;
				}
			}

			// Token: 0x17001AF8 RID: 6904
			// (get) Token: 0x06008288 RID: 33416 RVA: 0x00370028 File Offset: 0x0036E428
			public int[] PickelItemIDs
			{
				[CompilerGenerated]
				get
				{
					return this._pickelItemIDs;
				}
			}

			// Token: 0x17001AF9 RID: 6905
			// (get) Token: 0x06008289 RID: 33417 RVA: 0x00370030 File Offset: 0x0036E430
			public int[] ShovelItemlIDs
			{
				[CompilerGenerated]
				get
				{
					return this._shovelItemIDs;
				}
			}

			// Token: 0x17001AFA RID: 6906
			// (get) Token: 0x0600828A RID: 33418 RVA: 0x00370038 File Offset: 0x0036E438
			public int[] RodItemIDs
			{
				[CompilerGenerated]
				get
				{
					return this._rodItemIDs;
				}
			}

			// Token: 0x17001AFB RID: 6907
			// (get) Token: 0x0600828B RID: 33419 RVA: 0x00370040 File Offset: 0x0036E440
			public int[] HeadItemIDs
			{
				[CompilerGenerated]
				get
				{
					return this._headItemIDs;
				}
			}

			// Token: 0x17001AFC RID: 6908
			// (get) Token: 0x0600828C RID: 33420 RVA: 0x00370048 File Offset: 0x0036E448
			public int[] BackItemIDs
			{
				[CompilerGenerated]
				get
				{
					return this._backItemIDs;
				}
			}

			// Token: 0x17001AFD RID: 6909
			// (get) Token: 0x0600828D RID: 33421 RVA: 0x00370050 File Offset: 0x0036E450
			public int[] NeckItemIDs
			{
				[CompilerGenerated]
				get
				{
					return this._neckItemIDs;
				}
			}

			// Token: 0x17001AFE RID: 6910
			// (get) Token: 0x0600828E RID: 33422 RVA: 0x00370058 File Offset: 0x0036E458
			public int[] PlayerLightItemIDs
			{
				[CompilerGenerated]
				get
				{
					return this._playerLightItemIDs;
				}
			}

			// Token: 0x17001AFF RID: 6911
			// (get) Token: 0x0600828F RID: 33423 RVA: 0x00370060 File Offset: 0x0036E460
			public int[] FemaleLightItemIDs
			{
				[CompilerGenerated]
				get
				{
					return this._femaleLightItemIDs;
				}
			}

			// Token: 0x040068E4 RID: 26852
			[SerializeField]
			private ItemIDKeyPair _rareGloveID = default(ItemIDKeyPair);

			// Token: 0x040068E5 RID: 26853
			[SerializeField]
			private ItemIDKeyPair _sRareGloveID = default(ItemIDKeyPair);

			// Token: 0x040068E6 RID: 26854
			[SerializeField]
			private ItemIDKeyPair _netID = default(ItemIDKeyPair);

			// Token: 0x040068E7 RID: 26855
			[SerializeField]
			private ItemIDKeyPair _rareNetID = default(ItemIDKeyPair);

			// Token: 0x040068E8 RID: 26856
			[SerializeField]
			private ItemIDKeyPair _sRareNetID = default(ItemIDKeyPair);

			// Token: 0x040068E9 RID: 26857
			[SerializeField]
			private ItemIDKeyPair _shovelID = default(ItemIDKeyPair);

			// Token: 0x040068EA RID: 26858
			[SerializeField]
			private ItemIDKeyPair _rareShovelID = default(ItemIDKeyPair);

			// Token: 0x040068EB RID: 26859
			[SerializeField]
			private ItemIDKeyPair _sRareShovelID = default(ItemIDKeyPair);

			// Token: 0x040068EC RID: 26860
			[SerializeField]
			private ItemIDKeyPair _pickelID = default(ItemIDKeyPair);

			// Token: 0x040068ED RID: 26861
			[SerializeField]
			private ItemIDKeyPair _rarePickelID = default(ItemIDKeyPair);

			// Token: 0x040068EE RID: 26862
			[SerializeField]
			private ItemIDKeyPair _sRarePickelID = default(ItemIDKeyPair);

			// Token: 0x040068EF RID: 26863
			[SerializeField]
			private ItemIDKeyPair _rodID = default(ItemIDKeyPair);

			// Token: 0x040068F0 RID: 26864
			[SerializeField]
			private ItemIDKeyPair _umbrellaID = default(ItemIDKeyPair);

			// Token: 0x040068F1 RID: 26865
			[SerializeField]
			private ItemIDKeyPair _handLampID = default(ItemIDKeyPair);

			// Token: 0x040068F2 RID: 26866
			[SerializeField]
			private ItemIDKeyPair _torchID = default(ItemIDKeyPair);

			// Token: 0x040068F3 RID: 26867
			[SerializeField]
			private ItemIDKeyPair _maleLampID = default(ItemIDKeyPair);

			// Token: 0x040068F4 RID: 26868
			[SerializeField]
			private ItemIDKeyPair _flashlightID = default(ItemIDKeyPair);

			// Token: 0x040068F5 RID: 26869
			[SerializeField]
			private ItemIDKeyPair _driftwoodID = default(ItemIDKeyPair);

			// Token: 0x040068F6 RID: 26870
			[SerializeField]
			private ItemIDKeyPair _shansKeyID = default(ItemIDKeyPair);

			// Token: 0x040068F7 RID: 26871
			[SerializeField]
			private ItemIDKeyPair _gauzeID = default(ItemIDKeyPair);

			// Token: 0x040068F8 RID: 26872
			[SerializeField]
			private int _toolCategoryID;

			// Token: 0x040068F9 RID: 26873
			[SerializeField]
			private int _equipmentCategoryID;

			// Token: 0x040068FA RID: 26874
			[SerializeField]
			private int _normalSkillCategoryID;

			// Token: 0x040068FB RID: 26875
			[SerializeField]
			private int _hSkillCategoryID;

			// Token: 0x040068FC RID: 26876
			[SerializeField]
			private int[] _extendItemIDs;

			// Token: 0x040068FD RID: 26877
			[SerializeField]
			private int[] _gloveItemIDs;

			// Token: 0x040068FE RID: 26878
			[SerializeField]
			private int[] _netItemIDs;

			// Token: 0x040068FF RID: 26879
			[SerializeField]
			private int[] _pickelItemIDs;

			// Token: 0x04006900 RID: 26880
			[SerializeField]
			private int[] _shovelItemIDs;

			// Token: 0x04006901 RID: 26881
			[SerializeField]
			private int[] _rodItemIDs;

			// Token: 0x04006902 RID: 26882
			[SerializeField]
			private int[] _headItemIDs;

			// Token: 0x04006903 RID: 26883
			[SerializeField]
			private int[] _backItemIDs;

			// Token: 0x04006904 RID: 26884
			[SerializeField]
			private int[] _neckItemIDs;

			// Token: 0x04006905 RID: 26885
			[SerializeField]
			private int[] _playerLightItemIDs;

			// Token: 0x04006906 RID: 26886
			[SerializeField]
			private int[] _femaleLightItemIDs;
		}

		// Token: 0x02000F5A RID: 3930
		[Serializable]
		public class TutorialSetting
		{
			// Token: 0x17001B00 RID: 6912
			// (get) Token: 0x06008291 RID: 33425 RVA: 0x00370091 File Offset: 0x0036E491
			public float OpeningWakeUpStartFadeTime
			{
				[CompilerGenerated]
				get
				{
					return this._openingWakeUpStartFadeTime;
				}
			}

			// Token: 0x17001B01 RID: 6913
			// (get) Token: 0x06008292 RID: 33426 RVA: 0x00370099 File Offset: 0x0036E499
			public float OpeningWakeUpFadeTime
			{
				[CompilerGenerated]
				get
				{
					return this._openingWakeUpFadeTime;
				}
			}

			// Token: 0x17001B02 RID: 6914
			// (get) Token: 0x06008293 RID: 33427 RVA: 0x003700A1 File Offset: 0x0036E4A1
			public float FollowGirlWaitTime
			{
				[CompilerGenerated]
				get
				{
					return this._followGirlWaitTime;
				}
			}

			// Token: 0x17001B03 RID: 6915
			// (get) Token: 0x06008294 RID: 33428 RVA: 0x003700A9 File Offset: 0x0036E4A9
			public float UIDisplayDelayTime
			{
				[CompilerGenerated]
				get
				{
					return this._uiDisplayDelayTime;
				}
			}

			// Token: 0x17001B04 RID: 6916
			// (get) Token: 0x06008295 RID: 33429 RVA: 0x003700B1 File Offset: 0x0036E4B1
			public float WalkToRunTime
			{
				[CompilerGenerated]
				get
				{
					return this._walkToRunTime;
				}
			}

			// Token: 0x17001B05 RID: 6917
			// (get) Token: 0x06008296 RID: 33430 RVA: 0x003700B9 File Offset: 0x0036E4B9
			public int[] KitchenPointIDList
			{
				[CompilerGenerated]
				get
				{
					return this._kitchenPointIDList;
				}
			}

			// Token: 0x17001B06 RID: 6918
			// (get) Token: 0x06008297 RID: 33431 RVA: 0x003700C1 File Offset: 0x0036E4C1
			public int YotunbaiRegisterID
			{
				[CompilerGenerated]
				get
				{
					return this._yotunbaiRegisterID;
				}
			}

			// Token: 0x04006907 RID: 26887
			[SerializeField]
			private float _openingWakeUpStartFadeTime;

			// Token: 0x04006908 RID: 26888
			[SerializeField]
			private float _openingWakeUpFadeTime = 1f;

			// Token: 0x04006909 RID: 26889
			[SerializeField]
			private float _followGirlWaitTime = 10f;

			// Token: 0x0400690A RID: 26890
			[SerializeField]
			private float _uiDisplayDelayTime;

			// Token: 0x0400690B RID: 26891
			[SerializeField]
			private float _walkToRunTime = 1f;

			// Token: 0x0400690C RID: 26892
			[SerializeField]
			private int[] _kitchenPointIDList;

			// Token: 0x0400690D RID: 26893
			[SerializeField]
			private int _yotunbaiRegisterID;
		}

		// Token: 0x02000F5B RID: 3931
		[Serializable]
		public class EventStoryInfoGroup
		{
			// Token: 0x17001B07 RID: 6919
			// (get) Token: 0x06008299 RID: 33433 RVA: 0x003700D1 File Offset: 0x0036E4D1
			public float StartADVFadeTime
			{
				[CompilerGenerated]
				get
				{
					return this._startADVFadeTime;
				}
			}

			// Token: 0x17001B08 RID: 6920
			// (get) Token: 0x0600829A RID: 33434 RVA: 0x003700D9 File Offset: 0x0036E4D9
			public float EndADVFadeTime
			{
				[CompilerGenerated]
				get
				{
					return this._endADVFadeTime;
				}
			}

			// Token: 0x17001B09 RID: 6921
			// (get) Token: 0x0600829B RID: 33435 RVA: 0x003700E1 File Offset: 0x0036E4E1
			public float StartEventFadeTime
			{
				[CompilerGenerated]
				get
				{
					return this._startEventFadeTime;
				}
			}

			// Token: 0x17001B0A RID: 6922
			// (get) Token: 0x0600829C RID: 33436 RVA: 0x003700E9 File Offset: 0x0036E4E9
			public float EndEventFadeTime
			{
				[CompilerGenerated]
				get
				{
					return this._endEventFadeTime;
				}
			}

			// Token: 0x17001B0B RID: 6923
			// (get) Token: 0x0600829D RID: 33437 RVA: 0x003700F1 File Offset: 0x0036E4F1
			public CommonDefine.EventStoryInfoGroup.EventSoundInfo JunkRoad
			{
				[CompilerGenerated]
				get
				{
					return this._junkRoad;
				}
			}

			// Token: 0x17001B0C RID: 6924
			// (get) Token: 0x0600829E RID: 33438 RVA: 0x003700F9 File Offset: 0x0036E4F9
			public CommonDefine.EventStoryInfoGroup.EventSoundInfo FenceDoor
			{
				[CompilerGenerated]
				get
				{
					return this._fenceDoorInfo;
				}
			}

			// Token: 0x17001B0D RID: 6925
			// (get) Token: 0x0600829F RID: 33439 RVA: 0x00370101 File Offset: 0x0036E501
			public CommonDefine.EventStoryInfoGroup.EventSoundInfo Generator
			{
				[CompilerGenerated]
				get
				{
					return this._generatorInfo;
				}
			}

			// Token: 0x17001B0E RID: 6926
			// (get) Token: 0x060082A0 RID: 33440 RVA: 0x00370109 File Offset: 0x0036E509
			public CommonDefine.EventStoryInfoGroup.EventSoundInfo ShipRepair
			{
				[CompilerGenerated]
				get
				{
					return this._shipRepair;
				}
			}

			// Token: 0x17001B0F RID: 6927
			// (get) Token: 0x060082A1 RID: 33441 RVA: 0x00370111 File Offset: 0x0036E511
			public CommonDefine.EventStoryInfoGroup.EventSoundInfo AutomaticDoor
			{
				[CompilerGenerated]
				get
				{
					return this._automaticDoor;
				}
			}

			// Token: 0x17001B10 RID: 6928
			// (get) Token: 0x060082A2 RID: 33442 RVA: 0x00370119 File Offset: 0x0036E519
			public CommonDefine.EventStoryInfoGroup.EventSoundInfo PodDevice
			{
				[CompilerGenerated]
				get
				{
					return this._podDevice;
				}
			}

			// Token: 0x17001B11 RID: 6929
			// (get) Token: 0x060082A3 RID: 33443 RVA: 0x00370121 File Offset: 0x0036E521
			public float StoryCompleteNextSupportChangeTime
			{
				[CompilerGenerated]
				get
				{
					return this._storyCompleteNextSupportChangeTime;
				}
			}

			// Token: 0x0400690E RID: 26894
			[SerializeField]
			private float _startADVFadeTime;

			// Token: 0x0400690F RID: 26895
			[SerializeField]
			private float _endADVFadeTime;

			// Token: 0x04006910 RID: 26896
			[SerializeField]
			private float _startEventFadeTime;

			// Token: 0x04006911 RID: 26897
			[SerializeField]
			private float _endEventFadeTime;

			// Token: 0x04006912 RID: 26898
			[SerializeField]
			private CommonDefine.EventStoryInfoGroup.EventSoundInfo _junkRoad;

			// Token: 0x04006913 RID: 26899
			[SerializeField]
			private CommonDefine.EventStoryInfoGroup.EventSoundInfo _fenceDoorInfo;

			// Token: 0x04006914 RID: 26900
			[SerializeField]
			private CommonDefine.EventStoryInfoGroup.EventSoundInfo _generatorInfo;

			// Token: 0x04006915 RID: 26901
			[SerializeField]
			private CommonDefine.EventStoryInfoGroup.EventSoundInfo _shipRepair;

			// Token: 0x04006916 RID: 26902
			[SerializeField]
			private CommonDefine.EventStoryInfoGroup.EventSoundInfo _automaticDoor;

			// Token: 0x04006917 RID: 26903
			[SerializeField]
			private CommonDefine.EventStoryInfoGroup.EventSoundInfo _podDevice;

			// Token: 0x04006918 RID: 26904
			[SerializeField]
			private float _storyCompleteNextSupportChangeTime;

			// Token: 0x02000F5C RID: 3932
			[Serializable]
			public class EventSoundInfo
			{
				// Token: 0x17001B12 RID: 6930
				// (get) Token: 0x060082A5 RID: 33445 RVA: 0x00370131 File Offset: 0x0036E531
				public int SEID
				{
					[CompilerGenerated]
					get
					{
						return this._seID;
					}
				}

				// Token: 0x17001B13 RID: 6931
				// (get) Token: 0x060082A6 RID: 33446 RVA: 0x00370139 File Offset: 0x0036E539
				public float SoundPlayDelayTime
				{
					[CompilerGenerated]
					get
					{
						return this._soundPlayDelayTime;
					}
				}

				// Token: 0x17001B14 RID: 6932
				// (get) Token: 0x060082A7 RID: 33447 RVA: 0x00370141 File Offset: 0x0036E541
				public float EndIntervalTime
				{
					[CompilerGenerated]
					get
					{
						return this._endIntervalTime;
					}
				}

				// Token: 0x04006919 RID: 26905
				[SerializeField]
				private int _seID;

				// Token: 0x0400691A RID: 26906
				[SerializeField]
				private float _soundPlayDelayTime;

				// Token: 0x0400691B RID: 26907
				[SerializeField]
				private float _endIntervalTime;
			}
		}
	}
}
