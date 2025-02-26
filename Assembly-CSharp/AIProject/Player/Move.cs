using System;
using AIProject.Definitions;
using IllusionUtility.GetUtility;
using Manager;
using UniRx;
using UnityEngine;
using UnityEx;

namespace AIProject.Player
{
	// Token: 0x02000E04 RID: 3588
	public class Move : PlayerStateBase
	{
		// Token: 0x06006F05 RID: 28421 RVA: 0x002F8530 File Offset: 0x002F6930
		protected override void OnAwake(PlayerActor player)
		{
			Move.<OnAwake>c__AnonStorey0 <OnAwake>c__AnonStorey = new Move.<OnAwake>c__AnonStorey0();
			<OnAwake>c__AnonStorey.player = player;
			<OnAwake>c__AnonStorey.player.SetActiveOnEquipedItem(false);
			<OnAwake>c__AnonStorey.player.ChaControl.setAllLayerWeight(0f);
			UnityEx.ValueTuple<int, string> valueTuple;
			AIProject.Definitions.Action.NameTable.TryGetValue(EventType.Move, out valueTuple);
			int item = valueTuple.Item1;
			ActionPointInfo actionPointInfo;
			<OnAwake>c__AnonStorey.player.CurrentPoint.TryGetPlayerActionPointInfo(EventType.Move, out actionPointInfo);
			<OnAwake>c__AnonStorey.player.CurrentPoint.SetBookingUser(<OnAwake>c__AnonStorey.player);
			GameObject gameObject = <OnAwake>c__AnonStorey.player.CurrentPoint.transform.FindLoop(actionPointInfo.baseNullName);
			Transform transform = ((gameObject != null) ? gameObject.transform : null) ?? <OnAwake>c__AnonStorey.player.CurrentPoint.transform;
			GameObject gameObject2 = <OnAwake>c__AnonStorey.player.CurrentPoint.transform.FindLoop(actionPointInfo.recoveryNullName);
			<OnAwake>c__AnonStorey.player.Animation.RecoveryPoint = ((gameObject2 != null) ? gameObject2.transform : null);
			int poseID = actionPointInfo.poseID;
			<OnAwake>c__AnonStorey.player.PoseID = poseID;
			int num = poseID;
			PlayState playState = Singleton<Manager.Resources>.Instance.Animation.PlayerActionAnimTable[(int)<OnAwake>c__AnonStorey.player.ChaControl.sex][item][num];
			<OnAwake>c__AnonStorey.player.DeactivateNavMeshAgent();
			Move.<OnAwake>c__AnonStorey0 <OnAwake>c__AnonStorey2 = <OnAwake>c__AnonStorey;
			ActorAnimInfo animInfo = new ActorAnimInfo
			{
				layer = playState.Layer,
				inEnableBlend = playState.MainStateInfo.InStateInfo.EnableFade,
				inBlendSec = playState.MainStateInfo.InStateInfo.FadeSecond,
				outEnableBlend = playState.MainStateInfo.OutStateInfo.EnableFade,
				outBlendSec = playState.MainStateInfo.OutStateInfo.FadeSecond,
				directionType = playState.DirectionType,
				endEnableBlend = playState.EndEnableBlend,
				endBlendSec = playState.EndBlendRate
			};
			<OnAwake>c__AnonStorey.player.Animation.AnimInfo = animInfo;
			<OnAwake>c__AnonStorey2.animInfo = animInfo;
			<OnAwake>c__AnonStorey.player.Animation.LoadSEEventKeyTable(item, num);
			<OnAwake>c__AnonStorey.player.Animation.InitializeStates(playState.MainStateInfo.InStateInfo.StateInfos, playState.MainStateInfo.OutStateInfo.StateInfos, playState.MainStateInfo.AssetBundleInfo);
			<OnAwake>c__AnonStorey.player.Animation.PlayInAnimation(<OnAwake>c__AnonStorey.animInfo.inEnableBlend, <OnAwake>c__AnonStorey.animInfo.inBlendSec, playState.MainStateInfo.FadeOutTime, <OnAwake>c__AnonStorey.animInfo.layer);
			this._onEndAction.Take(1).Subscribe(delegate(Unit _)
			{
				<OnAwake>c__AnonStorey.player.Animation.PlayOutAnimation(<OnAwake>c__AnonStorey.animInfo.outEnableBlend, <OnAwake>c__AnonStorey.animInfo.outBlendSec, <OnAwake>c__AnonStorey.animInfo.layer);
			});
			<OnAwake>c__AnonStorey.player.SetStand(transform, playState.MainStateInfo.InStateInfo.EnableFade, playState.MainStateInfo.InStateInfo.FadeSecond, playState.DirectionType);
			<OnAwake>c__AnonStorey.player.CameraControl.Mode = CameraMode.ActionNotMove;
			<OnAwake>c__AnonStorey.player.CameraControl.SetShotTypeForce(ShotType.Near);
			<OnAwake>c__AnonStorey.player.CameraControl.LoadActionCameraFile(item, num, transform);
		}

		// Token: 0x06006F06 RID: 28422 RVA: 0x002F886C File Offset: 0x002F6C6C
		protected override void OnRelease(PlayerActor player)
		{
			ActorAnimInfo animInfo = player.Animation.AnimInfo;
			if (player.CurrentPoint != null && player.CurrentPoint.RemoveBooking(player))
			{
				player.CurrentPoint.SetImpossible(false, player);
				CommandArea commandArea = player.PlayerController.CommandArea;
				commandArea.RemoveConsiderationObject(player.CurrentPoint);
				commandArea.RefreshCommands();
			}
			player.SetStand(player.Animation.RecoveryPoint, animInfo.endEnableBlend, animInfo.endBlendSec, animInfo.directionType);
			player.Animation.RefsActAnimInfo = true;
		}

		// Token: 0x06006F07 RID: 28423 RVA: 0x002F8905 File Offset: 0x002F6D05
		protected override void OnAfterUpdate(PlayerActor actor, Actor.InputInfo info)
		{
			actor.CharacterTPS.UpdateState(info, ActorLocomotion.UpdateType.Update);
		}

		// Token: 0x06006F08 RID: 28424 RVA: 0x002F8914 File Offset: 0x002F6D14
		protected override void OnUpdate(PlayerActor actor, ref Actor.InputInfo info)
		{
			if (actor.Animation.PlayingInAnimation)
			{
				return;
			}
			if (this._onEndAction != null)
			{
				this._onEndAction.OnNext(Unit.Default);
			}
			if (actor.Animation.PlayingOutAnimation)
			{
				return;
			}
			this.Elapsed(actor);
		}

		// Token: 0x06006F09 RID: 28425 RVA: 0x002F8968 File Offset: 0x002F6D68
		private void Elapsed(PlayerActor player)
		{
			if (Manager.Map.TutorialMode && Singleton<Manager.Resources>.IsInstance() && Manager.Map.GetTutorialProgress() == 14)
			{
				ActionPoint currentPoint = player.CurrentPoint;
				CommonDefine commonDefine = Singleton<Manager.Resources>.Instance.CommonDefine;
				int? num;
				if (commonDefine == null)
				{
					num = null;
				}
				else
				{
					CommonDefine.TutorialSetting tutorial = commonDefine.Tutorial;
					num = ((tutorial != null) ? new int?(tutorial.YotunbaiRegisterID) : null);
				}
				int? num2 = num;
				if (currentPoint != null && num2 != null && currentPoint.RegisterID == num2.Value)
				{
					Manager.Map.SetTutorialProgress(15);
				}
			}
			if (player.CurrentPoint != null)
			{
				player.CurrentPoint.RemoveBooking(player);
				CommandArea commandArea = player.PlayerController.CommandArea;
				commandArea.RemoveConsiderationObject(player.CurrentPoint);
				commandArea.RefreshCommands();
			}
			if (player.PlayerController.PrevStateName == "Follow")
			{
				player.PlayerController.ChangeState("Follow");
			}
			else
			{
				player.Controller.ChangeState("Normal");
			}
		}

		// Token: 0x04005BD7 RID: 23511
		protected int _currentState = -1;

		// Token: 0x04005BD8 RID: 23512
		private Subject<Unit> _onEndAction = new Subject<Unit>();
	}
}
