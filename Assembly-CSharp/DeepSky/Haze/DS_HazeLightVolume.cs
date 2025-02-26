using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace DeepSky.Haze
{
	// Token: 0x020002F2 RID: 754
	[ExecuteInEditMode]
	[RequireComponent(typeof(Light))]
	[AddComponentMenu("DeepSky Haze/Light Volume", 2)]
	public class DS_HazeLightVolume : MonoBehaviour
	{
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000CBC RID: 3260 RVA: 0x000341F9 File Offset: 0x000325F9
		public Light LightSource
		{
			get
			{
				return this.m_Light;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000CBD RID: 3261 RVA: 0x00034201 File Offset: 0x00032601
		public LightType Type
		{
			get
			{
				return (!(this.m_Light != null)) ? LightType.Point : this.m_Light.type;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000CBE RID: 3262 RVA: 0x00034225 File Offset: 0x00032625
		public bool CastShadows
		{
			get
			{
				return this.m_Light.shadows != LightShadows.None;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000CBF RID: 3263 RVA: 0x0003423E File Offset: 0x0003263E
		public CommandBuffer RenderCommandBuffer
		{
			get
			{
				return this.m_RenderCmd;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000CC0 RID: 3264 RVA: 0x00034246 File Offset: 0x00032646
		// (set) Token: 0x06000CC1 RID: 3265 RVA: 0x0003424E File Offset: 0x0003264E
		public DS_SamplingQuality Samples
		{
			get
			{
				return this.m_Samples;
			}
			set
			{
				this.m_Samples = value;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000CC2 RID: 3266 RVA: 0x00034257 File Offset: 0x00032657
		// (set) Token: 0x06000CC3 RID: 3267 RVA: 0x0003425F File Offset: 0x0003265F
		public DS_LightFalloff Falloff
		{
			get
			{
				return this.m_Falloff;
			}
			set
			{
				this.m_Falloff = value;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000CC4 RID: 3268 RVA: 0x00034268 File Offset: 0x00032668
		// (set) Token: 0x06000CC5 RID: 3269 RVA: 0x00034270 File Offset: 0x00032670
		public bool UseFog
		{
			get
			{
				return this.m_UseFog;
			}
			set
			{
				this.m_UseFog = value;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000CC6 RID: 3270 RVA: 0x00034279 File Offset: 0x00032679
		// (set) Token: 0x06000CC7 RID: 3271 RVA: 0x00034281 File Offset: 0x00032681
		public float Scattering
		{
			get
			{
				return this.m_Scattering;
			}
			set
			{
				this.m_Scattering = Mathf.Clamp01(value);
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000CC8 RID: 3272 RVA: 0x0003428F File Offset: 0x0003268F
		// (set) Token: 0x06000CC9 RID: 3273 RVA: 0x00034297 File Offset: 0x00032697
		public float ScatteringDirection
		{
			get
			{
				return this.m_ScatteringDirection;
			}
			set
			{
				this.m_ScatteringDirection = Mathf.Clamp(value, -1f, 1f);
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000CCA RID: 3274 RVA: 0x000342AF File Offset: 0x000326AF
		// (set) Token: 0x06000CCB RID: 3275 RVA: 0x000342B7 File Offset: 0x000326B7
		public Texture3D DensityTexture
		{
			get
			{
				return this.m_DensityTexture;
			}
			set
			{
				this.m_DensityTexture = value;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000CCC RID: 3276 RVA: 0x000342C0 File Offset: 0x000326C0
		// (set) Token: 0x06000CCD RID: 3277 RVA: 0x000342C8 File Offset: 0x000326C8
		public float DensityTextureScale
		{
			get
			{
				return this.m_DensityTextureScale;
			}
			set
			{
				this.m_DensityTextureScale = Mathf.Clamp01(this.m_DensityTextureScale);
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000CCE RID: 3278 RVA: 0x000342DB File Offset: 0x000326DB
		// (set) Token: 0x06000CCF RID: 3279 RVA: 0x000342E3 File Offset: 0x000326E3
		public Vector3 AnimateDirection
		{
			get
			{
				return this.m_AnimateDirection;
			}
			set
			{
				this.m_AnimateDirection = value.normalized;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000CD0 RID: 3280 RVA: 0x000342F2 File Offset: 0x000326F2
		// (set) Token: 0x06000CD1 RID: 3281 RVA: 0x000342FA File Offset: 0x000326FA
		public float AnimateSpeed
		{
			get
			{
				return this.m_AnimateSpeed;
			}
			set
			{
				this.m_AnimateSpeed = Mathf.Clamp01(value);
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000CD2 RID: 3282 RVA: 0x00034308 File Offset: 0x00032708
		// (set) Token: 0x06000CD3 RID: 3283 RVA: 0x00034310 File Offset: 0x00032710
		public float StartFade
		{
			get
			{
				return this.m_StartFade;
			}
			set
			{
				this.m_StartFade = ((value <= 0f) ? 1f : value);
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000CD4 RID: 3284 RVA: 0x0003432E File Offset: 0x0003272E
		// (set) Token: 0x06000CD5 RID: 3285 RVA: 0x00034336 File Offset: 0x00032736
		public float EndFade
		{
			get
			{
				return this.m_EndFade;
			}
			set
			{
				this.m_EndFade = ((value <= this.m_StartFade) ? (this.m_StartFade + 1f) : value);
			}
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x0003435C File Offset: 0x0003275C
		private void CreateProxyMeshCone(Mesh proxyMesh)
		{
			float num = Mathf.Tan(this.m_Light.spotAngle / 2f * 0.017453292f) * this.m_FarClip;
			Vector3[] array = new Vector3[DS_HazeLightVolume.kConeSubdivisions + 2];
			int[] array2 = new int[DS_HazeLightVolume.kConeSubdivisions * 6];
			float num2 = 6.2831855f / (float)DS_HazeLightVolume.kConeSubdivisions;
			float num3 = 0f;
			for (int i = 0; i < DS_HazeLightVolume.kConeSubdivisions; i++)
			{
				array[i] = new Vector3(Mathf.Sin(num3) * num, Mathf.Cos(num3) * num, this.m_FarClip);
				num3 += num2;
			}
			array[DS_HazeLightVolume.kConeSubdivisions] = new Vector3(0f, 0f, this.m_FarClip);
			array[DS_HazeLightVolume.kConeSubdivisions + 1] = new Vector3(0f, 0f, -0.1f);
			for (int j = 0; j < DS_HazeLightVolume.kConeSubdivisions; j++)
			{
				array2[j * 3] = DS_HazeLightVolume.kConeSubdivisions;
				array2[j * 3 + 1] = ((j != DS_HazeLightVolume.kConeSubdivisions - 1) ? (j + 1) : 0);
				array2[j * 3 + 2] = j;
				array2[DS_HazeLightVolume.kConeSubdivisions * 3 + j * 3] = j;
				array2[DS_HazeLightVolume.kConeSubdivisions * 3 + j * 3 + 1] = ((j != DS_HazeLightVolume.kConeSubdivisions - 1) ? (j + 1) : 0);
				array2[DS_HazeLightVolume.kConeSubdivisions * 3 + j * 3 + 2] = DS_HazeLightVolume.kConeSubdivisions + 1;
			}
			proxyMesh.vertices = array;
			proxyMesh.triangles = array2;
			proxyMesh.hideFlags = HideFlags.HideAndDontSave;
			this.m_PreviousAngle = this.m_Light.spotAngle;
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x0003451C File Offset: 0x0003291C
		public bool ProxyMeshRequiresRebuild()
		{
			return !(this.m_Light == null) && (this.m_ProxyMesh == null || (this.m_Light.type == LightType.Spot && this.m_Light.spotAngle != this.m_PreviousAngle));
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x00034576 File Offset: 0x00032976
		public bool LightTypeChanged()
		{
			return !(this.m_Light == null) && this.m_Light.type != this.m_PreviousLightType;
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x000345A4 File Offset: 0x000329A4
		public void UpdateLightType()
		{
			this.m_VolumeMaterial.DisableKeyword("POINT_COOKIE");
			this.m_VolumeMaterial.DisableKeyword("SPOT_COOKIE");
			if (this.m_Light.type == LightType.Point)
			{
				this.m_VolumeMaterial.EnableKeyword("POINT");
				this.m_VolumeMaterial.DisableKeyword("SPOT");
			}
			else
			{
				if (this.m_Light.type != LightType.Spot)
				{
					base.enabled = false;
					return;
				}
				this.m_VolumeMaterial.EnableKeyword("SPOT");
				this.m_VolumeMaterial.DisableKeyword("POINT");
			}
			this.RebuildProxyMesh();
			this.m_PreviousLightType = this.m_Light.type;
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x0003465C File Offset: 0x00032A5C
		public void RebuildProxyMesh()
		{
			LightType type = this.m_Light.type;
			if (type != LightType.Point)
			{
				if (type != LightType.Spot)
				{
					base.enabled = false;
				}
				else
				{
					if (this.m_PreviousLightType == LightType.Point)
					{
						this.m_ProxyMesh = new Mesh();
					}
					else if (this.m_ProxyMesh != null)
					{
						this.m_ProxyMesh.Clear();
					}
					this.CreateProxyMeshCone(this.m_ProxyMesh);
				}
			}
			else
			{
				if (this.m_PreviousLightType != LightType.Point)
				{
					UnityEngine.Object.DestroyImmediate(this.m_ProxyMesh);
				}
				this.m_ProxyMesh = Resources.Load<Mesh>("DS_HazeMeshProxySphere");
			}
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x00034708 File Offset: 0x00032B08
		public bool ShadowModeChanged()
		{
			return !(this.m_Light == null) && this.m_Light.shadows != this.m_PreviousShadowMode;
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x00034734 File Offset: 0x00032B34
		public void UpdateShadowMode()
		{
			if (this.m_Light.shadows == LightShadows.None)
			{
				this.m_VolumeMaterial.DisableKeyword("SHADOWS_DEPTH");
				this.m_VolumeMaterial.DisableKeyword("SHADOWS_CUBE");
			}
			else if (this.m_Light.type == LightType.Point)
			{
				this.m_VolumeMaterial.EnableKeyword("SHADOWS_CUBE");
				this.m_VolumeMaterial.DisableKeyword("SHADOWS_DEPTH");
			}
			else if (this.m_Light.type == LightType.Spot)
			{
				this.m_VolumeMaterial.EnableKeyword("SHADOWS_DEPTH");
				this.m_VolumeMaterial.DisableKeyword("SHADOWS_CUBE");
			}
			this.m_PreviousShadowMode = this.m_Light.shadows;
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x000347F0 File Offset: 0x00032BF0
		public void Register()
		{
			DS_HazeCore instance = DS_HazeCore.Instance;
			if (!(instance == null))
			{
				instance.AddLightVolume(this);
			}
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x0003481C File Offset: 0x00032C1C
		public void Deregister()
		{
			DS_HazeCore instance = DS_HazeCore.Instance;
			if (instance != null)
			{
				instance.RemoveLightVolume(this);
			}
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x00034842 File Offset: 0x00032C42
		public bool WillRender(Vector3 cameraPos)
		{
			return base.isActiveAndEnabled & Vector3.Distance(cameraPos, base.transform.position) < this.m_EndFade;
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x00034864 File Offset: 0x00032C64
		private void Update()
		{
			this.m_DensityOffset -= this.m_AnimateDirection * this.m_AnimateSpeed * Time.deltaTime * 0.1f;
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x0003489C File Offset: 0x00032C9C
		private void OnEnable()
		{
			this.m_Light = base.GetComponent<Light>();
			if (this.m_Light == null)
			{
				base.enabled = false;
			}
			if (DS_HazeLightVolume.kLightVolumeShader == null)
			{
				DS_HazeLightVolume.kLightVolumeShader = Resources.Load<Shader>("DS_HazeLightVolume");
			}
			if (this.m_VolumeMaterial == null)
			{
				this.m_VolumeMaterial = new Material(DS_HazeLightVolume.kLightVolumeShader);
				this.m_VolumeMaterial.hideFlags = HideFlags.HideAndDontSave;
			}
			if (this.m_RenderCmd == null)
			{
				this.m_RenderCmd = new CommandBuffer();
				this.m_RenderCmd.name = base.gameObject.name + "_DS_Haze_RenderLightVolume";
				this.m_Light.AddCommandBuffer(LightEvent.AfterShadowMap, this.m_RenderCmd);
			}
			if (this.LightTypeChanged())
			{
				this.UpdateLightType();
			}
			else if (this.ProxyMeshRequiresRebuild())
			{
				this.RebuildProxyMesh();
			}
			if (this.ShadowModeChanged())
			{
				this.UpdateShadowMode();
			}
			this.Register();
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x000349A0 File Offset: 0x00032DA0
		private void OnDisable()
		{
			this.Deregister();
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x000349A8 File Offset: 0x00032DA8
		private void OnDestroy()
		{
			if (this.m_RenderCmd != null)
			{
				this.m_RenderCmd.Dispose();
			}
			this.Deregister();
			if (this.m_ProxyMesh != null && this.m_Light.type != LightType.Point)
			{
				UnityEngine.Object.DestroyImmediate(this.m_ProxyMesh);
			}
			if (this.m_VolumeMaterial != null)
			{
				UnityEngine.Object.DestroyImmediate(this.m_VolumeMaterial);
			}
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x00034A1C File Offset: 0x00032E1C
		private int SetShaderPassAndMatrix(Transform cameraTransform, int downSampleFactor, out Matrix4x4 worldMtx)
		{
			worldMtx = Matrix4x4.TRS(base.transform.position, base.transform.rotation, new Vector3(this.m_Light.range, this.m_Light.range, this.m_Light.range));
			int num = 0;
			if (this.m_Light.type == LightType.Spot)
			{
				float num2 = Mathf.Cos(this.m_Light.spotAngle / 2f * 0.017453292f);
				Vector3 normalized = (cameraTransform.position - base.transform.position).normalized;
				float num3 = Vector3.Dot(normalized, base.transform.forward);
				if (num3 > num2)
				{
					num = 1;
				}
				else
				{
					num = 2;
				}
			}
			if (downSampleFactor == 4)
			{
				num += 3;
			}
			if (this.m_Falloff == DS_LightFalloff.Quadratic)
			{
				num += 6;
			}
			if (this.m_UseFog)
			{
				num += 12;
			}
			return num;
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x00034B10 File Offset: 0x00032F10
		public void FillLightCommandBuffer(RenderTexture radianceTarget, Transform cameraTransform, int downSampleFactor)
		{
			this.m_RenderCmd.SetGlobalTexture("_ShadowMapTexture", BuiltinRenderTextureType.CurrentActive);
			Matrix4x4 matrix;
			int shaderPass = this.SetShaderPassAndMatrix(cameraTransform, downSampleFactor, out matrix);
			this.m_RenderCmd.SetRenderTarget(radianceTarget);
			this.m_RenderCmd.DrawMesh(this.m_ProxyMesh, matrix, this.m_VolumeMaterial, 0, shaderPass);
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x00034B6C File Offset: 0x00032F6C
		public void AddLightRenderCommand(Transform cameraTransform, CommandBuffer cmd, int downSampleFactor)
		{
			Matrix4x4 matrix;
			int shaderPass = this.SetShaderPassAndMatrix(cameraTransform, downSampleFactor, out matrix);
			cmd.DrawMesh(this.m_ProxyMesh, matrix, this.m_VolumeMaterial, 0, shaderPass);
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x00034B9C File Offset: 0x00032F9C
		public void SetupMaterialPerFrame(Matrix4x4 viewProjMtx, Matrix4x4 viewMtx, Transform cameraTransform, float offsetIndex)
		{
			this.m_VolumeMaterial.DisableKeyword("SAMPLES_4");
			this.m_VolumeMaterial.DisableKeyword("SAMPLES_8");
			this.m_VolumeMaterial.DisableKeyword("SAMPLES_16");
			this.m_VolumeMaterial.DisableKeyword("SAMPLES_32");
			switch (this.m_Samples)
			{
			case DS_SamplingQuality.x4:
				this.m_VolumeMaterial.EnableKeyword("SAMPLES_4");
				break;
			case DS_SamplingQuality.x8:
				this.m_VolumeMaterial.EnableKeyword("SAMPLES_8");
				break;
			case DS_SamplingQuality.x16:
				this.m_VolumeMaterial.EnableKeyword("SAMPLES_16");
				break;
			case DS_SamplingQuality.x32:
				this.m_VolumeMaterial.EnableKeyword("SAMPLES_32");
				break;
			default:
				this.m_VolumeMaterial.EnableKeyword("SAMPLES_16");
				break;
			}
			float b = 1f - Mathf.Clamp01((Vector3.Distance(cameraTransform.position, base.transform.position) - this.m_StartFade) / (this.m_EndFade - this.m_StartFade));
			this.m_VolumeMaterial.SetVector("_DS_HazeSamplingParams", new Vector4(offsetIndex, 0f, this.m_DensityTextureContrast, 0f));
			this.m_VolumeMaterial.SetVector("_DS_HazeCameraDirection", new Vector4(cameraTransform.forward.x, cameraTransform.forward.y, cameraTransform.forward.z, 1f));
			this.m_VolumeMaterial.SetColor("_DS_HazeLightVolumeColour", this.m_Light.color.linear * this.m_Light.intensity * b);
			this.m_VolumeMaterial.SetVector("_DS_HazeLightVolumeScattering", new Vector4(this.m_Scattering, this.m_SecondaryScattering, this.m_ScatteringDirection, Mathf.Clamp01(1f - this.m_SecondaryScattering)));
			this.m_VolumeMaterial.SetVector("_DS_HazeLightVolumeParams0", new Vector4(base.transform.position.x, base.transform.position.y, base.transform.position.z, this.m_Light.range));
			Matrix4x4 rhs = Matrix4x4.TRS(base.transform.position, base.transform.rotation, new Vector3(this.m_Light.range, this.m_Light.range, this.m_Light.range));
			this.m_VolumeMaterial.SetMatrix("_WorldViewProj", viewProjMtx * rhs);
			this.m_VolumeMaterial.SetMatrix("_WorldView", viewMtx * rhs);
			if (this.m_DensityTexture)
			{
				this.m_VolumeMaterial.EnableKeyword("DENSITY_TEXTURE");
				this.m_VolumeMaterial.SetTexture("_DensityTexture", this.m_DensityTexture);
				this.m_VolumeMaterial.SetVector("_DS_HazeDensityParams", new Vector4(this.m_DensityOffset.x, this.m_DensityOffset.y, this.m_DensityOffset.z, this.m_DensityTextureScale * 0.01f));
			}
			else
			{
				this.m_VolumeMaterial.DisableKeyword("DENSITY_TEXTURE");
			}
			bool flag = this.m_Light.shadows != LightShadows.None;
			if (this.m_Light.type == LightType.Point)
			{
				this.m_VolumeMaterial.DisableKeyword("SPOT_COOKIE");
				this.m_VolumeMaterial.DisableKeyword("SHADOWS_DEPTH");
				if (flag)
				{
					this.m_VolumeMaterial.EnableKeyword("SHADOWS_CUBE");
				}
				else
				{
					this.m_VolumeMaterial.DisableKeyword("SHADOWS_CUBE");
				}
				if (this.m_Light.cookie)
				{
					this.m_VolumeMaterial.EnableKeyword("POINT_COOKIE");
					this.m_VolumeMaterial.SetMatrix("_DS_Haze_WorldToCookie", base.transform.worldToLocalMatrix);
					this.m_VolumeMaterial.SetTexture("_LightTexture0", this.m_Light.cookie);
				}
				else
				{
					this.m_VolumeMaterial.DisableKeyword("POINT_COOKIE");
				}
			}
			else if (this.m_Light.type == LightType.Spot)
			{
				this.m_VolumeMaterial.DisableKeyword("POINT_COOKIE");
				this.m_VolumeMaterial.DisableKeyword("SHADOWS_CUBE");
				if (flag)
				{
					this.m_VolumeMaterial.EnableKeyword("SHADOWS_DEPTH");
					Matrix4x4 inverse = Matrix4x4.TRS(base.transform.position, base.transform.rotation, Vector3.one).inverse;
					Matrix4x4 lhs = Matrix4x4.TRS(new Vector3(0.5f, 0.5f, 0.5f), Quaternion.identity, new Vector3(0.5f, 0.5f, 0.5f));
					Matrix4x4 rhs2 = Matrix4x4.Perspective(this.m_Light.spotAngle, 1f, this.m_Light.range, this.m_Light.shadowNearPlane);
					Matrix4x4 matrix4x = lhs * rhs2;
					ref Matrix4x4 ptr = ref matrix4x;
					matrix4x[0, 2] = ptr[0, 2] * -1f;
					ptr = ref matrix4x;
					matrix4x[1, 2] = ptr[1, 2] * -1f;
					ptr = ref matrix4x;
					matrix4x[2, 2] = ptr[2, 2] * -1f;
					ptr = ref matrix4x;
					matrix4x[3, 2] = ptr[3, 2] * -1f;
					matrix4x *= inverse;
					this.m_VolumeMaterial.SetMatrix("_DS_Haze_WorldToShadow", matrix4x);
				}
				else
				{
					this.m_VolumeMaterial.DisableKeyword("SHADOWS_DEPTH");
				}
				float num = Mathf.Cos(this.m_Light.spotAngle / 2f * 0.017453292f);
				Vector3 lhs2 = base.transform.position + base.transform.forward * this.m_Light.range;
				float z = -Vector3.Dot(lhs2, base.transform.forward);
				this.m_VolumeMaterial.SetVector("_DS_HazeLightVolumeParams1", new Vector4(base.transform.forward.x, base.transform.forward.y, base.transform.forward.z, 1f));
				this.m_VolumeMaterial.SetVector("_DS_HazeLightVolumeParams2", new Vector4(num, 1f / num, z, 0f));
				if (this.m_Light.cookie)
				{
					this.m_VolumeMaterial.EnableKeyword("SPOT_COOKIE");
					Matrix4x4 inverse2 = Matrix4x4.TRS(base.transform.position, base.transform.rotation, Vector3.one).inverse;
					Matrix4x4 lhs3 = Matrix4x4.TRS(new Vector3(0.5f, 0.5f, 0f), Quaternion.identity, new Vector3(-0.5f, -0.5f, 1f));
					Matrix4x4 rhs3 = Matrix4x4.Perspective(this.m_Light.spotAngle, 1f, 0f, 1f);
					this.m_VolumeMaterial.SetMatrix("_DS_Haze_WorldToCookie", lhs3 * rhs3 * inverse2);
					this.m_VolumeMaterial.SetTexture("_LightTexture0", this.m_Light.cookie);
				}
				else
				{
					this.m_VolumeMaterial.DisableKeyword("SPOT_COOKIE");
				}
			}
		}

		// Token: 0x04000BEE RID: 3054
		private static int kConeSubdivisions = 16;

		// Token: 0x04000BEF RID: 3055
		private static Shader kLightVolumeShader;

		// Token: 0x04000BF0 RID: 3056
		private Light m_Light;

		// Token: 0x04000BF1 RID: 3057
		private Mesh m_ProxyMesh;

		// Token: 0x04000BF2 RID: 3058
		private Matrix4x4 m_LightVolumeTransform;

		// Token: 0x04000BF3 RID: 3059
		private CommandBuffer m_RenderCmd;

		// Token: 0x04000BF4 RID: 3060
		private Material m_VolumeMaterial;

		// Token: 0x04000BF5 RID: 3061
		private Vector3 m_DensityOffset = Vector3.zero;

		// Token: 0x04000BF6 RID: 3062
		[SerializeField]
		private DS_SamplingQuality m_Samples = DS_SamplingQuality.x16;

		// Token: 0x04000BF7 RID: 3063
		[SerializeField]
		private DS_LightFalloff m_Falloff;

		// Token: 0x04000BF8 RID: 3064
		[SerializeField]
		private bool m_UseFog;

		// Token: 0x04000BF9 RID: 3065
		[SerializeField]
		[Range(0f, 100f)]
		private float m_Scattering = 1f;

		// Token: 0x04000BFA RID: 3066
		[SerializeField]
		[Range(0f, 1f)]
		private float m_SecondaryScattering = 0.1f;

		// Token: 0x04000BFB RID: 3067
		[SerializeField]
		[Range(-1f, 1f)]
		private float m_ScatteringDirection = 0.75f;

		// Token: 0x04000BFC RID: 3068
		[SerializeField]
		private Texture3D m_DensityTexture;

		// Token: 0x04000BFD RID: 3069
		[SerializeField]
		[Range(0.1f, 10f)]
		private float m_DensityTextureScale = 1f;

		// Token: 0x04000BFE RID: 3070
		[SerializeField]
		[Range(0.1f, 3f)]
		private float m_DensityTextureContrast = 1f;

		// Token: 0x04000BFF RID: 3071
		[SerializeField]
		private Vector3 m_AnimateDirection = Vector3.zero;

		// Token: 0x04000C00 RID: 3072
		[SerializeField]
		[Range(0f, 10f)]
		private float m_AnimateSpeed = 1f;

		// Token: 0x04000C01 RID: 3073
		[SerializeField]
		private float m_StartFade = 25f;

		// Token: 0x04000C02 RID: 3074
		[SerializeField]
		private float m_EndFade = 30f;

		// Token: 0x04000C03 RID: 3075
		[SerializeField]
		[Range(0.01f, 1f)]
		private float m_FarClip = 1f;

		// Token: 0x04000C04 RID: 3076
		private LightType m_PreviousLightType = LightType.Point;

		// Token: 0x04000C05 RID: 3077
		private float m_PreviousAngle = 45f;

		// Token: 0x04000C06 RID: 3078
		private LightShadows m_PreviousShadowMode;
	}
}
