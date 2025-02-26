using System;
using System.Collections;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject.Player
{
	// Token: 0x02000DF7 RID: 3575
	public class EntryChara : PlayerStateBase
	{
		// Token: 0x06006E79 RID: 28281 RVA: 0x002F4548 File Offset: 0x002F2948
		protected override void OnAwake(PlayerActor player)
		{
			MapUIContainer.SetActiveCharaEntryUI(true);
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

		// Token: 0x06006E7A RID: 28282 RVA: 0x002F45B8 File Offset: 0x002F29B8
		protected override void OnRelease(PlayerActor player)
		{
			base.OnRelease(player);
		}

		// Token: 0x06006E7B RID: 28283 RVA: 0x002F45C4 File Offset: 0x002F29C4
		protected override void OnUpdate(PlayerActor player, ref Actor.InputInfo info)
		{
			player.NavMeshAgent.velocity = (info.move = Vector3.zero);
			if (MapUIContainer.CharaEntryUI.IsActiveControl)
			{
				return;
			}
			this._onEndMenu.OnNext(Unit.Default);
		}

		// Token: 0x06006E7C RID: 28284 RVA: 0x002F460A File Offset: 0x002F2A0A
		protected override void OnAfterUpdate(PlayerActor player, Actor.InputInfo info)
		{
			player.CharacterTPS.UpdateState(info, ActorLocomotion.UpdateType.Update);
		}

		// Token: 0x06006E7D RID: 28285 RVA: 0x002F461C File Offset: 0x002F2A1C
		public override IEnumerator End(Actor actor)
		{
			yield break;
		}

		// Token: 0x04005BA3 RID: 23459
		private Subject<Unit> _onEndMenu = new Subject<Unit>();
	}
}
