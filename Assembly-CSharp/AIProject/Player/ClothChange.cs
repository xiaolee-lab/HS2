using System;
using System.Collections;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject.Player
{
	// Token: 0x02000DED RID: 3565
	public class ClothChange : PlayerStateBase
	{
		// Token: 0x06006E39 RID: 28217 RVA: 0x002F2B68 File Offset: 0x002F0F68
		protected override void OnAwake(PlayerActor player)
		{
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.UI);
			Singleton<Manager.Input>.Instance.SetupState();
			MapUIContainer.ClosetUI.CoordinateFilterSource = Singleton<Game>.Instance.Environment.ClosetCoordinateList;
			MapUIContainer.SetActiveClosetUI(true);
			this._onEndMenu.Take(1).Subscribe(delegate(Unit _)
			{
				player.Controller.ChangeState("Normal");
				MapUIContainer.ClosetUI.CoordinateFilterSource = null;
			});
		}

		// Token: 0x06006E3A RID: 28218 RVA: 0x002F2BD4 File Offset: 0x002F0FD4
		protected override void OnRelease(PlayerActor player)
		{
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.Action);
			Singleton<Manager.Input>.Instance.SetupState();
		}

		// Token: 0x06006E3B RID: 28219 RVA: 0x002F2BEC File Offset: 0x002F0FEC
		protected override void OnUpdate(PlayerActor player, ref Actor.InputInfo info)
		{
			player.NavMeshAgent.velocity = (info.move = Vector3.zero);
			if (MapUIContainer.ClosetUI.IsActiveControl)
			{
				return;
			}
			this._onEndMenu.OnNext(Unit.Default);
		}

		// Token: 0x06006E3C RID: 28220 RVA: 0x002F2C32 File Offset: 0x002F1032
		protected override void OnAfterUpdate(PlayerActor player, Actor.InputInfo info)
		{
			player.CharacterTPS.UpdateState(info, ActorLocomotion.UpdateType.Update);
		}

		// Token: 0x06006E3D RID: 28221 RVA: 0x002F2C44 File Offset: 0x002F1044
		protected override IEnumerator OnEnd(PlayerActor player)
		{
			yield break;
		}

		// Token: 0x04005B91 RID: 23441
		private Subject<Unit> _onEndMenu = new Subject<Unit>();
	}
}
