using System;
using System.Collections;
using System.Collections.Generic;
using AIProject.Definitions;
using Manager;
using UniRx;
using UnityEngine;
using UnityEx;

namespace AIProject.Player
{
	// Token: 0x02000E11 RID: 3601
	public class Sleep : PlayerStateBase
	{
		// Token: 0x06006F6D RID: 28525 RVA: 0x002FD22C File Offset: 0x002FB62C
		protected override void OnAwake(PlayerActor player)
		{
			player.EventKey = EventType.Sleep;
			player.SetActiveOnEquipedItem(false);
			player.ChaControl.setAllLayerWeight(0f);
			player.SetScheduledInteractionState(false);
			player.ReleaseInteraction();
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.UI);
			Singleton<Manager.Input>.Instance.SetupState();
			if (player.PlayerController.PrevStateName != "Lie")
			{
				player.PlayActionMotion(EventType.Sleep);
				Observable.Timer(TimeSpan.FromMilliseconds(1000.0)).Subscribe(delegate(long _)
				{
					int id = 7;
					if (MapUIContainer.OpenOnceTutorial(id, false))
					{
						MapUIContainer.TutorialUI.ClosedEvent = delegate()
						{
							this.OnStart(player);
						};
					}
					else
					{
						this.OnStart(player);
					}
				});
				player.CameraControl.SetShotTypeForce(ShotType.Near);
			}
			else
			{
				this.OnStart(player);
			}
			UnityEx.ValueTuple<int, string> valueTuple;
			AIProject.Definitions.Action.NameTable.TryGetValue(EventType.Sleep, out valueTuple);
			int item = valueTuple.Item1;
			player.CameraControl.Mode = CameraMode.ActionFreeLook;
			player.CameraControl.LoadActionCameraFile(item, player.PoseID, null);
		}

		// Token: 0x06006F6E RID: 28526 RVA: 0x002FD360 File Offset: 0x002FB760
		private void OnStart(PlayerActor player)
		{
			MapUIContainer.RefreshCommands(0, player.SleepCommandInfos);
			MapUIContainer.CommandList.CancelEvent = delegate()
			{
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
				ActorAnimInfo animInfo = player.Animation.AnimInfo;
				player.SetStand(player.Animation.RecoveryPoint, animInfo.endEnableBlend, animInfo.endBlendSec, animInfo.directionType);
				player.Animation.RecoveryPoint = null;
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
				MapUIContainer.SetActiveCommandList(false);
			};
			MapUIContainer.SetActiveCommandList(true, "睡眠");
			this._onEndActionDisposable = this._onEndAction.Take(1).Subscribe(delegate(Unit __)
			{
				Dictionary<int, Dictionary<int, Dictionary<int, PlayState>>> playerActionAnimTable = Singleton<Manager.Resources>.Instance.Animation.PlayerActionAnimTable;
				PoseKeyPair wakeupPoseID = Singleton<Manager.Resources>.Instance.PlayerProfile.PoseIDData.WakeupPoseID;
				PlayState playState = playerActionAnimTable[(int)player.ChaControl.sex][wakeupPoseID.postureID][wakeupPoseID.poseID];
				player.Animation.StopAllAnimCoroutine();
				player.Animation.InitializeStates(playState);
				player.ActivateNavMeshAgent();
				player.IsKinematic = false;
				ActorAnimInfo animInfo = player.Animation.AnimInfo;
				player.SetStand(player.Animation.RecoveryPoint, animInfo.endEnableBlend, animInfo.endBlendSec, 0);
				player.Animation.PlayInAnimation(playState.MainStateInfo.InStateInfo.EnableFade, playState.MainStateInfo.InStateInfo.FadeSecond, playState.MainStateInfo.FadeOutTime, playState.Layer);
				player.CameraControl.Mode = CameraMode.Normal;
				player.CameraControl.RecoverShotType();
				player.CameraControl.EnabledInput = true;
			});
			this._onEndInAnimDisposable = this._onEndInAnim.Take(1).Subscribe(delegate(Unit __)
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
			});
		}

		// Token: 0x06006F6F RID: 28527 RVA: 0x002FD3F2 File Offset: 0x002FB7F2
		public override void Release(Actor actor, EventType type)
		{
			this.OnRelease(actor as PlayerActor);
		}

		// Token: 0x06006F70 RID: 28528 RVA: 0x002FD400 File Offset: 0x002FB800
		protected override void OnRelease(PlayerActor player)
		{
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.Action);
			Singleton<Manager.Input>.Instance.SetupState();
			player.SetScheduledInteractionState(true);
			player.ReleaseInteraction();
			if (this._onEndActionDisposable != null)
			{
				this._onEndActionDisposable.Dispose();
			}
			if (this._onEndInAnimDisposable != null)
			{
				this._onEndInAnimDisposable.Dispose();
			}
		}

		// Token: 0x06006F71 RID: 28529 RVA: 0x002FD460 File Offset: 0x002FB860
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
				if (Mathf.Sqrt(moveAxis.x * moveAxis.x + moveAxis.y * moveAxis.y) > 0.5f)
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

		// Token: 0x06006F72 RID: 28530 RVA: 0x002FD536 File Offset: 0x002FB936
		protected override void OnAfterUpdate(PlayerActor actor, Actor.InputInfo info)
		{
			actor.CharacterTPS.UpdateState(info, ActorLocomotion.UpdateType.Update);
		}

		// Token: 0x06006F73 RID: 28531 RVA: 0x002FD545 File Offset: 0x002FB945
		protected override void OnAnimatorStateEnterInternal(PlayerController control, AnimatorStateInfo stateInfo)
		{
		}

		// Token: 0x06006F74 RID: 28532 RVA: 0x002FD547 File Offset: 0x002FB947
		protected override void OnAnimatorStateExitInternal(PlayerController control, AnimatorStateInfo stateInfo)
		{
		}

		// Token: 0x06006F75 RID: 28533 RVA: 0x002FD54C File Offset: 0x002FB94C
		protected override IEnumerator OnEnd(PlayerActor player)
		{
			yield break;
		}

		// Token: 0x04005C0E RID: 23566
		private Subject<Unit> _onEndAction = new Subject<Unit>();

		// Token: 0x04005C0F RID: 23567
		private Subject<Unit> _onEndInAnim = new Subject<Unit>();

		// Token: 0x04005C10 RID: 23568
		private IDisposable _onEndActionDisposable;

		// Token: 0x04005C11 RID: 23569
		private IDisposable _onEndInAnimDisposable;
	}
}
