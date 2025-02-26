using System;
using Manager;
using UnityEngine;

namespace AIProject.Player
{
	// Token: 0x02000E13 RID: 3603
	public class WMap : PlayerStateBase
	{
		// Token: 0x06006F7E RID: 28542 RVA: 0x002FDA10 File Offset: 0x002FBE10
		protected override void OnAwake(PlayerActor player)
		{
			this.minimapUI = Singleton<MapUIContainer>.Instance.MinimapUI;
			this.input = Singleton<Manager.Input>.Instance;
			this.dt = 0f;
			this.minimapUI.OpenAllMap(this.minimapUI.VisibleMode);
			Singleton<MapUIContainer>.Instance.MinimapUI.VisibleMode = 1;
			MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.UI);
			Singleton<Manager.Input>.Instance.SetupState();
			this.minimapUI.FromHomeMenu = false;
			Singleton<MapUIContainer>.Instance.MinimapUI.WarpProc = delegate(BasePoint x)
			{
				Singleton<MapUIContainer>.Instance.MinimapUI.AllMapClosedAction = delegate()
				{
				};
				string prevStateName = player.PlayerController.PrevStateName;
				Singleton<Map>.Instance.WarpToBasePoint(x, delegate
				{
					if (prevStateName == "Onbu")
					{
						player.Controller.ChangeState("Onbu");
					}
					else
					{
						player.Controller.ChangeState("Normal");
					}
					player.Controller.ChangeState("Idle");
					GC.Collect();
					if (<OnAwake>c__AnonStorey.minimapUI.prevVisibleMode == 0 && Config.GameData.MiniMap)
					{
						<OnAwake>c__AnonStorey.minimapUI.OpenMiniMap();
					}
				}, delegate
				{
					if (prevStateName == "Onbu")
					{
						player.Controller.ChangeState("Onbu");
					}
					else
					{
						player.Controller.ChangeState("Normal");
					}
					Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.Action);
					Singleton<Manager.Input>.Instance.SetupState();
					Singleton<Map>.Instance.Player.SetScheduledInteractionState(true);
					Singleton<Map>.Instance.Player.ReleaseInteraction();
				});
				Singleton<MapUIContainer>.Instance.MinimapUI.WarpProc = null;
			};
			ActorAnimInfo animInfo = player.Animation.AnimInfo;
			animInfo.outEnableBlend = true;
			animInfo.outBlendSec = 0f;
			player.Animation.AnimInfo = animInfo;
		}

		// Token: 0x06006F7F RID: 28543 RVA: 0x002FDAF8 File Offset: 0x002FBEF8
		protected override void OnUpdate(PlayerActor actor, ref Actor.InputInfo info)
		{
			actor.NavMeshAgent.velocity = (info.move = Vector3.zero);
			this.dt += Time.unscaledDeltaTime;
			if (this.dt < 0.4f)
			{
				return;
			}
			if (this.input.IsPressedKey(KeyCode.M) && !this.minimapUI.nowCloseAllMap)
			{
				this.minimapUI.ChangeCamera(false, false);
				this.minimapUI.WarpMoveDispose();
			}
		}

		// Token: 0x06006F80 RID: 28544 RVA: 0x002FDB7B File Offset: 0x002FBF7B
		protected override void OnAfterUpdate(PlayerActor actor, Actor.InputInfo info)
		{
			actor.CharacterTPS.UpdateState(info, ActorLocomotion.UpdateType.Update);
		}

		// Token: 0x04005C13 RID: 23571
		private MiniMapControler minimapUI;

		// Token: 0x04005C14 RID: 23572
		private Manager.Input input;

		// Token: 0x04005C15 RID: 23573
		private float dt;

		// Token: 0x04005C16 RID: 23574
		private const float nonOpeTime = 0.4f;
	}
}
