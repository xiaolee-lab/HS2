using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x0200054A RID: 1354
	[AddComponentMenu("")]
	public class InputBehaviorWindow : Window
	{
		// Token: 0x06001C3F RID: 7231 RVA: 0x000A9564 File Offset: 0x000A7964
		public override void Initialize(int id, Func<int, bool> isFocusedCallback)
		{
			if (this.spawnTransform == null || this.doneButton == null || this.cancelButton == null || this.defaultButton == null || this.uiControlSetPrefab == null || this.uiSliderControlPrefab == null || this.doneButtonLabel == null || this.cancelButtonLabel == null || this.defaultButtonLabel == null)
			{
				UnityEngine.Debug.LogError("Rewired Control Mapper: All inspector values must be assigned!");
				return;
			}
			this.inputBehaviorInfo = new List<InputBehaviorWindow.InputBehaviorInfo>();
			this.buttonCallbacks = new Dictionary<int, Action<int>>();
			this.doneButtonLabel.text = ControlMapper.GetLanguage().done;
			this.cancelButtonLabel.text = ControlMapper.GetLanguage().cancel;
			this.defaultButtonLabel.text = ControlMapper.GetLanguage().default_;
			base.Initialize(id, isFocusedCallback);
		}

		// Token: 0x06001C40 RID: 7232 RVA: 0x000A9674 File Offset: 0x000A7A74
		public void SetData(int playerId, ControlMapper.InputBehaviorSettings[] data)
		{
			if (!base.initialized)
			{
				return;
			}
			this.playerId = playerId;
			foreach (ControlMapper.InputBehaviorSettings inputBehaviorSettings in data)
			{
				if (inputBehaviorSettings != null && inputBehaviorSettings.isValid)
				{
					InputBehavior inputBehavior = this.GetInputBehavior(inputBehaviorSettings.inputBehaviorId);
					if (inputBehavior != null)
					{
						UIControlSet uicontrolSet = this.CreateControlSet();
						Dictionary<int, InputBehaviorWindow.PropertyType> dictionary = new Dictionary<int, InputBehaviorWindow.PropertyType>();
						string customEntry = ControlMapper.GetLanguage().GetCustomEntry(inputBehaviorSettings.labelLanguageKey);
						if (!string.IsNullOrEmpty(customEntry))
						{
							uicontrolSet.SetTitle(customEntry);
						}
						else
						{
							uicontrolSet.SetTitle(inputBehavior.name);
						}
						if (inputBehaviorSettings.showJoystickAxisSensitivity)
						{
							UISliderControl uisliderControl = this.CreateSlider(uicontrolSet, inputBehavior.id, null, ControlMapper.GetLanguage().GetCustomEntry(inputBehaviorSettings.joystickAxisSensitivityLabelLanguageKey), inputBehaviorSettings.joystickAxisSensitivityIcon, inputBehaviorSettings.joystickAxisSensitivityMin, inputBehaviorSettings.joystickAxisSensitivityMax, new Action<int, int, float>(this.JoystickAxisSensitivityValueChanged), new Action<int, int>(this.JoystickAxisSensitivityCanceled));
							uisliderControl.slider.value = Mathf.Clamp(inputBehavior.joystickAxisSensitivity, inputBehaviorSettings.joystickAxisSensitivityMin, inputBehaviorSettings.joystickAxisSensitivityMax);
							dictionary.Add(uisliderControl.id, InputBehaviorWindow.PropertyType.JoystickAxisSensitivity);
						}
						if (inputBehaviorSettings.showMouseXYAxisSensitivity)
						{
							UISliderControl uisliderControl2 = this.CreateSlider(uicontrolSet, inputBehavior.id, null, ControlMapper.GetLanguage().GetCustomEntry(inputBehaviorSettings.mouseXYAxisSensitivityLabelLanguageKey), inputBehaviorSettings.mouseXYAxisSensitivityIcon, inputBehaviorSettings.mouseXYAxisSensitivityMin, inputBehaviorSettings.mouseXYAxisSensitivityMax, new Action<int, int, float>(this.MouseXYAxisSensitivityValueChanged), new Action<int, int>(this.MouseXYAxisSensitivityCanceled));
							uisliderControl2.slider.value = Mathf.Clamp(inputBehavior.mouseXYAxisSensitivity, inputBehaviorSettings.mouseXYAxisSensitivityMin, inputBehaviorSettings.mouseXYAxisSensitivityMax);
							dictionary.Add(uisliderControl2.id, InputBehaviorWindow.PropertyType.MouseXYAxisSensitivity);
						}
						this.inputBehaviorInfo.Add(new InputBehaviorWindow.InputBehaviorInfo(inputBehavior, uicontrolSet, dictionary));
					}
				}
			}
			base.defaultUIElement = this.doneButton.gameObject;
		}

		// Token: 0x06001C41 RID: 7233 RVA: 0x000A9854 File Offset: 0x000A7C54
		public void SetButtonCallback(InputBehaviorWindow.ButtonIdentifier buttonIdentifier, Action<int> callback)
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

		// Token: 0x06001C42 RID: 7234 RVA: 0x000A98A4 File Offset: 0x000A7CA4
		public override void Cancel()
		{
			if (!base.initialized)
			{
				return;
			}
			foreach (InputBehaviorWindow.InputBehaviorInfo inputBehaviorInfo in this.inputBehaviorInfo)
			{
				inputBehaviorInfo.RestorePreviousData();
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

		// Token: 0x06001C43 RID: 7235 RVA: 0x000A9944 File Offset: 0x000A7D44
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

		// Token: 0x06001C44 RID: 7236 RVA: 0x000A997D File Offset: 0x000A7D7D
		public void OnCancel()
		{
			this.Cancel();
		}

		// Token: 0x06001C45 RID: 7237 RVA: 0x000A9988 File Offset: 0x000A7D88
		public void OnRestoreDefault()
		{
			if (!base.initialized)
			{
				return;
			}
			foreach (InputBehaviorWindow.InputBehaviorInfo inputBehaviorInfo in this.inputBehaviorInfo)
			{
				inputBehaviorInfo.RestoreDefaultData();
			}
		}

		// Token: 0x06001C46 RID: 7238 RVA: 0x000A99F0 File Offset: 0x000A7DF0
		private void JoystickAxisSensitivityValueChanged(int inputBehaviorId, int controlId, float value)
		{
			this.GetInputBehavior(inputBehaviorId).joystickAxisSensitivity = value;
		}

		// Token: 0x06001C47 RID: 7239 RVA: 0x000A99FF File Offset: 0x000A7DFF
		private void MouseXYAxisSensitivityValueChanged(int inputBehaviorId, int controlId, float value)
		{
			this.GetInputBehavior(inputBehaviorId).mouseXYAxisSensitivity = value;
		}

		// Token: 0x06001C48 RID: 7240 RVA: 0x000A9A10 File Offset: 0x000A7E10
		private void JoystickAxisSensitivityCanceled(int inputBehaviorId, int controlId)
		{
			InputBehaviorWindow.InputBehaviorInfo inputBehaviorInfo = this.GetInputBehaviorInfo(inputBehaviorId);
			if (inputBehaviorInfo == null)
			{
				return;
			}
			inputBehaviorInfo.RestoreData(InputBehaviorWindow.PropertyType.JoystickAxisSensitivity, controlId);
		}

		// Token: 0x06001C49 RID: 7241 RVA: 0x000A9A34 File Offset: 0x000A7E34
		private void MouseXYAxisSensitivityCanceled(int inputBehaviorId, int controlId)
		{
			InputBehaviorWindow.InputBehaviorInfo inputBehaviorInfo = this.GetInputBehaviorInfo(inputBehaviorId);
			if (inputBehaviorInfo == null)
			{
				return;
			}
			inputBehaviorInfo.RestoreData(InputBehaviorWindow.PropertyType.MouseXYAxisSensitivity, controlId);
		}

		// Token: 0x06001C4A RID: 7242 RVA: 0x000A9A58 File Offset: 0x000A7E58
		public override void TakeInputFocus()
		{
			base.TakeInputFocus();
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x000A9A60 File Offset: 0x000A7E60
		private UIControlSet CreateControlSet()
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.uiControlSetPrefab);
			gameObject.transform.SetParent(this.spawnTransform, false);
			return gameObject.GetComponent<UIControlSet>();
		}

		// Token: 0x06001C4C RID: 7244 RVA: 0x000A9A94 File Offset: 0x000A7E94
		private UISliderControl CreateSlider(UIControlSet set, int inputBehaviorId, string defaultTitle, string overrideTitle, Sprite icon, float minValue, float maxValue, Action<int, int, float> valueChangedCallback, Action<int, int> cancelCallback)
		{
			UISliderControl uisliderControl = set.CreateSlider(this.uiSliderControlPrefab, icon, minValue, maxValue, delegate(int cId, float value)
			{
				valueChangedCallback(inputBehaviorId, cId, value);
			}, delegate(int cId)
			{
				cancelCallback(inputBehaviorId, cId);
			});
			string text = (!string.IsNullOrEmpty(overrideTitle)) ? overrideTitle : defaultTitle;
			if (!string.IsNullOrEmpty(text))
			{
				uisliderControl.showTitle = true;
				uisliderControl.title.text = text;
			}
			else
			{
				uisliderControl.showTitle = false;
			}
			uisliderControl.showIcon = (icon != null);
			return uisliderControl;
		}

		// Token: 0x06001C4D RID: 7245 RVA: 0x000A9B37 File Offset: 0x000A7F37
		private InputBehavior GetInputBehavior(int id)
		{
			return ReInput.mapping.GetInputBehavior(this.playerId, id);
		}

		// Token: 0x06001C4E RID: 7246 RVA: 0x000A9B4C File Offset: 0x000A7F4C
		private InputBehaviorWindow.InputBehaviorInfo GetInputBehaviorInfo(int inputBehaviorId)
		{
			int count = this.inputBehaviorInfo.Count;
			for (int i = 0; i < count; i++)
			{
				if (this.inputBehaviorInfo[i].inputBehavior.id == inputBehaviorId)
				{
					return this.inputBehaviorInfo[i];
				}
			}
			return null;
		}

		// Token: 0x04001D7E RID: 7550
		private const float minSensitivity = 0.1f;

		// Token: 0x04001D7F RID: 7551
		[SerializeField]
		private RectTransform spawnTransform;

		// Token: 0x04001D80 RID: 7552
		[SerializeField]
		private Button doneButton;

		// Token: 0x04001D81 RID: 7553
		[SerializeField]
		private Button cancelButton;

		// Token: 0x04001D82 RID: 7554
		[SerializeField]
		private Button defaultButton;

		// Token: 0x04001D83 RID: 7555
		[SerializeField]
		private Text doneButtonLabel;

		// Token: 0x04001D84 RID: 7556
		[SerializeField]
		private Text cancelButtonLabel;

		// Token: 0x04001D85 RID: 7557
		[SerializeField]
		private Text defaultButtonLabel;

		// Token: 0x04001D86 RID: 7558
		[SerializeField]
		private GameObject uiControlSetPrefab;

		// Token: 0x04001D87 RID: 7559
		[SerializeField]
		private GameObject uiSliderControlPrefab;

		// Token: 0x04001D88 RID: 7560
		private List<InputBehaviorWindow.InputBehaviorInfo> inputBehaviorInfo;

		// Token: 0x04001D89 RID: 7561
		private Dictionary<int, Action<int>> buttonCallbacks;

		// Token: 0x04001D8A RID: 7562
		private int playerId;

		// Token: 0x0200054B RID: 1355
		private class InputBehaviorInfo
		{
			// Token: 0x06001C4F RID: 7247 RVA: 0x000A9BA6 File Offset: 0x000A7FA6
			public InputBehaviorInfo(InputBehavior inputBehavior, UIControlSet controlSet, Dictionary<int, InputBehaviorWindow.PropertyType> idToProperty)
			{
				this._inputBehavior = inputBehavior;
				this._controlSet = controlSet;
				this.idToProperty = idToProperty;
				this.copyOfOriginal = new InputBehavior(inputBehavior);
			}

			// Token: 0x1700024B RID: 587
			// (get) Token: 0x06001C50 RID: 7248 RVA: 0x000A9BCF File Offset: 0x000A7FCF
			public InputBehavior inputBehavior
			{
				get
				{
					return this._inputBehavior;
				}
			}

			// Token: 0x1700024C RID: 588
			// (get) Token: 0x06001C51 RID: 7249 RVA: 0x000A9BD7 File Offset: 0x000A7FD7
			public UIControlSet controlSet
			{
				get
				{
					return this._controlSet;
				}
			}

			// Token: 0x06001C52 RID: 7250 RVA: 0x000A9BDF File Offset: 0x000A7FDF
			public void RestorePreviousData()
			{
				this._inputBehavior.ImportData(this.copyOfOriginal);
			}

			// Token: 0x06001C53 RID: 7251 RVA: 0x000A9BF3 File Offset: 0x000A7FF3
			public void RestoreDefaultData()
			{
				this._inputBehavior.Reset();
				this.RefreshControls();
			}

			// Token: 0x06001C54 RID: 7252 RVA: 0x000A9C08 File Offset: 0x000A8008
			public void RestoreData(InputBehaviorWindow.PropertyType propertyType, int controlId)
			{
				if (propertyType != InputBehaviorWindow.PropertyType.JoystickAxisSensitivity)
				{
					if (propertyType == InputBehaviorWindow.PropertyType.MouseXYAxisSensitivity)
					{
						float mouseXYAxisSensitivity = this.copyOfOriginal.mouseXYAxisSensitivity;
						this._inputBehavior.mouseXYAxisSensitivity = mouseXYAxisSensitivity;
						UISliderControl control = this._controlSet.GetControl<UISliderControl>(controlId);
						if (control != null)
						{
							control.slider.value = mouseXYAxisSensitivity;
						}
					}
				}
				else
				{
					float joystickAxisSensitivity = this.copyOfOriginal.joystickAxisSensitivity;
					this._inputBehavior.joystickAxisSensitivity = joystickAxisSensitivity;
					UISliderControl control2 = this._controlSet.GetControl<UISliderControl>(controlId);
					if (control2 != null)
					{
						control2.slider.value = joystickAxisSensitivity;
					}
				}
			}

			// Token: 0x06001C55 RID: 7253 RVA: 0x000A9CAC File Offset: 0x000A80AC
			public void RefreshControls()
			{
				if (this._controlSet == null)
				{
					return;
				}
				if (this.idToProperty == null)
				{
					return;
				}
				foreach (KeyValuePair<int, InputBehaviorWindow.PropertyType> keyValuePair in this.idToProperty)
				{
					UISliderControl control = this._controlSet.GetControl<UISliderControl>(keyValuePair.Key);
					if (!(control == null))
					{
						InputBehaviorWindow.PropertyType value = keyValuePair.Value;
						if (value != InputBehaviorWindow.PropertyType.JoystickAxisSensitivity)
						{
							if (value == InputBehaviorWindow.PropertyType.MouseXYAxisSensitivity)
							{
								control.slider.value = this._inputBehavior.mouseXYAxisSensitivity;
							}
						}
						else
						{
							control.slider.value = this._inputBehavior.joystickAxisSensitivity;
						}
					}
				}
			}

			// Token: 0x04001D8B RID: 7563
			private InputBehavior _inputBehavior;

			// Token: 0x04001D8C RID: 7564
			private UIControlSet _controlSet;

			// Token: 0x04001D8D RID: 7565
			private Dictionary<int, InputBehaviorWindow.PropertyType> idToProperty;

			// Token: 0x04001D8E RID: 7566
			private InputBehavior copyOfOriginal;
		}

		// Token: 0x0200054C RID: 1356
		public enum ButtonIdentifier
		{
			// Token: 0x04001D90 RID: 7568
			Done,
			// Token: 0x04001D91 RID: 7569
			Cancel,
			// Token: 0x04001D92 RID: 7570
			Default
		}

		// Token: 0x0200054D RID: 1357
		private enum PropertyType
		{
			// Token: 0x04001D94 RID: 7572
			JoystickAxisSensitivity,
			// Token: 0x04001D95 RID: 7573
			MouseXYAxisSensitivity
		}
	}
}
