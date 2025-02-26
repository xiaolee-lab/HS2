using System;
using UnityEngine;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x020011D1 RID: 4561
	public class CharaFileInfo
	{
		// Token: 0x060095AF RID: 38319 RVA: 0x003DDFAF File Offset: 0x003DC3AF
		public CharaFileInfo(string _file = "", string _name = "")
		{
			this.file = _file;
			this.name = _name;
		}

		// Token: 0x17001FB3 RID: 8115
		// (get) Token: 0x060095B0 RID: 38320 RVA: 0x003DDFE2 File Offset: 0x003DC3E2
		// (set) Token: 0x060095B1 RID: 38321 RVA: 0x003DDFEA File Offset: 0x003DC3EA
		public ListNode node { get; set; }

		// Token: 0x17001FB4 RID: 8116
		// (get) Token: 0x060095B2 RID: 38322 RVA: 0x003DDFF3 File Offset: 0x003DC3F3
		// (set) Token: 0x060095B3 RID: 38323 RVA: 0x003DE000 File Offset: 0x003DC400
		public bool select
		{
			get
			{
				return this.node.select;
			}
			set
			{
				this.node.select = value;
				if (this.button)
				{
					this.button.image.color = ((!value) ? Color.white : Color.green);
				}
			}
		}

		// Token: 0x17001FB5 RID: 8117
		// (set) Token: 0x060095B4 RID: 38324 RVA: 0x003DE04E File Offset: 0x003DC44E
		public int siblingIndex
		{
			set
			{
				this.node.transform.SetSiblingIndex(value);
			}
		}

		// Token: 0x04007887 RID: 30855
		public string file = string.Empty;

		// Token: 0x04007888 RID: 30856
		public string name = string.Empty;

		// Token: 0x04007889 RID: 30857
		public DateTime time;

		// Token: 0x0400788A RID: 30858
		public int index = -1;

		// Token: 0x0400788C RID: 30860
		public Button button;
	}
}
