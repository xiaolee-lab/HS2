using System;
using UnityEngine;

namespace AIProject.Animal
{
	// Token: 0x02000B47 RID: 2887
	[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Field)]
	public class CreateRangeAttribute : PropertyAttribute
	{
		// Token: 0x06005476 RID: 21622 RVA: 0x002539DC File Offset: 0x00251DDC
		public CreateRangeAttribute(string _label)
		{
			this.label = _label;
		}

		// Token: 0x04004F43 RID: 20291
		public string label = string.Empty;
	}
}
