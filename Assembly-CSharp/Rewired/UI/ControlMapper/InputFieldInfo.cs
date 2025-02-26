using System;
using UnityEngine;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x0200054E RID: 1358
	[AddComponentMenu("")]
	public class InputFieldInfo : UIElementInfo
	{
		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06001C57 RID: 7255 RVA: 0x000A9DCD File Offset: 0x000A81CD
		// (set) Token: 0x06001C58 RID: 7256 RVA: 0x000A9DD5 File Offset: 0x000A81D5
		public int actionId { get; set; }

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06001C59 RID: 7257 RVA: 0x000A9DDE File Offset: 0x000A81DE
		// (set) Token: 0x06001C5A RID: 7258 RVA: 0x000A9DE6 File Offset: 0x000A81E6
		public AxisRange axisRange { get; set; }

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06001C5B RID: 7259 RVA: 0x000A9DEF File Offset: 0x000A81EF
		// (set) Token: 0x06001C5C RID: 7260 RVA: 0x000A9DF7 File Offset: 0x000A81F7
		public int actionElementMapId { get; set; }

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06001C5D RID: 7261 RVA: 0x000A9E00 File Offset: 0x000A8200
		// (set) Token: 0x06001C5E RID: 7262 RVA: 0x000A9E08 File Offset: 0x000A8208
		public ControllerType controllerType { get; set; }

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06001C5F RID: 7263 RVA: 0x000A9E11 File Offset: 0x000A8211
		// (set) Token: 0x06001C60 RID: 7264 RVA: 0x000A9E19 File Offset: 0x000A8219
		public int controllerId { get; set; }
	}
}
