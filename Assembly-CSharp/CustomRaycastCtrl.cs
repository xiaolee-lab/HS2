using System;
using System.Linq;
using UnityEngine;

// Token: 0x020009D1 RID: 2513
public class CustomRaycastCtrl : MonoBehaviour
{
	// Token: 0x060049A3 RID: 18851 RVA: 0x001C1610 File Offset: 0x001BFA10
	private void GetRaycastCtrlComponents()
	{
		UI_RaycastCtrl[] componentsInChildren = base.GetComponentsInChildren<UI_RaycastCtrl>(true);
		this.raycastCtrl = componentsInChildren.ToArray<UI_RaycastCtrl>();
	}

	// Token: 0x060049A4 RID: 18852 RVA: 0x001C1634 File Offset: 0x001BFA34
	private void UpdateRaycastCtrl()
	{
		foreach (UI_RaycastCtrl ui_RaycastCtrl in this.raycastCtrl)
		{
			ui_RaycastCtrl.Reset();
		}
	}

	// Token: 0x060049A5 RID: 18853 RVA: 0x001C1666 File Offset: 0x001BFA66
	private void Reset()
	{
		this.GetRaycastCtrlComponents();
	}

	// Token: 0x04004444 RID: 17476
	[Button("GetRaycastCtrlComponents", "取得", new object[]
	{

	})]
	public int getRaycastCtrlComponents;

	// Token: 0x04004445 RID: 17477
	[Button("UpdateRaycastCtrl", "全更新", new object[]
	{

	})]
	public int updateRaycastCtrl;

	// Token: 0x04004446 RID: 17478
	[SerializeField]
	private UI_RaycastCtrl[] raycastCtrl;
}
