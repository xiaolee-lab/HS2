using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000A29 RID: 2601
public class UI_OnMouseOverMessage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IEventSystemHandler
{
	// Token: 0x06004D48 RID: 19784 RVA: 0x001DAAE6 File Offset: 0x001D8EE6
	private void Start()
	{
		if (null != this.imgComment)
		{
			this.imgComment.enabled = false;
		}
		if (null != this.txtComment)
		{
			this.txtComment.enabled = false;
		}
	}

	// Token: 0x06004D49 RID: 19785 RVA: 0x001DAB24 File Offset: 0x001D8F24
	public void OnPointerEnter(PointerEventData ped)
	{
		if (this.active)
		{
			if (null != this.imgComment)
			{
				this.imgComment.enabled = true;
			}
			if (null != this.txtComment)
			{
				this.txtComment.enabled = true;
				this.txtComment.text = this.comment;
			}
		}
		else
		{
			if (null != this.imgComment)
			{
				this.imgComment.enabled = false;
			}
			if (null != this.txtComment)
			{
				this.txtComment.enabled = false;
			}
		}
	}

	// Token: 0x06004D4A RID: 19786 RVA: 0x001DABC6 File Offset: 0x001D8FC6
	public void OnPointerExit(PointerEventData ped)
	{
		if (null != this.imgComment)
		{
			this.imgComment.enabled = false;
		}
		if (null != this.txtComment)
		{
			this.txtComment.enabled = false;
		}
	}

	// Token: 0x06004D4B RID: 19787 RVA: 0x001DAC02 File Offset: 0x001D9002
	private void Update()
	{
	}

	// Token: 0x040046B1 RID: 18097
	public bool active = true;

	// Token: 0x040046B2 RID: 18098
	public Image imgComment;

	// Token: 0x040046B3 RID: 18099
	public Text txtComment;

	// Token: 0x040046B4 RID: 18100
	public string comment = string.Empty;
}
