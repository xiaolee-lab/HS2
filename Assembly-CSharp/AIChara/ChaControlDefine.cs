using System;
using UnityEngine;

namespace AIChara
{
	// Token: 0x020007BB RID: 1979
	public static class ChaControlDefine
	{
		// Token: 0x04002E16 RID: 11798
		public const string headBoneName = "cf_J_FaceRoot";

		// Token: 0x04002E17 RID: 11799
		public const string bodyBoneName = "cf_J_Root";

		// Token: 0x04002E18 RID: 11800
		public const string bodyTopName = "BodyTop";

		// Token: 0x04002E19 RID: 11801
		public const string AnimeMannequinState = "mannequin";

		// Token: 0x04002E1A RID: 11802
		public const string AnimeMannequinState02 = "mannequin02";

		// Token: 0x04002E1B RID: 11803
		public const string objHeadName = "ct_head";

		// Token: 0x04002E1C RID: 11804
		public const int FaceTexSize = 2048;

		// Token: 0x04002E1D RID: 11805
		public const int BodyTexSize = 4096;

		// Token: 0x04002E1E RID: 11806
		public static readonly Bounds bounds = new Bounds(new Vector3(0f, -2f, 0f), new Vector3(20f, 20f, 20f));

		// Token: 0x04002E1F RID: 11807
		public static readonly string[] extraAcsNames = new string[]
		{
			"mapAcsHead",
			"mapAcsBack",
			"mapAcsNeck",
			"mapAcsWaist"
		};

		// Token: 0x020007BC RID: 1980
		public enum ExtraAccessoryParts
		{
			// Token: 0x04002E21 RID: 11809
			Head,
			// Token: 0x04002E22 RID: 11810
			Back,
			// Token: 0x04002E23 RID: 11811
			Neck,
			// Token: 0x04002E24 RID: 11812
			Waist
		}

		// Token: 0x020007BD RID: 1981
		public enum DynamicBoneKind
		{
			// Token: 0x04002E26 RID: 11814
			BreastL,
			// Token: 0x04002E27 RID: 11815
			BreastR,
			// Token: 0x04002E28 RID: 11816
			HipL,
			// Token: 0x04002E29 RID: 11817
			HipR
		}
	}
}
