using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x02000550 RID: 1360
	public class LanguageData : ScriptableObject
	{
		// Token: 0x06001C67 RID: 7271 RVA: 0x000AA107 File Offset: 0x000A8507
		public void Initialize()
		{
			if (this._initialized)
			{
				return;
			}
			this.customDict = LanguageData.CustomEntry.ToDictionary(this._customEntries);
			this._initialized = true;
		}

		// Token: 0x06001C68 RID: 7272 RVA: 0x000AA130 File Offset: 0x000A8530
		public string GetCustomEntry(string key)
		{
			if (string.IsNullOrEmpty(key))
			{
				return string.Empty;
			}
			string result;
			if (!this.customDict.TryGetValue(key, out result))
			{
				return string.Empty;
			}
			return result;
		}

		// Token: 0x06001C69 RID: 7273 RVA: 0x000AA168 File Offset: 0x000A8568
		public bool ContainsCustomEntryKey(string key)
		{
			return !string.IsNullOrEmpty(key) && this.customDict.ContainsKey(key);
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06001C6A RID: 7274 RVA: 0x000AA183 File Offset: 0x000A8583
		public string yes
		{
			get
			{
				return this._yes;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06001C6B RID: 7275 RVA: 0x000AA18B File Offset: 0x000A858B
		public string no
		{
			get
			{
				return this._no;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06001C6C RID: 7276 RVA: 0x000AA193 File Offset: 0x000A8593
		public string add
		{
			get
			{
				return this._add;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06001C6D RID: 7277 RVA: 0x000AA19B File Offset: 0x000A859B
		public string replace
		{
			get
			{
				return this._replace;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06001C6E RID: 7278 RVA: 0x000AA1A3 File Offset: 0x000A85A3
		public string remove
		{
			get
			{
				return this._remove;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06001C6F RID: 7279 RVA: 0x000AA1AB File Offset: 0x000A85AB
		public string swap
		{
			get
			{
				return this._swap;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06001C70 RID: 7280 RVA: 0x000AA1B3 File Offset: 0x000A85B3
		public string cancel
		{
			get
			{
				return this._cancel;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06001C71 RID: 7281 RVA: 0x000AA1BB File Offset: 0x000A85BB
		public string none
		{
			get
			{
				return this._none;
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06001C72 RID: 7282 RVA: 0x000AA1C3 File Offset: 0x000A85C3
		public string okay
		{
			get
			{
				return this._okay;
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06001C73 RID: 7283 RVA: 0x000AA1CB File Offset: 0x000A85CB
		public string done
		{
			get
			{
				return this._done;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06001C74 RID: 7284 RVA: 0x000AA1D3 File Offset: 0x000A85D3
		public string default_
		{
			get
			{
				return this._default;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06001C75 RID: 7285 RVA: 0x000AA1DB File Offset: 0x000A85DB
		public string assignControllerWindowTitle
		{
			get
			{
				return this._assignControllerWindowTitle;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06001C76 RID: 7286 RVA: 0x000AA1E3 File Offset: 0x000A85E3
		public string assignControllerWindowMessage
		{
			get
			{
				return this._assignControllerWindowMessage;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06001C77 RID: 7287 RVA: 0x000AA1EB File Offset: 0x000A85EB
		public string controllerAssignmentConflictWindowTitle
		{
			get
			{
				return this._controllerAssignmentConflictWindowTitle;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06001C78 RID: 7288 RVA: 0x000AA1F3 File Offset: 0x000A85F3
		public string elementAssignmentPrePollingWindowMessage
		{
			get
			{
				return this._elementAssignmentPrePollingWindowMessage;
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06001C79 RID: 7289 RVA: 0x000AA1FB File Offset: 0x000A85FB
		public string elementAssignmentConflictWindowMessage
		{
			get
			{
				return this._elementAssignmentConflictWindowMessage;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06001C7A RID: 7290 RVA: 0x000AA203 File Offset: 0x000A8603
		public string mouseAssignmentConflictWindowTitle
		{
			get
			{
				return this._mouseAssignmentConflictWindowTitle;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06001C7B RID: 7291 RVA: 0x000AA20B File Offset: 0x000A860B
		public string calibrateControllerWindowTitle
		{
			get
			{
				return this._calibrateControllerWindowTitle;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06001C7C RID: 7292 RVA: 0x000AA213 File Offset: 0x000A8613
		public string calibrateAxisStep1WindowTitle
		{
			get
			{
				return this._calibrateAxisStep1WindowTitle;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06001C7D RID: 7293 RVA: 0x000AA21B File Offset: 0x000A861B
		public string calibrateAxisStep2WindowTitle
		{
			get
			{
				return this._calibrateAxisStep2WindowTitle;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06001C7E RID: 7294 RVA: 0x000AA223 File Offset: 0x000A8623
		public string inputBehaviorSettingsWindowTitle
		{
			get
			{
				return this._inputBehaviorSettingsWindowTitle;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06001C7F RID: 7295 RVA: 0x000AA22B File Offset: 0x000A862B
		public string restoreDefaultsWindowTitle
		{
			get
			{
				return this._restoreDefaultsWindowTitle;
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06001C80 RID: 7296 RVA: 0x000AA233 File Offset: 0x000A8633
		public string actionColumnLabel
		{
			get
			{
				return this._actionColumnLabel;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06001C81 RID: 7297 RVA: 0x000AA23B File Offset: 0x000A863B
		public string keyboardColumnLabel
		{
			get
			{
				return this._keyboardColumnLabel;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06001C82 RID: 7298 RVA: 0x000AA243 File Offset: 0x000A8643
		public string mouseColumnLabel
		{
			get
			{
				return this._mouseColumnLabel;
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06001C83 RID: 7299 RVA: 0x000AA24B File Offset: 0x000A864B
		public string controllerColumnLabel
		{
			get
			{
				return this._controllerColumnLabel;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06001C84 RID: 7300 RVA: 0x000AA253 File Offset: 0x000A8653
		public string removeControllerButtonLabel
		{
			get
			{
				return this._removeControllerButtonLabel;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06001C85 RID: 7301 RVA: 0x000AA25B File Offset: 0x000A865B
		public string calibrateControllerButtonLabel
		{
			get
			{
				return this._calibrateControllerButtonLabel;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06001C86 RID: 7302 RVA: 0x000AA263 File Offset: 0x000A8663
		public string assignControllerButtonLabel
		{
			get
			{
				return this._assignControllerButtonLabel;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06001C87 RID: 7303 RVA: 0x000AA26B File Offset: 0x000A866B
		public string inputBehaviorSettingsButtonLabel
		{
			get
			{
				return this._inputBehaviorSettingsButtonLabel;
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06001C88 RID: 7304 RVA: 0x000AA273 File Offset: 0x000A8673
		public string doneButtonLabel
		{
			get
			{
				return this._doneButtonLabel;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06001C89 RID: 7305 RVA: 0x000AA27B File Offset: 0x000A867B
		public string restoreDefaultsButtonLabel
		{
			get
			{
				return this._restoreDefaultsButtonLabel;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06001C8A RID: 7306 RVA: 0x000AA283 File Offset: 0x000A8683
		public string controllerSettingsGroupLabel
		{
			get
			{
				return this._controllerSettingsGroupLabel;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06001C8B RID: 7307 RVA: 0x000AA28B File Offset: 0x000A868B
		public string playersGroupLabel
		{
			get
			{
				return this._playersGroupLabel;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06001C8C RID: 7308 RVA: 0x000AA293 File Offset: 0x000A8693
		public string assignedControllersGroupLabel
		{
			get
			{
				return this._assignedControllersGroupLabel;
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06001C8D RID: 7309 RVA: 0x000AA29B File Offset: 0x000A869B
		public string settingsGroupLabel
		{
			get
			{
				return this._settingsGroupLabel;
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06001C8E RID: 7310 RVA: 0x000AA2A3 File Offset: 0x000A86A3
		public string mapCategoriesGroupLabel
		{
			get
			{
				return this._mapCategoriesGroupLabel;
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06001C8F RID: 7311 RVA: 0x000AA2AB File Offset: 0x000A86AB
		public string restoreDefaultsWindowMessage
		{
			get
			{
				if (ReInput.players.playerCount > 1)
				{
					return this._restoreDefaultsWindowMessage_multiPlayer;
				}
				return this._restoreDefaultsWindowMessage_onePlayer;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06001C90 RID: 7312 RVA: 0x000AA2CA File Offset: 0x000A86CA
		public string calibrateWindow_deadZoneSliderLabel
		{
			get
			{
				return this._calibrateWindow_deadZoneSliderLabel;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06001C91 RID: 7313 RVA: 0x000AA2D2 File Offset: 0x000A86D2
		public string calibrateWindow_zeroSliderLabel
		{
			get
			{
				return this._calibrateWindow_zeroSliderLabel;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06001C92 RID: 7314 RVA: 0x000AA2DA File Offset: 0x000A86DA
		public string calibrateWindow_sensitivitySliderLabel
		{
			get
			{
				return this._calibrateWindow_sensitivitySliderLabel;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06001C93 RID: 7315 RVA: 0x000AA2E2 File Offset: 0x000A86E2
		public string calibrateWindow_invertToggleLabel
		{
			get
			{
				return this._calibrateWindow_invertToggleLabel;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06001C94 RID: 7316 RVA: 0x000AA2EA File Offset: 0x000A86EA
		public string calibrateWindow_calibrateButtonLabel
		{
			get
			{
				return this._calibrateWindow_calibrateButtonLabel;
			}
		}

		// Token: 0x06001C95 RID: 7317 RVA: 0x000AA2F2 File Offset: 0x000A86F2
		public string GetControllerAssignmentConflictWindowMessage(string joystickName, string otherPlayerName, string currentPlayerName)
		{
			return string.Format(this._controllerAssignmentConflictWindowMessage, joystickName, otherPlayerName, currentPlayerName);
		}

		// Token: 0x06001C96 RID: 7318 RVA: 0x000AA302 File Offset: 0x000A8702
		public string GetJoystickElementAssignmentPollingWindowMessage(string actionName)
		{
			return string.Format(this._joystickElementAssignmentPollingWindowMessage, actionName);
		}

		// Token: 0x06001C97 RID: 7319 RVA: 0x000AA310 File Offset: 0x000A8710
		public string GetJoystickElementAssignmentPollingWindowMessage_FullAxisFieldOnly(string actionName)
		{
			return string.Format(this._joystickElementAssignmentPollingWindowMessage_fullAxisFieldOnly, actionName);
		}

		// Token: 0x06001C98 RID: 7320 RVA: 0x000AA31E File Offset: 0x000A871E
		public string GetKeyboardElementAssignmentPollingWindowMessage(string actionName)
		{
			return string.Format(this._keyboardElementAssignmentPollingWindowMessage, actionName);
		}

		// Token: 0x06001C99 RID: 7321 RVA: 0x000AA32C File Offset: 0x000A872C
		public string GetMouseElementAssignmentPollingWindowMessage(string actionName)
		{
			return string.Format(this._mouseElementAssignmentPollingWindowMessage, actionName);
		}

		// Token: 0x06001C9A RID: 7322 RVA: 0x000AA33A File Offset: 0x000A873A
		public string GetMouseElementAssignmentPollingWindowMessage_FullAxisFieldOnly(string actionName)
		{
			return string.Format(this._mouseElementAssignmentPollingWindowMessage_fullAxisFieldOnly, actionName);
		}

		// Token: 0x06001C9B RID: 7323 RVA: 0x000AA348 File Offset: 0x000A8748
		public string GetElementAlreadyInUseBlocked(string elementName)
		{
			return string.Format(this._elementAlreadyInUseBlocked, elementName);
		}

		// Token: 0x06001C9C RID: 7324 RVA: 0x000AA356 File Offset: 0x000A8756
		public string GetElementAlreadyInUseCanReplace(string elementName, bool allowConflicts)
		{
			if (!allowConflicts)
			{
				return string.Format(this._elementAlreadyInUseCanReplace, elementName);
			}
			return string.Format(this._elementAlreadyInUseCanReplace_conflictAllowed, elementName);
		}

		// Token: 0x06001C9D RID: 7325 RVA: 0x000AA377 File Offset: 0x000A8777
		public string GetMouseAssignmentConflictWindowMessage(string otherPlayerName, string thisPlayerName)
		{
			return string.Format(this._mouseAssignmentConflictWindowMessage, otherPlayerName, thisPlayerName);
		}

		// Token: 0x06001C9E RID: 7326 RVA: 0x000AA386 File Offset: 0x000A8786
		public string GetCalibrateAxisStep1WindowMessage(string axisName)
		{
			return string.Format(this._calibrateAxisStep1WindowMessage, axisName);
		}

		// Token: 0x06001C9F RID: 7327 RVA: 0x000AA394 File Offset: 0x000A8794
		public string GetCalibrateAxisStep2WindowMessage(string axisName)
		{
			return string.Format(this._calibrateAxisStep2WindowMessage, axisName);
		}

		// Token: 0x04001D9F RID: 7583
		[SerializeField]
		private string _yes = "Yes";

		// Token: 0x04001DA0 RID: 7584
		[SerializeField]
		private string _no = "No";

		// Token: 0x04001DA1 RID: 7585
		[SerializeField]
		private string _add = "Add";

		// Token: 0x04001DA2 RID: 7586
		[SerializeField]
		private string _replace = "Replace";

		// Token: 0x04001DA3 RID: 7587
		[SerializeField]
		private string _remove = "Remove";

		// Token: 0x04001DA4 RID: 7588
		[SerializeField]
		private string _swap = "Swap";

		// Token: 0x04001DA5 RID: 7589
		[SerializeField]
		private string _cancel = "Cancel";

		// Token: 0x04001DA6 RID: 7590
		[SerializeField]
		private string _none = "None";

		// Token: 0x04001DA7 RID: 7591
		[SerializeField]
		private string _okay = "Okay";

		// Token: 0x04001DA8 RID: 7592
		[SerializeField]
		private string _done = "Done";

		// Token: 0x04001DA9 RID: 7593
		[SerializeField]
		private string _default = "Default";

		// Token: 0x04001DAA RID: 7594
		[SerializeField]
		private string _assignControllerWindowTitle = "Choose Controller";

		// Token: 0x04001DAB RID: 7595
		[SerializeField]
		private string _assignControllerWindowMessage = "Press any button or move an axis on the controller you would like to use.";

		// Token: 0x04001DAC RID: 7596
		[SerializeField]
		private string _controllerAssignmentConflictWindowTitle = "Controller Assignment";

		// Token: 0x04001DAD RID: 7597
		[SerializeField]
		[Tooltip("{0} = Joystick Name\n{1} = Other Player Name\n{2} = This Player Name")]
		private string _controllerAssignmentConflictWindowMessage = "{0} is already assigned to {1}. Do you want to assign this controller to {2} instead?";

		// Token: 0x04001DAE RID: 7598
		[SerializeField]
		private string _elementAssignmentPrePollingWindowMessage = "First center or zero all sticks and axes and press any button or wait for the timer to finish.";

		// Token: 0x04001DAF RID: 7599
		[SerializeField]
		[Tooltip("{0} = Action Name")]
		private string _joystickElementAssignmentPollingWindowMessage = "Now press a button or move an axis to assign it to {0}.";

		// Token: 0x04001DB0 RID: 7600
		[SerializeField]
		[Tooltip("This text is only displayed when split-axis fields have been disabled and the user clicks on the full-axis field. Button/key/D-pad input cannot be assigned to a full-axis field.\n{0} = Action Name")]
		private string _joystickElementAssignmentPollingWindowMessage_fullAxisFieldOnly = "Now move an axis to assign it to {0}.";

		// Token: 0x04001DB1 RID: 7601
		[SerializeField]
		[Tooltip("{0} = Action Name")]
		private string _keyboardElementAssignmentPollingWindowMessage = "Press a key to assign it to {0}. Modifier keys may also be used. To assign a modifier key alone, hold it down for 1 second.";

		// Token: 0x04001DB2 RID: 7602
		[SerializeField]
		[Tooltip("{0} = Action Name")]
		private string _mouseElementAssignmentPollingWindowMessage = "Press a mouse button or move an axis to assign it to {0}.";

		// Token: 0x04001DB3 RID: 7603
		[SerializeField]
		[Tooltip("This text is only displayed when split-axis fields have been disabled and the user clicks on the full-axis field. Button/key/D-pad input cannot be assigned to a full-axis field.\n{0} = Action Name")]
		private string _mouseElementAssignmentPollingWindowMessage_fullAxisFieldOnly = "Move an axis to assign it to {0}.";

		// Token: 0x04001DB4 RID: 7604
		[SerializeField]
		private string _elementAssignmentConflictWindowMessage = "Assignment Conflict";

		// Token: 0x04001DB5 RID: 7605
		[SerializeField]
		[Tooltip("{0} = Element Name")]
		private string _elementAlreadyInUseBlocked = "{0} is already in use cannot be replaced.";

		// Token: 0x04001DB6 RID: 7606
		[SerializeField]
		[Tooltip("{0} = Element Name")]
		private string _elementAlreadyInUseCanReplace = "{0} is already in use. Do you want to replace it?";

		// Token: 0x04001DB7 RID: 7607
		[SerializeField]
		[Tooltip("{0} = Element Name")]
		private string _elementAlreadyInUseCanReplace_conflictAllowed = "{0} is already in use. Do you want to replace it? You may also choose to add the assignment anyway.";

		// Token: 0x04001DB8 RID: 7608
		[SerializeField]
		private string _mouseAssignmentConflictWindowTitle = "Mouse Assignment";

		// Token: 0x04001DB9 RID: 7609
		[SerializeField]
		[Tooltip("{0} = Other Player Name\n{1} = This Player Name")]
		private string _mouseAssignmentConflictWindowMessage = "The mouse is already assigned to {0}. Do you want to assign the mouse to {1} instead?";

		// Token: 0x04001DBA RID: 7610
		[SerializeField]
		private string _calibrateControllerWindowTitle = "Calibrate Controller";

		// Token: 0x04001DBB RID: 7611
		[SerializeField]
		private string _calibrateAxisStep1WindowTitle = "Calibrate Zero";

		// Token: 0x04001DBC RID: 7612
		[SerializeField]
		[Tooltip("{0} = Axis Name")]
		private string _calibrateAxisStep1WindowMessage = "Center or zero {0} and press any button or wait for the timer to finish.";

		// Token: 0x04001DBD RID: 7613
		[SerializeField]
		private string _calibrateAxisStep2WindowTitle = "Calibrate Range";

		// Token: 0x04001DBE RID: 7614
		[SerializeField]
		[Tooltip("{0} = Axis Name")]
		private string _calibrateAxisStep2WindowMessage = "Move {0} through its entire range then press any button or wait for the timer to finish.";

		// Token: 0x04001DBF RID: 7615
		[SerializeField]
		private string _inputBehaviorSettingsWindowTitle = "Sensitivity Settings";

		// Token: 0x04001DC0 RID: 7616
		[SerializeField]
		private string _restoreDefaultsWindowTitle = "Restore Defaults";

		// Token: 0x04001DC1 RID: 7617
		[SerializeField]
		[Tooltip("Message for a single player game.")]
		private string _restoreDefaultsWindowMessage_onePlayer = "This will restore the default input configuration. Are you sure you want to do this?";

		// Token: 0x04001DC2 RID: 7618
		[SerializeField]
		[Tooltip("Message for a multi-player game.")]
		private string _restoreDefaultsWindowMessage_multiPlayer = "This will restore the default input configuration for all players. Are you sure you want to do this?";

		// Token: 0x04001DC3 RID: 7619
		[SerializeField]
		private string _actionColumnLabel = "Actions";

		// Token: 0x04001DC4 RID: 7620
		[SerializeField]
		private string _keyboardColumnLabel = "Keyboard";

		// Token: 0x04001DC5 RID: 7621
		[SerializeField]
		private string _mouseColumnLabel = "Mouse";

		// Token: 0x04001DC6 RID: 7622
		[SerializeField]
		private string _controllerColumnLabel = "Controller";

		// Token: 0x04001DC7 RID: 7623
		[SerializeField]
		private string _removeControllerButtonLabel = "Remove";

		// Token: 0x04001DC8 RID: 7624
		[SerializeField]
		private string _calibrateControllerButtonLabel = "Calibrate";

		// Token: 0x04001DC9 RID: 7625
		[SerializeField]
		private string _assignControllerButtonLabel = "Assign Controller";

		// Token: 0x04001DCA RID: 7626
		[SerializeField]
		private string _inputBehaviorSettingsButtonLabel = "Sensitivity";

		// Token: 0x04001DCB RID: 7627
		[SerializeField]
		private string _doneButtonLabel = "Done";

		// Token: 0x04001DCC RID: 7628
		[SerializeField]
		private string _restoreDefaultsButtonLabel = "Restore Defaults";

		// Token: 0x04001DCD RID: 7629
		[SerializeField]
		private string _playersGroupLabel = "Players:";

		// Token: 0x04001DCE RID: 7630
		[SerializeField]
		private string _controllerSettingsGroupLabel = "Controller:";

		// Token: 0x04001DCF RID: 7631
		[SerializeField]
		private string _assignedControllersGroupLabel = "Assigned Controllers:";

		// Token: 0x04001DD0 RID: 7632
		[SerializeField]
		private string _settingsGroupLabel = "Settings:";

		// Token: 0x04001DD1 RID: 7633
		[SerializeField]
		private string _mapCategoriesGroupLabel = "Categories:";

		// Token: 0x04001DD2 RID: 7634
		[SerializeField]
		private string _calibrateWindow_deadZoneSliderLabel = "Dead Zone:";

		// Token: 0x04001DD3 RID: 7635
		[SerializeField]
		private string _calibrateWindow_zeroSliderLabel = "Zero:";

		// Token: 0x04001DD4 RID: 7636
		[SerializeField]
		private string _calibrateWindow_sensitivitySliderLabel = "Sensitivity:";

		// Token: 0x04001DD5 RID: 7637
		[SerializeField]
		private string _calibrateWindow_invertToggleLabel = "Invert";

		// Token: 0x04001DD6 RID: 7638
		[SerializeField]
		private string _calibrateWindow_calibrateButtonLabel = "Calibrate";

		// Token: 0x04001DD7 RID: 7639
		[SerializeField]
		private LanguageData.CustomEntry[] _customEntries;

		// Token: 0x04001DD8 RID: 7640
		private bool _initialized;

		// Token: 0x04001DD9 RID: 7641
		private Dictionary<string, string> customDict;

		// Token: 0x02000551 RID: 1361
		[Serializable]
		private class CustomEntry
		{
			// Token: 0x06001CA0 RID: 7328 RVA: 0x000AA3A2 File Offset: 0x000A87A2
			public CustomEntry()
			{
			}

			// Token: 0x06001CA1 RID: 7329 RVA: 0x000AA3AA File Offset: 0x000A87AA
			public CustomEntry(string key, string value)
			{
				this.key = key;
				this.value = value;
			}

			// Token: 0x06001CA2 RID: 7330 RVA: 0x000AA3C0 File Offset: 0x000A87C0
			public static Dictionary<string, string> ToDictionary(LanguageData.CustomEntry[] array)
			{
				if (array == null)
				{
					return new Dictionary<string, string>();
				}
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] != null)
					{
						if (!string.IsNullOrEmpty(array[i].key) && !string.IsNullOrEmpty(array[i].value))
						{
							if (dictionary.ContainsKey(array[i].key))
							{
								UnityEngine.Debug.LogError("Key \"" + array[i].key + "\" is already in dictionary!");
							}
							else
							{
								dictionary.Add(array[i].key, array[i].value);
							}
						}
					}
				}
				return dictionary;
			}

			// Token: 0x04001DDA RID: 7642
			public string key;

			// Token: 0x04001DDB RID: 7643
			public string value;
		}
	}
}
