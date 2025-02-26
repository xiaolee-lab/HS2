using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using ADV;
using AIProject.Animal;
using AIProject.Definitions;
using Manager;
using MessagePack;
using UnityEngine;

namespace AIProject.SaveData
{
	// Token: 0x02000977 RID: 2423
	[MessagePackObject(false)]
	public class Environment : IDiffComparer, ICommandData
	{
		// Token: 0x06004441 RID: 17473 RVA: 0x001A8EC4 File Offset: 0x001A72C4
		public Environment()
		{
			this.Init();
		}

		// Token: 0x17000D16 RID: 3350
		// (get) Token: 0x06004442 RID: 17474 RVA: 0x001A8FFB File Offset: 0x001A73FB
		// (set) Token: 0x06004443 RID: 17475 RVA: 0x001A9003 File Offset: 0x001A7403
		[Key(0)]
		public System.Version Version { get; set; } = new System.Version();

		// Token: 0x17000D17 RID: 3351
		// (get) Token: 0x06004444 RID: 17476 RVA: 0x001A900C File Offset: 0x001A740C
		// (set) Token: 0x06004445 RID: 17477 RVA: 0x001A9014 File Offset: 0x001A7414
		[Key(1)]
		public int TutorialProgress { get; set; }

		// Token: 0x17000D18 RID: 3352
		// (get) Token: 0x06004446 RID: 17478 RVA: 0x001A901D File Offset: 0x001A741D
		// (set) Token: 0x06004447 RID: 17479 RVA: 0x001A9025 File Offset: 0x001A7425
		[Key(2)]
		public AIProject.SaveData.Environment.SerializableTimeSpan TotalPlayTime { get; set; } = default(AIProject.SaveData.Environment.SerializableTimeSpan);

		// Token: 0x17000D19 RID: 3353
		// (get) Token: 0x06004448 RID: 17480 RVA: 0x001A902E File Offset: 0x001A742E
		// (set) Token: 0x06004449 RID: 17481 RVA: 0x001A9036 File Offset: 0x001A7436
		[Key(3)]
		public AIProject.SaveData.Environment.SerializableDateTime Time { get; set; } = default(AIProject.SaveData.Environment.SerializableDateTime);

		// Token: 0x17000D1A RID: 3354
		// (get) Token: 0x0600444A RID: 17482 RVA: 0x001A903F File Offset: 0x001A743F
		// (set) Token: 0x0600444B RID: 17483 RVA: 0x001A9047 File Offset: 0x001A7447
		[Key(5)]
		public Weather Weather { get; set; } = Weather.Cloud1;

		// Token: 0x17000D1B RID: 3355
		// (get) Token: 0x0600444C RID: 17484 RVA: 0x001A9050 File Offset: 0x001A7450
		// (set) Token: 0x0600444D RID: 17485 RVA: 0x001A9058 File Offset: 0x001A7458
		[Key(7)]
		public Temperature Temperature { get; set; } = Temperature.Normal;

		// Token: 0x17000D1C RID: 3356
		// (get) Token: 0x0600444E RID: 17486 RVA: 0x001A9061 File Offset: 0x001A7461
		// (set) Token: 0x0600444F RID: 17487 RVA: 0x001A9069 File Offset: 0x001A7469
		[Key(8)]
		public List<StuffItem> ItemListInStorage { get; set; } = new List<StuffItem>();

		// Token: 0x17000D1D RID: 3357
		// (get) Token: 0x06004450 RID: 17488 RVA: 0x001A9072 File Offset: 0x001A7472
		// (set) Token: 0x06004451 RID: 17489 RVA: 0x001A907A File Offset: 0x001A747A
		[Key(9)]
		public List<StuffItem> ItemListInPantry { get; set; } = new List<StuffItem>();

		// Token: 0x17000D1E RID: 3358
		// (get) Token: 0x06004452 RID: 17490 RVA: 0x001A9083 File Offset: 0x001A7483
		// (set) Token: 0x06004453 RID: 17491 RVA: 0x001A908B File Offset: 0x001A748B
		[Key(10)]
		public Dictionary<int, bool> AreaOpenState { get; set; } = new Dictionary<int, bool>();

		// Token: 0x17000D1F RID: 3359
		// (get) Token: 0x06004454 RID: 17492 RVA: 0x001A9094 File Offset: 0x001A7494
		// (set) Token: 0x06004455 RID: 17493 RVA: 0x001A909C File Offset: 0x001A749C
		[Key(11)]
		public float TemperatureValue { get; set; }

		// Token: 0x17000D20 RID: 3360
		// (get) Token: 0x06004456 RID: 17494 RVA: 0x001A90A5 File Offset: 0x001A74A5
		// (set) Token: 0x06004457 RID: 17495 RVA: 0x001A90AD File Offset: 0x001A74AD
		[Key(12)]
		public Dictionary<int, bool> TimeObjOpenState { get; set; } = new Dictionary<int, bool>();

		// Token: 0x17000D21 RID: 3361
		// (get) Token: 0x06004458 RID: 17496 RVA: 0x001A90B6 File Offset: 0x001A74B6
		// (set) Token: 0x06004459 RID: 17497 RVA: 0x001A90BE File Offset: 0x001A74BE
		[Key(13)]
		public Dictionary<int, Dictionary<int, bool>> BasePointOpenState { get; set; } = new Dictionary<int, Dictionary<int, bool>>();

		// Token: 0x17000D22 RID: 3362
		// (get) Token: 0x0600445A RID: 17498 RVA: 0x001A90C7 File Offset: 0x001A74C7
		// (set) Token: 0x0600445B RID: 17499 RVA: 0x001A90CF File Offset: 0x001A74CF
		[Key(14)]
		public Dictionary<int, bool> LightObjectSwitchStateTable { get; set; } = new Dictionary<int, bool>();

		// Token: 0x17000D23 RID: 3363
		// (get) Token: 0x0600445C RID: 17500 RVA: 0x001A90D8 File Offset: 0x001A74D8
		// (set) Token: 0x0600445D RID: 17501 RVA: 0x001A90E0 File Offset: 0x001A74E0
		[Key(15)]
		public Dictionary<int, Dictionary<int, int>> EventPointStateTable { get; set; } = new Dictionary<int, Dictionary<int, int>>();

		// Token: 0x17000D24 RID: 3364
		// (get) Token: 0x0600445E RID: 17502 RVA: 0x001A90E9 File Offset: 0x001A74E9
		// (set) Token: 0x0600445F RID: 17503 RVA: 0x001A90F1 File Offset: 0x001A74F1
		[Key(16)]
		public Dictionary<int, List<AIProject.SaveData.Environment.PlantInfo>> FarmlandTable { get; set; } = new Dictionary<int, List<AIProject.SaveData.Environment.PlantInfo>>();

		// Token: 0x17000D25 RID: 3365
		// (get) Token: 0x06004460 RID: 17504 RVA: 0x001A90FA File Offset: 0x001A74FA
		// (set) Token: 0x06004461 RID: 17505 RVA: 0x001A9102 File Offset: 0x001A7502
		[Key(17)]
		public Dictionary<int, AIProject.SaveData.Environment.SearchActionInfo> SearchActionLockTable { get; set; } = new Dictionary<int, AIProject.SaveData.Environment.SearchActionInfo>();

		// Token: 0x17000D26 RID: 3366
		// (get) Token: 0x06004462 RID: 17506 RVA: 0x001A910B File Offset: 0x001A750B
		// (set) Token: 0x06004463 RID: 17507 RVA: 0x001A9113 File Offset: 0x001A7513
		[Key(18)]
		public Dictionary<int, List<AIProject.SaveData.Environment.ChickenInfo>> ChickenTable { get; set; } = new Dictionary<int, List<AIProject.SaveData.Environment.ChickenInfo>>();

		// Token: 0x17000D27 RID: 3367
		// (get) Token: 0x06004464 RID: 17508 RVA: 0x001A911C File Offset: 0x001A751C
		// (set) Token: 0x06004465 RID: 17509 RVA: 0x001A9124 File Offset: 0x001A7524
		[Key(19)]
		public List<StuffItem> ItemListInEggBox { get; set; } = new List<StuffItem>();

		// Token: 0x17000D28 RID: 3368
		// (get) Token: 0x06004466 RID: 17510 RVA: 0x001A912D File Offset: 0x001A752D
		// (set) Token: 0x06004467 RID: 17511 RVA: 0x001A9135 File Offset: 0x001A7535
		[Key(20)]
		public int TotalAgentFlavorAdditionAmount { get; set; }

		// Token: 0x17000D29 RID: 3369
		// (get) Token: 0x06004468 RID: 17512 RVA: 0x001A913E File Offset: 0x001A753E
		// (set) Token: 0x06004469 RID: 17513 RVA: 0x001A9146 File Offset: 0x001A7546
		[Key(21)]
		public List<int> RegIDList { get; set; } = new List<int>();

		// Token: 0x17000D2A RID: 3370
		// (get) Token: 0x0600446A RID: 17514 RVA: 0x001A914F File Offset: 0x001A754F
		// (set) Token: 0x0600446B RID: 17515 RVA: 0x001A9157 File Offset: 0x001A7557
		[Key(22)]
		public Dictionary<int, AIProject.SaveData.Environment.PetHomeInfo> PetHomeStateTable { get; set; } = new Dictionary<int, AIProject.SaveData.Environment.PetHomeInfo>();

		// Token: 0x17000D2B RID: 3371
		// (get) Token: 0x0600446C RID: 17516 RVA: 0x001A9160 File Offset: 0x001A7560
		// (set) Token: 0x0600446D RID: 17517 RVA: 0x001A9168 File Offset: 0x001A7568
		[Key(23)]
		public List<string> ClosetCoordinateList { get; set; } = new List<string>();

		// Token: 0x17000D2C RID: 3372
		// (get) Token: 0x0600446E RID: 17518 RVA: 0x001A9171 File Offset: 0x001A7571
		// (set) Token: 0x0600446F RID: 17519 RVA: 0x001A9179 File Offset: 0x001A7579
		[Key(24)]
		public List<string> DressCoordinateList { get; set; } = new List<string>();

		// Token: 0x17000D2D RID: 3373
		// (get) Token: 0x06004470 RID: 17520 RVA: 0x001A9182 File Offset: 0x001A7582
		// (set) Token: 0x06004471 RID: 17521 RVA: 0x001A918A File Offset: 0x001A758A
		[Key(25)]
		public Dictionary<int, string> JukeBoxAudioNameTable { get; set; } = new Dictionary<int, string>();

		// Token: 0x17000D2E RID: 3374
		// (get) Token: 0x06004472 RID: 17522 RVA: 0x001A9193 File Offset: 0x001A7593
		// (set) Token: 0x06004473 RID: 17523 RVA: 0x001A919B File Offset: 0x001A759B
		[Key(28)]
		public Dictionary<int, bool> OnceActionPointStateTable { get; set; } = new Dictionary<int, bool>();

		// Token: 0x17000D2F RID: 3375
		// (get) Token: 0x06004474 RID: 17524 RVA: 0x001A91A4 File Offset: 0x001A75A4
		// (set) Token: 0x06004475 RID: 17525 RVA: 0x001A91AC File Offset: 0x001A75AC
		[Key(29)]
		public Dictionary<int, float> DropSearchActionPointCoolTimeTable { get; set; } = new Dictionary<int, float>();

		// Token: 0x17000D30 RID: 3376
		// (get) Token: 0x06004476 RID: 17526 RVA: 0x001A91B5 File Offset: 0x001A75B5
		// (set) Token: 0x06004477 RID: 17527 RVA: 0x001A91BD File Offset: 0x001A75BD
		[Key(31)]
		public Dictionary<int, Dictionary<int, string>> AnotherJukeBoxAudioNameTable { get; set; } = new Dictionary<int, Dictionary<int, string>>();

		// Token: 0x17000D31 RID: 3377
		// (get) Token: 0x06004478 RID: 17528 RVA: 0x001A91C6 File Offset: 0x001A75C6
		// (set) Token: 0x06004479 RID: 17529 RVA: 0x001A91CE File Offset: 0x001A75CE
		[Key(32)]
		public Dictionary<int, Dictionary<int, Dictionary<int, AnimalData>>> HousingChickenDataTable { get; set; } = new Dictionary<int, Dictionary<int, Dictionary<int, AnimalData>>>();

		// Token: 0x17000D32 RID: 3378
		// (get) Token: 0x0600447A RID: 17530 RVA: 0x001A91D7 File Offset: 0x001A75D7
		// (set) Token: 0x0600447B RID: 17531 RVA: 0x001A91DF File Offset: 0x001A75DF
		[Key(33)]
		public Dictionary<int, RecyclingData> RecyclingDataTable { get; set; } = new Dictionary<int, RecyclingData>();

		// Token: 0x0600447C RID: 17532 RVA: 0x001A91E8 File Offset: 0x001A75E8
		public void Init()
		{
			Dictionary<int, Dictionary<int, bool>> dictionary = new Dictionary<int, Dictionary<int, bool>>();
			Dictionary<int, Dictionary<int, bool>> dictionary2 = dictionary;
			int key = 0;
			Dictionary<int, bool> dictionary3 = new Dictionary<int, bool>();
			dictionary3[-1] = false;
			dictionary3[0] = false;
			dictionary3[1] = false;
			dictionary3[2] = false;
			dictionary2[key] = dictionary3;
			this.BasePointOpenState = dictionary;
		}

		// Token: 0x0600447D RID: 17533 RVA: 0x001A9230 File Offset: 0x001A7630
		public void Copy(AIProject.SaveData.Environment source)
		{
			this.Version = source.Version;
			this.TutorialProgress = source.TutorialProgress;
			this.TotalPlayTime = source.TotalPlayTime;
			this.Time = source.Time;
			this.Weather = source.Weather;
			this.Temperature = source.Temperature;
			this.ItemListInStorage.Clear();
			foreach (StuffItem stuffItem in source.ItemListInStorage)
			{
				this.ItemListInStorage.Add(new StuffItem(stuffItem.CategoryID, stuffItem.ID, stuffItem.Count));
			}
			this.ItemListInPantry.Clear();
			foreach (StuffItem stuffItem2 in source.ItemListInPantry)
			{
				this.ItemListInPantry.Add(new StuffItem(stuffItem2.CategoryID, stuffItem2.ID, stuffItem2.Count));
			}
			this.AreaOpenState = source.AreaOpenState.ToDictionary((KeyValuePair<int, bool> x) => x.Key, (KeyValuePair<int, bool> x) => x.Value);
			this.TemperatureValue = source.TemperatureValue;
			this.TimeObjOpenState = source.TimeObjOpenState.ToDictionary((KeyValuePair<int, bool> x) => x.Key, (KeyValuePair<int, bool> x) => x.Value);
			this.BasePointOpenState = source.BasePointOpenState.ToDictionary((KeyValuePair<int, Dictionary<int, bool>> x) => x.Key, delegate(KeyValuePair<int, Dictionary<int, bool>> x)
			{
				Dictionary<int, bool> value = x.Value;
				Dictionary<int, bool> result;
				if (value == null)
				{
					result = null;
				}
				else
				{
					result = value.ToDictionary((KeyValuePair<int, bool> y) => y.Key, (KeyValuePair<int, bool> y) => y.Value);
				}
				return result;
			});
			this.LightObjectSwitchStateTable = source.LightObjectSwitchStateTable.ToDictionary((KeyValuePair<int, bool> x) => x.Key, (KeyValuePair<int, bool> x) => x.Value);
			this.EventPointStateTable = source.EventPointStateTable.ToDictionary((KeyValuePair<int, Dictionary<int, int>> x) => x.Key, delegate(KeyValuePair<int, Dictionary<int, int>> x)
			{
				Dictionary<int, int> value = x.Value;
				Dictionary<int, int> result;
				if (value == null)
				{
					result = null;
				}
				else
				{
					result = value.ToDictionary((KeyValuePair<int, int> y) => y.Key, (KeyValuePair<int, int> y) => y.Value);
				}
				return result;
			});
			this.FarmlandTable = source.FarmlandTable.ToDictionary((KeyValuePair<int, List<AIProject.SaveData.Environment.PlantInfo>> x) => x.Key, delegate(KeyValuePair<int, List<AIProject.SaveData.Environment.PlantInfo>> x)
			{
				List<AIProject.SaveData.Environment.PlantInfo> value = x.Value;
				List<AIProject.SaveData.Environment.PlantInfo> result;
				if (value == null)
				{
					result = null;
				}
				else
				{
					IEnumerable<AIProject.SaveData.Environment.PlantInfo> enumerable = from y in value
					select (y == null) ? null : new AIProject.SaveData.Environment.PlantInfo(y.nameHash, y.timeLimit, y.timer, y.result);
					result = ((enumerable != null) ? enumerable.ToList<AIProject.SaveData.Environment.PlantInfo>() : null);
				}
				return result;
			});
			this.SearchActionLockTable = source.SearchActionLockTable.ToDictionary((KeyValuePair<int, AIProject.SaveData.Environment.SearchActionInfo> x) => x.Key, (KeyValuePair<int, AIProject.SaveData.Environment.SearchActionInfo> x) => x.Value);
			this.ChickenTable = source.ChickenTable.ToDictionary((KeyValuePair<int, List<AIProject.SaveData.Environment.ChickenInfo>> x) => x.Key, delegate(KeyValuePair<int, List<AIProject.SaveData.Environment.ChickenInfo>> x)
			{
				List<AIProject.SaveData.Environment.ChickenInfo> value = x.Value;
				List<AIProject.SaveData.Environment.ChickenInfo> result;
				if (value == null)
				{
					result = null;
				}
				else
				{
					IEnumerable<AIProject.SaveData.Environment.ChickenInfo> enumerable = from y in value
					select (y == null) ? null : new AIProject.SaveData.Environment.ChickenInfo
					{
						name = y.name,
						AnimalData = ((y.AnimalData == null) ? null : new AnimalData(y.AnimalData))
					};
					result = ((enumerable != null) ? enumerable.ToList<AIProject.SaveData.Environment.ChickenInfo>() : null);
				}
				return result;
			});
			this.ItemListInEggBox.Clear();
			foreach (StuffItem stuffItem3 in source.ItemListInEggBox)
			{
				this.ItemListInEggBox.Add(new StuffItem(stuffItem3.CategoryID, stuffItem3.ID, stuffItem3.Count));
			}
			this.TotalAgentFlavorAdditionAmount = source.TotalAgentFlavorAdditionAmount;
			this.RegIDList = source.RegIDList.ToList<int>();
			this.PetHomeStateTable = source.PetHomeStateTable.ToDictionary((KeyValuePair<int, AIProject.SaveData.Environment.PetHomeInfo> x) => x.Key, (KeyValuePair<int, AIProject.SaveData.Environment.PetHomeInfo> x) => new AIProject.SaveData.Environment.PetHomeInfo(x.Value));
			this.ClosetCoordinateList = source.ClosetCoordinateList.ToList<string>();
			this.DressCoordinateList = source.DressCoordinateList.ToList<string>();
			this.JukeBoxAudioNameTable = (from pair in source.JukeBoxAudioNameTable
			where !pair.Value.IsNullOrEmpty()
			select pair).ToDictionary((KeyValuePair<int, string> x) => x.Key, (KeyValuePair<int, string> x) => x.Value);
			if (!source.OnceActionPointStateTable.IsNullOrEmpty<int, bool>())
			{
				this.OnceActionPointStateTable = source.OnceActionPointStateTable.ToDictionary((KeyValuePair<int, bool> x) => x.Key, (KeyValuePair<int, bool> x) => x.Value);
			}
			else
			{
				this.OnceActionPointStateTable.Clear();
			}
			if (!source.DropSearchActionPointCoolTimeTable.IsNullOrEmpty<int, float>())
			{
				this.DropSearchActionPointCoolTimeTable = source.DropSearchActionPointCoolTimeTable.ToDictionary((KeyValuePair<int, float> x) => x.Key, (KeyValuePair<int, float> x) => x.Value);
			}
			else
			{
				this.DropSearchActionPointCoolTimeTable.Clear();
			}
			if (!source.AnotherJukeBoxAudioNameTable.IsNullOrEmpty<int, Dictionary<int, string>>())
			{
				this.AnotherJukeBoxAudioNameTable = (from x in source.AnotherJukeBoxAudioNameTable
				where !x.Value.IsNullOrEmpty<int, string>()
				select x).ToDictionary((KeyValuePair<int, Dictionary<int, string>> x) => x.Key, (KeyValuePair<int, Dictionary<int, string>> x) => (from y in x.Value
				where !y.Value.IsNullOrEmpty()
				select y).ToDictionary((KeyValuePair<int, string> y) => y.Key, (KeyValuePair<int, string> y) => y.Value));
			}
			else
			{
				this.AnotherJukeBoxAudioNameTable.Clear();
			}
			if (!source.HousingChickenDataTable.IsNullOrEmpty<int, Dictionary<int, Dictionary<int, AnimalData>>>())
			{
				this.HousingChickenDataTable = (from a in source.HousingChickenDataTable
				where !a.Value.IsNullOrEmpty<int, Dictionary<int, AnimalData>>()
				select a).ToDictionary((KeyValuePair<int, Dictionary<int, Dictionary<int, AnimalData>>> a) => a.Key, (KeyValuePair<int, Dictionary<int, Dictionary<int, AnimalData>>> a) => (from b in a.Value
				where !b.Value.IsNullOrEmpty<int, AnimalData>()
				select b).ToDictionary((KeyValuePair<int, Dictionary<int, AnimalData>> b) => b.Key, (KeyValuePair<int, Dictionary<int, AnimalData>> b) => (from c in b.Value
				where c.Value != null
				select c).ToDictionary((KeyValuePair<int, AnimalData> c) => c.Key, (KeyValuePair<int, AnimalData> c) => new AnimalData(c.Value))));
			}
			else
			{
				this.HousingChickenDataTable.Clear();
			}
			if (!source.RecyclingDataTable.IsNullOrEmpty<int, RecyclingData>())
			{
				this.RecyclingDataTable = (from x in source.RecyclingDataTable
				where x.Value != null
				select x).ToDictionary((KeyValuePair<int, RecyclingData> x) => x.Key, (KeyValuePair<int, RecyclingData> x) => new RecyclingData(x.Value));
			}
			else
			{
				this.RecyclingDataTable.Clear();
			}
		}

		// Token: 0x0600447E RID: 17534 RVA: 0x001A99BC File Offset: 0x001A7DBC
		public void ComplementWithVersion()
		{
			this.Version = AIProject.Definitions.Version.EnvironmentDataVersion;
		}

		// Token: 0x0600447F RID: 17535 RVA: 0x001A99C9 File Offset: 0x001A7DC9
		public void SetSimulation(EnvironmentSimulator sim)
		{
			this.Time = sim.Now;
			this.Weather = sim.Weather;
			this.Temperature = sim.Temperature;
			this.TemperatureValue = sim.TemperatureValue;
		}

		// Token: 0x06004480 RID: 17536 RVA: 0x001A9A00 File Offset: 0x001A7E00
		public void UpdateDiff()
		{
			this.ItemListInStorage.OrganizeItemList();
		}

		// Token: 0x06004481 RID: 17537 RVA: 0x001A9A10 File Offset: 0x001A7E10
		public IEnumerable<CommandData> CreateCommandData(string head)
		{
			List<CommandData> list = new List<CommandData>();
			string str = head + "AreaOpenState";
			foreach (KeyValuePair<int, bool> keyValuePair in this.AreaOpenState)
			{
				int key = keyValuePair.Key;
				bool value = keyValuePair.Value;
				list.Add(new CommandData(CommandData.Command.BOOL, str + string.Format("[{0}]", key), () => value, delegate(object o)
				{
					bool flag = (bool)o;
					if (Singleton<Manager.Map>.IsInstance())
					{
						Singleton<Manager.Map>.Instance.SetOpenAreaState(key, flag);
					}
					else
					{
						this.AreaOpenState[key] = flag;
					}
				}));
			}
			return list;
		}

		// Token: 0x06004482 RID: 17538 RVA: 0x001A9AE8 File Offset: 0x001A7EE8
		public bool CheckPetEvent(int checkValue)
		{
			int num = 0;
			foreach (KeyValuePair<int, AIProject.SaveData.Environment.PetHomeInfo> keyValuePair in this.PetHomeStateTable)
			{
				if (keyValuePair.Value.AnimalData != null)
				{
					AnimalTypes animalType = keyValuePair.Value.AnimalData.AnimalType;
					if (animalType != AnimalTypes.Fish)
					{
						num++;
					}
				}
			}
			return num >= checkValue;
		}

		// Token: 0x02000978 RID: 2424
		[MessagePackObject(false)]
		public struct SerializableDateTime : ICommandData
		{
			// Token: 0x060044B4 RID: 17588 RVA: 0x001AA067 File Offset: 0x001A8467
			public SerializableDateTime(int year, int month, int day)
			{
				this = new AIProject.SaveData.Environment.SerializableDateTime(year, month, day, 0, 0, 0, 0);
			}

			// Token: 0x060044B5 RID: 17589 RVA: 0x001AA076 File Offset: 0x001A8476
			public SerializableDateTime(int year, int month, int day, int hour, int minute, int second)
			{
				this = new AIProject.SaveData.Environment.SerializableDateTime(year, month, day, hour, minute, second, 0);
			}

			// Token: 0x060044B6 RID: 17590 RVA: 0x001AA088 File Offset: 0x001A8488
			public SerializableDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond)
			{
				this._year = Mathf.Max(0, year);
				this._month = Mathf.Max(0, month);
				this._day = Mathf.Max(0, day);
				this.Hour = hour;
				this.Minute = minute;
				this.Second = second;
				this.Millisecond = millisecond;
			}

			// Token: 0x17000D33 RID: 3379
			// (get) Token: 0x060044B7 RID: 17591 RVA: 0x001AA0DC File Offset: 0x001A84DC
			// (set) Token: 0x060044B8 RID: 17592 RVA: 0x001AA0EA File Offset: 0x001A84EA
			[Key(0)]
			public int Year
			{
				get
				{
					return this.SetYear(this._year);
				}
				set
				{
					this.SetYear(value);
				}
			}

			// Token: 0x060044B9 RID: 17593 RVA: 0x001AA0F4 File Offset: 0x001A84F4
			private int SetYear(int year)
			{
				return this._year = Mathf.Max(1, year);
			}

			// Token: 0x17000D34 RID: 3380
			// (get) Token: 0x060044BA RID: 17594 RVA: 0x001AA111 File Offset: 0x001A8511
			// (set) Token: 0x060044BB RID: 17595 RVA: 0x001AA11F File Offset: 0x001A851F
			[Key(1)]
			public int Month
			{
				get
				{
					return this.SetMonth(this._month);
				}
				set
				{
					this.SetMonth(value);
				}
			}

			// Token: 0x060044BC RID: 17596 RVA: 0x001AA12C File Offset: 0x001A852C
			private int SetMonth(int month)
			{
				return this._month = Mathf.Max(1, month);
			}

			// Token: 0x17000D35 RID: 3381
			// (get) Token: 0x060044BD RID: 17597 RVA: 0x001AA149 File Offset: 0x001A8549
			// (set) Token: 0x060044BE RID: 17598 RVA: 0x001AA157 File Offset: 0x001A8557
			[Key(2)]
			public int Day
			{
				get
				{
					return this.SetDay(this._day);
				}
				set
				{
					this.SetDay(value);
				}
			}

			// Token: 0x060044BF RID: 17599 RVA: 0x001AA164 File Offset: 0x001A8564
			private int SetDay(int day)
			{
				return this._day = Mathf.Max(1, day);
			}

			// Token: 0x17000D36 RID: 3382
			// (get) Token: 0x060044C0 RID: 17600 RVA: 0x001AA181 File Offset: 0x001A8581
			// (set) Token: 0x060044C1 RID: 17601 RVA: 0x001AA189 File Offset: 0x001A8589
			[Key(3)]
			public int Hour { get; set; }

			// Token: 0x17000D37 RID: 3383
			// (get) Token: 0x060044C2 RID: 17602 RVA: 0x001AA192 File Offset: 0x001A8592
			// (set) Token: 0x060044C3 RID: 17603 RVA: 0x001AA19A File Offset: 0x001A859A
			[Key(4)]
			public int Minute { get; set; }

			// Token: 0x17000D38 RID: 3384
			// (get) Token: 0x060044C4 RID: 17604 RVA: 0x001AA1A3 File Offset: 0x001A85A3
			// (set) Token: 0x060044C5 RID: 17605 RVA: 0x001AA1AB File Offset: 0x001A85AB
			[Key(5)]
			public int Second { get; set; }

			// Token: 0x17000D39 RID: 3385
			// (get) Token: 0x060044C6 RID: 17606 RVA: 0x001AA1B4 File Offset: 0x001A85B4
			// (set) Token: 0x060044C7 RID: 17607 RVA: 0x001AA1BC File Offset: 0x001A85BC
			[Key(6)]
			public int Millisecond { get; set; }

			// Token: 0x17000D3A RID: 3386
			// (get) Token: 0x060044C8 RID: 17608 RVA: 0x001AA1C8 File Offset: 0x001A85C8
			[IgnoreMember]
			public DateTime DateTime
			{
				get
				{
					DateTime result = new DateTime(this.Year, this.Month, this.Day, this.Hour, this.Minute, this.Second, this.Millisecond);
					return result;
				}
			}

			// Token: 0x060044C9 RID: 17609 RVA: 0x001AA207 File Offset: 0x001A8607
			public static implicit operator AIProject.SaveData.Environment.SerializableDateTime(DateTime dateTime)
			{
				return new AIProject.SaveData.Environment.SerializableDateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond);
			}

			// Token: 0x060044CA RID: 17610 RVA: 0x001AA240 File Offset: 0x001A8640
			public IEnumerable<CommandData> CreateCommandData(string head)
			{
				return new CommandData[]
				{
					new CommandData(CommandData.Command.String, head + string.Format("[{0}]", "DateTime"), () => this.DateTime.ToString(), null)
				};
			}

			// Token: 0x040040B4 RID: 16564
			private int _year;

			// Token: 0x040040B5 RID: 16565
			private int _month;

			// Token: 0x040040B6 RID: 16566
			private int _day;
		}

		// Token: 0x02000979 RID: 2425
		[MessagePackObject(false)]
		public struct SerializableTimeSpan : ICommandData
		{
			// Token: 0x060044CB RID: 17611 RVA: 0x001AA2C0 File Offset: 0x001A86C0
			public SerializableTimeSpan(int days, int hours, int minutes, int seconds, int milliseconds)
			{
				this._days = Mathf.Max(0, days);
				this._hours = Mathf.Max(0, hours);
				this._minutes = Mathf.Max(0, minutes);
				this._seconds = Mathf.Max(0, seconds);
				this._milliseconds = Mathf.Max(0, milliseconds);
			}

			// Token: 0x060044CC RID: 17612 RVA: 0x001AA310 File Offset: 0x001A8710
			public SerializableTimeSpan(int days, int hours, int minutes, int seconds)
			{
				this._days = Mathf.Max(0, days);
				this._hours = Mathf.Max(0, hours);
				this._minutes = Mathf.Max(0, minutes);
				this._seconds = Mathf.Max(0, seconds);
				this._milliseconds = Mathf.Max(0, 0);
			}

			// Token: 0x17000D3B RID: 3387
			// (get) Token: 0x060044CD RID: 17613 RVA: 0x001AA35F File Offset: 0x001A875F
			// (set) Token: 0x060044CE RID: 17614 RVA: 0x001AA36D File Offset: 0x001A876D
			[Key(0)]
			public int Days
			{
				get
				{
					return this.SetDays(this._days);
				}
				set
				{
					this.SetDays(value);
				}
			}

			// Token: 0x060044CF RID: 17615 RVA: 0x001AA378 File Offset: 0x001A8778
			private int SetDays(int days)
			{
				return this._days = Mathf.Max(0, days);
			}

			// Token: 0x17000D3C RID: 3388
			// (get) Token: 0x060044D0 RID: 17616 RVA: 0x001AA395 File Offset: 0x001A8795
			// (set) Token: 0x060044D1 RID: 17617 RVA: 0x001AA3A3 File Offset: 0x001A87A3
			[Key(1)]
			public int Hours
			{
				get
				{
					return this.SetHours(this._hours);
				}
				set
				{
					this.SetHours(value);
				}
			}

			// Token: 0x060044D2 RID: 17618 RVA: 0x001AA3B0 File Offset: 0x001A87B0
			private int SetHours(int hours)
			{
				return this._hours = Mathf.Max(0, hours);
			}

			// Token: 0x17000D3D RID: 3389
			// (get) Token: 0x060044D3 RID: 17619 RVA: 0x001AA3CD File Offset: 0x001A87CD
			// (set) Token: 0x060044D4 RID: 17620 RVA: 0x001AA3DB File Offset: 0x001A87DB
			[Key(2)]
			public int Minutes
			{
				get
				{
					return this.SetMinutes(this._minutes);
				}
				set
				{
					this.SetMinutes(value);
				}
			}

			// Token: 0x060044D5 RID: 17621 RVA: 0x001AA3E8 File Offset: 0x001A87E8
			private int SetMinutes(int minutes)
			{
				return this._minutes = Mathf.Max(0, minutes);
			}

			// Token: 0x17000D3E RID: 3390
			// (get) Token: 0x060044D6 RID: 17622 RVA: 0x001AA405 File Offset: 0x001A8805
			// (set) Token: 0x060044D7 RID: 17623 RVA: 0x001AA413 File Offset: 0x001A8813
			[Key(3)]
			public int Seconds
			{
				get
				{
					return this.SetSeconds(this._seconds);
				}
				set
				{
					this.SetSeconds(value);
				}
			}

			// Token: 0x060044D8 RID: 17624 RVA: 0x001AA420 File Offset: 0x001A8820
			private int SetSeconds(int seconds)
			{
				return this._seconds = Mathf.Max(0, this._seconds);
			}

			// Token: 0x17000D3F RID: 3391
			// (get) Token: 0x060044D9 RID: 17625 RVA: 0x001AA442 File Offset: 0x001A8842
			// (set) Token: 0x060044DA RID: 17626 RVA: 0x001AA450 File Offset: 0x001A8850
			[Key(4)]
			public int MilliSeconds
			{
				get
				{
					return this.SetMilliseconds(this._milliseconds);
				}
				set
				{
					this.SetMilliseconds(value);
				}
			}

			// Token: 0x060044DB RID: 17627 RVA: 0x001AA45C File Offset: 0x001A885C
			private int SetMilliseconds(int milliseconds)
			{
				return this._milliseconds = Mathf.Max(0, this._milliseconds);
			}

			// Token: 0x17000D40 RID: 3392
			// (get) Token: 0x060044DC RID: 17628 RVA: 0x001AA480 File Offset: 0x001A8880
			[IgnoreMember]
			public TimeSpan TimeSpan
			{
				get
				{
					TimeSpan result = new TimeSpan(this.Days, this.Hours, this.Minutes, this.Seconds, this.MilliSeconds);
					return result;
				}
			}

			// Token: 0x060044DD RID: 17629 RVA: 0x001AA4B3 File Offset: 0x001A88B3
			public static implicit operator AIProject.SaveData.Environment.SerializableTimeSpan(TimeSpan timeSpan)
			{
				return new AIProject.SaveData.Environment.SerializableTimeSpan(timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
			}

			// Token: 0x060044DE RID: 17630 RVA: 0x001AA4E0 File Offset: 0x001A88E0
			public IEnumerable<CommandData> CreateCommandData(string head)
			{
				return new CommandData[]
				{
					new CommandData(CommandData.Command.String, head + string.Format("[{0}]", "TimeSpan"), () => this.TimeSpan.ToString(), null)
				};
			}

			// Token: 0x040040BB RID: 16571
			private int _days;

			// Token: 0x040040BC RID: 16572
			private int _hours;

			// Token: 0x040040BD RID: 16573
			private int _minutes;

			// Token: 0x040040BE RID: 16574
			private int _seconds;

			// Token: 0x040040BF RID: 16575
			private int _milliseconds;
		}

		// Token: 0x0200097A RID: 2426
		[MessagePackObject(false)]
		public class ScheduleData
		{
			// Token: 0x060044DF RID: 17631 RVA: 0x001AA55E File Offset: 0x001A895E
			public ScheduleData()
			{
			}

			// Token: 0x060044E0 RID: 17632 RVA: 0x001AA566 File Offset: 0x001A8966
			public ScheduleData(AIProject.SaveData.Environment.ScheduleData source)
			{
				this.DaysToGo = source.DaysToGo;
				this.Event = source.Event;
			}

			// Token: 0x17000D41 RID: 3393
			// (get) Token: 0x060044E1 RID: 17633 RVA: 0x001AA586 File Offset: 0x001A8986
			// (set) Token: 0x060044E2 RID: 17634 RVA: 0x001AA58E File Offset: 0x001A898E
			[Key(0)]
			public int DaysToGo { get; set; }

			// Token: 0x17000D42 RID: 3394
			// (get) Token: 0x060044E3 RID: 17635 RVA: 0x001AA597 File Offset: 0x001A8997
			// (set) Token: 0x060044E4 RID: 17636 RVA: 0x001AA59F File Offset: 0x001A899F
			[Key(1)]
			public AIProject.SaveData.Environment.SerializableDateTime StartTime { get; set; }

			// Token: 0x17000D43 RID: 3395
			// (get) Token: 0x060044E5 RID: 17637 RVA: 0x001AA5A8 File Offset: 0x001A89A8
			// (set) Token: 0x060044E6 RID: 17638 RVA: 0x001AA5B0 File Offset: 0x001A89B0
			[Key(2)]
			public AIProject.SaveData.Environment.SerializableTimeSpan Duration { get; set; }

			// Token: 0x17000D44 RID: 3396
			// (get) Token: 0x060044E7 RID: 17639 RVA: 0x001AA5B9 File Offset: 0x001A89B9
			// (set) Token: 0x060044E8 RID: 17640 RVA: 0x001AA5C1 File Offset: 0x001A89C1
			[Key(3)]
			public AIProject.SaveData.Environment.SchedulingEvent Event { get; set; }
		}

		// Token: 0x0200097B RID: 2427
		[MessagePackObject(false)]
		public struct SchedulingEvent
		{
			// Token: 0x060044E9 RID: 17641 RVA: 0x001AA5CA File Offset: 0x001A89CA
			public SchedulingEvent(int aID, int pID)
			{
				this.agentID = aID;
				this.pointID = pID;
			}

			// Token: 0x040040C4 RID: 16580
			[Key(0)]
			public int agentID;

			// Token: 0x040040C5 RID: 16581
			[Key(1)]
			public int pointID;
		}

		// Token: 0x0200097C RID: 2428
		[MessagePackObject(false)]
		public class SearchActionInfo
		{
			// Token: 0x17000D45 RID: 3397
			// (get) Token: 0x060044EB RID: 17643 RVA: 0x001AA5E2 File Offset: 0x001A89E2
			// (set) Token: 0x060044EC RID: 17644 RVA: 0x001AA5EA File Offset: 0x001A89EA
			[Key(0)]
			public int Count { get; set; }

			// Token: 0x17000D46 RID: 3398
			// (get) Token: 0x060044ED RID: 17645 RVA: 0x001AA5F3 File Offset: 0x001A89F3
			// (set) Token: 0x060044EE RID: 17646 RVA: 0x001AA5FB File Offset: 0x001A89FB
			[Key(1)]
			public float ElapsedTime { get; set; }
		}

		// Token: 0x0200097D RID: 2429
		[MessagePackObject(false)]
		public class PlantInfo
		{
			// Token: 0x060044EF RID: 17647 RVA: 0x001AA604 File Offset: 0x001A8A04
			public PlantInfo(int nameHash, int timeLimit, StuffItem[] result)
			{
				this.nameHash = nameHash;
				this.timeLimit = timeLimit;
				this.result = result;
			}

			// Token: 0x060044F0 RID: 17648 RVA: 0x001AA624 File Offset: 0x001A8A24
			[SerializationConstructor]
			public PlantInfo(int nameHash, int timeLimit, float timer, StuffItem[] result)
			{
				this.nameHash = nameHash;
				this.timeLimit = timeLimit;
				this.timer = timer;
				this.result = (from x in result
				select new StuffItem(x)).ToArray<StuffItem>();
			}

			// Token: 0x17000D47 RID: 3399
			// (get) Token: 0x060044F1 RID: 17649 RVA: 0x001AA67B File Offset: 0x001A8A7B
			[Key(0)]
			public int nameHash { get; }

			// Token: 0x17000D48 RID: 3400
			// (get) Token: 0x060044F2 RID: 17650 RVA: 0x001AA683 File Offset: 0x001A8A83
			[Key(1)]
			public int timeLimit { get; }

			// Token: 0x17000D49 RID: 3401
			// (get) Token: 0x060044F3 RID: 17651 RVA: 0x001AA68B File Offset: 0x001A8A8B
			// (set) Token: 0x060044F4 RID: 17652 RVA: 0x001AA693 File Offset: 0x001A8A93
			[Key(2)]
			public float timer { get; private set; }

			// Token: 0x17000D4A RID: 3402
			// (get) Token: 0x060044F5 RID: 17653 RVA: 0x001AA69C File Offset: 0x001A8A9C
			[Key(3)]
			public StuffItem[] result { get; }

			// Token: 0x17000D4B RID: 3403
			// (get) Token: 0x060044F6 RID: 17654 RVA: 0x001AA6A4 File Offset: 0x001A8AA4
			[IgnoreMember]
			public float progress
			{
				[CompilerGenerated]
				get
				{
					return Mathf.InverseLerp(0f, (float)this.timeLimit, this.timer);
				}
			}

			// Token: 0x17000D4C RID: 3404
			// (get) Token: 0x060044F7 RID: 17655 RVA: 0x001AA6BD File Offset: 0x001A8ABD
			[IgnoreMember]
			public bool isEnd
			{
				[CompilerGenerated]
				get
				{
					return this.timer >= (float)this.timeLimit;
				}
			}

			// Token: 0x060044F8 RID: 17656 RVA: 0x001AA6D1 File Offset: 0x001A8AD1
			public void AddTimer(float add)
			{
				this.timer = Mathf.Min(this.timer + add, (float)this.timeLimit);
			}

			// Token: 0x060044F9 RID: 17657 RVA: 0x001AA6ED File Offset: 0x001A8AED
			public void Finish()
			{
				this.timer = (float)this.timeLimit;
			}

			// Token: 0x060044FA RID: 17658 RVA: 0x001AA6FC File Offset: 0x001A8AFC
			public override string ToString()
			{
				int num = this.timeLimit - (int)this.timer;
				int num2 = num / 3600;
				num %= 3600;
				int num3 = num / 60;
				num %= 60;
				int num4 = num;
				string str = string.Empty;
				if (num2 > 0)
				{
					str += string.Format("{0}", num2);
				}
				return str + string.Format("{0:00}:{1:00}", num3, num4);
			}
		}

		// Token: 0x0200097E RID: 2430
		[MessagePackObject(false)]
		public class ChickenInfo
		{
			// Token: 0x040040CD RID: 16589
			[Key(0)]
			public string name = string.Empty;

			// Token: 0x040040CE RID: 16590
			[Key(1)]
			public AnimalData AnimalData;
		}

		// Token: 0x0200097F RID: 2431
		[MessagePackObject(false)]
		public class PetHomeInfo
		{
			// Token: 0x060044FD RID: 17661 RVA: 0x001AA797 File Offset: 0x001A8B97
			public PetHomeInfo()
			{
			}

			// Token: 0x060044FE RID: 17662 RVA: 0x001AA79F File Offset: 0x001A8B9F
			public PetHomeInfo(AIProject.SaveData.Environment.PetHomeInfo source)
			{
				this.Copy(source);
			}

			// Token: 0x17000D4D RID: 3405
			// (get) Token: 0x060044FF RID: 17663 RVA: 0x001AA7AE File Offset: 0x001A8BAE
			// (set) Token: 0x06004500 RID: 17664 RVA: 0x001AA7B6 File Offset: 0x001A8BB6
			[Key(0)]
			public int HousingID { get; set; }

			// Token: 0x17000D4E RID: 3406
			// (get) Token: 0x06004501 RID: 17665 RVA: 0x001AA7BF File Offset: 0x001A8BBF
			// (set) Token: 0x06004502 RID: 17666 RVA: 0x001AA7C7 File Offset: 0x001A8BC7
			[Key(1)]
			public AnimalData AnimalData { get; set; }

			// Token: 0x17000D4F RID: 3407
			// (get) Token: 0x06004503 RID: 17667 RVA: 0x001AA7D0 File Offset: 0x001A8BD0
			// (set) Token: 0x06004504 RID: 17668 RVA: 0x001AA7D8 File Offset: 0x001A8BD8
			[Key(2)]
			public bool ChaseActor { get; set; }

			// Token: 0x17000D50 RID: 3408
			// (get) Token: 0x06004505 RID: 17669 RVA: 0x001AA7E1 File Offset: 0x001A8BE1
			// (set) Token: 0x06004506 RID: 17670 RVA: 0x001AA7E9 File Offset: 0x001A8BE9
			[Key(3)]
			public bool NicknameDisplay { get; set; }

			// Token: 0x06004507 RID: 17671 RVA: 0x001AA7F4 File Offset: 0x001A8BF4
			public void Copy(AIProject.SaveData.Environment.PetHomeInfo source)
			{
				if (source == null)
				{
					return;
				}
				this.HousingID = source.HousingID;
				if (source.AnimalData != null)
				{
					this.AnimalData = new AnimalData();
					this.AnimalData.Copy(source.AnimalData);
				}
				this.ChaseActor = source.ChaseActor;
				this.NicknameDisplay = source.NicknameDisplay;
			}
		}

		// Token: 0x02000980 RID: 2432
		[MessagePackObject(false)]
		public class DateTemperatureInfo
		{
			// Token: 0x06004508 RID: 17672 RVA: 0x001AA853 File Offset: 0x001A8C53
			public DateTemperatureInfo(Temperature temp, float morning, float day, float night)
			{
				this.Temperature = temp;
				this.MorningTemp = morning;
				this.DayTemp = day;
				this.NightTemp = night;
			}

			// Token: 0x06004509 RID: 17673 RVA: 0x001AA87F File Offset: 0x001A8C7F
			public DateTemperatureInfo()
			{
			}

			// Token: 0x0600450A RID: 17674 RVA: 0x001AA88E File Offset: 0x001A8C8E
			public DateTemperatureInfo(AIProject.SaveData.Environment.DateTemperatureInfo info)
			{
				this.Copy(info);
			}

			// Token: 0x17000D51 RID: 3409
			// (get) Token: 0x0600450B RID: 17675 RVA: 0x001AA8A4 File Offset: 0x001A8CA4
			// (set) Token: 0x0600450C RID: 17676 RVA: 0x001AA8AC File Offset: 0x001A8CAC
			[Key(0)]
			public Temperature Temperature { get; set; } = Temperature.Normal;

			// Token: 0x17000D52 RID: 3410
			// (get) Token: 0x0600450D RID: 17677 RVA: 0x001AA8B5 File Offset: 0x001A8CB5
			// (set) Token: 0x0600450E RID: 17678 RVA: 0x001AA8BD File Offset: 0x001A8CBD
			[Key(1)]
			public float MorningTemp { get; set; }

			// Token: 0x17000D53 RID: 3411
			// (get) Token: 0x0600450F RID: 17679 RVA: 0x001AA8C6 File Offset: 0x001A8CC6
			// (set) Token: 0x06004510 RID: 17680 RVA: 0x001AA8CE File Offset: 0x001A8CCE
			[Key(2)]
			public float DayTemp { get; set; }

			// Token: 0x17000D54 RID: 3412
			// (get) Token: 0x06004511 RID: 17681 RVA: 0x001AA8D7 File Offset: 0x001A8CD7
			// (set) Token: 0x06004512 RID: 17682 RVA: 0x001AA8DF File Offset: 0x001A8CDF
			[Key(3)]
			public float NightTemp { get; set; }

			// Token: 0x06004513 RID: 17683 RVA: 0x001AA8E8 File Offset: 0x001A8CE8
			public void Copy(AIProject.SaveData.Environment.DateTemperatureInfo info)
			{
				if (info == null)
				{
					return;
				}
				this.Temperature = info.Temperature;
				this.MorningTemp = info.MorningTemp;
				this.DayTemp = info.DayTemp;
				this.NightTemp = info.NightTemp;
			}

			// Token: 0x06004514 RID: 17684 RVA: 0x001AA924 File Offset: 0x001A8D24
			public float GetTempValue(TimeZone timeZone)
			{
				switch (timeZone)
				{
				case TimeZone.Morning:
					return this.MorningTemp;
				case TimeZone.Day:
					return this.DayTemp;
				case TimeZone.Night:
					return this.NightTemp;
				}
				return (this.MorningTemp + this.DayTemp + this.NightTemp) / 3f;
			}
		}
	}
}
