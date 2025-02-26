using System;
using System.Collections;
using System.Collections.Generic;
using Rewired.Utils.Libraries.TinyJson;
using UnityEngine;

namespace Rewired.Data
{
	// Token: 0x02000584 RID: 1412
	public class UserDataStore_PlayerPrefs : UserDataStore
	{
		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06002015 RID: 8213 RVA: 0x000AFC4C File Offset: 0x000AE04C
		// (set) Token: 0x06002016 RID: 8214 RVA: 0x000AFC54 File Offset: 0x000AE054
		public bool IsEnabled
		{
			get
			{
				return this.isEnabled;
			}
			set
			{
				this.isEnabled = value;
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06002017 RID: 8215 RVA: 0x000AFC5D File Offset: 0x000AE05D
		// (set) Token: 0x06002018 RID: 8216 RVA: 0x000AFC65 File Offset: 0x000AE065
		public bool LoadDataOnStart
		{
			get
			{
				return this.loadDataOnStart;
			}
			set
			{
				this.loadDataOnStart = value;
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06002019 RID: 8217 RVA: 0x000AFC6E File Offset: 0x000AE06E
		// (set) Token: 0x0600201A RID: 8218 RVA: 0x000AFC76 File Offset: 0x000AE076
		public bool LoadJoystickAssignments
		{
			get
			{
				return this.loadJoystickAssignments;
			}
			set
			{
				this.loadJoystickAssignments = value;
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x0600201B RID: 8219 RVA: 0x000AFC7F File Offset: 0x000AE07F
		// (set) Token: 0x0600201C RID: 8220 RVA: 0x000AFC87 File Offset: 0x000AE087
		public bool LoadKeyboardAssignments
		{
			get
			{
				return this.loadKeyboardAssignments;
			}
			set
			{
				this.loadKeyboardAssignments = value;
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x0600201D RID: 8221 RVA: 0x000AFC90 File Offset: 0x000AE090
		// (set) Token: 0x0600201E RID: 8222 RVA: 0x000AFC98 File Offset: 0x000AE098
		public bool LoadMouseAssignments
		{
			get
			{
				return this.loadMouseAssignments;
			}
			set
			{
				this.loadMouseAssignments = value;
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x0600201F RID: 8223 RVA: 0x000AFCA1 File Offset: 0x000AE0A1
		// (set) Token: 0x06002020 RID: 8224 RVA: 0x000AFCA9 File Offset: 0x000AE0A9
		public string PlayerPrefsKeyPrefix
		{
			get
			{
				return this.playerPrefsKeyPrefix;
			}
			set
			{
				this.playerPrefsKeyPrefix = value;
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06002021 RID: 8225 RVA: 0x000AFCB2 File Offset: 0x000AE0B2
		private string playerPrefsKey_controllerAssignments
		{
			get
			{
				return string.Format("{0}_{1}", this.playerPrefsKeyPrefix, "ControllerAssignments");
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06002022 RID: 8226 RVA: 0x000AFCC9 File Offset: 0x000AE0C9
		private bool loadControllerAssignments
		{
			get
			{
				return this.loadKeyboardAssignments || this.loadMouseAssignments || this.loadJoystickAssignments;
			}
		}

		// Token: 0x06002023 RID: 8227 RVA: 0x000AFCEA File Offset: 0x000AE0EA
		public override void Save()
		{
			if (!this.isEnabled)
			{
				UnityEngine.Debug.LogWarning("Rewired: UserDataStore_PlayerPrefs is disabled and will not save any data.", this);
				return;
			}
			this.SaveAll();
		}

		// Token: 0x06002024 RID: 8228 RVA: 0x000AFD09 File Offset: 0x000AE109
		public override void SaveControllerData(int playerId, ControllerType controllerType, int controllerId)
		{
			if (!this.isEnabled)
			{
				UnityEngine.Debug.LogWarning("Rewired: UserDataStore_PlayerPrefs is disabled and will not save any data.", this);
				return;
			}
			this.SaveControllerDataNow(playerId, controllerType, controllerId);
		}

		// Token: 0x06002025 RID: 8229 RVA: 0x000AFD2B File Offset: 0x000AE12B
		public override void SaveControllerData(ControllerType controllerType, int controllerId)
		{
			if (!this.isEnabled)
			{
				UnityEngine.Debug.LogWarning("Rewired: UserDataStore_PlayerPrefs is disabled and will not save any data.", this);
				return;
			}
			this.SaveControllerDataNow(controllerType, controllerId);
		}

		// Token: 0x06002026 RID: 8230 RVA: 0x000AFD4C File Offset: 0x000AE14C
		public override void SavePlayerData(int playerId)
		{
			if (!this.isEnabled)
			{
				UnityEngine.Debug.LogWarning("Rewired: UserDataStore_PlayerPrefs is disabled and will not save any data.", this);
				return;
			}
			this.SavePlayerDataNow(playerId);
		}

		// Token: 0x06002027 RID: 8231 RVA: 0x000AFD6C File Offset: 0x000AE16C
		public override void SaveInputBehavior(int playerId, int behaviorId)
		{
			if (!this.isEnabled)
			{
				UnityEngine.Debug.LogWarning("Rewired: UserDataStore_PlayerPrefs is disabled and will not save any data.", this);
				return;
			}
			this.SaveInputBehaviorNow(playerId, behaviorId);
		}

		// Token: 0x06002028 RID: 8232 RVA: 0x000AFD90 File Offset: 0x000AE190
		public override void Load()
		{
			if (!this.isEnabled)
			{
				UnityEngine.Debug.LogWarning("Rewired: UserDataStore_PlayerPrefs is disabled and will not load any data.", this);
				return;
			}
			int num = this.LoadAll();
		}

		// Token: 0x06002029 RID: 8233 RVA: 0x000AFDBC File Offset: 0x000AE1BC
		public override void LoadControllerData(int playerId, ControllerType controllerType, int controllerId)
		{
			if (!this.isEnabled)
			{
				UnityEngine.Debug.LogWarning("Rewired: UserDataStore_PlayerPrefs is disabled and will not load any data.", this);
				return;
			}
			int num = this.LoadControllerDataNow(playerId, controllerType, controllerId);
		}

		// Token: 0x0600202A RID: 8234 RVA: 0x000AFDEC File Offset: 0x000AE1EC
		public override void LoadControllerData(ControllerType controllerType, int controllerId)
		{
			if (!this.isEnabled)
			{
				UnityEngine.Debug.LogWarning("Rewired: UserDataStore_PlayerPrefs is disabled and will not load any data.", this);
				return;
			}
			int num = this.LoadControllerDataNow(controllerType, controllerId);
		}

		// Token: 0x0600202B RID: 8235 RVA: 0x000AFE1C File Offset: 0x000AE21C
		public override void LoadPlayerData(int playerId)
		{
			if (!this.isEnabled)
			{
				UnityEngine.Debug.LogWarning("Rewired: UserDataStore_PlayerPrefs is disabled and will not load any data.", this);
				return;
			}
			int num = this.LoadPlayerDataNow(playerId);
		}

		// Token: 0x0600202C RID: 8236 RVA: 0x000AFE48 File Offset: 0x000AE248
		public override void LoadInputBehavior(int playerId, int behaviorId)
		{
			if (!this.isEnabled)
			{
				UnityEngine.Debug.LogWarning("Rewired: UserDataStore_PlayerPrefs is disabled and will not load any data.", this);
				return;
			}
			int num = this.LoadInputBehaviorNow(playerId, behaviorId);
		}

		// Token: 0x0600202D RID: 8237 RVA: 0x000AFE75 File Offset: 0x000AE275
		protected override void OnInitialize()
		{
			if (this.loadDataOnStart)
			{
				this.Load();
				if (this.loadControllerAssignments && ReInput.controllers.joystickCount > 0)
				{
					this.SaveControllerAssignments();
				}
			}
		}

		// Token: 0x0600202E RID: 8238 RVA: 0x000AFEAC File Offset: 0x000AE2AC
		protected override void OnControllerConnected(ControllerStatusChangedEventArgs args)
		{
			if (!this.isEnabled)
			{
				return;
			}
			if (args.controllerType == ControllerType.Joystick)
			{
				int num = this.LoadJoystickData(args.controllerId);
				if (this.loadDataOnStart && this.loadJoystickAssignments && !this.wasJoystickEverDetected)
				{
					base.StartCoroutine(this.LoadJoystickAssignmentsDeferred());
				}
				if (this.loadJoystickAssignments && !this.deferredJoystickAssignmentLoadPending)
				{
					this.SaveControllerAssignments();
				}
				this.wasJoystickEverDetected = true;
			}
		}

		// Token: 0x0600202F RID: 8239 RVA: 0x000AFF30 File Offset: 0x000AE330
		protected override void OnControllerPreDiscconnect(ControllerStatusChangedEventArgs args)
		{
			if (!this.isEnabled)
			{
				return;
			}
			if (args.controllerType == ControllerType.Joystick)
			{
				this.SaveJoystickData(args.controllerId);
			}
		}

		// Token: 0x06002030 RID: 8240 RVA: 0x000AFF56 File Offset: 0x000AE356
		protected override void OnControllerDisconnected(ControllerStatusChangedEventArgs args)
		{
			if (!this.isEnabled)
			{
				return;
			}
			if (this.loadControllerAssignments)
			{
				this.SaveControllerAssignments();
			}
		}

		// Token: 0x06002031 RID: 8241 RVA: 0x000AFF78 File Offset: 0x000AE378
		private int LoadAll()
		{
			int num = 0;
			if (this.loadControllerAssignments && this.LoadControllerAssignmentsNow())
			{
				num++;
			}
			IList<Player> allPlayers = ReInput.players.AllPlayers;
			for (int i = 0; i < allPlayers.Count; i++)
			{
				num += this.LoadPlayerDataNow(allPlayers[i]);
			}
			return num + this.LoadAllJoystickCalibrationData();
		}

		// Token: 0x06002032 RID: 8242 RVA: 0x000AFFDD File Offset: 0x000AE3DD
		private int LoadPlayerDataNow(int playerId)
		{
			return this.LoadPlayerDataNow(ReInput.players.GetPlayer(playerId));
		}

		// Token: 0x06002033 RID: 8243 RVA: 0x000AFFF0 File Offset: 0x000AE3F0
		private int LoadPlayerDataNow(Player player)
		{
			if (player == null)
			{
				return 0;
			}
			int num = 0;
			num += this.LoadInputBehaviors(player.id);
			num += this.LoadControllerMaps(player.id, ControllerType.Keyboard, 0);
			num += this.LoadControllerMaps(player.id, ControllerType.Mouse, 0);
			foreach (Joystick joystick in player.controllers.Joysticks)
			{
				num += this.LoadControllerMaps(player.id, ControllerType.Joystick, joystick.id);
			}
			return num;
		}

		// Token: 0x06002034 RID: 8244 RVA: 0x000B009C File Offset: 0x000AE49C
		private int LoadAllJoystickCalibrationData()
		{
			int num = 0;
			IList<Joystick> joysticks = ReInput.controllers.Joysticks;
			for (int i = 0; i < joysticks.Count; i++)
			{
				num += this.LoadJoystickCalibrationData(joysticks[i]);
			}
			return num;
		}

		// Token: 0x06002035 RID: 8245 RVA: 0x000B00DE File Offset: 0x000AE4DE
		private int LoadJoystickCalibrationData(Joystick joystick)
		{
			if (joystick == null)
			{
				return 0;
			}
			return (!joystick.ImportCalibrationMapFromXmlString(this.GetJoystickCalibrationMapXml(joystick))) ? 0 : 1;
		}

		// Token: 0x06002036 RID: 8246 RVA: 0x000B0101 File Offset: 0x000AE501
		private int LoadJoystickCalibrationData(int joystickId)
		{
			return this.LoadJoystickCalibrationData(ReInput.controllers.GetJoystick(joystickId));
		}

		// Token: 0x06002037 RID: 8247 RVA: 0x000B0114 File Offset: 0x000AE514
		private int LoadJoystickData(int joystickId)
		{
			int num = 0;
			IList<Player> allPlayers = ReInput.players.AllPlayers;
			for (int i = 0; i < allPlayers.Count; i++)
			{
				Player player = allPlayers[i];
				if (player.controllers.ContainsController(ControllerType.Joystick, joystickId))
				{
					num += this.LoadControllerMaps(player.id, ControllerType.Joystick, joystickId);
				}
			}
			return num + this.LoadJoystickCalibrationData(joystickId);
		}

		// Token: 0x06002038 RID: 8248 RVA: 0x000B0180 File Offset: 0x000AE580
		private int LoadControllerDataNow(int playerId, ControllerType controllerType, int controllerId)
		{
			int num = 0;
			num += this.LoadControllerMaps(playerId, controllerType, controllerId);
			return num + this.LoadControllerDataNow(controllerType, controllerId);
		}

		// Token: 0x06002039 RID: 8249 RVA: 0x000B01A8 File Offset: 0x000AE5A8
		private int LoadControllerDataNow(ControllerType controllerType, int controllerId)
		{
			int num = 0;
			if (controllerType == ControllerType.Joystick)
			{
				num += this.LoadJoystickCalibrationData(controllerId);
			}
			return num;
		}

		// Token: 0x0600203A RID: 8250 RVA: 0x000B01CC File Offset: 0x000AE5CC
		private int LoadControllerMaps(int playerId, ControllerType controllerType, int controllerId)
		{
			int num = 0;
			Player player = ReInput.players.GetPlayer(playerId);
			if (player == null)
			{
				return num;
			}
			Controller controller = ReInput.controllers.GetController(controllerType, controllerId);
			if (controller == null)
			{
				return num;
			}
			List<UserDataStore_PlayerPrefs.SavedControllerMapData> allControllerMapsXml = this.GetAllControllerMapsXml(player, true, controller);
			if (allControllerMapsXml.Count == 0)
			{
				return num;
			}
			num += player.controllers.maps.AddMapsFromXml(controllerType, controllerId, UserDataStore_PlayerPrefs.SavedControllerMapData.GetXmlStringList(allControllerMapsXml));
			this.AddDefaultMappingsForNewActions(player, allControllerMapsXml, controllerType, controllerId);
			return num;
		}

		// Token: 0x0600203B RID: 8251 RVA: 0x000B0244 File Offset: 0x000AE644
		private int LoadInputBehaviors(int playerId)
		{
			Player player = ReInput.players.GetPlayer(playerId);
			if (player == null)
			{
				return 0;
			}
			int num = 0;
			IList<InputBehavior> inputBehaviors = ReInput.mapping.GetInputBehaviors(player.id);
			for (int i = 0; i < inputBehaviors.Count; i++)
			{
				num += this.LoadInputBehaviorNow(player, inputBehaviors[i]);
			}
			return num;
		}

		// Token: 0x0600203C RID: 8252 RVA: 0x000B02A4 File Offset: 0x000AE6A4
		private int LoadInputBehaviorNow(int playerId, int behaviorId)
		{
			Player player = ReInput.players.GetPlayer(playerId);
			if (player == null)
			{
				return 0;
			}
			InputBehavior inputBehavior = ReInput.mapping.GetInputBehavior(playerId, behaviorId);
			if (inputBehavior == null)
			{
				return 0;
			}
			return this.LoadInputBehaviorNow(player, inputBehavior);
		}

		// Token: 0x0600203D RID: 8253 RVA: 0x000B02E4 File Offset: 0x000AE6E4
		private int LoadInputBehaviorNow(Player player, InputBehavior inputBehavior)
		{
			if (player == null || inputBehavior == null)
			{
				return 0;
			}
			string inputBehaviorXml = this.GetInputBehaviorXml(player, inputBehavior.id);
			if (inputBehaviorXml == null || inputBehaviorXml == string.Empty)
			{
				return 0;
			}
			return (!inputBehavior.ImportXmlString(inputBehaviorXml)) ? 0 : 1;
		}

		// Token: 0x0600203E RID: 8254 RVA: 0x000B0338 File Offset: 0x000AE738
		private bool LoadControllerAssignmentsNow()
		{
			try
			{
				UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo controllerAssignmentSaveInfo = this.LoadControllerAssignmentData();
				if (controllerAssignmentSaveInfo == null)
				{
					return false;
				}
				if (this.loadKeyboardAssignments || this.loadMouseAssignments)
				{
					this.LoadKeyboardAndMouseAssignmentsNow(controllerAssignmentSaveInfo);
				}
				if (this.loadJoystickAssignments)
				{
					this.LoadJoystickAssignmentsNow(controllerAssignmentSaveInfo);
				}
			}
			catch
			{
			}
			return true;
		}

		// Token: 0x0600203F RID: 8255 RVA: 0x000B03A8 File Offset: 0x000AE7A8
		private bool LoadKeyboardAndMouseAssignmentsNow(UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo data)
		{
			try
			{
				if (data == null && (data = this.LoadControllerAssignmentData()) == null)
				{
					return false;
				}
				foreach (Player player in ReInput.players.AllPlayers)
				{
					if (data.ContainsPlayer(player.id))
					{
						UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo.PlayerInfo playerInfo = data.players[data.IndexOfPlayer(player.id)];
						if (this.loadKeyboardAssignments)
						{
							player.controllers.hasKeyboard = playerInfo.hasKeyboard;
						}
						if (this.loadMouseAssignments)
						{
							player.controllers.hasMouse = playerInfo.hasMouse;
						}
					}
				}
			}
			catch
			{
			}
			return true;
		}

		// Token: 0x06002040 RID: 8256 RVA: 0x000B0498 File Offset: 0x000AE898
		private bool LoadJoystickAssignmentsNow(UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo data)
		{
			try
			{
				if (ReInput.controllers.joystickCount == 0)
				{
					return false;
				}
				if (data == null && (data = this.LoadControllerAssignmentData()) == null)
				{
					return false;
				}
				foreach (Player player in ReInput.players.AllPlayers)
				{
					player.controllers.ClearControllersOfType(ControllerType.Joystick);
				}
				List<UserDataStore_PlayerPrefs.JoystickAssignmentHistoryInfo> list = (!this.loadJoystickAssignments) ? null : new List<UserDataStore_PlayerPrefs.JoystickAssignmentHistoryInfo>();
				foreach (Player player2 in ReInput.players.AllPlayers)
				{
					if (data.ContainsPlayer(player2.id))
					{
						UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo.PlayerInfo playerInfo = data.players[data.IndexOfPlayer(player2.id)];
						for (int i = 0; i < playerInfo.joystickCount; i++)
						{
							UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo.JoystickInfo joystickInfo2 = playerInfo.joysticks[i];
							if (joystickInfo2 != null)
							{
								Joystick joystick = this.FindJoystickPrecise(joystickInfo2);
								if (joystick != null)
								{
									if (list.Find((UserDataStore_PlayerPrefs.JoystickAssignmentHistoryInfo x) => x.joystick == joystick) == null)
									{
										list.Add(new UserDataStore_PlayerPrefs.JoystickAssignmentHistoryInfo(joystick, joystickInfo2.id));
									}
									player2.controllers.AddController(joystick, false);
								}
							}
						}
					}
				}
				if (this.allowImpreciseJoystickAssignmentMatching)
				{
					foreach (Player player3 in ReInput.players.AllPlayers)
					{
						if (data.ContainsPlayer(player3.id))
						{
							UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo.PlayerInfo playerInfo2 = data.players[data.IndexOfPlayer(player3.id)];
							for (int j = 0; j < playerInfo2.joystickCount; j++)
							{
								UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo.JoystickInfo joystickInfo = playerInfo2.joysticks[j];
								if (joystickInfo != null)
								{
									Joystick joystick2 = null;
									int num = list.FindIndex((UserDataStore_PlayerPrefs.JoystickAssignmentHistoryInfo x) => x.oldJoystickId == joystickInfo.id);
									if (num >= 0)
									{
										joystick2 = list[num].joystick;
									}
									else
									{
										List<Joystick> list2;
										if (!this.TryFindJoysticksImprecise(joystickInfo, out list2))
										{
											goto IL_30F;
										}
										using (List<Joystick>.Enumerator enumerator4 = list2.GetEnumerator())
										{
											while (enumerator4.MoveNext())
											{
												Joystick match = enumerator4.Current;
												if (list.Find((UserDataStore_PlayerPrefs.JoystickAssignmentHistoryInfo x) => x.joystick == match) == null)
												{
													joystick2 = match;
													break;
												}
											}
										}
										if (joystick2 == null)
										{
											goto IL_30F;
										}
										list.Add(new UserDataStore_PlayerPrefs.JoystickAssignmentHistoryInfo(joystick2, joystickInfo.id));
									}
									player3.controllers.AddController(joystick2, false);
								}
								IL_30F:;
							}
						}
					}
				}
			}
			catch
			{
			}
			if (ReInput.configuration.autoAssignJoysticks)
			{
				ReInput.controllers.AutoAssignJoysticks();
			}
			return true;
		}

		// Token: 0x06002041 RID: 8257 RVA: 0x000B088C File Offset: 0x000AEC8C
		private UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo LoadControllerAssignmentData()
		{
			UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo result;
			try
			{
				if (!PlayerPrefs.HasKey(this.playerPrefsKey_controllerAssignments))
				{
					result = null;
				}
				else
				{
					string @string = PlayerPrefs.GetString(this.playerPrefsKey_controllerAssignments);
					if (string.IsNullOrEmpty(@string))
					{
						result = null;
					}
					else
					{
						UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo controllerAssignmentSaveInfo = JsonParser.FromJson<UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo>(@string);
						if (controllerAssignmentSaveInfo == null || controllerAssignmentSaveInfo.playerCount == 0)
						{
							result = null;
						}
						else
						{
							result = controllerAssignmentSaveInfo;
						}
					}
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06002042 RID: 8258 RVA: 0x000B0910 File Offset: 0x000AED10
		private IEnumerator LoadJoystickAssignmentsDeferred()
		{
			this.deferredJoystickAssignmentLoadPending = true;
			yield return new WaitForEndOfFrame();
			if (!ReInput.isReady)
			{
				yield break;
			}
			if (this.LoadJoystickAssignmentsNow(null))
			{
			}
			this.SaveControllerAssignments();
			this.deferredJoystickAssignmentLoadPending = false;
			yield break;
		}

		// Token: 0x06002043 RID: 8259 RVA: 0x000B092C File Offset: 0x000AED2C
		private void SaveAll()
		{
			IList<Player> allPlayers = ReInput.players.AllPlayers;
			for (int i = 0; i < allPlayers.Count; i++)
			{
				this.SavePlayerDataNow(allPlayers[i]);
			}
			this.SaveAllJoystickCalibrationData();
			if (this.loadControllerAssignments)
			{
				this.SaveControllerAssignments();
			}
			PlayerPrefs.Save();
		}

		// Token: 0x06002044 RID: 8260 RVA: 0x000B0985 File Offset: 0x000AED85
		private void SavePlayerDataNow(int playerId)
		{
			this.SavePlayerDataNow(ReInput.players.GetPlayer(playerId));
			PlayerPrefs.Save();
		}

		// Token: 0x06002045 RID: 8261 RVA: 0x000B09A0 File Offset: 0x000AEDA0
		private void SavePlayerDataNow(Player player)
		{
			if (player == null)
			{
				return;
			}
			PlayerSaveData saveData = player.GetSaveData(true);
			this.SaveInputBehaviors(player, saveData);
			this.SaveControllerMaps(player, saveData);
		}

		// Token: 0x06002046 RID: 8262 RVA: 0x000B09CC File Offset: 0x000AEDCC
		private void SaveAllJoystickCalibrationData()
		{
			IList<Joystick> joysticks = ReInput.controllers.Joysticks;
			for (int i = 0; i < joysticks.Count; i++)
			{
				this.SaveJoystickCalibrationData(joysticks[i]);
			}
		}

		// Token: 0x06002047 RID: 8263 RVA: 0x000B0A08 File Offset: 0x000AEE08
		private void SaveJoystickCalibrationData(int joystickId)
		{
			this.SaveJoystickCalibrationData(ReInput.controllers.GetJoystick(joystickId));
		}

		// Token: 0x06002048 RID: 8264 RVA: 0x000B0A1C File Offset: 0x000AEE1C
		private void SaveJoystickCalibrationData(Joystick joystick)
		{
			if (joystick == null)
			{
				return;
			}
			JoystickCalibrationMapSaveData calibrationMapSaveData = joystick.GetCalibrationMapSaveData();
			string joystickCalibrationMapPlayerPrefsKey = this.GetJoystickCalibrationMapPlayerPrefsKey(joystick);
			PlayerPrefs.SetString(joystickCalibrationMapPlayerPrefsKey, calibrationMapSaveData.map.ToXmlString());
		}

		// Token: 0x06002049 RID: 8265 RVA: 0x000B0A50 File Offset: 0x000AEE50
		private void SaveJoystickData(int joystickId)
		{
			IList<Player> allPlayers = ReInput.players.AllPlayers;
			for (int i = 0; i < allPlayers.Count; i++)
			{
				Player player = allPlayers[i];
				if (player.controllers.ContainsController(ControllerType.Joystick, joystickId))
				{
					this.SaveControllerMaps(player.id, ControllerType.Joystick, joystickId);
				}
			}
			this.SaveJoystickCalibrationData(joystickId);
		}

		// Token: 0x0600204A RID: 8266 RVA: 0x000B0AB3 File Offset: 0x000AEEB3
		private void SaveControllerDataNow(int playerId, ControllerType controllerType, int controllerId)
		{
			this.SaveControllerMaps(playerId, controllerType, controllerId);
			this.SaveControllerDataNow(controllerType, controllerId);
			PlayerPrefs.Save();
		}

		// Token: 0x0600204B RID: 8267 RVA: 0x000B0ACB File Offset: 0x000AEECB
		private void SaveControllerDataNow(ControllerType controllerType, int controllerId)
		{
			if (controllerType == ControllerType.Joystick)
			{
				this.SaveJoystickCalibrationData(controllerId);
			}
			PlayerPrefs.Save();
		}

		// Token: 0x0600204C RID: 8268 RVA: 0x000B0AE0 File Offset: 0x000AEEE0
		private void SaveControllerMaps(Player player, PlayerSaveData playerSaveData)
		{
			foreach (ControllerMapSaveData saveData in playerSaveData.AllControllerMapSaveData)
			{
				this.SaveControllerMap(player, saveData);
			}
		}

		// Token: 0x0600204D RID: 8269 RVA: 0x000B0B3C File Offset: 0x000AEF3C
		private void SaveControllerMaps(int playerId, ControllerType controllerType, int controllerId)
		{
			Player player = ReInput.players.GetPlayer(playerId);
			if (player == null)
			{
				return;
			}
			if (!player.controllers.ContainsController(controllerType, controllerId))
			{
				return;
			}
			ControllerMapSaveData[] mapSaveData = player.controllers.maps.GetMapSaveData(controllerType, controllerId, true);
			if (mapSaveData == null)
			{
				return;
			}
			for (int i = 0; i < mapSaveData.Length; i++)
			{
				this.SaveControllerMap(player, mapSaveData[i]);
			}
		}

		// Token: 0x0600204E RID: 8270 RVA: 0x000B0BA8 File Offset: 0x000AEFA8
		private void SaveControllerMap(Player player, ControllerMapSaveData saveData)
		{
			string key = this.GetControllerMapPlayerPrefsKey(player, saveData.controller, saveData.categoryId, saveData.layoutId, true);
			PlayerPrefs.SetString(key, saveData.map.ToXmlString());
			key = this.GetControllerMapKnownActionIdsPlayerPrefsKey(player, saveData.controller, saveData.categoryId, saveData.layoutId, true);
			PlayerPrefs.SetString(key, this.GetAllActionIdsString());
		}

		// Token: 0x0600204F RID: 8271 RVA: 0x000B0C08 File Offset: 0x000AF008
		private void SaveInputBehaviors(Player player, PlayerSaveData playerSaveData)
		{
			if (player == null)
			{
				return;
			}
			InputBehavior[] inputBehaviors = playerSaveData.inputBehaviors;
			for (int i = 0; i < inputBehaviors.Length; i++)
			{
				this.SaveInputBehaviorNow(player, inputBehaviors[i]);
			}
		}

		// Token: 0x06002050 RID: 8272 RVA: 0x000B0C44 File Offset: 0x000AF044
		private void SaveInputBehaviorNow(int playerId, int behaviorId)
		{
			Player player = ReInput.players.GetPlayer(playerId);
			if (player == null)
			{
				return;
			}
			InputBehavior inputBehavior = ReInput.mapping.GetInputBehavior(playerId, behaviorId);
			if (inputBehavior == null)
			{
				return;
			}
			this.SaveInputBehaviorNow(player, inputBehavior);
			PlayerPrefs.Save();
		}

		// Token: 0x06002051 RID: 8273 RVA: 0x000B0C88 File Offset: 0x000AF088
		private void SaveInputBehaviorNow(Player player, InputBehavior inputBehavior)
		{
			if (player == null || inputBehavior == null)
			{
				return;
			}
			string inputBehaviorPlayerPrefsKey = this.GetInputBehaviorPlayerPrefsKey(player, inputBehavior.id);
			PlayerPrefs.SetString(inputBehaviorPlayerPrefsKey, inputBehavior.ToXmlString());
		}

		// Token: 0x06002052 RID: 8274 RVA: 0x000B0CBC File Offset: 0x000AF0BC
		private bool SaveControllerAssignments()
		{
			try
			{
				UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo controllerAssignmentSaveInfo = new UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo(ReInput.players.allPlayerCount);
				for (int i = 0; i < ReInput.players.allPlayerCount; i++)
				{
					Player player = ReInput.players.AllPlayers[i];
					UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo.PlayerInfo playerInfo = new UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo.PlayerInfo();
					controllerAssignmentSaveInfo.players[i] = playerInfo;
					playerInfo.id = player.id;
					playerInfo.hasKeyboard = player.controllers.hasKeyboard;
					playerInfo.hasMouse = player.controllers.hasMouse;
					UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo.JoystickInfo[] array = new UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo.JoystickInfo[player.controllers.joystickCount];
					playerInfo.joysticks = array;
					for (int j = 0; j < player.controllers.joystickCount; j++)
					{
						Joystick joystick = player.controllers.Joysticks[j];
						array[j] = new UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo.JoystickInfo
						{
							instanceGuid = joystick.deviceInstanceGuid,
							id = joystick.id,
							hardwareIdentifier = joystick.hardwareIdentifier
						};
					}
				}
				PlayerPrefs.SetString(this.playerPrefsKey_controllerAssignments, JsonWriter.ToJson(controllerAssignmentSaveInfo));
				PlayerPrefs.Save();
			}
			catch
			{
			}
			return true;
		}

		// Token: 0x06002053 RID: 8275 RVA: 0x000B0E08 File Offset: 0x000AF208
		private bool ControllerAssignmentSaveDataExists()
		{
			if (!PlayerPrefs.HasKey(this.playerPrefsKey_controllerAssignments))
			{
				return false;
			}
			string @string = PlayerPrefs.GetString(this.playerPrefsKey_controllerAssignments);
			return !string.IsNullOrEmpty(@string);
		}

		// Token: 0x06002054 RID: 8276 RVA: 0x000B0E44 File Offset: 0x000AF244
		private string GetBasePlayerPrefsKey(Player player)
		{
			string str = this.playerPrefsKeyPrefix;
			return str + "|playerName=" + player.name;
		}

		// Token: 0x06002055 RID: 8277 RVA: 0x000B0E6C File Offset: 0x000AF26C
		private string GetControllerMapPlayerPrefsKey(Player player, Controller controller, int categoryId, int layoutId, bool includeDuplicateIndex)
		{
			string text = this.GetBasePlayerPrefsKey(player);
			text += "|dataType=ControllerMap";
			text = text + "|controllerMapType=" + controller.mapTypeString;
			string text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				"|categoryId=",
				categoryId,
				"|layoutId=",
				layoutId
			});
			text = text + "|hardwareIdentifier=" + controller.hardwareIdentifier;
			if (controller.type == ControllerType.Joystick)
			{
				text = text + "|hardwareGuid=" + ((Joystick)controller).hardwareTypeGuid.ToString();
				if (includeDuplicateIndex)
				{
					text = text + "|duplicate=" + UserDataStore_PlayerPrefs.GetDuplicateIndex(player, controller).ToString();
				}
			}
			return text;
		}

		// Token: 0x06002056 RID: 8278 RVA: 0x000B0F40 File Offset: 0x000AF340
		private string GetControllerMapKnownActionIdsPlayerPrefsKey(Player player, Controller controller, int categoryId, int layoutId, bool includeDuplicateIndex)
		{
			string text = this.GetBasePlayerPrefsKey(player);
			text += "|dataType=ControllerMap_KnownActionIds";
			text = text + "|controllerMapType=" + controller.mapTypeString;
			string text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				"|categoryId=",
				categoryId,
				"|layoutId=",
				layoutId
			});
			text = text + "|hardwareIdentifier=" + controller.hardwareIdentifier;
			if (controller.type == ControllerType.Joystick)
			{
				text = text + "|hardwareGuid=" + ((Joystick)controller).hardwareTypeGuid.ToString();
				if (includeDuplicateIndex)
				{
					text = text + "|duplicate=" + UserDataStore_PlayerPrefs.GetDuplicateIndex(player, controller).ToString();
				}
			}
			return text;
		}

		// Token: 0x06002057 RID: 8279 RVA: 0x000B1014 File Offset: 0x000AF414
		private string GetJoystickCalibrationMapPlayerPrefsKey(Joystick joystick)
		{
			string str = this.playerPrefsKeyPrefix;
			str += "|dataType=CalibrationMap";
			str = str + "|controllerType=" + joystick.type.ToString();
			str = str + "|hardwareIdentifier=" + joystick.hardwareIdentifier;
			return str + "|hardwareGuid=" + joystick.hardwareTypeGuid.ToString();
		}

		// Token: 0x06002058 RID: 8280 RVA: 0x000B1088 File Offset: 0x000AF488
		private string GetInputBehaviorPlayerPrefsKey(Player player, int inputBehaviorId)
		{
			string text = this.GetBasePlayerPrefsKey(player);
			text += "|dataType=InputBehavior";
			return text + "|id=" + inputBehaviorId;
		}

		// Token: 0x06002059 RID: 8281 RVA: 0x000B10BC File Offset: 0x000AF4BC
		private string GetControllerMapXml(Player player, Controller controller, int categoryId, int layoutId)
		{
			string controllerMapPlayerPrefsKey = this.GetControllerMapPlayerPrefsKey(player, controller, categoryId, layoutId, true);
			if (!PlayerPrefs.HasKey(controllerMapPlayerPrefsKey))
			{
				if (controller.type != ControllerType.Joystick)
				{
					return string.Empty;
				}
				controllerMapPlayerPrefsKey = this.GetControllerMapPlayerPrefsKey(player, controller, categoryId, layoutId, false);
				if (!PlayerPrefs.HasKey(controllerMapPlayerPrefsKey))
				{
					return string.Empty;
				}
			}
			return PlayerPrefs.GetString(controllerMapPlayerPrefsKey);
		}

		// Token: 0x0600205A RID: 8282 RVA: 0x000B1118 File Offset: 0x000AF518
		private List<int> GetControllerMapKnownActionIds(Player player, Controller controller, int categoryId, int layoutId)
		{
			List<int> list = new List<int>();
			string controllerMapKnownActionIdsPlayerPrefsKey = this.GetControllerMapKnownActionIdsPlayerPrefsKey(player, controller, categoryId, layoutId, true);
			if (!PlayerPrefs.HasKey(controllerMapKnownActionIdsPlayerPrefsKey))
			{
				if (controller.type != ControllerType.Joystick)
				{
					return list;
				}
				controllerMapKnownActionIdsPlayerPrefsKey = this.GetControllerMapKnownActionIdsPlayerPrefsKey(player, controller, categoryId, layoutId, false);
				if (!PlayerPrefs.HasKey(controllerMapKnownActionIdsPlayerPrefsKey))
				{
					return list;
				}
			}
			string @string = PlayerPrefs.GetString(controllerMapKnownActionIdsPlayerPrefsKey);
			if (string.IsNullOrEmpty(@string))
			{
				return list;
			}
			string[] array = @string.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				if (!string.IsNullOrEmpty(array[i]))
				{
					int item;
					if (int.TryParse(array[i], out item))
					{
						list.Add(item);
					}
				}
			}
			return list;
		}

		// Token: 0x0600205B RID: 8283 RVA: 0x000B11D8 File Offset: 0x000AF5D8
		private List<UserDataStore_PlayerPrefs.SavedControllerMapData> GetAllControllerMapsXml(Player player, bool userAssignableMapsOnly, Controller controller)
		{
			List<UserDataStore_PlayerPrefs.SavedControllerMapData> list = new List<UserDataStore_PlayerPrefs.SavedControllerMapData>();
			IList<InputMapCategory> mapCategories = ReInput.mapping.MapCategories;
			for (int i = 0; i < mapCategories.Count; i++)
			{
				InputMapCategory inputMapCategory = mapCategories[i];
				if (!userAssignableMapsOnly || inputMapCategory.userAssignable)
				{
					IList<InputLayout> list2 = ReInput.mapping.MapLayouts(controller.type);
					for (int j = 0; j < list2.Count; j++)
					{
						InputLayout inputLayout = list2[j];
						string controllerMapXml = this.GetControllerMapXml(player, controller, inputMapCategory.id, inputLayout.id);
						if (!(controllerMapXml == string.Empty))
						{
							List<int> controllerMapKnownActionIds = this.GetControllerMapKnownActionIds(player, controller, inputMapCategory.id, inputLayout.id);
							list.Add(new UserDataStore_PlayerPrefs.SavedControllerMapData(controllerMapXml, controllerMapKnownActionIds));
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0600205C RID: 8284 RVA: 0x000B12B8 File Offset: 0x000AF6B8
		private string GetJoystickCalibrationMapXml(Joystick joystick)
		{
			string joystickCalibrationMapPlayerPrefsKey = this.GetJoystickCalibrationMapPlayerPrefsKey(joystick);
			if (!PlayerPrefs.HasKey(joystickCalibrationMapPlayerPrefsKey))
			{
				return string.Empty;
			}
			return PlayerPrefs.GetString(joystickCalibrationMapPlayerPrefsKey);
		}

		// Token: 0x0600205D RID: 8285 RVA: 0x000B12E4 File Offset: 0x000AF6E4
		private string GetInputBehaviorXml(Player player, int id)
		{
			string inputBehaviorPlayerPrefsKey = this.GetInputBehaviorPlayerPrefsKey(player, id);
			if (!PlayerPrefs.HasKey(inputBehaviorPlayerPrefsKey))
			{
				return string.Empty;
			}
			return PlayerPrefs.GetString(inputBehaviorPlayerPrefsKey);
		}

		// Token: 0x0600205E RID: 8286 RVA: 0x000B1314 File Offset: 0x000AF714
		private void AddDefaultMappingsForNewActions(Player player, List<UserDataStore_PlayerPrefs.SavedControllerMapData> savedData, ControllerType controllerType, int controllerId)
		{
			if (player == null || savedData == null)
			{
				return;
			}
			List<int> allActionIds = this.GetAllActionIds();
			for (int i = 0; i < savedData.Count; i++)
			{
				UserDataStore_PlayerPrefs.SavedControllerMapData savedControllerMapData = savedData[i];
				if (savedControllerMapData != null)
				{
					if (savedControllerMapData.knownActionIds != null && savedControllerMapData.knownActionIds.Count != 0)
					{
						ControllerMap controllerMap = ControllerMap.CreateFromXml(controllerType, savedData[i].xml);
						if (controllerMap != null)
						{
							ControllerMap map = player.controllers.maps.GetMap(controllerType, controllerId, controllerMap.categoryId, controllerMap.layoutId);
							if (map != null)
							{
								ControllerMap controllerMapInstance = ReInput.mapping.GetControllerMapInstance(ReInput.controllers.GetController(controllerType, controllerId), controllerMap.categoryId, controllerMap.layoutId);
								if (controllerMapInstance != null)
								{
									List<int> list = new List<int>();
									foreach (int item in allActionIds)
									{
										if (!savedControllerMapData.knownActionIds.Contains(item))
										{
											list.Add(item);
										}
									}
									if (list.Count != 0)
									{
										foreach (ActionElementMap actionElementMap in controllerMapInstance.AllMaps)
										{
											if (list.Contains(actionElementMap.actionId))
											{
												if (!map.DoesElementAssignmentConflict(actionElementMap))
												{
													ElementAssignment elementAssignment = new ElementAssignment(controllerType, actionElementMap.elementType, actionElementMap.elementIdentifierId, actionElementMap.axisRange, actionElementMap.keyCode, actionElementMap.modifierKeyFlags, actionElementMap.actionId, actionElementMap.axisContribution, actionElementMap.invert);
													map.CreateElementMap(elementAssignment);
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600205F RID: 8287 RVA: 0x000B1538 File Offset: 0x000AF938
		private List<int> GetAllActionIds()
		{
			List<int> list = new List<int>();
			IList<InputAction> actions = ReInput.mapping.Actions;
			for (int i = 0; i < actions.Count; i++)
			{
				list.Add(actions[i].id);
			}
			return list;
		}

		// Token: 0x06002060 RID: 8288 RVA: 0x000B1580 File Offset: 0x000AF980
		private string GetAllActionIdsString()
		{
			string text = string.Empty;
			List<int> allActionIds = this.GetAllActionIds();
			for (int i = 0; i < allActionIds.Count; i++)
			{
				if (i > 0)
				{
					text += ",";
				}
				text += allActionIds[i];
			}
			return text;
		}

		// Token: 0x06002061 RID: 8289 RVA: 0x000B15D8 File Offset: 0x000AF9D8
		private Joystick FindJoystickPrecise(UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo.JoystickInfo joystickInfo)
		{
			if (joystickInfo == null)
			{
				return null;
			}
			if (joystickInfo.instanceGuid == Guid.Empty)
			{
				return null;
			}
			IList<Joystick> joysticks = ReInput.controllers.Joysticks;
			for (int i = 0; i < joysticks.Count; i++)
			{
				if (joysticks[i].deviceInstanceGuid == joystickInfo.instanceGuid)
				{
					return joysticks[i];
				}
			}
			return null;
		}

		// Token: 0x06002062 RID: 8290 RVA: 0x000B164C File Offset: 0x000AFA4C
		private bool TryFindJoysticksImprecise(UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo.JoystickInfo joystickInfo, out List<Joystick> matches)
		{
			matches = null;
			if (joystickInfo == null)
			{
				return false;
			}
			if (string.IsNullOrEmpty(joystickInfo.hardwareIdentifier))
			{
				return false;
			}
			IList<Joystick> joysticks = ReInput.controllers.Joysticks;
			for (int i = 0; i < joysticks.Count; i++)
			{
				if (string.Equals(joysticks[i].hardwareIdentifier, joystickInfo.hardwareIdentifier, StringComparison.OrdinalIgnoreCase))
				{
					if (matches == null)
					{
						matches = new List<Joystick>();
					}
					matches.Add(joysticks[i]);
				}
			}
			return matches != null;
		}

		// Token: 0x06002063 RID: 8291 RVA: 0x000B16DC File Offset: 0x000AFADC
		private static int GetDuplicateIndex(Player player, Controller controller)
		{
			int num = 0;
			foreach (Controller controller2 in player.controllers.Controllers)
			{
				if (controller2.type == controller.type)
				{
					bool flag = false;
					if (controller.type == ControllerType.Joystick)
					{
						if ((controller2 as Joystick).hardwareTypeGuid != (controller as Joystick).hardwareTypeGuid)
						{
							continue;
						}
						if ((controller as Joystick).hardwareTypeGuid != Guid.Empty)
						{
							flag = true;
						}
					}
					if (flag || !(controller2.hardwareIdentifier != controller.hardwareIdentifier))
					{
						if (controller2 == controller)
						{
							return num;
						}
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x04002023 RID: 8227
		private const string thisScriptName = "UserDataStore_PlayerPrefs";

		// Token: 0x04002024 RID: 8228
		private const string editorLoadedMessage = "\nIf unexpected input issues occur, the loaded XML data may be outdated or invalid. Clear PlayerPrefs using the inspector option on the UserDataStore_PlayerPrefs component.";

		// Token: 0x04002025 RID: 8229
		private const string playerPrefsKeySuffix_controllerAssignments = "ControllerAssignments";

		// Token: 0x04002026 RID: 8230
		[Tooltip("Should this script be used? If disabled, nothing will be saved or loaded.")]
		[SerializeField]
		private bool isEnabled = true;

		// Token: 0x04002027 RID: 8231
		[Tooltip("Should saved data be loaded on start?")]
		[SerializeField]
		private bool loadDataOnStart = true;

		// Token: 0x04002028 RID: 8232
		[Tooltip("Should Player Joystick assignments be saved and loaded? This is not totally reliable for all Joysticks on all platforms. Some platforms/input sources do not provide enough information to reliably save assignments from session to session and reboot to reboot.")]
		[SerializeField]
		private bool loadJoystickAssignments = true;

		// Token: 0x04002029 RID: 8233
		[Tooltip("Should Player Keyboard assignments be saved and loaded?")]
		[SerializeField]
		private bool loadKeyboardAssignments = true;

		// Token: 0x0400202A RID: 8234
		[Tooltip("Should Player Mouse assignments be saved and loaded?")]
		[SerializeField]
		private bool loadMouseAssignments = true;

		// Token: 0x0400202B RID: 8235
		[Tooltip("The PlayerPrefs key prefix. Change this to change how keys are stored in PlayerPrefs. Changing this will make saved data already stored with the old key no longer accessible.")]
		[SerializeField]
		private string playerPrefsKeyPrefix = "RewiredSaveData";

		// Token: 0x0400202C RID: 8236
		private bool allowImpreciseJoystickAssignmentMatching = true;

		// Token: 0x0400202D RID: 8237
		private bool deferredJoystickAssignmentLoadPending;

		// Token: 0x0400202E RID: 8238
		private bool wasJoystickEverDetected;

		// Token: 0x02000585 RID: 1413
		private class SavedControllerMapData
		{
			// Token: 0x06002064 RID: 8292 RVA: 0x000B17D4 File Offset: 0x000AFBD4
			public SavedControllerMapData(string xml, List<int> knownActionIds)
			{
				this.xml = xml;
				this.knownActionIds = knownActionIds;
			}

			// Token: 0x06002065 RID: 8293 RVA: 0x000B17EC File Offset: 0x000AFBEC
			public static List<string> GetXmlStringList(List<UserDataStore_PlayerPrefs.SavedControllerMapData> data)
			{
				List<string> list = new List<string>();
				if (data == null)
				{
					return list;
				}
				for (int i = 0; i < data.Count; i++)
				{
					if (data[i] != null)
					{
						if (!string.IsNullOrEmpty(data[i].xml))
						{
							list.Add(data[i].xml);
						}
					}
				}
				return list;
			}

			// Token: 0x0400202F RID: 8239
			public string xml;

			// Token: 0x04002030 RID: 8240
			public List<int> knownActionIds;
		}

		// Token: 0x02000586 RID: 1414
		private class ControllerAssignmentSaveInfo
		{
			// Token: 0x06002066 RID: 8294 RVA: 0x000B185D File Offset: 0x000AFC5D
			public ControllerAssignmentSaveInfo()
			{
			}

			// Token: 0x06002067 RID: 8295 RVA: 0x000B1868 File Offset: 0x000AFC68
			public ControllerAssignmentSaveInfo(int playerCount)
			{
				this.players = new UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo.PlayerInfo[playerCount];
				for (int i = 0; i < playerCount; i++)
				{
					this.players[i] = new UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo.PlayerInfo();
				}
			}

			// Token: 0x170004F2 RID: 1266
			// (get) Token: 0x06002068 RID: 8296 RVA: 0x000B18A6 File Offset: 0x000AFCA6
			public int playerCount
			{
				get
				{
					return (this.players == null) ? 0 : this.players.Length;
				}
			}

			// Token: 0x06002069 RID: 8297 RVA: 0x000B18C4 File Offset: 0x000AFCC4
			public int IndexOfPlayer(int playerId)
			{
				for (int i = 0; i < this.playerCount; i++)
				{
					if (this.players[i] != null)
					{
						if (this.players[i].id == playerId)
						{
							return i;
						}
					}
				}
				return -1;
			}

			// Token: 0x0600206A RID: 8298 RVA: 0x000B1910 File Offset: 0x000AFD10
			public bool ContainsPlayer(int playerId)
			{
				return this.IndexOfPlayer(playerId) >= 0;
			}

			// Token: 0x04002031 RID: 8241
			public UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo.PlayerInfo[] players;

			// Token: 0x02000587 RID: 1415
			public class PlayerInfo
			{
				// Token: 0x170004F3 RID: 1267
				// (get) Token: 0x0600206C RID: 8300 RVA: 0x000B1927 File Offset: 0x000AFD27
				public int joystickCount
				{
					get
					{
						return (this.joysticks == null) ? 0 : this.joysticks.Length;
					}
				}

				// Token: 0x0600206D RID: 8301 RVA: 0x000B1944 File Offset: 0x000AFD44
				public int IndexOfJoystick(int joystickId)
				{
					for (int i = 0; i < this.joystickCount; i++)
					{
						if (this.joysticks[i] != null)
						{
							if (this.joysticks[i].id == joystickId)
							{
								return i;
							}
						}
					}
					return -1;
				}

				// Token: 0x0600206E RID: 8302 RVA: 0x000B1990 File Offset: 0x000AFD90
				public bool ContainsJoystick(int joystickId)
				{
					return this.IndexOfJoystick(joystickId) >= 0;
				}

				// Token: 0x04002032 RID: 8242
				public int id;

				// Token: 0x04002033 RID: 8243
				public bool hasKeyboard;

				// Token: 0x04002034 RID: 8244
				public bool hasMouse;

				// Token: 0x04002035 RID: 8245
				public UserDataStore_PlayerPrefs.ControllerAssignmentSaveInfo.JoystickInfo[] joysticks;
			}

			// Token: 0x02000588 RID: 1416
			public class JoystickInfo
			{
				// Token: 0x04002036 RID: 8246
				public Guid instanceGuid;

				// Token: 0x04002037 RID: 8247
				public string hardwareIdentifier;

				// Token: 0x04002038 RID: 8248
				public int id;
			}
		}

		// Token: 0x02000589 RID: 1417
		private class JoystickAssignmentHistoryInfo
		{
			// Token: 0x06002070 RID: 8304 RVA: 0x000B19A7 File Offset: 0x000AFDA7
			public JoystickAssignmentHistoryInfo(Joystick joystick, int oldJoystickId)
			{
				if (joystick == null)
				{
					throw new ArgumentNullException("joystick");
				}
				this.joystick = joystick;
				this.oldJoystickId = oldJoystickId;
			}

			// Token: 0x04002039 RID: 8249
			public readonly Joystick joystick;

			// Token: 0x0400203A RID: 8250
			public readonly int oldJoystickId;
		}
	}
}
