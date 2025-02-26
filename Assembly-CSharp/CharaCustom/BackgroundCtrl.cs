using System;
using AIChara;
using UnityEngine;

namespace CharaCustom
{
	// Token: 0x02000993 RID: 2451
	public class BackgroundCtrl : MonoBehaviour
	{
		// Token: 0x17000DB1 RID: 3505
		// (get) Token: 0x06004660 RID: 18016 RVA: 0x001AE8AC File Offset: 0x001ACCAC
		// (set) Token: 0x06004661 RID: 18017 RVA: 0x001AE8B4 File Offset: 0x001ACCB4
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

		// Token: 0x06004662 RID: 18018 RVA: 0x001AE8CC File Offset: 0x001ACCCC
		public bool ChangeBGImage(byte changeNo, bool listUpdate = true)
		{
			if (listUpdate)
			{
				string str = "bg";
				string[] searchPattern = new string[]
				{
					"*.png"
				};
				this.dirBG.lstFile.Clear();
				this.dirBG.CreateFolderInfoEx(DefaultData.Path + str, searchPattern, true);
				this.dirBG.CreateFolderInfoEx(UserData.Path + str, searchPattern, false);
				this.dirBG.SortName(true);
			}
			int fileCount = this.dirBG.GetFileCount();
			if (fileCount == 0)
			{
				return false;
			}
			int num = this.dirBG.GetIndexFromFileName(this.lastBGName);
			if (num == -1)
			{
				num = 0;
			}
			else if (changeNo == 0)
			{
				num = (num + 1) % fileCount;
			}
			else if (changeNo == 1)
			{
				num = (num + fileCount - 1) % fileCount;
			}
			Texture value = PngAssist.LoadTexture(this.dirBG.lstFile[num].FullPath);
			if (this.meshRenderer && this.meshRenderer.material)
			{
				Texture texture = this.meshRenderer.material.GetTexture(ChaShader.MainTex);
				if (texture)
				{
					UnityEngine.Object.Destroy(texture);
				}
				this.meshRenderer.material.SetTexture(ChaShader.MainTex, value);
			}
			this.lastBGName = this.dirBG.lstFile[num].FileName;
			return true;
		}

		// Token: 0x06004663 RID: 18019 RVA: 0x001AEA38 File Offset: 0x001ACE38
		private void Reflect()
		{
			if (null == this.backCam)
			{
				return;
			}
			Vector3[] vertices = this.meshFilter.mesh.vertices;
			float num = this.backCam.fieldOfView / 2f;
			float angle = Mathf.Atan(Mathf.Tan(0.017453292f * num) * this.backCam.aspect) * 57.29578f;
			Plane plane = new Plane(Vector3.back, this.backCam.farClipPlane - 2f);
			Vector3 vector = this.Raycast(plane, Vector3.forward);
			Vector3 vector2 = this.Raycast(plane, Quaternion.AngleAxis(angle, Vector3.up) * Vector3.forward);
			Vector3 vector3 = this.Raycast(plane, Quaternion.AngleAxis(num, Vector3.right) * Vector3.forward);
			if (this.type == 0)
			{
				vertices[0] = new Vector3(vector2.x, -vector3.y, vector.z);
				vertices[1] = new Vector3(-vector2.x, vector3.y, vector.z);
				vertices[2] = new Vector3(-vector2.x, -vector3.y, vector.z);
				vertices[3] = new Vector3(vector2.x, vector3.y, vector.z);
			}
			else
			{
				float num2 = 0.39375f;
				float num3 = 0.97777f;
				vertices[0] = new Vector3(vector2.x * num2, -vector3.y * num3, vector.z - 0.1f);
				vertices[1] = new Vector3(-vector2.x * num2, vector3.y * num3, vector.z - 0.1f);
				vertices[2] = new Vector3(-vector2.x * num2, -vector3.y * num3, vector.z - 0.1f);
				vertices[3] = new Vector3(vector2.x * num2, vector3.y * num3, vector.z - 0.1f);
			}
			this.meshFilter.mesh.vertices = vertices;
			this.meshFilter.mesh.RecalculateBounds();
		}

		// Token: 0x06004664 RID: 18020 RVA: 0x001AECAC File Offset: 0x001AD0AC
		private Vector3 Raycast(Plane _plane, Vector3 _dir)
		{
			float d = 0f;
			_plane.Raycast(new Ray(Vector3.zero, _dir), out d);
			return _dir * d;
		}

		// Token: 0x06004665 RID: 18021 RVA: 0x001AECDB File Offset: 0x001AD0DB
		private void Start()
		{
			this.isVisible = true;
		}

		// Token: 0x06004666 RID: 18022 RVA: 0x001AECE4 File Offset: 0x001AD0E4
		private void LateUpdate()
		{
			if (!this.isVisible)
			{
				return;
			}
			if (this.initialize)
			{
				this.Reflect();
				if (this.type == 0)
				{
					this.ChangeBGImage(0, true);
				}
				this.initialize = false;
			}
		}

		// Token: 0x04004177 RID: 16759
		[SerializeField]
		private MeshFilter meshFilter;

		// Token: 0x04004178 RID: 16760
		[SerializeField]
		private MeshRenderer meshRenderer;

		// Token: 0x04004179 RID: 16761
		[SerializeField]
		private Camera backCam;

		// Token: 0x0400417A RID: 16762
		[SerializeField]
		private int type;

		// Token: 0x0400417B RID: 16763
		private FolderAssist dirBG = new FolderAssist();

		// Token: 0x0400417C RID: 16764
		private string lastBGName = string.Empty;

		// Token: 0x0400417D RID: 16765
		private bool m_IsVisible;

		// Token: 0x0400417E RID: 16766
		private bool initialize = true;
	}
}
