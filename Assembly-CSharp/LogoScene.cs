using System;
using System.Collections;
using AIProject.Definitions;
using Manager;
using UnityEngine;

// Token: 0x02000B43 RID: 2883
public class LogoScene : BaseLoader
{
	// Token: 0x06005455 RID: 21589 RVA: 0x00252F94 File Offset: 0x00251394
	private IEnumerator Start()
	{
		base.enabled = false;
		Singleton<Game>.Instance.LoadGlobalData();
		string path = Path.WorldSaveDataFile;
		Singleton<Game>.Instance.LoadProfile(path);
		yield return new WaitWhile(() => Singleton<Scene>.Instance.sceneFade.IsFadeNow);
		yield return new global::WaitForSecondsRealtime(this.waitTime);
		Singleton<Scene>.Instance.LoadReserve(new Scene.Data
		{
			levelName = "Title",
			isFade = true
		}, false);
		base.enabled = true;
		yield break;
	}

	// Token: 0x04004F2E RID: 20270
	[SerializeField]
	private float waitTime = 2f;
}
