using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000B15 RID: 2837
public class HItemNode : ScrollCylinderNode
{
	// Token: 0x17000EF2 RID: 3826
	// (get) Token: 0x06005348 RID: 21320 RVA: 0x0024821A File Offset: 0x0024661A
	public Toggle Toggle
	{
		get
		{
			return this.toggle;
		}
	}

	// Token: 0x17000EF3 RID: 3827
	// (get) Token: 0x06005349 RID: 21321 RVA: 0x00248222 File Offset: 0x00246622
	public GameObject ScaleSet
	{
		[CompilerGenerated]
		get
		{
			return this.scaleSet.gameObject;
		}
	}

	// Token: 0x0600534A RID: 21322 RVA: 0x0024822F File Offset: 0x0024662F
	private void Start()
	{
		this.ToggleCheckMark = this.toggle.graphic.GetComponent<Image>();
	}

	// Token: 0x0600534B RID: 21323 RVA: 0x00248248 File Offset: 0x00246648
	protected override void Update()
	{
		this.tmpColor = this.BG.color;
		float num = 0f;
		float deltaTime = Time.deltaTime;
		this.tmpColor.a = Mathf.SmoothDamp(this.tmpColor.a, this.endA, ref num, this.smoothTime, float.PositiveInfinity, deltaTime);
		this.BG.color = this.tmpColor;
		if (this.ToggleCheckMark != null)
		{
			this.tmpColor.r = this.ToggleCheckMark.color.r;
			this.tmpColor.g = this.ToggleCheckMark.color.g;
			this.tmpColor.b = this.ToggleCheckMark.color.b;
			this.ToggleCheckMark.color = this.tmpColor;
		}
		this.tmpColor.r = this.IconImage.color.r;
		this.tmpColor.g = this.IconImage.color.g;
		this.tmpColor.b = this.IconImage.color.b;
		this.IconImage.color = this.tmpColor;
		if (this.text != null)
		{
			this.tmpColor.r = this.text.color.r;
			this.tmpColor.g = this.text.color.g;
			this.tmpColor.b = this.text.color.b;
			this.text.color = this.tmpColor;
		}
		if (this.NumMark != null)
		{
			this.tmpColor.r = this.text.color.r;
			this.tmpColor.g = this.text.color.g;
			this.tmpColor.b = this.text.color.b;
			this.NumMark.color = this.tmpColor;
		}
		if (this.NumTxt != null)
		{
			this.tmpColor.r = this.text.color.r;
			this.tmpColor.g = this.text.color.g;
			this.tmpColor.b = this.text.color.b;
			this.NumTxt.color = this.tmpColor;
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
		this.scaleSet.localScale = this.tmpScl;
	}

	// Token: 0x04004DD5 RID: 19925
	[SerializeField]
	private Image IconImage;

	// Token: 0x04004DD6 RID: 19926
	[SerializeField]
	private Toggle toggle;

	// Token: 0x04004DD7 RID: 19927
	[SerializeField]
	private Text NumMark;

	// Token: 0x04004DD8 RID: 19928
	public Text NumTxt;

	// Token: 0x04004DD9 RID: 19929
	[SerializeField]
	private RectTransform scaleSet;
}
