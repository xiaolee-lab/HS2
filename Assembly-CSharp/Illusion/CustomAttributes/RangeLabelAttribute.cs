using System;
using UnityEngine;

namespace Illusion.CustomAttributes
{
	// Token: 0x0200106E RID: 4206
	[AttributeUsage(AttributeTargets.Field)]
	public class RangeLabelAttribute : PropertyAttribute
	{
		// Token: 0x06008D11 RID: 36113 RVA: 0x003B0786 File Offset: 0x003AEB86
		public RangeLabelAttribute(string label, float min, float max)
		{
			this.label = label;
			this.min = min;
			this.max = max;
		}

		// Token: 0x040072B4 RID: 29364
		public string label;

		// Token: 0x040072B5 RID: 29365
		public float min;

		// Token: 0x040072B6 RID: 29366
		public float max;
	}
}
