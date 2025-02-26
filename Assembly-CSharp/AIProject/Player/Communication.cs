using System;
using Manager;
using UnityEngine;

namespace AIProject.Player
{
	// Token: 0x02000DEE RID: 3566
	public class Communication : PlayerStateBase
	{
		// Token: 0x06006E3F RID: 28223 RVA: 0x002F2CC8 File Offset: 0x002F10C8
		protected override void OnAwake(PlayerActor player)
		{
			player.SetActiveOnEquipedItem(false);
			player.ChaControl.setAllLayerWeight(0f);
			player.ChaControl.visibleAll = false;
			MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);
			player.SetScheduledInteractionState(false);
			player.ReleaseInteraction();
			ActorAnimInfo animInfo = player.Animation.AnimInfo;
			animInfo.outBlendSec = 0f;
			animInfo.outEnableBlend = true;
			player.Animation.AnimInfo = animInfo;
			if (player.CommCompanion != null)
			{
				player.CommCompanion.Animation.BeginIgnoreExpression();
				player.CommCompanion.Animation.BeginIgnoreVoice();
			}
		}

		// Token: 0x06006E40 RID: 28224 RVA: 0x002F2D6C File Offset: 0x002F116C
		protected override void OnRelease(PlayerActor player)
		{
			player.SetScheduledInteractionState(true);
			player.ReleaseInteraction();
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.Action);
			Singleton<Manager.Input>.Instance.SetupState();
			if (player.CommCompanion != null)
			{
				player.CommCompanion.Animation.EndIgnoreExpression();
				player.CommCompanion.Animation.EndIgnoreVoice();
			}
		}

		// Token: 0x06006E41 RID: 28225 RVA: 0x002F2DCC File Offset: 0x002F11CC
		protected override void OnUpdate(PlayerActor player, ref Actor.InputInfo info)
		{
			player.NavMeshAgent.velocity = (info.move = Vector3.zero);
		}

		// Token: 0x06006E42 RID: 28226 RVA: 0x002F2DF2 File Offset: 0x002F11F2
		protected override void OnAfterUpdate(PlayerActor player, Actor.InputInfo info)
		{
			player.CharacterTPS.UpdateState(info, ActorLocomotion.UpdateType.Update);
		}
	}
}
