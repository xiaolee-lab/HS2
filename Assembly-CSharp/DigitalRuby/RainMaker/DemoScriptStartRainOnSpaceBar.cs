using System;
using UnityEngine;

namespace DigitalRuby.RainMaker
{
	// Token: 0x020004E1 RID: 1249
	public class DemoScriptStartRainOnSpaceBar : MonoBehaviour
	{
		// Token: 0x06001726 RID: 5926 RVA: 0x00091C8E File Offset: 0x0009008E
		private void Start()
		{
			if (this.RainScript == null)
			{
				return;
			}
			this.RainScript.EnableWind = false;
		}

		// Token: 0x06001727 RID: 5927 RVA: 0x00091CB0 File Offset: 0x000900B0
		private void Update()
		{
			if (this.RainScript == null)
			{
				return;
			}
			if (Input.GetKeyDown(KeyCode.Space))
			{
				this.RainScript.RainIntensity = ((this.RainScript.RainIntensity != 0f) ? 0f : 1f);
				this.RainScript.EnableWind = !this.RainScript.EnableWind;
			}
		}

		// Token: 0x04001A85 RID: 6789
		public BaseRainScript RainScript;
	}
}
