using System;
using UnityEngine;

// Token: 0x020006A1 RID: 1697
public class LineRendererFadeInOut : MonoBehaviour
{
	// Token: 0x0600283B RID: 10299 RVA: 0x000EEAB0 File Offset: 0x000ECEB0
	private void Start()
	{
		if (this.EffectSettings != null)
		{
			this.EffectSettings.CollisionEnter += this.EffectSettings_CollisionEnter;
		}
		this.lineRenderer = base.GetComponent<LineRenderer>();
		this.fadeInOutStatus = FadeInOutStatus.In;
		this.lineRenderer.SetPosition(1, new Vector3(0f, 0f, 0f));
		this.lineRenderer.SetWidth(0f, 0f);
		this.lineRenderer.enabled = true;
		this.isInit = true;
	}

	// Token: 0x0600283C RID: 10300 RVA: 0x000EEB40 File Offset: 0x000ECF40
	private void EffectSettings_CollisionEnter(object sender, CollisionInfo e)
	{
		this.fadeInOutStatus = FadeInOutStatus.Out;
		this.canUpdate = true;
	}

	// Token: 0x0600283D RID: 10301 RVA: 0x000EEB50 File Offset: 0x000ECF50
	private void OnEnable()
	{
		if (this.isInit)
		{
			this.fadeInOutStatus = FadeInOutStatus.In;
			this.canUpdate = true;
			this.lineRenderer.enabled = true;
		}
	}

	// Token: 0x0600283E RID: 10302 RVA: 0x000EEB78 File Offset: 0x000ECF78
	private void Update()
	{
		FadeInOutStatus fadeInOutStatus = this.fadeInOutStatus;
		if (fadeInOutStatus != FadeInOutStatus.In)
		{
			if (fadeInOutStatus == FadeInOutStatus.Out)
			{
				if (!this.canUpdate)
				{
					return;
				}
				this.currentStartWidth -= Time.deltaTime * (this.StartWidth / this.FadeOutSpeed);
				this.currentEndWidth -= Time.deltaTime * (this.EndWidth / this.FadeOutSpeed);
				this.currentLength -= Time.deltaTime * (this.Length / this.FadeOutSpeed);
				if (this.currentStartWidth <= 0f)
				{
					this.canUpdate = false;
					this.currentStartWidth = 0f;
					this.currentEndWidth = 0f;
					this.currentLength = 0f;
				}
				this.lineRenderer.SetPosition(1, new Vector3(0f, 0f, this.currentLength));
				this.lineRenderer.SetWidth(this.currentStartWidth, this.currentEndWidth);
				if (!this.canUpdate)
				{
					this.lineRenderer.enabled = false;
				}
			}
		}
		else
		{
			if (!this.canUpdate)
			{
				return;
			}
			this.currentStartWidth += Time.deltaTime * (this.StartWidth / this.FadeInSpeed);
			this.currentEndWidth += Time.deltaTime * (this.EndWidth / this.FadeInSpeed);
			this.currentLength += Time.deltaTime * (this.Length / this.FadeInSpeed);
			if (this.currentStartWidth >= this.StartWidth)
			{
				this.canUpdate = false;
				this.currentStartWidth = this.StartWidth;
				this.currentEndWidth = this.EndWidth;
				this.currentLength = this.Length;
			}
			this.lineRenderer.SetPosition(1, new Vector3(0f, 0f, this.currentLength));
			this.lineRenderer.SetWidth(this.currentStartWidth, this.currentEndWidth);
		}
	}

	// Token: 0x04002938 RID: 10552
	public EffectSettings EffectSettings;

	// Token: 0x04002939 RID: 10553
	public float FadeInSpeed;

	// Token: 0x0400293A RID: 10554
	public float FadeOutSpeed;

	// Token: 0x0400293B RID: 10555
	public float Length = 2f;

	// Token: 0x0400293C RID: 10556
	public float StartWidth = 1f;

	// Token: 0x0400293D RID: 10557
	public float EndWidth = 1f;

	// Token: 0x0400293E RID: 10558
	private FadeInOutStatus fadeInOutStatus;

	// Token: 0x0400293F RID: 10559
	private LineRenderer lineRenderer;

	// Token: 0x04002940 RID: 10560
	private float currentStartWidth;

	// Token: 0x04002941 RID: 10561
	private float currentEndWidth;

	// Token: 0x04002942 RID: 10562
	private float currentLength;

	// Token: 0x04002943 RID: 10563
	private bool isInit;

	// Token: 0x04002944 RID: 10564
	private bool canUpdate = true;
}
