using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR;

// Token: 0x020004CE RID: 1230
[AddComponentMenu("")]
public class AmplifyOcclusionBase : MonoBehaviour
{
	// Token: 0x17000152 RID: 338
	// (get) Token: 0x060016B9 RID: 5817 RVA: 0x0008BE8A File Offset: 0x0008A28A
	private bool UsingTemporalFilter
	{
		get
		{
			return this.m_sampleStep > 0U && this.FilterEnabled && this.m_targetCamera.cameraType != CameraType.SceneView;
		}
	}

	// Token: 0x17000153 RID: 339
	// (get) Token: 0x060016BA RID: 5818 RVA: 0x0008BEB7 File Offset: 0x0008A2B7
	private bool UsingMotionVectors
	{
		get
		{
			return this.UsingTemporalFilter && this.ApplyMethod != AmplifyOcclusionBase.ApplicationMethod.Deferred;
		}
	}

	// Token: 0x060016BB RID: 5819 RVA: 0x0008BED4 File Offset: 0x0008A2D4
	private void createCommandBuffer(ref AmplifyOcclusionBase.CmdBuffer aCmdBuffer, string aCmdBufferName, CameraEvent aCameraEvent)
	{
		if (aCmdBuffer.cmdBuffer != null)
		{
			this.cleanupCommandBuffer(ref aCmdBuffer);
		}
		aCmdBuffer.cmdBufferName = aCmdBufferName;
		aCmdBuffer.cmdBuffer = new CommandBuffer();
		aCmdBuffer.cmdBuffer.name = aCmdBufferName;
		aCmdBuffer.cmdBufferEvent = aCameraEvent;
		this.m_targetCamera.AddCommandBuffer(aCameraEvent, aCmdBuffer.cmdBuffer);
	}

	// Token: 0x060016BC RID: 5820 RVA: 0x0008BF2C File Offset: 0x0008A32C
	private void cleanupCommandBuffer(ref AmplifyOcclusionBase.CmdBuffer aCmdBuffer)
	{
		CommandBuffer[] commandBuffers = this.m_targetCamera.GetCommandBuffers(aCmdBuffer.cmdBufferEvent);
		for (int i = 0; i < commandBuffers.Length; i++)
		{
			if (commandBuffers[i].name == aCmdBuffer.cmdBufferName)
			{
				this.m_targetCamera.RemoveCommandBuffer(aCmdBuffer.cmdBufferEvent, commandBuffers[i]);
			}
		}
		aCmdBuffer.cmdBufferName = null;
		aCmdBuffer.cmdBufferEvent = CameraEvent.BeforeDepthTexture;
		aCmdBuffer.cmdBuffer = null;
	}

	// Token: 0x060016BD RID: 5821 RVA: 0x0008BFA0 File Offset: 0x0008A3A0
	private void createQuadMesh()
	{
		if (AmplifyOcclusionBase.m_quadMesh == null)
		{
			AmplifyOcclusionBase.m_quadMesh = new Mesh();
			AmplifyOcclusionBase.m_quadMesh.vertices = new Vector3[]
			{
				new Vector3(0f, 0f, 0f),
				new Vector3(0f, 1f, 0f),
				new Vector3(1f, 1f, 0f),
				new Vector3(1f, 0f, 0f)
			};
			AmplifyOcclusionBase.m_quadMesh.uv = new Vector2[]
			{
				new Vector2(0f, 0f),
				new Vector2(0f, 1f),
				new Vector2(1f, 1f),
				new Vector2(1f, 0f)
			};
			AmplifyOcclusionBase.m_quadMesh.triangles = new int[]
			{
				0,
				1,
				2,
				0,
				2,
				3
			};
			AmplifyOcclusionBase.m_quadMesh.normals = new Vector3[0];
			AmplifyOcclusionBase.m_quadMesh.tangents = new Vector4[0];
			AmplifyOcclusionBase.m_quadMesh.colors32 = new Color32[0];
			AmplifyOcclusionBase.m_quadMesh.colors = new Color[0];
		}
	}

	// Token: 0x060016BE RID: 5822 RVA: 0x0008C12E File Offset: 0x0008A52E
	private void PerformBlit(CommandBuffer cb, Material mat, int pass)
	{
		cb.DrawMesh(AmplifyOcclusionBase.m_quadMesh, Matrix4x4.identity, mat, 0, pass);
	}

	// Token: 0x060016BF RID: 5823 RVA: 0x0008C144 File Offset: 0x0008A544
	private Material createMaterialWithShaderName(string aShaderName, bool aThroughErrorMsg)
	{
		Shader shader = Shader.Find(aShaderName);
		if (shader == null)
		{
			if (aThroughErrorMsg)
			{
			}
			return null;
		}
		return new Material(shader)
		{
			hideFlags = HideFlags.DontSave
		};
	}

	// Token: 0x060016C0 RID: 5824 RVA: 0x0008C17C File Offset: 0x0008A57C
	private void checkMaterials(bool aThroughErrorMsg)
	{
		if (AmplifyOcclusionBase.m_occlusionMat == null)
		{
			AmplifyOcclusionBase.m_occlusionMat = this.createMaterialWithShaderName("Hidden/Amplify Occlusion/Occlusion", aThroughErrorMsg);
		}
		if (AmplifyOcclusionBase.m_blurMat == null)
		{
			AmplifyOcclusionBase.m_blurMat = this.createMaterialWithShaderName("Hidden/Amplify Occlusion/Blur", aThroughErrorMsg);
		}
		if (AmplifyOcclusionBase.m_applyOcclusionMat == null)
		{
			AmplifyOcclusionBase.m_applyOcclusionMat = this.createMaterialWithShaderName("Hidden/Amplify Occlusion/Apply", aThroughErrorMsg);
			if (AmplifyOcclusionBase.m_applyOcclusionMat != null)
			{
				this.useMRTBlendingFallback = (AmplifyOcclusionBase.m_applyOcclusionMat.GetTag("MRTBlending", false).ToUpper() != "TRUE");
			}
		}
	}

	// Token: 0x060016C1 RID: 5825 RVA: 0x0008C224 File Offset: 0x0008A624
	private bool checkRenderTextureFormats()
	{
		if (SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGB32) && SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGBHalf))
		{
			this.m_occlusionRTFormat = RenderTextureFormat.RGHalf;
			if (!SystemInfo.SupportsRenderTextureFormat(this.m_occlusionRTFormat))
			{
				this.m_occlusionRTFormat = RenderTextureFormat.RGFloat;
				if (!SystemInfo.SupportsRenderTextureFormat(this.m_occlusionRTFormat))
				{
					this.m_occlusionRTFormat = RenderTextureFormat.ARGBHalf;
				}
			}
			return true;
		}
		return false;
	}

	// Token: 0x060016C2 RID: 5826 RVA: 0x0008C284 File Offset: 0x0008A684
	private void OnEnable()
	{
		this.m_myID = AmplifyOcclusionBase.m_nextID;
		this.m_myIDstring = this.m_myID.ToString();
		AmplifyOcclusionBase.m_nextID++;
		if (!this.checkRenderTextureFormats())
		{
			base.enabled = false;
			return;
		}
		this.checkMaterials(false);
		this.createQuadMesh();
	}

	// Token: 0x060016C3 RID: 5827 RVA: 0x0008C2E0 File Offset: 0x0008A6E0
	private void Reset()
	{
		if (this.m_commandBuffer_Parameters.cmdBuffer != null)
		{
			this.cleanupCommandBuffer(ref this.m_commandBuffer_Parameters);
		}
		if (this.m_commandBuffer_Occlusion.cmdBuffer != null)
		{
			this.cleanupCommandBuffer(ref this.m_commandBuffer_Occlusion);
		}
		if (this.m_commandBuffer_Apply.cmdBuffer != null)
		{
			this.cleanupCommandBuffer(ref this.m_commandBuffer_Apply);
		}
		this.safeReleaseRT(ref this.m_occlusionDepthRT);
		this.safeReleaseRT(ref this.m_depthMipmap);
		this.releaseTemporalRT();
		this.m_tmpMipString = null;
	}

	// Token: 0x060016C4 RID: 5828 RVA: 0x0008C366 File Offset: 0x0008A766
	private void OnDisable()
	{
		this.Reset();
	}

	// Token: 0x060016C5 RID: 5829 RVA: 0x0008C370 File Offset: 0x0008A770
	private void releaseTemporalRT()
	{
		if (this.m_temporalAccumRT != null)
		{
			for (int i = 0; i < this.m_temporalAccumRT.Length; i++)
			{
				this.safeReleaseRT(ref this.m_temporalAccumRT[i]);
			}
		}
		this.m_temporalAccumRT = null;
	}

	// Token: 0x060016C6 RID: 5830 RVA: 0x0008C3BC File Offset: 0x0008A7BC
	private void ClearHistory(CommandBuffer cb)
	{
		this.m_clearHistory = false;
		if (this.m_temporalAccumRT != null && this.m_occlusionDepthRT != null)
		{
			for (int i = 0; i < this.m_temporalAccumRT.Length; i++)
			{
				cb.SetRenderTarget(this.m_temporalAccumRT[i]);
				this.PerformBlit(cb, AmplifyOcclusionBase.m_occlusionMat, 34);
			}
		}
	}

	// Token: 0x060016C7 RID: 5831 RVA: 0x0008C428 File Offset: 0x0008A828
	private void checkParamsChanged()
	{
		bool allowHDR = this.m_targetCamera.allowHDR;
		bool flag = this.m_targetCamera.allowMSAA && this.m_targetCamera.actualRenderingPath != RenderingPath.DeferredLighting && this.m_targetCamera.actualRenderingPath != RenderingPath.DeferredShading && QualitySettings.antiAliasing >= 1;
		int antiAliasing = (!flag) ? 1 : QualitySettings.antiAliasing;
		if (this.m_occlusionDepthRT != null)
		{
			if (this.m_occlusionDepthRT.width != this.m_target.width || this.m_occlusionDepthRT.height != this.m_target.height || this.m_prevMSAA != flag || !this.m_occlusionDepthRT.IsCreated() || (this.m_temporalAccumRT != null && (!this.m_temporalAccumRT[0].IsCreated() || !this.m_temporalAccumRT[1].IsCreated())))
			{
				this.safeReleaseRT(ref this.m_occlusionDepthRT);
				this.safeReleaseRT(ref this.m_depthMipmap);
				this.releaseTemporalRT();
				this.m_paramsChanged = true;
			}
			else if (this.m_prevFilterEnabled != this.FilterEnabled)
			{
				this.releaseTemporalRT();
			}
		}
		if (this.m_temporalAccumRT != null)
		{
			if (this.isStereoMultiPassEnabled())
			{
				if (this.m_temporalAccumRT.Length != 4)
				{
					this.m_temporalAccumRT = null;
				}
			}
			else if (this.m_temporalAccumRT.Length != 2)
			{
				this.m_temporalAccumRT = null;
			}
		}
		if (this.m_occlusionDepthRT == null)
		{
			this.m_occlusionDepthRT = this.safeAllocateRT("_AO_OcclusionDepthTexture", this.m_target.width, this.m_target.height, this.m_occlusionRTFormat, RenderTextureReadWrite.Linear, FilterMode.Bilinear, 1, false);
		}
		if (this.m_temporalAccumRT == null && this.FilterEnabled)
		{
			if (this.isStereoMultiPassEnabled())
			{
				this.m_temporalAccumRT = new RenderTexture[4];
			}
			else
			{
				this.m_temporalAccumRT = new RenderTexture[2];
			}
			for (int i = 0; i < this.m_temporalAccumRT.Length; i++)
			{
				this.m_temporalAccumRT[i] = this.safeAllocateRT("_AO_TemporalAccum_" + i.ToString(), this.m_target.width, this.m_target.height, this.m_accumTemporalRTFormat, RenderTextureReadWrite.Linear, FilterMode.Bilinear, antiAliasing, false);
			}
			this.m_clearHistory = true;
		}
		if (this.CacheAware && this.m_depthMipmap == null)
		{
			this.m_depthMipmap = this.safeAllocateRT("_AO_DepthMipmap", this.m_target.width >> 1, this.m_target.height >> 1, RenderTextureFormat.RFloat, RenderTextureReadWrite.Linear, FilterMode.Point, 1, true);
			int num = Mathf.Min(this.m_target.width, this.m_target.height);
			this.m_numberMips = (int)(Mathf.Log((float)num, 2f) + 1f) - 1;
			this.m_tmpMipString = new string[this.m_numberMips];
			for (int j = 0; j < this.m_numberMips; j++)
			{
				this.m_tmpMipString[j] = "_AO_TmpMip_" + j.ToString();
			}
		}
		else if (!this.CacheAware && this.m_depthMipmap != null)
		{
			this.safeReleaseRT(ref this.m_depthMipmap);
			this.m_tmpMipString = null;
		}
		if (this.m_prevSampleCount != this.SampleCount || this.m_prevDownsample != this.Downsample || this.m_prevCacheAware != this.CacheAware || this.m_prevBlurEnabled != this.BlurEnabled || ((this.m_prevBlurPasses != this.BlurPasses || this.m_prevBlurRadius != this.BlurRadius) && this.BlurEnabled) || this.m_prevFilterEnabled != this.FilterEnabled || this.m_prevHDR != allowHDR || this.m_prevMSAA != flag)
		{
			this.m_clearHistory |= (this.m_prevHDR != allowHDR);
			this.m_clearHistory |= (this.m_prevMSAA != flag);
			this.m_HDR = allowHDR;
			this.m_MSAA = flag;
			this.m_paramsChanged = true;
		}
	}

	// Token: 0x060016C8 RID: 5832 RVA: 0x0008C878 File Offset: 0x0008AC78
	private void updateParams()
	{
		this.m_prevSampleCount = this.SampleCount;
		this.m_prevDownsample = this.Downsample;
		this.m_prevCacheAware = this.CacheAware;
		this.m_prevBlurEnabled = this.BlurEnabled;
		this.m_prevBlurPasses = this.BlurPasses;
		this.m_prevBlurRadius = this.BlurRadius;
		this.m_prevFilterEnabled = this.FilterEnabled;
		this.m_prevHDR = this.m_HDR;
		this.m_prevMSAA = this.m_MSAA;
		this.m_paramsChanged = false;
	}

	// Token: 0x060016C9 RID: 5833 RVA: 0x0008C8F8 File Offset: 0x0008ACF8
	private void Update()
	{
		if (this.m_targetCamera != null)
		{
			if (this.m_targetCamera.actualRenderingPath != RenderingPath.DeferredShading)
			{
				if (this.PerPixelNormals != AmplifyOcclusionBase.PerPixelNormalSource.None && this.PerPixelNormals != AmplifyOcclusionBase.PerPixelNormalSource.Camera)
				{
					this.m_paramsChanged = true;
					this.PerPixelNormals = AmplifyOcclusionBase.PerPixelNormalSource.Camera;
					UnityEngine.Debug.LogWarning("[AmplifyOcclusion] GBuffer Normals only available in Camera Deferred Shading mode. Switched to Camera source.");
				}
				if (this.ApplyMethod == AmplifyOcclusionBase.ApplicationMethod.Deferred)
				{
					this.m_paramsChanged = true;
					this.ApplyMethod = AmplifyOcclusionBase.ApplicationMethod.PostEffect;
					UnityEngine.Debug.LogWarning("[AmplifyOcclusion] Deferred Method requires a Deferred Shading path. Switching to Post Effect Method.");
				}
			}
			else if (this.PerPixelNormals == AmplifyOcclusionBase.PerPixelNormalSource.Camera)
			{
				this.m_paramsChanged = true;
				this.PerPixelNormals = AmplifyOcclusionBase.PerPixelNormalSource.GBuffer;
				UnityEngine.Debug.LogWarning("[AmplifyOcclusion] Camera Normals not supported for Deferred Method. Switching to GBuffer Normals.");
			}
			if ((this.m_targetCamera.depthTextureMode & DepthTextureMode.Depth) == DepthTextureMode.None)
			{
				this.m_targetCamera.depthTextureMode |= DepthTextureMode.Depth;
			}
			if (this.PerPixelNormals == AmplifyOcclusionBase.PerPixelNormalSource.Camera && (this.m_targetCamera.depthTextureMode & DepthTextureMode.DepthNormals) == DepthTextureMode.None)
			{
				this.m_targetCamera.depthTextureMode |= DepthTextureMode.DepthNormals;
			}
			if (this.UsingMotionVectors && (this.m_targetCamera.depthTextureMode & DepthTextureMode.MotionVectors) == DepthTextureMode.None)
			{
				this.m_targetCamera.depthTextureMode |= DepthTextureMode.MotionVectors;
			}
		}
		else
		{
			this.m_targetCamera = base.GetComponent<Camera>();
		}
	}

	// Token: 0x060016CA RID: 5834 RVA: 0x0008CA3C File Offset: 0x0008AE3C
	private void OnPreRender()
	{
		this.checkMaterials(true);
		if (this.m_targetCamera != null)
		{
			bool flag = GraphicsSettings.GetShaderMode(BuiltinShaderType.DeferredReflections) != BuiltinShaderMode.Disabled;
			if (this.m_prevPerPixelNormals != this.PerPixelNormals || this.m_prevApplyMethod != this.ApplyMethod || this.m_prevDeferredReflections != flag || this.m_commandBuffer_Parameters.cmdBuffer == null || this.m_commandBuffer_Occlusion.cmdBuffer == null || this.m_commandBuffer_Apply.cmdBuffer == null)
			{
				CameraEvent aCameraEvent = CameraEvent.BeforeImageEffectsOpaque;
				if (this.ApplyMethod == AmplifyOcclusionBase.ApplicationMethod.Deferred)
				{
					aCameraEvent = ((!flag) ? CameraEvent.BeforeLighting : CameraEvent.BeforeReflections);
				}
				this.createCommandBuffer(ref this.m_commandBuffer_Parameters, "AmplifyOcclusion_Parameters_" + this.m_myIDstring, aCameraEvent);
				this.createCommandBuffer(ref this.m_commandBuffer_Occlusion, "AmplifyOcclusion_Compute_" + this.m_myIDstring, aCameraEvent);
				this.createCommandBuffer(ref this.m_commandBuffer_Apply, "AmplifyOcclusion_Apply_" + this.m_myIDstring, aCameraEvent);
				this.m_prevPerPixelNormals = this.PerPixelNormals;
				this.m_prevApplyMethod = this.ApplyMethod;
				this.m_prevDeferredReflections = flag;
				this.m_paramsChanged = true;
			}
			if (this.m_commandBuffer_Parameters.cmdBuffer != null && this.m_commandBuffer_Occlusion.cmdBuffer != null && this.m_commandBuffer_Apply.cmdBuffer != null)
			{
				if (this.isStereoMultiPassEnabled())
				{
					uint num = this.m_sampleStep >> 1 & 1U;
					uint num2 = this.m_sampleStep & 1U;
					this.m_curTemporalIdx = num2 * 2U + num;
					this.m_prevTemporalIdx = num2 * 2U + (1U - num);
				}
				else
				{
					uint num3 = this.m_sampleStep & 1U;
					this.m_curTemporalIdx = num3;
					this.m_prevTemporalIdx = 1U - num3;
				}
				this.m_commandBuffer_Parameters.cmdBuffer.Clear();
				this.UpdateGlobalShaderConstants(this.m_commandBuffer_Parameters.cmdBuffer);
				this.UpdateGlobalShaderConstants_Matrices(this.m_commandBuffer_Parameters.cmdBuffer);
				this.UpdateGlobalShaderConstants_AmbientOcclusion(this.m_commandBuffer_Parameters.cmdBuffer);
				this.checkParamsChanged();
				if (this.m_paramsChanged)
				{
					this.m_commandBuffer_Occlusion.cmdBuffer.Clear();
					this.commandBuffer_FillComputeOcclusion(this.m_commandBuffer_Occlusion.cmdBuffer);
				}
				this.m_commandBuffer_Apply.cmdBuffer.Clear();
				if (this.ApplyMethod == AmplifyOcclusionBase.ApplicationMethod.Debug)
				{
					this.commandBuffer_FillApplyDebug(this.m_commandBuffer_Apply.cmdBuffer);
				}
				else if (this.ApplyMethod == AmplifyOcclusionBase.ApplicationMethod.PostEffect)
				{
					this.commandBuffer_FillApplyPostEffect(this.m_commandBuffer_Apply.cmdBuffer);
				}
				else
				{
					bool logTarget = !this.m_HDR;
					this.commandBuffer_FillApplyDeferred(this.m_commandBuffer_Apply.cmdBuffer, logTarget);
				}
				this.updateParams();
				this.m_sampleStep += 1U;
			}
		}
		else
		{
			this.m_targetCamera = base.GetComponent<Camera>();
			this.Update();
		}
	}

	// Token: 0x060016CB RID: 5835 RVA: 0x0008CD00 File Offset: 0x0008B100
	private void OnPostRender()
	{
		if (this.m_occlusionDepthRT != null)
		{
			this.m_occlusionDepthRT.MarkRestoreExpected();
		}
		if (this.m_temporalAccumRT != null)
		{
			foreach (RenderTexture renderTexture in this.m_temporalAccumRT)
			{
				renderTexture.MarkRestoreExpected();
			}
		}
	}

	// Token: 0x060016CC RID: 5836 RVA: 0x0008CD5C File Offset: 0x0008B15C
	private int safeAllocateTemporaryRT(CommandBuffer cb, string propertyName, int width, int height, RenderTextureFormat format = RenderTextureFormat.Default, RenderTextureReadWrite readWrite = RenderTextureReadWrite.Default, FilterMode filterMode = FilterMode.Point)
	{
		int num = Shader.PropertyToID(propertyName);
		cb.GetTemporaryRT(num, width, height, 0, filterMode, format, readWrite);
		return num;
	}

	// Token: 0x060016CD RID: 5837 RVA: 0x0008CD82 File Offset: 0x0008B182
	private void safeReleaseTemporaryRT(CommandBuffer cb, int id)
	{
		cb.ReleaseTemporaryRT(id);
	}

	// Token: 0x060016CE RID: 5838 RVA: 0x0008CD8C File Offset: 0x0008B18C
	private RenderTexture safeAllocateRT(string name, int width, int height, RenderTextureFormat format, RenderTextureReadWrite readWrite, FilterMode filterMode = FilterMode.Point, int antiAliasing = 1, bool aUseMipMap = false)
	{
		width = Mathf.Clamp(width, 1, 65536);
		height = Mathf.Clamp(height, 1, 65536);
		RenderTexture renderTexture = new RenderTexture(width, height, 0, format, readWrite)
		{
			hideFlags = HideFlags.DontSave
		};
		renderTexture.name = name;
		renderTexture.filterMode = filterMode;
		renderTexture.wrapMode = TextureWrapMode.Clamp;
		renderTexture.antiAliasing = Mathf.Max(antiAliasing, 1);
		renderTexture.useMipMap = aUseMipMap;
		renderTexture.Create();
		return renderTexture;
	}

	// Token: 0x060016CF RID: 5839 RVA: 0x0008CE00 File Offset: 0x0008B200
	private void safeReleaseRT(ref RenderTexture rt)
	{
		if (rt != null)
		{
			RenderTexture.active = null;
			rt.Release();
			UnityEngine.Object.DestroyImmediate(rt);
			rt = null;
		}
	}

	// Token: 0x060016D0 RID: 5840 RVA: 0x0008CE26 File Offset: 0x0008B226
	private void BeginSample(CommandBuffer cb, string name)
	{
		cb.BeginSample(name);
	}

	// Token: 0x060016D1 RID: 5841 RVA: 0x0008CE2F File Offset: 0x0008B22F
	private void EndSample(CommandBuffer cb, string name)
	{
		cb.EndSample(name);
	}

	// Token: 0x060016D2 RID: 5842 RVA: 0x0008CE38 File Offset: 0x0008B238
	private void commandBuffer_FillComputeOcclusion(CommandBuffer cb)
	{
		this.BeginSample(cb, "AO 1 - ComputeOcclusion");
		if (this.PerPixelNormals == AmplifyOcclusionBase.PerPixelNormalSource.GBuffer || this.PerPixelNormals == AmplifyOcclusionBase.PerPixelNormalSource.GBufferOctaEncoded)
		{
			cb.SetGlobalTexture(AmplifyOcclusionBase.PropertyID._AO_GBufferNormals, BuiltinRenderTextureType.GBuffer2);
		}
		Vector4 value = new Vector4(this.m_target.oneOverWidth, this.m_target.oneOverHeight, (float)this.m_target.width, (float)this.m_target.height);
		int num = (int)(this.SampleCount * (AmplifyOcclusionBase.SampleCountLevel)AmplifyOcclusionBase.PerPixelNormalSourceCount);
		int num2 = (int)(num + this.PerPixelNormals);
		if (this.CacheAware)
		{
			num2 += 16;
			int num3 = 0;
			for (int i = 0; i < this.m_numberMips; i++)
			{
				int width = this.m_target.width >> i + 1;
				int height = this.m_target.height >> i + 1;
				int num4 = this.safeAllocateTemporaryRT(cb, this.m_tmpMipString[i], width, height, RenderTextureFormat.RFloat, RenderTextureReadWrite.Linear, FilterMode.Bilinear);
				cb.SetRenderTarget(num4);
				this.PerformBlit(cb, AmplifyOcclusionBase.m_occlusionMat, (i != 0) ? 35 : 36);
				cb.CopyTexture(num4, 0, 0, this.m_depthMipmap, 0, i);
				if (num3 != 0)
				{
					this.safeReleaseTemporaryRT(cb, num3);
				}
				num3 = num4;
				cb.SetGlobalTexture(AmplifyOcclusionBase.PropertyID._AO_Source, num4);
			}
			this.safeReleaseTemporaryRT(cb, num3);
			cb.SetGlobalTexture(AmplifyOcclusionBase.PropertyID._AO_SourceDepthMipmap, this.m_depthMipmap);
		}
		if (this.Downsample)
		{
			int num5 = this.safeAllocateTemporaryRT(cb, "_AO_SmallOcclusionTexture", this.m_target.width / 2, this.m_target.height / 2, this.m_occlusionRTFormat, RenderTextureReadWrite.Linear, FilterMode.Bilinear);
			cb.SetGlobalVector(AmplifyOcclusionBase.PropertyID._AO_Source_TexelSize, value);
			cb.SetGlobalVector(AmplifyOcclusionBase.PropertyID._AO_Target_TexelSize, new Vector4(1f / ((float)this.m_target.width / 2f), 1f / ((float)this.m_target.height / 2f), (float)this.m_target.width / 2f, (float)this.m_target.height / 2f));
			cb.SetRenderTarget(num5);
			this.PerformBlit(cb, AmplifyOcclusionBase.m_occlusionMat, num2);
			cb.SetRenderTarget(null);
			this.EndSample(cb, "AO 1 - ComputeOcclusion");
			if (this.BlurEnabled)
			{
				this.commandBuffer_Blur(cb, num5, this.m_target.width / 2, this.m_target.height / 2);
			}
			this.BeginSample(cb, "AO 2b - Combine");
			cb.SetGlobalTexture(AmplifyOcclusionBase.PropertyID._AO_Source, num5);
			cb.SetGlobalVector(AmplifyOcclusionBase.PropertyID._AO_Target_TexelSize, value);
			cb.SetRenderTarget(this.m_occlusionDepthRT);
			this.PerformBlit(cb, AmplifyOcclusionBase.m_occlusionMat, 32);
			this.safeReleaseTemporaryRT(cb, num5);
			cb.SetRenderTarget(null);
			this.EndSample(cb, "AO 2b - Combine");
		}
		else
		{
			cb.SetGlobalVector(AmplifyOcclusionBase.PropertyID._AO_Source_TexelSize, value);
			cb.SetGlobalVector(AmplifyOcclusionBase.PropertyID._AO_Target_TexelSize, value);
			cb.SetRenderTarget(this.m_occlusionDepthRT);
			this.PerformBlit(cb, AmplifyOcclusionBase.m_occlusionMat, num2);
			cb.SetRenderTarget(null);
			this.EndSample(cb, "AO 1 - ComputeOcclusion");
			if (this.BlurEnabled)
			{
				this.commandBuffer_Blur(cb, this.m_occlusionDepthRT, this.m_target.width, this.m_target.height);
			}
		}
	}

	// Token: 0x060016D3 RID: 5843 RVA: 0x0008D1C0 File Offset: 0x0008B5C0
	private int commandBuffer_NeighborMotionIntensity(CommandBuffer cb, int aSourceWidth, int aSourceHeight)
	{
		int num = this.safeAllocateTemporaryRT(cb, "_AO_IntensityTmp", aSourceWidth / 4, aSourceHeight / 4, this.m_motionIntensityRTFormat, RenderTextureReadWrite.Linear, FilterMode.Bilinear);
		cb.SetRenderTarget(num);
		cb.SetGlobalVector("_AO_Target_TexelSize", new Vector4(1f / ((float)aSourceWidth / 4f), 1f / ((float)aSourceHeight / 4f), (float)aSourceWidth / 4f, (float)aSourceHeight / 4f));
		this.PerformBlit(cb, AmplifyOcclusionBase.m_occlusionMat, 33);
		int num2 = this.safeAllocateTemporaryRT(cb, "_AO_BlurIntensityTmp", aSourceWidth / 4, aSourceHeight / 4, this.m_motionIntensityRTFormat, RenderTextureReadWrite.Linear, FilterMode.Bilinear);
		cb.SetGlobalTexture(AmplifyOcclusionBase.PropertyID._AO_Source, num);
		cb.SetRenderTarget(num2);
		this.PerformBlit(cb, AmplifyOcclusionBase.m_blurMat, 8);
		cb.SetGlobalTexture(AmplifyOcclusionBase.PropertyID._AO_Source, num2);
		cb.SetRenderTarget(num);
		this.PerformBlit(cb, AmplifyOcclusionBase.m_blurMat, 9);
		this.safeReleaseTemporaryRT(cb, num2);
		cb.SetGlobalTexture(AmplifyOcclusionBase.PropertyID._AO_CurrMotionIntensity, num);
		return num;
	}

	// Token: 0x060016D4 RID: 5844 RVA: 0x0008D2C8 File Offset: 0x0008B6C8
	private void commandBuffer_Blur(CommandBuffer cb, RenderTargetIdentifier aSourceRT, int aSourceWidth, int aSourceHeight)
	{
		this.BeginSample(cb, "AO 2 - Blur");
		int num = this.safeAllocateTemporaryRT(cb, "_AO_BlurTmp", aSourceWidth, aSourceHeight, this.m_occlusionRTFormat, RenderTextureReadWrite.Linear, FilterMode.Bilinear);
		for (int i = 0; i < this.BlurPasses; i++)
		{
			cb.SetGlobalTexture(AmplifyOcclusionBase.PropertyID._AO_Source, aSourceRT);
			int pass = (this.BlurRadius - 1) * 2;
			cb.SetRenderTarget(num);
			this.PerformBlit(cb, AmplifyOcclusionBase.m_blurMat, pass);
			cb.SetGlobalTexture(AmplifyOcclusionBase.PropertyID._AO_Source, num);
			int pass2 = 1 + (this.BlurRadius - 1) * 2;
			cb.SetRenderTarget(aSourceRT);
			this.PerformBlit(cb, AmplifyOcclusionBase.m_blurMat, pass2);
		}
		this.safeReleaseTemporaryRT(cb, num);
		cb.SetRenderTarget(null);
		this.EndSample(cb, "AO 2 - Blur");
	}

	// Token: 0x060016D5 RID: 5845 RVA: 0x0008D392 File Offset: 0x0008B792
	private int getTemporalPass()
	{
		return (!this.UsingMotionVectors || this.m_sampleStep <= 1U) ? 0 : 1;
	}

	// Token: 0x060016D6 RID: 5846 RVA: 0x0008D3B4 File Offset: 0x0008B7B4
	private void commandBuffer_TemporalFilter(CommandBuffer cb)
	{
		if (this.m_clearHistory)
		{
			this.ClearHistory(cb);
		}
		float value = Mathf.Lerp(0.01f, 0.99f, this.FilterBlending);
		cb.SetGlobalFloat(AmplifyOcclusionBase.PropertyID._AO_TemporalCurveAdj, value);
		cb.SetGlobalFloat(AmplifyOcclusionBase.PropertyID._AO_TemporalMotionSensibility, this.FilterResponse * this.FilterResponse + 0.01f);
		cb.SetGlobalTexture(AmplifyOcclusionBase.PropertyID._AO_CurrOcclusionDepth, this.m_occlusionDepthRT);
		cb.SetGlobalTexture(AmplifyOcclusionBase.PropertyID._AO_TemporalAccumm, this.m_temporalAccumRT[(int)((UIntPtr)this.m_prevTemporalIdx)]);
	}

	// Token: 0x060016D7 RID: 5847 RVA: 0x0008D448 File Offset: 0x0008B848
	private void commandBuffer_TemporalFilterDirectionsOffsets(CommandBuffer cb)
	{
		float num = AmplifyOcclusionBase.m_temporalRotations[(int)((UIntPtr)(this.m_sampleStep % 6U))];
		float value = AmplifyOcclusionBase.m_spatialOffsets[(int)((UIntPtr)(this.m_sampleStep / 6U % 4U))];
		cb.SetGlobalFloat(AmplifyOcclusionBase.PropertyID._AO_TemporalDirections, num / 360f);
		cb.SetGlobalFloat(AmplifyOcclusionBase.PropertyID._AO_TemporalOffsets, value);
	}

	// Token: 0x060016D8 RID: 5848 RVA: 0x0008D498 File Offset: 0x0008B898
	private void commandBuffer_FillApplyDeferred(CommandBuffer cb, bool logTarget)
	{
		this.BeginSample(cb, "AO 3 - ApplyDeferred");
		if (!logTarget)
		{
			if (this.UsingTemporalFilter)
			{
				this.commandBuffer_TemporalFilter(cb);
				int id = 0;
				if (this.UsingMotionVectors)
				{
					id = this.commandBuffer_NeighborMotionIntensity(cb, this.m_target.width, this.m_target.height);
				}
				int num = 0;
				if (this.useMRTBlendingFallback)
				{
					num = this.safeAllocateTemporaryRT(cb, "_AO_ApplyOcclusionTexture", this.m_target.fullWidth, this.m_target.fullHeight, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default, FilterMode.Point);
					this.applyOcclusionTemporal[0] = num;
					this.applyOcclusionTemporal[1] = new RenderTargetIdentifier(this.m_temporalAccumRT[(int)((UIntPtr)this.m_curTemporalIdx)]);
					cb.SetRenderTarget(this.applyOcclusionTemporal, this.applyOcclusionTemporal[0]);
					this.PerformBlit(cb, AmplifyOcclusionBase.m_applyOcclusionMat, 10 + this.getTemporalPass());
				}
				else
				{
					this.applyDeferredTargetsTemporal[0] = this.m_applyDeferredTargets[0];
					this.applyDeferredTargetsTemporal[1] = this.m_applyDeferredTargets[1];
					this.applyDeferredTargetsTemporal[2] = new RenderTargetIdentifier(this.m_temporalAccumRT[(int)((UIntPtr)this.m_curTemporalIdx)]);
					cb.SetRenderTarget(this.applyDeferredTargetsTemporal, this.applyDeferredTargetsTemporal[0]);
					this.PerformBlit(cb, AmplifyOcclusionBase.m_applyOcclusionMat, 4 + this.getTemporalPass());
				}
				if (this.useMRTBlendingFallback)
				{
					cb.SetGlobalTexture("_AO_ApplyOcclusionTexture", num);
					this.applyOcclusionTemporal[0] = this.m_applyDeferredTargets[0];
					this.applyOcclusionTemporal[1] = this.m_applyDeferredTargets[1];
					cb.SetRenderTarget(this.applyOcclusionTemporal, this.applyOcclusionTemporal[0]);
					this.PerformBlit(cb, AmplifyOcclusionBase.m_applyOcclusionMat, 13);
					this.safeReleaseTemporaryRT(cb, num);
				}
				if (this.UsingMotionVectors)
				{
					this.safeReleaseTemporaryRT(cb, id);
				}
			}
			else
			{
				cb.SetGlobalTexture(AmplifyOcclusionBase.PropertyID._AO_OcclusionTexture, this.m_occlusionDepthRT);
				cb.SetRenderTarget(this.m_applyDeferredTargets, this.m_applyDeferredTargets[0]);
				this.PerformBlit(cb, AmplifyOcclusionBase.m_applyOcclusionMat, 3);
			}
		}
		else
		{
			int num2 = this.safeAllocateTemporaryRT(cb, "_AO_tmpAlbedo", this.m_target.fullWidth, this.m_target.fullHeight, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default, FilterMode.Point);
			int num3 = this.safeAllocateTemporaryRT(cb, "_AO_tmpEmission", this.m_target.fullWidth, this.m_target.fullHeight, this.m_temporaryEmissionRTFormat, RenderTextureReadWrite.Default, FilterMode.Point);
			cb.Blit(BuiltinRenderTextureType.GBuffer0, num2);
			cb.Blit(BuiltinRenderTextureType.GBuffer3, num3);
			cb.SetGlobalTexture(AmplifyOcclusionBase.PropertyID._AO_GBufferAlbedo, num2);
			cb.SetGlobalTexture(AmplifyOcclusionBase.PropertyID._AO_GBufferEmission, num3);
			if (this.UsingTemporalFilter)
			{
				this.commandBuffer_TemporalFilter(cb);
				int id2 = 0;
				if (this.UsingMotionVectors)
				{
					id2 = this.commandBuffer_NeighborMotionIntensity(cb, this.m_target.width, this.m_target.height);
				}
				this.applyDeferredTargets_Log_Temporal[0] = this.m_applyDeferredTargets_Log[0];
				this.applyDeferredTargets_Log_Temporal[1] = this.m_applyDeferredTargets_Log[1];
				this.applyDeferredTargets_Log_Temporal[2] = new RenderTargetIdentifier(this.m_temporalAccumRT[(int)((UIntPtr)this.m_curTemporalIdx)]);
				cb.SetRenderTarget(this.applyDeferredTargets_Log_Temporal, this.applyDeferredTargets_Log_Temporal[0]);
				this.PerformBlit(cb, AmplifyOcclusionBase.m_applyOcclusionMat, 7 + this.getTemporalPass());
				if (this.UsingMotionVectors)
				{
					this.safeReleaseTemporaryRT(cb, id2);
				}
			}
			else
			{
				cb.SetGlobalTexture(AmplifyOcclusionBase.PropertyID._AO_OcclusionTexture, this.m_occlusionDepthRT);
				cb.SetRenderTarget(this.m_applyDeferredTargets_Log, this.m_applyDeferredTargets_Log[0]);
				this.PerformBlit(cb, AmplifyOcclusionBase.m_applyOcclusionMat, 6);
			}
			this.safeReleaseTemporaryRT(cb, num2);
			this.safeReleaseTemporaryRT(cb, num3);
		}
		cb.SetRenderTarget(null);
		this.EndSample(cb, "AO 3 - ApplyDeferred");
	}

	// Token: 0x060016D9 RID: 5849 RVA: 0x0008D914 File Offset: 0x0008BD14
	private void commandBuffer_FillApplyPostEffect(CommandBuffer cb)
	{
		this.BeginSample(cb, "AO 3 - ApplyPostEffect");
		if (this.UsingTemporalFilter)
		{
			this.commandBuffer_TemporalFilter(cb);
			int id = 0;
			if (this.UsingMotionVectors)
			{
				id = this.commandBuffer_NeighborMotionIntensity(cb, this.m_target.width, this.m_target.height);
			}
			int num = 0;
			if (this.useMRTBlendingFallback)
			{
				num = this.safeAllocateTemporaryRT(cb, "_AO_ApplyOcclusionTexture", this.m_target.fullWidth, this.m_target.fullHeight, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default, FilterMode.Point);
				this.applyPostEffectTargetsTemporal[0] = num;
			}
			else
			{
				this.applyPostEffectTargetsTemporal[0] = BuiltinRenderTextureType.CameraTarget;
			}
			this.applyPostEffectTargetsTemporal[1] = new RenderTargetIdentifier(this.m_temporalAccumRT[(int)((UIntPtr)this.m_curTemporalIdx)]);
			cb.SetRenderTarget(this.applyPostEffectTargetsTemporal, this.applyPostEffectTargetsTemporal[0]);
			this.PerformBlit(cb, AmplifyOcclusionBase.m_applyOcclusionMat, 10 + this.getTemporalPass());
			if (this.useMRTBlendingFallback)
			{
				cb.SetGlobalTexture("_AO_ApplyOcclusionTexture", num);
				cb.SetRenderTarget(BuiltinRenderTextureType.CameraTarget);
				this.PerformBlit(cb, AmplifyOcclusionBase.m_applyOcclusionMat, 12);
				this.safeReleaseTemporaryRT(cb, num);
			}
			if (this.UsingMotionVectors)
			{
				this.safeReleaseTemporaryRT(cb, id);
			}
		}
		else
		{
			cb.SetGlobalTexture(AmplifyOcclusionBase.PropertyID._AO_OcclusionTexture, this.m_occlusionDepthRT);
			cb.SetRenderTarget(BuiltinRenderTextureType.CameraTarget);
			this.PerformBlit(cb, AmplifyOcclusionBase.m_applyOcclusionMat, 9);
		}
		cb.SetRenderTarget(null);
		this.EndSample(cb, "AO 3 - ApplyPostEffect");
	}

	// Token: 0x060016DA RID: 5850 RVA: 0x0008DAC4 File Offset: 0x0008BEC4
	private void commandBuffer_FillApplyDebug(CommandBuffer cb)
	{
		this.BeginSample(cb, "AO 3 - ApplyDebug");
		if (this.UsingTemporalFilter)
		{
			this.commandBuffer_TemporalFilter(cb);
			int id = 0;
			if (this.UsingMotionVectors)
			{
				id = this.commandBuffer_NeighborMotionIntensity(cb, this.m_target.width, this.m_target.height);
			}
			this.applyDebugTargetsTemporal[0] = BuiltinRenderTextureType.CameraTarget;
			this.applyDebugTargetsTemporal[1] = new RenderTargetIdentifier(this.m_temporalAccumRT[(int)((UIntPtr)this.m_curTemporalIdx)]);
			cb.SetRenderTarget(this.applyDebugTargetsTemporal, this.applyDebugTargetsTemporal[0]);
			this.PerformBlit(cb, AmplifyOcclusionBase.m_applyOcclusionMat, 1 + this.getTemporalPass());
			if (this.UsingMotionVectors)
			{
				this.safeReleaseTemporaryRT(cb, id);
			}
		}
		else
		{
			cb.SetGlobalTexture(AmplifyOcclusionBase.PropertyID._AO_OcclusionTexture, this.m_occlusionDepthRT);
			cb.SetRenderTarget(BuiltinRenderTextureType.CameraTarget);
			this.PerformBlit(cb, AmplifyOcclusionBase.m_applyOcclusionMat, 0);
		}
		cb.SetRenderTarget(null);
		this.EndSample(cb, "AO 3 - ApplyDebug");
	}

	// Token: 0x060016DB RID: 5851 RVA: 0x0008DBE8 File Offset: 0x0008BFE8
	private bool isStereoSinglePassEnabled()
	{
		return this.m_targetCamera.stereoEnabled && XRSettings.eyeTextureDesc.vrUsage == VRTextureUsage.TwoEyes;
	}

	// Token: 0x060016DC RID: 5852 RVA: 0x0008DC18 File Offset: 0x0008C018
	private bool isStereoMultiPassEnabled()
	{
		return this.m_targetCamera.stereoEnabled && XRSettings.eyeTextureDesc.vrUsage == VRTextureUsage.OneEye;
	}

	// Token: 0x060016DD RID: 5853 RVA: 0x0008DC48 File Offset: 0x0008C048
	private void UpdateGlobalShaderConstants(CommandBuffer cb)
	{
		if (XRSettings.enabled)
		{
			this.m_target.fullWidth = (int)((float)XRSettings.eyeTextureDesc.width * XRSettings.eyeTextureResolutionScale);
			this.m_target.fullHeight = (int)((float)XRSettings.eyeTextureDesc.height * XRSettings.eyeTextureResolutionScale);
		}
		else
		{
			this.m_target.fullWidth = this.m_targetCamera.pixelWidth;
			this.m_target.fullHeight = this.m_targetCamera.pixelHeight;
		}
		this.m_target.width = this.m_target.fullWidth;
		this.m_target.height = this.m_target.fullHeight;
		this.m_target.oneOverWidth = 1f / (float)this.m_target.width;
		this.m_target.oneOverHeight = 1f / (float)this.m_target.height;
		float num = this.m_targetCamera.fieldOfView * 0.017453292f;
		float num2 = 1f / Mathf.Tan(num * 0.5f);
		Vector2 vector = new Vector2(num2 * ((float)this.m_target.height / (float)this.m_target.width), num2);
		Vector2 vector2 = new Vector2(1f / vector.x, 1f / vector.y);
		cb.SetGlobalVector(AmplifyOcclusionBase.PropertyID._AO_UVToView, new Vector4(2f * vector2.x, 2f * vector2.y, -1f * vector2.x, -1f * vector2.y));
		float num3;
		if (this.m_targetCamera.orthographic)
		{
			num3 = (float)this.m_target.height / this.m_targetCamera.orthographicSize;
		}
		else
		{
			num3 = (float)this.m_target.height / (Mathf.Tan(num * 0.5f) * 2f);
		}
		if (this.Downsample)
		{
			num3 = num3 * 0.5f * 0.5f;
		}
		else
		{
			num3 *= 0.5f;
		}
		cb.SetGlobalFloat(AmplifyOcclusionBase.PropertyID._AO_HalfProjScale, num3);
	}

	// Token: 0x060016DE RID: 5854 RVA: 0x0008DE6C File Offset: 0x0008C26C
	private void UpdateGlobalShaderConstants_AmbientOcclusion(CommandBuffer cb)
	{
		cb.SetGlobalFloat(AmplifyOcclusionBase.PropertyID._AO_Radius, this.Radius);
		cb.SetGlobalFloat(AmplifyOcclusionBase.PropertyID._AO_PowExponent, this.PowerExponent);
		cb.SetGlobalFloat(AmplifyOcclusionBase.PropertyID._AO_Bias, this.Bias * this.Bias);
		cb.SetGlobalColor(AmplifyOcclusionBase.PropertyID._AO_Levels, new Color(this.Tint.r, this.Tint.g, this.Tint.b, this.Intensity));
		float num = 1f - this.Thickness;
		cb.SetGlobalFloat(AmplifyOcclusionBase.PropertyID._AO_ThicknessDecay, (1f - num * num) * 0.98f);
		if (this.BlurEnabled)
		{
			cb.SetGlobalFloat(AmplifyOcclusionBase.PropertyID._AO_BlurSharpness, this.BlurSharpness * 100f);
		}
		if (this.FadeEnabled)
		{
			this.FadeStart = Mathf.Max(0f, this.FadeStart);
			this.FadeLength = Mathf.Max(0.01f, this.FadeLength);
			float y = 1f / this.FadeLength;
			cb.SetGlobalVector(AmplifyOcclusionBase.PropertyID._AO_FadeParams, new Vector2(this.FadeStart, y));
			float num2 = 1f - this.FadeToThickness;
			cb.SetGlobalVector(AmplifyOcclusionBase.PropertyID._AO_FadeValues, new Vector4(this.FadeToIntensity, this.FadeToRadius, this.FadeToPowerExponent, (1f - num2 * num2) * 0.98f));
			cb.SetGlobalColor(AmplifyOcclusionBase.PropertyID._AO_FadeToTint, new Color(this.FadeToTint.r, this.FadeToTint.g, this.FadeToTint.b, 0f));
		}
		else
		{
			cb.SetGlobalVector(AmplifyOcclusionBase.PropertyID._AO_FadeParams, new Vector2(0f, 0f));
		}
		if (this.FilterEnabled)
		{
			this.commandBuffer_TemporalFilterDirectionsOffsets(cb);
		}
		else
		{
			cb.SetGlobalFloat(AmplifyOcclusionBase.PropertyID._AO_TemporalDirections, 0f);
			cb.SetGlobalFloat(AmplifyOcclusionBase.PropertyID._AO_TemporalOffsets, 0f);
		}
	}

	// Token: 0x060016DF RID: 5855 RVA: 0x0008E064 File Offset: 0x0008C464
	private void UpdateGlobalShaderConstants_Matrices(CommandBuffer cb)
	{
		if (this.isStereoSinglePassEnabled())
		{
			Matrix4x4 stereoViewMatrix = this.m_targetCamera.GetStereoViewMatrix(Camera.StereoscopicEye.Left);
			Matrix4x4 stereoViewMatrix2 = this.m_targetCamera.GetStereoViewMatrix(Camera.StereoscopicEye.Right);
			cb.SetGlobalMatrix(AmplifyOcclusionBase.PropertyID._AO_CameraViewLeft, stereoViewMatrix);
			cb.SetGlobalMatrix(AmplifyOcclusionBase.PropertyID._AO_CameraViewRight, stereoViewMatrix2);
			Matrix4x4 stereoProjectionMatrix = this.m_targetCamera.GetStereoProjectionMatrix(Camera.StereoscopicEye.Left);
			Matrix4x4 stereoProjectionMatrix2 = this.m_targetCamera.GetStereoProjectionMatrix(Camera.StereoscopicEye.Right);
			Matrix4x4 gpuprojectionMatrix = GL.GetGPUProjectionMatrix(stereoProjectionMatrix, false);
			Matrix4x4 gpuprojectionMatrix2 = GL.GetGPUProjectionMatrix(stereoProjectionMatrix2, false);
			cb.SetGlobalMatrix(AmplifyOcclusionBase.PropertyID._AO_ProjMatrixLeft, gpuprojectionMatrix);
			cb.SetGlobalMatrix(AmplifyOcclusionBase.PropertyID._AO_ProjMatrixRight, gpuprojectionMatrix2);
			if (this.UsingTemporalFilter)
			{
				Matrix4x4 matrix4x = gpuprojectionMatrix * stereoViewMatrix;
				Matrix4x4 matrix4x2 = gpuprojectionMatrix2 * stereoViewMatrix2;
				Matrix4x4 matrix4x3 = Matrix4x4.Inverse(matrix4x);
				Matrix4x4 matrix4x4 = Matrix4x4.Inverse(matrix4x2);
				cb.SetGlobalMatrix(AmplifyOcclusionBase.PropertyID._AO_InvViewProjMatrixLeft, matrix4x3);
				cb.SetGlobalMatrix(AmplifyOcclusionBase.PropertyID._AO_PrevViewProjMatrixLeft, this.m_prevViewProjMatrixLeft);
				cb.SetGlobalMatrix(AmplifyOcclusionBase.PropertyID._AO_PrevInvViewProjMatrixLeft, this.m_prevInvViewProjMatrixLeft);
				cb.SetGlobalMatrix(AmplifyOcclusionBase.PropertyID._AO_InvViewProjMatrixRight, matrix4x4);
				cb.SetGlobalMatrix(AmplifyOcclusionBase.PropertyID._AO_PrevViewProjMatrixRight, this.m_prevViewProjMatrixRight);
				cb.SetGlobalMatrix(AmplifyOcclusionBase.PropertyID._AO_PrevInvViewProjMatrixRight, this.m_prevInvViewProjMatrixRight);
				this.m_prevViewProjMatrixLeft = matrix4x;
				this.m_prevInvViewProjMatrixLeft = matrix4x3;
				this.m_prevViewProjMatrixRight = matrix4x2;
				this.m_prevInvViewProjMatrixRight = matrix4x4;
			}
		}
		else
		{
			Matrix4x4 worldToCameraMatrix = this.m_targetCamera.worldToCameraMatrix;
			cb.SetGlobalMatrix(AmplifyOcclusionBase.PropertyID._AO_CameraViewLeft, worldToCameraMatrix);
			if (this.UsingTemporalFilter)
			{
				Matrix4x4 gpuprojectionMatrix3 = GL.GetGPUProjectionMatrix(this.m_targetCamera.projectionMatrix, false);
				Matrix4x4 matrix4x5 = gpuprojectionMatrix3 * worldToCameraMatrix;
				Matrix4x4 matrix4x6 = Matrix4x4.Inverse(matrix4x5);
				cb.SetGlobalMatrix(AmplifyOcclusionBase.PropertyID._AO_InvViewProjMatrixLeft, matrix4x6);
				cb.SetGlobalMatrix(AmplifyOcclusionBase.PropertyID._AO_PrevViewProjMatrixLeft, this.m_prevViewProjMatrixLeft);
				cb.SetGlobalMatrix(AmplifyOcclusionBase.PropertyID._AO_PrevInvViewProjMatrixLeft, this.m_prevInvViewProjMatrixLeft);
				this.m_prevViewProjMatrixLeft = matrix4x5;
				this.m_prevInvViewProjMatrixLeft = matrix4x6;
			}
		}
	}

	// Token: 0x0400197C RID: 6524
	private static int m_nextID = 0;

	// Token: 0x0400197D RID: 6525
	private int m_myID;

	// Token: 0x0400197E RID: 6526
	private string m_myIDstring;

	// Token: 0x0400197F RID: 6527
	[Header("Ambient Occlusion")]
	[Tooltip("How to inject the occlusion: Post Effect = Overlay, Deferred = Deferred Injection, Debug - Vizualize.")]
	public AmplifyOcclusionBase.ApplicationMethod ApplyMethod;

	// Token: 0x04001980 RID: 6528
	[Tooltip("Number of samples per pass.")]
	public AmplifyOcclusionBase.SampleCountLevel SampleCount = AmplifyOcclusionBase.SampleCountLevel.Medium;

	// Token: 0x04001981 RID: 6529
	[Tooltip("Source of per-pixel normals: None = All, Camera = Forward, GBuffer = Deferred.")]
	public AmplifyOcclusionBase.PerPixelNormalSource PerPixelNormals = AmplifyOcclusionBase.PerPixelNormalSource.Camera;

	// Token: 0x04001982 RID: 6530
	[Tooltip("Final applied intensity of the occlusion effect.")]
	[Range(0f, 1f)]
	public float Intensity = 1f;

	// Token: 0x04001983 RID: 6531
	[Tooltip("Color tint for occlusion.")]
	public Color Tint = Color.black;

	// Token: 0x04001984 RID: 6532
	[Tooltip("Radius spread of the occlusion.")]
	[Range(0f, 32f)]
	public float Radius = 2f;

	// Token: 0x04001985 RID: 6533
	[Tooltip("Power exponent attenuation of the occlusion.")]
	[Range(0f, 16f)]
	public float PowerExponent = 1.8f;

	// Token: 0x04001986 RID: 6534
	[Tooltip("Controls the initial occlusion contribution offset.")]
	[Range(0f, 0.99f)]
	public float Bias = 0.05f;

	// Token: 0x04001987 RID: 6535
	[Tooltip("Controls the thickness occlusion contribution.")]
	[Range(0f, 1f)]
	public float Thickness = 1f;

	// Token: 0x04001988 RID: 6536
	[Tooltip("Compute the Occlusion and Blur at half of the resolution.")]
	public bool Downsample = true;

	// Token: 0x04001989 RID: 6537
	[Tooltip("Cache optimization for best performance / quality tradeoff.")]
	public bool CacheAware = true;

	// Token: 0x0400198A RID: 6538
	[Header("Distance Fade")]
	[Tooltip("Control parameters at faraway.")]
	public bool FadeEnabled;

	// Token: 0x0400198B RID: 6539
	[Tooltip("Distance in Unity unities that start to fade.")]
	public float FadeStart = 100f;

	// Token: 0x0400198C RID: 6540
	[Tooltip("Length distance to performe the transition.")]
	public float FadeLength = 50f;

	// Token: 0x0400198D RID: 6541
	[Tooltip("Final Intensity parameter.")]
	[Range(0f, 1f)]
	public float FadeToIntensity;

	// Token: 0x0400198E RID: 6542
	public Color FadeToTint = Color.black;

	// Token: 0x0400198F RID: 6543
	[Tooltip("Final Radius parameter.")]
	[Range(0f, 32f)]
	public float FadeToRadius = 2f;

	// Token: 0x04001990 RID: 6544
	[Tooltip("Final PowerExponent parameter.")]
	[Range(0f, 16f)]
	public float FadeToPowerExponent = 1f;

	// Token: 0x04001991 RID: 6545
	[Tooltip("Final Thickness parameter.")]
	[Range(0f, 1f)]
	public float FadeToThickness = 1f;

	// Token: 0x04001992 RID: 6546
	[Header("Bilateral Blur")]
	public bool BlurEnabled = true;

	// Token: 0x04001993 RID: 6547
	[Tooltip("Radius in screen pixels.")]
	[Range(1f, 4f)]
	public int BlurRadius = 3;

	// Token: 0x04001994 RID: 6548
	[Tooltip("Number of times that the Blur will repeat.")]
	[Range(1f, 4f)]
	public int BlurPasses = 1;

	// Token: 0x04001995 RID: 6549
	[Tooltip("Sharpness of blur edge-detection: 0 = Softer Edges, 20 = Sharper Edges.")]
	[Range(0f, 20f)]
	public float BlurSharpness = 15f;

	// Token: 0x04001996 RID: 6550
	[Header("Temporal Filter")]
	[Tooltip("Accumulates the effect over the time.")]
	public bool FilterEnabled = true;

	// Token: 0x04001997 RID: 6551
	[Tooltip("Controls the accumulation decayment: 0 = More flicker with less ghosting, 1 = Less flicker with more ghosting.")]
	[Range(0f, 1f)]
	public float FilterBlending = 0.8f;

	// Token: 0x04001998 RID: 6552
	[Tooltip("Controls the discard sensitivity based on the motion of the scene and objects.")]
	[Range(0f, 1f)]
	public float FilterResponse = 0.5f;

	// Token: 0x04001999 RID: 6553
	private bool m_HDR = true;

	// Token: 0x0400199A RID: 6554
	private bool m_MSAA = true;

	// Token: 0x0400199B RID: 6555
	private AmplifyOcclusionBase.PerPixelNormalSource m_prevPerPixelNormals;

	// Token: 0x0400199C RID: 6556
	private AmplifyOcclusionBase.ApplicationMethod m_prevApplyMethod;

	// Token: 0x0400199D RID: 6557
	private bool m_prevDeferredReflections;

	// Token: 0x0400199E RID: 6558
	private AmplifyOcclusionBase.SampleCountLevel m_prevSampleCount;

	// Token: 0x0400199F RID: 6559
	private bool m_prevDownsample;

	// Token: 0x040019A0 RID: 6560
	private bool m_prevCacheAware;

	// Token: 0x040019A1 RID: 6561
	private bool m_prevBlurEnabled;

	// Token: 0x040019A2 RID: 6562
	private int m_prevBlurRadius;

	// Token: 0x040019A3 RID: 6563
	private int m_prevBlurPasses;

	// Token: 0x040019A4 RID: 6564
	private bool m_prevFilterEnabled = true;

	// Token: 0x040019A5 RID: 6565
	private bool m_prevHDR = true;

	// Token: 0x040019A6 RID: 6566
	private bool m_prevMSAA = true;

	// Token: 0x040019A7 RID: 6567
	private Camera m_targetCamera;

	// Token: 0x040019A8 RID: 6568
	private RenderTargetIdentifier[] applyDebugTargetsTemporal = new RenderTargetIdentifier[2];

	// Token: 0x040019A9 RID: 6569
	private RenderTargetIdentifier[] applyDeferredTargets_Log_Temporal = new RenderTargetIdentifier[3];

	// Token: 0x040019AA RID: 6570
	private RenderTargetIdentifier[] applyDeferredTargetsTemporal = new RenderTargetIdentifier[3];

	// Token: 0x040019AB RID: 6571
	private RenderTargetIdentifier[] applyOcclusionTemporal = new RenderTargetIdentifier[2];

	// Token: 0x040019AC RID: 6572
	private RenderTargetIdentifier[] applyPostEffectTargetsTemporal = new RenderTargetIdentifier[2];

	// Token: 0x040019AD RID: 6573
	private bool useMRTBlendingFallback;

	// Token: 0x040019AE RID: 6574
	private AmplifyOcclusionBase.CmdBuffer m_commandBuffer_Parameters;

	// Token: 0x040019AF RID: 6575
	private AmplifyOcclusionBase.CmdBuffer m_commandBuffer_Occlusion;

	// Token: 0x040019B0 RID: 6576
	private AmplifyOcclusionBase.CmdBuffer m_commandBuffer_Apply;

	// Token: 0x040019B1 RID: 6577
	private static Mesh m_quadMesh = null;

	// Token: 0x040019B2 RID: 6578
	private static Material m_occlusionMat = null;

	// Token: 0x040019B3 RID: 6579
	private static Material m_blurMat = null;

	// Token: 0x040019B4 RID: 6580
	private static Material m_applyOcclusionMat = null;

	// Token: 0x040019B5 RID: 6581
	private RenderTextureFormat m_occlusionRTFormat = RenderTextureFormat.RGHalf;

	// Token: 0x040019B6 RID: 6582
	private RenderTextureFormat m_accumTemporalRTFormat;

	// Token: 0x040019B7 RID: 6583
	private RenderTextureFormat m_temporaryEmissionRTFormat = RenderTextureFormat.ARGB2101010;

	// Token: 0x040019B8 RID: 6584
	private RenderTextureFormat m_motionIntensityRTFormat = RenderTextureFormat.R8;

	// Token: 0x040019B9 RID: 6585
	private bool m_paramsChanged = true;

	// Token: 0x040019BA RID: 6586
	private bool m_clearHistory = true;

	// Token: 0x040019BB RID: 6587
	private RenderTexture m_occlusionDepthRT;

	// Token: 0x040019BC RID: 6588
	private RenderTexture[] m_temporalAccumRT;

	// Token: 0x040019BD RID: 6589
	private RenderTexture m_depthMipmap;

	// Token: 0x040019BE RID: 6590
	private uint m_sampleStep;

	// Token: 0x040019BF RID: 6591
	private uint m_curTemporalIdx;

	// Token: 0x040019C0 RID: 6592
	private uint m_prevTemporalIdx;

	// Token: 0x040019C1 RID: 6593
	private static readonly int PerPixelNormalSourceCount = 4;

	// Token: 0x040019C2 RID: 6594
	private Matrix4x4 m_prevViewProjMatrixLeft = Matrix4x4.identity;

	// Token: 0x040019C3 RID: 6595
	private Matrix4x4 m_prevInvViewProjMatrixLeft = Matrix4x4.identity;

	// Token: 0x040019C4 RID: 6596
	private Matrix4x4 m_prevViewProjMatrixRight = Matrix4x4.identity;

	// Token: 0x040019C5 RID: 6597
	private Matrix4x4 m_prevInvViewProjMatrixRight = Matrix4x4.identity;

	// Token: 0x040019C6 RID: 6598
	private static readonly float[] m_temporalRotations = new float[]
	{
		60f,
		300f,
		180f,
		240f,
		120f,
		0f
	};

	// Token: 0x040019C7 RID: 6599
	private static readonly float[] m_spatialOffsets = new float[]
	{
		0f,
		0.5f,
		0.25f,
		0.75f
	};

	// Token: 0x040019C8 RID: 6600
	private string[] m_tmpMipString;

	// Token: 0x040019C9 RID: 6601
	private int m_numberMips;

	// Token: 0x040019CA RID: 6602
	private readonly RenderTargetIdentifier[] m_applyDeferredTargets = new RenderTargetIdentifier[]
	{
		BuiltinRenderTextureType.GBuffer0,
		BuiltinRenderTextureType.CameraTarget
	};

	// Token: 0x040019CB RID: 6603
	private readonly RenderTargetIdentifier[] m_applyDeferredTargets_Log = new RenderTargetIdentifier[]
	{
		BuiltinRenderTextureType.GBuffer0,
		BuiltinRenderTextureType.GBuffer3
	};

	// Token: 0x040019CC RID: 6604
	private AmplifyOcclusionBase.TargetDesc m_target = default(AmplifyOcclusionBase.TargetDesc);

	// Token: 0x020004CF RID: 1231
	public enum ApplicationMethod
	{
		// Token: 0x040019CE RID: 6606
		PostEffect,
		// Token: 0x040019CF RID: 6607
		Deferred,
		// Token: 0x040019D0 RID: 6608
		Debug
	}

	// Token: 0x020004D0 RID: 1232
	public enum PerPixelNormalSource
	{
		// Token: 0x040019D2 RID: 6610
		None,
		// Token: 0x040019D3 RID: 6611
		Camera,
		// Token: 0x040019D4 RID: 6612
		GBuffer,
		// Token: 0x040019D5 RID: 6613
		GBufferOctaEncoded
	}

	// Token: 0x020004D1 RID: 1233
	public enum SampleCountLevel
	{
		// Token: 0x040019D7 RID: 6615
		Low,
		// Token: 0x040019D8 RID: 6616
		Medium,
		// Token: 0x040019D9 RID: 6617
		High,
		// Token: 0x040019DA RID: 6618
		VeryHigh
	}

	// Token: 0x020004D2 RID: 1234
	private struct CmdBuffer
	{
		// Token: 0x040019DB RID: 6619
		public CommandBuffer cmdBuffer;

		// Token: 0x040019DC RID: 6620
		public CameraEvent cmdBufferEvent;

		// Token: 0x040019DD RID: 6621
		public string cmdBufferName;
	}

	// Token: 0x020004D3 RID: 1235
	private struct TargetDesc
	{
		// Token: 0x040019DE RID: 6622
		public int fullWidth;

		// Token: 0x040019DF RID: 6623
		public int fullHeight;

		// Token: 0x040019E0 RID: 6624
		public int width;

		// Token: 0x040019E1 RID: 6625
		public int height;

		// Token: 0x040019E2 RID: 6626
		public float oneOverWidth;

		// Token: 0x040019E3 RID: 6627
		public float oneOverHeight;
	}

	// Token: 0x020004D4 RID: 1236
	private static class ShaderPass
	{
		// Token: 0x040019E4 RID: 6628
		public const int CombineDownsampledOcclusionDepth = 32;

		// Token: 0x040019E5 RID: 6629
		public const int NeighborMotionIntensity = 33;

		// Token: 0x040019E6 RID: 6630
		public const int ClearTemporal = 34;

		// Token: 0x040019E7 RID: 6631
		public const int ScaleDownCloserDepthEven = 35;

		// Token: 0x040019E8 RID: 6632
		public const int ScaleDownCloserDepthEven_CameraDepthTexture = 36;

		// Token: 0x040019E9 RID: 6633
		public const int BlurHorizontal1 = 0;

		// Token: 0x040019EA RID: 6634
		public const int BlurVertical1 = 1;

		// Token: 0x040019EB RID: 6635
		public const int BlurHorizontal2 = 2;

		// Token: 0x040019EC RID: 6636
		public const int BlurVertical2 = 3;

		// Token: 0x040019ED RID: 6637
		public const int BlurHorizontal3 = 4;

		// Token: 0x040019EE RID: 6638
		public const int BlurVertical3 = 5;

		// Token: 0x040019EF RID: 6639
		public const int BlurHorizontal4 = 6;

		// Token: 0x040019F0 RID: 6640
		public const int BlurVertical4 = 7;

		// Token: 0x040019F1 RID: 6641
		public const int BlurHorizontalIntensity = 8;

		// Token: 0x040019F2 RID: 6642
		public const int BlurVerticalIntensity = 9;

		// Token: 0x040019F3 RID: 6643
		public const int ApplyDebug = 0;

		// Token: 0x040019F4 RID: 6644
		public const int ApplyDebugTemporal = 1;

		// Token: 0x040019F5 RID: 6645
		public const int ApplyDeferred = 3;

		// Token: 0x040019F6 RID: 6646
		public const int ApplyDeferredTemporal = 4;

		// Token: 0x040019F7 RID: 6647
		public const int ApplyDeferredLog = 6;

		// Token: 0x040019F8 RID: 6648
		public const int ApplyDeferredLogTemporal = 7;

		// Token: 0x040019F9 RID: 6649
		public const int ApplyPostEffect = 9;

		// Token: 0x040019FA RID: 6650
		public const int ApplyPostEffectTemporal = 10;

		// Token: 0x040019FB RID: 6651
		public const int ApplyPostEffectTemporalMultiply = 12;

		// Token: 0x040019FC RID: 6652
		public const int ApplyDeferredTemporalMultiply = 13;

		// Token: 0x040019FD RID: 6653
		public const int OcclusionLow_None = 0;

		// Token: 0x040019FE RID: 6654
		public const int OcclusionLow_Camera = 1;

		// Token: 0x040019FF RID: 6655
		public const int OcclusionLow_GBuffer = 2;

		// Token: 0x04001A00 RID: 6656
		public const int OcclusionLow_GBufferOctaEncoded = 3;

		// Token: 0x04001A01 RID: 6657
		public const int OcclusionLow_None_UseDynamicDepthMips = 16;
	}

	// Token: 0x020004D5 RID: 1237
	private static class PropertyID
	{
		// Token: 0x04001A02 RID: 6658
		public static readonly int _AO_Radius = Shader.PropertyToID("_AO_Radius");

		// Token: 0x04001A03 RID: 6659
		public static readonly int _AO_PowExponent = Shader.PropertyToID("_AO_PowExponent");

		// Token: 0x04001A04 RID: 6660
		public static readonly int _AO_Bias = Shader.PropertyToID("_AO_Bias");

		// Token: 0x04001A05 RID: 6661
		public static readonly int _AO_Levels = Shader.PropertyToID("_AO_Levels");

		// Token: 0x04001A06 RID: 6662
		public static readonly int _AO_ThicknessDecay = Shader.PropertyToID("_AO_ThicknessDecay");

		// Token: 0x04001A07 RID: 6663
		public static readonly int _AO_BlurSharpness = Shader.PropertyToID("_AO_BlurSharpness");

		// Token: 0x04001A08 RID: 6664
		public static readonly int _AO_CameraViewLeft = Shader.PropertyToID("_AO_CameraViewLeft");

		// Token: 0x04001A09 RID: 6665
		public static readonly int _AO_CameraViewRight = Shader.PropertyToID("_AO_CameraViewRight");

		// Token: 0x04001A0A RID: 6666
		public static readonly int _AO_ProjMatrixLeft = Shader.PropertyToID("_AO_ProjMatrixLeft");

		// Token: 0x04001A0B RID: 6667
		public static readonly int _AO_ProjMatrixRight = Shader.PropertyToID("_AO_ProjMatrixRight");

		// Token: 0x04001A0C RID: 6668
		public static readonly int _AO_InvViewProjMatrixLeft = Shader.PropertyToID("_AO_InvViewProjMatrixLeft");

		// Token: 0x04001A0D RID: 6669
		public static readonly int _AO_PrevViewProjMatrixLeft = Shader.PropertyToID("_AO_PrevViewProjMatrixLeft");

		// Token: 0x04001A0E RID: 6670
		public static readonly int _AO_PrevInvViewProjMatrixLeft = Shader.PropertyToID("_AO_PrevInvViewProjMatrixLeft");

		// Token: 0x04001A0F RID: 6671
		public static readonly int _AO_InvViewProjMatrixRight = Shader.PropertyToID("_AO_InvViewProjMatrixRight");

		// Token: 0x04001A10 RID: 6672
		public static readonly int _AO_PrevViewProjMatrixRight = Shader.PropertyToID("_AO_PrevViewProjMatrixRight");

		// Token: 0x04001A11 RID: 6673
		public static readonly int _AO_PrevInvViewProjMatrixRight = Shader.PropertyToID("_AO_PrevInvViewProjMatrixRight");

		// Token: 0x04001A12 RID: 6674
		public static readonly int _AO_GBufferNormals = Shader.PropertyToID("_AO_GBufferNormals");

		// Token: 0x04001A13 RID: 6675
		public static readonly int _AO_Target_TexelSize = Shader.PropertyToID("_AO_Target_TexelSize");

		// Token: 0x04001A14 RID: 6676
		public static readonly int _AO_TemporalCurveAdj = Shader.PropertyToID("_AO_TemporalCurveAdj");

		// Token: 0x04001A15 RID: 6677
		public static readonly int _AO_TemporalMotionSensibility = Shader.PropertyToID("_AO_TemporalMotionSensibility");

		// Token: 0x04001A16 RID: 6678
		public static readonly int _AO_CurrOcclusionDepth = Shader.PropertyToID("_AO_CurrOcclusionDepth");

		// Token: 0x04001A17 RID: 6679
		public static readonly int _AO_TemporalAccumm = Shader.PropertyToID("_AO_TemporalAccumm");

		// Token: 0x04001A18 RID: 6680
		public static readonly int _AO_TemporalDirections = Shader.PropertyToID("_AO_TemporalDirections");

		// Token: 0x04001A19 RID: 6681
		public static readonly int _AO_TemporalOffsets = Shader.PropertyToID("_AO_TemporalOffsets");

		// Token: 0x04001A1A RID: 6682
		public static readonly int _AO_OcclusionTexture = Shader.PropertyToID("_AO_OcclusionTexture");

		// Token: 0x04001A1B RID: 6683
		public static readonly int _AO_GBufferAlbedo = Shader.PropertyToID("_AO_GBufferAlbedo");

		// Token: 0x04001A1C RID: 6684
		public static readonly int _AO_GBufferEmission = Shader.PropertyToID("_AO_GBufferEmission");

		// Token: 0x04001A1D RID: 6685
		public static readonly int _AO_UVToView = Shader.PropertyToID("_AO_UVToView");

		// Token: 0x04001A1E RID: 6686
		public static readonly int _AO_HalfProjScale = Shader.PropertyToID("_AO_HalfProjScale");

		// Token: 0x04001A1F RID: 6687
		public static readonly int _AO_FadeParams = Shader.PropertyToID("_AO_FadeParams");

		// Token: 0x04001A20 RID: 6688
		public static readonly int _AO_FadeValues = Shader.PropertyToID("_AO_FadeValues");

		// Token: 0x04001A21 RID: 6689
		public static readonly int _AO_FadeToTint = Shader.PropertyToID("_AO_FadeToTint");

		// Token: 0x04001A22 RID: 6690
		public static readonly int _AO_Source_TexelSize = Shader.PropertyToID("_AO_Source_TexelSize");

		// Token: 0x04001A23 RID: 6691
		public static readonly int _AO_Source = Shader.PropertyToID("_AO_Source");

		// Token: 0x04001A24 RID: 6692
		public static readonly int _AO_CurrMotionIntensity = Shader.PropertyToID("_AO_CurrMotionIntensity");

		// Token: 0x04001A25 RID: 6693
		public static readonly int _AO_SourceDepthMipmap = Shader.PropertyToID("_AO_SourceDepthMipmap");
	}
}
