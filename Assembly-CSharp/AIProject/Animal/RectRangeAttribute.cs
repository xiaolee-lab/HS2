using System;
using UnityEngine;

namespace AIProject.Animal
{
	// Token: 0x02000B46 RID: 2886
	[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Field)]
	public class RectRangeAttribute : PropertyAttribute
	{
		// Token: 0x06005475 RID: 21621 RVA: 0x002539C2 File Offset: 0x00251DC2
		public RectRangeAttribute(string _label)
		{
			this.label = _label;
		}

		// Token: 0x04004F42 RID: 20290
		public string label = string.Empty;
	}
}
