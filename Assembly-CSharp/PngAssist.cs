using System;
using System.IO;
using IllusionUtility.GetUtility;
using UnityEngine;

// Token: 0x0200083E RID: 2110
public static class PngAssist
{
	// Token: 0x060035E4 RID: 13796 RVA: 0x0013D810 File Offset: 0x0013BC10
	public static Sprite LoadSpriteFromFile(string path, int width, int height, Vector2 pivot)
	{
		if (!File.Exists(path))
		{
			return null;
		}
		Sprite result;
		using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
		{
			using (BinaryReader binaryReader = new BinaryReader(fileStream))
			{
				byte[] data = binaryReader.ReadBytes((int)binaryReader.BaseStream.Length);
				Texture2D texture2D = new Texture2D(width, height);
				if (null == texture2D)
				{
					result = null;
				}
				else
				{
					texture2D.LoadImage(data);
					if (width == 0 || height == 0)
					{
						width = texture2D.width;
						height = texture2D.height;
					}
					result = Sprite.Create(texture2D, new Rect(0f, 0f, (float)width, (float)height), pivot);
				}
			}
		}
		return result;
	}

	// Token: 0x060035E5 RID: 13797 RVA: 0x0013D8E8 File Offset: 0x0013BCE8
	public static Texture2D LoadTexture2DFromAssetBundle(string assetBundleName, string assetName)
	{
		TextAsset textAsset = CommonLib.LoadAsset<TextAsset>(assetBundleName, assetName, false, string.Empty);
		AssetBundleManager.UnloadAssetBundle(assetBundleName, true, null, false);
		Texture2D texture2D = new Texture2D(0, 0, TextureFormat.ARGB32, false, true);
		if (null == texture2D)
		{
			return null;
		}
		texture2D.LoadImage(textAsset.bytes);
		return texture2D;
	}

	// Token: 0x060035E6 RID: 13798 RVA: 0x0013D934 File Offset: 0x0013BD34
	public static Sprite LoadSpriteFromAssetBundle(string assetBundleName, string assetName, int width, int height, Vector2 pivot)
	{
		TextAsset textAsset = CommonLib.LoadAsset<TextAsset>(assetBundleName, assetName, false, string.Empty);
		AssetBundleManager.UnloadAssetBundle(assetBundleName, true, null, false);
		Texture2D texture2D = new Texture2D(width, height);
		if (null == texture2D)
		{
			return null;
		}
		texture2D.LoadImage(textAsset.bytes);
		if (width == 0 || height == 0)
		{
			width = texture2D.width;
			height = texture2D.height;
		}
		return Sprite.Create(texture2D, new Rect(0f, 0f, (float)width, (float)height), pivot);
	}

	// Token: 0x060035E7 RID: 13799 RVA: 0x0013D9B4 File Offset: 0x0013BDB4
	public static Texture2D ChangeTextureFromByte(byte[] data, int width = 0, int height = 0, TextureFormat format = TextureFormat.ARGB32, bool mipmap = false)
	{
		Texture2D texture2D = new Texture2D(width, height, format, mipmap);
		if (null == texture2D)
		{
			return null;
		}
		texture2D.LoadImage(data);
		return texture2D;
	}

	// Token: 0x060035E8 RID: 13800 RVA: 0x0013D9E4 File Offset: 0x0013BDE4
	public static void SavePng(BinaryWriter writer, int capW = 504, int capH = 704, int createW = 252, int createH = 352, float renderRate = 1f, bool drawBackSp = true, bool drawFrontSp = true)
	{
		byte[] array = null;
		PngAssist.CreatePng(ref array, capW, capH, createW, createH, renderRate, drawBackSp, drawFrontSp);
		if (array == null)
		{
			return;
		}
		writer.Write(array);
		array = null;
	}

	// Token: 0x060035E9 RID: 13801 RVA: 0x0013DA18 File Offset: 0x0013BE18
	public static void CreatePng(ref byte[] pngData, int capW = 504, int capH = 704, int createW = 252, int createH = 352, float renderRate = 1f, bool drawBackSp = true, bool drawFrontSp = true)
	{
		GameObject gameObject = GameObject.FindGameObjectWithTag("SpriteTop");
		Vector2 screenSize = ScreenInfo.GetScreenSize();
		float screenRate = ScreenInfo.GetScreenRate();
		float screenCorrectY = ScreenInfo.GetScreenCorrectY();
		float num = 720f * screenRate / screenSize.y;
		int num2 = (int)((float)capW * renderRate);
		int num3 = (int)((float)capH * renderRate);
		RenderTexture temporary;
		if (QualitySettings.antiAliasing == 0)
		{
			temporary = RenderTexture.GetTemporary((int)(1280f * renderRate / num), (int)(720f * renderRate / num), 24);
		}
		else
		{
			temporary = RenderTexture.GetTemporary((int)(1280f * renderRate / num), (int)(720f * renderRate / num), 24, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default, QualitySettings.antiAliasing);
		}
		if (drawBackSp && null != gameObject)
		{
			GameObject gameObject2 = gameObject.transform.FindLoop("BackSpCam");
			if (null != gameObject2)
			{
				Camera component = gameObject2.GetComponent<Camera>();
				if (null != component)
				{
					component.targetTexture = temporary;
					component.Render();
					component.targetTexture = null;
				}
			}
		}
		if (null != Camera.main)
		{
			Camera main = Camera.main;
			RenderTexture targetTexture = main.targetTexture;
			Rect rect = main.rect;
			main.targetTexture = temporary;
			main.Render();
			main.targetTexture = targetTexture;
			main.rect = rect;
		}
		if (drawFrontSp && null != gameObject)
		{
			GameObject gameObject3 = gameObject.transform.FindLoop("FrontSpCam");
			if (null != gameObject3)
			{
				Camera component2 = gameObject3.GetComponent<Camera>();
				if (null != component2)
				{
					component2.targetTexture = temporary;
					component2.Render();
					component2.targetTexture = null;
				}
			}
		}
		Texture2D texture2D = new Texture2D(num2, num3, TextureFormat.ARGB32, false, true);
		RenderTexture.active = temporary;
		float x = (float)(1280 - capW) / 2f * renderRate + (1280f / num - 1280f) * 0.5f * renderRate;
		float y = (float)(720 - capH) / 2f * renderRate + screenCorrectY / screenRate * renderRate;
		texture2D.ReadPixels(new Rect(x, y, (float)num2, (float)num3), 0, 0);
		texture2D.Apply();
		RenderTexture.active = null;
		RenderTexture.ReleaseTemporary(temporary);
		if (num2 != createW || num3 != createH)
		{
			TextureScale.Bilinear(texture2D, createW, createH);
		}
		pngData = texture2D.EncodeToPNG();
		UnityEngine.Object.Destroy(texture2D);
	}

	// Token: 0x060035EA RID: 13802 RVA: 0x0013DC88 File Offset: 0x0013C088
	public static void CreatePngScreen(ref byte[] pngData, int createW, int createH)
	{
		Vector2 screenSize = ScreenInfo.GetScreenSize();
		int num = (int)screenSize.x;
		int num2 = (int)screenSize.y;
		Texture2D texture2D = new Texture2D(num, num2, TextureFormat.RGB24, false);
		RenderTexture temporary;
		if (QualitySettings.antiAliasing == 0)
		{
			temporary = RenderTexture.GetTemporary(num, num2, 24);
		}
		else
		{
			temporary = RenderTexture.GetTemporary(num, num2, 24, RenderTextureFormat.Default, RenderTextureReadWrite.Default, QualitySettings.antiAliasing);
		}
		if (null != Camera.main)
		{
			Camera main = Camera.main;
			RenderTexture targetTexture = main.targetTexture;
			Rect rect = main.rect;
			main.targetTexture = temporary;
			main.Render();
			main.targetTexture = targetTexture;
			main.rect = rect;
		}
		RenderTexture.active = temporary;
		texture2D.ReadPixels(new Rect(0f, 0f, (float)num, (float)num2), 0, 0);
		texture2D.Apply();
		RenderTexture.active = null;
		RenderTexture.ReleaseTemporary(temporary);
		TextureScale.Bilinear(texture2D, createW, createH);
		pngData = texture2D.EncodeToPNG();
		UnityEngine.Object.Destroy(texture2D);
	}

	// Token: 0x060035EB RID: 13803 RVA: 0x0013DD84 File Offset: 0x0013C184
	public static Sprite LoadSpriteFromFile(string path)
	{
		Sprite result;
		using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
		{
			long pngSize = PngFile.GetPngSize(fileStream);
			if (pngSize == 0L)
			{
				result = null;
			}
			else
			{
				using (BinaryReader binaryReader = new BinaryReader(fileStream))
				{
					byte[] data = binaryReader.ReadBytes((int)pngSize);
					int num = 0;
					int num2 = 0;
					Texture2D texture2D = PngAssist.ChangeTextureFromPngByte(data, ref num, ref num2, TextureFormat.ARGB32, false);
					if (null == texture2D)
					{
						result = null;
					}
					else
					{
						result = Sprite.Create(texture2D, new Rect(0f, 0f, (float)num, (float)num2), new Vector2(0.5f, 0.5f));
					}
				}
			}
		}
		return result;
	}

	// Token: 0x060035EC RID: 13804 RVA: 0x0013DE54 File Offset: 0x0013C254
	public static Texture2D ChangeTextureFromPngByte(byte[] data, ref int width, ref int height, TextureFormat format = TextureFormat.ARGB32, bool mipmap = false)
	{
		Texture2D texture2D = new Texture2D(width, height, format, mipmap);
		if (null == texture2D)
		{
			return null;
		}
		texture2D.LoadImage(data);
		texture2D.Apply(true, true);
		width = texture2D.width;
		height = texture2D.height;
		return texture2D;
	}

	// Token: 0x060035ED RID: 13805 RVA: 0x0013DEA0 File Offset: 0x0013C2A0
	public static Texture2D LoadTexture(string _path)
	{
		Texture2D result;
		using (FileStream fileStream = new FileStream(_path, FileMode.Open, FileAccess.Read))
		{
			long pngSize = PngFile.GetPngSize(fileStream);
			if (pngSize == 0L)
			{
				result = null;
			}
			else
			{
				using (BinaryReader binaryReader = new BinaryReader(fileStream))
				{
					byte[] data = binaryReader.ReadBytes((int)pngSize);
					int num = 0;
					int num2 = 0;
					result = PngAssist.ChangeTextureFromPngByte(data, ref num, ref num2, TextureFormat.ARGB32, false);
				}
			}
		}
		return result;
	}
}
