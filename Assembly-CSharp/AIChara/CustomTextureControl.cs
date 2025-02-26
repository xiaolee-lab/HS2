using System;
using Manager;
using UnityEngine;

namespace AIChara
{
	// Token: 0x020007E0 RID: 2016
	public class CustomTextureControl
	{
		// Token: 0x060031A6 RID: 12710 RVA: 0x0012E2E0 File Offset: 0x0012C6E0
		public CustomTextureControl(int num, string drawMatManifest, string drawMatABName, string drawMatName, Transform trfParent = null)
		{
			this.createCustomTex = new CustomTextureCreate[num];
			for (int i = 0; i < num; i++)
			{
				this.createCustomTex[i] = new CustomTextureCreate(trfParent);
			}
			this.matDraw = CommonLib.LoadAsset<Material>(drawMatABName, drawMatName, true, drawMatManifest);
			Singleton<Character>.Instance.AddLoadAssetBundle(drawMatABName, drawMatManifest);
		}

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x060031A7 RID: 12711 RVA: 0x0012E33C File Offset: 0x0012C73C
		// (set) Token: 0x060031A8 RID: 12712 RVA: 0x0012E344 File Offset: 0x0012C744
		public Material matDraw { get; private set; }

		// Token: 0x060031A9 RID: 12713 RVA: 0x0012E350 File Offset: 0x0012C750
		public bool Initialize(int index, string createMatManifest, string createMatABName, string createMatName, int width, int height, RenderTextureFormat format = RenderTextureFormat.ARGB32)
		{
			return this.createCustomTex != null && index < this.createCustomTex.Length && this.createCustomTex[index] != null && this.createCustomTex[index].Initialize(createMatManifest, createMatABName, createMatName, width, height, format);
		}

		// Token: 0x060031AA RID: 12714 RVA: 0x0012E3A8 File Offset: 0x0012C7A8
		public void Release()
		{
			foreach (CustomTextureCreate customTextureCreate in this.createCustomTex)
			{
				customTextureCreate.Release();
			}
			UnityEngine.Object.Destroy(this.matDraw);
			this.matDraw = null;
		}

		// Token: 0x060031AB RID: 12715 RVA: 0x0012E3EC File Offset: 0x0012C7EC
		public bool SetNewCreateTexture(int index, int propertyId)
		{
			if (this.createCustomTex == null)
			{
				return false;
			}
			if (index >= this.createCustomTex.Length || this.createCustomTex[index] == null)
			{
				return false;
			}
			this.createCustomTex[index].RebuildTextureAndSetMaterial();
			Texture createTexture = this.createCustomTex[index].GetCreateTexture();
			if (null != this.matDraw)
			{
				this.matDraw.SetTexture(propertyId, createTexture);
			}
			return true;
		}

		// Token: 0x040031E7 RID: 12775
		public CustomTextureCreate[] createCustomTex;
	}
}
