using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000A2B RID: 2603
public class UI_RaycastCtrl : MonoBehaviour
{
	// Token: 0x06004D51 RID: 19793 RVA: 0x001DAD1C File Offset: 0x001D911C
	private void GetImageComponents()
	{
		Image[] componentsInChildren = base.GetComponentsInChildren<Image>(true);
		this.imgRaycastTargetOn = (from img in componentsInChildren
		where img.gameObject.name != "Viewport"
		where img.raycastTarget
		select img).ToArray<Image>();
	}

	// Token: 0x06004D52 RID: 19794 RVA: 0x001DAD84 File Offset: 0x001D9184
	private void GetCanvasGroup()
	{
		List<CanvasGroup> list = new List<CanvasGroup>();
		CanvasGroup component = base.GetComponent<CanvasGroup>();
		if (null != component)
		{
			list.Add(component);
		}
		Transform parent = base.transform.parent;
		if (null == parent)
		{
			return;
		}
		for (;;)
		{
			component = parent.GetComponent<CanvasGroup>();
			if (null != component)
			{
				list.Add(component);
			}
			if (null == parent.parent)
			{
				break;
			}
			parent = parent.parent;
		}
		this.canvasGrp = list.ToArray();
	}

	// Token: 0x06004D53 RID: 19795 RVA: 0x001DAE14 File Offset: 0x001D9214
	public void ChangeRaycastTarget(bool enable)
	{
		foreach (Image image in this.imgRaycastTargetOn)
		{
			image.raycastTarget = enable;
		}
	}

	// Token: 0x06004D54 RID: 19796 RVA: 0x001DAE47 File Offset: 0x001D9247
	public void Reset()
	{
		this.GetCanvasGroup();
		this.GetImageComponents();
	}

	// Token: 0x06004D55 RID: 19797 RVA: 0x001DAE58 File Offset: 0x001D9258
	private void Update()
	{
		if (this.canvasGrp == null || this.imgRaycastTargetOn == null)
		{
			return;
		}
		bool enable = true;
		foreach (CanvasGroup canvasGroup in this.canvasGrp)
		{
			if (!canvasGroup.blocksRaycasts)
			{
				enable = false;
				break;
			}
		}
		this.ChangeRaycastTarget(enable);
	}

	// Token: 0x040046B9 RID: 18105
	[SerializeField]
	private CanvasGroup[] canvasGrp;

	// Token: 0x040046BA RID: 18106
	[SerializeField]
	private Image[] imgRaycastTargetOn;
}
