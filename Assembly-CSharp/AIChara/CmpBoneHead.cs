using System;
using UnityEngine;

namespace AIChara
{
	// Token: 0x020007CC RID: 1996
	[DisallowMultipleComponent]
	public class CmpBoneHead : CmpBase
	{
		// Token: 0x0600316D RID: 12653 RVA: 0x001256CB File Offset: 0x00123ACB
		public CmpBoneHead() : base(false)
		{
		}

		// Token: 0x0600316E RID: 12654 RVA: 0x001256EC File Offset: 0x00123AEC
		public override void SetReferenceObject()
		{
			FindAssist findAssist = new FindAssist();
			findAssist.Initialize(base.transform);
			this.targetAccessory.acs_Hair_pony = findAssist.GetTransformFromName("N_Hair_pony");
			this.targetAccessory.acs_Hair_twin_L = findAssist.GetTransformFromName("N_Hair_twin_L");
			this.targetAccessory.acs_Hair_twin_R = findAssist.GetTransformFromName("N_Hair_twin_R");
			this.targetAccessory.acs_Hair_pin_L = findAssist.GetTransformFromName("N_Hair_pin_L");
			this.targetAccessory.acs_Hair_pin_R = findAssist.GetTransformFromName("N_Hair_pin_R");
			this.targetAccessory.acs_Head_top = findAssist.GetTransformFromName("N_Head_top");
			this.targetAccessory.acs_Head = findAssist.GetTransformFromName("N_Head");
			this.targetAccessory.acs_Hitai = findAssist.GetTransformFromName("N_Hitai");
			this.targetAccessory.acs_Face = findAssist.GetTransformFromName("N_Face");
			this.targetAccessory.acs_Megane = findAssist.GetTransformFromName("N_Megane");
			this.targetAccessory.acs_Earring_L = findAssist.GetTransformFromName("N_Earring_L");
			this.targetAccessory.acs_Earring_R = findAssist.GetTransformFromName("N_Earring_R");
			this.targetAccessory.acs_Nose = findAssist.GetTransformFromName("N_Nose");
			this.targetAccessory.acs_Mouth = findAssist.GetTransformFromName("N_Mouth");
			this.targetEtc.trfHairParent = findAssist.GetTransformFromName("N_hair_Root");
			this.targetEtc.trfMouthAdjustWidth = findAssist.GetTransformFromName("cf_J_MouthMove");
		}

		// Token: 0x04002F6E RID: 12142
		[Header("アクセサリの親")]
		public CmpBoneHead.TargetAccessory targetAccessory = new CmpBoneHead.TargetAccessory();

		// Token: 0x04002F6F RID: 12143
		[Header("その他ターゲット")]
		public CmpBoneHead.TargetEtc targetEtc = new CmpBoneHead.TargetEtc();

		// Token: 0x020007CD RID: 1997
		[Serializable]
		public class TargetAccessory
		{
			// Token: 0x04002F70 RID: 12144
			public Transform acs_Hair_pony;

			// Token: 0x04002F71 RID: 12145
			public Transform acs_Hair_twin_L;

			// Token: 0x04002F72 RID: 12146
			public Transform acs_Hair_twin_R;

			// Token: 0x04002F73 RID: 12147
			public Transform acs_Hair_pin_L;

			// Token: 0x04002F74 RID: 12148
			public Transform acs_Hair_pin_R;

			// Token: 0x04002F75 RID: 12149
			public Transform acs_Head_top;

			// Token: 0x04002F76 RID: 12150
			public Transform acs_Head;

			// Token: 0x04002F77 RID: 12151
			public Transform acs_Hitai;

			// Token: 0x04002F78 RID: 12152
			public Transform acs_Face;

			// Token: 0x04002F79 RID: 12153
			public Transform acs_Megane;

			// Token: 0x04002F7A RID: 12154
			public Transform acs_Earring_L;

			// Token: 0x04002F7B RID: 12155
			public Transform acs_Earring_R;

			// Token: 0x04002F7C RID: 12156
			public Transform acs_Nose;

			// Token: 0x04002F7D RID: 12157
			public Transform acs_Mouth;
		}

		// Token: 0x020007CE RID: 1998
		[Serializable]
		public class TargetEtc
		{
			// Token: 0x04002F7E RID: 12158
			public Transform trfHairParent;

			// Token: 0x04002F7F RID: 12159
			public Transform trfMouthAdjustWidth;
		}
	}
}
