using System;

// Token: 0x02000623 RID: 1571
public static class AllOptionsKeyPro
{
	// Token: 0x0600257E RID: 9598 RVA: 0x000D6B7C File Offset: 0x000D4F7C
	public static int BoolToInt(bool b)
	{
		return (!b) ? 0 : 1;
	}

	// Token: 0x0600257F RID: 9599 RVA: 0x000D6B8B File Offset: 0x000D4F8B
	public static bool IntToBool(int i)
	{
		return i == 1;
	}

	// Token: 0x0400253C RID: 9532
	public const string AntiAliasing = "GameName.AntiAliasing";

	// Token: 0x0400253D RID: 9533
	public const string AnisoTropic = "GameName.AnisoTropic";

	// Token: 0x0400253E RID: 9534
	public const string Resolution = "GameName.ResolutionScreen";

	// Token: 0x0400253F RID: 9535
	public const string ResolutionMode = "GameName.ResolutionMode";

	// Token: 0x04002540 RID: 9536
	public const string VsyncCount = "GameName.VSyncCount";

	// Token: 0x04002541 RID: 9537
	public const string BlendWeight = "GameName.BlendWeight";

	// Token: 0x04002542 RID: 9538
	public const string Volumen = "GameName.Volumen";

	// Token: 0x04002543 RID: 9539
	public const string Quality = "GameName.QualityLevel";

	// Token: 0x04002544 RID: 9540
	public const string TextureLimit = "GameName.TextureLimit";

	// Token: 0x04002545 RID: 9541
	public const string ShadowCascade = "GameName.ShadowCascade";

	// Token: 0x04002546 RID: 9542
	public const string ShowFPS = "GameName.ShowFPS";

	// Token: 0x04002547 RID: 9543
	public const string ShadowDistance = "GameName.ShadowDistance";

	// Token: 0x04002548 RID: 9544
	public const string ShadownProjection = "GameName.ShadowProjection";

	// Token: 0x04002549 RID: 9545
	public const string PauseAudio = "GameName.PauseAudio";

	// Token: 0x0400254A RID: 9546
	public const string ShadowEnable = "GameName.ShadowEnable";

	// Token: 0x0400254B RID: 9547
	public const string Brightness = "GameName.Brightness";

	// Token: 0x0400254C RID: 9548
	public const string RealtimeReflection = "GameName.RealtimeReflection";

	// Token: 0x0400254D RID: 9549
	public const string LodBias = "GameName.LoadBias";

	// Token: 0x0400254E RID: 9550
	public const string HUDScale = "GameName.HudScale";
}
