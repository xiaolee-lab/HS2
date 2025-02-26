using System;
using System.Collections;
using PicoGames.QuickRopes;
using PicoGames.Utilities;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000A64 RID: 2660
public class RopeRendererSceneUI : MonoBehaviour
{
	// Token: 0x06004EC7 RID: 20167 RVA: 0x001E3674 File Offset: 0x001E1A74
	private void Start()
	{
		if (this.rRender == null)
		{
			return;
		}
		this.radiusSlider.value = this.rRender.Radius;
		this.edgeCountSlider.value = (float)this.rRender.EdgeCount;
		this.edgeIndentSlider.value = this.rRender.EdgeIndent;
		this.edgeDetailSlider.value = (float)this.rRender.EdgeDetail;
		this.strandCountSlider.value = (float)this.rRender.StrandCount;
		this.strandOffsetSlider.value = this.rRender.StrandOffset;
		this.strandTwistSlider.value = this.rRender.StrandTwist;
		this.buttonText.text = "Press To " + ((!this.rope.usePhysics) ? "Play Physics" : "Stop Physics");
		this.playButton.colors = new ColorBlock
		{
			normalColor = ((!this.rope.usePhysics) ? this.stoppedColor : this.playingColor),
			highlightedColor = ((!this.rope.usePhysics) ? this.stoppedColor : this.playingColor),
			pressedColor = ((!this.rope.usePhysics) ? this.stoppedColor : this.playingColor),
			colorMultiplier = 1f,
			fadeDuration = 0.2f
		};
		this.UpdateLabels();
		this.radiusSlider.onValueChanged.AddListener(new UnityAction<float>(this.UpdateRenderer));
		this.edgeCountSlider.onValueChanged.AddListener(new UnityAction<float>(this.UpdateRenderer));
		this.edgeIndentSlider.onValueChanged.AddListener(new UnityAction<float>(this.UpdateRenderer));
		this.edgeDetailSlider.onValueChanged.AddListener(new UnityAction<float>(this.UpdateRenderer));
		this.strandCountSlider.onValueChanged.AddListener(new UnityAction<float>(this.UpdateRenderer));
		this.strandOffsetSlider.onValueChanged.AddListener(new UnityAction<float>(this.UpdateRenderer));
		this.strandTwistSlider.onValueChanged.AddListener(new UnityAction<float>(this.UpdateRenderer));
	}

	// Token: 0x06004EC8 RID: 20168 RVA: 0x001E38D4 File Offset: 0x001E1CD4
	public void TogglePhysics()
	{
		this.rope.usePhysics = !this.rope.usePhysics;
		this.buttonText.text = "Press To " + ((!this.rope.usePhysics) ? "Play Physics" : "Stop Physics");
		this.playButton.colors = new ColorBlock
		{
			normalColor = ((!this.rope.usePhysics) ? this.stoppedColor : this.playingColor),
			highlightedColor = ((!this.rope.usePhysics) ? this.stoppedColor : this.playingColor),
			pressedColor = ((!this.rope.usePhysics) ? this.stoppedColor : this.playingColor),
			colorMultiplier = 1f,
			fadeDuration = 0.2f
		};
		this.rope.defaultColliderSettings.radius = this.rRender.Radius * 0.5f;
		this.rope.Generate();
	}

	// Token: 0x06004EC9 RID: 20169 RVA: 0x001E3A00 File Offset: 0x001E1E00
	private void UpdateRenderer(float value)
	{
		this.rRender.Radius = this.radiusSlider.value;
		this.rRender.EdgeCount = (int)this.edgeCountSlider.value;
		this.rRender.EdgeIndent = this.edgeIndentSlider.value;
		this.rRender.EdgeDetail = (int)this.edgeDetailSlider.value;
		this.rRender.StrandCount = (int)this.strandCountSlider.value;
		this.rRender.StrandOffset = this.strandOffsetSlider.value;
		this.rRender.StrandTwist = this.strandTwistSlider.value;
		this.UpdateLabels();
	}

	// Token: 0x06004ECA RID: 20170 RVA: 0x001E3AB0 File Offset: 0x001E1EB0
	private void UpdateLabels()
	{
		this.radiusText.text = this.rRender.Radius.ToString("0.00");
		this.edgeCountText.text = this.rRender.EdgeCount.ToString();
		this.edgeIndentText.text = this.rRender.EdgeIndent.ToString("0.00");
		this.edgeDetailText.text = this.rRender.EdgeDetail.ToString();
		this.strandCountText.text = this.rRender.StrandCount.ToString();
		this.strandOffsetText.text = this.rRender.StrandOffset.ToString("0.00");
		this.strandTwistText.text = this.rRender.StrandTwist.ToString("0.00");
	}

	// Token: 0x06004ECB RID: 20171 RVA: 0x001E3BB8 File Offset: 0x001E1FB8
	public void VisitAssetStore()
	{
	}

	// Token: 0x06004ECC RID: 20172 RVA: 0x001E3BBC File Offset: 0x001E1FBC
	public void FadeToSingleStrand()
	{
		base.StopAllCoroutines();
		base.StartCoroutine(this.SmoothSetRope(0.3f, 8, 1, 5f, 1, 0.75f, 0f));
	}

	// Token: 0x06004ECD RID: 20173 RVA: 0x001E3BF4 File Offset: 0x001E1FF4
	public void FadeToHighPolyBraidedRope()
	{
		base.StopAllCoroutines();
		base.StartCoroutine(this.SmoothSetRope(0.2f, 12, 1, 5f, 6, 0.9f, 35f));
	}

	// Token: 0x06004ECE RID: 20174 RVA: 0x001E3C2C File Offset: 0x001E202C
	public void FadeToLowPolyBraidedRope()
	{
		base.StopAllCoroutines();
		base.StartCoroutine(this.SmoothSetRope(0.3f, 6, 1, 5f, 3, 0.5f, 50f));
	}

	// Token: 0x06004ECF RID: 20175 RVA: 0x001E3C64 File Offset: 0x001E2064
	public void FadeToStarRope()
	{
		base.StopAllCoroutines();
		base.StartCoroutine(this.SmoothSetRope(0.5f, 5, 2, 2.5f, 1, 0f, 15f));
	}

	// Token: 0x06004ED0 RID: 20176 RVA: 0x001E3C9C File Offset: 0x001E209C
	public void FadeToFlowerRope()
	{
		base.StopAllCoroutines();
		base.StartCoroutine(this.SmoothSetRope(0.5f, 7, 10, 2f, 1, 0f, 15f));
	}

	// Token: 0x06004ED1 RID: 20177 RVA: 0x001E3CD4 File Offset: 0x001E20D4
	public void FadeToSmallCable()
	{
		base.StopAllCoroutines();
		base.StartCoroutine(this.SmoothSetRope(0.15f, 8, 1, 5f, 4, 0.5f, 50f));
	}

	// Token: 0x06004ED2 RID: 20178 RVA: 0x001E3D0C File Offset: 0x001E210C
	private IEnumerator SmoothSetRope(float _radius, int _edgeCount, int _edgeDetail, float _edgeIndent, int _strandCount, float _strandOffset, float _strandTwist)
	{
		bool hasFinished = false;
		float edgeCountFloat = this.edgeCountSlider.value;
		float edgeDetailFloat = this.edgeDetailSlider.value;
		float strandCountFloat = this.strandCountSlider.value;
		while (!hasFinished)
		{
			this.radiusSlider.value = Mathf.MoveTowards(this.radiusSlider.value, _radius, Time.deltaTime * this.radiusSlider.maxValue * 0.5f);
			edgeCountFloat = Mathf.MoveTowards(edgeCountFloat, (float)_edgeCount, Time.deltaTime * this.edgeCountSlider.maxValue * 0.5f);
			edgeDetailFloat = Mathf.MoveTowards(edgeDetailFloat, (float)_edgeDetail, Time.deltaTime * this.edgeDetailSlider.maxValue * 0.5f);
			this.edgeIndentSlider.value = Mathf.MoveTowards(this.edgeIndentSlider.value, _edgeIndent, Time.deltaTime * this.edgeIndentSlider.maxValue * 0.5f);
			strandCountFloat = Mathf.MoveTowards(strandCountFloat, (float)_strandCount, Time.deltaTime * this.strandCountSlider.maxValue * 0.5f);
			this.strandOffsetSlider.value = Mathf.MoveTowards(this.strandOffsetSlider.value, _strandOffset, Time.deltaTime * this.strandOffsetSlider.maxValue * 0.5f);
			this.strandTwistSlider.value = Mathf.MoveTowards(this.strandTwistSlider.value, _strandTwist, Time.deltaTime * this.strandTwistSlider.maxValue * 0.5f);
			this.edgeCountSlider.value = edgeCountFloat;
			this.edgeDetailSlider.value = edgeDetailFloat;
			this.strandCountSlider.value = strandCountFloat;
			if (this.radiusSlider.value == _radius && this.edgeDetailSlider.value == (float)_edgeDetail && this.edgeCountSlider.value == (float)_edgeCount && this.edgeIndentSlider.value == _edgeIndent && this.strandCountSlider.value == (float)_strandCount && this.strandOffsetSlider.value == _strandOffset && this.strandTwistSlider.value == _strandTwist)
			{
				hasFinished = true;
			}
			yield return 0;
		}
		yield break;
	}

	// Token: 0x040047BB RID: 18363
	public Color stoppedColor = Color.green;

	// Token: 0x040047BC RID: 18364
	public Color playingColor = Color.red;

	// Token: 0x040047BD RID: 18365
	public Slider radiusSlider;

	// Token: 0x040047BE RID: 18366
	public Slider edgeCountSlider;

	// Token: 0x040047BF RID: 18367
	public Slider edgeIndentSlider;

	// Token: 0x040047C0 RID: 18368
	public Slider edgeDetailSlider;

	// Token: 0x040047C1 RID: 18369
	public Slider strandCountSlider;

	// Token: 0x040047C2 RID: 18370
	public Slider strandOffsetSlider;

	// Token: 0x040047C3 RID: 18371
	public Slider strandTwistSlider;

	// Token: 0x040047C4 RID: 18372
	public Button playButton;

	// Token: 0x040047C5 RID: 18373
	public Text radiusText;

	// Token: 0x040047C6 RID: 18374
	public Text edgeCountText;

	// Token: 0x040047C7 RID: 18375
	public Text edgeIndentText;

	// Token: 0x040047C8 RID: 18376
	public Text edgeDetailText;

	// Token: 0x040047C9 RID: 18377
	public Text strandCountText;

	// Token: 0x040047CA RID: 18378
	public Text strandOffsetText;

	// Token: 0x040047CB RID: 18379
	public Text strandTwistText;

	// Token: 0x040047CC RID: 18380
	public Text buttonText;

	// Token: 0x040047CD RID: 18381
	public Material[] availMaterials;

	// Token: 0x040047CE RID: 18382
	public QuickRope rope;

	// Token: 0x040047CF RID: 18383
	public RopeRenderer rRender;
}
