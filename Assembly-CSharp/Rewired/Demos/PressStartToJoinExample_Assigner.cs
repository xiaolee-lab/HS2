using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rewired.Demos
{
	// Token: 0x02000520 RID: 1312
	[AddComponentMenu("")]
	public class PressStartToJoinExample_Assigner : MonoBehaviour
	{
		// Token: 0x06001939 RID: 6457 RVA: 0x0009BD24 File Offset: 0x0009A124
		public static Player GetRewiredPlayer(int gamePlayerId)
		{
			if (!ReInput.isReady)
			{
				return null;
			}
			if (PressStartToJoinExample_Assigner.instance == null)
			{
				UnityEngine.Debug.LogError("Not initialized. Do you have a PressStartToJoinPlayerSelector in your scehe?");
				return null;
			}
			for (int i = 0; i < PressStartToJoinExample_Assigner.instance.playerMap.Count; i++)
			{
				if (PressStartToJoinExample_Assigner.instance.playerMap[i].gamePlayerId == gamePlayerId)
				{
					return ReInput.players.GetPlayer(PressStartToJoinExample_Assigner.instance.playerMap[i].rewiredPlayerId);
				}
			}
			return null;
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x0009BDB5 File Offset: 0x0009A1B5
		private void Awake()
		{
			this.playerMap = new List<PressStartToJoinExample_Assigner.PlayerMap>();
			PressStartToJoinExample_Assigner.instance = this;
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x0009BDC8 File Offset: 0x0009A1C8
		private void Update()
		{
			for (int i = 0; i < ReInput.players.playerCount; i++)
			{
				if (ReInput.players.GetPlayer(i).GetButtonDown("JoinGame"))
				{
					this.AssignNextPlayer(i);
				}
			}
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x0009BE14 File Offset: 0x0009A214
		private void AssignNextPlayer(int rewiredPlayerId)
		{
			if (this.playerMap.Count >= this.maxPlayers)
			{
				UnityEngine.Debug.LogError("Max player limit already reached!");
				return;
			}
			int nextGamePlayerId = this.GetNextGamePlayerId();
			this.playerMap.Add(new PressStartToJoinExample_Assigner.PlayerMap(rewiredPlayerId, nextGamePlayerId));
			Player player = ReInput.players.GetPlayer(rewiredPlayerId);
			player.controllers.maps.SetMapsEnabled(false, "Assignment");
			player.controllers.maps.SetMapsEnabled(true, "Default");
			UnityEngine.Debug.Log(string.Concat(new object[]
			{
				"Added Rewired Player id ",
				rewiredPlayerId,
				" to game player ",
				nextGamePlayerId
			}));
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x0009BEC8 File Offset: 0x0009A2C8
		private int GetNextGamePlayerId()
		{
			return this.gamePlayerIdCounter++;
		}

		// Token: 0x04001C25 RID: 7205
		private static PressStartToJoinExample_Assigner instance;

		// Token: 0x04001C26 RID: 7206
		public int maxPlayers = 4;

		// Token: 0x04001C27 RID: 7207
		private List<PressStartToJoinExample_Assigner.PlayerMap> playerMap;

		// Token: 0x04001C28 RID: 7208
		private int gamePlayerIdCounter;

		// Token: 0x02000521 RID: 1313
		private class PlayerMap
		{
			// Token: 0x0600193E RID: 6462 RVA: 0x0009BEE6 File Offset: 0x0009A2E6
			public PlayerMap(int rewiredPlayerId, int gamePlayerId)
			{
				this.rewiredPlayerId = rewiredPlayerId;
				this.gamePlayerId = gamePlayerId;
			}

			// Token: 0x04001C29 RID: 7209
			public int rewiredPlayerId;

			// Token: 0x04001C2A RID: 7210
			public int gamePlayerId;
		}
	}
}
