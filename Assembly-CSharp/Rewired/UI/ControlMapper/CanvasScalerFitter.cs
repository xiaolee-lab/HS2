using System;
using Rewired.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x0200052B RID: 1323
	[RequireComponent(typeof(CanvasScalerExt))]
	public class CanvasScalerFitter : MonoBehaviour
	{
		// Token: 0x06001984 RID: 6532 RVA: 0x0009E142 File Offset: 0x0009C542
		private void OnEnable()
		{
			this.canvasScaler = base.GetComponent<CanvasScalerExt>();
			this.Update();
			this.canvasScaler.ForceRefresh();
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x0009E161 File Offset: 0x0009C561
		private void Update()
		{
			if (Screen.width != this.screenWidth || Screen.height != this.screenHeight)
			{
				this.screenWidth = Screen.width;
				this.screenHeight = Screen.height;
				this.UpdateSize();
			}
		}

		// Token: 0x06001986 RID: 6534 RVA: 0x0009E1A0 File Offset: 0x0009C5A0
		private void UpdateSize()
		{
			if (this.canvasScaler.uiScaleMode != CanvasScaler.ScaleMode.ScaleWithScreenSize)
			{
				return;
			}
			if (this.breakPoints == null)
			{
				return;
			}
			float num = (float)Screen.width / (float)Screen.height;
			float num2 = float.PositiveInfinity;
			int num3 = 0;
			for (int i = 0; i < this.breakPoints.Length; i++)
			{
				float num4 = Mathf.Abs(num - this.breakPoints[i].screenAspectRatio);
				if (num4 <= this.breakPoints[i].screenAspectRatio || MathTools.IsNear(this.breakPoints[i].screenAspectRatio, 0.01f))
				{
					if (num4 < num2)
					{
						num2 = num4;
						num3 = i;
					}
				}
			}
			this.canvasScaler.referenceResolution = this.breakPoints[num3].referenceResolution;
		}

		// Token: 0x04001C70 RID: 7280
		[SerializeField]
		private CanvasScalerFitter.BreakPoint[] breakPoints;

		// Token: 0x04001C71 RID: 7281
		private CanvasScalerExt canvasScaler;

		// Token: 0x04001C72 RID: 7282
		private int screenWidth;

		// Token: 0x04001C73 RID: 7283
		private int screenHeight;

		// Token: 0x04001C74 RID: 7284
		private Action ScreenSizeChanged;

		// Token: 0x0200052C RID: 1324
		[Serializable]
		private class BreakPoint
		{
			// Token: 0x04001C75 RID: 7285
			[SerializeField]
			public string name;

			// Token: 0x04001C76 RID: 7286
			[SerializeField]
			public float screenAspectRatio;

			// Token: 0x04001C77 RID: 7287
			[SerializeField]
			public Vector2 referenceResolution;
		}
	}
}
