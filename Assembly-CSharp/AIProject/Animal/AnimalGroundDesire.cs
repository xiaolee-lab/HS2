using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Manager;
using UnityEngine;
using UnityEngine.AI;

namespace AIProject.Animal
{
	// Token: 0x02000B85 RID: 2949
	public class AnimalGroundDesire : AnimalGround
	{
		// Token: 0x1700102D RID: 4141
		// (get) Token: 0x06005794 RID: 22420 RVA: 0x0025D286 File Offset: 0x0025B686
		public AnimalSearchActionPoint SearchAction
		{
			[CompilerGenerated]
			get
			{
				return this._searchAction;
			}
		}

		// Token: 0x1700102E RID: 4142
		// (get) Token: 0x06005795 RID: 22421 RVA: 0x0025D28E File Offset: 0x0025B68E
		public List<AnimalActionPoint> ActionPoints
		{
			[CompilerGenerated]
			get
			{
				return (!(this.SearchAction != null)) ? null : this.SearchAction.SearchPoints;
			}
		}

		// Token: 0x1700102F RID: 4143
		// (get) Token: 0x06005796 RID: 22422 RVA: 0x0025D2B2 File Offset: 0x0025B6B2
		public List<AnimalActionPoint> VisibleActionPoints
		{
			[CompilerGenerated]
			get
			{
				return (!(this.SearchAction != null)) ? null : this.SearchAction.VisibleList;
			}
		}

		// Token: 0x17001030 RID: 4144
		// (get) Token: 0x06005797 RID: 22423 RVA: 0x0025D2D8 File Offset: 0x0025B6D8
		public override bool WaitPossible
		{
			get
			{
				return base.CurrentState == AnimalState.Idle || base.CurrentState == AnimalState.Locomotion || base.CurrentState == AnimalState.Grooming || base.CurrentState == AnimalState.MoveEars || base.CurrentState == AnimalState.Roar || base.CurrentState == AnimalState.Peck || base.CurrentState == AnimalState.Action0 || base.CurrentState == AnimalState.Action1 || base.CurrentState == AnimalState.Action2;
			}
		}

		// Token: 0x06005798 RID: 22424 RVA: 0x0025D358 File Offset: 0x0025B758
		protected override void Awake()
		{
			base.Awake();
			if (!this.desireController)
			{
				this.desireController = base.GetComponentInChildren<AnimalDesireController>(true);
			}
			if (this.desireController)
			{
				this.desireController.DesireFilledEvent = new Func<DesireType, bool>(this.DesireFilledEvent);
				this.desireController.ChangedCandidateDesireEvent = new Func<DesireType, bool>(this.ChangedCandidateDesire);
			}
		}

		// Token: 0x06005799 RID: 22425 RVA: 0x0025D3C8 File Offset: 0x0025B7C8
		public override void SetSearchTargetEnabled(bool _enabled, bool _clearCollision = true)
		{
			if (base.SearchActor != null)
			{
				base.SearchActor.SetSearchEnabled(_enabled, _clearCollision);
			}
			if (this.SearchAction != null)
			{
				this.SearchAction.SetSearchEnabled(_enabled, _clearCollision);
			}
		}

		// Token: 0x0600579A RID: 22426 RVA: 0x0025D406 File Offset: 0x0025B806
		public override void RefreshSearchTarget()
		{
			if (this.SearchAction != null)
			{
				this.SearchAction.RefreshQueryPoints();
			}
			if (base.SearchActor != null)
			{
				base.SearchActor.RefreshSearchActorTable();
			}
		}

		// Token: 0x0600579B RID: 22427 RVA: 0x0025D440 File Offset: 0x0025B840
		public override void Clear()
		{
			this.desireController.Clear();
			base.Clear();
		}

		// Token: 0x0600579C RID: 22428 RVA: 0x0025D454 File Offset: 0x0025B854
		public void ReduceDesireParameter(bool _success, DesireType _changeDesire)
		{
			List<AnimalState> list;
			if (this.desireController.TargetStateTable.TryGetValue(_changeDesire, out list) && list.Contains(base.CurrentState))
			{
				this.desireController.ReduceParameter(_success, _changeDesire);
			}
		}

		// Token: 0x0600579D RID: 22429 RVA: 0x0025D498 File Offset: 0x0025B898
		protected override void ChangedStateEvent()
		{
			base.ChangedStateEvent();
			if (this.desireController.HasCurrentDesire)
			{
				this.ReduceDesireParameter(true, this.desireController.CurrentDesire);
			}
			if (this.desireController.HasCandidateDesire)
			{
				this.ReduceDesireParameter(true, this.desireController.CandidateDesire);
			}
		}

		// Token: 0x0600579E RID: 22430 RVA: 0x0025D4EF File Offset: 0x0025B8EF
		public override void OnMinuteUpdate(TimeSpan _deltaTime)
		{
			base.OnMinuteUpdate(_deltaTime);
			this.desireController.OnMinuteUpdate();
		}

		// Token: 0x0600579F RID: 22431 RVA: 0x0025D503 File Offset: 0x0025B903
		protected virtual bool DesireFilledEvent(DesireType _desireType)
		{
			return false;
		}

		// Token: 0x060057A0 RID: 22432 RVA: 0x0025D506 File Offset: 0x0025B906
		protected virtual bool ChangedCandidateDesire(DesireType _desireType)
		{
			return !this.desireController.HasCurrentDesire;
		}

		// Token: 0x060057A1 RID: 22433 RVA: 0x0025D51C File Offset: 0x0025B91C
		public override bool SetActionPoint(AnimalActionPoint _actionPoint, AnimalState _nextState, ActionTypes _actionType)
		{
			if (!base.Agent.enabled)
			{
				return this.SetActionPointNonAgent(_actionPoint, _nextState, _actionType);
			}
			if (_actionPoint == null || this.actionPoint == _actionPoint || _nextState == AnimalState.None)
			{
				return false;
			}
			if (this.calculatePath == null)
			{
				this.calculatePath = new NavMeshPath();
			}
			if (!base.Agent.CalculatePath(_actionPoint.Destination, this.calculatePath) || this.calculatePath.status != NavMeshPathStatus.PathComplete || !base.Agent.SetPath(this.calculatePath))
			{
				return false;
			}
			this.calculatePath = null;
			base.ClearCurrentWaypoint();
			this.destination = null;
			base.LocomotionCount = 1;
			base.NextState = _nextState;
			if (this.actionPoint != null)
			{
				this.actionPoint.RemoveBooking(this);
			}
			this.actionPoint = _actionPoint;
			this.actionPoint.AddBooking(this);
			base.ActionType = _actionType;
			return true;
		}

		// Token: 0x060057A2 RID: 22434 RVA: 0x0025D624 File Offset: 0x0025BA24
		public bool SetActionPointNonAgent(AnimalActionPoint _actionPoint, AnimalState _nextState, ActionTypes _actionType)
		{
			if (_actionPoint == null || this.actionPoint == _actionPoint || _nextState == AnimalState.None)
			{
				return false;
			}
			base.ClearCurrentWaypoint();
			base.LocomotionCount = 0;
			base.NextState = _nextState;
			if (this.actionPoint != null)
			{
				this.actionPoint.RemoveBooking(this);
			}
			this.actionPoint = _actionPoint;
			this.actionPoint.AddBooking(this);
			base.ActionType = _actionType;
			return true;
		}

		// Token: 0x060057A3 RID: 22435 RVA: 0x0025D6A4 File Offset: 0x0025BAA4
		protected AnimalActionPoint[] GetActionPoints(ActionTypes _actionType)
		{
			List<AnimalActionPoint> list = ListPool<AnimalActionPoint>.Get();
			for (int i = 0; i < this.ActionPoints.Count; i++)
			{
				AnimalActionPoint animalActionPoint = this.ActionPoints[i];
				if (animalActionPoint.Available(this) && (animalActionPoint.ActionType & _actionType) != ActionTypes.None)
				{
					list.Add(animalActionPoint);
				}
			}
			AnimalActionPoint[] array = new AnimalActionPoint[list.Count];
			for (int j = 0; j < list.Count; j++)
			{
				array[j] = list[j];
			}
			ListPool<AnimalActionPoint>.Release(list);
			return array;
		}

		// Token: 0x060057A4 RID: 22436 RVA: 0x0025D73C File Offset: 0x0025BB3C
		protected virtual void EnterAction()
		{
			if (base.HasActionPoint)
			{
				if (base.IsNearPoint(this.actionPoint.Destination) && base.ActionType.Contains(this.actionPoint.ActionType))
				{
					this.actionPoint.SetUse(this);
					this.actionPoint.SetStand(this, this.actionPoint.GetSlot(base.ActionType).Item1);
					this.prevAgentEnabled = base.Agent.enabled;
					if (this.prevAgentEnabled != this.actionPoint.EnabledNavMeshAgent)
					{
						base.Agent.enabled = this.actionPoint.EnabledNavMeshAgent;
					}
				}
				else
				{
					this.MissingActionPoint();
				}
			}
		}

		// Token: 0x060057A5 RID: 22437 RVA: 0x0025D7FC File Offset: 0x0025BBFC
		protected virtual void EnterAction(Action _endEvent)
		{
			this.EnterAction();
			this.ScheduleEndEvent = _endEvent;
		}

		// Token: 0x060057A6 RID: 22438 RVA: 0x0025D80C File Offset: 0x0025BC0C
		protected virtual void OnAction()
		{
			if (this.schedule.managing)
			{
				if (!this.schedule.enable)
				{
					base.ClearSchedule();
					base.StateEndEvent();
					return;
				}
			}
			else if (!base.AnimationKeepWaiting())
			{
				base.StateEndEvent();
				return;
			}
		}

		// Token: 0x060057A7 RID: 22439 RVA: 0x0025D860 File Offset: 0x0025BC60
		protected virtual void ExitAction()
		{
			if (base.HasActionPoint)
			{
				if (this.actionPoint.MyUse(this))
				{
					float time = 180f;
					if (Singleton<Manager.Resources>.IsInstance())
					{
						AnimalDefinePack.AllAnimalInfoGroup allAnimalInfo = Singleton<Manager.Resources>.Instance.AnimalDefinePack.AllAnimalInfo;
						if (allAnimalInfo != null)
						{
							time = allAnimalInfo.ActionPointUsedCoolTime;
						}
					}
					this.actionPoint.SetUsedCoolTime(this, time, false);
					this.actionPoint.StopUsing();
					this.actionPoint.SetStand(this, this.actionPoint.GetSlot(base.ActionType).Item2);
				}
				if (this.prevAgentEnabled != base.Agent.enabled)
				{
					base.Agent.enabled = this.prevAgentEnabled;
				}
				this.actionPoint = null;
			}
		}

		// Token: 0x060057A8 RID: 22440 RVA: 0x0025D920 File Offset: 0x0025BD20
		protected override void EnterSleep()
		{
			this.ModelInfo.EyesShapeInfo.SetBlendShape(100f);
			this.EnterAction(delegate()
			{
				this.SetState(AnimalState.Locomotion, null);
			});
			base.PlayInAnim(AnimationCategoryID.Sleep, null);
			base.SetSchedule(this.CurrentAnimState);
		}

		// Token: 0x060057A9 RID: 22441 RVA: 0x0025D95E File Offset: 0x0025BD5E
		protected override void OnSleep()
		{
			this.OnAction();
		}

		// Token: 0x060057AA RID: 22442 RVA: 0x0025D966 File Offset: 0x0025BD66
		protected override void ExitSleep()
		{
			this.ModelInfo.EyesShapeInfo.SetBlendShape(0f);
			this.ExitAction();
		}

		// Token: 0x060057AB RID: 22443 RVA: 0x0025D983 File Offset: 0x0025BD83
		protected override void EnterAction0()
		{
			this.EnterAction(delegate()
			{
				this.SetState(AnimalState.Locomotion, null);
			});
			base.PlayInAnim(AnimationCategoryID.Action, null);
			base.SetSchedule(this.CurrentAnimState);
		}

		// Token: 0x060057AC RID: 22444 RVA: 0x0025D9AC File Offset: 0x0025BDAC
		protected override void OnAction0()
		{
			this.OnAction();
		}

		// Token: 0x060057AD RID: 22445 RVA: 0x0025D9B4 File Offset: 0x0025BDB4
		protected override void ExitAction0()
		{
			this.ExitAction();
		}

		// Token: 0x060057AE RID: 22446 RVA: 0x0025D9BC File Offset: 0x0025BDBC
		protected override void EnterAction1()
		{
			this.EnterAction(delegate()
			{
				this.SetState(AnimalState.Locomotion, null);
			});
		}

		// Token: 0x060057AF RID: 22447 RVA: 0x0025D9D0 File Offset: 0x0025BDD0
		protected override void OnAction1()
		{
			this.OnAction();
		}

		// Token: 0x060057B0 RID: 22448 RVA: 0x0025D9D8 File Offset: 0x0025BDD8
		protected override void ExitAction1()
		{
			this.ExitAction();
		}

		// Token: 0x060057B1 RID: 22449 RVA: 0x0025D9E0 File Offset: 0x0025BDE0
		protected override void EnterAction2()
		{
			this.EnterAction(delegate()
			{
				this.SetState(AnimalState.Locomotion, null);
			});
		}

		// Token: 0x060057B2 RID: 22450 RVA: 0x0025D9F4 File Offset: 0x0025BDF4
		protected override void OnAction2()
		{
			this.OnAction();
		}

		// Token: 0x060057B3 RID: 22451 RVA: 0x0025D9FC File Offset: 0x0025BDFC
		protected override void ExitAction2()
		{
			this.ExitAction();
		}

		// Token: 0x060057B4 RID: 22452 RVA: 0x0025DA04 File Offset: 0x0025BE04
		protected override void EnterAction3()
		{
			this.EnterAction(delegate()
			{
				this.SetState(AnimalState.Locomotion, null);
			});
		}

		// Token: 0x060057B5 RID: 22453 RVA: 0x0025DA18 File Offset: 0x0025BE18
		protected override void OnAction3()
		{
			this.OnAction();
		}

		// Token: 0x060057B6 RID: 22454 RVA: 0x0025DA20 File Offset: 0x0025BE20
		protected override void ExitAction3()
		{
			this.ExitAction();
		}

		// Token: 0x060057B7 RID: 22455 RVA: 0x0025DA28 File Offset: 0x0025BE28
		protected override void EnterAction4()
		{
			this.EnterAction(delegate()
			{
				this.SetState(AnimalState.Locomotion, null);
			});
		}

		// Token: 0x060057B8 RID: 22456 RVA: 0x0025DA3C File Offset: 0x0025BE3C
		protected override void OnAction4()
		{
			this.OnAction();
		}

		// Token: 0x060057B9 RID: 22457 RVA: 0x0025DA44 File Offset: 0x0025BE44
		protected override void ExitAction4()
		{
			this.ExitAction();
		}

		// Token: 0x040050B3 RID: 20659
		[SerializeField]
		protected AnimalDesireController desireController;

		// Token: 0x040050B4 RID: 20660
		[SerializeField]
		private AnimalSearchActionPoint _searchAction;
	}
}
