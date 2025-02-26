using System;
using System.Collections.Generic;
using Rewired;

// Token: 0x02000834 RID: 2100
public class ControllerSetting
{
	// Token: 0x06003554 RID: 13652 RVA: 0x0013ABE9 File Offset: 0x00138FE9
	public ControllerSetting()
	{
		this.controllerName = string.Empty;
		this.controllerType = ControllerType.Keyboard;
		this.elements = new List<InputSetting>();
	}

	// Token: 0x06003555 RID: 13653 RVA: 0x0013AC24 File Offset: 0x00139024
	public ControllerSetting(string _controllerName)
	{
		this.controllerName = string.Copy(_controllerName);
		this.elements = new List<InputSetting>();
	}

	// Token: 0x06003556 RID: 13654 RVA: 0x0013AC59 File Offset: 0x00139059
	public ControllerSetting(ControllerType _controllerType, int _controllerId)
	{
		this.Setting(_controllerType, _controllerId);
	}

	// Token: 0x06003557 RID: 13655 RVA: 0x0013AC7F File Offset: 0x0013907F
	public ControllerSetting(string _controllerName, ControllerType _controllerType)
	{
		this.controllerName = string.Copy(_controllerName);
		this.controllerType = _controllerType;
		this.elements = new List<InputSetting>();
	}

	// Token: 0x06003558 RID: 13656 RVA: 0x0013ACBC File Offset: 0x001390BC
	public ControllerSetting(string _controllerName, ControllerType _controllerType, IList<ControllerMap> _maps)
	{
		this.controllerName = string.Copy(_controllerName);
		this.controllerType = _controllerType;
		this.elements = new List<InputSetting>();
		this.maps = _maps;
	}

	// Token: 0x06003559 RID: 13657 RVA: 0x0013AD0A File Offset: 0x0013910A
	public ControllerSetting(string _controllerName, List<InputSetting> _elements)
	{
		this.controllerName = string.Copy(_controllerName);
		this.controllerType = ControllerType.Keyboard;
		this.elements = _elements;
	}

	// Token: 0x0600355A RID: 13658 RVA: 0x0013AD42 File Offset: 0x00139142
	public ControllerSetting(string _controllerName, ControllerType _controllerType, List<InputSetting> _elements)
	{
		this.controllerName = string.Copy(_controllerName);
		this.controllerType = _controllerType;
		this.elements = _elements;
	}

	// Token: 0x0600355B RID: 13659 RVA: 0x0013AD7A File Offset: 0x0013917A
	public void AddElement(InputSetting _inputSetting)
	{
		this.elements.Add(_inputSetting);
	}

	// Token: 0x0600355C RID: 13660 RVA: 0x0013AD88 File Offset: 0x00139188
	public void Clear()
	{
		this.controllerName = string.Empty;
		this.elements = new List<InputSetting>();
	}

	// Token: 0x0600355D RID: 13661 RVA: 0x0013ADA0 File Offset: 0x001391A0
	public void Setting(ControllerType _controllerType, int _controllerId)
	{
		this.controllerName = string.Copy(ReInput.players.GetPlayer(0).controllers.GetController(_controllerType, _controllerId).hardwareName);
		this.controllerType = _controllerType;
		this.maps = ReInput.players.GetPlayer(0).controllers.maps.GetMaps(_controllerType, _controllerId);
	}

	// Token: 0x0600355E RID: 13662 RVA: 0x0013ADFD File Offset: 0x001391FD
	public void Setting(int _controllerId)
	{
		this.Setting(this.controllerType, _controllerId);
	}

	// Token: 0x040035F3 RID: 13811
	public IList<ControllerMap> maps;

	// Token: 0x040035F4 RID: 13812
	public string controllerName = string.Empty;

	// Token: 0x040035F5 RID: 13813
	public ControllerType controllerType;

	// Token: 0x040035F6 RID: 13814
	public List<InputSetting> elements = new List<InputSetting>();
}
