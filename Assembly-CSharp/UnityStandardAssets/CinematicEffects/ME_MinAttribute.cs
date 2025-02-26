using System;
using UnityEngine;

namespace UnityStandardAssets.CinematicEffects
{
	// Token: 0x0200042A RID: 1066
	public sealed class ME_MinAttribute : PropertyAttribute
	{
		// Token: 0x06001375 RID: 4981 RVA: 0x0007801C File Offset: 0x0007641C
		public ME_MinAttribute(float min)
		{
			this.min = min;
		}

		// Token: 0x040015D7 RID: 5591
		public readonly float min;
	}
}
