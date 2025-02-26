using System;
using UnityEngine;
using UnityEngine.UI;

namespace DigitalRuby.RainMaker
{
	// Token: 0x020004E0 RID: 1248
	public class DemoScript2D : MonoBehaviour
	{
		// Token: 0x06001721 RID: 5921 RVA: 0x00091B74 File Offset: 0x0008FF74
		private void Start()
		{
			BaseRainScript rainScript = this.RainScript;
			float num = 0.5f;
			this.RainSlider.value = num;
			rainScript.RainIntensity = num;
			this.RainScript.EnableWind = true;
		}

		// Token: 0x06001722 RID: 5922 RVA: 0x00091BAC File Offset: 0x0008FFAC
		private void Update()
		{
			Vector3 vector = Camera.main.ViewportToWorldPoint(Vector3.zero);
			float num = Camera.main.ViewportToWorldPoint(Vector3.one).x - vector.x;
			if (Input.GetKey(KeyCode.LeftArrow))
			{
				Camera.main.transform.Translate(Time.deltaTime * -(num * 0.1f), 0f, 0f);
			}
			else if (Input.GetKey(KeyCode.RightArrow))
			{
				Camera.main.transform.Translate(Time.deltaTime * (num * 0.1f), 0f, 0f);
			}
		}

		// Token: 0x06001723 RID: 5923 RVA: 0x00091C59 File Offset: 0x00090059
		public void RainSliderChanged(float val)
		{
			this.RainScript.RainIntensity = val;
		}

		// Token: 0x06001724 RID: 5924 RVA: 0x00091C67 File Offset: 0x00090067
		public void CollisionToggleChanged(bool val)
		{
			this.RainScript.CollisionMask = ((!val) ? 0 : -1);
		}

		// Token: 0x04001A83 RID: 6787
		public Slider RainSlider;

		// Token: 0x04001A84 RID: 6788
		public RainScript2D RainScript;
	}
}
