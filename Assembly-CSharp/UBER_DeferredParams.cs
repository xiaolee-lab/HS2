using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x02000612 RID: 1554
[AddComponentMenu("UBER/Deferred Params")]
[RequireComponent(typeof(Camera))]
[DisallowMultipleComponent]
[ExecuteInEditMode]
public class UBER_DeferredParams : MonoBehaviour
{
	// Token: 0x06002510 RID: 9488 RVA: 0x000D299A File Offset: 0x000D0D9A
	private void Start()
	{
		this.SetupTranslucencyValues();
	}

	// Token: 0x06002511 RID: 9489 RVA: 0x000D29A2 File Offset: 0x000D0DA2
	public void OnValidate()
	{
		this.SetupTranslucencyValues();
	}

	// Token: 0x06002512 RID: 9490 RVA: 0x000D29AC File Offset: 0x000D0DAC
	public void SetupTranslucencyValues()
	{
		if (this.TranslucencyPropsTex == null)
		{
			this.TranslucencyPropsTex = new Texture2D(4, 3, TextureFormat.RGBAFloat, false, true);
			this.TranslucencyPropsTex.anisoLevel = 0;
			this.TranslucencyPropsTex.filterMode = FilterMode.Point;
			this.TranslucencyPropsTex.wrapMode = TextureWrapMode.Clamp;
			this.TranslucencyPropsTex.hideFlags = HideFlags.HideAndDontSave;
		}
		Shader.SetGlobalTexture("_UBERTranslucencySetup", this.TranslucencyPropsTex);
		byte[] array = new byte[192];
		this.EncodeRGBAFloatTo16Bytes(this.TranslucencyColor1.r, this.TranslucencyColor1.g, this.TranslucencyColor1.b, this.Strength1, array, 0, 0);
		this.EncodeRGBAFloatTo16Bytes(this.PointLightsDirectionality1, this.Constant1, this.Scattering1, this.SpotExponent1, array, 0, 1);
		this.EncodeRGBAFloatTo16Bytes(this.SuppressShadows1, this.NdotLReduction1, 1f, 1f, array, 0, 2);
		this.EncodeRGBAFloatTo16Bytes(this.TranslucencyColor2.r, this.TranslucencyColor2.g, this.TranslucencyColor2.b, this.Strength2, array, 1, 0);
		this.EncodeRGBAFloatTo16Bytes(this.PointLightsDirectionality2, this.Constant2, this.Scattering2, this.SpotExponent2, array, 1, 1);
		this.EncodeRGBAFloatTo16Bytes(this.SuppressShadows2, this.NdotLReduction2, 1f, 1f, array, 1, 2);
		this.EncodeRGBAFloatTo16Bytes(this.TranslucencyColor3.r, this.TranslucencyColor3.g, this.TranslucencyColor3.b, this.Strength3, array, 2, 0);
		this.EncodeRGBAFloatTo16Bytes(this.PointLightsDirectionality3, this.Constant3, this.Scattering3, this.SpotExponent3, array, 2, 1);
		this.EncodeRGBAFloatTo16Bytes(this.SuppressShadows3, this.NdotLReduction3, 1f, 1f, array, 2, 2);
		this.EncodeRGBAFloatTo16Bytes(this.TranslucencyColor4.r, this.TranslucencyColor4.g, this.TranslucencyColor4.b, this.Strength4, array, 3, 0);
		this.EncodeRGBAFloatTo16Bytes(this.PointLightsDirectionality4, this.Constant4, this.Scattering4, this.SpotExponent4, array, 3, 1);
		this.EncodeRGBAFloatTo16Bytes(this.SuppressShadows4, this.NdotLReduction4, 1f, 1f, array, 3, 2);
		this.TranslucencyPropsTex.LoadRawTextureData(array);
		this.TranslucencyPropsTex.Apply();
	}

	// Token: 0x06002513 RID: 9491 RVA: 0x000D2C00 File Offset: 0x000D1000
	private void EncodeRGBAFloatTo16Bytes(float r, float g, float b, float a, byte[] rawTexdata, int idx_u, int idx_v)
	{
		int num = idx_v * 4 * 16 + idx_u * 16;
		UBER_RGBA_ByteArray uber_RGBA_ByteArray = default(UBER_RGBA_ByteArray);
		uber_RGBA_ByteArray.R = r;
		uber_RGBA_ByteArray.G = g;
		uber_RGBA_ByteArray.B = b;
		uber_RGBA_ByteArray.A = a;
		rawTexdata[num++] = uber_RGBA_ByteArray.Byte0;
		rawTexdata[num++] = uber_RGBA_ByteArray.Byte1;
		rawTexdata[num++] = uber_RGBA_ByteArray.Byte2;
		rawTexdata[num++] = uber_RGBA_ByteArray.Byte3;
		rawTexdata[num++] = uber_RGBA_ByteArray.Byte4;
		rawTexdata[num++] = uber_RGBA_ByteArray.Byte5;
		rawTexdata[num++] = uber_RGBA_ByteArray.Byte6;
		rawTexdata[num++] = uber_RGBA_ByteArray.Byte7;
		rawTexdata[num++] = uber_RGBA_ByteArray.Byte8;
		rawTexdata[num++] = uber_RGBA_ByteArray.Byte9;
		rawTexdata[num++] = uber_RGBA_ByteArray.Byte10;
		rawTexdata[num++] = uber_RGBA_ByteArray.Byte11;
		rawTexdata[num++] = uber_RGBA_ByteArray.Byte12;
		rawTexdata[num++] = uber_RGBA_ByteArray.Byte13;
		rawTexdata[num++] = uber_RGBA_ByteArray.Byte14;
		rawTexdata[num++] = uber_RGBA_ByteArray.Byte15;
	}

	// Token: 0x06002514 RID: 9492 RVA: 0x000D2D34 File Offset: 0x000D1134
	public void OnEnable()
	{
		this.SetupTranslucencyValues();
		if (this.NotifyDecals())
		{
			return;
		}
		if (this.mycam == null)
		{
			this.mycam = base.GetComponent<Camera>();
			if (this.mycam == null)
			{
				return;
			}
		}
		this.Initialize();
		Camera.onPreRender = (Camera.CameraCallback)Delegate.Combine(Camera.onPreRender, new Camera.CameraCallback(this.SetupCam));
	}

	// Token: 0x06002515 RID: 9493 RVA: 0x000D2DA8 File Offset: 0x000D11A8
	public void OnDisable()
	{
		this.NotifyDecals();
		this.Cleanup();
	}

	// Token: 0x06002516 RID: 9494 RVA: 0x000D2DB7 File Offset: 0x000D11B7
	public void OnDestroy()
	{
		this.NotifyDecals();
		this.Cleanup();
	}

	// Token: 0x06002517 RID: 9495 RVA: 0x000D2DC8 File Offset: 0x000D11C8
	private bool NotifyDecals()
	{
		Type type = Type.GetType("UBERDecalSystem.DecalManager");
		if (type != null)
		{
			bool flag = UnityEngine.Object.FindObjectOfType(type) != null && UnityEngine.Object.FindObjectOfType(type) is MonoBehaviour && (UnityEngine.Object.FindObjectOfType(type) as MonoBehaviour).enabled;
			if (flag)
			{
				(UnityEngine.Object.FindObjectOfType(type) as MonoBehaviour).Invoke("OnDisable", 0f);
				(UnityEngine.Object.FindObjectOfType(type) as MonoBehaviour).Invoke("OnEnable", 0f);
				return true;
			}
		}
		return false;
	}

	// Token: 0x06002518 RID: 9496 RVA: 0x000D2E60 File Offset: 0x000D1260
	private void Cleanup()
	{
		if (this.TranslucencyPropsTex)
		{
			UnityEngine.Object.DestroyImmediate(this.TranslucencyPropsTex);
			this.TranslucencyPropsTex = null;
		}
		if (this.combufPreLight != null)
		{
			if (this.mycam)
			{
				this.mycam.RemoveCommandBuffer(CameraEvent.BeforeReflections, this.combufPreLight);
				this.mycam.RemoveCommandBuffer(CameraEvent.AfterLighting, this.combufPostLight);
			}
			foreach (Camera camera in this.sceneCamsWithBuffer)
			{
				if (camera)
				{
					camera.RemoveCommandBuffer(CameraEvent.BeforeReflections, this.combufPreLight);
					camera.RemoveCommandBuffer(CameraEvent.AfterLighting, this.combufPostLight);
				}
			}
		}
		this.sceneCamsWithBuffer.Clear();
		Camera.onPreRender = (Camera.CameraCallback)Delegate.Remove(Camera.onPreRender, new Camera.CameraCallback(this.SetupCam));
	}

	// Token: 0x06002519 RID: 9497 RVA: 0x000D2F68 File Offset: 0x000D1368
	private void SetupCam(Camera cam)
	{
		bool flag = false;
		if (cam == this.mycam || flag)
		{
			this.RefreshComBufs(cam, flag);
		}
	}

	// Token: 0x0600251A RID: 9498 RVA: 0x000D2F98 File Offset: 0x000D1398
	public void RefreshComBufs(Camera cam, bool isSceneCam)
	{
		if (cam && this.combufPreLight != null && this.combufPostLight != null)
		{
			CommandBuffer[] commandBuffers = cam.GetCommandBuffers(CameraEvent.BeforeReflections);
			bool flag = false;
			foreach (CommandBuffer commandBuffer in commandBuffers)
			{
				if (commandBuffer.name == this.combufPreLight.name)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				cam.AddCommandBuffer(CameraEvent.BeforeReflections, this.combufPreLight);
				cam.AddCommandBuffer(CameraEvent.AfterLighting, this.combufPostLight);
				if (isSceneCam)
				{
					this.sceneCamsWithBuffer.Add(cam);
				}
			}
		}
	}

	// Token: 0x0600251B RID: 9499 RVA: 0x000D3048 File Offset: 0x000D1448
	public void Initialize()
	{
		if (this.combufPreLight == null)
		{
			int nameID = Shader.PropertyToID("_UBERPropsBuffer");
			if (this.CopyPropsMat == null)
			{
				if (this.CopyPropsMat != null)
				{
					UnityEngine.Object.DestroyImmediate(this.CopyPropsMat);
				}
				this.CopyPropsMat = new Material(Shader.Find("Hidden/UBER_CopyPropsTexture"));
				this.CopyPropsMat.hideFlags = HideFlags.DontSave;
			}
			this.combufPreLight = new CommandBuffer();
			this.combufPreLight.name = "UBERPropsPrelight";
			this.combufPreLight.GetTemporaryRT(nameID, -1, -1, 0, FilterMode.Point, RenderTextureFormat.RHalf);
			this.combufPreLight.Blit(BuiltinRenderTextureType.CameraTarget, nameID, this.CopyPropsMat);
			this.combufPostLight = new CommandBuffer();
			this.combufPostLight.name = "UBERPropsPostlight";
			this.combufPostLight.ReleaseTemporaryRT(nameID);
		}
	}

	// Token: 0x0400244E RID: 9294
	[Header("Translucency setup 1")]
	[ColorUsage(false)]
	public Color TranslucencyColor1 = new Color(1f, 1f, 1f, 1f);

	// Token: 0x0400244F RID: 9295
	[Tooltip("You can control strength per light using its color alpha (first enable in UBER config file)")]
	public float Strength1 = 4f;

	// Token: 0x04002450 RID: 9296
	[Range(0f, 1f)]
	public float PointLightsDirectionality1 = 0.7f;

	// Token: 0x04002451 RID: 9297
	[Range(0f, 0.5f)]
	public float Constant1 = 0.1f;

	// Token: 0x04002452 RID: 9298
	[Range(0f, 0.3f)]
	public float Scattering1 = 0.05f;

	// Token: 0x04002453 RID: 9299
	[Range(0f, 100f)]
	public float SpotExponent1 = 30f;

	// Token: 0x04002454 RID: 9300
	[Range(0f, 20f)]
	public float SuppressShadows1 = 0.5f;

	// Token: 0x04002455 RID: 9301
	[Range(0f, 1f)]
	public float NdotLReduction1;

	// Token: 0x04002456 RID: 9302
	[Space]
	[Header("Translucency setup 2")]
	[ColorUsage(false)]
	public Color TranslucencyColor2 = new Color(1f, 1f, 1f, 1f);

	// Token: 0x04002457 RID: 9303
	[Tooltip("You can control strength per light using its color alpha (first enable in UBER config file)")]
	public float Strength2 = 4f;

	// Token: 0x04002458 RID: 9304
	[Range(0f, 1f)]
	public float PointLightsDirectionality2 = 0.7f;

	// Token: 0x04002459 RID: 9305
	[Range(0f, 0.5f)]
	public float Constant2 = 0.1f;

	// Token: 0x0400245A RID: 9306
	[Range(0f, 0.3f)]
	public float Scattering2 = 0.05f;

	// Token: 0x0400245B RID: 9307
	[Range(0f, 100f)]
	public float SpotExponent2 = 30f;

	// Token: 0x0400245C RID: 9308
	[Range(0f, 20f)]
	public float SuppressShadows2 = 0.5f;

	// Token: 0x0400245D RID: 9309
	[Range(0f, 1f)]
	public float NdotLReduction2;

	// Token: 0x0400245E RID: 9310
	[Space]
	[Header("Translucency setup 3")]
	[ColorUsage(false)]
	public Color TranslucencyColor3 = new Color(1f, 1f, 1f, 1f);

	// Token: 0x0400245F RID: 9311
	[Tooltip("You can control strength per light using its color alpha (first enable in UBER config file)")]
	public float Strength3 = 4f;

	// Token: 0x04002460 RID: 9312
	[Range(0f, 1f)]
	public float PointLightsDirectionality3 = 0.7f;

	// Token: 0x04002461 RID: 9313
	[Range(0f, 0.5f)]
	public float Constant3 = 0.1f;

	// Token: 0x04002462 RID: 9314
	[Range(0f, 0.3f)]
	public float Scattering3 = 0.05f;

	// Token: 0x04002463 RID: 9315
	[Range(0f, 100f)]
	public float SpotExponent3 = 30f;

	// Token: 0x04002464 RID: 9316
	[Range(0f, 20f)]
	public float SuppressShadows3 = 0.5f;

	// Token: 0x04002465 RID: 9317
	[Range(0f, 1f)]
	public float NdotLReduction3;

	// Token: 0x04002466 RID: 9318
	[Space]
	[Header("Translucency setup 4")]
	[ColorUsage(false)]
	public Color TranslucencyColor4 = new Color(1f, 1f, 1f, 1f);

	// Token: 0x04002467 RID: 9319
	[Tooltip("You can control strength per light using its color alpha (first enable in UBER config file)")]
	public float Strength4 = 4f;

	// Token: 0x04002468 RID: 9320
	[Range(0f, 1f)]
	public float PointLightsDirectionality4 = 0.7f;

	// Token: 0x04002469 RID: 9321
	[Range(0f, 0.5f)]
	public float Constant4 = 0.1f;

	// Token: 0x0400246A RID: 9322
	[Range(0f, 0.3f)]
	public float Scattering4 = 0.05f;

	// Token: 0x0400246B RID: 9323
	[Range(0f, 100f)]
	public float SpotExponent4 = 30f;

	// Token: 0x0400246C RID: 9324
	[Range(0f, 20f)]
	public float SuppressShadows4 = 0.5f;

	// Token: 0x0400246D RID: 9325
	[Range(0f, 1f)]
	public float NdotLReduction4;

	// Token: 0x0400246E RID: 9326
	private Camera mycam;

	// Token: 0x0400246F RID: 9327
	private CommandBuffer combufPreLight;

	// Token: 0x04002470 RID: 9328
	private CommandBuffer combufPostLight;

	// Token: 0x04002471 RID: 9329
	private Material CopyPropsMat;

	// Token: 0x04002472 RID: 9330
	private bool UBERPresenceChecked;

	// Token: 0x04002473 RID: 9331
	private bool UBERPresent;

	// Token: 0x04002474 RID: 9332
	[HideInInspector]
	public Texture2D TranslucencyPropsTex;

	// Token: 0x04002475 RID: 9333
	private HashSet<Camera> sceneCamsWithBuffer = new HashSet<Camera>();
}
