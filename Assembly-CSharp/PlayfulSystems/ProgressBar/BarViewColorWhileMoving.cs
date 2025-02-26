using System;
using UnityEngine;
using UnityEngine.UI;

namespace PlayfulSystems.ProgressBar
{
	// Token: 0x02000642 RID: 1602
	[RequireComponent(typeof(Graphic))]
	public class BarViewColorWhileMoving : ProgressBarProView
	{
		// Token: 0x06002613 RID: 9747 RVA: 0x000D8D16 File Offset: 0x000D7116
		private void OnEnable()
		{
			this.SetDefaultColor();
		}

		// Token: 0x06002614 RID: 9748 RVA: 0x000D8D20 File Offset: 0x000D7120
		public override void UpdateView(float currentValue, float targetValue)
		{
			bool flag = currentValue != targetValue;
			if (this.isMoving == flag)
			{
				return;
			}
			this.isMoving = flag;
			this.graphic.CrossFadeColor(this.GetCurrentColor(), (!this.isMoving) ? this.blendTimeOnStop : this.blendTimeOnMove, false, true);
		}

		// Token: 0x06002615 RID: 9749 RVA: 0x000D8D78 File Offset: 0x000D7178
		private Color GetCurrentColor()
		{
			return (!this.isMoving) ? this.colorStatic : this.colorMoving;
		}

		// Token: 0x06002616 RID: 9750 RVA: 0x000D8D96 File Offset: 0x000D7196
		private void SetDefaultColor()
		{
			this.graphic.canvasRenderer.SetColor(this.GetCurrentColor());
		}

		// Token: 0x040025CB RID: 9675
		[SerializeField]
		protected Graphic graphic;

		// Token: 0x040025CC RID: 9676
		[SerializeField]
		private Color colorStatic = Color.white;

		// Token: 0x040025CD RID: 9677
		[SerializeField]
		private Color colorMoving = Color.blue;

		// Token: 0x040025CE RID: 9678
		[SerializeField]
		private float blendTimeOnMove = 0.2f;

		// Token: 0x040025CF RID: 9679
		[SerializeField]
		private float blendTimeOnStop = 0.05f;

		// Token: 0x040025D0 RID: 9680
		private bool isMoving;
	}
}
