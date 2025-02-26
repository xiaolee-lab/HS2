using System;
using System.Collections.Generic;
using Rewired;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000832 RID: 2098
public class ControllerSelect : MonoBehaviour
{
	// Token: 0x06003551 RID: 13649 RVA: 0x0013AA74 File Offset: 0x00138E74
	public void SetController(IList<Controller> _controllers, string _controllerName)
	{
		this.stateList.Clear();
		this.dropdown.options.Clear();
		this.dropdown.captionText.text = string.Empty;
		int num = 0;
		this.dropdown.interactable = (_controllers.Count > 0);
		for (int i = 0; i < _controllers.Count; i++)
		{
			Controller controller = _controllers[i];
			string name = controller.name;
			Dropdown.OptionData item = new Dropdown.OptionData(name);
			this.dropdown.options.Add(item);
			if (name == _controllerName)
			{
				this.dropdown.captionText.text = name;
				this.dropdown.value = i;
			}
			this.stateList.Add(new ControllerSelect.ControllerState((int)controller.type, (controller.type == ControllerType.Joystick) ? num : 0));
			if (controller.type == ControllerType.Joystick)
			{
				num++;
			}
		}
	}

	// Token: 0x06003552 RID: 13650 RVA: 0x0013AB6C File Offset: 0x00138F6C
	public void ChangeController(Dropdown _dropdown)
	{
		if (_dropdown.value < 0 || this.stateList.Count <= _dropdown.value)
		{
			return;
		}
		this.remapping.SetSelectedController(this.stateList[_dropdown.value].type, this.stateList[_dropdown.value].joystickIndexId);
	}

	// Token: 0x040035EE RID: 13806
	public Dropdown dropdown;

	// Token: 0x040035EF RID: 13807
	public Remapping remapping;

	// Token: 0x040035F0 RID: 13808
	private List<ControllerSelect.ControllerState> stateList = new List<ControllerSelect.ControllerState>();

	// Token: 0x02000833 RID: 2099
	private struct ControllerState
	{
		// Token: 0x06003553 RID: 13651 RVA: 0x0013ABD9 File Offset: 0x00138FD9
		public ControllerState(int _type, int _joystickIndexId)
		{
			this.type = _type;
			this.joystickIndexId = _joystickIndexId;
		}

		// Token: 0x040035F1 RID: 13809
		public int type;

		// Token: 0x040035F2 RID: 13810
		public int joystickIndexId;
	}
}
