using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200104A RID: 4170
public class ScreenShotCamera : MonoBehaviour
{
	// Token: 0x17001EA8 RID: 7848
	// (get) Token: 0x06008C44 RID: 35908 RVA: 0x003ADB2E File Offset: 0x003ABF2E
	// (set) Token: 0x06008C45 RID: 35909 RVA: 0x003ADB36 File Offset: 0x003ABF36
	public RenderTexture renderTexture { get; private set; }

	// Token: 0x06008C46 RID: 35910 RVA: 0x003ADB40 File Offset: 0x003ABF40
	private IEnumerator Start()
	{
		base.enabled = false;
		do
		{
			this.renderTexture = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default);
			yield return null;
		}
		while (this.renderTexture == null);
		base.enabled = true;
		yield break;
	}

	// Token: 0x06008C47 RID: 35911 RVA: 0x003ADB5B File Offset: 0x003ABF5B
	private void OnRenderImage(RenderTexture src, RenderTexture dst)
	{
		Graphics.Blit(src, this.renderTexture);
		Graphics.Blit(src, dst);
	}

	// Token: 0x06008C48 RID: 35912 RVA: 0x003ADB70 File Offset: 0x003ABF70
	private void OnDestroy()
	{
		if (this.renderTexture)
		{
			this.renderTexture.Release();
			this.renderTexture = null;
		}
	}
}
