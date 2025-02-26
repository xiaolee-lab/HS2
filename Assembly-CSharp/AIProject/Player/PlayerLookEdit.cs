using System;
using System.Collections;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject.Player
{
	// Token: 0x02000E0C RID: 3596
	public class PlayerLookEdit : PlayerStateBase
	{
		// Token: 0x06006F4B RID: 28491 RVA: 0x002FBF38 File Offset: 0x002FA338
		protected override void OnAwake(PlayerActor player)
		{
			MapUIContainer.SetActivePlayerLookEditUI(true);
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.UI);
			Singleton<Manager.Input>.Instance.SetupState();
			player.SetScheduledInteractionState(false);
			player.ReleaseInteraction();
			MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);
			this._onEndMenu.Take(1).Subscribe(delegate(Unit _)
			{
				player.Controller.ChangeState("DeviceMenu");
			});
		}

		// Token: 0x06006F4C RID: 28492 RVA: 0x002FBFA8 File Offset: 0x002FA3A8
		protected override void OnRelease(PlayerActor player)
		{
			player.PlayerController.CommandArea.RefreshCommands();
		}

		// Token: 0x06006F4D RID: 28493 RVA: 0x002FBFBC File Offset: 0x002FA3BC
		protected override void OnUpdate(PlayerActor player, ref Actor.InputInfo info)
		{
			player.NavMeshAgent.velocity = (info.move = Vector3.zero);
			if (MapUIContainer.PlayerLookEditUI.IsActiveControl)
			{
				return;
			}
			this._onEndMenu.OnNext(Unit.Default);
		}

		// Token: 0x06006F4E RID: 28494 RVA: 0x002FC002 File Offset: 0x002FA402
		protected override void OnAfterUpdate(PlayerActor player, Actor.InputInfo info)
		{
			player.CharacterTPS.UpdateState(info, ActorLocomotion.UpdateType.Update);
		}

		// Token: 0x06006F4F RID: 28495 RVA: 0x002FC014 File Offset: 0x002FA414
		public override IEnumerator End(Actor actor)
		{
			yield break;
		}

		// Token: 0x04005C0A RID: 23562
		private Subject<Unit> _onEndMenu = new Subject<Unit>();
	}
}
