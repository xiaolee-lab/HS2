using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using AIProject.Animal;
using AIProject.Player;
using AIProject.SaveData;
using Housing;
using IllusionUtility.GetUtility;
using Manager;
using UnityEngine;
using UnityEngine.AI;

namespace AIProject
{
	// Token: 0x02000C08 RID: 3080
	public class FarmPoint : Point, ICommandable
	{
		// Token: 0x1700125C RID: 4700
		// (get) Token: 0x06005EE9 RID: 24297 RVA: 0x00282838 File Offset: 0x00280C38
		// (set) Token: 0x06005EEA RID: 24298 RVA: 0x00282840 File Offset: 0x00280C40
		public override int RegisterID
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		// Token: 0x1700125D RID: 4701
		// (get) Token: 0x06005EEB RID: 24299 RVA: 0x00282849 File Offset: 0x00280C49
		public FarmSection[] HarvestSections
		{
			[CompilerGenerated]
			get
			{
				return this._harvestSections;
			}
		}

		// Token: 0x1700125E RID: 4702
		// (get) Token: 0x06005EEC RID: 24300 RVA: 0x00282851 File Offset: 0x00280C51
		public float Radius
		{
			[CompilerGenerated]
			get
			{
				return this._radius;
			}
		}

		// Token: 0x1700125F RID: 4703
		// (get) Token: 0x06005EED RID: 24301 RVA: 0x00282859 File Offset: 0x00280C59
		public FarmPoint.FarmKind Kind
		{
			[CompilerGenerated]
			get
			{
				return this._farmKind;
			}
		}

		// Token: 0x17001260 RID: 4704
		// (get) Token: 0x06005EEE RID: 24302 RVA: 0x00282861 File Offset: 0x00280C61
		// (set) Token: 0x06005EEF RID: 24303 RVA: 0x00282869 File Offset: 0x00280C69
		public int AreaIDOnHousingArea { get; set; } = -1;

		// Token: 0x17001261 RID: 4705
		// (get) Token: 0x06005EF0 RID: 24304 RVA: 0x00282872 File Offset: 0x00280C72
		public int InstanceID
		{
			get
			{
				if (this._hashCode == null)
				{
					this._hashCode = new int?(base.GetInstanceID());
				}
				return this._hashCode.Value;
			}
		}

		// Token: 0x17001262 RID: 4706
		// (get) Token: 0x06005EF1 RID: 24305 RVA: 0x002828A0 File Offset: 0x00280CA0
		// (set) Token: 0x06005EF2 RID: 24306 RVA: 0x002828A8 File Offset: 0x00280CA8
		public bool IsImpossible { get; private set; }

		// Token: 0x06005EF3 RID: 24307 RVA: 0x002828B1 File Offset: 0x00280CB1
		public bool SetImpossible(bool value, Actor actor)
		{
			return true;
		}

		// Token: 0x06005EF4 RID: 24308 RVA: 0x002828B4 File Offset: 0x00280CB4
		public bool Entered(Vector3 basePosition, float distance, float radiusA, float radiusB, float angle, Vector3 forward)
		{
			if (this.TutorialHideMode())
			{
				return false;
			}
			if (distance > radiusA)
			{
				return false;
			}
			Vector3 commandCenter = this.CommandCenter;
			commandCenter.y = 0f;
			float num = angle / 2f;
			float num2 = Vector3.Angle(commandCenter - basePosition, forward);
			return num2 <= num;
		}

		// Token: 0x06005EF5 RID: 24309 RVA: 0x0028290C File Offset: 0x00280D0C
		public bool IsReachable(NavMeshAgent nmAgent, float radiusA, float radiusB)
		{
			if (this._pathForCalc == null)
			{
				this._pathForCalc = new NavMeshPath();
			}
			bool result = true;
			if (nmAgent.isActiveAndEnabled)
			{
				bool flag = false;
				foreach (Transform transform in this.NavMeshPoints)
				{
					nmAgent.CalculatePath(this.Position, this._pathForCalc);
					if (this._pathForCalc.status == NavMeshPathStatus.PathComplete)
					{
						float num = 0f;
						Vector3[] corners = this._pathForCalc.corners;
						float num2 = (this.CommandType != CommandType.Forward) ? radiusB : radiusA;
						num2 += this._radius;
						for (int i = 0; i < corners.Length - 1; i++)
						{
							float num3 = Vector3.Distance(corners[i], corners[i + 1]);
							num += num3;
						}
						if (num < num2)
						{
							flag = true;
							break;
						}
						if (!flag)
						{
							result = false;
						}
					}
				}
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x17001263 RID: 4707
		// (get) Token: 0x06005EF6 RID: 24310 RVA: 0x00282A48 File Offset: 0x00280E48
		public bool IsNeutralCommand
		{
			get
			{
				return !this.TutorialHideMode();
			}
		}

		// Token: 0x06005EF7 RID: 24311 RVA: 0x00282A58 File Offset: 0x00280E58
		public bool TutorialHideMode()
		{
			return Map.TutorialMode;
		}

		// Token: 0x17001264 RID: 4708
		// (get) Token: 0x06005EF8 RID: 24312 RVA: 0x00282A67 File Offset: 0x00280E67
		public Vector3 Position
		{
			[CompilerGenerated]
			get
			{
				return base.transform.position;
			}
		}

		// Token: 0x17001265 RID: 4709
		// (get) Token: 0x06005EF9 RID: 24313 RVA: 0x00282A74 File Offset: 0x00280E74
		public Vector3 CommandCenter
		{
			get
			{
				if (this._commandBasePoint != null)
				{
					return this._commandBasePoint.position;
				}
				return base.transform.position;
			}
		}

		// Token: 0x17001266 RID: 4710
		// (get) Token: 0x06005EFA RID: 24314 RVA: 0x00282A9E File Offset: 0x00280E9E
		// (set) Token: 0x06005EFB RID: 24315 RVA: 0x00282AA6 File Offset: 0x00280EA6
		public List<Transform> NavMeshPoints { get; set; } = new List<Transform>();

		// Token: 0x17001267 RID: 4711
		// (get) Token: 0x06005EFC RID: 24316 RVA: 0x00282AB0 File Offset: 0x00280EB0
		public CommandLabel.CommandInfo[] Labels
		{
			get
			{
				PlayerActor playerActor = (!Singleton<Map>.IsInstance()) ? null : Singleton<Map>.Instance.Player;
				if (playerActor != null && playerActor.PlayerController.State is Onbu)
				{
					return null;
				}
				return this._labels;
			}
		}

		// Token: 0x17001268 RID: 4712
		// (get) Token: 0x06005EFD RID: 24317 RVA: 0x00282B01 File Offset: 0x00280F01
		// (set) Token: 0x06005EFE RID: 24318 RVA: 0x00282B09 File Offset: 0x00280F09
		public CommandLabel.CommandInfo[] DateLabels { get; private set; }

		// Token: 0x17001269 RID: 4713
		// (get) Token: 0x06005EFF RID: 24319 RVA: 0x00282B12 File Offset: 0x00280F12
		public ObjectLayer Layer { get; } = 2;

		// Token: 0x1700126A RID: 4714
		// (get) Token: 0x06005F00 RID: 24320 RVA: 0x00282B1A File Offset: 0x00280F1A
		public CommandType CommandType { get; }

		// Token: 0x06005F01 RID: 24321 RVA: 0x00282B24 File Offset: 0x00280F24
		private void Awake()
		{
			this._harvestSections = base.GetComponentsInChildren<FarmSection>();
			int num = 0;
			foreach (FarmSection farmSection in this._harvestSections)
			{
				farmSection.HarvestID = this._id;
				farmSection.SectionID = num++;
			}
		}

		// Token: 0x06005F02 RID: 24322 RVA: 0x00282B78 File Offset: 0x00280F78
		protected override void Start()
		{
			CommonDefine.CommonIconGroup icon = Singleton<Manager.Resources>.Instance.CommonDefine.Icon;
			DefinePack.MapGroup mapDefines = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines;
			if (this._commandBasePoint == null)
			{
				GameObject gameObject = base.transform.FindLoop(Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.CommandTargetName);
				this._commandBasePoint = (((gameObject != null) ? gameObject.transform : null) ?? base.transform);
			}
			base.Start();
			this.NavMeshPoints.Add(base.transform);
			List<GameObject> list = ListPool<GameObject>.Get();
			base.transform.FindLoopPrefix(list, mapDefines.NavMeshTargetName);
			if (!list.IsNullOrEmpty<GameObject>())
			{
				foreach (GameObject gameObject2 in list)
				{
					this.NavMeshPoints.Add(gameObject2.transform);
				}
			}
			ListPool<GameObject>.Release(list);
			int farmIconID = Singleton<Manager.Resources>.Instance.CommonDefine.Icon.FarmIconID;
			Sprite icon2;
			Singleton<Manager.Resources>.Instance.itemIconTables.ActionIconTable.TryGetValue(farmIconID, out icon2);
			int chickenCoopIconID = Singleton<Manager.Resources>.Instance.CommonDefine.Icon.ChickenCoopIconID;
			Sprite icon3;
			Singleton<Manager.Resources>.Instance.itemIconTables.ActionIconTable.TryGetValue(chickenCoopIconID, out icon3);
			int wellIconID = Singleton<Manager.Resources>.Instance.CommonDefine.Icon.WellIconID;
			Sprite sprite;
			Singleton<Manager.Resources>.Instance.itemIconTables.ActionIconTable.TryGetValue(wellIconID, out sprite);
			GameObject gameObject3 = base.transform.FindLoop(mapDefines.FarmPointLabelTargetName);
			Transform transform = ((gameObject3 != null) ? gameObject3.transform : null) ?? base.transform;
			if (this._farmKind == FarmPoint.FarmKind.Plant)
			{
				this._labels = new CommandLabel.CommandInfo[]
				{
					new CommandLabel.CommandInfo
					{
						Text = "畑",
						Icon = icon2,
						IsHold = true,
						TargetSpriteInfo = icon.ActionSpriteInfo,
						Transform = transform,
						Condition = ((PlayerActor x) => this._farmKind == FarmPoint.FarmKind.Plant),
						Event = delegate
						{
							MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);
							List<AIProject.SaveData.Environment.PlantInfo> list2;
							if (!Singleton<Game>.Instance.Environment.FarmlandTable.TryGetValue(this._id, out list2))
							{
								List<AIProject.SaveData.Environment.PlantInfo> list3 = new List<AIProject.SaveData.Environment.PlantInfo>();
								Singleton<Game>.Instance.Environment.FarmlandTable[this._id] = list3;
								list2 = list3;
								foreach (FarmSection farmSection in this._harvestSections)
								{
									list2.Add(null);
								}
							}
							MapUIContainer.FarmlandUI.currentPlant = list2;
							MapUIContainer.SetActiveFarmlandUI(true);
							Singleton<Map>.Instance.Player.Controller.ChangeState("Harvest");
						}
					}
				};
			}
			else if (this._farmKind == FarmPoint.FarmKind.ChickenCoop)
			{
				this._labels = new CommandLabel.CommandInfo[]
				{
					new CommandLabel.CommandInfo
					{
						Text = "鶏小屋",
						Icon = icon3,
						IsHold = true,
						TargetSpriteInfo = icon.ActionSpriteInfo,
						Transform = transform,
						Condition = ((PlayerActor x) => this._farmKind == FarmPoint.FarmKind.ChickenCoop),
						Event = delegate
						{
							PlayerActor playerActor = (!Singleton<Map>.IsInstance()) ? null : Singleton<Map>.Instance.Player;
							if (playerActor != null)
							{
								playerActor.CurrentFarmPoint = this;
								playerActor.PlayerController.ChangeState("ChickenCoopMenu");
							}
						}
					}
				};
			}
			else if (this._farmKind == FarmPoint.FarmKind.Well)
			{
			}
		}

		// Token: 0x06005F03 RID: 24323 RVA: 0x00282E5C File Offset: 0x0028125C
		private int GetHousingAreaID()
		{
			if (!Singleton<Manager.Resources>.IsInstance())
			{
				return -1;
			}
			ItemComponent componentInParent = base.GetComponentInParent<ItemComponent>();
			if (componentInParent == null)
			{
				return -1;
			}
			Vector3 origin = componentInParent.position + Vector3.up * 5f;
			LayerMask areaDetectionLayer = Singleton<Manager.Resources>.Instance.DefinePack.MapDefines.AreaDetectionLayer;
			int num = Physics.RaycastNonAlloc(origin, Vector3.down, Point._raycastHits, 1000f, areaDetectionLayer);
			num = Mathf.Min(num, Point._raycastHits.Length);
			if (num <= 0)
			{
				return -1;
			}
			for (int i = 0; i < num; i++)
			{
				RaycastHit raycastHit = Point._raycastHits[i];
				if (!(raycastHit.transform == null))
				{
					MapArea componentInParent2 = raycastHit.transform.GetComponentInParent<MapArea>();
					if (!(componentInParent2 == null))
					{
						return componentInParent2.AreaID;
					}
				}
			}
			return -1;
		}

		// Token: 0x1700126B RID: 4715
		// (get) Token: 0x06005F04 RID: 24324 RVA: 0x00282F56 File Offset: 0x00281356
		public IReadOnlyList<Vector3> ChickenWaypointPositionList
		{
			[CompilerGenerated]
			get
			{
				return this._chickenWaypointPositionList;
			}
		}

		// Token: 0x1700126C RID: 4716
		// (get) Token: 0x06005F05 RID: 24325 RVA: 0x00282F5E File Offset: 0x0028135E
		public IReadOnlyList<Waypoint> ChickenWaypointList
		{
			[CompilerGenerated]
			get
			{
				return this._chickenWaypointList;
			}
		}

		// Token: 0x1700126D RID: 4717
		// (get) Token: 0x06005F06 RID: 24326 RVA: 0x00282F68 File Offset: 0x00281368
		public Transform AnimalRoot
		{
			get
			{
				if (this._animalRoot == null)
				{
					this._animalRoot = new GameObject("Animal Root").transform;
					ItemComponent componentInParent = base.GetComponentInParent<ItemComponent>();
					Transform parent = (!(componentInParent != null) || !componentInParent.transform) ? base.transform : componentInParent.transform;
					this._animalRoot.SetParent(parent, false);
				}
				return this._animalRoot;
			}
		}

		// Token: 0x06005F07 RID: 24327 RVA: 0x00282FE4 File Offset: 0x002813E4
		public void SetChickenWayPoint()
		{
			if (this._farmKind != FarmPoint.FarmKind.ChickenCoop)
			{
				return;
			}
			this.AreaIDOnHousingArea = this.GetHousingAreaID();
			if (!Singleton<Manager.Resources>.IsInstance())
			{
				return;
			}
			ItemComponent componentInParent = base.GetComponentInParent<ItemComponent>();
			Collider[] array = (componentInParent != null) ? componentInParent.GetComponentsInChildren<Collider>() : null;
			if (array.IsNullOrEmpty<Collider>())
			{
				return;
			}
			AnimalDefinePack.ChickenCoopWaypointSettings chickenCoopWaypointSetting = Singleton<Manager.Resources>.Instance.AnimalDefinePack.ChickenCoopWaypointSetting;
			Collider collider = null;
			LayerMask layer = chickenCoopWaypointSetting.Layer;
			string tagName = chickenCoopWaypointSetting.TagName;
			foreach (Collider collider2 in array)
			{
				if (!(collider2 == null) && !(collider2.gameObject == null))
				{
					int num = 1 << collider2.gameObject.layer;
					if ((num & layer) != 0 && collider2.gameObject.tag == tagName)
					{
						collider = collider2;
						break;
					}
				}
			}
			if (collider == null)
			{
				return;
			}
			Transform transform = componentInParent.transform;
			Vector3 position = collider.transform.position;
			Quaternion rotation = collider.transform.rotation;
			if (this._chickenWaypointPositionList == null)
			{
				this._chickenWaypointPositionList = new List<Vector3>();
			}
			else
			{
				this._chickenWaypointPositionList.Clear();
			}
			if (this._chickenWaypointRoot == null)
			{
				this._chickenWaypointRoot = new GameObject("Chicken Waypoint Root").transform;
				this._chickenWaypointRoot.SetParent(transform, false);
				Transform chickenWaypointRoot = this._chickenWaypointRoot;
				Vector3 zero = Vector3.zero;
				this._chickenWaypointRoot.localEulerAngles = zero;
				chickenWaypointRoot.localPosition = zero;
			}
			Vector3 installation = chickenCoopWaypointSetting.Installation;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			while (++num4 <= 999999)
			{
				int num5 = 0;
				int num6 = 0;
				for (;;)
				{
					Vector3 vector = new Vector3((float)this.ReversiNumber(num3), 0f, (float)this.ReversiNumber(num6));
					vector = Vector3.Scale(vector, installation);
					vector = position + rotation * vector;
					int num7 = Physics.RaycastNonAlloc(vector + Vector3.up, Vector3.down, FarmPoint._chickenWaypointRaycastHits, 2f, layer);
					num7 = Mathf.Min(FarmPoint._chickenWaypointRaycastHits.Length, num7);
					bool flag = 0 < num7;
					if (flag)
					{
						flag = false;
						for (int j = 0; j < num7; j++)
						{
							RaycastHit raycastHit = FarmPoint._chickenWaypointRaycastHits[j];
							if (collider == raycastHit.collider)
							{
								flag = true;
								break;
							}
						}
					}
					if (!flag)
					{
						num5++;
						if (num6 == 0)
						{
							goto Block_14;
						}
					}
					else
					{
						num2 = 0;
						num5 = 0;
						this._chickenWaypointPositionList.Add(vector);
					}
					if (2 <= num5)
					{
						break;
					}
					num6++;
				}
				IL_2D5:
				num3++;
				if (2 <= num2)
				{
					break;
				}
				continue;
				Block_14:
				num2++;
				goto IL_2D5;
			}
			foreach (Waypoint point in this._chickenWaypointList)
			{
				this.ReturnChickenWaypoint(point);
			}
			this._chickenWaypointList.Clear();
			if (this._chickenWaypointPositionList.IsNullOrEmpty<Vector3>())
			{
				return;
			}
			float closestEdgeDistance = chickenCoopWaypointSetting.ClosestEdgeDistance;
			float sampleDistance = chickenCoopWaypointSetting.SampleDistance;
			int agentAreaMask = chickenCoopWaypointSetting.AgentAreaMask;
			for (int k = 0; k < this._chickenWaypointPositionList.Count; k++)
			{
				Vector3 vector2 = this._chickenWaypointPositionList[k];
				bool flag2 = true;
				NavMeshHit navMeshHit;
				flag2 &= NavMesh.SamplePosition(vector2, out navMeshHit, sampleDistance, agentAreaMask);
				if (flag2)
				{
					vector2 = navMeshHit.position;
				}
				if (flag2)
				{
					flag2 &= NavMesh.FindClosestEdge(vector2, out navMeshHit, agentAreaMask);
				}
				if (flag2)
				{
					flag2 &= (closestEdgeDistance <= navMeshHit.distance);
				}
				if (flag2)
				{
					int num8 = Physics.RaycastNonAlloc(vector2 + Vector3.up, Vector3.down, FarmPoint._chickenWaypointRaycastHits, sampleDistance * 2f, layer);
					flag2 = (0 < num8);
					if (flag2)
					{
						num8 = Mathf.Min(num8, FarmPoint._chickenWaypointRaycastHits.Length);
						for (int l = 0; l < num8; l++)
						{
							RaycastHit raycastHit2 = FarmPoint._chickenWaypointRaycastHits[l];
							if (flag2 = (collider == raycastHit2.collider))
							{
								break;
							}
						}
					}
				}
				if (flag2)
				{
					Waypoint orCreateChickenWaypoint = this.GetOrCreateChickenWaypoint(k);
					orCreateChickenWaypoint.transform.position = vector2;
					if (!orCreateChickenWaypoint.gameObject.activeSelf)
					{
						orCreateChickenWaypoint.gameObject.SetActive(true);
					}
					this._chickenWaypointList.Add(orCreateChickenWaypoint);
				}
			}
		}

		// Token: 0x06005F08 RID: 24328 RVA: 0x002834D8 File Offset: 0x002818D8
		private int ReversiNumber(int num)
		{
			bool flag = num % 2 == 0;
			return (!flag) ? (num - num / 2) : (num * -1 + num / 2);
		}

		// Token: 0x06005F09 RID: 24329 RVA: 0x00283504 File Offset: 0x00281904
		private Waypoint GetOrCreateChickenWaypoint(int id)
		{
			Waypoint waypoint = this._chickenWaypointCacheList.PopFront<Waypoint>();
			if (waypoint != null)
			{
				return waypoint;
			}
			GameObject gameObject = new GameObject(string.Format("chicken_waypoint_{0:00}", id));
			waypoint = gameObject.GetOrAddComponent<Waypoint>();
			waypoint.GroupID = this.RegisterID;
			waypoint.ID = id;
			waypoint.transform.SetParent(this._chickenWaypointRoot, false);
			waypoint.Affiliation = Waypoint.AffiliationType.Item;
			return waypoint;
		}

		// Token: 0x06005F0A RID: 24330 RVA: 0x00283578 File Offset: 0x00281978
		private bool ReturnChickenWaypoint(Waypoint point)
		{
			if (point == null || point.gameObject == null)
			{
				return false;
			}
			if (!this._chickenWaypointCacheList.Contains(point))
			{
				if (point.gameObject.activeSelf)
				{
					point.gameObject.SetActive(false);
				}
				this._chickenWaypointCacheList.Add(point);
				return true;
			}
			return false;
		}

		// Token: 0x06005F0B RID: 24331 RVA: 0x002835E0 File Offset: 0x002819E0
		public void ClearChickenWayPoint()
		{
			if (this._chickenWaypointRoot == null)
			{
				return;
			}
			UnityEngine.Object.Destroy(this._chickenWaypointRoot.gameObject);
			this._chickenWaypointRoot = null;
			if (this._chickenWaypointPositionList != null)
			{
				this._chickenWaypointPositionList.Clear();
			}
			this._chickenWaypointPositionList = null;
			this._chickenWaypointList.Clear();
			this._chickenWaypointCacheList.Clear();
		}

		// Token: 0x1700126E RID: 4718
		// (get) Token: 0x06005F0C RID: 24332 RVA: 0x0028364B File Offset: 0x00281A4B
		// (set) Token: 0x06005F0D RID: 24333 RVA: 0x00283653 File Offset: 0x00281A53
		public List<PetChicken> ChickenList { get; private set; }

		// Token: 0x06005F0E RID: 24334 RVA: 0x0028365C File Offset: 0x00281A5C
		public void CreateChicken()
		{
			if (this._farmKind != FarmPoint.FarmKind.ChickenCoop)
			{
				return;
			}
			if (this.ChickenList != null)
			{
				this.ChickenList.RemoveAll((PetChicken x) => x == null);
				return;
			}
			if (!Singleton<Game>.IsInstance() || !Singleton<AnimalManager>.IsInstance())
			{
				return;
			}
			AIProject.SaveData.Environment environment = Singleton<Game>.Instance.Environment;
			if (environment == null)
			{
				return;
			}
			this.ChickenList = ListPool<PetChicken>.Get();
			Dictionary<int, List<AIProject.SaveData.Environment.ChickenInfo>> chickenTable = environment.ChickenTable;
			if (chickenTable == null)
			{
				return;
			}
			List<AIProject.SaveData.Environment.ChickenInfo> list = null;
			if (!chickenTable.TryGetValue(this.RegisterID, out list) || list.IsNullOrEmpty<AIProject.SaveData.Environment.ChickenInfo>())
			{
				if (Singleton<Map>.IsInstance())
				{
					int mapID = Singleton<Map>.Instance.MapID;
					int areaIDOnHousingArea = this.AreaIDOnHousingArea;
					Dictionary<int, Dictionary<int, Dictionary<int, AIProject.SaveData.AnimalData>>> housingChickenDataTable = environment.HousingChickenDataTable;
					Dictionary<int, Dictionary<int, AIProject.SaveData.AnimalData>> dictionary;
					if (!housingChickenDataTable.TryGetValue(mapID, out dictionary) || dictionary.IsNullOrEmpty<int, Dictionary<int, AIProject.SaveData.AnimalData>>())
					{
						return;
					}
					Dictionary<int, AIProject.SaveData.AnimalData> dictionary2;
					if (!dictionary.TryGetValue(areaIDOnHousingArea, out dictionary2) || dictionary2.IsNullOrEmpty<int, AIProject.SaveData.AnimalData>())
					{
						return;
					}
					int num = dictionary2.Keys.Max();
					if (list == null)
					{
						list = (chickenTable[this.RegisterID] = new List<AIProject.SaveData.Environment.ChickenInfo>());
					}
					while (list.Count <= num)
					{
						list.Add(null);
					}
					for (int i = 0; i < list.Count; i++)
					{
						list[i] = null;
					}
					foreach (KeyValuePair<int, AIProject.SaveData.AnimalData> keyValuePair in dictionary2)
					{
						int key = keyValuePair.Key;
						AIProject.SaveData.AnimalData value = keyValuePair.Value;
						if (value == null)
						{
							list[key] = null;
						}
						else
						{
							AIProject.SaveData.Environment.ChickenInfo chickenInfo = new AIProject.SaveData.Environment.ChickenInfo();
							chickenInfo.name = value.Nickname;
							chickenInfo.AnimalData = value;
							list[key] = chickenInfo;
						}
					}
				}
				if (list.IsNullOrEmpty<AIProject.SaveData.Environment.ChickenInfo>())
				{
					return;
				}
			}
			for (int j = 0; j < list.Count; j++)
			{
				AIProject.SaveData.Environment.ChickenInfo element = list.GetElement(j);
				AIProject.SaveData.AnimalData animalData = (element != null) ? element.AnimalData : null;
				if (animalData != null)
				{
					if (!animalData.InitAnimalTypeID || animalData.AnimalTypeID < 0)
					{
						animalData.AnimalTypeID = AIProject.Animal.AnimalData.GetAnimalTypeID(animalData.AnimalType);
						animalData.InitAnimalTypeID = true;
					}
					AnimalBase animalBase = Singleton<AnimalManager>.Instance.CreateBase(animalData.AnimalTypeID, (int)animalData.BreedingType);
					if (!(animalBase == null))
					{
						animalBase.transform.SetParent(this.AnimalRoot, true);
						animalData.AnimalID = animalBase.AnimalID;
						IPetAnimal petAnimal = animalBase as IPetAnimal;
						if (petAnimal != null)
						{
							petAnimal.AnimalData = animalData;
						}
						PetChicken petChicken = animalBase as PetChicken;
						this.AddChicken(j, petChicken);
						if (petChicken != null)
						{
							petChicken.Initialize(this);
						}
					}
				}
			}
		}

		// Token: 0x06005F0F RID: 24335 RVA: 0x00283974 File Offset: 0x00281D74
		public void AddChicken(int index, PetChicken chicken)
		{
			if (chicken == null)
			{
				return;
			}
			if (this.ChickenList == null)
			{
				return;
			}
			if (this.ChickenList.Contains(chicken))
			{
				return;
			}
			this.ChickenList.Add(chicken);
			if (!Singleton<Map>.IsInstance() || !Singleton<Game>.IsInstance())
			{
				return;
			}
			AIProject.SaveData.Environment environment = Singleton<Game>.Instance.Environment;
			if (environment == null)
			{
				return;
			}
			int mapID = Singleton<Map>.Instance.MapID;
			int areaIDOnHousingArea = this.AreaIDOnHousingArea;
			Dictionary<int, Dictionary<int, Dictionary<int, AIProject.SaveData.AnimalData>>> housingChickenDataTable = environment.HousingChickenDataTable;
			Dictionary<int, Dictionary<int, AIProject.SaveData.AnimalData>> dictionary;
			if (!housingChickenDataTable.TryGetValue(mapID, out dictionary) || dictionary == null)
			{
				dictionary = (housingChickenDataTable[mapID] = new Dictionary<int, Dictionary<int, AIProject.SaveData.AnimalData>>());
			}
			Dictionary<int, AIProject.SaveData.AnimalData> dictionary2;
			if (!dictionary.TryGetValue(areaIDOnHousingArea, out dictionary2) || dictionary2 == null)
			{
				dictionary2 = (dictionary[areaIDOnHousingArea] = new Dictionary<int, AIProject.SaveData.AnimalData>());
			}
			dictionary[areaIDOnHousingArea][index] = chicken.AnimalData;
		}

		// Token: 0x06005F10 RID: 24336 RVA: 0x00283A58 File Offset: 0x00281E58
		public void RemoveChicken(int index, PetChicken chicken)
		{
			if (chicken == null)
			{
				return;
			}
			if (this.ChickenList.IsNullOrEmpty<PetChicken>())
			{
				return;
			}
			if (!this.ChickenList.Contains(chicken))
			{
				return;
			}
			this.ChickenList.Remove(chicken);
			if (!Singleton<Map>.IsInstance() || !Singleton<Game>.IsInstance())
			{
				return;
			}
			AIProject.SaveData.Environment environment = Singleton<Game>.Instance.Environment;
			if (environment == null)
			{
				return;
			}
			int mapID = Singleton<Map>.Instance.MapID;
			int areaIDOnHousingArea = this.AreaIDOnHousingArea;
			Dictionary<int, Dictionary<int, Dictionary<int, AIProject.SaveData.AnimalData>>> housingChickenDataTable = environment.HousingChickenDataTable;
			Dictionary<int, Dictionary<int, AIProject.SaveData.AnimalData>> dictionary;
			if (!housingChickenDataTable.TryGetValue(mapID, out dictionary) || dictionary.IsNullOrEmpty<int, Dictionary<int, AIProject.SaveData.AnimalData>>())
			{
				return;
			}
			Dictionary<int, AIProject.SaveData.AnimalData> dictionary2;
			if (!dictionary.TryGetValue(areaIDOnHousingArea, out dictionary2) || dictionary2.IsNullOrEmpty<int, AIProject.SaveData.AnimalData>())
			{
				return;
			}
			dictionary2.Remove(index);
		}

		// Token: 0x06005F11 RID: 24337 RVA: 0x00283B24 File Offset: 0x00281F24
		public void DestroyChicken()
		{
			if (this._farmKind != FarmPoint.FarmKind.ChickenCoop)
			{
				return;
			}
			if (this.ChickenList != null)
			{
				this.ChickenList.RemoveAll((PetChicken x) => x == null);
				foreach (PetChicken petChicken in this.ChickenList)
				{
					IPetAnimal petAnimal = petChicken;
					if (petAnimal != null)
					{
						petAnimal.Release();
					}
					else
					{
						petChicken.Release();
					}
				}
				ListPool<PetChicken>.Release(this.ChickenList);
				this.ChickenList = null;
			}
			AIProject.SaveData.Environment environment = (!Singleton<Game>.IsInstance()) ? null : Singleton<Game>.Instance.Environment;
			if (environment == null)
			{
				return;
			}
			Dictionary<int, List<AIProject.SaveData.Environment.ChickenInfo>> chickenTable = environment.ChickenTable;
			List<AIProject.SaveData.Environment.ChickenInfo> list;
			if (!chickenTable.TryGetValue(this.RegisterID, out list))
			{
				return;
			}
			chickenTable.Remove(this.RegisterID);
		}

		// Token: 0x04005478 RID: 21624
		[SerializeField]
		private int _id;

		// Token: 0x04005479 RID: 21625
		[SerializeField]
		private FarmSection[] _harvestSections;

		// Token: 0x0400547A RID: 21626
		[SerializeField]
		private bool _enabledRangeCheck = true;

		// Token: 0x0400547B RID: 21627
		[SerializeField]
		private float _radius = 1f;

		// Token: 0x0400547C RID: 21628
		[SerializeField]
		private FarmPoint.FarmKind _farmKind;

		// Token: 0x0400547D RID: 21629
		[SerializeField]
		private Transform _commandBasePoint;

		// Token: 0x0400547F RID: 21631
		private int? _hashCode;

		// Token: 0x04005481 RID: 21633
		private NavMeshPath _pathForCalc;

		// Token: 0x04005483 RID: 21635
		private CommandLabel.CommandInfo[] _labels;

		// Token: 0x04005487 RID: 21639
		private List<Vector3> _chickenWaypointPositionList;

		// Token: 0x04005488 RID: 21640
		private Transform _chickenWaypointRoot;

		// Token: 0x04005489 RID: 21641
		private List<Waypoint> _chickenWaypointList = new List<Waypoint>();

		// Token: 0x0400548A RID: 21642
		private List<Waypoint> _chickenWaypointCacheList = new List<Waypoint>();

		// Token: 0x0400548B RID: 21643
		private static RaycastHit[] _chickenWaypointRaycastHits = new RaycastHit[10];

		// Token: 0x0400548C RID: 21644
		private Transform _animalRoot;

		// Token: 0x02000C09 RID: 3081
		public enum FarmKind
		{
			// Token: 0x04005491 RID: 21649
			Plant,
			// Token: 0x04005492 RID: 21650
			ChickenCoop,
			// Token: 0x04005493 RID: 21651
			Well
		}
	}
}
