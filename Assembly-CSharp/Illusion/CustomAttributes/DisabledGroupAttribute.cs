using System;
using UnityEngine;

namespace Illusion.CustomAttributes
{
	// Token: 0x02001061 RID: 4193
	public class DisabledGroupAttribute : PropertyAttribute
	{
		// Token: 0x06008D03 RID: 36099 RVA: 0x003B06C4 File Offset: 0x003AEAC4
		public DisabledGroupAttribute(string label)
		{
			this.label = label;
		}

		// Token: 0x040072AA RID: 29354
		public string label;
	}
}
