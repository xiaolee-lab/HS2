using System;
using System.Collections;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject.Player
{
	// Token: 0x02000DEA RID: 3562
	public class CharaLookEdit : PlayerStateBase
	{
		// Token: 0x06006E26 RID: 28198 RVA: 0x002F2490 File Offset: 0x002F0890
		protected override void OnAwake(PlayerActor player)
		{
			MapUIContainer.SetActiveCharaLookEditUI(true);
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

		// Token: 0x06006E27 RID: 28199 RVA: 0x002F2500 File Offset: 0x002F0900
		protected override void OnRelease(PlayerActor player)
		{
			player.PlayerController.CommandArea.RefreshCommands();
		}

		// Token: 0x06006E28 RID: 28200 RVA: 0x002F2514 File Offset: 0x002F0914
		protected override void OnUpdate(PlayerActor player, ref Actor.InputInfo info)
		{
			player.NavMeshAgent.velocity = (info.move = Vector3.zero);
			if (MapUIContainer.CharaLookEditUI.IsActiveControl)
			{
				return;
			}
			this._onEndMenu.OnNext(Unit.Default);
		}

		// Token: 0x06006E29 RID: 28201 RVA: 0x002F255A File Offset: 0x002F095A
		protected override void OnAfterUpdate(PlayerActor player, Actor.InputInfo info)
		{
			player.CharacterTPS.UpdateState(info, ActorLocomotion.UpdateType.Update);
		}

		// Token: 0x06006E2A RID: 28202 RVA: 0x002F256C File Offset: 0x002F096C
		public override IEnumerator End(Actor actor)
		{
			yield break;
		}

		// Token: 0x04005B8A RID: 23434
		private Subject<Unit> _onEndMenu = new Subject<Unit>();
	}
}
