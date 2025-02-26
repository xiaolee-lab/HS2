using System;
using System.Collections.Generic;
using UnityEx;

namespace AIProject.Definitions
{
	// Token: 0x02000931 RID: 2353
	public static class Desire
	{
		// Token: 0x0600426A RID: 17002 RVA: 0x001A1ACC File Offset: 0x0019FECC
		public static bool ContainsSickFilterTable(Desire.ActionType source)
		{
			foreach (Desire.ActionType actionType in Desire._sickFilterTable)
			{
				if (source == actionType)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x0600426B RID: 17003 RVA: 0x001A1B01 File Offset: 0x0019FF01
		public static Dictionary<Desire.Type, string> NameTable { get; }

		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x0600426C RID: 17004 RVA: 0x001A1B08 File Offset: 0x0019FF08
		public static Dictionary<Desire.ActionType, Desire.Type> ModeTable { get; }

		// Token: 0x0600426D RID: 17005 RVA: 0x001A1B10 File Offset: 0x0019FF10
		public static int GetDesireKey(EventType type)
		{
			foreach (UnityEx.ValueTuple<EventType, Desire.Type> valueTuple in Desire.ValuePairs)
			{
				if (type == valueTuple.Item1)
				{
					return Desire.GetDesireKey(valueTuple.Item2);
				}
			}
			return -1;
		}

		// Token: 0x0600426E RID: 17006 RVA: 0x001A1B60 File Offset: 0x0019FF60
		public static int GetDesireKey(Desire.Type key)
		{
			int result;
			if (!Desire._desireKeyTable.TryGetValue(key, out result))
			{
				return -1;
			}
			return result;
		}

		// Token: 0x0600426F RID: 17007 RVA: 0x001A1B84 File Offset: 0x0019FF84
		public static Desire.Type DesireTypeFromValue(int value)
		{
			foreach (KeyValuePair<Desire.Type, int> keyValuePair in Desire._desireKeyTable)
			{
				if (keyValuePair.Value == value)
				{
					return keyValuePair.Key;
				}
			}
			return Desire.Type.None;
		}

		// Token: 0x06004270 RID: 17008 RVA: 0x001A1BF8 File Offset: 0x0019FFF8
		public static UnityEx.ValueTuple<EventType, Desire.Type>[] FindAt(Desire.Type type)
		{
			List<UnityEx.ValueTuple<EventType, Desire.Type>> list = ListPool<UnityEx.ValueTuple<EventType, Desire.Type>>.Get();
			foreach (UnityEx.ValueTuple<EventType, Desire.Type> item in Desire.ValuePairs)
			{
				if (type == item.Item2)
				{
					list.Add(item);
				}
			}
			UnityEx.ValueTuple<EventType, Desire.Type>[] result = list.ToArray();
			ListPool<UnityEx.ValueTuple<EventType, Desire.Type>>.Release(list);
			return result;
		}

		// Token: 0x17000C88 RID: 3208
		// (get) Token: 0x06004271 RID: 17009 RVA: 0x001A1C56 File Offset: 0x001A0056
		public static UnityEx.ValueTuple<EventType, Desire.Type>[] ValuePairs { get; }

		// Token: 0x06004272 RID: 17010 RVA: 0x001A1C60 File Offset: 0x001A0060
		// Note: this type is marked as 'beforefieldinit'.
		static Desire()
		{
			Dictionary<Desire.ActionType, Desire.ActionType> dictionary = new Dictionary<Desire.ActionType, Desire.ActionType>();
			dictionary[Desire.ActionType.Cold2A] = Desire.ActionType.Cold2B;
			dictionary[Desire.ActionType.Cold3A] = Desire.ActionType.Cold3B;
			dictionary[Desire.ActionType.OverworkA] = Desire.ActionType.OverworkB;
			dictionary[Desire.ActionType.WeaknessA] = Desire.ActionType.WeaknessB;
			Desire.SickPairTable = dictionary;
			Desire._sickFilterTable = new Desire.ActionType[]
			{
				Desire.ActionType.Faint,
				Desire.ActionType.Cold2A,
				Desire.ActionType.Cold2B,
				Desire.ActionType.Cold2BMedicated,
				Desire.ActionType.Cold3A,
				Desire.ActionType.Cold3B,
				Desire.ActionType.Cold3BMedicated,
				Desire.ActionType.OverworkA,
				Desire.ActionType.OverworkB
			};
			Desire.WithPlayerDesireTable = new Desire.Type[]
			{
				Desire.Type.Gift,
				Desire.Type.Game,
				Desire.Type.H
			};
			Dictionary<Desire.Type, string> dictionary2 = new Dictionary<Desire.Type, string>();
			dictionary2[Desire.Type.None] = "なし";
			dictionary2[Desire.Type.Toilet] = "トイレ";
			dictionary2[Desire.Type.Bath] = "風呂";
			dictionary2[Desire.Type.Sleep] = "睡眠";
			dictionary2[Desire.Type.Eat] = "飲食";
			dictionary2[Desire.Type.Break] = "休憩";
			dictionary2[Desire.Type.Gift] = "ギフト";
			dictionary2[Desire.Type.Want] = "おねだり";
			dictionary2[Desire.Type.Lonely] = "寂しい";
			dictionary2[Desire.Type.H] = "H";
			dictionary2[Desire.Type.Dummy] = "ダミー";
			dictionary2[Desire.Type.Hunt] = "採取";
			dictionary2[Desire.Type.Game] = "遊び";
			dictionary2[Desire.Type.Cook] = "料理";
			dictionary2[Desire.Type.Animal] = "動物";
			dictionary2[Desire.Type.Location] = "ロケーション";
			dictionary2[Desire.Type.Drink] = "飲む";
			Desire.NameTable = dictionary2;
			Dictionary<Desire.ActionType, Desire.Type> dictionary3 = new Dictionary<Desire.ActionType, Desire.Type>();
			dictionary3[Desire.ActionType.Normal] = Desire.Type.None;
			dictionary3[Desire.ActionType.Date] = Desire.Type.None;
			dictionary3[Desire.ActionType.SearchBath] = Desire.Type.Bath;
			dictionary3[Desire.ActionType.SearchToilet] = Desire.Type.Toilet;
			dictionary3[Desire.ActionType.EndTaskEat] = Desire.Type.Eat;
			dictionary3[Desire.ActionType.SearchActor] = Desire.Type.Lonely;
			dictionary3[Desire.ActionType.WithPlayer] = Desire.Type.None;
			dictionary3[Desire.ActionType.SearchGather] = Desire.Type.Hunt;
			dictionary3[Desire.ActionType.SearchGift] = Desire.Type.Gift;
			dictionary3[Desire.ActionType.EndTaskGift] = Desire.Type.Gift;
			dictionary3[Desire.ActionType.SearchBreak] = Desire.Type.Break;
			dictionary3[Desire.ActionType.SearchCook] = Desire.Type.Cook;
			dictionary3[Desire.ActionType.SearchSleep] = Desire.Type.Sleep;
			dictionary3[Desire.ActionType.EndTaskGimme] = Desire.Type.Want;
			dictionary3[Desire.ActionType.EndTaskMasturbation] = Desire.Type.H;
			dictionary3[Desire.ActionType.SearchH] = Desire.Type.H;
			dictionary3[Desire.ActionType.EndTaskH] = Desire.Type.H;
			dictionary3[Desire.ActionType.SearchRevRape] = Desire.Type.H;
			dictionary3[Desire.ActionType.ReverseRape] = Desire.Type.H;
			dictionary3[Desire.ActionType.SearchEat] = Desire.Type.Eat;
			dictionary3[Desire.ActionType.SearchGame] = Desire.Type.Game;
			dictionary3[Desire.ActionType.EndTaskTalkToPlayer] = Desire.Type.Lonely;
			dictionary3[Desire.ActionType.EndTaskTalk] = Desire.Type.Lonely;
			dictionary3[Desire.ActionType.ReceiveTalk] = Desire.Type.Lonely;
			dictionary3[Desire.ActionType.Fight] = Desire.Type.Lonely;
			dictionary3[Desire.ActionType.ReceiveFight] = Desire.Type.Lonely;
			dictionary3[Desire.ActionType.SearchAnimal] = Desire.Type.Animal;
			dictionary3[Desire.ActionType.EndTaskPetAnimal] = Desire.Type.Animal;
			dictionary3[Desire.ActionType.EndTaskWildAnimal] = Desire.Type.Animal;
			dictionary3[Desire.ActionType.SearchLocation] = Desire.Type.Location;
			dictionary3[Desire.ActionType.EndTaskLocation] = Desire.Type.Location;
			dictionary3[Desire.ActionType.WalkWithAgent] = Desire.Type.Game;
			dictionary3[Desire.ActionType.ChaseToTalk] = Desire.Type.Lonely;
			dictionary3[Desire.ActionType.ChaseToPairWalk] = Desire.Type.Lonely;
			dictionary3[Desire.ActionType.SearchMasturbation] = Desire.Type.H;
			dictionary3[Desire.ActionType.SearchItemForEat] = Desire.Type.Hunt;
			dictionary3[Desire.ActionType.SearchEatSpot] = Desire.Type.Eat;
			dictionary3[Desire.ActionType.SearchRevRape] = Desire.Type.H;
			dictionary3[Desire.ActionType.SearchItemForDrink] = Desire.Type.Hunt;
			dictionary3[Desire.ActionType.SearchDrinkSpot] = Desire.Type.Drink;
			dictionary3[Desire.ActionType.WalkWithAgent] = Desire.Type.Game;
			dictionary3[Desire.ActionType.EndTaskToilet] = Desire.Type.Toilet;
			dictionary3[Desire.ActionType.EndTaskBath] = Desire.Type.Bath;
			dictionary3[Desire.ActionType.GiftForceEncounter] = Desire.Type.Gift;
			dictionary3[Desire.ActionType.SearchGimme] = Desire.Type.Want;
			dictionary3[Desire.ActionType.EndTaskGame] = Desire.Type.Game;
			dictionary3[Desire.ActionType.EndTaskGameThere] = Desire.Type.Game;
			dictionary3[Desire.ActionType.EndTaskBreak] = Desire.Type.Break;
			dictionary3[Desire.ActionType.EndTaskCook] = Desire.Type.Cook;
			dictionary3[Desire.ActionType.EndTaskSleep] = Desire.Type.Sleep;
			dictionary3[Desire.ActionType.EndTaskSecondSleep] = Desire.Type.Sleep;
			dictionary3[Desire.ActionType.EndTaskGather] = Desire.Type.Hunt;
			dictionary3[Desire.ActionType.EndTaskGatherForEat] = Desire.Type.Hunt;
			dictionary3[Desire.ActionType.EndTaskGatherForDrink] = Desire.Type.Hunt;
			dictionary3[Desire.ActionType.InviteSleep] = Desire.Type.Lonely;
			dictionary3[Desire.ActionType.InviteSleepH] = Desire.Type.Lonely;
			dictionary3[Desire.ActionType.InviteEat] = Desire.Type.Lonely;
			dictionary3[Desire.ActionType.InviteBreak] = Desire.Type.Lonely;
			dictionary3[Desire.ActionType.SearchWarpPoint] = Desire.Type.Game;
			Desire.ModeTable = dictionary3;
			Dictionary<Desire.Type, int> dictionary4 = new Dictionary<Desire.Type, int>();
			dictionary4[Desire.Type.Toilet] = 0;
			dictionary4[Desire.Type.Bath] = 1;
			dictionary4[Desire.Type.Sleep] = 2;
			dictionary4[Desire.Type.Eat] = 3;
			dictionary4[Desire.Type.Break] = 4;
			dictionary4[Desire.Type.Gift] = 5;
			dictionary4[Desire.Type.Want] = 6;
			dictionary4[Desire.Type.Lonely] = 7;
			dictionary4[Desire.Type.H] = 8;
			dictionary4[Desire.Type.Dummy] = 9;
			dictionary4[Desire.Type.Hunt] = 10;
			dictionary4[Desire.Type.Game] = 11;
			dictionary4[Desire.Type.Cook] = 12;
			dictionary4[Desire.Type.Animal] = 13;
			dictionary4[Desire.Type.Location] = 14;
			dictionary4[Desire.Type.Drink] = 15;
			Desire._desireKeyTable = dictionary4;
			Desire.ValuePairs = new UnityEx.ValueTuple<EventType, Desire.Type>[]
			{
				new UnityEx.ValueTuple<EventType, Desire.Type>(EventType.Sleep, Desire.Type.Sleep),
				new UnityEx.ValueTuple<EventType, Desire.Type>(EventType.Break, Desire.Type.Break),
				new UnityEx.ValueTuple<EventType, Desire.Type>(EventType.Eat, Desire.Type.Eat),
				new UnityEx.ValueTuple<EventType, Desire.Type>(EventType.Toilet, Desire.Type.Toilet),
				new UnityEx.ValueTuple<EventType, Desire.Type>(EventType.Play, Desire.Type.Game),
				new UnityEx.ValueTuple<EventType, Desire.Type>(EventType.Search, Desire.Type.Hunt),
				new UnityEx.ValueTuple<EventType, Desire.Type>(EventType.Cook, Desire.Type.Cook),
				new UnityEx.ValueTuple<EventType, Desire.Type>(EventType.DressIn, Desire.Type.Bath),
				new UnityEx.ValueTuple<EventType, Desire.Type>(EventType.Bath, Desire.Type.Bath),
				new UnityEx.ValueTuple<EventType, Desire.Type>(EventType.DressOut, Desire.Type.Bath),
				new UnityEx.ValueTuple<EventType, Desire.Type>(EventType.Masturbation, Desire.Type.H),
				new UnityEx.ValueTuple<EventType, Desire.Type>(EventType.ShallowSleep, Desire.Type.Sleep),
				new UnityEx.ValueTuple<EventType, Desire.Type>(EventType.Location, Desire.Type.Location),
				new UnityEx.ValueTuple<EventType, Desire.Type>(EventType.Drink, Desire.Type.Drink)
			};
		}

		// Token: 0x04003DC1 RID: 15809
		public static Dictionary<Desire.ActionType, Desire.ActionType> SickPairTable;

		// Token: 0x04003DC2 RID: 15810
		private static Desire.ActionType[] _sickFilterTable;

		// Token: 0x04003DC3 RID: 15811
		public static readonly Desire.Type[] WithPlayerDesireTable;

		// Token: 0x04003DC6 RID: 15814
		private static Dictionary<Desire.Type, int> _desireKeyTable;

		// Token: 0x02000932 RID: 2354
		[Flags]
		public enum Type
		{
			// Token: 0x04003DC9 RID: 15817
			None = 0,
			// Token: 0x04003DCA RID: 15818
			Toilet = 1,
			// Token: 0x04003DCB RID: 15819
			Bath = 2,
			// Token: 0x04003DCC RID: 15820
			Sleep = 4,
			// Token: 0x04003DCD RID: 15821
			Eat = 8,
			// Token: 0x04003DCE RID: 15822
			Break = 16,
			// Token: 0x04003DCF RID: 15823
			Gift = 32,
			// Token: 0x04003DD0 RID: 15824
			Want = 64,
			// Token: 0x04003DD1 RID: 15825
			Lonely = 128,
			// Token: 0x04003DD2 RID: 15826
			H = 256,
			// Token: 0x04003DD3 RID: 15827
			Dummy = 512,
			// Token: 0x04003DD4 RID: 15828
			Hunt = 1024,
			// Token: 0x04003DD5 RID: 15829
			Game = 2048,
			// Token: 0x04003DD6 RID: 15830
			Cook = 4096,
			// Token: 0x04003DD7 RID: 15831
			Animal = 8192,
			// Token: 0x04003DD8 RID: 15832
			Location = 16384,
			// Token: 0x04003DD9 RID: 15833
			Drink = 32768
		}

		// Token: 0x02000933 RID: 2355
		public enum ActionType
		{
			// Token: 0x04003DDB RID: 15835
			Called = -1,
			// Token: 0x04003DDC RID: 15836
			Normal,
			// Token: 0x04003DDD RID: 15837
			Date,
			// Token: 0x04003DDE RID: 15838
			Onbu,
			// Token: 0x04003DDF RID: 15839
			Idle,
			// Token: 0x04003DE0 RID: 15840
			SearchBath,
			// Token: 0x04003DE1 RID: 15841
			SearchToilet,
			// Token: 0x04003DE2 RID: 15842
			EndTaskEat,
			// Token: 0x04003DE3 RID: 15843
			SearchActor,
			// Token: 0x04003DE4 RID: 15844
			WithPlayer,
			// Token: 0x04003DE5 RID: 15845
			WithAgent,
			// Token: 0x04003DE6 RID: 15846
			WithMerchant,
			// Token: 0x04003DE7 RID: 15847
			SearchGather,
			// Token: 0x04003DE8 RID: 15848
			SearchGift,
			// Token: 0x04003DE9 RID: 15849
			EndTaskGift,
			// Token: 0x04003DEA RID: 15850
			SearchBreak,
			// Token: 0x04003DEB RID: 15851
			SearchCook,
			// Token: 0x04003DEC RID: 15852
			SearchSleep,
			// Token: 0x04003DED RID: 15853
			EndTaskGimme,
			// Token: 0x04003DEE RID: 15854
			SearchH,
			// Token: 0x04003DEF RID: 15855
			EndTaskMasturbation,
			// Token: 0x04003DF0 RID: 15856
			Escape,
			// Token: 0x04003DF1 RID: 15857
			EndTaskH,
			// Token: 0x04003DF2 RID: 15858
			ChaseLesbianH,
			// Token: 0x04003DF3 RID: 15859
			EndTaskLesbianMerchantH,
			// Token: 0x04003DF4 RID: 15860
			ReverseRape = 25,
			// Token: 0x04003DF5 RID: 15861
			EndTaskItemBox,
			// Token: 0x04003DF6 RID: 15862
			Steal,
			// Token: 0x04003DF7 RID: 15863
			SearchEat,
			// Token: 0x04003DF8 RID: 15864
			AfterCook,
			// Token: 0x04003DF9 RID: 15865
			ShallowSleep = 31,
			// Token: 0x04003DFA RID: 15866
			EndTaskSleep,
			// Token: 0x04003DFB RID: 15867
			SearchGame = 35,
			// Token: 0x04003DFC RID: 15868
			EndTaskGame,
			// Token: 0x04003DFD RID: 15869
			SearchPlayerToTalk,
			// Token: 0x04003DFE RID: 15870
			EndTaskTalkToPlayer,
			// Token: 0x04003DFF RID: 15871
			EndTaskTalk,
			// Token: 0x04003E00 RID: 15872
			ReceiveTalk,
			// Token: 0x04003E01 RID: 15873
			Fight,
			// Token: 0x04003E02 RID: 15874
			ReceiveFight,
			// Token: 0x04003E03 RID: 15875
			SearchAnimal = 33,
			// Token: 0x04003E04 RID: 15876
			EndTaskPetAnimal,
			// Token: 0x04003E05 RID: 15877
			EndTaskWildAnimal = 43,
			// Token: 0x04003E06 RID: 15878
			Encounter,
			// Token: 0x04003E07 RID: 15879
			Faint,
			// Token: 0x04003E08 RID: 15880
			Cold2A,
			// Token: 0x04003E09 RID: 15881
			Cold2B,
			// Token: 0x04003E0A RID: 15882
			Cold2BMedicated,
			// Token: 0x04003E0B RID: 15883
			Cold3A,
			// Token: 0x04003E0C RID: 15884
			Cold3B,
			// Token: 0x04003E0D RID: 15885
			Cold3BMedicated,
			// Token: 0x04003E0E RID: 15886
			OverworkA = 53,
			// Token: 0x04003E0F RID: 15887
			OverworkB,
			// Token: 0x04003E10 RID: 15888
			Cure,
			// Token: 0x04003E11 RID: 15889
			SearchLocation,
			// Token: 0x04003E12 RID: 15890
			EndTaskLocation,
			// Token: 0x04003E13 RID: 15891
			EndTaskSleepAfterDate,
			// Token: 0x04003E14 RID: 15892
			WalkWithAgent,
			// Token: 0x04003E15 RID: 15893
			EndTaskGameWithAgent,
			// Token: 0x04003E16 RID: 15894
			EndTaskToilet,
			// Token: 0x04003E17 RID: 15895
			GiftForceEncounter,
			// Token: 0x04003E18 RID: 15896
			GiftStandby,
			// Token: 0x04003E19 RID: 15897
			SearchGimme,
			// Token: 0x04003E1A RID: 15898
			WalkWithAgentFollow,
			// Token: 0x04003E1B RID: 15899
			ChaseToTalk,
			// Token: 0x04003E1C RID: 15900
			ChaseToPairWalk,
			// Token: 0x04003E1D RID: 15901
			EndTaskGather,
			// Token: 0x04003E1E RID: 15902
			SearchMasturbation,
			// Token: 0x04003E1F RID: 15903
			GoTowardItemBox,
			// Token: 0x04003E20 RID: 15904
			EndTaskGameThere,
			// Token: 0x04003E21 RID: 15905
			EndTaskEatThere,
			// Token: 0x04003E22 RID: 15906
			SearchItemForEat,
			// Token: 0x04003E23 RID: 15907
			EndTaskGatherForEat,
			// Token: 0x04003E24 RID: 15908
			SearchEatSpot,
			// Token: 0x04003E25 RID: 15909
			SearchRevRape,
			// Token: 0x04003E26 RID: 15910
			EndTaskPeeing,
			// Token: 0x04003E27 RID: 15911
			SearchDrink,
			// Token: 0x04003E28 RID: 15912
			SearchItemForDrink,
			// Token: 0x04003E29 RID: 15913
			EndTaskGatherForDrink,
			// Token: 0x04003E2A RID: 15914
			SearchDrinkSpot,
			// Token: 0x04003E2B RID: 15915
			EndTaskDrink,
			// Token: 0x04003E2C RID: 15916
			EndTaskDrinkThere,
			// Token: 0x04003E2D RID: 15917
			EndTaskDressIn,
			// Token: 0x04003E2E RID: 15918
			EndTaskBath,
			// Token: 0x04003E2F RID: 15919
			EndTaskDressOut,
			// Token: 0x04003E30 RID: 15920
			EndTaskCook,
			// Token: 0x04003E31 RID: 15921
			EndTaskBreak,
			// Token: 0x04003E32 RID: 15922
			EndTaskSleepTogether,
			// Token: 0x04003E33 RID: 15923
			DiscussLesbianH,
			// Token: 0x04003E34 RID: 15924
			GotoLesbianSpot,
			// Token: 0x04003E35 RID: 15925
			GotoLesbianSpotFollow,
			// Token: 0x04003E36 RID: 15926
			EndTaskLesbianH,
			// Token: 0x04003E37 RID: 15927
			EndTaskSecondSleep,
			// Token: 0x04003E38 RID: 15928
			WokenUp,
			// Token: 0x04003E39 RID: 15929
			PhotoEncounter,
			// Token: 0x04003E3A RID: 15930
			FoundPeeping,
			// Token: 0x04003E3B RID: 15931
			GotoBath,
			// Token: 0x04003E3C RID: 15932
			GotoDressOut,
			// Token: 0x04003E3D RID: 15933
			WaitForCalled,
			// Token: 0x04003E3E RID: 15934
			Ovation,
			// Token: 0x04003E3F RID: 15935
			GoTowardFacialWash,
			// Token: 0x04003E40 RID: 15936
			EndTaskFacialWash,
			// Token: 0x04003E41 RID: 15937
			EndTaskSteal,
			// Token: 0x04003E42 RID: 15938
			GoTowardChestInSearchLoop,
			// Token: 0x04003E43 RID: 15939
			EndTaskChestInSearchLoop,
			// Token: 0x04003E44 RID: 15940
			GotoClothChange,
			// Token: 0x04003E45 RID: 15941
			EndTaskClothChange,
			// Token: 0x04003E46 RID: 15942
			GotoRestoreCloth,
			// Token: 0x04003E47 RID: 15943
			EndTaskRestoreCloth,
			// Token: 0x04003E48 RID: 15944
			GotoHandWash,
			// Token: 0x04003E49 RID: 15945
			EndTaskHandWash,
			// Token: 0x04003E4A RID: 15946
			SearchBirthdayGift,
			// Token: 0x04003E4B RID: 15947
			BirthdayGift,
			// Token: 0x04003E4C RID: 15948
			WeaknessA,
			// Token: 0x04003E4D RID: 15949
			WeaknessB,
			// Token: 0x04003E4E RID: 15950
			TakeHPoint,
			// Token: 0x04003E4F RID: 15951
			CommonSearchBreak,
			// Token: 0x04003E50 RID: 15952
			CommonBreak,
			// Token: 0x04003E51 RID: 15953
			CommonGameThere,
			// Token: 0x04003E52 RID: 15954
			ComeSleepTogether,
			// Token: 0x04003E53 RID: 15955
			ChaseYobai,
			// Token: 0x04003E54 RID: 15956
			InviteSleep,
			// Token: 0x04003E55 RID: 15957
			InviteSleepH,
			// Token: 0x04003E56 RID: 15958
			InviteEat,
			// Token: 0x04003E57 RID: 15959
			InviteBreak,
			// Token: 0x04003E58 RID: 15960
			TakeSleepPoint,
			// Token: 0x04003E59 RID: 15961
			TakeSleepHPoint,
			// Token: 0x04003E5A RID: 15962
			TakeEatPoint,
			// Token: 0x04003E5B RID: 15963
			TakeBreakPoint,
			// Token: 0x04003E5C RID: 15964
			SearchWarpPoint,
			// Token: 0x04003E5D RID: 15965
			Warp,
			// Token: 0x04003E5E RID: 15966
			YandereWarp
		}
	}
}
