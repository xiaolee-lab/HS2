using System;
using System.Collections;
using System.Collections.Generic;
using AIProject.SaveData;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject.Player
{
	// Token: 0x02000DEB RID: 3563
	public class CharaMigration : PlayerStateBase
	{
		// Token: 0x06006E2C RID: 28204 RVA: 0x002F2604 File Offset: 0x002F0A04
		protected override void OnAwake(PlayerActor player)
		{
			MapUIContainer.SetActiveCharaMigrationUI(true);
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.UI);
			Singleton<Manager.Input>.Instance.SetupState();
			player.SetScheduledInteractionState(false);
			player.ReleaseInteraction();
			MapUIContainer.SetCommandLabelAcception(CommandLabel.AcceptionState.None);
			this._agentCharaFiles.Clear();
			this._agentCharaMapIDs.Clear();
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

		// Token: 0x06006E2D RID: 28205 RVA: 0x002F2720 File Offset: 0x002F0B20
		protected override void OnRelease(PlayerActor player)
		{
			player.PlayerController.CommandArea.RefreshCommands();
		}

		// Token: 0x06006E2E RID: 28206 RVA: 0x002F2734 File Offset: 0x002F0B34
		protected override void OnUpdate(PlayerActor player, ref Actor.InputInfo info)
		{
			player.NavMeshAgent.velocity = (info.move = Vector3.zero);
			if (MapUIContainer.CharaMigrateUI.IsActiveControl)
			{
				return;
			}
			this._onEndMenu.OnNext(Unit.Default);
		}

		// Token: 0x06006E2F RID: 28207 RVA: 0x002F277A File Offset: 0x002F0B7A
		protected override void OnAfterUpdate(PlayerActor player, Actor.InputInfo info)
		{
			player.CharacterTPS.UpdateState(info, ActorLocomotion.UpdateType.Update);
		}

		// Token: 0x06006E30 RID: 28208 RVA: 0x002F278C File Offset: 0x002F0B8C
		public override IEnumerator End(Actor actor)
		{
			yield break;
		}

		// Token: 0x06006E31 RID: 28209 RVA: 0x002F27A0 File Offset: 0x002F0BA0
		private bool CheckChange(PlayerActor player)
		{
			WorldData worldData = Singleton<Game>.Instance.WorldData;
			Singleton<Map>.Instance.ChangedCharaFiles.Clear();
			foreach (KeyValuePair<int, AgentData> keyValuePair in worldData.AgentTable)
			{
				if (this._agentCharaFiles[keyValuePair.Key] != keyValuePair.Value.CharaFileName || this._agentCharaMapIDs[keyValuePair.Key] != keyValuePair.Value.MapID)
				{
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

		// Token: 0x04005B8B RID: 23435
		private Subject<Unit> _onEndMenu = new Subject<Unit>();

		// Token: 0x04005B8C RID: 23436
		private Dictionary<int, string> _agentCharaFiles = new Dictionary<int, string>();

		// Token: 0x04005B8D RID: 23437
		private Dictionary<int, int> _agentCharaMapIDs = new Dictionary<int, int>();
	}
}
