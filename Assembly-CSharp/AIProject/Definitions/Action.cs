using System;
using System.Collections.Generic;
using UnityEx;

namespace AIProject.Definitions
{
	// Token: 0x02000936 RID: 2358
	public static class Action
	{
		// Token: 0x06004273 RID: 17011 RVA: 0x001A2298 File Offset: 0x001A0698
		// Note: this type is marked as 'beforefieldinit'.
		static Action()
		{
			Dictionary<int, EventType> dictionary = new Dictionary<int, EventType>();
			dictionary[0] = EventType.Sleep;
			dictionary[1] = EventType.Break;
			dictionary[2] = EventType.Eat;
			dictionary[3] = EventType.Toilet;
			dictionary[4] = EventType.Bath;
			dictionary[5] = EventType.Play;
			dictionary[6] = EventType.Search;
			dictionary[7] = EventType.StorageIn;
			dictionary[8] = EventType.StorageOut;
			dictionary[9] = EventType.Cook;
			dictionary[10] = EventType.DressIn;
			dictionary[11] = EventType.DressOut;
			dictionary[12] = EventType.Masturbation;
			dictionary[13] = EventType.Lesbian;
			dictionary[14] = EventType.Move;
			dictionary[15] = EventType.PutItem;
			dictionary[16] = EventType.ShallowSleep;
			dictionary[17] = EventType.Wash;
			dictionary[18] = EventType.Location;
			dictionary[19] = EventType.DoorOpen;
			dictionary[20] = EventType.DoorClose;
			dictionary[21] = EventType.Drink;
			dictionary[22] = EventType.ClothChange;
			dictionary[23] = EventType.Warp;
			Action.EventTypeTable = dictionary;
			Dictionary<EventType, UnityEx.ValueTuple<int, string>> dictionary2 = new Dictionary<EventType, UnityEx.ValueTuple<int, string>>();
			dictionary2[EventType.Sleep] = new UnityEx.ValueTuple<int, string>(0, "睡眠");
			dictionary2[EventType.Break] = new UnityEx.ValueTuple<int, string>(1, "休憩");
			dictionary2[EventType.Eat] = new UnityEx.ValueTuple<int, string>(2, "食事");
			dictionary2[EventType.Toilet] = new UnityEx.ValueTuple<int, string>(3, "トイレ");
			dictionary2[EventType.Bath] = new UnityEx.ValueTuple<int, string>(4, "沐浴");
			dictionary2[EventType.Play] = new UnityEx.ValueTuple<int, string>(5, "遊戯");
			dictionary2[EventType.Search] = new UnityEx.ValueTuple<int, string>(6, "探索");
			dictionary2[EventType.StorageIn] = new UnityEx.ValueTuple<int, string>(7, "アイテムボックス(搬入)");
			dictionary2[EventType.StorageOut] = new UnityEx.ValueTuple<int, string>(8, "アイテムボックス(搬出)");
			dictionary2[EventType.Cook] = new UnityEx.ValueTuple<int, string>(9, "料理");
			dictionary2[EventType.DressIn] = new UnityEx.ValueTuple<int, string>(10, "着替え(脱衣)");
			dictionary2[EventType.DressOut] = new UnityEx.ValueTuple<int, string>(11, "着替え(着衣)");
			dictionary2[EventType.Masturbation] = new UnityEx.ValueTuple<int, string>(12, "オナニー");
			dictionary2[EventType.Lesbian] = new UnityEx.ValueTuple<int, string>(13, "レズ");
			dictionary2[EventType.Move] = new UnityEx.ValueTuple<int, string>(14, "特殊移動");
			dictionary2[EventType.PutItem] = new UnityEx.ValueTuple<int, string>(15, "アイテムを置く");
			dictionary2[EventType.ShallowSleep] = new UnityEx.ValueTuple<int, string>(16, "浅い眠り");
			dictionary2[EventType.Wash] = new UnityEx.ValueTuple<int, string>(17, "洗う");
			dictionary2[EventType.Location] = new UnityEx.ValueTuple<int, string>(18, "ロケーション");
			dictionary2[EventType.DoorOpen] = new UnityEx.ValueTuple<int, string>(19, "ドアを開ける");
			dictionary2[EventType.DoorClose] = new UnityEx.ValueTuple<int, string>(20, "ドアを閉める");
			dictionary2[EventType.Drink] = new UnityEx.ValueTuple<int, string>(21, "飲む");
			dictionary2[EventType.ClothChange] = new UnityEx.ValueTuple<int, string>(22, "着替える");
			dictionary2[EventType.Warp] = new UnityEx.ValueTuple<int, string>(23, "ワープ");
			Action.NameTable = dictionary2;
		}

		// Token: 0x04003E63 RID: 15971
		public static readonly Dictionary<int, EventType> EventTypeTable;

		// Token: 0x04003E64 RID: 15972
		public static readonly Dictionary<EventType, UnityEx.ValueTuple<int, string>> NameTable;
	}
}
