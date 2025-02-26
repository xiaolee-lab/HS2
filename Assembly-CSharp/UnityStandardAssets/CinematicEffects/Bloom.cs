using System;
using UnityEngine;

namespace UnityStandardAssets.CinematicEffects
{
	// Token: 0x02000437 RID: 1079
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	[AddComponentMenu("Image Effects/Cinematic/Bloom")]
	[ImageEffectAllowedInSceneView]
	public class Bloom : MonoBehaviour
	{
		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060013C3 RID: 5059 RVA: 0x0007A93F File Offset: 0x00078D3F
		public Shader shader
		{
			get
			{
				if (this.m_Shader == null)
				{
					this.m_Shader = Shader.Find("Hidden/Image Effects/Cinematic/Bloom");
				}
				return this.m_Shader;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060013C4 RID: 5060 RVA: 0x0007A968 File Offset: 0x00078D68
		public Material material
		{
			get
			{
				if (this.m_Material == null)
				{
					this.m_Material = ImageEffectHelper.CheckShaderAndCreateMaterial(this.shader);
				}
				return this.m_Material;
			}
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x0007A994 File Offset: 0x00078D94
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

		// Token: 0x060013C6 RID: 5062 RVA: 0x0007AA21 File Offset: 0x00078E21
		private void OnEnable()
		{
			if (!ImageEffectHelper.IsSupported(this.shader, true, false, this))
			{
				base.enabled = false;
			}
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x0007AA3D File Offset: 0x00078E3D
		private void OnDisable()
		{
			if (this.m_Material != null)
			{
				UnityEngine.Object.DestroyImmediate(this.m_Material);
			}
			this.m_Material = null;
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x0007AA64 File Offset: 0x00078E64
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

		// Token: 0x0400162D RID: 5677
		[SerializeField]
		public Bloom.Settings settings = Bloom.Settings.defaultSettings;

		// Token: 0x0400162E RID: 5678
		[SerializeField]
		[HideInInspector]
		private Shader m_Shader;

		// Token: 0x0400162F RID: 5679
		private Material m_Material;

		// Token: 0x04001630 RID: 5680
		private const int kMaxIterations = 16;

		// Token: 0x04001631 RID: 5681
		private RenderTexture[] m_blurBuffer1 = new RenderTexture[16];

		// Token: 0x04001632 RID: 5682
		private RenderTexture[] m_blurBuffer2 = new RenderTexture[16];

		// Token: 0x04001633 RID: 5683
		private int m_Threshold;

		// Token: 0x04001634 RID: 5684
		private int m_Curve;

		// Token: 0x04001635 RID: 5685
		private int m_PrefilterOffs;

		// Token: 0x04001636 RID: 5686
		private int m_SampleScale;

		// Token: 0x04001637 RID: 5687
		private int m_Intensity;

		// Token: 0x04001638 RID: 5688
		private int m_DirtTex;

		// Token: 0x04001639 RID: 5689
		private int m_DirtIntensity;

		// Token: 0x0400163A RID: 5690
		private int m_BaseTex;

		// Token: 0x02000438 RID: 1080
		[Serializable]
		public struct Settings
		{
			// Token: 0x17000132 RID: 306
			// (get) Token: 0x060013CA RID: 5066 RVA: 0x0007AE32 File Offset: 0x00079232
			// (set) Token: 0x060013C9 RID: 5065 RVA: 0x0007AE29 File Offset: 0x00079229
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

			// Token: 0x17000133 RID: 307
			// (get) Token: 0x060013CC RID: 5068 RVA: 0x0007AE52 File Offset: 0x00079252
			// (set) Token: 0x060013CB RID: 5067 RVA: 0x0007AE44 File Offset: 0x00079244
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

			// Token: 0x17000134 RID: 308
			// (get) Token: 0x060013CD RID: 5069 RVA: 0x0007AE60 File Offset: 0x00079260
			public static Bloom.Settings defaultSettings
			{
				get
				{
					return new Bloom.Settings
					{
						threshold = 0.9f,
						softKnee = 0.5f,
						radius = 2f,
						intensity = 0.7f,
						highQuality = true,
						antiFlicker = false,
						dirtTexture = null,
						dirtIntensity = 2.5f
					};
				}
			}

			// Token: 0x0400163B RID: 5691
			[SerializeField]
			[Tooltip("Filters out pixels under this level of brightness.")]
			public float threshold;

			// Token: 0x0400163C RID: 5692
			[SerializeField]
			[Range(0f, 1f)]
			[Tooltip("Makes transition between under/over-threshold gradual.")]
			public float softKnee;

			// Token: 0x0400163D RID: 5693
			[SerializeField]
			[Range(1f, 7f)]
			[Tooltip("Changes extent of veiling effects in a screen resolution-independent fashion.")]
			public float radius;

			// Token: 0x0400163E RID: 5694
			[SerializeField]
			[Tooltip("Blend factor of the result image.")]
			public float intensity;

			// Token: 0x0400163F RID: 5695
			[SerializeField]
			[Tooltip("Controls filter quality and buffer resolution.")]
			public bool highQuality;

			// Token: 0x04001640 RID: 5696
			[SerializeField]
			[Tooltip("Reduces flashing noise with an additional filter.")]
			public bool antiFlicker;

			// Token: 0x04001641 RID: 5697
			[Tooltip("Dirtiness texture to add smudges or dust to the lens.")]
			public Texture dirtTexture;

			// Token: 0x04001642 RID: 5698
			[Min(0f)]
			[Tooltip("Amount of lens dirtiness.")]
			public float dirtIntensity;
		}
	}
}
