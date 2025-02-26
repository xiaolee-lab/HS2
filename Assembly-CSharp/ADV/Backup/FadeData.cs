using System;
using Illusion.Extensions;
using UnityEngine;

namespace ADV.Backup
{
	// Token: 0x020006B4 RID: 1716
	internal class FadeData
	{
		// Token: 0x0600289F RID: 10399 RVA: 0x000F08BB File Offset: 0x000EECBB
		public FadeData(SimpleFade fade)
		{
			this.color = fade._Color;
			this.time = fade._Time;
			this.texture = fade._Texture;
		}

		// Token: 0x060028A0 RID: 10400 RVA: 0x000F08E7 File Offset: 0x000EECE7
		public void Load(SimpleFade fade)
		{
			fade._Color = fade._Color.Get(this.color, false, true, true, true);
			fade._Time = this.time;
			fade._Texture = this.texture;
		}

		// Token: 0x04002A27 RID: 10791
		private Color color;

		// Token: 0x04002A28 RID: 10792
		private float time;

		// Token: 0x04002A29 RID: 10793
		private Texture2D texture;
	}
}
