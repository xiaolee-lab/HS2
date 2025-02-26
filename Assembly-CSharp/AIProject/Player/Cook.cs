using System;
using System.Collections;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject.Player
{
	// Token: 0x02000DEF RID: 3567
	public class Cook : PlayerStateBase
	{
		// Token: 0x06006E44 RID: 28228 RVA: 0x002F2E14 File Offset: 0x002F1214
		protected override void OnAwake(PlayerActor player)
		{
			MapUIContainer.SetActiveCookingUI(true);
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.UI);
			Singleton<Manager.Input>.Instance.SetupState();
			player.SetScheduledInteractionState(false);
			player.ReleaseInteraction();
			MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);
			ActorAnimInfo animInfo = player.Animation.AnimInfo;
			animInfo.outEnableBlend = true;
			animInfo.outBlendSec = 0f;
			player.Animation.AnimInfo = animInfo;
			this._onEndMenu.Take(1).Subscribe(delegate(Unit _)
			{
				player.Controller.ChangeState("Kitchen");
			});
		}

		// Token: 0x06006E45 RID: 28229 RVA: 0x002F2EBC File Offset: 0x002F12BC
		protected override void OnUpdate(PlayerActor player, ref Actor.InputInfo info)
		{
			player.NavMeshAgent.velocity = (info.move = Vector3.zero);
			if (MapUIContainer.CookingUI.IsActiveControl)
			{
				return;
			}
			this._onEndMenu.OnNext(Unit.Default);
		}

		// Token: 0x06006E46 RID: 28230 RVA: 0x002F2F02 File Offset: 0x002F1302
		protected override void OnAfterUpdate(PlayerActor player, Actor.InputInfo info)
		{
			player.CharacterTPS.UpdateState(info, ActorLocomotion.UpdateType.Update);
		}

		// Token: 0x06006E47 RID: 28231 RVA: 0x002F2F14 File Offset: 0x002F1314
		public override IEnumerator End(Actor actor)
		{
			yield break;
		}

		// Token: 0x06006E48 RID: 28232 RVA: 0x002F2F28 File Offset: 0x002F1328
		public override void Release(Actor actor, EventType type)
		{
			this.OnRelease(actor as PlayerActor);
		}

		// Token: 0x06006E49 RID: 28233 RVA: 0x002F2F36 File Offset: 0x002F1336
		protected override void OnRelease(PlayerActor player)
		{
		}

		// Token: 0x04005B92 RID: 23442
		private Subject<Unit> _onEndMenu = new Subject<Unit>();
	}
}
