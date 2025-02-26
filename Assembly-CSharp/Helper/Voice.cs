using System;
using System.Collections.Generic;
using Manager;
using UnityEngine;

namespace Helper
{
	// Token: 0x02000A39 RID: 2617
	public static class Voice
	{
		// Token: 0x06004DF9 RID: 19961 RVA: 0x001DDAB0 File Offset: 0x001DBEB0
		public static Transform Play(Voice voice, Voice.Setting s)
		{
			int no = s.no;
			string assetBundleName = s.assetBundleName;
			string assetName = s.assetName;
			float pitch = s.pitch;
			float delayTime = s.delayTime;
			bool isAsync = s.isAsync;
			return voice.Play(no, assetBundleName, assetName, pitch, delayTime, 0f, isAsync, s.voiceTrans, s.type, s.settingNo, s.isPlayEndDelete, s.isBundleUnload, false);
		}

		// Token: 0x06004DFA RID: 19962 RVA: 0x001DDB1C File Offset: 0x001DBF1C
		public static Transform OnecePlay(Voice voice, Voice.Setting s)
		{
			int no = s.no;
			string assetBundleName = s.assetBundleName;
			string assetName = s.assetName;
			float pitch = s.pitch;
			float delayTime = s.delayTime;
			bool isAsync = s.isAsync;
			return voice.OnecePlay(no, assetBundleName, assetName, pitch, delayTime, 0f, isAsync, s.voiceTrans, s.type, s.settingNo, s.isPlayEndDelete, s.isBundleUnload, false);
		}

		// Token: 0x06004DFB RID: 19963 RVA: 0x001DDB88 File Offset: 0x001DBF88
		public static Transform OnecePlayChara(Voice voice, Voice.Setting s)
		{
			int no = s.no;
			string assetBundleName = s.assetBundleName;
			string assetName = s.assetName;
			float pitch = s.pitch;
			float delayTime = s.delayTime;
			bool isAsync = s.isAsync;
			return voice.OnecePlayChara(no, assetBundleName, assetName, pitch, delayTime, 0f, isAsync, s.voiceTrans, s.type, s.settingNo, s.isPlayEndDelete, s.isBundleUnload, false);
		}

		// Token: 0x0400472B RID: 18219
		public static Dictionary<int, string> SoundBasePath = new Dictionary<int, string>
		{
			{
				0,
				"sound/data/pcm/c00/"
			}
		};

		// Token: 0x02000A3A RID: 2618
		public class Setting
		{
			// Token: 0x0400472C RID: 18220
			public string assetBundleName;

			// Token: 0x0400472D RID: 18221
			public string assetName;

			// Token: 0x0400472E RID: 18222
			public Voice.Type type;

			// Token: 0x0400472F RID: 18223
			public int no;

			// Token: 0x04004730 RID: 18224
			public float pitch = 1f;

			// Token: 0x04004731 RID: 18225
			public Transform voiceTrans;

			// Token: 0x04004732 RID: 18226
			public float delayTime;

			// Token: 0x04004733 RID: 18227
			public bool isAsync = true;

			// Token: 0x04004734 RID: 18228
			public int settingNo = -1;

			// Token: 0x04004735 RID: 18229
			public bool isPlayEndDelete = true;

			// Token: 0x04004736 RID: 18230
			public bool isBundleUnload;

			// Token: 0x04004737 RID: 18231
			public string manifestFileName = "sounddata";
		}
	}
}
