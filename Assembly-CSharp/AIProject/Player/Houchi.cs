using System;
using System.Collections;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject.Player
{
	// Token: 0x02000DFD RID: 3581
	public class Houchi : PlayerStateBase
	{
		// Token: 0x06006ED3 RID: 28371 RVA: 0x002F6980 File Offset: 0x002F4D80
		protected override void OnAwake(PlayerActor player)
		{
			player.EventKey = (EventType)0;
			player.SetActiveOnEquipedItem(false);
			player.ChaControl.setAllLayerWeight(0f);
			Manager.Resources instance = Singleton<Manager.Resources>.Instance;
			PoseKeyPair leftPoseID = instance.PlayerProfile.PoseIDData.LeftPoseID;
			PlayState playState = instance.Animation.PlayerActionAnimTable[(int)player.ChaControl.sex][leftPoseID.postureID][leftPoseID.poseID];
			player.Animation.InitializeStates(playState);
			ActorAnimInfo actorAnimInfo = new ActorAnimInfo
			{
				layer = playState.Layer,
				inEnableBlend = playState.MainStateInfo.InStateInfo.EnableFade,
				inBlendSec = playState.MainStateInfo.InStateInfo.FadeSecond,
				outEnableBlend = playState.MainStateInfo.OutStateInfo.EnableFade,
				outBlendSec = playState.MainStateInfo.OutStateInfo.FadeSecond,
				directionType = playState.DirectionType,
				isLoop = playState.MainStateInfo.IsLoop,
				endEnableBlend = playState.EndEnableBlend,
				endBlendSec = playState.EndBlendRate
			};
			player.Animation.AnimInfo = actorAnimInfo;
			ActorAnimInfo actorAnimInfo2 = actorAnimInfo;
			this._hasAction = playState.ActionInfo.hasAction;
			if (this._hasAction)
			{
				this._loopStateName = playState.MainStateInfo.InStateInfo.StateInfos.GetElement(playState.MainStateInfo.InStateInfo.StateInfos.Length - 1).stateName;
				this._randomCount = playState.ActionInfo.randomCount;
				this._oldNormalizedTime = 0f;
			}
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
			player.Animation.StopAllAnimCoroutine();
			player.Animation.PlayInLocoAnimation(actorAnimInfo2.inEnableBlend, actorAnimInfo2.inBlendSec, actorAnimInfo2.layer);
			this._onEndInput.Take(1).Subscribe(delegate(Unit __)
			{
				player.CameraControl.CrossFade.FadeStart(-1f);
				if (player.Animation.PlayingInLocoAnimation)
				{
					player.Animation.StopInLocoAnimCoroutine();
				}
				if (player.Animation.PlayingActAnimation)
				{
					player.Animation.StopActionAnimCoroutine();
				}
				player.CameraControl.Mode = CameraMode.Normal;
				player.Controller.ChangeState("Normal");
				MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.InvokeAcception);
			});
		}

		// Token: 0x06006ED4 RID: 28372 RVA: 0x002F6C19 File Offset: 0x002F5019
		protected override void OnRelease(PlayerActor player)
		{
			base.OnRelease(player);
			if (player.OldEnabledHoldingHand)
			{
				player.HandsHolder.enabled = true;
				player.OldEnabledHoldingHand = false;
			}
		}

		// Token: 0x06006ED5 RID: 28373 RVA: 0x002F6C40 File Offset: 0x002F5040
		protected override void OnUpdate(PlayerActor actor, ref Actor.InputInfo info)
		{
			Singleton<Map>.Instance.CheckStoryProgress();
			Vector2 moveAxis = Singleton<Manager.Input>.Instance.MoveAxis;
			if (Math.Sqrt((double)(moveAxis.x * moveAxis.x + moveAxis.y * moveAxis.y)) > 0.5)
			{
				this._onEndInput.OnNext(Unit.Default);
			}
			if (actor.Animation.PlayingInLocoAnimation)
			{
				return;
			}
			if (!actor.Animation.PlayingActAnimation)
			{
				AnimatorStateInfo currentAnimatorStateInfo = actor.Animation.Animator.GetCurrentAnimatorStateInfo(0);
				if (currentAnimatorStateInfo.IsName(this._loopStateName))
				{
					float num = currentAnimatorStateInfo.normalizedTime - this._oldNormalizedTime;
					if (num > 1f)
					{
						this._oldNormalizedTime = currentAnimatorStateInfo.normalizedTime;
						if (UnityEngine.Random.Range(0, this._randomCount) == 0)
						{
							actor.Animation.PlayActionAnimation(actor.Animation.AnimInfo.layer);
							this._oldNormalizedTime = 0f;
						}
					}
				}
			}
		}

		// Token: 0x06006ED6 RID: 28374 RVA: 0x002F6D50 File Offset: 0x002F5150
		protected override IEnumerator OnEnd(PlayerActor player)
		{
			ActorAnimInfo animInfo = player.Animation.AnimInfo;
			player.Animation.PlayOutAnimation(animInfo.outEnableBlend, animInfo.outBlendSec, animInfo.layer);
			while (player.Animation.PlayingOutAnimation)
			{
				yield return null;
			}
			yield break;
		}

		// Token: 0x04005BC7 RID: 23495
		private Subject<Unit> _onEndInput = new Subject<Unit>();
	}
}
