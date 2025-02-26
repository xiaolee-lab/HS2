using System;
using UnityEngine;
using UnityEngine.UI;

namespace Battlehub.UIControls
{
	// Token: 0x02000075 RID: 117
	public class ProgressBar : MonoBehaviour
	{
		// Token: 0x060000E5 RID: 229 RVA: 0x00009868 File Offset: 0x00007C68
		private void Start()
		{
			this.m_alpha = new float[this.Images.Length];
			for (int i = 0; i < this.Images.Length; i++)
			{
				this.m_alpha[i] = (float)i / (float)this.Images.Length;
				Color color = this.Images[i].color;
				color.a = this.m_alpha[i];
				this.Images[i].color = color;
				Color effectColor = this.Outlines[i].effectColor;
				effectColor.a = this.m_alpha[i];
				this.Outlines[i].effectColor = effectColor;
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000990C File Offset: 0x00007D0C
		private void FixedUpdate()
		{
			if (!this.IsInProgress)
			{
				return;
			}
			for (int i = 0; i < this.Images.Length; i++)
			{
				this.Images[i].color = this.UpdateAlpha(this.Images[i].color, i);
				this.Outlines[i].effectColor = this.UpdateAlpha(this.Outlines[i].effectColor, i);
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00009980 File Offset: 0x00007D80
		private Color UpdateAlpha(Color color, int index)
		{
			this.m_alpha[index] -= Time.deltaTime * this.Speed;
			if (this.m_alpha[index] < 0f)
			{
				this.m_alpha[index] = 1f;
			}
			color.a = Mathf.Clamp01(this.m_alpha[index]);
			return color;
		}

		// Token: 0x040001F2 RID: 498
		[SerializeField]
		private Image[] Images;

		// Token: 0x040001F3 RID: 499
		[SerializeField]
		private Outline[] Outlines;

		// Token: 0x040001F4 RID: 500
		private float[] m_alpha;

		// Token: 0x040001F5 RID: 501
		[SerializeField]
		private float Speed = 1f;

		// Token: 0x040001F6 RID: 502
		public bool IsInProgress = true;
	}
}
