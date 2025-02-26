using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000A2A RID: 2602
public class UI_OnMouseOverMessageEx : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IEventSystemHandler
{
	// Token: 0x06004D4D RID: 19789 RVA: 0x001DAC0C File Offset: 0x001D900C
	private void Start()
	{
		if (null != this.imgMessage)
		{
			this.imgMessage.enabled = false;
		}
		if (null != this.textMessage)
		{
			this.textMessage.enabled = false;
		}
	}

	// Token: 0x06004D4E RID: 19790 RVA: 0x001DAC48 File Offset: 0x001D9048
	public void OnPointerEnter(PointerEventData ped)
	{
		if (null != this.imgMessage)
		{
			this.imgMessage.enabled = true;
		}
		if (null != this.textMessage)
		{
			this.textMessage.enabled = true;
			if (this.strMessage != null && this.showMsgNo < this.strMessage.Length && this.strMessage[this.showMsgNo] != null)
			{
				this.textMessage.text = this.strMessage[this.showMsgNo];
			}
		}
	}

	// Token: 0x06004D4F RID: 19791 RVA: 0x001DACD7 File Offset: 0x001D90D7
	public void OnPointerExit(PointerEventData ped)
	{
		if (null != this.imgMessage)
		{
			this.imgMessage.enabled = false;
		}
		if (null != this.textMessage)
		{
			this.textMessage.enabled = false;
		}
	}

	// Token: 0x040046B5 RID: 18101
	[SerializeField]
	private Image imgMessage;

	// Token: 0x040046B6 RID: 18102
	[SerializeField]
	private Text textMessage;

	// Token: 0x040046B7 RID: 18103
	[SerializeField]
	private string[] strMessage;

	// Token: 0x040046B8 RID: 18104
	public int showMsgNo;
}
