using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using ADV;
using AIChara;
using AIProject.Definitions;
using Illusion;
using Manager;
using MessagePack;
using UnityEngine;

namespace AIProject.SaveData
{
	// Token: 0x0200096E RID: 2414
	[MessagePackObject(false)]
	public class AgentData : ICharacterInfo, IDiffComparer, IParams, ICommandData
	{
		// Token: 0x060042F8 RID: 17144 RVA: 0x001A5BF4 File Offset: 0x001A3FF4
		public AgentData()
		{
			this.Init();
		}

		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x060042F9 RID: 17145 RVA: 0x001A5EAC File Offset: 0x001A42AC
		[IgnoreMember]
		public CharaParams param
		{
			get
			{
				return this.GetCache(ref this._param, () => new CharaParams(this, "H"));
			}
		}

		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x060042FA RID: 17146 RVA: 0x001A5EC6 File Offset: 0x001A42C6
		[IgnoreMember]
		Params IParams.param
		{
			[CompilerGenerated]
			get
			{
				return this.param;
			}
		}

		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x060042FB RID: 17147 RVA: 0x001A5ECE File Offset: 0x001A42CE
		// (set) Token: 0x060042FC RID: 17148 RVA: 0x001A5ED6 File Offset: 0x001A42D6
		[Key(0)]
		public System.Version Version { get; set; } = new System.Version();

		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x060042FD RID: 17149 RVA: 0x001A5EDF File Offset: 0x001A42DF
		// (set) Token: 0x060042FE RID: 17150 RVA: 0x001A5EE7 File Offset: 0x001A42E7
		[Key(1)]
		public bool OpenState { get; set; }

		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x060042FF RID: 17151 RVA: 0x001A5EF0 File Offset: 0x001A42F0
		// (set) Token: 0x06004300 RID: 17152 RVA: 0x001A5EF8 File Offset: 0x001A42F8
		[Key(2)]
		public string CharaFileName { get; set; }

		// Token: 0x17000CA1 RID: 3233
		// (get) Token: 0x06004301 RID: 17153 RVA: 0x001A5F01 File Offset: 0x001A4301
		// (set) Token: 0x06004302 RID: 17154 RVA: 0x001A5F09 File Offset: 0x001A4309
		[Key(3)]
		public bool PlayEnterScene { get; set; }

		// Token: 0x17000CA2 RID: 3234
		// (get) Token: 0x06004303 RID: 17155 RVA: 0x001A5F12 File Offset: 0x001A4312
		// (set) Token: 0x06004304 RID: 17156 RVA: 0x001A5F1A File Offset: 0x001A431A
		[Key(4)]
		public Dictionary<int, float> StatsTable { get; set; } = AgentData.StatsTableTemprate.ToDictionary((KeyValuePair<int, float> x) => x.Key, (KeyValuePair<int, float> x) => x.Value);

		// Token: 0x17000CA3 RID: 3235
		// (get) Token: 0x06004305 RID: 17157 RVA: 0x001A5F23 File Offset: 0x001A4323
		// (set) Token: 0x06004306 RID: 17158 RVA: 0x001A5F2B File Offset: 0x001A432B
		[Key(5)]
		public Dictionary<int, float> DesireTable { get; set; } = AgentData.DesireTableTemprate.ToDictionary((KeyValuePair<int, float> x) => x.Key, (KeyValuePair<int, float> x) => x.Value);

		// Token: 0x17000CA4 RID: 3236
		// (get) Token: 0x06004307 RID: 17159 RVA: 0x001A5F34 File Offset: 0x001A4334
		// (set) Token: 0x06004308 RID: 17160 RVA: 0x001A5F3C File Offset: 0x001A433C
		[Key(6)]
		public Dictionary<int, float> MotivationTable { get; set; }

		// Token: 0x17000CA5 RID: 3237
		// (get) Token: 0x06004309 RID: 17161 RVA: 0x001A5F45 File Offset: 0x001A4345
		// (set) Token: 0x0600430A RID: 17162 RVA: 0x001A5F4D File Offset: 0x001A434D
		[Key(9)]
		public int ChunkID { get; set; }

		// Token: 0x17000CA6 RID: 3238
		// (get) Token: 0x0600430B RID: 17163 RVA: 0x001A5F56 File Offset: 0x001A4356
		// (set) Token: 0x0600430C RID: 17164 RVA: 0x001A5F5E File Offset: 0x001A435E
		[Key(10)]
		public int AreaID { get; set; }

		// Token: 0x17000CA7 RID: 3239
		// (get) Token: 0x0600430D RID: 17165 RVA: 0x001A5F67 File Offset: 0x001A4367
		// (set) Token: 0x0600430E RID: 17166 RVA: 0x001A5F6F File Offset: 0x001A436F
		[Key(11)]
		public List<int> ReservedWaypointIDList { get; set; } = new List<int>();

		// Token: 0x17000CA8 RID: 3240
		// (get) Token: 0x0600430F RID: 17167 RVA: 0x001A5F78 File Offset: 0x001A4378
		// (set) Token: 0x06004310 RID: 17168 RVA: 0x001A5F80 File Offset: 0x001A4380
		[Key(12)]
		public List<int> WalkRouteWaypointIDList { get; set; } = new List<int>();

		// Token: 0x17000CA9 RID: 3241
		// (get) Token: 0x06004311 RID: 17169 RVA: 0x001A5F89 File Offset: 0x001A4389
		// (set) Token: 0x06004312 RID: 17170 RVA: 0x001A5F91 File Offset: 0x001A4391
		[Key(15)]
		public int ActionTargetID { get; set; } = -1;

		// Token: 0x17000CAA RID: 3242
		// (get) Token: 0x06004313 RID: 17171 RVA: 0x001A5F9A File Offset: 0x001A439A
		// (set) Token: 0x06004314 RID: 17172 RVA: 0x001A5FA2 File Offset: 0x001A43A2
		[Key(16)]
		public Desire.ActionType ModeType { get; set; }

		// Token: 0x17000CAB RID: 3243
		// (get) Token: 0x06004315 RID: 17173 RVA: 0x001A5FAB File Offset: 0x001A43AB
		// (set) Token: 0x06004316 RID: 17174 RVA: 0x001A5FB3 File Offset: 0x001A43B3
		[Key(17)]
		public Dictionary<int, int> TiredNumberTable { get; set; } = new Dictionary<int, int>();

		// Token: 0x17000CAC RID: 3244
		// (get) Token: 0x06004317 RID: 17175 RVA: 0x001A5FBC File Offset: 0x001A43BC
		// (set) Token: 0x06004318 RID: 17176 RVA: 0x001A5FC4 File Offset: 0x001A43C4
		[Key(21)]
		public Vector3 Position { get; set; }

		// Token: 0x17000CAD RID: 3245
		// (get) Token: 0x06004319 RID: 17177 RVA: 0x001A5FCD File Offset: 0x001A43CD
		// (set) Token: 0x0600431A RID: 17178 RVA: 0x001A5FD5 File Offset: 0x001A43D5
		[Key(22)]
		public Quaternion Rotation { get; set; }

		// Token: 0x17000CAE RID: 3246
		// (get) Token: 0x0600431B RID: 17179 RVA: 0x001A5FDE File Offset: 0x001A43DE
		// (set) Token: 0x0600431C RID: 17180 RVA: 0x001A5FE6 File Offset: 0x001A43E6
		[Key(23)]
		public Sickness SickState { get; set; } = new Sickness();

		// Token: 0x17000CAF RID: 3247
		// (get) Token: 0x0600431D RID: 17181 RVA: 0x001A5FEF File Offset: 0x001A43EF
		// (set) Token: 0x0600431E RID: 17182 RVA: 0x001A5FF7 File Offset: 0x001A43F7
		[Key(24)]
		public float Wetness { get; set; }

		// Token: 0x17000CB0 RID: 3248
		// (get) Token: 0x0600431F RID: 17183 RVA: 0x001A6000 File Offset: 0x001A4400
		// (set) Token: 0x06004320 RID: 17184 RVA: 0x001A6008 File Offset: 0x001A4408
		[Key(25)]
		public List<StuffItem> ItemList { get; set; } = new List<StuffItem>();

		// Token: 0x17000CB1 RID: 3249
		// (get) Token: 0x06004321 RID: 17185 RVA: 0x001A6011 File Offset: 0x001A4411
		// (set) Token: 0x06004322 RID: 17186 RVA: 0x001A6019 File Offset: 0x001A4419
		[Key(27)]
		public StuffItem CarryingItem { get; set; }

		// Token: 0x17000CB2 RID: 3250
		// (get) Token: 0x06004323 RID: 17187 RVA: 0x001A6022 File Offset: 0x001A4422
		// (set) Token: 0x06004324 RID: 17188 RVA: 0x001A602A File Offset: 0x001A442A
		[Key(28)]
		public StuffItem EquipedGloveItem { get; set; } = new StuffItem
		{
			CategoryID = -1,
			ID = -1
		};

		// Token: 0x17000CB3 RID: 3251
		// (get) Token: 0x06004325 RID: 17189 RVA: 0x001A6033 File Offset: 0x001A4433
		// (set) Token: 0x06004326 RID: 17190 RVA: 0x001A603B File Offset: 0x001A443B
		[Key(29)]
		public StuffItem EquipedShovelItem { get; set; } = new StuffItem
		{
			CategoryID = -1,
			ID = -1
		};

		// Token: 0x17000CB4 RID: 3252
		// (get) Token: 0x06004327 RID: 17191 RVA: 0x001A6044 File Offset: 0x001A4444
		// (set) Token: 0x06004328 RID: 17192 RVA: 0x001A604C File Offset: 0x001A444C
		[Key(30)]
		public StuffItem EquipedPickelItem { get; set; } = new StuffItem
		{
			CategoryID = -1,
			ID = -1
		};

		// Token: 0x17000CB5 RID: 3253
		// (get) Token: 0x06004329 RID: 17193 RVA: 0x001A6055 File Offset: 0x001A4455
		// (set) Token: 0x0600432A RID: 17194 RVA: 0x001A605D File Offset: 0x001A445D
		[Key(31)]
		public StuffItem EquipedNetItem { get; set; } = new StuffItem
		{
			CategoryID = -1,
			ID = -1
		};

		// Token: 0x17000CB6 RID: 3254
		// (get) Token: 0x0600432B RID: 17195 RVA: 0x001A6066 File Offset: 0x001A4466
		// (set) Token: 0x0600432C RID: 17196 RVA: 0x001A606E File Offset: 0x001A446E
		[Key(32)]
		public StuffItem EquipedFishingItem { get; set; } = new StuffItem
		{
			CategoryID = -1,
			ID = -1
		};

		// Token: 0x17000CB7 RID: 3255
		// (get) Token: 0x0600432D RID: 17197 RVA: 0x001A6077 File Offset: 0x001A4477
		// (set) Token: 0x0600432E RID: 17198 RVA: 0x001A607F File Offset: 0x001A447F
		[Key(33)]
		public StuffItem EquipedHeadItem { get; set; } = new StuffItem
		{
			CategoryID = -1,
			ID = -1
		};

		// Token: 0x17000CB8 RID: 3256
		// (get) Token: 0x0600432F RID: 17199 RVA: 0x001A6088 File Offset: 0x001A4488
		// (set) Token: 0x06004330 RID: 17200 RVA: 0x001A6090 File Offset: 0x001A4490
		[Key(34)]
		public StuffItem EquipedBackItem { get; set; } = new StuffItem
		{
			CategoryID = -1,
			ID = -1
		};

		// Token: 0x17000CB9 RID: 3257
		// (get) Token: 0x06004331 RID: 17201 RVA: 0x001A6099 File Offset: 0x001A4499
		// (set) Token: 0x06004332 RID: 17202 RVA: 0x001A60A1 File Offset: 0x001A44A1
		[Key(35)]
		public StuffItem EquipedNeckItem { get; set; } = new StuffItem
		{
			CategoryID = -1,
			ID = -1
		};

		// Token: 0x17000CBA RID: 3258
		// (get) Token: 0x06004333 RID: 17203 RVA: 0x001A60AA File Offset: 0x001A44AA
		// (set) Token: 0x06004334 RID: 17204 RVA: 0x001A60B2 File Offset: 0x001A44B2
		[Key(36)]
		public StuffItem EquipedLampItem { get; set; } = new StuffItem
		{
			CategoryID = -1,
			ID = -1
		};

		// Token: 0x17000CBB RID: 3259
		// (get) Token: 0x06004335 RID: 17205 RVA: 0x001A60BB File Offset: 0x001A44BB
		// (set) Token: 0x06004336 RID: 17206 RVA: 0x001A60C3 File Offset: 0x001A44C3
		[Key(37)]
		public StuffItem EquipedUmbrellaItem { get; set; } = new StuffItem
		{
			CategoryID = -1,
			ID = -1
		};

		// Token: 0x17000CBC RID: 3260
		// (get) Token: 0x06004337 RID: 17207 RVA: 0x001A60CC File Offset: 0x001A44CC
		// (set) Token: 0x06004338 RID: 17208 RVA: 0x001A60D4 File Offset: 0x001A44D4
		[Key(38)]
		public Dictionary<int, int> FriendlyRelationShipTable { get; set; } = AgentData.FriendlyRelationShipTableTemprate.ToDictionary((KeyValuePair<int, int> x) => x.Key, (KeyValuePair<int, int> x) => x.Value);

		// Token: 0x17000CBD RID: 3261
		// (get) Token: 0x06004339 RID: 17209 RVA: 0x001A60DD File Offset: 0x001A44DD
		// (set) Token: 0x0600433A RID: 17210 RVA: 0x001A60E5 File Offset: 0x001A44E5
		[Key(39)]
		public float CallCTCount { get; set; }

		// Token: 0x17000CBE RID: 3262
		// (get) Token: 0x0600433B RID: 17211 RVA: 0x001A60EE File Offset: 0x001A44EE
		// (set) Token: 0x0600433C RID: 17212 RVA: 0x001A60F6 File Offset: 0x001A44F6
		[Key(40)]
		public int CalledCount { get; set; }

		// Token: 0x17000CBF RID: 3263
		// (get) Token: 0x0600433D RID: 17213 RVA: 0x001A60FF File Offset: 0x001A44FF
		// (set) Token: 0x0600433E RID: 17214 RVA: 0x001A6107 File Offset: 0x001A4507
		[Key(41)]
		public bool Greeted { get; set; }

		// Token: 0x17000CC0 RID: 3264
		// (get) Token: 0x0600433F RID: 17215 RVA: 0x001A6110 File Offset: 0x001A4510
		// (set) Token: 0x06004340 RID: 17216 RVA: 0x001A6118 File Offset: 0x001A4518
		[Key(42)]
		public Tutorial.ActionType TutorialModeType { get; set; }

		// Token: 0x17000CC1 RID: 3265
		// (get) Token: 0x06004341 RID: 17217 RVA: 0x001A6121 File Offset: 0x001A4521
		// (set) Token: 0x06004342 RID: 17218 RVA: 0x001A6129 File Offset: 0x001A4529
		[Key(43)]
		public float TalkMotivation { get; set; }

		// Token: 0x17000CC2 RID: 3266
		// (get) Token: 0x06004343 RID: 17219 RVA: 0x001A6132 File Offset: 0x001A4532
		// (set) Token: 0x06004344 RID: 17220 RVA: 0x001A613A File Offset: 0x001A453A
		[Key(44)]
		public int FlavorAdditionAmount { get; set; }

		// Token: 0x17000CC3 RID: 3267
		// (get) Token: 0x06004345 RID: 17221 RVA: 0x001A6143 File Offset: 0x001A4543
		// (set) Token: 0x06004346 RID: 17222 RVA: 0x001A614B File Offset: 0x001A454B
		[Key(45)]
		public bool CheckCatEvent { get; set; }

		// Token: 0x17000CC4 RID: 3268
		// (get) Token: 0x06004347 RID: 17223 RVA: 0x001A6154 File Offset: 0x001A4554
		// (set) Token: 0x06004348 RID: 17224 RVA: 0x001A615C File Offset: 0x001A455C
		[Key(46)]
		public string NowCoordinateFileName { get; set; }

		// Token: 0x17000CC5 RID: 3269
		// (get) Token: 0x06004349 RID: 17225 RVA: 0x001A6165 File Offset: 0x001A4565
		// (set) Token: 0x0600434A RID: 17226 RVA: 0x001A616D File Offset: 0x001A456D
		[Key(47)]
		public string BathCoordinateFileName { get; set; }

		// Token: 0x17000CC6 RID: 3270
		// (get) Token: 0x0600434B RID: 17227 RVA: 0x001A6176 File Offset: 0x001A4576
		// (set) Token: 0x0600434C RID: 17228 RVA: 0x001A617E File Offset: 0x001A457E
		[Key(48)]
		public bool PlayedDressIn { get; set; }

		// Token: 0x17000CC7 RID: 3271
		// (get) Token: 0x0600434D RID: 17229 RVA: 0x001A6187 File Offset: 0x001A4587
		// (set) Token: 0x0600434E RID: 17230 RVA: 0x001A618F File Offset: 0x001A458F
		[Key(49)]
		public Dictionary<int, AIProject.SaveData.Environment.SearchActionInfo> SearchActionLockTable { get; set; } = new Dictionary<int, AIProject.SaveData.Environment.SearchActionInfo>();

		// Token: 0x17000CC8 RID: 3272
		// (get) Token: 0x0600434F RID: 17231 RVA: 0x001A6198 File Offset: 0x001A4598
		// (set) Token: 0x06004350 RID: 17232 RVA: 0x001A61A0 File Offset: 0x001A45A0
		[Key(50)]
		public bool IsOtherCoordinate { get; set; }

		// Token: 0x17000CC9 RID: 3273
		// (get) Token: 0x06004351 RID: 17233 RVA: 0x001A61A9 File Offset: 0x001A45A9
		// (set) Token: 0x06004352 RID: 17234 RVA: 0x001A61B1 File Offset: 0x001A45B1
		[Key(51)]
		public ItemScrounge ItemScrounge { get; set; } = new ItemScrounge();

		// Token: 0x17000CCA RID: 3274
		// (get) Token: 0x06004353 RID: 17235 RVA: 0x001A61BA File Offset: 0x001A45BA
		// (set) Token: 0x06004354 RID: 17236 RVA: 0x001A61C2 File Offset: 0x001A45C2
		[Key(52)]
		public bool LockTalk { get; set; }

		// Token: 0x17000CCB RID: 3275
		// (get) Token: 0x06004355 RID: 17237 RVA: 0x001A61CB File Offset: 0x001A45CB
		// (set) Token: 0x06004356 RID: 17238 RVA: 0x001A61D3 File Offset: 0x001A45D3
		[Key(53)]
		public float TalkElapsedTime { get; set; }

		// Token: 0x17000CCC RID: 3276
		// (get) Token: 0x06004357 RID: 17239 RVA: 0x001A61DC File Offset: 0x001A45DC
		// (set) Token: 0x06004358 RID: 17240 RVA: 0x001A61E4 File Offset: 0x001A45E4
		[Key(54)]
		public bool IsPlayerForBirthdayEvent { get; set; }

		// Token: 0x17000CCD RID: 3277
		// (get) Token: 0x06004359 RID: 17241 RVA: 0x001A61ED File Offset: 0x001A45ED
		// (set) Token: 0x0600435A RID: 17242 RVA: 0x001A61F5 File Offset: 0x001A45F5
		[Key(55)]
		public Dictionary<int, HashSet<string>> advEventCheck { get; set; } = new Dictionary<int, HashSet<string>>();

		// Token: 0x17000CCE RID: 3278
		// (get) Token: 0x0600435B RID: 17243 RVA: 0x001A61FE File Offset: 0x001A45FE
		// (set) Token: 0x0600435C RID: 17244 RVA: 0x001A6206 File Offset: 0x001A4606
		[Key(56)]
		public bool IsWet { get; set; }

		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x0600435D RID: 17245 RVA: 0x001A620F File Offset: 0x001A460F
		// (set) Token: 0x0600435E RID: 17246 RVA: 0x001A6217 File Offset: 0x001A4617
		[Key(57)]
		public HashSet<int> advEventLimitation { get; set; } = new HashSet<int>();

		// Token: 0x17000CD0 RID: 3280
		// (get) Token: 0x0600435F RID: 17247 RVA: 0x001A6220 File Offset: 0x001A4620
		// (set) Token: 0x06004360 RID: 17248 RVA: 0x001A6228 File Offset: 0x001A4628
		[Key(58)]
		public Desire.ActionType PrevMode { get; set; }

		// Token: 0x17000CD1 RID: 3281
		// (get) Token: 0x06004361 RID: 17249 RVA: 0x001A6231 File Offset: 0x001A4631
		// (set) Token: 0x06004362 RID: 17250 RVA: 0x001A6239 File Offset: 0x001A4639
		[Key(59)]
		public int LocationCount { get; set; }

		// Token: 0x17000CD2 RID: 3282
		// (get) Token: 0x06004363 RID: 17251 RVA: 0x001A6242 File Offset: 0x001A4642
		// (set) Token: 0x06004364 RID: 17252 RVA: 0x001A624A File Offset: 0x001A464A
		[Key(60)]
		public int LocationTaskCount { get; set; }

		// Token: 0x17000CD3 RID: 3283
		// (get) Token: 0x06004365 RID: 17253 RVA: 0x001A6253 File Offset: 0x001A4653
		// (set) Token: 0x06004366 RID: 17254 RVA: 0x001A625B File Offset: 0x001A465B
		[Key(61)]
		public int CurrentActionPointID { get; set; } = -1;

		// Token: 0x17000CD4 RID: 3284
		// (get) Token: 0x06004367 RID: 17255 RVA: 0x001A6264 File Offset: 0x001A4664
		// (set) Token: 0x06004368 RID: 17256 RVA: 0x001A626C File Offset: 0x001A466C
		[Key(62)]
		public bool ScheduleEnabled { get; set; }

		// Token: 0x17000CD5 RID: 3285
		// (get) Token: 0x06004369 RID: 17257 RVA: 0x001A6275 File Offset: 0x001A4675
		// (set) Token: 0x0600436A RID: 17258 RVA: 0x001A627D File Offset: 0x001A467D
		[Key(63)]
		public float ScheduleElapsedTime { get; set; }

		// Token: 0x17000CD6 RID: 3286
		// (get) Token: 0x0600436B RID: 17259 RVA: 0x001A6286 File Offset: 0x001A4686
		// (set) Token: 0x0600436C RID: 17260 RVA: 0x001A628E File Offset: 0x001A468E
		[Key(64)]
		public float ScheduleDuration { get; set; }

		// Token: 0x17000CD7 RID: 3287
		// (get) Token: 0x0600436D RID: 17261 RVA: 0x001A6297 File Offset: 0x001A4697
		// (set) Token: 0x0600436E RID: 17262 RVA: 0x001A629F File Offset: 0x001A469F
		[Key(65)]
		public float WeaknessMotivation { get; set; }

		// Token: 0x17000CD8 RID: 3288
		// (get) Token: 0x0600436F RID: 17263 RVA: 0x001A62A8 File Offset: 0x001A46A8
		// (set) Token: 0x06004370 RID: 17264 RVA: 0x001A62B0 File Offset: 0x001A46B0
		[Key(66)]
		public SickLockInfo ColdLockInfo { get; set; } = new SickLockInfo();

		// Token: 0x17000CD9 RID: 3289
		// (get) Token: 0x06004371 RID: 17265 RVA: 0x001A62B9 File Offset: 0x001A46B9
		// (set) Token: 0x06004372 RID: 17266 RVA: 0x001A62C1 File Offset: 0x001A46C1
		[Key(67)]
		public SickLockInfo HeatStrokeLockInfo { get; set; } = new SickLockInfo();

		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x06004373 RID: 17267 RVA: 0x001A62CA File Offset: 0x001A46CA
		// (set) Token: 0x06004374 RID: 17268 RVA: 0x001A62D2 File Offset: 0x001A46D2
		[Key(68)]
		public bool YobaiTrigger { get; set; }

		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x06004375 RID: 17269 RVA: 0x001A62DB File Offset: 0x001A46DB
		// (set) Token: 0x06004376 RID: 17270 RVA: 0x001A62E3 File Offset: 0x001A46E3
		[Key(69)]
		public int MapID { get; set; }

		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x06004377 RID: 17271 RVA: 0x001A62EC File Offset: 0x001A46EC
		// (set) Token: 0x06004378 RID: 17272 RVA: 0x001A62F4 File Offset: 0x001A46F4
		[Key(70)]
		public bool ParameterLock { get; set; }

		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x06004379 RID: 17273 RVA: 0x001A62FD File Offset: 0x001A46FD
		// (set) Token: 0x0600437A RID: 17274 RVA: 0x001A6305 File Offset: 0x001A4705
		[Key(71)]
		public int? PrevMapID { get; set; }

		// Token: 0x17000CDE RID: 3294
		// (get) Token: 0x0600437B RID: 17275 RVA: 0x001A630E File Offset: 0x001A470E
		// (set) Token: 0x0600437C RID: 17276 RVA: 0x001A6316 File Offset: 0x001A4716
		[Key(72)]
		public AIProject.SaveData.Environment.SerializableTimeSpan AssignedDuration { get; set; }

		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x0600437D RID: 17277 RVA: 0x001A631F File Offset: 0x001A471F
		// (set) Token: 0x0600437E RID: 17278 RVA: 0x001A6327 File Offset: 0x001A4727
		[Key(73)]
		public AIProject.SaveData.Environment.SerializableTimeSpan ADVEventTimeCount { get; set; }

		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x0600437F RID: 17279 RVA: 0x001A6330 File Offset: 0x001A4730
		// (set) Token: 0x06004380 RID: 17280 RVA: 0x001A6338 File Offset: 0x001A4738
		[Key(74)]
		public Dictionary<int, bool> AppendEventFlagCheck { get; set; } = new Dictionary<int, bool>();

		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x06004381 RID: 17281 RVA: 0x001A6341 File Offset: 0x001A4741
		// (set) Token: 0x06004382 RID: 17282 RVA: 0x001A6349 File Offset: 0x001A4749
		[Key(75)]
		public Dictionary<int, int> AppendEventFlagParam { get; set; } = new Dictionary<int, int>();

		// Token: 0x17000CE2 RID: 3298
		// (get) Token: 0x06004383 RID: 17283 RVA: 0x001A6352 File Offset: 0x001A4752
		// (set) Token: 0x06004384 RID: 17284 RVA: 0x001A635A File Offset: 0x001A475A
		[Key(76)]
		public int ADVEventTimeCond { get; set; }

		// Token: 0x17000CE3 RID: 3299
		// (get) Token: 0x06004385 RID: 17285 RVA: 0x001A6363 File Offset: 0x001A4763
		// (set) Token: 0x06004386 RID: 17286 RVA: 0x001A636B File Offset: 0x001A476B
		[Key(77)]
		public bool YandereWarpLimitation { get; set; }

		// Token: 0x17000CE4 RID: 3300
		// (get) Token: 0x06004387 RID: 17287 RVA: 0x001A6374 File Offset: 0x001A4774
		// (set) Token: 0x06004388 RID: 17288 RVA: 0x001A637C File Offset: 0x001A477C
		[Key(78)]
		public bool YandereWarpRetry { get; set; }

		// Token: 0x17000CE5 RID: 3301
		// (get) Token: 0x06004389 RID: 17289 RVA: 0x001A6385 File Offset: 0x001A4785
		// (set) Token: 0x0600438A RID: 17290 RVA: 0x001A638D File Offset: 0x001A478D
		[Key(79)]
		public float YandereWarpRetryTimer { get; set; }

		// Token: 0x17000CE6 RID: 3302
		// (get) Token: 0x0600438B RID: 17291 RVA: 0x001A6396 File Offset: 0x001A4796
		// (set) Token: 0x0600438C RID: 17292 RVA: 0x001A639E File Offset: 0x001A479E
		[Key(80)]
		public Dictionary<int, bool> AppendEventLimitation { get; set; } = new Dictionary<int, bool>();

		// Token: 0x17000CE7 RID: 3303
		// (get) Token: 0x0600438D RID: 17293 RVA: 0x001A63A8 File Offset: 0x001A47A8
		public static Dictionary<int, float> StatsTableTemprate
		{
			get
			{
				Dictionary<int, float> dictionary = new Dictionary<int, float>();
				dictionary[0] = 50f;
				dictionary[1] = 50f;
				dictionary[2] = 100f;
				dictionary[3] = 100f;
				dictionary[4] = 100f;
				dictionary[5] = 100f;
				dictionary[6] = 0f;
				return dictionary;
			}
		}

		// Token: 0x17000CE8 RID: 3304
		// (get) Token: 0x0600438E RID: 17294 RVA: 0x001A6410 File Offset: 0x001A4810
		public static Dictionary<int, int> FriendlyRelationShipTableTemprate
		{
			get
			{
				Dictionary<int, int> dictionary = new Dictionary<int, int>();
				dictionary[-99] = 50;
				dictionary[-90] = 50;
				dictionary[0] = 50;
				dictionary[1] = 50;
				dictionary[2] = 50;
				dictionary[3] = 50;
				return dictionary;
			}
		}

		// Token: 0x17000CE9 RID: 3305
		// (get) Token: 0x0600438F RID: 17295 RVA: 0x001A645C File Offset: 0x001A485C
		public static Dictionary<int, float> DesireTableTemprate
		{
			[CompilerGenerated]
			get
			{
				return AgentData.DesireTableKeys.ToDictionary((int x) => x, (int _) => 0f);
			}
		}

		// Token: 0x17000CEA RID: 3306
		// (get) Token: 0x06004390 RID: 17296 RVA: 0x001A64AD File Offset: 0x001A48AD
		public static ReadOnlyDictionary<int, int> FlavorSkillTemplate { get; } = new ReadOnlyDictionary<int, int>(Enumerable.Range(0, 8).ToDictionary((int x) => x, (int x) => 0));

		// Token: 0x06004391 RID: 17297 RVA: 0x001A64B4 File Offset: 0x001A48B4
		public StuffItem EquipedSearchItem(int id)
		{
			switch (id)
			{
			case 0:
			case 1:
			case 2:
				return this.EquipedGloveItem;
			case 3:
				return this.EquipedShovelItem;
			case 4:
				return this.EquipedPickelItem;
			case 5:
				return this.EquipedNetItem;
			case 6:
				return this.EquipedFishingItem;
			default:
				return null;
			}
		}

		// Token: 0x06004392 RID: 17298 RVA: 0x001A650C File Offset: 0x001A490C
		public void Init()
		{
			this.MotivationTable = AgentData.DesireTableKeys.ToDictionary((int x) => x, (int _) => this.StatsTable[5]);
			this.TalkMotivation = this.StatsTable[5];
		}

		// Token: 0x06004393 RID: 17299 RVA: 0x001A6564 File Offset: 0x001A4964
		public void Copy(AgentData source)
		{
			this.Version = source.Version;
			this.OpenState = source.OpenState;
			this.CharaFileName = source.CharaFileName;
			this.PlayEnterScene = source.PlayEnterScene;
			if (!this.PlayEnterScene)
			{
				this.CharaFileName = null;
			}
			this.StatsTable = source.StatsTable.ToDictionary((KeyValuePair<int, float> x) => x.Key, (KeyValuePair<int, float> x) => x.Value);
			this.DesireTable = source.DesireTable.ToDictionary((KeyValuePair<int, float> x) => x.Key, (KeyValuePair<int, float> x) => x.Value);
			this.MotivationTable = source.MotivationTable.ToDictionary((KeyValuePair<int, float> x) => x.Key, (KeyValuePair<int, float> x) => x.Value);
			this.ChunkID = source.ChunkID;
			this.AreaID = source.AreaID;
			this.ReservedWaypointIDList = source.ReservedWaypointIDList.ToList<int>();
			this.WalkRouteWaypointIDList = source.WalkRouteWaypointIDList.ToList<int>();
			this.ActionTargetID = source.ActionTargetID;
			this.ModeType = source.ModeType;
			this.TiredNumberTable = source.TiredNumberTable.ToDictionary((KeyValuePair<int, int> x) => x.Key, (KeyValuePair<int, int> x) => x.Value);
			this.Position = source.Position;
			this.Rotation = source.Rotation;
			this.SickState = new Sickness(source.SickState);
			this.ItemList.Clear();
			foreach (StuffItem source2 in source.ItemList)
			{
				this.ItemList.Add(new StuffItem(source2));
			}
			if (source.CarryingItem != null)
			{
				this.CarryingItem = new StuffItem(source.CarryingItem);
			}
			else
			{
				this.CarryingItem = null;
			}
			this.EquipedGloveItem = new StuffItem(source.EquipedGloveItem);
			this.EquipedShovelItem = new StuffItem(source.EquipedShovelItem);
			this.EquipedPickelItem = new StuffItem(source.EquipedPickelItem);
			this.EquipedNetItem = new StuffItem(source.EquipedNetItem);
			this.EquipedFishingItem = new StuffItem(source.EquipedFishingItem);
			this.EquipedHeadItem = new StuffItem(source.EquipedHeadItem);
			this.EquipedBackItem = new StuffItem(source.EquipedBackItem);
			this.EquipedNeckItem = new StuffItem(source.EquipedNeckItem);
			this.EquipedLampItem = new StuffItem(source.EquipedLampItem);
			this.EquipedUmbrellaItem = new StuffItem(source.EquipedUmbrellaItem);
			this.FriendlyRelationShipTable = source.FriendlyRelationShipTable.ToDictionary((KeyValuePair<int, int> x) => x.Key, (KeyValuePair<int, int> x) => x.Value);
			this.CallCTCount = source.CallCTCount;
			this.CalledCount = source.CalledCount;
			this.Greeted = source.Greeted;
			this.TutorialModeType = source.TutorialModeType;
			this.TalkMotivation = source.TalkMotivation;
			this.FlavorAdditionAmount = source.FlavorAdditionAmount;
			this.CheckCatEvent = source.CheckCatEvent;
			this.NowCoordinateFileName = source.NowCoordinateFileName;
			this.BathCoordinateFileName = source.BathCoordinateFileName;
			this.PlayedDressIn = source.PlayedDressIn;
			this.SearchActionLockTable = source.SearchActionLockTable.ToDictionary((KeyValuePair<int, AIProject.SaveData.Environment.SearchActionInfo> x) => x.Key, (KeyValuePair<int, AIProject.SaveData.Environment.SearchActionInfo> x) => x.Value);
			this.IsOtherCoordinate = source.IsOtherCoordinate;
			this.ItemScrounge = new ItemScrounge(source.ItemScrounge);
			this.LockTalk = source.LockTalk;
			this.TalkElapsedTime = source.TalkElapsedTime;
			this.IsPlayerForBirthdayEvent = source.IsPlayerForBirthdayEvent;
			if (source.advEventCheck == null)
			{
				source.advEventCheck = new Dictionary<int, HashSet<string>>();
			}
			this.advEventCheck = source.advEventCheck.ToDictionary((KeyValuePair<int, HashSet<string>> v) => v.Key, (KeyValuePair<int, HashSet<string>> v) => new HashSet<string>(v.Value));
			this.IsWet = source.IsWet;
			if (source.advEventLimitation == null)
			{
				source.advEventLimitation = new HashSet<int>();
			}
			this.advEventLimitation = new HashSet<int>(source.advEventLimitation);
			this.PrevMode = source.PrevMode;
			this.CurrentActionPointID = source.CurrentActionPointID;
			this.ScheduleEnabled = source.ScheduleEnabled;
			this.ScheduleElapsedTime = source.ScheduleElapsedTime;
			this.ScheduleDuration = source.ScheduleDuration;
			this.WeaknessMotivation = source.WeaknessMotivation;
			if (source.ColdLockInfo != null)
			{
				this.ColdLockInfo = new SickLockInfo
				{
					Lock = source.ColdLockInfo.Lock,
					ElapsedTime = source.ColdLockInfo.ElapsedTime
				};
			}
			if (source.HeatStrokeLockInfo != null)
			{
				this.HeatStrokeLockInfo = new SickLockInfo
				{
					Lock = source.HeatStrokeLockInfo.Lock,
					ElapsedTime = source.HeatStrokeLockInfo.ElapsedTime
				};
			}
			this.MapID = source.MapID;
			this.ParameterLock = source.ParameterLock;
			this.PrevMapID = source.PrevMapID;
			this.AssignedDuration = source.AssignedDuration;
			this.ADVEventTimeCount = source.ADVEventTimeCount;
			if (source.AppendEventFlagCheck == null)
			{
				source.AppendEventFlagCheck = new Dictionary<int, bool>();
			}
			this.AppendEventFlagCheck = source.AppendEventFlagCheck.ToDictionary((KeyValuePair<int, bool> x) => x.Key, (KeyValuePair<int, bool> x) => x.Value);
			if (source.AppendEventFlagParam == null)
			{
				source.AppendEventFlagParam = new Dictionary<int, int>();
			}
			this.AppendEventFlagParam = source.AppendEventFlagParam.ToDictionary((KeyValuePair<int, int> x) => x.Key, (KeyValuePair<int, int> x) => x.Value);
			if (source.ADVEventTimeCond == 0)
			{
				int randomValue = Singleton<Manager.Resources>.Instance.AgentProfile.DayRandElapseCheck.RandomValue;
				source.ADVEventTimeCond = randomValue;
			}
			this.ADVEventTimeCond = source.ADVEventTimeCond;
			this.YandereWarpLimitation = source.YandereWarpLimitation;
			this.YandereWarpRetry = source.YandereWarpRetry;
			this.YandereWarpRetryTimer = source.YandereWarpRetryTimer;
			if (source.AppendEventLimitation == null)
			{
				source.AppendEventLimitation = new Dictionary<int, bool>();
			}
			this.AppendEventLimitation = new Dictionary<int, bool>(source.AppendEventLimitation);
		}

		// Token: 0x06004394 RID: 17300 RVA: 0x001A6CB0 File Offset: 0x001A50B0
		public void ComplementDiff()
		{
			if (this.Version < new System.Version("0.0.1"))
			{
				this.ModeType = Desire.ActionType.Normal;
				this.CurrentActionPointID = -1;
			}
			if (this.Version < new System.Version("0.0.2"))
			{
				IEnumerator enumerator = Illusion.Utils.Enum<EventFlags>.Values.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						EventFlags eventFlags = (EventFlags)obj;
						if (eventFlags < EventFlags.HaveCat || eventFlags > EventFlags.DayCheck5)
						{
							int key = (int)eventFlags;
							if (!this.AppendEventFlagCheck.ContainsKey(key))
							{
								this.AppendEventFlagCheck[key] = false;
							}
						}
					}
				}
				finally
				{
					IDisposable disposable;
					if ((disposable = (enumerator as IDisposable)) != null)
					{
						disposable.Dispose();
					}
				}
				IEnumerator enumerator2 = Illusion.Utils.Enum<EventParams>.Values.GetEnumerator();
				try
				{
					while (enumerator2.MoveNext())
					{
						object obj2 = enumerator2.Current;
						EventParams eventParams = (EventParams)obj2;
						int key2 = (int)eventParams;
						if (!this.AppendEventFlagParam.ContainsKey(key2))
						{
							this.AppendEventFlagParam[key2] = 0;
						}
					}
				}
				finally
				{
					IDisposable disposable2;
					if ((disposable2 = (enumerator2 as IDisposable)) != null)
					{
						disposable2.Dispose();
					}
				}
			}
			this.Version = AIProject.Definitions.Version.AgentDataVersion;
		}

		// Token: 0x06004395 RID: 17301 RVA: 0x001A6E08 File Offset: 0x001A5208
		public void UpdateDiff()
		{
			foreach (int key in AgentData.StatsTableTemprate.Keys)
			{
				if (!this.StatsTable.ContainsKey(key))
				{
					this.StatsTable[key] = AgentData.StatsTableTemprate[key];
				}
			}
			foreach (int key2 in AgentData.DesireTableTemprate.Keys)
			{
				float num;
				if (this.DesireTable.TryGetValue(key2, out num))
				{
					this.DesireTable[key2] = AgentData.DesireTableTemprate[key2];
				}
			}
			this.ItemList.OrganizeItemList();
		}

		// Token: 0x06004396 RID: 17302 RVA: 0x001A6F10 File Offset: 0x001A5310
		public IEnumerable<CommandData> CreateCommandData(string head)
		{
			List<CommandData> list = new List<CommandData>();
			string str = head + "DesireTable";
			using (Dictionary<int, float>.Enumerator enumerator = this.DesireTable.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					AgentData.<CreateCommandData>c__AnonStorey0 <CreateCommandData>c__AnonStorey = new AgentData.<CreateCommandData>c__AnonStorey0();
					<CreateCommandData>c__AnonStorey.item = enumerator.Current;
					<CreateCommandData>c__AnonStorey.$this = this;
					int key = <CreateCommandData>c__AnonStorey.item.Key;
					list.Add(new CommandData(CommandData.Command.FLOAT, str + string.Format("[{0}]", key), () => <CreateCommandData>c__AnonStorey.item.Value, delegate(object o)
					{
						<CreateCommandData>c__AnonStorey.$this.DesireTable[key] = Mathf.Clamp((float)o, 0f, 100f);
					}));
				}
			}
			string head2 = head + "SickState";
			this.SickState.AddList(list, head2);
			if (this.param.actor != null && this.param.actor.ChaControl != null && this.param.actor.ChaControl.fileGameInfo != null)
			{
				string str2 = head + "StatsTable";
				using (Dictionary<int, float>.Enumerator enumerator2 = this.StatsTable.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						AgentData.<CreateCommandData>c__AnonStorey2 <CreateCommandData>c__AnonStorey4 = new AgentData.<CreateCommandData>c__AnonStorey2();
						<CreateCommandData>c__AnonStorey4.item = enumerator2.Current;
						int key = <CreateCommandData>c__AnonStorey4.item.Key;
						list.Add(new CommandData(CommandData.Command.FLOAT, str2 + string.Format("[{0}]", key), delegate()
						{
							if (<CreateCommandData>c__AnonStorey4.item.Key == 7)
							{
								return this.param.actor.ChaControl.fileGameInfo.morality;
							}
							return <CreateCommandData>c__AnonStorey4.item.Value;
						}, delegate(object o)
						{
							AgentActor agentActor = this.param.actor as AgentActor;
							if (agentActor != null)
							{
								float num = (float)o;
								if (key == 3)
								{
									StatusProfile statusProfile = Singleton<Manager.Resources>.Instance.StatusProfile;
									int flavor = agentActor.ChaControl.fileGameInfo.flavorState[6];
									float num2 = AgentActor.FlavorVariation(statusProfile.DarknessPhysicalBuffMinMax, statusProfile.DarknessPhysicalBuff, flavor);
									num += num2;
								}
								agentActor.SetStatus(key, num);
							}
						}));
					}
				}
				ChaFileGameInfo fileGameInfo = this.param.actor.ChaControl.fileGameInfo;
				ChaFileGameInfo.MinMaxInfo tempBound = fileGameInfo.tempBound;
				string str3 = head + "tempBound";
				list.Add(new CommandData(CommandData.Command.FLOAT, str3 + ".low", () => tempBound.lower, null));
				list.Add(new CommandData(CommandData.Command.FLOAT, str3 + ".high", () => tempBound.upper, null));
				ChaFileGameInfo.MinMaxInfo MoodBound = fileGameInfo.moodBound;
				string str4 = head + "MoodBound";
				list.Add(new CommandData(CommandData.Command.FLOAT, str4 + ".low", () => MoodBound.lower, null));
				list.Add(new CommandData(CommandData.Command.FLOAT, str4 + ".high", () => MoodBound.upper, null));
				Dictionary<int, int> flavorState = fileGameInfo.flavorState;
				string str5 = head + "FlavorSkillTable";
				using (Dictionary<int, int>.Enumerator enumerator3 = flavorState.GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						KeyValuePair<int, int> item = enumerator3.Current;
						list.Add(new CommandData(CommandData.Command.Int, str5 + string.Format("[{0}]", item.Key), () => item.Value, delegate(object o)
						{
							this.SetFlavorSkill(item.Key, (int)o);
						}));
					}
				}
				string key7 = string.Format("{0}{1}", head, "TalkMotivation");
				list.Add(new CommandData(CommandData.Command.FLOAT, key7, () => this.TalkMotivation, delegate(object o)
				{
					float max = this.StatsTable[5];
					this.TalkMotivation = Mathf.Clamp((float)o, 0f, max);
				}));
				string key2 = string.Format("{0}InstructProb", head);
				list.Add(new CommandData(CommandData.Command.FLOAT, key2, delegate()
				{
					int num = fileGameInfo.flavorState[1];
					StatusProfile statusProfile = Singleton<Manager.Resources>.Instance.StatusProfile;
					float num2 = statusProfile.DefaultInstructionRate;
					Threshold flavorReliabilityInstructionMinMax = statusProfile.FlavorReliabilityInstructionMinMax;
					float t = Mathf.InverseLerp(flavorReliabilityInstructionMinMax.min, flavorReliabilityInstructionMinMax.max, (float)num);
					num2 += statusProfile.FlavorReliabilityInstruction.Lerp(t);
					if (fileGameInfo.normalSkill.ContainsValue(27))
					{
						num2 += statusProfile.InstructionRateDebuff;
					}
					return num2;
				}, null));
				string key3 = string.Format("{0}FollowProb", head);
				list.Add(new CommandData(CommandData.Command.FLOAT, key3, delegate()
				{
					int num = fileGameInfo.flavorState[1];
					StatusProfile statusProfile = Singleton<Manager.Resources>.Instance.StatusProfile;
					float num2 = statusProfile.DefaultFollowRate;
					float t = Mathf.InverseLerp(statusProfile.FollowReliabilityMinMax.min, statusProfile.FollowReliabilityMinMax.max, (float)num);
					num2 += statusProfile.FollowRateReliabilityBuff.Lerp(t);
					if (fileGameInfo.normalSkill.ContainsValue(8))
					{
						num2 += statusProfile.FollowRateBuff;
					}
					return num2;
				}, null));
				int TotalFlavor = fileGameInfo.totalFlavor;
				string key4 = head + "TotalFlavor";
				list.Add(new CommandData(CommandData.Command.Int, key4, () => TotalFlavor, null));
				Dictionary<int, float> desireDefVal = fileGameInfo.desireDefVal;
				string str6 = head + "DesireDef";
				using (Dictionary<int, float>.Enumerator enumerator4 = desireDefVal.GetEnumerator())
				{
					while (enumerator4.MoveNext())
					{
						KeyValuePair<int, float> item = enumerator4.Current;
						int desireKey = Desire.GetDesireKey((Desire.Type)item.Key);
						list.Add(new CommandData(CommandData.Command.FLOAT, str6 + string.Format("[{0}]", desireKey), () => item.Value, null));
					}
				}
				int Phase = fileGameInfo.phase;
				string key5 = head + "Phase";
				list.Add(new CommandData(CommandData.Command.Int, key5, () => Phase, null));
				Dictionary<int, int> NormalSkill = fileGameInfo.normalSkill;
				string str7 = head + "NormalSkill";
				using (Dictionary<int, int>.Enumerator enumerator5 = NormalSkill.GetEnumerator())
				{
					while (enumerator5.MoveNext())
					{
						KeyValuePair<int, int> item = enumerator5.Current;
						list.Add(new CommandData(CommandData.Command.Int, str7 + string.Format("[{0}]", item.Key), () => item.Value, delegate(object o)
						{
							NormalSkill[item.Key] = (int)o;
						}));
					}
				}
				Dictionary<int, int> HSkill = fileGameInfo.hSkill;
				string str8 = head + "HSkill";
				using (Dictionary<int, int>.Enumerator enumerator6 = HSkill.GetEnumerator())
				{
					while (enumerator6.MoveNext())
					{
						KeyValuePair<int, int> item = enumerator6.Current;
						list.Add(new CommandData(CommandData.Command.Int, str8 + string.Format("[{0}]", item.Key), () => item.Value, delegate(object o)
						{
							HSkill[item.Key] = (int)o;
						}));
					}
				}
				int FavoritePlace = fileGameInfo.favoritePlace;
				string key6 = head + "FavoritePlace";
				list.Add(new CommandData(CommandData.Command.Int, key6, () => FavoritePlace, null));
			}
			return list;
		}

		// Token: 0x06004397 RID: 17303 RVA: 0x001A7688 File Offset: 0x001A5A88
		public void AddFlavorSkill(int id, int value)
		{
			this.AddFlavorAdditionAmount(value);
		}

		// Token: 0x06004398 RID: 17304 RVA: 0x001A7694 File Offset: 0x001A5A94
		public void AddFlavorAdditionAmount(int value)
		{
			int num = this.FlavorAdditionAmount + value;
			this.FlavorAdditionAmount = Mathf.Max(num, 0);
			if (Singleton<Game>.IsInstance())
			{
				AIProject.SaveData.Environment environment = Singleton<Game>.Instance.Environment;
				if (environment != null)
				{
					num = environment.TotalAgentFlavorAdditionAmount + value;
					environment.TotalAgentFlavorAdditionAmount = Mathf.Clamp(num, 0, 99999);
				}
			}
		}

		// Token: 0x06004399 RID: 17305 RVA: 0x001A76F0 File Offset: 0x001A5AF0
		public void SetFlavorSkill(int id, int value)
		{
			ChaFileGameInfo fileGameInfo = this.param.actor.ChaControl.fileGameInfo;
			if (!fileGameInfo.flavorState.ContainsKey(id))
			{
				return;
			}
			int num = value - fileGameInfo.flavorState[id];
			fileGameInfo.flavorState[id] = Mathf.Clamp(value, 0, 99999);
			int a = fileGameInfo.totalFlavor + num;
			fileGameInfo.totalFlavor = Mathf.Max(a, 0);
			this.AddFlavorAdditionAmount(num);
		}

		// Token: 0x0600439A RID: 17306 RVA: 0x001A776C File Offset: 0x001A5B6C
		public HashSet<string> GetAdvEventCheck(int category)
		{
			if (this.advEventCheck == null)
			{
				this.advEventCheck = new Dictionary<int, HashSet<string>>();
			}
			HashSet<string> result;
			if (!this.advEventCheck.TryGetValue(category, out result))
			{
				result = (this.advEventCheck[category] = new HashSet<string>());
			}
			return result;
		}

		// Token: 0x0600439B RID: 17307 RVA: 0x001A77B6 File Offset: 0x001A5BB6
		public void ResetAssignedDuration()
		{
			this.AssignedDuration = TimeSpan.Zero;
		}

		// Token: 0x0600439C RID: 17308 RVA: 0x001A77C8 File Offset: 0x001A5BC8
		public void ElapseAssignedDuration(TimeSpan deltaTS)
		{
			this.AssignedDuration = this.AssignedDuration.TimeSpan + deltaTS;
		}

		// Token: 0x0600439D RID: 17309 RVA: 0x001A77F4 File Offset: 0x001A5BF4
		public void ElapseADVEventTimeCount(TimeSpan deltaTS)
		{
			if (!Game.isAdd50)
			{
				return;
			}
			this.ADVEventTimeCount = this.ADVEventTimeCount.TimeSpan + deltaTS;
		}

		// Token: 0x0600439E RID: 17310 RVA: 0x001A782B File Offset: 0x001A5C2B
		public void ResetADVEventTimeCount()
		{
			this.ADVEventTimeCount = TimeSpan.Zero;
		}

		// Token: 0x0600439F RID: 17311 RVA: 0x001A783D File Offset: 0x001A5C3D
		public void SetADVEventTimeCond(int time)
		{
			this.ADVEventTimeCond = time;
		}

		// Token: 0x060043A0 RID: 17312 RVA: 0x001A7848 File Offset: 0x001A5C48
		public bool IsOverADVEventTimeCount()
		{
			return this.ADVEventTimeCount.Days >= this.ADVEventTimeCond;
		}

		// Token: 0x060043A1 RID: 17313 RVA: 0x001A7870 File Offset: 0x001A5C70
		public bool GetAppendEventFlagCheck(int id)
		{
			if (this.AppendEventFlagCheck == null)
			{
				this.AppendEventFlagCheck = new Dictionary<int, bool>();
			}
			if (id >= 9 && id <= 17)
			{
				return false;
			}
			bool result;
			if (!this.AppendEventFlagCheck.TryGetValue(id, out result))
			{
				this.AppendEventFlagCheck[id] = false;
			}
			if (!Game.isAdd50)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060043A2 RID: 17314 RVA: 0x001A78D2 File Offset: 0x001A5CD2
		public void SetAppendEventFlagCheck(int id, bool value)
		{
			if (this.AppendEventFlagCheck == null)
			{
				this.AppendEventFlagCheck = new Dictionary<int, bool>();
			}
			if (!Game.isAdd50)
			{
				return;
			}
			this.AppendEventFlagCheck[id] = value;
		}

		// Token: 0x060043A3 RID: 17315 RVA: 0x001A7904 File Offset: 0x001A5D04
		public int GetAppendEventFlagParam(int id)
		{
			if (this.AppendEventFlagParam == null)
			{
				this.AppendEventFlagParam = new Dictionary<int, int>();
			}
			int result;
			if (!this.AppendEventFlagParam.TryGetValue(id, out result))
			{
				this.AppendEventFlagParam[id] = 0;
			}
			if (!Game.isAdd50)
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x060043A4 RID: 17316 RVA: 0x001A7954 File Offset: 0x001A5D54
		public bool TryGetAppendEventFlagParam(int id, out int result)
		{
			if (this.AppendEventFlagParam == null)
			{
				this.AppendEventFlagParam = new Dictionary<int, int>();
			}
			if (!this.AppendEventFlagParam.TryGetValue(id, out result))
			{
				return false;
			}
			if (!Game.isAdd50)
			{
				result = 0;
				return false;
			}
			return true;
		}

		// Token: 0x060043A5 RID: 17317 RVA: 0x001A7990 File Offset: 0x001A5D90
		public void SetAppendEventFlagParam(int id, int value)
		{
			if (this.AppendEventFlagParam == null)
			{
				this.AppendEventFlagParam = new Dictionary<int, int>();
			}
			this.AppendEventFlagParam[id] = value;
		}

		// Token: 0x060043A6 RID: 17318 RVA: 0x001A79B8 File Offset: 0x001A5DB8
		public void AddAppendEventFlagParam(int id, int value)
		{
			if (this.AppendEventFlagParam == null)
			{
				this.AppendEventFlagParam = new Dictionary<int, int>();
			}
			if (!this.AppendEventFlagParam.ContainsKey(id))
			{
				this.AppendEventFlagParam[id] = 0;
			}
			if (!Game.isAdd50)
			{
				return;
			}
			Dictionary<int, int> appendEventFlagParam;
			(appendEventFlagParam = this.AppendEventFlagParam)[id] = appendEventFlagParam[id] + value;
		}

		// Token: 0x04003FB5 RID: 16309
		private CharaParams _param;

		// Token: 0x04003FFF RID: 16383
		public static int[] DesireTableKeys = Enumerable.Range(0, 16).ToArray<int>();
	}
}
