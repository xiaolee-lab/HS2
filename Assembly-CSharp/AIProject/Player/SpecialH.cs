using System;
using System.Collections;
using Manager;
using UnityEngine;

namespace AIProject.Player
{
	// Token: 0x02000DE3 RID: 3555
	public class SpecialH : PlayerStateBase
	{
		// Token: 0x06006DED RID: 28141 RVA: 0x002EF074 File Offset: 0x002ED474
		protected override void OnAwake(PlayerActor player)
		{
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.UI);
			Singleton<Manager.Input>.Instance.SetupState();
			DateActionPointInfo dateActionPointInfo;
			player.CurrentPoint.TryGetPlayerDateActionPointInfo(player.ChaControl.sex, EventType.Lesbian, out dateActionPointInfo);
			player.HPoseID = dateActionPointInfo.poseIDA;
			MapUIContainer.RefreshCommands(0, player.SpecialHCommandInfo);
			MapUIContainer.CommandList.CancelEvent = delegate()
			{
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
				MapUIContainer.SetActiveCommandList(false);
				player.Controller.ChangeState("Normal");
				player.ReleaseCurrentPoint();
				if (player.PlayerController.CommandArea != null)
				{
					player.PlayerController.CommandArea.enabled = true;
				}
				MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.InvokeAcception);
				player.ActivateNavMeshAgent();
				player.IsKinematic = false;
			};
			MapUIContainer.SetActiveCommandList(true, "特殊エッチ");
		}

		// Token: 0x06006DEE RID: 28142 RVA: 0x002EF10F File Offset: 0x002ED50F
		protected override void OnRelease(PlayerActor player)
		{
		}

		// Token: 0x06006DEF RID: 28143 RVA: 0x002EF114 File Offset: 0x002ED514
		protected override void OnUpdate(PlayerActor player, ref Actor.InputInfo info)
		{
			player.NavMeshAgent.velocity = (info.move = Vector3.zero);
		}

		// Token: 0x06006DF0 RID: 28144 RVA: 0x002EF13A File Offset: 0x002ED53A
		protected override void OnAfterUpdate(PlayerActor player, Actor.InputInfo info)
		{
			player.CharacterTPS.UpdateState(info, ActorLocomotion.UpdateType.Update);
		}

		// Token: 0x06006DF1 RID: 28145 RVA: 0x002EF14C File Offset: 0x002ED54C
		protected override IEnumerator OnEnd(PlayerActor player)
		{
			yield break;
		}
	}
}
