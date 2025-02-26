using System;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject.Player
{
	// Token: 0x02000DDF RID: 3551
	public class DateOpenHarbordoor : PlayerStateBase
	{
		// Token: 0x06006DD2 RID: 28114 RVA: 0x002ED3C0 File Offset: 0x002EB7C0
		protected override void OnAwake(PlayerActor player)
		{
			DateOpenHarbordoor.<OnAwake>c__AnonStorey0 <OnAwake>c__AnonStorey = new DateOpenHarbordoor.<OnAwake>c__AnonStorey0();
			<OnAwake>c__AnonStorey.player = player;
			<OnAwake>c__AnonStorey.$this = this;
			this._eventPoint = <OnAwake>c__AnonStorey.player.CurrentEventPoint;
			if (this._eventPoint == null)
			{
				this.ErrorEnd(<OnAwake>c__AnonStorey.player, "イベントポイント持っていないのにバルブ扉を開こうとした");
				return;
			}
			if (this._eventPoint == EventPoint.GetTargetPoint())
			{
				EventPoint.SetTargetID(-1, -1);
			}
			HarborDoorAnimation component = this._eventPoint.GetComponent<HarborDoorAnimation>();
			if (component == null)
			{
				this.ErrorEnd(<OnAwake>c__AnonStorey.player, "イベントポイントからHarborDoorAnimationが取得できなかった");
				return;
			}
			this._prevAcceptionState = MapUIContainer.CommandLabel.Acception;
			if (this._prevAcceptionState != CommandLabel.AcceptionState.None)
			{
				MapUIContainer.CommandLabel.Acception = CommandLabel.AcceptionState.None;
			}
			this._animData = component.AnimData;
			<OnAwake>c__AnonStorey.player.EventKey = EventType.DoorOpen;
			this._poseInfo = component.PoseInfo;
			<OnAwake>c__AnonStorey.player.SetActiveOnEquipedItem(false);
			<OnAwake>c__AnonStorey.player.ChaControl.setAllLayerWeight(0f);
			Transform t = component.BasePoint ?? this._eventPoint.transform;
			<OnAwake>c__AnonStorey.player.Animation.RecoveryPoint = component.RecoveryPoint;
			int sex = (int)<OnAwake>c__AnonStorey.player.ChaControl.sex;
			int postureID = this._poseInfo.postureID;
			int poseID = this._poseInfo.poseID;
			this._info = Singleton<Manager.Resources>.Instance.Animation.PlayerActionAnimTable[sex][postureID][poseID];
			DateOpenHarbordoor.<OnAwake>c__AnonStorey0 <OnAwake>c__AnonStorey2 = <OnAwake>c__AnonStorey;
			ActorAnimInfo animInfo = new ActorAnimInfo
			{
				layer = this._info.Layer,
				inEnableBlend = this._info.MainStateInfo.InStateInfo.EnableFade,
				inBlendSec = this._info.MainStateInfo.InStateInfo.FadeSecond,
				outEnableBlend = this._info.MainStateInfo.OutStateInfo.EnableFade,
				outBlendSec = this._info.MainStateInfo.OutStateInfo.FadeSecond,
				directionType = this._info.DirectionType,
				endEnableBlend = this._info.EndEnableBlend,
				endBlendSec = this._info.EndBlendRate
			};
			<OnAwake>c__AnonStorey.player.Animation.AnimInfo = animInfo;
			<OnAwake>c__AnonStorey2.animInfo = animInfo;
			<OnAwake>c__AnonStorey.player.Animation.LoadSEEventKeyTable(postureID, poseID);
			<OnAwake>c__AnonStorey.player.Animation.InitializeStates(this._info.MainStateInfo.InStateInfo.StateInfos, this._info.MainStateInfo.OutStateInfo.StateInfos, this._info.MainStateInfo.AssetBundleInfo);
			<OnAwake>c__AnonStorey.player.Animation.PlayInAnimation(<OnAwake>c__AnonStorey.animInfo.inEnableBlend, <OnAwake>c__AnonStorey.animInfo.inBlendSec, this._info.MainStateInfo.FadeOutTime, <OnAwake>c__AnonStorey.animInfo.layer);
			this._onEndAction.Take(1).Subscribe(delegate(Unit _)
			{
				<OnAwake>c__AnonStorey.player.Animation.PlayOutAnimation(<OnAwake>c__AnonStorey.animInfo.outEnableBlend, <OnAwake>c__AnonStorey.animInfo.outBlendSec, <OnAwake>c__AnonStorey.animInfo.layer);
			});
			<OnAwake>c__AnonStorey.player.DeactivateNavMeshAgent();
			<OnAwake>c__AnonStorey.player.IsKinematic = true;
			<OnAwake>c__AnonStorey.player.SetStand(t, this._info.MainStateInfo.InStateInfo.EnableFade, this._info.MainStateInfo.InStateInfo.FadeSecond, this._info.DirectionType);
			if (this._animData != null)
			{
				this._animData.AnimEndAction = delegate()
				{
					<OnAwake>c__AnonStorey.$this._animData.PlayOpenIdleAnimation(false, 0f, 0f, 0);
				};
				this._animData.PlayToOpenAnimation(<OnAwake>c__AnonStorey.animInfo.inEnableBlend, <OnAwake>c__AnonStorey.animInfo.inBlendSec, this._info.MainStateInfo.FadeOutTime, <OnAwake>c__AnonStorey.animInfo.layer);
			}
			this._onComplete.Take(1).Subscribe(delegate(Unit _)
			{
				if (<OnAwake>c__AnonStorey.$this._animData is AreaOpenLinkedHarborDoorAnimData)
				{
					int areaOpenID = (<OnAwake>c__AnonStorey.$this._animData as AreaOpenLinkedHarborDoorAnimData).AreaOpenID;
					Singleton<Map>.Instance.SetOpenAreaState(areaOpenID, true);
				}
				if (<OnAwake>c__AnonStorey.$this._eventPoint.GroupID == 1)
				{
					int pointID = <OnAwake>c__AnonStorey.$this._eventPoint.PointID;
					if (pointID != 2)
					{
						if (pointID != 4)
						{
						}
					}
					else
					{
						Map.ForcedSetTutorialProgress(26);
					}
				}
				if (<OnAwake>c__AnonStorey.$this._prevAcceptionState != MapUIContainer.CommandLabel.Acception)
				{
					MapUIContainer.CommandLabel.Acception = <OnAwake>c__AnonStorey.$this._prevAcceptionState;
				}
				<OnAwake>c__AnonStorey.player.ActivateNavMeshAgent();
				<OnAwake>c__AnonStorey.player.PlayerController.ChangeState("Normal");
			});
			<OnAwake>c__AnonStorey.player.CameraControl.Mode = CameraMode.ActionFreeLook;
			<OnAwake>c__AnonStorey.player.CameraControl.SetShotTypeForce(ShotType.Near);
			<OnAwake>c__AnonStorey.player.CameraControl.LoadActionCameraFile(postureID, poseID, null);
			<OnAwake>c__AnonStorey.player.OldEnabledHoldingHand = <OnAwake>c__AnonStorey.player.HandsHolder.enabled;
			if (<OnAwake>c__AnonStorey.player.OldEnabledHoldingHand)
			{
				<OnAwake>c__AnonStorey.player.HandsHolder.enabled = false;
				<OnAwake>c__AnonStorey.player.HandsHolder.Weight = 0f;
				if (<OnAwake>c__AnonStorey.player.HandsHolder.EnabledHolding)
				{
					<OnAwake>c__AnonStorey.player.HandsHolder.EnabledHolding = false;
				}
			}
			this._initSuccess = true;
		}

		// Token: 0x06006DD3 RID: 28115 RVA: 0x002ED87A File Offset: 0x002EBC7A
		private void ErrorEnd(PlayerActor player, string log)
		{
			player.PlayerController.ChangeState("Normal");
			if (global::Debug.isDebugBuild)
			{
			}
		}

		// Token: 0x06006DD4 RID: 28116 RVA: 0x002ED898 File Offset: 0x002EBC98
		protected override void OnUpdate(PlayerActor player, ref Actor.InputInfo info)
		{
			player.NavMeshAgent.velocity = (info.move = Vector3.zero);
			if (player.Animation.PlayingInAnimation)
			{
				return;
			}
			if (this._onEndAction != null)
			{
				this._onEndAction.OnNext(Unit.Default);
			}
			if (player.Animation.PlayingOutAnimation)
			{
				return;
			}
			bool? flag = (this._animData != null) ? new bool?(this._animData.PlayingAnimation) : null;
			if (flag != null && flag.Value)
			{
				return;
			}
			if (this._onComplete != null)
			{
				this._onComplete.OnNext(Unit.Default);
			}
		}

		// Token: 0x06006DD5 RID: 28117 RVA: 0x002ED960 File Offset: 0x002EBD60
		protected override void OnAfterUpdate(PlayerActor player, Actor.InputInfo info)
		{
			player.CharacterTPS.UpdateState(info, ActorLocomotion.UpdateType.Update);
		}

		// Token: 0x06006DD6 RID: 28118 RVA: 0x002ED970 File Offset: 0x002EBD70
		protected override void OnRelease(PlayerActor player)
		{
			player.CurrentEventPoint = null;
			if (!this._initSuccess)
			{
				return;
			}
			ActorAnimInfo animInfo = player.Animation.AnimInfo;
			player.SetStand(player.Animation.RecoveryPoint, animInfo.endEnableBlend, animInfo.endBlendSec, animInfo.directionType);
			player.Animation.RefsActAnimInfo = true;
			if (player.OldEnabledHoldingHand)
			{
				player.HandsHolder.enabled = true;
				player.OldEnabledHoldingHand = false;
			}
		}

		// Token: 0x04005B52 RID: 23378
		private EventPoint _eventPoint;

		// Token: 0x04005B53 RID: 23379
		private Subject<Unit> _onEndAction = new Subject<Unit>();

		// Token: 0x04005B54 RID: 23380
		private Subject<Unit> _onComplete = new Subject<Unit>();

		// Token: 0x04005B55 RID: 23381
		private PoseKeyPair _poseInfo = default(PoseKeyPair);

		// Token: 0x04005B56 RID: 23382
		private HarborDoorAnimData _animData;

		// Token: 0x04005B57 RID: 23383
		private PlayState _info;

		// Token: 0x04005B58 RID: 23384
		private bool _initSuccess;

		// Token: 0x04005B59 RID: 23385
		private CommandLabel.AcceptionState _prevAcceptionState;
	}
}
