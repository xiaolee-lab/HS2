using System;
using UnityEngine;

namespace Studio
{
	// Token: 0x02001237 RID: 4663
	public static class ScreenEffectDefine
	{
		// Token: 0x04007AA3 RID: 31395
		public static int ColorGradingLookupTexture = 0;

		// Token: 0x04007AA4 RID: 31396
		public static float ColorGradingBlend = 0f;

		// Token: 0x04007AA5 RID: 31397
		public static int ColorGradingSaturation = 15;

		// Token: 0x04007AA6 RID: 31398
		public static int ColorGradingBrightness = -20;

		// Token: 0x04007AA7 RID: 31399
		public static int ColorGradingContrast = 40;

		// Token: 0x04007AA8 RID: 31400
		public static bool AmbientOcclusion = true;

		// Token: 0x04007AA9 RID: 31401
		public static float AmbientOcclusionIntensity = 0.2f;

		// Token: 0x04007AAA RID: 31402
		public static float AmbientOcclusionThicknessModeifier = 2f;

		// Token: 0x04007AAB RID: 31403
		[ColorUsage(false, true)]
		public static Color AmbientOcclusionColor = Utility.ConvertColor(0, 0, 0);

		// Token: 0x04007AAC RID: 31404
		public static bool Bloom = true;

		// Token: 0x04007AAD RID: 31405
		public static float BloomIntensity = 2f;

		// Token: 0x04007AAE RID: 31406
		public static float BloomThreshold = 1f;

		// Token: 0x04007AAF RID: 31407
		public static float BloomSoftKnee = 0.76f;

		// Token: 0x04007AB0 RID: 31408
		public static bool BloomClamp = true;

		// Token: 0x04007AB1 RID: 31409
		public static float BloomDiffusion = 7f;

		// Token: 0x04007AB2 RID: 31410
		public static Color BloomColor = Utility.ConvertColor(191, 191, 191);

		// Token: 0x04007AB3 RID: 31411
		public static bool Vignette = true;

		// Token: 0x04007AB4 RID: 31412
		public static float VignetteIntensity = 0.35f;

		// Token: 0x04007AB5 RID: 31413
		public static bool ScreenSpaceReflections = true;

		// Token: 0x04007AB6 RID: 31414
		public static bool ReflectionProbe = false;

		// Token: 0x04007AB7 RID: 31415
		public static int ReflectionProbeCubemap = 0;

		// Token: 0x04007AB8 RID: 31416
		public static float ReflectionProbeIntensity = 1f;

		// Token: 0x04007AB9 RID: 31417
		public static bool Fog = false;

		// Token: 0x04007ABA RID: 31418
		public static bool FogExcludeFarPixels = false;

		// Token: 0x04007ABB RID: 31419
		public static float FogHeight = 50f;

		// Token: 0x04007ABC RID: 31420
		public static float FogHeightDensity = 0.01f;

		// Token: 0x04007ABD RID: 31421
		public static Color FogColor = Utility.ConvertColor(138, 168, 203);

		// Token: 0x04007ABE RID: 31422
		public static float FogDensity = 0.0005f;

		// Token: 0x04007ABF RID: 31423
		public static bool DepthOfField = false;

		// Token: 0x04007AC0 RID: 31424
		public static int DepthOfFieldForcus = -1;

		// Token: 0x04007AC1 RID: 31425
		public static float DepthOfFieldFocalSize = 0.4f;

		// Token: 0x04007AC2 RID: 31426
		public static float DepthOfFieldAperture = 0.6f;

		// Token: 0x04007AC3 RID: 31427
		public static bool SunShaft = false;

		// Token: 0x04007AC4 RID: 31428
		public static int SunShaftCaster = -1;

		// Token: 0x04007AC5 RID: 31429
		public static Color SunShaftThresholdColor = Utility.ConvertColor(128, 128, 128);

		// Token: 0x04007AC6 RID: 31430
		public static Color SunShaftShaftsColor = Utility.ConvertColor(255, 255, 255);

		// Token: 0x04007AC7 RID: 31431
		public static float SunShaftDistanceFallOff = 0.75f;

		// Token: 0x04007AC8 RID: 31432
		public static float SunShaftBlurSize = 5f;

		// Token: 0x04007AC9 RID: 31433
		public static float SunShaftIntensity = 5f;

		// Token: 0x04007ACA RID: 31434
		public static Color EnvironmentLightingSkyColor = Utility.ConvertColor(170, 188, 243);

		// Token: 0x04007ACB RID: 31435
		public static Color EnvironmentLightingEquatorColor = Utility.ConvertColor(185, 195, 205);

		// Token: 0x04007ACC RID: 31436
		public static Color EnvironmentLightingGroundColor = Utility.ConvertColor(204, 109, 41);
	}
}
