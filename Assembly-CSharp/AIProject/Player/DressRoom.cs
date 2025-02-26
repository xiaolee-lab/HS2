using System;
using System.Collections;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject.Player
{
	// Token: 0x02000DF4 RID: 3572
	public class DressRoom : PlayerStateBase
	{
		// Token: 0x06006E62 RID: 28258 RVA: 0x002F3C6C File Offset: 0x002F206C
		protected override void OnAwake(PlayerActor player)
		{
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.UI);
			Singleton<Manager.Input>.Instance.SetupState();
			MapUIContainer.DressRoomUI.CoordinateFilterSource = Singleton<Game>.Instance.WorldData.Environment.DressCoordinateList;
			MapUIContainer.SetActiveDressRoomUI(true);
			this._onEndMenu.Take(1).Subscribe(delegate(Unit _)
			{
				player.Controller.ChangeState("Normal");
				MapUIContainer.DressRoomUI.CoordinateFilterSource = null;
			});
		}

		// Token: 0x06006E63 RID: 28259 RVA: 0x002F3CDD File Offset: 0x002F20DD
		protected override void OnRelease(PlayerActor player)
		{
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.Action);
			Singleton<Manager.Input>.Instance.SetupState();
		}

		// Token: 0x06006E64 RID: 28260 RVA: 0x002F3CF4 File Offset: 0x002F20F4
		protected override void OnUpdate(PlayerActor player, ref Actor.InputInfo info)
		{
			player.NavMeshAgent.velocity = (info.move = Vector3.zero);
			if (MapUIContainer.DressRoomUI.IsActiveControl)
			{
				return;
			}
			this._onEndMenu.OnNext(Unit.Default);
		}

		// Token: 0x06006E65 RID: 28261 RVA: 0x002F3D3A File Offset: 0x002F213A
		protected override void OnAfterUpdate(PlayerActor player, Actor.InputInfo info)
		{
			player.CharacterTPS.UpdateState(info, ActorLocomotion.UpdateType.Update);
		}

		// Token: 0x06006E66 RID: 28262 RVA: 0x002F3D4C File Offset: 0x002F214C
		protected override IEnumerator OnEnd(PlayerActor player)
		{
			yield break;
		}

		// Token: 0x04005B99 RID: 23449
		private Subject<Unit> _onEndMenu = new Subject<Unit>();
	}
}
