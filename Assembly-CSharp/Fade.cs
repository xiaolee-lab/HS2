using System;
using IllusionUtility.GetUtility;
using UnityEngine;

// Token: 0x02001179 RID: 4473
public class Fade : MonoBehaviour
{
	// Token: 0x17001F7C RID: 8060
	// (get) Token: 0x060093BC RID: 37820 RVA: 0x003D14B8 File Offset: 0x003CF8B8
	// (set) Token: 0x060093BD RID: 37821 RVA: 0x003D14C0 File Offset: 0x003CF8C0
	public Fade.Type nowType { get; private set; }

	// Token: 0x17001F7D RID: 8061
	// (get) Token: 0x060093BE RID: 37822 RVA: 0x003D14C9 File Offset: 0x003CF8C9
	// (set) Token: 0x060093BF RID: 37823 RVA: 0x003D14D1 File Offset: 0x003CF8D1
	public bool isFadeNow { get; private set; }

	// Token: 0x060093C0 RID: 37824 RVA: 0x003D14DC File Offset: 0x003CF8DC
	public bool FadeSet(Fade.Type type, bool _usingLoadingTex = true)
	{
		if (!this.fadeRenderer)
		{
			this.isFadeNow = false;
			return false;
		}
		if (!this.loadingRenderer)
		{
			this.isFadeNow = false;
			return false;
		}
		this.isFadeNow = true;
		this.usingLoadingTex = _usingLoadingTex;
		this.nowType = type;
		this.nowWaitTimer = 0f;
		this.fadeTimer = 0f;
		if (type == Fade.Type.InOut || type == Fade.Type.In)
		{
			this.fadeRenderer.SetAlpha(this.fadeIn.start);
			this.loadingRenderer.SetAlpha((!this.usingLoadingTex) ? 0f : this.fadeIn.start);
		}
		else if (type == Fade.Type.Out)
		{
			this.fadeRenderer.SetAlpha(this.fadeOut.start);
			this.loadingRenderer.SetAlpha((!this.usingLoadingTex) ? 0f : this.fadeOut.start);
		}
		return true;
	}

	// Token: 0x060093C1 RID: 37825 RVA: 0x003D15E4 File Offset: 0x003CF9E4
	public void FadeEnd()
	{
		this.nowType = Fade.Type.Out;
		this.isFadeNow = false;
		if (this.fadeRenderer)
		{
			this.fadeRenderer.SetAlpha(0f);
		}
		if (this.loadingRenderer)
		{
			this.loadingRenderer.SetAlpha(0f);
		}
	}

	// Token: 0x060093C2 RID: 37826 RVA: 0x003D163F File Offset: 0x003CFA3F
	public bool IsFadeIn()
	{
		return this.nowType == Fade.Type.In || this.nowType == Fade.Type.InOut;
	}

	// Token: 0x060093C3 RID: 37827 RVA: 0x003D1659 File Offset: 0x003CFA59
	public bool IsAlphaMax()
	{
		return this.fadeRenderer.GetAlpha() >= 1f;
	}

	// Token: 0x060093C4 RID: 37828 RVA: 0x003D1670 File Offset: 0x003CFA70
	public bool IsAlphaMin()
	{
		return this.fadeRenderer.GetAlpha() <= 0f;
	}

	// Token: 0x060093C5 RID: 37829 RVA: 0x003D1687 File Offset: 0x003CFA87
	public void SetColor(Color _color)
	{
		this.fadeRenderer.SetColor(_color);
	}

	// Token: 0x060093C6 RID: 37830 RVA: 0x003D1698 File Offset: 0x003CFA98
	private void Awake()
	{
		if (this.fadeRenderer == null)
		{
			GameObject gameObject = base.transform.FindLoop("Fade");
			if (gameObject)
			{
				this.fadeRenderer = gameObject.GetComponent<CanvasRenderer>();
			}
		}
		if (this.loadingRenderer == null)
		{
			GameObject gameObject2 = base.transform.FindLoop("NowLoading");
			if (gameObject2)
			{
				this.loadingRenderer = gameObject2.GetComponent<CanvasRenderer>();
			}
		}
		this.fadeRenderer.SetAlpha(0f);
		this.loadingRenderer.SetAlpha(0f);
		this.isFadeNow = false;
		this.nowType = Fade.Type.Out;
		this.nowWaitTimer = 0f;
	}

	// Token: 0x060093C7 RID: 37831 RVA: 0x003D1750 File Offset: 0x003CFB50
	private void Start()
	{
	}

	// Token: 0x060093C8 RID: 37832 RVA: 0x003D1754 File Offset: 0x003CFB54
	private void Update()
	{
		if (!this.isFadeNow)
		{
			return;
		}
		float num = Mathf.Clamp01(Time.unscaledDeltaTime);
		if (this.nowType == Fade.Type.In || this.nowType == Fade.Type.InOut)
		{
			this.fadeTimer += num;
			float num2 = Mathf.Clamp01(Mathf.Lerp(this.fadeIn.start, this.fadeIn.end, Mathf.InverseLerp(0f, this.fadeIn.time, this.fadeTimer)));
			this.fadeRenderer.SetAlpha(num2);
			this.loadingRenderer.SetAlpha((!this.usingLoadingTex) ? 0f : num2);
			if (num2 == this.fadeIn.end && this.nowType == Fade.Type.InOut)
			{
				this.nowWaitTimer = Mathf.Min(this.nowWaitTimer + num, this.fadeWaitTime);
				if (this.nowWaitTimer >= this.fadeWaitTime)
				{
					this.nowType = Fade.Type.Out;
					this.fadeTimer = 0f;
				}
			}
		}
		else if (this.nowType == Fade.Type.Out)
		{
			this.fadeTimer += num;
			float num3 = Mathf.Clamp01(Mathf.Lerp(this.fadeOut.start, this.fadeOut.end, Mathf.InverseLerp(0f, this.fadeOut.time, this.fadeTimer)));
			this.fadeRenderer.SetAlpha(num3);
			this.loadingRenderer.SetAlpha((!this.usingLoadingTex) ? 0f : num3);
			if (num3 == this.fadeOut.end)
			{
				this.FadeEnd();
			}
		}
	}

	// Token: 0x0400771A RID: 30490
	[SerializeField]
	private CanvasRenderer fadeRenderer;

	// Token: 0x0400771B RID: 30491
	[SerializeField]
	private CanvasRenderer loadingRenderer;

	// Token: 0x0400771C RID: 30492
	public float fadeWaitTime = 1f;

	// Token: 0x0400771D RID: 30493
	public Fade.FadeIn fadeIn;

	// Token: 0x0400771E RID: 30494
	public Fade.FadeOut fadeOut;

	// Token: 0x04007720 RID: 30496
	private float fadeTimer;

	// Token: 0x04007721 RID: 30497
	private float nowWaitTimer;

	// Token: 0x04007723 RID: 30499
	private bool usingLoadingTex;

	// Token: 0x0200117A RID: 4474
	public enum Type
	{
		// Token: 0x04007725 RID: 30501
		InOut,
		// Token: 0x04007726 RID: 30502
		In,
		// Token: 0x04007727 RID: 30503
		Out
	}

	// Token: 0x0200117B RID: 4475
	[Serializable]
	public class FadeIn
	{
		// Token: 0x04007728 RID: 30504
		public float start;

		// Token: 0x04007729 RID: 30505
		public float end = 1f;

		// Token: 0x0400772A RID: 30506
		public float time = 2f;
	}

	// Token: 0x0200117C RID: 4476
	[Serializable]
	public class FadeOut
	{
		// Token: 0x0400772B RID: 30507
		public float start = 1f;

		// Token: 0x0400772C RID: 30508
		public float end;

		// Token: 0x0400772D RID: 30509
		public float time = 2f;
	}
}
