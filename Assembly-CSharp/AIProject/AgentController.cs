using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using AIProject.Definitions;
using AIProject.SaveData;
using Manager;
using UniRx;
using UnityEngine;
using UnityEx;

namespace AIProject
{
	// Token: 0x02000C7A RID: 3194
	[RequireComponent(typeof(ActorLocomotion))]
	public class AgentController : ActorController
	{
		// Token: 0x1700150F RID: 5391
		// (get) Token: 0x060068AF RID: 26799 RVA: 0x002C9766 File Offset: 0x002C7B66
		public SearchArea SearchArea
		{
			[CompilerGenerated]
			get
			{
				return this._searchArea;
			}
		}

		// Token: 0x17001510 RID: 5392
		// (get) Token: 0x060068B0 RID: 26800 RVA: 0x002C976E File Offset: 0x002C7B6E
		public float DistanceReached
		{
			[CompilerGenerated]
			get
			{
				return this._distanceReached;
			}
		}

		// Token: 0x17001511 RID: 5393
		// (get) Token: 0x060068B1 RID: 26801 RVA: 0x002C9776 File Offset: 0x002C7B76
		// (set) Token: 0x060068B2 RID: 26802 RVA: 0x002C977E File Offset: 0x002C7B7E
		public int PrevID { get; private set; } = -1;

		// Token: 0x17001512 RID: 5394
		// (get) Token: 0x060068B3 RID: 26803 RVA: 0x002C9787 File Offset: 0x002C7B87
		// (set) Token: 0x060068B4 RID: 26804 RVA: 0x002C978F File Offset: 0x002C7B8F
		public EventType PrevEvent { get; private set; }

		// Token: 0x17001513 RID: 5395
		// (get) Token: 0x060068B5 RID: 26805 RVA: 0x002C9798 File Offset: 0x002C7B98
		// (set) Token: 0x060068B6 RID: 26806 RVA: 0x002C97A0 File Offset: 0x002C7BA0
		public Rarelity PrevRarelity { get; private set; } = Rarelity.N;

		// Token: 0x17001514 RID: 5396
		// (get) Token: 0x060068B7 RID: 26807 RVA: 0x002C97A9 File Offset: 0x002C7BA9
		// (set) Token: 0x060068B8 RID: 26808 RVA: 0x002C97B1 File Offset: 0x002C7BB1
		public AgentController.PermissionStatus Permission { get; private set; }

		// Token: 0x17001515 RID: 5397
		// (get) Token: 0x060068B9 RID: 26809 RVA: 0x002C97BA File Offset: 0x002C7BBA
		// (set) Token: 0x060068BA RID: 26810 RVA: 0x002C97C4 File Offset: 0x002C7BC4
		public bool SleepedSchedule
		{
			get
			{
				return this._sleepedSchedule;
			}
			set
			{
				if (value == this._sleepedSchedule)
				{
					return;
				}
				this._sleepedSchedule = value;
				EnvironmentSimulator simulator = Singleton<Manager.Map>.Instance.Simulator;
				if (value)
				{
					this._startTime = simulator.Now;
				}
				else
				{
					DateTime now = simulator.Now;
					this._extendedTime = now - this._startTime;
				}
			}
		}

		// Token: 0x060068BB RID: 26811 RVA: 0x002C9820 File Offset: 0x002C7C20
		private void OnDisable()
		{
			if (this._character)
			{
				this._character.enabled = false;
			}
		}

		// Token: 0x060068BC RID: 26812 RVA: 0x002C983E File Offset: 0x002C7C3E
		public override void StartBehavior()
		{
			if (this._character == null)
			{
				this._character = base.GetComponent<ActorLocomotionAgent>();
			}
		}

		// Token: 0x060068BD RID: 26813 RVA: 0x002C985D File Offset: 0x002C7C5D
		protected override void Start()
		{
			base.Start();
			(from _ in Observable.EveryUpdate().TakeUntilDestroy(base.gameObject)
			where base.isActiveAndEnabled
			select _).Subscribe(delegate(long _)
			{
				this.OnUpdate();
			});
		}

		// Token: 0x060068BE RID: 26814 RVA: 0x002C9898 File Offset: 0x002C7C98
		private void OnUpdate()
		{
			bool? flag = (this._actor != null) ? new bool?(this._actor.IsInit) : null;
			if (flag == null || !flag.Value)
			{
				return;
			}
			if (this._character != null)
			{
				this._character.Move(Vector3.zero);
			}
			if (Singleton<Scene>.Instance.IsNowLoadingFade)
			{
				return;
			}
		}

		// Token: 0x060068BF RID: 26815 RVA: 0x002C9918 File Offset: 0x002C7D18
		protected override void SubFixedUpdate()
		{
			if (this._character != null)
			{
				this._character.UpdateState();
			}
		}

		// Token: 0x060068C0 RID: 26816 RVA: 0x002C9934 File Offset: 0x002C7D34
		public AgentController.PermissionStatus GetPermission(ActionPoint point)
		{
			AgentActor agentActor = this._actor as AgentActor;
			UnityEx.ValueTuple<EventType, Desire.Type>[] valuePairs = Desire.ValuePairs;
			Desire.Type requestedDesire = agentActor.RequestedDesire;
			if (!point.IsNeutralCommand)
			{
				AgentController.PermissionStatus permissionStatus = AgentController.PermissionStatus.Prohibition;
				this.Permission = permissionStatus;
				return permissionStatus;
			}
			AgentController.PermissionStatus result;
			if (AgentController.AnyDesire(valuePairs, requestedDesire))
			{
				EventType type = AgentController.FirstDesire(valuePairs, requestedDesire).Item1;
				if (requestedDesire == Desire.Type.Bath && (float)agentActor.ChaControl.fileGameInfo.flavorState[2] < Singleton<Manager.Resources>.Instance.StatusProfile.CanDressBorder)
				{
					type = EventType.Bath;
				}
				if (point.AgentEventType.Contains(type))
				{
					if (point is SearchActionPoint)
					{
						SearchActionPoint searchActionPoint = point as SearchActionPoint;
						StuffItem itemInfo = agentActor.AgentData.EquipedSearchItem(searchActionPoint.TableID);
						int searchAreaID = agentActor.SearchAreaID;
						if (searchAreaID != 0)
						{
							if (agentActor.SearchAreaID == searchActionPoint.TableID)
							{
								if (searchActionPoint.CanSearch(EventType.Search, itemInfo))
								{
									AgentController.PermissionStatus permissionStatus = AgentController.PermissionStatus.Permission;
									this.Permission = permissionStatus;
									result = permissionStatus;
								}
								else
								{
									AgentController.PermissionStatus permissionStatus = AgentController.PermissionStatus.Prohibition;
									this.Permission = permissionStatus;
									result = permissionStatus;
								}
							}
							else
							{
								AgentController.PermissionStatus permissionStatus = AgentController.PermissionStatus.Prohibition;
								this.Permission = permissionStatus;
								result = permissionStatus;
							}
						}
						else if (searchActionPoint.TableID == 0 || searchActionPoint.TableID == 1 || searchActionPoint.TableID == 2)
						{
							if (searchActionPoint.CanSearch(EventType.Search, itemInfo))
							{
								AgentController.PermissionStatus permissionStatus = AgentController.PermissionStatus.Permission;
								this.Permission = permissionStatus;
								result = permissionStatus;
							}
							else
							{
								AgentController.PermissionStatus permissionStatus = AgentController.PermissionStatus.Prohibition;
								this.Permission = permissionStatus;
								result = permissionStatus;
							}
						}
						else
						{
							AgentController.PermissionStatus permissionStatus = AgentController.PermissionStatus.Prohibition;
							this.Permission = permissionStatus;
							result = permissionStatus;
						}
					}
					else
					{
						AgentController.PermissionStatus permissionStatus = AgentController.PermissionStatus.Permission;
						this.Permission = permissionStatus;
						result = permissionStatus;
					}
				}
				else
				{
					AgentController.PermissionStatus permissionStatus = AgentController.PermissionStatus.Prohibition;
					this.Permission = permissionStatus;
					result = permissionStatus;
				}
			}
			else
			{
				AgentController.PermissionStatus permissionStatus = AgentController.PermissionStatus.Prohibition;
				this.Permission = permissionStatus;
				result = permissionStatus;
			}
			return result;
		}

		// Token: 0x060068C1 RID: 26817 RVA: 0x002C9B1C File Offset: 0x002C7F1C
		private static bool AnyDesire(ReadOnlyDictionary<EventType, Desire.Type> table, Desire.Type type)
		{
			foreach (KeyValuePair<EventType, Desire.Type> keyValuePair in table)
			{
				if (keyValuePair.Value == type)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060068C2 RID: 26818 RVA: 0x002C9B80 File Offset: 0x002C7F80
		private static bool AnyDesire(UnityEx.ValueTuple<EventType, Desire.Type>[] collection, Desire.Type type)
		{
			foreach (UnityEx.ValueTuple<EventType, Desire.Type> valueTuple in collection)
			{
				if (valueTuple.Item2 == type)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060068C3 RID: 26819 RVA: 0x002C9BC0 File Offset: 0x002C7FC0
		private static KeyValuePair<EventType, Desire.Type> FirstDesire(ReadOnlyDictionary<EventType, Desire.Type> table, Desire.Type type)
		{
			foreach (KeyValuePair<EventType, Desire.Type> result in table)
			{
				if (result.Value == type)
				{
					return result;
				}
			}
			return default(KeyValuePair<EventType, Desire.Type>);
		}

		// Token: 0x060068C4 RID: 26820 RVA: 0x002C9C2C File Offset: 0x002C802C
		public static UnityEx.ValueTuple<EventType, Desire.Type> FirstDesire(UnityEx.ValueTuple<EventType, Desire.Type>[] collection, Desire.Type type)
		{
			foreach (UnityEx.ValueTuple<EventType, Desire.Type> result in collection)
			{
				if (result.Item2 == type)
				{
					return result;
				}
			}
			return default(UnityEx.ValueTuple<EventType, Desire.Type>);
		}

		// Token: 0x060068C5 RID: 26821 RVA: 0x002C9C74 File Offset: 0x002C8074
		private void OnTriggerExit(Collider other)
		{
		}

		// Token: 0x060068C6 RID: 26822 RVA: 0x002C9C76 File Offset: 0x002C8076
		public override void ChangeState(string stateName)
		{
			if (!Singleton<Scene>.Instance.AddSceneName.IsNullOrEmpty())
			{
				return;
			}
		}

		// Token: 0x0400595F RID: 22879
		[SerializeField]
		private ActorLocomotionAgent _character;

		// Token: 0x04005960 RID: 22880
		[SerializeField]
		private SearchArea _searchArea;

		// Token: 0x04005961 RID: 22881
		[SerializeField]
		private float _distanceReached = 0.5f;

		// Token: 0x04005966 RID: 22886
		private bool _sleepedSchedule;

		// Token: 0x04005967 RID: 22887
		private DateTime _startTime = DateTime.MinValue;

		// Token: 0x04005968 RID: 22888
		private TimeSpan _extendedTime = TimeSpan.MinValue;

		// Token: 0x02000C7B RID: 3195
		public enum PermissionStatus
		{
			// Token: 0x0400596A RID: 22890
			None,
			// Token: 0x0400596B RID: 22891
			Prohibition,
			// Token: 0x0400596C RID: 22892
			Permission
		}
	}
}
