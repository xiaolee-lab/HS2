using System;
using UnityEngine;

// Token: 0x02001182 RID: 4482
public class GameScreenShotAssist : MonoBehaviour
{
	// Token: 0x17001F80 RID: 8064
	// (get) Token: 0x060093E3 RID: 37859 RVA: 0x003D251D File Offset: 0x003D091D
	// (set) Token: 0x060093E4 RID: 37860 RVA: 0x003D2525 File Offset: 0x003D0925
	public RenderTexture rtCamera { get; private set; }

	// Token: 0x060093E5 RID: 37861 RVA: 0x003D252E File Offset: 0x003D092E
	private void Start()
	{
		this.rtCamera = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default);
	}

	// Token: 0x060093E6 RID: 37862 RVA: 0x003D2549 File Offset: 0x003D0949
	private void OnRenderImage(RenderTexture src, RenderTexture dst)
	{
		if (this.rtCamera == null)
		{
			Graphics.Blit(src, dst);
		}
		else
		{
			Graphics.Blit(src, this.rtCamera);
			Graphics.Blit(src, dst);
		}
	}

	// Token: 0x060093E7 RID: 37863 RVA: 0x003D257B File Offset: 0x003D097B
	private void OnDestroy()
	{
		if (this.rtCamera)
		{
			this.rtCamera.Release();
			this.rtCamera = null;
		}
	}
}
