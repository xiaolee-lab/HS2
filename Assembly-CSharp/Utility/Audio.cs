using System;
using UnityEngine;

namespace Utility
{
	// Token: 0x02001168 RID: 4456
	public static class Audio
	{
		// Token: 0x06009327 RID: 37671 RVA: 0x003D0638 File Offset: 0x003CEA38
		public static void PlayEndDestroy(this AudioSource self, float delayTime)
		{
			UnityEngine.Object.Destroy(self.gameObject, delayTime + self.clip.length);
		}
	}
}
