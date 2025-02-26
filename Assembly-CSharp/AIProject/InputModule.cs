using System;
using Manager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace AIProject
{
	// Token: 0x0200083A RID: 2106
	[AddComponentMenu("AI少女/Event/Input Module")]
	public class InputModule : PointerInputModule
	{
		// Token: 0x060035B8 RID: 13752 RVA: 0x0013C9C5 File Offset: 0x0013ADC5
		protected InputModule()
		{
		}

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x060035B9 RID: 13753 RVA: 0x0013CA04 File Offset: 0x0013AE04
		// (set) Token: 0x060035BA RID: 13754 RVA: 0x0013CA0C File Offset: 0x0013AE0C
		public bool ForceModuleActive
		{
			get
			{
				return this._forceModuleActive;
			}
			set
			{
				this._forceModuleActive = value;
			}
		}

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x060035BB RID: 13755 RVA: 0x0013CA15 File Offset: 0x0013AE15
		// (set) Token: 0x060035BC RID: 13756 RVA: 0x0013CA1D File Offset: 0x0013AE1D
		public float InputActionsPerSecond
		{
			get
			{
				return this._inputActionPerSecond;
			}
			set
			{
				this._inputActionPerSecond = value;
			}
		}

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x060035BD RID: 13757 RVA: 0x0013CA26 File Offset: 0x0013AE26
		// (set) Token: 0x060035BE RID: 13758 RVA: 0x0013CA2E File Offset: 0x0013AE2E
		public float RepeatDelay
		{
			get
			{
				return this._repeatDelay;
			}
			set
			{
				this._repeatDelay = value;
			}
		}

		// Token: 0x060035BF RID: 13759 RVA: 0x0013CA38 File Offset: 0x0013AE38
		private bool ShouldIgnoreEventsOnNoFocus()
		{
			bool result;
			switch (SystemInfo.operatingSystemFamily)
			{
			case OperatingSystemFamily.MacOSX:
			case OperatingSystemFamily.Windows:
			case OperatingSystemFamily.Linux:
				result = false;
				break;
			default:
				result = false;
				break;
			}
			return result;
		}

		// Token: 0x060035C0 RID: 13760 RVA: 0x0013CA73 File Offset: 0x0013AE73
		public override void UpdateModule()
		{
			if (base.eventSystem.isFocused || !this.ShouldIgnoreEventsOnNoFocus())
			{
				this._lastMousePosition = this._mousePosition;
				this._mousePosition = base.input.mousePosition;
			}
		}

		// Token: 0x060035C1 RID: 13761 RVA: 0x0013CAAD File Offset: 0x0013AEAD
		public override bool IsModuleSupported()
		{
			return this._forceModuleActive || base.input.mousePresent || base.input.touchSupported;
		}

		// Token: 0x060035C2 RID: 13762 RVA: 0x0013CAD8 File Offset: 0x0013AED8
		public override bool ShouldActivateModule()
		{
			bool result;
			if (!base.ShouldActivateModule())
			{
				result = false;
			}
			else
			{
				bool flag = this._forceModuleActive;
				if (Singleton<Manager.Input>.IsInstance())
				{
					Manager.Input instance = Singleton<Manager.Input>.Instance;
					flag |= instance.IsPressedKey(KeyCode.Mouse0);
					if (instance.State == Manager.Input.ValidType.UI)
					{
						flag |= instance.IsPressedAction();
						flag |= !Mathf.Approximately(instance.UIAxisRow.x, 0f);
						flag |= !Mathf.Approximately(instance.UIAxisRow.y, 0f);
					}
				}
				flag |= ((this._mousePosition - this._lastMousePosition).sqrMagnitude > 0f);
				if (base.input.touchCount > 0)
				{
					flag = true;
				}
				result = flag;
			}
			return result;
		}

		// Token: 0x060035C3 RID: 13763 RVA: 0x0013CBA8 File Offset: 0x0013AFA8
		public override void ActivateModule()
		{
			if (!base.eventSystem.isFocused && this.ShouldIgnoreEventsOnNoFocus())
			{
				return;
			}
			base.ActivateModule();
			this._mousePosition = base.input.mousePosition;
			this._lastMousePosition = base.input.mousePosition;
			GameObject gameObject = base.eventSystem.currentSelectedGameObject;
			if (gameObject == null)
			{
				gameObject = base.eventSystem.firstSelectedGameObject;
			}
			base.eventSystem.SetSelectedGameObject(gameObject, this.GetBaseEventData());
		}

		// Token: 0x060035C4 RID: 13764 RVA: 0x0013CC2F File Offset: 0x0013B02F
		public override void DeactivateModule()
		{
			base.DeactivateModule();
			base.ClearSelection();
		}

		// Token: 0x060035C5 RID: 13765 RVA: 0x0013CC40 File Offset: 0x0013B040
		public override void Process()
		{
			if (!base.eventSystem.isFocused && this.ShouldIgnoreEventsOnNoFocus())
			{
				return;
			}
			bool flag = this.SendUpdateEventToSelectedObject();
			if (base.eventSystem.sendNavigationEvents)
			{
				if (!flag)
				{
					flag |= this.SendMoveEventToSelectedObject();
				}
				if (!flag)
				{
					this.SendSubmitEventToSelectedObject();
				}
			}
			if (!this.ProcessTouchEvents() && base.input.mousePresent)
			{
				this.ProcessMouseEvent();
			}
		}

		// Token: 0x060035C6 RID: 13766 RVA: 0x0013CCC0 File Offset: 0x0013B0C0
		private bool ProcessTouchEvents()
		{
			for (int i = 0; i < base.input.touchCount; i++)
			{
				Touch touch = base.input.GetTouch(i);
				if (touch.type != TouchType.Indirect)
				{
					bool pressed;
					bool flag;
					PointerEventData touchPointerEventData = base.GetTouchPointerEventData(touch, out pressed, out flag);
					this.ProcessTouchPress(touchPointerEventData, pressed, flag);
					if (!flag)
					{
						this.ProcessMove(touchPointerEventData);
						this.ProcessDrag(touchPointerEventData);
					}
					else
					{
						base.RemovePointerData(touchPointerEventData);
					}
				}
			}
			return base.input.touchCount > 0;
		}

		// Token: 0x060035C7 RID: 13767 RVA: 0x0013CD4C File Offset: 0x0013B14C
		protected void ProcessTouchPress(PointerEventData pointerEvent, bool pressed, bool released)
		{
			GameObject gameObject = pointerEvent.pointerCurrentRaycast.gameObject;
			if (pressed)
			{
				pointerEvent.eligibleForClick = true;
				pointerEvent.delta = Vector2.zero;
				pointerEvent.dragging = false;
				pointerEvent.useDragThreshold = true;
				pointerEvent.pressPosition = pointerEvent.position;
				pointerEvent.pointerPressRaycast = pointerEvent.pointerCurrentRaycast;
				base.DeselectIfSelectionChanged(gameObject, pointerEvent);
				if (pointerEvent.pointerEnter != gameObject)
				{
					base.HandlePointerExitAndEnter(pointerEvent, gameObject);
					pointerEvent.pointerEnter = gameObject;
				}
				GameObject gameObject2 = ExecuteEvents.ExecuteHierarchy<IPointerDownHandler>(gameObject, pointerEvent, ExecuteEvents.pointerDownHandler);
				if (gameObject2 == null)
				{
					gameObject2 = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject);
				}
				float unscaledTime = Time.unscaledTime;
				if (gameObject2 == pointerEvent.lastPress)
				{
					float num = unscaledTime - pointerEvent.clickTime;
					if ((double)num < 0.3)
					{
						pointerEvent.clickTime += 1f;
					}
					else
					{
						pointerEvent.clickCount = 1;
					}
					pointerEvent.clickTime = unscaledTime;
				}
				else
				{
					pointerEvent.clickCount = 1;
				}
				pointerEvent.pointerPress = gameObject2;
				pointerEvent.rawPointerPress = gameObject;
				pointerEvent.clickTime = unscaledTime;
				pointerEvent.pointerDrag = ExecuteEvents.GetEventHandler<IDragHandler>(gameObject);
				if (pointerEvent.pointerDrag != null)
				{
					ExecuteEvents.Execute<IInitializePotentialDragHandler>(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.initializePotentialDrag);
				}
			}
			if (released)
			{
				ExecuteEvents.Execute<IPointerUpHandler>(pointerEvent.pointerPress, pointerEvent, ExecuteEvents.pointerUpHandler);
				GameObject eventHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject);
				if (pointerEvent.pointerPress == eventHandler && pointerEvent.eligibleForClick)
				{
					ExecuteEvents.Execute<IPointerClickHandler>(pointerEvent.pointerPress, pointerEvent, ExecuteEvents.pointerClickHandler);
				}
				else if (pointerEvent.pointerDrag != null && pointerEvent.dragging)
				{
					ExecuteEvents.ExecuteHierarchy<IDropHandler>(gameObject, pointerEvent, ExecuteEvents.dropHandler);
				}
				pointerEvent.eligibleForClick = false;
				pointerEvent.pointerPress = null;
				pointerEvent.rawPointerPress = null;
				if (pointerEvent.pointerDrag != null && pointerEvent.dragging)
				{
					ExecuteEvents.Execute<IEndDragHandler>(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.endDragHandler);
				}
				pointerEvent.dragging = false;
				pointerEvent.pointerDrag = null;
				ExecuteEvents.ExecuteHierarchy<IPointerExitHandler>(pointerEvent.pointerEnter, pointerEvent, ExecuteEvents.pointerExitHandler);
				pointerEvent.pointerEnter = null;
			}
		}

		// Token: 0x060035C8 RID: 13768 RVA: 0x0013CF7C File Offset: 0x0013B37C
		protected bool SendSubmitEventToSelectedObject()
		{
			bool result;
			if (base.eventSystem.currentSelectedGameObject == null)
			{
				result = false;
			}
			else
			{
				BaseEventData baseEventData = this.GetBaseEventData();
				if (Singleton<Manager.Input>.IsInstance() && Singleton<Manager.Input>.Instance.IsPressedSubmit())
				{
					ExecuteEvents.Execute<ISubmitHandler>(base.eventSystem.currentSelectedGameObject, baseEventData, ExecuteEvents.submitHandler);
				}
				result = baseEventData.used;
			}
			return result;
		}

		// Token: 0x060035C9 RID: 13769 RVA: 0x0013CFE8 File Offset: 0x0013B3E8
		private Vector2 GetRawMoveVector()
		{
			Vector2 zero = Vector2.zero;
			if (Singleton<Manager.Input>.IsInstance())
			{
				Manager.Input instance = Singleton<Manager.Input>.Instance;
				if (instance.State == Manager.Input.ValidType.UI)
				{
					zero.x = instance.UIAxisRow.x;
					zero.y = instance.UIAxisRow.y;
					if (instance.IsPressedHorizontal())
					{
						if (zero.x < 0f)
						{
							zero.x = -1f;
						}
						if (zero.x > 0f)
						{
							zero.x = 1f;
						}
					}
					if (instance.IsPressedVertical())
					{
						if (zero.y < 0f)
						{
							zero.y = -1f;
						}
						if (zero.y > 0f)
						{
							zero.y = 1f;
						}
					}
				}
			}
			return zero;
		}

		// Token: 0x060035CA RID: 13770 RVA: 0x0013D0CC File Offset: 0x0013B4CC
		protected bool SendMoveEventToSelectedObject()
		{
			float unscaledTime = Time.unscaledTime;
			Vector2 rawMoveVector = this.GetRawMoveVector();
			bool result;
			if (Mathf.Approximately(rawMoveVector.x, 0f) && Mathf.Approximately(rawMoveVector.y, 0f))
			{
				this._consecutiveMoveCount = 0;
				result = false;
			}
			else
			{
				Manager.Input instance = Singleton<Manager.Input>.Instance;
				bool flag = instance.State == Manager.Input.ValidType.UI && Singleton<Manager.Input>.IsInstance() && (instance.IsPressedHorizontal() || instance.IsPressedVertical());
				bool flag2 = Vector2.Dot(rawMoveVector, this._lastMoveVector) > 0f;
				if (!flag)
				{
					if (flag2 && this._consecutiveMoveCount == 1)
					{
						flag = (unscaledTime > this._prevActionTime + this._repeatDelay);
					}
					else
					{
						flag = (unscaledTime > this._prevActionTime + 1f / this._inputActionPerSecond);
					}
				}
				if (flag)
				{
					AxisEventData axisEventData = this.GetAxisEventData(rawMoveVector.x, rawMoveVector.y, 0.6f);
					if (axisEventData.moveDir != MoveDirection.None)
					{
						ExecuteEvents.Execute<IMoveHandler>(base.eventSystem.currentSelectedGameObject, axisEventData, ExecuteEvents.moveHandler);
						if (!flag2)
						{
							this._consecutiveMoveCount = 0;
						}
						this._consecutiveMoveCount++;
						this._prevActionTime = unscaledTime;
						this._lastMoveVector = rawMoveVector;
					}
					else
					{
						this._consecutiveMoveCount = 0;
					}
					result = axisEventData.used;
				}
				result = false;
			}
			return result;
		}

		// Token: 0x060035CB RID: 13771 RVA: 0x0013D24D File Offset: 0x0013B64D
		protected void ProcessMouseEvent()
		{
			this.ProcessMouseEvent(0);
		}

		// Token: 0x060035CC RID: 13772 RVA: 0x0013D258 File Offset: 0x0013B658
		protected void ProcessMouseEvent(int id)
		{
			PointerInputModule.MouseState mousePointerEventData = this.GetMousePointerEventData(id);
			PointerInputModule.MouseButtonEventData eventData = mousePointerEventData.GetButtonState(PointerEventData.InputButton.Left).eventData;
			this._currentFocusedGameObject = eventData.buttonData.pointerCurrentRaycast.gameObject;
			this.ProcessMousePress(eventData);
			this.ProcessMove(eventData.buttonData);
			this.ProcessDrag(eventData.buttonData);
			this.ProcessMousePress(mousePointerEventData.GetButtonState(PointerEventData.InputButton.Right).eventData);
			this.ProcessDrag(mousePointerEventData.GetButtonState(PointerEventData.InputButton.Right).eventData.buttonData);
			this.ProcessMousePress(mousePointerEventData.GetButtonState(PointerEventData.InputButton.Middle).eventData);
			this.ProcessDrag(mousePointerEventData.GetButtonState(PointerEventData.InputButton.Middle).eventData.buttonData);
			if (!Mathf.Approximately(eventData.buttonData.scrollDelta.sqrMagnitude, 0f))
			{
				GameObject eventHandler = ExecuteEvents.GetEventHandler<IScrollHandler>(eventData.buttonData.pointerCurrentRaycast.gameObject);
				ExecuteEvents.ExecuteHierarchy<IScrollHandler>(eventHandler, eventData.buttonData, ExecuteEvents.scrollHandler);
			}
		}

		// Token: 0x060035CD RID: 13773 RVA: 0x0013D354 File Offset: 0x0013B754
		protected bool SendUpdateEventToSelectedObject()
		{
			bool result;
			if (base.eventSystem.currentSelectedGameObject == null)
			{
				result = false;
			}
			else
			{
				BaseEventData baseEventData = this.GetBaseEventData();
				ExecuteEvents.Execute<IUpdateSelectedHandler>(base.eventSystem.currentSelectedGameObject, baseEventData, ExecuteEvents.updateSelectedHandler);
				result = baseEventData.used;
			}
			return result;
		}

		// Token: 0x060035CE RID: 13774 RVA: 0x0013D3A4 File Offset: 0x0013B7A4
		protected void ProcessMousePress(PointerInputModule.MouseButtonEventData data)
		{
			PointerEventData buttonData = data.buttonData;
			GameObject gameObject = buttonData.pointerCurrentRaycast.gameObject;
			if (data.PressedThisFrame())
			{
				buttonData.eligibleForClick = true;
				buttonData.delta = Vector2.zero;
				buttonData.dragging = false;
				buttonData.useDragThreshold = true;
				buttonData.pressPosition = buttonData.position;
				buttonData.pointerPressRaycast = buttonData.pointerCurrentRaycast;
				base.DeselectIfSelectionChanged(gameObject, buttonData);
				GameObject gameObject2 = ExecuteEvents.ExecuteHierarchy<IPointerDownHandler>(gameObject, buttonData, ExecuteEvents.pointerDownHandler);
				if (gameObject2 == null)
				{
					gameObject2 = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject);
				}
				float unscaledTime = Time.unscaledTime;
				if (gameObject2 == buttonData.lastPress)
				{
					float num = unscaledTime - buttonData.clickTime;
					if (num < 0.3f)
					{
						buttonData.clickCount++;
					}
					else
					{
						buttonData.clickCount = 1;
					}
					buttonData.clickTime = unscaledTime;
				}
				else
				{
					buttonData.clickCount = 1;
				}
				buttonData.pointerPress = gameObject2;
				buttonData.rawPointerPress = gameObject;
				buttonData.clickTime = unscaledTime;
				buttonData.pointerDrag = ExecuteEvents.GetEventHandler<IDragHandler>(gameObject);
				if (buttonData.pointerDrag != null)
				{
					ExecuteEvents.Execute<IInitializePotentialDragHandler>(buttonData.pointerDrag, buttonData, ExecuteEvents.initializePotentialDrag);
				}
			}
			if (data.ReleasedThisFrame())
			{
				ExecuteEvents.Execute<IPointerUpHandler>(buttonData.pointerPress, buttonData, ExecuteEvents.pointerUpHandler);
				GameObject eventHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject);
				if (buttonData.pointerPress == eventHandler && buttonData.eligibleForClick)
				{
					ExecuteEvents.Execute<IPointerClickHandler>(buttonData.pointerPress, buttonData, ExecuteEvents.pointerClickHandler);
				}
				else if (buttonData.pointerDrag != null && buttonData.dragging)
				{
					ExecuteEvents.ExecuteHierarchy<IDropHandler>(gameObject, buttonData, ExecuteEvents.dropHandler);
				}
				buttonData.eligibleForClick = false;
				buttonData.pointerPress = null;
				buttonData.rawPointerPress = null;
				if (buttonData.pointerDrag != null && buttonData.dragging)
				{
					ExecuteEvents.Execute<IEndDragHandler>(buttonData.pointerDrag, buttonData, ExecuteEvents.endDragHandler);
				}
				buttonData.dragging = false;
				buttonData.pointerDrag = null;
				if (gameObject != buttonData.pointerEnter)
				{
					base.HandlePointerExitAndEnter(buttonData, null);
					base.HandlePointerExitAndEnter(buttonData, gameObject);
				}
			}
		}

		// Token: 0x060035CF RID: 13775 RVA: 0x0013D5C8 File Offset: 0x0013B9C8
		protected GameObject GetCurrentFocusedGameObject()
		{
			return this._currentFocusedGameObject;
		}

		// Token: 0x04003626 RID: 13862
		private float _prevActionTime;

		// Token: 0x04003627 RID: 13863
		private Vector2 _lastMoveVector = Vector2.zero;

		// Token: 0x04003628 RID: 13864
		private int _consecutiveMoveCount;

		// Token: 0x04003629 RID: 13865
		private Vector2 _mousePosition = Vector2.zero;

		// Token: 0x0400362A RID: 13866
		private Vector2 _lastMousePosition = Vector2.zero;

		// Token: 0x0400362B RID: 13867
		private GameObject _currentFocusedGameObject;

		// Token: 0x0400362C RID: 13868
		[SerializeField]
		private float _inputActionPerSecond = 10f;

		// Token: 0x0400362D RID: 13869
		[SerializeField]
		private float _repeatDelay = 0.5f;

		// Token: 0x0400362E RID: 13870
		[FormerlySerializedAs("_allowActivationOnMobileDevice")]
		[SerializeField]
		private bool _forceModuleActive;
	}
}
