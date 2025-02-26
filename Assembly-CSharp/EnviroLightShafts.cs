using System;
using UnityEngine;

// Token: 0x02000327 RID: 807
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Enviro/Effects/LightShafts")]
public class EnviroLightShafts : EnviroEffects
{
	// Token: 0x06000E3B RID: 3643 RVA: 0x00045300 File Offset: 0x00043700
	public override bool CheckResources()
	{
		base.CheckSupport(this.useDepthTexture);
		this.sunShaftsMaterial = base.CheckShaderAndCreateMaterial(this.sunShaftsShader, this.sunShaftsMaterial);
		this.simpleClearMaterial = base.CheckShaderAndCreateMaterial(this.simpleClearShader, this.simpleClearMaterial);
		if (!this.isSupported)
		{
			base.ReportAutoDisable();
		}
		return this.isSupported;
	}

	// Token: 0x06000E3C RID: 3644 RVA: 0x00045364 File Offset: 0x00043764
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.CheckResources())
		{
			Graphics.Blit(source, destination);
			return;
		}
		if (EnviroSky.instance == null)
		{
			Graphics.Blit(source, destination);
			return;
		}
		if (this.cam == null)
		{
			this.cam = base.GetComponent<Camera>();
		}
		if (this.useDepthTexture && this.cam.actualRenderingPath == RenderingPath.Forward)
		{
			this.cam.depthTextureMode |= DepthTextureMode.Depth;
		}
		int num = 4;
		if (this.resolution == EnviroLightShafts.SunShaftsResolution.Normal)
		{
			num = 2;
		}
		else if (this.resolution == EnviroLightShafts.SunShaftsResolution.High)
		{
			num = 1;
		}
		Vector3 vector = Vector3.one * 0.5f;
		if (this.sunTransform)
		{
			vector = this.cam.WorldToViewportPoint(this.sunTransform.position);
		}
		else
		{
			vector = new Vector3(0.5f, 0.5f, 0f);
		}
		int width = source.width / num;
		int height = source.height / num;
		RenderTexture temporary = RenderTexture.GetTemporary(width, height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Default, 1, RenderTextureMemoryless.None, source.vrUsage);
		this.sunShaftsMaterial.SetVector("_BlurRadius4", new Vector4(1f, 1f, 0f, 0f) * this.sunShaftBlurRadius);
		this.sunShaftsMaterial.SetVector("_SunPosition", new Vector4(vector.x, vector.y, vector.z, this.maxRadius));
		this.sunShaftsMaterial.SetVector("_SunThreshold", this.sunThreshold);
		if (!this.useDepthTexture)
		{
			RenderTextureFormat format = (!EnviroSky.instance.GetCameraHDR(this.cam)) ? RenderTextureFormat.Default : RenderTextureFormat.DefaultHDR;
			RenderTexture temporary2 = RenderTexture.GetTemporary(source.width, source.height, 0, format);
			RenderTexture.active = temporary2;
			GL.ClearWithSkybox(false, this.cam);
			this.sunShaftsMaterial.SetTexture("_Skybox", temporary2);
			Graphics.Blit(source, temporary, this.sunShaftsMaterial, 3);
			RenderTexture.ReleaseTemporary(temporary2);
		}
		else
		{
			Graphics.Blit(source, temporary, this.sunShaftsMaterial, 2);
		}
		if (this.cam.stereoActiveEye == Camera.MonoOrStereoscopicEye.Mono)
		{
			base.DrawBorder(temporary, this.simpleClearMaterial);
		}
		this.radialBlurIterations = Mathf.Clamp(this.radialBlurIterations, 1, 4);
		float num2 = this.sunShaftBlurRadius * 0.0013020834f;
		this.sunShaftsMaterial.SetVector("_BlurRadius4", new Vector4(num2, num2, 0f, 0f));
		this.sunShaftsMaterial.SetVector("_SunPosition", new Vector4(vector.x, vector.y, vector.z, this.maxRadius));
		for (int i = 0; i < this.radialBlurIterations; i++)
		{
			RenderTexture temporary3 = RenderTexture.GetTemporary(width, height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Default, 1, RenderTextureMemoryless.None, source.vrUsage);
			Graphics.Blit(temporary, temporary3, this.sunShaftsMaterial, 1);
			RenderTexture.ReleaseTemporary(temporary);
			num2 = this.sunShaftBlurRadius * (((float)i * 2f + 1f) * 6f) / 768f;
			this.sunShaftsMaterial.SetVector("_BlurRadius4", new Vector4(num2, num2, 0f, 0f));
			temporary = RenderTexture.GetTemporary(width, height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Default, 1, RenderTextureMemoryless.None, source.vrUsage);
			Graphics.Blit(temporary3, temporary, this.sunShaftsMaterial, 1);
			RenderTexture.ReleaseTemporary(temporary3);
			num2 = this.sunShaftBlurRadius * (((float)i * 2f + 2f) * 6f) / 768f;
			this.sunShaftsMaterial.SetVector("_BlurRadius4", new Vector4(num2, num2, 0f, 0f));
		}
		if (vector.z >= 0f)
		{
			this.sunShaftsMaterial.SetVector("_SunColor", new Vector4(this.sunColor.r, this.sunColor.g, this.sunColor.b, this.sunColor.a) * this.sunShaftIntensity);
		}
		else
		{
			this.sunShaftsMaterial.SetVector("_SunColor", Vector4.zero);
		}
		this.sunShaftsMaterial.SetTexture("_ColorBuffer", temporary);
		Graphics.Blit(source, destination, this.sunShaftsMaterial, (this.screenBlendMode != EnviroLightShafts.ShaftsScreenBlendMode.Screen) ? 4 : 0);
		RenderTexture.ReleaseTemporary(temporary);
	}

	// Token: 0x04000E2F RID: 3631
	[HideInInspector]
	public EnviroLightShafts.SunShaftsResolution resolution = EnviroLightShafts.SunShaftsResolution.Normal;

	// Token: 0x04000E30 RID: 3632
	[HideInInspector]
	public EnviroLightShafts.ShaftsScreenBlendMode screenBlendMode;

	// Token: 0x04000E31 RID: 3633
	[HideInInspector]
	public Transform sunTransform;

	// Token: 0x04000E32 RID: 3634
	[HideInInspector]
	public int radialBlurIterations = 2;

	// Token: 0x04000E33 RID: 3635
	[HideInInspector]
	public Color sunColor = Color.white;

	// Token: 0x04000E34 RID: 3636
	[HideInInspector]
	public Color sunThreshold = new Color(0.87f, 0.74f, 0.65f);

	// Token: 0x04000E35 RID: 3637
	[HideInInspector]
	public float sunShaftBlurRadius = 2.5f;

	// Token: 0x04000E36 RID: 3638
	[HideInInspector]
	public float sunShaftIntensity = 1.15f;

	// Token: 0x04000E37 RID: 3639
	[HideInInspector]
	public float maxRadius = 0.75f;

	// Token: 0x04000E38 RID: 3640
	[HideInInspector]
	public bool useDepthTexture = true;

	// Token: 0x04000E39 RID: 3641
	[HideInInspector]
	public Shader sunShaftsShader;

	// Token: 0x04000E3A RID: 3642
	[HideInInspector]
	public Material sunShaftsMaterial;

	// Token: 0x04000E3B RID: 3643
	[HideInInspector]
	public Shader simpleClearShader;

	// Token: 0x04000E3C RID: 3644
	[HideInInspector]
	public Material simpleClearMaterial;

	// Token: 0x04000E3D RID: 3645
	private Camera cam;

	// Token: 0x02000328 RID: 808
	public enum SunShaftsResolution
	{
		// Token: 0x04000E3F RID: 3647
		Low,
		// Token: 0x04000E40 RID: 3648
		Normal,
		// Token: 0x04000E41 RID: 3649
		High
	}

	// Token: 0x02000329 RID: 809
	public enum ShaftsScreenBlendMode
	{
		// Token: 0x04000E43 RID: 3651
		Screen,
		// Token: 0x04000E44 RID: 3652
		Add
	}
}
