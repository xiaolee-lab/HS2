using System;
using UnityEngine;

namespace UnityStandardAssets.CinematicEffects
{
	// Token: 0x02000427 RID: 1063
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	[AddComponentMenu("Image Effects/Cinematic/ME_Bloom")]
	[ImageEffectAllowedInSceneView]
	public class ME_Bloom : MonoBehaviour
	{
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06001367 RID: 4967 RVA: 0x000779E7 File Offset: 0x00075DE7
		public Shader shader
		{
			get
			{
				if (this.m_Shader == null)
				{
					this.m_Shader = Shader.Find("Hidden/Image Effects/Cinematic/ME_Bloom");
				}
				return this.m_Shader;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06001368 RID: 4968 RVA: 0x00077A10 File Offset: 0x00075E10
		public Material material
		{
			get
			{
				if (this.m_Material == null)
				{
					this.m_Material = ME_ImageEffectHelper.CheckShaderAndCreateMaterial(this.shader);
				}
				return this.m_Material;
			}
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x00077A3C File Offset: 0x00075E3C
		private void Awake()
		{
			this.m_Threshold = Shader.PropertyToID("_Threshold");
			this.m_Curve = Shader.PropertyToID("_Curve");
			this.m_PrefilterOffs = Shader.PropertyToID("_PrefilterOffs");
			this.m_SampleScale = Shader.PropertyToID("_SampleScale");
			this.m_Intensity = Shader.PropertyToID("_Intensity");
			this.m_DirtTex = Shader.PropertyToID("_DirtTex");
			this.m_DirtIntensity = Shader.PropertyToID("_DirtIntensity");
			this.m_BaseTex = Shader.PropertyToID("_BaseTex");
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x00077AC9 File Offset: 0x00075EC9
		private void OnEnable()
		{
			if (!ME_ImageEffectHelper.IsSupported(this.shader, true, false, this))
			{
				base.enabled = false;
			}
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x00077AE5 File Offset: 0x00075EE5
		private void OnDisable()
		{
			if (this.m_Material != null)
			{
				UnityEngine.Object.DestroyImmediate(this.m_Material);
			}
			this.m_Material = null;
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x00077B0C File Offset: 0x00075F0C
		private void OnRenderImage(RenderTexture source, RenderTexture destination)
		{
			bool isMobilePlatform = Application.isMobilePlatform;
			int num = source.width;
			int num2 = source.height;
			if (!this.settings.highQuality)
			{
				num /= 2;
				num2 /= 2;
			}
			RenderTextureFormat format = (!isMobilePlatform) ? RenderTextureFormat.DefaultHDR : RenderTextureFormat.Default;
			float num3 = Mathf.Log((float)num2, 2f) + this.settings.radius - 8f;
			int num4 = (int)num3;
			int num5 = Mathf.Clamp(num4, 1, 16);
			float thresholdLinear = this.settings.thresholdLinear;
			this.material.SetFloat(this.m_Threshold, thresholdLinear);
			float num6 = thresholdLinear * this.settings.softKnee + 1E-05f;
			Vector3 v = new Vector3(thresholdLinear - num6, num6 * 2f, 0.25f / num6);
			this.material.SetVector(this.m_Curve, v);
			bool flag = !this.settings.highQuality && this.settings.antiFlicker;
			this.material.SetFloat(this.m_PrefilterOffs, (!flag) ? 0f : -0.5f);
			this.material.SetFloat(this.m_SampleScale, 0.5f + num3 - (float)num4);
			this.material.SetFloat(this.m_Intensity, Mathf.Max(0f, this.settings.intensity));
			bool flag2 = false;
			if (this.settings.dirtTexture != null)
			{
				this.material.SetTexture(this.m_DirtTex, this.settings.dirtTexture);
				this.material.SetFloat(this.m_DirtIntensity, this.settings.dirtIntensity);
				flag2 = true;
			}
			RenderTexture temporary = RenderTexture.GetTemporary(num, num2, 0, format);
			Graphics.Blit(source, temporary, this.material, (!this.settings.antiFlicker) ? 0 : 1);
			RenderTexture renderTexture = temporary;
			for (int i = 0; i < num5; i++)
			{
				this.m_blurBuffer1[i] = RenderTexture.GetTemporary(renderTexture.width / 2, renderTexture.height / 2, 0, format);
				Graphics.Blit(renderTexture, this.m_blurBuffer1[i], this.material, (i != 0) ? 4 : ((!this.settings.antiFlicker) ? 2 : 3));
				renderTexture = this.m_blurBuffer1[i];
			}
			for (int j = num5 - 2; j >= 0; j--)
			{
				RenderTexture renderTexture2 = this.m_blurBuffer1[j];
				this.material.SetTexture(this.m_BaseTex, renderTexture2);
				this.m_blurBuffer2[j] = RenderTexture.GetTemporary(renderTexture2.width, renderTexture2.height, 0, format);
				Graphics.Blit(renderTexture, this.m_blurBuffer2[j], this.material, (!this.settings.highQuality) ? 5 : 6);
				renderTexture = this.m_blurBuffer2[j];
			}
			int num7 = (!flag2) ? 7 : 9;
			num7 += ((!this.settings.highQuality) ? 0 : 1);
			this.material.SetTexture(this.m_BaseTex, source);
			Graphics.Blit(renderTexture, destination, this.material, num7);
			for (int k = 0; k < 16; k++)
			{
				if (this.m_blurBuffer1[k] != null)
				{
					RenderTexture.ReleaseTemporary(this.m_blurBuffer1[k]);
				}
				if (this.m_blurBuffer2[k] != null)
				{
					RenderTexture.ReleaseTemporary(this.m_blurBuffer2[k]);
				}
				this.m_blurBuffer1[k] = null;
				this.m_blurBuffer2[k] = null;
			}
			RenderTexture.ReleaseTemporary(temporary);
		}

		// Token: 0x040015C1 RID: 5569
		[SerializeField]
		public ME_Bloom.Settings settings = ME_Bloom.Settings.defaultSettings;

		// Token: 0x040015C2 RID: 5570
		[SerializeField]
		[HideInInspector]
		private Shader m_Shader;

		// Token: 0x040015C3 RID: 5571
		private Material m_Material;

		// Token: 0x040015C4 RID: 5572
		private const int kMaxIterations = 16;

		// Token: 0x040015C5 RID: 5573
		private RenderTexture[] m_blurBuffer1 = new RenderTexture[16];

		// Token: 0x040015C6 RID: 5574
		private RenderTexture[] m_blurBuffer2 = new RenderTexture[16];

		// Token: 0x040015C7 RID: 5575
		private int m_Threshold;

		// Token: 0x040015C8 RID: 5576
		private int m_Curve;

		// Token: 0x040015C9 RID: 5577
		private int m_PrefilterOffs;

		// Token: 0x040015CA RID: 5578
		private int m_SampleScale;

		// Token: 0x040015CB RID: 5579
		private int m_Intensity;

		// Token: 0x040015CC RID: 5580
		private int m_DirtTex;

		// Token: 0x040015CD RID: 5581
		private int m_DirtIntensity;

		// Token: 0x040015CE RID: 5582
		private int m_BaseTex;

		// Token: 0x02000428 RID: 1064
		[Serializable]
		public struct Settings
		{
			// Token: 0x1700012C RID: 300
			// (get) Token: 0x0600136E RID: 4974 RVA: 0x00077EDA File Offset: 0x000762DA
			// (set) Token: 0x0600136D RID: 4973 RVA: 0x00077ED1 File Offset: 0x000762D1
			public float thresholdGamma
			{
				get
				{
					return Mathf.Max(0f, this.threshold);
				}
				set
				{
					this.threshold = value;
				}
			}

			// Token: 0x1700012D RID: 301
			// (get) Token: 0x06001370 RID: 4976 RVA: 0x00077EFA File Offset: 0x000762FA
			// (set) Token: 0x0600136F RID: 4975 RVA: 0x00077EEC File Offset: 0x000762EC
			public float thresholdLinear
			{
				get
				{
					return Mathf.GammaToLinearSpace(this.thresholdGamma);
				}
				set
				{
					this.threshold = Mathf.LinearToGammaSpace(value);
				}
			}

			// Token: 0x1700012E RID: 302
			// (get) Token: 0x06001371 RID: 4977 RVA: 0x00077F08 File Offset: 0x00076308
			public static ME_Bloom.Settings defaultSettings
			{
				get
				{
					return new ME_Bloom.Settings
					{
						threshold = 1.1f,
						softKnee = 0.1f,
						radius = 7f,
						intensity = 0.5f,
						highQuality = true,
						antiFlicker = true,
						dirtTexture = null,
						dirtIntensity = 2.5f
					};
				}
			}

			// Token: 0x040015CF RID: 5583
			[SerializeField]
			[Tooltip("Filters out pixels under this level of brightness.")]
			public float threshold;

			// Token: 0x040015D0 RID: 5584
			[SerializeField]
			[Range(0f, 1f)]
			[Tooltip("Makes transition between under/over-threshold gradual.")]
			public float softKnee;

			// Token: 0x040015D1 RID: 5585
			[SerializeField]
			[Range(1f, 7f)]
			[Tooltip("Changes extent of veiling effects in a screen resolution-independent fashion.")]
			public float radius;

			// Token: 0x040015D2 RID: 5586
			[SerializeField]
			[Tooltip("Blend factor of the result image.")]
			public float intensity;

			// Token: 0x040015D3 RID: 5587
			[SerializeField]
			[Tooltip("Controls filter quality and buffer resolution.")]
			public bool highQuality;

			// Token: 0x040015D4 RID: 5588
			[SerializeField]
			[Tooltip("Reduces flashing noise with an additional filter.")]
			public bool antiFlicker;

			// Token: 0x040015D5 RID: 5589
			[Tooltip("Dirtiness texture to add smudges or dust to the lens.")]
			public Texture dirtTexture;

			// Token: 0x040015D6 RID: 5590
			[ME_Min(0f)]
			[Tooltip("Amount of lens dirtiness.")]
			public float dirtIntensity;
		}
	}
}
