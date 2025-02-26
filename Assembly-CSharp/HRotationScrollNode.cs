using System;
using UnityEngine;

// Token: 0x02000B16 RID: 2838
public class HRotationScrollNode : ScrollCylinderNode
{
	// Token: 0x0600534D RID: 21325 RVA: 0x002485EC File Offset: 0x002469EC
	protected override void Update()
	{
		this.tmpColor = this.BG.color;
		float num = 0f;
		float deltaTime = Time.deltaTime;
		this.tmpColor.a = Mathf.SmoothDamp(this.tmpColor.a, this.endA, ref num, this.smoothTime, float.PositiveInfinity, deltaTime);
		this.BG.color = this.tmpColor;
		if (this.text != null)
		{
			this.text.color = this.tmpColor;
		}
		this.tmpScl = this.BG.transform.localScale;
		Vector3 zero = Vector3.zero;
		if ((this.prephaseScale == 0 && this.phaseScale == 1) || (this.prephaseScale == 1 && this.phaseScale == 0))
		{
			this.tmpScl = Vector3.SmoothDamp(this.tmpScl, this.endScl, ref zero, this.smoothTime, float.PositiveInfinity, deltaTime);
		}
		else
		{
			this.tmpScl = Vector3.SmoothDamp(this.tmpScl, this.endScl, ref zero, this.smoothTimeV2, float.PositiveInfinity, deltaTime);
		}
		this.BG.transform.localScale = this.tmpScl;
		if (this.text != null)
		{
			this.text.transform.localScale = this.tmpScl;
		}
	}

	// Token: 0x04004DDA RID: 19930
	public int id = -1;
}
