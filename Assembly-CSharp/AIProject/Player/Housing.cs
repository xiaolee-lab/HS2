using System;
using System.Collections;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject.Player
{
	// Token: 0x02000DFE RID: 3582
	public class Housing : PlayerStateBase
	{
		// Token: 0x06006ED8 RID: 28376 RVA: 0x002F6F00 File Offset: 0x002F5300
		protected override void OnAwake(PlayerActor player)
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
				MapUIContainer.NicknameUI.CanvasAlpha = 0f;
			}
			Singleton<Map>.Instance.Player.SetScheduledInteractionState(false);
			MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.UI);
			Singleton<Manager.Input>.Instance.SetupState();
			IObservable<Unit> source = MapUIContainer.StartFade(FadeCanvas.PanelType.Blackout, FadeType.In, 1f, true);
			source.Subscribe(delegate(Unit __)
			{
			}, delegate()
			{
				player.SetActiveOnEquipedItem(false);
				player.ChaControl.setAllLayerWeight(0f);
				Singleton<Map>.Instance.DisableEntity();
				Singleton<Map>.Instance.Simulator.EnabledTimeProgression = false;
				player.CameraControl.CameraComponent.enabled = false;
				Singleton<Housing>.Instance.StartHousing();
			});
		}

		// Token: 0x06006ED9 RID: 28377 RVA: 0x002F6F9C File Offset: 0x002F539C
		protected override void OnRelease(PlayerActor player)
		{
			MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.InvokeAcception);
		}

		// Token: 0x06006EDA RID: 28378 RVA: 0x002F6FA4 File Offset: 0x002F53A4
		protected override void OnUpdate(PlayerActor player, ref Actor.InputInfo info)
		{
			player.NavMeshAgent.velocity = (info.move = Vector3.zero);
		}

		// Token: 0x06006EDB RID: 28379 RVA: 0x002F6FCA File Offset: 0x002F53CA
		protected override void OnAfterUpdate(PlayerActor player, Actor.InputInfo info)
		{
			player.CharacterTPS.UpdateState(info, ActorLocomotion.UpdateType.Update);
		}

		// Token: 0x06006EDC RID: 28380 RVA: 0x002F6FDC File Offset: 0x002F53DC
		protected override IEnumerator OnEnd(PlayerActor player)
		{
			yield break;
		}
	}
}
