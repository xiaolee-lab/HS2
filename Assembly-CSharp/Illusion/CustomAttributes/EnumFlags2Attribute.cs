using System;
using UnityEngine;

namespace Illusion.CustomAttributes
{
	// Token: 0x02001062 RID: 4194
	[AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field)]
	public sealed class EnumFlags2Attribute : PropertyAttribute
	{
		// Token: 0x06008D04 RID: 36100 RVA: 0x003B06D3 File Offset: 0x003AEAD3
		public EnumFlags2Attribute(string label, int _oneline = -1)
		{
			this.label = label;
			this.line = _oneline;
		}

		// Token: 0x040072AB RID: 29355
		public string label;

		// Token: 0x040072AC RID: 29356
		public int line;
	}
}
