using System;
using UnityEngine;

// Token: 0x02001177 RID: 4471
public class CrossFadeCreater
{
	// Token: 0x060093B3 RID: 37811 RVA: 0x003D12C0 File Offset: 0x003CF6C0
	private static CrossFadeObject Create()
	{
		GameObject gameObject = new GameObject("CrossFade");
		return gameObject.AddComponent<CrossFadeObject>();
	}

	// Token: 0x060093B4 RID: 37812 RVA: 0x003D12E0 File Offset: 0x003CF6E0
	public static RenderTexture Capture(Camera renderCamera)
	{
		RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
		renderTexture.enableRandomWrite = false;
		renderCamera.targetTexture = renderTexture;
		renderCamera.Render();
		renderCamera.targetTexture = null;
		return renderTexture;
	}
}
