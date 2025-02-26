using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEx;

namespace AIProject.Animal
{
	// Token: 0x02000B67 RID: 2919
	public static class AnimalData
	{
		// Token: 0x1700100E RID: 4110
		// (get) Token: 0x060056FA RID: 22266 RVA: 0x0025A44E File Offset: 0x0025884E
		public static ReadOnlyDictionary<AnimalTypes, string> AnimalNameTable
		{
			get
			{
				ReadOnlyDictionary<AnimalTypes, string> result;
				if ((result = AnimalData._animalNameTable) == null)
				{
					result = (AnimalData._animalNameTable = new ReadOnlyDictionary<AnimalTypes, string>(AnimalData.animalNameTable_));
				}
				return result;
			}
		}

		// Token: 0x1700100F RID: 4111
		// (get) Token: 0x060056FB RID: 22267 RVA: 0x0025A46C File Offset: 0x0025886C
		public static ReadOnlyDictionary<BreedingTypes, string> BreedingStr
		{
			get
			{
				ReadOnlyDictionary<BreedingTypes, string> result;
				if ((result = AnimalData._breedingStr) == null)
				{
					result = (AnimalData._breedingStr = new ReadOnlyDictionary<BreedingTypes, string>(AnimalData.breedingStr_));
				}
				return result;
			}
		}

		// Token: 0x17001010 RID: 4112
		// (get) Token: 0x060056FC RID: 22268 RVA: 0x0025A48A File Offset: 0x0025888A
		public static ReadOnlyDictionary<int, AnimalTypes> AnimalTypeIDTable
		{
			get
			{
				ReadOnlyDictionary<int, AnimalTypes> result;
				if ((result = AnimalData._animalTypeIDTable) == null)
				{
					result = (AnimalData._animalTypeIDTable = new ReadOnlyDictionary<int, AnimalTypes>(AnimalData.animalTypeIDTable_));
				}
				return result;
			}
		}

		// Token: 0x17001011 RID: 4113
		// (get) Token: 0x060056FD RID: 22269 RVA: 0x0025A4A8 File Offset: 0x002588A8
		public static ReadOnlyDictionary<int, AnimalState> AnimalStateIDTable
		{
			get
			{
				ReadOnlyDictionary<int, AnimalState> result;
				if ((result = AnimalData._animalStateIDTable) == null)
				{
					result = (AnimalData._animalStateIDTable = new ReadOnlyDictionary<int, AnimalState>(AnimalData.animalStateIDTable_));
				}
				return result;
			}
		}

		// Token: 0x17001012 RID: 4114
		// (get) Token: 0x060056FE RID: 22270 RVA: 0x0025A4C6 File Offset: 0x002588C6
		public static IReadOnlyList<UnityEx.ValueTuple<string, AnimalTypes>> AnimalNameList
		{
			get
			{
				IReadOnlyList<UnityEx.ValueTuple<string, AnimalTypes>> result;
				if ((result = AnimalData._animalNameList) == null)
				{
					result = (AnimalData._animalNameList = AnimalData.animalNameList_);
				}
				return result;
			}
		}

		// Token: 0x17001013 RID: 4115
		// (get) Token: 0x060056FF RID: 22271 RVA: 0x0025A4DF File Offset: 0x002588DF
		public static IReadOnlyList<UnityEx.ValueTuple<string, BreedingTypes>> BreedingNameList
		{
			get
			{
				IReadOnlyList<UnityEx.ValueTuple<string, BreedingTypes>> result;
				if ((result = AnimalData._breedingNameList) == null)
				{
					result = (AnimalData._breedingNameList = AnimalData.breedingNameList_);
				}
				return result;
			}
		}

		// Token: 0x06005700 RID: 22272 RVA: 0x0025A4F8 File Offset: 0x002588F8
		public static int GetAnimalTypeID(AnimalTypes animalType)
		{
			int num = 32;
			for (int i = 0; i < num; i++)
			{
				if (animalType >> (i & 31) == AnimalTypes.Cat && animalType == (AnimalTypes)(1 << i))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0400504B RID: 20555
		private static Dictionary<AnimalTypes, string> animalNameTable_ = new Dictionary<AnimalTypes, string>
		{
			{
				AnimalTypes.Cat,
				"cat"
			},
			{
				AnimalTypes.Chicken,
				"chicken"
			},
			{
				AnimalTypes.Fish,
				"fish"
			},
			{
				AnimalTypes.Mecha,
				"mecha"
			},
			{
				AnimalTypes.Frog,
				"frog"
			},
			{
				AnimalTypes.Butterfly,
				"butterfly"
			},
			{
				AnimalTypes.BirdFlock,
				"birdflock"
			},
			{
				AnimalTypes.CatWithFish,
				"catwithfish"
			},
			{
				AnimalTypes.CatTank,
				"cattank"
			},
			{
				AnimalTypes.Chick,
				"chick"
			},
			{
				AnimalTypes.Fairy,
				"fairy"
			},
			{
				AnimalTypes.DarkSpirit,
				"darkspirit"
			}
		};

		// Token: 0x0400504C RID: 20556
		private static ReadOnlyDictionary<AnimalTypes, string> _animalNameTable = null;

		// Token: 0x0400504D RID: 20557
		private static Dictionary<BreedingTypes, string> breedingStr_ = new Dictionary<BreedingTypes, string>
		{
			{
				BreedingTypes.Wild,
				"wild"
			},
			{
				BreedingTypes.Pet,
				"pet"
			}
		};

		// Token: 0x0400504E RID: 20558
		private static ReadOnlyDictionary<BreedingTypes, string> _breedingStr = null;

		// Token: 0x0400504F RID: 20559
		private static Dictionary<int, AnimalTypes> animalTypeIDTable_ = new Dictionary<int, AnimalTypes>
		{
			{
				0,
				AnimalTypes.Cat
			},
			{
				1,
				AnimalTypes.Chicken
			},
			{
				2,
				AnimalTypes.Fish
			},
			{
				3,
				AnimalTypes.Butterfly
			},
			{
				4,
				AnimalTypes.Mecha
			},
			{
				5,
				AnimalTypes.Frog
			},
			{
				6,
				AnimalTypes.BirdFlock
			},
			{
				7,
				AnimalTypes.CatWithFish
			},
			{
				8,
				AnimalTypes.CatTank
			},
			{
				9,
				AnimalTypes.Chick
			},
			{
				10,
				AnimalTypes.Fairy
			},
			{
				11,
				AnimalTypes.DarkSpirit
			}
		};

		// Token: 0x04005050 RID: 20560
		private static ReadOnlyDictionary<int, AnimalTypes> _animalTypeIDTable = null;

		// Token: 0x04005051 RID: 20561
		private static Dictionary<int, AnimalState> animalStateIDTable_ = new Dictionary<int, AnimalState>
		{
			{
				0,
				AnimalState.None
			},
			{
				1,
				AnimalState.Start
			},
			{
				2,
				AnimalState.Repop
			},
			{
				3,
				AnimalState.Depop
			},
			{
				4,
				AnimalState.Idle
			},
			{
				5,
				AnimalState.Wait
			},
			{
				6,
				AnimalState.SitWait
			},
			{
				7,
				AnimalState.Locomotion
			},
			{
				8,
				AnimalState.LovelyIdle
			},
			{
				9,
				AnimalState.LovelyFollow
			},
			{
				10,
				AnimalState.Escape
			},
			{
				11,
				AnimalState.Swim
			},
			{
				12,
				AnimalState.Sleep
			},
			{
				13,
				AnimalState.Toilet
			},
			{
				14,
				AnimalState.Rest
			},
			{
				15,
				AnimalState.Eat
			},
			{
				16,
				AnimalState.Drink
			},
			{
				17,
				AnimalState.Actinidia
			},
			{
				18,
				AnimalState.Grooming
			},
			{
				19,
				AnimalState.MoveEars
			},
			{
				20,
				AnimalState.Roar
			},
			{
				21,
				AnimalState.Peck
			},
			{
				22,
				AnimalState.ToIndoor
			},
			{
				90,
				AnimalState.Action0
			},
			{
				91,
				AnimalState.Action1
			},
			{
				92,
				AnimalState.Action2
			},
			{
				93,
				AnimalState.Action3
			},
			{
				94,
				AnimalState.Action4
			},
			{
				95,
				AnimalState.Action5
			},
			{
				96,
				AnimalState.Action6
			},
			{
				97,
				AnimalState.Action7
			},
			{
				98,
				AnimalState.Action8
			},
			{
				99,
				AnimalState.Action9
			},
			{
				100,
				AnimalState.WithPlayer
			},
			{
				101,
				AnimalState.WithAgent
			},
			{
				999,
				AnimalState.Destroyed
			}
		};

		// Token: 0x04005052 RID: 20562
		private static ReadOnlyDictionary<int, AnimalState> _animalStateIDTable = null;

		// Token: 0x04005053 RID: 20563
		private static List<UnityEx.ValueTuple<string, AnimalTypes>> animalNameList_ = new List<UnityEx.ValueTuple<string, AnimalTypes>>
		{
			new UnityEx.ValueTuple<string, AnimalTypes>("cat", AnimalTypes.Cat),
			new UnityEx.ValueTuple<string, AnimalTypes>("chicken", AnimalTypes.Chicken),
			new UnityEx.ValueTuple<string, AnimalTypes>("fish", AnimalTypes.Fish),
			new UnityEx.ValueTuple<string, AnimalTypes>("mecha", AnimalTypes.Mecha),
			new UnityEx.ValueTuple<string, AnimalTypes>("frog", AnimalTypes.Frog),
			new UnityEx.ValueTuple<string, AnimalTypes>("butterfly", AnimalTypes.Butterfly),
			new UnityEx.ValueTuple<string, AnimalTypes>("birdflock", AnimalTypes.BirdFlock),
			new UnityEx.ValueTuple<string, AnimalTypes>("catwithfish", AnimalTypes.CatWithFish),
			new UnityEx.ValueTuple<string, AnimalTypes>("cattank", AnimalTypes.CatTank),
			new UnityEx.ValueTuple<string, AnimalTypes>("chick", AnimalTypes.Chick),
			new UnityEx.ValueTuple<string, AnimalTypes>("fairy", AnimalTypes.Fairy),
			new UnityEx.ValueTuple<string, AnimalTypes>("darkspirit", AnimalTypes.DarkSpirit)
		};

		// Token: 0x04005054 RID: 20564
		private static IReadOnlyList<UnityEx.ValueTuple<string, AnimalTypes>> _animalNameList = null;

		// Token: 0x04005055 RID: 20565
		private static List<UnityEx.ValueTuple<string, BreedingTypes>> breedingNameList_ = new List<UnityEx.ValueTuple<string, BreedingTypes>>
		{
			new UnityEx.ValueTuple<string, BreedingTypes>("wild", BreedingTypes.Wild),
			new UnityEx.ValueTuple<string, BreedingTypes>("pet", BreedingTypes.Pet)
		};

		// Token: 0x04005056 RID: 20566
		private static IReadOnlyList<UnityEx.ValueTuple<string, BreedingTypes>> _breedingNameList = null;
	}
}
