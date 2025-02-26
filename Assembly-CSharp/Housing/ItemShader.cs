using System;
using UnityEngine;

namespace Housing
{
	// Token: 0x0200088B RID: 2187
	[DefaultExecutionOrder(-5)]
	public static class ItemShader
	{
		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x06003837 RID: 14391 RVA: 0x0014E403 File Offset: 0x0014C803
		// (set) Token: 0x06003838 RID: 14392 RVA: 0x0014E40A File Offset: 0x0014C80A
		public static int _Color { get; private set; } = Shader.PropertyToID("_Color");

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x06003839 RID: 14393 RVA: 0x0014E412 File Offset: 0x0014C812
		// (set) Token: 0x0600383A RID: 14394 RVA: 0x0014E419 File Offset: 0x0014C819
		public static int _Color2 { get; private set; } = Shader.PropertyToID("_Color2");

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x0600383B RID: 14395 RVA: 0x0014E421 File Offset: 0x0014C821
		// (set) Token: 0x0600383C RID: 14396 RVA: 0x0014E428 File Offset: 0x0014C828
		public static int _Color3 { get; private set; } = Shader.PropertyToID("_Color3");

		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x0600383D RID: 14397 RVA: 0x0014E430 File Offset: 0x0014C830
		// (set) Token: 0x0600383E RID: 14398 RVA: 0x0014E437 File Offset: 0x0014C837
		public static int _Color4 { get; private set; } = Shader.PropertyToID("_Color4");

		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x0600383F RID: 14399 RVA: 0x0014E43F File Offset: 0x0014C83F
		// (set) Token: 0x06003840 RID: 14400 RVA: 0x0014E446 File Offset: 0x0014C846
		public static int _EmissionColor { get; private set; } = Shader.PropertyToID("_EmissionColor");

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x06003841 RID: 14401 RVA: 0x0014E44E File Offset: 0x0014C84E
		// (set) Token: 0x06003842 RID: 14402 RVA: 0x0014E455 File Offset: 0x0014C855
		public static int _EmissionPower { get; private set; } = Shader.PropertyToID("_EmissionPower");
	}
}
