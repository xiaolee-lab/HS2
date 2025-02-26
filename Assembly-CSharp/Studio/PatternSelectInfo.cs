using System;

namespace Studio
{
	// Token: 0x02001335 RID: 4917
	public class PatternSelectInfo
	{
		// Token: 0x1700227E RID: 8830
		// (get) Token: 0x0600A49A RID: 42138 RVA: 0x00432837 File Offset: 0x00430C37
		public bool activeSelf
		{
			get
			{
				return this.sic.gameObject.activeSelf;
			}
		}

		// Token: 0x1700227F RID: 8831
		// (get) Token: 0x0600A49B RID: 42139 RVA: 0x00432849 File Offset: 0x00430C49
		public bool interactable
		{
			get
			{
				return this.sic.tgl.interactable;
			}
		}

		// Token: 0x17002280 RID: 8832
		// (get) Token: 0x0600A49C RID: 42140 RVA: 0x0043285B File Offset: 0x00430C5B
		public bool isOn
		{
			get
			{
				return this.sic.tgl.isOn;
			}
		}

		// Token: 0x0400819E RID: 33182
		public int index = -1;

		// Token: 0x0400819F RID: 33183
		public string name = string.Empty;

		// Token: 0x040081A0 RID: 33184
		public string assetBundle = string.Empty;

		// Token: 0x040081A1 RID: 33185
		public string assetName = string.Empty;

		// Token: 0x040081A2 RID: 33186
		public int category;

		// Token: 0x040081A3 RID: 33187
		public bool disable;

		// Token: 0x040081A4 RID: 33188
		public bool disvisible;

		// Token: 0x040081A5 RID: 33189
		public PatternSelectInfoComponent sic;
	}
}
