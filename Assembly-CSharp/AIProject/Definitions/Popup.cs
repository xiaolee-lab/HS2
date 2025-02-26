using System;

namespace AIProject.Definitions
{
	// Token: 0x02000952 RID: 2386
	public static class Popup
	{
		// Token: 0x02000953 RID: 2387
		public static class Warning
		{
			// Token: 0x02000954 RID: 2388
			public enum Type
			{
				// Token: 0x04003F3C RID: 16188
				PouchIsFull,
				// Token: 0x04003F3D RID: 16189
				ChestIsFull,
				// Token: 0x04003F3E RID: 16190
				NonFishFood,
				// Token: 0x04003F3F RID: 16191
				NotOpenThisSide,
				// Token: 0x04003F40 RID: 16192
				IsLocked,
				// Token: 0x04003F41 RID: 16193
				CantFix,
				// Token: 0x04003F42 RID: 16194
				CanOpenWithElec,
				// Token: 0x04003F43 RID: 16195
				EquipedItemRankIsLow,
				// Token: 0x04003F44 RID: 16196
				NotSetFishingRod,
				// Token: 0x04003F45 RID: 16197
				NotSetShavel,
				// Token: 0x04003F46 RID: 16198
				NotSetPickaxe,
				// Token: 0x04003F47 RID: 16199
				NotSetInsectNet,
				// Token: 0x04003F48 RID: 16200
				IsBroken,
				// Token: 0x04003F49 RID: 16201
				DontReactAlone,
				// Token: 0x04003F4A RID: 16202
				InsufficientBattery,
				// Token: 0x04003F4B RID: 16203
				PouchAndChestIsFull
			}
		}

		// Token: 0x02000955 RID: 2389
		public static class Request
		{
			// Token: 0x02000956 RID: 2390
			public enum Type
			{
				// Token: 0x04003F4D RID: 16205
				GeneratorRepair,
				// Token: 0x04003F4E RID: 16206
				ShipRepair,
				// Token: 0x04003F4F RID: 16207
				Pod,
				// Token: 0x04003F50 RID: 16208
				Cuby,
				// Token: 0x04003F51 RID: 16209
				ForestBridge,
				// Token: 0x04003F52 RID: 16210
				RuinsDoor,
				// Token: 0x04003F53 RID: 16211
				StationValve
			}
		}

		// Token: 0x02000957 RID: 2391
		public static class StorySupport
		{
			// Token: 0x02000958 RID: 2392
			public enum Type
			{
				// Token: 0x04003F55 RID: 16213
				Start,
				// Token: 0x04003F56 RID: 16214
				ExamineAround,
				// Token: 0x04003F57 RID: 16215
				TalkToGirlPart1,
				// Token: 0x04003F58 RID: 16216
				GetDriftwood,
				// Token: 0x04003F59 RID: 16217
				CraftFishingRod,
				// Token: 0x04003F5A RID: 16218
				EquippedFishingRod,
				// Token: 0x04003F5B RID: 16219
				FishingGetFish,
				// Token: 0x04003F5C RID: 16220
				TalkToGirlPart2,
				// Token: 0x04003F5D RID: 16221
				FindCookFishPlace,
				// Token: 0x04003F5E RID: 16222
				TalkToGirlPart3,
				// Token: 0x04003F5F RID: 16223
				ExamineNearbySignboard,
				// Token: 0x04003F60 RID: 16224
				ArrangeKitchen,
				// Token: 0x04003F61 RID: 16225
				CookFish,
				// Token: 0x04003F62 RID: 16226
				TalkToGirlPart4,
				// Token: 0x04003F63 RID: 16227
				FollowGirl,
				// Token: 0x04003F64 RID: 16228
				ExamineAbandonedHouse,
				// Token: 0x04003F65 RID: 16229
				GrowGirls1,
				// Token: 0x04003F66 RID: 16230
				ExamineStoryPoint1,
				// Token: 0x04003F67 RID: 16231
				ExamineNextStoryPoint1,
				// Token: 0x04003F68 RID: 16232
				GrowGirls2,
				// Token: 0x04003F69 RID: 16233
				ExamineStoryPoint2,
				// Token: 0x04003F6A RID: 16234
				ExamineNextStoryPoint2,
				// Token: 0x04003F6B RID: 16235
				GrowGirls3,
				// Token: 0x04003F6C RID: 16236
				ExamineStoryPoint3,
				// Token: 0x04003F6D RID: 16237
				RepairGenerator,
				// Token: 0x04003F6E RID: 16238
				ExamineStoryPoint4,
				// Token: 0x04003F6F RID: 16239
				ExamineNextStoryPoint3,
				// Token: 0x04003F70 RID: 16240
				RepairShip,
				// Token: 0x04003F71 RID: 16241
				Complete,
				// Token: 0x04003F72 RID: 16242
				GotoDifferentIslandByBoat
			}
		}

		// Token: 0x02000959 RID: 2393
		public static class Tutorial
		{
			// Token: 0x0200095A RID: 2394
			public enum Type
			{
				// Token: 0x04003F74 RID: 16244
				Collection,
				// Token: 0x04003F75 RID: 16245
				Craft,
				// Token: 0x04003F76 RID: 16246
				Kitchen,
				// Token: 0x04003F77 RID: 16247
				Girl,
				// Token: 0x04003F78 RID: 16248
				Communication,
				// Token: 0x04003F79 RID: 16249
				H,
				// Token: 0x04003F7A RID: 16250
				DevicePoint,
				// Token: 0x04003F7B RID: 16251
				Sleep,
				// Token: 0x04003F7C RID: 16252
				Equipment,
				// Token: 0x04003F7D RID: 16253
				Fishing,
				// Token: 0x04003F7E RID: 16254
				Shop,
				// Token: 0x04003F7F RID: 16255
				Chest,
				// Token: 0x04003F80 RID: 16256
				BasePoint,
				// Token: 0x04003F81 RID: 16257
				Housing,
				// Token: 0x04003F82 RID: 16258
				Dressing,
				// Token: 0x04003F83 RID: 16259
				FarmPlant
			}
		}
	}
}
