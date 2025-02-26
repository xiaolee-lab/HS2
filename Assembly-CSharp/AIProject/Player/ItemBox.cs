using System;
using System.Collections;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject.Player
{
	// Token: 0x02000E00 RID: 3584
	public class ItemBox : PlayerStateBase
	{
		// Token: 0x06006EE4 RID: 28388 RVA: 0x002F723C File Offset: 0x002F563C
		protected override void OnAwake(PlayerActor player)
		{
			player.EventKey = EventType.StorageIn;
			if (player.CurrentPoint != null)
			{
				this._chestAnimation = player.CurrentPoint.GetComponent<ChestAnimation>();
				if (this._chestAnimation != null)
				{
					this._chestAnimation.PlayInAnimation();
				}
			}
			this._onEndInAnimation.Take(1).Subscribe(delegate(Unit _)
			{
				MapUIContainer.SetActiveItemBoxUI(true);
			}, delegate()
			{
				this._onEndMenu.Take(1).Subscribe(delegate(Unit _)
				{
					MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.InvokeAcception);
					player.Controller.ChangeState("Normal");
				});
			});
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.UI);
			Singleton<Manager.Input>.Instance.SetupState();
			ActorAnimInfo animInfo = player.Animation.AnimInfo;
			animInfo.outEnableBlend = true;
			animInfo.outBlendSec = 0f;
			player.Animation.AnimInfo = animInfo;
		}

		// Token: 0x06006EE5 RID: 28389 RVA: 0x002F733B File Offset: 0x002F573B
		protected override void OnRelease(PlayerActor player)
		{
			if (this._chestAnimation != null)
			{
				this._chestAnimation.PlayOutAnimation();
			}
		}

		// Token: 0x06006EE6 RID: 28390 RVA: 0x002F7359 File Offset: 0x002F5759
		protected override void OnAfterUpdate(PlayerActor actor, Actor.InputInfo info)
		{
			actor.CharacterTPS.UpdateState(info, ActorLocomotion.UpdateType.Update);
		}

		// Token: 0x06006EE7 RID: 28391 RVA: 0x002F7368 File Offset: 0x002F5768
		protected override void OnUpdate(PlayerActor actor, ref Actor.InputInfo info)
		{
			actor.NavMeshAgent.velocity = (info.move = Vector3.zero);
			this.OnEndInAnimation();
			this.OnEndMenu();
		}

		// Token: 0x06006EE8 RID: 28392 RVA: 0x002F739C File Offset: 0x002F579C
		private void OnEndInAnimation()
		{
			if (this._chestAnimation != null && this._chestAnimation.PlayingInAniamtion)
			{
				return;
			}
			if (this._onEndInAnimation != null)
			{
				this._onEndInAnimation.OnNext(Unit.Default);
			}
		}

		// Token: 0x06006EE9 RID: 28393 RVA: 0x002F73E8 File Offset: 0x002F57E8
		private void OnEndMenu()
		{
			if (MapUIContainer.ItemBoxUI.IsActiveControl)
			{
				return;
			}
			this._onEndMenu.OnNext(Unit.Default);
		}

		// Token: 0x06006EEA RID: 28394 RVA: 0x002F740C File Offset: 0x002F580C
		public override IEnumerator End(Actor actor)
		{
			yield break;
		}

		// Token: 0x04005BCC RID: 23500
		private ChestAnimation _chestAnimation;

		// Token: 0x04005BCD RID: 23501
		private Subject<Unit> _onEndInAnimation = new Subject<Unit>();

		// Token: 0x04005BCE RID: 23502
		private Subject<Unit> _onEndMenu = new Subject<Unit>();
	}
}
