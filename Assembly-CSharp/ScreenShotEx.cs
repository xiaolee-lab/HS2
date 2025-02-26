using System;
using System.Collections;
using System.IO;
using UnityEngine;

// Token: 0x0200119C RID: 4508
public class ScreenShotEx : MonoBehaviour
{
	// Token: 0x06009466 RID: 37990 RVA: 0x003D3D90 File Offset: 0x003D2190
	private void Update()
	{
		if (this.capFlag)
		{
			this.capFlag = false;
			base.StartCoroutine(this.CaptureProc(this.ssinfo));
		}
	}

	// Token: 0x06009467 RID: 37991 RVA: 0x003D3DB8 File Offset: 0x003D21B8
	private IEnumerator CaptureProc(ScreenShotEx.SSInfo ssinfo)
	{
		yield return new WaitForEndOfFrame();
		int width = ((ssinfo.width != 0) ? ssinfo.width : Screen.width) * ssinfo.rate;
		int height = ((ssinfo.height != 0) ? ssinfo.height : Screen.height) * ssinfo.rate;
		Texture2D tex = new Texture2D(width, height, (!ssinfo.alpha) ? TextureFormat.RGB24 : TextureFormat.ARGB32, false);
		RenderTexture rt = null;
		if (QualitySettings.antiAliasing == 0)
		{
			rt = RenderTexture.GetTemporary(width, height, (!ssinfo.alpha) ? 24 : 32);
		}
		else
		{
			rt = RenderTexture.GetTemporary(width, height, (!ssinfo.alpha) ? 24 : 32, RenderTextureFormat.Default, RenderTextureReadWrite.Default, QualitySettings.antiAliasing);
		}
		Camera RenderCam = Camera.main;
		RenderTexture backRenderTexture = RenderCam.targetTexture;
		Rect backRect = RenderCam.rect;
		RenderCam.targetTexture = rt;
		RenderCam.Render();
		RenderCam.targetTexture = backRenderTexture;
		RenderCam.rect = backRect;
		RenderTexture.active = rt;
		tex.ReadPixels(new Rect(0f, 0f, (float)width, (float)height), 0, 0);
		tex.Apply();
		RenderTexture.active = null;
		byte[] bytes = tex.EncodeToPNG();
		UnityEngine.Object.Destroy(tex);
		RenderTexture.ReleaseTemporary(rt);
		tex = null;
		if (string.Empty == ssinfo.path)
		{
			ssinfo.path = UserData.Create("cap");
			string str = ".png";
			string text = DateTime.Now.Year.ToString("0000");
			text += DateTime.Now.Month.ToString("00");
			text += DateTime.Now.Day.ToString("00");
			text += DateTime.Now.Hour.ToString("00");
			text += DateTime.Now.Minute.ToString("00");
			text += DateTime.Now.Second.ToString("00");
			text += DateTime.Now.Millisecond.ToString("000");
			ssinfo.path = ssinfo.path + text + str;
		}
		File.WriteAllBytes(ssinfo.path, bytes);
		UnityEngine.Object.Destroy(base.gameObject);
		yield return null;
		yield break;
	}

	// Token: 0x06009468 RID: 37992 RVA: 0x003D3DDC File Offset: 0x003D21DC
	public static void Capture(string _path, bool _alpha, int _width, int _height, int _rate)
	{
		GameObject gameObject = new GameObject("CapExObj");
		if (gameObject)
		{
			ScreenShotEx screenShotEx = gameObject.AddComponent<ScreenShotEx>();
			screenShotEx.ssinfo.Set(_path, _alpha, _width, _height, _rate);
			screenShotEx.capFlag = true;
		}
	}

	// Token: 0x04007773 RID: 30579
	private bool capFlag;

	// Token: 0x04007774 RID: 30580
	private ScreenShotEx.SSInfo ssinfo = new ScreenShotEx.SSInfo();

	// Token: 0x0200119D RID: 4509
	public class SSInfo
	{
		// Token: 0x0600946A RID: 37994 RVA: 0x003D3E38 File Offset: 0x003D2238
		public void Set(string _path, bool _alpha, int _width, int _height, int _rate)
		{
			this.path = _path;
			this.alpha = _alpha;
			this.width = _width;
			this.height = _height;
			this.rate = _rate;
		}

		// Token: 0x04007775 RID: 30581
		public string path = string.Empty;

		// Token: 0x04007776 RID: 30582
		public bool alpha;

		// Token: 0x04007777 RID: 30583
		public int width;

		// Token: 0x04007778 RID: 30584
		public int height;

		// Token: 0x04007779 RID: 30585
		public int rate = 1;
	}
}
