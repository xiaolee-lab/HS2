using System;
using UnityEngine;

namespace AIChara
{
	// Token: 0x020007E1 RID: 2017
	public class CustomTextureCreate
	{
		// Token: 0x060031AC RID: 12716 RVA: 0x0012E45E File Offset: 0x0012C85E
		public CustomTextureCreate(Transform _trfParent = null)
		{
			this.trfParent = _trfParent;
		}

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x060031AD RID: 12717 RVA: 0x0012E46D File Offset: 0x0012C86D
		// (set) Token: 0x060031AE RID: 12718 RVA: 0x0012E475 File Offset: 0x0012C875
		public int baseW { get; private set; }

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x060031AF RID: 12719 RVA: 0x0012E47E File Offset: 0x0012C87E
		// (set) Token: 0x060031B0 RID: 12720 RVA: 0x0012E486 File Offset: 0x0012C886
		public int baseH { get; private set; }

		// Token: 0x060031B1 RID: 12721 RVA: 0x0012E490 File Offset: 0x0012C890
		public bool Initialize(string createMatManifest, string createMatABName, string createMatName, int width, int height, RenderTextureFormat format = RenderTextureFormat.ARGB32)
		{
			this.baseW = width;
			this.baseH = height;
			this.rtFormat = format;
			this.matCreate = CommonLib.LoadAsset<Material>(createMatABName, createMatName, true, string.Empty);
			if (null == this.matCreate)
			{
				return false;
			}
			this.texMain = this.matCreate.GetTexture(ChaShader.MainTex);
			this.createTex = new RenderTexture(this.baseW, this.baseH, 0, this.rtFormat);
			this.createTex.useMipMap = true;
			return true;
		}

		// Token: 0x060031B2 RID: 12722 RVA: 0x0012E51C File Offset: 0x0012C91C
		public void Release()
		{
			UnityEngine.Object.Destroy(this.createTex);
			this.createTex = null;
			UnityEngine.Object.Destroy(this.matCreate);
			this.matCreate = null;
		}

		// Token: 0x060031B3 RID: 12723 RVA: 0x0012E542 File Offset: 0x0012C942
		public void ReleaseCreateMaterial()
		{
			UnityEngine.Object.Destroy(this.matCreate);
			this.matCreate = null;
		}

		// Token: 0x060031B4 RID: 12724 RVA: 0x0012E556 File Offset: 0x0012C956
		public void SetMainTexture(Texture tex)
		{
			if (null == this.matCreate)
			{
				return;
			}
			this.texMain = tex;
		}

		// Token: 0x060031B5 RID: 12725 RVA: 0x0012E571 File Offset: 0x0012C971
		public void SetTexture(string propertyName, Texture tex)
		{
			if (null != this.matCreate)
			{
				this.matCreate.SetTexture(propertyName, tex);
			}
		}

		// Token: 0x060031B6 RID: 12726 RVA: 0x0012E591 File Offset: 0x0012C991
		public void SetTexture(int propertyID, Texture tex)
		{
			if (null != this.matCreate)
			{
				this.matCreate.SetTexture(propertyID, tex);
			}
		}

		// Token: 0x060031B7 RID: 12727 RVA: 0x0012E5B1 File Offset: 0x0012C9B1
		public void SetColor(string propertyName, Color color)
		{
			if (null != this.matCreate)
			{
				this.matCreate.SetColor(propertyName, color);
			}
		}

		// Token: 0x060031B8 RID: 12728 RVA: 0x0012E5D1 File Offset: 0x0012C9D1
		public Color GetColor(string propertyName)
		{
			return (!(null != this.matCreate)) ? Color.white : this.matCreate.GetColor(propertyName);
		}

		// Token: 0x060031B9 RID: 12729 RVA: 0x0012E5FA File Offset: 0x0012C9FA
		public void SetColor(int propertyID, Color color)
		{
			if (null != this.matCreate)
			{
				this.matCreate.SetColor(propertyID, color);
			}
		}

		// Token: 0x060031BA RID: 12730 RVA: 0x0012E61A File Offset: 0x0012CA1A
		public Color GetColor(int propertyID)
		{
			return (!(null != this.matCreate)) ? Color.white : this.matCreate.GetColor(propertyID);
		}

		// Token: 0x060031BB RID: 12731 RVA: 0x0012E643 File Offset: 0x0012CA43
		public void SetOffsetAndTilingDirect(string propertyName, float tx, float ty, float ox, float oy)
		{
			if (null == this.matCreate)
			{
				return;
			}
			this.matCreate.SetTextureOffset(propertyName, new Vector2(ox, oy));
			this.matCreate.SetTextureScale(propertyName, new Vector2(tx, ty));
		}

		// Token: 0x060031BC RID: 12732 RVA: 0x0012E67F File Offset: 0x0012CA7F
		public void SetOffsetAndTilingDirect(int propertyID, float tx, float ty, float ox, float oy)
		{
			if (null == this.matCreate)
			{
				return;
			}
			this.matCreate.SetTextureOffset(propertyID, new Vector2(ox, oy));
			this.matCreate.SetTextureScale(propertyID, new Vector2(tx, ty));
		}

		// Token: 0x060031BD RID: 12733 RVA: 0x0012E6BC File Offset: 0x0012CABC
		public void SetOffsetAndTiling(string propertyName, int addW, int addH, float addPx, float addPy)
		{
			if (null == this.matCreate)
			{
				return;
			}
			float num = (float)this.baseW / (float)addW;
			float num2 = (float)this.baseH / (float)addH;
			float ox = -(addPx / (float)this.baseW) * num;
			float oy = -(((float)this.baseH - addPy - (float)addH) / (float)this.baseH) * num2;
			this.SetOffsetAndTilingDirect(propertyName, num, num2, ox, oy);
		}

		// Token: 0x060031BE RID: 12734 RVA: 0x0012E728 File Offset: 0x0012CB28
		public void SetOffsetAndTiling(int propertyID, int addW, int addH, float addPx, float addPy)
		{
			if (null == this.matCreate)
			{
				return;
			}
			float num = (float)this.baseW / (float)addW;
			float num2 = (float)this.baseH / (float)addH;
			float ox = -(addPx / (float)this.baseW) * num;
			float oy = -(((float)this.baseH - addPy - (float)addH) / (float)this.baseH) * num2;
			this.SetOffsetAndTilingDirect(propertyID, num, num2, ox, oy);
		}

		// Token: 0x060031BF RID: 12735 RVA: 0x0012E791 File Offset: 0x0012CB91
		public void SetFloat(string propertyName, float value)
		{
			if (null != this.matCreate)
			{
				this.matCreate.SetFloat(propertyName, value);
			}
		}

		// Token: 0x060031C0 RID: 12736 RVA: 0x0012E7B1 File Offset: 0x0012CBB1
		public float GetFloat(string propertyName)
		{
			return (!(null != this.matCreate)) ? 0f : this.matCreate.GetFloat(propertyName);
		}

		// Token: 0x060031C1 RID: 12737 RVA: 0x0012E7DA File Offset: 0x0012CBDA
		public void SetFloat(int propertyID, float value)
		{
			if (null != this.matCreate)
			{
				this.matCreate.SetFloat(propertyID, value);
			}
		}

		// Token: 0x060031C2 RID: 12738 RVA: 0x0012E7FA File Offset: 0x0012CBFA
		public float GetFloat(int propertyID)
		{
			return (!(null != this.matCreate)) ? 0f : this.matCreate.GetFloat(propertyID);
		}

		// Token: 0x060031C3 RID: 12739 RVA: 0x0012E823 File Offset: 0x0012CC23
		public void SetVector4(string propertyName, Vector4 value)
		{
			if (null != this.matCreate)
			{
				this.matCreate.SetVector(propertyName, value);
			}
		}

		// Token: 0x060031C4 RID: 12740 RVA: 0x0012E843 File Offset: 0x0012CC43
		public Vector4 GetVector4(string propertyName)
		{
			return (!(null != this.matCreate)) ? Vector4.zero : this.matCreate.GetVector(propertyName);
		}

		// Token: 0x060031C5 RID: 12741 RVA: 0x0012E86C File Offset: 0x0012CC6C
		public void SetVector4(int propertyID, Vector4 value)
		{
			if (null != this.matCreate)
			{
				this.matCreate.SetVector(propertyID, value);
			}
		}

		// Token: 0x060031C6 RID: 12742 RVA: 0x0012E88C File Offset: 0x0012CC8C
		public Vector4 GetVector4(int propertyID)
		{
			return (!(null != this.matCreate)) ? Vector4.zero : this.matCreate.GetVector(propertyID);
		}

		// Token: 0x060031C7 RID: 12743 RVA: 0x0012E8B8 File Offset: 0x0012CCB8
		public Texture RebuildTextureAndSetMaterial()
		{
			if (null == this.matCreate)
			{
				return null;
			}
			bool sRGBWrite = GL.sRGBWrite;
			GL.sRGBWrite = true;
			Graphics.SetRenderTarget(this.createTex);
			GL.Clear(false, true, Color.clear);
			Graphics.SetRenderTarget(null);
			Graphics.Blit(this.texMain, this.createTex, this.matCreate, 0);
			GL.sRGBWrite = sRGBWrite;
			return this.createTex;
		}

		// Token: 0x060031C8 RID: 12744 RVA: 0x0012E925 File Offset: 0x0012CD25
		public Texture GetCreateTexture()
		{
			return this.createTex;
		}

		// Token: 0x040031E9 RID: 12777
		private RenderTextureFormat rtFormat;

		// Token: 0x040031EA RID: 12778
		private RenderTexture createTex;

		// Token: 0x040031EB RID: 12779
		private Material matCreate;

		// Token: 0x040031EC RID: 12780
		private Texture texMain;

		// Token: 0x040031EF RID: 12783
		public Transform trfParent;
	}
}
