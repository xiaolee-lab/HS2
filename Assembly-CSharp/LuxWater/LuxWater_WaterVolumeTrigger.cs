using System;
using UnityEngine;

namespace LuxWater
{
	// Token: 0x020003E9 RID: 1001
	public class LuxWater_WaterVolumeTrigger : MonoBehaviour
	{
		// Token: 0x060011CD RID: 4557 RVA: 0x0006A2A8 File Offset: 0x000686A8
		private void OnEnable()
		{
			if (this.cam == null)
			{
				Camera component = base.GetComponent<Camera>();
				if (component != null)
				{
					this.cam = component;
				}
				else
				{
					this.active = false;
				}
			}
		}

		// Token: 0x040013FF RID: 5119
		[Space(6f)]
		[LuxWater_HelpBtn("h.cetbv2etlk23")]
		public Camera cam;

		// Token: 0x04001400 RID: 5120
		public bool active = true;
	}
}
