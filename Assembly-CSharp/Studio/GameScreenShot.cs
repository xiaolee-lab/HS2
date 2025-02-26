using System;
using System.Collections;
using System.IO;
using System.Text;
using AIProject;
using Manager;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x0200120B RID: 4619
	[DefaultExecutionOrder(13000)]
	public class GameScreenShot : MonoBehaviour
	{
		// Token: 0x17001FFD RID: 8189
		// (get) Token: 0x06009730 RID: 38704 RVA: 0x003E84BD File Offset: 0x003E68BD
		// (set) Token: 0x06009731 RID: 38705 RVA: 0x003E84C5 File Offset: 0x003E68C5
		public bool modeARGB
		{
			get
			{
				return this._modeARGB;
			}
			set
			{
				this._modeARGB = value;
			}
		}

		// Token: 0x17001FFE RID: 8190
		// (get) Token: 0x06009732 RID: 38706 RVA: 0x003E84CE File Offset: 0x003E68CE
		// (set) Token: 0x06009733 RID: 38707 RVA: 0x003E84D6 File Offset: 0x003E68D6
		public int capRate
		{
			get
			{
				return this._capRate;
			}
			set
			{
				this._capRate = value;
			}
		}

		// Token: 0x17001FFF RID: 8191
		// (get) Token: 0x06009734 RID: 38708 RVA: 0x003E84DF File Offset: 0x003E68DF
		// (set) Token: 0x06009735 RID: 38709 RVA: 0x003E84E7 File Offset: 0x003E68E7
		public bool capMark
		{
			get
			{
				return this._capMark;
			}
			set
			{
				this._capMark = value;
			}
		}

		// Token: 0x06009736 RID: 38710 RVA: 0x003E84F0 File Offset: 0x003E68F0
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

		// Token: 0x06009737 RID: 38711 RVA: 0x003E85F4 File Offset: 0x003E69F4
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
			this.savePath = path;
			if (this.savePath == string.Empty)
			{
				this.savePath = this.CreateCaptureFileName();
			}
			this.captureDisposable = Observable.FromCoroutine(new Func<IEnumerator>(this.CaptureFunc), false).Subscribe(delegate(Unit _)
			{
				Utility.PlaySE(SoundPack.SystemSE.Photo);
			}, delegate()
			{
				this.captureDisposable = null;
			});
		}

		// Token: 0x06009738 RID: 38712 RVA: 0x003E8695 File Offset: 0x003E6A95
		public void UnityCapture(string path = "")
		{
			this.savePath = path;
			if (string.Empty == this.savePath)
			{
				this.savePath = this.CreateCaptureFileName();
			}
		}

		// Token: 0x06009739 RID: 38713 RVA: 0x003E86C0 File Offset: 0x003E6AC0
		public byte[] CreatePngScreen(int _width, int _height, bool _ARGB = false, bool _cap = false)
		{
			Texture2D texture2D = new Texture2D(_width, _height, (!_ARGB) ? TextureFormat.RGB24 : TextureFormat.ARGB32, false);
			int antiAliasing = (QualitySettings.antiAliasing != 0) ? QualitySettings.antiAliasing : 1;
			RenderTexture temporary = RenderTexture.GetTemporary(texture2D.width, texture2D.height, 24, RenderTextureFormat.Default, RenderTextureReadWrite.Default, antiAliasing);
			if (_cap)
			{
				this.imageCap.enabled = true;
			}
			Graphics.SetRenderTarget(temporary);
			GL.Clear(true, true, Color.black);
			Graphics.SetRenderTarget(null);
			bool sRGBWrite = GL.sRGBWrite;
			GL.sRGBWrite = true;
			foreach (Camera camera in this.renderCam)
			{
				if (!(null == camera))
				{
					int cullingMask = camera.cullingMask;
					camera.cullingMask &= ~(1 << LayerMask.NameToLayer("Studio/Camera"));
					bool enabled = camera.enabled;
					RenderTexture targetTexture = camera.targetTexture;
					Rect rect = camera.rect;
					camera.enabled = true;
					camera.targetTexture = temporary;
					camera.Render();
					camera.targetTexture = targetTexture;
					camera.rect = rect;
					camera.enabled = enabled;
					camera.cullingMask = cullingMask;
				}
			}
			if (_cap)
			{
				this.imageCap.enabled = false;
			}
			GL.sRGBWrite = sRGBWrite;
			RenderTexture.active = temporary;
			texture2D.ReadPixels(new Rect(0f, 0f, (float)texture2D.width, (float)texture2D.height), 0, 0);
			texture2D.Apply();
			RenderTexture.active = null;
			byte[] result = texture2D.EncodeToPNG();
			RenderTexture.ReleaseTemporary(temporary);
			UnityEngine.Object.Destroy(texture2D);
			UnityEngine.Resources.UnloadUnusedAssets();
			return result;
		}

		// Token: 0x0600973A RID: 38714 RVA: 0x003E886C File Offset: 0x003E6C6C
		private IEnumerator CaptureFunc()
		{
			if (this.captureBeforeFunc != null)
			{
				this.captureBeforeFunc();
			}
			yield return new WaitForEndOfFrame();
			float rate = (float)((this._capRate != 0) ? this._capRate : 1);
			byte[] bytes = this.CreatePngScreen((int)((float)Screen.width * rate), (int)((float)Screen.height * rate), this._modeARGB, this._capMark);
			File.WriteAllBytes(this.savePath, bytes);
			if (this.captureAfterFunc != null)
			{
				this.captureAfterFunc();
			}
			yield return null;
			yield break;
		}

		// Token: 0x04007958 RID: 31064
		[Button("Capture", "キャプチャー", new object[]
		{
			""
		})]
		public int excuteCapture;

		// Token: 0x04007959 RID: 31065
		[Button("UnityCapture", "Unityキャプチャー", new object[]
		{
			""
		})]
		public int excuteCaptureEx;

		// Token: 0x0400795A RID: 31066
		[SerializeField]
		private bool _modeARGB;

		// Token: 0x0400795B RID: 31067
		[SerializeField]
		private int _capRate = 1;

		// Token: 0x0400795C RID: 31068
		[SerializeField]
		private Camera[] renderCam;

		// Token: 0x0400795D RID: 31069
		[SerializeField]
		private Image imageCap;

		// Token: 0x0400795E RID: 31070
		[SerializeField]
		private bool _capMark = true;

		// Token: 0x0400795F RID: 31071
		public Action captureBeforeFunc;

		// Token: 0x04007960 RID: 31072
		public Action captureAfterFunc;

		// Token: 0x04007961 RID: 31073
		private string savePath = string.Empty;

		// Token: 0x04007962 RID: 31074
		private IDisposable captureDisposable;
	}
}
