using System;
using UnityEngine;

// Token: 0x0200110C RID: 4364
[Serializable]
public class MatAnmSimpleInfo
{
	// Token: 0x060090D4 RID: 37076 RVA: 0x003C4974 File Offset: 0x003C2D74
	public void Update(int colorPropertyId)
	{
		if (null == this.mr)
		{
			return;
		}
		float num;
		if (null == this.yst)
		{
			this.cnt += Time.deltaTime;
			while (this.time < this.cnt)
			{
				this.cnt -= this.time;
			}
			num = this.cnt / this.time;
		}
		else
		{
			num = this.yst.rate;
		}
		Color color = this.mr.material.GetColor(colorPropertyId);
		if (this.change_RGB)
		{
			color.r = this.Value.Evaluate(num).r;
			color.g = this.Value.Evaluate(num).g;
			color.b = this.Value.Evaluate(num).b;
		}
		if (this.change_A)
		{
			color.a = this.Value.Evaluate(num).a;
		}
		this.mr.material.SetColor(colorPropertyId, color);
	}

	// Token: 0x04007560 RID: 30048
	public YS_Timer yst;

	// Token: 0x04007561 RID: 30049
	public MeshRenderer mr;

	// Token: 0x04007562 RID: 30050
	public int materialNo;

	// Token: 0x04007563 RID: 30051
	public float time;

	// Token: 0x04007564 RID: 30052
	public bool change_RGB = true;

	// Token: 0x04007565 RID: 30053
	public bool change_A = true;

	// Token: 0x04007566 RID: 30054
	public Gradient Value;

	// Token: 0x04007567 RID: 30055
	private float cnt;
}
