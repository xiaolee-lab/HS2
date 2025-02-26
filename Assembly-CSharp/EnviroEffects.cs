using System;
using UnityEngine;

// Token: 0x02000325 RID: 805
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class EnviroEffects : MonoBehaviour
{
	// Token: 0x06000E2C RID: 3628 RVA: 0x00044DFC File Offset: 0x000431FC
	protected Material CheckShaderAndCreateMaterial(Shader s, Material m2Create)
	{
		if (!s)
		{
			base.enabled = false;
			return null;
		}
		if (s.isSupported && m2Create && m2Create.shader == s)
		{
			return m2Create;
		}
		if (!s.isSupported)
		{
			this.NotSupported();
			return null;
		}
		m2Create = new Material(s);
		m2Create.hideFlags = HideFlags.DontSave;
		if (m2Create)
		{
			return m2Create;
		}
		return null;
	}

	// Token: 0x06000E2D RID: 3629 RVA: 0x00044E78 File Offset: 0x00043278
	protected Material CreateMaterial(Shader s, Material m2Create)
	{
		if (!s)
		{
			return null;
		}
		if (m2Create && m2Create.shader == s && s.isSupported)
		{
			return m2Create;
		}
		if (!s.isSupported)
		{
			return null;
		}
		m2Create = new Material(s);
		m2Create.hideFlags = HideFlags.DontSave;
		if (m2Create)
		{
			return m2Create;
		}
		return null;
	}

	// Token: 0x06000E2E RID: 3630 RVA: 0x00044EE6 File Offset: 0x000432E6
	private void OnEnable()
	{
		this.isSupported = true;
	}

	// Token: 0x06000E2F RID: 3631 RVA: 0x00044EEF File Offset: 0x000432EF
	protected bool CheckSupport()
	{
		return this.CheckSupport(false);
	}

	// Token: 0x06000E30 RID: 3632 RVA: 0x00044EF8 File Offset: 0x000432F8
	public virtual bool CheckResources()
	{
		return this.isSupported;
	}

	// Token: 0x06000E31 RID: 3633 RVA: 0x00044F00 File Offset: 0x00043300
	protected void Start()
	{
		this.CheckResources();
	}

	// Token: 0x06000E32 RID: 3634 RVA: 0x00044F0C File Offset: 0x0004330C
	protected bool CheckSupport(bool needDepth)
	{
		this.isSupported = true;
		this.supportHDRTextures = SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGBHalf);
		this.supportDX11 = (SystemInfo.graphicsShaderLevel >= 50 && SystemInfo.supportsComputeShaders);
		if (!SystemInfo.supportsImageEffects)
		{
			this.NotSupported();
			return false;
		}
		if (needDepth && !SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.Depth))
		{
			this.NotSupported();
			return false;
		}
		if (needDepth)
		{
			base.GetComponent<Camera>().depthTextureMode |= DepthTextureMode.Depth;
		}
		return true;
	}

	// Token: 0x06000E33 RID: 3635 RVA: 0x00044F8B File Offset: 0x0004338B
	protected bool CheckSupport(bool needDepth, bool needHdr)
	{
		if (!this.CheckSupport(needDepth))
		{
			return false;
		}
		if (needHdr && !this.supportHDRTextures)
		{
			this.NotSupported();
			return false;
		}
		return true;
	}

	// Token: 0x06000E34 RID: 3636 RVA: 0x00044FB5 File Offset: 0x000433B5
	public bool Dx11Support()
	{
		return this.supportDX11;
	}

	// Token: 0x06000E35 RID: 3637 RVA: 0x00044FBD File Offset: 0x000433BD
	protected void ReportAutoDisable()
	{
	}

	// Token: 0x06000E36 RID: 3638 RVA: 0x00044FBF File Offset: 0x000433BF
	private bool CheckShader(Shader s)
	{
		if (!s.isSupported)
		{
			this.NotSupported();
			return false;
		}
		return false;
	}

	// Token: 0x06000E37 RID: 3639 RVA: 0x00044FD5 File Offset: 0x000433D5
	protected void NotSupported()
	{
		base.enabled = false;
		this.isSupported = false;
	}

	// Token: 0x06000E38 RID: 3640 RVA: 0x00044FE8 File Offset: 0x000433E8
	protected void DrawBorder(RenderTexture dest, Material material)
	{
		RenderTexture.active = dest;
		bool flag = true;
		GL.PushMatrix();
		GL.LoadOrtho();
		for (int i = 0; i < material.passCount; i++)
		{
			material.SetPass(i);
			float y;
			float y2;
			if (flag)
			{
				y = 1f;
				y2 = 0f;
			}
			else
			{
				y = 0f;
				y2 = 1f;
			}
			float x = 0f;
			float x2 = 1f / ((float)dest.width * 1f);
			float y3 = 0f;
			float y4 = 1f;
			GL.Begin(7);
			GL.TexCoord2(0f, y);
			GL.Vertex3(x, y3, 0.1f);
			GL.TexCoord2(1f, y);
			GL.Vertex3(x2, y3, 0.1f);
			GL.TexCoord2(1f, y2);
			GL.Vertex3(x2, y4, 0.1f);
			GL.TexCoord2(0f, y2);
			GL.Vertex3(x, y4, 0.1f);
			x = 1f - 1f / ((float)dest.width * 1f);
			x2 = 1f;
			y3 = 0f;
			y4 = 1f;
			GL.TexCoord2(0f, y);
			GL.Vertex3(x, y3, 0.1f);
			GL.TexCoord2(1f, y);
			GL.Vertex3(x2, y3, 0.1f);
			GL.TexCoord2(1f, y2);
			GL.Vertex3(x2, y4, 0.1f);
			GL.TexCoord2(0f, y2);
			GL.Vertex3(x, y4, 0.1f);
			x = 0f;
			x2 = 1f;
			y3 = 0f;
			y4 = 1f / ((float)dest.height * 1f);
			GL.TexCoord2(0f, y);
			GL.Vertex3(x, y3, 0.1f);
			GL.TexCoord2(1f, y);
			GL.Vertex3(x2, y3, 0.1f);
			GL.TexCoord2(1f, y2);
			GL.Vertex3(x2, y4, 0.1f);
			GL.TexCoord2(0f, y2);
			GL.Vertex3(x, y4, 0.1f);
			x = 0f;
			x2 = 1f;
			y3 = 1f - 1f / ((float)dest.height * 1f);
			y4 = 1f;
			GL.TexCoord2(0f, y);
			GL.Vertex3(x, y3, 0.1f);
			GL.TexCoord2(1f, y);
			GL.Vertex3(x2, y3, 0.1f);
			GL.TexCoord2(1f, y2);
			GL.Vertex3(x2, y4, 0.1f);
			GL.TexCoord2(0f, y2);
			GL.Vertex3(x, y4, 0.1f);
			GL.End();
		}
		GL.PopMatrix();
	}

	// Token: 0x04000E2C RID: 3628
	protected bool supportHDRTextures = true;

	// Token: 0x04000E2D RID: 3629
	protected bool supportDX11;

	// Token: 0x04000E2E RID: 3630
	protected bool isSupported = true;
}
