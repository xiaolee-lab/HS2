using System;
using System.Collections.Generic;
using System.Text;
using Rewired.UI;
using Rewired.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Rewired.Integration.UnityUI
{
	// Token: 0x0200056F RID: 1391
	public abstract class RewiredPointerInputModule : BaseInputModule
	{
		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06001D7A RID: 7546 RVA: 0x000AC338 File Offset: 0x000AA738
		private RewiredPointerInputModule.UnityInputSource defaultInputSource
		{
			get
			{
				return (this.__m_DefaultInputSource == null) ? (this.__m_DefaultInputSource = new RewiredPointerInputModule.UnityInputSource()) : this.__m_DefaultInputSource;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06001D7B RID: 7547 RVA: 0x000AC369 File Offset: 0x000AA769
		private IMouseInputSource defaultMouseInputSource
		{
			get
			{
				return this.defaultInputSource;
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06001D7C RID: 7548 RVA: 0x000AC371 File Offset: 0x000AA771
		protected ITouchInputSource defaultTouchInputSource
		{
			get
			{
				return this.defaultInputSource;
			}
		}

		// Token: 0x06001D7D RID: 7549 RVA: 0x000AC379 File Offset: 0x000AA779
		protected bool IsDefaultMouse(IMouseInputSource mouse)
		{
			return this.defaultMouseInputSource == mouse;
		}

		// Token: 0x06001D7E RID: 7550 RVA: 0x000AC384 File Offset: 0x000AA784
		public IMouseInputSource GetMouseInputSource(int playerId, int mouseIndex)
		{
			if (mouseIndex < 0)
			{
				throw new ArgumentOutOfRangeException("mouseIndex");
			}
			if (this.m_MouseInputSourcesList.Count == 0 && this.IsDefaultPlayer(playerId))
			{
				return this.defaultMouseInputSource;
			}
			int count = this.m_MouseInputSourcesList.Count;
			int num = 0;
			for (int i = 0; i < count; i++)
			{
				IMouseInputSource mouseInputSource = this.m_MouseInputSourcesList[i];
				if (!UnityTools.IsNullOrDestroyed<IMouseInputSource>(mouseInputSource))
				{
					if (mouseInputSource.playerId == playerId)
					{
						if (mouseIndex == num)
						{
							return mouseInputSource;
						}
						num++;
					}
				}
			}
			return null;
		}

		// Token: 0x06001D7F RID: 7551 RVA: 0x000AC422 File Offset: 0x000AA822
		public void RemoveMouseInputSource(IMouseInputSource source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			this.m_MouseInputSourcesList.Remove(source);
		}

		// Token: 0x06001D80 RID: 7552 RVA: 0x000AC442 File Offset: 0x000AA842
		public void AddMouseInputSource(IMouseInputSource source)
		{
			if (UnityTools.IsNullOrDestroyed<IMouseInputSource>(source))
			{
				throw new ArgumentNullException("source");
			}
			this.m_MouseInputSourcesList.Add(source);
		}

		// Token: 0x06001D81 RID: 7553 RVA: 0x000AC468 File Offset: 0x000AA868
		public int GetMouseInputSourceCount(int playerId)
		{
			if (this.m_MouseInputSourcesList.Count == 0 && this.IsDefaultPlayer(playerId))
			{
				return 1;
			}
			int count = this.m_MouseInputSourcesList.Count;
			int num = 0;
			for (int i = 0; i < count; i++)
			{
				IMouseInputSource mouseInputSource = this.m_MouseInputSourcesList[i];
				if (!UnityTools.IsNullOrDestroyed<IMouseInputSource>(mouseInputSource))
				{
					if (mouseInputSource.playerId == playerId)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x06001D82 RID: 7554 RVA: 0x000AC4E6 File Offset: 0x000AA8E6
		public ITouchInputSource GetTouchInputSource(int playerId, int sourceIndex)
		{
			if (!UnityTools.IsNullOrDestroyed<ITouchInputSource>(this.m_UserDefaultTouchInputSource))
			{
				return this.m_UserDefaultTouchInputSource;
			}
			return this.defaultTouchInputSource;
		}

		// Token: 0x06001D83 RID: 7555 RVA: 0x000AC505 File Offset: 0x000AA905
		public void RemoveTouchInputSource(ITouchInputSource source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (this.m_UserDefaultTouchInputSource == source)
			{
				this.m_UserDefaultTouchInputSource = null;
			}
		}

		// Token: 0x06001D84 RID: 7556 RVA: 0x000AC52B File Offset: 0x000AA92B
		public void AddTouchInputSource(ITouchInputSource source)
		{
			if (UnityTools.IsNullOrDestroyed<ITouchInputSource>(source))
			{
				throw new ArgumentNullException("source");
			}
			this.m_UserDefaultTouchInputSource = source;
		}

		// Token: 0x06001D85 RID: 7557 RVA: 0x000AC54A File Offset: 0x000AA94A
		public int GetTouchInputSourceCount(int playerId)
		{
			return (!this.IsDefaultPlayer(playerId)) ? 0 : 1;
		}

		// Token: 0x06001D86 RID: 7558 RVA: 0x000AC55F File Offset: 0x000AA95F
		protected void ClearMouseInputSources()
		{
			this.m_MouseInputSourcesList.Clear();
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06001D87 RID: 7559 RVA: 0x000AC56C File Offset: 0x000AA96C
		protected virtual bool isMouseSupported
		{
			get
			{
				int count = this.m_MouseInputSourcesList.Count;
				if (count == 0)
				{
					return this.defaultMouseInputSource.enabled;
				}
				for (int i = 0; i < count; i++)
				{
					if (this.m_MouseInputSourcesList[i].enabled)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x06001D88 RID: 7560
		protected abstract bool IsDefaultPlayer(int playerId);

		// Token: 0x06001D89 RID: 7561 RVA: 0x000AC5C4 File Offset: 0x000AA9C4
		protected bool GetPointerData(int playerId, int pointerIndex, int pointerTypeId, out PlayerPointerEventData data, bool create, PointerEventType pointerEventType)
		{
			Dictionary<int, PlayerPointerEventData>[] array;
			if (!this.m_PlayerPointerData.TryGetValue(playerId, out array))
			{
				array = new Dictionary<int, PlayerPointerEventData>[pointerIndex + 1];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = new Dictionary<int, PlayerPointerEventData>();
				}
				this.m_PlayerPointerData.Add(playerId, array);
			}
			if (pointerIndex >= array.Length)
			{
				Dictionary<int, PlayerPointerEventData>[] array2 = new Dictionary<int, PlayerPointerEventData>[pointerIndex + 1];
				for (int j = 0; j < array.Length; j++)
				{
					array2[j] = array[j];
				}
				array2[pointerIndex] = new Dictionary<int, PlayerPointerEventData>();
				array = array2;
				this.m_PlayerPointerData[playerId] = array;
			}
			Dictionary<int, PlayerPointerEventData> dictionary = array[pointerIndex];
			if (!dictionary.TryGetValue(pointerTypeId, out data) && create)
			{
				data = this.CreatePointerEventData(playerId, pointerIndex, pointerTypeId, pointerEventType);
				dictionary.Add(pointerTypeId, data);
				return true;
			}
			data.mouseSource = ((pointerEventType != PointerEventType.Mouse) ? null : this.GetMouseInputSource(playerId, pointerIndex));
			data.touchSource = ((pointerEventType != PointerEventType.Touch) ? null : this.GetTouchInputSource(playerId, pointerIndex));
			return false;
		}

		// Token: 0x06001D8A RID: 7562 RVA: 0x000AC6CC File Offset: 0x000AAACC
		private PlayerPointerEventData CreatePointerEventData(int playerId, int pointerIndex, int pointerTypeId, PointerEventType pointerEventType)
		{
			PlayerPointerEventData playerPointerEventData = new PlayerPointerEventData(base.eventSystem)
			{
				playerId = playerId,
				inputSourceIndex = pointerIndex,
				pointerId = pointerTypeId,
				sourceType = pointerEventType
			};
			if (pointerEventType == PointerEventType.Mouse)
			{
				playerPointerEventData.mouseSource = this.GetMouseInputSource(playerId, pointerIndex);
			}
			else if (pointerEventType == PointerEventType.Touch)
			{
				playerPointerEventData.touchSource = this.GetTouchInputSource(playerId, pointerIndex);
			}
			if (pointerTypeId == -1)
			{
				playerPointerEventData.buttonIndex = 0;
			}
			else if (pointerTypeId == -2)
			{
				playerPointerEventData.buttonIndex = 1;
			}
			else if (pointerTypeId == -3)
			{
				playerPointerEventData.buttonIndex = 2;
			}
			else if (pointerTypeId >= -2147483520 && pointerTypeId <= -2147483392)
			{
				playerPointerEventData.buttonIndex = pointerTypeId - -2147483520;
			}
			return playerPointerEventData;
		}

		// Token: 0x06001D8B RID: 7563 RVA: 0x000AC794 File Offset: 0x000AAB94
		protected void RemovePointerData(PlayerPointerEventData data)
		{
			Dictionary<int, PlayerPointerEventData>[] array;
			if (this.m_PlayerPointerData.TryGetValue(data.playerId, out array) && data.inputSourceIndex < array.Length)
			{
				array[data.inputSourceIndex].Remove(data.pointerId);
			}
		}

		// Token: 0x06001D8C RID: 7564 RVA: 0x000AC7DC File Offset: 0x000AABDC
		protected PlayerPointerEventData GetTouchPointerEventData(int playerId, int touchDeviceIndex, Touch input, out bool pressed, out bool released)
		{
			PlayerPointerEventData playerPointerEventData;
			bool pointerData = this.GetPointerData(playerId, touchDeviceIndex, input.fingerId, out playerPointerEventData, true, PointerEventType.Touch);
			playerPointerEventData.Reset();
			pressed = (pointerData || input.phase == TouchPhase.Began);
			released = (input.phase == TouchPhase.Canceled || input.phase == TouchPhase.Ended);
			if (pointerData)
			{
				playerPointerEventData.position = input.position;
			}
			if (pressed)
			{
				playerPointerEventData.delta = Vector2.zero;
			}
			else
			{
				playerPointerEventData.delta = input.position - playerPointerEventData.position;
			}
			playerPointerEventData.position = input.position;
			playerPointerEventData.button = PointerEventData.InputButton.Left;
			base.eventSystem.RaycastAll(playerPointerEventData, this.m_RaycastResultCache);
			RaycastResult pointerCurrentRaycast = BaseInputModule.FindFirstRaycast(this.m_RaycastResultCache);
			playerPointerEventData.pointerCurrentRaycast = pointerCurrentRaycast;
			this.m_RaycastResultCache.Clear();
			return playerPointerEventData;
		}

		// Token: 0x06001D8D RID: 7565 RVA: 0x000AC8C0 File Offset: 0x000AACC0
		protected virtual RewiredPointerInputModule.MouseState GetMousePointerEventData(int playerId, int mouseIndex)
		{
			IMouseInputSource mouseInputSource = this.GetMouseInputSource(playerId, mouseIndex);
			if (mouseInputSource == null)
			{
				return null;
			}
			PlayerPointerEventData playerPointerEventData;
			bool pointerData = this.GetPointerData(playerId, mouseIndex, -1, out playerPointerEventData, true, PointerEventType.Mouse);
			playerPointerEventData.Reset();
			if (pointerData)
			{
				playerPointerEventData.position = mouseInputSource.screenPosition;
			}
			Vector2 screenPosition = mouseInputSource.screenPosition;
			if (mouseInputSource.locked)
			{
				playerPointerEventData.position = new Vector2(-1f, -1f);
				playerPointerEventData.delta = Vector2.zero;
			}
			else
			{
				playerPointerEventData.delta = screenPosition - playerPointerEventData.position;
				playerPointerEventData.position = screenPosition;
			}
			playerPointerEventData.scrollDelta = mouseInputSource.wheelDelta;
			playerPointerEventData.button = PointerEventData.InputButton.Left;
			base.eventSystem.RaycastAll(playerPointerEventData, this.m_RaycastResultCache);
			RaycastResult pointerCurrentRaycast = BaseInputModule.FindFirstRaycast(this.m_RaycastResultCache);
			playerPointerEventData.pointerCurrentRaycast = pointerCurrentRaycast;
			this.m_RaycastResultCache.Clear();
			PlayerPointerEventData playerPointerEventData2;
			this.GetPointerData(playerId, mouseIndex, -2, out playerPointerEventData2, true, PointerEventType.Mouse);
			this.CopyFromTo(playerPointerEventData, playerPointerEventData2);
			playerPointerEventData2.button = PointerEventData.InputButton.Right;
			PlayerPointerEventData playerPointerEventData3;
			this.GetPointerData(playerId, mouseIndex, -3, out playerPointerEventData3, true, PointerEventType.Mouse);
			this.CopyFromTo(playerPointerEventData, playerPointerEventData3);
			playerPointerEventData3.button = PointerEventData.InputButton.Middle;
			for (int i = 3; i < mouseInputSource.buttonCount; i++)
			{
				PlayerPointerEventData playerPointerEventData4;
				this.GetPointerData(playerId, mouseIndex, -2147483520 + i, out playerPointerEventData4, true, PointerEventType.Mouse);
				this.CopyFromTo(playerPointerEventData, playerPointerEventData4);
				playerPointerEventData4.button = (PointerEventData.InputButton)(-1);
			}
			this.m_MouseState.SetButtonState(0, this.StateForMouseButton(playerId, mouseIndex, 0), playerPointerEventData);
			this.m_MouseState.SetButtonState(1, this.StateForMouseButton(playerId, mouseIndex, 1), playerPointerEventData2);
			this.m_MouseState.SetButtonState(2, this.StateForMouseButton(playerId, mouseIndex, 2), playerPointerEventData3);
			for (int j = 3; j < mouseInputSource.buttonCount; j++)
			{
				PlayerPointerEventData data;
				this.GetPointerData(playerId, mouseIndex, -2147483520 + j, out data, false, PointerEventType.Mouse);
				this.m_MouseState.SetButtonState(j, this.StateForMouseButton(playerId, mouseIndex, j), data);
			}
			return this.m_MouseState;
		}

		// Token: 0x06001D8E RID: 7566 RVA: 0x000ACAB0 File Offset: 0x000AAEB0
		protected PlayerPointerEventData GetLastPointerEventData(int playerId, int pointerIndex, int pointerTypeId, bool ignorePointerTypeId, PointerEventType pointerEventType)
		{
			if (!ignorePointerTypeId)
			{
				PlayerPointerEventData result;
				this.GetPointerData(playerId, pointerIndex, pointerTypeId, out result, false, pointerEventType);
				return result;
			}
			Dictionary<int, PlayerPointerEventData>[] array;
			if (!this.m_PlayerPointerData.TryGetValue(playerId, out array))
			{
				return null;
			}
			if (pointerIndex >= array.Length)
			{
				return null;
			}
			using (Dictionary<int, PlayerPointerEventData>.Enumerator enumerator = array[pointerIndex].GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					KeyValuePair<int, PlayerPointerEventData> keyValuePair = enumerator.Current;
					return keyValuePair.Value;
				}
			}
			return null;
		}

		// Token: 0x06001D8F RID: 7567 RVA: 0x000ACB4C File Offset: 0x000AAF4C
		private static bool ShouldStartDrag(Vector2 pressPos, Vector2 currentPos, float threshold, bool useDragThreshold)
		{
			return !useDragThreshold || (pressPos - currentPos).sqrMagnitude >= threshold * threshold;
		}

		// Token: 0x06001D90 RID: 7568 RVA: 0x000ACB78 File Offset: 0x000AAF78
		protected virtual void ProcessMove(PlayerPointerEventData pointerEvent)
		{
			GameObject newEnterTarget;
			if (pointerEvent.sourceType == PointerEventType.Mouse)
			{
				newEnterTarget = ((!this.GetMouseInputSource(pointerEvent.playerId, pointerEvent.inputSourceIndex).locked) ? pointerEvent.pointerCurrentRaycast.gameObject : null);
			}
			else
			{
				if (pointerEvent.sourceType != PointerEventType.Touch)
				{
					throw new NotImplementedException();
				}
				newEnterTarget = pointerEvent.pointerCurrentRaycast.gameObject;
			}
			base.HandlePointerExitAndEnter(pointerEvent, newEnterTarget);
		}

		// Token: 0x06001D91 RID: 7569 RVA: 0x000ACBF4 File Offset: 0x000AAFF4
		protected virtual void ProcessDrag(PlayerPointerEventData pointerEvent)
		{
			if (!pointerEvent.IsPointerMoving() || pointerEvent.pointerDrag == null)
			{
				return;
			}
			if (pointerEvent.sourceType == PointerEventType.Mouse && this.GetMouseInputSource(pointerEvent.playerId, pointerEvent.inputSourceIndex).locked)
			{
				return;
			}
			if (!pointerEvent.dragging && RewiredPointerInputModule.ShouldStartDrag(pointerEvent.pressPosition, pointerEvent.position, (float)base.eventSystem.pixelDragThreshold, pointerEvent.useDragThreshold))
			{
				ExecuteEvents.Execute<IBeginDragHandler>(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.beginDragHandler);
				pointerEvent.dragging = true;
			}
			if (pointerEvent.dragging)
			{
				if (pointerEvent.pointerPress != pointerEvent.pointerDrag)
				{
					ExecuteEvents.Execute<IPointerUpHandler>(pointerEvent.pointerPress, pointerEvent, ExecuteEvents.pointerUpHandler);
					pointerEvent.eligibleForClick = false;
					pointerEvent.pointerPress = null;
					pointerEvent.rawPointerPress = null;
				}
				ExecuteEvents.Execute<IDragHandler>(pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.dragHandler);
			}
		}

		// Token: 0x06001D92 RID: 7570 RVA: 0x000ACCEC File Offset: 0x000AB0EC
		public override bool IsPointerOverGameObject(int pointerTypeId)
		{
			foreach (KeyValuePair<int, Dictionary<int, PlayerPointerEventData>[]> keyValuePair in this.m_PlayerPointerData)
			{
				foreach (Dictionary<int, PlayerPointerEventData> dictionary in keyValuePair.Value)
				{
					PlayerPointerEventData playerPointerEventData;
					if (dictionary.TryGetValue(pointerTypeId, out playerPointerEventData))
					{
						if (playerPointerEventData.pointerEnter != null)
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06001D93 RID: 7571 RVA: 0x000ACD98 File Offset: 0x000AB198
		protected void ClearSelection()
		{
			BaseEventData baseEventData = this.GetBaseEventData();
			foreach (KeyValuePair<int, Dictionary<int, PlayerPointerEventData>[]> keyValuePair in this.m_PlayerPointerData)
			{
				Dictionary<int, PlayerPointerEventData>[] value = keyValuePair.Value;
				for (int i = 0; i < value.Length; i++)
				{
					foreach (KeyValuePair<int, PlayerPointerEventData> keyValuePair2 in value[i])
					{
						base.HandlePointerExitAndEnter(keyValuePair2.Value, null);
					}
					value[i].Clear();
				}
			}
			base.eventSystem.SetSelectedGameObject(null, baseEventData);
		}

		// Token: 0x06001D94 RID: 7572 RVA: 0x000ACE7C File Offset: 0x000AB27C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("<b>Pointer Input Module of type: </b>" + base.GetType());
			stringBuilder.AppendLine();
			foreach (KeyValuePair<int, Dictionary<int, PlayerPointerEventData>[]> keyValuePair in this.m_PlayerPointerData)
			{
				stringBuilder.AppendLine("<B>Player Id:</b> " + keyValuePair.Key);
				Dictionary<int, PlayerPointerEventData>[] value = keyValuePair.Value;
				for (int i = 0; i < value.Length; i++)
				{
					stringBuilder.AppendLine("<B>Pointer Index:</b> " + i);
					foreach (KeyValuePair<int, PlayerPointerEventData> keyValuePair2 in value[i])
					{
						stringBuilder.AppendLine("<B>Button Id:</b> " + keyValuePair2.Key);
						stringBuilder.AppendLine(keyValuePair2.Value.ToString());
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001D95 RID: 7573 RVA: 0x000ACFC0 File Offset: 0x000AB3C0
		protected void DeselectIfSelectionChanged(GameObject currentOverGo, BaseEventData pointerEvent)
		{
			GameObject eventHandler = ExecuteEvents.GetEventHandler<ISelectHandler>(currentOverGo);
			if (eventHandler != base.eventSystem.currentSelectedGameObject)
			{
				base.eventSystem.SetSelectedGameObject(null, pointerEvent);
			}
		}

		// Token: 0x06001D96 RID: 7574 RVA: 0x000ACFF7 File Offset: 0x000AB3F7
		protected void CopyFromTo(PointerEventData from, PointerEventData to)
		{
			to.position = from.position;
			to.delta = from.delta;
			to.scrollDelta = from.scrollDelta;
			to.pointerCurrentRaycast = from.pointerCurrentRaycast;
			to.pointerEnter = from.pointerEnter;
		}

		// Token: 0x06001D97 RID: 7575 RVA: 0x000AD038 File Offset: 0x000AB438
		protected PointerEventData.FramePressState StateForMouseButton(int playerId, int mouseIndex, int buttonId)
		{
			IMouseInputSource mouseInputSource = this.GetMouseInputSource(playerId, mouseIndex);
			if (mouseInputSource == null)
			{
				return PointerEventData.FramePressState.NotChanged;
			}
			bool buttonDown = mouseInputSource.GetButtonDown(buttonId);
			bool buttonUp = mouseInputSource.GetButtonUp(buttonId);
			if (buttonDown && buttonUp)
			{
				return PointerEventData.FramePressState.PressedAndReleased;
			}
			if (buttonDown)
			{
				return PointerEventData.FramePressState.Pressed;
			}
			if (buttonUp)
			{
				return PointerEventData.FramePressState.Released;
			}
			return PointerEventData.FramePressState.NotChanged;
		}

		// Token: 0x04001E51 RID: 7761
		public const int kMouseLeftId = -1;

		// Token: 0x04001E52 RID: 7762
		public const int kMouseRightId = -2;

		// Token: 0x04001E53 RID: 7763
		public const int kMouseMiddleId = -3;

		// Token: 0x04001E54 RID: 7764
		public const int kFakeTouchesId = -4;

		// Token: 0x04001E55 RID: 7765
		private const int customButtonsStartingId = -2147483520;

		// Token: 0x04001E56 RID: 7766
		private const int customButtonsMaxCount = 128;

		// Token: 0x04001E57 RID: 7767
		private const int customButtonsLastId = -2147483392;

		// Token: 0x04001E58 RID: 7768
		private readonly List<IMouseInputSource> m_MouseInputSourcesList = new List<IMouseInputSource>();

		// Token: 0x04001E59 RID: 7769
		private Dictionary<int, Dictionary<int, PlayerPointerEventData>[]> m_PlayerPointerData = new Dictionary<int, Dictionary<int, PlayerPointerEventData>[]>();

		// Token: 0x04001E5A RID: 7770
		private ITouchInputSource m_UserDefaultTouchInputSource;

		// Token: 0x04001E5B RID: 7771
		private RewiredPointerInputModule.UnityInputSource __m_DefaultInputSource;

		// Token: 0x04001E5C RID: 7772
		private readonly RewiredPointerInputModule.MouseState m_MouseState = new RewiredPointerInputModule.MouseState();

		// Token: 0x02000570 RID: 1392
		protected class MouseState
		{
			// Token: 0x06001D99 RID: 7577 RVA: 0x000AD098 File Offset: 0x000AB498
			public bool AnyPressesThisFrame()
			{
				for (int i = 0; i < this.m_TrackedButtons.Count; i++)
				{
					if (this.m_TrackedButtons[i].eventData.PressedThisFrame())
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06001D9A RID: 7578 RVA: 0x000AD0E0 File Offset: 0x000AB4E0
			public bool AnyReleasesThisFrame()
			{
				for (int i = 0; i < this.m_TrackedButtons.Count; i++)
				{
					if (this.m_TrackedButtons[i].eventData.ReleasedThisFrame())
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06001D9B RID: 7579 RVA: 0x000AD128 File Offset: 0x000AB528
			public RewiredPointerInputModule.ButtonState GetButtonState(int button)
			{
				RewiredPointerInputModule.ButtonState buttonState = null;
				for (int i = 0; i < this.m_TrackedButtons.Count; i++)
				{
					if (this.m_TrackedButtons[i].button == button)
					{
						buttonState = this.m_TrackedButtons[i];
						break;
					}
				}
				if (buttonState == null)
				{
					buttonState = new RewiredPointerInputModule.ButtonState
					{
						button = button,
						eventData = new RewiredPointerInputModule.MouseButtonEventData()
					};
					this.m_TrackedButtons.Add(buttonState);
				}
				return buttonState;
			}

			// Token: 0x06001D9C RID: 7580 RVA: 0x000AD1AC File Offset: 0x000AB5AC
			public void SetButtonState(int button, PointerEventData.FramePressState stateForMouseButton, PlayerPointerEventData data)
			{
				RewiredPointerInputModule.ButtonState buttonState = this.GetButtonState(button);
				buttonState.eventData.buttonState = stateForMouseButton;
				buttonState.eventData.buttonData = data;
			}

			// Token: 0x04001E5D RID: 7773
			private List<RewiredPointerInputModule.ButtonState> m_TrackedButtons = new List<RewiredPointerInputModule.ButtonState>();
		}

		// Token: 0x02000571 RID: 1393
		public class MouseButtonEventData
		{
			// Token: 0x06001D9E RID: 7582 RVA: 0x000AD1E1 File Offset: 0x000AB5E1
			public bool PressedThisFrame()
			{
				return this.buttonState == PointerEventData.FramePressState.Pressed || this.buttonState == PointerEventData.FramePressState.PressedAndReleased;
			}

			// Token: 0x06001D9F RID: 7583 RVA: 0x000AD1FA File Offset: 0x000AB5FA
			public bool ReleasedThisFrame()
			{
				return this.buttonState == PointerEventData.FramePressState.Released || this.buttonState == PointerEventData.FramePressState.PressedAndReleased;
			}

			// Token: 0x04001E5E RID: 7774
			public PointerEventData.FramePressState buttonState;

			// Token: 0x04001E5F RID: 7775
			public PlayerPointerEventData buttonData;
		}

		// Token: 0x02000572 RID: 1394
		protected class ButtonState
		{
			// Token: 0x170002CB RID: 715
			// (get) Token: 0x06001DA1 RID: 7585 RVA: 0x000AD21C File Offset: 0x000AB61C
			// (set) Token: 0x06001DA2 RID: 7586 RVA: 0x000AD224 File Offset: 0x000AB624
			public RewiredPointerInputModule.MouseButtonEventData eventData
			{
				get
				{
					return this.m_EventData;
				}
				set
				{
					this.m_EventData = value;
				}
			}

			// Token: 0x170002CC RID: 716
			// (get) Token: 0x06001DA3 RID: 7587 RVA: 0x000AD22D File Offset: 0x000AB62D
			// (set) Token: 0x06001DA4 RID: 7588 RVA: 0x000AD235 File Offset: 0x000AB635
			public int button
			{
				get
				{
					return this.m_Button;
				}
				set
				{
					this.m_Button = value;
				}
			}

			// Token: 0x04001E60 RID: 7776
			private int m_Button;

			// Token: 0x04001E61 RID: 7777
			private RewiredPointerInputModule.MouseButtonEventData m_EventData;
		}

		// Token: 0x02000573 RID: 1395
		private sealed class UnityInputSource : IMouseInputSource, ITouchInputSource
		{
			// Token: 0x170002CD RID: 717
			// (get) Token: 0x06001DA6 RID: 7590 RVA: 0x000AD24D File Offset: 0x000AB64D
			int IMouseInputSource.playerId
			{
				get
				{
					this.TryUpdate();
					return 0;
				}
			}

			// Token: 0x170002CE RID: 718
			// (get) Token: 0x06001DA7 RID: 7591 RVA: 0x000AD256 File Offset: 0x000AB656
			int ITouchInputSource.playerId
			{
				get
				{
					this.TryUpdate();
					return 0;
				}
			}

			// Token: 0x170002CF RID: 719
			// (get) Token: 0x06001DA8 RID: 7592 RVA: 0x000AD25F File Offset: 0x000AB65F
			bool IMouseInputSource.enabled
			{
				get
				{
					this.TryUpdate();
					return Input.mousePresent;
				}
			}

			// Token: 0x170002D0 RID: 720
			// (get) Token: 0x06001DA9 RID: 7593 RVA: 0x000AD26C File Offset: 0x000AB66C
			bool IMouseInputSource.locked
			{
				get
				{
					this.TryUpdate();
					return Cursor.lockState == CursorLockMode.Locked;
				}
			}

			// Token: 0x170002D1 RID: 721
			// (get) Token: 0x06001DAA RID: 7594 RVA: 0x000AD27C File Offset: 0x000AB67C
			int IMouseInputSource.buttonCount
			{
				get
				{
					this.TryUpdate();
					return 3;
				}
			}

			// Token: 0x06001DAB RID: 7595 RVA: 0x000AD285 File Offset: 0x000AB685
			bool IMouseInputSource.GetButtonDown(int button)
			{
				this.TryUpdate();
				return Input.GetMouseButtonDown(button);
			}

			// Token: 0x06001DAC RID: 7596 RVA: 0x000AD293 File Offset: 0x000AB693
			bool IMouseInputSource.GetButtonUp(int button)
			{
				this.TryUpdate();
				return Input.GetMouseButtonUp(button);
			}

			// Token: 0x06001DAD RID: 7597 RVA: 0x000AD2A1 File Offset: 0x000AB6A1
			bool IMouseInputSource.GetButton(int button)
			{
				this.TryUpdate();
				return Input.GetMouseButton(button);
			}

			// Token: 0x170002D2 RID: 722
			// (get) Token: 0x06001DAE RID: 7598 RVA: 0x000AD2AF File Offset: 0x000AB6AF
			Vector2 IMouseInputSource.screenPosition
			{
				get
				{
					this.TryUpdate();
					return Input.mousePosition;
				}
			}

			// Token: 0x170002D3 RID: 723
			// (get) Token: 0x06001DAF RID: 7599 RVA: 0x000AD2C1 File Offset: 0x000AB6C1
			Vector2 IMouseInputSource.screenPositionDelta
			{
				get
				{
					this.TryUpdate();
					return this.m_MousePosition - this.m_MousePositionPrev;
				}
			}

			// Token: 0x170002D4 RID: 724
			// (get) Token: 0x06001DB0 RID: 7600 RVA: 0x000AD2DA File Offset: 0x000AB6DA
			Vector2 IMouseInputSource.wheelDelta
			{
				get
				{
					this.TryUpdate();
					return Input.mouseScrollDelta;
				}
			}

			// Token: 0x170002D5 RID: 725
			// (get) Token: 0x06001DB1 RID: 7601 RVA: 0x000AD2E7 File Offset: 0x000AB6E7
			bool ITouchInputSource.touchSupported
			{
				get
				{
					this.TryUpdate();
					return Input.touchSupported;
				}
			}

			// Token: 0x170002D6 RID: 726
			// (get) Token: 0x06001DB2 RID: 7602 RVA: 0x000AD2F4 File Offset: 0x000AB6F4
			int ITouchInputSource.touchCount
			{
				get
				{
					this.TryUpdate();
					return Input.touchCount;
				}
			}

			// Token: 0x06001DB3 RID: 7603 RVA: 0x000AD301 File Offset: 0x000AB701
			Touch ITouchInputSource.GetTouch(int index)
			{
				this.TryUpdate();
				return Input.GetTouch(index);
			}

			// Token: 0x06001DB4 RID: 7604 RVA: 0x000AD30F File Offset: 0x000AB70F
			private void TryUpdate()
			{
				if (Time.frameCount == this.m_LastUpdatedFrame)
				{
					return;
				}
				this.m_LastUpdatedFrame = Time.frameCount;
				this.m_MousePositionPrev = this.m_MousePosition;
				this.m_MousePosition = Input.mousePosition;
			}

			// Token: 0x04001E62 RID: 7778
			private Vector2 m_MousePosition;

			// Token: 0x04001E63 RID: 7779
			private Vector2 m_MousePositionPrev;

			// Token: 0x04001E64 RID: 7780
			private int m_LastUpdatedFrame = -1;
		}
	}
}
