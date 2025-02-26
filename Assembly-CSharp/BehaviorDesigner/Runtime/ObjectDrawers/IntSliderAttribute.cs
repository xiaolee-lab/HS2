using System;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime.ObjectDrawers
{
	// Token: 0x020000CD RID: 205
	public class IntSliderAttribute : ObjectDrawerAttribute
	{
		// Token: 0x060004A9 RID: 1193 RVA: 0x0001D369 File Offset: 0x0001B769
		public IntSliderAttribute(int min, int max)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x040003FE RID: 1022
		public int min;

		// Token: 0x040003FF RID: 1023
		public int max;
	}
}
