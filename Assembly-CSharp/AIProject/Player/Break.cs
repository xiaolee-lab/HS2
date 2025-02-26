using System;
using System.Collections;
using AIProject.Definitions;
using IllusionUtility.GetUtility;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject.Player
{
	// Token: 0x02000DE5 RID: 3557
	public class Break : PlayerStateBase
	{
		// Token: 0x06006DF8 RID: 28152 RVA: 0x002EF38C File Offset: 0x002ED78C
		protected override void OnAwake(PlayerActor player)
		{
			Break.<OnAwake>c__AnonStorey1 <OnAwake>c__AnonStorey = new Break.<OnAwake>c__AnonStorey1();
			<OnAwake>c__AnonStorey.player = player;
			<OnAwake>c__AnonStorey.player.EventKey = EventType.Break;
			<OnAwake>c__AnonStorey.player.SetActiveOnEquipedItem(false);
			<OnAwake>c__AnonStorey.player.ChaControl.setAllLayerWeight(0f);
			<OnAwake>c__AnonStorey.player.CurrentPoint.SetActiveMapItemObjs(false);
			int item = AIProject.Definitions.Action.NameTable[EventType.Break].Item1;
			ActionPointInfo actionPointInfo;
			<OnAwake>c__AnonStorey.player.CurrentPoint.TryGetPlayerActionPointInfo(EventType.Break, out actionPointInfo);
			int poseID = actionPointInfo.poseID;
			<OnAwake>c__AnonStorey.player.PoseID = poseID;
			int num = poseID;
			GameObject gameObject = <OnAwake>c__AnonStorey.player.CurrentPoint.transform.FindLoop(actionPointInfo.baseNullName);
			Transform t = ((gameObject != null) ? gameObject.transform : null) ?? <OnAwake>c__AnonStorey.player.CurrentPoint.transform;
			GameObject gameObject2 = <OnAwake>c__AnonStorey.player.CurrentPoint.transform.FindLoop(actionPointInfo.recoveryNullName);
			<OnAwake>c__AnonStorey.player.Animation.RecoveryPoint = ((gameObject2 != null) ? gameObject2.transform : null);
			<OnAwake>c__AnonStorey.info = Singleton<Manager.Resources>.Instance.Animation.PlayerActionAnimTable[(int)<OnAwake>c__AnonStorey.player.ChaControl.sex][item][num];
			<OnAwake>c__AnonStorey.player.Animation.LoadEventKeyTable(item, actionPointInfo.poseID);
			<OnAwake>c__AnonStorey.player.LoadEventItems(<OnAwake>c__AnonStorey.info);
			<OnAwake>c__AnonStorey.player.LoadEventParticles(item, actionPointInfo.poseID);
			<OnAwake>c__AnonStorey.player.Animation.InitializeStates(<OnAwake>c__AnonStorey.info);
			Break.<OnAwake>c__AnonStorey1 <OnAwake>c__AnonStorey2 = <OnAwake>c__AnonStorey;
			ActorAnimInfo animInfo = new ActorAnimInfo
			{
				layer = <OnAwake>c__AnonStorey.info.Layer,
				inEnableBlend = <OnAwake>c__AnonStorey.info.MainStateInfo.InStateInfo.EnableFade,
				inBlendSec = <OnAwake>c__AnonStorey.info.MainStateInfo.InStateInfo.FadeSecond,
				outEnableBlend = <OnAwake>c__AnonStorey.info.MainStateInfo.OutStateInfo.EnableFade,
				outBlendSec = <OnAwake>c__AnonStorey.info.MainStateInfo.OutStateInfo.FadeSecond,
				directionType = <OnAwake>c__AnonStorey.info.DirectionType,
				isLoop = <OnAwake>c__AnonStorey.info.MainStateInfo.IsLoop,
				endEnableBlend = <OnAwake>c__AnonStorey.info.EndEnableBlend,
				endBlendSec = <OnAwake>c__AnonStorey.info.EndBlendRate
			};
			<OnAwake>c__AnonStorey.player.Animation.AnimInfo = animInfo;
			<OnAwake>c__AnonStorey2.animInfo = animInfo;
			<OnAwake>c__AnonStorey.player.DeactivateNavMeshAgent();
			<OnAwake>c__AnonStorey.player.IsKinematic = true;
			this._hasAction = <OnAwake>c__AnonStorey.info.ActionInfo.hasAction;
			if (this._hasAction)
			{
				this._loopStateName = <OnAwake>c__AnonStorey.info.MainStateInfo.InStateInfo.StateInfos.GetElement(<OnAwake>c__AnonStorey.info.MainStateInfo.InStateInfo.StateInfos.Length - 1).stateName;
				this._randomCount = <OnAwake>c__AnonStorey.info.ActionInfo.randomCount;
				this._oldNormalizedTime = 0f;
			}
			<OnAwake>c__AnonStorey.player.Animation.PlayInAnimation(<OnAwake>c__AnonStorey.animInfo.inEnableBlend, <OnAwake>c__AnonStorey.animInfo.inBlendSec, <OnAwake>c__AnonStorey.info.MainStateInfo.FadeOutTime, <OnAwake>c__AnonStorey.animInfo.layer);
			this._onEndAction.Take(1).Subscribe(delegate(Unit _)
			{
				<OnAwake>c__AnonStorey.player.Animation.PlayOutAnimation(<OnAwake>c__AnonStorey.animInfo.outEnableBlend, <OnAwake>c__AnonStorey.animInfo.outBlendSec, <OnAwake>c__AnonStorey.animInfo.layer);
			});
			this._onElapseTime.Take(1).Subscribe(delegate(Unit _)
			{
				if (<OnAwake>c__AnonStorey.info.SubStateInfos.IsNullOrEmpty<PlayState.PlayStateInfo>())
				{
					return;
				}
				<OnAwake>c__AnonStorey.player.Animation.InStates.Clear();
				PlayState.PlayStateInfo element = <OnAwake>c__AnonStorey.info.SubStateInfos.GetElement(UnityEngine.Random.Range(0, <OnAwake>c__AnonStorey.info.SubStateInfos.Count));
				foreach (PlayState.Info item3 in element.InStateInfo.StateInfos)
				{
					<OnAwake>c__AnonStorey.player.Animation.InStates.Enqueue(item3);
				}
				ActorAnimInfo actorAnimInfo = new ActorAnimInfo
				{
					layer = <OnAwake>c__AnonStorey.info.Layer,
					inEnableBlend = element.InStateInfo.EnableFade,
					inBlendSec = element.InStateInfo.FadeSecond,
					outEnableBlend = element.OutStateInfo.EnableFade,
					outBlendSec = element.OutStateInfo.FadeSecond,
					directionType = <OnAwake>c__AnonStorey.info.DirectionType,
					isLoop = element.IsLoop
				};
				<OnAwake>c__AnonStorey.player.Animation.AnimInfo = actorAnimInfo;
				ActorAnimInfo actorAnimInfo2 = actorAnimInfo;
				<OnAwake>c__AnonStorey.player.Animation.AnimABInfo = <OnAwake>c__AnonStorey.info.MainStateInfo.AssetBundleInfo;
				<OnAwake>c__AnonStorey.player.Animation.PlayInAnimation(actorAnimInfo2.inEnableBlend, actorAnimInfo2.inBlendSec, element.FadeOutTime, actorAnimInfo2.layer);
			});
			Tuple<Transform, Transform> tuple = <OnAwake>c__AnonStorey.player.CurrentPoint.SetSlot(<OnAwake>c__AnonStorey.player);
			Transform item2 = tuple.Item1;
			<OnAwake>c__AnonStorey.player.SetStand(t, <OnAwake>c__AnonStorey.animInfo.inEnableBlend, <OnAwake>c__AnonStorey.animInfo.inBlendSec, <OnAwake>c__AnonStorey.animInfo.directionType);
			<OnAwake>c__AnonStorey.player.CameraControl.Mode = CameraMode.ActionFreeLook;
			<OnAwake>c__AnonStorey.player.CameraControl.SetShotTypeForce(ShotType.Near);
			<OnAwake>c__AnonStorey.player.CameraControl.LoadActionCameraFile(item, num, null);
			MapUIContainer.StorySupportUI.Close();
			Observable.Timer(TimeSpan.FromMilliseconds(1000.0)).Subscribe(delegate(long _)
			{
				Manager.Resources instance = Singleton<Manager.Resources>.Instance;
				Sprite icon;
				Singleton<Manager.Resources>.Instance.itemIconTables.ActionIconTable.TryGetValue(instance.PlayerProfile.CommonActionIconID, out icon);
				MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.CancelAcception);
				MapUIContainer.CommandLabel.CancelCommand = new CommandLabel.CommandInfo
				{
					Text = "立ち上がる",
					Icon = icon,
					TargetSpriteInfo = null,
					Transform = null,
					Event = delegate
					{
						MapUIContainer.CommandLabel.CancelCommand = null;
						MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);
						Observable.FromCoroutine(() => <OnAwake>c__AnonStorey.player.Controller.State.End(<OnAwake>c__AnonStorey.player), false).Take(1).Subscribe(delegate(Unit __)
						{
							<OnAwake>c__AnonStorey.player.PlayerController.ChangeState("Normal");
							<OnAwake>c__AnonStorey.player.CameraControl.Mode = CameraMode.Normal;
							MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.InvokeAcception);
							MapUIContainer.StorySupportUI.Open();
						});
					}
				};
			});
		}

		// Token: 0x06006DF9 RID: 28153 RVA: 0x002EF804 File Offset: 0x002EDC04
		protected override void OnRelease(PlayerActor player)
		{
			player.ClearItems();
			player.ClearParticles();
			if (player.CurrentPoint != null)
			{
				player.CurrentPoint.SetActiveMapItemObjs(true);
			}
			ActorAnimInfo animInfo = player.Animation.AnimInfo;
			player.SetStand(player.Animation.RecoveryPoint, animInfo.endEnableBlend, animInfo.endBlendSec, animInfo.directionType);
		}

		// Token: 0x06006DFA RID: 28154 RVA: 0x002EF86C File Offset: 0x002EDC6C
		protected override void OnAfterUpdate(PlayerActor actor, Actor.InputInfo info)
		{
			actor.CharacterTPS.UpdateState(info, ActorLocomotion.UpdateType.Update);
		}

		// Token: 0x06006DFB RID: 28155 RVA: 0x002EF87C File Offset: 0x002EDC7C
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
					this._onElapseTime.OnNext(Unit.Default);
				}
			}
		}

		// Token: 0x06006DFC RID: 28156 RVA: 0x002EF914 File Offset: 0x002EDD14
		protected override IEnumerator OnEnd(PlayerActor player)
		{
			ActorAnimInfo animInfo = player.Animation.AnimInfo;
			player.Animation.PlayOutAnimation(animInfo.outEnableBlend, animInfo.outBlendSec, animInfo.layer);
			while (player.Animation.PlayingOutAnimation)
			{
				yield return null;
			}
			base.FadeOutActionAsObservable(player, (int)player.ChaControl.sex, player.Animation.RecoveryPoint, player.CurrentPoint);
			yield break;
		}

		// Token: 0x04005B5F RID: 23391
		private Subject<Unit> _onEndAction = new Subject<Unit>();

		// Token: 0x04005B60 RID: 23392
		private Subject<Unit> _onElapseTime = new Subject<Unit>();
	}
}
