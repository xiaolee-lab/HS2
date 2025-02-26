using System;
using UnityEngine;

namespace Illusion.CustomAttributes
{
	// Token: 0x0200106D RID: 4205
	[AttributeUsage(AttributeTargets.Field)]
	public class RangeIntLabelAttribute : PropertyAttribute
	{
		// Token: 0x06008D10 RID: 36112 RVA: 0x003B0769 File Offset: 0x003AEB69
		public RangeIntLabelAttribute(string label, int min, int max)
		{
			this.label = label;
			this.min = min;
			this.max = max;
		}

		// Token: 0x040072B1 RID: 29361
		public string label;

		// Token: 0x040072B2 RID: 29362
		public int min;

		// Token: 0x040072B3 RID: 29363
		public int max;
	}
}
