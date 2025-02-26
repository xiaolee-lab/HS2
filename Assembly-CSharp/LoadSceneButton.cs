using System;
using PlayfulSystems.LoadingScreen;
using UnityEngine;

// Token: 0x02000631 RID: 1585
public class LoadSceneButton : MonoBehaviour
{
	// Token: 0x060025B0 RID: 9648 RVA: 0x000D73D0 File Offset: 0x000D57D0
	public void SetLoadingScene(string sceneName)
	{
		LoadingScreenConfig.loadingSceneName = sceneName;
		CameraFade cameraFade = base.gameObject.AddComponent<CameraFade>();
		cameraFade.Init();
		cameraFade.StartFadeTo(Color.black, 1f, new Action(this.LoadTargetScene));
	}

	// Token: 0x060025B1 RID: 9649 RVA: 0x000D7411 File Offset: 0x000D5811
	private void LoadTargetScene()
	{
		LoadingScreenProBase.LoadScene(this.targetSceneName);
	}

	// Token: 0x0400256C RID: 9580
	public string targetSceneName = "TestScene_0";
}
