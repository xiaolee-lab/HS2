using System;
using System.Collections;
using AIProject.Definitions;
using IllusionUtility.GetUtility;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject.Player
{
	// Token: 0x02000DDD RID: 3549
	public class DateBreak : PlayerStateBase
	{
		// Token: 0x06006DC5 RID: 28101 RVA: 0x002EBD24 File Offset: 0x002EA124
		protected override void OnAwake(PlayerActor player)
		{
			player.EventKey = EventType.Break;
			player.SetActiveOnEquipedItem(false);
			player.ChaControl.setAllLayerWeight(0f);
			int item = AIProject.Definitions.Action.NameTable[EventType.Break].Item1;
			DateActionPointInfo dateActionPointInfo;
			player.CurrentPoint.TryGetPlayerDateActionPointInfo(player.ChaControl.sex, EventType.Break, out dateActionPointInfo);
			int poseIDA = dateActionPointInfo.poseIDA;
			player.PoseID = poseIDA;
			int num = poseIDA;
			GameObject gameObject = player.CurrentPoint.transform.FindLoop(dateActionPointInfo.baseNullNameA);
			Transform transform = ((gameObject != null) ? gameObject.transform : null) ?? player.CurrentPoint.transform;
			GameObject gameObject2 = player.CurrentPoint.transform.FindLoop(dateActionPointInfo.baseNullNameB);
			Transform t = ((gameObject2 != null) ? gameObject2.transform : null) ?? player.CurrentPoint.transform;
			GameObject gameObject3 = player.CurrentPoint.transform.FindLoop(dateActionPointInfo.recoveryNullNameA);
			player.Animation.RecoveryPoint = ((gameObject3 != null) ? gameObject3.transform : null);
			GameObject gameObject4 = player.CurrentPoint.transform.FindLoop(dateActionPointInfo.recoveryNullNameB);
			player.Partner.Animation.RecoveryPoint = ((gameObject4 != null) ? gameObject4.transform : null);
			PlayState info = Singleton<Manager.Resources>.Instance.Animation.PlayerActionAnimTable[(int)player.ChaControl.sex][item][num];
			player.Animation.LoadEventKeyTable(item, dateActionPointInfo.poseIDA);
			player.LoadEventItems(info);
			player.LoadEventParticles(item, dateActionPointInfo.poseIDA);
			player.Animation.InitializeStates(info);
			Actor partner = player.Partner;
			partner.Animation.LoadEventKeyTable(item, dateActionPointInfo.poseIDB);
			PlayState playState = Singleton<Manager.Resources>.Instance.Animation.AgentActionAnimTable[item][dateActionPointInfo.poseIDB];
			partner.LoadEventItems(playState);
			partner.LoadEventParticles(item, dateActionPointInfo.poseIDB);
			partner.Animation.InitializeStates(playState);
			ActorAnimInfo actorAnimInfo = new ActorAnimInfo
			{
				layer = info.Layer,
				inEnableBlend = info.MainStateInfo.InStateInfo.EnableFade,
				inBlendSec = info.MainStateInfo.InStateInfo.FadeSecond,
				outEnableBlend = info.MainStateInfo.OutStateInfo.EnableFade,
				outBlendSec = info.MainStateInfo.OutStateInfo.FadeSecond,
				directionType = info.DirectionType,
				isLoop = info.MainStateInfo.IsLoop,
				endEnableBlend = info.EndEnableBlend,
				endBlendSec = info.EndBlendRate
			};
			player.Animation.AnimInfo = actorAnimInfo;
			ActorAnimInfo actorAnimInfo2 = actorAnimInfo;
			ActorAnimInfo actorAnimInfo3 = new ActorAnimInfo
			{
				layer = playState.Layer,
				inEnableBlend = playState.MainStateInfo.InStateInfo.EnableFade,
				inBlendSec = playState.MainStateInfo.InStateInfo.FadeSecond,
				outEnableBlend = playState.MainStateInfo.OutStateInfo.EnableFade,
				outBlendSec = playState.MainStateInfo.OutStateInfo.FadeSecond,
				directionType = info.DirectionType,
				isLoop = info.MainStateInfo.IsLoop,
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
			this._hasAction = info.ActionInfo.hasAction;
			if (this._hasAction)
			{
				this._loopStateName = info.MainStateInfo.InStateInfo.StateInfos.GetElement(info.MainStateInfo.InStateInfo.StateInfos.Length - 1).stateName;
				this._randomCount = info.ActionInfo.randomCount;
				this._oldNormalizedTime = 0f;
			}
			player.StopAllCoroutines();
			player.Animation.PlayInAnimation(actorAnimInfo2.inEnableBlend, actorAnimInfo2.inBlendSec, info.MainStateInfo.FadeOutTime, actorAnimInfo2.layer);
			player.SetStand(transform, info.MainStateInfo.InStateInfo.EnableFade, info.MainStateInfo.InStateInfo.FadeSecond, info.DirectionType);
			partner.StopAllCoroutines();
			partner.Animation.PlayInAnimation(actorAnimInfo4.inEnableBlend, actorAnimInfo4.inBlendSec, playState.MainStateInfo.FadeOutTime, actorAnimInfo4.layer);
			partner.SetStand(t, playState.MainStateInfo.InStateInfo.EnableFade, playState.MainStateInfo.InStateInfo.FadeSecond, actorAnimInfo4.layer);
			this._onElapseTime.Take(1).Subscribe(delegate(Unit _)
			{
				if (info.SubStateInfos.IsNullOrEmpty<PlayState.PlayStateInfo>())
				{
					return;
				}
				player.Animation.InStates.Clear();
				PlayState.PlayStateInfo element = info.SubStateInfos.GetElement(UnityEngine.Random.Range(0, info.SubStateInfos.Count));
				foreach (PlayState.Info item2 in element.InStateInfo.StateInfos)
				{
					player.Animation.InStates.Enqueue(item2);
				}
			});
			player.OldEnabledHoldingHand = player.HandsHolder.enabled;
			if (player.OldEnabledHoldingHand)
			{
				player.HandsHolder.enabled = false;
				if (player.HandsHolder.EnabledHolding)
				{
					player.HandsHolder.EnabledHolding = false;
				}
			}
			player.CameraControl.Mode = CameraMode.ActionFreeLook;
			player.CameraControl.SetShotTypeForce(ShotType.Near);
			player.CameraControl.LoadActionCameraFile(item, num, transform);
			Manager.Resources instance = Singleton<Manager.Resources>.Instance;
			Sprite actionIcon;
			Singleton<Manager.Resources>.Instance.itemIconTables.ActionIconTable.TryGetValue(instance.PlayerProfile.CommonActionIconID, out actionIcon);
			Observable.Timer(TimeSpan.FromSeconds(1.0)).Subscribe(delegate(long _)
			{
				MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.CancelAcception);
				MapUIContainer.CommandLabel.CancelCommand = new CommandLabel.CommandInfo
				{
					Text = "立ち上がる",
					Icon = actionIcon,
					TargetSpriteInfo = null,
					Transform = null,
					Event = delegate
					{
						MapUIContainer.CommandLabel.CancelCommand = null;
						MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);
						Observable.FromCoroutine(() => player.Controller.State.End(player), false).Take(1).Subscribe(delegate(Unit __)
						{
							player.PlayerController.ChangeState("Normal");
							player.CameraControl.Mode = CameraMode.Normal;
							player.CameraControl.RecoverShotType();
							MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.InvokeAcception);
							AgentActor agentPartner = player.AgentPartner;
							agentPartner.ActivateNavMeshAgent();
							agentPartner.SetActiveOnEquipedItem(true);
							ActorAnimInfo animInfo = agentPartner.Animation.AnimInfo;
							agentPartner.SetStand(agentPartner.Animation.RecoveryPoint, animInfo.endEnableBlend, animInfo.endBlendSec, animInfo.directionType);
							if (player.OldEnabledHoldingHand)
							{
								player.HandsHolder.enabled = true;
								player.OldEnabledHoldingHand = false;
							}
							agentPartner.BehaviorResources.ChangeMode(Desire.ActionType.Date);
						});
					}
				};
			});
		}

		// Token: 0x06006DC6 RID: 28102 RVA: 0x002EC468 File Offset: 0x002EA868
		protected override void OnRelease(PlayerActor player)
		{
			Vector3 locatedPosition = player.CurrentPoint.LocatedPosition;
			locatedPosition.y = player.Locomotor.transform.position.y;
			RaycastHit raycastHit;
			Physics.Raycast(locatedPosition, Vector3.down * 10f, out raycastHit);
			locatedPosition.y = raycastHit.point.y;
			player.Locomotor.transform.position = locatedPosition;
			ActorAnimInfo animInfo = player.Animation.AnimInfo;
			player.SetStand(player.Animation.RecoveryPoint, animInfo.endEnableBlend, animInfo.endBlendSec, animInfo.directionType);
			player.ClearParticles();
			Actor partner = player.Partner;
			if (partner != null)
			{
				partner.ClearParticles();
			}
			player.SetActiveOnEquipedItem(true);
		}

		// Token: 0x06006DC7 RID: 28103 RVA: 0x002EC534 File Offset: 0x002EA934
		protected override void OnUpdate(PlayerActor player, ref Actor.InputInfo info)
		{
			player.NavMeshAgent.velocity = (info.move = Vector3.zero);
			if (player.Animation.PlayingInAnimation)
			{
				return;
			}
			if (player.Animation.Animator.GetCurrentAnimatorStateInfo(0).IsName(this._loopStateName))
			{
				this._elapsedTime += Time.deltaTime;
				if (this._elapsedTime > Singleton<Manager.Resources>.Instance.LocomotionProfile.TimeToLeftState)
				{
				}
			}
		}

		// Token: 0x06006DC8 RID: 28104 RVA: 0x002EC5BB File Offset: 0x002EA9BB
		protected override void OnAfterUpdate(PlayerActor actor, Actor.InputInfo info)
		{
			actor.CharacterTPS.UpdateState(info, ActorLocomotion.UpdateType.Update);
		}

		// Token: 0x06006DC9 RID: 28105 RVA: 0x002EC5CC File Offset: 0x002EA9CC
		protected override IEnumerator OnEnd(PlayerActor player)
		{
			yield break;
		}

		// Token: 0x04005B51 RID: 23377
		private Subject<Unit> _onElapseTime = new Subject<Unit>();
	}
}
