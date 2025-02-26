using System;
using UnityEngine;

namespace Illusion.CustomAttributes
{
	// Token: 0x02001066 RID: 4198
	[AttributeUsage(AttributeTargets.Field)]
	public class LabelAttribute : PropertyAttribute
	{
		// Token: 0x06008D08 RID: 36104 RVA: 0x003B0708 File Offset: 0x003AEB08
		public LabelAttribute(string label)
		{
			this.label = label;
		}

		// Token: 0x040072AE RID: 29358
		public string label;
	}
}
