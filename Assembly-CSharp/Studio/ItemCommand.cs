using System;
using UnityEngine;

namespace Studio
{
	// Token: 0x02001202 RID: 4610
	public static class ItemCommand
	{
		// Token: 0x02001203 RID: 4611
		public class ColorInfo
		{
			// Token: 0x0600971A RID: 38682 RVA: 0x003E818B File Offset: 0x003E658B
			public ColorInfo(int _dicKey, Color _oldValue, Color _newValue)
			{
				this.dicKey = _dicKey;
				this.oldValue = _oldValue;
				this.newValue = _newValue;
			}

			// Token: 0x17001FFA RID: 8186
			// (get) Token: 0x0600971B RID: 38683 RVA: 0x003E81A8 File Offset: 0x003E65A8
			// (set) Token: 0x0600971C RID: 38684 RVA: 0x003E81B0 File Offset: 0x003E65B0
			public int dicKey { get; protected set; }

			// Token: 0x17001FFB RID: 8187
			// (get) Token: 0x0600971D RID: 38685 RVA: 0x003E81B9 File Offset: 0x003E65B9
			// (set) Token: 0x0600971E RID: 38686 RVA: 0x003E81C1 File Offset: 0x003E65C1
			public Color oldValue { get; protected set; }

			// Token: 0x17001FFC RID: 8188
			// (get) Token: 0x0600971F RID: 38687 RVA: 0x003E81CA File Offset: 0x003E65CA
			// (set) Token: 0x06009720 RID: 38688 RVA: 0x003E81D2 File Offset: 0x003E65D2
			public Color newValue { get; protected set; }
		}
	}
}
