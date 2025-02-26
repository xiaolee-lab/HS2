using System;
using System.Collections.Generic;
using Manager;

namespace Helper
{
	// Token: 0x02000A36 RID: 2614
	public static class Sound
	{
		// Token: 0x06004DF3 RID: 19955 RVA: 0x001DD92B File Offset: 0x001DBD2B
		public static bool isPlay(Sound sound, Sound.SystemSE se)
		{
			return sound.IsPlay(Sound.Type.SystemSE, Sound.SystemSECast[se]);
		}

		// Token: 0x04004717 RID: 18199
		public static readonly Dictionary<Sound.SystemSE, string> SystemSECast = new Dictionary<Sound.SystemSE, string>
		{
			{
				Sound.SystemSE.sel,
				"sse_00_01"
			},
			{
				Sound.SystemSE.ok_s,
				"sse_00_02"
			},
			{
				Sound.SystemSE.ok_l,
				"sse_00_03"
			},
			{
				Sound.SystemSE.cancel,
				"sse_00_04"
			},
			{
				Sound.SystemSE.photo,
				"sse_00_05"
			},
			{
				Sound.SystemSE.title,
				"se_06_title"
			},
			{
				Sound.SystemSE.ok_s2,
				"se_07_button_A"
			},
			{
				Sound.SystemSE.window_o,
				"se_08_window_B"
			},
			{
				Sound.SystemSE.save,
				"se_09_save_A"
			}
		};

		// Token: 0x04004718 RID: 18200
		public static readonly Dictionary<Sound.Type, string> SoundBasePath = new Dictionary<Sound.Type, string>
		{
			{
				Sound.Type.BGM,
				"sound/data/bgm.unity3d"
			},
			{
				Sound.Type.ENV,
				"sound/data/env.unity3d"
			},
			{
				Sound.Type.GameSE2D,
				"sound/data/se.unity3d"
			},
			{
				Sound.Type.GameSE3D,
				"sound/data/se.unity3d"
			},
			{
				Sound.Type.SystemSE,
				"sound/data/systemse.unity3d"
			}
		};

		// Token: 0x02000A37 RID: 2615
		public enum SystemSE
		{
			// Token: 0x0400471A RID: 18202
			sel,
			// Token: 0x0400471B RID: 18203
			ok_s,
			// Token: 0x0400471C RID: 18204
			ok_l,
			// Token: 0x0400471D RID: 18205
			cancel,
			// Token: 0x0400471E RID: 18206
			photo,
			// Token: 0x0400471F RID: 18207
			title,
			// Token: 0x04004720 RID: 18208
			ok_s2,
			// Token: 0x04004721 RID: 18209
			window_o,
			// Token: 0x04004722 RID: 18210
			save
		}

		// Token: 0x02000A38 RID: 2616
		public class Setting
		{
			// Token: 0x06004DF5 RID: 19957 RVA: 0x001DDA0D File Offset: 0x001DBE0D
			public Setting()
			{
			}

			// Token: 0x06004DF6 RID: 19958 RVA: 0x001DDA2E File Offset: 0x001DBE2E
			public Setting(Sound.SystemSE se)
			{
				this.Cast(Sound.Type.SystemSE);
				this.assetName = Sound.SystemSECast[se];
			}

			// Token: 0x06004DF7 RID: 19959 RVA: 0x001DDA67 File Offset: 0x001DBE67
			public Setting(Sound.Type type)
			{
				this.Cast(type);
			}

			// Token: 0x06004DF8 RID: 19960 RVA: 0x001DDA8F File Offset: 0x001DBE8F
			public void Cast(Sound.Type type)
			{
				this.type = type;
				this.assetBundleName = Sound.SoundBasePath[this.type];
			}

			// Token: 0x04004723 RID: 18211
			public Sound.Type type;

			// Token: 0x04004724 RID: 18212
			public string assetBundleName;

			// Token: 0x04004725 RID: 18213
			public string assetName;

			// Token: 0x04004726 RID: 18214
			public float fadeOrdelayTime;

			// Token: 0x04004727 RID: 18215
			public bool isAsync = true;

			// Token: 0x04004728 RID: 18216
			public int settingNo = -1;

			// Token: 0x04004729 RID: 18217
			public bool isBundleUnload;

			// Token: 0x0400472A RID: 18218
			public string manifestFileName = "sounddata";
		}
	}
}
