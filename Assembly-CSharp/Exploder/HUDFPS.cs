using System;
using UnityEngine;
using UnityEngine.UI;

namespace Exploder
{
	// Token: 0x020003CB RID: 971
	public class HUDFPS : MonoBehaviour
	{
		// Token: 0x06001141 RID: 4417 RVA: 0x00065360 File Offset: 0x00063760
		private void Start()
		{
			this.text = base.GetComponent<Text>();
			if (!this.text)
			{
				base.enabled = false;
				return;
			}
			this.timeleft = this.updateInterval;
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x00065394 File Offset: 0x00063794
		private void Update()
		{
			this.timeleft -= Time.deltaTime;
			this.accum += Time.timeScale / Time.deltaTime;
			this.frames++;
			if ((double)this.timeleft <= 0.0)
			{
				float num = this.accum / (float)this.frames;
				string text = string.Format("{0:F2} FPS", num);
				this.text.text = text;
				if (num < 30f)
				{
					this.text.material.color = Color.yellow;
				}
				else if (num < 10f)
				{
					this.text.material.color = Color.red;
				}
				else
				{
					this.text.material.color = Color.black;
				}
				this.timeleft = this.updateInterval;
				this.accum = 0f;
				this.frames = 0;
			}
		}

		// Token: 0x0400131E RID: 4894
		public float updateInterval = 0.5f;

		// Token: 0x0400131F RID: 4895
		private float accum;

		// Token: 0x04001320 RID: 4896
		private int frames;

		// Token: 0x04001321 RID: 4897
		private float timeleft;

		// Token: 0x04001322 RID: 4898
		private Text text;
	}
}
