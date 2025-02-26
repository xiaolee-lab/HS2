using System;
using UnityEngine;

// Token: 0x0200048E RID: 1166
public class UsTextureUtil
{
	// Token: 0x0600157E RID: 5502 RVA: 0x00084E20 File Offset: 0x00083220
	public static int GetBitsPerPixel(TextureFormat format)
	{
		switch (format)
		{
		case TextureFormat.Alpha8:
			return 8;
		case TextureFormat.ARGB4444:
			return 16;
		case TextureFormat.RGB24:
			return 24;
		case TextureFormat.RGBA32:
			return 32;
		case TextureFormat.ARGB32:
			return 32;
		default:
			switch (format)
			{
			case TextureFormat.PVRTC_RGB2:
				return 2;
			case TextureFormat.PVRTC_RGBA2:
				return 2;
			case TextureFormat.PVRTC_RGB4:
				return 4;
			case TextureFormat.PVRTC_RGBA4:
				return 4;
			case TextureFormat.ETC_RGB4:
				return 4;
			default:
				if (format != TextureFormat.ETC2_RGBA8)
				{
					return 0;
				}
				return 8;
			}
			break;
		case TextureFormat.RGB565:
			return 16;
		case TextureFormat.DXT1:
			return 4;
		case TextureFormat.DXT5:
			return 8;
		case TextureFormat.RGBA4444:
			return 16;
		case TextureFormat.BGRA32:
			return 32;
		}
	}

	// Token: 0x0600157F RID: 5503 RVA: 0x00084EC0 File Offset: 0x000832C0
	public static int CalculateTextureSizeBytes(Texture tTexture)
	{
		int num = tTexture.width;
		int num2 = tTexture.height;
		if (tTexture is Texture2D)
		{
			Texture2D texture2D = tTexture as Texture2D;
			int bitsPerPixel = UsTextureUtil.GetBitsPerPixel(texture2D.format);
			int mipmapCount = texture2D.mipmapCount;
			int i = 1;
			int num3 = 0;
			while (i <= mipmapCount)
			{
				num3 += num * num2 * bitsPerPixel / 8;
				num /= 2;
				num2 /= 2;
				i++;
			}
			return num3;
		}
		if (tTexture is Cubemap)
		{
			Cubemap cubemap = tTexture as Cubemap;
			int bitsPerPixel2 = UsTextureUtil.GetBitsPerPixel(cubemap.format);
			return num * num2 * 6 * bitsPerPixel2 / 8;
		}
		return 0;
	}

	// Token: 0x06001580 RID: 5504 RVA: 0x00084F5F File Offset: 0x0008335F
	public static string FormatSizeString(int memSizeKB)
	{
		return string.Empty + memSizeKB + "k";
	}
}
