using System;
using System.Collections.Generic;
using AIProject.SaveData;
using Manager;
using UnityEngine;

namespace AIProject.Player
{
	// Token: 0x02000DEC RID: 3564
	public class ChickenCoopMenu : PlayerStateBase
	{
		// Token: 0x06006E33 RID: 28211 RVA: 0x002F2920 File Offset: 0x002F0D20
		protected override void OnAwake(PlayerActor player)
		{
			this._currentFarmPoint = player.CurrentFarmPoint;
			if (this._currentFarmPoint == null)
			{
				player.PlayerController.ChangeState("Normal");
				return;
			}
			this._input = Singleton<Manager.Input>.Instance;
			Manager.Input.ValidType state = this._input.State;
			this._input.ReserveState(Manager.Input.ValidType.UI);
			this._input.SetupState();
			this._input.ReserveState(state);
			MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);
			MapUIContainer.SetVisibleHUD(false);
			int registerID = this._currentFarmPoint.RegisterID;
			List<AIProject.SaveData.Environment.ChickenInfo> list = null;
			Dictionary<int, List<AIProject.SaveData.Environment.ChickenInfo>> dictionary;
			if (Singleton<Game>.IsInstance())
			{
				AIProject.SaveData.Environment environment = Singleton<Game>.Instance.Environment;
				dictionary = ((environment != null) ? environment.ChickenTable : null);
			}
			else
			{
				dictionary = null;
			}
			Dictionary<int, List<AIProject.SaveData.Environment.ChickenInfo>> dictionary2 = dictionary;
			if (dictionary2 != null && (!dictionary2.TryGetValue(registerID, out list) || list == null))
			{
				List<AIProject.SaveData.Environment.ChickenInfo> list2 = new List<AIProject.SaveData.Environment.ChickenInfo>();
				dictionary2[registerID] = list2;
				list = list2;
			}
			if (list == null)
			{
				list = new List<AIProject.SaveData.Environment.ChickenInfo>();
			}
			MapUIContainer.ChickenCoopUI.currentChickens = list;
			MapUIContainer.ChickenCoopUI.ClosedEvent = delegate()
			{
				MapUIContainer.CommandList.Visibled = true;
			};
			MapUIContainer.RefreshCommands(0, player.ChickenCoopCommandInfos);
			MapUIContainer.CommandList.CancelEvent = delegate()
			{
				MapUIContainer.SetActiveCommandList(false);
				player.PlayerController.ChangeState("Normal");
			};
			MapUIContainer.SetActiveCommandList(true, "鶏小屋");
		}

		// Token: 0x06006E34 RID: 28212 RVA: 0x002F2A94 File Offset: 0x002F0E94
		protected override void OnUpdate(PlayerActor player, ref Actor.InputInfo info)
		{
			player.NavMeshAgent.velocity = (info.move = Vector3.zero);
		}

		// Token: 0x06006E35 RID: 28213 RVA: 0x002F2ABA File Offset: 0x002F0EBA
		protected override void OnAfterUpdate(PlayerActor player, Actor.InputInfo info)
		{
			player.CharacterTPS.UpdateState(info, ActorLocomotion.UpdateType.Update);
		}

		// Token: 0x06006E36 RID: 28214 RVA: 0x002F2ACC File Offset: 0x002F0ECC
		protected override void OnRelease(PlayerActor player)
		{
			if (this._currentFarmPoint == null)
			{
				return;
			}
			player.CurrentFarmPoint = null;
			MapUIContainer.ChickenCoopUI.ClosedEvent = null;
			MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.InvokeAcception);
			MapUIContainer.SetVisibleHUD(true);
			if (this._input != null)
			{
				this._input.SetupState();
			}
		}

		// Token: 0x04005B8E RID: 23438
		private Manager.Input _input;

		// Token: 0x04005B8F RID: 23439
		private FarmPoint _currentFarmPoint;
	}
}
