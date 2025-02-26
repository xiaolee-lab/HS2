using System;

namespace ConfigScene
{
	// Token: 0x02000853 RID: 2131
	public class ActionSystem : BaseSystem
	{
		// Token: 0x06003654 RID: 13908 RVA: 0x00140AD6 File Offset: 0x0013EED6
		public ActionSystem(string elementName) : base(elementName)
		{
		}

		// Token: 0x06003655 RID: 13909 RVA: 0x00140AE6 File Offset: 0x0013EEE6
		public override void Init()
		{
			this.TPSSensitivityX = 0;
			this.TPSSensitivityY = 0;
			this.FPSSensitivityX = 0;
			this.FPSSensitivityY = 0;
			this.InvertMoveX = false;
			this.InvertMoveY = false;
			this.Look = true;
		}

		// Token: 0x040036A4 RID: 13988
		public const int TPS_SENSITIVITY_X = 0;

		// Token: 0x040036A5 RID: 13989
		public const int TPS_SENSITIVITY_Y = 0;

		// Token: 0x040036A6 RID: 13990
		public const int FPS_SENSITIVITY_X = 0;

		// Token: 0x040036A7 RID: 13991
		public const int FPS_SENSITIVITY_Y = 0;

		// Token: 0x040036A8 RID: 13992
		public int TPSSensitivityX;

		// Token: 0x040036A9 RID: 13993
		public int TPSSensitivityY;

		// Token: 0x040036AA RID: 13994
		public int FPSSensitivityX;

		// Token: 0x040036AB RID: 13995
		public int FPSSensitivityY;

		// Token: 0x040036AC RID: 13996
		public bool InvertMoveX;

		// Token: 0x040036AD RID: 13997
		public bool InvertMoveY;

		// Token: 0x040036AE RID: 13998
		public bool Look = true;
	}
}
