using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AIProject;
using Illusion.Extensions;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Manager
{
	// Token: 0x020010FC RID: 4348
	public sealed class Scene : Singleton<Scene>
	{
		// Token: 0x06009013 RID: 36883 RVA: 0x003C04B0 File Offset: 0x003BE8B0
		protected override void Awake()
		{
			if (!base.CheckInstance())
			{
				UnityEngine.Object.Destroy(base.gameObject);
				return;
			}
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			GameObject asset = AssetBundleManager.LoadAsset("scene/scenemanager.unity3d", "scenemanager", typeof(GameObject), null).GetAsset<GameObject>();
			this.manager = UnityEngine.Object.Instantiate<GameObject>(asset, base.transform, false);
			this.manager.name = asset.name;
			this.nowLoadingImage = this.manager.GetComponentsInChildren<Image>(true).FirstOrDefault((Image p) => p.name == "NowLoading");
			this.progressSlider = this.manager.GetComponentsInChildren<Slider>(true).FirstOrDefault((Slider p) => p.name == "Progress");
			this.loadingAnimeImage = this.manager.GetComponentsInChildren<Image>(true).FirstOrDefault((Image p) => p.name == "LoadingAnime");
			this.nowLoadingImage.SafeProc(delegate(Image t)
			{
				t.gameObject.SetActive(false);
			});
			this.progressSlider.SafeProc(delegate(Slider t)
			{
				t.gameObject.SetActive(false);
			});
			this.loadingAnimeImage.SafeProc(delegate(Image t)
			{
				t.gameObject.SetActive(false);
			});
			this.loadingPanel = this.manager.GetComponentInChildren<LoadingPanel>(true);
			this.loadingPanel.SafeProc(delegate(LoadingPanel t)
			{
				t.gameObject.SetActive(false);
			});
			this.sceneFade = this.manager.GetComponentInChildren<SceneFade>(true);
			this.initFadeColor = this.sceneFade._Color;
			this.sceneFade._Fade = SimpleFade.Fade.Out;
			this.sceneFade.ForceEnd();
			AssetBundleManager.UnloadAssetBundle("scene/scenemanager.unity3d", false, null, false);
			this.sceneStack = new Scene.SceneStack<Scene.Data>(new Scene.Data
			{
				levelName = Scene.ActiveScene.name,
				isAdd = false
			});
			this.loadStack = new Stack<Scene.Data>();
			this.CreateSpace();
			Application.wantsToQuit += this.CanQuit;
			this.isGameEndCheck = true;
			this.isSkipGameExit = false;
		}

		// Token: 0x06009014 RID: 36884 RVA: 0x003C0718 File Offset: 0x003BEB18
		private bool CanQuit()
		{
			if (Singleton<Game>.Instance.ExitScene != null)
			{
				this.GameExit();
				return true;
			}
			if (this.isSkipGameExit)
			{
				return false;
			}
			if (!this.isGameEndCheck)
			{
				this.GameExit();
				return true;
			}
			this.isGameEndCheck = false;
			bool flag = true;
			flag &= !this.IsExit;
			flag &= !Application.isEditor;
			flag &= !this.IsNowLoadingFade;
			flag &= (this.LoadSceneName != "Initialize");
			flag &= (this.LoadSceneName != "Logo");
			if (flag)
			{
				Singleton<Game>.Instance.LoadExit();
				this.isSkipGameExit = true;
				return false;
			}
			this.GameExit();
			return true;
		}

		// Token: 0x17001F12 RID: 7954
		// (get) Token: 0x06009015 RID: 36885 RVA: 0x003C07D4 File Offset: 0x003BEBD4
		// (set) Token: 0x06009016 RID: 36886 RVA: 0x003C07DC File Offset: 0x003BEBDC
		public GameObject commonSpace { get; private set; }

		// Token: 0x17001F13 RID: 7955
		// (get) Token: 0x06009017 RID: 36887 RVA: 0x003C07E5 File Offset: 0x003BEBE5
		// (set) Token: 0x06009018 RID: 36888 RVA: 0x003C07ED File Offset: 0x003BEBED
		public GameObject manager { get; private set; }

		// Token: 0x17001F14 RID: 7956
		// (get) Token: 0x06009019 RID: 36889 RVA: 0x003C07F6 File Offset: 0x003BEBF6
		// (set) Token: 0x0600901A RID: 36890 RVA: 0x003C07FE File Offset: 0x003BEBFE
		public SceneFade sceneFade { get; private set; }

		// Token: 0x17001F15 RID: 7957
		// (get) Token: 0x0600901B RID: 36891 RVA: 0x003C0807 File Offset: 0x003BEC07
		// (set) Token: 0x0600901C RID: 36892 RVA: 0x003C080F File Offset: 0x003BEC0F
		public LoadingPanel loadingPanel { get; private set; }

		// Token: 0x17001F16 RID: 7958
		// (get) Token: 0x0600901D RID: 36893 RVA: 0x003C0818 File Offset: 0x003BEC18
		public Scene.Data baseScene
		{
			get
			{
				return this._baseScene;
			}
		}

		// Token: 0x0600901E RID: 36894 RVA: 0x003C0820 File Offset: 0x003BEC20
		private void GameExit()
		{
			Scene.isGameEnd = true;
			if (Singleton<Config>.IsInstance())
			{
				Singleton<Config>.Instance.Save();
			}
			if (Singleton<Voice>.IsInstance())
			{
				Singleton<Voice>.Instance.Save();
			}
		}

		// Token: 0x0600901F RID: 36895 RVA: 0x003C0850 File Offset: 0x003BEC50
		private IEnumerator LoadSet(Scene.Data data)
		{
			data.isLoading = true;
			while (this.IsOverlap)
			{
				yield return (!this.IsExit) ? base.StartCoroutine(this.UnLoad(false)) : null;
			}
			this.sceneStack.Push(data);
			if (data.operation != null)
			{
				data.operation.AsObservable(null).Subscribe(delegate(AsyncOperation _)
				{
					data.isLoading = false;
					data.onLoad.Call();
				});
			}
			int count = 1;
			if (this.loadingCount != 0)
			{
				count = this.loadingCount;
			}
			int sceneNum = this.loadingCount - this.loadStack.Count;
			if (data.assetBundleOperation != null)
			{
				count++;
				while (data.assetBundleOperation.Request == null)
				{
					this.progressSlider.SafeProc(delegate(Slider t)
					{
						t.value = (AssetBundleManager.Progress + (float)sceneNum) / (float)count;
					});
					yield return null;
				}
				while (!data.assetBundleOperation.Request.isDone)
				{
					yield return null;
					this.progressSlider.SafeProc(delegate(Slider t)
					{
						t.value = (AssetBundleManager.Progress + data.assetBundleOperation.Request.progress + (float)sceneNum) / (float)count;
					});
				}
			}
			else if (data.operation != null)
			{
				while (!data.operation.isDone)
				{
					yield return null;
					this.progressSlider.SafeProc(delegate(Slider t)
					{
						t.value = (data.operation.progress + (float)sceneNum) / (float)count;
					});
				}
			}
			if (data.operation == null)
			{
				data.isLoading = false;
				if (!data.onLoad.IsNullOrEmpty())
				{
					yield return null;
					data.onLoad.Call();
				}
			}
			yield break;
		}

		// Token: 0x06009020 RID: 36896 RVA: 0x003C0874 File Offset: 0x003BEC74
		private void SetImageAlpha(Image image, float alpha)
		{
			if (image == null)
			{
				return;
			}
			Color color = image.color;
			color.a = alpha;
			image.color = color;
		}

		// Token: 0x06009021 RID: 36897 RVA: 0x003C08A4 File Offset: 0x003BECA4
		public void LoadBaseScene(Scene.Data data)
		{
			base.StartCoroutine(this.LoadBaseSceneCoroutine(data));
		}

		// Token: 0x06009022 RID: 36898 RVA: 0x003C08B4 File Offset: 0x003BECB4
		public IEnumerator LoadBaseSceneCoroutine(Scene.Data data)
		{
			data.isAdd = true;
			if (data.isFadeIn)
			{
				yield return base.StartCoroutine(this.Fade(SimpleFade.Fade.In, null));
				if (data.onFadeIn != null)
				{
					yield return Observable.FromCoroutine(data.onFadeIn, false).StartAsCoroutine(default(CancellationToken));
				}
			}
			yield return base.StartCoroutine(this.UnloadBaseSceneCoroutine(this.LoadSceneName));
			data.Load();
			yield return base.StartCoroutine(this.UnloadBaseSceneCoroutine(data.levelName));
			this._baseScene = data;
			data.onLoad.Call();
			if (data.isFadeOut)
			{
				if (data.onFadeOut != null)
				{
					yield return Observable.FromCoroutine(data.onFadeOut, false).StartAsCoroutine(default(CancellationToken));
				}
				yield return base.StartCoroutine(this.Fade(SimpleFade.Fade.Out, null));
			}
			yield break;
		}

		// Token: 0x06009023 RID: 36899 RVA: 0x003C08D6 File Offset: 0x003BECD6
		public void UnloadBaseScene()
		{
			base.StartCoroutine(this.UnloadBaseSceneCoroutine(this.LoadSceneName));
		}

		// Token: 0x06009024 RID: 36900 RVA: 0x003C08EC File Offset: 0x003BECEC
		public IEnumerator UnloadBaseSceneCoroutine(string levelName)
		{
			if (this._baseScene != null)
			{
				if (Scene.GetScene(this._baseScene.levelName).IsValid())
				{
					yield return SceneManager.UnloadSceneAsync(this._baseScene.levelName);
				}
				AssetBundleManager.UnloadAssetBundle(this._baseScene.assetBundleName, true, null, false);
			}
			this._baseScene = null;
			Scene scene = Scene.GetScene(levelName);
			yield return new WaitWhile(() => !scene.isLoaded);
			Scene.ActiveScene = scene;
			Resources.UnloadUnusedAssets();
			yield break;
		}

		// Token: 0x06009025 RID: 36901 RVA: 0x003C090E File Offset: 0x003BED0E
		public static void MapSettingChange(LightMapDataObject lightMap, Scene.FogData fog = null)
		{
			if (lightMap != null)
			{
				lightMap.Change();
			}
			if (fog != null)
			{
				fog.Change();
			}
		}

		// Token: 0x17001F17 RID: 7959
		// (get) Token: 0x06009026 RID: 36902 RVA: 0x003C092E File Offset: 0x003BED2E
		// (set) Token: 0x06009027 RID: 36903 RVA: 0x003C0935 File Offset: 0x003BED35
		public static bool isGameEnd { get; private set; }

		// Token: 0x17001F18 RID: 7960
		// (get) Token: 0x06009028 RID: 36904 RVA: 0x003C093D File Offset: 0x003BED3D
		// (set) Token: 0x06009029 RID: 36905 RVA: 0x003C0944 File Offset: 0x003BED44
		public static bool isReturnTitle { get; set; }

		// Token: 0x17001F19 RID: 7961
		// (get) Token: 0x0600902A RID: 36906 RVA: 0x003C094C File Offset: 0x003BED4C
		// (set) Token: 0x0600902B RID: 36907 RVA: 0x003C0953 File Offset: 0x003BED53
		public static Scene ActiveScene
		{
			get
			{
				return SceneManager.GetActiveScene();
			}
			set
			{
				SceneManager.SetActiveScene(value);
			}
		}

		// Token: 0x0600902C RID: 36908 RVA: 0x003C095C File Offset: 0x003BED5C
		public static Scene GetScene(string levelName)
		{
			return SceneManager.GetSceneByName(levelName);
		}

		// Token: 0x0600902D RID: 36909 RVA: 0x003C0964 File Offset: 0x003BED64
		public static GameObject[] GetRootGameObjects(string sceneName)
		{
			Scene scene = Scene.GetScene(sceneName);
			return scene.isLoaded ? scene.GetRootGameObjects() : null;
		}

		// Token: 0x0600902E RID: 36910 RVA: 0x003C0994 File Offset: 0x003BED94
		public static T GetRootComponent<T>(string sceneName) where T : Component
		{
			GameObject[] rootGameObjects = Scene.GetRootGameObjects(sceneName);
			if (rootGameObjects == null)
			{
				return (T)((object)null);
			}
			foreach (GameObject gameObject in rootGameObjects)
			{
				T component = gameObject.GetComponent<T>();
				if (component != null)
				{
					return component;
				}
			}
			foreach (GameObject gameObject2 in rootGameObjects)
			{
				T[] componentsInChildren = gameObject2.GetComponentsInChildren<T>(true);
				if (!componentsInChildren.IsNullOrEmpty<T>())
				{
					return componentsInChildren[0];
				}
			}
			return (T)((object)null);
		}

		// Token: 0x17001F1A RID: 7962
		// (get) Token: 0x0600902F RID: 36911 RVA: 0x003C0A34 File Offset: 0x003BEE34
		public bool IsOverlap
		{
			get
			{
				return this.sceneStack.Any<Scene.Data>() && this.sceneStack.Peek().isOverlap;
			}
		}

		// Token: 0x17001F1B RID: 7963
		// (get) Token: 0x06009030 RID: 36912 RVA: 0x003C0A59 File Offset: 0x003BEE59
		public bool IsExit
		{
			get
			{
				return Singleton<Game>.Instance.ExitScene != null;
			}
		}

		// Token: 0x17001F1C RID: 7964
		// (get) Token: 0x06009031 RID: 36913 RVA: 0x003C0A6C File Offset: 0x003BEE6C
		public bool IsNowLoading
		{
			get
			{
				if (this.loadStack.Count > 0)
				{
					return true;
				}
				bool result = false;
				foreach (Scene.Data data in this.sceneStack)
				{
					if (data.isLoading)
					{
						result = true;
						break;
					}
					if (data.operation != null && !data.operation.isDone)
					{
						result = true;
						break;
					}
				}
				return result;
			}
		}

		// Token: 0x17001F1D RID: 7965
		// (get) Token: 0x06009032 RID: 36914 RVA: 0x003C0B0C File Offset: 0x003BEF0C
		public bool IsNowLoadingFade
		{
			get
			{
				return this.IsNowLoading || this.sceneFade.IsFadeNow;
			}
		}

		// Token: 0x17001F1E RID: 7966
		// (get) Token: 0x06009033 RID: 36915 RVA: 0x003C0B27 File Offset: 0x003BEF27
		public bool IsFadeNow
		{
			get
			{
				return this.sceneFade.IsFadeNow;
			}
		}

		// Token: 0x17001F1F RID: 7967
		// (get) Token: 0x06009034 RID: 36916 RVA: 0x003C0B34 File Offset: 0x003BEF34
		public string LoadSceneName
		{
			get
			{
				return this.sceneStack.NowSceneNameList.Last<string>();
			}
		}

		// Token: 0x17001F20 RID: 7968
		// (get) Token: 0x06009035 RID: 36917 RVA: 0x003C0B46 File Offset: 0x003BEF46
		public string AddSceneName
		{
			get
			{
				return (this.sceneStack.NowSceneNameList.Count <= 1) ? string.Empty : this.sceneStack.NowSceneNameList[0];
			}
		}

		// Token: 0x17001F21 RID: 7969
		// (get) Token: 0x06009036 RID: 36918 RVA: 0x003C0B7C File Offset: 0x003BEF7C
		public string AddSceneNameOverlapRemoved
		{
			get
			{
				foreach (Scene.Data data in this.sceneStack)
				{
					if (!data.isOverlap)
					{
						if (!data.isAdd)
						{
							return string.Empty;
						}
						return data.levelName;
					}
				}
				return string.Empty;
			}
		}

		// Token: 0x17001F22 RID: 7970
		// (get) Token: 0x06009037 RID: 36919 RVA: 0x003C0C0C File Offset: 0x003BF00C
		public string PrevLoadSceneName
		{
			get
			{
				bool flag = false;
				foreach (Scene.Data data in this.sceneStack)
				{
					if (!data.isAdd)
					{
						if (flag)
						{
							return data.levelName;
						}
						flag = true;
					}
				}
				return string.Empty;
			}
		}

		// Token: 0x17001F23 RID: 7971
		// (get) Token: 0x06009038 RID: 36920 RVA: 0x003C0C94 File Offset: 0x003BF094
		public string PrevAddSceneName
		{
			get
			{
				return (this.sceneStack.NowSceneNameList.Count <= 2) ? string.Empty : this.sceneStack.NowSceneNameList[1];
			}
		}

		// Token: 0x17001F24 RID: 7972
		// (get) Token: 0x06009039 RID: 36921 RVA: 0x003C0CC7 File Offset: 0x003BF0C7
		public List<string> NowSceneNames
		{
			get
			{
				return this.sceneStack.NowSceneNameList;
			}
		}

		// Token: 0x0600903A RID: 36922 RVA: 0x003C0CD4 File Offset: 0x003BF0D4
		public void GameEnd(bool _isCheck = true)
		{
			this.isGameEndCheck = _isCheck;
			Application.Quit();
		}

		// Token: 0x0600903B RID: 36923 RVA: 0x003C0CE2 File Offset: 0x003BF0E2
		public void LoadReserve(Scene.Data data, bool isLoadingImageDraw)
		{
			base.StartCoroutine(this.LoadStart(data, isLoadingImageDraw));
		}

		// Token: 0x0600903C RID: 36924 RVA: 0x003C0CF4 File Offset: 0x003BF0F4
		public IEnumerator LoadSceneBack(bool isNowSceneRemove, bool isAddSceneLoad)
		{
			if (isNowSceneRemove && this.sceneStack.Count > 1)
			{
				this.sceneStack.Pop();
			}
			do
			{
				this.loadStack.Push(this.sceneStack.Pop());
			}
			while (this.loadStack.Peek().isAdd);
			this.loadingCount = this.loadStack.Count;
			while (this.loadStack.Any<Scene.Data>())
			{
				Scene.Data data = this.loadStack.Pop();
				if (!isAddSceneLoad)
				{
					this.loadStack.Clear();
				}
				yield return base.StartCoroutine(this.LoadStart(data, true));
			}
			this.loadingCount = 0;
			yield break;
		}

		// Token: 0x0600903D RID: 36925 RVA: 0x003C0D1D File Offset: 0x003BF11D
		public void UnloadAddScene()
		{
			while (this.sceneStack.Peek().isAdd)
			{
				this.sceneStack.Peek().Unload();
				this.sceneStack.Pop();
			}
			Resources.UnloadUnusedAssets();
		}

		// Token: 0x0600903E RID: 36926 RVA: 0x003C0D5C File Offset: 0x003BF15C
		public IEnumerator UnloadAddScene(bool isFade, bool isLoadingImageDraw = false)
		{
			if (isFade)
			{
				yield return base.StartCoroutine(this.Fade(SimpleFade.Fade.In, null));
				this.DrawImageAndProgress(0f, 1f, isLoadingImageDraw);
				this.UnloadAddScene();
				if (this.sceneFade._Fade == SimpleFade.Fade.In)
				{
					yield return base.StartCoroutine(this.Fade(SimpleFade.Fade.Out, delegate
					{
						Scene $this = this.$this;
						float a = this.sceneFade._Color.a;
						$this.DrawImageAndProgress(-1f, a, isLoadingImageDraw);
					}));
					bool isLoadingImageDraw2 = isLoadingImageDraw;
					this.DrawImageAndProgress(-1f, -1f, isLoadingImageDraw2);
				}
			}
			else
			{
				this.UnloadAddScene();
			}
			yield break;
		}

		// Token: 0x0600903F RID: 36927 RVA: 0x003C0D85 File Offset: 0x003BF185
		public void Reload()
		{
			base.StartCoroutine(this.LoadSceneBack(false, true));
		}

		// Token: 0x06009040 RID: 36928 RVA: 0x003C0D98 File Offset: 0x003BF198
		public bool UnLoad()
		{
			bool flag = false;
			if (this.sceneStack.Count <= 1)
			{
				return false;
			}
			Scene.Data scene = this.sceneStack.Peek();
			AsyncOperation asyncOperation = scene.Unload();
			this.sceneStack.Pop();
			Scene.Data.UnloadType unloadType = Scene.Data.UnloadType.Success;
			if (asyncOperation != null)
			{
				if (this.NowSceneNames.Any((string s) => s == scene.levelName))
				{
					unloadType = Scene.Data.UnloadType.Loaded;
				}
			}
			else
			{
				unloadType = Scene.Data.UnloadType.Fail;
			}
			if (unloadType != Scene.Data.UnloadType.Fail)
			{
				if (unloadType == Scene.Data.UnloadType.Loaded)
				{
					flag = true;
				}
			}
			else if (this.AddSceneName.IsNullOrEmpty())
			{
				base.StartCoroutine(this.LoadStart(this.sceneStack.Pop(), true));
			}
			else
			{
				flag = true;
			}
			if (flag)
			{
				do
				{
					this.loadStack.Push(this.sceneStack.Pop());
				}
				while (this.loadStack.Peek().isAdd);
				while (this.loadStack.Any<Scene.Data>())
				{
					Scene.Data data = this.loadStack.Pop();
					bool isAsync = data.isAsync;
					data.isAsync = false;
					this.sceneStack.Push(data);
					data.isAsync = isAsync;
				}
			}
			return true;
		}

		// Token: 0x06009041 RID: 36929 RVA: 0x003C0EE0 File Offset: 0x003BF2E0
		public IEnumerator UnLoad(bool isLoadBack)
		{
			if (this.sceneStack.Count <= 1)
			{
				yield break;
			}
			bool isLoadSceneBack = false;
			bool isNowSceneRemove = false;
			if (isLoadBack)
			{
				isLoadSceneBack = true;
				isNowSceneRemove = true;
			}
			else
			{
				Scene.Data scene = this.sceneStack.Peek();
				AsyncOperation asyncOperation = scene.Unload();
				this.sceneStack.Pop();
				Scene.Data.UnloadType unloadType = Scene.Data.UnloadType.Success;
				if (asyncOperation != null)
				{
					if (this.NowSceneNames.Any((string s) => s == scene.levelName))
					{
						unloadType = Scene.Data.UnloadType.Loaded;
					}
				}
				else
				{
					unloadType = Scene.Data.UnloadType.Fail;
				}
				if (unloadType != Scene.Data.UnloadType.Success)
				{
					isLoadSceneBack = true;
				}
			}
			if (isLoadSceneBack)
			{
				yield return base.StartCoroutine(this.LoadSceneBack(isNowSceneRemove, true));
			}
			yield break;
		}

		// Token: 0x06009042 RID: 36930 RVA: 0x003C0F04 File Offset: 0x003BF304
		public IEnumerator UnLoad(string levelName, Action<bool> act = null)
		{
			bool isFind = this.IsFind(levelName);
			act.Call(isFind);
			if (!isFind)
			{
				yield break;
			}
			this.RollBack(levelName);
			yield return base.StartCoroutine(this.LoadSceneBack(false, true));
			yield break;
		}

		// Token: 0x06009043 RID: 36931 RVA: 0x003C0F30 File Offset: 0x003BF330
		public bool IsFind(string levelName)
		{
			return this.sceneStack.Any((Scene.Data scene) => scene.levelName == levelName);
		}

		// Token: 0x06009044 RID: 36932 RVA: 0x003C0F61 File Offset: 0x003BF361
		public void RollBack(string levelName)
		{
			while (this.sceneStack.Peek().levelName != levelName)
			{
				this.sceneStack.Pop();
			}
		}

		// Token: 0x06009045 RID: 36933 RVA: 0x003C0F90 File Offset: 0x003BF390
		public bool StartFade(int _fadeType, Color _color, float _inTime = 1f, float _outTime = 1f, float _waitTime = 1f)
		{
			if (this.sceneFade == null)
			{
				return false;
			}
			if (_fadeType != 0)
			{
				if (_fadeType != 1)
				{
					if (_fadeType == 2)
					{
						SimpleFade.FadeInOut set = new SimpleFade.FadeInOut
						{
							inColor = _color,
							outColor = _color,
							inTime = _inTime,
							outTime = _outTime,
							waitTime = _waitTime
						};
						this.sceneFade.FadeInOutSet(set, null);
					}
				}
				else
				{
					this.sceneFade._Color = _color;
					this.sceneFade.FadeSet(SimpleFade.Fade.Out, _outTime, null);
				}
			}
			else
			{
				this.sceneFade._Color = _color;
				this.sceneFade.FadeSet(SimpleFade.Fade.In, _inTime, null);
			}
			return true;
		}

		// Token: 0x06009046 RID: 36934 RVA: 0x003C1048 File Offset: 0x003BF448
		public void SetFadeColor(Color _color)
		{
			this.sceneFade.SafeProc(delegate(SceneFade fade)
			{
				fade._Color = _color;
			});
		}

		// Token: 0x06009047 RID: 36935 RVA: 0x003C107A File Offset: 0x003BF47A
		public void SetFadeColorDefault()
		{
			this.sceneFade.SafeProc(delegate(SceneFade fade)
			{
				float a = fade._Color.a;
				fade._Color = this.initFadeColor;
				fade._Color.a = a;
			});
		}

		// Token: 0x06009048 RID: 36936 RVA: 0x003C1094 File Offset: 0x003BF494
		public void CreateSpace()
		{
			UnityEngine.Object.Destroy(this.commonSpace);
			this.commonSpace = new GameObject("CommonSpace");
			UnityEngine.Object.DontDestroyOnLoad(this.commonSpace);
		}

		// Token: 0x06009049 RID: 36937 RVA: 0x003C10BC File Offset: 0x003BF4BC
		public void SpaceRegister(Transform trans, bool worldPositionStays = false)
		{
			trans.SetParent(this.commonSpace.transform, worldPositionStays);
		}

		// Token: 0x0600904A RID: 36938 RVA: 0x003C10D0 File Offset: 0x003BF4D0
		public IEnumerator Fade(SimpleFade.Fade fade, Action fadeWaitProc = null)
		{
			this.sceneFade.FadeSet(fade, -1f, null);
			while (!this.sceneFade.IsEnd)
			{
				yield return null;
				fadeWaitProc.Call();
			}
			yield break;
		}

		// Token: 0x0600904B RID: 36939 RVA: 0x003C10FC File Offset: 0x003BF4FC
		public void DrawImageAndProgress(float value = -1f, float alpha = -1f, bool isLoadingImageDraw = true)
		{
			bool isDraw = value >= 0f;
			this.progressSlider.SafeProc(delegate(Slider t)
			{
				t.value = ((!isDraw) ? 0f : value);
				t.gameObject.SetActive(isDraw);
			});
			if (alpha < 0f)
			{
				alpha = (float)((!isDraw) ? 0 : 1);
			}
			this.SetImageAlpha(this.nowLoadingImage, alpha);
			this.nowLoadingImage.SafeProc(delegate(Image t)
			{
				t.gameObject.SetActive(isLoadingImageDraw && alpha > 0f);
			});
		}

		// Token: 0x0600904C RID: 36940 RVA: 0x003C11A8 File Offset: 0x003BF5A8
		public IEnumerator LoadStart(Scene.Data data, bool isLoadingImageDraw = true)
		{
			if (data.isFadeIn && !this.sceneFade.IsFadeNow)
			{
				if (Time.timeScale == 0f)
				{
					Time.timeScale = 1f;
				}
				yield return base.StartCoroutine(this.Fade(SimpleFade.Fade.In, null));
				yield return new WaitForSeconds(0.1f);
				this.DrawImageAndProgress((float)((!data.isDrawProgressBar || !data.isAsync) ? -1 : 0), 1f, isLoadingImageDraw);
			}
			if (data.isFadeIn && data.onFadeIn != null)
			{
				yield return Observable.FromCoroutine(data.onFadeIn, false).StartAsCoroutine(default(CancellationToken));
			}
			yield return base.StartCoroutine(this.LoadSet(data));
			if (data.isFadeOut && data.onFadeOut != null)
			{
				yield return Observable.FromCoroutine(data.onFadeOut, false).StartAsCoroutine(default(CancellationToken));
			}
			if (!this.loadStack.Any<Scene.Data>())
			{
				Resources.UnloadUnusedAssets();
				if (data.isFadeOut && this.sceneFade._Fade == SimpleFade.Fade.In)
				{
					yield return new WaitForSeconds(0.1f);
					yield return base.StartCoroutine(this.Fade(SimpleFade.Fade.Out, delegate
					{
						Scene $this = this.$this;
						float a = this.sceneFade._Color.a;
						$this.DrawImageAndProgress(-1f, a, isLoadingImageDraw);
					}));
				}
				bool isLoadingImageDraw2 = isLoadingImageDraw;
				this.DrawImageAndProgress(-1f, -1f, isLoadingImageDraw2);
			}
			yield break;
		}

		// Token: 0x040074E8 RID: 29928
		private Color initFadeColor;

		// Token: 0x040074E9 RID: 29929
		[SerializeField]
		private Image nowLoadingImage;

		// Token: 0x040074EA RID: 29930
		[SerializeField]
		private Slider progressSlider;

		// Token: 0x040074EB RID: 29931
		[SerializeField]
		private Image loadingAnimeImage;

		// Token: 0x040074EC RID: 29932
		private Scene.SceneStack<Scene.Data> sceneStack;

		// Token: 0x040074ED RID: 29933
		private Stack<Scene.Data> loadStack;

		// Token: 0x040074EE RID: 29934
		private int loadingCount;

		// Token: 0x040074EF RID: 29935
		private const float FadeWaitTime = 0.1f;

		// Token: 0x040074F0 RID: 29936
		private Scene.Data _baseScene;

		// Token: 0x040074F1 RID: 29937
		public bool isGameEndCheck;

		// Token: 0x040074F2 RID: 29938
		public bool isSkipGameExit;

		// Token: 0x020010FD RID: 4349
		public class SceneStack<T> : Stack<T> where T : Scene.Data
		{
			// Token: 0x06009055 RID: 36949 RVA: 0x003C1271 File Offset: 0x003BF671
			public SceneStack(T item)
			{
				base.Push(item);
				this.nowSceneNameList.Push(item.levelName);
			}

			// Token: 0x17001F25 RID: 7973
			// (get) Token: 0x06009056 RID: 36950 RVA: 0x003C12A1 File Offset: 0x003BF6A1
			public List<string> NowSceneNameList
			{
				get
				{
					return this.nowSceneNameList;
				}
			}

			// Token: 0x06009057 RID: 36951 RVA: 0x003C12AC File Offset: 0x003BF6AC
			public new void Push(T item)
			{
				base.Push(item);
				if (!item.isAdd)
				{
					this.nowSceneNameList.Clear();
				}
				this.nowSceneNameList.Push(item.levelName);
				item.Load();
			}

			// Token: 0x06009058 RID: 36952 RVA: 0x003C1300 File Offset: 0x003BF700
			public new T Pop()
			{
				T t = base.Pop();
				if (this.nowSceneNameList.Any<string>())
				{
					this.nowSceneNameList.Pop<string>();
				}
				if (!this.nowSceneNameList.Any<string>())
				{
					foreach (T t2 in this)
					{
						this.nowSceneNameList.Add(t2.levelName);
						if (!t2.isAdd)
						{
							break;
						}
					}
				}
				AssetBundleManager.UnloadAssetBundle(t.assetBundleName, false, t.manifestFileName, false);
				return t;
			}

			// Token: 0x040074FC RID: 29948
			private List<string> nowSceneNameList = new List<string>();
		}

		// Token: 0x020010FE RID: 4350
		public class Data
		{
			// Token: 0x17001F26 RID: 7974
			// (set) Token: 0x0600905A RID: 36954 RVA: 0x003C13F1 File Offset: 0x003BF7F1
			public bool isFade
			{
				set
				{
					this.fadeType = ((!value) ? Scene.Data.FadeType.None : Scene.Data.FadeType.InOut);
				}
			}

			// Token: 0x17001F27 RID: 7975
			// (get) Token: 0x0600905B RID: 36955 RVA: 0x003C1406 File Offset: 0x003BF806
			public bool isFadeIn
			{
				get
				{
					return this.fadeType == Scene.Data.FadeType.InOut || this.fadeType == Scene.Data.FadeType.In;
				}
			}

			// Token: 0x17001F28 RID: 7976
			// (get) Token: 0x0600905C RID: 36956 RVA: 0x003C1420 File Offset: 0x003BF820
			public bool isFadeOut
			{
				get
				{
					return this.fadeType == Scene.Data.FadeType.InOut || this.fadeType == Scene.Data.FadeType.Out;
				}
			}

			// Token: 0x17001F29 RID: 7977
			// (get) Token: 0x0600905D RID: 36957 RVA: 0x003C143A File Offset: 0x003BF83A
			// (set) Token: 0x0600905E RID: 36958 RVA: 0x003C1442 File Offset: 0x003BF842
			public AsyncOperation operation { get; private set; }

			// Token: 0x17001F2A RID: 7978
			// (get) Token: 0x0600905F RID: 36959 RVA: 0x003C144B File Offset: 0x003BF84B
			// (set) Token: 0x06009060 RID: 36960 RVA: 0x003C1453 File Offset: 0x003BF853
			public Action onLoad { get; set; }

			// Token: 0x17001F2B RID: 7979
			// (get) Token: 0x06009061 RID: 36961 RVA: 0x003C145C File Offset: 0x003BF85C
			// (set) Token: 0x06009062 RID: 36962 RVA: 0x003C1464 File Offset: 0x003BF864
			public Func<IEnumerator> onFadeIn { get; set; }

			// Token: 0x17001F2C RID: 7980
			// (get) Token: 0x06009063 RID: 36963 RVA: 0x003C146D File Offset: 0x003BF86D
			// (set) Token: 0x06009064 RID: 36964 RVA: 0x003C1475 File Offset: 0x003BF875
			public Func<IEnumerator> onFadeOut { get; set; }

			// Token: 0x17001F2D RID: 7981
			// (get) Token: 0x06009065 RID: 36965 RVA: 0x003C147E File Offset: 0x003BF87E
			// (set) Token: 0x06009066 RID: 36966 RVA: 0x003C1486 File Offset: 0x003BF886
			public AssetBundleLoadLevelOperation assetBundleOperation { get; private set; }

			// Token: 0x06009067 RID: 36967 RVA: 0x003C148F File Offset: 0x003BF88F
			public AsyncOperation Unload()
			{
				if (!this.isAdd)
				{
					return null;
				}
				return SceneManager.UnloadSceneAsync(this.levelName);
			}

			// Token: 0x06009068 RID: 36968 RVA: 0x003C14AC File Offset: 0x003BF8AC
			public void Load()
			{
				if (!this.assetBundleName.IsNullOrEmpty())
				{
					if (!this.isAsync)
					{
						AssetBundleManager.LoadLevel(this.assetBundleName, this.levelName, this.isAdd, this.manifestFileName);
					}
					else
					{
						this.assetBundleOperation = (AssetBundleManager.LoadLevelAsync(this.assetBundleName, this.levelName, this.isAdd, this.manifestFileName) as AssetBundleLoadLevelOperation);
					}
				}
				else if (!this.isAsync)
				{
					SceneManager.LoadScene(this.levelName, (!this.isAdd) ? LoadSceneMode.Single : LoadSceneMode.Additive);
				}
				else
				{
					this.operation = SceneManager.LoadSceneAsync(this.levelName, (!this.isAdd) ? LoadSceneMode.Single : LoadSceneMode.Additive);
				}
			}

			// Token: 0x040074FD RID: 29949
			public Scene.Data.FadeType fadeType;

			// Token: 0x040074FE RID: 29950
			public string assetBundleName = string.Empty;

			// Token: 0x040074FF RID: 29951
			public string levelName = string.Empty;

			// Token: 0x04007500 RID: 29952
			public bool isAdd;

			// Token: 0x04007501 RID: 29953
			public bool isAsync;

			// Token: 0x04007502 RID: 29954
			public bool isOverlap;

			// Token: 0x04007503 RID: 29955
			public string manifestFileName;

			// Token: 0x04007508 RID: 29960
			public bool isLoading;

			// Token: 0x04007509 RID: 29961
			public bool isDrawProgressBar = true;

			// Token: 0x020010FF RID: 4351
			public enum FadeType
			{
				// Token: 0x0400750C RID: 29964
				None,
				// Token: 0x0400750D RID: 29965
				InOut,
				// Token: 0x0400750E RID: 29966
				In,
				// Token: 0x0400750F RID: 29967
				Out
			}

			// Token: 0x02001100 RID: 4352
			public enum UnloadType
			{
				// Token: 0x04007511 RID: 29969
				Success,
				// Token: 0x04007512 RID: 29970
				Fail,
				// Token: 0x04007513 RID: 29971
				Loaded
			}
		}

		// Token: 0x02001101 RID: 4353
		[Serializable]
		public class FogData
		{
			// Token: 0x0600906A RID: 36970 RVA: 0x003C15A4 File Offset: 0x003BF9A4
			public void Change()
			{
				RenderSettings.fog = this.use;
				RenderSettings.fogMode = this.mode;
				RenderSettings.fogColor = this.color;
				RenderSettings.fogDensity = this.density;
				RenderSettings.fogStartDistance = this.start;
				RenderSettings.fogEndDistance = this.end;
			}

			// Token: 0x04007514 RID: 29972
			public FogMode mode = FogMode.Exponential;

			// Token: 0x04007515 RID: 29973
			public bool use;

			// Token: 0x04007516 RID: 29974
			public Color color = Color.clear;

			// Token: 0x04007517 RID: 29975
			public float density = 0.01f;

			// Token: 0x04007518 RID: 29976
			public float start;

			// Token: 0x04007519 RID: 29977
			public float end = 1000f;
		}
	}
}
