using System;
using System.Collections;
using AIProject.SaveData;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject.Player
{
	// Token: 0x02000E03 RID: 3587
	public class Menu : PlayerStateBase
	{
		// Token: 0x06006EFD RID: 28413 RVA: 0x002F7E2C File Offset: 0x002F622C
		protected override void OnAwake(PlayerActor player)
		{
			Menu.<OnAwake>c__AnonStorey1 <OnAwake>c__AnonStorey = new Menu.<OnAwake>c__AnonStorey1();
			<OnAwake>c__AnonStorey.player = player;
			<OnAwake>c__AnonStorey.$this = this;
			<OnAwake>c__AnonStorey.player.EventKey = (EventType)0;
			<OnAwake>c__AnonStorey.player.SetActiveOnEquipedItem(false);
			<OnAwake>c__AnonStorey.player.ChaControl.setAllLayerWeight(0f);
			if (<OnAwake>c__AnonStorey.player.PlayerController.PrevStateName != "Onbu")
			{
				Menu.<OnAwake>c__AnonStorey2 <OnAwake>c__AnonStorey2 = new Menu.<OnAwake>c__AnonStorey2();
				<OnAwake>c__AnonStorey2.<>f__ref$1 = <OnAwake>c__AnonStorey;
				Manager.Resources instance = Singleton<Manager.Resources>.Instance;
				PoseKeyPair menuPoseID = instance.PlayerProfile.PoseIDData.MenuPoseID;
				PlayState playState = instance.Animation.PlayerActionAnimTable[(int)<OnAwake>c__AnonStorey.player.ChaControl.sex][menuPoseID.postureID][menuPoseID.poseID];
				<OnAwake>c__AnonStorey.player.Animation.LoadEventKeyTable(menuPoseID.postureID, menuPoseID.poseID);
				if (<OnAwake>c__AnonStorey.player.ChaControl.visibleAll)
				{
					<OnAwake>c__AnonStorey.player.LoadEventItems(playState);
					<OnAwake>c__AnonStorey.player.LoadEventParticles(menuPoseID.postureID, menuPoseID.poseID);
				}
				<OnAwake>c__AnonStorey.player.Animation.InitializeStates(playState);
				Menu.<OnAwake>c__AnonStorey2 <OnAwake>c__AnonStorey3 = <OnAwake>c__AnonStorey2;
				ActorAnimInfo animInfo = new ActorAnimInfo
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
				<OnAwake>c__AnonStorey.player.Animation.AnimInfo = animInfo;
				<OnAwake>c__AnonStorey3.animInfo = animInfo;
				<OnAwake>c__AnonStorey.player.OldEnabledHoldingHand = <OnAwake>c__AnonStorey.player.HandsHolder.enabled;
				if (<OnAwake>c__AnonStorey.player.OldEnabledHoldingHand)
				{
					<OnAwake>c__AnonStorey.player.HandsHolder.enabled = false;
					if (<OnAwake>c__AnonStorey.player.HandsHolder.EnabledHolding)
					{
						<OnAwake>c__AnonStorey.player.HandsHolder.EnabledHolding = false;
					}
				}
				MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);
				<OnAwake>c__AnonStorey.player.Animation.StopAllAnimCoroutine();
				<OnAwake>c__AnonStorey.player.Animation.PlayInLocoAnimation(<OnAwake>c__AnonStorey2.animInfo.inEnableBlend, <OnAwake>c__AnonStorey2.animInfo.inBlendSec, <OnAwake>c__AnonStorey2.animInfo.layer);
				this._onEndMenu.Take(1).Subscribe(delegate(Unit _)
				{
					<OnAwake>c__AnonStorey2.<>f__ref$1.player.Animation.PlayOutAnimation(<OnAwake>c__AnonStorey2.animInfo.outEnableBlend, <OnAwake>c__AnonStorey2.animInfo.outBlendSec, <OnAwake>c__AnonStorey2.animInfo.layer);
				});
				this._onEndOutAnimDisposable = this._onEndOutAnimation.Take(1).Subscribe(delegate(Unit _)
				{
					if (<OnAwake>c__AnonStorey2.<>f__ref$1.$this._onEndInputDisposable != null)
					{
						<OnAwake>c__AnonStorey2.<>f__ref$1.$this._onEndInputDisposable.Dispose();
					}
					<OnAwake>c__AnonStorey2.<>f__ref$1.$this.EndState(<OnAwake>c__AnonStorey2.<>f__ref$1.player);
				});
			}
			this._onEndInAnimation.Take(1).Subscribe(delegate(Unit _)
			{
				MapUIContainer.SetActiveSystemMenuUI(true);
			});
			this._onEndInputDisposable = this._onEndInput.Take(1).Subscribe(delegate(Unit _)
			{
				if (<OnAwake>c__AnonStorey.$this._onEndOutAnimDisposable != null)
				{
					<OnAwake>c__AnonStorey.$this._onEndOutAnimDisposable.Dispose();
				}
				<OnAwake>c__AnonStorey.$this.EndState(<OnAwake>c__AnonStorey.player);
			});
		}

		// Token: 0x06006EFE RID: 28414 RVA: 0x002F8170 File Offset: 0x002F6570
		protected override void OnRelease(PlayerActor player)
		{
			player.ClearItems();
			player.ClearParticles();
			if (player.OldEnabledHoldingHand)
			{
				player.HandsHolder.enabled = true;
				player.OldEnabledHoldingHand = false;
			}
		}

		// Token: 0x06006EFF RID: 28415 RVA: 0x002F819C File Offset: 0x002F659C
		protected override void OnUpdate(PlayerActor actor, ref Actor.InputInfo info)
		{
			actor.NavMeshAgent.velocity = (info.move = Vector3.zero);
			if (actor.Animation.PlayingInLocoAnimation)
			{
				return;
			}
			if (this._onEndInAnimation != null)
			{
				this._onEndInAnimation.OnNext(Unit.Default);
			}
			if (MapUIContainer.SystemMenuUI.IsActiveControl)
			{
				return;
			}
			this._onEndMenu.OnNext(Unit.Default);
			if (actor.Animation.PlayingOutAnimation)
			{
				Vector2 moveAxis = Singleton<Manager.Input>.Instance.MoveAxis;
				if (Mathf.Sqrt(moveAxis.x * moveAxis.x + moveAxis.y * moveAxis.y) > 0.5f)
				{
					this._onEndInput.OnNext(Unit.Default);
				}
				return;
			}
			this._onEndInput.OnNext(Unit.Default);
		}

		// Token: 0x06006F00 RID: 28416 RVA: 0x002F827A File Offset: 0x002F667A
		protected override void OnAfterUpdate(PlayerActor actor, Actor.InputInfo info)
		{
			actor.CharacterTPS.UpdateState(info, ActorLocomotion.UpdateType.Update);
		}

		// Token: 0x06006F01 RID: 28417 RVA: 0x002F828C File Offset: 0x002F668C
		public override IEnumerator End(Actor actor)
		{
			yield return null;
			yield break;
		}

		// Token: 0x06006F02 RID: 28418 RVA: 0x002F82A0 File Offset: 0x002F66A0
		private void EndState(PlayerActor player)
		{
			StuffItem equipedLampItem = player.PlayerData.EquipedLampItem;
			ItemIDKeyPair torchID = Singleton<Manager.Resources>.Instance.CommonDefine.ItemIDDefine.TorchID;
			ItemIDKeyPair maleLampID = Singleton<Manager.Resources>.Instance.CommonDefine.ItemIDDefine.MaleLampID;
			ItemIDKeyPair flashlightID = Singleton<Manager.Resources>.Instance.CommonDefine.ItemIDDefine.FlashlightID;
			if ((equipedLampItem.CategoryID == torchID.categoryID && equipedLampItem.ID == torchID.itemID) || (equipedLampItem.CategoryID == maleLampID.categoryID && equipedLampItem.ID == maleLampID.itemID) || (equipedLampItem.CategoryID == flashlightID.categoryID && equipedLampItem.ID == flashlightID.itemID))
			{
				player.CameraControl.CrossFade.FadeStart(-1f);
			}
			if (player.PlayerController.PrevStateName == "Onbu")
			{
				player.PlayerController.ChangeState("Onbu");
			}
			else
			{
				player.PlayerController.ChangeState("Normal");
			}
		}

		// Token: 0x04005BD0 RID: 23504
		private Subject<Unit> _onEndInAnimation = new Subject<Unit>();

		// Token: 0x04005BD1 RID: 23505
		private Subject<Unit> _onEndMenu = new Subject<Unit>();

		// Token: 0x04005BD2 RID: 23506
		private Subject<Unit> _onEndInput = new Subject<Unit>();

		// Token: 0x04005BD3 RID: 23507
		private IDisposable _onEndInputDisposable;

		// Token: 0x04005BD4 RID: 23508
		private Subject<Unit> _onEndOutAnimation = new Subject<Unit>();

		// Token: 0x04005BD5 RID: 23509
		private IDisposable _onEndOutAnimDisposable;
	}
}
