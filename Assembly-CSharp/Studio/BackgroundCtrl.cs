using System;
using System.IO;
using UnityEngine;

namespace Studio
{
	// Token: 0x020011CA RID: 4554
	public class BackgroundCtrl : MonoBehaviour
	{
		// Token: 0x17001F9F RID: 8095
		// (get) Token: 0x06009556 RID: 38230 RVA: 0x003DB29C File Offset: 0x003D969C
		// (set) Token: 0x06009557 RID: 38231 RVA: 0x003DB2A4 File Offset: 0x003D96A4
		public bool isVisible
		{
			get
			{
				return this.m_IsVisible;
			}
			set
			{
				this.m_IsVisible = value;
				this.meshRenderer.enabled = value;
			}
		}

		// Token: 0x17001FA0 RID: 8096
		// (get) Token: 0x06009558 RID: 38232 RVA: 0x003DB2B9 File Offset: 0x003D96B9
		private Camera mainCamera
		{
			get
			{
				if (this.m_Camera == null)
				{
					this.m_Camera = Camera.main;
				}
				return this.m_Camera;
			}
		}

		// Token: 0x06009559 RID: 38233 RVA: 0x003DB2E0 File Offset: 0x003D96E0
		public bool Load(string _file)
		{
			string path = UserData.Path + BackgroundList.dirName + "/" + _file;
			if (!File.Exists(path))
			{
				this.isVisible = false;
				Singleton<Studio>.Instance.sceneInfo.background = string.Empty;
				return false;
			}
			Texture texture = PngAssist.LoadTexture(path);
			if (texture == null)
			{
				this.isVisible = false;
				return false;
			}
			Material material = this.meshRenderer.material;
			material.SetTexture("_MainTex", texture);
			this.meshRenderer.material = material;
			this.isVisible = true;
			Singleton<Studio>.Instance.sceneInfo.background = _file;
			Resources.UnloadUnusedAssets();
			GC.Collect();
			return true;
		}

		// Token: 0x0600955A RID: 38234 RVA: 0x003DB390 File Offset: 0x003D9790
		private void Reflect()
		{
			Vector3[] vertices = this.meshFilter.mesh.vertices;
			float num = this.mainCamera.fieldOfView / 2f;
			float angle = Mathf.Atan(Mathf.Tan(0.017453292f * num) * this.mainCamera.aspect) * 57.29578f;
			Plane plane = new Plane(Vector3.back, this.mainCamera.farClipPlane * this.farRate);
			Vector3 vector = this.Raycast(plane, Vector3.forward);
			Vector3 vector2 = this.Raycast(plane, Quaternion.AngleAxis(angle, Vector3.up) * Vector3.forward);
			Vector3 vector3 = this.Raycast(plane, Quaternion.AngleAxis(num, Vector3.right) * Vector3.forward);
			vertices[0] = new Vector3(vector2.x, -vector3.y, vector.z);
			vertices[1] = new Vector3(-vector2.x, vector3.y, vector.z);
			vertices[2] = new Vector3(-vector2.x, -vector3.y, vector.z);
			vertices[3] = new Vector3(vector2.x, vector3.y, vector.z);
			this.meshFilter.mesh.vertices = vertices;
			this.meshFilter.mesh.RecalculateBounds();
			this.oldFOV = this.mainCamera.fieldOfView;
		}

		// Token: 0x0600955B RID: 38235 RVA: 0x003DB51C File Offset: 0x003D991C
		private Vector3 Raycast(Plane _plane, Vector3 _dir)
		{
			float d = 0f;
			_plane.Raycast(new Ray(Vector3.zero, _dir), out d);
			return _dir * d;
		}

		// Token: 0x0600955C RID: 38236 RVA: 0x003DB54B File Offset: 0x003D994B
		private void Start()
		{
			this.isVisible = false;
		}

		// Token: 0x0600955D RID: 38237 RVA: 0x003DB554 File Offset: 0x003D9954
		private void LateUpdate()
		{
			if (!this.isVisible)
			{
				return;
			}
			if (this.oldFOV != this.mainCamera.fieldOfView)
			{
				this.Reflect();
			}
		}

		// Token: 0x04007847 RID: 30791
		[SerializeField]
		private MeshFilter meshFilter;

		// Token: 0x04007848 RID: 30792
		[SerializeField]
		private MeshRenderer meshRenderer;

		// Token: 0x04007849 RID: 30793
		[SerializeField]
		[Range(0.01f, 1f)]
		private float farRate = 1f;

		// Token: 0x0400784A RID: 30794
		private bool m_IsVisible;

		// Token: 0x0400784B RID: 30795
		private Camera m_Camera;

		// Token: 0x0400784C RID: 30796
		private float oldFOV;
	}
}
