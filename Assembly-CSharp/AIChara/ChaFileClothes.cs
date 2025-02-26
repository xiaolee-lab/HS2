using System;
using MessagePack;
using UnityEngine;

namespace AIChara
{
	// Token: 0x020007E9 RID: 2025
	[MessagePackObject(true)]
	public class ChaFileClothes
	{
		// Token: 0x06003218 RID: 12824 RVA: 0x0012FC2A File Offset: 0x0012E02A
		public ChaFileClothes()
		{
			this.MemberInit();
		}

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06003219 RID: 12825 RVA: 0x0012FC38 File Offset: 0x0012E038
		// (set) Token: 0x0600321A RID: 12826 RVA: 0x0012FC40 File Offset: 0x0012E040
		public Version version { get; set; }

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x0600321B RID: 12827 RVA: 0x0012FC49 File Offset: 0x0012E049
		// (set) Token: 0x0600321C RID: 12828 RVA: 0x0012FC51 File Offset: 0x0012E051
		public ChaFileClothes.PartsInfo[] parts { get; set; }

		// Token: 0x0600321D RID: 12829 RVA: 0x0012FC5C File Offset: 0x0012E05C
		public void MemberInit()
		{
			this.version = ChaFileDefine.ChaFileClothesVersion;
			this.parts = new ChaFileClothes.PartsInfo[Enum.GetValues(typeof(ChaFileDefine.ClothesKind)).Length];
			for (int i = 0; i < this.parts.Length; i++)
			{
				this.parts[i] = new ChaFileClothes.PartsInfo();
			}
		}

		// Token: 0x0600321E RID: 12830 RVA: 0x0012FCB9 File Offset: 0x0012E0B9
		public void ComplementWithVersion()
		{
			this.version = ChaFileDefine.ChaFileClothesVersion;
		}

		// Token: 0x020007EA RID: 2026
		[MessagePackObject(true)]
		public class PartsInfo
		{
			// Token: 0x0600321F RID: 12831 RVA: 0x0012FCC6 File Offset: 0x0012E0C6
			public PartsInfo()
			{
				this.MemberInit();
			}

			// Token: 0x1700088B RID: 2187
			// (get) Token: 0x06003220 RID: 12832 RVA: 0x0012FCD4 File Offset: 0x0012E0D4
			// (set) Token: 0x06003221 RID: 12833 RVA: 0x0012FCDC File Offset: 0x0012E0DC
			public int id { get; set; }

			// Token: 0x1700088C RID: 2188
			// (get) Token: 0x06003222 RID: 12834 RVA: 0x0012FCE5 File Offset: 0x0012E0E5
			// (set) Token: 0x06003223 RID: 12835 RVA: 0x0012FCED File Offset: 0x0012E0ED
			public ChaFileClothes.PartsInfo.ColorInfo[] colorInfo { get; set; }

			// Token: 0x1700088D RID: 2189
			// (get) Token: 0x06003224 RID: 12836 RVA: 0x0012FCF6 File Offset: 0x0012E0F6
			// (set) Token: 0x06003225 RID: 12837 RVA: 0x0012FCFE File Offset: 0x0012E0FE
			public float breakRate { get; set; }

			// Token: 0x1700088E RID: 2190
			// (get) Token: 0x06003226 RID: 12838 RVA: 0x0012FD07 File Offset: 0x0012E107
			// (set) Token: 0x06003227 RID: 12839 RVA: 0x0012FD0F File Offset: 0x0012E10F
			public bool[] hideOpt { get; set; }

			// Token: 0x06003228 RID: 12840 RVA: 0x0012FD18 File Offset: 0x0012E118
			public void MemberInit()
			{
				this.id = 0;
				this.colorInfo = new ChaFileClothes.PartsInfo.ColorInfo[3];
				for (int i = 0; i < this.colorInfo.Length; i++)
				{
					this.colorInfo[i] = new ChaFileClothes.PartsInfo.ColorInfo();
				}
				this.breakRate = 0f;
				this.hideOpt = new bool[2];
			}

			// Token: 0x020007EB RID: 2027
			[MessagePackObject(true)]
			public class ColorInfo
			{
				// Token: 0x06003229 RID: 12841 RVA: 0x0012FD75 File Offset: 0x0012E175
				public ColorInfo()
				{
					this.MemberInit();
				}

				// Token: 0x1700088F RID: 2191
				// (get) Token: 0x0600322A RID: 12842 RVA: 0x0012FD83 File Offset: 0x0012E183
				// (set) Token: 0x0600322B RID: 12843 RVA: 0x0012FD8B File Offset: 0x0012E18B
				public Color baseColor { get; set; }

				// Token: 0x17000890 RID: 2192
				// (get) Token: 0x0600322C RID: 12844 RVA: 0x0012FD94 File Offset: 0x0012E194
				// (set) Token: 0x0600322D RID: 12845 RVA: 0x0012FD9C File Offset: 0x0012E19C
				public int pattern { get; set; }

				// Token: 0x17000891 RID: 2193
				// (get) Token: 0x0600322E RID: 12846 RVA: 0x0012FDA5 File Offset: 0x0012E1A5
				// (set) Token: 0x0600322F RID: 12847 RVA: 0x0012FDAD File Offset: 0x0012E1AD
				public Vector4 layout { get; set; }

				// Token: 0x17000892 RID: 2194
				// (get) Token: 0x06003230 RID: 12848 RVA: 0x0012FDB6 File Offset: 0x0012E1B6
				// (set) Token: 0x06003231 RID: 12849 RVA: 0x0012FDBE File Offset: 0x0012E1BE
				public float rotation { get; set; }

				// Token: 0x17000893 RID: 2195
				// (get) Token: 0x06003232 RID: 12850 RVA: 0x0012FDC7 File Offset: 0x0012E1C7
				// (set) Token: 0x06003233 RID: 12851 RVA: 0x0012FDCF File Offset: 0x0012E1CF
				public Color patternColor { get; set; }

				// Token: 0x17000894 RID: 2196
				// (get) Token: 0x06003234 RID: 12852 RVA: 0x0012FDD8 File Offset: 0x0012E1D8
				// (set) Token: 0x06003235 RID: 12853 RVA: 0x0012FDE0 File Offset: 0x0012E1E0
				public float glossPower { get; set; }

				// Token: 0x17000895 RID: 2197
				// (get) Token: 0x06003236 RID: 12854 RVA: 0x0012FDE9 File Offset: 0x0012E1E9
				// (set) Token: 0x06003237 RID: 12855 RVA: 0x0012FDF1 File Offset: 0x0012E1F1
				public float metallicPower { get; set; }

				// Token: 0x06003238 RID: 12856 RVA: 0x0012FDFC File Offset: 0x0012E1FC
				public void MemberInit()
				{
					this.baseColor = Color.white;
					this.pattern = 0;
					this.layout = new Vector4(1f, 1f, 0f, 0f);
					this.rotation = 0.5f;
					this.patternColor = Color.white;
					this.glossPower = 0.5f;
					this.metallicPower = 0f;
				}
			}
		}
	}
}
