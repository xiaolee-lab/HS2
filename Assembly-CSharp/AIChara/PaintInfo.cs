using System;
using MessagePack;
using UnityEngine;

namespace AIChara
{
	// Token: 0x020007F0 RID: 2032
	[MessagePackObject(true)]
	public class PaintInfo
	{
		// Token: 0x0600326F RID: 12911 RVA: 0x00130716 File Offset: 0x0012EB16
		public PaintInfo()
		{
			this.MemberInit();
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x06003270 RID: 12912 RVA: 0x00130724 File Offset: 0x0012EB24
		// (set) Token: 0x06003271 RID: 12913 RVA: 0x0013072C File Offset: 0x0012EB2C
		public int id { get; set; }

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x06003272 RID: 12914 RVA: 0x00130735 File Offset: 0x0012EB35
		// (set) Token: 0x06003273 RID: 12915 RVA: 0x0013073D File Offset: 0x0012EB3D
		public Color color { get; set; }

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x06003274 RID: 12916 RVA: 0x00130746 File Offset: 0x0012EB46
		// (set) Token: 0x06003275 RID: 12917 RVA: 0x0013074E File Offset: 0x0012EB4E
		public float glossPower { get; set; }

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06003276 RID: 12918 RVA: 0x00130757 File Offset: 0x0012EB57
		// (set) Token: 0x06003277 RID: 12919 RVA: 0x0013075F File Offset: 0x0012EB5F
		public float metallicPower { get; set; }

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06003278 RID: 12920 RVA: 0x00130768 File Offset: 0x0012EB68
		// (set) Token: 0x06003279 RID: 12921 RVA: 0x00130770 File Offset: 0x0012EB70
		public int layoutId { get; set; }

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x0600327A RID: 12922 RVA: 0x00130779 File Offset: 0x0012EB79
		// (set) Token: 0x0600327B RID: 12923 RVA: 0x00130781 File Offset: 0x0012EB81
		public Vector4 layout { get; set; }

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x0600327C RID: 12924 RVA: 0x0013078A File Offset: 0x0012EB8A
		// (set) Token: 0x0600327D RID: 12925 RVA: 0x00130792 File Offset: 0x0012EB92
		public float rotation { get; set; }

		// Token: 0x0600327E RID: 12926 RVA: 0x0013079C File Offset: 0x0012EB9C
		public void MemberInit()
		{
			this.id = 0;
			this.color = Color.red;
			this.glossPower = 0.5f;
			this.metallicPower = 0.5f;
			this.layoutId = 0;
			this.layout = new Vector4(0.5f, 0.5f, 0.5f, 0.5f);
			this.rotation = 0.5f;
		}

		// Token: 0x0600327F RID: 12927 RVA: 0x00130804 File Offset: 0x0012EC04
		public void Copy(PaintInfo src)
		{
			this.id = src.id;
			this.color = src.color;
			this.glossPower = src.glossPower;
			this.metallicPower = src.metallicPower;
			this.layoutId = src.layoutId;
			this.layout = src.layout;
			this.rotation = src.rotation;
		}
	}
}
