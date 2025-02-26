using System;
using System.Collections;
using UnityEngine;

namespace EnviroSamples
{
	// Token: 0x02000322 RID: 802
	public class SampleHUDFPS : MonoBehaviour
	{
		// Token: 0x06000E1E RID: 3614 RVA: 0x000446D4 File Offset: 0x00042AD4
		private void Start()
		{
			base.StartCoroutine(this.FPS());
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x000446E3 File Offset: 0x00042AE3
		private void Update()
		{
			this.accum += Time.timeScale / Time.deltaTime;
			this.frames++;
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x0004470C File Offset: 0x00042B0C
		private IEnumerator FPS()
		{
			for (;;)
			{
				float fps = this.accum / (float)this.frames;
				this.sFPS = fps.ToString("f" + Mathf.Clamp(this.nbDecimal, 0, 10));
				this.color = ((fps < 30f) ? ((fps <= 10f) ? Color.yellow : Color.red) : Color.green);
				this.accum = 0f;
				this.frames = 0;
				yield return new WaitForSeconds(this.frequency);
			}
			yield break;
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x00044728 File Offset: 0x00042B28
		private void OnGUI()
		{
			if (this.style == null)
			{
				this.style = new GUIStyle(GUI.skin.label);
				this.style.normal.textColor = Color.white;
				this.style.alignment = TextAnchor.MiddleCenter;
			}
			GUI.color = ((!this.updateColor) ? Color.white : this.color);
			this.startRect = GUI.Window(0, this.startRect, new GUI.WindowFunction(this.DoMyWindow), string.Empty);
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x000447BC File Offset: 0x00042BBC
		private void DoMyWindow(int windowID)
		{
			GUI.Label(new Rect(0f, 0f, this.startRect.width, this.startRect.height), this.sFPS + " FPS", this.style);
			if (this.allowDrag)
			{
				GUI.DragWindow(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height));
			}
		}

		// Token: 0x04000E15 RID: 3605
		public Rect startRect = new Rect(10f, 10f, 75f, 50f);

		// Token: 0x04000E16 RID: 3606
		public bool updateColor = true;

		// Token: 0x04000E17 RID: 3607
		public bool allowDrag = true;

		// Token: 0x04000E18 RID: 3608
		public float frequency = 0.5f;

		// Token: 0x04000E19 RID: 3609
		public int nbDecimal = 1;

		// Token: 0x04000E1A RID: 3610
		private float accum;

		// Token: 0x04000E1B RID: 3611
		private int frames;

		// Token: 0x04000E1C RID: 3612
		private Color color = Color.white;

		// Token: 0x04000E1D RID: 3613
		private string sFPS = string.Empty;

		// Token: 0x04000E1E RID: 3614
		private GUIStyle style;
	}
}
