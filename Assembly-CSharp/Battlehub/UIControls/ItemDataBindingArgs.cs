using System;
using UnityEngine;

namespace Battlehub.UIControls
{
	// Token: 0x02000085 RID: 133
	public class ItemDataBindingArgs : EventArgs
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000168 RID: 360 RVA: 0x0000B563 File Offset: 0x00009963
		// (set) Token: 0x06000169 RID: 361 RVA: 0x0000B56B File Offset: 0x0000996B
		public object Item { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600016A RID: 362 RVA: 0x0000B574 File Offset: 0x00009974
		// (set) Token: 0x0600016B RID: 363 RVA: 0x0000B57C File Offset: 0x0000997C
		public GameObject ItemPresenter { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600016C RID: 364 RVA: 0x0000B585 File Offset: 0x00009985
		// (set) Token: 0x0600016D RID: 365 RVA: 0x0000B58D File Offset: 0x0000998D
		public GameObject EditorPresenter { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600016E RID: 366 RVA: 0x0000B596 File Offset: 0x00009996
		// (set) Token: 0x0600016F RID: 367 RVA: 0x0000B59E File Offset: 0x0000999E
		public bool CanEdit
		{
			get
			{
				return this.m_canEdit;
			}
			set
			{
				this.m_canEdit = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000170 RID: 368 RVA: 0x0000B5A7 File Offset: 0x000099A7
		// (set) Token: 0x06000171 RID: 369 RVA: 0x0000B5AF File Offset: 0x000099AF
		public bool CanDrag
		{
			get
			{
				return this.m_canDrag;
			}
			set
			{
				this.m_canDrag = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000172 RID: 370 RVA: 0x0000B5B8 File Offset: 0x000099B8
		// (set) Token: 0x06000173 RID: 371 RVA: 0x0000B5C0 File Offset: 0x000099C0
		public bool CanBeParent
		{
			get
			{
				return this.m_canDrop;
			}
			set
			{
				this.m_canDrop = value;
			}
		}

		// Token: 0x04000231 RID: 561
		private bool m_canEdit = true;

		// Token: 0x04000232 RID: 562
		private bool m_canDrag = true;

		// Token: 0x04000233 RID: 563
		private bool m_canDrop = true;
	}
}
