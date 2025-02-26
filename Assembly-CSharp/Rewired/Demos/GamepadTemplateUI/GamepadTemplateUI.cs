using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rewired.Demos.GamepadTemplateUI
{
	// Token: 0x02000518 RID: 1304
	public class GamepadTemplateUI : MonoBehaviour
	{
		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060018FE RID: 6398 RVA: 0x0009A699 File Offset: 0x00098A99
		private Player player
		{
			get
			{
				return ReInput.players.GetPlayer(this.playerId);
			}
		}

		// Token: 0x060018FF RID: 6399 RVA: 0x0009A6AC File Offset: 0x00098AAC
		private void Awake()
		{
			this._uiElementsArray = new GamepadTemplateUI.UIElement[]
			{
				new GamepadTemplateUI.UIElement(0, this.leftStickX),
				new GamepadTemplateUI.UIElement(1, this.leftStickY),
				new GamepadTemplateUI.UIElement(17, this.leftStickButton),
				new GamepadTemplateUI.UIElement(2, this.rightStickX),
				new GamepadTemplateUI.UIElement(3, this.rightStickY),
				new GamepadTemplateUI.UIElement(18, this.rightStickButton),
				new GamepadTemplateUI.UIElement(4, this.actionBottomRow1),
				new GamepadTemplateUI.UIElement(5, this.actionBottomRow2),
				new GamepadTemplateUI.UIElement(6, this.actionBottomRow3),
				new GamepadTemplateUI.UIElement(7, this.actionTopRow1),
				new GamepadTemplateUI.UIElement(8, this.actionTopRow2),
				new GamepadTemplateUI.UIElement(9, this.actionTopRow3),
				new GamepadTemplateUI.UIElement(14, this.center1),
				new GamepadTemplateUI.UIElement(15, this.center2),
				new GamepadTemplateUI.UIElement(16, this.center3),
				new GamepadTemplateUI.UIElement(19, this.dPadUp),
				new GamepadTemplateUI.UIElement(20, this.dPadRight),
				new GamepadTemplateUI.UIElement(21, this.dPadDown),
				new GamepadTemplateUI.UIElement(22, this.dPadLeft),
				new GamepadTemplateUI.UIElement(10, this.leftShoulder),
				new GamepadTemplateUI.UIElement(11, this.leftTrigger),
				new GamepadTemplateUI.UIElement(12, this.rightShoulder),
				new GamepadTemplateUI.UIElement(13, this.rightTrigger)
			};
			for (int i = 0; i < this._uiElementsArray.Length; i++)
			{
				this._uiElements.Add(this._uiElementsArray[i].id, this._uiElementsArray[i].element);
			}
			this._sticks = new GamepadTemplateUI.Stick[]
			{
				new GamepadTemplateUI.Stick(this.leftStick, 0, 1),
				new GamepadTemplateUI.Stick(this.rightStick, 2, 3)
			};
			ReInput.ControllerConnectedEvent += this.OnControllerConnected;
			ReInput.ControllerDisconnectedEvent += this.OnControllerDisconnected;
		}

		// Token: 0x06001900 RID: 6400 RVA: 0x0009A8C7 File Offset: 0x00098CC7
		private void Start()
		{
			if (!ReInput.isReady)
			{
				return;
			}
			this.DrawLabels();
		}

		// Token: 0x06001901 RID: 6401 RVA: 0x0009A8DA File Offset: 0x00098CDA
		private void OnDestroy()
		{
			ReInput.ControllerConnectedEvent -= this.OnControllerConnected;
			ReInput.ControllerDisconnectedEvent -= this.OnControllerDisconnected;
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x0009A8FE File Offset: 0x00098CFE
		private void Update()
		{
			if (!ReInput.isReady)
			{
				return;
			}
			this.DrawActiveElements();
		}

		// Token: 0x06001903 RID: 6403 RVA: 0x0009A914 File Offset: 0x00098D14
		private void DrawActiveElements()
		{
			for (int i = 0; i < this._uiElementsArray.Length; i++)
			{
				this._uiElementsArray[i].element.Deactivate();
			}
			for (int j = 0; j < this._sticks.Length; j++)
			{
				this._sticks[j].Reset();
			}
			IList<InputAction> actions = ReInput.mapping.Actions;
			for (int k = 0; k < actions.Count; k++)
			{
				this.ActivateElements(this.player, actions[k].id);
			}
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x0009A9AC File Offset: 0x00098DAC
		private void ActivateElements(Player player, int actionId)
		{
			float axis = player.GetAxis(actionId);
			if (axis == 0f)
			{
				return;
			}
			IList<InputActionSourceData> currentInputSources = player.GetCurrentInputSources(actionId);
			for (int i = 0; i < currentInputSources.Count; i++)
			{
				InputActionSourceData inputActionSourceData = currentInputSources[i];
				IGamepadTemplate template = inputActionSourceData.controller.GetTemplate<IGamepadTemplate>();
				if (template != null)
				{
					template.GetElementTargets(inputActionSourceData.actionElementMap, this._tempTargetList);
					for (int j = 0; j < this._tempTargetList.Count; j++)
					{
						ControllerTemplateElementTarget controllerTemplateElementTarget = this._tempTargetList[j];
						int id = controllerTemplateElementTarget.element.id;
						ControllerUIElement controllerUIElement = this._uiElements[id];
						if (controllerTemplateElementTarget.elementType == ControllerTemplateElementType.Axis)
						{
							controllerUIElement.Activate(axis);
						}
						else if (controllerTemplateElementTarget.elementType == ControllerTemplateElementType.Button && (player.GetButton(actionId) || player.GetNegativeButton(actionId)))
						{
							controllerUIElement.Activate(1f);
						}
						GamepadTemplateUI.Stick stick = this.GetStick(id);
						if (stick != null)
						{
							stick.SetAxisPosition(id, axis * 20f);
						}
					}
				}
			}
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x0009AAE0 File Offset: 0x00098EE0
		private void DrawLabels()
		{
			for (int i = 0; i < this._uiElementsArray.Length; i++)
			{
				this._uiElementsArray[i].element.ClearLabels();
			}
			IList<InputAction> actions = ReInput.mapping.Actions;
			for (int j = 0; j < actions.Count; j++)
			{
				this.DrawLabels(this.player, actions[j]);
			}
		}

		// Token: 0x06001906 RID: 6406 RVA: 0x0009AB50 File Offset: 0x00098F50
		private void DrawLabels(Player player, InputAction action)
		{
			Controller firstControllerWithTemplate = player.controllers.GetFirstControllerWithTemplate<IGamepadTemplate>();
			if (firstControllerWithTemplate == null)
			{
				return;
			}
			IGamepadTemplate template = firstControllerWithTemplate.GetTemplate<IGamepadTemplate>();
			ControllerMap map = player.controllers.maps.GetMap(firstControllerWithTemplate, "Default", "Default");
			if (map == null)
			{
				return;
			}
			for (int i = 0; i < this._uiElementsArray.Length; i++)
			{
				ControllerUIElement element = this._uiElementsArray[i].element;
				int id = this._uiElementsArray[i].id;
				IControllerTemplateElement element2 = template.GetElement(id);
				this.DrawLabel(element, action, map, template, element2);
			}
		}

		// Token: 0x06001907 RID: 6407 RVA: 0x0009ABE8 File Offset: 0x00098FE8
		private void DrawLabel(ControllerUIElement uiElement, InputAction action, ControllerMap controllerMap, IControllerTemplate template, IControllerTemplateElement element)
		{
			if (element.source == null)
			{
				return;
			}
			if (element.source.type == ControllerTemplateElementSourceType.Axis)
			{
				IControllerTemplateAxisSource controllerTemplateAxisSource = element.source as IControllerTemplateAxisSource;
				if (controllerTemplateAxisSource.splitAxis)
				{
					ActionElementMap firstElementMapWithElementTarget = controllerMap.GetFirstElementMapWithElementTarget(controllerTemplateAxisSource.positiveTarget, action.id, true);
					if (firstElementMapWithElementTarget != null)
					{
						uiElement.SetLabel(firstElementMapWithElementTarget.actionDescriptiveName, AxisRange.Positive);
					}
					firstElementMapWithElementTarget = controllerMap.GetFirstElementMapWithElementTarget(controllerTemplateAxisSource.negativeTarget, action.id, true);
					if (firstElementMapWithElementTarget != null)
					{
						uiElement.SetLabel(firstElementMapWithElementTarget.actionDescriptiveName, AxisRange.Negative);
					}
				}
				else
				{
					ActionElementMap firstElementMapWithElementTarget = controllerMap.GetFirstElementMapWithElementTarget(controllerTemplateAxisSource.fullTarget, action.id, true);
					if (firstElementMapWithElementTarget != null)
					{
						uiElement.SetLabel(firstElementMapWithElementTarget.actionDescriptiveName, AxisRange.Full);
					}
					else
					{
						firstElementMapWithElementTarget = controllerMap.GetFirstElementMapWithElementTarget(new ControllerElementTarget(controllerTemplateAxisSource.fullTarget)
						{
							axisRange = AxisRange.Positive
						}, action.id, true);
						if (firstElementMapWithElementTarget != null)
						{
							uiElement.SetLabel(firstElementMapWithElementTarget.actionDescriptiveName, AxisRange.Positive);
						}
						firstElementMapWithElementTarget = controllerMap.GetFirstElementMapWithElementTarget(new ControllerElementTarget(controllerTemplateAxisSource.fullTarget)
						{
							axisRange = AxisRange.Negative
						}, action.id, true);
						if (firstElementMapWithElementTarget != null)
						{
							uiElement.SetLabel(firstElementMapWithElementTarget.actionDescriptiveName, AxisRange.Negative);
						}
					}
				}
			}
			else if (element.source.type == ControllerTemplateElementSourceType.Button)
			{
				IControllerTemplateButtonSource controllerTemplateButtonSource = element.source as IControllerTemplateButtonSource;
				ActionElementMap firstElementMapWithElementTarget = controllerMap.GetFirstElementMapWithElementTarget(controllerTemplateButtonSource.target, action.id, true);
				if (firstElementMapWithElementTarget != null)
				{
					uiElement.SetLabel(firstElementMapWithElementTarget.actionDescriptiveName, AxisRange.Full);
				}
			}
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x0009AD64 File Offset: 0x00099164
		private GamepadTemplateUI.Stick GetStick(int elementId)
		{
			for (int i = 0; i < this._sticks.Length; i++)
			{
				if (this._sticks[i].ContainsElement(elementId))
				{
					return this._sticks[i];
				}
			}
			return null;
		}

		// Token: 0x06001909 RID: 6409 RVA: 0x0009ADA7 File Offset: 0x000991A7
		private void OnControllerConnected(ControllerStatusChangedEventArgs args)
		{
			this.DrawLabels();
		}

		// Token: 0x0600190A RID: 6410 RVA: 0x0009ADAF File Offset: 0x000991AF
		private void OnControllerDisconnected(ControllerStatusChangedEventArgs args)
		{
			this.DrawLabels();
		}

		// Token: 0x04001BE5 RID: 7141
		private const float stickRadius = 20f;

		// Token: 0x04001BE6 RID: 7142
		public int playerId;

		// Token: 0x04001BE7 RID: 7143
		[SerializeField]
		private RectTransform leftStick;

		// Token: 0x04001BE8 RID: 7144
		[SerializeField]
		private RectTransform rightStick;

		// Token: 0x04001BE9 RID: 7145
		[SerializeField]
		private ControllerUIElement leftStickX;

		// Token: 0x04001BEA RID: 7146
		[SerializeField]
		private ControllerUIElement leftStickY;

		// Token: 0x04001BEB RID: 7147
		[SerializeField]
		private ControllerUIElement leftStickButton;

		// Token: 0x04001BEC RID: 7148
		[SerializeField]
		private ControllerUIElement rightStickX;

		// Token: 0x04001BED RID: 7149
		[SerializeField]
		private ControllerUIElement rightStickY;

		// Token: 0x04001BEE RID: 7150
		[SerializeField]
		private ControllerUIElement rightStickButton;

		// Token: 0x04001BEF RID: 7151
		[SerializeField]
		private ControllerUIElement actionBottomRow1;

		// Token: 0x04001BF0 RID: 7152
		[SerializeField]
		private ControllerUIElement actionBottomRow2;

		// Token: 0x04001BF1 RID: 7153
		[SerializeField]
		private ControllerUIElement actionBottomRow3;

		// Token: 0x04001BF2 RID: 7154
		[SerializeField]
		private ControllerUIElement actionTopRow1;

		// Token: 0x04001BF3 RID: 7155
		[SerializeField]
		private ControllerUIElement actionTopRow2;

		// Token: 0x04001BF4 RID: 7156
		[SerializeField]
		private ControllerUIElement actionTopRow3;

		// Token: 0x04001BF5 RID: 7157
		[SerializeField]
		private ControllerUIElement leftShoulder;

		// Token: 0x04001BF6 RID: 7158
		[SerializeField]
		private ControllerUIElement leftTrigger;

		// Token: 0x04001BF7 RID: 7159
		[SerializeField]
		private ControllerUIElement rightShoulder;

		// Token: 0x04001BF8 RID: 7160
		[SerializeField]
		private ControllerUIElement rightTrigger;

		// Token: 0x04001BF9 RID: 7161
		[SerializeField]
		private ControllerUIElement center1;

		// Token: 0x04001BFA RID: 7162
		[SerializeField]
		private ControllerUIElement center2;

		// Token: 0x04001BFB RID: 7163
		[SerializeField]
		private ControllerUIElement center3;

		// Token: 0x04001BFC RID: 7164
		[SerializeField]
		private ControllerUIElement dPadUp;

		// Token: 0x04001BFD RID: 7165
		[SerializeField]
		private ControllerUIElement dPadRight;

		// Token: 0x04001BFE RID: 7166
		[SerializeField]
		private ControllerUIElement dPadDown;

		// Token: 0x04001BFF RID: 7167
		[SerializeField]
		private ControllerUIElement dPadLeft;

		// Token: 0x04001C00 RID: 7168
		private GamepadTemplateUI.UIElement[] _uiElementsArray;

		// Token: 0x04001C01 RID: 7169
		private Dictionary<int, ControllerUIElement> _uiElements = new Dictionary<int, ControllerUIElement>();

		// Token: 0x04001C02 RID: 7170
		private IList<ControllerTemplateElementTarget> _tempTargetList = new List<ControllerTemplateElementTarget>(2);

		// Token: 0x04001C03 RID: 7171
		private GamepadTemplateUI.Stick[] _sticks;

		// Token: 0x02000519 RID: 1305
		private class Stick
		{
			// Token: 0x0600190B RID: 6411 RVA: 0x0009ADB8 File Offset: 0x000991B8
			public Stick(RectTransform transform, int xAxisElementId, int yAxisElementId)
			{
				if (transform == null)
				{
					return;
				}
				this._transform = transform;
				this._origPosition = this._transform.anchoredPosition;
				this._xAxisElementId = xAxisElementId;
				this._yAxisElementId = yAxisElementId;
			}

			// Token: 0x17000180 RID: 384
			// (get) Token: 0x0600190C RID: 6412 RVA: 0x0009AE0C File Offset: 0x0009920C
			// (set) Token: 0x0600190D RID: 6413 RVA: 0x0009AE3F File Offset: 0x0009923F
			public Vector2 position
			{
				get
				{
					return (!(this._transform != null)) ? Vector2.zero : (this._transform.anchoredPosition - this._origPosition);
				}
				set
				{
					if (this._transform == null)
					{
						return;
					}
					this._transform.anchoredPosition = this._origPosition + value;
				}
			}

			// Token: 0x0600190E RID: 6414 RVA: 0x0009AE6A File Offset: 0x0009926A
			public void Reset()
			{
				if (this._transform == null)
				{
					return;
				}
				this._transform.anchoredPosition = this._origPosition;
			}

			// Token: 0x0600190F RID: 6415 RVA: 0x0009AE8F File Offset: 0x0009928F
			public bool ContainsElement(int elementId)
			{
				return !(this._transform == null) && (elementId == this._xAxisElementId || elementId == this._yAxisElementId);
			}

			// Token: 0x06001910 RID: 6416 RVA: 0x0009AEBC File Offset: 0x000992BC
			public void SetAxisPosition(int elementId, float value)
			{
				if (this._transform == null)
				{
					return;
				}
				Vector2 position = this.position;
				if (elementId == this._xAxisElementId)
				{
					position.x = value;
				}
				else if (elementId == this._yAxisElementId)
				{
					position.y = value;
				}
				this.position = position;
			}

			// Token: 0x04001C04 RID: 7172
			private RectTransform _transform;

			// Token: 0x04001C05 RID: 7173
			private Vector2 _origPosition;

			// Token: 0x04001C06 RID: 7174
			private int _xAxisElementId = -1;

			// Token: 0x04001C07 RID: 7175
			private int _yAxisElementId = -1;
		}

		// Token: 0x0200051A RID: 1306
		private class UIElement
		{
			// Token: 0x06001911 RID: 6417 RVA: 0x0009AF16 File Offset: 0x00099316
			public UIElement(int id, ControllerUIElement element)
			{
				this.id = id;
				this.element = element;
			}

			// Token: 0x04001C08 RID: 7176
			public int id;

			// Token: 0x04001C09 RID: 7177
			public ControllerUIElement element;
		}
	}
}
