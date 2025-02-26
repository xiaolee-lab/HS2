using System;
using System.Collections.Generic;

namespace AIProject.Definitions
{
	// Token: 0x02000940 RID: 2368
	public static class Merchant
	{
		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x0600427A RID: 17018 RVA: 0x001A276E File Offset: 0x001A0B6E
		public static Dictionary<Merchant.ActionType, Merchant.StateType> StateTypeTable { get; }

		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x0600427B RID: 17019 RVA: 0x001A2775 File Offset: 0x001A0B75
		public static Dictionary<int, Merchant.EventType> EventTypeTable { get; }

		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x0600427C RID: 17020 RVA: 0x001A277C File Offset: 0x001A0B7C
		public static Dictionary<Merchant.StateType, MerchantActor.MerchantSchedule.MerchantEvent> ScheduleTaskTable { get; }

		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x0600427D RID: 17021 RVA: 0x001A2783 File Offset: 0x001A0B83
		public static List<Merchant.ActionType> ChangeableModeList { get; }

		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x0600427E RID: 17022 RVA: 0x001A278A File Offset: 0x001A0B8A
		public static List<Merchant.ActionType> NormalModeList { get; }

		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x0600427F RID: 17023 RVA: 0x001A2791 File Offset: 0x001A0B91
		public static List<Merchant.ActionType> EncountList { get; }

		// Token: 0x06004280 RID: 17024 RVA: 0x001A2798 File Offset: 0x001A0B98
		// Note: this type is marked as 'beforefieldinit'.
		static Merchant()
		{
			Dictionary<Merchant.ActionType, Merchant.StateType> dictionary = new Dictionary<Merchant.ActionType, Merchant.StateType>();
			dictionary[Merchant.ActionType.Absent] = Merchant.StateType.Absent;
			dictionary[Merchant.ActionType.ToAbsent] = Merchant.StateType.Absent;
			dictionary[Merchant.ActionType.Wait] = Merchant.StateType.Wait;
			dictionary[Merchant.ActionType.ToWait] = Merchant.StateType.Wait;
			dictionary[Merchant.ActionType.Search] = Merchant.StateType.Search;
			dictionary[Merchant.ActionType.ToSearch] = Merchant.StateType.Search;
			dictionary[Merchant.ActionType.TalkWithPlayer] = Merchant.StateType.TalkWithPlayer;
			dictionary[Merchant.ActionType.TalkWithAgent] = Merchant.StateType.TalkWithAgent;
			dictionary[Merchant.ActionType.HWithPlayer] = Merchant.StateType.HWithPlayer;
			dictionary[Merchant.ActionType.HWithAgent] = Merchant.StateType.HWithAgent;
			dictionary[Merchant.ActionType.Encounter] = Merchant.StateType.Encounter;
			dictionary[Merchant.ActionType.Idle] = Merchant.StateType.Idle;
			dictionary[Merchant.ActionType.GotoLesbianSpotFollow] = Merchant.StateType.GotoLesbianSpotFollow;
			Merchant.StateTypeTable = dictionary;
			Dictionary<int, Merchant.EventType> dictionary2 = new Dictionary<int, Merchant.EventType>();
			dictionary2[0] = Merchant.EventType.Wait;
			dictionary2[1] = Merchant.EventType.Search;
			Merchant.EventTypeTable = dictionary2;
			Dictionary<Merchant.StateType, MerchantActor.MerchantSchedule.MerchantEvent> dictionary3 = new Dictionary<Merchant.StateType, MerchantActor.MerchantSchedule.MerchantEvent>();
			dictionary3[Merchant.StateType.Absent] = new MerchantActor.MerchantSchedule.MerchantEvent(Merchant.ActionType.ToAbsent, Merchant.ActionType.Absent);
			dictionary3[Merchant.StateType.Wait] = new MerchantActor.MerchantSchedule.MerchantEvent(Merchant.ActionType.ToWait, Merchant.ActionType.Wait);
			dictionary3[Merchant.StateType.Search] = new MerchantActor.MerchantSchedule.MerchantEvent(Merchant.ActionType.ToSearch, Merchant.ActionType.Search);
			Merchant.ScheduleTaskTable = dictionary3;
			Merchant.ChangeableModeList = new List<Merchant.ActionType>
			{
				Merchant.ActionType.Absent,
				Merchant.ActionType.ToSearch,
				Merchant.ActionType.Wait,
				Merchant.ActionType.ToWait
			};
			Merchant.NormalModeList = new List<Merchant.ActionType>
			{
				Merchant.ActionType.Absent,
				Merchant.ActionType.ToAbsent,
				Merchant.ActionType.Search,
				Merchant.ActionType.ToSearch,
				Merchant.ActionType.Wait,
				Merchant.ActionType.ToWait
			};
			Merchant.EncountList = new List<Merchant.ActionType>
			{
				Merchant.ActionType.ToAbsent,
				Merchant.ActionType.ToWait,
				Merchant.ActionType.Wait,
				Merchant.ActionType.ToSearch,
				Merchant.ActionType.Search
			};
		}

		// Token: 0x02000941 RID: 2369
		public enum StateType
		{
			// Token: 0x04003ED6 RID: 16086
			Absent,
			// Token: 0x04003ED7 RID: 16087
			Wait,
			// Token: 0x04003ED8 RID: 16088
			Search,
			// Token: 0x04003ED9 RID: 16089
			TalkWithPlayer,
			// Token: 0x04003EDA RID: 16090
			TalkWithAgent,
			// Token: 0x04003EDB RID: 16091
			HWithPlayer,
			// Token: 0x04003EDC RID: 16092
			HWithAgent,
			// Token: 0x04003EDD RID: 16093
			Encounter,
			// Token: 0x04003EDE RID: 16094
			Idle,
			// Token: 0x04003EDF RID: 16095
			GotoLesbianSpotFollow
		}

		// Token: 0x02000942 RID: 2370
		public enum ActionType
		{
			// Token: 0x04003EE1 RID: 16097
			ToAbsent,
			// Token: 0x04003EE2 RID: 16098
			Absent,
			// Token: 0x04003EE3 RID: 16099
			ToWait,
			// Token: 0x04003EE4 RID: 16100
			Wait,
			// Token: 0x04003EE5 RID: 16101
			ToSearch,
			// Token: 0x04003EE6 RID: 16102
			Search,
			// Token: 0x04003EE7 RID: 16103
			TalkWithPlayer,
			// Token: 0x04003EE8 RID: 16104
			TalkWithAgent,
			// Token: 0x04003EE9 RID: 16105
			HWithPlayer,
			// Token: 0x04003EEA RID: 16106
			HWithAgent,
			// Token: 0x04003EEB RID: 16107
			Encounter,
			// Token: 0x04003EEC RID: 16108
			Idle,
			// Token: 0x04003EED RID: 16109
			GotoLesbianSpotFollow
		}

		// Token: 0x02000943 RID: 2371
		[Flags]
		public enum EventType
		{
			// Token: 0x04003EEF RID: 16111
			Wait = 1,
			// Token: 0x04003EF0 RID: 16112
			Search = 2
		}
	}
}
