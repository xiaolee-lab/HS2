using System;
using UnityEngine;

// Token: 0x0200042F RID: 1071
public class ME_EffectSettingColor : MonoBehaviour
{
	// Token: 0x06001388 RID: 5000 RVA: 0x00078C80 File Offset: 0x00077080
	private void OnEnable()
	{
		this.Update();
	}

	// Token: 0x06001389 RID: 5001 RVA: 0x00078C88 File Offset: 0x00077088
	private void Update()
	{
		if (this.previousColor != this.Color)
		{
			this.UpdateColor();
		}
	}

	// Token: 0x0600138A RID: 5002 RVA: 0x00078CA8 File Offset: 0x000770A8
	private void UpdateColor()
	{
		float h = ME_ColorHelper.ColorToHSV(this.Color).H;
		ME_ColorHelper.ChangeObjectColorByHUE(base.gameObject, h);
		this.previousColor = this.Color;
	}

	// Token: 0x040015E5 RID: 5605
	public Color Color = Color.red;

	// Token: 0x040015E6 RID: 5606
	private Color previousColor;
}
