using System;
using System.Collections.Generic;
using Rewired.Integration.UnityUI;
using Rewired.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x02000528 RID: 1320
	[AddComponentMenu("")]
	public class CalibrationWindow : Window
	{
		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06001962 RID: 6498 RVA: 0x0009D320 File Offset: 0x0009B720
		private bool axisSelected
		{
			get
			{
				return this.joystick != null && this.selectedAxis >= 0 && this.selectedAxis < this.joystick.calibrationMap.axisCount;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06001963 RID: 6499 RVA: 0x0009D359 File Offset: 0x0009B759
		private AxisCalibration axisCalibration
		{
			get
			{
				if (!this.axisSelected)
				{
					return null;
				}
				return this.joystick.calibrationMap.GetAxis(this.selectedAxis);
			}
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x0009D380 File Offset: 0x0009B780
		public override void Initialize(int id, Func<int, bool> isFocusedCallback)
		{
			if (this.rightContentContainer == null || this.valueDisplayGroup == null || this.calibratedValueMarker == null || this.rawValueMarker == null || this.calibratedZeroMarker == null || this.deadzoneArea == null || this.deadzoneSlider == null || this.sensitivitySlider == null || this.zeroSlider == null || this.invertToggle == null || this.axisScrollAreaContent == null || this.doneButton == null || this.calibrateButton == null || this.axisButtonPrefab == null || this.doneButtonLabel == null || this.cancelButtonLabel == null || this.defaultButtonLabel == null || this.deadzoneSliderLabel == null || this.zeroSliderLabel == null || this.sensitivitySliderLabel == null || this.invertToggleLabel == null || this.calibrateButtonLabel == null)
			{
				UnityEngine.Debug.LogError("Rewired Control Mapper: All inspector values must be assigned!");
				return;
			}
			this.axisButtons = new List<Button>();
			this.buttonCallbacks = new Dictionary<int, Action<int>>();
			this.doneButtonLabel.text = ControlMapper.GetLanguage().done;
			this.cancelButtonLabel.text = ControlMapper.GetLanguage().cancel;
			this.defaultButtonLabel.text = ControlMapper.GetLanguage().default_;
			this.deadzoneSliderLabel.text = ControlMapper.GetLanguage().calibrateWindow_deadZoneSliderLabel;
			this.zeroSliderLabel.text = ControlMapper.GetLanguage().calibrateWindow_zeroSliderLabel;
			this.sensitivitySliderLabel.text = ControlMapper.GetLanguage().calibrateWindow_sensitivitySliderLabel;
			this.invertToggleLabel.text = ControlMapper.GetLanguage().calibrateWindow_invertToggleLabel;
			this.calibrateButtonLabel.text = ControlMapper.GetLanguage().calibrateWindow_calibrateButtonLabel;
			base.Initialize(id, isFocusedCallback);
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x0009D5D4 File Offset: 0x0009B9D4
		public void SetJoystick(int playerId, Joystick joystick)
		{
			if (!base.initialized)
			{
				return;
			}
			this.playerId = playerId;
			this.joystick = joystick;
			if (joystick == null)
			{
				UnityEngine.Debug.LogError("Rewired Control Mapper: Joystick cannot be null!");
				return;
			}
			float num = 0f;
			for (int i = 0; i < joystick.axisCount; i++)
			{
				int index = i;
				GameObject gameObject = UITools.InstantiateGUIObject<Button>(this.axisButtonPrefab, this.axisScrollAreaContent, "Axis" + i);
				Button button = gameObject.GetComponent<Button>();
				button.onClick.AddListener(delegate()
				{
					this.OnAxisSelected(index, button);
				});
				Text componentInSelfOrChildren = UnityTools.GetComponentInSelfOrChildren<Text>(gameObject);
				if (componentInSelfOrChildren != null)
				{
					componentInSelfOrChildren.text = joystick.AxisElementIdentifiers[i].name;
				}
				if (num == 0f)
				{
					num = UnityTools.GetComponentInSelfOrChildren<LayoutElement>(gameObject).minHeight;
				}
				this.axisButtons.Add(button);
			}
			float spacing = this.axisScrollAreaContent.GetComponent<VerticalLayoutGroup>().spacing;
			this.axisScrollAreaContent.sizeDelta = new Vector2(this.axisScrollAreaContent.sizeDelta.x, Mathf.Max((float)joystick.axisCount * (num + spacing) - spacing, this.axisScrollAreaContent.sizeDelta.y));
			this.origCalibrationData = joystick.calibrationMap.ToXmlString();
			this.displayAreaWidth = this.rightContentContainer.sizeDelta.x;
			this.rewiredStandaloneInputModule = base.gameObject.transform.root.GetComponentInChildren<RewiredStandaloneInputModule>();
			if (this.rewiredStandaloneInputModule != null)
			{
				this.menuHorizActionId = ReInput.mapping.GetActionId(this.rewiredStandaloneInputModule.horizontalAxis);
				this.menuVertActionId = ReInput.mapping.GetActionId(this.rewiredStandaloneInputModule.verticalAxis);
			}
			if (joystick.axisCount > 0)
			{
				this.SelectAxis(0);
			}
			base.defaultUIElement = this.doneButton.gameObject;
			this.RefreshControls();
			this.Redraw();
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x0009D7F8 File Offset: 0x0009BBF8
		public void SetButtonCallback(CalibrationWindow.ButtonIdentifier buttonIdentifier, Action<int> callback)
		{
			if (!base.initialized)
			{
				return;
			}
			if (callback == null)
			{
				return;
			}
			if (this.buttonCallbacks.ContainsKey((int)buttonIdentifier))
			{
				this.buttonCallbacks[(int)buttonIdentifier] = callback;
			}
			else
			{
				this.buttonCallbacks.Add((int)buttonIdentifier, callback);
			}
		}

		// Token: 0x06001967 RID: 6503 RVA: 0x0009D848 File Offset: 0x0009BC48
		public override void Cancel()
		{
			if (!base.initialized)
			{
				return;
			}
			if (this.joystick != null)
			{
				this.joystick.ImportCalibrationMapFromXmlString(this.origCalibrationData);
			}
			Action<int> action;
			if (!this.buttonCallbacks.TryGetValue(1, out action))
			{
				if (this.cancelCallback != null)
				{
					this.cancelCallback();
				}
				return;
			}
			action(base.id);
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x0009D8B4 File Offset: 0x0009BCB4
		protected override void Update()
		{
			if (!base.initialized)
			{
				return;
			}
			base.Update();
			this.UpdateDisplay();
		}

		// Token: 0x06001969 RID: 6505 RVA: 0x0009D8D0 File Offset: 0x0009BCD0
		public void OnDone()
		{
			if (!base.initialized)
			{
				return;
			}
			Action<int> action;
			if (!this.buttonCallbacks.TryGetValue(0, out action))
			{
				return;
			}
			action(base.id);
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x0009D909 File Offset: 0x0009BD09
		public void OnCancel()
		{
			this.Cancel();
		}

		// Token: 0x0600196B RID: 6507 RVA: 0x0009D911 File Offset: 0x0009BD11
		public void OnRestoreDefault()
		{
			if (!base.initialized)
			{
				return;
			}
			if (this.joystick == null)
			{
				return;
			}
			this.joystick.calibrationMap.Reset();
			this.RefreshControls();
			this.Redraw();
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x0009D948 File Offset: 0x0009BD48
		public void OnCalibrate()
		{
			if (!base.initialized)
			{
				return;
			}
			Action<int> action;
			if (!this.buttonCallbacks.TryGetValue(3, out action))
			{
				return;
			}
			action(this.selectedAxis);
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x0009D981 File Offset: 0x0009BD81
		public void OnInvert(bool state)
		{
			if (!base.initialized)
			{
				return;
			}
			if (!this.axisSelected)
			{
				return;
			}
			this.axisCalibration.invert = state;
		}

		// Token: 0x0600196E RID: 6510 RVA: 0x0009D9A7 File Offset: 0x0009BDA7
		public void OnZeroValueChange(float value)
		{
			if (!base.initialized)
			{
				return;
			}
			if (!this.axisSelected)
			{
				return;
			}
			this.axisCalibration.calibratedZero = value;
			this.RedrawCalibratedZero();
		}

		// Token: 0x0600196F RID: 6511 RVA: 0x0009D9D3 File Offset: 0x0009BDD3
		public void OnZeroCancel()
		{
			if (!base.initialized)
			{
				return;
			}
			if (!this.axisSelected)
			{
				return;
			}
			this.axisCalibration.calibratedZero = this.origSelectedAxisCalibrationData.zero;
			this.RedrawCalibratedZero();
			this.RefreshControls();
		}

		// Token: 0x06001970 RID: 6512 RVA: 0x0009DA10 File Offset: 0x0009BE10
		public void OnDeadzoneValueChange(float value)
		{
			if (!base.initialized)
			{
				return;
			}
			if (!this.axisSelected)
			{
				return;
			}
			this.axisCalibration.deadZone = Mathf.Clamp(value, 0f, 0.8f);
			if (value > 0.8f)
			{
				this.deadzoneSlider.value = 0.8f;
			}
			this.RedrawDeadzone();
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x0009DA71 File Offset: 0x0009BE71
		public void OnDeadzoneCancel()
		{
			if (!base.initialized)
			{
				return;
			}
			if (!this.axisSelected)
			{
				return;
			}
			this.axisCalibration.deadZone = this.origSelectedAxisCalibrationData.deadZone;
			this.RedrawDeadzone();
			this.RefreshControls();
		}

		// Token: 0x06001972 RID: 6514 RVA: 0x0009DAB0 File Offset: 0x0009BEB0
		public void OnSensitivityValueChange(float value)
		{
			if (!base.initialized)
			{
				return;
			}
			if (!this.axisSelected)
			{
				return;
			}
			this.axisCalibration.sensitivity = Mathf.Clamp(value, this.minSensitivity, float.PositiveInfinity);
			if (value < this.minSensitivity)
			{
				this.sensitivitySlider.value = this.minSensitivity;
			}
		}

		// Token: 0x06001973 RID: 6515 RVA: 0x0009DB0E File Offset: 0x0009BF0E
		public void OnSensitivityCancel(float value)
		{
			if (!base.initialized)
			{
				return;
			}
			if (!this.axisSelected)
			{
				return;
			}
			this.axisCalibration.sensitivity = this.origSelectedAxisCalibrationData.sensitivity;
			this.RefreshControls();
		}

		// Token: 0x06001974 RID: 6516 RVA: 0x0009DB44 File Offset: 0x0009BF44
		public void OnAxisScrollRectScroll(Vector2 pos)
		{
			if (!base.initialized)
			{
				return;
			}
		}

		// Token: 0x06001975 RID: 6517 RVA: 0x0009DB52 File Offset: 0x0009BF52
		private void OnAxisSelected(int axisIndex, Button button)
		{
			if (!base.initialized)
			{
				return;
			}
			if (this.joystick == null)
			{
				return;
			}
			this.SelectAxis(axisIndex);
			this.RefreshControls();
			this.Redraw();
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x0009DB7F File Offset: 0x0009BF7F
		private void UpdateDisplay()
		{
			this.RedrawValueMarkers();
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x0009DB87 File Offset: 0x0009BF87
		private void Redraw()
		{
			this.RedrawCalibratedZero();
			this.RedrawValueMarkers();
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x0009DB98 File Offset: 0x0009BF98
		private void RefreshControls()
		{
			if (!this.axisSelected)
			{
				this.deadzoneSlider.value = 0f;
				this.zeroSlider.value = 0f;
				this.sensitivitySlider.value = 0f;
				this.invertToggle.isOn = false;
			}
			else
			{
				this.deadzoneSlider.value = this.axisCalibration.deadZone;
				this.zeroSlider.value = this.axisCalibration.calibratedZero;
				this.sensitivitySlider.value = this.axisCalibration.sensitivity;
				this.invertToggle.isOn = this.axisCalibration.invert;
			}
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x0009DC4C File Offset: 0x0009C04C
		private void RedrawDeadzone()
		{
			if (!this.axisSelected)
			{
				return;
			}
			float x = this.displayAreaWidth * this.axisCalibration.deadZone;
			this.deadzoneArea.sizeDelta = new Vector2(x, this.deadzoneArea.sizeDelta.y);
			this.deadzoneArea.anchoredPosition = new Vector2(this.axisCalibration.calibratedZero * -this.deadzoneArea.parent.localPosition.x, this.deadzoneArea.anchoredPosition.y);
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x0009DCE4 File Offset: 0x0009C0E4
		private void RedrawCalibratedZero()
		{
			if (!this.axisSelected)
			{
				return;
			}
			this.calibratedZeroMarker.anchoredPosition = new Vector2(this.axisCalibration.calibratedZero * -this.deadzoneArea.parent.localPosition.x, this.calibratedZeroMarker.anchoredPosition.y);
			this.RedrawDeadzone();
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x0009DD4C File Offset: 0x0009C14C
		private void RedrawValueMarkers()
		{
			if (!this.axisSelected)
			{
				this.calibratedValueMarker.anchoredPosition = new Vector2(0f, this.calibratedValueMarker.anchoredPosition.y);
				this.rawValueMarker.anchoredPosition = new Vector2(0f, this.rawValueMarker.anchoredPosition.y);
				return;
			}
			float axis = this.joystick.GetAxis(this.selectedAxis);
			float num = Mathf.Clamp(this.joystick.GetAxisRaw(this.selectedAxis), -1f, 1f);
			this.calibratedValueMarker.anchoredPosition = new Vector2(this.displayAreaWidth * 0.5f * axis, this.calibratedValueMarker.anchoredPosition.y);
			this.rawValueMarker.anchoredPosition = new Vector2(this.displayAreaWidth * 0.5f * num, this.rawValueMarker.anchoredPosition.y);
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x0009DE4C File Offset: 0x0009C24C
		private void SelectAxis(int index)
		{
			if (index < 0 || index >= this.axisButtons.Count)
			{
				return;
			}
			if (this.axisButtons[index] == null)
			{
				return;
			}
			this.axisButtons[index].interactable = false;
			this.axisButtons[index].Select();
			for (int i = 0; i < this.axisButtons.Count; i++)
			{
				if (i != index)
				{
					this.axisButtons[i].interactable = true;
				}
			}
			this.selectedAxis = index;
			this.origSelectedAxisCalibrationData = this.axisCalibration.GetData();
			this.SetMinSensitivity();
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x0009DF05 File Offset: 0x0009C305
		public override void TakeInputFocus()
		{
			base.TakeInputFocus();
			if (this.selectedAxis >= 0)
			{
				this.SelectAxis(this.selectedAxis);
			}
			this.RefreshControls();
			this.Redraw();
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x0009DF34 File Offset: 0x0009C334
		private void SetMinSensitivity()
		{
			if (!this.axisSelected)
			{
				return;
			}
			this.minSensitivity = 0.1f;
			if (this.rewiredStandaloneInputModule != null)
			{
				if (this.IsMenuAxis(this.menuHorizActionId, this.selectedAxis))
				{
					this.GetAxisButtonDeadZone(this.playerId, this.menuHorizActionId, ref this.minSensitivity);
				}
				else if (this.IsMenuAxis(this.menuVertActionId, this.selectedAxis))
				{
					this.GetAxisButtonDeadZone(this.playerId, this.menuVertActionId, ref this.minSensitivity);
				}
			}
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x0009DFCC File Offset: 0x0009C3CC
		private bool IsMenuAxis(int actionId, int axisIndex)
		{
			if (this.rewiredStandaloneInputModule == null)
			{
				return false;
			}
			IList<Player> allPlayers = ReInput.players.AllPlayers;
			int count = allPlayers.Count;
			for (int i = 0; i < count; i++)
			{
				IList<JoystickMap> maps = allPlayers[i].controllers.maps.GetMaps<JoystickMap>(this.joystick.id);
				if (maps != null)
				{
					int count2 = maps.Count;
					for (int j = 0; j < count2; j++)
					{
						IList<ActionElementMap> axisMaps = maps[j].AxisMaps;
						if (axisMaps != null)
						{
							int count3 = axisMaps.Count;
							for (int k = 0; k < count3; k++)
							{
								ActionElementMap actionElementMap = axisMaps[k];
								if (actionElementMap.actionId == actionId && actionElementMap.elementIndex == axisIndex)
								{
									return true;
								}
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x0009E0C0 File Offset: 0x0009C4C0
		private void GetAxisButtonDeadZone(int playerId, int actionId, ref float value)
		{
			InputAction action = ReInput.mapping.GetAction(actionId);
			if (action == null)
			{
				return;
			}
			int behaviorId = action.behaviorId;
			InputBehavior inputBehavior = ReInput.mapping.GetInputBehavior(playerId, behaviorId);
			if (inputBehavior == null)
			{
				return;
			}
			value = inputBehavior.buttonDeadZone + 0.1f;
		}

		// Token: 0x04001C47 RID: 7239
		private const float minSensitivityOtherAxes = 0.1f;

		// Token: 0x04001C48 RID: 7240
		private const float maxDeadzone = 0.8f;

		// Token: 0x04001C49 RID: 7241
		[SerializeField]
		private RectTransform rightContentContainer;

		// Token: 0x04001C4A RID: 7242
		[SerializeField]
		private RectTransform valueDisplayGroup;

		// Token: 0x04001C4B RID: 7243
		[SerializeField]
		private RectTransform calibratedValueMarker;

		// Token: 0x04001C4C RID: 7244
		[SerializeField]
		private RectTransform rawValueMarker;

		// Token: 0x04001C4D RID: 7245
		[SerializeField]
		private RectTransform calibratedZeroMarker;

		// Token: 0x04001C4E RID: 7246
		[SerializeField]
		private RectTransform deadzoneArea;

		// Token: 0x04001C4F RID: 7247
		[SerializeField]
		private Slider deadzoneSlider;

		// Token: 0x04001C50 RID: 7248
		[SerializeField]
		private Slider zeroSlider;

		// Token: 0x04001C51 RID: 7249
		[SerializeField]
		private Slider sensitivitySlider;

		// Token: 0x04001C52 RID: 7250
		[SerializeField]
		private Toggle invertToggle;

		// Token: 0x04001C53 RID: 7251
		[SerializeField]
		private RectTransform axisScrollAreaContent;

		// Token: 0x04001C54 RID: 7252
		[SerializeField]
		private Button doneButton;

		// Token: 0x04001C55 RID: 7253
		[SerializeField]
		private Button calibrateButton;

		// Token: 0x04001C56 RID: 7254
		[SerializeField]
		private Text doneButtonLabel;

		// Token: 0x04001C57 RID: 7255
		[SerializeField]
		private Text cancelButtonLabel;

		// Token: 0x04001C58 RID: 7256
		[SerializeField]
		private Text defaultButtonLabel;

		// Token: 0x04001C59 RID: 7257
		[SerializeField]
		private Text deadzoneSliderLabel;

		// Token: 0x04001C5A RID: 7258
		[SerializeField]
		private Text zeroSliderLabel;

		// Token: 0x04001C5B RID: 7259
		[SerializeField]
		private Text sensitivitySliderLabel;

		// Token: 0x04001C5C RID: 7260
		[SerializeField]
		private Text invertToggleLabel;

		// Token: 0x04001C5D RID: 7261
		[SerializeField]
		private Text calibrateButtonLabel;

		// Token: 0x04001C5E RID: 7262
		[SerializeField]
		private GameObject axisButtonPrefab;

		// Token: 0x04001C5F RID: 7263
		private Joystick joystick;

		// Token: 0x04001C60 RID: 7264
		private string origCalibrationData;

		// Token: 0x04001C61 RID: 7265
		private int selectedAxis = -1;

		// Token: 0x04001C62 RID: 7266
		private AxisCalibrationData origSelectedAxisCalibrationData;

		// Token: 0x04001C63 RID: 7267
		private float displayAreaWidth;

		// Token: 0x04001C64 RID: 7268
		private List<Button> axisButtons;

		// Token: 0x04001C65 RID: 7269
		private Dictionary<int, Action<int>> buttonCallbacks;

		// Token: 0x04001C66 RID: 7270
		private int playerId;

		// Token: 0x04001C67 RID: 7271
		private RewiredStandaloneInputModule rewiredStandaloneInputModule;

		// Token: 0x04001C68 RID: 7272
		private int menuHorizActionId = -1;

		// Token: 0x04001C69 RID: 7273
		private int menuVertActionId = -1;

		// Token: 0x04001C6A RID: 7274
		private float minSensitivity;

		// Token: 0x02000529 RID: 1321
		public enum ButtonIdentifier
		{
			// Token: 0x04001C6C RID: 7276
			Done,
			// Token: 0x04001C6D RID: 7277
			Cancel,
			// Token: 0x04001C6E RID: 7278
			Default,
			// Token: 0x04001C6F RID: 7279
			Calibrate
		}
	}
}
