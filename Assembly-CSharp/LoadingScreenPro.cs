using System;
using PlayfulSystems.LoadingScreen;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000635 RID: 1589
public class LoadingScreenPro : LoadingScreenProBase
{
	// Token: 0x060025B9 RID: 9657 RVA: 0x000D7A71 File Offset: 0x000D5E71
	protected override void Init()
	{
		if (this.doFade)
		{
			this.fade = base.gameObject.AddComponent<CameraFade>();
			this.fade.Init();
		}
	}

	// Token: 0x060025BA RID: 9658 RVA: 0x000D7A9C File Offset: 0x000D5E9C
	protected override void DisplaySceneInfo(SceneInfo info)
	{
		this.SetTextIfStringIsNotNull(this.sceneInfoHeader, (info == null) ? null : info.header);
		this.SetTextIfStringIsNotNull(this.sceneInfoDescription, (info == null) ? null : info.description);
		if (this.sceneInfoImage != null && info != null && !string.IsNullOrEmpty(info.imageName))
		{
			this.sceneInfoImage.sprite = Resources.Load<Sprite>("ScenePreviews/" + info.imageName);
			AspectRatioFitter component = this.sceneInfoImage.GetComponent<AspectRatioFitter>();
			if (component != null && this.sceneInfoImage.sprite != null)
			{
				component.aspectRatio = (float)this.sceneInfoImage.sprite.texture.width / (float)this.sceneInfoImage.sprite.texture.height;
			}
		}
	}

	// Token: 0x060025BB RID: 9659 RVA: 0x000D7B8D File Offset: 0x000D5F8D
	protected override void DisplayGameTip(LoadingTip info)
	{
		this.SetTextIfStringIsNotNull(this.tipHeader, (info == null) ? null : info.header);
		this.SetTextIfStringIsNotNull(this.tipDescription, (info == null) ? null : info.description);
	}

	// Token: 0x060025BC RID: 9660 RVA: 0x000D7BCC File Offset: 0x000D5FCC
	protected override void ShowStartingVisuals()
	{
		if (this.doFade)
		{
			this.fade.StartFadeFrom(this.fadeFromColor, this.fadeInDuration, null);
		}
		this.SetLoadingVisuals(0f);
		this.ShowGroup(this.loadingCanvasGroup, true, 0f);
		this.ShowGroup(this.confirmationCanvasGroup, false, 0f);
	}

	// Token: 0x060025BD RID: 9661 RVA: 0x000D7C2B File Offset: 0x000D602B
	protected override void ShowProgressVisuals(float progress)
	{
		this.SetLoadingVisuals(progress);
	}

	// Token: 0x060025BE RID: 9662 RVA: 0x000D7C34 File Offset: 0x000D6034
	protected override void ShowLoadingDoneVisuals()
	{
		this.ShowGroup(this.loadingCanvasGroup, false, 0.25f);
		this.ShowGroup(this.confirmationCanvasGroup, true, 0.25f);
	}

	// Token: 0x060025BF RID: 9663 RVA: 0x000D7C5A File Offset: 0x000D605A
	protected override void ShowEndingVisuals()
	{
		if (this.doFade)
		{
			this.fade.StartFadeTo(this.fadeToColor, this.fadeOutDuration, null);
		}
	}

	// Token: 0x060025C0 RID: 9664 RVA: 0x000D7C7F File Offset: 0x000D607F
	protected override bool CanShowConfirmation()
	{
		return !(this.progressBar != null) || !this.progressBar.IsAnimating();
	}

	// Token: 0x060025C1 RID: 9665 RVA: 0x000D7CA2 File Offset: 0x000D60A2
	protected override bool CanActivateTargetScene()
	{
		return !this.doFade || !(this.fade != null) || !this.fade.IsFading();
	}

	// Token: 0x060025C2 RID: 9666 RVA: 0x000D7CD0 File Offset: 0x000D60D0
	private void SetTextIfStringIsNotNull(Text text, string s)
	{
		if (text == null)
		{
			return;
		}
		if (string.IsNullOrEmpty(s))
		{
			text.gameObject.SetActive(false);
		}
		else
		{
			text.gameObject.SetActive(true);
			text.text = s;
		}
	}

	// Token: 0x060025C3 RID: 9667 RVA: 0x000D7D10 File Offset: 0x000D6110
	private void ShowGroup(CanvasGroup group, bool show, float fadeDuration)
	{
		if (group == null)
		{
			return;
		}
		CanvasGroupFade canvasGroupFade = group.GetComponent<CanvasGroupFade>();
		if (canvasGroupFade == null)
		{
			canvasGroupFade = group.gameObject.AddComponent<CanvasGroupFade>();
		}
		if (canvasGroupFade != null)
		{
			canvasGroupFade.FadeAlpha((!show) ? 1f : 0f, (!show) ? 0f : 1f, fadeDuration);
		}
	}

	// Token: 0x060025C4 RID: 9668 RVA: 0x000D7D88 File Offset: 0x000D6188
	private void SetLoadingVisuals(float progress)
	{
		if (this.progressBar != null)
		{
			this.progressBar.Value = progress;
		}
		if (this.loadingBar != null)
		{
			this.loadingBar.fillAmount = progress;
		}
		if (this.loadingText != null)
		{
			this.loadingText.text = this.loadingString.Replace("#progress#", Mathf.RoundToInt(progress * 100f).ToString());
		}
	}

	// Token: 0x0400257A RID: 9594
	[Header("Scene Info")]
	public Text sceneInfoHeader;

	// Token: 0x0400257B RID: 9595
	public Text sceneInfoDescription;

	// Token: 0x0400257C RID: 9596
	public Image sceneInfoImage;

	// Token: 0x0400257D RID: 9597
	private const string scenePreviewPath = "ScenePreviews/";

	// Token: 0x0400257E RID: 9598
	[Header("Game Tips")]
	public Text tipHeader;

	// Token: 0x0400257F RID: 9599
	public Text tipDescription;

	// Token: 0x04002580 RID: 9600
	[Header("Fade Settings")]
	public bool doFade = true;

	// Token: 0x04002581 RID: 9601
	public float fadeInDuration = 1f;

	// Token: 0x04002582 RID: 9602
	public float fadeOutDuration = 1f;

	// Token: 0x04002583 RID: 9603
	public Color fadeFromColor = Color.black;

	// Token: 0x04002584 RID: 9604
	public Color fadeToColor = Color.black;

	// Token: 0x04002585 RID: 9605
	private CameraFade fade;

	// Token: 0x04002586 RID: 9606
	[Header("Loading Visuals")]
	[Tooltip("A canvas group and parent for all graphics to show during loading. Leave empty if you want no fading.")]
	public CanvasGroup loadingCanvasGroup;

	// Token: 0x04002587 RID: 9607
	[Tooltip("Progress Bar Pro that will filled as the target scene loads.")]
	public ProgressBarPro progressBar;

	// Token: 0x04002588 RID: 9608
	[Tooltip("Fillable image that will be filled as the target scene loads.")]
	public Image loadingBar;

	// Token: 0x04002589 RID: 9609
	public Text loadingText;

	// Token: 0x0400258A RID: 9610
	[Tooltip("#progress# will be replaced with the loading progress from 0 to 100.")]
	public string loadingString = "#progress#%";

	// Token: 0x0400258B RID: 9611
	[Header("Confirmation Visuals")]
	[Tooltip("A canvas group and parent for all graphics to show once loading is done. Leave empty if you want no fading.")]
	public CanvasGroup confirmationCanvasGroup;
}
