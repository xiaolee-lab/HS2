using System;
using System.Collections;
using System.Collections.Generic;
using AIProject.Definitions;
using IllusionUtility.GetUtility;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject.Player
{
	// Token: 0x02000DE1 RID: 3553
	public class DateSleep : PlayerStateBase
	{
		// Token: 0x06006DDF RID: 28127 RVA: 0x002EE358 File Offset: 0x002EC758
		protected override void OnAwake(PlayerActor player)
		{
			player.EventKey = EventType.Sleep;
			player.SetActiveOnEquipedItem(false);
			player.ChaControl.setAllLayerWeight(0f);
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.UI);
			Singleton<Manager.Input>.Instance.SetupState();
			MapUIContainer.SetVisibleHUDExceptStoryUI(false);
			MapUIContainer.StorySupportUI.Close();
			Observable.Timer(TimeSpan.FromMilliseconds(1000.0)).Subscribe(delegate(long _)
			{
				this.OnStart(player);
			});
			int item = AIProject.Definitions.Action.NameTable[EventType.Sleep].Item1;
			DateActionPointInfo apInfo;
			player.CurrentPoint.TryGetPlayerDateActionPointInfo(player.ChaControl.sex, EventType.Sleep, out apInfo);
			int poseIDA = apInfo.poseIDA;
			player.PoseID = poseIDA;
			int key = poseIDA;
			GameObject gameObject = player.CurrentPoint.transform.FindLoop(apInfo.baseNullNameA);
			Transform t = ((gameObject != null) ? gameObject.transform : null) ?? player.CurrentPoint.transform;
			GameObject gameObject2 = player.CurrentPoint.transform.FindLoop(apInfo.baseNullNameB);
			Transform t2 = ((gameObject2 != null) ? gameObject2.transform : null) ?? player.CurrentPoint.transform;
			GameObject gameObject3 = player.CurrentPoint.transform.FindLoop(apInfo.recoveryNullNameA);
			player.Animation.RecoveryPoint = ((gameObject3 != null) ? gameObject3.transform : null);
			GameObject gameObject4 = player.CurrentPoint.transform.FindLoop(apInfo.recoveryNullNameB);
			ActorAnimation animation = player.Partner.Animation;
			Transform recoveryPoint = (gameObject4 != null) ? gameObject4.transform : null;
			player.Partner.Animation.RecoveryPoint = recoveryPoint;
			animation.RecoveryPoint = recoveryPoint;
			PlayState playState = Singleton<Manager.Resources>.Instance.Animation.PlayerActionAnimTable[(int)player.ChaControl.sex][item][key];
			player.Animation.LoadEventKeyTable(item, apInfo.poseIDA);
			player.LoadEventItems(playState);
			player.LoadEventParticles(item, apInfo.poseIDA);
			player.Animation.InitializeStates(playState);
			Actor partner = player.Partner;
			partner.Animation.LoadEventKeyTable(item, apInfo.poseIDB);
			PlayState playState2 = Singleton<Manager.Resources>.Instance.Animation.AgentActionAnimTable[item][apInfo.poseIDB];
			partner.LoadEventItems(playState2);
			partner.LoadEventParticles(item, apInfo.poseIDB);
			partner.Animation.InitializeStates(playState2);
			ActorAnimInfo actorAnimInfo = new ActorAnimInfo
			{
				layer = playState.Layer,
				inEnableBlend = playState.MainStateInfo.InStateInfo.EnableFade,
				inBlendSec = playState.MainStateInfo.InStateInfo.FadeSecond,
				outEnableBlend = playState.MainStateInfo.OutStateInfo.EnableFade,
				outBlendSec = playState.MainStateInfo.OutStateInfo.FadeSecond,
				directionType = playState.DirectionType,
				isLoop = playState.MainStateInfo.IsLoop
			};
			player.Partner.Animation.AnimInfo = actorAnimInfo;
			actorAnimInfo = actorAnimInfo;
			player.Animation.AnimInfo = actorAnimInfo;
			ActorAnimInfo actorAnimInfo2 = actorAnimInfo;
			ActorAnimInfo actorAnimInfo3 = new ActorAnimInfo
			{
				layer = playState2.Layer,
				inEnableBlend = playState2.MainStateInfo.InStateInfo.EnableFade,
				inBlendSec = playState2.MainStateInfo.InStateInfo.FadeSecond,
				outEnableBlend = playState2.MainStateInfo.OutStateInfo.EnableFade,
				outBlendSec = playState2.MainStateInfo.OutStateInfo.FadeSecond,
				directionType = playState2.DirectionType,
				isLoop = playState2.MainStateInfo.IsLoop,
				loopMinTime = playState.MainStateInfo.LoopMin,
				loopMaxTime = playState.MainStateInfo.LoopMax,
				hasAction = playState.ActionInfo.hasAction
			};
			partner.Animation.AnimInfo = actorAnimInfo3;
			ActorAnimInfo actorAnimInfo4 = actorAnimInfo3;
			player.DeactivateNavMeshAgent();
			player.IsKinematic = true;
			partner.SetActiveOnEquipedItem(false);
			partner.ChaControl.setAllLayerWeight(0f);
			partner.DeactivateNavMeshAgent();
			partner.IsKinematic = true;
			this._hasAction = playState.ActionInfo.hasAction;
			if (this._hasAction)
			{
				this._loopStateName = playState.MainStateInfo.InStateInfo.StateInfos.GetElement(playState.MainStateInfo.InStateInfo.StateInfos.Length - 1).stateName;
				this._randomCount = playState.ActionInfo.randomCount;
				this._oldNormalizedTime = 0f;
			}
			player.Animation.PlayInAnimation(actorAnimInfo2.inEnableBlend, actorAnimInfo2.inBlendSec, playState.MainStateInfo.FadeOutTime, actorAnimInfo2.layer);
			player.SetStand(t, playState.MainStateInfo.InStateInfo.EnableFade, playState.MainStateInfo.InStateInfo.FadeSecond, playState.DirectionType);
			partner.Animation.PlayInAnimation(actorAnimInfo2.inEnableBlend, actorAnimInfo2.inBlendSec, playState2.MainStateInfo.FadeOutTime, actorAnimInfo2.layer);
			partner.SetStand(t2, actorAnimInfo4.inEnableBlend, actorAnimInfo4.inBlendSec, actorAnimInfo2.layer);
			Observable.EveryLateUpdate().Skip(1).Take(1).Subscribe(delegate(long _)
			{
				if (apInfo.pointID == 501)
				{
					ADV.ChangeADVFixedAngleCamera(player, 5);
				}
				else if (apInfo.pointID == 500)
				{
					ADV.ChangeADVFixedAngleCamera(partner, 5);
				}
			});
			bool enabled = player.HandsHolder.enabled;
			player.OldEnabledHoldingHand = enabled;
			bool flag = enabled;
			if (flag)
			{
				player.HandsHolder.enabled = false;
				if (player.HandsHolder.EnabledHolding)
				{
					player.HandsHolder.EnabledHolding = false;
				}
			}
			player.CameraControl.SetShotTypeForce(ShotType.Near);
		}

		// Token: 0x06006DE0 RID: 28128 RVA: 0x002EEA6C File Offset: 0x002ECE6C
		private void OnStart(PlayerActor player)
		{
			MapUIContainer.RefreshCommands(0, player.CoSleepCommandInfos);
			MapUIContainer.CommandList.CancelEvent = delegate()
			{
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
				player.CancelCommand();
				MapUIContainer.SetVisibleHUDExceptStoryUI(true);
				MapUIContainer.StorySupportUI.Open();
			};
			MapUIContainer.SetActiveCommandList(true, "睡眠");
			this._onEndActionDisposable = this._onEndAction.Take(1).Subscribe(delegate(Unit _)
			{
				Dictionary<int, Dictionary<int, Dictionary<int, PlayState>>> playerActionAnimTable = Singleton<Manager.Resources>.Instance.Animation.PlayerActionAnimTable;
				PoseKeyPair wakeupPoseID = Singleton<Manager.Resources>.Instance.PlayerProfile.PoseIDData.WakeupPoseID;
				PlayState playState = playerActionAnimTable[(int)player.ChaControl.sex][wakeupPoseID.postureID][wakeupPoseID.poseID];
				player.Animation.StopAllAnimCoroutine();
				player.Animation.InitializeStates(playState);
				player.SetStand(player.Animation.RecoveryPoint, playState.MainStateInfo.InStateInfo.EnableFade, playState.MainStateInfo.InStateInfo.FadeSecond, 0);
				player.Animation.PlayInAnimation(playState.MainStateInfo.InStateInfo.EnableFade, playState.MainStateInfo.InStateInfo.FadeSecond, playState.MainStateInfo.FadeOutTime, playState.Layer);
				player.CameraControl.Mode = CameraMode.Normal;
				player.CameraControl.RecoverShotType();
				player.CameraControl.EnabledInput = true;
			});
			this._onEndInAnimDisposable = this._onEndInAnim.Take(1).Subscribe(delegate(Unit _)
			{
				ActorAnimInfo animInfo = player.Animation.AnimInfo;
				player.SetStand(player.Animation.RecoveryPoint, animInfo.endEnableBlend, animInfo.endBlendSec, animInfo.directionType);
				player.Animation.RefsActAnimInfo = true;
				player.Controller.ChangeState("Normal");
				player.ReleaseCurrentPoint();
				if (player.PlayerController.CommandArea != null)
				{
					player.PlayerController.CommandArea.enabled = true;
				}
				MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.InvokeAcception);
				player.ActivateNavMeshAgent();
				player.IsKinematic = false;
			});
		}

		// Token: 0x06006DE1 RID: 28129 RVA: 0x002EEAFE File Offset: 0x002ECEFE
		public override void Release(Actor actor, EventType type)
		{
			this.OnRelease(actor as PlayerActor);
		}

		// Token: 0x06006DE2 RID: 28130 RVA: 0x002EEB0C File Offset: 0x002ECF0C
		protected override void OnRelease(PlayerActor player)
		{
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.Action);
			Singleton<Manager.Input>.Instance.SetupState();
			player.SetScheduledInteractionState(true);
			player.ReleaseInteraction();
			player.ClearParticles();
			Actor partner = player.Partner;
			if (partner != null)
			{
				partner.ClearParticles();
			}
			if (this._onEndActionDisposable != null)
			{
				this._onEndActionDisposable.Dispose();
			}
			if (this._onEndInAnimDisposable != null)
			{
				this._onEndInAnimDisposable.Dispose();
			}
		}

		// Token: 0x06006DE3 RID: 28131 RVA: 0x002EEB88 File Offset: 0x002ECF88
		protected override void OnUpdate(PlayerActor player, ref Actor.InputInfo info)
		{
			player.NavMeshAgent.velocity = (info.move = Vector3.zero);
			if (MapUIContainer.CommandList.IsActiveControl)
			{
				return;
			}
			if (player.ProcessingTimeSkip)
			{
				return;
			}
			if (this._onEndAction != null)
			{
				this._onEndAction.OnNext(Unit.Default);
			}
			if (player.Animation.PlayingInAnimation)
			{
				Vector2 moveAxis = Singleton<Manager.Input>.Instance.MoveAxis;
				if (Mathf.Sqrt(moveAxis.x * moveAxis.x + moveAxis.y + moveAxis.y) > 0.5f)
				{
					this._onEndInAnim.OnNext(Unit.Default);
				}
				return;
			}
			if (this._onEndInAnim != null)
			{
				this._onEndInAnim.OnNext(Unit.Default);
			}
		}

		// Token: 0x06006DE4 RID: 28132 RVA: 0x002EEC5E File Offset: 0x002ED05E
		protected override void OnAfterUpdate(PlayerActor actor, Actor.InputInfo info)
		{
			actor.CharacterTPS.UpdateState(info, ActorLocomotion.UpdateType.Update);
		}

		// Token: 0x06006DE5 RID: 28133 RVA: 0x002EEC70 File Offset: 0x002ED070
		protected override IEnumerator OnEnd(PlayerActor player)
		{
			yield break;
		}

		// Token: 0x04005B5B RID: 23387
		private Subject<Unit> _onEndAction = new Subject<Unit>();

		// Token: 0x04005B5C RID: 23388
		private Subject<Unit> _onEndInAnim = new Subject<Unit>();

		// Token: 0x04005B5D RID: 23389
		private IDisposable _onEndActionDisposable;

		// Token: 0x04005B5E RID: 23390
		private IDisposable _onEndInAnimDisposable;
	}
}
