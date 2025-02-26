using System;
using Rewired;
using UnityEngine;

// Token: 0x02000835 RID: 2101
public class InputSetting
{
	// Token: 0x0600355F RID: 13663 RVA: 0x0013AE0C File Offset: 0x0013920C
	public InputSetting(int _categoryId, int _layoutId, ControllerType _controllerType, ControllerElementType _elementType, int _elementIdentifierId, AxisRange _axisRange, KeyCode _keyboardKey, ModifierKeyFlags _modifierKeyFlags, int _actionId, Pole _axisContribution, bool _invert, int _elementMapId)
	{
		this.categoryId = _categoryId;
		this.layoutId = _layoutId;
		this.controllerType = _controllerType;
		this.elementType = _elementType;
		this.elementIdentifierId = _elementIdentifierId;
		this.axisRange = _axisRange;
		this.keyboardKey = _keyboardKey;
		this.modifierKeyFlags = _modifierKeyFlags;
		this.actionId = _actionId;
		this.axisContribution = _axisContribution;
		this.invert = _invert;
		this.elementMapId = _elementMapId;
	}

	// Token: 0x06003560 RID: 13664 RVA: 0x0013AE7C File Offset: 0x0013927C
	public InputSetting(ControllerType _controllerType, ControllerElementType _elementType, int _elementIdentifierId, AxisRange _axisRange, KeyCode _keyboardKey, ModifierKeyFlags _modifierKeyFlags, int _actionId, Pole _axisContribution, bool _invert, int _elementMapId)
	{
		int num = 0;
		this.layoutId = num;
		this.categoryId = num;
		this.controllerType = _controllerType;
		this.elementType = _elementType;
		this.elementIdentifierId = _elementIdentifierId;
		this.axisRange = _axisRange;
		this.keyboardKey = _keyboardKey;
		this.modifierKeyFlags = _modifierKeyFlags;
		this.actionId = _actionId;
		this.axisContribution = _axisContribution;
		this.invert = _invert;
		this.elementMapId = _elementMapId;
	}

	// Token: 0x06003561 RID: 13665 RVA: 0x0013AEEC File Offset: 0x001392EC
	public InputSetting(ActionElementMap _actionElementMap)
	{
		this.categoryId = _actionElementMap.controllerMap.categoryId;
		this.layoutId = _actionElementMap.controllerMap.layoutId;
		this.controllerType = _actionElementMap.controllerMap.controllerType;
		this.elementType = _actionElementMap.elementType;
		this.elementIdentifierId = _actionElementMap.elementIdentifierId;
		this.axisRange = _actionElementMap.axisRange;
		this.keyboardKey = _actionElementMap.keyCode;
		this.modifierKeyFlags = _actionElementMap.modifierKeyFlags;
		this.actionId = _actionElementMap.actionId;
		this.axisContribution = _actionElementMap.axisContribution;
		this.invert = _actionElementMap.invert;
		this.elementMapId = _actionElementMap.id;
	}

	// Token: 0x170009A0 RID: 2464
	// (get) Token: 0x06003562 RID: 13666 RVA: 0x0013AF9E File Offset: 0x0013939E
	// (set) Token: 0x06003563 RID: 13667 RVA: 0x0013AFA6 File Offset: 0x001393A6
	public int categoryId { get; private set; }

	// Token: 0x170009A1 RID: 2465
	// (get) Token: 0x06003564 RID: 13668 RVA: 0x0013AFAF File Offset: 0x001393AF
	// (set) Token: 0x06003565 RID: 13669 RVA: 0x0013AFB7 File Offset: 0x001393B7
	public int layoutId { get; private set; }

	// Token: 0x170009A2 RID: 2466
	// (get) Token: 0x06003566 RID: 13670 RVA: 0x0013AFC0 File Offset: 0x001393C0
	// (set) Token: 0x06003567 RID: 13671 RVA: 0x0013AFC8 File Offset: 0x001393C8
	public ControllerType controllerType { get; private set; }

	// Token: 0x170009A3 RID: 2467
	// (get) Token: 0x06003568 RID: 13672 RVA: 0x0013AFD1 File Offset: 0x001393D1
	// (set) Token: 0x06003569 RID: 13673 RVA: 0x0013AFD9 File Offset: 0x001393D9
	public ControllerElementType elementType { get; private set; }

	// Token: 0x170009A4 RID: 2468
	// (get) Token: 0x0600356A RID: 13674 RVA: 0x0013AFE2 File Offset: 0x001393E2
	// (set) Token: 0x0600356B RID: 13675 RVA: 0x0013AFEA File Offset: 0x001393EA
	public int elementIdentifierId { get; private set; }

	// Token: 0x170009A5 RID: 2469
	// (get) Token: 0x0600356C RID: 13676 RVA: 0x0013AFF3 File Offset: 0x001393F3
	// (set) Token: 0x0600356D RID: 13677 RVA: 0x0013AFFB File Offset: 0x001393FB
	public AxisRange axisRange { get; private set; }

	// Token: 0x170009A6 RID: 2470
	// (get) Token: 0x0600356E RID: 13678 RVA: 0x0013B004 File Offset: 0x00139404
	// (set) Token: 0x0600356F RID: 13679 RVA: 0x0013B00C File Offset: 0x0013940C
	public KeyCode keyboardKey { get; private set; }

	// Token: 0x170009A7 RID: 2471
	// (get) Token: 0x06003570 RID: 13680 RVA: 0x0013B015 File Offset: 0x00139415
	// (set) Token: 0x06003571 RID: 13681 RVA: 0x0013B01D File Offset: 0x0013941D
	public ModifierKeyFlags modifierKeyFlags { get; private set; }

	// Token: 0x170009A8 RID: 2472
	// (get) Token: 0x06003572 RID: 13682 RVA: 0x0013B026 File Offset: 0x00139426
	// (set) Token: 0x06003573 RID: 13683 RVA: 0x0013B02E File Offset: 0x0013942E
	public int actionId { get; private set; }

	// Token: 0x170009A9 RID: 2473
	// (get) Token: 0x06003574 RID: 13684 RVA: 0x0013B037 File Offset: 0x00139437
	// (set) Token: 0x06003575 RID: 13685 RVA: 0x0013B03F File Offset: 0x0013943F
	public Pole axisContribution { get; private set; }

	// Token: 0x170009AA RID: 2474
	// (get) Token: 0x06003576 RID: 13686 RVA: 0x0013B048 File Offset: 0x00139448
	// (set) Token: 0x06003577 RID: 13687 RVA: 0x0013B050 File Offset: 0x00139450
	public bool invert { get; private set; }

	// Token: 0x170009AB RID: 2475
	// (get) Token: 0x06003578 RID: 13688 RVA: 0x0013B059 File Offset: 0x00139459
	// (set) Token: 0x06003579 RID: 13689 RVA: 0x0013B061 File Offset: 0x00139461
	public int elementMapId { get; private set; }

	// Token: 0x0600357A RID: 13690 RVA: 0x0013B06C File Offset: 0x0013946C
	public ElementAssignment ToElementAssignment()
	{
		return new ElementAssignment(this.controllerType, this.elementType, this.elementIdentifierId, this.axisRange, this.keyboardKey, this.modifierKeyFlags, this.actionId, this.axisContribution, this.invert, this.elementMapId);
	}

	// Token: 0x0600357B RID: 13691 RVA: 0x0013B0BC File Offset: 0x001394BC
	public ElementAssignment ToElementAssignmentCreateSetting()
	{
		return new ElementAssignment(this.controllerType, this.elementType, this.elementIdentifierId, this.axisRange, this.keyboardKey, this.modifierKeyFlags, this.actionId, this.axisContribution, this.invert, -1);
	}
}
