using System;
using System.Collections;
using AIProject.Scene;
using Manager;
using UnityEngine;

namespace AIProject.Player
{
	// Token: 0x02000DF1 RID: 3569
	public class DeviceMenu : PlayerStateBase
	{
		// Token: 0x06006E50 RID: 28240 RVA: 0x002F318C File Offset: 0x002F158C
		protected override void OnAwake(PlayerActor player)
		{
			Singleton<MapScene>.Instance.SaveProfile(true);
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.UI);
			Singleton<Manager.Input>.Instance.SetupState();
			MapUIContainer.SetVisibleHUDExceptStoryUI(false);
			MapUIContainer.StorySupportUI.Close();
			int id = 6;
			if (MapUIContainer.OpenOnceTutorial(id, false))
			{
				MapUIContainer.TutorialUI.ClosedEvent = delegate()
				{
					this.OnStart(player);
				};
			}
			else
			{
				this.OnStart(player);
			}
		}

		// Token: 0x06006E51 RID: 28241 RVA: 0x002F3214 File Offset: 0x002F1614
		private void OnStart(PlayerActor player)
		{
			Singleton<Map>.Instance.AccessDeviceID = player.CurrentDevicePoint.ID;
			MapUIContainer.RefreshCommands(0, player.DeviceCommandInfos);
			MapUIContainer.CommandList.CancelEvent = delegate()
			{
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
				Singleton<Map>.Instance.AccessDeviceID = -1;
				MapUIContainer.SetActiveCommandList(false);
				MapUIContainer.SetVisibleHUDExceptStoryUI(true);
				MapUIContainer.StorySupportUI.Open();
				player.Controller.ChangeState("Normal");
				player.CurrentDevicePoint = null;
			};
			MapUIContainer.SetActiveCommandList(true, "データ端末");
		}

		// Token: 0x06006E52 RID: 28242 RVA: 0x002F327A File Offset: 0x002F167A
		protected override void OnRelease(PlayerActor player)
		{
		}

		// Token: 0x06006E53 RID: 28243 RVA: 0x002F327C File Offset: 0x002F167C
		protected override void OnUpdate(PlayerActor player, ref Actor.InputInfo info)
		{
			player.NavMeshAgent.velocity = (info.move = Vector3.zero);
		}

		// Token: 0x06006E54 RID: 28244 RVA: 0x002F32A2 File Offset: 0x002F16A2
		protected override void OnAfterUpdate(PlayerActor player, Actor.InputInfo info)
		{
			player.CharacterTPS.UpdateState(info, ActorLocomotion.UpdateType.Update);
		}

		// Token: 0x06006E55 RID: 28245 RVA: 0x002F32B4 File Offset: 0x002F16B4
		public override IEnumerator End(Actor actor)
		{
			yield break;
		}
	}
}
