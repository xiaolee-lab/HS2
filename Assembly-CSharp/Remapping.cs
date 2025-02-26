using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Rewired;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000836 RID: 2102
public class Remapping : MonoBehaviour
{
	// Token: 0x0600357D RID: 13693 RVA: 0x0013B12A File Offset: 0x0013952A
	private bool CheckKeyboardSetting()
	{
		return true;
	}

	// Token: 0x0600357E RID: 13694 RVA: 0x0013B12D File Offset: 0x0013952D
	private bool CheckMouseSetting()
	{
		return false;
	}

	// Token: 0x170009AC RID: 2476
	// (get) Token: 0x0600357F RID: 13695 RVA: 0x0013B130 File Offset: 0x00139530
	// (set) Token: 0x06003580 RID: 13696 RVA: 0x0013B188 File Offset: 0x00139588
	private int joystickIndexId
	{
		get
		{
			if (0 < this.joystickCount)
			{
				if (this.joystickIndexId_ < 0)
				{
					this.joystickIndexId_ = 0;
				}
				else if (this.joystickCount <= this.joystickIndexId_)
				{
					this.joystickIndexId_ = this.joystickCount - 1;
				}
			}
			return this.joystickIndexId_;
		}
		set
		{
			this.joystickIndexId_ = value;
			if (this.joystickCount <= 0)
			{
				this.joystickIndexId_ = -1;
			}
			else if (this.joystickIndexId_ < 0)
			{
				this.joystickIndexId_ = 0;
			}
			else if (this.joystickCount <= this.joystickIndexId_)
			{
				this.joystickIndexId_ = this.joystickCount - 1;
			}
			this.SetJoystickActiv();
		}
	}

	// Token: 0x170009AD RID: 2477
	// (get) Token: 0x06003581 RID: 13697 RVA: 0x0013B1F1 File Offset: 0x001395F1
	private IList<ActionElementMap> actionElementMaps
	{
		get
		{
			return (this.controllerMap == null) ? null : this.controllerMap.AllMaps;
		}
	}

	// Token: 0x170009AE RID: 2478
	// (get) Token: 0x06003582 RID: 13698 RVA: 0x0013B20F File Offset: 0x0013960F
	private Player player
	{
		get
		{
			return ReInput.players.GetPlayer(0);
		}
	}

	// Token: 0x170009AF RID: 2479
	// (get) Token: 0x06003583 RID: 13699 RVA: 0x0013B21C File Offset: 0x0013961C
	private Controller controller
	{
		get
		{
			return (this.player == null) ? null : this.player.controllers.GetController(this.selectedControllerType, this.selectedControllerId);
		}
	}

	// Token: 0x170009B0 RID: 2480
	// (get) Token: 0x06003584 RID: 13700 RVA: 0x0013B24B File Offset: 0x0013964B
	private int joystickCount
	{
		get
		{
			return (this.player == null) ? 0 : this.player.controllers.joystickCount;
		}
	}

	// Token: 0x170009B1 RID: 2481
	// (get) Token: 0x06003585 RID: 13701 RVA: 0x0013B26E File Offset: 0x0013966E
	private IList<Joystick> joysticks
	{
		get
		{
			return (this.player == null) ? null : this.player.controllers.Joysticks;
		}
	}

	// Token: 0x170009B2 RID: 2482
	// (get) Token: 0x06003586 RID: 13702 RVA: 0x0013B294 File Offset: 0x00139694
	private Joystick joystick
	{
		get
		{
			return (this.joysticks == null || 0 > this.joystickIndexId || this.joystickIndexId >= this.joysticks.Count) ? null : this.joysticks[this.joystickIndexId];
		}
	}

	// Token: 0x170009B3 RID: 2483
	// (get) Token: 0x06003587 RID: 13703 RVA: 0x0013B2E8 File Offset: 0x001396E8
	private ControllerMap controllerMap
	{
		get
		{
			if (this.controller == null)
			{
				return null;
			}
			return this.player.controllers.maps.GetMap(this.controller.type, this.controller.id, "Default", "Default");
		}
	}

	// Token: 0x06003588 RID: 13704 RVA: 0x0013B338 File Offset: 0x00139738
	private void OnEnable()
	{
		this.updateMode = Remapping.UpdateMode.ButtonSelectMode;
		this.selectedControllerType = ControllerType.Joystick;
		this.selectedControllerId = 0;
		this.settingOrDeleteWindow.SetActive(false);
		if (!ReInput.isReady)
		{
			return;
		}
		ReInput.ControllerConnectedEvent += this.OnControllerChanged;
		ReInput.ControllerDisconnectedEvent += this.OnControllerChanged;
		this.settingButton.onClick.RemoveAllListeners();
		this.deleteButton.onClick.RemoveAllListeners();
		this.returnButton.onClick.RemoveAllListeners();
		this.settingButton.onClick.AddListener(delegate()
		{
			this.OnInputSetting();
		});
		this.deleteButton.onClick.AddListener(delegate()
		{
			this.OnDeleteInputSetting();
		});
		this.returnButton.onClick.AddListener(delegate()
		{
			this.OnReturnToButtonSelectMode();
		});
		this.SetJoystickActiv();
		this.SetControllerToDropdown();
		this.LoadControllerSetting();
		this.InitializeUI();
	}

	// Token: 0x06003589 RID: 13705 RVA: 0x0013B42F File Offset: 0x0013982F
	private void OnDisable()
	{
		ReInput.ControllerConnectedEvent -= this.OnControllerChanged;
		ReInput.ControllerDisconnectedEvent -= this.OnControllerChanged;
	}

	// Token: 0x0600358A RID: 13706 RVA: 0x0013B453 File Offset: 0x00139853
	private void Start()
	{
		(from _ in Observable.EveryUpdate().TakeUntilDestroy(base.gameObject)
		where base.isActiveAndEnabled
		select _).Subscribe(delegate(long _)
		{
			this.U_Update();
		});
	}

	// Token: 0x0600358B RID: 13707 RVA: 0x0013B488 File Offset: 0x00139888
	private void InitializeUI()
	{
		IEnumerator enumerator = this.actionNameSortArea.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				UnityEngine.Object.Destroy(transform.gameObject);
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
		IEnumerator enumerator2 = this.keyInputButtonSortArea.GetEnumerator();
		try
		{
			while (enumerator2.MoveNext())
			{
				object obj2 = enumerator2.Current;
				Transform transform2 = (Transform)obj2;
				UnityEngine.Object.Destroy(transform2.gameObject);
			}
		}
		finally
		{
			IDisposable disposable2;
			if ((disposable2 = (enumerator2 as IDisposable)) != null)
			{
				disposable2.Dispose();
			}
		}
		this.rows.Clear();
		foreach (InputAction inputAction in ReInput.mapping.ActionsInCategory("Default"))
		{
			if (inputAction.userAssignable)
			{
				if (inputAction.type == InputActionType.Button)
				{
					this.CreateUIRow(inputAction, AxisRange.Positive, inputAction.descriptiveName);
				}
				else if (inputAction.type == InputActionType.Axis)
				{
					this.CreateUIRow(inputAction, AxisRange.Full, inputAction.descriptiveName);
					this.CreateUIRow(inputAction, AxisRange.Positive, string.IsNullOrEmpty(inputAction.positiveDescriptiveName) ? (inputAction.descriptiveName + " +") : inputAction.positiveDescriptiveName);
					this.CreateUIRow(inputAction, AxisRange.Negative, string.IsNullOrEmpty(inputAction.negativeDescriptiveName) ? (inputAction.descriptiveName + " -") : inputAction.negativeDescriptiveName);
				}
			}
		}
		this.RedrawUI();
	}

	// Token: 0x0600358C RID: 13708 RVA: 0x0013B670 File Offset: 0x00139A70
	private void CreateUIRow(InputAction _action, AxisRange _actionRange, string _actionName)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.actionNameTextPrefab);
		gameObject.transform.SetParent(this.actionNameSortArea);
		gameObject.transform.SetAsLastSibling();
		gameObject.GetComponent<Text>().text = _actionName;
		GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.inputButtonPrefab);
		gameObject2.transform.SetParent(this.keyInputButtonSortArea);
		gameObject2.transform.SetAsLastSibling();
		this.rows.Add(new Remapping.Row
		{
			action = _action,
			actionRange = _actionRange,
			button = gameObject2.GetComponent<Button>(),
			text = gameObject2.GetComponentInChildren<Text>()
		});
	}

	// Token: 0x0600358D RID: 13709 RVA: 0x0013B714 File Offset: 0x00139B14
	private void RedrawUI()
	{
		if (this.controller == null)
		{
			this.ClearUI();
			return;
		}
		this.ControllerMappingToSetting();
		this.messageText.text = "編集したいアクションを選択してください";
		for (int i = 0; i < this.rows.Count; i++)
		{
			Remapping.Row row = this.rows[i];
			InputAction action = row.action;
			string text = string.Empty;
			foreach (ActionElementMap actionElementMap in this.controllerMap.ElementMapsWithAction(action.id))
			{
				if (actionElementMap.ShowInField(row.actionRange))
				{
					text = actionElementMap.elementIdentifierName;
					break;
				}
			}
			row.text.text = text;
			row.button.onClick.RemoveAllListeners();
			int _index = i;
			row.button.onClick.AddListener(delegate()
			{
				this.OnInputFieldClicked(_index);
			});
		}
	}

	// Token: 0x0600358E RID: 13710 RVA: 0x0013B844 File Offset: 0x00139C44
	private void ClearUI()
	{
		this.messageText.text = "ボタンのテキストを空にしました";
		for (int i = 0; i < this.rows.Count; i++)
		{
			this.rows[i].text.text = string.Empty;
		}
	}

	// Token: 0x0600358F RID: 13711 RVA: 0x0013B898 File Offset: 0x00139C98
	private void OnInputFieldClicked(int _buttonIndex)
	{
		if (_buttonIndex < 0 || this.rows.Count <= _buttonIndex)
		{
			return;
		}
		if (this.controller == null)
		{
			return;
		}
		this.controllerSelectDropDown.interactable = false;
		this.row = this.rows[_buttonIndex];
		this.updateMode = Remapping.UpdateMode.SettingOrDelete;
		this.messageText.text = string.Empty;
		this.settingOrDeleteWindow.SetActive(true);
	}

	// Token: 0x06003590 RID: 13712 RVA: 0x0013B90B File Offset: 0x00139D0B
	private void OnInputSetting()
	{
		this.messageText.text = "キーを押してください";
		this.CloseWindow(Remapping.UpdateMode.InputCheckMode);
	}

	// Token: 0x06003591 RID: 13713 RVA: 0x0013B924 File Offset: 0x00139D24
	private void OnDeleteInputSetting()
	{
		if (this.row != null)
		{
			if (this.controllerMap != null)
			{
				this.controllerMap.DeleteElementMapsWithAction(this.row.action.id);
			}
			this.row = null;
		}
		this.CloseWindow(Remapping.UpdateMode.ButtonSelectMode);
		this.controllerSelectDropDown.interactable = true;
		this.RedrawUI();
	}

	// Token: 0x06003592 RID: 13714 RVA: 0x0013B984 File Offset: 0x00139D84
	private void SetControllerToDropdown()
	{
		IList<Controller> list = new List<Controller>();
		if (this.CheckKeyboardSetting())
		{
			list.Add(this.player.controllers.GetController(ControllerType.Keyboard, 0));
		}
		if (this.CheckMouseSetting())
		{
			list.Add(this.player.controllers.GetController(ControllerType.Mouse, 0));
		}
		for (int i = 0; i < this.joystickCount; i++)
		{
			int id = this.joysticks[i].id;
			list.Add(this.player.controllers.GetController(ControllerType.Joystick, id));
		}
		this.controllerSelecter.SetController(list, (this.controller == null) ? string.Empty : this.controller.name);
	}

	// Token: 0x06003593 RID: 13715 RVA: 0x0013BA4A File Offset: 0x00139E4A
	public void SetSelectedController(int _controllerType, int _joystickIndexId)
	{
		if (_controllerType == 2)
		{
			this.joystickIndexId = _joystickIndexId;
		}
		this.SetSelectedController((ControllerType)_controllerType);
	}

	// Token: 0x06003594 RID: 13716 RVA: 0x0013BA61 File Offset: 0x00139E61
	private void OnReturnToButtonSelectMode()
	{
		this.row = null;
		this.CloseWindow(Remapping.UpdateMode.ButtonSelectMode);
		this.controllerSelectDropDown.interactable = true;
	}

	// Token: 0x06003595 RID: 13717 RVA: 0x0013BA7D File Offset: 0x00139E7D
	private void CloseWindow(Remapping.UpdateMode _updateMode)
	{
		this.updateMode = _updateMode;
		this.settingOrDeleteWindow.SetActive(false);
	}

	// Token: 0x06003596 RID: 13718 RVA: 0x0013BA92 File Offset: 0x00139E92
	private string GetControllerName(ControllerType _controllerType, int _controllerId)
	{
		return string.Copy(ReInput.players.GetPlayer(0).controllers.GetController(_controllerType, _controllerId).hardwareName);
	}

	// Token: 0x06003597 RID: 13719 RVA: 0x0013BAB8 File Offset: 0x00139EB8
	private void U_Update()
	{
		switch (this.updateMode)
		{
		case Remapping.UpdateMode.ButtonSelectMode:
			this.UpdateButtonSelectMode();
			break;
		case Remapping.UpdateMode.InputCheckMode:
			this.UpdateInputCheckMode();
			break;
		}
	}

	// Token: 0x06003598 RID: 13720 RVA: 0x0013BB03 File Offset: 0x00139F03
	private void UpdateButtonSelectMode()
	{
		if (Input.GetKeyDown(KeyCode.S))
		{
			this.SaveControllerSetting();
			this.RedrawUI();
		}
		else if (Input.GetKeyDown(KeyCode.L))
		{
			this.LoadControllerSetting();
		}
		if (Input.GetKeyDown(KeyCode.I))
		{
		}
	}

	// Token: 0x06003599 RID: 13721 RVA: 0x0013BB40 File Offset: 0x00139F40
	private void UpdateInputCheckMode()
	{
		ControllerPollingInfo pollingInfo = ReInput.controllers.polling.PollControllerForFirstElementDown(this.selectedControllerType, this.selectedControllerId);
		if (pollingInfo.success && pollingInfo.elementType == ControllerElementType.Button)
		{
			InputAction action = this.row.action;
			bool flag = this.controllerMap.ContainsAction(action.id);
			ActionElementMap actionElementMap = (!flag) ? null : this.controllerMap.GetElementMapsWithAction(action.id)[0];
			ElementAssignment elementAssignment = this.ToElementAssignment(pollingInfo, ModifierKeyFlags.None, this.row.actionRange, action.id, actionElementMap);
			ElementAssignmentConflictCheck conflictCheck = default(ElementAssignmentConflictCheck);
			if (this.CreateConflictCheck(elementAssignment, out conflictCheck, actionElementMap) && !ReInput.controllers.conflictChecking.DoesElementAssignmentConflict(conflictCheck))
			{
				this.controllerMap.ReplaceOrCreateElementMap(elementAssignment);
			}
			this.row = null;
			this.updateMode = Remapping.UpdateMode.ButtonSelectMode;
			this.controllerSelectDropDown.interactable = true;
			this.RedrawUI();
		}
	}

	// Token: 0x0600359A RID: 13722 RVA: 0x0013BC38 File Offset: 0x0013A038
	private void SetSelectedController(ControllerType _controllerType)
	{
		bool flag = true;
		if (this.selectedControllerType != _controllerType)
		{
			this.selectedControllerType = _controllerType;
			flag = true;
		}
		int num = this.selectedControllerId;
		if (this.selectedControllerType == ControllerType.Joystick)
		{
			if (this.joystickCount > 0)
			{
				if (this.joystickIndexId < 0)
				{
					this.joystickIndexId = 0;
				}
				else if (this.joystickCount <= this.joystickIndexId)
				{
					this.joystickIndexId = this.joystickCount - 1;
				}
				this.selectedControllerId = this.joysticks[this.joystickIndexId].id;
			}
			else
			{
				int joystickIndexId = -1;
				this.joystickIndexId = joystickIndexId;
				this.selectedControllerId = joystickIndexId;
			}
		}
		else
		{
			this.selectedControllerId = 0;
		}
		if (this.selectedControllerId != num)
		{
			flag = true;
		}
		this.SetControllerToDropdown();
		if (flag)
		{
			this.ControllerSettingToMapping(_controllerType);
			this.RedrawUI();
		}
	}

	// Token: 0x0600359B RID: 13723 RVA: 0x0013BD16 File Offset: 0x0013A116
	private void OnControllerChanged(ControllerStatusChangedEventArgs _args)
	{
		this.SetSelectedController(this.selectedControllerType);
	}

	// Token: 0x0600359C RID: 13724 RVA: 0x0013BD24 File Offset: 0x0013A124
	public void OnControllerSelected(int _controllerType)
	{
		this.SetSelectedController((ControllerType)_controllerType);
	}

	// Token: 0x0600359D RID: 13725 RVA: 0x0013BD30 File Offset: 0x0013A130
	private ElementAssignment ToElementAssignment(ControllerPollingInfo _pollingInfo, ModifierKeyFlags _modifierKeyFlag, AxisRange _axisRange, int _actionId, ActionElementMap _actionElementMap)
	{
		AxisRange axisRange = AxisRange.Positive;
		if (_pollingInfo.elementType == ControllerElementType.Axis)
		{
			if (_axisRange == AxisRange.Full)
			{
				axisRange = AxisRange.Full;
			}
			else
			{
				axisRange = ((_pollingInfo.axisPole != Pole.Positive) ? AxisRange.Negative : AxisRange.Positive);
			}
		}
		return new ElementAssignment(_pollingInfo.controllerType, _pollingInfo.elementType, _pollingInfo.elementIdentifierId, axisRange, _pollingInfo.keyboardKey, _modifierKeyFlag, _actionId, (_axisRange != AxisRange.Negative) ? Pole.Positive : Pole.Negative, false, (_actionElementMap == null) ? -1 : _actionElementMap.id);
	}

	// Token: 0x0600359E RID: 13726 RVA: 0x0013BDB4 File Offset: 0x0013A1B4
	private bool CreateConflictCheck(ElementAssignment _elementAssignment, out ElementAssignmentConflictCheck _conflictCheck, ActionElementMap _actionElementMap)
	{
		if (this.controllerMap == null || this.player == null)
		{
			_conflictCheck = default(ElementAssignmentConflictCheck);
			return false;
		}
		_conflictCheck = _elementAssignment.ToElementAssignmentConflictCheck();
		_conflictCheck.playerId = this.player.id;
		_conflictCheck.controllerType = this.controllerMap.controllerType;
		_conflictCheck.controllerMapId = this.controllerMap.id;
		_conflictCheck.controllerMapCategoryId = this.controllerMap.categoryId;
		if (_actionElementMap != null)
		{
			_conflictCheck.elementMapId = _actionElementMap.id;
		}
		return true;
	}

	// Token: 0x0600359F RID: 13727 RVA: 0x0013BE44 File Offset: 0x0013A244
	private void SetJoystickActiv()
	{
		foreach (Joystick joystick in this.player.controllers.Joysticks)
		{
			joystick.enabled = false;
		}
		if (this.joystick != null)
		{
			this.joystick.enabled = true;
		}
	}

	// Token: 0x060035A0 RID: 13728 RVA: 0x0013BEC0 File Offset: 0x0013A2C0
	public int GetJoystickIndexId()
	{
		return this.joystickIndexId;
	}

	// Token: 0x060035A1 RID: 13729 RVA: 0x0013BEC8 File Offset: 0x0013A2C8
	private void DeleteControllerSetting(ControllerType _controllerType, string _controllerName)
	{
		for (int i = 0; i < this.controllerList.Count; i++)
		{
			bool flag;
			if (_controllerType != ControllerType.Keyboard && _controllerType != ControllerType.Mouse)
			{
				flag = (_controllerName == this.controllerList[i].controllerName);
			}
			else
			{
				flag = (_controllerType == this.controllerList[i].controllerType);
			}
			if (flag)
			{
				this.controllerList.RemoveAt(i);
				i--;
			}
		}
	}

	// Token: 0x060035A2 RID: 13730 RVA: 0x0013BF52 File Offset: 0x0013A352
	private void ControllerSettingToMapping()
	{
		if (this.CheckKeyboardSetting())
		{
			this.ControllerSettingToMapping(ControllerType.Keyboard);
		}
		if (this.CheckMouseSetting())
		{
			this.ControllerSettingToMapping(ControllerType.Mouse);
		}
		this.ControllerSettingToMapping(ControllerType.Joystick);
	}

	// Token: 0x060035A3 RID: 13731 RVA: 0x0013BF80 File Offset: 0x0013A380
	private void ControllerSettingToMapping(ControllerType _controllerType)
	{
		if (_controllerType == ControllerType.Joystick && this.joystickCount <= 0)
		{
			return;
		}
		int num = (_controllerType != ControllerType.Joystick) ? 1 : this.joystickCount;
		for (int i = 0; i < num; i++)
		{
			ControllerSetting controllerSetting = null;
			foreach (ControllerSetting controllerSetting2 in this.controllerList)
			{
				if (_controllerType == ControllerType.Keyboard || _controllerType == ControllerType.Mouse)
				{
					if (controllerSetting2.controllerType == _controllerType)
					{
						controllerSetting = controllerSetting2;
						break;
					}
				}
				else
				{
					if (this.joysticks == null)
					{
						break;
					}
					if (this.joysticks[i].hardwareName == controllerSetting2.controllerName)
					{
						controllerSetting = controllerSetting2;
						break;
					}
				}
			}
			if (controllerSetting != null)
			{
				int controllerId = (_controllerType != ControllerType.Joystick) ? 0 : this.joysticks[i].id;
				foreach (ControllerMap controllerMap in this.player.controllers.maps.GetMaps(controllerSetting.controllerType, controllerId))
				{
					controllerMap.ClearElementMaps();
				}
				foreach (InputSetting inputSetting in controllerSetting.elements)
				{
					int categoryId = inputSetting.categoryId;
					int layoutId = inputSetting.layoutId;
					this.player.controllers.maps.GetMap(controllerSetting.controllerType, controllerId, categoryId, layoutId).ReplaceOrCreateElementMap(inputSetting.ToElementAssignmentCreateSetting());
				}
			}
		}
	}

	// Token: 0x060035A4 RID: 13732 RVA: 0x0013C180 File Offset: 0x0013A580
	private void ControllerMappingToSetting()
	{
		if (this.CheckKeyboardSetting())
		{
			this.ControllerMappingToSetting(ControllerType.Keyboard);
		}
		if (this.CheckMouseSetting())
		{
			this.ControllerMappingToSetting(ControllerType.Mouse);
		}
		this.ControllerMappingToSetting(ControllerType.Joystick);
	}

	// Token: 0x060035A5 RID: 13733 RVA: 0x0013C1B0 File Offset: 0x0013A5B0
	private void ControllerMappingToSetting(ControllerType _controllerType)
	{
		List<ControllerSetting> list = new List<ControllerSetting>();
		switch (_controllerType)
		{
		case ControllerType.Keyboard:
		case ControllerType.Mouse:
		{
			this.DeleteControllerSetting(_controllerType, null);
			ControllerSetting item = new ControllerSetting(_controllerType, 0);
			if (_controllerType == ControllerType.Keyboard)
			{
				list.Add(item);
			}
			break;
		}
		case ControllerType.Joystick:
			for (int i = 0; i < this.joystickCount; i++)
			{
				ControllerSetting controllerSetting = new ControllerSetting(_controllerType, this.joysticks[i].id);
				this.DeleteControllerSetting(_controllerType, controllerSetting.controllerName);
				list.Add(controllerSetting);
			}
			break;
		}
		foreach (ControllerSetting controllerSetting2 in list)
		{
			if (controllerSetting2.maps != null)
			{
				this.controllerList.Add(controllerSetting2);
				foreach (ControllerMap controllerMap in controllerSetting2.maps)
				{
					foreach (ActionElementMap actionElementMap in controllerMap.AllMaps)
					{
						controllerSetting2.AddElement(new InputSetting(actionElementMap));
					}
				}
			}
		}
		list.Clear();
	}

	// Token: 0x060035A6 RID: 13734 RVA: 0x0013C350 File Offset: 0x0013A750
	private void SaveControllerSetting()
	{
		this.ControllerMappingToSetting();
		if (!Directory.Exists("UserData/save/"))
		{
			Directory.CreateDirectory("UserData/save/");
		}
		this.SaveFile("UserData/save/keyconfig");
	}

	// Token: 0x060035A7 RID: 13735 RVA: 0x0013C380 File Offset: 0x0013A780
	public void SaveFile(string _path)
	{
		using (FileStream fileStream = new FileStream(_path, FileMode.Create, FileAccess.Write, FileShare.Write))
		{
			this.SaveFile(fileStream);
		}
	}

	// Token: 0x060035A8 RID: 13736 RVA: 0x0013C3C0 File Offset: 0x0013A7C0
	public void SaveFile(Stream _stream)
	{
		using (BinaryWriter binaryWriter = new BinaryWriter(_stream))
		{
			this.SaveFile(binaryWriter);
		}
	}

	// Token: 0x060035A9 RID: 13737 RVA: 0x0013C400 File Offset: 0x0013A800
	public void SaveFile(BinaryWriter _writer)
	{
		_writer.Write(this.controllerList.Count);
		foreach (ControllerSetting controllerSetting in this.controllerList)
		{
			_writer.Write(controllerSetting.controllerName);
			_writer.Write((int)controllerSetting.controllerType);
			_writer.Write(controllerSetting.elements.Count);
			foreach (InputSetting inputSetting in controllerSetting.elements)
			{
				_writer.Write(inputSetting.categoryId);
				_writer.Write(inputSetting.layoutId);
				_writer.Write((int)inputSetting.controllerType);
				_writer.Write((int)inputSetting.elementType);
				_writer.Write(inputSetting.elementIdentifierId);
				_writer.Write((int)inputSetting.axisRange);
				_writer.Write((int)inputSetting.keyboardKey);
				_writer.Write((int)inputSetting.modifierKeyFlags);
				_writer.Write(inputSetting.actionId);
				_writer.Write((int)inputSetting.axisContribution);
				_writer.Write(inputSetting.invert);
				_writer.Write(inputSetting.elementMapId);
			}
		}
	}

	// Token: 0x060035AA RID: 13738 RVA: 0x0013C57C File Offset: 0x0013A97C
	private void LoadControllerSetting()
	{
		this.LoadFile("UserData/save/keyconfig");
		this.ControllerSettingToMapping();
		this.RedrawUI();
	}

	// Token: 0x060035AB RID: 13739 RVA: 0x0013C598 File Offset: 0x0013A998
	public void LoadFile(string _path)
	{
		try
		{
			using (FileStream fileStream = new FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				if (fileStream.Length != 0L)
				{
					this.Load(fileStream);
				}
			}
		}
		catch (Exception ex)
		{
			if (!(ex is FileNotFoundException))
			{
				if (ex is DirectoryNotFoundException)
				{
				}
			}
		}
	}

	// Token: 0x060035AC RID: 13740 RVA: 0x0013C61C File Offset: 0x0013AA1C
	public void Load(Stream _stream)
	{
		using (BinaryReader binaryReader = new BinaryReader(_stream))
		{
			this.Load(binaryReader);
		}
	}

	// Token: 0x060035AD RID: 13741 RVA: 0x0013C65C File Offset: 0x0013AA5C
	public void Load(BinaryReader _reader)
	{
		try
		{
			int num = _reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				string controllerName = _reader.ReadString();
				ControllerType controllerType = (ControllerType)_reader.ReadInt32();
				int num2 = _reader.ReadInt32();
				this.DeleteControllerSetting(controllerType, controllerName);
				ControllerSetting controllerSetting = new ControllerSetting(controllerName, controllerType);
				for (int j = 0; j < num2; j++)
				{
					InputSetting inputSetting = new InputSetting(_reader.ReadInt32(), _reader.ReadInt32(), (ControllerType)_reader.ReadInt32(), (ControllerElementType)_reader.ReadInt32(), _reader.ReadInt32(), (AxisRange)_reader.ReadInt32(), (KeyCode)_reader.ReadInt32(), (ModifierKeyFlags)_reader.ReadInt32(), _reader.ReadInt32(), (Pole)_reader.ReadInt32(), _reader.ReadBoolean(), _reader.ReadInt32());
					controllerSetting.AddElement(inputSetting);
				}
				this.controllerList.Add(controllerSetting);
			}
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x060035AE RID: 13742 RVA: 0x0013C744 File Offset: 0x0013AB44
	private void ControllerDefaultSetting()
	{
	}

	// Token: 0x060035AF RID: 13743 RVA: 0x0013C748 File Offset: 0x0013AB48
	private void LogActionMap()
	{
		foreach (InputAction inputAction in ReInput.mapping.Actions)
		{
		}
	}

	// Token: 0x060035B0 RID: 13744 RVA: 0x0013C7A0 File Offset: 0x0013ABA0
	private void LogControllerAllButton()
	{
		if (this.selectedControllerType == ControllerType.Keyboard)
		{
			Keyboard keyboard = this.player.controllers.Keyboard;
			if (keyboard != null)
			{
				int num = 0;
				foreach (ControllerElementIdentifier element in keyboard.ButtonElementIdentifiers)
				{
					this.LogControllerElementIdentifier(element);
					num++;
				}
			}
		}
		else if (this.selectedControllerType == ControllerType.Mouse)
		{
			Mouse mouse = this.player.controllers.Mouse;
			if (mouse != null)
			{
				int num2 = 0;
				foreach (ControllerElementIdentifier element2 in mouse.ButtonElementIdentifiers)
				{
					this.LogControllerElementIdentifier(element2);
					num2++;
				}
			}
		}
		else if (this.selectedControllerType == ControllerType.Joystick && this.joystick != null)
		{
			int num3 = 0;
			foreach (ControllerElementIdentifier element3 in this.joystick.AxisElementIdentifiers)
			{
				this.LogControllerElementIdentifier(element3);
				num3++;
			}
			foreach (ControllerElementIdentifier element4 in this.joystick.ButtonElementIdentifiers)
			{
				this.LogControllerElementIdentifier(element4);
				num3++;
			}
		}
	}

	// Token: 0x060035B1 RID: 13745 RVA: 0x0013C978 File Offset: 0x0013AD78
	private void LogControllerElementIdentifier(ControllerElementIdentifier _element)
	{
	}

	// Token: 0x04003603 RID: 13827
	private const Remapping.SettingControllerMode settingControllerMode = Remapping.SettingControllerMode.JoystickKeyboard;

	// Token: 0x04003604 RID: 13828
	private Remapping.UpdateMode updateMode;

	// Token: 0x04003605 RID: 13829
	private const string categoryName = "Default";

	// Token: 0x04003606 RID: 13830
	private const string layoutName = "Default";

	// Token: 0x04003607 RID: 13831
	public GameObject actionNameTextPrefab;

	// Token: 0x04003608 RID: 13832
	public GameObject inputButtonPrefab;

	// Token: 0x04003609 RID: 13833
	public RectTransform actionNameSortArea;

	// Token: 0x0400360A RID: 13834
	public RectTransform keyInputButtonSortArea;

	// Token: 0x0400360B RID: 13835
	public Text messageText;

	// Token: 0x0400360C RID: 13836
	public GameObject settingOrDeleteWindow;

	// Token: 0x0400360D RID: 13837
	public Button settingButton;

	// Token: 0x0400360E RID: 13838
	public Button deleteButton;

	// Token: 0x0400360F RID: 13839
	public Button returnButton;

	// Token: 0x04003610 RID: 13840
	public Dropdown controllerSelectDropDown;

	// Token: 0x04003611 RID: 13841
	public ControllerSelect controllerSelecter;

	// Token: 0x04003612 RID: 13842
	private ControllerType selectedControllerType = ControllerType.Joystick;

	// Token: 0x04003613 RID: 13843
	private int joystickIndexId_;

	// Token: 0x04003614 RID: 13844
	private int selectedControllerId;

	// Token: 0x04003615 RID: 13845
	private List<Remapping.Row> rows = new List<Remapping.Row>();

	// Token: 0x04003616 RID: 13846
	private Remapping.Row row;

	// Token: 0x04003617 RID: 13847
	private List<ControllerSetting> controllerList = new List<ControllerSetting>();

	// Token: 0x04003618 RID: 13848
	private const string saveFileDirectoryName = "UserData/save/";

	// Token: 0x04003619 RID: 13849
	private const string saveFileName = "keyconfig";

	// Token: 0x02000837 RID: 2103
	private enum UpdateMode
	{
		// Token: 0x0400361B RID: 13851
		ButtonSelectMode,
		// Token: 0x0400361C RID: 13852
		SettingOrDelete,
		// Token: 0x0400361D RID: 13853
		InputCheckMode
	}

	// Token: 0x02000838 RID: 2104
	private enum SettingControllerMode
	{
		// Token: 0x0400361F RID: 13855
		Joystick,
		// Token: 0x04003620 RID: 13856
		JoystickKeyboard,
		// Token: 0x04003621 RID: 13857
		AllController
	}

	// Token: 0x02000839 RID: 2105
	private class Row
	{
		// Token: 0x04003622 RID: 13858
		public InputAction action;

		// Token: 0x04003623 RID: 13859
		public AxisRange actionRange;

		// Token: 0x04003624 RID: 13860
		public Button button;

		// Token: 0x04003625 RID: 13861
		public Text text;
	}
}
