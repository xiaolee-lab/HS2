using System;
using System.Collections.Generic;
using Rewired.Components;
using Rewired.UI;
using Rewired.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Rewired.Integration.UnityUI
{
	// Token: 0x02000575 RID: 1397
	[AddComponentMenu("Event/Rewired Standalone Input Module")]
	public sealed class RewiredStandaloneInputModule : RewiredPointerInputModule
	{
		// Token: 0x06001DB5 RID: 7605 RVA: 0x000AD34C File Offset: 0x000AB74C
		private RewiredStandaloneInputModule()
		{
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06001DB6 RID: 7606 RVA: 0x000AD3E5 File Offset: 0x000AB7E5
		// (set) Token: 0x06001DB7 RID: 7607 RVA: 0x000AD3ED File Offset: 0x000AB7ED
		public InputManager_Base RewiredInputManager
		{
			get
			{
				return this.rewiredInputManager;
			}
			set
			{
				this.rewiredInputManager = value;
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06001DB8 RID: 7608 RVA: 0x000AD3F6 File Offset: 0x000AB7F6
		// (set) Token: 0x06001DB9 RID: 7609 RVA: 0x000AD400 File Offset: 0x000AB800
		public bool UseAllRewiredGamePlayers
		{
			get
			{
				return this.useAllRewiredGamePlayers;
			}
			set
			{
				bool flag = value != this.useAllRewiredGamePlayers;
				this.useAllRewiredGamePlayers = value;
				if (flag)
				{
					this.SetupRewiredVars();
				}
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06001DBA RID: 7610 RVA: 0x000AD42D File Offset: 0x000AB82D
		// (set) Token: 0x06001DBB RID: 7611 RVA: 0x000AD438 File Offset: 0x000AB838
		public bool UseRewiredSystemPlayer
		{
			get
			{
				return this.useRewiredSystemPlayer;
			}
			set
			{
				bool flag = value != this.useRewiredSystemPlayer;
				this.useRewiredSystemPlayer = value;
				if (flag)
				{
					this.SetupRewiredVars();
				}
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06001DBC RID: 7612 RVA: 0x000AD465 File Offset: 0x000AB865
		// (set) Token: 0x06001DBD RID: 7613 RVA: 0x000AD477 File Offset: 0x000AB877
		public int[] RewiredPlayerIds
		{
			get
			{
				return (int[])this.rewiredPlayerIds.Clone();
			}
			set
			{
				this.rewiredPlayerIds = ((value == null) ? new int[0] : ((int[])value.Clone()));
				this.SetupRewiredVars();
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06001DBE RID: 7614 RVA: 0x000AD4A1 File Offset: 0x000AB8A1
		// (set) Token: 0x06001DBF RID: 7615 RVA: 0x000AD4A9 File Offset: 0x000AB8A9
		public bool UsePlayingPlayersOnly
		{
			get
			{
				return this.usePlayingPlayersOnly;
			}
			set
			{
				this.usePlayingPlayersOnly = value;
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06001DC0 RID: 7616 RVA: 0x000AD4B2 File Offset: 0x000AB8B2
		// (set) Token: 0x06001DC1 RID: 7617 RVA: 0x000AD4BF File Offset: 0x000AB8BF
		public List<PlayerMouse> PlayerMice
		{
			get
			{
				return new List<PlayerMouse>(this.playerMice);
			}
			set
			{
				if (value == null)
				{
					this.playerMice = new List<PlayerMouse>();
					this.SetupRewiredVars();
					return;
				}
				this.playerMice = new List<PlayerMouse>(value);
				this.SetupRewiredVars();
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06001DC2 RID: 7618 RVA: 0x000AD4EB File Offset: 0x000AB8EB
		// (set) Token: 0x06001DC3 RID: 7619 RVA: 0x000AD4F3 File Offset: 0x000AB8F3
		public bool MoveOneElementPerAxisPress
		{
			get
			{
				return this.moveOneElementPerAxisPress;
			}
			set
			{
				this.moveOneElementPerAxisPress = value;
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06001DC4 RID: 7620 RVA: 0x000AD4FC File Offset: 0x000AB8FC
		// (set) Token: 0x06001DC5 RID: 7621 RVA: 0x000AD504 File Offset: 0x000AB904
		public bool allowMouseInput
		{
			get
			{
				return this.m_allowMouseInput;
			}
			set
			{
				this.m_allowMouseInput = value;
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06001DC6 RID: 7622 RVA: 0x000AD50D File Offset: 0x000AB90D
		// (set) Token: 0x06001DC7 RID: 7623 RVA: 0x000AD515 File Offset: 0x000AB915
		public bool allowMouseInputIfTouchSupported
		{
			get
			{
				return this.m_allowMouseInputIfTouchSupported;
			}
			set
			{
				this.m_allowMouseInputIfTouchSupported = value;
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06001DC8 RID: 7624 RVA: 0x000AD51E File Offset: 0x000AB91E
		// (set) Token: 0x06001DC9 RID: 7625 RVA: 0x000AD526 File Offset: 0x000AB926
		public bool allowTouchInput
		{
			get
			{
				return this.m_allowTouchInput;
			}
			set
			{
				this.m_allowTouchInput = value;
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06001DCA RID: 7626 RVA: 0x000AD52F File Offset: 0x000AB92F
		// (set) Token: 0x06001DCB RID: 7627 RVA: 0x000AD537 File Offset: 0x000AB937
		public bool SetActionsById
		{
			get
			{
				return this.setActionsById;
			}
			set
			{
				if (this.setActionsById == value)
				{
					return;
				}
				this.setActionsById = value;
				this.SetupRewiredVars();
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06001DCC RID: 7628 RVA: 0x000AD553 File Offset: 0x000AB953
		// (set) Token: 0x06001DCD RID: 7629 RVA: 0x000AD55C File Offset: 0x000AB95C
		public int HorizontalActionId
		{
			get
			{
				return this.horizontalActionId;
			}
			set
			{
				if (value == this.horizontalActionId)
				{
					return;
				}
				this.horizontalActionId = value;
				if (ReInput.isReady)
				{
					this.m_HorizontalAxis = ((ReInput.mapping.GetAction(value) == null) ? string.Empty : ReInput.mapping.GetAction(value).name);
				}
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06001DCE RID: 7630 RVA: 0x000AD5B7 File Offset: 0x000AB9B7
		// (set) Token: 0x06001DCF RID: 7631 RVA: 0x000AD5C0 File Offset: 0x000AB9C0
		public int VerticalActionId
		{
			get
			{
				return this.verticalActionId;
			}
			set
			{
				if (value == this.verticalActionId)
				{
					return;
				}
				this.verticalActionId = value;
				if (ReInput.isReady)
				{
					this.m_VerticalAxis = ((ReInput.mapping.GetAction(value) == null) ? string.Empty : ReInput.mapping.GetAction(value).name);
				}
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06001DD0 RID: 7632 RVA: 0x000AD61B File Offset: 0x000ABA1B
		// (set) Token: 0x06001DD1 RID: 7633 RVA: 0x000AD624 File Offset: 0x000ABA24
		public int SubmitActionId
		{
			get
			{
				return this.submitActionId;
			}
			set
			{
				if (value == this.submitActionId)
				{
					return;
				}
				this.submitActionId = value;
				if (ReInput.isReady)
				{
					this.m_SubmitButton = ((ReInput.mapping.GetAction(value) == null) ? string.Empty : ReInput.mapping.GetAction(value).name);
				}
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06001DD2 RID: 7634 RVA: 0x000AD67F File Offset: 0x000ABA7F
		// (set) Token: 0x06001DD3 RID: 7635 RVA: 0x000AD688 File Offset: 0x000ABA88
		public int CancelActionId
		{
			get
			{
				return this.cancelActionId;
			}
			set
			{
				if (value == this.cancelActionId)
				{
					return;
				}
				this.cancelActionId = value;
				if (ReInput.isReady)
				{
					this.m_CancelButton = ((ReInput.mapping.GetAction(value) == null) ? string.Empty : ReInput.mapping.GetAction(value).name);
				}
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06001DD4 RID: 7636 RVA: 0x000AD6E3 File Offset: 0x000ABAE3
		protected override bool isMouseSupported
		{
			get
			{
				return base.isMouseSupported && this.m_allowMouseInput && (!this.isTouchSupported || this.m_allowMouseInputIfTouchSupported);
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06001DD5 RID: 7637 RVA: 0x000AD716 File Offset: 0x000ABB16
		private bool isTouchAllowed
		{
			get
			{
				return this.isTouchSupported && this.m_allowTouchInput;
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06001DD6 RID: 7638 RVA: 0x000AD72B File Offset: 0x000ABB2B
		// (set) Token: 0x06001DD7 RID: 7639 RVA: 0x000AD733 File Offset: 0x000ABB33
		[Obsolete("allowActivationOnMobileDevice has been deprecated. Use forceModuleActive instead")]
		public bool allowActivationOnMobileDevice
		{
			get
			{
				return this.m_ForceModuleActive;
			}
			set
			{
				this.m_ForceModuleActive = value;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06001DD8 RID: 7640 RVA: 0x000AD73C File Offset: 0x000ABB3C
		// (set) Token: 0x06001DD9 RID: 7641 RVA: 0x000AD744 File Offset: 0x000ABB44
		public bool forceModuleActive
		{
			get
			{
				return this.m_ForceModuleActive;
			}
			set
			{
				this.m_ForceModuleActive = value;
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06001DDA RID: 7642 RVA: 0x000AD74D File Offset: 0x000ABB4D
		// (set) Token: 0x06001DDB RID: 7643 RVA: 0x000AD755 File Offset: 0x000ABB55
		public float inputActionsPerSecond
		{
			get
			{
				return this.m_InputActionsPerSecond;
			}
			set
			{
				this.m_InputActionsPerSecond = value;
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06001DDC RID: 7644 RVA: 0x000AD75E File Offset: 0x000ABB5E
		// (set) Token: 0x06001DDD RID: 7645 RVA: 0x000AD766 File Offset: 0x000ABB66
		public float repeatDelay
		{
			get
			{
				return this.m_RepeatDelay;
			}
			set
			{
				this.m_RepeatDelay = value;
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06001DDE RID: 7646 RVA: 0x000AD76F File Offset: 0x000ABB6F
		// (set) Token: 0x06001DDF RID: 7647 RVA: 0x000AD777 File Offset: 0x000ABB77
		public string horizontalAxis
		{
			get
			{
				return this.m_HorizontalAxis;
			}
			set
			{
				if (this.m_HorizontalAxis == value)
				{
					return;
				}
				this.m_HorizontalAxis = value;
				if (ReInput.isReady)
				{
					this.horizontalActionId = ReInput.mapping.GetActionId(value);
				}
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06001DE0 RID: 7648 RVA: 0x000AD7AD File Offset: 0x000ABBAD
		// (set) Token: 0x06001DE1 RID: 7649 RVA: 0x000AD7B5 File Offset: 0x000ABBB5
		public string verticalAxis
		{
			get
			{
				return this.m_VerticalAxis;
			}
			set
			{
				if (this.m_VerticalAxis == value)
				{
					return;
				}
				this.m_VerticalAxis = value;
				if (ReInput.isReady)
				{
					this.verticalActionId = ReInput.mapping.GetActionId(value);
				}
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06001DE2 RID: 7650 RVA: 0x000AD7EB File Offset: 0x000ABBEB
		// (set) Token: 0x06001DE3 RID: 7651 RVA: 0x000AD7F3 File Offset: 0x000ABBF3
		public string submitButton
		{
			get
			{
				return this.m_SubmitButton;
			}
			set
			{
				if (this.m_SubmitButton == value)
				{
					return;
				}
				this.m_SubmitButton = value;
				if (ReInput.isReady)
				{
					this.submitActionId = ReInput.mapping.GetActionId(value);
				}
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06001DE4 RID: 7652 RVA: 0x000AD829 File Offset: 0x000ABC29
		// (set) Token: 0x06001DE5 RID: 7653 RVA: 0x000AD831 File Offset: 0x000ABC31
		public string cancelButton
		{
			get
			{
				return this.m_CancelButton;
			}
			set
			{
				if (this.m_CancelButton == value)
				{
					return;
				}
				this.m_CancelButton = value;
				if (ReInput.isReady)
				{
					this.cancelActionId = ReInput.mapping.GetActionId(value);
				}
			}
		}

		// Token: 0x06001DE6 RID: 7654 RVA: 0x000AD868 File Offset: 0x000ABC68
		protected override void Awake()
		{
			base.Awake();
			this.isTouchSupported = base.defaultTouchInputSource.touchSupported;
			TouchInputModule component = base.GetComponent<TouchInputModule>();
			if (component != null)
			{
				component.enabled = false;
			}
			ReInput.InitializedEvent += this.OnRewiredInitialized;
			this.InitializeRewired();
		}

		// Token: 0x06001DE7 RID: 7655 RVA: 0x000AD8BD File Offset: 0x000ABCBD
		public override void UpdateModule()
		{
			this.CheckEditorRecompile();
			if (this.recompiling)
			{
				return;
			}
			if (!ReInput.isReady)
			{
				return;
			}
			if (!this.m_HasFocus && this.ShouldIgnoreEventsOnNoFocus())
			{
				return;
			}
		}

		// Token: 0x06001DE8 RID: 7656 RVA: 0x000AD8F3 File Offset: 0x000ABCF3
		public override bool IsModuleSupported()
		{
			return true;
		}

		// Token: 0x06001DE9 RID: 7657 RVA: 0x000AD8F8 File Offset: 0x000ABCF8
		public override bool ShouldActivateModule()
		{
			if (!base.ShouldActivateModule())
			{
				return false;
			}
			if (this.recompiling)
			{
				return false;
			}
			if (!ReInput.isReady)
			{
				return false;
			}
			bool flag = this.m_ForceModuleActive;
			for (int i = 0; i < this.playerIds.Length; i++)
			{
				Player player = ReInput.players.GetPlayer(this.playerIds[i]);
				if (player != null)
				{
					if (!this.usePlayingPlayersOnly || player.isPlaying)
					{
						flag |= this.GetButtonDown(player, this.submitActionId);
						flag |= this.GetButtonDown(player, this.cancelActionId);
						if (this.moveOneElementPerAxisPress)
						{
							flag |= (this.GetButtonDown(player, this.horizontalActionId) || this.GetNegativeButtonDown(player, this.horizontalActionId));
							flag |= (this.GetButtonDown(player, this.verticalActionId) || this.GetNegativeButtonDown(player, this.verticalActionId));
						}
						else
						{
							flag |= !Mathf.Approximately(this.GetAxis(player, this.horizontalActionId), 0f);
							flag |= !Mathf.Approximately(this.GetAxis(player, this.verticalActionId), 0f);
						}
					}
				}
			}
			if (this.isMouseSupported)
			{
				flag |= this.DidAnyMouseMove();
				flag |= this.GetMouseButtonDownOnAnyMouse(0);
			}
			if (this.isTouchAllowed)
			{
				for (int j = 0; j < base.defaultTouchInputSource.touchCount; j++)
				{
					Touch touch = base.defaultTouchInputSource.GetTouch(j);
					flag |= (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary);
				}
			}
			return flag;
		}

		// Token: 0x06001DEA RID: 7658 RVA: 0x000ADAB8 File Offset: 0x000ABEB8
		public override void ActivateModule()
		{
			if (!this.m_HasFocus && this.ShouldIgnoreEventsOnNoFocus())
			{
				return;
			}
			base.ActivateModule();
			GameObject gameObject = base.eventSystem.currentSelectedGameObject;
			if (gameObject == null)
			{
				gameObject = base.eventSystem.firstSelectedGameObject;
			}
			base.eventSystem.SetSelectedGameObject(gameObject, this.GetBaseEventData());
		}

		// Token: 0x06001DEB RID: 7659 RVA: 0x000ADB18 File Offset: 0x000ABF18
		public override void DeactivateModule()
		{
			base.DeactivateModule();
			base.ClearSelection();
		}

		// Token: 0x06001DEC RID: 7660 RVA: 0x000ADB28 File Offset: 0x000ABF28
		public override void Process()
		{
			if (!ReInput.isReady)
			{
				return;
			}
			if (!this.m_HasFocus && this.ShouldIgnoreEventsOnNoFocus())
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
			if (!this.ProcessTouchEvents() && this.isMouseSupported)
			{
				this.ProcessMouseEvents();
			}
		}

		// Token: 0x06001DED RID: 7661 RVA: 0x000ADBA8 File Offset: 0x000ABFA8
		private bool ProcessTouchEvents()
		{
			if (!this.isTouchAllowed)
			{
				return false;
			}
			for (int i = 0; i < base.defaultTouchInputSource.touchCount; i++)
			{
				Touch touch = base.defaultTouchInputSource.GetTouch(i);
				if (touch.type != TouchType.Indirect)
				{
					bool pressed;
					bool flag;
					PlayerPointerEventData touchPointerEventData = base.GetTouchPointerEventData(0, 0, touch, out pressed, out flag);
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
			return base.defaultTouchInputSource.touchCount > 0;
		}

		// Token: 0x06001DEE RID: 7662 RVA: 0x000ADC48 File Offset: 0x000AC048
		private void ProcessTouchPress(PointerEventData pointerEvent, bool pressed, bool released)
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
					if (num < 0.3f)
					{
						pointerEvent.clickCount++;
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
				if (pointerEvent.pointerDrag != null)
				{
					ExecuteEvents.Execute<IEndDragHandler>(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.endDragHandler);
				}
				pointerEvent.pointerDrag = null;
				ExecuteEvents.ExecuteHierarchy<IPointerExitHandler>(pointerEvent.pointerEnter, pointerEvent, ExecuteEvents.pointerExitHandler);
				pointerEvent.pointerEnter = null;
			}
		}

		// Token: 0x06001DEF RID: 7663 RVA: 0x000ADE9C File Offset: 0x000AC29C
		private bool SendSubmitEventToSelectedObject()
		{
			if (base.eventSystem.currentSelectedGameObject == null)
			{
				return false;
			}
			if (this.recompiling)
			{
				return false;
			}
			BaseEventData baseEventData = this.GetBaseEventData();
			for (int i = 0; i < this.playerIds.Length; i++)
			{
				Player player = ReInput.players.GetPlayer(this.playerIds[i]);
				if (player != null)
				{
					if (!this.usePlayingPlayersOnly || player.isPlaying)
					{
						if (this.GetButtonDown(player, this.submitActionId))
						{
							ExecuteEvents.Execute<ISubmitHandler>(base.eventSystem.currentSelectedGameObject, baseEventData, ExecuteEvents.submitHandler);
							break;
						}
						if (this.GetButtonDown(player, this.cancelActionId))
						{
							ExecuteEvents.Execute<ICancelHandler>(base.eventSystem.currentSelectedGameObject, baseEventData, ExecuteEvents.cancelHandler);
							break;
						}
					}
				}
			}
			return baseEventData.used;
		}

		// Token: 0x06001DF0 RID: 7664 RVA: 0x000ADF8C File Offset: 0x000AC38C
		private Vector2 GetRawMoveVector()
		{
			if (this.recompiling)
			{
				return Vector2.zero;
			}
			Vector2 zero = Vector2.zero;
			bool flag = false;
			bool flag2 = false;
			for (int i = 0; i < this.playerIds.Length; i++)
			{
				Player player = ReInput.players.GetPlayer(this.playerIds[i]);
				if (player != null)
				{
					if (!this.usePlayingPlayersOnly || player.isPlaying)
					{
						if (this.moveOneElementPerAxisPress)
						{
							float num = 0f;
							if (this.GetButtonDown(player, this.horizontalActionId))
							{
								num = 1f;
							}
							else if (this.GetNegativeButtonDown(player, this.horizontalActionId))
							{
								num = -1f;
							}
							float num2 = 0f;
							if (this.GetButtonDown(player, this.verticalActionId))
							{
								num2 = 1f;
							}
							else if (this.GetNegativeButtonDown(player, this.verticalActionId))
							{
								num2 = -1f;
							}
							zero.x += num;
							zero.y += num2;
						}
						else
						{
							zero.x += this.GetAxis(player, this.horizontalActionId);
							zero.y += this.GetAxis(player, this.verticalActionId);
						}
						flag |= (this.GetButtonDown(player, this.horizontalActionId) || this.GetNegativeButtonDown(player, this.horizontalActionId));
						flag2 |= (this.GetButtonDown(player, this.verticalActionId) || this.GetNegativeButtonDown(player, this.verticalActionId));
					}
				}
			}
			if (flag)
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
			if (flag2)
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
			return zero;
		}

		// Token: 0x06001DF1 RID: 7665 RVA: 0x000AE1C0 File Offset: 0x000AC5C0
		private bool SendMoveEventToSelectedObject()
		{
			if (this.recompiling)
			{
				return false;
			}
			float unscaledTime = Time.unscaledTime;
			Vector2 rawMoveVector = this.GetRawMoveVector();
			if (Mathf.Approximately(rawMoveVector.x, 0f) && Mathf.Approximately(rawMoveVector.y, 0f))
			{
				this.m_ConsecutiveMoveCount = 0;
				return false;
			}
			bool flag = Vector2.Dot(rawMoveVector, this.m_LastMoveVector) > 0f;
			bool flag2;
			bool flag3;
			this.CheckButtonOrKeyMovement(unscaledTime, out flag2, out flag3);
			AxisEventData axisEventData = null;
			bool flag4 = flag2 || flag3;
			if (flag4)
			{
				axisEventData = this.GetAxisEventData(rawMoveVector.x, rawMoveVector.y, 0.6f);
				MoveDirection moveDir = axisEventData.moveDir;
				flag4 = (((moveDir == MoveDirection.Up || moveDir == MoveDirection.Down) && flag3) || ((moveDir == MoveDirection.Left || moveDir == MoveDirection.Right) && flag2));
			}
			if (!flag4)
			{
				if (this.m_RepeatDelay > 0f)
				{
					if (flag && this.m_ConsecutiveMoveCount == 1)
					{
						flag4 = (unscaledTime > this.m_PrevActionTime + this.m_RepeatDelay);
					}
					else
					{
						flag4 = (unscaledTime > this.m_PrevActionTime + 1f / this.m_InputActionsPerSecond);
					}
				}
				else
				{
					flag4 = (unscaledTime > this.m_PrevActionTime + 1f / this.m_InputActionsPerSecond);
				}
			}
			if (!flag4)
			{
				return false;
			}
			if (axisEventData == null)
			{
				axisEventData = this.GetAxisEventData(rawMoveVector.x, rawMoveVector.y, 0.6f);
			}
			if (axisEventData.moveDir != MoveDirection.None)
			{
				ExecuteEvents.Execute<IMoveHandler>(base.eventSystem.currentSelectedGameObject, axisEventData, ExecuteEvents.moveHandler);
				if (!flag)
				{
					this.m_ConsecutiveMoveCount = 0;
				}
				if (this.m_ConsecutiveMoveCount == 0 || (!flag2 && !flag3))
				{
					this.m_ConsecutiveMoveCount++;
				}
				this.m_PrevActionTime = unscaledTime;
				this.m_LastMoveVector = rawMoveVector;
			}
			else
			{
				this.m_ConsecutiveMoveCount = 0;
			}
			return axisEventData.used;
		}

		// Token: 0x06001DF2 RID: 7666 RVA: 0x000AE3C0 File Offset: 0x000AC7C0
		private void CheckButtonOrKeyMovement(float time, out bool downHorizontal, out bool downVertical)
		{
			downHorizontal = false;
			downVertical = false;
			for (int i = 0; i < this.playerIds.Length; i++)
			{
				Player player = ReInput.players.GetPlayer(this.playerIds[i]);
				if (player != null)
				{
					if (!this.usePlayingPlayersOnly || player.isPlaying)
					{
						downHorizontal |= (this.GetButtonDown(player, this.horizontalActionId) || this.GetNegativeButtonDown(player, this.horizontalActionId));
						downVertical |= (this.GetButtonDown(player, this.verticalActionId) || this.GetNegativeButtonDown(player, this.verticalActionId));
					}
				}
			}
		}

		// Token: 0x06001DF3 RID: 7667 RVA: 0x000AE474 File Offset: 0x000AC874
		private void ProcessMouseEvents()
		{
			for (int i = 0; i < this.playerIds.Length; i++)
			{
				Player player = ReInput.players.GetPlayer(this.playerIds[i]);
				if (player != null)
				{
					if (!this.usePlayingPlayersOnly || player.isPlaying)
					{
						int mouseInputSourceCount = base.GetMouseInputSourceCount(this.playerIds[i]);
						for (int j = 0; j < mouseInputSourceCount; j++)
						{
							this.ProcessMouseEvent(this.playerIds[i], j);
						}
					}
				}
			}
		}

		// Token: 0x06001DF4 RID: 7668 RVA: 0x000AE504 File Offset: 0x000AC904
		private void ProcessMouseEvent(int playerId, int pointerIndex)
		{
			RewiredPointerInputModule.MouseState mousePointerEventData = this.GetMousePointerEventData(playerId, pointerIndex);
			if (mousePointerEventData == null)
			{
				return;
			}
			RewiredPointerInputModule.MouseButtonEventData eventData = mousePointerEventData.GetButtonState(0).eventData;
			this.ProcessMousePress(eventData);
			this.ProcessMove(eventData.buttonData);
			this.ProcessDrag(eventData.buttonData);
			this.ProcessMousePress(mousePointerEventData.GetButtonState(1).eventData);
			this.ProcessDrag(mousePointerEventData.GetButtonState(1).eventData.buttonData);
			this.ProcessMousePress(mousePointerEventData.GetButtonState(2).eventData);
			this.ProcessDrag(mousePointerEventData.GetButtonState(2).eventData.buttonData);
			IMouseInputSource mouseInputSource = base.GetMouseInputSource(playerId, pointerIndex);
			for (int i = 3; i < mouseInputSource.buttonCount; i++)
			{
				this.ProcessMousePress(mousePointerEventData.GetButtonState(i).eventData);
				this.ProcessDrag(mousePointerEventData.GetButtonState(i).eventData.buttonData);
			}
			if (!Mathf.Approximately(eventData.buttonData.scrollDelta.sqrMagnitude, 0f))
			{
				GameObject eventHandler = ExecuteEvents.GetEventHandler<IScrollHandler>(eventData.buttonData.pointerCurrentRaycast.gameObject);
				ExecuteEvents.ExecuteHierarchy<IScrollHandler>(eventHandler, eventData.buttonData, ExecuteEvents.scrollHandler);
			}
		}

		// Token: 0x06001DF5 RID: 7669 RVA: 0x000AE63C File Offset: 0x000ACA3C
		private bool SendUpdateEventToSelectedObject()
		{
			if (base.eventSystem.currentSelectedGameObject == null)
			{
				return false;
			}
			BaseEventData baseEventData = this.GetBaseEventData();
			ExecuteEvents.Execute<IUpdateSelectedHandler>(base.eventSystem.currentSelectedGameObject, baseEventData, ExecuteEvents.updateSelectedHandler);
			return baseEventData.used;
		}

		// Token: 0x06001DF6 RID: 7670 RVA: 0x000AE688 File Offset: 0x000ACA88
		private void ProcessMousePress(RewiredPointerInputModule.MouseButtonEventData data)
		{
			PlayerPointerEventData buttonData = data.buttonData;
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

		// Token: 0x06001DF7 RID: 7671 RVA: 0x000AE8AC File Offset: 0x000ACCAC
		private void OnApplicationFocus(bool hasFocus)
		{
			this.m_HasFocus = hasFocus;
		}

		// Token: 0x06001DF8 RID: 7672 RVA: 0x000AE8B5 File Offset: 0x000ACCB5
		private bool ShouldIgnoreEventsOnNoFocus()
		{
			return !ReInput.isReady || ReInput.configuration.ignoreInputWhenAppNotInFocus;
		}

		// Token: 0x06001DF9 RID: 7673 RVA: 0x000AE8CD File Offset: 0x000ACCCD
		protected override void OnDestroy()
		{
			base.OnDestroy();
			ReInput.InitializedEvent -= this.OnRewiredInitialized;
		}

		// Token: 0x06001DFA RID: 7674 RVA: 0x000AE8E8 File Offset: 0x000ACCE8
		protected override bool IsDefaultPlayer(int playerId)
		{
			if (this.playerIds == null)
			{
				return false;
			}
			if (!ReInput.isReady)
			{
				return false;
			}
			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < this.playerIds.Length; j++)
				{
					Player player = ReInput.players.GetPlayer(this.playerIds[j]);
					if (player != null)
					{
						if (i >= 1 || !this.usePlayingPlayersOnly || player.isPlaying)
						{
							if (i >= 2 || player.controllers.hasMouse)
							{
								return this.playerIds[j] == playerId;
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06001DFB RID: 7675 RVA: 0x000AE9A4 File Offset: 0x000ACDA4
		private void InitializeRewired()
		{
			if (!ReInput.isReady)
			{
				UnityEngine.Debug.LogError("Rewired is not initialized! Are you missing a Rewired Input Manager in your scene?");
				return;
			}
			ReInput.ShutDownEvent -= this.OnRewiredShutDown;
			ReInput.ShutDownEvent += this.OnRewiredShutDown;
			ReInput.EditorRecompileEvent -= this.OnEditorRecompile;
			ReInput.EditorRecompileEvent += this.OnEditorRecompile;
			this.SetupRewiredVars();
		}

		// Token: 0x06001DFC RID: 7676 RVA: 0x000AEA10 File Offset: 0x000ACE10
		private void SetupRewiredVars()
		{
			if (!ReInput.isReady)
			{
				return;
			}
			this.SetUpRewiredActions();
			if (this.useAllRewiredGamePlayers)
			{
				IList<Player> list = (!this.useRewiredSystemPlayer) ? ReInput.players.Players : ReInput.players.AllPlayers;
				this.playerIds = new int[list.Count];
				for (int i = 0; i < list.Count; i++)
				{
					this.playerIds[i] = list[i].id;
				}
			}
			else
			{
				bool flag = false;
				List<int> list2 = new List<int>(this.rewiredPlayerIds.Length + 1);
				for (int j = 0; j < this.rewiredPlayerIds.Length; j++)
				{
					Player player = ReInput.players.GetPlayer(this.rewiredPlayerIds[j]);
					if (player != null)
					{
						if (!list2.Contains(player.id))
						{
							list2.Add(player.id);
							if (player.id == 9999999)
							{
								flag = true;
							}
						}
					}
				}
				if (this.useRewiredSystemPlayer && !flag)
				{
					list2.Insert(0, ReInput.players.GetSystemPlayer().id);
				}
				this.playerIds = list2.ToArray();
			}
			this.SetUpRewiredPlayerMice();
		}

		// Token: 0x06001DFD RID: 7677 RVA: 0x000AEB60 File Offset: 0x000ACF60
		private void SetUpRewiredPlayerMice()
		{
			if (!ReInput.isReady)
			{
				return;
			}
			base.ClearMouseInputSources();
			for (int i = 0; i < this.playerMice.Count; i++)
			{
				PlayerMouse playerMouse = this.playerMice[i];
				if (!UnityTools.IsNullOrDestroyed<PlayerMouse>(playerMouse))
				{
					base.AddMouseInputSource(playerMouse);
				}
			}
		}

		// Token: 0x06001DFE RID: 7678 RVA: 0x000AEBC0 File Offset: 0x000ACFC0
		private void SetUpRewiredActions()
		{
			if (!ReInput.isReady)
			{
				return;
			}
			if (!this.setActionsById)
			{
				this.horizontalActionId = ReInput.mapping.GetActionId(this.m_HorizontalAxis);
				this.verticalActionId = ReInput.mapping.GetActionId(this.m_VerticalAxis);
				this.submitActionId = ReInput.mapping.GetActionId(this.m_SubmitButton);
				this.cancelActionId = ReInput.mapping.GetActionId(this.m_CancelButton);
			}
			else
			{
				InputAction action = ReInput.mapping.GetAction(this.horizontalActionId);
				this.m_HorizontalAxis = ((action == null) ? string.Empty : action.name);
				if (action == null)
				{
					this.horizontalActionId = -1;
				}
				action = ReInput.mapping.GetAction(this.verticalActionId);
				this.m_VerticalAxis = ((action == null) ? string.Empty : action.name);
				if (action == null)
				{
					this.verticalActionId = -1;
				}
				action = ReInput.mapping.GetAction(this.submitActionId);
				this.m_SubmitButton = ((action == null) ? string.Empty : action.name);
				if (action == null)
				{
					this.submitActionId = -1;
				}
				action = ReInput.mapping.GetAction(this.cancelActionId);
				this.m_CancelButton = ((action == null) ? string.Empty : action.name);
				if (action == null)
				{
					this.cancelActionId = -1;
				}
			}
		}

		// Token: 0x06001DFF RID: 7679 RVA: 0x000AED28 File Offset: 0x000AD128
		private bool GetButtonDown(Player player, int actionId)
		{
			return actionId >= 0 && player.GetButtonDown(actionId);
		}

		// Token: 0x06001E00 RID: 7680 RVA: 0x000AED3A File Offset: 0x000AD13A
		private bool GetNegativeButtonDown(Player player, int actionId)
		{
			return actionId >= 0 && player.GetNegativeButtonDown(actionId);
		}

		// Token: 0x06001E01 RID: 7681 RVA: 0x000AED4C File Offset: 0x000AD14C
		private float GetAxis(Player player, int actionId)
		{
			if (actionId < 0)
			{
				return 0f;
			}
			return player.GetAxis(actionId);
		}

		// Token: 0x06001E02 RID: 7682 RVA: 0x000AED62 File Offset: 0x000AD162
		private void CheckEditorRecompile()
		{
			if (!this.recompiling)
			{
				return;
			}
			if (!ReInput.isReady)
			{
				return;
			}
			this.recompiling = false;
			this.InitializeRewired();
		}

		// Token: 0x06001E03 RID: 7683 RVA: 0x000AED88 File Offset: 0x000AD188
		private void OnEditorRecompile()
		{
			this.recompiling = true;
			this.ClearRewiredVars();
		}

		// Token: 0x06001E04 RID: 7684 RVA: 0x000AED97 File Offset: 0x000AD197
		private void ClearRewiredVars()
		{
			Array.Clear(this.playerIds, 0, this.playerIds.Length);
			base.ClearMouseInputSources();
		}

		// Token: 0x06001E05 RID: 7685 RVA: 0x000AEDB4 File Offset: 0x000AD1B4
		private bool DidAnyMouseMove()
		{
			for (int i = 0; i < this.playerIds.Length; i++)
			{
				int playerId = this.playerIds[i];
				Player player = ReInput.players.GetPlayer(playerId);
				if (player != null)
				{
					if (!this.usePlayingPlayersOnly || player.isPlaying)
					{
						int mouseInputSourceCount = base.GetMouseInputSourceCount(playerId);
						for (int j = 0; j < mouseInputSourceCount; j++)
						{
							IMouseInputSource mouseInputSource = base.GetMouseInputSource(playerId, j);
							if (mouseInputSource != null)
							{
								if (mouseInputSource.screenPositionDelta.sqrMagnitude > 0f)
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

		// Token: 0x06001E06 RID: 7686 RVA: 0x000AEE68 File Offset: 0x000AD268
		private bool GetMouseButtonDownOnAnyMouse(int buttonIndex)
		{
			for (int i = 0; i < this.playerIds.Length; i++)
			{
				int playerId = this.playerIds[i];
				Player player = ReInput.players.GetPlayer(playerId);
				if (player != null)
				{
					if (!this.usePlayingPlayersOnly || player.isPlaying)
					{
						int mouseInputSourceCount = base.GetMouseInputSourceCount(playerId);
						for (int j = 0; j < mouseInputSourceCount; j++)
						{
							IMouseInputSource mouseInputSource = base.GetMouseInputSource(playerId, j);
							if (mouseInputSource != null)
							{
								if (mouseInputSource.GetButtonDown(buttonIndex))
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

		// Token: 0x06001E07 RID: 7687 RVA: 0x000AEF0E File Offset: 0x000AD30E
		private void OnRewiredInitialized()
		{
			this.InitializeRewired();
		}

		// Token: 0x06001E08 RID: 7688 RVA: 0x000AEF16 File Offset: 0x000AD316
		private void OnRewiredShutDown()
		{
			this.ClearRewiredVars();
		}

		// Token: 0x04001E68 RID: 7784
		private const string DEFAULT_ACTION_MOVE_HORIZONTAL = "UIHorizontal";

		// Token: 0x04001E69 RID: 7785
		private const string DEFAULT_ACTION_MOVE_VERTICAL = "UIVertical";

		// Token: 0x04001E6A RID: 7786
		private const string DEFAULT_ACTION_SUBMIT = "UISubmit";

		// Token: 0x04001E6B RID: 7787
		private const string DEFAULT_ACTION_CANCEL = "UICancel";

		// Token: 0x04001E6C RID: 7788
		[Tooltip("(Optional) Link the Rewired Input Manager here for easier access to Player ids, etc.")]
		[SerializeField]
		private InputManager_Base rewiredInputManager;

		// Token: 0x04001E6D RID: 7789
		[SerializeField]
		[Tooltip("Use all Rewired game Players to control the UI. This does not include the System Player. If enabled, this setting overrides individual Player Ids set in Rewired Player Ids.")]
		private bool useAllRewiredGamePlayers;

		// Token: 0x04001E6E RID: 7790
		[SerializeField]
		[Tooltip("Allow the Rewired System Player to control the UI.")]
		private bool useRewiredSystemPlayer;

		// Token: 0x04001E6F RID: 7791
		[SerializeField]
		[Tooltip("A list of Player Ids that are allowed to control the UI. If Use All Rewired Game Players = True, this list will be ignored.")]
		private int[] rewiredPlayerIds = new int[1];

		// Token: 0x04001E70 RID: 7792
		[SerializeField]
		[Tooltip("Allow only Players with Player.isPlaying = true to control the UI.")]
		private bool usePlayingPlayersOnly;

		// Token: 0x04001E71 RID: 7793
		[SerializeField]
		[Tooltip("Player Mice allowed to interact with the UI. Each Player that owns a Player Mouse must also be allowed to control the UI or the Player Mouse will not function.")]
		private List<PlayerMouse> playerMice = new List<PlayerMouse>();

		// Token: 0x04001E72 RID: 7794
		[SerializeField]
		[Tooltip("Makes an axis press always move only one UI selection. Enable if you do not want to allow scrolling through UI elements by holding an axis direction.")]
		private bool moveOneElementPerAxisPress;

		// Token: 0x04001E73 RID: 7795
		[SerializeField]
		[Tooltip("If enabled, Action Ids will be used to set the Actions. If disabled, string names will be used to set the Actions.")]
		private bool setActionsById;

		// Token: 0x04001E74 RID: 7796
		[SerializeField]
		[Tooltip("Id of the horizontal Action for movement (if axis events are used).")]
		private int horizontalActionId = -1;

		// Token: 0x04001E75 RID: 7797
		[SerializeField]
		[Tooltip("Id of the vertical Action for movement (if axis events are used).")]
		private int verticalActionId = -1;

		// Token: 0x04001E76 RID: 7798
		[SerializeField]
		[Tooltip("Id of the Action used to submit.")]
		private int submitActionId = -1;

		// Token: 0x04001E77 RID: 7799
		[SerializeField]
		[Tooltip("Id of the Action used to cancel.")]
		private int cancelActionId = -1;

		// Token: 0x04001E78 RID: 7800
		[SerializeField]
		[Tooltip("Name of the horizontal axis for movement (if axis events are used).")]
		private string m_HorizontalAxis = "UIHorizontal";

		// Token: 0x04001E79 RID: 7801
		[SerializeField]
		[Tooltip("Name of the vertical axis for movement (if axis events are used).")]
		private string m_VerticalAxis = "UIVertical";

		// Token: 0x04001E7A RID: 7802
		[SerializeField]
		[Tooltip("Name of the action used to submit.")]
		private string m_SubmitButton = "UISubmit";

		// Token: 0x04001E7B RID: 7803
		[SerializeField]
		[Tooltip("Name of the action used to cancel.")]
		private string m_CancelButton = "UICancel";

		// Token: 0x04001E7C RID: 7804
		[SerializeField]
		[Tooltip("Number of selection changes allowed per second when a movement button/axis is held in a direction.")]
		private float m_InputActionsPerSecond = 10f;

		// Token: 0x04001E7D RID: 7805
		[SerializeField]
		[Tooltip("Delay in seconds before vertical/horizontal movement starts repeating continouously when a movement direction is held.")]
		private float m_RepeatDelay;

		// Token: 0x04001E7E RID: 7806
		[SerializeField]
		[Tooltip("Allows the mouse to be used to select elements.")]
		private bool m_allowMouseInput = true;

		// Token: 0x04001E7F RID: 7807
		[SerializeField]
		[Tooltip("Allows the mouse to be used to select elements if the device also supports touch control.")]
		private bool m_allowMouseInputIfTouchSupported = true;

		// Token: 0x04001E80 RID: 7808
		[SerializeField]
		[Tooltip("Allows touch input to be used to select elements.")]
		private bool m_allowTouchInput = true;

		// Token: 0x04001E81 RID: 7809
		[SerializeField]
		[FormerlySerializedAs("m_AllowActivationOnMobileDevice")]
		[Tooltip("Forces the module to always be active.")]
		private bool m_ForceModuleActive;

		// Token: 0x04001E82 RID: 7810
		[NonSerialized]
		private int[] playerIds;

		// Token: 0x04001E83 RID: 7811
		private bool recompiling;

		// Token: 0x04001E84 RID: 7812
		[NonSerialized]
		private bool isTouchSupported;

		// Token: 0x04001E85 RID: 7813
		[NonSerialized]
		private float m_PrevActionTime;

		// Token: 0x04001E86 RID: 7814
		[NonSerialized]
		private Vector2 m_LastMoveVector;

		// Token: 0x04001E87 RID: 7815
		[NonSerialized]
		private int m_ConsecutiveMoveCount;

		// Token: 0x04001E88 RID: 7816
		[NonSerialized]
		private bool m_HasFocus = true;

		// Token: 0x02000576 RID: 1398
		[Serializable]
		public class PlayerSetting
		{
			// Token: 0x06001E09 RID: 7689 RVA: 0x000AEF1E File Offset: 0x000AD31E
			public PlayerSetting()
			{
			}

			// Token: 0x06001E0A RID: 7690 RVA: 0x000AEF34 File Offset: 0x000AD334
			private PlayerSetting(RewiredStandaloneInputModule.PlayerSetting other)
			{
				if (other == null)
				{
					throw new ArgumentNullException("other");
				}
				this.playerId = other.playerId;
				this.playerMice = new List<PlayerMouse>();
				if (other.playerMice != null)
				{
					foreach (PlayerMouse item in other.playerMice)
					{
						this.playerMice.Add(item);
					}
				}
			}

			// Token: 0x06001E0B RID: 7691 RVA: 0x000AEFDC File Offset: 0x000AD3DC
			public RewiredStandaloneInputModule.PlayerSetting Clone()
			{
				return new RewiredStandaloneInputModule.PlayerSetting(this);
			}

			// Token: 0x04001E89 RID: 7817
			public int playerId;

			// Token: 0x04001E8A RID: 7818
			public List<PlayerMouse> playerMice = new List<PlayerMouse>();
		}
	}
}
