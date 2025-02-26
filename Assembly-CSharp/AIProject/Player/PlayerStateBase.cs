using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using ReMotion;
using UniRx;
using UnityEngine;

namespace AIProject.Player
{
	// Token: 0x02000E17 RID: 3607
	public abstract class PlayerStateBase : StateBase, IPlayer
	{
		// Token: 0x1700155B RID: 5467
		// (get) Token: 0x06006F95 RID: 28565 RVA: 0x002EB303 File Offset: 0x002E9703
		// (set) Token: 0x06006F96 RID: 28566 RVA: 0x002EB30B File Offset: 0x002E970B
		public Action OnCompleted { get; set; }

		// Token: 0x06006F97 RID: 28567 RVA: 0x002EB314 File Offset: 0x002E9714
		public override void Awake(Actor actor)
		{
			Actor.InputInfo stateInfo = actor.StateInfo;
			stateInfo.move = Vector3.zero;
			actor.StateInfo = stateInfo;
			if (actor is PlayerActor)
			{
				this.OnAwake(actor as PlayerActor);
			}
		}

		// Token: 0x06006F98 RID: 28568
		protected abstract void OnAwake(PlayerActor player);

		// Token: 0x06006F99 RID: 28569 RVA: 0x002EB354 File Offset: 0x002E9754
		public override void Release(Actor actor, EventType type)
		{
			PlayerActor playerActor = actor as PlayerActor;
			this.OnRelease(playerActor);
			if (!(this is Normal) && !(this is Houchi) && !(this is Onbu))
			{
				playerActor.ReleaseCurrentPoint();
				if (playerActor.PlayerController.CommandArea != null)
				{
					playerActor.PlayerController.CommandArea.enabled = true;
				}
				MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.InvokeAcception);
			}
			actor.ActivateNavMeshAgent();
			actor.IsKinematic = false;
		}

		// Token: 0x06006F9A RID: 28570 RVA: 0x002EB3D0 File Offset: 0x002E97D0
		protected virtual void OnRelease(PlayerActor player)
		{
		}

		// Token: 0x06006F9B RID: 28571 RVA: 0x002EB3D2 File Offset: 0x002E97D2
		public override void AfterUpdate(Actor actor, Actor.InputInfo info)
		{
			if (actor is PlayerActor)
			{
				this.OnAfterUpdate(actor as PlayerActor, info);
			}
		}

		// Token: 0x06006F9C RID: 28572 RVA: 0x002EB3EC File Offset: 0x002E97EC
		protected virtual void OnAfterUpdate(PlayerActor player, Actor.InputInfo info)
		{
		}

		// Token: 0x06006F9D RID: 28573 RVA: 0x002EB3EE File Offset: 0x002E97EE
		public override void Update(Actor actor, ref Actor.InputInfo info)
		{
			if (actor is PlayerActor)
			{
				this.OnUpdate(actor as PlayerActor, ref info);
			}
		}

		// Token: 0x06006F9E RID: 28574
		protected abstract void OnUpdate(PlayerActor player, ref Actor.InputInfo info);

		// Token: 0x06006F9F RID: 28575 RVA: 0x002EB408 File Offset: 0x002E9808
		public override void FixedUpdate(Actor actor, Actor.InputInfo info)
		{
			if (actor is PlayerActor)
			{
				this.OnFixedUpdate(actor as PlayerActor, info);
			}
		}

		// Token: 0x06006FA0 RID: 28576 RVA: 0x002EB422 File Offset: 0x002E9822
		protected virtual void OnFixedUpdate(PlayerActor player, Actor.InputInfo info)
		{
		}

		// Token: 0x06006FA1 RID: 28577 RVA: 0x002EB424 File Offset: 0x002E9824
		public override void OnAnimatorStateEnter(ActorController control, AnimatorStateInfo stateInfo)
		{
			if (control is PlayerController)
			{
				this.OnAnimatorStateEnterInternal(control as PlayerController, stateInfo);
			}
		}

		// Token: 0x06006FA2 RID: 28578 RVA: 0x002EB43E File Offset: 0x002E983E
		protected virtual void OnAnimatorStateEnterInternal(PlayerController control, AnimatorStateInfo stateInfo)
		{
		}

		// Token: 0x06006FA3 RID: 28579 RVA: 0x002EB440 File Offset: 0x002E9840
		public override void OnAnimatorStateExit(ActorController control, AnimatorStateInfo stateInfo)
		{
			if (control is PlayerController)
			{
				this.OnAnimatorStateExitInternal(control as PlayerController, stateInfo);
			}
		}

		// Token: 0x06006FA4 RID: 28580 RVA: 0x002EB45A File Offset: 0x002E985A
		protected virtual void OnAnimatorStateExitInternal(PlayerController control, AnimatorStateInfo stateInfo)
		{
		}

		// Token: 0x06006FA5 RID: 28581 RVA: 0x002EB45C File Offset: 0x002E985C
		public override IEnumerator End(Actor actor)
		{
			if (actor is PlayerActor)
			{
				yield return this.OnEnd(actor as PlayerActor);
				yield break;
			}
			yield break;
		}

		// Token: 0x06006FA6 RID: 28582 RVA: 0x002EB480 File Offset: 0x002E9880
		protected virtual IEnumerator OnEnd(PlayerActor player)
		{
			yield break;
		}

		// Token: 0x06006FA7 RID: 28583 RVA: 0x002EB494 File Offset: 0x002E9894
		protected IObservable<TimeInterval<float>> FadeOutActionAsObservable(PlayerActor actor, int sex, Transform t, ActionPoint actionPoint)
		{
			if (t != null)
			{
				Vector3 position = actor.Position;
				Quaternion rotation = actor.Rotation;
				ActionPointInfo actionPointInfo;
				actionPoint.TryGetPlayerActionPointInfo(actor.EventKey, out actionPointInfo);
				Dictionary<int, Dictionary<int, PlayState>> dictionary;
				Dictionary<int, PlayState> dictionary2;
				PlayState playState;
				if (Singleton<Manager.Resources>.Instance.Animation.PlayerActionAnimTable.TryGetValue(sex, out dictionary) && dictionary.TryGetValue(actionPointInfo.eventID, out dictionary2) && dictionary2.TryGetValue(actionPointInfo.poseID, out playState))
				{
					IConnectableObservable<TimeInterval<float>> connectableObservable = ObservableEasing.Linear(playState.MainStateInfo.OutStateInfo.FadeSecond, false).FrameTimeInterval(false).Publish<TimeInterval<float>>();
					connectableObservable.Connect();
					int directionType = playState.DirectionType;
					if (directionType != 0)
					{
						if (directionType == 1)
						{
							Quaternion lookRotation = Quaternion.LookRotation(actionPoint.Position - position);
							if (playState.MainStateInfo.OutStateInfo.EnableFade)
							{
								connectableObservable.Subscribe(delegate(TimeInterval<float> x)
								{
									actor.Rotation = Quaternion.Slerp(rotation, lookRotation, x.Value);
								});
							}
							else
							{
								actor.Rotation = lookRotation;
							}
						}
					}
					else if (playState.MainStateInfo.OutStateInfo.EnableFade)
					{
						connectableObservable.Subscribe(delegate(TimeInterval<float> x)
						{
							actor.Position = Vector3.Lerp(position, t.position, x.Value);
							actor.Rotation = Quaternion.Slerp(rotation, t.rotation, x.Value);
						});
					}
					else
					{
						actor.Position = t.position;
						actor.Rotation = t.rotation;
					}
					return connectableObservable;
				}
			}
			return Observable.Empty<TimeInterval<float>>();
		}

		// Token: 0x04005C18 RID: 23576
		protected DateTime _startTime = DateTime.MinValue;

		// Token: 0x04005C19 RID: 23577
		protected TimeSpan _totalMinute = TimeSpan.MinValue;

		// Token: 0x04005C1A RID: 23578
		protected float _duration;

		// Token: 0x04005C1B RID: 23579
		protected float _elapsedTime;

		// Token: 0x04005C1C RID: 23580
		protected bool _hasAction;

		// Token: 0x04005C1D RID: 23581
		protected string _loopStateName = string.Empty;

		// Token: 0x04005C1E RID: 23582
		protected int _randomCount;

		// Token: 0x04005C1F RID: 23583
		protected float _oldNormalizedTime;
	}
}
