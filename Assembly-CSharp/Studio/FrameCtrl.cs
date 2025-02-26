using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x0200123B RID: 4667
	public class FrameCtrl : MonoBehaviour
	{
		// Token: 0x06009986 RID: 39302 RVA: 0x003F3B9C File Offset: 0x003F1F9C
		public bool Load(string _file)
		{
			this.Release();
			string path = UserData.Path + "frame/" + _file;
			if (!File.Exists(path))
			{
				Singleton<Studio>.Instance.sceneInfo.frame = string.Empty;
				return false;
			}
			Texture texture = PngAssist.LoadTexture(path);
			if (texture == null)
			{
				return false;
			}
			this.imageFrame.texture = texture;
			this.imageFrame.enabled = true;
			this.cameraUI.enabled = true;
			Singleton<Studio>.Instance.sceneInfo.frame = _file;
			Resources.UnloadUnusedAssets();
			GC.Collect();
			return true;
		}

		// Token: 0x06009987 RID: 39303 RVA: 0x003F3C36 File Offset: 0x003F2036
		public void Release()
		{
			UnityEngine.Object.Destroy(this.imageFrame.texture);
			this.imageFrame.texture = null;
			this.imageFrame.enabled = false;
			this.cameraUI.enabled = false;
			Resources.UnloadUnusedAssets();
		}

		// Token: 0x04007AE0 RID: 31456
		[SerializeField]
		private Camera cameraUI;

		// Token: 0x04007AE1 RID: 31457
		[SerializeField]
		private RawImage imageFrame;
	}
}
