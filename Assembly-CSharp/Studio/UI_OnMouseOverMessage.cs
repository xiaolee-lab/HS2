using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x02001360 RID: 4960
	public class UI_OnMouseOverMessage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IEventSystemHandler
	{
		// Token: 0x0600A650 RID: 42576 RVA: 0x0043A10F File Offset: 0x0043850F
		private void Start()
		{
			if (this.imgComment)
			{
				this.imgComment.enabled = false;
			}
			if (this.txtComment)
			{
				this.txtComment.enabled = false;
			}
		}

		// Token: 0x0600A651 RID: 42577 RVA: 0x0043A14C File Offset: 0x0043854C
		public void OnPointerEnter(PointerEventData ped)
		{
			if (this.active)
			{
				if (this.imgComment)
				{
					this.imgComment.enabled = true;
				}
				if (this.txtComment)
				{
					this.txtComment.enabled = true;
					this.txtComment.text = this.comment;
				}
			}
			else
			{
				if (this.imgComment)
				{
					this.imgComment.enabled = false;
				}
				if (this.txtComment)
				{
					this.txtComment.enabled = false;
				}
			}
		}

		// Token: 0x0600A652 RID: 42578 RVA: 0x0043A1EA File Offset: 0x004385EA
		public void OnPointerExit(PointerEventData ped)
		{
			if (this.imgComment)
			{
				this.imgComment.enabled = false;
			}
			if (this.txtComment)
			{
				this.txtComment.enabled = false;
			}
		}

		// Token: 0x040082A5 RID: 33445
		public bool active = true;

		// Token: 0x040082A6 RID: 33446
		public Image imgComment;

		// Token: 0x040082A7 RID: 33447
		public TextMeshProUGUI txtComment;

		// Token: 0x040082A8 RID: 33448
		public string comment = string.Empty;
	}
}
