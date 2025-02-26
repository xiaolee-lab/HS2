using System;
using UnityEngine;

namespace CharaCustom
{
	// Token: 0x020009C9 RID: 2505
	public class CustomCapture : MonoBehaviour
	{
		// Token: 0x06004941 RID: 18753 RVA: 0x001BD8C0 File Offset: 0x001BBCC0
		public byte[] CapCharaCard(bool enableBG, SaveFrameAssist saveFrameAssist, bool forceHideBackFrame = false)
		{
			byte[] result = null;
			bool flag = !(null == saveFrameAssist) && saveFrameAssist.backFrameDraw;
			if (forceHideBackFrame)
			{
				flag = false;
			}
			bool flag2 = !(null == saveFrameAssist) && saveFrameAssist.frontFrameDraw;
			Camera camera = (!(null == saveFrameAssist)) ? ((!flag) ? null : saveFrameAssist.backFrameCam) : null;
			Camera camFrontFrame = (!(null == saveFrameAssist)) ? ((!flag2) ? null : saveFrameAssist.frontFrameCam) : null;
			CustomCapture.CreatePng(ref result, (!enableBG) ? null : this.camBG, camera, this.camMain, camFrontFrame);
			camera = ((!(null != saveFrameAssist)) ? null : saveFrameAssist.backFrameCam);
			if (null != camera)
			{
				camera.targetTexture = null;
			}
			return result;
		}

		// Token: 0x06004942 RID: 18754 RVA: 0x001BD9A4 File Offset: 0x001BBDA4
		public byte[] CapCoordinateCard(bool enableBG, SaveFrameAssist saveFrameAssist, Camera main)
		{
			byte[] result = null;
			bool flag = !(null == saveFrameAssist) && saveFrameAssist.backFrameDraw;
			bool flag2 = !(null == saveFrameAssist) && saveFrameAssist.frontFrameDraw;
			Camera camera = (!(null == saveFrameAssist)) ? ((!flag) ? null : saveFrameAssist.backFrameCam) : null;
			Camera camFrontFrame = (!(null == saveFrameAssist)) ? ((!flag2) ? null : saveFrameAssist.frontFrameCam) : null;
			CustomCapture.CreatePng(ref result, (!enableBG) ? null : this.camBG, camera, main, camFrontFrame);
			camera = ((!(null != saveFrameAssist)) ? null : saveFrameAssist.backFrameCam);
			if (null != camera)
			{
				camera.targetTexture = null;
			}
			if (null != this.camMain)
			{
				this.camMain.targetTexture = null;
			}
			if (null != this.camBG)
			{
				this.camBG.targetTexture = null;
			}
			return result;
		}

		// Token: 0x06004943 RID: 18755 RVA: 0x001BDAB4 File Offset: 0x001BBEB4
		public static void CreatePng(ref byte[] pngData, Camera _camBG = null, Camera _camBackFrame = null, Camera _camMain = null, Camera _camFrontFrame = null)
		{
			int num = 1280;
			int num2 = 720;
			int num3 = 504;
			int num4 = 704;
			RenderTexture temporary;
			if (QualitySettings.antiAliasing == 0)
			{
				temporary = RenderTexture.GetTemporary(num, num2, 24);
			}
			else
			{
				temporary = RenderTexture.GetTemporary(num, num2, 24, RenderTextureFormat.Default, RenderTextureReadWrite.Default, QualitySettings.antiAliasing);
			}
			bool sRGBWrite = GL.sRGBWrite;
			GL.sRGBWrite = true;
			if (null != _camMain)
			{
				RenderTexture targetTexture = _camMain.targetTexture;
				bool allowHDR = _camMain.allowHDR;
				_camMain.allowHDR = false;
				_camMain.targetTexture = temporary;
				_camMain.Render();
				_camMain.targetTexture = targetTexture;
				_camMain.allowHDR = allowHDR;
			}
			if (null != _camBG)
			{
				bool allowHDR2 = _camBG.allowHDR;
				_camBG.allowHDR = false;
				_camBG.targetTexture = temporary;
				_camBG.Render();
				_camBG.targetTexture = null;
				_camBG.allowHDR = allowHDR2;
			}
			if (null != _camBackFrame)
			{
				_camBackFrame.targetTexture = temporary;
				_camBackFrame.Render();
				_camBackFrame.targetTexture = null;
			}
			if (null != _camFrontFrame)
			{
				_camFrontFrame.targetTexture = temporary;
				_camFrontFrame.Render();
				_camFrontFrame.targetTexture = null;
			}
			GL.sRGBWrite = sRGBWrite;
			Texture2D texture2D = new Texture2D(num3, num4, TextureFormat.RGB24, false, true);
			RenderTexture.active = temporary;
			texture2D.ReadPixels(new Rect((float)(num - num3) / 2f, (float)(num2 - num4) / 2f, (float)num3, (float)num4), 0, 0);
			texture2D.Apply();
			RenderTexture.active = null;
			RenderTexture.ReleaseTemporary(temporary);
			TextureScale.Bilinear(texture2D, num3 / 2, num4 / 2);
			pngData = texture2D.EncodeToPNG();
			UnityEngine.Object.Destroy(texture2D);
		}

		// Token: 0x040043DA RID: 17370
		[SerializeField]
		private Camera camBG;

		// Token: 0x040043DB RID: 17371
		public Camera camMain;
	}
}
