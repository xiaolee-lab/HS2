using System;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime.ObjectDrawers
{
	// Token: 0x020000CC RID: 204
	public class FloatSliderAttribute : ObjectDrawerAttribute
	{
		// Token: 0x060004A8 RID: 1192 RVA: 0x0001D353 File Offset: 0x0001B753
		public FloatSliderAttribute(float min, float max)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x040003FC RID: 1020
		public float min;

		// Token: 0x040003FD RID: 1021
		public float max;
	}
}
