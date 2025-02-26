using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rewired.Demos
{
	// Token: 0x020004FF RID: 1279
	[AddComponentMenu("")]
	public class ControlRemappingDemo1 : MonoBehaviour
	{
		// Token: 0x06001824 RID: 6180 RVA: 0x00095C94 File Offset: 0x00094094
		private void Awake()
		{
			this.inputMapper.options.timeout = 5f;
			this.inputMapper.options.ignoreMouseXAxis = true;
			this.inputMapper.options.ignoreMouseYAxis = true;
			this.Initialize();
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x00095CD3 File Offset: 0x000940D3
		private void OnEnable()
		{
			this.Subscribe();
		}

		// Token: 0x06001826 RID: 6182 RVA: 0x00095CDB File Offset: 0x000940DB
		private void OnDisable()
		{
			this.Unsubscribe();
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x00095CE4 File Offset: 0x000940E4
		private void Initialize()
		{
			this.dialog = new ControlRemappingDemo1.DialogHelper();
			this.actionQueue = new Queue<ControlRemappingDemo1.QueueEntry>();
			this.selectedController = new ControlRemappingDemo1.ControllerSelection();
			ReInput.ControllerConnectedEvent += this.JoystickConnected;
			ReInput.ControllerPreDisconnectEvent += this.JoystickPreDisconnect;
			ReInput.ControllerDisconnectedEvent += this.JoystickDisconnected;
			this.ResetAll();
			this.initialized = true;
			ReInput.userDataStore.Load();
			if (ReInput.unityJoystickIdentificationRequired)
			{
				this.IdentifyAllJoysticks();
			}
		}

		// Token: 0x06001828 RID: 6184 RVA: 0x00095D6C File Offset: 0x0009416C
		private void Setup()
		{
			if (this.setupFinished)
			{
				return;
			}
			this.style_wordWrap = new GUIStyle(GUI.skin.label);
			this.style_wordWrap.wordWrap = true;
			this.style_centeredBox = new GUIStyle(GUI.skin.box);
			this.style_centeredBox.alignment = TextAnchor.MiddleCenter;
			this.setupFinished = true;
		}

		// Token: 0x06001829 RID: 6185 RVA: 0x00095DCE File Offset: 0x000941CE
		private void Subscribe()
		{
			this.Unsubscribe();
			this.inputMapper.ConflictFoundEvent += this.OnConflictFound;
			this.inputMapper.StoppedEvent += this.OnStopped;
		}

		// Token: 0x0600182A RID: 6186 RVA: 0x00095E04 File Offset: 0x00094204
		private void Unsubscribe()
		{
			this.inputMapper.RemoveAllEventListeners();
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x00095E14 File Offset: 0x00094214
		public void OnGUI()
		{
			if (!this.initialized)
			{
				return;
			}
			this.Setup();
			this.HandleMenuControl();
			if (!this.showMenu)
			{
				this.DrawInitialScreen();
				return;
			}
			this.SetGUIStateStart();
			this.ProcessQueue();
			this.DrawPage();
			this.ShowDialog();
			this.SetGUIStateEnd();
			this.busy = false;
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x00095E70 File Offset: 0x00094270
		private void HandleMenuControl()
		{
			if (this.dialog.enabled)
			{
				return;
			}
			if (Event.current.type == EventType.Layout && ReInput.players.GetSystemPlayer().GetButtonDown("Menu"))
			{
				if (this.showMenu)
				{
					ReInput.userDataStore.Save();
					this.Close();
				}
				else
				{
					this.Open();
				}
			}
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x00095EDD File Offset: 0x000942DD
		private void Close()
		{
			this.ClearWorkingVars();
			this.showMenu = false;
		}

		// Token: 0x0600182E RID: 6190 RVA: 0x00095EEC File Offset: 0x000942EC
		private void Open()
		{
			this.showMenu = true;
		}

		// Token: 0x0600182F RID: 6191 RVA: 0x00095EF8 File Offset: 0x000942F8
		private void DrawInitialScreen()
		{
			ActionElementMap firstElementMapWithAction = ReInput.players.GetSystemPlayer().controllers.maps.GetFirstElementMapWithAction("Menu", true);
			GUIContent content;
			if (firstElementMapWithAction != null)
			{
				content = new GUIContent("Press " + firstElementMapWithAction.elementIdentifierName + " to open the menu.");
			}
			else
			{
				content = new GUIContent("There is no element assigned to open the menu!");
			}
			GUILayout.BeginArea(this.GetScreenCenteredRect(300f, 50f));
			GUILayout.Box(content, this.style_centeredBox, new GUILayoutOption[]
			{
				GUILayout.ExpandHeight(true),
				GUILayout.ExpandWidth(true)
			});
			GUILayout.EndArea();
		}

		// Token: 0x06001830 RID: 6192 RVA: 0x00095F94 File Offset: 0x00094394
		private void DrawPage()
		{
			if (GUI.enabled != this.pageGUIState)
			{
				GUI.enabled = this.pageGUIState;
			}
			Rect screenRect = new Rect(((float)Screen.width - (float)Screen.width * 0.9f) * 0.5f, ((float)Screen.height - (float)Screen.height * 0.9f) * 0.5f, (float)Screen.width * 0.9f, (float)Screen.height * 0.9f);
			GUILayout.BeginArea(screenRect);
			this.DrawPlayerSelector();
			this.DrawJoystickSelector();
			this.DrawMouseAssignment();
			this.DrawControllerSelector();
			this.DrawCalibrateButton();
			this.DrawMapCategories();
			this.actionScrollPos = GUILayout.BeginScrollView(this.actionScrollPos, Array.Empty<GUILayoutOption>());
			this.DrawCategoryActions();
			GUILayout.EndScrollView();
			GUILayout.EndArea();
		}

		// Token: 0x06001831 RID: 6193 RVA: 0x00096060 File Offset: 0x00094460
		private void DrawPlayerSelector()
		{
			if (ReInput.players.allPlayerCount == 0)
			{
				GUILayout.Label("There are no players.", Array.Empty<GUILayoutOption>());
				return;
			}
			GUILayout.Space(15f);
			GUILayout.Label("Players:", Array.Empty<GUILayoutOption>());
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			foreach (Player player in ReInput.players.GetPlayers(true))
			{
				if (this.selectedPlayer == null)
				{
					this.selectedPlayer = player;
				}
				bool flag = player == this.selectedPlayer;
				bool flag2 = GUILayout.Toggle(flag, (!(player.descriptiveName != string.Empty)) ? player.name : player.descriptiveName, "Button", new GUILayoutOption[]
				{
					GUILayout.ExpandWidth(false)
				});
				if (flag2 != flag && flag2)
				{
					this.selectedPlayer = player;
					this.selectedController.Clear();
					this.selectedMapCategoryId = -1;
				}
			}
			GUILayout.EndHorizontal();
		}

		// Token: 0x06001832 RID: 6194 RVA: 0x00096190 File Offset: 0x00094590
		private void DrawMouseAssignment()
		{
			bool enabled = GUI.enabled;
			if (this.selectedPlayer == null)
			{
				GUI.enabled = false;
			}
			GUILayout.Space(15f);
			GUILayout.Label("Assign Mouse:", Array.Empty<GUILayoutOption>());
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			bool flag = this.selectedPlayer != null && this.selectedPlayer.controllers.hasMouse;
			bool flag2 = GUILayout.Toggle(flag, "Assign Mouse", "Button", new GUILayoutOption[]
			{
				GUILayout.ExpandWidth(false)
			});
			if (flag2 != flag)
			{
				if (flag2)
				{
					this.selectedPlayer.controllers.hasMouse = true;
					foreach (Player player in ReInput.players.Players)
					{
						if (player != this.selectedPlayer)
						{
							player.controllers.hasMouse = false;
						}
					}
				}
				else
				{
					this.selectedPlayer.controllers.hasMouse = false;
				}
			}
			GUILayout.EndHorizontal();
			if (GUI.enabled != enabled)
			{
				GUI.enabled = enabled;
			}
		}

		// Token: 0x06001833 RID: 6195 RVA: 0x000962DC File Offset: 0x000946DC
		private void DrawJoystickSelector()
		{
			bool enabled = GUI.enabled;
			if (this.selectedPlayer == null)
			{
				GUI.enabled = false;
			}
			GUILayout.Space(15f);
			GUILayout.Label("Assign Joysticks:", Array.Empty<GUILayoutOption>());
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			bool flag = this.selectedPlayer == null || this.selectedPlayer.controllers.joystickCount == 0;
			bool flag2 = GUILayout.Toggle(flag, "None", "Button", new GUILayoutOption[]
			{
				GUILayout.ExpandWidth(false)
			});
			if (flag2 != flag)
			{
				this.selectedPlayer.controllers.ClearControllersOfType(ControllerType.Joystick);
				this.ControllerSelectionChanged();
			}
			if (this.selectedPlayer != null)
			{
				foreach (Joystick joystick in ReInput.controllers.Joysticks)
				{
					flag = this.selectedPlayer.controllers.ContainsController(joystick);
					flag2 = GUILayout.Toggle(flag, joystick.name, "Button", new GUILayoutOption[]
					{
						GUILayout.ExpandWidth(false)
					});
					if (flag2 != flag)
					{
						this.EnqueueAction(new ControlRemappingDemo1.JoystickAssignmentChange(this.selectedPlayer.id, joystick.id, flag2));
					}
				}
			}
			GUILayout.EndHorizontal();
			if (GUI.enabled != enabled)
			{
				GUI.enabled = enabled;
			}
		}

		// Token: 0x06001834 RID: 6196 RVA: 0x0009645C File Offset: 0x0009485C
		private void DrawControllerSelector()
		{
			if (this.selectedPlayer == null)
			{
				return;
			}
			bool enabled = GUI.enabled;
			GUILayout.Space(15f);
			GUILayout.Label("Controller to Map:", Array.Empty<GUILayoutOption>());
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			if (!this.selectedController.hasSelection)
			{
				this.selectedController.Set(0, ControllerType.Keyboard);
				this.ControllerSelectionChanged();
			}
			bool flag = this.selectedController.type == ControllerType.Keyboard;
			bool flag2 = GUILayout.Toggle(flag, "Keyboard", "Button", new GUILayoutOption[]
			{
				GUILayout.ExpandWidth(false)
			});
			if (flag2 != flag)
			{
				this.selectedController.Set(0, ControllerType.Keyboard);
				this.ControllerSelectionChanged();
			}
			if (!this.selectedPlayer.controllers.hasMouse)
			{
				GUI.enabled = false;
			}
			flag = (this.selectedController.type == ControllerType.Mouse);
			flag2 = GUILayout.Toggle(flag, "Mouse", "Button", new GUILayoutOption[]
			{
				GUILayout.ExpandWidth(false)
			});
			if (flag2 != flag)
			{
				this.selectedController.Set(0, ControllerType.Mouse);
				this.ControllerSelectionChanged();
			}
			if (GUI.enabled != enabled)
			{
				GUI.enabled = enabled;
			}
			foreach (Joystick joystick in this.selectedPlayer.controllers.Joysticks)
			{
				flag = (this.selectedController.type == ControllerType.Joystick && this.selectedController.id == joystick.id);
				flag2 = GUILayout.Toggle(flag, joystick.name, "Button", new GUILayoutOption[]
				{
					GUILayout.ExpandWidth(false)
				});
				if (flag2 != flag)
				{
					this.selectedController.Set(joystick.id, ControllerType.Joystick);
					this.ControllerSelectionChanged();
				}
			}
			GUILayout.EndHorizontal();
			if (GUI.enabled != enabled)
			{
				GUI.enabled = enabled;
			}
		}

		// Token: 0x06001835 RID: 6197 RVA: 0x00096660 File Offset: 0x00094A60
		private void DrawCalibrateButton()
		{
			if (this.selectedPlayer == null)
			{
				return;
			}
			bool enabled = GUI.enabled;
			GUILayout.Space(10f);
			Controller controller = (!this.selectedController.hasSelection) ? null : this.selectedPlayer.controllers.GetController(this.selectedController.type, this.selectedController.id);
			if (controller == null || this.selectedController.type != ControllerType.Joystick)
			{
				GUI.enabled = false;
				GUILayout.Button("Select a controller to calibrate", new GUILayoutOption[]
				{
					GUILayout.ExpandWidth(false)
				});
				if (GUI.enabled != enabled)
				{
					GUI.enabled = enabled;
				}
			}
			else if (GUILayout.Button("Calibrate " + controller.name, new GUILayoutOption[]
			{
				GUILayout.ExpandWidth(false)
			}))
			{
				Joystick joystick = controller as Joystick;
				if (joystick != null)
				{
					CalibrationMap calibrationMap = joystick.calibrationMap;
					if (calibrationMap != null)
					{
						this.EnqueueAction(new ControlRemappingDemo1.Calibration(this.selectedPlayer, joystick, calibrationMap));
					}
				}
			}
			if (GUI.enabled != enabled)
			{
				GUI.enabled = enabled;
			}
		}

		// Token: 0x06001836 RID: 6198 RVA: 0x0009677C File Offset: 0x00094B7C
		private void DrawMapCategories()
		{
			if (this.selectedPlayer == null)
			{
				return;
			}
			if (!this.selectedController.hasSelection)
			{
				return;
			}
			bool enabled = GUI.enabled;
			GUILayout.Space(15f);
			GUILayout.Label("Categories:", Array.Empty<GUILayoutOption>());
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			foreach (InputMapCategory inputMapCategory in ReInput.mapping.UserAssignableMapCategories)
			{
				if (!this.selectedPlayer.controllers.maps.ContainsMapInCategory(this.selectedController.type, inputMapCategory.id))
				{
					GUI.enabled = false;
				}
				else if (this.selectedMapCategoryId < 0)
				{
					this.selectedMapCategoryId = inputMapCategory.id;
					this.selectedMap = this.selectedPlayer.controllers.maps.GetFirstMapInCategory(this.selectedController.type, this.selectedController.id, inputMapCategory.id);
				}
				bool flag = inputMapCategory.id == this.selectedMapCategoryId;
				bool flag2 = GUILayout.Toggle(flag, (!(inputMapCategory.descriptiveName != string.Empty)) ? inputMapCategory.name : inputMapCategory.descriptiveName, "Button", new GUILayoutOption[]
				{
					GUILayout.ExpandWidth(false)
				});
				if (flag2 != flag)
				{
					this.selectedMapCategoryId = inputMapCategory.id;
					this.selectedMap = this.selectedPlayer.controllers.maps.GetFirstMapInCategory(this.selectedController.type, this.selectedController.id, inputMapCategory.id);
				}
				if (GUI.enabled != enabled)
				{
					GUI.enabled = enabled;
				}
			}
			GUILayout.EndHorizontal();
			if (GUI.enabled != enabled)
			{
				GUI.enabled = enabled;
			}
		}

		// Token: 0x06001837 RID: 6199 RVA: 0x0009697C File Offset: 0x00094D7C
		private void DrawCategoryActions()
		{
			if (this.selectedPlayer == null)
			{
				return;
			}
			if (this.selectedMapCategoryId < 0)
			{
				return;
			}
			bool enabled = GUI.enabled;
			if (this.selectedMap == null)
			{
				return;
			}
			GUILayout.Space(15f);
			GUILayout.Label("Actions:", Array.Empty<GUILayoutOption>());
			InputMapCategory mapCategory = ReInput.mapping.GetMapCategory(this.selectedMapCategoryId);
			if (mapCategory == null)
			{
				return;
			}
			InputCategory actionCategory = ReInput.mapping.GetActionCategory(mapCategory.name);
			if (actionCategory == null)
			{
				return;
			}
			float width = 150f;
			foreach (InputAction inputAction in ReInput.mapping.ActionsInCategory(actionCategory.id))
			{
				string text = (!(inputAction.descriptiveName != string.Empty)) ? inputAction.name : inputAction.descriptiveName;
				if (inputAction.type == InputActionType.Button)
				{
					GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
					GUILayout.Label(text, new GUILayoutOption[]
					{
						GUILayout.Width(width)
					});
					this.DrawAddActionMapButton(this.selectedPlayer.id, inputAction, AxisRange.Positive, this.selectedController, this.selectedMap);
					foreach (ActionElementMap actionElementMap in this.selectedMap.AllMaps)
					{
						if (actionElementMap.actionId == inputAction.id)
						{
							this.DrawActionAssignmentButton(this.selectedPlayer.id, inputAction, AxisRange.Positive, this.selectedController, this.selectedMap, actionElementMap);
						}
					}
					GUILayout.EndHorizontal();
				}
				else if (inputAction.type == InputActionType.Axis)
				{
					if (this.selectedController.type != ControllerType.Keyboard)
					{
						GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
						GUILayout.Label(text, new GUILayoutOption[]
						{
							GUILayout.Width(width)
						});
						this.DrawAddActionMapButton(this.selectedPlayer.id, inputAction, AxisRange.Full, this.selectedController, this.selectedMap);
						foreach (ActionElementMap actionElementMap2 in this.selectedMap.AllMaps)
						{
							if (actionElementMap2.actionId == inputAction.id)
							{
								if (actionElementMap2.elementType != ControllerElementType.Button)
								{
									if (actionElementMap2.axisType != AxisType.Split)
									{
										this.DrawActionAssignmentButton(this.selectedPlayer.id, inputAction, AxisRange.Full, this.selectedController, this.selectedMap, actionElementMap2);
										this.DrawInvertButton(this.selectedPlayer.id, inputAction, Pole.Positive, this.selectedController, this.selectedMap, actionElementMap2);
									}
								}
							}
						}
						GUILayout.EndHorizontal();
					}
					string text2 = (!(inputAction.positiveDescriptiveName != string.Empty)) ? (inputAction.descriptiveName + " +") : inputAction.positiveDescriptiveName;
					GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
					GUILayout.Label(text2, new GUILayoutOption[]
					{
						GUILayout.Width(width)
					});
					this.DrawAddActionMapButton(this.selectedPlayer.id, inputAction, AxisRange.Positive, this.selectedController, this.selectedMap);
					foreach (ActionElementMap actionElementMap3 in this.selectedMap.AllMaps)
					{
						if (actionElementMap3.actionId == inputAction.id)
						{
							if (actionElementMap3.axisContribution == Pole.Positive)
							{
								if (actionElementMap3.axisType != AxisType.Normal)
								{
									this.DrawActionAssignmentButton(this.selectedPlayer.id, inputAction, AxisRange.Positive, this.selectedController, this.selectedMap, actionElementMap3);
								}
							}
						}
					}
					GUILayout.EndHorizontal();
					string text3 = (!(inputAction.negativeDescriptiveName != string.Empty)) ? (inputAction.descriptiveName + " -") : inputAction.negativeDescriptiveName;
					GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
					GUILayout.Label(text3, new GUILayoutOption[]
					{
						GUILayout.Width(width)
					});
					this.DrawAddActionMapButton(this.selectedPlayer.id, inputAction, AxisRange.Negative, this.selectedController, this.selectedMap);
					foreach (ActionElementMap actionElementMap4 in this.selectedMap.AllMaps)
					{
						if (actionElementMap4.actionId == inputAction.id)
						{
							if (actionElementMap4.axisContribution == Pole.Negative)
							{
								if (actionElementMap4.axisType != AxisType.Normal)
								{
									this.DrawActionAssignmentButton(this.selectedPlayer.id, inputAction, AxisRange.Negative, this.selectedController, this.selectedMap, actionElementMap4);
								}
							}
						}
					}
					GUILayout.EndHorizontal();
				}
			}
			if (GUI.enabled != enabled)
			{
				GUI.enabled = enabled;
			}
		}

		// Token: 0x06001838 RID: 6200 RVA: 0x00096F24 File Offset: 0x00095324
		private void DrawActionAssignmentButton(int playerId, InputAction action, AxisRange actionRange, ControlRemappingDemo1.ControllerSelection controller, ControllerMap controllerMap, ActionElementMap elementMap)
		{
			if (GUILayout.Button(elementMap.elementIdentifierName, new GUILayoutOption[]
			{
				GUILayout.ExpandWidth(false),
				GUILayout.MinWidth(30f)
			}))
			{
				InputMapper.Context context = new InputMapper.Context
				{
					actionId = action.id,
					actionRange = actionRange,
					controllerMap = controllerMap,
					actionElementMapToReplace = elementMap
				};
				this.EnqueueAction(new ControlRemappingDemo1.ElementAssignmentChange(ControlRemappingDemo1.ElementAssignmentChangeType.ReassignOrRemove, context));
				this.startListening = true;
			}
			GUILayout.Space(4f);
		}

		// Token: 0x06001839 RID: 6201 RVA: 0x00096FA8 File Offset: 0x000953A8
		private void DrawInvertButton(int playerId, InputAction action, Pole actionAxisContribution, ControlRemappingDemo1.ControllerSelection controller, ControllerMap controllerMap, ActionElementMap elementMap)
		{
			bool invert = elementMap.invert;
			bool flag = GUILayout.Toggle(invert, "Invert", new GUILayoutOption[]
			{
				GUILayout.ExpandWidth(false)
			});
			if (flag != invert)
			{
				elementMap.invert = flag;
			}
			GUILayout.Space(10f);
		}

		// Token: 0x0600183A RID: 6202 RVA: 0x00096FF4 File Offset: 0x000953F4
		private void DrawAddActionMapButton(int playerId, InputAction action, AxisRange actionRange, ControlRemappingDemo1.ControllerSelection controller, ControllerMap controllerMap)
		{
			if (GUILayout.Button("Add...", new GUILayoutOption[]
			{
				GUILayout.ExpandWidth(false)
			}))
			{
				InputMapper.Context context = new InputMapper.Context
				{
					actionId = action.id,
					actionRange = actionRange,
					controllerMap = controllerMap
				};
				this.EnqueueAction(new ControlRemappingDemo1.ElementAssignmentChange(ControlRemappingDemo1.ElementAssignmentChangeType.Add, context));
				this.startListening = true;
			}
			GUILayout.Space(10f);
		}

		// Token: 0x0600183B RID: 6203 RVA: 0x00097060 File Offset: 0x00095460
		private void ShowDialog()
		{
			this.dialog.Update();
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x00097070 File Offset: 0x00095470
		private void DrawModalWindow(string title, string message)
		{
			if (!this.dialog.enabled)
			{
				return;
			}
			GUILayout.Space(5f);
			GUILayout.Label(message, this.style_wordWrap, Array.Empty<GUILayoutOption>());
			GUILayout.FlexibleSpace();
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			this.dialog.DrawConfirmButton("Okay");
			GUILayout.FlexibleSpace();
			this.dialog.DrawCancelButton();
			GUILayout.EndHorizontal();
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x000970E0 File Offset: 0x000954E0
		private void DrawModalWindow_OkayOnly(string title, string message)
		{
			if (!this.dialog.enabled)
			{
				return;
			}
			GUILayout.Space(5f);
			GUILayout.Label(message, this.style_wordWrap, Array.Empty<GUILayoutOption>());
			GUILayout.FlexibleSpace();
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			this.dialog.DrawConfirmButton("Okay");
			GUILayout.EndHorizontal();
		}

		// Token: 0x0600183E RID: 6206 RVA: 0x00097140 File Offset: 0x00095540
		private void DrawElementAssignmentWindow(string title, string message)
		{
			if (!this.dialog.enabled)
			{
				return;
			}
			GUILayout.Space(5f);
			GUILayout.Label(message, this.style_wordWrap, Array.Empty<GUILayoutOption>());
			GUILayout.FlexibleSpace();
			ControlRemappingDemo1.ElementAssignmentChange elementAssignmentChange = this.actionQueue.Peek() as ControlRemappingDemo1.ElementAssignmentChange;
			if (elementAssignmentChange == null)
			{
				this.dialog.Cancel();
				return;
			}
			float num;
			if (!this.dialog.busy)
			{
				if (this.startListening && this.inputMapper.status == InputMapper.Status.Idle)
				{
					this.inputMapper.Start(elementAssignmentChange.context);
					this.startListening = false;
				}
				if (this.conflictFoundEventData != null)
				{
					this.dialog.Confirm();
					return;
				}
				num = this.inputMapper.timeRemaining;
				if (num == 0f)
				{
					this.dialog.Cancel();
					return;
				}
			}
			else
			{
				num = this.inputMapper.options.timeout;
			}
			GUILayout.Label("Assignment will be canceled in " + ((int)Mathf.Ceil(num)).ToString() + "...", this.style_wordWrap, Array.Empty<GUILayoutOption>());
		}

		// Token: 0x0600183F RID: 6207 RVA: 0x0009726C File Offset: 0x0009566C
		private void DrawElementAssignmentProtectedConflictWindow(string title, string message)
		{
			if (!this.dialog.enabled)
			{
				return;
			}
			GUILayout.Space(5f);
			GUILayout.Label(message, this.style_wordWrap, Array.Empty<GUILayoutOption>());
			GUILayout.FlexibleSpace();
			if (!(this.actionQueue.Peek() is ControlRemappingDemo1.ElementAssignmentChange))
			{
				this.dialog.Cancel();
				return;
			}
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			this.dialog.DrawConfirmButton(ControlRemappingDemo1.UserResponse.Custom1, "Add");
			GUILayout.FlexibleSpace();
			this.dialog.DrawCancelButton();
			GUILayout.EndHorizontal();
		}

		// Token: 0x06001840 RID: 6208 RVA: 0x00097300 File Offset: 0x00095700
		private void DrawElementAssignmentNormalConflictWindow(string title, string message)
		{
			if (!this.dialog.enabled)
			{
				return;
			}
			GUILayout.Space(5f);
			GUILayout.Label(message, this.style_wordWrap, Array.Empty<GUILayoutOption>());
			GUILayout.FlexibleSpace();
			if (!(this.actionQueue.Peek() is ControlRemappingDemo1.ElementAssignmentChange))
			{
				this.dialog.Cancel();
				return;
			}
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			this.dialog.DrawConfirmButton(ControlRemappingDemo1.UserResponse.Confirm, "Replace");
			GUILayout.FlexibleSpace();
			this.dialog.DrawConfirmButton(ControlRemappingDemo1.UserResponse.Custom1, "Add");
			GUILayout.FlexibleSpace();
			this.dialog.DrawCancelButton();
			GUILayout.EndHorizontal();
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x000973A8 File Offset: 0x000957A8
		private void DrawReassignOrRemoveElementAssignmentWindow(string title, string message)
		{
			if (!this.dialog.enabled)
			{
				return;
			}
			GUILayout.Space(5f);
			GUILayout.Label(message, this.style_wordWrap, Array.Empty<GUILayoutOption>());
			GUILayout.FlexibleSpace();
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			this.dialog.DrawConfirmButton("Reassign");
			GUILayout.FlexibleSpace();
			this.dialog.DrawCancelButton("Remove");
			GUILayout.EndHorizontal();
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x0009741C File Offset: 0x0009581C
		private void DrawFallbackJoystickIdentificationWindow(string title, string message)
		{
			if (!this.dialog.enabled)
			{
				return;
			}
			ControlRemappingDemo1.FallbackJoystickIdentification fallbackJoystickIdentification = this.actionQueue.Peek() as ControlRemappingDemo1.FallbackJoystickIdentification;
			if (fallbackJoystickIdentification == null)
			{
				this.dialog.Cancel();
				return;
			}
			GUILayout.Space(5f);
			GUILayout.Label(message, this.style_wordWrap, Array.Empty<GUILayoutOption>());
			GUILayout.Label("Press any button or axis on \"" + fallbackJoystickIdentification.joystickName + "\" now.", this.style_wordWrap, Array.Empty<GUILayoutOption>());
			GUILayout.FlexibleSpace();
			if (GUILayout.Button("Skip", Array.Empty<GUILayoutOption>()))
			{
				this.dialog.Cancel();
				return;
			}
			if (this.dialog.busy)
			{
				return;
			}
			if (!ReInput.controllers.SetUnityJoystickIdFromAnyButtonOrAxisPress(fallbackJoystickIdentification.joystickId, 0.8f, false))
			{
				return;
			}
			this.dialog.Confirm();
		}

		// Token: 0x06001843 RID: 6211 RVA: 0x000974FC File Offset: 0x000958FC
		private void DrawCalibrationWindow(string title, string message)
		{
			if (!this.dialog.enabled)
			{
				return;
			}
			ControlRemappingDemo1.Calibration calibration = this.actionQueue.Peek() as ControlRemappingDemo1.Calibration;
			if (calibration == null)
			{
				this.dialog.Cancel();
				return;
			}
			GUILayout.Space(5f);
			GUILayout.Label(message, this.style_wordWrap, Array.Empty<GUILayoutOption>());
			GUILayout.Space(20f);
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			bool enabled = GUI.enabled;
			GUILayout.BeginVertical(new GUILayoutOption[]
			{
				GUILayout.Width(200f)
			});
			this.calibrateScrollPos = GUILayout.BeginScrollView(this.calibrateScrollPos, Array.Empty<GUILayoutOption>());
			if (calibration.recording)
			{
				GUI.enabled = false;
			}
			IList<ControllerElementIdentifier> axisElementIdentifiers = calibration.joystick.AxisElementIdentifiers;
			for (int i = 0; i < axisElementIdentifiers.Count; i++)
			{
				ControllerElementIdentifier controllerElementIdentifier = axisElementIdentifiers[i];
				bool flag = calibration.selectedElementIdentifierId == controllerElementIdentifier.id;
				bool flag2 = GUILayout.Toggle(flag, controllerElementIdentifier.name, "Button", new GUILayoutOption[]
				{
					GUILayout.ExpandWidth(false)
				});
				if (flag != flag2)
				{
					calibration.selectedElementIdentifierId = controllerElementIdentifier.id;
				}
			}
			if (GUI.enabled != enabled)
			{
				GUI.enabled = enabled;
			}
			GUILayout.EndScrollView();
			GUILayout.EndVertical();
			GUILayout.BeginVertical(new GUILayoutOption[]
			{
				GUILayout.Width(200f)
			});
			if (calibration.selectedElementIdentifierId >= 0)
			{
				float axisRawById = calibration.joystick.GetAxisRawById(calibration.selectedElementIdentifierId);
				GUILayout.Label("Raw Value: " + axisRawById.ToString(), Array.Empty<GUILayoutOption>());
				int axisIndexById = calibration.joystick.GetAxisIndexById(calibration.selectedElementIdentifierId);
				AxisCalibration axis = calibration.calibrationMap.GetAxis(axisIndexById);
				GUILayout.Label("Calibrated Value: " + calibration.joystick.GetAxisById(calibration.selectedElementIdentifierId), Array.Empty<GUILayoutOption>());
				GUILayout.Label("Zero: " + axis.calibratedZero, Array.Empty<GUILayoutOption>());
				GUILayout.Label("Min: " + axis.calibratedMin, Array.Empty<GUILayoutOption>());
				GUILayout.Label("Max: " + axis.calibratedMax, Array.Empty<GUILayoutOption>());
				GUILayout.Label("Dead Zone: " + axis.deadZone, Array.Empty<GUILayoutOption>());
				GUILayout.Space(15f);
				bool flag3 = GUILayout.Toggle(axis.enabled, "Enabled", "Button", new GUILayoutOption[]
				{
					GUILayout.ExpandWidth(false)
				});
				if (axis.enabled != flag3)
				{
					axis.enabled = flag3;
				}
				GUILayout.Space(10f);
				bool flag4 = GUILayout.Toggle(calibration.recording, "Record Min/Max", "Button", new GUILayoutOption[]
				{
					GUILayout.ExpandWidth(false)
				});
				if (flag4 != calibration.recording)
				{
					if (flag4)
					{
						axis.calibratedMax = 0f;
						axis.calibratedMin = 0f;
					}
					calibration.recording = flag4;
				}
				if (calibration.recording)
				{
					axis.calibratedMin = Mathf.Min(new float[]
					{
						axis.calibratedMin,
						axisRawById,
						axis.calibratedMin
					});
					axis.calibratedMax = Mathf.Max(new float[]
					{
						axis.calibratedMax,
						axisRawById,
						axis.calibratedMax
					});
					GUI.enabled = false;
				}
				if (GUILayout.Button("Set Zero", new GUILayoutOption[]
				{
					GUILayout.ExpandWidth(false)
				}))
				{
					axis.calibratedZero = axisRawById;
				}
				if (GUILayout.Button("Set Dead Zone", new GUILayoutOption[]
				{
					GUILayout.ExpandWidth(false)
				}))
				{
					axis.deadZone = axisRawById;
				}
				bool flag5 = GUILayout.Toggle(axis.invert, "Invert", "Button", new GUILayoutOption[]
				{
					GUILayout.ExpandWidth(false)
				});
				if (axis.invert != flag5)
				{
					axis.invert = flag5;
				}
				GUILayout.Space(10f);
				if (GUILayout.Button("Reset", new GUILayoutOption[]
				{
					GUILayout.ExpandWidth(false)
				}))
				{
					axis.Reset();
				}
				if (GUI.enabled != enabled)
				{
					GUI.enabled = enabled;
				}
			}
			else
			{
				GUILayout.Label("Select an axis to begin.", Array.Empty<GUILayoutOption>());
			}
			GUILayout.EndVertical();
			GUILayout.EndHorizontal();
			GUILayout.FlexibleSpace();
			if (calibration.recording)
			{
				GUI.enabled = false;
			}
			if (GUILayout.Button("Close", Array.Empty<GUILayoutOption>()))
			{
				this.calibrateScrollPos = default(Vector2);
				this.dialog.Confirm();
			}
			if (GUI.enabled != enabled)
			{
				GUI.enabled = enabled;
			}
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x000979D0 File Offset: 0x00095DD0
		private void DialogResultCallback(int queueActionId, ControlRemappingDemo1.UserResponse response)
		{
			foreach (ControlRemappingDemo1.QueueEntry queueEntry in this.actionQueue)
			{
				if (queueEntry.id == queueActionId)
				{
					if (response != ControlRemappingDemo1.UserResponse.Cancel)
					{
						queueEntry.Confirm(response);
					}
					else
					{
						queueEntry.Cancel();
					}
					break;
				}
			}
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x00097A54 File Offset: 0x00095E54
		private Rect GetScreenCenteredRect(float width, float height)
		{
			return new Rect((float)Screen.width * 0.5f - width * 0.5f, (float)((double)Screen.height * 0.5 - (double)(height * 0.5f)), width, height);
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x00097A8C File Offset: 0x00095E8C
		private void EnqueueAction(ControlRemappingDemo1.QueueEntry entry)
		{
			if (entry == null)
			{
				return;
			}
			this.busy = true;
			GUI.enabled = false;
			this.actionQueue.Enqueue(entry);
		}

		// Token: 0x06001847 RID: 6215 RVA: 0x00097AB0 File Offset: 0x00095EB0
		private void ProcessQueue()
		{
			if (this.dialog.enabled)
			{
				return;
			}
			if (this.busy || this.actionQueue.Count == 0)
			{
				return;
			}
			while (this.actionQueue.Count > 0)
			{
				ControlRemappingDemo1.QueueEntry queueEntry = this.actionQueue.Peek();
				bool flag = false;
				switch (queueEntry.queueActionType)
				{
				case ControlRemappingDemo1.QueueActionType.JoystickAssignment:
					flag = this.ProcessJoystickAssignmentChange((ControlRemappingDemo1.JoystickAssignmentChange)queueEntry);
					break;
				case ControlRemappingDemo1.QueueActionType.ElementAssignment:
					flag = this.ProcessElementAssignmentChange((ControlRemappingDemo1.ElementAssignmentChange)queueEntry);
					break;
				case ControlRemappingDemo1.QueueActionType.FallbackJoystickIdentification:
					flag = this.ProcessFallbackJoystickIdentification((ControlRemappingDemo1.FallbackJoystickIdentification)queueEntry);
					break;
				case ControlRemappingDemo1.QueueActionType.Calibrate:
					flag = this.ProcessCalibration((ControlRemappingDemo1.Calibration)queueEntry);
					break;
				}
				if (!flag)
				{
					break;
				}
				this.actionQueue.Dequeue();
			}
		}

		// Token: 0x06001848 RID: 6216 RVA: 0x00097B94 File Offset: 0x00095F94
		private bool ProcessJoystickAssignmentChange(ControlRemappingDemo1.JoystickAssignmentChange entry)
		{
			if (entry.state == ControlRemappingDemo1.QueueEntry.State.Canceled)
			{
				return true;
			}
			Player player = ReInput.players.GetPlayer(entry.playerId);
			if (player == null)
			{
				return true;
			}
			if (!entry.assign)
			{
				player.controllers.RemoveController(ControllerType.Joystick, entry.joystickId);
				this.ControllerSelectionChanged();
				return true;
			}
			if (player.controllers.ContainsController(ControllerType.Joystick, entry.joystickId))
			{
				return true;
			}
			bool flag = ReInput.controllers.IsJoystickAssigned(entry.joystickId);
			if (!flag || entry.state == ControlRemappingDemo1.QueueEntry.State.Confirmed)
			{
				player.controllers.AddController(ControllerType.Joystick, entry.joystickId, true);
				this.ControllerSelectionChanged();
				return true;
			}
			this.dialog.StartModal(entry.id, ControlRemappingDemo1.DialogHelper.DialogType.JoystickConflict, new ControlRemappingDemo1.WindowProperties
			{
				title = "Joystick Reassignment",
				message = "This joystick is already assigned to another player. Do you want to reassign this joystick to " + player.descriptiveName + "?",
				rect = this.GetScreenCenteredRect(250f, 200f),
				windowDrawDelegate = new Action<string, string>(this.DrawModalWindow)
			}, new Action<int, ControlRemappingDemo1.UserResponse>(this.DialogResultCallback));
			return false;
		}

		// Token: 0x06001849 RID: 6217 RVA: 0x00097CC0 File Offset: 0x000960C0
		private bool ProcessElementAssignmentChange(ControlRemappingDemo1.ElementAssignmentChange entry)
		{
			switch (entry.changeType)
			{
			case ControlRemappingDemo1.ElementAssignmentChangeType.Add:
			case ControlRemappingDemo1.ElementAssignmentChangeType.Replace:
				return this.ProcessAddOrReplaceElementAssignment(entry);
			case ControlRemappingDemo1.ElementAssignmentChangeType.Remove:
				return this.ProcessRemoveElementAssignment(entry);
			case ControlRemappingDemo1.ElementAssignmentChangeType.ReassignOrRemove:
				return this.ProcessRemoveOrReassignElementAssignment(entry);
			case ControlRemappingDemo1.ElementAssignmentChangeType.ConflictCheck:
				return this.ProcessElementAssignmentConflictCheck(entry);
			default:
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x00097D18 File Offset: 0x00096118
		private bool ProcessRemoveOrReassignElementAssignment(ControlRemappingDemo1.ElementAssignmentChange entry)
		{
			if (entry.context.controllerMap == null)
			{
				return true;
			}
			if (entry.state == ControlRemappingDemo1.QueueEntry.State.Canceled)
			{
				ControlRemappingDemo1.ElementAssignmentChange elementAssignmentChange = new ControlRemappingDemo1.ElementAssignmentChange(entry);
				elementAssignmentChange.changeType = ControlRemappingDemo1.ElementAssignmentChangeType.Remove;
				this.actionQueue.Enqueue(elementAssignmentChange);
				return true;
			}
			if (entry.state == ControlRemappingDemo1.QueueEntry.State.Confirmed)
			{
				ControlRemappingDemo1.ElementAssignmentChange elementAssignmentChange2 = new ControlRemappingDemo1.ElementAssignmentChange(entry);
				elementAssignmentChange2.changeType = ControlRemappingDemo1.ElementAssignmentChangeType.Replace;
				this.actionQueue.Enqueue(elementAssignmentChange2);
				return true;
			}
			this.dialog.StartModal(entry.id, ControlRemappingDemo1.DialogHelper.DialogType.AssignElement, new ControlRemappingDemo1.WindowProperties
			{
				title = "Reassign or Remove",
				message = "Do you want to reassign or remove this assignment?",
				rect = this.GetScreenCenteredRect(250f, 200f),
				windowDrawDelegate = new Action<string, string>(this.DrawReassignOrRemoveElementAssignmentWindow)
			}, new Action<int, ControlRemappingDemo1.UserResponse>(this.DialogResultCallback));
			return false;
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x00097DF4 File Offset: 0x000961F4
		private bool ProcessRemoveElementAssignment(ControlRemappingDemo1.ElementAssignmentChange entry)
		{
			if (entry.context.controllerMap == null)
			{
				return true;
			}
			if (entry.state == ControlRemappingDemo1.QueueEntry.State.Canceled)
			{
				return true;
			}
			if (entry.state == ControlRemappingDemo1.QueueEntry.State.Confirmed)
			{
				entry.context.controllerMap.DeleteElementMap(entry.context.actionElementMapToReplace.id);
				return true;
			}
			this.dialog.StartModal(entry.id, ControlRemappingDemo1.DialogHelper.DialogType.DeleteAssignmentConfirmation, new ControlRemappingDemo1.WindowProperties
			{
				title = "Remove Assignment",
				message = "Are you sure you want to remove this assignment?",
				rect = this.GetScreenCenteredRect(250f, 200f),
				windowDrawDelegate = new Action<string, string>(this.DrawModalWindow)
			}, new Action<int, ControlRemappingDemo1.UserResponse>(this.DialogResultCallback));
			return false;
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x00097EBC File Offset: 0x000962BC
		private bool ProcessAddOrReplaceElementAssignment(ControlRemappingDemo1.ElementAssignmentChange entry)
		{
			if (entry.state == ControlRemappingDemo1.QueueEntry.State.Canceled)
			{
				this.inputMapper.Stop();
				return true;
			}
			if (entry.state != ControlRemappingDemo1.QueueEntry.State.Confirmed)
			{
				string text;
				if (entry.context.controllerMap.controllerType == ControllerType.Keyboard)
				{
					bool flag = Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer;
					if (flag)
					{
						text = "Press any key to assign it to this action. You may also use the modifier keys Command, Control, Alt, and Shift. If you wish to assign a modifier key itself to this action, press and hold the key for 1 second.";
					}
					else
					{
						text = "Press any key to assign it to this action. You may also use the modifier keys Control, Alt, and Shift. If you wish to assign a modifier key itself to this action, press and hold the key for 1 second.";
					}
					if (Application.isEditor)
					{
						text += "\n\nNOTE: Some modifier key combinations will not work in the Unity Editor, but they will work in a game build.";
					}
				}
				else if (entry.context.controllerMap.controllerType == ControllerType.Mouse)
				{
					text = "Press any mouse button or axis to assign it to this action.\n\nTo assign mouse movement axes, move the mouse quickly in the direction you want mapped to the action. Slow movements will be ignored.";
				}
				else
				{
					text = "Press any button or axis to assign it to this action.";
				}
				this.dialog.StartModal(entry.id, ControlRemappingDemo1.DialogHelper.DialogType.AssignElement, new ControlRemappingDemo1.WindowProperties
				{
					title = "Assign",
					message = text,
					rect = this.GetScreenCenteredRect(250f, 200f),
					windowDrawDelegate = new Action<string, string>(this.DrawElementAssignmentWindow)
				}, new Action<int, ControlRemappingDemo1.UserResponse>(this.DialogResultCallback));
				return false;
			}
			if (Event.current.type != EventType.Layout)
			{
				return false;
			}
			if (this.conflictFoundEventData != null)
			{
				ControlRemappingDemo1.ElementAssignmentChange elementAssignmentChange = new ControlRemappingDemo1.ElementAssignmentChange(entry);
				elementAssignmentChange.changeType = ControlRemappingDemo1.ElementAssignmentChangeType.ConflictCheck;
				this.actionQueue.Enqueue(elementAssignmentChange);
			}
			return true;
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x00098014 File Offset: 0x00096414
		private bool ProcessElementAssignmentConflictCheck(ControlRemappingDemo1.ElementAssignmentChange entry)
		{
			if (entry.context.controllerMap == null)
			{
				return true;
			}
			if (entry.state == ControlRemappingDemo1.QueueEntry.State.Canceled)
			{
				this.inputMapper.Stop();
				return true;
			}
			if (this.conflictFoundEventData == null)
			{
				return true;
			}
			if (entry.state == ControlRemappingDemo1.QueueEntry.State.Confirmed)
			{
				if (entry.response == ControlRemappingDemo1.UserResponse.Confirm)
				{
					this.conflictFoundEventData.responseCallback(InputMapper.ConflictResponse.Replace);
				}
				else
				{
					if (entry.response != ControlRemappingDemo1.UserResponse.Custom1)
					{
						throw new NotImplementedException();
					}
					this.conflictFoundEventData.responseCallback(InputMapper.ConflictResponse.Add);
				}
				return true;
			}
			if (this.conflictFoundEventData.isProtected)
			{
				string message = this.conflictFoundEventData.assignment.elementDisplayName + " is already in use and is protected from reassignment. You cannot remove the protected assignment, but you can still assign the action to this element. If you do so, the element will trigger multiple actions when activated.";
				this.dialog.StartModal(entry.id, ControlRemappingDemo1.DialogHelper.DialogType.AssignElement, new ControlRemappingDemo1.WindowProperties
				{
					title = "Assignment Conflict",
					message = message,
					rect = this.GetScreenCenteredRect(250f, 200f),
					windowDrawDelegate = new Action<string, string>(this.DrawElementAssignmentProtectedConflictWindow)
				}, new Action<int, ControlRemappingDemo1.UserResponse>(this.DialogResultCallback));
			}
			else
			{
				string message2 = this.conflictFoundEventData.assignment.elementDisplayName + " is already in use. You may replace the other conflicting assignments, add this assignment anyway which will leave multiple actions assigned to this element, or cancel this assignment.";
				this.dialog.StartModal(entry.id, ControlRemappingDemo1.DialogHelper.DialogType.AssignElement, new ControlRemappingDemo1.WindowProperties
				{
					title = "Assignment Conflict",
					message = message2,
					rect = this.GetScreenCenteredRect(250f, 200f),
					windowDrawDelegate = new Action<string, string>(this.DrawElementAssignmentNormalConflictWindow)
				}, new Action<int, ControlRemappingDemo1.UserResponse>(this.DialogResultCallback));
			}
			return false;
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x000981C8 File Offset: 0x000965C8
		private bool ProcessFallbackJoystickIdentification(ControlRemappingDemo1.FallbackJoystickIdentification entry)
		{
			if (entry.state == ControlRemappingDemo1.QueueEntry.State.Canceled)
			{
				return true;
			}
			if (entry.state == ControlRemappingDemo1.QueueEntry.State.Confirmed)
			{
				return true;
			}
			this.dialog.StartModal(entry.id, ControlRemappingDemo1.DialogHelper.DialogType.JoystickConflict, new ControlRemappingDemo1.WindowProperties
			{
				title = "Joystick Identification Required",
				message = "A joystick has been attached or removed. You will need to identify each joystick by pressing a button on the controller listed below:",
				rect = this.GetScreenCenteredRect(250f, 200f),
				windowDrawDelegate = new Action<string, string>(this.DrawFallbackJoystickIdentificationWindow)
			}, new Action<int, ControlRemappingDemo1.UserResponse>(this.DialogResultCallback), 1f);
			return false;
		}

		// Token: 0x0600184F RID: 6223 RVA: 0x00098260 File Offset: 0x00096660
		private bool ProcessCalibration(ControlRemappingDemo1.Calibration entry)
		{
			if (entry.state == ControlRemappingDemo1.QueueEntry.State.Canceled)
			{
				return true;
			}
			if (entry.state == ControlRemappingDemo1.QueueEntry.State.Confirmed)
			{
				return true;
			}
			this.dialog.StartModal(entry.id, ControlRemappingDemo1.DialogHelper.DialogType.JoystickConflict, new ControlRemappingDemo1.WindowProperties
			{
				title = "Calibrate Controller",
				message = "Select an axis to calibrate on the " + entry.joystick.name + ".",
				rect = this.GetScreenCenteredRect(450f, 480f),
				windowDrawDelegate = new Action<string, string>(this.DrawCalibrationWindow)
			}, new Action<int, ControlRemappingDemo1.UserResponse>(this.DialogResultCallback));
			return false;
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x00098308 File Offset: 0x00096708
		private void PlayerSelectionChanged()
		{
			this.ClearControllerSelection();
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x00098310 File Offset: 0x00096710
		private void ControllerSelectionChanged()
		{
			this.ClearMapSelection();
		}

		// Token: 0x06001852 RID: 6226 RVA: 0x00098318 File Offset: 0x00096718
		private void ClearControllerSelection()
		{
			this.selectedController.Clear();
			this.ClearMapSelection();
		}

		// Token: 0x06001853 RID: 6227 RVA: 0x0009832B File Offset: 0x0009672B
		private void ClearMapSelection()
		{
			this.selectedMapCategoryId = -1;
			this.selectedMap = null;
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x0009833B File Offset: 0x0009673B
		private void ResetAll()
		{
			this.ClearWorkingVars();
			this.initialized = false;
			this.showMenu = false;
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x00098354 File Offset: 0x00096754
		private void ClearWorkingVars()
		{
			this.selectedPlayer = null;
			this.ClearMapSelection();
			this.selectedController.Clear();
			this.actionScrollPos = default(Vector2);
			this.dialog.FullReset();
			this.actionQueue.Clear();
			this.busy = false;
			this.startListening = false;
			this.conflictFoundEventData = null;
			this.inputMapper.Stop();
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x000983C0 File Offset: 0x000967C0
		private void SetGUIStateStart()
		{
			this.guiState = true;
			if (this.busy)
			{
				this.guiState = false;
			}
			this.pageGUIState = (this.guiState && !this.busy && !this.dialog.enabled && !this.dialog.busy);
			if (GUI.enabled != this.guiState)
			{
				GUI.enabled = this.guiState;
			}
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x0009843E File Offset: 0x0009683E
		private void SetGUIStateEnd()
		{
			this.guiState = true;
			if (!GUI.enabled)
			{
				GUI.enabled = this.guiState;
			}
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x0009845C File Offset: 0x0009685C
		private void JoystickConnected(ControllerStatusChangedEventArgs args)
		{
			if (ReInput.controllers.IsControllerAssigned(args.controllerType, args.controllerId))
			{
				foreach (Player player in ReInput.players.AllPlayers)
				{
					if (player.controllers.ContainsController(args.controllerType, args.controllerId))
					{
						ReInput.userDataStore.LoadControllerData(player.id, args.controllerType, args.controllerId);
					}
				}
			}
			else
			{
				ReInput.userDataStore.LoadControllerData(args.controllerType, args.controllerId);
			}
			if (ReInput.unityJoystickIdentificationRequired)
			{
				this.IdentifyAllJoysticks();
			}
		}

		// Token: 0x06001859 RID: 6233 RVA: 0x00098530 File Offset: 0x00096930
		private void JoystickPreDisconnect(ControllerStatusChangedEventArgs args)
		{
			if (this.selectedController.hasSelection && args.controllerType == this.selectedController.type && args.controllerId == this.selectedController.id)
			{
				this.ClearControllerSelection();
			}
			if (this.showMenu)
			{
				if (ReInput.controllers.IsControllerAssigned(args.controllerType, args.controllerId))
				{
					foreach (Player player in ReInput.players.AllPlayers)
					{
						if (player.controllers.ContainsController(args.controllerType, args.controllerId))
						{
							ReInput.userDataStore.SaveControllerData(player.id, args.controllerType, args.controllerId);
						}
					}
				}
				else
				{
					ReInput.userDataStore.SaveControllerData(args.controllerType, args.controllerId);
				}
			}
		}

		// Token: 0x0600185A RID: 6234 RVA: 0x00098648 File Offset: 0x00096A48
		private void JoystickDisconnected(ControllerStatusChangedEventArgs args)
		{
			if (this.showMenu)
			{
				this.ClearWorkingVars();
			}
			if (ReInput.unityJoystickIdentificationRequired)
			{
				this.IdentifyAllJoysticks();
			}
		}

		// Token: 0x0600185B RID: 6235 RVA: 0x0009866B File Offset: 0x00096A6B
		private void OnConflictFound(InputMapper.ConflictFoundEventData data)
		{
			this.conflictFoundEventData = data;
		}

		// Token: 0x0600185C RID: 6236 RVA: 0x00098674 File Offset: 0x00096A74
		private void OnStopped(InputMapper.StoppedEventData data)
		{
			this.conflictFoundEventData = null;
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x00098680 File Offset: 0x00096A80
		public void IdentifyAllJoysticks()
		{
			if (ReInput.controllers.joystickCount == 0)
			{
				return;
			}
			this.ClearWorkingVars();
			this.Open();
			foreach (Joystick joystick in ReInput.controllers.Joysticks)
			{
				this.actionQueue.Enqueue(new ControlRemappingDemo1.FallbackJoystickIdentification(joystick.id, joystick.name));
			}
		}

		// Token: 0x0600185E RID: 6238 RVA: 0x00098710 File Offset: 0x00096B10
		protected void CheckRecompile()
		{
		}

		// Token: 0x0600185F RID: 6239 RVA: 0x00098712 File Offset: 0x00096B12
		private void RecompileWindow(int windowId)
		{
		}

		// Token: 0x04001B3F RID: 6975
		private const float defaultModalWidth = 250f;

		// Token: 0x04001B40 RID: 6976
		private const float defaultModalHeight = 200f;

		// Token: 0x04001B41 RID: 6977
		private const float assignmentTimeout = 5f;

		// Token: 0x04001B42 RID: 6978
		private ControlRemappingDemo1.DialogHelper dialog;

		// Token: 0x04001B43 RID: 6979
		private InputMapper inputMapper = new InputMapper();

		// Token: 0x04001B44 RID: 6980
		private InputMapper.ConflictFoundEventData conflictFoundEventData;

		// Token: 0x04001B45 RID: 6981
		private bool guiState;

		// Token: 0x04001B46 RID: 6982
		private bool busy;

		// Token: 0x04001B47 RID: 6983
		private bool pageGUIState;

		// Token: 0x04001B48 RID: 6984
		private Player selectedPlayer;

		// Token: 0x04001B49 RID: 6985
		private int selectedMapCategoryId;

		// Token: 0x04001B4A RID: 6986
		private ControlRemappingDemo1.ControllerSelection selectedController;

		// Token: 0x04001B4B RID: 6987
		private ControllerMap selectedMap;

		// Token: 0x04001B4C RID: 6988
		private bool showMenu;

		// Token: 0x04001B4D RID: 6989
		private bool startListening;

		// Token: 0x04001B4E RID: 6990
		private Vector2 actionScrollPos;

		// Token: 0x04001B4F RID: 6991
		private Vector2 calibrateScrollPos;

		// Token: 0x04001B50 RID: 6992
		private Queue<ControlRemappingDemo1.QueueEntry> actionQueue;

		// Token: 0x04001B51 RID: 6993
		private bool setupFinished;

		// Token: 0x04001B52 RID: 6994
		[NonSerialized]
		private bool initialized;

		// Token: 0x04001B53 RID: 6995
		private bool isCompiling;

		// Token: 0x04001B54 RID: 6996
		private GUIStyle style_wordWrap;

		// Token: 0x04001B55 RID: 6997
		private GUIStyle style_centeredBox;

		// Token: 0x02000500 RID: 1280
		private class ControllerSelection
		{
			// Token: 0x06001860 RID: 6240 RVA: 0x00098714 File Offset: 0x00096B14
			public ControllerSelection()
			{
				this.Clear();
			}

			// Token: 0x17000161 RID: 353
			// (get) Token: 0x06001861 RID: 6241 RVA: 0x00098722 File Offset: 0x00096B22
			// (set) Token: 0x06001862 RID: 6242 RVA: 0x0009872A File Offset: 0x00096B2A
			public int id
			{
				get
				{
					return this._id;
				}
				set
				{
					this._idPrev = this._id;
					this._id = value;
				}
			}

			// Token: 0x17000162 RID: 354
			// (get) Token: 0x06001863 RID: 6243 RVA: 0x0009873F File Offset: 0x00096B3F
			// (set) Token: 0x06001864 RID: 6244 RVA: 0x00098747 File Offset: 0x00096B47
			public ControllerType type
			{
				get
				{
					return this._type;
				}
				set
				{
					this._typePrev = this._type;
					this._type = value;
				}
			}

			// Token: 0x17000163 RID: 355
			// (get) Token: 0x06001865 RID: 6245 RVA: 0x0009875C File Offset: 0x00096B5C
			public int idPrev
			{
				get
				{
					return this._idPrev;
				}
			}

			// Token: 0x17000164 RID: 356
			// (get) Token: 0x06001866 RID: 6246 RVA: 0x00098764 File Offset: 0x00096B64
			public ControllerType typePrev
			{
				get
				{
					return this._typePrev;
				}
			}

			// Token: 0x17000165 RID: 357
			// (get) Token: 0x06001867 RID: 6247 RVA: 0x0009876C File Offset: 0x00096B6C
			public bool hasSelection
			{
				get
				{
					return this._id >= 0;
				}
			}

			// Token: 0x06001868 RID: 6248 RVA: 0x0009877A File Offset: 0x00096B7A
			public void Set(int id, ControllerType type)
			{
				this.id = id;
				this.type = type;
			}

			// Token: 0x06001869 RID: 6249 RVA: 0x0009878A File Offset: 0x00096B8A
			public void Clear()
			{
				this._id = -1;
				this._idPrev = -1;
				this._type = ControllerType.Joystick;
				this._typePrev = ControllerType.Joystick;
			}

			// Token: 0x04001B56 RID: 6998
			private int _id;

			// Token: 0x04001B57 RID: 6999
			private int _idPrev;

			// Token: 0x04001B58 RID: 7000
			private ControllerType _type;

			// Token: 0x04001B59 RID: 7001
			private ControllerType _typePrev;
		}

		// Token: 0x02000501 RID: 1281
		private class DialogHelper
		{
			// Token: 0x0600186A RID: 6250 RVA: 0x000987A8 File Offset: 0x00096BA8
			public DialogHelper()
			{
				this.drawWindowDelegate = new Action<int>(this.DrawWindow);
				this.drawWindowFunction = new GUI.WindowFunction(this.drawWindowDelegate.Invoke);
			}

			// Token: 0x17000166 RID: 358
			// (get) Token: 0x0600186B RID: 6251 RVA: 0x000987D9 File Offset: 0x00096BD9
			private float busyTimer
			{
				get
				{
					if (!this._busyTimerRunning)
					{
						return 0f;
					}
					return this._busyTime - Time.realtimeSinceStartup;
				}
			}

			// Token: 0x17000167 RID: 359
			// (get) Token: 0x0600186C RID: 6252 RVA: 0x000987F8 File Offset: 0x00096BF8
			// (set) Token: 0x0600186D RID: 6253 RVA: 0x00098800 File Offset: 0x00096C00
			public bool enabled
			{
				get
				{
					return this._enabled;
				}
				set
				{
					if (value)
					{
						if (this._type == ControlRemappingDemo1.DialogHelper.DialogType.None)
						{
							return;
						}
						this.StateChanged(0.25f);
					}
					else
					{
						this._enabled = value;
						this._type = ControlRemappingDemo1.DialogHelper.DialogType.None;
						this.StateChanged(0.1f);
					}
				}
			}

			// Token: 0x17000168 RID: 360
			// (get) Token: 0x0600186E RID: 6254 RVA: 0x0009883D File Offset: 0x00096C3D
			// (set) Token: 0x0600186F RID: 6255 RVA: 0x00098852 File Offset: 0x00096C52
			public ControlRemappingDemo1.DialogHelper.DialogType type
			{
				get
				{
					if (!this._enabled)
					{
						return ControlRemappingDemo1.DialogHelper.DialogType.None;
					}
					return this._type;
				}
				set
				{
					if (value == ControlRemappingDemo1.DialogHelper.DialogType.None)
					{
						this._enabled = false;
						this.StateChanged(0.1f);
					}
					else
					{
						this._enabled = true;
						this.StateChanged(0.25f);
					}
					this._type = value;
				}
			}

			// Token: 0x17000169 RID: 361
			// (get) Token: 0x06001870 RID: 6256 RVA: 0x0009888A File Offset: 0x00096C8A
			public bool busy
			{
				get
				{
					return this._busyTimerRunning;
				}
			}

			// Token: 0x06001871 RID: 6257 RVA: 0x00098892 File Offset: 0x00096C92
			public void StartModal(int queueActionId, ControlRemappingDemo1.DialogHelper.DialogType type, ControlRemappingDemo1.WindowProperties windowProperties, Action<int, ControlRemappingDemo1.UserResponse> resultCallback)
			{
				this.StartModal(queueActionId, type, windowProperties, resultCallback, -1f);
			}

			// Token: 0x06001872 RID: 6258 RVA: 0x000988A4 File Offset: 0x00096CA4
			public void StartModal(int queueActionId, ControlRemappingDemo1.DialogHelper.DialogType type, ControlRemappingDemo1.WindowProperties windowProperties, Action<int, ControlRemappingDemo1.UserResponse> resultCallback, float openBusyDelay)
			{
				this.currentActionId = queueActionId;
				this.windowProperties = windowProperties;
				this.type = type;
				this.resultCallback = resultCallback;
				if (openBusyDelay >= 0f)
				{
					this.StateChanged(openBusyDelay);
				}
			}

			// Token: 0x06001873 RID: 6259 RVA: 0x000988D7 File Offset: 0x00096CD7
			public void Update()
			{
				this.Draw();
				this.UpdateTimers();
			}

			// Token: 0x06001874 RID: 6260 RVA: 0x000988E8 File Offset: 0x00096CE8
			public void Draw()
			{
				if (!this._enabled)
				{
					return;
				}
				bool enabled = GUI.enabled;
				GUI.enabled = true;
				GUILayout.Window(this.windowProperties.windowId, this.windowProperties.rect, this.drawWindowFunction, this.windowProperties.title, Array.Empty<GUILayoutOption>());
				GUI.FocusWindow(this.windowProperties.windowId);
				if (GUI.enabled != enabled)
				{
					GUI.enabled = enabled;
				}
			}

			// Token: 0x06001875 RID: 6261 RVA: 0x00098960 File Offset: 0x00096D60
			public void DrawConfirmButton()
			{
				this.DrawConfirmButton("Confirm");
			}

			// Token: 0x06001876 RID: 6262 RVA: 0x00098970 File Offset: 0x00096D70
			public void DrawConfirmButton(string title)
			{
				bool enabled = GUI.enabled;
				if (this.busy)
				{
					GUI.enabled = false;
				}
				if (GUILayout.Button(title, Array.Empty<GUILayoutOption>()))
				{
					this.Confirm(ControlRemappingDemo1.UserResponse.Confirm);
				}
				if (GUI.enabled != enabled)
				{
					GUI.enabled = enabled;
				}
			}

			// Token: 0x06001877 RID: 6263 RVA: 0x000989BC File Offset: 0x00096DBC
			public void DrawConfirmButton(ControlRemappingDemo1.UserResponse response)
			{
				this.DrawConfirmButton(response, "Confirm");
			}

			// Token: 0x06001878 RID: 6264 RVA: 0x000989CC File Offset: 0x00096DCC
			public void DrawConfirmButton(ControlRemappingDemo1.UserResponse response, string title)
			{
				bool enabled = GUI.enabled;
				if (this.busy)
				{
					GUI.enabled = false;
				}
				if (GUILayout.Button(title, Array.Empty<GUILayoutOption>()))
				{
					this.Confirm(response);
				}
				if (GUI.enabled != enabled)
				{
					GUI.enabled = enabled;
				}
			}

			// Token: 0x06001879 RID: 6265 RVA: 0x00098A18 File Offset: 0x00096E18
			public void DrawCancelButton()
			{
				this.DrawCancelButton("Cancel");
			}

			// Token: 0x0600187A RID: 6266 RVA: 0x00098A28 File Offset: 0x00096E28
			public void DrawCancelButton(string title)
			{
				bool enabled = GUI.enabled;
				if (this.busy)
				{
					GUI.enabled = false;
				}
				if (GUILayout.Button(title, Array.Empty<GUILayoutOption>()))
				{
					this.Cancel();
				}
				if (GUI.enabled != enabled)
				{
					GUI.enabled = enabled;
				}
			}

			// Token: 0x0600187B RID: 6267 RVA: 0x00098A73 File Offset: 0x00096E73
			public void Confirm()
			{
				this.Confirm(ControlRemappingDemo1.UserResponse.Confirm);
			}

			// Token: 0x0600187C RID: 6268 RVA: 0x00098A7C File Offset: 0x00096E7C
			public void Confirm(ControlRemappingDemo1.UserResponse response)
			{
				this.resultCallback(this.currentActionId, response);
				this.Close();
			}

			// Token: 0x0600187D RID: 6269 RVA: 0x00098A96 File Offset: 0x00096E96
			public void Cancel()
			{
				this.resultCallback(this.currentActionId, ControlRemappingDemo1.UserResponse.Cancel);
				this.Close();
			}

			// Token: 0x0600187E RID: 6270 RVA: 0x00098AB0 File Offset: 0x00096EB0
			private void DrawWindow(int windowId)
			{
				this.windowProperties.windowDrawDelegate(this.windowProperties.title, this.windowProperties.message);
			}

			// Token: 0x0600187F RID: 6271 RVA: 0x00098AD8 File Offset: 0x00096ED8
			private void UpdateTimers()
			{
				if (this._busyTimerRunning && this.busyTimer <= 0f)
				{
					this._busyTimerRunning = false;
				}
			}

			// Token: 0x06001880 RID: 6272 RVA: 0x00098AFC File Offset: 0x00096EFC
			private void StartBusyTimer(float time)
			{
				this._busyTime = time + Time.realtimeSinceStartup;
				this._busyTimerRunning = true;
			}

			// Token: 0x06001881 RID: 6273 RVA: 0x00098B12 File Offset: 0x00096F12
			private void Close()
			{
				this.Reset();
				this.StateChanged(0.1f);
			}

			// Token: 0x06001882 RID: 6274 RVA: 0x00098B25 File Offset: 0x00096F25
			private void StateChanged(float delay)
			{
				this.StartBusyTimer(delay);
			}

			// Token: 0x06001883 RID: 6275 RVA: 0x00098B2E File Offset: 0x00096F2E
			private void Reset()
			{
				this._enabled = false;
				this._type = ControlRemappingDemo1.DialogHelper.DialogType.None;
				this.currentActionId = -1;
				this.resultCallback = null;
			}

			// Token: 0x06001884 RID: 6276 RVA: 0x00098B4C File Offset: 0x00096F4C
			private void ResetTimers()
			{
				this._busyTimerRunning = false;
			}

			// Token: 0x06001885 RID: 6277 RVA: 0x00098B55 File Offset: 0x00096F55
			public void FullReset()
			{
				this.Reset();
				this.ResetTimers();
			}

			// Token: 0x04001B5A RID: 7002
			private const float openBusyDelay = 0.25f;

			// Token: 0x04001B5B RID: 7003
			private const float closeBusyDelay = 0.1f;

			// Token: 0x04001B5C RID: 7004
			private ControlRemappingDemo1.DialogHelper.DialogType _type;

			// Token: 0x04001B5D RID: 7005
			private bool _enabled;

			// Token: 0x04001B5E RID: 7006
			private float _busyTime;

			// Token: 0x04001B5F RID: 7007
			private bool _busyTimerRunning;

			// Token: 0x04001B60 RID: 7008
			private Action<int> drawWindowDelegate;

			// Token: 0x04001B61 RID: 7009
			private GUI.WindowFunction drawWindowFunction;

			// Token: 0x04001B62 RID: 7010
			private ControlRemappingDemo1.WindowProperties windowProperties;

			// Token: 0x04001B63 RID: 7011
			private int currentActionId;

			// Token: 0x04001B64 RID: 7012
			private Action<int, ControlRemappingDemo1.UserResponse> resultCallback;

			// Token: 0x02000502 RID: 1282
			public enum DialogType
			{
				// Token: 0x04001B66 RID: 7014
				None,
				// Token: 0x04001B67 RID: 7015
				JoystickConflict,
				// Token: 0x04001B68 RID: 7016
				ElementConflict,
				// Token: 0x04001B69 RID: 7017
				KeyConflict,
				// Token: 0x04001B6A RID: 7018
				DeleteAssignmentConfirmation = 10,
				// Token: 0x04001B6B RID: 7019
				AssignElement
			}
		}

		// Token: 0x02000503 RID: 1283
		private abstract class QueueEntry
		{
			// Token: 0x06001886 RID: 6278 RVA: 0x00098B63 File Offset: 0x00096F63
			public QueueEntry(ControlRemappingDemo1.QueueActionType queueActionType)
			{
				this.id = ControlRemappingDemo1.QueueEntry.nextId;
				this.queueActionType = queueActionType;
			}

			// Token: 0x1700016A RID: 362
			// (get) Token: 0x06001887 RID: 6279 RVA: 0x00098B7D File Offset: 0x00096F7D
			// (set) Token: 0x06001888 RID: 6280 RVA: 0x00098B85 File Offset: 0x00096F85
			public int id { get; protected set; }

			// Token: 0x1700016B RID: 363
			// (get) Token: 0x06001889 RID: 6281 RVA: 0x00098B8E File Offset: 0x00096F8E
			// (set) Token: 0x0600188A RID: 6282 RVA: 0x00098B96 File Offset: 0x00096F96
			public ControlRemappingDemo1.QueueActionType queueActionType { get; protected set; }

			// Token: 0x1700016C RID: 364
			// (get) Token: 0x0600188B RID: 6283 RVA: 0x00098B9F File Offset: 0x00096F9F
			// (set) Token: 0x0600188C RID: 6284 RVA: 0x00098BA7 File Offset: 0x00096FA7
			public ControlRemappingDemo1.QueueEntry.State state { get; protected set; }

			// Token: 0x1700016D RID: 365
			// (get) Token: 0x0600188D RID: 6285 RVA: 0x00098BB0 File Offset: 0x00096FB0
			// (set) Token: 0x0600188E RID: 6286 RVA: 0x00098BB8 File Offset: 0x00096FB8
			public ControlRemappingDemo1.UserResponse response { get; protected set; }

			// Token: 0x1700016E RID: 366
			// (get) Token: 0x0600188F RID: 6287 RVA: 0x00098BC4 File Offset: 0x00096FC4
			protected static int nextId
			{
				get
				{
					int result = ControlRemappingDemo1.QueueEntry.uidCounter;
					ControlRemappingDemo1.QueueEntry.uidCounter++;
					return result;
				}
			}

			// Token: 0x06001890 RID: 6288 RVA: 0x00098BE4 File Offset: 0x00096FE4
			public void Confirm(ControlRemappingDemo1.UserResponse response)
			{
				this.state = ControlRemappingDemo1.QueueEntry.State.Confirmed;
				this.response = response;
			}

			// Token: 0x06001891 RID: 6289 RVA: 0x00098BF4 File Offset: 0x00096FF4
			public void Cancel()
			{
				this.state = ControlRemappingDemo1.QueueEntry.State.Canceled;
			}

			// Token: 0x04001B70 RID: 7024
			private static int uidCounter;

			// Token: 0x02000504 RID: 1284
			public enum State
			{
				// Token: 0x04001B72 RID: 7026
				Waiting,
				// Token: 0x04001B73 RID: 7027
				Confirmed,
				// Token: 0x04001B74 RID: 7028
				Canceled
			}
		}

		// Token: 0x02000505 RID: 1285
		private class JoystickAssignmentChange : ControlRemappingDemo1.QueueEntry
		{
			// Token: 0x06001892 RID: 6290 RVA: 0x00098BFD File Offset: 0x00096FFD
			public JoystickAssignmentChange(int newPlayerId, int joystickId, bool assign) : base(ControlRemappingDemo1.QueueActionType.JoystickAssignment)
			{
				this.playerId = newPlayerId;
				this.joystickId = joystickId;
				this.assign = assign;
			}

			// Token: 0x1700016F RID: 367
			// (get) Token: 0x06001893 RID: 6291 RVA: 0x00098C1B File Offset: 0x0009701B
			// (set) Token: 0x06001894 RID: 6292 RVA: 0x00098C23 File Offset: 0x00097023
			public int playerId { get; private set; }

			// Token: 0x17000170 RID: 368
			// (get) Token: 0x06001895 RID: 6293 RVA: 0x00098C2C File Offset: 0x0009702C
			// (set) Token: 0x06001896 RID: 6294 RVA: 0x00098C34 File Offset: 0x00097034
			public int joystickId { get; private set; }

			// Token: 0x17000171 RID: 369
			// (get) Token: 0x06001897 RID: 6295 RVA: 0x00098C3D File Offset: 0x0009703D
			// (set) Token: 0x06001898 RID: 6296 RVA: 0x00098C45 File Offset: 0x00097045
			public bool assign { get; private set; }
		}

		// Token: 0x02000506 RID: 1286
		private class ElementAssignmentChange : ControlRemappingDemo1.QueueEntry
		{
			// Token: 0x06001899 RID: 6297 RVA: 0x00098C4E File Offset: 0x0009704E
			public ElementAssignmentChange(ControlRemappingDemo1.ElementAssignmentChangeType changeType, InputMapper.Context context) : base(ControlRemappingDemo1.QueueActionType.ElementAssignment)
			{
				this.changeType = changeType;
				this.context = context;
			}

			// Token: 0x0600189A RID: 6298 RVA: 0x00098C65 File Offset: 0x00097065
			public ElementAssignmentChange(ControlRemappingDemo1.ElementAssignmentChange other) : this(other.changeType, other.context.Clone())
			{
			}

			// Token: 0x17000172 RID: 370
			// (get) Token: 0x0600189B RID: 6299 RVA: 0x00098C7E File Offset: 0x0009707E
			// (set) Token: 0x0600189C RID: 6300 RVA: 0x00098C86 File Offset: 0x00097086
			public ControlRemappingDemo1.ElementAssignmentChangeType changeType { get; set; }

			// Token: 0x17000173 RID: 371
			// (get) Token: 0x0600189D RID: 6301 RVA: 0x00098C8F File Offset: 0x0009708F
			// (set) Token: 0x0600189E RID: 6302 RVA: 0x00098C97 File Offset: 0x00097097
			public InputMapper.Context context { get; private set; }
		}

		// Token: 0x02000507 RID: 1287
		private class FallbackJoystickIdentification : ControlRemappingDemo1.QueueEntry
		{
			// Token: 0x0600189F RID: 6303 RVA: 0x00098CA0 File Offset: 0x000970A0
			public FallbackJoystickIdentification(int joystickId, string joystickName) : base(ControlRemappingDemo1.QueueActionType.FallbackJoystickIdentification)
			{
				this.joystickId = joystickId;
				this.joystickName = joystickName;
			}

			// Token: 0x17000174 RID: 372
			// (get) Token: 0x060018A0 RID: 6304 RVA: 0x00098CB7 File Offset: 0x000970B7
			// (set) Token: 0x060018A1 RID: 6305 RVA: 0x00098CBF File Offset: 0x000970BF
			public int joystickId { get; private set; }

			// Token: 0x17000175 RID: 373
			// (get) Token: 0x060018A2 RID: 6306 RVA: 0x00098CC8 File Offset: 0x000970C8
			// (set) Token: 0x060018A3 RID: 6307 RVA: 0x00098CD0 File Offset: 0x000970D0
			public string joystickName { get; private set; }
		}

		// Token: 0x02000508 RID: 1288
		private class Calibration : ControlRemappingDemo1.QueueEntry
		{
			// Token: 0x060018A4 RID: 6308 RVA: 0x00098CD9 File Offset: 0x000970D9
			public Calibration(Player player, Joystick joystick, CalibrationMap calibrationMap) : base(ControlRemappingDemo1.QueueActionType.Calibrate)
			{
				this.player = player;
				this.joystick = joystick;
				this.calibrationMap = calibrationMap;
				this.selectedElementIdentifierId = -1;
			}

			// Token: 0x17000176 RID: 374
			// (get) Token: 0x060018A5 RID: 6309 RVA: 0x00098CFE File Offset: 0x000970FE
			// (set) Token: 0x060018A6 RID: 6310 RVA: 0x00098D06 File Offset: 0x00097106
			public Player player { get; private set; }

			// Token: 0x17000177 RID: 375
			// (get) Token: 0x060018A7 RID: 6311 RVA: 0x00098D0F File Offset: 0x0009710F
			// (set) Token: 0x060018A8 RID: 6312 RVA: 0x00098D17 File Offset: 0x00097117
			public ControllerType controllerType { get; private set; }

			// Token: 0x17000178 RID: 376
			// (get) Token: 0x060018A9 RID: 6313 RVA: 0x00098D20 File Offset: 0x00097120
			// (set) Token: 0x060018AA RID: 6314 RVA: 0x00098D28 File Offset: 0x00097128
			public Joystick joystick { get; private set; }

			// Token: 0x17000179 RID: 377
			// (get) Token: 0x060018AB RID: 6315 RVA: 0x00098D31 File Offset: 0x00097131
			// (set) Token: 0x060018AC RID: 6316 RVA: 0x00098D39 File Offset: 0x00097139
			public CalibrationMap calibrationMap { get; private set; }

			// Token: 0x04001B80 RID: 7040
			public int selectedElementIdentifierId;

			// Token: 0x04001B81 RID: 7041
			public bool recording;
		}

		// Token: 0x02000509 RID: 1289
		private struct WindowProperties
		{
			// Token: 0x04001B82 RID: 7042
			public int windowId;

			// Token: 0x04001B83 RID: 7043
			public Rect rect;

			// Token: 0x04001B84 RID: 7044
			public Action<string, string> windowDrawDelegate;

			// Token: 0x04001B85 RID: 7045
			public string title;

			// Token: 0x04001B86 RID: 7046
			public string message;
		}

		// Token: 0x0200050A RID: 1290
		private enum QueueActionType
		{
			// Token: 0x04001B88 RID: 7048
			None,
			// Token: 0x04001B89 RID: 7049
			JoystickAssignment,
			// Token: 0x04001B8A RID: 7050
			ElementAssignment,
			// Token: 0x04001B8B RID: 7051
			FallbackJoystickIdentification,
			// Token: 0x04001B8C RID: 7052
			Calibrate
		}

		// Token: 0x0200050B RID: 1291
		private enum ElementAssignmentChangeType
		{
			// Token: 0x04001B8E RID: 7054
			Add,
			// Token: 0x04001B8F RID: 7055
			Replace,
			// Token: 0x04001B90 RID: 7056
			Remove,
			// Token: 0x04001B91 RID: 7057
			ReassignOrRemove,
			// Token: 0x04001B92 RID: 7058
			ConflictCheck
		}

		// Token: 0x0200050C RID: 1292
		public enum UserResponse
		{
			// Token: 0x04001B94 RID: 7060
			Confirm,
			// Token: 0x04001B95 RID: 7061
			Cancel,
			// Token: 0x04001B96 RID: 7062
			Custom1,
			// Token: 0x04001B97 RID: 7063
			Custom2
		}
	}
}
