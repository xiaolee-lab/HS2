using System;
using UnityEngine;

namespace AIProject.Animal
{
	// Token: 0x02000B48 RID: 2888
	[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Field)]
	public class RangeZeroOverAttribute : PropertyAttribute
	{
		// Token: 0x06005477 RID: 21623 RVA: 0x002539F6 File Offset: 0x00251DF6
		public RangeZeroOverAttribute(string _label)
		{
			this.label = _label;
		}

		// Token: 0x04004F44 RID: 20292
		public string label = string.Empty;
	}
}
