using System;
using System.Collections;
using Manager;
using UnityEngine;

namespace AIProject.Player
{
	// Token: 0x02000E01 RID: 3585
	public class Kitchen : PlayerStateBase
	{
		// Token: 0x06006EED RID: 28397 RVA: 0x002F74B8 File Offset: 0x002F58B8
		protected override void OnAwake(PlayerActor player)
		{
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.UI);
			Singleton<Manager.Input>.Instance.SetupState();
			MapUIContainer.SetVisibleHUDExceptStoryUI(false);
			MapUIContainer.StorySupportUI.Close();
			int id = 2;
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
		}

		// Token: 0x06006EEE RID: 28398 RVA: 0x002F7534 File Offset: 0x002F5934
		private void OnStart(PlayerActor player)
		{
			MapUIContainer.RefreshCommands(0, player.CookCommandInfos);
			MapUIContainer.CommandList.CancelEvent = delegate()
			{
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
				MapUIContainer.SetActiveCommandList(false);
				MapUIContainer.SetVisibleHUDExceptStoryUI(true);
				MapUIContainer.StorySupportUI.Open();
				player.Controller.ChangeState("Normal");
				player.ReleaseCurrentPoint();
				if (player.PlayerController.CommandArea != null)
				{
					player.PlayerController.CommandArea.enabled = true;
				}
				MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.InvokeAcception);
				player.ActivateNavMeshAgent();
				player.IsKinematic = false;
			};
			MapUIContainer.SetActiveCommandList(true, "料理");
		}

		// Token: 0x06006EEF RID: 28399 RVA: 0x002F7580 File Offset: 0x002F5980
		public override void Release(Actor actor, EventType type)
		{
			this.OnRelease(actor as PlayerActor);
		}

		// Token: 0x06006EF0 RID: 28400 RVA: 0x002F758E File Offset: 0x002F598E
		protected override void OnRelease(PlayerActor player)
		{
		}

		// Token: 0x06006EF1 RID: 28401 RVA: 0x002F7590 File Offset: 0x002F5990
		protected override void OnUpdate(PlayerActor actor, ref Actor.InputInfo info)
		{
			actor.NavMeshAgent.velocity = (info.move = Vector3.zero);
		}

		// Token: 0x06006EF2 RID: 28402 RVA: 0x002F75B6 File Offset: 0x002F59B6
		protected override void OnAfterUpdate(PlayerActor actor, Actor.InputInfo info)
		{
			actor.CharacterTPS.UpdateState(info, ActorLocomotion.UpdateType.Update);
		}

		// Token: 0x06006EF3 RID: 28403 RVA: 0x002F75C8 File Offset: 0x002F59C8
		protected override IEnumerator OnEnd(PlayerActor player)
		{
			yield break;
		}
	}
}
