using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200006D RID: 109
[AddComponentMenu("AQUAS/Reflection")]
[ExecuteInEditMode]
public class AQUAS_Reflection : MonoBehaviour
{
	// Token: 0x17000009 RID: 9
	// (get) Token: 0x0600009B RID: 155 RVA: 0x0000898E File Offset: 0x00006D8E
	// (set) Token: 0x0600009C RID: 156 RVA: 0x00008996 File Offset: 0x00006D96
	public Camera Camera
	{
		get
		{
			return this._camera;
		}
		set
		{
			this._camera = value;
			if (value != null)
			{
				this._skybox = value.GetComponent<Skybox>();
			}
		}
	}

	// Token: 0x0600009D RID: 157 RVA: 0x000089B8 File Offset: 0x00006DB8
	public void OnWillRenderObject()
	{
		if (this._renderer == null)
		{
			this._renderer = base.GetComponent<Renderer>();
		}
		if (!base.enabled || !this._renderer.enabled || this._renderer == null || this._renderer.sharedMaterial == null)
		{
			return;
		}
		if (!this._renderer.isVisible)
		{
			return;
		}
		Camera camera = this._camera;
		if (!camera)
		{
			return;
		}
		if (AQUAS_Reflection.s_InsideRendering)
		{
			return;
		}
		AQUAS_Reflection.s_InsideRendering = true;
		Camera camera2;
		Skybox destSkybox;
		this.CreateMirrorObjects(camera, out camera2, out destSkybox);
		Vector3 position = base.transform.position;
		Vector3 up = base.transform.up;
		int pixelLightCount = QualitySettings.pixelLightCount;
		if (this.m_DisablePixelLights)
		{
			QualitySettings.pixelLightCount = 0;
		}
		this.UpdateCameraModes(camera, camera2, destSkybox);
		float w = -Vector3.Dot(up, position) - this.m_ClipPlaneOffset;
		Vector4 plane = new Vector4(up.x, up.y, up.z, w);
		if (this.ignoreOcclusionCulling)
		{
			camera2.useOcclusionCulling = false;
		}
		else
		{
			camera2.useOcclusionCulling = true;
		}
		Matrix4x4 zero = Matrix4x4.zero;
		AQUAS_Reflection.CalculateReflectionMatrix(ref zero, plane);
		Vector3 position2 = camera.transform.position;
		Vector3 position3 = zero.MultiplyPoint(position2);
		camera2.worldToCameraMatrix = camera.worldToCameraMatrix * zero;
		Vector4 clipPlane = this.CameraSpacePlane(camera2, position, up, 1f);
		Matrix4x4 projectionMatrix = camera.projectionMatrix;
		AQUAS_Reflection.CalculateObliqueMatrix(ref projectionMatrix, clipPlane);
		camera2.projectionMatrix = projectionMatrix;
		camera2.cullingMask = (-17 & this.m_ReflectLayers.value);
		camera2.targetTexture = this.m_ReflectionTexture;
		GL.invertCulling = true;
		camera2.transform.position = position3;
		Vector3 eulerAngles = camera.transform.eulerAngles;
		camera2.transform.eulerAngles = new Vector3(0f, eulerAngles.y, eulerAngles.z);
		camera2.Render();
		camera2.transform.position = position2;
		GL.invertCulling = false;
		Material[] sharedMaterials = this._renderer.sharedMaterials;
		foreach (Material material in sharedMaterials)
		{
			if (material.HasProperty("_ReflectionTex"))
			{
				material.SetTexture("_ReflectionTex", this.m_ReflectionTexture);
			}
		}
		Matrix4x4 lhs = Matrix4x4.TRS(new Vector3(0.5f, 0.5f, 0.5f), Quaternion.identity, new Vector3(0.5f, 0.5f, 0.5f));
		Vector3 lossyScale = base.transform.lossyScale;
		Matrix4x4 matrix4x = base.transform.localToWorldMatrix * Matrix4x4.Scale(new Vector3(1f / lossyScale.x, 1f / lossyScale.y, 1f / lossyScale.z));
		matrix4x = lhs * camera.projectionMatrix * camera.worldToCameraMatrix * matrix4x;
		foreach (Material material2 in sharedMaterials)
		{
			material2.SetMatrix("_ProjMatrix", matrix4x);
		}
		if (this.m_DisablePixelLights)
		{
			QualitySettings.pixelLightCount = pixelLightCount;
		}
		AQUAS_Reflection.s_InsideRendering = false;
	}

	// Token: 0x0600009E RID: 158 RVA: 0x00008D14 File Offset: 0x00007114
	private void OnDisable()
	{
		if (this.m_ReflectionTexture)
		{
			UnityEngine.Object.DestroyImmediate(this.m_ReflectionTexture);
			this.m_ReflectionTexture = null;
		}
		IDictionaryEnumerator enumerator = this.m_ReflectionCameras.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				UnityEngine.Object.DestroyImmediate(((Camera)((DictionaryEntry)obj).Value).gameObject);
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
		this.m_ReflectionCameras.Clear();
	}

	// Token: 0x0600009F RID: 159 RVA: 0x00008DB8 File Offset: 0x000071B8
	private void UpdateCameraModes(Camera src, Camera dest, Skybox destSkybox)
	{
		if (dest == null)
		{
			return;
		}
		dest.clearFlags = src.clearFlags;
		dest.backgroundColor = src.backgroundColor;
		if (src.clearFlags == CameraClearFlags.Skybox)
		{
			Skybox skybox = this._skybox;
			if (!skybox || !skybox.material)
			{
				destSkybox.enabled = false;
			}
			else
			{
				destSkybox.enabled = true;
				destSkybox.material = skybox.material;
			}
		}
		dest.farClipPlane = src.farClipPlane;
		dest.nearClipPlane = src.nearClipPlane;
		dest.orthographic = src.orthographic;
		dest.fieldOfView = src.fieldOfView;
		dest.aspect = src.aspect;
		dest.orthographicSize = src.orthographicSize;
	}

	// Token: 0x060000A0 RID: 160 RVA: 0x00008E84 File Offset: 0x00007284
	private void CreateMirrorObjects(Camera currentCamera, out Camera reflectionCamera, out Skybox skybox)
	{
		reflectionCamera = null;
		if (!this.m_ReflectionTexture || this.m_OldReflectionTextureSize != this.m_TextureSize)
		{
			if (this.m_ReflectionTexture)
			{
				UnityEngine.Object.DestroyImmediate(this.m_ReflectionTexture);
			}
			this.m_ReflectionTexture = new RenderTexture(this.m_TextureSize, this.m_TextureSize, 16);
			this.m_ReflectionTexture.name = "__MirrorReflection" + base.GetInstanceID();
			this.m_ReflectionTexture.isPowerOfTwo = true;
			this.m_ReflectionTexture.hideFlags = HideFlags.DontSave;
			this.m_OldReflectionTextureSize = this.m_TextureSize;
		}
		reflectionCamera = (this.m_ReflectionCameras[currentCamera] as Camera);
		skybox = (this._reflectionSkyboxes[currentCamera] as Skybox);
		if (!reflectionCamera)
		{
			GameObject gameObject = new GameObject(string.Format("Mirror Refl Camera id {0} for {1}", base.GetInstanceID(), currentCamera.GetInstanceID()));
			reflectionCamera = gameObject.AddComponent<Camera>();
			reflectionCamera.enabled = false;
			reflectionCamera.transform.position = base.transform.position;
			reflectionCamera.transform.rotation = base.transform.rotation;
			reflectionCamera.gameObject.AddComponent<FlareLayer>();
			gameObject.hideFlags = HideFlags.HideAndDontSave;
			this.m_ReflectionCameras[currentCamera] = reflectionCamera;
			if (skybox == null)
			{
				skybox = gameObject.AddComponent<Skybox>();
				this._reflectionSkyboxes[currentCamera] = skybox;
			}
		}
	}

	// Token: 0x060000A1 RID: 161 RVA: 0x0000900A File Offset: 0x0000740A
	private static float sgn(float a)
	{
		if (a > 0f)
		{
			return 1f;
		}
		if (a < 0f)
		{
			return -1f;
		}
		return 0f;
	}

	// Token: 0x060000A2 RID: 162 RVA: 0x00009034 File Offset: 0x00007434
	private Vector4 CameraSpacePlane(Camera cam, Vector3 pos, Vector3 normal, float sideSign)
	{
		Vector3 point = pos + normal * this.m_ClipPlaneOffset;
		Matrix4x4 worldToCameraMatrix = cam.worldToCameraMatrix;
		Vector3 lhs = worldToCameraMatrix.MultiplyPoint(point);
		Vector3 rhs = worldToCameraMatrix.MultiplyVector(normal).normalized * sideSign;
		return new Vector4(rhs.x, rhs.y, rhs.z, -Vector3.Dot(lhs, rhs));
	}

	// Token: 0x060000A3 RID: 163 RVA: 0x000090A0 File Offset: 0x000074A0
	private static void CalculateObliqueMatrix(ref Matrix4x4 projection, Vector4 clipPlane)
	{
		Vector4 b = projection.inverse * new Vector4(AQUAS_Reflection.sgn(clipPlane.x), AQUAS_Reflection.sgn(clipPlane.y), 1f, 1f);
		Vector4 vector = clipPlane * (2f / Vector4.Dot(clipPlane, b));
		projection[2] = vector.x - projection[3];
		projection[6] = vector.y - projection[7];
		projection[10] = vector.z - projection[11];
		projection[14] = vector.w - projection[15];
	}

	// Token: 0x060000A4 RID: 164 RVA: 0x00009150 File Offset: 0x00007550
	private static void CalculateReflectionMatrix(ref Matrix4x4 reflectionMat, Vector4 plane)
	{
		reflectionMat.m00 = 1f - 2f * plane[0] * plane[0];
		reflectionMat.m01 = -2f * plane[0] * plane[1];
		reflectionMat.m02 = -2f * plane[0] * plane[2];
		reflectionMat.m03 = -2f * plane[3] * plane[0];
		reflectionMat.m10 = -2f * plane[1] * plane[0];
		reflectionMat.m11 = 1f - 2f * plane[1] * plane[1];
		reflectionMat.m12 = -2f * plane[1] * plane[2];
		reflectionMat.m13 = -2f * plane[3] * plane[1];
		reflectionMat.m20 = -2f * plane[2] * plane[0];
		reflectionMat.m21 = -2f * plane[2] * plane[1];
		reflectionMat.m22 = 1f - 2f * plane[2] * plane[2];
		reflectionMat.m23 = -2f * plane[3] * plane[2];
		reflectionMat.m30 = 0f;
		reflectionMat.m31 = 0f;
		reflectionMat.m32 = 0f;
		reflectionMat.m33 = 1f;
	}

	// Token: 0x040001DB RID: 475
	public bool m_DisablePixelLights = true;

	// Token: 0x040001DC RID: 476
	public int m_TextureSize = 256;

	// Token: 0x040001DD RID: 477
	public float m_ClipPlaneOffset = 0.07f;

	// Token: 0x040001DE RID: 478
	public LayerMask m_ReflectLayers = -1;

	// Token: 0x040001DF RID: 479
	[SerializeField]
	private Camera _camera;

	// Token: 0x040001E0 RID: 480
	private Skybox _skybox;

	// Token: 0x040001E1 RID: 481
	private Hashtable m_ReflectionCameras = new Hashtable();

	// Token: 0x040001E2 RID: 482
	private Hashtable _reflectionSkyboxes = new Hashtable();

	// Token: 0x040001E3 RID: 483
	private RenderTexture m_ReflectionTexture;

	// Token: 0x040001E4 RID: 484
	private int m_OldReflectionTextureSize;

	// Token: 0x040001E5 RID: 485
	private static bool s_InsideRendering;

	// Token: 0x040001E6 RID: 486
	public bool ignoreOcclusionCulling;

	// Token: 0x040001E7 RID: 487
	private Renderer _renderer;
}
