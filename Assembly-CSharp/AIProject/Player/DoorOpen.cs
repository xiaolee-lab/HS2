using System;
using AIProject.Definitions;
using IllusionUtility.GetUtility;
using Manager;
using UniRx;
using UnityEngine;
using UnityEx;

namespace AIProject.Player
{
	// Token: 0x02000DF3 RID: 3571
	public class DoorOpen : PlayerStateBase
	{
		// Token: 0x06006E5C RID: 28252 RVA: 0x002F359C File Offset: 0x002F199C
		protected override void OnAwake(PlayerActor player)
		{
			DoorOpen.<OnAwake>c__AnonStorey0 <OnAwake>c__AnonStorey = new DoorOpen.<OnAwake>c__AnonStorey0();
			<OnAwake>c__AnonStorey.player = player;
			<OnAwake>c__AnonStorey.player.EventKey = EventType.DoorOpen;
			<OnAwake>c__AnonStorey.player.SetActiveOnEquipedItem(false);
			<OnAwake>c__AnonStorey.player.ChaControl.setAllLayerWeight(0f);
			UnityEx.ValueTuple<int, string> valueTuple;
			AIProject.Definitions.Action.NameTable.TryGetValue(EventType.DoorOpen, out valueTuple);
			int item = valueTuple.Item1;
			ActionPointInfo actionPointInfo;
			<OnAwake>c__AnonStorey.player.CurrentPoint.TryGetPlayerActionPointInfo(EventType.DoorOpen, out actionPointInfo);
			<OnAwake>c__AnonStorey.player.CurrentPoint.SetBookingUser(<OnAwake>c__AnonStorey.player);
			int poseID = actionPointInfo.poseID;
			<OnAwake>c__AnonStorey.player.PoseID = poseID;
			int key = poseID;
			PlayState playState = Singleton<Manager.Resources>.Instance.Animation.PlayerActionAnimTable[(int)<OnAwake>c__AnonStorey.player.ChaControl.sex][item][key];
			DoorOpen.<OnAwake>c__AnonStorey0 <OnAwake>c__AnonStorey2 = <OnAwake>c__AnonStorey;
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
			string prevStateName = <OnAwake>c__AnonStorey.player.PlayerController.PrevStateName;
			bool flag = prevStateName != "Onbu";
			if (flag)
			{
				GameObject gameObject = <OnAwake>c__AnonStorey.player.CurrentPoint.transform.FindLoop(actionPointInfo.baseNullName);
				Transform t = ((gameObject != null) ? gameObject.transform : null) ?? <OnAwake>c__AnonStorey.player.CurrentPoint.transform;
				GameObject gameObject2 = <OnAwake>c__AnonStorey.player.CurrentPoint.transform.FindLoop(actionPointInfo.recoveryNullName);
				<OnAwake>c__AnonStorey.player.Animation.RecoveryPoint = ((gameObject2 != null) ? gameObject2.transform : null);
				<OnAwake>c__AnonStorey.player.Animation.InitializeStates(playState.MainStateInfo.InStateInfo.StateInfos, playState.MainStateInfo.OutStateInfo.StateInfos, playState.MainStateInfo.AssetBundleInfo);
				<OnAwake>c__AnonStorey.player.Animation.LoadAnimatorIfNotEquals(playState);
				<OnAwake>c__AnonStorey.player.Animation.PlayInAnimation(<OnAwake>c__AnonStorey.animInfo.inEnableBlend, <OnAwake>c__AnonStorey.animInfo.inBlendSec, playState.MainStateInfo.FadeOutTime, <OnAwake>c__AnonStorey.animInfo.layer);
				this._onEndAction.Take(1).Subscribe(delegate(Unit _)
				{
					<OnAwake>c__AnonStorey.player.Animation.PlayOutAnimation(<OnAwake>c__AnonStorey.animInfo.outEnableBlend, <OnAwake>c__AnonStorey.animInfo.outBlendSec, <OnAwake>c__AnonStorey.animInfo.layer);
				});
				<OnAwake>c__AnonStorey.player.SetStand(t, playState.MainStateInfo.InStateInfo.EnableFade, playState.MainStateInfo.InStateInfo.FadeSecond, playState.DirectionType);
			}
			<OnAwake>c__AnonStorey.player.Animation.LoadSEEventKeyTable(actionPointInfo.eventID, actionPointInfo.poseID);
			if (<OnAwake>c__AnonStorey.player.CurrentPoint != null)
			{
				this._doorAnimation = <OnAwake>c__AnonStorey.player.CurrentPoint.GetComponent<DoorAnimation>();
				if (this._doorAnimation != null)
				{
					this._doorAnimation.Load(playState.MainStateInfo.InStateInfo.StateInfos);
					this._doorAnimation.PlayAnimation(<OnAwake>c__AnonStorey.animInfo.inEnableBlend, <OnAwake>c__AnonStorey.animInfo.inBlendSec, playState.MainStateInfo.FadeOutTime, <OnAwake>c__AnonStorey.animInfo.layer);
				}
			}
		}

		// Token: 0x06006E5D RID: 28253 RVA: 0x002F397C File Offset: 0x002F1D7C
		protected override void OnRelease(PlayerActor player)
		{
			if (player.CurrentPoint != null && player.CurrentPoint.RemoveBooking(player))
			{
				player.CurrentPoint.SetImpossible(false, player);
				CommandArea commandArea = player.PlayerController.CommandArea;
				commandArea.RemoveConsiderationObject(player.CurrentPoint);
				commandArea.RefreshCommands();
			}
			player.Animation.ResetDefaultAnimatorController();
			int item = AIProject.Definitions.Action.NameTable[EventType.DoorOpen].Item1;
			ActorAnimInfo animInfo = player.Animation.AnimInfo;
			PlayState playState = Singleton<Manager.Resources>.Instance.Animation.PlayerActionAnimTable[(int)player.ChaControl.sex][item][player.PoseID];
			player.SetStand(player.Animation.RecoveryPoint, animInfo.endEnableBlend, animInfo.endBlendSec, playState.DirectionType);
			player.Animation.RefsActAnimInfo = true;
		}

		// Token: 0x06006E5E RID: 28254 RVA: 0x002F3A6C File Offset: 0x002F1E6C
		protected override void OnUpdate(PlayerActor actor, ref Actor.InputInfo info)
		{
			actor.NavMeshAgent.velocity = (info.move = Vector3.zero);
			if (actor.Animation.PlayingInAnimation)
			{
				return;
			}
			if (this._doorAnimation != null && this._doorAnimation.PlayingOpenAnim)
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

		// Token: 0x06006E5F RID: 28255 RVA: 0x002F3AFA File Offset: 0x002F1EFA
		protected override void OnAfterUpdate(PlayerActor actor, Actor.InputInfo info)
		{
			actor.CharacterTPS.UpdateState(info, ActorLocomotion.UpdateType.Update);
		}

		// Token: 0x06006E60 RID: 28256 RVA: 0x002F3B0C File Offset: 0x002F1F0C
		private void Elapsed(PlayerActor player)
		{
			DoorPoint doorPoint = player.CurrentPoint as DoorPoint;
			if (doorPoint != null)
			{
				if (doorPoint.OpenType == DoorPoint.OpenTypeState.Right || doorPoint.OpenType == DoorPoint.OpenTypeState.Right90)
				{
					doorPoint.SetOpenState(DoorPoint.OpenPattern.OpenRight, true);
				}
				else
				{
					doorPoint.SetOpenState(DoorPoint.OpenPattern.OpenLeft, true);
				}
			}
			player.PlayerController.CommandArea.RefreshCommands();
			if (player.CurrentPoint != null)
			{
				player.CurrentPoint.RemoveBooking(player);
				CommandArea commandArea = player.PlayerController.CommandArea;
				commandArea.RemoveConsiderationObject(player.CurrentPoint);
				commandArea.RefreshCommands();
			}
			if (player.PlayerController.PrevStateName == "Onbu")
			{
				player.Controller.ChangeState(player.PlayerController.PrevStateName);
			}
			else if (player.PlayerController.PrevStateName == "Follow")
			{
				player.PlayerController.ChangeState("Follow");
			}
			else
			{
				player.Controller.ChangeState("Normal");
			}
		}

		// Token: 0x04005B96 RID: 23446
		protected int _currentState = -1;

		// Token: 0x04005B97 RID: 23447
		private Subject<Unit> _onEndAction = new Subject<Unit>();

		// Token: 0x04005B98 RID: 23448
		private DoorAnimation _doorAnimation;
	}
}
