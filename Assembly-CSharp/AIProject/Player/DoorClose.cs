using System;
using UniRx;
using UnityEngine;

namespace AIProject.Player
{
	// Token: 0x02000DF2 RID: 3570
	public class DoorClose : PlayerStateBase
	{
		// Token: 0x06006E57 RID: 28247 RVA: 0x002F33A0 File Offset: 0x002F17A0
		protected override void OnAwake(PlayerActor player)
		{
			DoorPoint doorPoint = player.CurrentPoint as DoorPoint;
			if (doorPoint != null)
			{
				DoorPoint.OpenPattern openState = doorPoint.OpenState;
				doorPoint.SetOpenState(DoorPoint.OpenPattern.Close, true);
				this._doorAnimation = doorPoint.GetComponent<DoorAnimation>();
				if (this._doorAnimation != null)
				{
					this._doorAnimation.PlayCloseAnimation(openState);
				}
				doorPoint.SetBookingUser(player);
			}
			this._onEndAction.Take(1).Subscribe(delegate(Unit _)
			{
				this.Elapsed(player);
			});
			ActorAnimInfo animInfo = player.Animation.AnimInfo;
			animInfo.outEnableBlend = true;
			animInfo.outBlendSec = 0f;
			player.Animation.AnimInfo = animInfo;
		}

		// Token: 0x06006E58 RID: 28248 RVA: 0x002F3478 File Offset: 0x002F1878
		protected override void OnUpdate(PlayerActor actor, ref Actor.InputInfo info)
		{
			actor.NavMeshAgent.velocity = (info.move = Vector3.zero);
			if (this._doorAnimation != null && this._doorAnimation.PlayingCloseAnim)
			{
				return;
			}
			if (this._onEndAction != null)
			{
				this._onEndAction.OnNext(Unit.Default);
			}
		}

		// Token: 0x06006E59 RID: 28249 RVA: 0x002F34DD File Offset: 0x002F18DD
		protected override void OnAfterUpdate(PlayerActor actor, Actor.InputInfo info)
		{
			actor.CharacterTPS.UpdateState(info, ActorLocomotion.UpdateType.Update);
		}

		// Token: 0x06006E5A RID: 28250 RVA: 0x002F34EC File Offset: 0x002F18EC
		private void Elapsed(PlayerActor player)
		{
			player.PlayerController.CommandArea.RefreshCommands();
			ActionPoint currentPoint = player.CurrentPoint;
			if (currentPoint != null)
			{
				currentPoint.RemoveBookingUser(player);
			}
			if (player.PlayerController.PrevStateName == "Onbu")
			{
				player.Controller.ChangeState(player.PlayerController.PrevStateName);
			}
			else
			{
				player.Controller.ChangeState("Normal");
			}
		}

		// Token: 0x04005B93 RID: 23443
		protected int _currentState = -1;

		// Token: 0x04005B94 RID: 23444
		private Subject<Unit> _onEndAction = new Subject<Unit>();

		// Token: 0x04005B95 RID: 23445
		private DoorAnimation _doorAnimation;
	}
}
