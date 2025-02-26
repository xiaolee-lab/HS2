using System;
using System.Collections;
using System.Runtime.CompilerServices;
using Manager;
using UnityEngine;

namespace Studio
{
	// Token: 0x02001280 RID: 4736
	public class Map : Singleton<Map>
	{
		// Token: 0x17002176 RID: 8566
		// (get) Token: 0x06009C85 RID: 40069 RVA: 0x00400FD2 File Offset: 0x003FF3D2
		// (set) Token: 0x06009C86 RID: 40070 RVA: 0x00400FDA File Offset: 0x003FF3DA
		public GameObject MapRoot { get; private set; }

		// Token: 0x17002177 RID: 8567
		// (get) Token: 0x06009C87 RID: 40071 RVA: 0x00400FE3 File Offset: 0x003FF3E3
		// (set) Token: 0x06009C88 RID: 40072 RVA: 0x00400FEB File Offset: 0x003FF3EB
		public bool isLoading { get; private set; }

		// Token: 0x17002178 RID: 8568
		// (get) Token: 0x06009C89 RID: 40073 RVA: 0x00400FF4 File Offset: 0x003FF3F4
		// (set) Token: 0x06009C8A RID: 40074 RVA: 0x00400FFC File Offset: 0x003FF3FC
		public int no { get; private set; } = -1;

		// Token: 0x17002179 RID: 8569
		// (get) Token: 0x06009C8B RID: 40075 RVA: 0x00401005 File Offset: 0x003FF405
		// (set) Token: 0x06009C8C RID: 40076 RVA: 0x0040100D File Offset: 0x003FF40D
		private MapComponent MapComponent { get; set; }

		// Token: 0x1700217A RID: 8570
		// (get) Token: 0x06009C8D RID: 40077 RVA: 0x00401016 File Offset: 0x003FF416
		public bool IsOption
		{
			[CompilerGenerated]
			get
			{
				return this.MapComponent != null && this.MapComponent.CheckOption;
			}
		}

		// Token: 0x1700217B RID: 8571
		// (get) Token: 0x06009C8E RID: 40078 RVA: 0x00401037 File Offset: 0x003FF437
		// (set) Token: 0x06009C8F RID: 40079 RVA: 0x0040104D File Offset: 0x003FF44D
		public bool VisibleOption
		{
			get
			{
				return Singleton<Studio>.Instance.sceneInfo.mapInfo.option;
			}
			set
			{
				Singleton<Studio>.Instance.sceneInfo.mapInfo.option = value;
				MapComponent mapComponent = this.MapComponent;
				if (mapComponent != null)
				{
					mapComponent.SetOptionVisible(value);
				}
			}
		}

		// Token: 0x06009C90 RID: 40080 RVA: 0x0040107C File Offset: 0x003FF47C
		public IEnumerator LoadMapCoroutine(int _no, bool _wait = false)
		{
			if (!Singleton<Info>.Instance.dicMapLoadInfo.ContainsKey(_no))
			{
				this.ReleaseMap();
				yield break;
			}
			if (this.no == _no)
			{
				yield break;
			}
			this.MapComponent = null;
			if (_wait)
			{
				Singleton<Scene>.Instance.LoadReserve(new Scene.Data
				{
					levelName = "StudioWait",
					isAdd = true
				}, false);
			}
			this.isLoading = true;
			this.no = _no;
			Info.MapLoadInfo data = Singleton<Info>.Instance.dicMapLoadInfo[_no];
			Singleton<Scene>.Instance.LoadBaseScene(new Scene.Data
			{
				assetBundleName = data.bundlePath,
				levelName = data.fileName,
				fadeType = Scene.Data.FadeType.None,
				onLoad = delegate
				{
					this.OnLoadAfter(data.fileName);
				}
			});
			yield return new WaitWhile(new Func<bool>(this.get_isLoading));
			if (_wait)
			{
				Singleton<Scene>.Instance.UnLoad();
			}
			yield break;
		}

		// Token: 0x06009C91 RID: 40081 RVA: 0x004010A8 File Offset: 0x003FF4A8
		public void LoadMap(int _no)
		{
			if (!Singleton<Info>.Instance.dicMapLoadInfo.ContainsKey(_no))
			{
				this.ReleaseMap();
				return;
			}
			if (this.no == _no)
			{
				return;
			}
			this.MapComponent = null;
			this.isLoading = true;
			this.no = _no;
			Info.MapLoadInfo data = Singleton<Info>.Instance.dicMapLoadInfo[_no];
			Singleton<Scene>.Instance.LoadBaseScene(new Scene.Data
			{
				assetBundleName = data.bundlePath,
				levelName = data.fileName,
				fadeType = Scene.Data.FadeType.None,
				onLoad = delegate
				{
					this.OnLoadAfter(data.fileName);
				}
			});
		}

		// Token: 0x06009C92 RID: 40082 RVA: 0x00401162 File Offset: 0x003FF562
		public void ReleaseMap()
		{
			if (!Singleton<Map>.IsInstance())
			{
				return;
			}
			this.MapRoot = null;
			this.no = -1;
			this.MapComponent = null;
			Singleton<Scene>.Instance.UnloadBaseScene();
		}

		// Token: 0x06009C93 RID: 40083 RVA: 0x00401190 File Offset: 0x003FF590
		private void OnLoadAfter(string _levelName)
		{
			this.MapRoot = Scene.GetScene(_levelName).GetRootGameObjects().SafeGet(0);
			GameObject mapRoot = this.MapRoot;
			this.MapComponent = ((mapRoot != null) ? mapRoot.GetComponentInChildren<MapComponent>() : null);
			if (this.MapComponent != null)
			{
				this.MapComponent.SetupSea();
			}
			if (Singleton<MapCtrl>.IsInstance())
			{
				Singleton<MapCtrl>.Instance.Reflect();
			}
			if (Singleton<Studio>.IsInstance())
			{
				Singleton<Studio>.Instance.systemButtonCtrl.Apply();
			}
			this.isLoading = false;
		}

		// Token: 0x06009C94 RID: 40084 RVA: 0x00401222 File Offset: 0x003FF622
		protected override void Awake()
		{
			if (!base.CheckInstance())
			{
				return;
			}
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			this.isLoading = false;
			this.no = -1;
			this.MapRoot = null;
		}
	}
}
