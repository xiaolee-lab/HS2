using System;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000F8D RID: 3981
	public class StatusProfile : SerializedScriptableObject
	{
		// Token: 0x17001C75 RID: 7285
		// (get) Token: 0x06008454 RID: 33876 RVA: 0x00373904 File Offset: 0x00371D04
		public Threshold CookPheromoneBuffMinMax
		{
			[CompilerGenerated]
			get
			{
				return this._cookPheromoneBuffMinMax;
			}
		}

		// Token: 0x17001C76 RID: 7286
		// (get) Token: 0x06008455 RID: 33877 RVA: 0x0037390C File Offset: 0x00371D0C
		public Threshold CookPheromoneBuff
		{
			[CompilerGenerated]
			get
			{
				return this._cookPheromoneBuff;
			}
		}

		// Token: 0x17001C77 RID: 7287
		// (get) Token: 0x06008456 RID: 33878 RVA: 0x00373914 File Offset: 0x00371D14
		public float BuffCook
		{
			[CompilerGenerated]
			get
			{
				return this._buffCook;
			}
		}

		// Token: 0x17001C78 RID: 7288
		// (get) Token: 0x06008457 RID: 33879 RVA: 0x0037391C File Offset: 0x00371D1C
		public float BuffBath
		{
			[CompilerGenerated]
			get
			{
				return this._buffBath;
			}
		}

		// Token: 0x17001C79 RID: 7289
		// (get) Token: 0x06008458 RID: 33880 RVA: 0x00373924 File Offset: 0x00371D24
		public float BuffAnimal
		{
			[CompilerGenerated]
			get
			{
				return this._buffAnimal;
			}
		}

		// Token: 0x17001C7A RID: 7290
		// (get) Token: 0x06008459 RID: 33881 RVA: 0x0037392C File Offset: 0x00371D2C
		public float BuffSleep
		{
			[CompilerGenerated]
			get
			{
				return this._buffSleep;
			}
		}

		// Token: 0x17001C7B RID: 7291
		// (get) Token: 0x0600845A RID: 33882 RVA: 0x00373934 File Offset: 0x00371D34
		public Threshold SleepSociabilityBuffMinMax
		{
			[CompilerGenerated]
			get
			{
				return this._sleepSociabilityBuffMinMax;
			}
		}

		// Token: 0x17001C7C RID: 7292
		// (get) Token: 0x0600845B RID: 33883 RVA: 0x0037393C File Offset: 0x00371D3C
		public Threshold SleepSociabilityBuff
		{
			[CompilerGenerated]
			get
			{
				return this._sleepSociabilityBuff;
			}
		}

		// Token: 0x17001C7D RID: 7293
		// (get) Token: 0x0600845C RID: 33884 RVA: 0x00373944 File Offset: 0x00371D44
		public float BuffGift
		{
			[CompilerGenerated]
			get
			{
				return this._buffGift;
			}
		}

		// Token: 0x17001C7E RID: 7294
		// (get) Token: 0x0600845D RID: 33885 RVA: 0x0037394C File Offset: 0x00371D4C
		public Threshold GiftReliabilityBuffMinMax
		{
			[CompilerGenerated]
			get
			{
				return this._giftReliabilityBuffMinMax;
			}
		}

		// Token: 0x17001C7F RID: 7295
		// (get) Token: 0x0600845E RID: 33886 RVA: 0x00373954 File Offset: 0x00371D54
		public Threshold GiftReliabilityBuff
		{
			[CompilerGenerated]
			get
			{
				return this._giftReliabilityBuff;
			}
		}

		// Token: 0x17001C80 RID: 7296
		// (get) Token: 0x0600845F RID: 33887 RVA: 0x0037395C File Offset: 0x00371D5C
		public float BuffGimme
		{
			[CompilerGenerated]
			get
			{
				return this._buffGimme;
			}
		}

		// Token: 0x17001C81 RID: 7297
		// (get) Token: 0x06008460 RID: 33888 RVA: 0x00373964 File Offset: 0x00371D64
		public Threshold GimmeDarknessBuffMinMax
		{
			[CompilerGenerated]
			get
			{
				return this._gimmeDarknessBuffMinMax;
			}
		}

		// Token: 0x17001C82 RID: 7298
		// (get) Token: 0x06008461 RID: 33889 RVA: 0x0037396C File Offset: 0x00371D6C
		public Threshold GimmeDarknessBuff
		{
			[CompilerGenerated]
			get
			{
				return this._gimmeDarknessBuff;
			}
		}

		// Token: 0x17001C83 RID: 7299
		// (get) Token: 0x06008462 RID: 33890 RVA: 0x00373974 File Offset: 0x00371D74
		public Threshold GimmeWarinessBuffMinMax
		{
			[CompilerGenerated]
			get
			{
				return this._gimmeWarinessBuffMinMax;
			}
		}

		// Token: 0x17001C84 RID: 7300
		// (get) Token: 0x06008463 RID: 33891 RVA: 0x0037397C File Offset: 0x00371D7C
		public Threshold GimmeWarinessBuff
		{
			[CompilerGenerated]
			get
			{
				return this._gimmeWarinessBuff;
			}
		}

		// Token: 0x17001C85 RID: 7301
		// (get) Token: 0x06008464 RID: 33892 RVA: 0x00373984 File Offset: 0x00371D84
		public float BuffEat
		{
			[CompilerGenerated]
			get
			{
				return this._buffEat;
			}
		}

		// Token: 0x17001C86 RID: 7302
		// (get) Token: 0x06008465 RID: 33893 RVA: 0x0037398C File Offset: 0x00371D8C
		public Threshold EatPheromoneDebuffMinMax
		{
			[CompilerGenerated]
			get
			{
				return this._eatPheromoneDebuffMinMax;
			}
		}

		// Token: 0x17001C87 RID: 7303
		// (get) Token: 0x06008466 RID: 33894 RVA: 0x00373994 File Offset: 0x00371D94
		public Threshold EatPheromoneDebuff
		{
			[CompilerGenerated]
			get
			{
				return this._eatPheromoneDebuff;
			}
		}

		// Token: 0x17001C88 RID: 7304
		// (get) Token: 0x06008467 RID: 33895 RVA: 0x0037399C File Offset: 0x00371D9C
		public Threshold EatDarknessDebuffMinMax
		{
			[CompilerGenerated]
			get
			{
				return this._eatDarknessDebuffMinMax;
			}
		}

		// Token: 0x17001C89 RID: 7305
		// (get) Token: 0x06008468 RID: 33896 RVA: 0x003739A4 File Offset: 0x00371DA4
		public Threshold EatDarknessDebuff
		{
			[CompilerGenerated]
			get
			{
				return this._eatDarknessDebuff;
			}
		}

		// Token: 0x17001C8A RID: 7306
		// (get) Token: 0x06008469 RID: 33897 RVA: 0x003739AC File Offset: 0x00371DAC
		public Threshold EatInstinctBuffMinMax
		{
			[CompilerGenerated]
			get
			{
				return this._eatInstinctBuffMinMax;
			}
		}

		// Token: 0x17001C8B RID: 7307
		// (get) Token: 0x0600846A RID: 33898 RVA: 0x003739B4 File Offset: 0x00371DB4
		public Threshold EatInstinctBuff
		{
			[CompilerGenerated]
			get
			{
				return this._eatInstinctBuff;
			}
		}

		// Token: 0x17001C8C RID: 7308
		// (get) Token: 0x0600846B RID: 33899 RVA: 0x003739BC File Offset: 0x00371DBC
		public float BuffPlay
		{
			[CompilerGenerated]
			get
			{
				return this._buffPlay;
			}
		}

		// Token: 0x17001C8D RID: 7309
		// (get) Token: 0x0600846C RID: 33900 RVA: 0x003739C4 File Offset: 0x00371DC4
		public Threshold PlayReasonDebuffMinMax
		{
			[CompilerGenerated]
			get
			{
				return this._playReasonDebuffMinMax;
			}
		}

		// Token: 0x17001C8E RID: 7310
		// (get) Token: 0x0600846D RID: 33901 RVA: 0x003739CC File Offset: 0x00371DCC
		public Threshold PlayReasonDebuff
		{
			[CompilerGenerated]
			get
			{
				return this._playReasonDebuff;
			}
		}

		// Token: 0x17001C8F RID: 7311
		// (get) Token: 0x0600846E RID: 33902 RVA: 0x003739D4 File Offset: 0x00371DD4
		public Threshold PlayInstinctBuffMinMax
		{
			[CompilerGenerated]
			get
			{
				return this._playInstinctBuffMinMax;
			}
		}

		// Token: 0x17001C90 RID: 7312
		// (get) Token: 0x0600846F RID: 33903 RVA: 0x003739DC File Offset: 0x00371DDC
		public Threshold PlayInstinctBuff
		{
			[CompilerGenerated]
			get
			{
				return this._playInstinctBuff;
			}
		}

		// Token: 0x17001C91 RID: 7313
		// (get) Token: 0x06008470 RID: 33904 RVA: 0x003739E4 File Offset: 0x00371DE4
		public float BuffH
		{
			[CompilerGenerated]
			get
			{
				return this._buffH;
			}
		}

		// Token: 0x17001C92 RID: 7314
		// (get) Token: 0x06008471 RID: 33905 RVA: 0x003739EC File Offset: 0x00371DEC
		public Threshold HDirtyBuffMinMax
		{
			[CompilerGenerated]
			get
			{
				return this._hDirtyBuffMinMax;
			}
		}

		// Token: 0x17001C93 RID: 7315
		// (get) Token: 0x06008472 RID: 33906 RVA: 0x003739F4 File Offset: 0x00371DF4
		public Threshold HDirtyBuff
		{
			[CompilerGenerated]
			get
			{
				return this._hDirtyBuff;
			}
		}

		// Token: 0x17001C94 RID: 7316
		// (get) Token: 0x06008473 RID: 33907 RVA: 0x003739FC File Offset: 0x00371DFC
		public float CursedHBuff
		{
			[CompilerGenerated]
			get
			{
				return this._cursedHBuff;
			}
		}

		// Token: 0x17001C95 RID: 7317
		// (get) Token: 0x06008474 RID: 33908 RVA: 0x00373A04 File Offset: 0x00371E04
		public float BuffLonely
		{
			[CompilerGenerated]
			get
			{
				return this._buffLonely;
			}
		}

		// Token: 0x17001C96 RID: 7318
		// (get) Token: 0x06008475 RID: 33909 RVA: 0x00373A0C File Offset: 0x00371E0C
		public float BuffLonelySuperSense
		{
			[CompilerGenerated]
			get
			{
				return this._buffLonelySuperSense;
			}
		}

		// Token: 0x17001C97 RID: 7319
		// (get) Token: 0x06008476 RID: 33910 RVA: 0x00373A14 File Offset: 0x00371E14
		public Threshold LonelySociabilityBuffMinMax
		{
			[CompilerGenerated]
			get
			{
				return this._lonelySociabilityBuffMinMax;
			}
		}

		// Token: 0x17001C98 RID: 7320
		// (get) Token: 0x06008477 RID: 33911 RVA: 0x00373A1C File Offset: 0x00371E1C
		public Threshold LonelySociabilityBuff
		{
			[CompilerGenerated]
			get
			{
				return this._lonelySociabilityBuff;
			}
		}

		// Token: 0x17001C99 RID: 7321
		// (get) Token: 0x06008478 RID: 33912 RVA: 0x00373A24 File Offset: 0x00371E24
		public Threshold BreakReasonBuffMinMax
		{
			[CompilerGenerated]
			get
			{
				return this._breakReasonBuffMinMax;
			}
		}

		// Token: 0x17001C9A RID: 7322
		// (get) Token: 0x06008479 RID: 33913 RVA: 0x00373A2C File Offset: 0x00371E2C
		public Threshold BreakReasonBuff
		{
			[CompilerGenerated]
			get
			{
				return this._breakReasonBuff;
			}
		}

		// Token: 0x17001C9B RID: 7323
		// (get) Token: 0x0600847A RID: 33914 RVA: 0x00373A34 File Offset: 0x00371E34
		public Threshold BreakInstinctBuffMinMax
		{
			[CompilerGenerated]
			get
			{
				return this._breakInstinctBuffMinMax;
			}
		}

		// Token: 0x17001C9C RID: 7324
		// (get) Token: 0x0600847B RID: 33915 RVA: 0x00373A3C File Offset: 0x00371E3C
		public Threshold BreakInstinctBuff
		{
			[CompilerGenerated]
			get
			{
				return this._breakInstinctBuff;
			}
		}

		// Token: 0x17001C9D RID: 7325
		// (get) Token: 0x0600847C RID: 33916 RVA: 0x00373A44 File Offset: 0x00371E44
		public float BuffBreak
		{
			[CompilerGenerated]
			get
			{
				return this._buffBreak;
			}
		}

		// Token: 0x17001C9E RID: 7326
		// (get) Token: 0x0600847D RID: 33917 RVA: 0x00373A4C File Offset: 0x00371E4C
		public float BuffLocation
		{
			[CompilerGenerated]
			get
			{
				return this._buffLocation;
			}
		}

		// Token: 0x17001C9F RID: 7327
		// (get) Token: 0x0600847E RID: 33918 RVA: 0x00373A54 File Offset: 0x00371E54
		public float BuffSearchTough
		{
			[CompilerGenerated]
			get
			{
				return this._buffSearchTough;
			}
		}

		// Token: 0x17001CA0 RID: 7328
		// (get) Token: 0x0600847F RID: 33919 RVA: 0x00373A5C File Offset: 0x00371E5C
		public float BuffSearch
		{
			[CompilerGenerated]
			get
			{
				return this._buffSearch;
			}
		}

		// Token: 0x17001CA1 RID: 7329
		// (get) Token: 0x06008480 RID: 33920 RVA: 0x00373A64 File Offset: 0x00371E64
		public Threshold SearchWarinessBuffMinMax
		{
			[CompilerGenerated]
			get
			{
				return this._searchWarinessBuffMinMax;
			}
		}

		// Token: 0x17001CA2 RID: 7330
		// (get) Token: 0x06008481 RID: 33921 RVA: 0x00373A6C File Offset: 0x00371E6C
		public Threshold SearchWarinessBuff
		{
			[CompilerGenerated]
			get
			{
				return this._searchWarinessBuff;
			}
		}

		// Token: 0x17001CA3 RID: 7331
		// (get) Token: 0x06008482 RID: 33922 RVA: 0x00373A74 File Offset: 0x00371E74
		public Threshold SearchDarknessDebuffMinMax
		{
			[CompilerGenerated]
			get
			{
				return this._searchDarknessDebuffMinMax;
			}
		}

		// Token: 0x17001CA4 RID: 7332
		// (get) Token: 0x06008483 RID: 33923 RVA: 0x00373A7C File Offset: 0x00371E7C
		public Threshold SearchDarknessDebuff
		{
			[CompilerGenerated]
			get
			{
				return this._searchDarknessDebuff;
			}
		}

		// Token: 0x17001CA5 RID: 7333
		// (get) Token: 0x06008484 RID: 33924 RVA: 0x00373A84 File Offset: 0x00371E84
		public Threshold DrinkWarinessBuffMinMax
		{
			[CompilerGenerated]
			get
			{
				return this._drinkWarinessBuffMinMax;
			}
		}

		// Token: 0x17001CA6 RID: 7334
		// (get) Token: 0x06008485 RID: 33925 RVA: 0x00373A8C File Offset: 0x00371E8C
		public Threshold DrinkWarinessBuff
		{
			[CompilerGenerated]
			get
			{
				return this._drinkWarinessBuff;
			}
		}

		// Token: 0x17001CA7 RID: 7335
		// (get) Token: 0x06008486 RID: 33926 RVA: 0x00373A94 File Offset: 0x00371E94
		public float DebuffMood
		{
			[CompilerGenerated]
			get
			{
				return this._debuffMood;
			}
		}

		// Token: 0x17001CA8 RID: 7336
		// (get) Token: 0x06008487 RID: 33927 RVA: 0x00373A9C File Offset: 0x00371E9C
		public float DebuffMoodInBathDesire
		{
			[CompilerGenerated]
			get
			{
				return this._debuffMoodInBathDesire;
			}
		}

		// Token: 0x17001CA9 RID: 7337
		// (get) Token: 0x06008488 RID: 33928 RVA: 0x00373AA4 File Offset: 0x00371EA4
		public float BuffImmoral
		{
			[CompilerGenerated]
			get
			{
				return this._buffImmoral;
			}
		}

		// Token: 0x17001CAA RID: 7338
		// (get) Token: 0x06008489 RID: 33929 RVA: 0x00373AAC File Offset: 0x00371EAC
		public float GWifeMotivationBuff
		{
			[CompilerGenerated]
			get
			{
				return this._gWifeMotivationBuff;
			}
		}

		// Token: 0x17001CAB RID: 7339
		// (get) Token: 0x0600848A RID: 33930 RVA: 0x00373AB4 File Offset: 0x00371EB4
		public float ActiveBuffMotivation
		{
			[CompilerGenerated]
			get
			{
				return this._activeBuffMotivation;
			}
		}

		// Token: 0x17001CAC RID: 7340
		// (get) Token: 0x0600848B RID: 33931 RVA: 0x00373ABC File Offset: 0x00371EBC
		public float HealthyPhysicalBorder
		{
			[CompilerGenerated]
			get
			{
				return this._healthyPhysicalBorder;
			}
		}

		// Token: 0x17001CAD RID: 7341
		// (get) Token: 0x0600848C RID: 33932 RVA: 0x00373AC4 File Offset: 0x00371EC4
		public float CursedPhysicalBuff
		{
			[CompilerGenerated]
			get
			{
				return this._cursedPhysicalBuff;
			}
		}

		// Token: 0x17001CAE RID: 7342
		// (get) Token: 0x0600848D RID: 33933 RVA: 0x00373ACC File Offset: 0x00371ECC
		public Threshold DarknessPhysicalBuffMinMax
		{
			[CompilerGenerated]
			get
			{
				return this._darknessPhysicalBuffMinMax;
			}
		}

		// Token: 0x17001CAF RID: 7343
		// (get) Token: 0x0600848E RID: 33934 RVA: 0x00373AD4 File Offset: 0x00371ED4
		public Threshold DarknessPhysicalBuff
		{
			[CompilerGenerated]
			get
			{
				return this._darknessPhysicalBuff;
			}
		}

		// Token: 0x17001CB0 RID: 7344
		// (get) Token: 0x0600848F RID: 33935 RVA: 0x00373ADC File Offset: 0x00371EDC
		public Threshold DirtyImmoralMinMax
		{
			[CompilerGenerated]
			get
			{
				return this._dirtyImmoralMinMax;
			}
		}

		// Token: 0x17001CB1 RID: 7345
		// (get) Token: 0x06008490 RID: 33936 RVA: 0x00373AE4 File Offset: 0x00371EE4
		public Threshold DirtyImmoralBuff
		{
			[CompilerGenerated]
			get
			{
				return this._dirtyImmoralBuff;
			}
		}

		// Token: 0x17001CB2 RID: 7346
		// (get) Token: 0x06008491 RID: 33937 RVA: 0x00373AEC File Offset: 0x00371EEC
		public float ImmoralBuff
		{
			[CompilerGenerated]
			get
			{
				return this._immoralBuff;
			}
		}

		// Token: 0x17001CB3 RID: 7347
		// (get) Token: 0x06008492 RID: 33938 RVA: 0x00373AF4 File Offset: 0x00371EF4
		public int LustImmoralBuff
		{
			[CompilerGenerated]
			get
			{
				return this._lustImmoralBuff;
			}
		}

		// Token: 0x17001CB4 RID: 7348
		// (get) Token: 0x06008493 RID: 33939 RVA: 0x00373AFC File Offset: 0x00371EFC
		public int FiredBodyImmoralBuff
		{
			[CompilerGenerated]
			get
			{
				return this._firedBodyImmoralBuff;
			}
		}

		// Token: 0x17001CB5 RID: 7349
		// (get) Token: 0x06008494 RID: 33940 RVA: 0x00373B04 File Offset: 0x00371F04
		public float CursedImmoralBuff
		{
			[CompilerGenerated]
			get
			{
				return this._cursedImmoralBuff;
			}
		}

		// Token: 0x17001CB6 RID: 7350
		// (get) Token: 0x06008495 RID: 33941 RVA: 0x00373B0C File Offset: 0x00371F0C
		public int LesbianFriendlyRelationBorder
		{
			[CompilerGenerated]
			get
			{
				return this._lesbianFriendlyRelationBorder;
			}
		}

		// Token: 0x17001CB7 RID: 7351
		// (get) Token: 0x06008496 RID: 33942 RVA: 0x00373B14 File Offset: 0x00371F14
		public float CanClothChangeBorder
		{
			[CompilerGenerated]
			get
			{
				return this._canClothChangeBorder;
			}
		}

		// Token: 0x17001CB8 RID: 7352
		// (get) Token: 0x06008497 RID: 33943 RVA: 0x00373B1C File Offset: 0x00371F1C
		public Threshold ClothChangePheromoneValueMinMax
		{
			[CompilerGenerated]
			get
			{
				return this._clothChangePheromoneValueMinMax;
			}
		}

		// Token: 0x17001CB9 RID: 7353
		// (get) Token: 0x06008498 RID: 33944 RVA: 0x00373B24 File Offset: 0x00371F24
		public Threshold ClothChangePheromoneValue
		{
			[CompilerGenerated]
			get
			{
				return this._clothChangePheromoneValue;
			}
		}

		// Token: 0x17001CBA RID: 7354
		// (get) Token: 0x06008499 RID: 33945 RVA: 0x00373B2C File Offset: 0x00371F2C
		public int DarknessReduceMaiden
		{
			[CompilerGenerated]
			get
			{
				return this._darknessReduceMaiden;
			}
		}

		// Token: 0x17001CBB RID: 7355
		// (get) Token: 0x0600849A RID: 33946 RVA: 0x00373B34 File Offset: 0x00371F34
		public int ReliabilityGWife
		{
			[CompilerGenerated]
			get
			{
				return this._reliabilityGWifeBuff;
			}
		}

		// Token: 0x17001CBC RID: 7356
		// (get) Token: 0x0600849B RID: 33947 RVA: 0x00373B3C File Offset: 0x00371F3C
		public int MasturbationBorder
		{
			[CompilerGenerated]
			get
			{
				return this._masturbationBorder;
			}
		}

		// Token: 0x17001CBD RID: 7357
		// (get) Token: 0x0600849C RID: 33948 RVA: 0x00373B44 File Offset: 0x00371F44
		public int InvitationBorder
		{
			[CompilerGenerated]
			get
			{
				return this._invitationBorder;
			}
		}

		// Token: 0x17001CBE RID: 7358
		// (get) Token: 0x0600849D RID: 33949 RVA: 0x00373B4C File Offset: 0x00371F4C
		public int RevRapeBorder
		{
			[CompilerGenerated]
			get
			{
				return this._revRapeBorder;
			}
		}

		// Token: 0x17001CBF RID: 7359
		// (get) Token: 0x0600849E RID: 33950 RVA: 0x00373B54 File Offset: 0x00371F54
		public int LesbianBorder
		{
			[CompilerGenerated]
			get
			{
				return this._lesbianBorder;
			}
		}

		// Token: 0x17001CC0 RID: 7360
		// (get) Token: 0x0600849F RID: 33951 RVA: 0x00373B5C File Offset: 0x00371F5C
		public int HoldingHandBorderReliability
		{
			[CompilerGenerated]
			get
			{
				return this._holdingHandBorderReliability;
			}
		}

		// Token: 0x17001CC1 RID: 7361
		// (get) Token: 0x060084A0 RID: 33952 RVA: 0x00373B64 File Offset: 0x00371F64
		public int ApproachBorderReliability
		{
			[CompilerGenerated]
			get
			{
				return this._approachBorderReliability;
			}
		}

		// Token: 0x17001CC2 RID: 7362
		// (get) Token: 0x060084A1 RID: 33953 RVA: 0x00373B6C File Offset: 0x00371F6C
		public float CanGreetBorder
		{
			[CompilerGenerated]
			get
			{
				return (float)this._canGreetBorder;
			}
		}

		// Token: 0x17001CC3 RID: 7363
		// (get) Token: 0x060084A2 RID: 33954 RVA: 0x00373B75 File Offset: 0x00371F75
		public float CanDressBorder
		{
			[CompilerGenerated]
			get
			{
				return this._canDressBorder;
			}
		}

		// Token: 0x17001CC4 RID: 7364
		// (get) Token: 0x060084A3 RID: 33955 RVA: 0x00373B7D File Offset: 0x00371F7D
		public int WashFaceBorder
		{
			[CompilerGenerated]
			get
			{
				return this._washFaceBorder;
			}
		}

		// Token: 0x17001CC5 RID: 7365
		// (get) Token: 0x060084A4 RID: 33956 RVA: 0x00373B85 File Offset: 0x00371F85
		public int NightLightBorder
		{
			[CompilerGenerated]
			get
			{
				return this._nightLightBorder;
			}
		}

		// Token: 0x17001CC6 RID: 7366
		// (get) Token: 0x060084A5 RID: 33957 RVA: 0x00373B8D File Offset: 0x00371F8D
		public int SurpriseBorder
		{
			[CompilerGenerated]
			get
			{
				return this._surpriseBorder;
			}
		}

		// Token: 0x17001CC7 RID: 7367
		// (get) Token: 0x060084A6 RID: 33958 RVA: 0x00373B95 File Offset: 0x00371F95
		public int GirlsActionBorder
		{
			[CompilerGenerated]
			get
			{
				return this._girlsActionBorder;
			}
		}

		// Token: 0x17001CC8 RID: 7368
		// (get) Token: 0x060084A7 RID: 33959 RVA: 0x00373B9D File Offset: 0x00371F9D
		public int TalkRelationUpperBorder
		{
			[CompilerGenerated]
			get
			{
				return this._talkRelationUpperBorder;
			}
		}

		// Token: 0x17001CC9 RID: 7369
		// (get) Token: 0x060084A8 RID: 33960 RVA: 0x00373BA5 File Offset: 0x00371FA5
		public int LesbianSociabilityBuffBorder
		{
			[CompilerGenerated]
			get
			{
				return this._lesbianSociabilityBuffBorder;
			}
		}

		// Token: 0x17001CCA RID: 7370
		// (get) Token: 0x060084A9 RID: 33961 RVA: 0x00373BAD File Offset: 0x00371FAD
		public Threshold FlavorCookSuccessBoostMinMax
		{
			[CompilerGenerated]
			get
			{
				return this._flavorCookSuccessBoostMinMax;
			}
		}

		// Token: 0x17001CCB RID: 7371
		// (get) Token: 0x060084AA RID: 33962 RVA: 0x00373BB5 File Offset: 0x00371FB5
		public Threshold FlavorCookSuccessBoost
		{
			[CompilerGenerated]
			get
			{
				return this._flavorCookSuccessBoost;
			}
		}

		// Token: 0x17001CCC RID: 7372
		// (get) Token: 0x060084AB RID: 33963 RVA: 0x00373BBD File Offset: 0x00371FBD
		public int ChefCookSuccessBoost
		{
			[CompilerGenerated]
			get
			{
				return this._chefCookSuccessBoost;
			}
		}

		// Token: 0x17001CCD RID: 7373
		// (get) Token: 0x060084AC RID: 33964 RVA: 0x00373BC5 File Offset: 0x00371FC5
		public Threshold FlavorCatCaptureMinMax
		{
			[CompilerGenerated]
			get
			{
				return this._flavorCatCaptureMinMax;
			}
		}

		// Token: 0x17001CCE RID: 7374
		// (get) Token: 0x060084AD RID: 33965 RVA: 0x00373BCD File Offset: 0x00371FCD
		public Threshold FlavorCatCaptureRate
		{
			[CompilerGenerated]
			get
			{
				return this._flavorCatCaptureRate;
			}
		}

		// Token: 0x17001CCF RID: 7375
		// (get) Token: 0x060084AE RID: 33966 RVA: 0x00373BD5 File Offset: 0x00371FD5
		public int CatCaptureProbBuff
		{
			[CompilerGenerated]
			get
			{
				return this._catCaptureProbBuff;
			}
		}

		// Token: 0x17001CD0 RID: 7376
		// (get) Token: 0x060084AF RID: 33967 RVA: 0x00373BDD File Offset: 0x00371FDD
		public float DefaultInstructionRate
		{
			[CompilerGenerated]
			get
			{
				return this._defaultInstructionRate;
			}
		}

		// Token: 0x17001CD1 RID: 7377
		// (get) Token: 0x060084B0 RID: 33968 RVA: 0x00373BE5 File Offset: 0x00371FE5
		public Threshold FlavorReliabilityInstructionMinMax
		{
			[CompilerGenerated]
			get
			{
				return this._flavorReliabilityInstructionMinMax;
			}
		}

		// Token: 0x17001CD2 RID: 7378
		// (get) Token: 0x060084B1 RID: 33969 RVA: 0x00373BED File Offset: 0x00371FED
		public Threshold FlavorReliabilityInstruction
		{
			[CompilerGenerated]
			get
			{
				return this._flavorReliabilityInstruction;
			}
		}

		// Token: 0x17001CD3 RID: 7379
		// (get) Token: 0x060084B2 RID: 33970 RVA: 0x00373BF5 File Offset: 0x00371FF5
		public float InstructionRateDebuff
		{
			[CompilerGenerated]
			get
			{
				return this._instructionRateDebuff;
			}
		}

		// Token: 0x17001CD4 RID: 7380
		// (get) Token: 0x060084B3 RID: 33971 RVA: 0x00373BFD File Offset: 0x00371FFD
		public float DefaultFollowRate
		{
			[CompilerGenerated]
			get
			{
				return this._defaultFollowRate;
			}
		}

		// Token: 0x17001CD5 RID: 7381
		// (get) Token: 0x060084B4 RID: 33972 RVA: 0x00373C05 File Offset: 0x00372005
		public Threshold FollowReliabilityMinMax
		{
			[CompilerGenerated]
			get
			{
				return this._followReliabilityMinMax;
			}
		}

		// Token: 0x17001CD6 RID: 7382
		// (get) Token: 0x060084B5 RID: 33973 RVA: 0x00373C0D File Offset: 0x0037200D
		public Threshold FollowRateReliabilityBuff
		{
			[CompilerGenerated]
			get
			{
				return this._followRateReliabilityBuff;
			}
		}

		// Token: 0x17001CD7 RID: 7383
		// (get) Token: 0x060084B6 RID: 33974 RVA: 0x00373C15 File Offset: 0x00372015
		public float FollowRateBuff
		{
			[CompilerGenerated]
			get
			{
				return this._followRateBuff;
			}
		}

		// Token: 0x17001CD8 RID: 7384
		// (get) Token: 0x060084B7 RID: 33975 RVA: 0x00373C1D File Offset: 0x0037201D
		public Threshold DropBuffMinMax
		{
			[CompilerGenerated]
			get
			{
				return this._dropBuffMinMax;
			}
		}

		// Token: 0x17001CD9 RID: 7385
		// (get) Token: 0x060084B8 RID: 33976 RVA: 0x00373C25 File Offset: 0x00372025
		public Threshold DropBuff
		{
			[CompilerGenerated]
			get
			{
				return this._dropBuff;
			}
		}

		// Token: 0x17001CDA RID: 7386
		// (get) Token: 0x060084B9 RID: 33977 RVA: 0x00373C2D File Offset: 0x0037202D
		public float GirlsActionProb
		{
			[CompilerGenerated]
			get
			{
				return this._girlsActionProb;
			}
		}

		// Token: 0x17001CDB RID: 7387
		// (get) Token: 0x060084BA RID: 33978 RVA: 0x00373C35 File Offset: 0x00372035
		public float LesbianRate
		{
			[CompilerGenerated]
			get
			{
				return this._lesbianRate;
			}
		}

		// Token: 0x17001CDC RID: 7388
		// (get) Token: 0x060084BB RID: 33979 RVA: 0x00373C3D File Offset: 0x0037203D
		public float ShallowSleepProb
		{
			[CompilerGenerated]
			get
			{
				return this._shallowSleepProb;
			}
		}

		// Token: 0x17001CDD RID: 7389
		// (get) Token: 0x060084BC RID: 33980 RVA: 0x00373C45 File Offset: 0x00372045
		public Threshold YobaiMinMax
		{
			[CompilerGenerated]
			get
			{
				return this._yobaiMinMax;
			}
		}

		// Token: 0x17001CDE RID: 7390
		// (get) Token: 0x060084BD RID: 33981 RVA: 0x00373C4D File Offset: 0x0037204D
		public float CallProbBaseRate
		{
			[CompilerGenerated]
			get
			{
				return this._callProbBaseRate;
			}
		}

		// Token: 0x17001CDF RID: 7391
		// (get) Token: 0x060084BE RID: 33982 RVA: 0x00373C55 File Offset: 0x00372055
		public float CallProbPhaseRate
		{
			[CompilerGenerated]
			get
			{
				return this._callProbPhaseRate;
			}
		}

		// Token: 0x17001CE0 RID: 7392
		// (get) Token: 0x060084BF RID: 33983 RVA: 0x00373C5D File Offset: 0x0037205D
		public int[] CallReliabilityBorder
		{
			[CompilerGenerated]
			get
			{
				return this._callReliabilityBorder;
			}
		}

		// Token: 0x17001CE1 RID: 7393
		// (get) Token: 0x060084C0 RID: 33984 RVA: 0x00373C65 File Offset: 0x00372065
		public float[] CallReliabilityBuff
		{
			[CompilerGenerated]
			get
			{
				return this._callReliabilityBuff;
			}
		}

		// Token: 0x17001CE2 RID: 7394
		// (get) Token: 0x060084C1 RID: 33985 RVA: 0x00373C6D File Offset: 0x0037206D
		public float CallLowerMoodProb
		{
			[CompilerGenerated]
			get
			{
				return this._callLowerMoodProb;
			}
		}

		// Token: 0x17001CE3 RID: 7395
		// (get) Token: 0x060084C2 RID: 33986 RVA: 0x00373C75 File Offset: 0x00372075
		public float CallUpperMoodProb
		{
			[CompilerGenerated]
			get
			{
				return this._callUpperMoodProb;
			}
		}

		// Token: 0x17001CE4 RID: 7396
		// (get) Token: 0x060084C3 RID: 33987 RVA: 0x00373C7D File Offset: 0x0037207D
		public float CallSecondTimeProb
		{
			[CompilerGenerated]
			get
			{
				return this._callSecondTimeProb;
			}
		}

		// Token: 0x17001CE5 RID: 7397
		// (get) Token: 0x060084C4 RID: 33988 RVA: 0x00373C85 File Offset: 0x00372085
		public float CallOverTimeProb
		{
			[CompilerGenerated]
			get
			{
				return this._callOverTimeProb;
			}
		}

		// Token: 0x17001CE6 RID: 7398
		// (get) Token: 0x060084C5 RID: 33989 RVA: 0x00373C8D File Offset: 0x0037208D
		public float CallProbSuperSense
		{
			[CompilerGenerated]
			get
			{
				return this._callProbSuperSense;
			}
		}

		// Token: 0x17001CE7 RID: 7399
		// (get) Token: 0x060084C6 RID: 33990 RVA: 0x00373C95 File Offset: 0x00372095
		public float HandSearchProbBuff
		{
			[CompilerGenerated]
			get
			{
				return this._handSearchProbBuff;
			}
		}

		// Token: 0x17001CE8 RID: 7400
		// (get) Token: 0x060084C7 RID: 33991 RVA: 0x00373C9D File Offset: 0x0037209D
		public float FishingSearchProbBuff
		{
			[CompilerGenerated]
			get
			{
				return this._fishingSearchProbBuff;
			}
		}

		// Token: 0x17001CE9 RID: 7401
		// (get) Token: 0x060084C8 RID: 33992 RVA: 0x00373CA5 File Offset: 0x003720A5
		public float PickelSearchProbBuff
		{
			[CompilerGenerated]
			get
			{
				return this._pickelSearchProbBuff;
			}
		}

		// Token: 0x17001CEA RID: 7402
		// (get) Token: 0x060084C9 RID: 33993 RVA: 0x00373CAD File Offset: 0x003720AD
		public float ShovelSearchProbBuff
		{
			[CompilerGenerated]
			get
			{
				return this._shovelSearchProbBuff;
			}
		}

		// Token: 0x17001CEB RID: 7403
		// (get) Token: 0x060084CA RID: 33994 RVA: 0x00373CB5 File Offset: 0x003720B5
		public float NetSearchProbBuff
		{
			[CompilerGenerated]
			get
			{
				return this._netSearchProbBuff;
			}
		}

		// Token: 0x17001CEC RID: 7404
		// (get) Token: 0x060084CB RID: 33995 RVA: 0x00373CBD File Offset: 0x003720BD
		public float ColdDefaultIncidence
		{
			[CompilerGenerated]
			get
			{
				return this._coldDefaultIncidence;
			}
		}

		// Token: 0x17001CED RID: 7405
		// (get) Token: 0x060084CC RID: 33996 RVA: 0x00373CC5 File Offset: 0x003720C5
		public float ColdLockDuration
		{
			[CompilerGenerated]
			get
			{
				return this._coldLockDuration;
			}
		}

		// Token: 0x17001CEE RID: 7406
		// (get) Token: 0x060084CD RID: 33997 RVA: 0x00373CCD File Offset: 0x003720CD
		public int ColdBaseDuration
		{
			[CompilerGenerated]
			get
			{
				return this._coldBaseDuration;
			}
		}

		// Token: 0x17001CEF RID: 7407
		// (get) Token: 0x060084CE RID: 33998 RVA: 0x00373CD5 File Offset: 0x003720D5
		public float HeatStrokeDefaultIncidence
		{
			[CompilerGenerated]
			get
			{
				return this._heatStrokeDefaultIncidence;
			}
		}

		// Token: 0x17001CF0 RID: 7408
		// (get) Token: 0x060084CF RID: 33999 RVA: 0x00373CDD File Offset: 0x003720DD
		public float HeatStrokeLockDuration
		{
			[CompilerGenerated]
			get
			{
				return this._coldLockDuration;
			}
		}

		// Token: 0x17001CF1 RID: 7409
		// (get) Token: 0x060084D0 RID: 34000 RVA: 0x00373CE5 File Offset: 0x003720E5
		public float HurtDefaultIncidence
		{
			[CompilerGenerated]
			get
			{
				return this._hurtDefaultIncidence;
			}
		}

		// Token: 0x17001CF2 RID: 7410
		// (get) Token: 0x060084D1 RID: 34001 RVA: 0x00373CED File Offset: 0x003720ED
		public Threshold StomachacheRateDebuffMinMax
		{
			[CompilerGenerated]
			get
			{
				return this._stomachacheRateDebuffMinMax;
			}
		}

		// Token: 0x17001CF3 RID: 7411
		// (get) Token: 0x060084D2 RID: 34002 RVA: 0x00373CF5 File Offset: 0x003720F5
		public Threshold StomachacheRateBuff
		{
			[CompilerGenerated]
			get
			{
				return this._stomachacheRateBuff;
			}
		}

		// Token: 0x17001CF4 RID: 7412
		// (get) Token: 0x060084D3 RID: 34003 RVA: 0x00373CFD File Offset: 0x003720FD
		public Threshold ColdRateBuffMinMax
		{
			[CompilerGenerated]
			get
			{
				return this._coldRateBuffMinMax;
			}
		}

		// Token: 0x17001CF5 RID: 7413
		// (get) Token: 0x060084D4 RID: 34004 RVA: 0x00373D05 File Offset: 0x00372105
		public Threshold ColdRateBuff
		{
			[CompilerGenerated]
			get
			{
				return this._coldRateBuff;
			}
		}

		// Token: 0x17001CF6 RID: 7414
		// (get) Token: 0x060084D5 RID: 34005 RVA: 0x00373D0D File Offset: 0x0037210D
		public Threshold HeatStrokeRateBuffMinMax
		{
			[CompilerGenerated]
			get
			{
				return this._coldRateBuffMinMax;
			}
		}

		// Token: 0x17001CF7 RID: 7415
		// (get) Token: 0x060084D6 RID: 34006 RVA: 0x00373D15 File Offset: 0x00372115
		public Threshold HeatStrokeRateBuff
		{
			[CompilerGenerated]
			get
			{
				return this._heatStrokeRateBuff;
			}
		}

		// Token: 0x17001CF8 RID: 7416
		// (get) Token: 0x060084D7 RID: 34007 RVA: 0x00373D1D File Offset: 0x0037211D
		public Threshold HurtRateBuffMinMax
		{
			[CompilerGenerated]
			get
			{
				return this._hurtRateBuffMinMax;
			}
		}

		// Token: 0x17001CF9 RID: 7417
		// (get) Token: 0x060084D8 RID: 34008 RVA: 0x00373D25 File Offset: 0x00372125
		public Threshold HurtRateBuff
		{
			[CompilerGenerated]
			get
			{
				return this._hurtRateBuff;
			}
		}

		// Token: 0x17001CFA RID: 7418
		// (get) Token: 0x060084D9 RID: 34009 RVA: 0x00373D2D File Offset: 0x0037212D
		public Threshold SickIncidenceDarknessBuffMinMax
		{
			[CompilerGenerated]
			get
			{
				return this._sickIncidenceDarknessBuffMinMax;
			}
		}

		// Token: 0x17001CFB RID: 7419
		// (get) Token: 0x060084DA RID: 34010 RVA: 0x00373D35 File Offset: 0x00372135
		public Threshold SickIncidenceDarknessBuff
		{
			[CompilerGenerated]
			get
			{
				return this._sickIncidenceDarknessBuff;
			}
		}

		// Token: 0x17001CFC RID: 7420
		// (get) Token: 0x060084DB RID: 34011 RVA: 0x00373D3D File Offset: 0x0037213D
		public float ColdRateDebuffWeak
		{
			[CompilerGenerated]
			get
			{
				return this._coldRateDebuffWeak;
			}
		}

		// Token: 0x17001CFD RID: 7421
		// (get) Token: 0x060084DC RID: 34012 RVA: 0x00373D45 File Offset: 0x00372145
		public float HeatStrokeBuffGuts
		{
			[CompilerGenerated]
			get
			{
				return this._heatStrokeBuffGuts;
			}
		}

		// Token: 0x17001CFE RID: 7422
		// (get) Token: 0x060084DD RID: 34013 RVA: 0x00373D4D File Offset: 0x0037214D
		public int StarveWarinessValue
		{
			[CompilerGenerated]
			get
			{
				return this._starveWarinessValue;
			}
		}

		// Token: 0x17001CFF RID: 7423
		// (get) Token: 0x060084DE RID: 34014 RVA: 0x00373D55 File Offset: 0x00372155
		public int StarveDarknessValue
		{
			[CompilerGenerated]
			get
			{
				return this._starveDarknessValue;
			}
		}

		// Token: 0x17001D00 RID: 7424
		// (get) Token: 0x060084DF RID: 34015 RVA: 0x00373D5D File Offset: 0x0037215D
		public float WetRateInRain
		{
			[CompilerGenerated]
			get
			{
				return this._wetRateInRain;
			}
		}

		// Token: 0x17001D01 RID: 7425
		// (get) Token: 0x060084E0 RID: 34016 RVA: 0x00373D65 File Offset: 0x00372165
		public float WetRateInStorm
		{
			[CompilerGenerated]
			get
			{
				return this._wetRateInStorm;
			}
		}

		// Token: 0x17001D02 RID: 7426
		// (get) Token: 0x060084E1 RID: 34017 RVA: 0x00373D6D File Offset: 0x0037216D
		public float DrySpeed
		{
			[CompilerGenerated]
			get
			{
				return this._drySpeed;
			}
		}

		// Token: 0x17001D03 RID: 7427
		// (get) Token: 0x060084E2 RID: 34018 RVA: 0x00373D75 File Offset: 0x00372175
		public float WetTemperatureRate
		{
			[CompilerGenerated]
			get
			{
				return this._wetTemperatureRate;
			}
		}

		// Token: 0x17001D04 RID: 7428
		// (get) Token: 0x060084E3 RID: 34019 RVA: 0x00373D7D File Offset: 0x0037217D
		public float ColdTemperatureValue
		{
			[CompilerGenerated]
			get
			{
				return this._coldTemperatureValue;
			}
		}

		// Token: 0x17001D05 RID: 7429
		// (get) Token: 0x060084E4 RID: 34020 RVA: 0x00373D85 File Offset: 0x00372185
		public float HotTemperatureValue
		{
			[CompilerGenerated]
			get
			{
				return this._hotTemperatureValue;
			}
		}

		// Token: 0x17001D06 RID: 7430
		// (get) Token: 0x060084E5 RID: 34021 RVA: 0x00373D8D File Offset: 0x0037218D
		public float LesbianBorderDesire
		{
			[CompilerGenerated]
			get
			{
				return this._lesbianBorderDesire;
			}
		}

		// Token: 0x17001D07 RID: 7431
		// (get) Token: 0x060084E6 RID: 34022 RVA: 0x00373D95 File Offset: 0x00372195
		public float ShallowSleepHungerLowBorder
		{
			[CompilerGenerated]
			get
			{
				return this._shallowSleepHungerLowBorder;
			}
		}

		// Token: 0x17001D08 RID: 7432
		// (get) Token: 0x060084E7 RID: 34023 RVA: 0x00373D9D File Offset: 0x0037219D
		public int LampEquipableBorder
		{
			[CompilerGenerated]
			get
			{
				return this._lampEquipableBorder;
			}
		}

		// Token: 0x17001D09 RID: 7433
		// (get) Token: 0x060084E8 RID: 34024 RVA: 0x00373DA5 File Offset: 0x003721A5
		public EnvironmentSimulator.DateTimeSerialization ShouldRestoreCoordTime
		{
			[CompilerGenerated]
			get
			{
				return this._shouldRestoreCoordTime;
			}
		}

		// Token: 0x17001D0A RID: 7434
		// (get) Token: 0x060084E9 RID: 34025 RVA: 0x00373DAD File Offset: 0x003721AD
		public float RestoreRangeMinuteTime
		{
			[CompilerGenerated]
			get
			{
				return this._restoreRangeMinuteTime;
			}
		}

		// Token: 0x17001D0B RID: 7435
		// (get) Token: 0x060084EA RID: 34026 RVA: 0x00373DB5 File Offset: 0x003721B5
		public int SoineReliabilityBorder
		{
			[CompilerGenerated]
			get
			{
				return this._soineReliabilityBorder;
			}
		}

		// Token: 0x17001D0C RID: 7436
		// (get) Token: 0x060084EB RID: 34027 RVA: 0x00373DBD File Offset: 0x003721BD
		public float PotionImmoralAdd
		{
			[CompilerGenerated]
			get
			{
				return this._potionImmoralAdd;
			}
		}

		// Token: 0x17001D0D RID: 7437
		// (get) Token: 0x060084EC RID: 34028 RVA: 0x00373DC5 File Offset: 0x003721C5
		public float DiureticToiletAdd
		{
			[CompilerGenerated]
			get
			{
				return this._diureticToiletAdd;
			}
		}

		// Token: 0x17001D0E RID: 7438
		// (get) Token: 0x060084ED RID: 34029 RVA: 0x00373DCD File Offset: 0x003721CD
		public float PillSleepAdd
		{
			[CompilerGenerated]
			get
			{
				return this._pillSleepAdd;
			}
		}

		// Token: 0x17001D0F RID: 7439
		// (get) Token: 0x060084EE RID: 34030 RVA: 0x00373DD5 File Offset: 0x003721D5
		public int SleepFlagCount
		{
			[CompilerGenerated]
			get
			{
				return this._sleepFlagCount;
			}
		}

		// Token: 0x17001D10 RID: 7440
		// (get) Token: 0x060084EF RID: 34031 RVA: 0x00373DDD File Offset: 0x003721DD
		public int EatTogetherFlagCount
		{
			[CompilerGenerated]
			get
			{
				return this._eatTogetherFlagCount;
			}
		}

		// Token: 0x17001D11 RID: 7441
		// (get) Token: 0x060084F0 RID: 34032 RVA: 0x00373DE5 File Offset: 0x003721E5
		public int ColdFlagCount
		{
			[CompilerGenerated]
			get
			{
				return this._coldFlagCount;
			}
		}

		// Token: 0x17001D12 RID: 7442
		// (get) Token: 0x060084F1 RID: 34033 RVA: 0x00373DED File Offset: 0x003721ED
		public int PlayFlagCount
		{
			[CompilerGenerated]
			get
			{
				return this._playFlagCount;
			}
		}

		// Token: 0x17001D13 RID: 7443
		// (get) Token: 0x060084F2 RID: 34034 RVA: 0x00373DF5 File Offset: 0x003721F5
		public int DressFlagCount
		{
			[CompilerGenerated]
			get
			{
				return this._dressFlagCount;
			}
		}

		// Token: 0x17001D14 RID: 7444
		// (get) Token: 0x060084F3 RID: 34035 RVA: 0x00373DFD File Offset: 0x003721FD
		public int GatherFlagCount
		{
			[CompilerGenerated]
			get
			{
				return this._gatherFlagCount;
			}
		}

		// Token: 0x17001D15 RID: 7445
		// (get) Token: 0x060084F4 RID: 34036 RVA: 0x00373E05 File Offset: 0x00372205
		public int SleepTogetherFlagCount
		{
			[CompilerGenerated]
			get
			{
				return this._sleepTogetherFlagCount;
			}
		}

		// Token: 0x060084F5 RID: 34037 RVA: 0x00373E10 File Offset: 0x00372210
		public int GetFlagCount(int id)
		{
			switch (id)
			{
			case 0:
				return this._sleepFlagCount;
			case 1:
				return this._eatTogetherFlagCount;
			case 2:
				return this._coldFlagCount;
			case 3:
				return this._playFlagCount;
			case 4:
				return this._dressFlagCount;
			case 5:
				return this._gatherFlagCount;
			case 6:
				return this._sleepTogetherFlagCount;
			default:
				return 0;
			}
		}

		// Token: 0x060084F6 RID: 34038 RVA: 0x00373E78 File Offset: 0x00372278
		public bool TryGetFlagCount(int id, out int result)
		{
			switch (id)
			{
			case 0:
				result = this._sleepFlagCount;
				return true;
			case 1:
				result = this._eatTogetherFlagCount;
				return true;
			case 2:
				result = this._coldFlagCount;
				return true;
			case 3:
				result = this._playFlagCount;
				return true;
			case 4:
				result = this._dressFlagCount;
				return true;
			case 5:
				result = this._gatherFlagCount;
				return true;
			case 6:
				result = this._sleepTogetherFlagCount;
				return true;
			default:
				result = 0;
				return false;
			}
		}

		// Token: 0x17001D16 RID: 7446
		// (get) Token: 0x060084F7 RID: 34039 RVA: 0x00373EF6 File Offset: 0x003722F6
		public int CheckPetCountCond
		{
			[CompilerGenerated]
			get
			{
				return this._checkPetCountCond;
			}
		}

		// Token: 0x17001D17 RID: 7447
		// (get) Token: 0x060084F8 RID: 34040 RVA: 0x00373EFE File Offset: 0x003722FE
		public int RandomADVReliabilityCond
		{
			[CompilerGenerated]
			get
			{
				return this._randomADVReliabilityCond;
			}
		}

		// Token: 0x04006AF2 RID: 27378
		[SerializeField]
		private Threshold _cookPheromoneBuffMinMax = new Threshold(0f, 1f);

		// Token: 0x04006AF3 RID: 27379
		[SerializeField]
		private Threshold _cookPheromoneBuff = new Threshold(0f, 1f);

		// Token: 0x04006AF4 RID: 27380
		[SerializeField]
		private float _buffCook;

		// Token: 0x04006AF5 RID: 27381
		[SerializeField]
		private float _buffBath;

		// Token: 0x04006AF6 RID: 27382
		[SerializeField]
		private float _buffAnimal;

		// Token: 0x04006AF7 RID: 27383
		[SerializeField]
		private float _buffSleep;

		// Token: 0x04006AF8 RID: 27384
		[SerializeField]
		private Threshold _sleepSociabilityBuffMinMax = new Threshold(0f, 1f);

		// Token: 0x04006AF9 RID: 27385
		[SerializeField]
		private Threshold _sleepSociabilityBuff = new Threshold(0f, 1f);

		// Token: 0x04006AFA RID: 27386
		[SerializeField]
		private float _buffGift;

		// Token: 0x04006AFB RID: 27387
		[SerializeField]
		private Threshold _giftReliabilityBuffMinMax = new Threshold(0f, 1f);

		// Token: 0x04006AFC RID: 27388
		[SerializeField]
		private Threshold _giftReliabilityBuff = new Threshold(0f, 1f);

		// Token: 0x04006AFD RID: 27389
		[SerializeField]
		private float _buffGimme;

		// Token: 0x04006AFE RID: 27390
		[SerializeField]
		private Threshold _gimmeDarknessBuffMinMax = new Threshold(0f, 1f);

		// Token: 0x04006AFF RID: 27391
		[SerializeField]
		private Threshold _gimmeDarknessBuff = new Threshold(0f, 1f);

		// Token: 0x04006B00 RID: 27392
		[SerializeField]
		private Threshold _gimmeWarinessBuffMinMax = new Threshold(0f, 1f);

		// Token: 0x04006B01 RID: 27393
		[SerializeField]
		private Threshold _gimmeWarinessBuff = new Threshold(0f, 1f);

		// Token: 0x04006B02 RID: 27394
		[SerializeField]
		private float _buffEat;

		// Token: 0x04006B03 RID: 27395
		[SerializeField]
		private Threshold _eatPheromoneDebuffMinMax = new Threshold(0f, 1f);

		// Token: 0x04006B04 RID: 27396
		[SerializeField]
		private Threshold _eatPheromoneDebuff = new Threshold(0f, 1f);

		// Token: 0x04006B05 RID: 27397
		[SerializeField]
		private Threshold _eatDarknessDebuffMinMax = new Threshold(0f, 1f);

		// Token: 0x04006B06 RID: 27398
		[SerializeField]
		private Threshold _eatDarknessDebuff = new Threshold(0f, 1f);

		// Token: 0x04006B07 RID: 27399
		[SerializeField]
		private Threshold _eatInstinctBuffMinMax = new Threshold(0f, 1f);

		// Token: 0x04006B08 RID: 27400
		[SerializeField]
		private Threshold _eatInstinctBuff = new Threshold(0f, 1f);

		// Token: 0x04006B09 RID: 27401
		[SerializeField]
		private float _buffPlay;

		// Token: 0x04006B0A RID: 27402
		[SerializeField]
		private Threshold _playReasonDebuffMinMax = new Threshold(0f, 1f);

		// Token: 0x04006B0B RID: 27403
		[SerializeField]
		private Threshold _playReasonDebuff = new Threshold(0f, 1f);

		// Token: 0x04006B0C RID: 27404
		[SerializeField]
		private Threshold _playInstinctBuffMinMax = new Threshold(0f, 1f);

		// Token: 0x04006B0D RID: 27405
		[SerializeField]
		private Threshold _playInstinctBuff = new Threshold(0f, 1f);

		// Token: 0x04006B0E RID: 27406
		[SerializeField]
		private float _buffH;

		// Token: 0x04006B0F RID: 27407
		[SerializeField]
		private Threshold _hDirtyBuffMinMax = new Threshold(0f, 1f);

		// Token: 0x04006B10 RID: 27408
		[SerializeField]
		private Threshold _hDirtyBuff = new Threshold(0f, 1f);

		// Token: 0x04006B11 RID: 27409
		[SerializeField]
		private float _cursedHBuff;

		// Token: 0x04006B12 RID: 27410
		[SerializeField]
		private float _buffLonely;

		// Token: 0x04006B13 RID: 27411
		[SerializeField]
		private float _buffLonelySuperSense;

		// Token: 0x04006B14 RID: 27412
		[SerializeField]
		private Threshold _lonelySociabilityBuffMinMax = new Threshold(0f, 1f);

		// Token: 0x04006B15 RID: 27413
		[SerializeField]
		private Threshold _lonelySociabilityBuff = new Threshold(0f, 1f);

		// Token: 0x04006B16 RID: 27414
		[SerializeField]
		private Threshold _breakReasonBuffMinMax = new Threshold(0f, 1f);

		// Token: 0x04006B17 RID: 27415
		[SerializeField]
		private Threshold _breakReasonBuff = new Threshold(0f, 1f);

		// Token: 0x04006B18 RID: 27416
		[SerializeField]
		private Threshold _breakInstinctBuffMinMax = new Threshold(0f, 1f);

		// Token: 0x04006B19 RID: 27417
		[SerializeField]
		private Threshold _breakInstinctBuff = new Threshold(0f, 1f);

		// Token: 0x04006B1A RID: 27418
		[SerializeField]
		private float _buffBreak;

		// Token: 0x04006B1B RID: 27419
		[SerializeField]
		private float _buffLocation;

		// Token: 0x04006B1C RID: 27420
		[SerializeField]
		private float _buffSearchTough;

		// Token: 0x04006B1D RID: 27421
		[SerializeField]
		private float _buffSearch;

		// Token: 0x04006B1E RID: 27422
		[SerializeField]
		private Threshold _searchWarinessBuffMinMax = new Threshold(0f, 1f);

		// Token: 0x04006B1F RID: 27423
		[SerializeField]
		private Threshold _searchWarinessBuff = new Threshold(0f, 1f);

		// Token: 0x04006B20 RID: 27424
		[SerializeField]
		private Threshold _searchDarknessDebuffMinMax = new Threshold(0f, 1f);

		// Token: 0x04006B21 RID: 27425
		[SerializeField]
		private Threshold _searchDarknessDebuff = new Threshold(0f, 1f);

		// Token: 0x04006B22 RID: 27426
		[SerializeField]
		private Threshold _drinkWarinessBuffMinMax = new Threshold(0f, 1f);

		// Token: 0x04006B23 RID: 27427
		[SerializeField]
		private Threshold _drinkWarinessBuff = new Threshold(0f, 1f);

		// Token: 0x04006B24 RID: 27428
		[SerializeField]
		private float _debuffMood;

		// Token: 0x04006B25 RID: 27429
		[SerializeField]
		private float _debuffMoodInBathDesire;

		// Token: 0x04006B26 RID: 27430
		[SerializeField]
		private float _buffImmoral;

		// Token: 0x04006B27 RID: 27431
		[SerializeField]
		private float _gWifeMotivationBuff;

		// Token: 0x04006B28 RID: 27432
		[SerializeField]
		private float _activeBuffMotivation;

		// Token: 0x04006B29 RID: 27433
		[SerializeField]
		private float _healthyPhysicalBorder;

		// Token: 0x04006B2A RID: 27434
		[SerializeField]
		private float _cursedPhysicalBuff;

		// Token: 0x04006B2B RID: 27435
		[SerializeField]
		private Threshold _darknessPhysicalBuffMinMax = new Threshold(0f, 1f);

		// Token: 0x04006B2C RID: 27436
		[SerializeField]
		private Threshold _darknessPhysicalBuff = new Threshold(0f, 1f);

		// Token: 0x04006B2D RID: 27437
		[SerializeField]
		private Threshold _dirtyImmoralMinMax = new Threshold(0f, 1f);

		// Token: 0x04006B2E RID: 27438
		[SerializeField]
		private Threshold _dirtyImmoralBuff = new Threshold(0f, 1f);

		// Token: 0x04006B2F RID: 27439
		[SerializeField]
		private float _immoralBuff;

		// Token: 0x04006B30 RID: 27440
		[SerializeField]
		private int _lustImmoralBuff;

		// Token: 0x04006B31 RID: 27441
		[SerializeField]
		private int _firedBodyImmoralBuff;

		// Token: 0x04006B32 RID: 27442
		[SerializeField]
		private float _cursedImmoralBuff;

		// Token: 0x04006B33 RID: 27443
		[SerializeField]
		private int _lesbianFriendlyRelationBorder;

		// Token: 0x04006B34 RID: 27444
		[SerializeField]
		private float _canClothChangeBorder;

		// Token: 0x04006B35 RID: 27445
		[SerializeField]
		private Threshold _clothChangePheromoneValueMinMax = new Threshold(0f, 1f);

		// Token: 0x04006B36 RID: 27446
		[SerializeField]
		private Threshold _clothChangePheromoneValue = new Threshold(0f, 1f);

		// Token: 0x04006B37 RID: 27447
		[SerializeField]
		private int _darknessReduceMaiden;

		// Token: 0x04006B38 RID: 27448
		[SerializeField]
		private int _reliabilityGWifeBuff;

		// Token: 0x04006B39 RID: 27449
		[SerializeField]
		private int _masturbationBorder;

		// Token: 0x04006B3A RID: 27450
		[SerializeField]
		private int _invitationBorder;

		// Token: 0x04006B3B RID: 27451
		[SerializeField]
		private int _revRapeBorder;

		// Token: 0x04006B3C RID: 27452
		[SerializeField]
		private int _lesbianBorder;

		// Token: 0x04006B3D RID: 27453
		[SerializeField]
		private int _holdingHandBorderReliability;

		// Token: 0x04006B3E RID: 27454
		[SerializeField]
		private int _approachBorderReliability;

		// Token: 0x04006B3F RID: 27455
		[SerializeField]
		private int _canGreetBorder;

		// Token: 0x04006B40 RID: 27456
		[SerializeField]
		private float _canDressBorder;

		// Token: 0x04006B41 RID: 27457
		[SerializeField]
		private int _washFaceBorder;

		// Token: 0x04006B42 RID: 27458
		[SerializeField]
		private int _nightLightBorder;

		// Token: 0x04006B43 RID: 27459
		[SerializeField]
		private int _surpriseBorder;

		// Token: 0x04006B44 RID: 27460
		[SerializeField]
		private int _girlsActionBorder;

		// Token: 0x04006B45 RID: 27461
		[SerializeField]
		private int _talkRelationUpperBorder;

		// Token: 0x04006B46 RID: 27462
		[SerializeField]
		private int _lesbianSociabilityBuffBorder;

		// Token: 0x04006B47 RID: 27463
		[SerializeField]
		private Threshold _flavorCookSuccessBoostMinMax = default(Threshold);

		// Token: 0x04006B48 RID: 27464
		[SerializeField]
		private Threshold _flavorCookSuccessBoost = default(Threshold);

		// Token: 0x04006B49 RID: 27465
		[SerializeField]
		private int _chefCookSuccessBoost;

		// Token: 0x04006B4A RID: 27466
		[SerializeField]
		private Threshold _flavorCatCaptureMinMax = new Threshold(0f, 1f);

		// Token: 0x04006B4B RID: 27467
		[SerializeField]
		private Threshold _flavorCatCaptureRate = new Threshold(0f, 1f);

		// Token: 0x04006B4C RID: 27468
		[SerializeField]
		private int _catCaptureProbBuff;

		// Token: 0x04006B4D RID: 27469
		[SerializeField]
		private float _defaultInstructionRate;

		// Token: 0x04006B4E RID: 27470
		[SerializeField]
		private Threshold _flavorReliabilityInstructionMinMax = new Threshold(0f, 1f);

		// Token: 0x04006B4F RID: 27471
		[SerializeField]
		private Threshold _flavorReliabilityInstruction = new Threshold(0f, 1f);

		// Token: 0x04006B50 RID: 27472
		[SerializeField]
		private float _instructionRateDebuff;

		// Token: 0x04006B51 RID: 27473
		[SerializeField]
		private float _defaultFollowRate;

		// Token: 0x04006B52 RID: 27474
		[SerializeField]
		private Threshold _followReliabilityMinMax = new Threshold(0f, 1f);

		// Token: 0x04006B53 RID: 27475
		[SerializeField]
		private Threshold _followRateReliabilityBuff = new Threshold(0f, 1f);

		// Token: 0x04006B54 RID: 27476
		[SerializeField]
		private float _followRateBuff;

		// Token: 0x04006B55 RID: 27477
		[SerializeField]
		private Threshold _dropBuffMinMax = new Threshold(0f, 1f);

		// Token: 0x04006B56 RID: 27478
		[SerializeField]
		private Threshold _dropBuff = new Threshold(0f, 1f);

		// Token: 0x04006B57 RID: 27479
		[SerializeField]
		private float _girlsActionProb;

		// Token: 0x04006B58 RID: 27480
		[SerializeField]
		private float _lesbianRate;

		// Token: 0x04006B59 RID: 27481
		[SerializeField]
		private float _shallowSleepProb;

		// Token: 0x04006B5A RID: 27482
		[SerializeField]
		private Threshold _yobaiMinMax = new Threshold(0f, 1f);

		// Token: 0x04006B5B RID: 27483
		[SerializeField]
		private float _callProbBaseRate;

		// Token: 0x04006B5C RID: 27484
		[SerializeField]
		private float _callProbPhaseRate;

		// Token: 0x04006B5D RID: 27485
		[SerializeField]
		private int[] _callReliabilityBorder;

		// Token: 0x04006B5E RID: 27486
		[SerializeField]
		private float[] _callReliabilityBuff;

		// Token: 0x04006B5F RID: 27487
		[SerializeField]
		private float _callLowerMoodProb;

		// Token: 0x04006B60 RID: 27488
		[SerializeField]
		private float _callUpperMoodProb;

		// Token: 0x04006B61 RID: 27489
		[SerializeField]
		private float _callSecondTimeProb;

		// Token: 0x04006B62 RID: 27490
		[SerializeField]
		private float _callOverTimeProb;

		// Token: 0x04006B63 RID: 27491
		[SerializeField]
		private float _callProbSuperSense;

		// Token: 0x04006B64 RID: 27492
		[SerializeField]
		private float _handSearchProbBuff;

		// Token: 0x04006B65 RID: 27493
		[SerializeField]
		private float _fishingSearchProbBuff;

		// Token: 0x04006B66 RID: 27494
		[SerializeField]
		private float _pickelSearchProbBuff;

		// Token: 0x04006B67 RID: 27495
		[SerializeField]
		private float _shovelSearchProbBuff;

		// Token: 0x04006B68 RID: 27496
		[SerializeField]
		private float _netSearchProbBuff;

		// Token: 0x04006B69 RID: 27497
		[SerializeField]
		private float _coldDefaultIncidence;

		// Token: 0x04006B6A RID: 27498
		[SerializeField]
		private float _coldLockDuration = 1f;

		// Token: 0x04006B6B RID: 27499
		[SerializeField]
		private int _coldBaseDuration = 1;

		// Token: 0x04006B6C RID: 27500
		[SerializeField]
		private float _heatStrokeDefaultIncidence;

		// Token: 0x04006B6D RID: 27501
		[SerializeField]
		private float _heatStrokeLockDuration = 1f;

		// Token: 0x04006B6E RID: 27502
		[SerializeField]
		private float _hurtDefaultIncidence;

		// Token: 0x04006B6F RID: 27503
		[SerializeField]
		private Threshold _stomachacheRateDebuffMinMax = new Threshold(0f, 1f);

		// Token: 0x04006B70 RID: 27504
		[SerializeField]
		private Threshold _stomachacheRateBuff = new Threshold(0f, 1f);

		// Token: 0x04006B71 RID: 27505
		[SerializeField]
		private Threshold _coldRateBuffMinMax = new Threshold(0f, 1f);

		// Token: 0x04006B72 RID: 27506
		[SerializeField]
		private Threshold _coldRateBuff = new Threshold(0f, 1f);

		// Token: 0x04006B73 RID: 27507
		[SerializeField]
		private Threshold _heatStrokeRateBuffMinMax = new Threshold(0f, 1f);

		// Token: 0x04006B74 RID: 27508
		[SerializeField]
		private Threshold _heatStrokeRateBuff = new Threshold(0f, 1f);

		// Token: 0x04006B75 RID: 27509
		[SerializeField]
		private Threshold _hurtRateBuffMinMax = new Threshold(0f, 1f);

		// Token: 0x04006B76 RID: 27510
		[SerializeField]
		private Threshold _hurtRateBuff = new Threshold(0f, 1f);

		// Token: 0x04006B77 RID: 27511
		[SerializeField]
		private Threshold _sickIncidenceDarknessBuffMinMax = new Threshold(0f, 1f);

		// Token: 0x04006B78 RID: 27512
		[SerializeField]
		private Threshold _sickIncidenceDarknessBuff = new Threshold(0f, 1f);

		// Token: 0x04006B79 RID: 27513
		[SerializeField]
		private float _coldRateDebuffWeak;

		// Token: 0x04006B7A RID: 27514
		[SerializeField]
		private float _heatStrokeBuffGuts;

		// Token: 0x04006B7B RID: 27515
		[SerializeField]
		private int _starveWarinessValue = 1;

		// Token: 0x04006B7C RID: 27516
		[SerializeField]
		private int _starveDarknessValue = 1;

		// Token: 0x04006B7D RID: 27517
		[SerializeField]
		private float _wetRateInRain;

		// Token: 0x04006B7E RID: 27518
		[SerializeField]
		private float _wetRateInStorm;

		// Token: 0x04006B7F RID: 27519
		[SerializeField]
		private float _drySpeed;

		// Token: 0x04006B80 RID: 27520
		[SerializeField]
		private float _wetTemperatureRate;

		// Token: 0x04006B81 RID: 27521
		[SerializeField]
		private float _coldTemperatureValue;

		// Token: 0x04006B82 RID: 27522
		[SerializeField]
		private float _hotTemperatureValue;

		// Token: 0x04006B83 RID: 27523
		[SerializeField]
		private float _lesbianBorderDesire;

		// Token: 0x04006B84 RID: 27524
		[SerializeField]
		private float _shallowSleepHungerLowBorder;

		// Token: 0x04006B85 RID: 27525
		[SerializeField]
		private int _lampEquipableBorder;

		// Token: 0x04006B86 RID: 27526
		[SerializeField]
		private EnvironmentSimulator.DateTimeSerialization _shouldRestoreCoordTime = new EnvironmentSimulator.DateTimeSerialization(DateTime.MinValue);

		// Token: 0x04006B87 RID: 27527
		[SerializeField]
		private float _restoreRangeMinuteTime;

		// Token: 0x04006B88 RID: 27528
		[SerializeField]
		private int _soineReliabilityBorder;

		// Token: 0x04006B89 RID: 27529
		[SerializeField]
		private float _potionImmoralAdd;

		// Token: 0x04006B8A RID: 27530
		[SerializeField]
		private float _diureticToiletAdd;

		// Token: 0x04006B8B RID: 27531
		[SerializeField]
		private float _pillSleepAdd;

		// Token: 0x04006B8C RID: 27532
		[SerializeField]
		private int _sleepFlagCount = 1;

		// Token: 0x04006B8D RID: 27533
		[SerializeField]
		private int _eatTogetherFlagCount = 1;

		// Token: 0x04006B8E RID: 27534
		[SerializeField]
		private int _coldFlagCount = 1;

		// Token: 0x04006B8F RID: 27535
		[SerializeField]
		private int _playFlagCount = 1;

		// Token: 0x04006B90 RID: 27536
		[SerializeField]
		private int _dressFlagCount = 1;

		// Token: 0x04006B91 RID: 27537
		[SerializeField]
		private int _gatherFlagCount = 1;

		// Token: 0x04006B92 RID: 27538
		[SerializeField]
		private int _sleepTogetherFlagCount = 1;

		// Token: 0x04006B93 RID: 27539
		[SerializeField]
		private int _checkPetCountCond;

		// Token: 0x04006B94 RID: 27540
		[SerializeField]
		private int _randomADVReliabilityCond;
	}
}
