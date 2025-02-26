using System;
using System.Collections.Generic;
using Illusion.Game;
using UnityEngine;

namespace Illusion.Component
{
	// Token: 0x02001049 RID: 4169
	public class ScreenShot : MonoBehaviour
	{
		// Token: 0x06008C42 RID: 35906 RVA: 0x003ADAF3 File Offset: 0x003ABEF3
		public void Capture(string path = null)
		{
			if (path.IsNullOrEmpty())
			{
				path = Illusion.Game.Utils.ScreenShot.Path;
			}
			base.StartCoroutine(Illusion.Game.Utils.ScreenShot.CaptureGSS(this.ssCamList, path, this.texCapMark, this.capRate));
		}

		// Token: 0x04007246 RID: 29254
		[Button("Capture", "キャプチャー", new object[]
		{
			""
		})]
		public int excuteCapture;

		// Token: 0x04007247 RID: 29255
		public int capRate = 1;

		// Token: 0x04007248 RID: 29256
		public Texture texCapMark;

		// Token: 0x04007249 RID: 29257
		public List<ScreenShotCamera> ssCamList = new List<ScreenShotCamera>();
	}
}
