using System;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000C5A RID: 3162
	public class ProceduralTargetParameter
	{
		// Token: 0x17001470 RID: 5232
		// (get) Token: 0x060065F1 RID: 26097 RVA: 0x002B61C1 File Offset: 0x002B45C1
		// (set) Token: 0x060065F2 RID: 26098 RVA: 0x002B61C9 File Offset: 0x002B45C9
		public float Start { get; set; }

		// Token: 0x17001471 RID: 5233
		// (get) Token: 0x060065F3 RID: 26099 RVA: 0x002B61D2 File Offset: 0x002B45D2
		// (set) Token: 0x060065F4 RID: 26100 RVA: 0x002B61DA File Offset: 0x002B45DA
		public float End { get; set; }

		// Token: 0x17001472 RID: 5234
		// (get) Token: 0x060065F5 RID: 26101 RVA: 0x002B61E3 File Offset: 0x002B45E3
		// (set) Token: 0x060065F6 RID: 26102 RVA: 0x002B61EB File Offset: 0x002B45EB
		public Transform Target { get; set; }
	}
}
