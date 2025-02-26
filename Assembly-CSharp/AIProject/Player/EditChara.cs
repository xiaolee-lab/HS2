using System;
using System.Collections;
using System.Collections.Generic;
using AIProject.SaveData;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject.Player
{
	// Token: 0x02000DF5 RID: 3573
	public class EditChara : PlayerStateBase
	{
		// Token: 0x06006E68 RID: 28264 RVA: 0x002F3DF0 File Offset: 0x002F21F0
		protected override void OnAwake(PlayerActor player)
		{
			MapUIContainer.SetActiveCharaChangeUI(true);
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.UI);
			Singleton<Manager.Input>.Instance.SetupState();
			player.SetScheduledInteractionState(false);
			player.ReleaseInteraction();
			MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);
			this._agentCharaFiles.Clear();
			foreach (KeyValuePair<int, AgentData> keyValuePair in Singleton<Game>.Instance.WorldData.AgentTable)
			{
				this._agentCharaFiles[keyValuePair.Key] = keyValuePair.Value.CharaFileName;
				this._agentCharaMapIDs[keyValuePair.Key] = keyValuePair.Value.MapID;
			}
			this._onEndMenu.Take(1).Subscribe(delegate(Unit _)
			{
				if (this.CheckChange(player))
				{
					return;
				}
				player.Controller.ChangeState("DeviceMenu");
			});
		}

		// Token: 0x06006E69 RID: 28265 RVA: 0x002F3F00 File Offset: 0x002F2300
		protected override void OnRelease(PlayerActor player)
		{
			player.PlayerController.CommandArea.RefreshCommands();
		}

		// Token: 0x06006E6A RID: 28266 RVA: 0x002F3F14 File Offset: 0x002F2314
		protected override void OnUpdate(PlayerActor player, ref Actor.InputInfo info)
		{
			player.NavMeshAgent.velocity = (info.move = Vector3.zero);
			if (MapUIContainer.CharaChangeUI.IsActiveControl)
			{
				return;
			}
			this._onEndMenu.OnNext(Unit.Default);
		}

		// Token: 0x06006E6B RID: 28267 RVA: 0x002F3F5A File Offset: 0x002F235A
		protected override void OnAfterUpdate(PlayerActor player, Actor.InputInfo info)
		{
			player.CharacterTPS.UpdateState(info, ActorLocomotion.UpdateType.Update);
		}

		// Token: 0x06006E6C RID: 28268 RVA: 0x002F3F6C File Offset: 0x002F236C
		public override IEnumerator End(Actor actor)
		{
			yield break;
		}

		// Token: 0x06006E6D RID: 28269 RVA: 0x002F3F80 File Offset: 0x002F2380
		private bool CheckChange(PlayerActor player)
		{
			WorldData worldData = Singleton<Game>.Instance.WorldData;
			Singleton<Map>.Instance.ChangedCharaFiles.Clear();
			foreach (KeyValuePair<int, AgentData> keyValuePair in worldData.AgentTable)
			{
				if (this._agentCharaFiles[keyValuePair.Key] != keyValuePair.Value.CharaFileName || this._agentCharaMapIDs[keyValuePair.Key] != keyValuePair.Value.MapID)
				{
					keyValuePair.Value.ResetAssignedDuration();
					int randomValue = Singleton<Manager.Resources>.Instance.AgentProfile.DayRandElapseCheck.RandomValue;
					keyValuePair.Value.SetADVEventTimeCond(randomValue);
					keyValuePair.Value.ResetADVEventTimeCount();
					Singleton<Map>.Instance.ChangedCharaFiles[keyValuePair.Key] = keyValuePair.Value.CharaFileName;
				}
			}
			if (Singleton<Map>.Instance.ChangedCharaFiles.Count > 0)
			{
				player.Controller.ChangeState("CharaChange");
				return true;
			}
			return false;
		}

		// Token: 0x04005B9A RID: 23450
		private Subject<Unit> _onEndMenu = new Subject<Unit>();

		// Token: 0x04005B9B RID: 23451
		private Dictionary<int, string> _agentCharaFiles = new Dictionary<int, string>();

		// Token: 0x04005B9C RID: 23452
		private Dictionary<int, int> _agentCharaMapIDs = new Dictionary<int, int>();
	}
}
