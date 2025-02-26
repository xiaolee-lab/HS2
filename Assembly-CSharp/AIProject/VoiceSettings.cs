using System;
using Manager;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000F07 RID: 3847
	public class VoiceSettings
	{
		// Token: 0x170018BF RID: 6335
		// (get) Token: 0x06007DD9 RID: 32217 RVA: 0x0035BA0D File Offset: 0x00359E0D
		// (set) Token: 0x06007DDA RID: 32218 RVA: 0x0035BA15 File Offset: 0x00359E15
		public string AssetBundleName { get; set; }

		// Token: 0x170018C0 RID: 6336
		// (get) Token: 0x06007DDB RID: 32219 RVA: 0x0035BA1E File Offset: 0x00359E1E
		// (set) Token: 0x06007DDC RID: 32220 RVA: 0x0035BA26 File Offset: 0x00359E26
		public string AssetName { get; set; }

		// Token: 0x170018C1 RID: 6337
		// (get) Token: 0x06007DDD RID: 32221 RVA: 0x0035BA2F File Offset: 0x00359E2F
		// (set) Token: 0x06007DDE RID: 32222 RVA: 0x0035BA37 File Offset: 0x00359E37
		public Voice.Type Type { get; set; }

		// Token: 0x170018C2 RID: 6338
		// (get) Token: 0x06007DDF RID: 32223 RVA: 0x0035BA40 File Offset: 0x00359E40
		// (set) Token: 0x06007DE0 RID: 32224 RVA: 0x0035BA48 File Offset: 0x00359E48
		public int No { get; set; }

		// Token: 0x170018C3 RID: 6339
		// (get) Token: 0x06007DE1 RID: 32225 RVA: 0x0035BA51 File Offset: 0x00359E51
		// (set) Token: 0x06007DE2 RID: 32226 RVA: 0x0035BA59 File Offset: 0x00359E59
		public float Pitch { get; set; }

		// Token: 0x170018C4 RID: 6340
		// (get) Token: 0x06007DE3 RID: 32227 RVA: 0x0035BA62 File Offset: 0x00359E62
		// (set) Token: 0x06007DE4 RID: 32228 RVA: 0x0035BA6A File Offset: 0x00359E6A
		public Transform VoiceTransform { get; set; }

		// Token: 0x170018C5 RID: 6341
		// (get) Token: 0x06007DE5 RID: 32229 RVA: 0x0035BA73 File Offset: 0x00359E73
		// (set) Token: 0x06007DE6 RID: 32230 RVA: 0x0035BA7B File Offset: 0x00359E7B
		public float Delaytime { get; set; }

		// Token: 0x170018C6 RID: 6342
		// (get) Token: 0x06007DE7 RID: 32231 RVA: 0x0035BA84 File Offset: 0x00359E84
		// (set) Token: 0x06007DE8 RID: 32232 RVA: 0x0035BA8C File Offset: 0x00359E8C
		public float FadeTime { get; set; }

		// Token: 0x170018C7 RID: 6343
		// (get) Token: 0x06007DE9 RID: 32233 RVA: 0x0035BA95 File Offset: 0x00359E95
		// (set) Token: 0x06007DEA RID: 32234 RVA: 0x0035BA9D File Offset: 0x00359E9D
		public bool IsAsync { get; set; }

		// Token: 0x170018C8 RID: 6344
		// (get) Token: 0x06007DEB RID: 32235 RVA: 0x0035BAA6 File Offset: 0x00359EA6
		// (set) Token: 0x06007DEC RID: 32236 RVA: 0x0035BAAE File Offset: 0x00359EAE
		public int SettingNo { get; set; } = -1;

		// Token: 0x170018C9 RID: 6345
		// (get) Token: 0x06007DED RID: 32237 RVA: 0x0035BAB7 File Offset: 0x00359EB7
		// (set) Token: 0x06007DEE RID: 32238 RVA: 0x0035BABF File Offset: 0x00359EBF
		public bool IsPlayEndDelete { get; set; } = true;

		// Token: 0x170018CA RID: 6346
		// (get) Token: 0x06007DEF RID: 32239 RVA: 0x0035BAC8 File Offset: 0x00359EC8
		// (set) Token: 0x06007DF0 RID: 32240 RVA: 0x0035BAD0 File Offset: 0x00359ED0
		public bool IsBundleUnload { get; set; }

		// Token: 0x170018CB RID: 6347
		// (get) Token: 0x06007DF1 RID: 32241 RVA: 0x0035BAD9 File Offset: 0x00359ED9
		// (set) Token: 0x06007DF2 RID: 32242 RVA: 0x0035BAE1 File Offset: 0x00359EE1
		public bool Is2D { get; set; }
	}
}
