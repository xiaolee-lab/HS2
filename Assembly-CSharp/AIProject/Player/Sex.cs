using System;
using System.Collections;
using UnityEngine;

namespace AIProject.Player
{
	// Token: 0x02000E0F RID: 3599
	public class Sex : PlayerStateBase
	{
		// Token: 0x06006F62 RID: 28514 RVA: 0x002FCFD8 File Offset: 0x002FB3D8
		protected override void OnAwake(PlayerActor player)
		{
			player.ChaControl.visibleAll = false;
			MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);
			player.SetScheduledInteractionState(false);
			player.ReleaseInteraction();
			if (player.CommCompanion != null)
			{
				player.CommCompanion.Animation.BeginIgnoreExpression();
			}
		}

		// Token: 0x06006F63 RID: 28515 RVA: 0x002FD028 File Offset: 0x002FB428
		public override IEnumerator End(Actor actor)
		{
			yield break;
		}

		// Token: 0x06006F64 RID: 28516 RVA: 0x002FD03C File Offset: 0x002FB43C
		protected override void OnAfterUpdate(PlayerActor actor, Actor.InputInfo info)
		{
			actor.CharacterTPS.UpdateState(info, ActorLocomotion.UpdateType.Update);
		}

		// Token: 0x06006F65 RID: 28517 RVA: 0x002FD04C File Offset: 0x002FB44C
		protected override void OnUpdate(PlayerActor actor, ref Actor.InputInfo info)
		{
			actor.NavMeshAgent.velocity = (info.move = Vector3.zero);
		}
	}
}
