using System;
using AIProject.UI;
using UnityEngine;

namespace AIProject.Player
{
	// Token: 0x02000E12 RID: 3602
	public class Tutorial : PlayerStateBase
	{
		// Token: 0x06006F77 RID: 28535 RVA: 0x002FD904 File Offset: 0x002FBD04
		protected override void OnAwake(PlayerActor player)
		{
			TutorialUI tutorialUI = MapUIContainer.TutorialUI;
			tutorialUI.ClosedEvent = (Action)Delegate.Combine(tutorialUI.ClosedEvent, new Action(delegate()
			{
				EventPoint.ChangePrevPlayerMode();
			}));
			MapUIContainer.SetActiveTutorialUI(true);
			MapUIContainer.SetVisibleHUDExceptStoryUI(false);
		}

		// Token: 0x06006F78 RID: 28536 RVA: 0x002FD954 File Offset: 0x002FBD54
		protected override void OnUpdate(PlayerActor actor, ref Actor.InputInfo info)
		{
			actor.NavMeshAgent.velocity = (info.move = Vector3.zero);
		}

		// Token: 0x06006F79 RID: 28537 RVA: 0x002FD97A File Offset: 0x002FBD7A
		protected override void OnAfterUpdate(PlayerActor actor, Actor.InputInfo info)
		{
			actor.CharacterTPS.UpdateState(info, ActorLocomotion.UpdateType.Update);
		}

		// Token: 0x06006F7A RID: 28538 RVA: 0x002FD98C File Offset: 0x002FBD8C
		protected override void OnRelease(PlayerActor player)
		{
			IState state = player.PlayerController.State;
			if (state is Normal || state is Onbu || state is Houchi)
			{
				EventPoint.ChangeNormalState();
				MapUIContainer.SetVisibleHUD(true);
			}
		}

		// Token: 0x06006F7B RID: 28539 RVA: 0x002FD9D4 File Offset: 0x002FBDD4
		~Tutorial()
		{
		}
	}
}
