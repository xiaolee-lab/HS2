using System;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000C5B RID: 3163
	public class ItemAnimInfo
	{
		// Token: 0x17001473 RID: 5235
		// (get) Token: 0x060065F8 RID: 26104 RVA: 0x002B61FC File Offset: 0x002B45FC
		// (set) Token: 0x060065F9 RID: 26105 RVA: 0x002B6204 File Offset: 0x002B4604
		public Animator Animator { get; set; }

		// Token: 0x17001474 RID: 5236
		// (get) Token: 0x060065FA RID: 26106 RVA: 0x002B620D File Offset: 0x002B460D
		// (set) Token: 0x060065FB RID: 26107 RVA: 0x002B6215 File Offset: 0x002B4615
		public AnimatorControllerParameter[] Parameters { get; set; }

		// Token: 0x17001475 RID: 5237
		// (get) Token: 0x060065FC RID: 26108 RVA: 0x002B621E File Offset: 0x002B461E
		// (set) Token: 0x060065FD RID: 26109 RVA: 0x002B6226 File Offset: 0x002B4626
		public bool Sync { get; set; }
	}
}
