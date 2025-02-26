using System;
using System.Collections;
using System.IO;
using System.Text;
using Manager;
using UniRx;
using UnityEngine;

// Token: 0x02001181 RID: 4481
public class GameScreenShot : MonoBehaviour
{
	// Token: 0x060093DE RID: 37854 RVA: 0x003D1E40 File Offset: 0x003D0240
	public string CreateCaptureFileName()
	{
		StringBuilder stringBuilder = new StringBuilder(256);
		stringBuilder.Append(UserData.Create("cap"));
		DateTime now = DateTime.Now;
		stringBuilder.Append(now.Year.ToString("0000"));
		stringBuilder.Append(now.Month.ToString("00"));
		stringBuilder.Append(now.Day.ToString("00"));
		stringBuilder.Append(now.Hour.ToString("00"));
		stringBuilder.Append(now.Minute.ToString("00"));
		stringBuilder.Append(now.Second.ToString("00"));
		stringBuilder.Append(now.Millisecond.ToString("000"));
		stringBuilder.Append(".png");
		return stringBuilder.ToString();
	}

	// Token: 0x060093DF RID: 37855 RVA: 0x003D1F44 File Offset: 0x003D0344
	public void Capture(string path = "")
	{
		if (this.captureDisposable != null)
		{
			return;
		}
		if (Singleton<Scene>.IsInstance() && Singleton<Scene>.Instance.IsNowLoadingFade)
		{
			return;
		}
		bool isRenderSetCam = false;
		if (!this.capExMode)
		{
			if (this.renderCam.IsNullOrEmpty<Camera>())
			{
				if (Camera.main == null)
				{
					return;
				}
				isRenderSetCam = true;
				this.renderCam = new Camera[]
				{
					Camera.main
				};
			}
		}
		else if (this.scriptGssAssist.Length == 0)
		{
			return;
		}
		this.savePath = path;
		if (this.savePath == string.Empty)
		{
			this.savePath = this.CreateCaptureFileName();
		}
		this.captureDisposable = Observable.FromCoroutine(new Func<IEnumerator>(this.CaptureFunc), false).Subscribe(delegate(Unit _)
		{
			if (isRenderSetCam)
			{
				this.renderCam = null;
			}
			this.captureDisposable = null;
		});
	}

	// Token: 0x060093E0 RID: 37856 RVA: 0x003D2036 File Offset: 0x003D0436
	public void UnityCapture(string path = "")
	{
		this.savePath = path;
		if (string.Empty == this.savePath)
		{
			this.savePath = this.CreateCaptureFileName();
		}
		ScreenCapture.CaptureScreenshot(this.savePath, this.capRate);
	}

	// Token: 0x060093E1 RID: 37857 RVA: 0x003D2074 File Offset: 0x003D0474
	private IEnumerator CaptureFunc()
	{
		GameScreenShotOnGUI shotGUI = null;
		if (this.texCapMark != null)
		{
			shotGUI = this.GetOrAddComponent<GameScreenShotOnGUI>();
		}
		yield return new WaitForEndOfFrame();
		if (shotGUI != null)
		{
			UnityEngine.Object.Destroy(shotGUI);
		}
		float rate = (float)((this.capRate != 0) ? this.capRate : 1);
		Texture2D screenShot = new Texture2D((int)((float)Screen.width * rate), (int)((float)Screen.height * rate), (!this.modeARGB) ? TextureFormat.RGB24 : TextureFormat.ARGB32, false);
		int anti = (QualitySettings.antiAliasing != 0) ? QualitySettings.antiAliasing : 1;
		RenderTexture rt = RenderTexture.GetTemporary(screenShot.width, screenShot.height, 24, RenderTextureFormat.Default, RenderTextureReadWrite.Default, anti);
		Action drawCapMark = delegate()
		{
			float num = (float)Screen.width / 1920f;
			Graphics.DrawTexture(new Rect(this.texPosition.x * num, this.texPosition.y * num, (float)this.texCapMark.width * num, (float)this.texCapMark.height * num), this.texCapMark);
		};
		if (!this.capExMode)
		{
			Graphics.SetRenderTarget(rt);
			GL.Clear(true, true, Color.black);
			Graphics.SetRenderTarget(null);
			bool sRGBWrite = GL.sRGBWrite;
			GL.sRGBWrite = true;
			foreach (Camera camera in this.renderCam)
			{
				if (!(null == camera))
				{
					bool enabled = camera.enabled;
					RenderTexture targetTexture = camera.targetTexture;
					Rect rect = camera.rect;
					camera.enabled = true;
					camera.targetTexture = rt;
					camera.Render();
					camera.targetTexture = targetTexture;
					camera.rect = rect;
					camera.enabled = enabled;
				}
			}
			GL.sRGBWrite = sRGBWrite;
			if (this.texCapMark)
			{
				Graphics.SetRenderTarget(rt);
				drawCapMark();
				Graphics.SetRenderTarget(null);
			}
		}
		else
		{
			bool sRGBWrite2 = GL.sRGBWrite;
			GL.sRGBWrite = true;
			Graphics.Blit(this.scriptGssAssist[0].rtCamera, rt);
			GL.sRGBWrite = sRGBWrite2;
			for (int j = 1; j < this.scriptGssAssist.Length; j++)
			{
				Graphics.DrawTexture(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), this.scriptGssAssist[j].rtCamera);
			}
			if (this.texCapMark)
			{
				drawCapMark();
			}
		}
		RenderTexture.active = rt;
		screenShot.ReadPixels(new Rect(0f, 0f, (float)screenShot.width, (float)screenShot.height), 0, 0);
		screenShot.Apply();
		RenderTexture.active = null;
		byte[] bytes = screenShot.EncodeToPNG();
		RenderTexture.ReleaseTemporary(rt);
		UnityEngine.Object.Destroy(screenShot);
		screenShot = null;
		File.WriteAllBytes(this.savePath, bytes);
		yield return null;
		yield break;
	}

	// Token: 0x04007741 RID: 30529
	[Button("Capture", "キャプチャー", new object[]
	{
		""
	})]
	public int excuteCapture;

	// Token: 0x04007742 RID: 30530
	[Button("UnityCapture", "Unityキャプチャー", new object[]
	{
		""
	})]
	public int excuteCaptureEx;

	// Token: 0x04007743 RID: 30531
	public bool capExMode;

	// Token: 0x04007744 RID: 30532
	public bool modeARGB;

	// Token: 0x04007745 RID: 30533
	public Camera[] renderCam;

	// Token: 0x04007746 RID: 30534
	public GameScreenShotAssist[] scriptGssAssist;

	// Token: 0x04007747 RID: 30535
	public Texture texCapMark;

	// Token: 0x04007748 RID: 30536
	public Vector2 texPosition = new Vector2(1152f, 688f);

	// Token: 0x04007749 RID: 30537
	public int capRate = 1;

	// Token: 0x0400774A RID: 30538
	private string savePath = string.Empty;

	// Token: 0x0400774B RID: 30539
	private IDisposable captureDisposable;
}
