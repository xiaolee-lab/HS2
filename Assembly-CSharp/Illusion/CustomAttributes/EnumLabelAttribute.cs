using System;
using UnityEngine;

namespace Illusion.CustomAttributes
{
	// Token: 0x02001064 RID: 4196
	[AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field)]
	public class EnumLabelAttribute : PropertyAttribute
	{
		// Token: 0x06008D06 RID: 36102 RVA: 0x003B06F1 File Offset: 0x003AEAF1
		public EnumLabelAttribute(string label)
		{
			this.label = label;
		}

		// Token: 0x040072AD RID: 29357
		public string label;
	}
}
