using System;
using System.Collections;
using Manager;
using UnityEngine;

namespace AIProject.Player
{
	// Token: 0x02000DE2 RID: 3554
	public class ExitEatEvent : PlayerStateBase
	{
		// Token: 0x06006DE7 RID: 28135 RVA: 0x002EEF70 File Offset: 0x002ED370
		protected override void OnAwake(PlayerActor player)
		{
			player.EventKey = EventType.Eat;
			player.SetActiveOnEquipedItem(false);
			player.ChaControl.setAllLayerWeight(0f);
			MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.UI);
			Singleton<Manager.Input>.Instance.SetupState();
			MapUIContainer.RefreshCommands(0, player.ExitEatEventCommandInfo);
			MapUIContainer.SetActiveCommandList(true, "食事");
			MapUIContainer.CommandList.CancelEvent = null;
			player.OldEnabledHoldingHand = player.HandsHolder.enabled;
			if (player.OldEnabledHoldingHand)
			{
				player.HandsHolder.enabled = false;
				if (player.HandsHolder.EnabledHolding)
				{
					player.HandsHolder.EnabledHolding = false;
				}
			}
		}

		// Token: 0x06006DE8 RID: 28136 RVA: 0x002EF01D File Offset: 0x002ED41D
		protected override void OnRelease(PlayerActor player)
		{
			player.SetScheduledInteractionState(true);
			player.ReleaseInteraction();
		}

		// Token: 0x06006DE9 RID: 28137 RVA: 0x002EF02C File Offset: 0x002ED42C
		protected override void OnUpdate(PlayerActor player, ref Actor.InputInfo info)
		{
			player.NavMeshAgent.velocity = (info.move = Vector3.zero);
		}

		// Token: 0x06006DEA RID: 28138 RVA: 0x002EF052 File Offset: 0x002ED452
		protected override void OnAfterUpdate(PlayerActor player, Actor.InputInfo info)
		{
			player.CharacterTPS.UpdateState(info, ActorLocomotion.UpdateType.Update);
		}

		// Token: 0x06006DEB RID: 28139 RVA: 0x002EF061 File Offset: 0x002ED461
		protected override IEnumerator OnEnd(PlayerActor player)
		{
			return base.OnEnd(player);
		}
	}
}
