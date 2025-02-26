using System;
using UnityEngine;

namespace Illusion.CustomAttributes
{
	// Token: 0x02001067 RID: 4199
	public class NamedArrayAttribute : PropertyAttribute
	{
		// Token: 0x06008D09 RID: 36105 RVA: 0x003B0717 File Offset: 0x003AEB17
		public NamedArrayAttribute(params string[] names)
		{
			this.names = names;
		}

		// Token: 0x06008D0A RID: 36106 RVA: 0x003B0726 File Offset: 0x003AEB26
		public NamedArrayAttribute(Type enumType)
		{
			this.names = Enum.GetNames(enumType);
		}

		// Token: 0x040072AF RID: 29359
		public readonly string[] names;
	}
}
