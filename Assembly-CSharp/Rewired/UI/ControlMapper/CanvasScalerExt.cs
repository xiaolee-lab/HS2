using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x0200052A RID: 1322
	[AddComponentMenu("")]
	public class CanvasScalerExt : CanvasScaler
	{
		// Token: 0x06001982 RID: 6530 RVA: 0x0009E132 File Offset: 0x0009C532
		public void ForceRefresh()
		{
			this.Handle();
		}
	}
}
