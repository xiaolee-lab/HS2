using System;
using System.Collections;
using Manager;
using UnityEngine;

namespace AIProject.Player
{
	// Token: 0x02000DFC RID: 3580
	public class Harvest : PlayerStateBase
	{
		// Token: 0x06006ECD RID: 28365 RVA: 0x002F68CB File Offset: 0x002F4CCB
		protected override void OnAwake(PlayerActor player)
		{
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.UI);
			Singleton<Manager.Input>.Instance.SetupState();
		}

		// Token: 0x06006ECE RID: 28366 RVA: 0x002F68E2 File Offset: 0x002F4CE2
		protected override void OnRelease(PlayerActor player)
		{
		}

		// Token: 0x06006ECF RID: 28367 RVA: 0x002F68E4 File Offset: 0x002F4CE4
		protected override void OnUpdate(PlayerActor player, ref Actor.InputInfo info)
		{
			player.NavMeshAgent.velocity = (info.move = Vector3.zero);
		}

		// Token: 0x06006ED0 RID: 28368 RVA: 0x002F690A File Offset: 0x002F4D0A
		protected override void OnAfterUpdate(PlayerActor player, Actor.InputInfo info)
		{
			player.CharacterTPS.UpdateState(info, ActorLocomotion.UpdateType.Update);
		}

		// Token: 0x06006ED1 RID: 28369 RVA: 0x002F691C File Offset: 0x002F4D1C
		public override IEnumerator End(Actor actor)
		{
			yield break;
		}
	}
}
