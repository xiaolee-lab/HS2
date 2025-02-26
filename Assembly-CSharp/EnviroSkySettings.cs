using System;
using UnityEngine;

// Token: 0x02000330 RID: 816
[Serializable]
public class EnviroSkySettings
{
	// Token: 0x04000E64 RID: 3684
	[Header("Sky Mode:")]
	[Tooltip("Select if you want to use enviro skybox your custom material.")]
	public EnviroSkySettings.SkyboxModi skyboxMode;

	// Token: 0x04000E65 RID: 3685
	[Tooltip("If SkyboxMode == CustomSkybox : Assign your skybox material here!")]
	public Material customSkyboxMaterial;

	// Token: 0x04000E66 RID: 3686
	[Tooltip("If SkyboxMode == CustomColor : Select your sky color here!")]
	public Color customSkyboxColor;

	// Token: 0x04000E67 RID: 3687
	[Tooltip("Enable to render black skybox at ground level.")]
	public bool blackGroundMode;

	// Token: 0x04000E68 RID: 3688
	[Header("Scattering")]
	[Tooltip("Light Wavelength used for atmospheric scattering. Keep it near defaults for earthlike atmospheres, or change for alien or fantasy atmospheres for example.")]
	public Vector3 waveLength = new Vector3(540f, 496f, 437f);

	// Token: 0x04000E69 RID: 3689
	[Tooltip("Influence atmospheric scattering.")]
	public float rayleigh = 5.15f;

	// Token: 0x04000E6A RID: 3690
	[Tooltip("Sky turbidity. Particle in air. Influence atmospheric scattering.")]
	public float turbidity = 1f;

	// Token: 0x04000E6B RID: 3691
	[Tooltip("Influence scattering near sun.")]
	public float mie = 5f;

	// Token: 0x04000E6C RID: 3692
	[Tooltip("Influence scattering near sun.")]
	public float g = 0.8f;

	// Token: 0x04000E6D RID: 3693
	[Tooltip("Intensity gradient for atmospheric scattering. Influence atmospheric scattering based on current sun altitude.")]
	public AnimationCurve scatteringCurve = new AnimationCurve();

	// Token: 0x04000E6E RID: 3694
	[Tooltip("Color gradient for atmospheric scattering. Influence atmospheric scattering based on current sun altitude.")]
	public Gradient scatteringColor;

	// Token: 0x04000E6F RID: 3695
	[Header("Sun")]
	public EnviroSkySettings.SunAndMoonCalc sunAndMoonPosition = EnviroSkySettings.SunAndMoonCalc.Realistic;

	// Token: 0x04000E70 RID: 3696
	[Tooltip("Intensity of Sun Influence Scale and Dropoff of sundisk.")]
	public float sunIntensity = 100f;

	// Token: 0x04000E71 RID: 3697
	[Tooltip("Scale of rendered sundisk.")]
	public float sunDiskScale = 20f;

	// Token: 0x04000E72 RID: 3698
	[Tooltip("Intenisty of rendered sundisk.")]
	public float sunDiskIntensity = 3f;

	// Token: 0x04000E73 RID: 3699
	[Tooltip("Color gradient for sundisk. Influence sundisk color based on current sun altitude")]
	public Gradient sunDiskColor;

	// Token: 0x04000E74 RID: 3700
	[Header("Moon")]
	[Tooltip("Whether to render the moon.")]
	public bool renderMoon = true;

	// Token: 0x04000E75 RID: 3701
	[Tooltip("The Moon phase mode. Custom = for customizable phase.")]
	public EnviroSkySettings.MoonPhases moonPhaseMode = EnviroSkySettings.MoonPhases.Realistic;

	// Token: 0x04000E76 RID: 3702
	[Tooltip("The Moon texture.")]
	public Texture moonTexture;

	// Token: 0x04000E77 RID: 3703
	[Tooltip("The color of the moon")]
	public Color moonColor;

	// Token: 0x04000E78 RID: 3704
	[Range(0f, 5f)]
	[Tooltip("Brightness of the moon.")]
	public float moonBrightness = 1f;

	// Token: 0x04000E79 RID: 3705
	[Range(0f, 20f)]
	[Tooltip("Size of the moon.")]
	public float moonSize = 10f;

	// Token: 0x04000E7A RID: 3706
	[Tooltip("Glow around moon.")]
	public AnimationCurve moonGlow = new AnimationCurve();

	// Token: 0x04000E7B RID: 3707
	[Tooltip("Glow color around moon.")]
	public Color moonGlowColor;

	// Token: 0x04000E7C RID: 3708
	[Tooltip("Start moon phase when using custom phase mode.(-1f - 1f)")]
	[Range(-1f, 1f)]
	public float startMoonPhase;

	// Token: 0x04000E7D RID: 3709
	[Header("Sky Color Corrections")]
	[Tooltip("Higher values = brighter sky.")]
	public AnimationCurve skyLuminence = new AnimationCurve();

	// Token: 0x04000E7E RID: 3710
	[Tooltip("Higher values = stronger colors applied BEFORE clouds rendered!")]
	public AnimationCurve skyColorPower = new AnimationCurve();

	// Token: 0x04000E7F RID: 3711
	[Header("Tonemapping - LDR")]
	[Tooltip("Tonemapping when using LDR")]
	public float skyExposure = 1.5f;

	// Token: 0x04000E80 RID: 3712
	[Header("Stars")]
	[Tooltip("A cubemap for night sky.")]
	public Cubemap starsCubeMap;

	// Token: 0x04000E81 RID: 3713
	[Tooltip("Intensity of stars based on time of day.")]
	public AnimationCurve starsIntensity = new AnimationCurve();

	// Token: 0x04000E82 RID: 3714
	[Header("Galaxy")]
	[Tooltip("A cubemap for night galaxy.")]
	public Cubemap galaxyCubeMap;

	// Token: 0x04000E83 RID: 3715
	[Tooltip("Intensity of galaxy based on time of day.")]
	public AnimationCurve galaxyIntensity = new AnimationCurve();

	// Token: 0x04000E84 RID: 3716
	[Header("Sky Dithering")]
	public float noiseScale = 10f;

	// Token: 0x04000E85 RID: 3717
	public float noiseIntensity = 1.5f;

	// Token: 0x02000331 RID: 817
	public enum SunAndMoonCalc
	{
		// Token: 0x04000E87 RID: 3719
		Simple,
		// Token: 0x04000E88 RID: 3720
		Realistic
	}

	// Token: 0x02000332 RID: 818
	public enum MoonPhases
	{
		// Token: 0x04000E8A RID: 3722
		Custom,
		// Token: 0x04000E8B RID: 3723
		Realistic
	}

	// Token: 0x02000333 RID: 819
	public enum SkyboxModi
	{
		// Token: 0x04000E8D RID: 3725
		Default,
		// Token: 0x04000E8E RID: 3726
		CustomSkybox,
		// Token: 0x04000E8F RID: 3727
		CustomColor
	}
}
