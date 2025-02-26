using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000416 RID: 1046
public class PEButtonScript : MonoBehaviour, IEventSystemHandler, IPointerEnterHandler, IPointerExitHandler
{
	// Token: 0x06001310 RID: 4880 RVA: 0x00075787 File Offset: 0x00073B87
	private void Start()
	{
		this.myButton = base.gameObject.GetComponent<Button>();
	}

	// Token: 0x06001311 RID: 4881 RVA: 0x0007579A File Offset: 0x00073B9A
	public void OnPointerEnter(PointerEventData eventData)
	{
		UICanvasManager.GlobalAccess.MouseOverButton = true;
		UICanvasManager.GlobalAccess.UpdateToolTip(this.ButtonType);
	}

	// Token: 0x06001312 RID: 4882 RVA: 0x000757B7 File Offset: 0x00073BB7
	public void OnPointerExit(PointerEventData eventData)
	{
		UICanvasManager.GlobalAccess.MouseOverButton = false;
		UICanvasManager.GlobalAccess.ClearToolTip();
	}

	// Token: 0x06001313 RID: 4883 RVA: 0x000757CE File Offset: 0x00073BCE
	public void OnButtonClicked()
	{
		UICanvasManager.GlobalAccess.UIButtonClick(this.ButtonType);
	}

	// Token: 0x04001544 RID: 5444
	private Button myButton;

	// Token: 0x04001545 RID: 5445
	public ButtonTypes ButtonType;
}
