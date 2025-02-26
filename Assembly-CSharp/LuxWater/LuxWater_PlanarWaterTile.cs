using System;
using UnityEngine;

namespace LuxWater
{
	// Token: 0x020003DD RID: 989
	[ExecuteInEditMode]
	public class LuxWater_PlanarWaterTile : MonoBehaviour
	{
		// Token: 0x06001189 RID: 4489 RVA: 0x0006778F File Offset: 0x00065B8F
		public void OnEnable()
		{
			this.AcquireComponents();
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x00067798 File Offset: 0x00065B98
		private void AcquireComponents()
		{
			if (!this.reflection)
			{
				if (base.transform.parent)
				{
					this.reflection = base.transform.parent.GetComponent<LuxWater_PlanarReflection>();
				}
				else
				{
					this.reflection = base.transform.GetComponent<LuxWater_PlanarReflection>();
				}
			}
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x000677F6 File Offset: 0x00065BF6
		public void OnWillRenderObject()
		{
			if (this.reflection)
			{
				this.reflection.WaterTileBeingRendered(base.transform, Camera.current);
			}
		}

		// Token: 0x0400136C RID: 4972
		[Space(6f)]
		[LuxWater_HelpBtn("h.nu6w5ylbucb7")]
		public LuxWater_PlanarReflection reflection;
	}
}
