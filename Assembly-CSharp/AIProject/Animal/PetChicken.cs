using System;
using System.Collections.Generic;
using AIProject.Animal.Resources;
using AIProject.SaveData;
using Housing;
using Manager;
using UnityEngine;
using UnityEngine.AI;

namespace AIProject.Animal
{
	// Token: 0x02000BB0 RID: 2992
	[RequireComponent(typeof(NavMeshAgent))]
	public class PetChicken : MovingPetAnimal
	{
		// Token: 0x170010C5 RID: 4293
		// (get) Token: 0x060059D4 RID: 22996 RVA: 0x00267E5D File Offset: 0x0026625D
		// (set) Token: 0x060059D5 RID: 22997 RVA: 0x00267E65 File Offset: 0x00266265
		public FarmPoint FarmPoint { get; set; }

		// Token: 0x170010C6 RID: 4294
		// (get) Token: 0x060059D6 RID: 22998 RVA: 0x00267E6E File Offset: 0x0026626E
		// (set) Token: 0x060059D7 RID: 22999 RVA: 0x00267E76 File Offset: 0x00266276
		public int ChickenIndex { get; set; }

		// Token: 0x060059D8 RID: 23000 RVA: 0x00267E80 File Offset: 0x00266280
		public void Initialize(FarmPoint farmPoint)
		{
			this.Clear();
			this.FarmPoint = farmPoint;
			if (farmPoint == null)
			{
				this.SetState(AnimalState.Destroyed, null);
				return;
			}
			this._restrictedPointList = this.FarmPoint.ChickenWaypointList;
			List<Waypoint> list = ListPool<Waypoint>.Get();
			if (!this._restrictedPointList.IsNullOrEmpty<Waypoint>())
			{
				foreach (Waypoint waypoint in this._restrictedPointList)
				{
					if (waypoint != null && waypoint.Available(this))
					{
						list.Add(waypoint);
					}
				}
			}
			while (!list.IsNullOrEmpty<Waypoint>())
			{
				int index = UnityEngine.Random.Range(0, list.Count);
				Waypoint waypoint2 = list[index];
				list.RemoveAt(index);
				if (waypoint2.Available(this))
				{
					this._destination = waypoint2;
					break;
				}
			}
			ListPool<Waypoint>.Release(list);
			Dictionary<int, AnimalModelInfo> source;
			if (Singleton<Manager.Resources>.Instance.AnimalTable.ModelInfoTable.TryGetValue(base.AnimalData.AnimalTypeID, out source) && !source.IsNullOrEmpty<int, AnimalModelInfo>())
			{
				KeyValuePair<int, AnimalModelInfo> keyValuePair = source.Rand<int, AnimalModelInfo>();
				base.AnimalData.ModelID = keyValuePair.Key;
				base.SetModelInfo(keyValuePair.Value);
			}
			base.LoadBody();
			base.SetStateData();
			if (this._nicknameRoot == null)
			{
				Transform parent = (!(this.bodyObject != null)) ? base.transform : this.bodyObject.transform;
				this._nicknameRoot = new GameObject("Nickname Root").transform;
				this._nicknameRoot.SetParent(parent, false);
				this._nicknameRoot.localPosition = new Vector3(0f, base.NicknameHeightOffset, 0f);
			}
			bool flag = false;
			base.MarkerEnabled = flag;
			this.BodyEnabled = flag;
			base.Agent.enabled = false;
			this._originPriority = Singleton<Manager.Resources>.Instance.AnimalDefinePack.AgentInfo.GroundAnimalStartPriority;
			this._originPriority += this.ChickenIndex;
			base.Agent.avoidancePriority = this._originPriority;
			if (this._destination != null)
			{
				this._destination.Reserver = this;
				this.Position = this._destination.transform.position;
				base.Rotation = Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f);
			}
			this.SetState(AnimalState.Start, null);
		}

		// Token: 0x060059D9 RID: 23001 RVA: 0x0026812C File Offset: 0x0026652C
		public override void Initialize(AnimalData animalData)
		{
			base.AnimalData = animalData;
			if (animalData == null)
			{
				this.SetState(AnimalState.Destroyed, null);
				return;
			}
			animalData.AnimalID = base.AnimalID;
			int registerID = animalData.RegisterID;
		}

		// Token: 0x060059DA RID: 23002 RVA: 0x00268165 File Offset: 0x00266565
		public override void Initialize(PetHomePoint _homePoint)
		{
		}

		// Token: 0x060059DB RID: 23003 RVA: 0x00268168 File Offset: 0x00266568
		public override void ReturnHome()
		{
			if (this._restrictedPointList.IsNullOrEmpty<Waypoint>())
			{
				return;
			}
			this.ReleaseDestination();
			this.ReleaseAgentPath();
			List<Waypoint> list = ListPool<Waypoint>.Get();
			foreach (Waypoint waypoint in this._restrictedPointList)
			{
				if (waypoint != null && waypoint.Available(this))
				{
					list.Add(waypoint);
				}
			}
			this._destination = list.Rand<Waypoint>();
			if (this._destination == null)
			{
				ItemComponent itemComponent = (!(this.FarmPoint != null)) ? null : this.FarmPoint.GetComponentInParent<ItemComponent>();
				if (itemComponent != null)
				{
					this.Position = itemComponent.transform.position;
				}
			}
			else
			{
				this._destination.Reserver = this;
				this.Position = this._destination.transform.position;
			}
			ListPool<Waypoint>.Release(list);
			this.SetState(AnimalState.Idle, null);
		}

		// Token: 0x060059DC RID: 23004 RVA: 0x00268290 File Offset: 0x00266690
		protected override void EnterStart()
		{
			this.SetState(AnimalState.Idle, null);
			base.Agent.avoidancePriority = 0;
		}

		// Token: 0x060059DD RID: 23005 RVA: 0x002682A8 File Offset: 0x002666A8
		protected override void ExitStart()
		{
			bool flag = true;
			base.MarkerEnabled = flag;
			this.BodyEnabled = flag;
			this.ActivateNavMeshAgent();
			this.StartWaypointRetention();
			this.Active = true;
		}

		// Token: 0x060059DE RID: 23006 RVA: 0x002682D8 File Offset: 0x002666D8
		protected override void EnterIdle()
		{
			base.PlayInAnim(AnimationCategoryID.Idle, 0, null);
			base.Agent.avoidancePriority = 0;
		}

		// Token: 0x060059DF RID: 23007 RVA: 0x002682EF File Offset: 0x002666EF
		protected override void OnIdle()
		{
			if (base.AnimationKeepWaiting())
			{
				return;
			}
			this.SetState(AnimalState.Locomotion, null);
		}

		// Token: 0x060059E0 RID: 23008 RVA: 0x00268305 File Offset: 0x00266705
		protected override void ExitIdle()
		{
		}

		// Token: 0x060059E1 RID: 23009 RVA: 0x00268308 File Offset: 0x00266708
		protected override void EnterLocomotion()
		{
			base.PlayInAnim(AnimationCategoryID.Locomotion, 0, null);
			this.ActivateNavMeshAgent();
			base.Agent.avoidancePriority = this._originPriority;
			base.Agent.speed = base.WalkSpeed;
			base.Agent.stoppingDistance = base.NormalStoppingDistance;
			this._arrivalCount = 0;
			this._arrivalLimit = base.NarrowArrivalLimit.RandomRange();
			base.Agent.isStopped = false;
			base.SetNextMovePoint();
		}

		// Token: 0x060059E2 RID: 23010 RVA: 0x00268384 File Offset: 0x00266784
		protected override void OnLocomotion()
		{
			if (this._destination == null)
			{
				base.StateCounter = 0f;
				if (this._arrivalCount < this._arrivalLimit)
				{
					base.SetNextMovePoint();
				}
				return;
			}
			if (base.ClosedDestination())
			{
				this._arrivalCount++;
				if (this._arrivalCount < this._arrivalLimit)
				{
					base.SetNextMovePoint();
				}
				else
				{
					AnimalState animalState = (UnityEngine.Random.Range(0, 100) >= 95) ? AnimalState.Eat : AnimalState.Idle;
					if (animalState == AnimalState.Eat)
					{
						bool flag = Singleton<Manager.Resources>.IsInstance() && Singleton<Manager.Resources>.Instance.AnimalDefinePack != null;
						if (flag)
						{
							AnimalDefinePack animalDefinePack = Singleton<Manager.Resources>.Instance.AnimalDefinePack;
							AnimalDefinePack.ChickenCoopWaypointSettings chickenCoopWaypointSetting = animalDefinePack.ChickenCoopWaypointSetting;
							NavMeshHit navMeshHit;
							flag = (NavMesh.FindClosestEdge(this.Position, out navMeshHit, base.Agent.areaMask) && chickenCoopWaypointSetting.CanEatEdgeDistance <= navMeshHit.distance);
						}
						if (!flag)
						{
							animalState = AnimalState.Idle;
						}
					}
					this.SetState(animalState, null);
				}
			}
		}

		// Token: 0x060059E3 RID: 23011 RVA: 0x00268495 File Offset: 0x00266895
		protected override void ExitLocomotion()
		{
			base.SetFloat(this._locomotionParamName, 0f);
			this.ReleaseAgentPath();
			if (base.CurrentState != AnimalState.Destroyed)
			{
				base.Agent.isStopped = true;
			}
		}

		// Token: 0x060059E4 RID: 23012 RVA: 0x002684C8 File Offset: 0x002668C8
		protected override void AnimationLocomotion()
		{
			float value = (!Mathf.Approximately(base.Agent.velocity.magnitude, 0f)) ? 0.5f : 0f;
			base.SetFloat(this._locomotionParamName, value);
		}

		// Token: 0x060059E5 RID: 23013 RVA: 0x00268514 File Offset: 0x00266914
		protected override void EnterRoar()
		{
			base.PlayInAnim(AnimationCategoryID.Action, 0, null);
			base.Agent.avoidancePriority = 0;
		}

		// Token: 0x060059E6 RID: 23014 RVA: 0x0026852B File Offset: 0x0026692B
		protected override void OnRoar()
		{
			if (base.AnimationKeepWaiting())
			{
				return;
			}
			this.SetState(AnimalState.Idle, null);
		}

		// Token: 0x060059E7 RID: 23015 RVA: 0x00268541 File Offset: 0x00266941
		protected override void ExitRoar()
		{
		}

		// Token: 0x060059E8 RID: 23016 RVA: 0x00268543 File Offset: 0x00266943
		protected override void EnterEat()
		{
			base.PlayInAnim(AnimationCategoryID.Action, 0, null);
			base.Agent.avoidancePriority = 0;
		}

		// Token: 0x060059E9 RID: 23017 RVA: 0x0026855C File Offset: 0x0026695C
		protected override void OnEat()
		{
			if (base.AnimationKeepWaiting())
			{
				return;
			}
			AnimalState nextState = (!AnimalBase.RandomBool) ? AnimalState.Locomotion : AnimalState.Idle;
			this.SetState(nextState, null);
		}

		// Token: 0x060059EA RID: 23018 RVA: 0x0026858F File Offset: 0x0026698F
		protected override void ExitEat()
		{
		}

		// Token: 0x060059EB RID: 23019 RVA: 0x00268591 File Offset: 0x00266991
		protected override void EnterAction0()
		{
		}

		// Token: 0x060059EC RID: 23020 RVA: 0x00268593 File Offset: 0x00266993
		protected override void OnAction0()
		{
		}

		// Token: 0x060059ED RID: 23021 RVA: 0x00268595 File Offset: 0x00266995
		protected override void ExitAction0()
		{
		}

		// Token: 0x060059EE RID: 23022 RVA: 0x00268597 File Offset: 0x00266997
		protected override void OnDestroy()
		{
			this.ReleaseDestination();
			this.ReleaseMovePointList();
			base.OnDestroy();
		}
	}
}
