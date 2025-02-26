using System;
using System.Collections;
using Manager;
using UnityEngine;

namespace AIProject.Player
{
	// Token: 0x02000DDB RID: 3547
	public class Warp : PlayerStateBase
	{
		// Token: 0x06006DB6 RID: 28086 RVA: 0x002EB830 File Offset: 0x002E9C30
		protected override void OnAwake(PlayerActor player)
		{
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.UI);
			Singleton<Manager.Input>.Instance.SetupState();
			MapUIContainer.SetVisibleHUDExceptStoryUI(false);
			MapUIContainer.StorySupportUI.Close();
			MapUIContainer.RefreshCommands(0, player.WarpCommandInfos);
			MapUIContainer.CommandList.CancelEvent = delegate()
			{
				player.CancelWarp();
			};
			MapUIContainer.SetActiveCommandList(true, "ワープ装置");
		}

		// Token: 0x06006DB7 RID: 28087 RVA: 0x002EB8A1 File Offset: 0x002E9CA1
		protected override void OnRelease(PlayerActor player)
		{
		}

		// Token: 0x06006DB8 RID: 28088 RVA: 0x002EB8A4 File Offset: 0x002E9CA4
		protected override void OnUpdate(PlayerActor player, ref Actor.InputInfo info)
		{
			player.NavMeshAgent.velocity = (info.move = Vector3.zero);
		}

		// Token: 0x06006DB9 RID: 28089 RVA: 0x002EB8CA File Offset: 0x002E9CCA
		protected override void OnAfterUpdate(PlayerActor player, Actor.InputInfo info)
		{
			player.CharacterTPS.UpdateState(info, ActorLocomotion.UpdateType.Update);
		}

		// Token: 0x06006DBA RID: 28090 RVA: 0x002EB8DC File Offset: 0x002E9CDC
		public override IEnumerator End(Actor actor)
		{
			yield break;
		}
	}
}
