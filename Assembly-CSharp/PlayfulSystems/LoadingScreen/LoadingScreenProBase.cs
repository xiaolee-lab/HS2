using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlayfulSystems.LoadingScreen
{
	// Token: 0x02000636 RID: 1590
	public abstract class LoadingScreenProBase : MonoBehaviour
	{
		// Token: 0x060025C6 RID: 9670 RVA: 0x000D74BE File Offset: 0x000D58BE
		public static void LoadScene(int levelNum)
		{
			if (!LoadingScreenProBase.IsLegalLevelIndex(levelNum))
			{
				LoadingScreenProBase.targetSceneName = null;
				return;
			}
			LoadingScreenProBase.targetSceneIndex = levelNum;
			LoadingScreenProBase.targetSceneName = null;
			LoadingScreenProBase.LoadLoadingScene();
		}

		// Token: 0x060025C7 RID: 9671 RVA: 0x000D74E3 File Offset: 0x000D58E3
		private static bool IsLegalLevelIndex(int levelNum)
		{
			return levelNum >= 0 || levelNum < SceneManager.sceneCountInBuildSettings;
		}

		// Token: 0x060025C8 RID: 9672 RVA: 0x000D74F7 File Offset: 0x000D58F7
		public static void LoadScene(string levelName)
		{
			LoadingScreenProBase.targetSceneIndex = -1;
			LoadingScreenProBase.targetSceneName = levelName;
			LoadingScreenProBase.LoadLoadingScene();
		}

		// Token: 0x060025C9 RID: 9673 RVA: 0x000D750A File Offset: 0x000D590A
		private static void LoadLoadingScene()
		{
			Application.backgroundLoadingPriority = ThreadPriority.High;
			SceneManager.LoadScene(LoadingScreenConfig.loadingSceneName);
		}

		// Token: 0x060025CA RID: 9674 RVA: 0x000D751C File Offset: 0x000D591C
		protected virtual void Start()
		{
			if (LoadingScreenProBase.targetSceneName == null && LoadingScreenProBase.targetSceneIndex == -1)
			{
				return;
			}
			if (this.config == null)
			{
				return;
			}
			this.previousTimescale = Time.timeScale;
			Time.timeScale = 1f;
			this.currentScene = SceneManager.GetActiveScene();
			this.Init();
			Application.backgroundLoadingPriority = this.config.loadThreadPriority;
			base.StartCoroutine(this.LoadAsync(LoadingScreenProBase.targetSceneIndex, LoadingScreenProBase.targetSceneName));
		}

		// Token: 0x060025CB RID: 9675 RVA: 0x000D759E File Offset: 0x000D599E
		protected virtual void Init()
		{
		}

		// Token: 0x060025CC RID: 9676 RVA: 0x000D75A0 File Offset: 0x000D59A0
		private IEnumerator LoadAsync(int levelNum, string levelName)
		{
			this.ShowTips();
			this.ShowSceneInfos();
			this.ShowStartingVisuals();
			this.currentProgress = 0f;
			this.operation = this.StartOperation(levelNum, levelName);
			if (this.operation == null)
			{
				yield break;
			}
			this.operation.allowSceneActivation = this.CanLoadAdditively();
			while (!this.IsDoneLoading())
			{
				yield return null;
				this.SetProgress(this.operation.progress);
			}
			if (this.CanLoadAdditively() && this.audioListener != null)
			{
				this.audioListener.enabled = false;
			}
			yield return null;
			this.SetProgress(1f);
			while (!this.CanShowConfirmation())
			{
				yield return null;
			}
			this.ShowLoadingDoneVisuals();
			if (this.behaviorAfterLoad == LoadingScreenProBase.BehaviorAfterLoad.WaitForPlayerInput)
			{
				while (!Input.anyKey)
				{
					yield return null;
				}
			}
			else
			{
				yield return new WaitForSeconds(this.timeToAutoContinue);
			}
			this.ShowEndingVisuals();
			while (!this.CanActivateTargetScene())
			{
				yield return null;
			}
			this.ActivateLoadedScene();
			yield break;
		}

		// Token: 0x060025CD RID: 9677 RVA: 0x000D75C9 File Offset: 0x000D59C9
		private void SetProgress(float progress)
		{
			if (progress <= this.currentProgress)
			{
				return;
			}
			this.ShowProgressVisuals(progress);
			this.currentProgress = progress;
		}

		// Token: 0x060025CE RID: 9678 RVA: 0x000D75E8 File Offset: 0x000D59E8
		private AsyncOperation StartOperation(int levelNum, string levelName)
		{
			LoadSceneMode mode = (!this.CanLoadAdditively()) ? LoadSceneMode.Single : LoadSceneMode.Additive;
			if (string.IsNullOrEmpty(levelName))
			{
				return SceneManager.LoadSceneAsync(levelNum, mode);
			}
			return SceneManager.LoadSceneAsync(levelName, mode);
		}

		// Token: 0x060025CF RID: 9679 RVA: 0x000D7622 File Offset: 0x000D5A22
		private bool CanLoadAdditively()
		{
			return this.config.loadAdditively;
		}

		// Token: 0x060025D0 RID: 9680 RVA: 0x000D762F File Offset: 0x000D5A2F
		protected bool CanLoadAsynchronously()
		{
			return true;
		}

		// Token: 0x060025D1 RID: 9681 RVA: 0x000D7632 File Offset: 0x000D5A32
		private bool IsDoneLoading()
		{
			if (this.CanLoadAdditively())
			{
				return this.operation.isDone;
			}
			return this.operation.progress >= 0.9f;
		}

		// Token: 0x060025D2 RID: 9682 RVA: 0x000D7660 File Offset: 0x000D5A60
		private void ActivateLoadedScene()
		{
			LoadingScreenProBase.targetSceneIndex = -1;
			LoadingScreenProBase.targetSceneName = null;
			if (this.CanLoadAdditively())
			{
				SceneManager.UnloadSceneAsync(this.currentScene);
			}
			this.operation.allowSceneActivation = true;
			Resources.UnloadUnusedAssets();
			Time.timeScale = this.previousTimescale;
		}

		// Token: 0x060025D3 RID: 9683 RVA: 0x000D76B0 File Offset: 0x000D5AB0
		private void ShowSceneInfos()
		{
			if (!this.config.showSceneInfos || this.config.sceneInfos == null || this.config.sceneInfos.Length == 0)
			{
				this.DisplaySceneInfo(null);
			}
			else
			{
				this.DisplaySceneInfo(this.config.GetSceneInfo(LoadingScreenProBase.targetSceneName));
			}
		}

		// Token: 0x060025D4 RID: 9684 RVA: 0x000D7711 File Offset: 0x000D5B11
		protected virtual void DisplaySceneInfo(SceneInfo info)
		{
		}

		// Token: 0x060025D5 RID: 9685 RVA: 0x000D7714 File Offset: 0x000D5B14
		private void ShowTips()
		{
			if (!this.config.showRandomTip || this.config.gameTips == null || this.config.gameTips.Length == 0)
			{
				this.DisplayGameTip(null);
			}
			else
			{
				this.DisplayGameTip(this.config.GetGameTip());
			}
		}

		// Token: 0x060025D6 RID: 9686 RVA: 0x000D7770 File Offset: 0x000D5B70
		protected virtual void DisplayGameTip(LoadingTip tip)
		{
		}

		// Token: 0x060025D7 RID: 9687 RVA: 0x000D7772 File Offset: 0x000D5B72
		protected virtual void ShowStartingVisuals()
		{
		}

		// Token: 0x060025D8 RID: 9688 RVA: 0x000D7774 File Offset: 0x000D5B74
		protected virtual void ShowProgressVisuals(float progress)
		{
		}

		// Token: 0x060025D9 RID: 9689 RVA: 0x000D7776 File Offset: 0x000D5B76
		protected virtual void ShowLoadingDoneVisuals()
		{
		}

		// Token: 0x060025DA RID: 9690 RVA: 0x000D7778 File Offset: 0x000D5B78
		protected virtual void ShowEndingVisuals()
		{
		}

		// Token: 0x060025DB RID: 9691 RVA: 0x000D777A File Offset: 0x000D5B7A
		protected virtual bool CanShowConfirmation()
		{
			return true;
		}

		// Token: 0x060025DC RID: 9692 RVA: 0x000D777D File Offset: 0x000D5B7D
		protected virtual bool CanActivateTargetScene()
		{
			return true;
		}

		// Token: 0x0400258C RID: 9612
		private static int targetSceneIndex = -1;

		// Token: 0x0400258D RID: 9613
		private static string targetSceneName;

		// Token: 0x0400258E RID: 9614
		[Tooltip("Central Config asset.")]
		public LoadingScreenConfig config;

		// Token: 0x0400258F RID: 9615
		[Tooltip("If loading additively, set reference to this scene's camera's audio listener, to avoid multiple active audio listeners at any one time. The script will try to auto set this for your convenience.")]
		public AudioListener audioListener;

		// Token: 0x04002590 RID: 9616
		[Header("Timing Settings")]
		public LoadingScreenProBase.BehaviorAfterLoad behaviorAfterLoad;

		// Token: 0x04002591 RID: 9617
		[Tooltip("After finishing Loading, wait this much before showing the completion visuals.")]
		public float timeToAutoContinue = 0.25f;

		// Token: 0x04002592 RID: 9618
		private AsyncOperation operation;

		// Token: 0x04002593 RID: 9619
		private float previousTimescale;

		// Token: 0x04002594 RID: 9620
		private float currentProgress;

		// Token: 0x04002595 RID: 9621
		private Scene currentScene;

		// Token: 0x02000637 RID: 1591
		public enum BehaviorAfterLoad
		{
			// Token: 0x04002597 RID: 9623
			WaitForPlayerInput,
			// Token: 0x04002598 RID: 9624
			ContinueAutomatically
		}
	}
}
