using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x020012EE RID: 4846
	public class VoicePlayNode : VoiceNode
	{
		// Token: 0x17002213 RID: 8723
		// (set) Token: 0x0600A1C8 RID: 41416 RVA: 0x00426350 File Offset: 0x00424750
		public UnityAction addOnClickDelete
		{
			set
			{
				this.buttonDelete.onClick.AddListener(value);
			}
		}

		// Token: 0x17002214 RID: 8724
		// (get) Token: 0x0600A1C9 RID: 41417 RVA: 0x00426363 File Offset: 0x00424763
		private Image image
		{
			get
			{
				if (this.m_ImageButton == null)
				{
					this.m_ImageButton = this.m_Button.image;
				}
				return this.m_ImageButton;
			}
		}

		// Token: 0x17002215 RID: 8725
		// (get) Token: 0x0600A1CA RID: 41418 RVA: 0x0042638D File Offset: 0x0042478D
		// (set) Token: 0x0600A1CB RID: 41419 RVA: 0x00426395 File Offset: 0x00424795
		public bool select
		{
			get
			{
				return this.m_Select;
			}
			set
			{
				if (Utility.SetStruct<bool>(ref this.m_Select, value))
				{
					this.image.color = ((!this.m_Select) ? Color.white : Color.green);
				}
			}
		}

		// Token: 0x0600A1CC RID: 41420 RVA: 0x004263CD File Offset: 0x004247CD
		public void Destroy()
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x04007FEB RID: 32747
		[SerializeField]
		private Button buttonDelete;

		// Token: 0x04007FEC RID: 32748
		private Image m_ImageButton;

		// Token: 0x04007FED RID: 32749
		private bool m_Select;
	}
}
