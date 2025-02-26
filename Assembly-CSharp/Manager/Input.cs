using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using AIProject;
using AIProject.UI;
using Rewired;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Manager
{
	// Token: 0x020008E7 RID: 2279
	public class Input : Singleton<Input>
	{
		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x06003CD2 RID: 15570 RVA: 0x00163AEB File Offset: 0x00161EEB
		public Input.ValidType State
		{
			[CompilerGenerated]
			get
			{
				return this._state;
			}
		}

		// Token: 0x06003CD3 RID: 15571 RVA: 0x00163AF3 File Offset: 0x00161EF3
		public void ReserveState(Input.ValidType type)
		{
			this._reservedState = type;
		}

		// Token: 0x06003CD4 RID: 15572 RVA: 0x00163AFC File Offset: 0x00161EFC
		public void SetupState()
		{
			this._state = this._reservedState;
		}

		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x06003CD5 RID: 15573 RVA: 0x00163B0A File Offset: 0x00161F0A
		public float InputActionPerSecond
		{
			[CompilerGenerated]
			get
			{
				return this._inputActionPerSecond;
			}
		}

		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x06003CD6 RID: 15574 RVA: 0x00163B12 File Offset: 0x00161F12
		public float RepeatDelay
		{
			[CompilerGenerated]
			get
			{
				return this._repeatDelay;
			}
		}

		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x06003CD7 RID: 15575 RVA: 0x00163B1A File Offset: 0x00161F1A
		// (set) Token: 0x06003CD8 RID: 15576 RVA: 0x00163B22 File Offset: 0x00161F22
		public List<ISystemCommand> SystemElements { get; set; } = new List<ISystemCommand>();

		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x06003CD9 RID: 15577 RVA: 0x00163B2B File Offset: 0x00161F2B
		// (set) Token: 0x06003CDA RID: 15578 RVA: 0x00163B33 File Offset: 0x00161F33
		public List<IActionCommand> ActionElements { get; set; } = new List<IActionCommand>();

		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x06003CDB RID: 15579 RVA: 0x00163B3C File Offset: 0x00161F3C
		// (set) Token: 0x06003CDC RID: 15580 RVA: 0x00163B44 File Offset: 0x00161F44
		public MenuUIBehaviour[] MenuElements
		{
			get
			{
				return this._menuElements;
			}
			set
			{
				this._menuElements = value.ToArray<MenuUIBehaviour>();
			}
		}

		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x06003CDD RID: 15581 RVA: 0x00163B52 File Offset: 0x00161F52
		private MenuUIBehaviour[] Empty { get; } = new MenuUIBehaviour[0];

		// Token: 0x06003CDE RID: 15582 RVA: 0x00163B5C File Offset: 0x00161F5C
		public void ClearMenuElements()
		{
			foreach (MenuUIBehaviour menuUIBehaviour in this._menuElements)
			{
				menuUIBehaviour.EnabledInput = false;
			}
			this._menuElements = this.Empty;
		}

		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x06003CDF RID: 15583 RVA: 0x00163B9B File Offset: 0x00161F9B
		private Player Player0
		{
			get
			{
				return ReInput.players.GetPlayer(0);
			}
		}

		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x06003CE0 RID: 15584 RVA: 0x00163BA8 File Offset: 0x00161FA8
		// (set) Token: 0x06003CE1 RID: 15585 RVA: 0x00163BB0 File Offset: 0x00161FB0
		public int FocusLevel { get; set; } = -1;

		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x06003CE2 RID: 15586 RVA: 0x00163BBC File Offset: 0x00161FBC
		public Vector2 MoveAxis
		{
			get
			{
				if (this.Player0.GetButton(this.Action[ActionID.MouseLeft]))
				{
					return new Vector2(0f, 1f);
				}
				return this.Player0.GetAxis2D(this.Action[ActionID.MoveHorizontal], this.Action[ActionID.MoveVertical]);
			}
		}

		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x06003CE3 RID: 15587 RVA: 0x00163C1B File Offset: 0x0016201B
		public Vector2 LeftStickAxis
		{
			get
			{
				return this.Player0.GetAxis2D(this.Action[ActionID.MoveHorizontal], this.Action[ActionID.MoveVertical]);
			}
		}

		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x06003CE4 RID: 15588 RVA: 0x00163C44 File Offset: 0x00162044
		public Vector2 MouseAxis
		{
			get
			{
				Mouse controller = ReInput.controllers.GetController<Mouse>(0);
				if (controller == null)
				{
					return Vector2.zero;
				}
				return new Vector2(controller.GetAxis(0), controller.GetAxis(1));
			}
		}

		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x06003CE5 RID: 15589 RVA: 0x00163C7C File Offset: 0x0016207C
		public Mouse Mouse
		{
			get
			{
				return ReInput.controllers.GetController<Mouse>(0);
			}
		}

		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x06003CE6 RID: 15590 RVA: 0x00163C8C File Offset: 0x0016208C
		public Vector2 CameraAxis
		{
			get
			{
				return this.Player0.GetAxis2D(this.Action[ActionID.CameraHorizontal], this.Action[ActionID.CameraVertical]);
			}
		}

		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x06003CE7 RID: 15591 RVA: 0x00163CC0 File Offset: 0x001620C0
		public Vector2 UIAxisRow
		{
			get
			{
				Vector2 axis2D = this.Player0.GetAxis2D(this.Action[ActionID.MoveHorizontal], this.Action[ActionID.MoveVertical]);
				Vector2 axis2D2 = this.Player0.GetAxis2D(this.Action[ActionID.SelectHorizontal], this.Action[ActionID.SelectVertical]);
				float x = (Mathf.Abs(axis2D.x) >= Mathf.Abs(axis2D2.x)) ? axis2D.x : axis2D2.x;
				float y = (Mathf.Abs(axis2D.y) >= Mathf.Abs(axis2D2.y)) ? axis2D.y : axis2D2.y;
				return new Vector2(x, y);
			}
		}

		// Token: 0x06003CE8 RID: 15592 RVA: 0x00163D82 File Offset: 0x00162182
		public bool IsPressedHorizontal()
		{
			return this.IsPressedAxis(this.Action[ActionID.MoveHorizontal]) || this.IsPressedAxis(this.Action[ActionID.SelectHorizontal]);
		}

		// Token: 0x06003CE9 RID: 15593 RVA: 0x00163DB2 File Offset: 0x001621B2
		public bool IsPressedVertical()
		{
			return this.IsPressedAxis(this.Action[ActionID.MoveVertical]) || this.IsPressedAxis(this.Action[ActionID.SelectVertical]);
		}

		// Token: 0x06003CEA RID: 15594 RVA: 0x00163DE2 File Offset: 0x001621E2
		public bool IsPressedAction()
		{
			return this.Player0.GetButtonDown(this.Action[ActionID.Action]);
		}

		// Token: 0x06003CEB RID: 15595 RVA: 0x00163DFC File Offset: 0x001621FC
		public ActionID GetPressedKeyRewired()
		{
			foreach (KeyValuePair<ActionID, string> keyValuePair in ActionTable.Table)
			{
				ActionID key = keyValuePair.Key;
				switch (key)
				{
				case ActionID.MoveHorizontal:
				case ActionID.MoveVertical:
				case ActionID.CameraHorizontal:
				case ActionID.CameraVertical:
					break;
				default:
					switch (key)
					{
					case ActionID.SelectHorizontal:
					case ActionID.SelectVertical:
					case ActionID.MouseWheel:
						goto IL_5E;
					}
					if (this.IsPressedKey(keyValuePair.Value))
					{
						return keyValuePair.Key;
					}
					continue;
				}
				IL_5E:
				if (this.IsPressedAxis(keyValuePair.Value))
				{
					return keyValuePair.Key;
				}
			}
			return ActionID.None;
		}

		// Token: 0x06003CEC RID: 15596 RVA: 0x00163EE0 File Offset: 0x001622E0
		public bool GetAnyDown()
		{
			return this.Player0.GetAnyButtonDown();
		}

		// Token: 0x06003CED RID: 15597 RVA: 0x00163EF0 File Offset: 0x001622F0
		public KeyCode GetPressedKey()
		{
			if (Input.anyKeyDown)
			{
				IEnumerator enumerator = Enum.GetValues(typeof(KeyCode)).GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						KeyCode keyCode = (KeyCode)obj;
						if (Input.GetKeyDown(keyCode))
						{
							return keyCode;
						}
					}
				}
				finally
				{
					IDisposable disposable;
					if ((disposable = (enumerator as IDisposable)) != null)
					{
						disposable.Dispose();
					}
				}
				return KeyCode.None;
			}
			return KeyCode.None;
		}

		// Token: 0x06003CEE RID: 15598 RVA: 0x00163F80 File Offset: 0x00162380
		public bool IsPressedSubmit()
		{
			return this.Player0.GetButtonDown(this.Action[ActionID.Submit]);
		}

		// Token: 0x06003CEF RID: 15599 RVA: 0x00163F9A File Offset: 0x0016239A
		public bool IsPressedCancel()
		{
			return this.Player0.GetButtonDown(this.Action[ActionID.Cancel]);
		}

		// Token: 0x06003CF0 RID: 15600 RVA: 0x00163FB4 File Offset: 0x001623B4
		public bool IsDown(KeyCode key)
		{
			return Input.GetKey(key);
		}

		// Token: 0x06003CF1 RID: 15601 RVA: 0x00163FBC File Offset: 0x001623BC
		public bool IsPressedKey(KeyCode key)
		{
			return Input.GetKeyDown(key);
		}

		// Token: 0x06003CF2 RID: 15602 RVA: 0x00163FC4 File Offset: 0x001623C4
		public bool IsDown(string key)
		{
			return this.Player0.GetButton(key);
		}

		// Token: 0x06003CF3 RID: 15603 RVA: 0x00163FD2 File Offset: 0x001623D2
		public bool IsDown(ActionID id)
		{
			return this.Player0.GetButton((int)id);
		}

		// Token: 0x06003CF4 RID: 15604 RVA: 0x00163FE0 File Offset: 0x001623E0
		public bool IsDown(int id)
		{
			return this.Player0.GetButton(id);
		}

		// Token: 0x06003CF5 RID: 15605 RVA: 0x00163FEE File Offset: 0x001623EE
		public bool IsPressedKey(string key)
		{
			return this.Player0.GetButtonDown(key);
		}

		// Token: 0x06003CF6 RID: 15606 RVA: 0x00163FFC File Offset: 0x001623FC
		public bool IsPressedKey(ActionID id)
		{
			return this.Player0.GetButtonDown((int)id);
		}

		// Token: 0x06003CF7 RID: 15607 RVA: 0x0016400A File Offset: 0x0016240A
		public bool IsPressedKey(int id)
		{
			return this.Player0.GetButtonDown(id);
		}

		// Token: 0x06003CF8 RID: 15608 RVA: 0x00164018 File Offset: 0x00162418
		public bool IsPressedAxis(string axisName)
		{
			int num = (Mathf.Abs(this.Player0.GetAxisPrev(axisName)) <= 0.3f) ? 0 : 1;
			int num2 = (Mathf.Abs(this.Player0.GetAxis(axisName)) <= 0.3f) ? 0 : 1;
			return num2 > num;
		}

		// Token: 0x06003CF9 RID: 15609 RVA: 0x00164070 File Offset: 0x00162470
		public bool IsPressedAxis(ActionID axisID)
		{
			int num = (Mathf.Abs(this.Player0.GetAxisPrev((int)axisID)) <= 0.3f) ? 0 : 1;
			int num2 = (Mathf.Abs(this.Player0.GetAxis((int)axisID)) <= 0.3f) ? 0 : 1;
			return num2 > num;
		}

		// Token: 0x06003CFA RID: 15610 RVA: 0x001640C8 File Offset: 0x001624C8
		public bool IsPressedAxis(int axisID)
		{
			int num = Mathf.Abs((int)this.Player0.GetAxisPrev(axisID));
			int num2 = Mathf.Abs((int)this.Player0.GetAxis(axisID));
			return num < num2;
		}

		// Token: 0x06003CFB RID: 15611 RVA: 0x001640FF File Offset: 0x001624FF
		public float GetAxis(string axisName)
		{
			return this.Player0.GetAxis(axisName);
		}

		// Token: 0x06003CFC RID: 15612 RVA: 0x0016410D File Offset: 0x0016250D
		public float GetAxis(ActionID axisID)
		{
			return this.Player0.GetAxis((int)axisID);
		}

		// Token: 0x06003CFD RID: 15613 RVA: 0x0016411B File Offset: 0x0016251B
		public float GetAxis(int axisID)
		{
			return this.Player0.GetAxis(axisID);
		}

		// Token: 0x06003CFE RID: 15614 RVA: 0x00164129 File Offset: 0x00162529
		public float GetAxisRaw(string axisName)
		{
			return this.Player0.GetAxisRaw(axisName);
		}

		// Token: 0x06003CFF RID: 15615 RVA: 0x00164137 File Offset: 0x00162537
		public float GetAxisRaw(ActionID axisID)
		{
			return this.Player0.GetAxisRaw((int)axisID);
		}

		// Token: 0x06003D00 RID: 15616 RVA: 0x00164145 File Offset: 0x00162545
		public float GetAxisRaw(int axisID)
		{
			return this.Player0.GetAxisRaw(axisID);
		}

		// Token: 0x06003D01 RID: 15617 RVA: 0x00164153 File Offset: 0x00162553
		public float ScrollValue()
		{
			return this.Player0.GetAxis(this.Action[ActionID.MouseWheel]);
		}

		// Token: 0x06003D02 RID: 15618 RVA: 0x00164170 File Offset: 0x00162570
		protected virtual void Update()
		{
			if (Singleton<Scene>.Instance.IsNowLoadingFade)
			{
				return;
			}
			Game instance = Singleton<Game>.Instance;
			if (instance.MapShortcutUI == null && instance.Config == null && instance.Dialog == null && instance.ExitScene == null)
			{
				this.SendPressedSystemEvent();
			}
			Input.ValidType state = this._state;
			if (state != Input.ValidType.Action)
			{
				if (state == Input.ValidType.UI)
				{
					this.SendUpdateEventToSelectedObject();
					this.SendMoveEventToSelectedObject();
					this.SendSubMoveEvent();
					this.SendSubmitEventToSelectedObject();
				}
			}
			else
			{
				this.SendPressedActionEventToSelectedObject();
			}
		}

		// Token: 0x06003D03 RID: 15619 RVA: 0x00164224 File Offset: 0x00162624
		protected void SendPressedSystemEvent()
		{
			foreach (ISystemCommand systemCommand in this.SystemElements)
			{
				if (systemCommand.EnabledInput)
				{
					systemCommand.OnUpdateInput();
				}
			}
		}

		// Token: 0x06003D04 RID: 15620 RVA: 0x0016428C File Offset: 0x0016268C
		protected void SendPressedActionEventToSelectedObject()
		{
			foreach (IActionCommand actionCommand in this.ActionElements)
			{
				if (actionCommand.EnabledInput)
				{
					actionCommand.OnUpdateInput();
				}
			}
		}

		// Token: 0x06003D05 RID: 15621 RVA: 0x001642F4 File Offset: 0x001626F4
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
				bool flag = this._state == Input.ValidType.UI && (this.IsPressedHorizontal() || this.IsPressedVertical());
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
				if (!flag)
				{
					result = false;
				}
				else
				{
					AxisEventData axisEventData = this.GetAxisEventData(rawMoveVector.x, rawMoveVector.y, 0.6f);
					if (axisEventData.moveDir != MoveDirection.None)
					{
						int focusLevel = this.FocusLevel;
						Game instance = Singleton<Game>.Instance;
						if (instance.ExitScene != null)
						{
							instance.ExitScene.OnInputMoveDirection(axisEventData.moveDir);
						}
						else if (instance.Dialog != null)
						{
							instance.Dialog.OnInputMoveDirection(axisEventData.moveDir);
						}
						else if (!(instance.Config != null))
						{
							if (!(instance.MapShortcutUI != null))
							{
								foreach (MenuUIBehaviour menuUIBehaviour in this._menuElements)
								{
									if (menuUIBehaviour.EnabledInput && menuUIBehaviour.FocusLevel == focusLevel)
									{
										menuUIBehaviour.OnInputMoveDirection(axisEventData.moveDir);
									}
								}
							}
						}
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
			}
			return result;
		}

		// Token: 0x06003D06 RID: 15622 RVA: 0x00164524 File Offset: 0x00162924
		private Vector2 GetRawMoveVector()
		{
			Vector2 zero = Vector2.zero;
			if (Singleton<Input>.IsInstance() && this._state == Input.ValidType.UI)
			{
				zero.x = this.UIAxisRow.x;
				zero.y = this.UIAxisRow.y;
				if (this.IsPressedHorizontal())
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
				if (this.IsPressedVertical())
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
			return zero;
		}

		// Token: 0x06003D07 RID: 15623 RVA: 0x00164602 File Offset: 0x00162A02
		protected BaseEventData GetBaseEventData()
		{
			if (this._baseEventData == null)
			{
				this._baseEventData = new BaseEventData(EventSystem.current);
			}
			this._baseEventData.Reset();
			return this._baseEventData;
		}

		// Token: 0x06003D08 RID: 15624 RVA: 0x00164630 File Offset: 0x00162A30
		protected AxisEventData GetAxisEventData(float x, float y, float moveDeadZone)
		{
			if (this._axisEventData == null)
			{
				this._axisEventData = new AxisEventData(EventSystem.current);
			}
			this._axisEventData.Reset();
			this._axisEventData.moveVector = new Vector2(x, y);
			this._axisEventData.moveDir = Input.DetermineMoveDirection(x, y, moveDeadZone);
			return this._axisEventData;
		}

		// Token: 0x06003D09 RID: 15625 RVA: 0x00164690 File Offset: 0x00162A90
		protected static MoveDirection DetermineMoveDirection(float x, float y, float deadZone)
		{
			Vector2 vector = new Vector2(x, y);
			MoveDirection result;
			if (vector.sqrMagnitude < deadZone)
			{
				result = MoveDirection.None;
			}
			else if (Mathf.Abs(x) > Mathf.Abs(y))
			{
				if (x > 0f)
				{
					result = MoveDirection.Right;
				}
				else
				{
					result = MoveDirection.Left;
				}
			}
			else if (y > 0f)
			{
				result = MoveDirection.Up;
			}
			else
			{
				result = MoveDirection.Down;
			}
			return result;
		}

		// Token: 0x06003D0A RID: 15626 RVA: 0x001646FC File Offset: 0x00162AFC
		protected bool SendSubMoveEvent()
		{
			float unscaledTime = Time.unscaledTime;
			Vector2 subRawMoveVector = this.GetSubRawMoveVector();
			bool result;
			if (Mathf.Approximately(subRawMoveVector.x, 0f) && Mathf.Approximately(subRawMoveVector.y, 0f))
			{
				this._subConsecutiveMoveCount = 0;
				result = false;
			}
			else
			{
				bool flag = false;
				bool flag2 = Vector2.Dot(subRawMoveVector, this._subLastMoveVector) > 0f;
				if (!flag)
				{
					if (flag2 && this._subConsecutiveMoveCount == 1)
					{
						flag = (unscaledTime > this._subPrevActionTime + this._repeatDelay);
					}
					else
					{
						flag = (unscaledTime > this._subPrevActionTime + 1f / this._inputActionPerSecond);
					}
				}
				if (!flag)
				{
					result = false;
				}
				else
				{
					AxisEventData axisEventData = this.GetAxisEventData(subRawMoveVector.x, subRawMoveVector.y, 0.6f);
					if (axisEventData.moveDir != MoveDirection.None)
					{
						int focusLevel = this.FocusLevel;
						Game instance = Singleton<Game>.Instance;
						if (instance.ExitScene != null)
						{
							instance.ExitScene.OnInputSubMoveDirection(axisEventData.moveDir);
						}
						else if (instance.Dialog != null)
						{
							instance.Dialog.OnInputSubMoveDirection(axisEventData.moveDir);
						}
						else if (!(instance.Config != null))
						{
							if (!(instance.MapShortcutUI != null))
							{
								foreach (MenuUIBehaviour menuUIBehaviour in this._menuElements)
								{
									if (menuUIBehaviour.EnabledInput && menuUIBehaviour.FocusLevel == focusLevel)
									{
										menuUIBehaviour.OnInputSubMoveDirection(axisEventData.moveDir);
									}
								}
							}
						}
						if (!flag2)
						{
							this._subConsecutiveMoveCount = 0;
						}
						this._subConsecutiveMoveCount++;
						this._subPrevActionTime = unscaledTime;
						this._subLastMoveVector = subRawMoveVector;
					}
					else
					{
						this._subConsecutiveMoveCount = 0;
					}
					result = axisEventData.used;
				}
			}
			return result;
		}

		// Token: 0x06003D0B RID: 15627 RVA: 0x00164904 File Offset: 0x00162D04
		private Vector2 GetSubRawMoveVector()
		{
			Vector2 zero = Vector2.zero;
			if (Singleton<Input>.IsInstance() && this._state == Input.ValidType.UI)
			{
				zero.x = this.CameraAxis.x;
				zero.y = this.CameraAxis.y;
				if (this.IsPressedHorizontal())
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
				if (this.IsPressedVertical())
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
			return zero;
		}

		// Token: 0x06003D0C RID: 15628 RVA: 0x001649E4 File Offset: 0x00162DE4
		protected bool SendSubmitEventToSelectedObject()
		{
			BaseEventData baseEventData = this.GetBaseEventData();
			if (this._state == Input.ValidType.UI)
			{
				int focusLevel = this.FocusLevel;
				Game instance = Singleton<Game>.Instance;
				if (instance.ExitScene != null)
				{
					instance.ExitScene.OnUpdateInput(this);
				}
				else if (instance.Dialog != null)
				{
					instance.Dialog.OnUpdateInput(this);
				}
				else if (!(instance.Config != null))
				{
					if (instance.MapShortcutUI != null)
					{
						if (instance.MapShortcutUI.EnabledInput)
						{
							instance.MapShortcutUI.OnUpdateInput(this);
						}
					}
					else
					{
						foreach (MenuUIBehaviour menuUIBehaviour in this._menuElements)
						{
							if (!(menuUIBehaviour == null))
							{
								if (menuUIBehaviour.EnabledInput && menuUIBehaviour.FocusLevel == focusLevel)
								{
									menuUIBehaviour.OnUpdateInput(this);
								}
							}
						}
					}
				}
			}
			return baseEventData.used;
		}

		// Token: 0x06003D0D RID: 15629 RVA: 0x00164B08 File Offset: 0x00162F08
		protected bool SendUpdateEventToSelectedObject()
		{
			BaseEventData baseEventData = this.GetBaseEventData();
			return baseEventData.used;
		}

		// Token: 0x04003B02 RID: 15106
		[SerializeField]
		private Input.ValidType _state;

		// Token: 0x04003B03 RID: 15107
		private Input.ValidType _reservedState;

		// Token: 0x04003B04 RID: 15108
		[SerializeField]
		private float _inputActionPerSecond = 10f;

		// Token: 0x04003B05 RID: 15109
		[SerializeField]
		private float _repeatDelay = 0.5f;

		// Token: 0x04003B08 RID: 15112
		private MenuUIBehaviour[] _menuElements = new MenuUIBehaviour[0];

		// Token: 0x04003B0A RID: 15114
		private float _prevActionTime;

		// Token: 0x04003B0B RID: 15115
		private Vector2 _lastMoveVector = Vector2.zero;

		// Token: 0x04003B0C RID: 15116
		private int _consecutiveMoveCount;

		// Token: 0x04003B0D RID: 15117
		private float _subPrevActionTime;

		// Token: 0x04003B0E RID: 15118
		private Vector2 _subLastMoveVector = Vector2.zero;

		// Token: 0x04003B0F RID: 15119
		private int _subConsecutiveMoveCount;

		// Token: 0x04003B10 RID: 15120
		private ActionTable Action = new ActionTable();

		// Token: 0x04003B12 RID: 15122
		protected AxisEventData _axisEventData;

		// Token: 0x04003B13 RID: 15123
		protected BaseEventData _baseEventData;

		// Token: 0x020008E8 RID: 2280
		public enum ValidType
		{
			// Token: 0x04003B15 RID: 15125
			None,
			// Token: 0x04003B16 RID: 15126
			Action,
			// Token: 0x04003B17 RID: 15127
			UI
		}
	}
}
