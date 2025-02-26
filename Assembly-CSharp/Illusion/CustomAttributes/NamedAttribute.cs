using System;
using UnityEngine;

namespace Illusion.CustomAttributes
{
	// Token: 0x02001068 RID: 4200
	public class NamedAttribute : PropertyAttribute
	{
		// Token: 0x06008D0B RID: 36107 RVA: 0x003B073A File Offset: 0x003AEB3A
		public NamedAttribute(string name)
		{
			this.name = name;
		}

		// Token: 0x040072B0 RID: 29360
		public readonly string name;
	}
}
