using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using AIProject.Definitions;
using AIProject.UI.Viewer;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000F47 RID: 3911
	public class AgentProfile : SerializedScriptableObject
	{
		// Token: 0x17001A00 RID: 6656
		// (get) Token: 0x06008177 RID: 33143 RVA: 0x0036ECC4 File Offset: 0x0036D0C4
		public ReadOnlyDictionary<EventType, EventType> AfterActionTable
		{
			get
			{
				ReadOnlyDictionary<EventType, EventType> result;
				if ((result = this._afterActionReadOnlyTable) == null)
				{
					result = (this._afterActionReadOnlyTable = new ReadOnlyDictionary<EventType, EventType>(this._afterActionTable));
				}
				return result;
			}
		}

		// Token: 0x17001A01 RID: 6657
		// (get) Token: 0x06008178 RID: 33144 RVA: 0x0036ECF2 File Offset: 0x0036D0F2
		public List<Desire.ActionType> EncounterWhitelist
		{
			[CompilerGenerated]
			get
			{
				return this._encounterWhitelist;
			}
		}

		// Token: 0x17001A02 RID: 6658
		// (get) Token: 0x06008179 RID: 33145 RVA: 0x0036ECFA File Offset: 0x0036D0FA
		public List<Desire.ActionType> ScrollWhitelist
		{
			[CompilerGenerated]
			get
			{
				return this._strollWhitelist;
			}
		}

		// Token: 0x17001A03 RID: 6659
		// (get) Token: 0x0600817A RID: 33146 RVA: 0x0036ED02 File Offset: 0x0036D102
		public List<Desire.ActionType> BlackListInSaveAndLoad
		{
			[CompilerGenerated]
			get
			{
				return this._blackListInSaveAndLoad;
			}
		}

		// Token: 0x17001A04 RID: 6660
		// (get) Token: 0x0600817B RID: 33147 RVA: 0x0036ED0A File Offset: 0x0036D10A
		public Dictionary<int, int> DefaultAreaIDTable
		{
			[CompilerGenerated]
			get
			{
				return this._defaultAreaIDTable;
			}
		}

		// Token: 0x17001A05 RID: 6661
		// (get) Token: 0x0600817C RID: 33148 RVA: 0x0036ED12 File Offset: 0x0036D112
		public AgentProfile.WalkParameter WalkSetting
		{
			[CompilerGenerated]
			get
			{
				return this._walkSetting;
			}
		}

		// Token: 0x17001A06 RID: 6662
		// (get) Token: 0x0600817D RID: 33149 RVA: 0x0036ED1A File Offset: 0x0036D11A
		public int AvoidancePriorityDefault
		{
			[CompilerGenerated]
			get
			{
				return this._avoidancePriorityDefault;
			}
		}

		// Token: 0x17001A07 RID: 6663
		// (get) Token: 0x0600817E RID: 33150 RVA: 0x0036ED22 File Offset: 0x0036D122
		public int AvoidancePriorityStationary
		{
			[CompilerGenerated]
			get
			{
				return this._avoidancePriorityStationary;
			}
		}

		// Token: 0x17001A08 RID: 6664
		// (get) Token: 0x0600817F RID: 33151 RVA: 0x0036ED2A File Offset: 0x0036D12A
		public float ActionPointNearDistance
		{
			[CompilerGenerated]
			get
			{
				return this._actionPointNearDistance;
			}
		}

		// Token: 0x17001A09 RID: 6665
		// (get) Token: 0x06008180 RID: 33152 RVA: 0x0036ED32 File Offset: 0x0036D132
		public ThresholdInt EscapeViaPointNumThreshold
		{
			[CompilerGenerated]
			get
			{
				return this._escapeViaPointNumThreshold;
			}
		}

		// Token: 0x17001A0A RID: 6666
		// (get) Token: 0x06008181 RID: 33153 RVA: 0x0036ED3A File Offset: 0x0036D13A
		public AgentProfile.PoseIDCollection PoseIDTable
		{
			[CompilerGenerated]
			get
			{
				return this._poseIDTable;
			}
		}

		// Token: 0x17001A0B RID: 6667
		// (get) Token: 0x06008182 RID: 33154 RVA: 0x0036ED42 File Offset: 0x0036D142
		public Dictionary<int, PoseKeyPair> ADVIdleTable
		{
			[CompilerGenerated]
			get
			{
				return this._advIdleTable;
			}
		}

		// Token: 0x17001A0C RID: 6668
		// (get) Token: 0x06008183 RID: 33155 RVA: 0x0036ED4A File Offset: 0x0036D14A
		public Dictionary<int, PoseKeyPair> ADVHouchiTable
		{
			[CompilerGenerated]
			get
			{
				return this._advHouchiTable;
			}
		}

		// Token: 0x17001A0D RID: 6669
		// (get) Token: 0x06008184 RID: 33156 RVA: 0x0036ED52 File Offset: 0x0036D152
		public Dictionary<int, PoseKeyPair> ADVLeaveTable
		{
			[CompilerGenerated]
			get
			{
				return this._advLeaveTable;
			}
		}

		// Token: 0x17001A0E RID: 6670
		// (get) Token: 0x06008185 RID: 33157 RVA: 0x0036ED5A File Offset: 0x0036D15A
		public Dictionary<int, PoseKeyPair> ADVBreastTable
		{
			[CompilerGenerated]
			get
			{
				return this._advBreastTable;
			}
		}

		// Token: 0x17001A0F RID: 6671
		// (get) Token: 0x06008186 RID: 33158 RVA: 0x0036ED62 File Offset: 0x0036D162
		public Dictionary<int, PoseKeyPair> ADVBreastNoReactionTable
		{
			[CompilerGenerated]
			get
			{
				return this._advBreastNoReactionTable;
			}
		}

		// Token: 0x17001A10 RID: 6672
		// (get) Token: 0x06008187 RID: 33159 RVA: 0x0036ED6A File Offset: 0x0036D16A
		public AgentProfile.NormalSkillIDDefines NormalSkillIDSetting
		{
			[CompilerGenerated]
			get
			{
				return this._normalSkillIDSetting;
			}
		}

		// Token: 0x17001A11 RID: 6673
		// (get) Token: 0x06008188 RID: 33160 RVA: 0x0036ED72 File Offset: 0x0036D172
		public AgentProfile.HSkillIDDefines HSkillIDSetting
		{
			[CompilerGenerated]
			get
			{
				return this._hSkillIDSetting;
			}
		}

		// Token: 0x17001A12 RID: 6674
		// (get) Token: 0x06008189 RID: 33161 RVA: 0x0036ED7A File Offset: 0x0036D17A
		public float TurnMinAngle
		{
			[CompilerGenerated]
			get
			{
				return this._turnMinAngle;
			}
		}

		// Token: 0x17001A13 RID: 6675
		// (get) Token: 0x0600818A RID: 33162 RVA: 0x0036ED82 File Offset: 0x0036D182
		public AgentProfile.RangeParameter RangeSetting
		{
			[CompilerGenerated]
			get
			{
				return this._rangeSetting;
			}
		}

		// Token: 0x17001A14 RID: 6676
		// (get) Token: 0x0600818B RID: 33163 RVA: 0x0036ED8A File Offset: 0x0036D18A
		public AgentProfile.PhotoShotRangeParameter PhotoShotRangeSetting
		{
			[CompilerGenerated]
			get
			{
				return this._photoShotRangeSetting;
			}
		}

		// Token: 0x17001A15 RID: 6677
		// (get) Token: 0x0600818C RID: 33164 RVA: 0x0036ED92 File Offset: 0x0036D192
		public AgentProfile.ActionPointSightSetting ActionPointSight
		{
			[CompilerGenerated]
			get
			{
				return this._actionPointSight;
			}
		}

		// Token: 0x17001A16 RID: 6678
		// (get) Token: 0x0600818D RID: 33165 RVA: 0x0036ED9A File Offset: 0x0036D19A
		public int DefaultRelationShip
		{
			[CompilerGenerated]
			get
			{
				return this._defaultRelationShip;
			}
		}

		// Token: 0x17001A17 RID: 6679
		// (get) Token: 0x0600818E RID: 33166 RVA: 0x0036EDA2 File Offset: 0x0036D1A2
		public float HSampleDistance
		{
			[CompilerGenerated]
			get
			{
				return this._hSampleDistance;
			}
		}

		// Token: 0x17001A18 RID: 6680
		// (get) Token: 0x0600818F RID: 33167 RVA: 0x0036EDAA File Offset: 0x0036D1AA
		public AgentProfile.SightSetting CharacterFarSight
		{
			[CompilerGenerated]
			get
			{
				return this._characterFarSight;
			}
		}

		// Token: 0x17001A19 RID: 6681
		// (get) Token: 0x06008190 RID: 33168 RVA: 0x0036EDB2 File Offset: 0x0036D1B2
		public AgentProfile.SightSetting CharacterNearSight
		{
			[CompilerGenerated]
			get
			{
				return this._characterNearSight;
			}
		}

		// Token: 0x17001A1A RID: 6682
		// (get) Token: 0x06008191 RID: 33169 RVA: 0x0036EDBA File Offset: 0x0036D1BA
		public AgentProfile.SightSetting AnimalSight
		{
			[CompilerGenerated]
			get
			{
				return this._animalSight;
			}
		}

		// Token: 0x17001A1B RID: 6683
		// (get) Token: 0x06008192 RID: 33170 RVA: 0x0036EDC2 File Offset: 0x0036D1C2
		public float DurationCTForCall
		{
			[CompilerGenerated]
			get
			{
				return this._durationCTForCall;
			}
		}

		// Token: 0x17001A1C RID: 6684
		// (get) Token: 0x06008193 RID: 33171 RVA: 0x0036EDCA File Offset: 0x0036D1CA
		public float TalkLockDuration
		{
			[CompilerGenerated]
			get
			{
				return this._talkLockDuration;
			}
		}

		// Token: 0x17001A1D RID: 6685
		// (get) Token: 0x06008194 RID: 33172 RVA: 0x0036EDD2 File Offset: 0x0036D1D2
		public AgentProfile.DiminuationRates DiminuationRate
		{
			[CompilerGenerated]
			get
			{
				return this._diminuationRate;
			}
		}

		// Token: 0x17001A1E RID: 6686
		// (get) Token: 0x06008195 RID: 33173 RVA: 0x0036EDDA File Offset: 0x0036D1DA
		public Dictionary<int, AgentProfile.DiminuationRates> DiminMotivationRate
		{
			[CompilerGenerated]
			get
			{
				return this._diminMotivationRate;
			}
		}

		// Token: 0x17001A1F RID: 6687
		// (get) Token: 0x06008196 RID: 33174 RVA: 0x0036EDE2 File Offset: 0x0036D1E2
		public AgentProfile.DiminuationRates WeaknessDiminuationRate
		{
			[CompilerGenerated]
			get
			{
				return this._weaknessDiminuationRate;
			}
		}

		// Token: 0x17001A20 RID: 6688
		// (get) Token: 0x06008197 RID: 33175 RVA: 0x0036EDEA File Offset: 0x0036D1EA
		public AgentProfile.DiminuationRates TalkMotivationDimRate
		{
			[CompilerGenerated]
			get
			{
				return this._talkMotivationDimRate;
			}
		}

		// Token: 0x17001A21 RID: 6689
		// (get) Token: 0x06008198 RID: 33176 RVA: 0x0036EDF2 File Offset: 0x0036D1F2
		public ThresholdInt SecondSleepDurationMinMax
		{
			[CompilerGenerated]
			get
			{
				return this._secondSleepDurationMinMax;
			}
		}

		// Token: 0x17001A22 RID: 6690
		// (get) Token: 0x06008199 RID: 33177 RVA: 0x0036EDFA File Offset: 0x0036D1FA
		public Threshold StandDurationMinMax
		{
			[CompilerGenerated]
			get
			{
				return this._standDurationMinMax;
			}
		}

		// Token: 0x17001A23 RID: 6691
		// (get) Token: 0x0600819A RID: 33178 RVA: 0x0036EE02 File Offset: 0x0036D202
		public ItemIDKeyPair[] CanStandEatItems
		{
			[CompilerGenerated]
			get
			{
				return this._canStandEatItems;
			}
		}

		// Token: 0x17001A24 RID: 6692
		// (get) Token: 0x0600819B RID: 33179 RVA: 0x0036EE0A File Offset: 0x0036D20A
		public ItemIDKeyPair[] LowerTempDrinkItems
		{
			[CompilerGenerated]
			get
			{
				return this._lowerTempDrinkItems;
			}
		}

		// Token: 0x17001A25 RID: 6693
		// (get) Token: 0x0600819C RID: 33180 RVA: 0x0036EE12 File Offset: 0x0036D212
		public ItemIDKeyPair[] RaiseTempDrinkItems
		{
			[CompilerGenerated]
			get
			{
				return this._raiseTempDrinkItems;
			}
		}

		// Token: 0x17001A26 RID: 6694
		// (get) Token: 0x0600819D RID: 33181 RVA: 0x0036EE1A File Offset: 0x0036D21A
		public ItemIDKeyPair CoconutDrinkID
		{
			[CompilerGenerated]
			get
			{
				return this._coconutDrinkID;
			}
		}

		// Token: 0x17001A27 RID: 6695
		// (get) Token: 0x0600819E RID: 33182 RVA: 0x0036EE22 File Offset: 0x0036D222
		public float ColdTempBorder
		{
			[CompilerGenerated]
			get
			{
				return this._coldTempBorder;
			}
		}

		// Token: 0x17001A28 RID: 6696
		// (get) Token: 0x0600819F RID: 33183 RVA: 0x0036EE2A File Offset: 0x0036D22A
		public float HotTempBorder
		{
			[CompilerGenerated]
			get
			{
				return this._hotTempBorder;
			}
		}

		// Token: 0x17001A29 RID: 6697
		// (get) Token: 0x060081A0 RID: 33184 RVA: 0x0036EE32 File Offset: 0x0036D232
		public Dictionary<int, float> MotivationMinValueTable
		{
			[CompilerGenerated]
			get
			{
				return this._motivationMinValueTable;
			}
		}

		// Token: 0x17001A2A RID: 6698
		// (get) Token: 0x060081A1 RID: 33185 RVA: 0x0036EE3A File Offset: 0x0036D23A
		public float ActiveMotivationBorder
		{
			[CompilerGenerated]
			get
			{
				return this._activeMotivationBorder;
			}
		}

		// Token: 0x17001A2B RID: 6699
		// (get) Token: 0x060081A2 RID: 33186 RVA: 0x0036EE42 File Offset: 0x0036D242
		public float MustRunMotivationPercent
		{
			[CompilerGenerated]
			get
			{
				return this._mustRunMotivationPercent;
			}
		}

		// Token: 0x17001A2C RID: 6700
		// (get) Token: 0x060081A3 RID: 33187 RVA: 0x0036EE4A File Offset: 0x0036D24A
		public Threshold DiminuationInMasturbation
		{
			[CompilerGenerated]
			get
			{
				return this._diminuationInMasturbation;
			}
		}

		// Token: 0x17001A2D RID: 6701
		// (get) Token: 0x060081A4 RID: 33188 RVA: 0x0036EE52 File Offset: 0x0036D252
		public Threshold DiminuationInLesbian
		{
			[CompilerGenerated]
			get
			{
				return this._diminuationInLesbian;
			}
		}

		// Token: 0x17001A2E RID: 6702
		// (get) Token: 0x060081A5 RID: 33189 RVA: 0x0036EE5A File Offset: 0x0036D25A
		public int ItemSlotMaxInInventory
		{
			[CompilerGenerated]
			get
			{
				return this._itemSlotMaxInInventory;
			}
		}

		// Token: 0x17001A2F RID: 6703
		// (get) Token: 0x060081A6 RID: 33190 RVA: 0x0036EE62 File Offset: 0x0036D262
		public int ItemSlotCountToItemBox
		{
			[CompilerGenerated]
			get
			{
				return this._itemSlotCountToItemBox;
			}
		}

		// Token: 0x17001A30 RID: 6704
		// (get) Token: 0x060081A7 RID: 33191 RVA: 0x0036EE6A File Offset: 0x0036D26A
		public InventoryFacadeViewer.ItemFilter[] PresentItemFilter
		{
			[CompilerGenerated]
			get
			{
				return this._presentItemFilter;
			}
		}

		// Token: 0x17001A31 RID: 6705
		// (get) Token: 0x060081A8 RID: 33192 RVA: 0x0036EE72 File Offset: 0x0036D272
		public ItemIDKeyPair[] MedicineNormalItemList
		{
			[CompilerGenerated]
			get
			{
				return this._medicineNormalItemList;
			}
		}

		// Token: 0x17001A32 RID: 6706
		// (get) Token: 0x060081A9 RID: 33193 RVA: 0x0036EE7A File Offset: 0x0036D27A
		public ItemIDKeyPair[] MedicineColdItemList
		{
			[CompilerGenerated]
			get
			{
				return this._medicineColdItemList;
			}
		}

		// Token: 0x17001A33 RID: 6707
		// (get) Token: 0x060081AA RID: 33194 RVA: 0x0036EE82 File Offset: 0x0036D282
		public ItemIDKeyPair[] MedicineHurtItemList
		{
			[CompilerGenerated]
			get
			{
				return this._medicineHurtItemList;
			}
		}

		// Token: 0x17001A34 RID: 6708
		// (get) Token: 0x060081AB RID: 33195 RVA: 0x0036EE8A File Offset: 0x0036D28A
		public ItemIDKeyPair[] MedicineStomachacheItemList
		{
			[CompilerGenerated]
			get
			{
				return this._medicineStomachacheItemList;
			}
		}

		// Token: 0x17001A35 RID: 6709
		// (get) Token: 0x060081AC RID: 33196 RVA: 0x0036EE92 File Offset: 0x0036D292
		public ItemIDKeyPair[] MedicineHeatStrokeItemList
		{
			[CompilerGenerated]
			get
			{
				return this._medicineHeatStrokeItemList;
			}
		}

		// Token: 0x17001A36 RID: 6710
		// (get) Token: 0x060081AD RID: 33197 RVA: 0x0036EE9A File Offset: 0x0036D29A
		public ItemIDKeyPair FeverReducerID
		{
			[CompilerGenerated]
			get
			{
				return this._feverReducerID;
			}
		}

		// Token: 0x17001A37 RID: 6711
		// (get) Token: 0x060081AE RID: 33198 RVA: 0x0036EEA2 File Offset: 0x0036D2A2
		public ItemIDKeyPair ColdMedicineID
		{
			[CompilerGenerated]
			get
			{
				return this._coldMedicineID;
			}
		}

		// Token: 0x17001A38 RID: 6712
		// (get) Token: 0x060081AF RID: 33199 RVA: 0x0036EEAA File Offset: 0x0036D2AA
		public ItemIDKeyPair StomachMedicineID
		{
			[CompilerGenerated]
			get
			{
				return this._stomachMeidicineID;
			}
		}

		// Token: 0x17001A39 RID: 6713
		// (get) Token: 0x060081B0 RID: 33200 RVA: 0x0036EEB2 File Offset: 0x0036D2B2
		public ItemIDKeyPair WetTowelID
		{
			[CompilerGenerated]
			get
			{
				return this._wetTowelID;
			}
		}

		// Token: 0x17001A3A RID: 6714
		// (get) Token: 0x060081B1 RID: 33201 RVA: 0x0036EEBA File Offset: 0x0036D2BA
		public Vector3 OffsetInDate
		{
			[CompilerGenerated]
			get
			{
				return this._offsetInDate;
			}
		}

		// Token: 0x060081B2 RID: 33202 RVA: 0x0036EEC4 File Offset: 0x0036D2C4
		public Vector3 GetOffsetInParty(float rate)
		{
			if (rate < 0.5f)
			{
				float t = Mathf.InverseLerp(0f, 0.5f, rate);
				return Vector3.Lerp(this._offsetInPartyS, this._offsetInPartyM, t);
			}
			float t2 = Mathf.InverseLerp(0.5f, 1f, rate);
			return Vector3.Lerp(this._offsetInPartyM, this._offsetInPartyL, t2);
		}

		// Token: 0x17001A3B RID: 6715
		// (get) Token: 0x060081B3 RID: 33203 RVA: 0x0036EF23 File Offset: 0x0036D323
		public Vector3 OffsetInPartyS
		{
			[CompilerGenerated]
			get
			{
				return this._offsetInPartyS;
			}
		}

		// Token: 0x17001A3C RID: 6716
		// (get) Token: 0x060081B4 RID: 33204 RVA: 0x0036EF2B File Offset: 0x0036D32B
		public Vector3 OffsetInPartyM
		{
			[CompilerGenerated]
			get
			{
				return this._offsetInPartyM;
			}
		}

		// Token: 0x17001A3D RID: 6717
		// (get) Token: 0x060081B5 RID: 33205 RVA: 0x0036EF33 File Offset: 0x0036D333
		public Vector3 OffsetInPartyL
		{
			[CompilerGenerated]
			get
			{
				return Vector3.zero;
			}
		}

		// Token: 0x17001A3E RID: 6718
		// (get) Token: 0x060081B6 RID: 33206 RVA: 0x0036EF3A File Offset: 0x0036D33A
		public float RestDistance
		{
			[CompilerGenerated]
			get
			{
				return this._restDistance;
			}
		}

		// Token: 0x17001A3F RID: 6719
		// (get) Token: 0x060081B7 RID: 33207 RVA: 0x0036EF42 File Offset: 0x0036D342
		public float RunDistance
		{
			[CompilerGenerated]
			get
			{
				return this._runDistance;
			}
		}

		// Token: 0x17001A40 RID: 6720
		// (get) Token: 0x060081B8 RID: 33208 RVA: 0x0036EF4A File Offset: 0x0036D34A
		public int ActionPointSightNavMeshArea
		{
			[CompilerGenerated]
			get
			{
				return this._actionPointSightNavMeshArea;
			}
		}

		// Token: 0x17001A41 RID: 6721
		// (get) Token: 0x060081B9 RID: 33209 RVA: 0x0036EF52 File Offset: 0x0036D352
		public float CatEventBaseProb
		{
			[CompilerGenerated]
			get
			{
				return this._catEventBaseProb;
			}
		}

		// Token: 0x17001A42 RID: 6722
		// (get) Token: 0x060081BA RID: 33210 RVA: 0x0036EF5A File Offset: 0x0036D35A
		public Dictionary<int, PoseKeyPair> TutorialIdlePoseTable
		{
			[CompilerGenerated]
			get
			{
				return this._tutorialIdlePoseTable;
			}
		}

		// Token: 0x17001A43 RID: 6723
		// (get) Token: 0x060081BB RID: 33211 RVA: 0x0036EF62 File Offset: 0x0036D362
		public Dictionary<int, PoseKeyPair> TutorialWakeUpPoseTable
		{
			[CompilerGenerated]
			get
			{
				return this._tutorialWakeUpPoseTable;
			}
		}

		// Token: 0x17001A44 RID: 6724
		// (get) Token: 0x060081BC RID: 33212 RVA: 0x0036EF6A File Offset: 0x0036D36A
		public AgentProfile.TutorialSetting Tutorial
		{
			[CompilerGenerated]
			get
			{
				return this._tutorial;
			}
		}

		// Token: 0x17001A45 RID: 6725
		// (get) Token: 0x060081BD RID: 33213 RVA: 0x0036EF72 File Offset: 0x0036D372
		public IReadOnlyDictionary<int, int> DicAnimatorID
		{
			[CompilerGenerated]
			get
			{
				return this._dicAnimatorID;
			}
		}

		// Token: 0x17001A46 RID: 6726
		// (get) Token: 0x060081BE RID: 33214 RVA: 0x0036EF7A File Offset: 0x0036D37A
		public IReadOnlyDictionary<int, PoseKeyPair> DicGoGhroughAnimID
		{
			[CompilerGenerated]
			get
			{
				return this._dicGoGhroughAnimID;
			}
		}

		// Token: 0x17001A47 RID: 6727
		// (get) Token: 0x060081BF RID: 33215 RVA: 0x0036EF82 File Offset: 0x0036D382
		public IReadOnlyDictionary<int, PoseKeyPair> DicThreeStepJumpAnimID
		{
			[CompilerGenerated]
			get
			{
				return this._dicThreeStepJumpAnimID;
			}
		}

		// Token: 0x17001A48 RID: 6728
		// (get) Token: 0x060081C0 RID: 33216 RVA: 0x0036EF8A File Offset: 0x0036D38A
		public IReadOnlyDictionary<int, int> DicDayElapseCheck
		{
			[CompilerGenerated]
			get
			{
				return this._dicDayElapseCheck;
			}
		}

		// Token: 0x17001A49 RID: 6729
		// (get) Token: 0x060081C1 RID: 33217 RVA: 0x0036EF92 File Offset: 0x0036D392
		public float DayElapseEventADVRate
		{
			[CompilerGenerated]
			get
			{
				return this._dayElapseEventADVRate;
			}
		}

		// Token: 0x17001A4A RID: 6730
		// (get) Token: 0x060081C2 RID: 33218 RVA: 0x0036EF9A File Offset: 0x0036D39A
		public ThresholdInt DayRandElapseCheck
		{
			[CompilerGenerated]
			get
			{
				return this._dayRandElapseCheck;
			}
		}

		// Token: 0x17001A4B RID: 6731
		// (get) Token: 0x060081C3 RID: 33219 RVA: 0x0036EFA2 File Offset: 0x0036D3A2
		public float YandereWarpPosCorrect
		{
			[CompilerGenerated]
			get
			{
				return this._yandereWarpPosCorrect;
			}
		}

		// Token: 0x17001A4C RID: 6732
		// (get) Token: 0x060081C4 RID: 33220 RVA: 0x0036EFAA File Offset: 0x0036D3AA
		public float YandereWarpProb
		{
			[CompilerGenerated]
			get
			{
				return this._yandereWarpProb;
			}
		}

		// Token: 0x17001A4D RID: 6733
		// (get) Token: 0x060081C5 RID: 33221 RVA: 0x0036EFB2 File Offset: 0x0036D3B2
		public float YandereWarpWaitTime
		{
			[CompilerGenerated]
			get
			{
				return this._yandereWarpWaitTime;
			}
		}

		// Token: 0x17001A4E RID: 6734
		// (get) Token: 0x060081C6 RID: 33222 RVA: 0x0036EFBA File Offset: 0x0036D3BA
		public float YandereWarpRetryReserveTime
		{
			[CompilerGenerated]
			get
			{
				return this._yandereWarpRetryReserveTime;
			}
		}

		// Token: 0x040067E7 RID: 26599
		private ReadOnlyDictionary<EventType, EventType> _afterActionReadOnlyTable;

		// Token: 0x040067E8 RID: 26600
		[SerializeField]
		private Dictionary<EventType, EventType> _afterActionTable = new Dictionary<EventType, EventType>();

		// Token: 0x040067E9 RID: 26601
		[SerializeField]
		private List<Desire.ActionType> _encounterWhitelist;

		// Token: 0x040067EA RID: 26602
		[SerializeField]
		private List<Desire.ActionType> _strollWhitelist = new List<Desire.ActionType>();

		// Token: 0x040067EB RID: 26603
		[SerializeField]
		private List<Desire.ActionType> _blackListInSaveAndLoad = new List<Desire.ActionType>();

		// Token: 0x040067EC RID: 26604
		[SerializeField]
		private Dictionary<int, int> _defaultAreaIDTable = new Dictionary<int, int>();

		// Token: 0x040067ED RID: 26605
		[SerializeField]
		private AgentProfile.WalkParameter _walkSetting = default(AgentProfile.WalkParameter);

		// Token: 0x040067EE RID: 26606
		[SerializeField]
		private int _avoidancePriorityDefault;

		// Token: 0x040067EF RID: 26607
		[SerializeField]
		private int _avoidancePriorityStationary;

		// Token: 0x040067F0 RID: 26608
		[SerializeField]
		private float _actionPointNearDistance = 10f;

		// Token: 0x040067F1 RID: 26609
		[SerializeField]
		private ThresholdInt _escapeViaPointNumThreshold = new ThresholdInt(1, 3);

		// Token: 0x040067F2 RID: 26610
		[SerializeField]
		private AgentProfile.PoseIDCollection _poseIDTable;

		// Token: 0x040067F3 RID: 26611
		[SerializeField]
		private Dictionary<int, PoseKeyPair> _advIdleTable = new Dictionary<int, PoseKeyPair>();

		// Token: 0x040067F4 RID: 26612
		[SerializeField]
		private Dictionary<int, PoseKeyPair> _advHouchiTable = new Dictionary<int, PoseKeyPair>();

		// Token: 0x040067F5 RID: 26613
		[SerializeField]
		private Dictionary<int, PoseKeyPair> _advLeaveTable = new Dictionary<int, PoseKeyPair>();

		// Token: 0x040067F6 RID: 26614
		[SerializeField]
		private Dictionary<int, PoseKeyPair> _advBreastTable = new Dictionary<int, PoseKeyPair>();

		// Token: 0x040067F7 RID: 26615
		[SerializeField]
		private Dictionary<int, PoseKeyPair> _advBreastNoReactionTable = new Dictionary<int, PoseKeyPair>();

		// Token: 0x040067F8 RID: 26616
		[SerializeField]
		private AgentProfile.NormalSkillIDDefines _normalSkillIDSetting = new AgentProfile.NormalSkillIDDefines();

		// Token: 0x040067F9 RID: 26617
		[SerializeField]
		private AgentProfile.HSkillIDDefines _hSkillIDSetting = default(AgentProfile.HSkillIDDefines);

		// Token: 0x040067FA RID: 26618
		[SerializeField]
		private float _turnMinAngle;

		// Token: 0x040067FB RID: 26619
		[SerializeField]
		private AgentProfile.RangeParameter _rangeSetting = default(AgentProfile.RangeParameter);

		// Token: 0x040067FC RID: 26620
		[SerializeField]
		private AgentProfile.PhotoShotRangeParameter _photoShotRangeSetting = default(AgentProfile.PhotoShotRangeParameter);

		// Token: 0x040067FD RID: 26621
		[SerializeField]
		private AgentProfile.ActionPointSightSetting _actionPointSight = new AgentProfile.ActionPointSightSetting();

		// Token: 0x040067FE RID: 26622
		[SerializeField]
		private int _defaultRelationShip = 50;

		// Token: 0x040067FF RID: 26623
		[SerializeField]
		private float _hSampleDistance = 50f;

		// Token: 0x04006800 RID: 26624
		[SerializeField]
		private AgentProfile.SightSetting _characterFarSight = new AgentProfile.SightSetting();

		// Token: 0x04006801 RID: 26625
		[SerializeField]
		private AgentProfile.SightSetting _characterNearSight = new AgentProfile.SightSetting();

		// Token: 0x04006802 RID: 26626
		[SerializeField]
		private AgentProfile.SightSetting _animalSight = new AgentProfile.SightSetting();

		// Token: 0x04006803 RID: 26627
		[SerializeField]
		private float _durationCTForCall = 9999999f;

		// Token: 0x04006804 RID: 26628
		[SerializeField]
		private float _talkLockDuration = 9999999f;

		// Token: 0x04006805 RID: 26629
		[SerializeField]
		private AgentProfile.DiminuationRates _diminuationRate = new AgentProfile.DiminuationRates(1f, 2f);

		// Token: 0x04006806 RID: 26630
		[SerializeField]
		private Dictionary<int, AgentProfile.DiminuationRates> _diminMotivationRate = Enum.GetValues(typeof(Desire.Type)).Cast<Desire.Type>().Select((Desire.Type _, int idx) => idx).ToDictionary((int x) => x, (int x) => new AgentProfile.DiminuationRates(1f, 2f));

		// Token: 0x04006807 RID: 26631
		[SerializeField]
		private AgentProfile.DiminuationRates _weaknessDiminuationRate = new AgentProfile.DiminuationRates(1f, 2f);

		// Token: 0x04006808 RID: 26632
		[SerializeField]
		private AgentProfile.DiminuationRates _talkMotivationDimRate = new AgentProfile.DiminuationRates(1f, 2f);

		// Token: 0x04006809 RID: 26633
		[SerializeField]
		private ThresholdInt _secondSleepDurationMinMax = new ThresholdInt(30, 60);

		// Token: 0x0400680A RID: 26634
		[SerializeField]
		private Threshold _standDurationMinMax = new Threshold(1f, 3f);

		// Token: 0x0400680B RID: 26635
		[SerializeField]
		private ItemIDKeyPair[] _canStandEatItems;

		// Token: 0x0400680C RID: 26636
		[SerializeField]
		private ItemIDKeyPair[] _lowerTempDrinkItems;

		// Token: 0x0400680D RID: 26637
		[SerializeField]
		private ItemIDKeyPair[] _raiseTempDrinkItems;

		// Token: 0x0400680E RID: 26638
		[SerializeField]
		private ItemIDKeyPair _coconutDrinkID = default(ItemIDKeyPair);

		// Token: 0x0400680F RID: 26639
		[SerializeField]
		private float _coldTempBorder = 20f;

		// Token: 0x04006810 RID: 26640
		[SerializeField]
		private float _hotTempBorder = 80f;

		// Token: 0x04006811 RID: 26641
		[SerializeField]
		private Dictionary<int, float> _motivationMinValueTable = new Dictionary<int, float>();

		// Token: 0x04006812 RID: 26642
		[SerializeField]
		private float _activeMotivationBorder = 20f;

		// Token: 0x04006813 RID: 26643
		[SerializeField]
		[Range(0f, 1f)]
		private float _mustRunMotivationPercent = 50f;

		// Token: 0x04006814 RID: 26644
		[SerializeField]
		private Threshold _diminuationInMasturbation;

		// Token: 0x04006815 RID: 26645
		[SerializeField]
		private Threshold _diminuationInLesbian;

		// Token: 0x04006816 RID: 26646
		[SerializeField]
		private int _itemSlotMaxInInventory = 1;

		// Token: 0x04006817 RID: 26647
		[SerializeField]
		private int _itemSlotCountToItemBox = 1;

		// Token: 0x04006818 RID: 26648
		[SerializeField]
		private InventoryFacadeViewer.ItemFilter[] _presentItemFilter;

		// Token: 0x04006819 RID: 26649
		[SerializeField]
		private ItemIDKeyPair[] _medicineNormalItemList;

		// Token: 0x0400681A RID: 26650
		[SerializeField]
		private ItemIDKeyPair[] _medicineColdItemList;

		// Token: 0x0400681B RID: 26651
		[SerializeField]
		private ItemIDKeyPair[] _medicineHurtItemList;

		// Token: 0x0400681C RID: 26652
		[SerializeField]
		private ItemIDKeyPair[] _medicineStomachacheItemList;

		// Token: 0x0400681D RID: 26653
		[SerializeField]
		private ItemIDKeyPair[] _medicineHeatStrokeItemList;

		// Token: 0x0400681E RID: 26654
		[SerializeField]
		private ItemIDKeyPair _feverReducerID = default(ItemIDKeyPair);

		// Token: 0x0400681F RID: 26655
		[SerializeField]
		private ItemIDKeyPair _coldMedicineID = default(ItemIDKeyPair);

		// Token: 0x04006820 RID: 26656
		[SerializeField]
		private ItemIDKeyPair _stomachMeidicineID = default(ItemIDKeyPair);

		// Token: 0x04006821 RID: 26657
		[SerializeField]
		private ItemIDKeyPair _wetTowelID = default(ItemIDKeyPair);

		// Token: 0x04006822 RID: 26658
		[SerializeField]
		private Vector3 _offsetInDate = Vector3.zero;

		// Token: 0x04006823 RID: 26659
		[SerializeField]
		private Vector3 _offsetInPartyS = Vector3.zero;

		// Token: 0x04006824 RID: 26660
		[SerializeField]
		private Vector3 _offsetInPartyM = Vector3.zero;

		// Token: 0x04006825 RID: 26661
		[SerializeField]
		private Vector3 _offsetInPartyL = Vector3.zero;

		// Token: 0x04006826 RID: 26662
		[SerializeField]
		private float _restDistance = 1f;

		// Token: 0x04006827 RID: 26663
		[SerializeField]
		private float _runDistance = 10f;

		// Token: 0x04006828 RID: 26664
		[NavMeshAreaEnumMask]
		[SerializeField]
		private int _actionPointSightNavMeshArea;

		// Token: 0x04006829 RID: 26665
		[SerializeField]
		private float _catEventBaseProb = 1f;

		// Token: 0x0400682A RID: 26666
		[SerializeField]
		private Dictionary<int, PoseKeyPair> _tutorialIdlePoseTable = new Dictionary<int, PoseKeyPair>();

		// Token: 0x0400682B RID: 26667
		[SerializeField]
		private Dictionary<int, PoseKeyPair> _tutorialWakeUpPoseTable = new Dictionary<int, PoseKeyPair>();

		// Token: 0x0400682C RID: 26668
		[SerializeField]
		private AgentProfile.TutorialSetting _tutorial;

		// Token: 0x0400682D RID: 26669
		[SerializeField]
		private Dictionary<int, int> _dicAnimatorID = new Dictionary<int, int>();

		// Token: 0x0400682E RID: 26670
		[SerializeField]
		private Dictionary<int, PoseKeyPair> _dicGoGhroughAnimID = new Dictionary<int, PoseKeyPair>();

		// Token: 0x0400682F RID: 26671
		[SerializeField]
		private Dictionary<int, PoseKeyPair> _dicThreeStepJumpAnimID = new Dictionary<int, PoseKeyPair>();

		// Token: 0x04006830 RID: 26672
		[SerializeField]
		private Dictionary<int, int> _dicDayElapseCheck = new Dictionary<int, int>();

		// Token: 0x04006831 RID: 26673
		[SerializeField]
		private float _dayElapseEventADVRate;

		// Token: 0x04006832 RID: 26674
		[SerializeField]
		private ThresholdInt _dayRandElapseCheck = new ThresholdInt(1, 2);

		// Token: 0x04006833 RID: 26675
		[SerializeField]
		private float _yandereWarpPosCorrect = 1f;

		// Token: 0x04006834 RID: 26676
		[SerializeField]
		private float _yandereWarpProb;

		// Token: 0x04006835 RID: 26677
		[SerializeField]
		private float _yandereWarpWaitTime;

		// Token: 0x04006836 RID: 26678
		[SerializeField]
		private float _yandereWarpRetryReserveTime;

		// Token: 0x02000F48 RID: 3912
		[Serializable]
		public class NormalSkillIDDefines
		{
		}

		// Token: 0x02000F49 RID: 3913
		[Serializable]
		public struct HSkillIDDefines
		{
			// Token: 0x0400683A RID: 26682
			public int homosexualID;

			// Token: 0x0400683B RID: 26683
			public int groperID;
		}

		// Token: 0x02000F4A RID: 3914
		[Serializable]
		public class PoseIDCollection
		{
			// Token: 0x17001A4F RID: 6735
			// (get) Token: 0x060081CC RID: 33228 RVA: 0x0036F2F3 File Offset: 0x0036D6F3
			public int LocomotionID
			{
				[CompilerGenerated]
				get
				{
					return this._locomotionID;
				}
			}

			// Token: 0x17001A50 RID: 6736
			// (get) Token: 0x060081CD RID: 33229 RVA: 0x0036F2FB File Offset: 0x0036D6FB
			public int WalkLocoID
			{
				[CompilerGenerated]
				get
				{
					return this._walkLocoID;
				}
			}

			// Token: 0x17001A51 RID: 6737
			// (get) Token: 0x060081CE RID: 33230 RVA: 0x0036F303 File Offset: 0x0036D703
			public int UmbrellaLocoID
			{
				[CompilerGenerated]
				get
				{
					return this._umbrellaLocoID;
				}
			}

			// Token: 0x17001A52 RID: 6738
			// (get) Token: 0x060081CF RID: 33231 RVA: 0x0036F30B File Offset: 0x0036D70B
			public int LampLocoID
			{
				[CompilerGenerated]
				get
				{
					return this._lampLocoID;
				}
			}

			// Token: 0x17001A53 RID: 6739
			// (get) Token: 0x060081D0 RID: 33232 RVA: 0x0036F313 File Offset: 0x0036D713
			public int LampWalkLocoID
			{
				[CompilerGenerated]
				get
				{
					return this._lampWalkLocoID;
				}
			}

			// Token: 0x17001A54 RID: 6740
			// (get) Token: 0x060081D1 RID: 33233 RVA: 0x0036F31B File Offset: 0x0036D71B
			public int HurtLocoID
			{
				[CompilerGenerated]
				get
				{
					return this._hurtLocoID;
				}
			}

			// Token: 0x17001A55 RID: 6741
			// (get) Token: 0x060081D2 RID: 33234 RVA: 0x0036F323 File Offset: 0x0036D723
			public int MojimojiLocoID
			{
				[CompilerGenerated]
				get
				{
					return this._mojimojiLocoID;
				}
			}

			// Token: 0x17001A56 RID: 6742
			// (get) Token: 0x060081D3 RID: 33235 RVA: 0x0036F32B File Offset: 0x0036D72B
			public int RainRunLocoID
			{
				[CompilerGenerated]
				get
				{
					return this._rainRunLocoID;
				}
			}

			// Token: 0x17001A57 RID: 6743
			// (get) Token: 0x060081D4 RID: 33236 RVA: 0x0036F333 File Offset: 0x0036D733
			public int CookMoveLocoID
			{
				[CompilerGenerated]
				get
				{
					return this._cookMoveLocoID;
				}
			}

			// Token: 0x17001A58 RID: 6744
			// (get) Token: 0x060081D5 RID: 33237 RVA: 0x0036F33B File Offset: 0x0036D73B
			public PoseKeyPair FaintID
			{
				[CompilerGenerated]
				get
				{
					return this._faintID;
				}
			}

			// Token: 0x17001A59 RID: 6745
			// (get) Token: 0x060081D6 RID: 33238 RVA: 0x0036F343 File Offset: 0x0036D743
			public PoseKeyPair CollapseID
			{
				[CompilerGenerated]
				get
				{
					return this._collapseID;
				}
			}

			// Token: 0x17001A5A RID: 6746
			// (get) Token: 0x060081D7 RID: 33239 RVA: 0x0036F34B File Offset: 0x0036D74B
			public PoseKeyPair ComaID
			{
				[CompilerGenerated]
				get
				{
					return this._comaID;
				}
			}

			// Token: 0x17001A5B RID: 6747
			// (get) Token: 0x060081D8 RID: 33240 RVA: 0x0036F353 File Offset: 0x0036D753
			public PoseKeyPair MedicID
			{
				[CompilerGenerated]
				get
				{
					return this._medicID;
				}
			}

			// Token: 0x17001A5C RID: 6748
			// (get) Token: 0x060081D9 RID: 33241 RVA: 0x0036F35B File Offset: 0x0036D75B
			public PoseKeyPair CureID
			{
				[CompilerGenerated]
				get
				{
					return this._cureID;
				}
			}

			// Token: 0x17001A5D RID: 6749
			// (get) Token: 0x060081DA RID: 33242 RVA: 0x0036F363 File Offset: 0x0036D763
			public PoseKeyPair WeaknessID
			{
				[CompilerGenerated]
				get
				{
					return this._weaknessID;
				}
			}

			// Token: 0x17001A5E RID: 6750
			// (get) Token: 0x060081DB RID: 33243 RVA: 0x0036F36B File Offset: 0x0036D76B
			public PoseKeyPair GreetPoseID
			{
				[CompilerGenerated]
				get
				{
					return this._greetPoseID;
				}
			}

			// Token: 0x17001A5F RID: 6751
			// (get) Token: 0x060081DC RID: 33244 RVA: 0x0036F373 File Offset: 0x0036D773
			public PoseKeyPair[] NormalIDList
			{
				[CompilerGenerated]
				get
				{
					return this._normalIDList;
				}
			}

			// Token: 0x17001A60 RID: 6752
			// (get) Token: 0x060081DD RID: 33245 RVA: 0x0036F37B File Offset: 0x0036D77B
			public PoseKeyPair ColdPoseID
			{
				[CompilerGenerated]
				get
				{
					return this._coldPoseID;
				}
			}

			// Token: 0x17001A61 RID: 6753
			// (get) Token: 0x060081DE RID: 33246 RVA: 0x0036F383 File Offset: 0x0036D783
			public PoseKeyPair HotPoseID
			{
				[CompilerGenerated]
				get
				{
					return this._hotPoseID;
				}
			}

			// Token: 0x17001A62 RID: 6754
			// (get) Token: 0x060081DF RID: 33247 RVA: 0x0036F38B File Offset: 0x0036D78B
			public PoseKeyPair CoughID
			{
				[CompilerGenerated]
				get
				{
					return this._coughID;
				}
			}

			// Token: 0x17001A63 RID: 6755
			// (get) Token: 0x060081E0 RID: 33248 RVA: 0x0036F393 File Offset: 0x0036D793
			public PoseKeyPair GrossID
			{
				[CompilerGenerated]
				get
				{
					return this._grossID;
				}
			}

			// Token: 0x17001A64 RID: 6756
			// (get) Token: 0x060081E1 RID: 33249 RVA: 0x0036F39B File Offset: 0x0036D79B
			public PoseKeyPair YawnID
			{
				[CompilerGenerated]
				get
				{
					return this._yawnID;
				}
			}

			// Token: 0x17001A65 RID: 6757
			// (get) Token: 0x060081E2 RID: 33250 RVA: 0x0036F3A3 File Offset: 0x0036D7A3
			public PoseKeyPair WakeUpID
			{
				[CompilerGenerated]
				get
				{
					return this._wakeUpID;
				}
			}

			// Token: 0x17001A66 RID: 6758
			// (get) Token: 0x060081E3 RID: 33251 RVA: 0x0036F3AB File Offset: 0x0036D7AB
			public PoseKeyPair FearID
			{
				[CompilerGenerated]
				get
				{
					return this._fearID;
				}
			}

			// Token: 0x17001A67 RID: 6759
			// (get) Token: 0x060081E4 RID: 33252 RVA: 0x0036F3B3 File Offset: 0x0036D7B3
			public PoseKeyPair ChuckleID
			{
				[CompilerGenerated]
				get
				{
					return this._chuckleID;
				}
			}

			// Token: 0x17001A68 RID: 6760
			// (get) Token: 0x060081E5 RID: 33253 RVA: 0x0036F3BB File Offset: 0x0036D7BB
			public PoseKeyPair StandHurtID
			{
				[CompilerGenerated]
				get
				{
					return this._standHurtID;
				}
			}

			// Token: 0x17001A69 RID: 6761
			// (get) Token: 0x060081E6 RID: 33254 RVA: 0x0036F3C3 File Offset: 0x0036D7C3
			public PoseKeyPair SurprisedID
			{
				[CompilerGenerated]
				get
				{
					return this._surprisedID;
				}
			}

			// Token: 0x17001A6A RID: 6762
			// (get) Token: 0x060081E7 RID: 33255 RVA: 0x0036F3CB File Offset: 0x0036D7CB
			public PoseKeyPair DeepBreathID
			{
				[CompilerGenerated]
				get
				{
					return this._deepBreathID;
				}
			}

			// Token: 0x17001A6B RID: 6763
			// (get) Token: 0x060081E8 RID: 33256 RVA: 0x0036F3D3 File Offset: 0x0036D7D3
			public PoseKeyPair WakenUpID
			{
				[CompilerGenerated]
				get
				{
					return this._wakenUpID;
				}
			}

			// Token: 0x17001A6C RID: 6764
			// (get) Token: 0x060081E9 RID: 33257 RVA: 0x0036F3DB File Offset: 0x0036D7DB
			public PoseKeyPair SurpriseMasturbationID
			{
				[CompilerGenerated]
				get
				{
					return this._surpriseMasturbationID;
				}
			}

			// Token: 0x17001A6D RID: 6765
			// (get) Token: 0x060081EA RID: 33258 RVA: 0x0036F3E3 File Offset: 0x0036D7E3
			public PoseKeyPair SurpriseInToiletSquatID
			{
				[CompilerGenerated]
				get
				{
					return this._surpriseInToiletSquatID;
				}
			}

			// Token: 0x17001A6E RID: 6766
			// (get) Token: 0x060081EB RID: 33259 RVA: 0x0036F3EB File Offset: 0x0036D7EB
			public PoseKeyPair SurpriseInToiletSitID
			{
				[CompilerGenerated]
				get
				{
					return this._surpriseInToiletSitID;
				}
			}

			// Token: 0x17001A6F RID: 6767
			// (get) Token: 0x060081EC RID: 33260 RVA: 0x0036F3F3 File Offset: 0x0036D7F3
			public PoseKeyPair SurpriseInBathStandID
			{
				[CompilerGenerated]
				get
				{
					return this._surpriseInBathStandID;
				}
			}

			// Token: 0x17001A70 RID: 6768
			// (get) Token: 0x060081ED RID: 33261 RVA: 0x0036F3FB File Offset: 0x0036D7FB
			public PoseKeyPair SurpriseInBathSitID
			{
				[CompilerGenerated]
				get
				{
					return this._surpriseInBathSitID;
				}
			}

			// Token: 0x17001A71 RID: 6769
			// (get) Token: 0x060081EE RID: 33262 RVA: 0x0036F403 File Offset: 0x0036D803
			public PoseKeyPair SurpriseInGoemonID
			{
				[CompilerGenerated]
				get
				{
					return this._surpriseInGoemonID;
				}
			}

			// Token: 0x17001A72 RID: 6770
			// (get) Token: 0x060081EF RID: 33263 RVA: 0x0036F40B File Offset: 0x0036D80B
			public PoseKeyPair ChairCoughID
			{
				[CompilerGenerated]
				get
				{
					return this._chairCoughID;
				}
			}

			// Token: 0x17001A73 RID: 6771
			// (get) Token: 0x060081F0 RID: 33264 RVA: 0x0036F413 File Offset: 0x0036D813
			public PoseKeyPair SquatFearID
			{
				[CompilerGenerated]
				get
				{
					return this._squatFearID;
				}
			}

			// Token: 0x17001A74 RID: 6772
			// (get) Token: 0x060081F1 RID: 33265 RVA: 0x0036F41B File Offset: 0x0036D81B
			public PoseKeyPair GroomyID
			{
				[CompilerGenerated]
				get
				{
					return this._groomyID;
				}
			}

			// Token: 0x17001A75 RID: 6773
			// (get) Token: 0x060081F2 RID: 33266 RVA: 0x0036F423 File Offset: 0x0036D823
			public PoseKeyPair AngryID
			{
				[CompilerGenerated]
				get
				{
					return this._angryID;
				}
			}

			// Token: 0x17001A76 RID: 6774
			// (get) Token: 0x060081F3 RID: 33267 RVA: 0x0036F42B File Offset: 0x0036D82B
			public PoseKeyPair HungryID
			{
				[CompilerGenerated]
				get
				{
					return this._hungryID;
				}
			}

			// Token: 0x17001A77 RID: 6775
			// (get) Token: 0x060081F4 RID: 33268 RVA: 0x0036F433 File Offset: 0x0036D833
			public PoseKeyPair OvationID
			{
				[CompilerGenerated]
				get
				{
					return this._ovationID;
				}
			}

			// Token: 0x17001A78 RID: 6776
			// (get) Token: 0x060081F5 RID: 33269 RVA: 0x0036F43B File Offset: 0x0036D83B
			public PoseKeyPair[] PlayGameStandIDList
			{
				[CompilerGenerated]
				get
				{
					return this._playGameStandIDList;
				}
			}

			// Token: 0x17001A79 RID: 6777
			// (get) Token: 0x060081F6 RID: 33270 RVA: 0x0036F443 File Offset: 0x0036D843
			public PoseKeyPair[] PlayGameStandOutdoorIDList
			{
				[CompilerGenerated]
				get
				{
					return this._playGameStandOutdoorIDList;
				}
			}

			// Token: 0x17001A7A RID: 6778
			// (get) Token: 0x060081F7 RID: 33271 RVA: 0x0036F44B File Offset: 0x0036D84B
			public PoseKeyPair RainUmbrellaID
			{
				[CompilerGenerated]
				get
				{
					return this._rainUmbrellaID;
				}
			}

			// Token: 0x17001A7B RID: 6779
			// (get) Token: 0x060081F8 RID: 33272 RVA: 0x0036F453 File Offset: 0x0036D853
			public PoseKeyPair ClearPoseID
			{
				[CompilerGenerated]
				get
				{
					return this._clearPoseID;
				}
			}

			// Token: 0x17001A7C RID: 6780
			// (get) Token: 0x060081F9 RID: 33273 RVA: 0x0036F45B File Offset: 0x0036D85B
			public PoseKeyPair EatStandID
			{
				[CompilerGenerated]
				get
				{
					return this._eatStandID;
				}
			}

			// Token: 0x17001A7D RID: 6781
			// (get) Token: 0x060081FA RID: 33274 RVA: 0x0036F463 File Offset: 0x0036D863
			public PoseKeyPair EatChairID
			{
				[CompilerGenerated]
				get
				{
					return this._eatChairID;
				}
			}

			// Token: 0x17001A7E RID: 6782
			// (get) Token: 0x060081FB RID: 33275 RVA: 0x0036F46B File Offset: 0x0036D86B
			public PoseKeyPair EatDeskID
			{
				[CompilerGenerated]
				get
				{
					return this._eatDeskID;
				}
			}

			// Token: 0x17001A7F RID: 6783
			// (get) Token: 0x060081FC RID: 33276 RVA: 0x0036F473 File Offset: 0x0036D873
			public PoseKeyPair EatDishID
			{
				[CompilerGenerated]
				get
				{
					return this._eatDishID;
				}
			}

			// Token: 0x17001A80 RID: 6784
			// (get) Token: 0x060081FD RID: 33277 RVA: 0x0036F47B File Offset: 0x0036D87B
			public PoseKeyPair SnitchFoodID
			{
				[CompilerGenerated]
				get
				{
					return this._snitchFoodID;
				}
			}

			// Token: 0x17001A81 RID: 6785
			// (get) Token: 0x060081FE RID: 33278 RVA: 0x0036F483 File Offset: 0x0036D883
			public PoseKeyPair StealFoundID
			{
				[CompilerGenerated]
				get
				{
					return this._stealFoundID;
				}
			}

			// Token: 0x17001A82 RID: 6786
			// (get) Token: 0x060081FF RID: 33279 RVA: 0x0036F48B File Offset: 0x0036D88B
			public PoseKeyPair DrinkStandID
			{
				[CompilerGenerated]
				get
				{
					return this._drinkStandID;
				}
			}

			// Token: 0x17001A83 RID: 6787
			// (get) Token: 0x06008200 RID: 33280 RVA: 0x0036F493 File Offset: 0x0036D893
			public PoseKeyPair DrinkChairID
			{
				[CompilerGenerated]
				get
				{
					return this._drinkChairID;
				}
			}

			// Token: 0x17001A84 RID: 6788
			// (get) Token: 0x06008201 RID: 33281 RVA: 0x0036F49B File Offset: 0x0036D89B
			public PoseKeyPair PeeID
			{
				[CompilerGenerated]
				get
				{
					return this._peeID;
				}
			}

			// Token: 0x17001A85 RID: 6789
			// (get) Token: 0x06008202 RID: 33282 RVA: 0x0036F4A3 File Offset: 0x0036D8A3
			public PoseKeyPair SleepTogetherRight
			{
				[CompilerGenerated]
				get
				{
					return this._sleepTogetherRight;
				}
			}

			// Token: 0x17001A86 RID: 6790
			// (get) Token: 0x06008203 RID: 33283 RVA: 0x0036F4AB File Offset: 0x0036D8AB
			public PoseKeyPair SleepTogetherLeft
			{
				[CompilerGenerated]
				get
				{
					return this._sleepTogetherLeft;
				}
			}

			// Token: 0x17001A87 RID: 6791
			// (get) Token: 0x06008204 RID: 33284 RVA: 0x0036F4B3 File Offset: 0x0036D8B3
			public PoseKeyPair WateringID
			{
				[CompilerGenerated]
				get
				{
					return this._wateringID;
				}
			}

			// Token: 0x17001A88 RID: 6792
			// (get) Token: 0x06008205 RID: 33285 RVA: 0x0036F4BB File Offset: 0x0036D8BB
			public PoseKeyPair FaceWash
			{
				[CompilerGenerated]
				get
				{
					return this._faceWash;
				}
			}

			// Token: 0x17001A89 RID: 6793
			// (get) Token: 0x06008206 RID: 33286 RVA: 0x0036F4C3 File Offset: 0x0036D8C3
			public PoseKeyPair HandWash
			{
				[CompilerGenerated]
				get
				{
					return this._handWash;
				}
			}

			// Token: 0x17001A8A RID: 6794
			// (get) Token: 0x06008207 RID: 33287 RVA: 0x0036F4CB File Offset: 0x0036D8CB
			public PoseKeyPair Yobai
			{
				[CompilerGenerated]
				get
				{
					return this._yobai;
				}
			}

			// Token: 0x17001A8B RID: 6795
			// (get) Token: 0x06008208 RID: 33288 RVA: 0x0036F4D3 File Offset: 0x0036D8D3
			public PoseKeyPair[] AppearIDList
			{
				[CompilerGenerated]
				get
				{
					return this._appearIDList;
				}
			}

			// Token: 0x0400683C RID: 26684
			[SerializeField]
			private int _locomotionID;

			// Token: 0x0400683D RID: 26685
			[SerializeField]
			private int _walkLocoID;

			// Token: 0x0400683E RID: 26686
			[SerializeField]
			private int _umbrellaLocoID;

			// Token: 0x0400683F RID: 26687
			[SerializeField]
			private int _lampLocoID;

			// Token: 0x04006840 RID: 26688
			[SerializeField]
			private int _lampWalkLocoID;

			// Token: 0x04006841 RID: 26689
			[SerializeField]
			private int _hurtLocoID;

			// Token: 0x04006842 RID: 26690
			[SerializeField]
			private int _mojimojiLocoID;

			// Token: 0x04006843 RID: 26691
			[SerializeField]
			private int _rainRunLocoID;

			// Token: 0x04006844 RID: 26692
			[SerializeField]
			private int _cookMoveLocoID;

			// Token: 0x04006845 RID: 26693
			[SerializeField]
			private PoseKeyPair _faintID = default(PoseKeyPair);

			// Token: 0x04006846 RID: 26694
			[SerializeField]
			private PoseKeyPair _collapseID = default(PoseKeyPair);

			// Token: 0x04006847 RID: 26695
			[SerializeField]
			private PoseKeyPair _comaID = default(PoseKeyPair);

			// Token: 0x04006848 RID: 26696
			[SerializeField]
			private PoseKeyPair _medicID = default(PoseKeyPair);

			// Token: 0x04006849 RID: 26697
			[SerializeField]
			private PoseKeyPair _cureID = default(PoseKeyPair);

			// Token: 0x0400684A RID: 26698
			[SerializeField]
			private PoseKeyPair _weaknessID = default(PoseKeyPair);

			// Token: 0x0400684B RID: 26699
			[SerializeField]
			private PoseKeyPair _greetPoseID = default(PoseKeyPair);

			// Token: 0x0400684C RID: 26700
			[SerializeField]
			private PoseKeyPair[] _normalIDList;

			// Token: 0x0400684D RID: 26701
			[SerializeField]
			private PoseKeyPair _coldPoseID = default(PoseKeyPair);

			// Token: 0x0400684E RID: 26702
			[SerializeField]
			private PoseKeyPair _hotPoseID = default(PoseKeyPair);

			// Token: 0x0400684F RID: 26703
			[SerializeField]
			private PoseKeyPair _coughID = default(PoseKeyPair);

			// Token: 0x04006850 RID: 26704
			[SerializeField]
			private PoseKeyPair _grossID = default(PoseKeyPair);

			// Token: 0x04006851 RID: 26705
			[SerializeField]
			private PoseKeyPair _yawnID = default(PoseKeyPair);

			// Token: 0x04006852 RID: 26706
			[SerializeField]
			private PoseKeyPair _wakeUpID = default(PoseKeyPair);

			// Token: 0x04006853 RID: 26707
			private PoseKeyPair _fearID = default(PoseKeyPair);

			// Token: 0x04006854 RID: 26708
			[SerializeField]
			private PoseKeyPair _chuckleID = default(PoseKeyPair);

			// Token: 0x04006855 RID: 26709
			[SerializeField]
			private PoseKeyPair _standHurtID = default(PoseKeyPair);

			// Token: 0x04006856 RID: 26710
			[SerializeField]
			private PoseKeyPair _surprisedID = default(PoseKeyPair);

			// Token: 0x04006857 RID: 26711
			[SerializeField]
			private PoseKeyPair _deepBreathID = default(PoseKeyPair);

			// Token: 0x04006858 RID: 26712
			[SerializeField]
			private PoseKeyPair _wakenUpID = default(PoseKeyPair);

			// Token: 0x04006859 RID: 26713
			[SerializeField]
			private PoseKeyPair _surpriseMasturbationID = default(PoseKeyPair);

			// Token: 0x0400685A RID: 26714
			[SerializeField]
			private PoseKeyPair _surpriseInToiletSquatID = default(PoseKeyPair);

			// Token: 0x0400685B RID: 26715
			[SerializeField]
			private PoseKeyPair _surpriseInToiletSitID = default(PoseKeyPair);

			// Token: 0x0400685C RID: 26716
			[SerializeField]
			private PoseKeyPair _surpriseInBathStandID = default(PoseKeyPair);

			// Token: 0x0400685D RID: 26717
			[SerializeField]
			private PoseKeyPair _surpriseInBathSitID = default(PoseKeyPair);

			// Token: 0x0400685E RID: 26718
			[SerializeField]
			private PoseKeyPair _surpriseInGoemonID = default(PoseKeyPair);

			// Token: 0x0400685F RID: 26719
			[SerializeField]
			private PoseKeyPair _chairCoughID = default(PoseKeyPair);

			// Token: 0x04006860 RID: 26720
			[SerializeField]
			[Header("しゃがみアクション指定")]
			[LabelText("しゃがみ怖がる")]
			private PoseKeyPair _squatFearID = default(PoseKeyPair);

			// Token: 0x04006861 RID: 26721
			[SerializeField]
			private PoseKeyPair _groomyID = default(PoseKeyPair);

			// Token: 0x04006862 RID: 26722
			[SerializeField]
			private PoseKeyPair _angryID = default(PoseKeyPair);

			// Token: 0x04006863 RID: 26723
			[SerializeField]
			private PoseKeyPair _hungryID = default(PoseKeyPair);

			// Token: 0x04006864 RID: 26724
			[SerializeField]
			private PoseKeyPair _ovationID = default(PoseKeyPair);

			// Token: 0x04006865 RID: 26725
			[SerializeField]
			private PoseKeyPair[] _playGameStandIDList;

			// Token: 0x04006866 RID: 26726
			[SerializeField]
			private PoseKeyPair[] _playGameStandOutdoorIDList;

			// Token: 0x04006867 RID: 26727
			[SerializeField]
			private PoseKeyPair _rainUmbrellaID = default(PoseKeyPair);

			// Token: 0x04006868 RID: 26728
			[SerializeField]
			private PoseKeyPair _clearPoseID = default(PoseKeyPair);

			// Token: 0x04006869 RID: 26729
			[SerializeField]
			private PoseKeyPair _eatStandID = default(PoseKeyPair);

			// Token: 0x0400686A RID: 26730
			[SerializeField]
			private PoseKeyPair _eatChairID = default(PoseKeyPair);

			// Token: 0x0400686B RID: 26731
			[SerializeField]
			private PoseKeyPair _eatDeskID = default(PoseKeyPair);

			// Token: 0x0400686C RID: 26732
			[SerializeField]
			private PoseKeyPair _eatDishID = default(PoseKeyPair);

			// Token: 0x0400686D RID: 26733
			[SerializeField]
			private PoseKeyPair _snitchFoodID = default(PoseKeyPair);

			// Token: 0x0400686E RID: 26734
			[SerializeField]
			private PoseKeyPair _stealFoundID = default(PoseKeyPair);

			// Token: 0x0400686F RID: 26735
			[SerializeField]
			private PoseKeyPair _drinkStandID = default(PoseKeyPair);

			// Token: 0x04006870 RID: 26736
			[SerializeField]
			private PoseKeyPair _drinkChairID = default(PoseKeyPair);

			// Token: 0x04006871 RID: 26737
			[SerializeField]
			private PoseKeyPair _peeID = default(PoseKeyPair);

			// Token: 0x04006872 RID: 26738
			[SerializeField]
			private PoseKeyPair _sleepTogetherRight = default(PoseKeyPair);

			// Token: 0x04006873 RID: 26739
			[SerializeField]
			private PoseKeyPair _sleepTogetherLeft = default(PoseKeyPair);

			// Token: 0x04006874 RID: 26740
			[SerializeField]
			private PoseKeyPair _wateringID = default(PoseKeyPair);

			// Token: 0x04006875 RID: 26741
			[SerializeField]
			private PoseKeyPair _faceWash = default(PoseKeyPair);

			// Token: 0x04006876 RID: 26742
			[SerializeField]
			private PoseKeyPair _handWash = default(PoseKeyPair);

			// Token: 0x04006877 RID: 26743
			[SerializeField]
			private PoseKeyPair _yobai = default(PoseKeyPair);

			// Token: 0x04006878 RID: 26744
			[SerializeField]
			private PoseKeyPair[] _appearIDList;
		}

		// Token: 0x02000F4B RID: 3915
		[Serializable]
		public struct WalkParameter
		{
			// Token: 0x06008209 RID: 33289 RVA: 0x0036F4DB File Offset: 0x0036D8DB
			public WalkParameter(int pathCount, float distance, int min, int max)
			{
				this.reservedPathCount = pathCount;
				this.arrivedDistance = distance;
				this.viaPointNumThreshold = new ThresholdInt(min, max);
			}

			// Token: 0x0600820A RID: 33290 RVA: 0x0036F4F9 File Offset: 0x0036D8F9
			public WalkParameter(int pathCount, float distance, ThresholdInt threshold)
			{
				this.reservedPathCount = pathCount;
				this.arrivedDistance = distance;
				this.viaPointNumThreshold = threshold;
			}

			// Token: 0x04006879 RID: 26745
			[MinValue(3.0)]
			public int reservedPathCount;

			// Token: 0x0400687A RID: 26746
			public float arrivedDistance;

			// Token: 0x0400687B RID: 26747
			public ThresholdInt viaPointNumThreshold;
		}

		// Token: 0x02000F4C RID: 3916
		[Serializable]
		public struct RangeParameter
		{
			// Token: 0x0600820B RID: 33291 RVA: 0x0036F510 File Offset: 0x0036D910
			public RangeParameter(float arrivedDistance_, float distIncludeAction, float height, float heightAction, float leaveDistance_, float leaveDistanceInSurprise_)
			{
				this.arrivedDistance = arrivedDistance_;
				this.arrivedDistanceIncludeAct = distIncludeAction;
				this.acceptableHeight = height;
				this.acceptableHeightIncludeAct = heightAction;
				this.leaveDistance = leaveDistance_;
				this.leaveDistanceInSurprise = leaveDistanceInSurprise_;
			}

			// Token: 0x0400687C RID: 26748
			public float arrivedDistance;

			// Token: 0x0400687D RID: 26749
			public float acceptableHeight;

			// Token: 0x0400687E RID: 26750
			public float arrivedDistanceIncludeAct;

			// Token: 0x0400687F RID: 26751
			public float acceptableHeightIncludeAct;

			// Token: 0x04006880 RID: 26752
			public float leaveDistance;

			// Token: 0x04006881 RID: 26753
			public float leaveDistanceInSurprise;
		}

		// Token: 0x02000F4D RID: 3917
		[Serializable]
		public class ActionPointSightSetting
		{
			// Token: 0x17001A8C RID: 6796
			// (get) Token: 0x0600820D RID: 33293 RVA: 0x0036F568 File Offset: 0x0036D968
			public float FOVAngle
			{
				[CompilerGenerated]
				get
				{
					return this._fovAngle;
				}
			}

			// Token: 0x17001A8D RID: 6797
			// (get) Token: 0x0600820E RID: 33294 RVA: 0x0036F570 File Offset: 0x0036D970
			public float HeightRange
			{
				[CompilerGenerated]
				get
				{
					return this._heightRange;
				}
			}

			// Token: 0x17001A8E RID: 6798
			// (get) Token: 0x0600820F RID: 33295 RVA: 0x0036F578 File Offset: 0x0036D978
			public float ViewDistance
			{
				[CompilerGenerated]
				get
				{
					return this._viewDistance;
				}
			}

			// Token: 0x17001A8F RID: 6799
			// (get) Token: 0x06008210 RID: 33296 RVA: 0x0036F580 File Offset: 0x0036D980
			public Vector3 Offset
			{
				[CompilerGenerated]
				get
				{
					return this._offset;
				}
			}

			// Token: 0x17001A90 RID: 6800
			// (get) Token: 0x06008211 RID: 33297 RVA: 0x0036F588 File Offset: 0x0036D988
			public float AngleOffset2D
			{
				[CompilerGenerated]
				get
				{
					return this._angleOffset2D;
				}
			}

			// Token: 0x06008212 RID: 33298 RVA: 0x0036F590 File Offset: 0x0036D990
			public bool HasEntered(Transform @base, Vector3 target, float radius)
			{
				if (Vector3.Distance(target, @base.position) > this._viewDistance + radius)
				{
					return false;
				}
				if (Mathf.Abs(@base.transform.position.y - target.y) > this._heightRange)
				{
					return false;
				}
				float num = this._fovAngle / 2f;
				float num2 = Vector3.Angle(target - @base.position, @base.forward);
				return num2 < num;
			}

			// Token: 0x06008213 RID: 33299 RVA: 0x0036F610 File Offset: 0x0036DA10
			public bool HasEntered(Collider collider, Transform @base)
			{
				Vector3 b = (!(collider is SphereCollider)) ? ((!(collider is CapsuleCollider)) ? Vector3.zero : (collider as CapsuleCollider).center) : (collider as SphereCollider).center;
				float num = (!(collider is SphereCollider)) ? ((!(collider is CapsuleCollider)) ? 0f : (collider as CapsuleCollider).radius) : (collider as SphereCollider).radius;
				if (Vector3.Distance(collider.transform.position + b, @base.position) > this._viewDistance + num)
				{
					return false;
				}
				if (Mathf.Abs(@base.transform.position.y - collider.transform.position.y) > this._heightRange)
				{
					return false;
				}
				float num2 = this._fovAngle / 2f;
				float num3 = Vector3.Angle(collider.transform.position - @base.position, @base.forward);
				return num3 < num2;
			}

			// Token: 0x04006882 RID: 26754
			[SerializeField]
			private float _fovAngle = 90f;

			// Token: 0x04006883 RID: 26755
			[SerializeField]
			private float _heightRange = 1f;

			// Token: 0x04006884 RID: 26756
			[SerializeField]
			private float _viewDistance;

			// Token: 0x04006885 RID: 26757
			[SerializeField]
			private Vector3 _offset = Vector3.zero;

			// Token: 0x04006886 RID: 26758
			[SerializeField]
			private float _angleOffset2D;
		}

		// Token: 0x02000F4E RID: 3918
		[Serializable]
		public class SightSetting
		{
			// Token: 0x17001A91 RID: 6801
			// (get) Token: 0x06008215 RID: 33301 RVA: 0x0036F759 File Offset: 0x0036DB59
			public float FOVAngle
			{
				[CompilerGenerated]
				get
				{
					return this._fovAngle;
				}
			}

			// Token: 0x17001A92 RID: 6802
			// (get) Token: 0x06008216 RID: 33302 RVA: 0x0036F761 File Offset: 0x0036DB61
			public float HeightRange
			{
				[CompilerGenerated]
				get
				{
					return this._heightRange;
				}
			}

			// Token: 0x17001A93 RID: 6803
			// (get) Token: 0x06008217 RID: 33303 RVA: 0x0036F769 File Offset: 0x0036DB69
			public float ViewDistance
			{
				[CompilerGenerated]
				get
				{
					return this._viewDistance;
				}
			}

			// Token: 0x17001A94 RID: 6804
			// (get) Token: 0x06008218 RID: 33304 RVA: 0x0036F771 File Offset: 0x0036DB71
			public Vector3 Offset
			{
				[CompilerGenerated]
				get
				{
					return this._offset;
				}
			}

			// Token: 0x17001A95 RID: 6805
			// (get) Token: 0x06008219 RID: 33305 RVA: 0x0036F779 File Offset: 0x0036DB79
			public float AngleOffset2D
			{
				[CompilerGenerated]
				get
				{
					return this._angleOffset2D;
				}
			}

			// Token: 0x0600821A RID: 33306 RVA: 0x0036F784 File Offset: 0x0036DB84
			public bool HasEntered(Transform @base, Vector3 targetPosition)
			{
				if (Vector3.Distance(targetPosition, @base.position) > this._viewDistance)
				{
					return false;
				}
				if (Mathf.Abs(@base.transform.position.y - targetPosition.y) > this._heightRange)
				{
					return false;
				}
				float num = this._fovAngle / 2f;
				Vector3 b = new Vector3(@base.position.x, 0f, @base.position.z);
				Vector3 a = new Vector3(targetPosition.x, 0f, targetPosition.z);
				float num2 = Vector3.Angle(a - b, @base.forward);
				return num2 < num;
			}

			// Token: 0x0600821B RID: 33307 RVA: 0x0036F844 File Offset: 0x0036DC44
			public bool HasEntered(Transform @base, Vector3 targetPosition, float angleOffsetY)
			{
				if (this._viewDistance < Vector3.Distance(targetPosition, @base.position))
				{
					return false;
				}
				if (Mathf.Abs(@base.transform.position.y - targetPosition.y) > this._heightRange)
				{
					return false;
				}
				float num = this._fovAngle / 2f;
				Vector3 to = Quaternion.AngleAxis(angleOffsetY, @base.up) * @base.forward;
				Vector3 b = new Vector3(@base.position.x, 0f, @base.position.z);
				Vector3 a = new Vector3(targetPosition.x, 0f, targetPosition.z);
				float num2 = Vector3.Angle(a - b, to);
				return num2 < num;
			}

			// Token: 0x0600821C RID: 33308 RVA: 0x0036F915 File Offset: 0x0036DD15
			public void DrawGizmos(Transform transform)
			{
			}

			// Token: 0x0600821D RID: 33309 RVA: 0x0036F917 File Offset: 0x0036DD17
			public void DrawGizmos(Transform transform, float angleOffsetY)
			{
			}

			// Token: 0x04006887 RID: 26759
			[SerializeField]
			private float _fovAngle = 90f;

			// Token: 0x04006888 RID: 26760
			[SerializeField]
			private float _heightRange = 1f;

			// Token: 0x04006889 RID: 26761
			[SerializeField]
			private float _viewDistance;

			// Token: 0x0400688A RID: 26762
			[SerializeField]
			private Vector3 _offset = Vector3.zero;

			// Token: 0x0400688B RID: 26763
			[SerializeField]
			private float _angleOffset2D;
		}

		// Token: 0x02000F4F RID: 3919
		[Serializable]
		public class TutorialSetting
		{
			// Token: 0x17001A96 RID: 6806
			// (get) Token: 0x0600821F RID: 33311 RVA: 0x0036F970 File Offset: 0x0036DD70
			public int AnimatorID
			{
				[CompilerGenerated]
				get
				{
					return this._animatorID;
				}
			}

			// Token: 0x17001A97 RID: 6807
			// (get) Token: 0x06008220 RID: 33312 RVA: 0x0036F978 File Offset: 0x0036DD78
			public string DefaultStateName
			{
				[CompilerGenerated]
				get
				{
					return this._defaultStateName;
				}
			}

			// Token: 0x17001A98 RID: 6808
			// (get) Token: 0x06008221 RID: 33313 RVA: 0x0036F980 File Offset: 0x0036DD80
			public float DefaultStateFadeTime
			{
				[CompilerGenerated]
				get
				{
					return this._defaultStateFadeTime;
				}
			}

			// Token: 0x17001A99 RID: 6809
			// (get) Token: 0x06008222 RID: 33314 RVA: 0x0036F988 File Offset: 0x0036DD88
			public PoseKeyPair GoGhroughAnimID
			{
				[CompilerGenerated]
				get
				{
					return this._goGhroughAnimID;
				}
			}

			// Token: 0x17001A9A RID: 6810
			// (get) Token: 0x06008223 RID: 33315 RVA: 0x0036F990 File Offset: 0x0036DD90
			public int[] GoGhroughActionIDList
			{
				[CompilerGenerated]
				get
				{
					return this._goGhroughActionIDList;
				}
			}

			// Token: 0x17001A9B RID: 6811
			// (get) Token: 0x06008224 RID: 33316 RVA: 0x0036F998 File Offset: 0x0036DD98
			public PoseKeyPair ThreeStepJumpAnimID
			{
				[CompilerGenerated]
				get
				{
					return this._threeStepJumpAnimID;
				}
			}

			// Token: 0x17001A9C RID: 6812
			// (get) Token: 0x06008225 RID: 33317 RVA: 0x0036F9A0 File Offset: 0x0036DDA0
			public int[] ThreeStepJumpActionIDList
			{
				[CompilerGenerated]
				get
				{
					return this._threeStepJumpActionIDList;
				}
			}

			// Token: 0x0400688C RID: 26764
			[SerializeField]
			private int _animatorID;

			// Token: 0x0400688D RID: 26765
			[SerializeField]
			private string _defaultStateName = string.Empty;

			// Token: 0x0400688E RID: 26766
			[SerializeField]
			private float _defaultStateFadeTime;

			// Token: 0x0400688F RID: 26767
			[SerializeField]
			private PoseKeyPair _goGhroughAnimID = default(PoseKeyPair);

			// Token: 0x04006890 RID: 26768
			[SerializeField]
			private int[] _goGhroughActionIDList = new int[0];

			// Token: 0x04006891 RID: 26769
			[SerializeField]
			private PoseKeyPair _threeStepJumpAnimID = default(PoseKeyPair);

			// Token: 0x04006892 RID: 26770
			[SerializeField]
			private int[] _threeStepJumpActionIDList = new int[0];
		}

		// Token: 0x02000F50 RID: 3920
		[Serializable]
		public struct MapMasturbationSetting
		{
			// Token: 0x04006893 RID: 26771
			public int wMotivation;

			// Token: 0x04006894 RID: 26772
			public int mMotivation;

			// Token: 0x04006895 RID: 26773
			public int sMotivation;

			// Token: 0x04006896 RID: 26774
			public int oMotivation;
		}

		// Token: 0x02000F51 RID: 3921
		[Serializable]
		public struct MapLesbianSetting
		{
			// Token: 0x04006897 RID: 26775
			public int wMotivation;

			// Token: 0x04006898 RID: 26776
			public int sMotivation;

			// Token: 0x04006899 RID: 26777
			public int oMotivation;
		}

		// Token: 0x02000F52 RID: 3922
		[Serializable]
		public struct DiminuationRates
		{
			// Token: 0x06008226 RID: 33318 RVA: 0x0036F9A8 File Offset: 0x0036DDA8
			public DiminuationRates(float value_, float recovery)
			{
				this.value = value_;
				this.valueRecovery = recovery;
				this._durationInHour = 0f;
				this._durationInHourRecovery = 0f;
			}

			// Token: 0x17001A9D RID: 6813
			// (get) Token: 0x06008227 RID: 33319 RVA: 0x0036F9D0 File Offset: 0x0036DDD0
			public static AgentProfile.DiminuationRates Default
			{
				[CompilerGenerated]
				get
				{
					return new AgentProfile.DiminuationRates
					{
						value = 0f
					};
				}
			}

			// Token: 0x17001A9E RID: 6814
			// (get) Token: 0x06008228 RID: 33320 RVA: 0x0036F9F2 File Offset: 0x0036DDF2
			private float DurationInHour
			{
				[CompilerGenerated]
				get
				{
					return this._durationInHour;
				}
			}

			// Token: 0x17001A9F RID: 6815
			// (get) Token: 0x06008229 RID: 33321 RVA: 0x0036F9FA File Offset: 0x0036DDFA
			private float DurationInHourRecovery
			{
				[CompilerGenerated]
				get
				{
					return this._durationInHourRecovery;
				}
			}

			// Token: 0x0400689A RID: 26778
			[SerializeField]
			private float _durationInHour;

			// Token: 0x0400689B RID: 26779
			[SerializeField]
			private float _durationInHourRecovery;

			// Token: 0x0400689C RID: 26780
			public float value;

			// Token: 0x0400689D RID: 26781
			public float valueRecovery;
		}

		// Token: 0x02000F53 RID: 3923
		[Serializable]
		public struct PhotoShotRangeParameter
		{
			// Token: 0x0400689E RID: 26782
			public float arriveDistance;

			// Token: 0x0400689F RID: 26783
			public float acceptableHeight;

			// Token: 0x040068A0 RID: 26784
			public float leaveDistance;

			// Token: 0x040068A1 RID: 26785
			public float sightAngle;

			// Token: 0x040068A2 RID: 26786
			public float sightOffsetZ;

			// Token: 0x040068A3 RID: 26787
			public float invisibleAngle;

			// Token: 0x040068A4 RID: 26788
			public float reliabilityBorder;
		}
	}
}
