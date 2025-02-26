using System;
using System.Collections.Generic;
using UnityEngine;

namespace LuxWater
{
	// Token: 0x020003DA RID: 986
	[ExecuteInEditMode]
	public class LuxWater_PlanarReflection : MonoBehaviour
	{
		// Token: 0x06001179 RID: 4473 RVA: 0x00066E48 File Offset: 0x00065248
		private void OnEnable()
		{
			base.gameObject.layer = LayerMask.NameToLayer("Water");
			Renderer component = base.GetComponent<Renderer>();
			if (component != null)
			{
				this.m_SharedMaterial = base.GetComponent<Renderer>().sharedMaterial;
			}
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x00066E94 File Offset: 0x00065294
		private void OnDisable()
		{
			if (this.m_ReflectionCamera != null)
			{
				UnityEngine.Object.DestroyImmediate(this.m_ReflectionCamera.targetTexture);
				UnityEngine.Object.DestroyImmediate(this.m_ReflectionCamera);
			}
			if (this.m_HelperCameras != null)
			{
				this.m_HelperCameras.Clear();
			}
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x00066EE4 File Offset: 0x000652E4
		private Camera CreateReflectionCameraFor(Camera cam)
		{
			string name = base.gameObject.name + "Reflection" + cam.name;
			GameObject gameObject = GameObject.Find(name);
			if (!gameObject)
			{
				gameObject = new GameObject(name, new Type[]
				{
					typeof(Camera)
				});
				gameObject.hideFlags = HideFlags.HideAndDontSave;
			}
			if (!gameObject.GetComponent(typeof(Camera)))
			{
				gameObject.AddComponent(typeof(Camera));
			}
			Camera component = gameObject.GetComponent<Camera>();
			component.backgroundColor = this.clearColor;
			component.clearFlags = ((!this.reflectSkybox) ? CameraClearFlags.Color : CameraClearFlags.Skybox);
			this.SetStandardCameraParameter(component, this.reflectionMask);
			if (!component.targetTexture)
			{
				component.targetTexture = this.CreateTextureFor(cam);
			}
			return component;
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x00066FC2 File Offset: 0x000653C2
		private void SetStandardCameraParameter(Camera cam, LayerMask mask)
		{
			cam.cullingMask = (mask & ~(1 << LayerMask.NameToLayer("Water")));
			cam.backgroundColor = Color.black;
			cam.enabled = false;
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x00066FF4 File Offset: 0x000653F4
		private RenderTexture CreateTextureFor(Camera cam)
		{
			int width = Mathf.FloorToInt((float)(cam.pixelWidth / (int)this.Resolution));
			int height = Mathf.FloorToInt((float)(cam.pixelHeight / (int)this.Resolution));
			return new RenderTexture(width, height, 24)
			{
				hideFlags = HideFlags.DontSave
			};
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x0006703C File Offset: 0x0006543C
		public void RenderHelpCameras(Camera currentCam)
		{
			if (this.m_HelperCameras == null)
			{
				this.m_HelperCameras = new Dictionary<Camera, bool>();
			}
			if (!this.m_HelperCameras.ContainsKey(currentCam))
			{
				this.m_HelperCameras.Add(currentCam, false);
			}
			if (this.m_HelperCameras[currentCam])
			{
				return;
			}
			if (currentCam.name.Contains("Reflection Probes"))
			{
				return;
			}
			if (!this.m_ReflectionCamera)
			{
				this.m_ReflectionCamera = this.CreateReflectionCameraFor(currentCam);
			}
			this.RenderReflectionFor(currentCam, this.m_ReflectionCamera);
			this.m_HelperCameras[currentCam] = true;
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x000670DC File Offset: 0x000654DC
		public void LateUpdate()
		{
			if (this.m_HelperCameras != null)
			{
				this.m_HelperCameras.Clear();
			}
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x000670F4 File Offset: 0x000654F4
		public void WaterTileBeingRendered(Transform tr, Camera currentCam)
		{
			this.RenderHelpCameras(currentCam);
			if (this.m_ReflectionCamera && this.m_SharedMaterial)
			{
				this.m_SharedMaterial.SetTexture(this.reflectionSampler, this.m_ReflectionCamera.targetTexture);
			}
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x00067144 File Offset: 0x00065544
		public void OnWillRenderObject()
		{
			this.WaterTileBeingRendered(base.transform, Camera.current);
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x00067158 File Offset: 0x00065558
		private void RenderReflectionFor(Camera cam, Camera reflectCamera)
		{
			if (!reflectCamera)
			{
				return;
			}
			if (this.m_SharedMaterial && !this.m_SharedMaterial.HasProperty(this.reflectionSampler))
			{
				return;
			}
			reflectCamera.cullingMask = (this.reflectionMask & ~(1 << LayerMask.NameToLayer("Water")));
			this.SaneCameraSettings(reflectCamera);
			reflectCamera.backgroundColor = this.clearColor;
			reflectCamera.clearFlags = ((!this.reflectSkybox) ? CameraClearFlags.Color : CameraClearFlags.Skybox);
			GL.invertCulling = true;
			Transform transform = base.transform;
			Vector3 eulerAngles = cam.transform.eulerAngles;
			reflectCamera.transform.eulerAngles = new Vector3(-eulerAngles.x, eulerAngles.y, eulerAngles.z);
			reflectCamera.transform.position = cam.transform.position;
			reflectCamera.orthographic = cam.orthographic;
			reflectCamera.orthographicSize = cam.orthographicSize;
			Vector3 position = transform.transform.position;
			position.y = transform.position.y + this.WaterSurfaceOffset;
			Vector3 up = transform.transform.up;
			float w = -Vector3.Dot(up, position) - this.clipPlaneOffset;
			Vector4 plane = new Vector4(up.x, up.y, up.z, w);
			Matrix4x4 matrix4x = Matrix4x4.zero;
			matrix4x = LuxWater_PlanarReflection.CalculateReflectionMatrix(matrix4x, plane);
			this.m_Oldpos = cam.transform.position;
			Vector3 position2 = matrix4x.MultiplyPoint(this.m_Oldpos);
			reflectCamera.worldToCameraMatrix = cam.worldToCameraMatrix * matrix4x;
			Vector4 clipPlane = this.CameraSpacePlane(reflectCamera, position, up, 1f);
			Matrix4x4 matrix4x2 = cam.projectionMatrix;
			matrix4x2 = LuxWater_PlanarReflection.CalculateObliqueMatrix(matrix4x2, clipPlane);
			reflectCamera.projectionMatrix = matrix4x2;
			reflectCamera.transform.position = position2;
			Vector3 eulerAngles2 = cam.transform.eulerAngles;
			reflectCamera.transform.eulerAngles = new Vector3(-eulerAngles2.x, eulerAngles2.y, eulerAngles2.z);
			int pixelLightCount = QualitySettings.pixelLightCount;
			if (this.disablePixelLights)
			{
				QualitySettings.pixelLightCount = 0;
			}
			float num = QualitySettings.shadowDistance;
			int shadowCascades = QualitySettings.shadowCascades;
			if (!this.renderShadows)
			{
				QualitySettings.shadowDistance = 0f;
			}
			else if (this.shadowDistance > 0f)
			{
				QualitySettings.shadowDistance = this.shadowDistance;
			}
			QualitySettings.shadowCascades = (int)this.ShadowCascades;
			reflectCamera.Render();
			GL.invertCulling = false;
			if (this.disablePixelLights)
			{
				QualitySettings.pixelLightCount = pixelLightCount;
			}
			if (!this.renderShadows || this.shadowDistance > 0f)
			{
				QualitySettings.shadowDistance = num;
			}
			QualitySettings.shadowCascades = shadowCascades;
			if (this.isMaster)
			{
				for (int i = 0; i < this.WaterMaterials.Length; i++)
				{
					this.WaterMaterials[i].SetTexture(this.reflectionSampler, reflectCamera.targetTexture);
				}
			}
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x00067457 File Offset: 0x00065857
		private void SaneCameraSettings(Camera helperCam)
		{
			helperCam.depthTextureMode = DepthTextureMode.None;
			helperCam.backgroundColor = Color.black;
			helperCam.clearFlags = CameraClearFlags.Color;
			helperCam.renderingPath = RenderingPath.Forward;
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x0006747C File Offset: 0x0006587C
		private static Matrix4x4 CalculateObliqueMatrix(Matrix4x4 projection, Vector4 clipPlane)
		{
			Vector4 b = projection.inverse * new Vector4(LuxWater_PlanarReflection.Sgn(clipPlane.x), LuxWater_PlanarReflection.Sgn(clipPlane.y), 1f, 1f);
			Vector4 vector = clipPlane * (2f / Vector4.Dot(clipPlane, b));
			projection[2] = vector.x - projection[3];
			projection[6] = vector.y - projection[7];
			projection[10] = vector.z - projection[11];
			projection[14] = vector.w - projection[15];
			return projection;
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x00067538 File Offset: 0x00065938
		private static Matrix4x4 CalculateReflectionMatrix(Matrix4x4 reflectionMat, Vector4 plane)
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
			return reflectionMat;
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x000676F0 File Offset: 0x00065AF0
		private static float Sgn(float a)
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

		// Token: 0x06001187 RID: 4487 RVA: 0x0006771C File Offset: 0x00065B1C
		private Vector4 CameraSpacePlane(Camera cam, Vector3 pos, Vector3 normal, float sideSign)
		{
			Vector3 point = pos + normal * this.clipPlaneOffset;
			Matrix4x4 worldToCameraMatrix = cam.worldToCameraMatrix;
			Vector3 lhs = worldToCameraMatrix.MultiplyPoint(point);
			Vector3 rhs = worldToCameraMatrix.MultiplyVector(normal).normalized * sideSign;
			return new Vector4(rhs.x, rhs.y, rhs.z, -Vector3.Dot(lhs, rhs));
		}

		// Token: 0x04001350 RID: 4944
		[Space(6f)]
		[LuxWater_HelpBtn("h.5c3jy4qfh163")]
		public bool UpdateSceneView = true;

		// Token: 0x04001351 RID: 4945
		[Space(5f)]
		public bool isMaster;

		// Token: 0x04001352 RID: 4946
		public Material[] WaterMaterials;

		// Token: 0x04001353 RID: 4947
		[Space(5f)]
		public LayerMask reflectionMask = -1;

		// Token: 0x04001354 RID: 4948
		public LuxWater_PlanarReflection.ReflectionResolution Resolution = LuxWater_PlanarReflection.ReflectionResolution.Half;

		// Token: 0x04001355 RID: 4949
		public Color clearColor = Color.black;

		// Token: 0x04001356 RID: 4950
		public bool reflectSkybox = true;

		// Token: 0x04001357 RID: 4951
		[Space(5f)]
		public bool disablePixelLights;

		// Token: 0x04001358 RID: 4952
		[Space(5f)]
		public bool renderShadows = true;

		// Token: 0x04001359 RID: 4953
		public float shadowDistance;

		// Token: 0x0400135A RID: 4954
		public LuxWater_PlanarReflection.NumberOfShadowCascades ShadowCascades = LuxWater_PlanarReflection.NumberOfShadowCascades.One;

		// Token: 0x0400135B RID: 4955
		[Space(5f)]
		public float WaterSurfaceOffset;

		// Token: 0x0400135C RID: 4956
		public float clipPlaneOffset = 0.07f;

		// Token: 0x0400135D RID: 4957
		private string reflectionSampler = "_LuxWater_ReflectionTex";

		// Token: 0x0400135E RID: 4958
		private Vector3 m_Oldpos;

		// Token: 0x0400135F RID: 4959
		private Camera m_ReflectionCamera;

		// Token: 0x04001360 RID: 4960
		private Material m_SharedMaterial;

		// Token: 0x04001361 RID: 4961
		private Dictionary<Camera, bool> m_HelperCameras;

		// Token: 0x04001362 RID: 4962
		private RenderTexture m_reflectionMap;

		// Token: 0x020003DB RID: 987
		public enum ReflectionResolution
		{
			// Token: 0x04001364 RID: 4964
			Full = 1,
			// Token: 0x04001365 RID: 4965
			Half,
			// Token: 0x04001366 RID: 4966
			Quarter = 4,
			// Token: 0x04001367 RID: 4967
			Eighth = 8
		}

		// Token: 0x020003DC RID: 988
		public enum NumberOfShadowCascades
		{
			// Token: 0x04001369 RID: 4969
			One = 1,
			// Token: 0x0400136A RID: 4970
			Two,
			// Token: 0x0400136B RID: 4971
			Four = 4
		}
	}
}
