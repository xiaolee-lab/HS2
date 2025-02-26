using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000966 RID: 2406
public class ScrollCylinderNode : MonoBehaviour
{
	// Token: 0x060042C7 RID: 17095 RVA: 0x001A4C5C File Offset: 0x001A305C
	protected virtual void Update()
	{
		this.tmpColor = this.BG.color;
		float num = 0f;
		float deltaTime = Time.deltaTime;
		this.tmpColor.a = Mathf.SmoothDamp(this.tmpColor.a, this.endA, ref num, this.smoothTime, float.PositiveInfinity, deltaTime);
		this.BG.color = this.tmpColor;
		Toggle component = base.GetComponent<Toggle>();
		if (component != null)
		{
			this.ToggleCheckMark = component.graphic.GetComponent<Image>();
			if (this.ToggleCheckMark != null)
			{
				this.tmpColor.r = this.ToggleCheckMark.color.r;
				this.tmpColor.g = this.ToggleCheckMark.color.g;
				this.tmpColor.b = this.ToggleCheckMark.color.b;
				this.ToggleCheckMark.color = this.tmpColor;
			}
		}
		if (this.text != null)
		{
			this.tmpColor.r = this.text.color.r;
			this.tmpColor.g = this.text.color.g;
			this.tmpColor.b = this.text.color.b;
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

	// Token: 0x060042C8 RID: 17096 RVA: 0x001A4EBE File Offset: 0x001A32BE
	public void ChangeBGAlpha(int id)
	{
		this.endA = this.alpha[id];
	}

	// Token: 0x060042C9 RID: 17097 RVA: 0x001A4ECE File Offset: 0x001A32CE
	public void ChangeScale(int id)
	{
		this.prephaseScale = this.phaseScale;
		this.endScl = new Vector3(this.scale[id], this.scale[id], this.scale[id]);
		this.phaseScale = id;
	}

	// Token: 0x04003FA7 RID: 16295
	public Image BG;

	// Token: 0x04003FA8 RID: 16296
	public Text text;

	// Token: 0x04003FA9 RID: 16297
	public float smoothTime;

	// Token: 0x04003FAA RID: 16298
	public float smoothTimeV2;

	// Token: 0x04003FAB RID: 16299
	public float[] alpha = new float[4];

	// Token: 0x04003FAC RID: 16300
	public float[] scale = new float[4];

	// Token: 0x04003FAD RID: 16301
	protected int phaseScale;

	// Token: 0x04003FAE RID: 16302
	protected int prephaseScale;

	// Token: 0x04003FAF RID: 16303
	protected Image ToggleCheckMark;

	// Token: 0x04003FB0 RID: 16304
	protected float endA;

	// Token: 0x04003FB1 RID: 16305
	protected Vector3 endScl = Vector3.zero;

	// Token: 0x04003FB2 RID: 16306
	protected Color tmpColor;

	// Token: 0x04003FB3 RID: 16307
	protected Vector3 tmpScl;
}
