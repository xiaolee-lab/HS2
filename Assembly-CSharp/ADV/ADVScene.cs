using System;
using System.Runtime.CompilerServices;
using ADV.Backup;
using Cinemachine;
using ConfigScene;
using Manager;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace ADV
{
	// Token: 0x020006B3 RID: 1715
	public class ADVScene : BaseLoader
	{
		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x0600288D RID: 10381 RVA: 0x000F058C File Offset: 0x000EE98C
		public TextScenario Scenario
		{
			get
			{
				return this.scenario;
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x0600288E RID: 10382 RVA: 0x000F0594 File Offset: 0x000EE994
		public Transform Stand
		{
			get
			{
				return this.stand;
			}
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x0600288F RID: 10383 RVA: 0x000F059C File Offset: 0x000EE99C
		public ADVFade AdvFade
		{
			get
			{
				return this.advFade;
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06002890 RID: 10384 RVA: 0x000F05A4 File Offset: 0x000EE9A4
		// (set) Token: 0x06002891 RID: 10385 RVA: 0x000F05AC File Offset: 0x000EE9AC
		public string startAddSceneName { get; private set; }

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06002892 RID: 10386 RVA: 0x000F05B5 File Offset: 0x000EE9B5
		public Camera advCamera
		{
			[CompilerGenerated]
			get
			{
				return this._advCamera;
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06002893 RID: 10387 RVA: 0x000F05BD File Offset: 0x000EE9BD
		// (set) Token: 0x06002894 RID: 10388 RVA: 0x000F05C5 File Offset: 0x000EE9C5
		private Camera _advCamera { get; set; }

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06002895 RID: 10389 RVA: 0x000F05CE File Offset: 0x000EE9CE
		// (set) Token: 0x06002896 RID: 10390 RVA: 0x000F05D6 File Offset: 0x000EE9D6
		public float? fadeTime { get; set; }

		// Token: 0x06002897 RID: 10391 RVA: 0x000F05E0 File Offset: 0x000EE9E0
		private void Init()
		{
			this.isReleased = false;
			this.fadeTime = null;
			if (this.stand != null)
			{
				this.stand.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
			}
			this.bkFadeDat = new FadeData(Singleton<Scene>.Instance.sceneFade);
			TextScenario textScenario = this.scenario;
			Camera main = Camera.main;
			this._advCamera = main;
			textScenario.AdvCamera = main;
			if (Singleton<Map>.IsInstance() && Singleton<Map>.Instance.Player != null)
			{
				this.scenario.virtualCamera = (Singleton<Map>.Instance.Player.CameraControl.ADVCamera as CinemachineVirtualCamera);
			}
			ParameterList.Init();
			if ((this.scenario.isWait || this.fadeTime != null) && this.advFade != null)
			{
				ADVFade advfade = this.advFade;
				bool isFront = true;
				float alpha = 1f;
				float? fadeTime = this.fadeTime;
				advfade.CrossFadeAlpha(isFront, alpha, (fadeTime == null) ? 0f : fadeTime.Value, true);
				this.scenario.isWait = true;
				IObservable<Unit> source = (from _ in this.UpdateAsObservable()
				where this.advFade.IsFadeInEnd
				select _).Take(1);
				Action<Unit> onNext = delegate(Unit _)
				{
					this.scenario.isWait = false;
				};
				if (ADVScene.<>f__mg$cache0 == null)
				{
					ADVScene.<>f__mg$cache0 = new Action(ParameterList.WaitEndProc);
				}
				this.updateDisposable = source.Subscribe(onNext, ADVScene.<>f__mg$cache0);
			}
			this.scenario.ConfigProc();
		}

		// Token: 0x06002898 RID: 10392 RVA: 0x000F0774 File Offset: 0x000EEB74
		public void Release()
		{
			if (Scene.isGameEnd)
			{
				return;
			}
			if (this.isReleased)
			{
				return;
			}
			this.isReleased = true;
			if (this.updateDisposable != null)
			{
				this.updateDisposable.Dispose();
			}
			this.updateDisposable = null;
			this.scenario.Release();
			if (!Singleton<Scene>.IsInstance())
			{
				return;
			}
			if (this.bkFadeDat != null)
			{
				this.bkFadeDat.Load(Singleton<Scene>.Instance.sceneFade);
			}
			ParameterList.Release();
		}

		// Token: 0x06002899 RID: 10393 RVA: 0x000F07FC File Offset: 0x000EEBFC
		private void OnEnable()
		{
			SceneParameter.advScene = this;
			this.scenario.OnInitializedAsync.TakeUntilDestroy(this).Subscribe(delegate(Unit _)
			{
				this.Init();
			});
			this.startAddSceneName = (Singleton<Scene>.IsInstance() ? Singleton<Scene>.Instance.AddSceneNameOverlapRemoved : string.Empty);
		}

		// Token: 0x0600289A RID: 10394 RVA: 0x000F0856 File Offset: 0x000EEC56
		private void OnDisable()
		{
		}

		// Token: 0x0600289B RID: 10395 RVA: 0x000F0858 File Offset: 0x000EEC58
		private void Update()
		{
			if (Singleton<Game>.IsInstance())
			{
				ConfigWindow config = Singleton<Game>.Instance.Config;
				if (config != null)
				{
					config.CharaEntryInteractable(false);
					this.scenario.ConfigProc();
				}
			}
		}

		// Token: 0x04002A1B RID: 10779
		private FadeData bkFadeDat;

		// Token: 0x04002A1D RID: 10781
		[SerializeField]
		private TextScenario scenario;

		// Token: 0x04002A1E RID: 10782
		[SerializeField]
		private Transform stand;

		// Token: 0x04002A1F RID: 10783
		[SerializeField]
		private ADVFade advFade;

		// Token: 0x04002A21 RID: 10785
		private bool isReleased;

		// Token: 0x04002A23 RID: 10787
		private const string CreateCameraName = "FrontCamera";

		// Token: 0x04002A24 RID: 10788
		private const string CameraAssetName = "ActionCamera";

		// Token: 0x04002A25 RID: 10789
		private IDisposable updateDisposable;

		// Token: 0x04002A26 RID: 10790
		[CompilerGenerated]
		private static Action <>f__mg$cache0;
	}
}
