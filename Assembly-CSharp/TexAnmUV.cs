using System;
using UnityEngine;

// Token: 0x0200115B RID: 4443
public class TexAnmUV : MonoBehaviour
{
	// Token: 0x060092D4 RID: 37588 RVA: 0x003CE52F File Offset: 0x003CC92F
	private void Start()
	{
		this.rendererData = base.GetComponent<Renderer>();
		if (null == this.rendererData)
		{
			base.enabled = false;
		}
	}

	// Token: 0x060092D5 RID: 37589 RVA: 0x003CE558 File Offset: 0x003CC958
	private void Update()
	{
		float num = 0f;
		float num2 = 0f;
		if (this.ScrollU.Usage)
		{
			if (null == this.yst)
			{
				this.passTimeU += (int)(Time.deltaTime * 1000f);
				while (this.passTimeU >= this.ScrollU.ChangeTime)
				{
					this.passTimeU -= this.ScrollU.ChangeTime;
				}
				num = Mathf.InverseLerp(0f, (float)this.ScrollU.ChangeTime, (float)this.passTimeU);
			}
			else
			{
				num = this.yst.rate;
			}
			if (this.ScrollU.Plus)
			{
				num = 1f - num;
			}
			num += this.ScrollU.correct;
			if (num > 1f)
			{
				num -= 1f;
			}
		}
		if (this.ScrollV.Usage)
		{
			if (null == this.yst)
			{
				this.passTimeV += (int)(Time.deltaTime * 1000f);
				while (this.passTimeV >= this.ScrollV.ChangeTime)
				{
					this.passTimeV -= this.ScrollV.ChangeTime;
				}
				num2 = Mathf.InverseLerp(0f, (float)this.ScrollV.ChangeTime, (float)this.passTimeV);
			}
			else
			{
				num2 = this.yst.rate;
			}
			if (this.ScrollV.Plus)
			{
				num2 = 1f - num2;
			}
			num2 += this.ScrollV.correct;
			if (num2 > 1f)
			{
				num2 -= 1f;
			}
		}
		Vector2 value = new Vector2(num, 1f - num2);
		this.rendererData.material.SetTextureOffset("_MainTex", value);
	}

	// Token: 0x040076D6 RID: 30422
	public YS_Timer yst;

	// Token: 0x040076D7 RID: 30423
	public TexAnmUVParam ScrollU;

	// Token: 0x040076D8 RID: 30424
	public TexAnmUVParam ScrollV;

	// Token: 0x040076D9 RID: 30425
	private int passTimeU;

	// Token: 0x040076DA RID: 30426
	private int passTimeV;

	// Token: 0x040076DB RID: 30427
	private Renderer rendererData;
}
