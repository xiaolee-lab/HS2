using System;
using System.Collections;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject.Player
{
	// Token: 0x02000E0A RID: 3594
	public class Pantry : PlayerStateBase
	{
		// Token: 0x06006F3C RID: 28476 RVA: 0x002FB574 File Offset: 0x002F9974
		protected override void OnAwake(PlayerActor player)
		{
			MapUIContainer.SetActiveRefrigeratorUI(true);
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

		// Token: 0x06006F3D RID: 28477 RVA: 0x002FB61C File Offset: 0x002F9A1C
		protected override void OnUpdate(PlayerActor player, ref Actor.InputInfo info)
		{
			player.NavMeshAgent.velocity = (info.move = Vector3.zero);
			if (MapUIContainer.RefrigeratorUI.IsActiveControl)
			{
				return;
			}
			this._onEndMenu.OnNext(Unit.Default);
		}

		// Token: 0x06006F3E RID: 28478 RVA: 0x002FB662 File Offset: 0x002F9A62
		protected override void OnAfterUpdate(PlayerActor player, Actor.InputInfo info)
		{
			player.CharacterTPS.UpdateState(info, ActorLocomotion.UpdateType.Update);
		}

		// Token: 0x06006F3F RID: 28479 RVA: 0x002FB674 File Offset: 0x002F9A74
		public override IEnumerator End(Actor actor)
		{
			yield break;
		}

		// Token: 0x06006F40 RID: 28480 RVA: 0x002FB688 File Offset: 0x002F9A88
		public override void Release(Actor actor, EventType type)
		{
			this.OnRelease(actor as PlayerActor);
		}

		// Token: 0x06006F41 RID: 28481 RVA: 0x002FB696 File Offset: 0x002F9A96
		protected override void OnRelease(PlayerActor player)
		{
		}

		// Token: 0x04005BFA RID: 23546
		private Subject<Unit> _onEndMenu = new Subject<Unit>();
	}
}
