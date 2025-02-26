using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x02000360 RID: 864
[AddComponentMenu("Enviro/Volume Light")]
[RequireComponent(typeof(Light))]
public class EnviroVolumeLight : MonoBehaviour
{
	// Token: 0x1400004F RID: 79
	// (add) Token: 0x06000F4A RID: 3914 RVA: 0x00052C54 File Offset: 0x00051054
	// (remove) Token: 0x06000F4B RID: 3915 RVA: 0x00052C8C File Offset: 0x0005108C
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event Action<EnviroSkyRendering, EnviroVolumeLight, CommandBuffer, Matrix4x4> CustomRenderEvent;

	// Token: 0x170000DF RID: 223
	// (get) Token: 0x06000F4C RID: 3916 RVA: 0x00052CC2 File Offset: 0x000510C2
	public Light Light
	{
		get
		{
			return this._light;
		}
	}

	// Token: 0x170000E0 RID: 224
	// (get) Token: 0x06000F4D RID: 3917 RVA: 0x00052CCA File Offset: 0x000510CA
	public Material VolumetricMaterial
	{
		get
		{
			return this._material;
		}
	}

	// Token: 0x06000F4E RID: 3918 RVA: 0x00052CD4 File Offset: 0x000510D4
	private void Start()
	{
		if (SystemInfo.graphicsDeviceType == GraphicsDeviceType.Direct3D11 || SystemInfo.graphicsDeviceType == GraphicsDeviceType.Direct3D12 || SystemInfo.graphicsDeviceType == GraphicsDeviceType.Metal || SystemInfo.graphicsDeviceType == GraphicsDeviceType.PlayStation4 || SystemInfo.graphicsDeviceType == GraphicsDeviceType.Vulkan || SystemInfo.graphicsDeviceType == GraphicsDeviceType.XboxOne)
		{
			this._reversedZ = true;
		}
		this._commandBuffer = new CommandBuffer();
		this._commandBuffer.name = "Light Command Buffer";
		this._cascadeShadowCommandBuffer = new CommandBuffer();
		this._cascadeShadowCommandBuffer.name = "Dir Light Command Buffer";
		this._cascadeShadowCommandBuffer.SetGlobalTexture("_CascadeShadowMapTexture", new RenderTargetIdentifier(BuiltinRenderTextureType.CurrentActive));
		this._light = base.GetComponent<Light>();
		if (this._light.type == LightType.Directional)
		{
			this._light.AddCommandBuffer(LightEvent.BeforeScreenspaceMask, this._commandBuffer);
			this._light.AddCommandBuffer(LightEvent.AfterShadowMap, this._cascadeShadowCommandBuffer);
		}
		else
		{
			this._light.AddCommandBuffer(LightEvent.AfterShadowMap, this._commandBuffer);
		}
		if (this.volumeLightShader == null)
		{
			this.volumeLightShader = Shader.Find("Enviro/VolumeLight");
		}
		if (this.volumeLightShader == null)
		{
			throw new Exception("Critical Error: \"Enviro/VolumeLight\" shader is missing.");
		}
		if (this._light.type != LightType.Directional)
		{
			this._material = new Material(this.volumeLightShader);
		}
	}

	// Token: 0x06000F4F RID: 3919 RVA: 0x00052E32 File Offset: 0x00051232
	private void OnEnable()
	{
		if (base.GetComponent<Light>() != null && base.GetComponent<Light>().type != LightType.Directional)
		{
			EnviroSkyRendering.PreRenderEvent += this.VolumetricLightRenderer_PreRenderEvent;
		}
	}

	// Token: 0x06000F50 RID: 3920 RVA: 0x00052E67 File Offset: 0x00051267
	private void OnDisable()
	{
		if (base.GetComponent<Light>() != null && base.GetComponent<Light>().type != LightType.Directional)
		{
			EnviroSkyRendering.PreRenderEvent -= this.VolumetricLightRenderer_PreRenderEvent;
		}
	}

	// Token: 0x06000F51 RID: 3921 RVA: 0x00052E9C File Offset: 0x0005129C
	public void OnDestroy()
	{
		UnityEngine.Object.Destroy(this._material);
	}

	// Token: 0x06000F52 RID: 3922 RVA: 0x00052EAC File Offset: 0x000512AC
	private void VolumetricLightRenderer_PreRenderEvent(EnviroSkyRendering renderer, Matrix4x4 viewProj, Matrix4x4 viewProjSP)
	{
		if (EnviroSky.instance == null)
		{
			return;
		}
		if (this._light == null || this._light.gameObject == null)
		{
			EnviroSkyRendering.PreRenderEvent -= this.VolumetricLightRenderer_PreRenderEvent;
		}
		if (!this._light.gameObject.activeInHierarchy || !this._light.enabled)
		{
			return;
		}
		this._material.SetVector("_CameraForward", Camera.current.transform.forward);
		this._material.SetInt("_SampleCount", this.SampleCount);
		this._material.SetVector("_NoiseVelocity", new Vector4(EnviroSky.instance.volumeLightSettings.noiseVelocity.x, EnviroSky.instance.volumeLightSettings.noiseVelocity.y) * EnviroSky.instance.volumeLightSettings.noiseScale);
		this._material.SetVector("_NoiseData", new Vector4(EnviroSky.instance.volumeLightSettings.noiseScale, EnviroSky.instance.volumeLightSettings.noiseIntensity, EnviroSky.instance.volumeLightSettings.noiseIntensityOffset));
		this._material.SetVector("_MieG", new Vector4(1f - this.Anistropy * this.Anistropy, 1f + this.Anistropy * this.Anistropy, 2f * this.Anistropy, 0.07957747f));
		float x = this.ScatteringCoef;
		if (this.scaleWithTime)
		{
			x = this.ScatteringCoef * (1f - EnviroSky.instance.GameTime.solarTime);
		}
		this._material.SetVector("_VolumetricLight", new Vector4(x, this.ExtinctionCoef, this._light.range, 1f));
		this._material.SetTexture("_CameraDepthTexture", renderer.GetVolumeLightDepthBuffer());
		this._material.SetFloat("_ZTest", 8f);
		if (this._light.type == LightType.Point)
		{
			this.SetupPointLight(renderer, viewProj, viewProjSP);
		}
		else if (this._light.type == LightType.Spot)
		{
			this.SetupSpotLight(renderer, viewProj, viewProjSP);
		}
	}

	// Token: 0x06000F53 RID: 3923 RVA: 0x00053100 File Offset: 0x00051500
	private void SetupPointLight(EnviroSkyRendering renderer, Matrix4x4 viewProj, Matrix4x4 viewProjSP)
	{
		this._commandBuffer.Clear();
		int num = 0;
		if (!this.IsCameraInPointLightBounds())
		{
			num = 2;
		}
		this._material.SetPass(num);
		Mesh pointLightMesh = EnviroSkyRendering.GetPointLightMesh();
		float num2 = this._light.range * 2f;
		Matrix4x4 matrix4x = Matrix4x4.TRS(base.transform.position, this._light.transform.rotation, new Vector3(num2, num2, num2));
		this._material.SetMatrix("_WorldViewProj", viewProj * matrix4x);
		this._material.SetMatrix("_WorldViewProj_SP", viewProjSP * matrix4x);
		if (this.Noise)
		{
			this._material.EnableKeyword("NOISE");
		}
		else
		{
			this._material.DisableKeyword("NOISE");
		}
		this._material.SetVector("_LightPos", new Vector4(this._light.transform.position.x, this._light.transform.position.y, this._light.transform.position.z, 1f / (this._light.range * this._light.range)));
		this._material.SetColor("_LightColor", this._light.color * this._light.intensity);
		if (this._light.cookie == null)
		{
			this._material.EnableKeyword("POINT");
			this._material.DisableKeyword("POINT_COOKIE");
		}
		else
		{
			Matrix4x4 inverse = Matrix4x4.TRS(this._light.transform.position, this._light.transform.rotation, Vector3.one).inverse;
			this._material.SetMatrix("_MyLightMatrix0", inverse);
			this._material.EnableKeyword("POINT_COOKIE");
			this._material.DisableKeyword("POINT");
			this._material.SetTexture("_LightTexture0", this._light.cookie);
		}
		bool flag = false;
		if ((this._light.transform.position - EnviroSky.instance.PlayerCamera.transform.position).magnitude >= QualitySettings.shadowDistance)
		{
			flag = true;
		}
		if (this._light.shadows != LightShadows.None && !flag)
		{
			if (EnviroSky.instance.PlayerCamera.stereoEnabled)
			{
				if (EnviroSky.instance.singlePassVR)
				{
					this._material.EnableKeyword("SHADOWS_CUBE");
					this._commandBuffer.SetGlobalTexture("_ShadowMapTexture", BuiltinRenderTextureType.CurrentActive);
					this._commandBuffer.SetRenderTarget(renderer.GetVolumeLightBuffer());
					this._commandBuffer.DrawMesh(pointLightMesh, matrix4x, this._material, 0, num);
					if (this.CustomRenderEvent != null)
					{
						this.CustomRenderEvent(renderer, this, this._commandBuffer, viewProj);
					}
				}
				else
				{
					this._material.DisableKeyword("SHADOWS_CUBE");
					renderer.GlobalCommandBuffer.DrawMesh(pointLightMesh, matrix4x, this._material, 0, num);
					if (this.CustomRenderEvent != null)
					{
						this.CustomRenderEvent(renderer, this, renderer.GlobalCommandBuffer, viewProj);
					}
				}
			}
			else
			{
				this._material.EnableKeyword("SHADOWS_CUBE");
				this._commandBuffer.SetGlobalTexture("_ShadowMapTexture", BuiltinRenderTextureType.CurrentActive);
				this._commandBuffer.SetRenderTarget(renderer.GetVolumeLightBuffer());
				this._commandBuffer.DrawMesh(pointLightMesh, matrix4x, this._material, 0, num);
				if (this.CustomRenderEvent != null)
				{
					this.CustomRenderEvent(renderer, this, this._commandBuffer, viewProj);
				}
			}
		}
		else
		{
			this._material.DisableKeyword("SHADOWS_DEPTH");
			if (EnviroSky.instance.PlayerCamera.actualRenderingPath == RenderingPath.Forward)
			{
				renderer.GlobalCommandBufferForward.SetRenderTarget(renderer.GetVolumeLightBuffer());
				renderer.GlobalCommandBufferForward.DrawMesh(pointLightMesh, matrix4x, this._material, 0, num);
				if (this.CustomRenderEvent != null)
				{
					this.CustomRenderEvent(renderer, this, renderer.GlobalCommandBufferForward, viewProj);
				}
			}
			else
			{
				renderer.GlobalCommandBuffer.DrawMesh(pointLightMesh, matrix4x, this._material, 0, num);
				if (this.CustomRenderEvent != null)
				{
					this.CustomRenderEvent(renderer, this, renderer.GlobalCommandBuffer, viewProj);
				}
			}
		}
	}

	// Token: 0x06000F54 RID: 3924 RVA: 0x00053590 File Offset: 0x00051990
	private void SetupSpotLight(EnviroSkyRendering renderer, Matrix4x4 viewProj, Matrix4x4 viewProjSP)
	{
		this._commandBuffer.Clear();
		int shaderPass = 1;
		if (!this.IsCameraInSpotLightBounds())
		{
			shaderPass = 3;
		}
		Mesh spotLightMesh = EnviroSkyRendering.GetSpotLightMesh();
		float range = this._light.range;
		float num = Mathf.Tan((this._light.spotAngle + 1f) * 0.5f * 0.017453292f) * this._light.range;
		Matrix4x4 matrix4x = Matrix4x4.TRS(base.transform.position, base.transform.rotation, new Vector3(num, num, range));
		Matrix4x4 inverse = Matrix4x4.TRS(this._light.transform.position, this._light.transform.rotation, Vector3.one).inverse;
		Matrix4x4 lhs = Matrix4x4.TRS(new Vector3(0.5f, 0.5f, 0f), Quaternion.identity, new Vector3(-0.5f, -0.5f, 1f));
		Matrix4x4 rhs = Matrix4x4.Perspective(this._light.spotAngle, 1f, 0f, 1f);
		this._material.SetMatrix("_MyLightMatrix0", lhs * rhs * inverse);
		this._material.SetMatrix("_WorldViewProj", viewProj * matrix4x);
		this._material.SetMatrix("_WorldViewProj_SP", viewProjSP * matrix4x);
		this._material.SetVector("_LightPos", new Vector4(this._light.transform.position.x, this._light.transform.position.y, this._light.transform.position.z, 1f / (this._light.range * this._light.range)));
		this._material.SetVector("_LightColor", this._light.color * this._light.intensity);
		Vector3 position = base.transform.position;
		Vector3 forward = base.transform.forward;
		Vector3 lhs2 = position + forward * this._light.range;
		float value = -Vector3.Dot(lhs2, forward);
		this._material.SetFloat("_PlaneD", value);
		this._material.SetFloat("_CosAngle", Mathf.Cos((this._light.spotAngle + 1f) * 0.5f * 0.017453292f));
		this._material.SetVector("_ConeApex", new Vector4(position.x, position.y, position.z));
		this._material.SetVector("_ConeAxis", new Vector4(forward.x, forward.y, forward.z));
		this._material.EnableKeyword("SPOT");
		if (this.Noise)
		{
			this._material.EnableKeyword("NOISE");
		}
		else
		{
			this._material.DisableKeyword("NOISE");
		}
		if (this._light.cookie == null)
		{
			this._material.SetTexture("_LightTexture0", EnviroSkyRendering.GetDefaultSpotCookie());
		}
		else
		{
			this._material.SetTexture("_LightTexture0", this._light.cookie);
		}
		bool flag = false;
		if ((this._light.transform.position - EnviroSky.instance.PlayerCamera.transform.position).magnitude >= QualitySettings.shadowDistance)
		{
			flag = true;
		}
		if (this._light.shadows != LightShadows.None && !flag)
		{
			if (EnviroSky.instance.PlayerCamera.stereoEnabled)
			{
				if (EnviroSky.instance.singlePassVR)
				{
					lhs = Matrix4x4.TRS(new Vector3(0.5f, 0.5f, 0.5f), Quaternion.identity, new Vector3(0.5f, 0.5f, 0.5f));
					if (this._reversedZ)
					{
						rhs = Matrix4x4.Perspective(this._light.spotAngle, 1f, this._light.range, this._light.shadowNearPlane);
					}
					else
					{
						rhs = Matrix4x4.Perspective(this._light.spotAngle, 1f, this._light.shadowNearPlane, this._light.range);
					}
					Matrix4x4 lhs3 = lhs * rhs;
					ref Matrix4x4 ptr = ref lhs3;
					lhs3[0, 2] = ptr[0, 2] * -1f;
					ptr = ref lhs3;
					lhs3[1, 2] = ptr[1, 2] * -1f;
					ptr = ref lhs3;
					lhs3[2, 2] = ptr[2, 2] * -1f;
					ptr = ref lhs3;
					lhs3[3, 2] = ptr[3, 2] * -1f;
					this._material.SetMatrix("_MyWorld2Shadow", lhs3 * inverse);
					this._material.SetMatrix("_WorldView", lhs3 * inverse);
					this._material.EnableKeyword("SHADOWS_DEPTH");
					this._commandBuffer.SetGlobalTexture("_ShadowMapTexture", BuiltinRenderTextureType.CurrentActive);
					this._commandBuffer.SetRenderTarget(renderer.GetVolumeLightBuffer());
					this._commandBuffer.DrawMesh(spotLightMesh, matrix4x, this._material, 0, shaderPass);
					if (this.CustomRenderEvent != null)
					{
						this.CustomRenderEvent(renderer, this, this._commandBuffer, viewProj);
					}
				}
				else
				{
					this._material.DisableKeyword("SHADOWS_DEPTH");
					renderer.GlobalCommandBuffer.DrawMesh(spotLightMesh, matrix4x, this._material, 0, shaderPass);
					if (this.CustomRenderEvent != null)
					{
						this.CustomRenderEvent(renderer, this, renderer.GlobalCommandBuffer, viewProj);
					}
				}
			}
			else
			{
				lhs = Matrix4x4.TRS(new Vector3(0.5f, 0.5f, 0.5f), Quaternion.identity, new Vector3(0.5f, 0.5f, 0.5f));
				if (this._reversedZ)
				{
					rhs = Matrix4x4.Perspective(this._light.spotAngle, 1f, this._light.range, this._light.shadowNearPlane);
				}
				else
				{
					rhs = Matrix4x4.Perspective(this._light.spotAngle, 1f, this._light.shadowNearPlane, this._light.range);
				}
				Matrix4x4 lhs4 = lhs * rhs;
				ref Matrix4x4 ptr = ref lhs4;
				lhs4[0, 2] = ptr[0, 2] * -1f;
				ptr = ref lhs4;
				lhs4[1, 2] = ptr[1, 2] * -1f;
				ptr = ref lhs4;
				lhs4[2, 2] = ptr[2, 2] * -1f;
				ptr = ref lhs4;
				lhs4[3, 2] = ptr[3, 2] * -1f;
				this._material.SetMatrix("_MyWorld2Shadow", lhs4 * inverse);
				this._material.SetMatrix("_WorldView", lhs4 * inverse);
				this._material.EnableKeyword("SHADOWS_DEPTH");
				this._commandBuffer.SetGlobalTexture("_ShadowMapTexture", BuiltinRenderTextureType.CurrentActive);
				this._commandBuffer.SetRenderTarget(renderer.GetVolumeLightBuffer());
				this._commandBuffer.DrawMesh(spotLightMesh, matrix4x, this._material, 0, shaderPass);
				if (this.CustomRenderEvent != null)
				{
					this.CustomRenderEvent(renderer, this, this._commandBuffer, viewProj);
				}
			}
		}
		else
		{
			this._material.DisableKeyword("SHADOWS_DEPTH");
			if (EnviroSky.instance.PlayerCamera.actualRenderingPath == RenderingPath.Forward)
			{
				renderer.GlobalCommandBufferForward.SetRenderTarget(renderer.GetVolumeLightBuffer());
				renderer.GlobalCommandBufferForward.DrawMesh(spotLightMesh, matrix4x, this._material, 0, shaderPass);
				if (this.CustomRenderEvent != null)
				{
					this.CustomRenderEvent(renderer, this, renderer.GlobalCommandBufferForward, viewProj);
				}
			}
			else
			{
				renderer.GlobalCommandBuffer.DrawMesh(spotLightMesh, matrix4x, this._material, 0, shaderPass);
				if (this.CustomRenderEvent != null)
				{
					this.CustomRenderEvent(renderer, this, renderer.GlobalCommandBuffer, viewProj);
				}
			}
		}
	}

	// Token: 0x06000F55 RID: 3925 RVA: 0x00053DF4 File Offset: 0x000521F4
	private bool IsCameraInPointLightBounds()
	{
		float sqrMagnitude = (this._light.transform.position - EnviroSky.instance.PlayerCamera.transform.position).sqrMagnitude;
		float num = this._light.range + 1f;
		return sqrMagnitude < num * num;
	}

	// Token: 0x06000F56 RID: 3926 RVA: 0x00053E54 File Offset: 0x00052254
	private bool IsCameraInSpotLightBounds()
	{
		float num = Vector3.Dot(this._light.transform.forward, Camera.current.transform.position - this._light.transform.position);
		float num2 = this._light.range + 1f;
		if (num > num2)
		{
			return false;
		}
		float f = Vector3.Dot(base.transform.forward, (Camera.current.transform.position - this._light.transform.position).normalized);
		return Mathf.Acos(f) * 57.29578f <= (this._light.spotAngle + 3f) * 0.5f;
	}

	// Token: 0x04001076 RID: 4214
	private Light _light;

	// Token: 0x04001077 RID: 4215
	public Material _material;

	// Token: 0x04001078 RID: 4216
	public Shader volumeLightShader;

	// Token: 0x04001079 RID: 4217
	public Shader volumeLightBlurShader;

	// Token: 0x0400107A RID: 4218
	private CommandBuffer _commandBuffer;

	// Token: 0x0400107B RID: 4219
	private CommandBuffer _cascadeShadowCommandBuffer;

	// Token: 0x0400107C RID: 4220
	public RenderTexture temp;

	// Token: 0x0400107D RID: 4221
	[Range(1f, 64f)]
	public int SampleCount = 8;

	// Token: 0x0400107E RID: 4222
	public bool scaleWithTime = true;

	// Token: 0x0400107F RID: 4223
	[Range(0f, 1f)]
	public float ScatteringCoef = 0.5f;

	// Token: 0x04001080 RID: 4224
	[Range(0f, 0.1f)]
	public float ExtinctionCoef = 0.01f;

	// Token: 0x04001081 RID: 4225
	[Range(0f, 0.999f)]
	public float Anistropy = 0.1f;

	// Token: 0x04001082 RID: 4226
	[Header("3D Noise")]
	public bool Noise;

	// Token: 0x04001083 RID: 4227
	[HideInInspector]
	public float NoiseScale = 0.015f;

	// Token: 0x04001084 RID: 4228
	[HideInInspector]
	public float NoiseIntensity = 1f;

	// Token: 0x04001085 RID: 4229
	[HideInInspector]
	public float NoiseIntensityOffset = 0.3f;

	// Token: 0x04001086 RID: 4230
	[HideInInspector]
	public Vector2 NoiseVelocity = new Vector2(3f, 3f);

	// Token: 0x04001087 RID: 4231
	private bool _reversedZ;
}
