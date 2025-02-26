using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace LuxWater
{
	// Token: 0x020003E0 RID: 992
	[RequireComponent(typeof(Camera))]
	[ExecuteInEditMode]
	public class LuxWater_ProjectorRenderer : MonoBehaviour
	{
		// Token: 0x06001192 RID: 4498 RVA: 0x00067964 File Offset: 0x00065D64
		private void OnEnable()
		{
			this._LuxWater_FoamOverlayPID = Shader.PropertyToID("_LuxWater_FoamOverlay");
			this._LuxWater_NormalOverlayPID = Shader.PropertyToID("_LuxWater_NormalOverlay");
			LuxWater_ProjectorRenderer.cb_Foam = new CommandBuffer();
			LuxWater_ProjectorRenderer.cb_Foam.name = "Lux Water: Foam Overlay Buffer";
			LuxWater_ProjectorRenderer.cb_Normals = new CommandBuffer();
			LuxWater_ProjectorRenderer.cb_Normals.name = "Lux Water: Normal Overlay Buffer";
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x000679C4 File Offset: 0x00065DC4
		private void OnDisable()
		{
			if (this.ProjectedFoam != null)
			{
				UnityEngine.Object.DestroyImmediate(this.ProjectedFoam);
			}
			if (this.ProjectedNormals != null)
			{
				UnityEngine.Object.DestroyImmediate(this.ProjectedNormals);
			}
			if (this.defaultBump != null)
			{
				UnityEngine.Object.DestroyImmediate(this.defaultBump);
			}
			if (this.DebugMat != null)
			{
				UnityEngine.Object.DestroyImmediate(this.DebugMat);
			}
			if (LuxWater_ProjectorRenderer.cb_Foam != null && LuxWater_ProjectorRenderer.cb_Foam.sizeInBytes > 0)
			{
				LuxWater_ProjectorRenderer.cb_Foam.Clear();
				LuxWater_ProjectorRenderer.cb_Foam.Dispose();
			}
			if (LuxWater_ProjectorRenderer.cb_Normals != null && LuxWater_ProjectorRenderer.cb_Normals.sizeInBytes > 0)
			{
				LuxWater_ProjectorRenderer.cb_Normals.Clear();
				LuxWater_ProjectorRenderer.cb_Normals.Dispose();
			}
			Shader.DisableKeyword("USINGWATERPROJECTORS");
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x00067AA8 File Offset: 0x00065EA8
		private void OnPreCull()
		{
			this.cam = base.GetComponent<Camera>();
			int count = LuxWater_Projector.FoamProjectors.Count;
			int count2 = LuxWater_Projector.NormalProjectors.Count;
			if (count + count2 == 0)
			{
				if (LuxWater_ProjectorRenderer.cb_Foam != null)
				{
					LuxWater_ProjectorRenderer.cb_Foam.Clear();
				}
				if (LuxWater_ProjectorRenderer.cb_Normals != null)
				{
					LuxWater_ProjectorRenderer.cb_Normals.Clear();
				}
				Shader.DisableKeyword("USINGWATERPROJECTORS");
				return;
			}
			Shader.EnableKeyword("USINGWATERPROJECTORS");
			Matrix4x4 projectionMatrix = this.cam.projectionMatrix;
			Matrix4x4 worldToCameraMatrix = this.cam.worldToCameraMatrix;
			Matrix4x4 worldToProjectMatrix = projectionMatrix * worldToCameraMatrix;
			int pixelWidth = this.cam.pixelWidth;
			int pixelHeight = this.cam.pixelHeight;
			GeomUtil.CalculateFrustumPlanes(this.frustumPlanes, worldToProjectMatrix);
			int num = Mathf.FloorToInt((float)(pixelWidth / (int)this.FoamBufferResolution));
			int height = Mathf.FloorToInt((float)(pixelHeight / (int)this.FoamBufferResolution));
			if (!this.ProjectedFoam)
			{
				this.ProjectedFoam = new RenderTexture(num, height, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
			}
			else if (this.ProjectedFoam.width != num)
			{
				UnityEngine.Object.DestroyImmediate(this.ProjectedFoam);
				this.ProjectedFoam = new RenderTexture(num, height, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
			}
			GL.PushMatrix();
			GL.modelview = worldToCameraMatrix;
			GL.LoadProjectionMatrix(projectionMatrix);
			LuxWater_ProjectorRenderer.cb_Foam.Clear();
			LuxWater_ProjectorRenderer.cb_Foam.SetRenderTarget(this.ProjectedFoam);
			LuxWater_ProjectorRenderer.cb_Foam.ClearRenderTarget(true, true, new Color(0f, 0f, 0f, 0f), 1f);
			this.drawnFoamProjectors = 0;
			for (int i = 0; i < count; i++)
			{
				LuxWater_Projector luxWater_Projector = LuxWater_Projector.FoamProjectors[i];
				this.tempBounds = luxWater_Projector.m_Rend.bounds;
				if (GeometryUtility.TestPlanesAABB(this.frustumPlanes, this.tempBounds))
				{
					LuxWater_ProjectorRenderer.cb_Foam.DrawRenderer(luxWater_Projector.m_Rend, luxWater_Projector.m_Mat);
					this.drawnFoamProjectors++;
				}
			}
			Graphics.ExecuteCommandBuffer(LuxWater_ProjectorRenderer.cb_Foam);
			Shader.SetGlobalTexture(this._LuxWater_FoamOverlayPID, this.ProjectedFoam);
			num = Mathf.FloorToInt((float)(pixelWidth / (int)this.NormalBufferResolution));
			height = Mathf.FloorToInt((float)(pixelHeight / (int)this.NormalBufferResolution));
			if (!this.ProjectedNormals)
			{
				this.ProjectedNormals = new RenderTexture(num, height, 0, RenderTextureFormat.ARGBHalf, RenderTextureReadWrite.Linear);
			}
			else if (this.ProjectedNormals.width != num)
			{
				UnityEngine.Object.DestroyImmediate(this.ProjectedNormals);
				this.ProjectedNormals = new RenderTexture(num, height, 0, RenderTextureFormat.ARGBHalf, RenderTextureReadWrite.Linear);
			}
			LuxWater_ProjectorRenderer.cb_Normals.Clear();
			LuxWater_ProjectorRenderer.cb_Normals.SetRenderTarget(this.ProjectedNormals);
			LuxWater_ProjectorRenderer.cb_Normals.ClearRenderTarget(true, true, new Color(0f, 0f, 0f, 0f), 1f);
			this.drawnNormalProjectors = 0;
			for (int j = 0; j < count2; j++)
			{
				LuxWater_Projector luxWater_Projector2 = LuxWater_Projector.NormalProjectors[j];
				this.tempBounds = luxWater_Projector2.m_Rend.bounds;
				if (GeometryUtility.TestPlanesAABB(this.frustumPlanes, this.tempBounds))
				{
					LuxWater_ProjectorRenderer.cb_Normals.DrawRenderer(luxWater_Projector2.m_Rend, luxWater_Projector2.m_Mat);
					this.drawnNormalProjectors++;
				}
			}
			Graphics.ExecuteCommandBuffer(LuxWater_ProjectorRenderer.cb_Normals);
			Shader.SetGlobalTexture(this._LuxWater_NormalOverlayPID, this.ProjectedNormals);
			GL.PopMatrix();
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x00067E24 File Offset: 0x00066224
		private void OnDrawGizmos()
		{
			Camera component = base.GetComponent<Camera>();
			int num = 0;
			int num2 = (int)(component.aspect * 128f);
			if (this.DebugMat == null)
			{
				this.DebugMat = new Material(Shader.Find("Hidden/LuxWater_Debug"));
			}
			if (this.DebugNormalMat == null)
			{
				this.DebugNormalMat = new Material(Shader.Find("Hidden/LuxWater_DebugNormals"));
			}
			if (this.DebugFoamBuffer)
			{
				if (this.ProjectedFoam == null)
				{
					return;
				}
				GL.PushMatrix();
				GL.LoadPixelMatrix(0f, (float)Screen.width, (float)Screen.height, 0f);
				Graphics.DrawTexture(new Rect((float)num, 0f, (float)num2, 128f), this.ProjectedFoam, this.DebugMat);
				GL.PopMatrix();
				num = num2;
			}
			if (this.DebugNormalBuffer)
			{
				if (this.ProjectedNormals == null)
				{
					return;
				}
				GL.PushMatrix();
				GL.LoadPixelMatrix(0f, (float)Screen.width, (float)Screen.height, 0f);
				Graphics.DrawTexture(new Rect((float)num, 0f, (float)num2, 128f), this.ProjectedNormals, this.DebugNormalMat);
				GL.PopMatrix();
			}
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x00067F64 File Offset: 0x00066364
		private void OnGUI()
		{
			if (this.DebugStats)
			{
				int count = LuxWater_Projector.FoamProjectors.Count;
				int count2 = LuxWater_Projector.NormalProjectors.Count;
				TextAnchor alignment = GUI.skin.label.alignment;
				GUI.skin.label.alignment = TextAnchor.MiddleLeft;
				GUI.Label(new Rect(10f, 0f, 300f, 40f), string.Concat(new object[]
				{
					"Foam Projectors   [Registered] ",
					count,
					"  [Drawn] ",
					this.drawnFoamProjectors
				}));
				GUI.Label(new Rect(10f, 18f, 300f, 40f), string.Concat(new object[]
				{
					"Normal Projectors [Registered] ",
					count2,
					"  [Drawn] ",
					this.drawnNormalProjectors
				}));
				GUI.skin.label.alignment = alignment;
			}
		}

		// Token: 0x04001377 RID: 4983
		[Space(8f)]
		public LuxWater_ProjectorRenderer.BufferResolution FoamBufferResolution = LuxWater_ProjectorRenderer.BufferResolution.Full;

		// Token: 0x04001378 RID: 4984
		public LuxWater_ProjectorRenderer.BufferResolution NormalBufferResolution = LuxWater_ProjectorRenderer.BufferResolution.Full;

		// Token: 0x04001379 RID: 4985
		[Space(2f)]
		[Header("Debug")]
		[Space(4f)]
		public bool DebugFoamBuffer;

		// Token: 0x0400137A RID: 4986
		public bool DebugNormalBuffer;

		// Token: 0x0400137B RID: 4987
		public bool DebugStats;

		// Token: 0x0400137C RID: 4988
		private int drawnFoamProjectors;

		// Token: 0x0400137D RID: 4989
		private int drawnNormalProjectors;

		// Token: 0x0400137E RID: 4990
		private static CommandBuffer cb_Foam;

		// Token: 0x0400137F RID: 4991
		private static CommandBuffer cb_Normals;

		// Token: 0x04001380 RID: 4992
		private Camera cam;

		// Token: 0x04001381 RID: 4993
		private Transform camTransform;

		// Token: 0x04001382 RID: 4994
		private RenderTexture ProjectedFoam;

		// Token: 0x04001383 RID: 4995
		private RenderTexture ProjectedNormals;

		// Token: 0x04001384 RID: 4996
		private Texture2D defaultBump;

		// Token: 0x04001385 RID: 4997
		private Bounds tempBounds;

		// Token: 0x04001386 RID: 4998
		private int _LuxWater_FoamOverlayPID;

		// Token: 0x04001387 RID: 4999
		private int _LuxWater_NormalOverlayPID;

		// Token: 0x04001388 RID: 5000
		private Plane[] frustumPlanes = new Plane[6];

		// Token: 0x04001389 RID: 5001
		private Material DebugMat;

		// Token: 0x0400138A RID: 5002
		private Material DebugNormalMat;

		// Token: 0x020003E1 RID: 993
		public enum BufferResolution
		{
			// Token: 0x0400138C RID: 5004
			Full = 1,
			// Token: 0x0400138D RID: 5005
			Half,
			// Token: 0x0400138E RID: 5006
			Quarter = 4,
			// Token: 0x0400138F RID: 5007
			Eighth = 8
		}
	}
}
