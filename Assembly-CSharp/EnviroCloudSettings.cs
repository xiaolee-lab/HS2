using System;
using UnityEngine;

// Token: 0x0200033A RID: 826
[Serializable]
public class EnviroCloudSettings
{
	// Token: 0x04000ECA RID: 3786
	public EnviroCloudSettings.ReprojectionPixelSize reprojectionPixelSize;

	// Token: 0x04000ECB RID: 3787
	[Tooltip("Choose a clouds rendering quality or setup your own when choosing custom.")]
	public EnviroCloudSettings.CloudQuality cloudsQuality;

	// Token: 0x04000ECC RID: 3788
	[Range(10000f, 486000f)]
	[Header("Clouds Scale Settings")]
	[Tooltip("Clouds world scale. This settings will influece rendering of clouds at horizon.")]
	public float cloudsWorldScale = 113081f;

	// Token: 0x04000ECD RID: 3789
	[Tooltip("Clouds start height.")]
	public float bottomCloudHeight = 3000f;

	// Token: 0x04000ECE RID: 3790
	[Tooltip("Clouds end height.")]
	public float topCloudHeight = 7000f;

	// Token: 0x04000ECF RID: 3791
	[Header("Clouds Wind Animation")]
	public bool useWindZoneDirection;

	// Token: 0x04000ED0 RID: 3792
	[Range(-1f, 1f)]
	[Tooltip("Time scale / wind animation speed of clouds.")]
	public float cloudsTimeScale = 1f;

	// Token: 0x04000ED1 RID: 3793
	[Range(0f, 1f)]
	[Tooltip("Global clouds wind speed modificator.")]
	public float cloudsWindStrengthModificator = 0.001f;

	// Token: 0x04000ED2 RID: 3794
	[Range(-1f, 1f)]
	[Tooltip("Global clouds wind direction X axes.")]
	public float cloudsWindDirectionX = 1f;

	// Token: 0x04000ED3 RID: 3795
	[Range(-1f, 1f)]
	[Tooltip("Global clouds wind direction Y axes.")]
	public float cloudsWindDirectionY = 1f;

	// Token: 0x04000ED4 RID: 3796
	[Header("Cloud Rendering")]
	[Range(32f, 256f)]
	[Tooltip("Number of raymarching samples.")]
	public int raymarchSteps = 150;

	// Token: 0x04000ED5 RID: 3797
	[Tooltip("Increase performance by using less steps when clouds are hidden by objects.")]
	[Range(0.1f, 1f)]
	public float stepsInDepthModificator = 0.75f;

	// Token: 0x04000ED6 RID: 3798
	[Range(1f, 16f)]
	[Tooltip("Downsampling of clouds rendering. 1 = full res, 2 = half Res, ...")]
	public int cloudsRenderResolution = 4;

	// Token: 0x04000ED7 RID: 3799
	[Header("Clouds Lighting")]
	public float hgPhase = 0.5f;

	// Token: 0x04000ED8 RID: 3800
	public float primaryAttenuation = 3f;

	// Token: 0x04000ED9 RID: 3801
	public float secondaryAttenuation = 3f;

	// Token: 0x04000EDA RID: 3802
	[Tooltip("Global Color for volume clouds based sun positon.")]
	public Gradient volumeCloudsColor;

	// Token: 0x04000EDB RID: 3803
	[Tooltip("Global Color for clouds based moon positon.")]
	public Gradient volumeCloudsMoonColor;

	// Token: 0x04000EDC RID: 3804
	[Tooltip("Direct Light intensity for clouds based on time of day.")]
	public AnimationCurve directLightIntensity = new AnimationCurve();

	// Token: 0x04000EDD RID: 3805
	[Tooltip("Direct Light intensity for clouds based on time of day.")]
	public AnimationCurve ambientLightIntensity = new AnimationCurve();

	// Token: 0x04000EDE RID: 3806
	[Header("Tonemapping")]
	[Tooltip("Use color tonemapping?")]
	public bool tonemapping;

	// Token: 0x04000EDF RID: 3807
	[Tooltip("Tonemapping exposure")]
	public float cloudsExposure = 1f;

	// Token: 0x04000EE0 RID: 3808
	[Tooltip("LOD Distance for using lower res 3d texture for far away clouds. ")]
	[Range(0f, 1f)]
	public float lodDistance = 0.5f;

	// Token: 0x04000EE1 RID: 3809
	[Header("Weather Map")]
	[Tooltip("Tiling of the generated weather map.")]
	public int weatherMapTiling = 5;

	// Token: 0x04000EE2 RID: 3810
	[Tooltip("Option to add own weather map. Red Channel = Coverage, Blue = Clouds Height")]
	public Texture2D customWeatherMap;

	// Token: 0x04000EE3 RID: 3811
	[Tooltip("Weathermap sampling offset.")]
	public Vector2 locationOffset;

	// Token: 0x04000EE4 RID: 3812
	[Range(0f, 1f)]
	[Tooltip("Weathermap animation speed.")]
	public float weatherAnimSpeedScale = 0.33f;

	// Token: 0x04000EE5 RID: 3813
	[Header("Clouds Modelling")]
	[Tooltip("The UV scale of base noise. High Values = Low performance!")]
	[Range(2f, 100f)]
	public float baseNoiseUV = 20f;

	// Token: 0x04000EE6 RID: 3814
	[Tooltip("The UV scale of detail noise. High Values = Low performance!")]
	[Range(2f, 100f)]
	public float detailNoiseUV = 50f;

	// Token: 0x04000EE7 RID: 3815
	[Tooltip("Resolution of Detail Noise Texture.")]
	public EnviroCloudSettings.CloudDetailQuality detailQuality;

	// Token: 0x04000EE8 RID: 3816
	[Header("Global Clouds Control")]
	[Range(0f, 2f)]
	public float globalCloudCoverage = 1f;

	// Token: 0x04000EE9 RID: 3817
	[Tooltip("Texture for cirrus clouds.")]
	public Texture cirrusCloudsTexture;

	// Token: 0x04000EEA RID: 3818
	[Tooltip("Global Color for flat clouds based sun positon.")]
	public Gradient cirrusCloudsColor;

	// Token: 0x04000EEB RID: 3819
	[Range(5f, 15f)]
	[Tooltip("Flat Clouds Altitude")]
	public float cirrusCloudsAltitude = 10f;

	// Token: 0x04000EEC RID: 3820
	[Tooltip("Texture for flat procedural clouds.")]
	public Texture flatCloudsNoiseTexture;

	// Token: 0x04000EED RID: 3821
	[Tooltip("Resolution of generated flat clouds texture.")]
	public EnviroCloudSettings.FlatCloudResolution flatCloudsResolution = EnviroCloudSettings.FlatCloudResolution.R2048;

	// Token: 0x04000EEE RID: 3822
	[Tooltip("Global Color for flat clouds based sun positon.")]
	public Gradient flatCloudsColor;

	// Token: 0x04000EEF RID: 3823
	[Tooltip("Scale/Tiling of flat clouds.")]
	public float flatCloudsScale = 2f;

	// Token: 0x04000EF0 RID: 3824
	[Range(1f, 12f)]
	[Tooltip("Flat Clouds texture generation iterations.")]
	public int flatCloudsNoiseOctaves = 6;

	// Token: 0x04000EF1 RID: 3825
	[Range(30f, 100f)]
	[Tooltip("Flat Clouds Altitude")]
	public float flatCloudsAltitude = 70f;

	// Token: 0x04000EF2 RID: 3826
	[Range(0.01f, 1f)]
	[Tooltip("Flat Clouds morphing animation speed.")]
	public float flatCloudsMorphingSpeed = 0.2f;

	// Token: 0x04000EF3 RID: 3827
	[Tooltip("Clouds Shadowcast Intensity. 0 = disabled")]
	[Range(0f, 1f)]
	public float shadowIntensity;

	// Token: 0x04000EF4 RID: 3828
	[Tooltip("Size of the shadow cookie.")]
	[Range(1000f, 10000f)]
	public int shadowCookieSize = 10000;

	// Token: 0x0200033B RID: 827
	public enum CloudDetailQuality
	{
		// Token: 0x04000EF6 RID: 3830
		Low,
		// Token: 0x04000EF7 RID: 3831
		High
	}

	// Token: 0x0200033C RID: 828
	public enum CloudQuality
	{
		// Token: 0x04000EF9 RID: 3833
		Lowest,
		// Token: 0x04000EFA RID: 3834
		Low,
		// Token: 0x04000EFB RID: 3835
		Medium,
		// Token: 0x04000EFC RID: 3836
		High,
		// Token: 0x04000EFD RID: 3837
		Ultra,
		// Token: 0x04000EFE RID: 3838
		VR_Low,
		// Token: 0x04000EFF RID: 3839
		VR_Medium,
		// Token: 0x04000F00 RID: 3840
		VR_High,
		// Token: 0x04000F01 RID: 3841
		Custom
	}

	// Token: 0x0200033D RID: 829
	public enum FlatCloudResolution
	{
		// Token: 0x04000F03 RID: 3843
		R512,
		// Token: 0x04000F04 RID: 3844
		R1024,
		// Token: 0x04000F05 RID: 3845
		R2048,
		// Token: 0x04000F06 RID: 3846
		R4096
	}

	// Token: 0x0200033E RID: 830
	public enum ReprojectionPixelSize
	{
		// Token: 0x04000F08 RID: 3848
		Off,
		// Token: 0x04000F09 RID: 3849
		Low,
		// Token: 0x04000F0A RID: 3850
		Medium,
		// Token: 0x04000F0B RID: 3851
		High
	}
}
