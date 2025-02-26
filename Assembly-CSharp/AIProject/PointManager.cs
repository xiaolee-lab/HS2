using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AIProject.Animal;
using AIProject.SaveData;
using AIProject.Scene;
using Housing;
using Manager;
using UnityEngine;
using UnityEngine.AI;
using UnityEx;

namespace AIProject
{
	// Token: 0x02000C28 RID: 3112
	public class PointManager : MonoBehaviour
	{
		// Token: 0x170012CE RID: 4814
		// (get) Token: 0x06006013 RID: 24595 RVA: 0x0028792D File Offset: 0x00285D2D
		// (set) Token: 0x06006014 RID: 24596 RVA: 0x00287935 File Offset: 0x00285D35
		public List<ActionPoint> AppendActionPoints { get; private set; } = new List<ActionPoint>();

		// Token: 0x170012CF RID: 4815
		// (get) Token: 0x06006015 RID: 24597 RVA: 0x0028793E File Offset: 0x00285D3E
		// (set) Token: 0x06006016 RID: 24598 RVA: 0x00287946 File Offset: 0x00285D46
		public List<HPoint> AppendHPoints { get; private set; } = new List<HPoint>();

		// Token: 0x170012D0 RID: 4816
		// (get) Token: 0x06006017 RID: 24599 RVA: 0x0028794F File Offset: 0x00285D4F
		// (set) Token: 0x06006018 RID: 24600 RVA: 0x00287957 File Offset: 0x00285D57
		public Transform ActionPointRoot
		{
			get
			{
				return this._actionPointRoot;
			}
			set
			{
				this._actionPointRoot = value;
			}
		}

		// Token: 0x170012D1 RID: 4817
		// (get) Token: 0x06006019 RID: 24601 RVA: 0x00287960 File Offset: 0x00285D60
		// (set) Token: 0x0600601A RID: 24602 RVA: 0x00287968 File Offset: 0x00285D68
		public Transform BasePointRoot
		{
			get
			{
				return this._basePointRoot;
			}
			set
			{
				this._basePointRoot = value;
			}
		}

		// Token: 0x170012D2 RID: 4818
		// (get) Token: 0x0600601B RID: 24603 RVA: 0x00287971 File Offset: 0x00285D71
		// (set) Token: 0x0600601C RID: 24604 RVA: 0x00287979 File Offset: 0x00285D79
		public Transform DevicePointRoot
		{
			get
			{
				return this._devicePointRoot;
			}
			set
			{
				this._devicePointRoot = value;
			}
		}

		// Token: 0x170012D3 RID: 4819
		// (get) Token: 0x0600601D RID: 24605 RVA: 0x00287982 File Offset: 0x00285D82
		// (set) Token: 0x0600601E RID: 24606 RVA: 0x0028798A File Offset: 0x00285D8A
		public Transform HarvestPointRoot
		{
			get
			{
				return this._harvestPointRoot;
			}
			set
			{
				this._harvestPointRoot = value;
			}
		}

		// Token: 0x170012D4 RID: 4820
		// (get) Token: 0x0600601F RID: 24607 RVA: 0x00287993 File Offset: 0x00285D93
		// (set) Token: 0x06006020 RID: 24608 RVA: 0x0028799B File Offset: 0x00285D9B
		public Transform MerchantPointRoot
		{
			get
			{
				return this._merchantPointRoot;
			}
			set
			{
				this._merchantPointRoot = value;
			}
		}

		// Token: 0x170012D5 RID: 4821
		// (get) Token: 0x06006021 RID: 24609 RVA: 0x002879A4 File Offset: 0x00285DA4
		// (set) Token: 0x06006022 RID: 24610 RVA: 0x002879AC File Offset: 0x00285DAC
		public Transform EventPointRoot
		{
			get
			{
				return this._eventPointRoot;
			}
			set
			{
				this._eventPointRoot = value;
			}
		}

		// Token: 0x170012D6 RID: 4822
		// (get) Token: 0x06006023 RID: 24611 RVA: 0x002879B5 File Offset: 0x00285DB5
		// (set) Token: 0x06006024 RID: 24612 RVA: 0x002879BD File Offset: 0x00285DBD
		public Transform StoryPointRoot
		{
			get
			{
				return this._storyPointRoot;
			}
			set
			{
				this._storyPointRoot = value;
			}
		}

		// Token: 0x170012D7 RID: 4823
		// (get) Token: 0x06006025 RID: 24613 RVA: 0x002879C6 File Offset: 0x00285DC6
		// (set) Token: 0x06006026 RID: 24614 RVA: 0x002879CE File Offset: 0x00285DCE
		public Transform AnimalPointRoot
		{
			get
			{
				return this._animalPointRoot;
			}
			set
			{
				this._animalPointRoot = value;
			}
		}

		// Token: 0x170012D8 RID: 4824
		// (get) Token: 0x06006027 RID: 24615 RVA: 0x002879D7 File Offset: 0x00285DD7
		// (set) Token: 0x06006028 RID: 24616 RVA: 0x002879DF File Offset: 0x00285DDF
		public Transform LightSwitchPointRoot
		{
			get
			{
				return this._lightSwitchPointRoot;
			}
			set
			{
				this._lightSwitchPointRoot = value;
			}
		}

		// Token: 0x170012D9 RID: 4825
		// (get) Token: 0x06006029 RID: 24617 RVA: 0x002879E8 File Offset: 0x00285DE8
		// (set) Token: 0x0600602A RID: 24618 RVA: 0x002879F0 File Offset: 0x00285DF0
		public Transform ShipPointRoot
		{
			get
			{
				return this._shipPointRoot;
			}
			set
			{
				this._shipPointRoot = value;
			}
		}

		// Token: 0x170012DA RID: 4826
		// (get) Token: 0x0600602B RID: 24619 RVA: 0x002879F9 File Offset: 0x00285DF9
		// (set) Token: 0x0600602C RID: 24620 RVA: 0x00287A01 File Offset: 0x00285E01
		public bool ReadyTo { get; private set; }

		// Token: 0x0600602D RID: 24621 RVA: 0x00287A0C File Offset: 0x00285E0C
		public IEnumerator Load()
		{
			GameObject source = CommonLib.LoadAsset<GameObject>(Singleton<Manager.Resources>.Instance.DefinePack.ABPaths.MapScenePrefab, "Point", false, string.Empty);
			if (!MapScene.AssetBundlePaths.Exists((UnityEx.ValueTuple<string, string> x) => x.Item1 == Singleton<Manager.Resources>.Instance.DefinePack.ABPaths.MapScenePrefab))
			{
				MapScene.AssetBundlePaths.Add(new UnityEx.ValueTuple<string, string>(Singleton<Manager.Resources>.Instance.DefinePack.ABPaths.MapScenePrefab, string.Empty));
			}
			Dictionary<int, List<Vector3>> table = null;
			if (Singleton<Manager.Resources>.Instance.WaypointDataList.TryGetValue(Singleton<Map>.Instance.MapID, out table))
			{
				int groupID = 0;
				foreach (KeyValuePair<int, List<Vector3>> kvp in table)
				{
					int id = 0;
					foreach (Vector3 position in kvp.Value)
					{
						GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(source, this._routeParent, false);
						UnityEngine.Object @object = gameObject;
						string format = "Point {0}_{1}";
						object arg = groupID;
						int num;
						id = (num = id) + 1;
						num = num;
						@object.name = string.Format(format, arg, num.ToString("0000"));
						gameObject.transform.position = position;
						Waypoint component = gameObject.GetComponent<Waypoint>();
						component.GroupID = groupID;
					}
					groupID++;
					yield return null;
				}
			}
			this._waypoints = base.GetComponentsInChildren<Waypoint>(true);
			this._actionPoints = this._actionPointRoot.GetComponentsInChildren<ActionPoint>(true);
			this._basePoints = this._basePointRoot.GetComponentsInChildren<BasePoint>(true);
			this._devicePoints = this._devicePointRoot.GetComponentsInChildren<DevicePoint>(true);
			this.DevicePointDic.Clear();
			foreach (DevicePoint devicePoint in this._devicePoints)
			{
				this.DevicePointDic[devicePoint.ID] = devicePoint;
			}
			this._farmPoints = this._harvestPointRoot.GetComponentsInChildren<FarmPoint>(true);
			if (!this._farmPoints.IsNullOrEmpty<FarmPoint>())
			{
				foreach (FarmPoint farmPoint in this._farmPoints)
				{
					if (!(farmPoint == null))
					{
						farmPoint.SetChickenWayPoint();
					}
				}
			}
			this._shipPoints = this._shipPointRoot.GetComponentsInChildren<ShipPoint>(true);
			this._lightSwitchPoints = this._lightSwitchPointRoot.GetComponentsInChildren<LightSwitchPoint>(true);
			this._merchantPoints = this._merchantPointRoot.GetComponentsInChildren<MerchantPoint>(true);
			this._eventPoints = this._eventPointRoot.GetComponentsInChildren<EventPoint>(true);
			this._storyPoints = this._storyPointRoot.GetComponentsInChildren<StoryPoint>(true);
			this._animalPoints = this._animalPointRoot.GetComponentsInChildren<AnimalPoint>(true);
			this._animalActionPoints = this._animalPointRoot.GetComponentsInChildren<AnimalActionPoint>(true);
			if (!this._lightSwitchPoints.IsNullOrEmpty<LightSwitchPoint>())
			{
				foreach (LightSwitchPoint lightSwitchPoint in this._lightSwitchPoints)
				{
					if (!(lightSwitchPoint == null))
					{
						bool active = lightSwitchPoint.IsSwitch();
						lightSwitchPoint.Switch(active);
					}
				}
			}
			this.StoryPointTable.Clear();
			if (!this._storyPoints.IsNullOrEmpty<StoryPoint>())
			{
				foreach (StoryPoint storyPoint in this._storyPoints)
				{
					if (!(storyPoint == null))
					{
						this.StoryPointTable[storyPoint.PointID] = storyPoint;
					}
				}
			}
			yield return null;
			yield break;
		}

		// Token: 0x0600602E RID: 24622 RVA: 0x00287A28 File Offset: 0x00285E28
		public void Release()
		{
			foreach (Waypoint waypoint in this._waypoints)
			{
				UnityEngine.Object.Destroy(waypoint.gameObject);
			}
			this._waypoints = null;
			UnityEngine.Object.Destroy(this._actionPointRoot.gameObject);
			this._actionPointRoot = null;
			UnityEngine.Object.Destroy(this._basePointRoot.gameObject);
			this._basePointRoot = null;
			UnityEngine.Object.Destroy(this._devicePointRoot.gameObject);
			this._devicePointRoot = null;
			this.DevicePointDic.Clear();
			UnityEngine.Object.Destroy(this._harvestPointRoot.gameObject);
			this._harvestPointRoot = null;
			UnityEngine.Object.Destroy(this._shipPointRoot.gameObject);
			this._shipPointRoot = null;
			UnityEngine.Object.Destroy(this._lightSwitchPointRoot.gameObject);
			this._lightSwitchPointRoot = null;
			UnityEngine.Object.Destroy(this._merchantPointRoot.gameObject);
			this._merchantPointRoot = null;
			UnityEngine.Object.Destroy(this._eventPointRoot.gameObject);
			this._eventPointRoot = null;
			UnityEngine.Object.Destroy(this._storyPointRoot.gameObject);
			this._storyPointRoot = null;
			UnityEngine.Object.Destroy(this._animalPointRoot.gameObject);
			this._animalPointRoot = null;
			foreach (KeyValuePair<int, Transform> keyValuePair in this._housingWaypointParentTable)
			{
				UnityEngine.Object.Destroy(keyValuePair.Value.gameObject);
			}
			this._housingWaypointParentTable.Clear();
		}

		// Token: 0x0600602F RID: 24623 RVA: 0x00287BBC File Offset: 0x00285FBC
		private bool AnySamePoint(List<ActionPoint> list, ActionPoint point)
		{
			foreach (ActionPoint actionPoint in list)
			{
				bool flag = true;
				flag &= (actionPoint.ID == point.ID);
			}
			return false;
		}

		// Token: 0x06006030 RID: 24624 RVA: 0x00287C20 File Offset: 0x00286020
		private bool AnySamePoint(List<AnimalPoint> list, AnimalPoint point)
		{
			return false;
		}

		// Token: 0x06006031 RID: 24625 RVA: 0x00287C24 File Offset: 0x00286024
		public void AddRuntimeFarmPoints(FarmPoint[] runtimeFarmPoints)
		{
			this._runtimeFarmPointTable.Clear();
			if (runtimeFarmPoints.IsNullOrEmpty<FarmPoint>())
			{
				return;
			}
			foreach (FarmPoint farmPoint in runtimeFarmPoints)
			{
				if (!(farmPoint == null))
				{
					int registerID = farmPoint.RegisterID;
					this._runtimeFarmPointTable[registerID] = farmPoint;
				}
			}
		}

		// Token: 0x06006032 RID: 24626 RVA: 0x00287C88 File Offset: 0x00286088
		public void RemoveRuntimeFarmPoint(FarmPoint farmPoint)
		{
			if (farmPoint == null)
			{
				return;
			}
			int registerID = farmPoint.RegisterID;
			if (this._runtimeFarmPointTable.ContainsKey(registerID))
			{
				this._runtimeFarmPointTable.Remove(registerID);
			}
		}

		// Token: 0x06006033 RID: 24627 RVA: 0x00287CC8 File Offset: 0x002860C8
		public void AddPetHomePoints(PetHomePoint[] petHomePoints)
		{
			this._petHomePointTable.Clear();
			if (petHomePoints.IsNullOrEmpty<PetHomePoint>())
			{
				return;
			}
			foreach (PetHomePoint petHomePoint in petHomePoints)
			{
				if (!(petHomePoint == null))
				{
					int registerID = petHomePoint.RegisterID;
					this._petHomePointTable[registerID] = petHomePoint;
				}
			}
		}

		// Token: 0x06006034 RID: 24628 RVA: 0x00287D2C File Offset: 0x0028612C
		public void RemovePetHomePoint(PetHomePoint petHomePoint)
		{
			if (petHomePoint == null)
			{
				return;
			}
			int registerID = petHomePoint.RegisterID;
			if (this._petHomePointTable.ContainsKey(registerID))
			{
				this._petHomePointTable.Remove(registerID);
			}
		}

		// Token: 0x06006035 RID: 24629 RVA: 0x00287D6C File Offset: 0x0028616C
		public void AddJukePoints(JukePoint[] jukePoints)
		{
			foreach (KeyValuePair<int, List<JukePoint>> keyValuePair in this._jukePointTable)
			{
				if (!keyValuePair.Value.IsNullOrEmpty<JukePoint>())
				{
					keyValuePair.Value.Clear();
				}
			}
			if (jukePoints.IsNullOrEmpty<JukePoint>())
			{
				return;
			}
			foreach (JukePoint jukePoint in jukePoints)
			{
				if (!(jukePoint == null))
				{
					int areaID = jukePoint.AreaID;
					List<JukePoint> list;
					if (!this._jukePointTable.TryGetValue(areaID, out list) || list == null)
					{
						list = (this._jukePointTable[areaID] = new List<JukePoint>());
					}
					list.Add(jukePoint);
				}
			}
		}

		// Token: 0x06006036 RID: 24630 RVA: 0x00287E60 File Offset: 0x00286260
		public void RemoveJukePoint(JukePoint jukePoint)
		{
			if (jukePoint == null)
			{
				return;
			}
			int areaID = jukePoint.AreaID;
			List<JukePoint> list;
			if (this._jukePointTable.TryGetValue(areaID, out list) && !list.IsNullOrEmpty<JukePoint>())
			{
				list.Remove(jukePoint);
			}
		}

		// Token: 0x170012DB RID: 4827
		// (get) Token: 0x06006037 RID: 24631 RVA: 0x00287EA7 File Offset: 0x002862A7
		public Waypoint[] Waypoints
		{
			[CompilerGenerated]
			get
			{
				return this._waypoints;
			}
		}

		// Token: 0x170012DC RID: 4828
		// (get) Token: 0x06006038 RID: 24632 RVA: 0x00287EAF File Offset: 0x002862AF
		public ActionPoint[] ActionPoints
		{
			[CompilerGenerated]
			get
			{
				return this._actionPoints;
			}
		}

		// Token: 0x170012DD RID: 4829
		// (get) Token: 0x06006039 RID: 24633 RVA: 0x00287EB7 File Offset: 0x002862B7
		public BasePoint[] BasePoints
		{
			[CompilerGenerated]
			get
			{
				return this._basePoints;
			}
		}

		// Token: 0x170012DE RID: 4830
		// (get) Token: 0x0600603A RID: 24634 RVA: 0x00287EBF File Offset: 0x002862BF
		public DevicePoint[] DevicePoints
		{
			[CompilerGenerated]
			get
			{
				return this._devicePoints;
			}
		}

		// Token: 0x170012DF RID: 4831
		// (get) Token: 0x0600603B RID: 24635 RVA: 0x00287EC7 File Offset: 0x002862C7
		public FarmPoint[] FarmPoints
		{
			[CompilerGenerated]
			get
			{
				return this._farmPoints;
			}
		}

		// Token: 0x170012E0 RID: 4832
		// (get) Token: 0x0600603C RID: 24636 RVA: 0x00287ECF File Offset: 0x002862CF
		public ShipPoint[] ShipPoints
		{
			[CompilerGenerated]
			get
			{
				return this._shipPoints;
			}
		}

		// Token: 0x170012E1 RID: 4833
		// (get) Token: 0x0600603D RID: 24637 RVA: 0x00287ED7 File Offset: 0x002862D7
		public LightSwitchPoint[] LightSwitchPoints
		{
			[CompilerGenerated]
			get
			{
				return this._lightSwitchPoints;
			}
		}

		// Token: 0x170012E2 RID: 4834
		// (get) Token: 0x0600603E RID: 24638 RVA: 0x00287EDF File Offset: 0x002862DF
		// (set) Token: 0x0600603F RID: 24639 RVA: 0x00287EE7 File Offset: 0x002862E7
		public Dictionary<int, DevicePoint> DevicePointDic { get; set; } = new Dictionary<int, DevicePoint>();

		// Token: 0x170012E3 RID: 4835
		// (get) Token: 0x06006040 RID: 24640 RVA: 0x00287EF0 File Offset: 0x002862F0
		public AnimalPoint[] AnimalPoints
		{
			[CompilerGenerated]
			get
			{
				return this._animalPoints;
			}
		}

		// Token: 0x170012E4 RID: 4836
		// (get) Token: 0x06006041 RID: 24641 RVA: 0x00287EF8 File Offset: 0x002862F8
		public AnimalActionPoint[] AnimalActionPoints
		{
			[CompilerGenerated]
			get
			{
				return this._animalActionPoints;
			}
		}

		// Token: 0x170012E5 RID: 4837
		// (get) Token: 0x06006042 RID: 24642 RVA: 0x00287F00 File Offset: 0x00286300
		public MerchantPoint[] MerchantPoints
		{
			[CompilerGenerated]
			get
			{
				return this._merchantPoints;
			}
		}

		// Token: 0x170012E6 RID: 4838
		// (get) Token: 0x06006043 RID: 24643 RVA: 0x00287F08 File Offset: 0x00286308
		public EventPoint[] EventPoints
		{
			[CompilerGenerated]
			get
			{
				return this._eventPoints;
			}
		}

		// Token: 0x170012E7 RID: 4839
		// (get) Token: 0x06006044 RID: 24644 RVA: 0x00287F10 File Offset: 0x00286310
		public StoryPoint[] StoryPoints
		{
			[CompilerGenerated]
			get
			{
				return this._storyPoints;
			}
		}

		// Token: 0x170012E8 RID: 4840
		// (get) Token: 0x06006045 RID: 24645 RVA: 0x00287F18 File Offset: 0x00286318
		// (set) Token: 0x06006046 RID: 24646 RVA: 0x00287F20 File Offset: 0x00286320
		public Dictionary<int, StoryPoint> StoryPointTable { get; private set; } = new Dictionary<int, StoryPoint>();

		// Token: 0x170012E9 RID: 4841
		// (get) Token: 0x06006047 RID: 24647 RVA: 0x00287F29 File Offset: 0x00286329
		public IReadOnlyDictionary<int, FarmPoint> RuntimeFarmPointTable
		{
			[CompilerGenerated]
			get
			{
				return this._runtimeFarmPointTable;
			}
		}

		// Token: 0x170012EA RID: 4842
		// (get) Token: 0x06006048 RID: 24648 RVA: 0x00287F31 File Offset: 0x00286331
		public IReadOnlyDictionary<int, PetHomePoint> PetHomePointTable
		{
			[CompilerGenerated]
			get
			{
				return this._petHomePointTable;
			}
		}

		// Token: 0x170012EB RID: 4843
		// (get) Token: 0x06006049 RID: 24649 RVA: 0x00287F39 File Offset: 0x00286339
		public IReadOnlyDictionary<int, List<JukePoint>> JukePointTable
		{
			[CompilerGenerated]
			get
			{
				return this._jukePointTable;
			}
		}

		// Token: 0x170012EC RID: 4844
		// (get) Token: 0x0600604A RID: 24650 RVA: 0x00287F41 File Offset: 0x00286341
		public Dictionary<int, List<Waypoint>> HousingWaypointTable
		{
			[CompilerGenerated]
			get
			{
				return this._housingWaypointTable;
			}
		}

		// Token: 0x0600604B RID: 24651 RVA: 0x00287F4C File Offset: 0x0028634C
		public void CreateHousingWaypoint(int housingID)
		{
			HousingData housingData;
			if (Singleton<Game>.IsInstance())
			{
				WorldData worldData = Singleton<Game>.Instance.WorldData;
				housingData = ((worldData != null) ? worldData.HousingData : null);
			}
			else
			{
				housingData = null;
			}
			HousingData housingData2 = housingData;
			Dictionary<int, CraftInfo> dictionary = (housingData2 != null) ? housingData2.CraftInfos : null;
			if (dictionary.IsNullOrEmpty<int, CraftInfo>())
			{
				return;
			}
			CraftInfo craftInfo;
			if (!dictionary.TryGetValue(housingID, out craftInfo) || craftInfo == null)
			{
				return;
			}
			GameObject objRoot = craftInfo.ObjRoot ?? ((!Singleton<Housing>.IsInstance()) ? null : Singleton<Housing>.Instance.GetRoot(housingID));
			this.CreateHousingWaypoint(housingID, objRoot, craftInfo.LimitSize);
		}

		// Token: 0x0600604C RID: 24652 RVA: 0x00287FEC File Offset: 0x002863EC
		public void CreateHousingWaypoint()
		{
			if (!Singleton<Housing>.IsInstance())
			{
				return;
			}
			HousingData housingData;
			if (Singleton<Game>.IsInstance())
			{
				WorldData worldData = Singleton<Game>.Instance.WorldData;
				housingData = ((worldData != null) ? worldData.HousingData : null);
			}
			else
			{
				housingData = null;
			}
			HousingData housingData2 = housingData;
			Dictionary<int, CraftInfo> dictionary = (housingData2 != null) ? housingData2.CraftInfos : null;
			if (dictionary == null)
			{
				return;
			}
			foreach (KeyValuePair<int, CraftInfo> keyValuePair in dictionary)
			{
				CraftInfo value = keyValuePair.Value;
				GameObject objRoot = value.ObjRoot ?? Singleton<Housing>.Instance.GetRoot(keyValuePair.Key);
				if (Singleton<Map>.IsInstance() && Singleton<Map>.Instance.HousingPointTable.ContainsKey(keyValuePair.Key))
				{
					this.CreateHousingWaypoint(keyValuePair.Key, objRoot, value.LimitSize);
				}
			}
		}

		// Token: 0x0600604D RID: 24653 RVA: 0x002880FC File Offset: 0x002864FC
		private void CreateHousingWaypoint(int housingID, GameObject objRoot, Vector3 areaSize)
		{
			if (objRoot == null)
			{
				return;
			}
			if (!Singleton<Manager.Resources>.IsInstance())
			{
				return;
			}
			LocomotionProfile locomotionProfile = Singleton<Manager.Resources>.Instance.LocomotionProfile;
			if (locomotionProfile == null)
			{
				return;
			}
			List<UnityEx.ValueTuple<Vector3, Waypoint>> list;
			if (!this._housingWaypointCacheTable.TryGetValue(housingID, out list) || list == null)
			{
				list = (this._housingWaypointCacheTable[housingID] = new List<UnityEx.ValueTuple<Vector3, Waypoint>>());
				Transform transform = objRoot.transform;
				if (transform == null)
				{
					return;
				}
				float installationDistance = locomotionProfile.HousingWaypointSetting.InstallationDistance;
				float installationHeight = locomotionProfile.HousingWaypointSetting.InstallationHeight;
				SpiralPoint spiralPoint = new SpiralPoint(999);
				int num = 0;
				while (installationHeight * (float)num < areaSize.y)
				{
					float y = installationHeight * (float)num;
					spiralPoint.Clear();
					spiralPoint.Limit = 999;
					while (!spiralPoint.End)
					{
						Vector3 vector = new Vector3((float)spiralPoint.Current.x, 0f, (float)spiralPoint.Current.y) * installationDistance;
						vector.y = y;
						if (areaSize.y <= vector.y)
						{
							break;
						}
						if (vector.x <= -areaSize.x / 2f || areaSize.x / 2f <= vector.x)
						{
							break;
						}
						if (vector.z <= -areaSize.z / 2f || areaSize.z / 2f <= vector.z)
						{
							break;
						}
						vector = transform.position + transform.rotation * vector;
						list.Add(new UnityEx.ValueTuple<Vector3, Waypoint>(vector, null));
						spiralPoint.Next();
					}
					num++;
				}
			}
			this.RefreshWaypoints(housingID, list);
		}

		// Token: 0x0600604E RID: 24654 RVA: 0x002882E8 File Offset: 0x002866E8
		protected void RefreshWaypoints(int housingID, List<UnityEx.ValueTuple<Vector3, Waypoint>> waypointList)
		{
			if (!Singleton<Manager.Resources>.IsInstance())
			{
				return;
			}
			if (waypointList.IsNullOrEmpty<UnityEx.ValueTuple<Vector3, Waypoint>>())
			{
				return;
			}
			List<Waypoint> list = null;
			if (!this._housingWaypointTable.TryGetValue(housingID, out list) || list == null)
			{
				list = (this._housingWaypointTable[housingID] = new List<Waypoint>());
			}
			else
			{
				list.Clear();
			}
			float closestEdgeDistance = Singleton<Manager.Resources>.Instance.LocomotionProfile.HousingWaypointSetting.ClosestEdgeDistance;
			float sampleDistance = Singleton<Manager.Resources>.Instance.LocomotionProfile.HousingWaypointSetting.SampleDistance;
			for (int i = 0; i < waypointList.Count; i++)
			{
				UnityEx.ValueTuple<Vector3, Waypoint> value = waypointList[i];
				Vector3 vector = value.Item1;
				bool flag = true;
				NavMeshHit navMeshHit;
				flag &= NavMesh.SamplePosition(vector, out navMeshHit, sampleDistance, -1);
				if (flag)
				{
					vector = navMeshHit.position;
				}
				if (flag)
				{
					flag &= NavMesh.FindClosestEdge(vector, out navMeshHit, -1);
				}
				if (flag)
				{
					flag &= (closestEdgeDistance <= navMeshHit.distance);
				}
				Waypoint waypoint = value.Item2;
				if (waypoint != null)
				{
					waypoint.transform.position = vector;
					if (waypoint.gameObject.activeSelf != flag)
					{
						waypoint.gameObject.SetActive(flag);
					}
				}
				else if (flag)
				{
					waypoint = this.CreateWaypoint(housingID, i);
					waypoint.transform.position = vector;
					value.Item2 = waypoint;
					waypointList[i] = value;
					if (waypoint.gameObject.activeSelf != flag)
					{
						waypoint.gameObject.SetActive(flag);
					}
				}
				if (flag)
				{
					list.Add(waypoint);
				}
			}
			if (!Singleton<Map>.IsInstance() || list.IsNullOrEmpty<Waypoint>())
			{
				return;
			}
			Dictionary<int, Chunk> chunkTable = Singleton<Map>.Instance.ChunkTable;
			if (chunkTable.IsNullOrEmpty<int, Chunk>())
			{
				return;
			}
			LayerMask areaDetectionLayer = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.AreaDetectionLayer;
			LayerMask roofLayer = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.RoofLayer;
			for (int j = 0; j < list.Count; j++)
			{
				bool flag2 = false;
				Waypoint waypoint2 = list[j];
				foreach (KeyValuePair<int, Chunk> keyValuePair in chunkTable)
				{
					Chunk value2 = keyValuePair.Value;
					flag2 = (value2 != null && value2.CheckPointOnTheArea<Waypoint>(waypoint2, areaDetectionLayer, roofLayer, 1f));
					if (flag2)
					{
						break;
					}
				}
				if (!flag2)
				{
					waypoint2.gameObject.SetActive(false);
					list.RemoveAt(j);
					j--;
				}
			}
		}

		// Token: 0x0600604F RID: 24655 RVA: 0x002885C8 File Offset: 0x002869C8
		private Waypoint CreateWaypoint(int housingID, int pointID)
		{
			Transform transform = null;
			if (!this._housingWaypointParentTable.TryGetValue(housingID, out transform) || transform == null)
			{
				transform = (this._housingWaypointParentTable[housingID] = new GameObject(string.Format("Housing Area Waypoint [{0:00}]", housingID)).transform);
				transform.SetParent(base.transform, false);
			}
			Transform transform2 = new GameObject(string.Format("housing{0:00}_waypoint{1:0000}", housingID, pointID)).transform;
			transform2.SetParent(transform, false);
			Waypoint orAddComponent = transform2.GetOrAddComponent<Waypoint>();
			orAddComponent.GroupID = housingID;
			orAddComponent.ID = pointID;
			orAddComponent.Affiliation = Waypoint.AffiliationType.Housing;
			return orAddComponent;
		}

		// Token: 0x06006050 RID: 24656 RVA: 0x00288674 File Offset: 0x00286A74
		public void ClearHousingWaypoint()
		{
			foreach (KeyValuePair<int, Transform> keyValuePair in this._housingWaypointParentTable)
			{
				Transform value = keyValuePair.Value;
				if (value != null && value.gameObject != null)
				{
					UnityEngine.Object.Destroy(value.gameObject);
				}
			}
			this._housingWaypointParentTable.Clear();
			foreach (KeyValuePair<int, List<Waypoint>> keyValuePair2 in this._housingWaypointTable)
			{
				List<Waypoint> value2 = keyValuePair2.Value;
				if (value2 != null)
				{
					value2.Clear();
				}
			}
			this._housingWaypointTable.Clear();
			foreach (KeyValuePair<int, List<UnityEx.ValueTuple<Vector3, Waypoint>>> keyValuePair3 in this._housingWaypointCacheTable)
			{
				List<UnityEx.ValueTuple<Vector3, Waypoint>> value3 = keyValuePair3.Value;
				if (value3 != null)
				{
					value3.Clear();
				}
			}
			this._housingWaypointCacheTable.Clear();
		}

		// Token: 0x0400556D RID: 21869
		private Waypoint[] _waypoints;

		// Token: 0x0400556E RID: 21870
		private ActionPoint[] _actionPoints;

		// Token: 0x0400556F RID: 21871
		private BasePoint[] _basePoints;

		// Token: 0x04005570 RID: 21872
		private DevicePoint[] _devicePoints;

		// Token: 0x04005571 RID: 21873
		private FarmPoint[] _farmPoints;

		// Token: 0x04005572 RID: 21874
		private ShipPoint[] _shipPoints;

		// Token: 0x04005573 RID: 21875
		private LightSwitchPoint[] _lightSwitchPoints;

		// Token: 0x04005574 RID: 21876
		private MerchantPoint[] _merchantPoints;

		// Token: 0x04005575 RID: 21877
		private EventPoint[] _eventPoints;

		// Token: 0x04005576 RID: 21878
		private StoryPoint[] _storyPoints;

		// Token: 0x04005577 RID: 21879
		private AnimalPoint[] _animalPoints;

		// Token: 0x04005578 RID: 21880
		private AnimalActionPoint[] _animalActionPoints;

		// Token: 0x0400557A RID: 21882
		private Dictionary<int, FarmPoint> _runtimeFarmPointTable = new Dictionary<int, FarmPoint>();

		// Token: 0x0400557B RID: 21883
		private Dictionary<int, PetHomePoint> _petHomePointTable = new Dictionary<int, PetHomePoint>();

		// Token: 0x0400557C RID: 21884
		private Dictionary<int, List<JukePoint>> _jukePointTable = new Dictionary<int, List<JukePoint>>();

		// Token: 0x0400557E RID: 21886
		[SerializeField]
		private Transform _routeParent;

		// Token: 0x0400557F RID: 21887
		[SerializeField]
		private Transform _actionPointRoot;

		// Token: 0x04005580 RID: 21888
		[SerializeField]
		private Transform _basePointRoot;

		// Token: 0x04005581 RID: 21889
		[SerializeField]
		private Transform _devicePointRoot;

		// Token: 0x04005582 RID: 21890
		[SerializeField]
		private Transform _harvestPointRoot;

		// Token: 0x04005583 RID: 21891
		private Transform _merchantPointRoot;

		// Token: 0x04005584 RID: 21892
		private Transform _eventPointRoot;

		// Token: 0x04005585 RID: 21893
		private Transform _storyPointRoot;

		// Token: 0x04005586 RID: 21894
		private Transform _animalPointRoot;

		// Token: 0x04005587 RID: 21895
		private Transform _lightSwitchPointRoot;

		// Token: 0x04005588 RID: 21896
		private Transform _shipPointRoot;

		// Token: 0x0400558C RID: 21900
		private Dictionary<int, List<UnityEx.ValueTuple<Vector3, Waypoint>>> _housingWaypointCacheTable = new Dictionary<int, List<UnityEx.ValueTuple<Vector3, Waypoint>>>();

		// Token: 0x0400558D RID: 21901
		private Dictionary<int, List<Waypoint>> _housingWaypointTable = new Dictionary<int, List<Waypoint>>();

		// Token: 0x0400558E RID: 21902
		private Dictionary<int, Transform> _housingWaypointParentTable = new Dictionary<int, Transform>();
	}
}
