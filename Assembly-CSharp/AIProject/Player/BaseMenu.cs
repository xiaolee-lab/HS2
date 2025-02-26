using System;
using System.Collections;
using Manager;
using UnityEngine;

namespace AIProject.Player
{
	// Token: 0x02000DE4 RID: 3556
	public class BaseMenu : PlayerStateBase
	{
		// Token: 0x06006DF3 RID: 28147 RVA: 0x002EF23C File Offset: 0x002ED63C
		protected override void OnAwake(PlayerActor player)
		{
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.UI);
			Singleton<Manager.Input>.Instance.SetupState();
			MapUIContainer.SetVisibleHUDExceptStoryUI(false);
			MapUIContainer.StorySupportUI.Close();
			MapUIContainer.RefreshCommands(0, player.BaseCommandInfos);
			MapUIContainer.CommandList.CancelEvent = delegate()
			{
				MapUIContainer.SetActiveCommandList(false);
				MapUIContainer.SetVisibleHUDExceptStoryUI(true);
				MapUIContainer.StorySupportUI.Open();
				player.Controller.ChangeState("Normal");
			};
			MapUIContainer.SetActiveCommandList(true, "拠点");
		}

		// Token: 0x06006DF4 RID: 28148 RVA: 0x002EF2B0 File Offset: 0x002ED6B0
		protected override void OnUpdate(PlayerActor player, ref Actor.InputInfo info)
		{
			player.NavMeshAgent.velocity = (info.move = Vector3.zero);
		}

		// Token: 0x06006DF5 RID: 28149 RVA: 0x002EF2D6 File Offset: 0x002ED6D6
		protected override void OnAfterUpdate(PlayerActor player, Actor.InputInfo info)
		{
			player.CharacterTPS.UpdateState(info, ActorLocomotion.UpdateType.Update);
		}

		// Token: 0x06006DF6 RID: 28150 RVA: 0x002EF2E8 File Offset: 0x002ED6E8
		public override IEnumerator End(Actor actor)
		{
			yield break;
		}
	}
}
