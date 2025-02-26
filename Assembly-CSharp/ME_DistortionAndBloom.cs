using System;
using UnityEngine;

// Token: 0x02000426 RID: 1062
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("KriptoFX/ME_BloomAndDistortion")]
public class ME_DistortionAndBloom : MonoBehaviour
{
	// Token: 0x17000128 RID: 296
	// (get) Token: 0x0600135C RID: 4956 RVA: 0x00077228 File Offset: 0x00075628
	public Material mat
	{
		get
		{
			if (this.m_Material == null)
			{
				this.m_Material = ME_DistortionAndBloom.CheckShaderAndCreateMaterial(Shader.Find("Hidden/KriptoFX/PostEffects/ME_Bloom"));
			}
			return this.m_Material;
		}
	}

	// Token: 0x17000129 RID: 297
	// (get) Token: 0x0600135D RID: 4957 RVA: 0x00077256 File Offset: 0x00075656
	public Material matAdditive
	{
		get
		{
			if (this.m_MaterialAdditive == null)
			{
				this.m_MaterialAdditive = ME_DistortionAndBloom.CheckShaderAndCreateMaterial(Shader.Find("Hidden/KriptoFX/PostEffects/ME_BloomAdditive"));
				this.m_MaterialAdditive.renderQueue = 3900;
			}
			return this.m_MaterialAdditive;
		}
	}

	// Token: 0x0600135E RID: 4958 RVA: 0x00077294 File Offset: 0x00075694
	public static Material CheckShaderAndCreateMaterial(Shader s)
	{
		if (s == null || !s.isSupported)
		{
			return null;
		}
		return new Material(s)
		{
			hideFlags = HideFlags.DontSave
		};
	}

	// Token: 0x0600135F RID: 4959 RVA: 0x000772CC File Offset: 0x000756CC
	private void OnDisable()
	{
		if (this.m_Material != null)
		{
			UnityEngine.Object.DestroyImmediate(this.m_Material);
		}
		this.m_Material = null;
		if (this.m_MaterialAdditive != null)
		{
			UnityEngine.Object.DestroyImmediate(this.m_MaterialAdditive);
		}
		this.m_MaterialAdditive = null;
		if (this.tempGO != null)
		{
			UnityEngine.Object.DestroyImmediate(this.tempGO);
		}
		Shader.DisableKeyword("DISTORT_OFF");
		Shader.DisableKeyword("_MOBILEDEPTH_ON");
	}

	// Token: 0x06001360 RID: 4960 RVA: 0x0007734F File Offset: 0x0007574F
	private void Start()
	{
		this.InitializeRenderTarget();
	}

	// Token: 0x06001361 RID: 4961 RVA: 0x00077358 File Offset: 0x00075758
	private void LateUpdate()
	{
		if (this.previuosFrameWidth != Screen.width || this.previuosFrameHeight != Screen.height || Mathf.Abs(this.previousScale - this.RenderTextureResolutoinFactor) > 0.01f)
		{
			this.InitializeRenderTarget();
			this.previuosFrameWidth = Screen.width;
			this.previuosFrameHeight = Screen.height;
			this.previousScale = this.RenderTextureResolutoinFactor;
		}
		Shader.EnableKeyword("DISTORT_OFF");
		Shader.EnableKeyword("_MOBILEDEPTH_ON");
		this.GrabImage();
		if (this.UseBloom && this.HDRSupported)
		{
			this.UpdateBloom();
		}
		Shader.SetGlobalTexture("_GrabTexture", this.source);
		Shader.SetGlobalTexture("_GrabTextureMobile", this.source);
		Shader.SetGlobalTexture("_CameraDepthTexture", this.depth);
		Shader.SetGlobalFloat("_GrabTextureScale", this.RenderTextureResolutoinFactor);
		Shader.SetGlobalFloat("_GrabTextureMobileScale", this.RenderTextureResolutoinFactor);
		Shader.DisableKeyword("DISTORT_OFF");
	}

	// Token: 0x06001362 RID: 4962 RVA: 0x00077459 File Offset: 0x00075859
	private void OnPostRender()
	{
		Graphics.Blit(this.destination, null, this.matAdditive);
	}

	// Token: 0x06001363 RID: 4963 RVA: 0x00077470 File Offset: 0x00075870
	private void InitializeRenderTarget()
	{
		int num = (int)((float)Screen.width * this.RenderTextureResolutoinFactor);
		int num2 = (int)((float)Screen.height * this.RenderTextureResolutoinFactor);
		RenderTextureFormat format = RenderTextureFormat.RGB111110Float;
		if (SystemInfo.SupportsRenderTextureFormat(format))
		{
			this.source = new RenderTexture(num, num2, 0, format);
			this.depth = new RenderTexture(num, num2, 8, RenderTextureFormat.Depth);
			this.HDRSupported = true;
			if (this.UseBloom)
			{
				this.destination = new RenderTexture(((double)this.RenderTextureResolutoinFactor <= 0.99) ? (num / 2) : num, ((double)this.RenderTextureResolutoinFactor <= 0.99) ? (num2 / 2) : num2, 0, format);
			}
		}
		else
		{
			this.HDRSupported = false;
			this.source = new RenderTexture(num, num2, 0, RenderTextureFormat.RGB565);
			this.depth = new RenderTexture(num, num2, 8, RenderTextureFormat.Depth);
		}
	}

	// Token: 0x06001364 RID: 4964 RVA: 0x0007754C File Offset: 0x0007594C
	private void UpdateBloom()
	{
		bool isMobilePlatform = Application.isMobilePlatform;
		if (this.source == null)
		{
			return;
		}
		int num = this.source.width;
		int num2 = this.source.height;
		if (!this.HighQuality)
		{
			num /= 2;
			num2 /= 2;
		}
		RenderTextureFormat format = (!isMobilePlatform) ? RenderTextureFormat.DefaultHDR : RenderTextureFormat.Default;
		float num3 = Mathf.Log((float)num2, 2f) + this.Radius - 8f;
		int num4 = (int)num3;
		int num5 = Mathf.Clamp(num4, 1, 16);
		float num6 = Mathf.GammaToLinearSpace(this.Threshold);
		this.mat.SetFloat("_Threshold", num6);
		float num7 = num6 * this.SoftKnee + 1E-05f;
		Vector3 v = new Vector3(num6 - num7, num7 * 2f, 0.25f / num7);
		this.mat.SetVector("_Curve", v);
		bool flag = !this.HighQuality && this.AntiFlicker;
		this.mat.SetFloat("_PrefilterOffs", (!flag) ? 0f : -0.5f);
		this.mat.SetFloat("_SampleScale", 0.5f + num3 - (float)num4);
		this.mat.SetFloat("_Intensity", Mathf.Max(0f, this.Intensity));
		RenderTexture temporary = RenderTexture.GetTemporary(num, num2, 0, format);
		Graphics.Blit(this.source, temporary, this.mat, (!this.AntiFlicker) ? 0 : 1);
		RenderTexture renderTexture = temporary;
		for (int i = 0; i < num5; i++)
		{
			this.m_blurBuffer1[i] = RenderTexture.GetTemporary(renderTexture.width / 2, renderTexture.height / 2, 0, format);
			Graphics.Blit(renderTexture, this.m_blurBuffer1[i], this.mat, (i != 0) ? 4 : ((!this.AntiFlicker) ? 2 : 3));
			renderTexture = this.m_blurBuffer1[i];
		}
		for (int j = num5 - 2; j >= 0; j--)
		{
			RenderTexture renderTexture2 = this.m_blurBuffer1[j];
			this.mat.SetTexture("_BaseTex", renderTexture2);
			this.m_blurBuffer2[j] = RenderTexture.GetTemporary(renderTexture2.width, renderTexture2.height, 0, format);
			Graphics.Blit(renderTexture, this.m_blurBuffer2[j], this.mat, (!this.HighQuality) ? 5 : 6);
			renderTexture = this.m_blurBuffer2[j];
		}
		this.destination.DiscardContents();
		Graphics.Blit(renderTexture, this.destination, this.mat, (!this.HighQuality) ? 7 : 8);
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

	// Token: 0x06001365 RID: 4965 RVA: 0x0007788C File Offset: 0x00075C8C
	private void GrabImage()
	{
		Camera camera = Camera.current;
		if (camera == null)
		{
			camera = Camera.main;
		}
		if (this.tempGO == null)
		{
			this.tempGO = new GameObject();
			this.tempGO.hideFlags = HideFlags.HideAndDontSave;
			this.tempGO.name = "MobileCamera(Distort_Bloom_Depth)";
			this.addCamera = this.tempGO.AddComponent<Camera>();
			this.addCamera.enabled = false;
			this.addCamera.cullingMask = (~(1 << LayerMask.NameToLayer("CustomPostEffectIgnore")) & this.CullingMask);
		}
		else
		{
			this.addCamera = this.tempGO.GetComponent<Camera>();
		}
		this.addCamera.CopyFrom(camera);
		this.addCamera.SetTargetBuffers(this.source.colorBuffer, this.depth.depthBuffer);
		this.addCamera.depth -= 1f;
		this.addCamera.cullingMask = (~(1 << LayerMask.NameToLayer("CustomPostEffectIgnore")) & this.CullingMask);
		this.addCamera.Render();
	}

	// Token: 0x040015A7 RID: 5543
	public LayerMask CullingMask = -1;

	// Token: 0x040015A8 RID: 5544
	[Range(0.05f, 1f)]
	[Tooltip("Camera render texture resolution")]
	public float RenderTextureResolutoinFactor = 0.25f;

	// Token: 0x040015A9 RID: 5545
	public bool UseBloom = true;

	// Token: 0x040015AA RID: 5546
	[Range(0.1f, 3f)]
	[Tooltip("Filters out pixels under this level of brightness.")]
	public float Threshold = 1.2f;

	// Token: 0x040015AB RID: 5547
	[SerializeField]
	[Range(0f, 1f)]
	[Tooltip("Makes transition between under/over-threshold gradual.")]
	public float SoftKnee;

	// Token: 0x040015AC RID: 5548
	[Range(1f, 7f)]
	[Tooltip("Changes extent of veiling effects in A screen resolution-independent fashion.")]
	public float Radius = 7f;

	// Token: 0x040015AD RID: 5549
	[Tooltip("Blend factor of the result image.")]
	public float Intensity = 0.5f;

	// Token: 0x040015AE RID: 5550
	[Tooltip("Controls filter quality and buffer resolution.")]
	public bool HighQuality;

	// Token: 0x040015AF RID: 5551
	[Tooltip("Reduces flashing noise with an additional filter.")]
	public bool AntiFlicker;

	// Token: 0x040015B0 RID: 5552
	private const string shaderName = "Hidden/KriptoFX/PostEffects/ME_Bloom";

	// Token: 0x040015B1 RID: 5553
	private const string shaderAdditiveName = "Hidden/KriptoFX/PostEffects/ME_BloomAdditive";

	// Token: 0x040015B2 RID: 5554
	private const string cameraName = "MobileCamera(Distort_Bloom_Depth)";

	// Token: 0x040015B3 RID: 5555
	private RenderTexture source;

	// Token: 0x040015B4 RID: 5556
	private RenderTexture depth;

	// Token: 0x040015B5 RID: 5557
	private RenderTexture destination;

	// Token: 0x040015B6 RID: 5558
	private int previuosFrameWidth;

	// Token: 0x040015B7 RID: 5559
	private int previuosFrameHeight;

	// Token: 0x040015B8 RID: 5560
	private float previousScale;

	// Token: 0x040015B9 RID: 5561
	private Camera addCamera;

	// Token: 0x040015BA RID: 5562
	private GameObject tempGO;

	// Token: 0x040015BB RID: 5563
	private bool HDRSupported;

	// Token: 0x040015BC RID: 5564
	private Material m_Material;

	// Token: 0x040015BD RID: 5565
	private Material m_MaterialAdditive;

	// Token: 0x040015BE RID: 5566
	private const int kMaxIterations = 16;

	// Token: 0x040015BF RID: 5567
	private readonly RenderTexture[] m_blurBuffer1 = new RenderTexture[16];

	// Token: 0x040015C0 RID: 5568
	private readonly RenderTexture[] m_blurBuffer2 = new RenderTexture[16];
}
