using System;
using System.Collections.Generic;
using UnityEngine;

namespace usmooth
{
	// Token: 0x0200048C RID: 1164
	public class DataCollector
	{
		// Token: 0x0600156E RID: 5486 RVA: 0x00084640 File Offset: 0x00082A40
		public FrameData CollectFrameData()
		{
			this._visibleMaterials.Clear();
			this._visibleTextures.Clear();
			if (DataCollector.MainCamera == null)
			{
				GameObject gameObject = GameObject.Find("MainCamera");
				if (gameObject != null)
				{
					DataCollector.MainCamera = gameObject;
				}
			}
			this._currentFrame = new FrameData();
			this._currentFrame._frameCount = Time.frameCount;
			this._currentFrame._frameDeltaTime = Time.deltaTime;
			this._currentFrame._frameRealTime = Time.realtimeSinceStartup;
			this._currentFrame._frameStartTime = Time.time;
			this._meshLut.ClearLut();
			MeshRenderer[] array = UnityEngine.Object.FindObjectsOfType(typeof(MeshRenderer)) as MeshRenderer[];
			foreach (MeshRenderer meshRenderer in array)
			{
				if (meshRenderer.isVisible)
				{
					GameObject gameObject2 = meshRenderer.gameObject;
					if (this._meshLut.AddMesh(gameObject2))
					{
						this._currentFrame._frameMeshes.Add(gameObject2.GetInstanceID());
						this._nameLut[gameObject2.GetInstanceID()] = gameObject2.name;
						foreach (Material material in meshRenderer.sharedMaterials)
						{
							this.AddVisibleMaterial(material, meshRenderer.gameObject);
							if (material != null)
							{
								this.AddVisibleTexture(material.mainTexture, material);
							}
						}
					}
				}
			}
			this._frames.Add(this._currentFrame);
			return this._currentFrame;
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x000847D8 File Offset: 0x00082BD8
		public void WriteName(int instID, UsCmd cmd)
		{
			string value;
			if (this._nameLut.TryGetValue(instID, out value))
			{
				cmd.WriteInt32(instID);
				cmd.WriteStringStripped(value);
			}
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x00084808 File Offset: 0x00082C08
		private void AddVisibleMaterial(Material mat, GameObject gameobject)
		{
			if (mat != null)
			{
				if (!this._visibleMaterials.ContainsKey(mat))
				{
					this._visibleMaterials.Add(mat, new HashSet<GameObject>());
				}
				this._visibleMaterials[mat].Add(gameobject);
			}
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x00084858 File Offset: 0x00082C58
		private void AddVisibleTexture(Texture texture, Material ownerMat)
		{
			if (texture != null)
			{
				if (!this._visibleTextures.ContainsKey(texture))
				{
					this._visibleTextures.Add(texture, new HashSet<Material>());
				}
				this._visibleTextures[texture].Add(ownerMat);
				if (!this._textureSizeLut.ContainsKey(texture))
				{
					this._textureSizeLut[texture] = UsTextureUtil.CalculateTextureSizeBytes(texture);
				}
			}
		}

		// Token: 0x06001572 RID: 5490 RVA: 0x000848CC File Offset: 0x00082CCC
		public void DumpAllInfo()
		{
			string str = string.Empty;
			foreach (KeyValuePair<Material, HashSet<GameObject>> keyValuePair in this.VisibleMaterials)
			{
				str += string.Format("{0} {1} {2}\n", keyValuePair.Key.name, keyValuePair.Key.shader.name, keyValuePair.Value.Count);
			}
			string str2 = string.Empty;
			foreach (KeyValuePair<Texture, HashSet<Material>> keyValuePair2 in this.VisibleTextures)
			{
				Texture key = keyValuePair2.Key;
				str2 += string.Format("{0} {1} {2} {3} {4}\n", new object[]
				{
					key.name,
					key.width,
					key.height,
					keyValuePair2.Value.Count,
					UsTextureUtil.FormatSizeString(this._textureSizeLut[key] / 1024)
				});
			}
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x00084A2C File Offset: 0x00082E2C
		public UsCmd CreateMaterialCmd()
		{
			UsCmd usCmd = new UsCmd();
			usCmd.WriteNetCmd(eNetCmd.SV_FrameData_Material);
			usCmd.WriteInt32(this.VisibleMaterials.Count);
			foreach (KeyValuePair<Material, HashSet<GameObject>> keyValuePair in this.VisibleMaterials)
			{
				usCmd.WriteInt32(keyValuePair.Key.GetInstanceID());
				usCmd.WriteStringStripped(keyValuePair.Key.name);
				usCmd.WriteStringStripped(keyValuePair.Key.shader.name);
				usCmd.WriteInt32(keyValuePair.Value.Count);
				foreach (GameObject gameObject in keyValuePair.Value)
				{
					usCmd.WriteInt32(gameObject.GetInstanceID());
				}
			}
			return usCmd;
		}

		// Token: 0x06001574 RID: 5492 RVA: 0x00084B44 File Offset: 0x00082F44
		public UsCmd CreateTextureCmd()
		{
			UsCmd usCmd = new UsCmd();
			usCmd.WriteNetCmd(eNetCmd.SV_FrameData_Texture);
			usCmd.WriteInt32(this.VisibleTextures.Count);
			foreach (KeyValuePair<Texture, HashSet<Material>> keyValuePair in this.VisibleTextures)
			{
				usCmd.WriteInt32(keyValuePair.Key.GetInstanceID());
				usCmd.WriteStringStripped(keyValuePair.Key.name);
				usCmd.WriteString(string.Format("{0}x{1}", keyValuePair.Key.width, keyValuePair.Key.height));
				usCmd.WriteString(UsTextureUtil.FormatSizeString(this._textureSizeLut[keyValuePair.Key] / 1024));
				usCmd.WriteInt32(keyValuePair.Value.Count);
				foreach (Material material in keyValuePair.Value)
				{
					usCmd.WriteInt32(material.GetInstanceID());
				}
			}
			return usCmd;
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06001575 RID: 5493 RVA: 0x00084C9C File Offset: 0x0008309C
		public MeshLut MeshTable
		{
			get
			{
				return this._meshLut;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06001576 RID: 5494 RVA: 0x00084CA4 File Offset: 0x000830A4
		public Dictionary<Material, HashSet<GameObject>> VisibleMaterials
		{
			get
			{
				return this._visibleMaterials;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06001577 RID: 5495 RVA: 0x00084CAC File Offset: 0x000830AC
		public Dictionary<Texture, HashSet<Material>> VisibleTextures
		{
			get
			{
				return this._visibleTextures;
			}
		}

		// Token: 0x0400188C RID: 6284
		public static DataCollector Instance = new DataCollector();

		// Token: 0x0400188D RID: 6285
		public static GameObject MainCamera = null;

		// Token: 0x0400188E RID: 6286
		private FrameData _currentFrame;

		// Token: 0x0400188F RID: 6287
		private List<FrameData> _frames = new List<FrameData>();

		// Token: 0x04001890 RID: 6288
		private MeshLut _meshLut = new MeshLut();

		// Token: 0x04001891 RID: 6289
		private Dictionary<Material, HashSet<GameObject>> _visibleMaterials = new Dictionary<Material, HashSet<GameObject>>();

		// Token: 0x04001892 RID: 6290
		private Dictionary<Texture, HashSet<Material>> _visibleTextures = new Dictionary<Texture, HashSet<Material>>();

		// Token: 0x04001893 RID: 6291
		private Dictionary<int, string> _nameLut = new Dictionary<int, string>();

		// Token: 0x04001894 RID: 6292
		private Dictionary<Texture, int> _textureSizeLut = new Dictionary<Texture, int>();
	}
}
