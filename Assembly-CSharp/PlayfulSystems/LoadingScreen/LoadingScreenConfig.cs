using System;
using UnityEngine;

namespace PlayfulSystems.LoadingScreen
{
	// Token: 0x02000632 RID: 1586
	public class LoadingScreenConfig : ScriptableObject
	{
		// Token: 0x060025B3 RID: 9651 RVA: 0x000D7430 File Offset: 0x000D5830
		public virtual SceneInfo GetSceneInfo(string targetSceneName)
		{
			for (int i = 0; i < this.sceneInfos.Length; i++)
			{
				if (this.sceneInfos[i].sceneName == targetSceneName)
				{
					return this.sceneInfos[i];
				}
			}
			return null;
		}

		// Token: 0x060025B4 RID: 9652 RVA: 0x000D7478 File Offset: 0x000D5878
		public virtual LoadingTip GetGameTip()
		{
			return this.gameTips[UnityEngine.Random.Range(0, this.gameTips.Length)];
		}

		// Token: 0x0400256D RID: 9581
		public static string loadingSceneName = "PS-LoadingScene_5";

		// Token: 0x0400256E RID: 9582
		[Header("Loading Behavior")]
		[Tooltip("Loading additively means that the scene is loaded in the background in addition to the loading scene and is then turned off as the loading scene is unloaded.")]
		public bool loadAdditively;

		// Token: 0x0400256F RID: 9583
		[Tooltip("Lower priority means a background operation will run less often and will take up less time, but will progress more slowly.")]
		public ThreadPriority loadThreadPriority;

		// Token: 0x04002570 RID: 9584
		[Header("Scene Infos")]
		public bool showSceneInfos;

		// Token: 0x04002571 RID: 9585
		public SceneInfo[] sceneInfos;

		// Token: 0x04002572 RID: 9586
		[Header("Game Tips")]
		public bool showRandomTip = true;

		// Token: 0x04002573 RID: 9587
		public LoadingTip[] gameTips;
	}
}
