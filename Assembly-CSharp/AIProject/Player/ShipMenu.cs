using System;
using System.Collections;
using AIProject.Scene;
using Manager;
using UnityEngine;

namespace AIProject.Player
{
	// Token: 0x02000E10 RID: 3600
	public class ShipMenu : PlayerStateBase
	{
		// Token: 0x06006F67 RID: 28519 RVA: 0x002FD0B8 File Offset: 0x002FB4B8
		protected override void OnAwake(PlayerActor player)
		{
			Singleton<MapScene>.Instance.SaveProfile(true);
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.UI);
			Singleton<Manager.Input>.Instance.SetupState();
			MapUIContainer.SetVisibleHUDExceptStoryUI(false);
			MapUIContainer.StorySupportUI.Close();
			MapUIContainer.RefreshCommands(0, player.ShipCommandInfos);
			MapUIContainer.CommandList.CancelEvent = delegate()
			{
				MapUIContainer.SetActiveCommandList(false);
				MapUIContainer.StorySupportUI.Open();
				player.PlayerController.ChangeState("Normal");
			};
			MapUIContainer.SetActiveCommandList(true, "移動先");
		}

		// Token: 0x06006F68 RID: 28520 RVA: 0x002FD134 File Offset: 0x002FB534
		protected override void OnRelease(PlayerActor player)
		{
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.Action);
			Singleton<Manager.Input>.Instance.SetupState();
			player.SetScheduledInteractionState(true);
			player.ReleaseInteraction();
		}

		// Token: 0x06006F69 RID: 28521 RVA: 0x002FD158 File Offset: 0x002FB558
		protected override void OnUpdate(PlayerActor player, ref Actor.InputInfo info)
		{
			player.NavMeshAgent.velocity = (info.move = Vector3.zero);
		}

		// Token: 0x06006F6A RID: 28522 RVA: 0x002FD17E File Offset: 0x002FB57E
		protected override void OnAfterUpdate(PlayerActor player, Actor.InputInfo info)
		{
			player.CharacterTPS.UpdateState(info, ActorLocomotion.UpdateType.Update);
		}

		// Token: 0x06006F6B RID: 28523 RVA: 0x002FD190 File Offset: 0x002FB590
		public override IEnumerator End(Actor actor)
		{
			yield break;
		}
	}
}
