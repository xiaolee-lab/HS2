using System;
using AIProject.UI;
using AIProject.UI.Recycling;
using Manager;
using UnityEngine;

namespace AIProject.Player
{
	// Token: 0x02000DF0 RID: 3568
	public class Craft : PlayerStateBase
	{
		// Token: 0x06006E4B RID: 28235 RVA: 0x002F2F9C File Offset: 0x002F139C
		protected override void OnAwake(PlayerActor player)
		{
			CraftPoint currentCraftPoint = player.CurrentCraftPoint;
			if (currentCraftPoint == null || !Singleton<MapUIContainer>.IsInstance())
			{
				player.PlayerController.ChangeState("Normal");
				return;
			}
			MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);
			player.SetScheduledInteractionState(false);
			player.ReleaseInteraction();
			CraftUI craftUI = null;
			RecyclingUI recyclingUI = null;
			switch (currentCraftPoint.Kind)
			{
			case CraftPoint.CraftKind.Medicine:
				craftUI = MapUIContainer.MedicineCraftUI;
				break;
			case CraftPoint.CraftKind.Pet:
				craftUI = MapUIContainer.PetCraftUI;
				break;
			case CraftPoint.CraftKind.Recycling:
				recyclingUI = MapUIContainer.RecyclingUI;
				break;
			default:
				this.OnClosed(player);
				break;
			}
			if (craftUI != null)
			{
				craftUI.OnClosedEvent = delegate()
				{
					this.OnClosed(player);
				};
				craftUI.IsActiveControl = true;
			}
			else if (recyclingUI != null)
			{
				recyclingUI.OnClosedEvent = delegate()
				{
					this.OnClosed(player);
				};
				recyclingUI.IsActiveControl = true;
			}
			else
			{
				this.OnClosed(player);
			}
		}

		// Token: 0x06006E4C RID: 28236 RVA: 0x002F30C8 File Offset: 0x002F14C8
		private void OnClosed(PlayerActor player)
		{
			if (Singleton<MapUIContainer>.IsInstance())
			{
				MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.InvokeAcception);
			}
			player.SetScheduledInteractionState(true);
			player.ReleaseInteraction();
			if (Singleton<Manager.Input>.IsInstance())
			{
				Manager.Input instance = Singleton<Manager.Input>.Instance;
				instance.ReserveState(Manager.Input.ValidType.Action);
				instance.SetupState();
			}
			player.PlayerController.ChangeState("Normal");
		}

		// Token: 0x06006E4D RID: 28237 RVA: 0x002F3120 File Offset: 0x002F1520
		protected override void OnUpdate(PlayerActor player, ref Actor.InputInfo info)
		{
			player.NavMeshAgent.velocity = (info.move = Vector3.zero);
		}

		// Token: 0x06006E4E RID: 28238 RVA: 0x002F3146 File Offset: 0x002F1546
		protected override void OnAfterUpdate(PlayerActor player, Actor.InputInfo info)
		{
			player.CharacterTPS.UpdateState(info, ActorLocomotion.UpdateType.Update);
		}
	}
}
