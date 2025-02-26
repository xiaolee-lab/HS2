using System;
using Manager;
using UnityEngine;

namespace AIProject.Player
{
	// Token: 0x02000DFF RID: 3583
	public class Idle : PlayerStateBase
	{
		// Token: 0x06006EDF RID: 28383 RVA: 0x002F70AC File Offset: 0x002F54AC
		protected override void OnAwake(PlayerActor player)
		{
			this._prevAcceptionState = MapUIContainer.CommandLabel.Acception;
			this._prevValidType = Singleton<Manager.Input>.Instance.State;
			this._prevInteractionState = player.CurrentInteractionState;
			if (this._prevAcceptionState != CommandLabel.AcceptionState.None)
			{
				MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);
			}
			if (this._prevValidType != Manager.Input.ValidType.None)
			{
				Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.None);
				Singleton<Manager.Input>.Instance.SetupState();
			}
			if (this._prevInteractionState)
			{
				player.SetScheduledInteractionState(false);
				player.ReleaseInteraction();
			}
		}

		// Token: 0x06006EE0 RID: 28384 RVA: 0x002F7130 File Offset: 0x002F5530
		protected override void OnRelease(PlayerActor player)
		{
			IState state = player.PlayerController.State;
			if (state is Normal || state is Onbu)
			{
				this._prevInteractionState = true;
				this._prevValidType = Manager.Input.ValidType.Action;
				this._prevAcceptionState = CommandLabel.AcceptionState.InvokeAcception;
			}
			if (this._prevInteractionState != player.CurrentInteractionState)
			{
				player.SetScheduledInteractionState(this._prevInteractionState);
				player.ReleaseInteraction();
			}
			if (this._prevValidType != Singleton<Manager.Input>.Instance.State)
			{
				Singleton<Manager.Input>.Instance.ReserveState(this._prevValidType);
				Singleton<Manager.Input>.Instance.SetupState();
			}
			if (this._prevAcceptionState != MapUIContainer.CommandLabel.Acception)
			{
				MapUIContainer.SetCommandLabelAcception(this._prevAcceptionState);
			}
		}

		// Token: 0x06006EE1 RID: 28385 RVA: 0x002F71E8 File Offset: 0x002F55E8
		protected override void OnUpdate(PlayerActor player, ref Actor.InputInfo info)
		{
			player.NavMeshAgent.velocity = (info.move = Vector3.zero);
		}

		// Token: 0x06006EE2 RID: 28386 RVA: 0x002F720E File Offset: 0x002F560E
		protected override void OnAfterUpdate(PlayerActor player, Actor.InputInfo info)
		{
			player.CharacterTPS.UpdateState(info, ActorLocomotion.UpdateType.Update);
		}

		// Token: 0x04005BC9 RID: 23497
		private CommandLabel.AcceptionState _prevAcceptionState;

		// Token: 0x04005BCA RID: 23498
		private Manager.Input.ValidType _prevValidType;

		// Token: 0x04005BCB RID: 23499
		private bool _prevInteractionState;
	}
}
