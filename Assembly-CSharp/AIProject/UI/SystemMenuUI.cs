using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AIProject.Scene;
using ConfigScene;
using Manager;
using ReMotion;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace AIProject.UI
{
	// Token: 0x02000FED RID: 4077
	public class SystemMenuUI : SerializedMonoBehaviour
	{
		// Token: 0x17001E05 RID: 7685
		// (get) Token: 0x0600892C RID: 35116 RVA: 0x0039131A File Offset: 0x0038F71A
		// (set) Token: 0x0600892D RID: 35117 RVA: 0x00391327 File Offset: 0x0038F727
		public bool IsActiveControl
		{
			get
			{
				return this._isActive.Value;
			}
			set
			{
				if (this._isActive.Value && this._notifyingUI.IsActiveControl)
				{
					return;
				}
				this._visible = value;
				this._isActive.Value = value;
			}
		}

		// Token: 0x0600892E RID: 35118 RVA: 0x0039135D File Offset: 0x0038F75D
		protected IObservable<bool> OnActiveChangedAsObservable()
		{
			if (this._activeChanged == null)
			{
				this._activeChanged = this._isActive.TakeUntilDestroy(base.gameObject).Publish<bool>();
				this._activeChanged.Connect();
			}
			return this._activeChanged;
		}

		// Token: 0x17001E06 RID: 7686
		// (get) Token: 0x0600892F RID: 35119 RVA: 0x00391398 File Offset: 0x0038F798
		// (set) Token: 0x06008930 RID: 35120 RVA: 0x003913A0 File Offset: 0x0038F7A0
		public bool Visible
		{
			get
			{
				return this._visible;
			}
			set
			{
				if (this._visible == value)
				{
					return;
				}
				this._visible = value;
				if (this._visibleChangeDisposable != null)
				{
					this._visibleChangeDisposable.Dispose();
				}
				float startAlpha = this._canvasGroup.alpha;
				int destAlpha = (!value) ? 0 : 1;
				if (!value)
				{
					this._canvasGroup.blocksRaycasts = false;
				}
				this._visibleChangeDisposable = ObservableEasing.Linear(0.3f, true).FrameTimeInterval(true).DoOnCompleted(delegate
				{
					if (value)
					{
						this._canvasGroup.blocksRaycasts = true;
					}
				}).Subscribe(delegate(TimeInterval<float> x)
				{
					this._canvasGroup.alpha = Mathf.Lerp(startAlpha, (float)destAlpha, x.Value);
				});
			}
		}

		// Token: 0x17001E07 RID: 7687
		// (get) Token: 0x06008931 RID: 35121 RVA: 0x00391470 File Offset: 0x0038F870
		// (set) Token: 0x06008932 RID: 35122 RVA: 0x00391478 File Offset: 0x0038F878
		public bool ActiveCloseButton
		{
			get
			{
				return this._activeCloseButton;
			}
			set
			{
				if (this._activeCloseButton == value)
				{
					return;
				}
				this._activeCloseButton = value;
				if (this._activeCloseDisposable != null)
				{
					this._activeCloseDisposable.Dispose();
				}
				if (value)
				{
					this._activeCloseDisposable = ObservableEasing.Linear(0.1f, true).FrameTimeInterval(true).Subscribe(delegate(TimeInterval<float> x)
					{
						this._closeButtonCanvasGroup.alpha = x.Value;
					}, delegate()
					{
						this._closeButtonCanvasGroup.blocksRaycasts = true;
					});
				}
				else
				{
					this._closeButtonCanvasGroup.blocksRaycasts = false;
					this._activeCloseDisposable = ObservableEasing.Linear(0.1f, true).FrameTimeInterval(true).Subscribe(delegate(TimeInterval<float> x)
					{
						this._closeButtonCanvasGroup.alpha = 1f - x.Value;
					});
				}
			}
		}

		// Token: 0x17001E08 RID: 7688
		// (get) Token: 0x06008933 RID: 35123 RVA: 0x00391524 File Offset: 0x0038F924
		public HomeMenu HomeMenu
		{
			[CompilerGenerated]
			get
			{
				return this._homeMenu;
			}
		}

		// Token: 0x17001E09 RID: 7689
		// (get) Token: 0x06008934 RID: 35124 RVA: 0x0039152C File Offset: 0x0038F92C
		public StatusUI StatusUI
		{
			[CompilerGenerated]
			get
			{
				return this._statusUI;
			}
		}

		// Token: 0x17001E0A RID: 7690
		// (get) Token: 0x06008935 RID: 35125 RVA: 0x00391534 File Offset: 0x0038F934
		public InventoryUIController InventoryUI
		{
			[CompilerGenerated]
			get
			{
				return this._inventoryUI;
			}
		}

		// Token: 0x17001E0B RID: 7691
		// (get) Token: 0x06008936 RID: 35126 RVA: 0x0039153C File Offset: 0x0038F93C
		public InventoryUIController InventoryEnterUI
		{
			[CompilerGenerated]
			get
			{
				return this._inventoryEnterUI;
			}
		}

		// Token: 0x17001E0C RID: 7692
		// (get) Token: 0x06008937 RID: 35127 RVA: 0x00391544 File Offset: 0x0038F944
		// (set) Token: 0x06008938 RID: 35128 RVA: 0x0039154C File Offset: 0x0038F94C
		public Action OnClose { get; set; }

		// Token: 0x06008939 RID: 35129 RVA: 0x00391555 File Offset: 0x0038F955
		public void ReserveMove(SystemMenuUI.MenuMode mode)
		{
			this._reservedMode = new SystemMenuUI.MenuMode?(mode);
		}

		// Token: 0x0600893A RID: 35130 RVA: 0x00391564 File Offset: 0x0038F964
		private void Start()
		{
			this.OnActiveChangedAsObservable().Subscribe(delegate(bool x)
			{
				this.SetActiveControl(x);
			});
			DefinePack definePack = Singleton<Manager.Resources>.Instance.DefinePack;
			string manifestName = "abdata";
			GameObject gameObject = CommonLib.LoadAsset<GameObject>(definePack.ABPaths.MapScenePrefab, "HomeMenu", false, manifestName);
			if (gameObject != null)
			{
				this._homeMenu = UnityEngine.Object.Instantiate<GameObject>(gameObject, this._uiRoot, false).GetComponent<HomeMenu>();
				this._homeMenu.Observer = this;
				this._homeMenu.OnClose = delegate()
				{
					this._homeMenu.IsActiveControl = false;
					this.IsActiveControl = false;
				};
			}
			gameObject = CommonLib.LoadAsset<GameObject>(definePack.ABPaths.MapScenePrefab, "InventoryUI", false, manifestName);
			if (gameObject != null)
			{
				this._inventoryUI = UnityEngine.Object.Instantiate<GameObject>(gameObject, this._uiRoot, false).GetComponent<InventoryUIController>();
				this._inventoryUI.capacity = (() => Singleton<Map>.Instance.Player.PlayerData.InventorySlotMax);
				this._inventoryUI.itemList = (() => Singleton<Map>.Instance.Player.PlayerData.ItemList);
				this._inventoryUI.Observer = this;
			}
			gameObject = CommonLib.LoadAsset<GameObject>(definePack.ABPaths.MapScenePrefab, "InventoryEnterUI", false, manifestName);
			if (gameObject != null)
			{
				this._inventoryEnterUI = UnityEngine.Object.Instantiate<GameObject>(gameObject, this._uiRoot, false).GetComponent<InventoryUIController>();
				this._inventoryEnterUI.capacity = (() => Singleton<Map>.Instance.Player.PlayerData.InventorySlotMax);
				this._inventoryEnterUI.itemList = (() => Singleton<Map>.Instance.Player.PlayerData.ItemList);
				this._inventoryEnterUI.Observer = this;
			}
			gameObject = CommonLib.LoadAsset<GameObject>(definePack.ABPaths.MapScenePrefabAdd05, "StatusUI", false, definePack.ABManifests.Add05);
			if (gameObject != null)
			{
				this._statusUI = UnityEngine.Object.Instantiate<GameObject>(gameObject, this._uiRoot, false).GetComponent<StatusUI>();
				this._statusUI.Observer = this;
				this._statusUI.OnClose = delegate()
				{
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
					this._statusUI.OpenID = 0;
					this._statusUI.IsActiveControl = false;
					this.OpenModeMenu(SystemMenuUI.MenuMode.Home);
				};
			}
			this._notifyingUI.PouchOpen.onClick.AddListener(delegate()
			{
				this._notifyingUI.Hide();
			});
			this._notifyingUI.NotGet.onClick.AddListener(delegate()
			{
				this._notifyingUI.Hide();
				this.IsActiveControl = false;
			});
			if (this._closeButton)
			{
				this._closeButton.onClick.AddListener(delegate()
				{
					Action onClose = this.OnClose;
					if (onClose != null)
					{
						onClose();
					}
				});
				this._closeButtonCanvasGroup = this._closeButton.GetComponent<CanvasGroup>();
			}
		}

		// Token: 0x0600893B RID: 35131 RVA: 0x00391814 File Offset: 0x0038FC14
		private void SetActiveControl(bool active)
		{
			if (this._fadeDisposable != null)
			{
				this._fadeDisposable.Dispose();
			}
			Manager.Input instance = Singleton<Manager.Input>.Instance;
			IEnumerator coroutine;
			if (active)
			{
				this.ChangeBackground(-1);
				Time.timeScale = 0f;
				Singleton<Map>.Instance.Player.SetScheduledInteractionState(false);
				Singleton<Map>.Instance.Player.ReleaseInteraction();
				instance.FocusLevel = 0;
				coroutine = this.DoOpen();
				if (this._reservedMode != null)
				{
					this.OpenModeMenu(this._reservedMode.Value);
					this._reservedMode = null;
				}
				else
				{
					this.OpenModeMenu(SystemMenuUI.MenuMode.Home);
				}
			}
			else
			{
				instance.ClearMenuElements();
				instance.FocusLevel = -1;
				coroutine = this.DoClose();
			}
			this._fadeStream = Observable.FromCoroutine(() => coroutine, false).PublishLast<Unit>();
			this._fadeDisposable = this._fadeStream.Connect();
		}

		// Token: 0x0600893C RID: 35132 RVA: 0x00391918 File Offset: 0x0038FD18
		private IEnumerator DoOpen()
		{
			Manager.Input inputManager = Singleton<Manager.Input>.Instance;
			inputManager.ReserveState(Manager.Input.ValidType.UI);
			inputManager.SetupState();
			if (this._canvasGroup.blocksRaycasts)
			{
				this._canvasGroup.blocksRaycasts = false;
			}
			IObservable<TimeInterval<float>> lerpFadeInStream = ObservableEasing.Linear(0.3f, true).FrameTimeInterval(true);
			IConnectableObservable<TimeInterval<float>> lerpConnStream = lerpFadeInStream.Publish<TimeInterval<float>>();
			float startAlpha = this._canvasGroup.alpha;
			lerpConnStream = lerpFadeInStream.Publish<TimeInterval<float>>();
			lerpConnStream.Connect();
			lerpConnStream.Subscribe(delegate(TimeInterval<float> x)
			{
				this._canvasGroup.alpha = Mathf.Lerp(startAlpha, 1f, x.Value);
			});
			yield return lerpConnStream.ToYieldInstruction<TimeInterval<float>>();
			this._visible = true;
			this._canvasGroup.blocksRaycasts = true;
			Singleton<Manager.Input>.Instance.FocusLevel = 0;
			yield break;
		}

		// Token: 0x0600893D RID: 35133 RVA: 0x00391934 File Offset: 0x0038FD34
		private IEnumerator DoClose()
		{
			this._visible = false;
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.Action);
			this._canvasGroup.blocksRaycasts = false;
			Time.timeScale = 1f;
			Singleton<Manager.Input>.Instance.SetupState();
			this._notifyingUI.Hide();
			float startAlpha = this._canvasGroup.alpha;
			IObservable<TimeInterval<float>> lerpFadeOutStream = ObservableEasing.Linear(0.3f, true).FrameTimeInterval(true);
			IConnectableObservable<TimeInterval<float>> lerpConnStream = lerpFadeOutStream.Publish<TimeInterval<float>>();
			lerpConnStream.Connect();
			lerpConnStream.Subscribe(delegate(TimeInterval<float> x)
			{
				this._canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, x.Value);
			});
			yield return lerpConnStream.ToYieldInstruction<TimeInterval<float>>();
			Singleton<Map>.Instance.Player.SetScheduledInteractionState(true);
			Singleton<Map>.Instance.Player.ReleaseInteraction();
			yield break;
		}

		// Token: 0x0600893E RID: 35134 RVA: 0x0039194F File Offset: 0x0038FD4F
		public void SetNotify(string notifyText)
		{
			this._notifyingUI.Display(notifyText);
		}

		// Token: 0x0600893F RID: 35135 RVA: 0x00391960 File Offset: 0x0038FD60
		public void OpenModeMenu(SystemMenuUI.MenuMode mode)
		{
			SystemMenuUI.<OpenModeMenu>c__AnonStorey6 <OpenModeMenu>c__AnonStorey = new SystemMenuUI.<OpenModeMenu>c__AnonStorey6();
			<OpenModeMenu>c__AnonStorey.$this = this;
			switch (mode + 1)
			{
			case SystemMenuUI.MenuMode.Status:
				this._homeMenu.UsageRestriction();
				this._homeMenu.IsActiveControl = true;
				break;
			case SystemMenuUI.MenuMode.Inventory:
				this._statusUI.IsActiveControl = true;
				break;
			case SystemMenuUI.MenuMode.Map:
				this._inventoryUI.IsActiveControl = true;
				break;
			case SystemMenuUI.MenuMode.Craft:
				this.Visible = false;
				Singleton<MapUIContainer>.Instance.MinimapUI.OpenAllMap(-1);
				Singleton<MapUIContainer>.Instance.MinimapUI.FromHomeMenu = true;
				<OpenModeMenu>c__AnonStorey.prevMapVisibleMode = Singleton<MapUIContainer>.Instance.MinimapUI.VisibleMode;
				Singleton<MapUIContainer>.Instance.MinimapUI.VisibleMode = 1;
				Singleton<MapUIContainer>.Instance.MinimapUI.AllMapClosedAction = delegate()
				{
					<OpenModeMenu>c__AnonStorey.$this.Visible = true;
					<OpenModeMenu>c__AnonStorey.$this.OpenModeMenu(SystemMenuUI.MenuMode.Home);
					Singleton<MapUIContainer>.Instance.MinimapUI.AllMapClosedAction = null;
					bool miniMap = Config.GameData.MiniMap;
					if (<OpenModeMenu>c__AnonStorey.prevMapVisibleMode == 0 && miniMap)
					{
						Singleton<MapUIContainer>.Instance.MinimapUI.OpenMiniMap();
					}
				};
				Singleton<MapUIContainer>.Instance.MinimapUI.WarpProc = delegate(BasePoint x)
				{
					string prevStateName = Singleton<Map>.Instance.Player.PlayerController.PrevStateName;
					Singleton<MapUIContainer>.Instance.MinimapUI.AllMapClosedAction = delegate()
					{
					};
					Singleton<Map>.Instance.WarpToBasePoint(x, delegate
					{
						<OpenModeMenu>c__AnonStorey.IsActiveControl = false;
						if (prevStateName == "Onbu")
						{
							Singleton<Map>.Instance.Player.Controller.ChangeState("Onbu");
						}
						else
						{
							Singleton<Map>.Instance.Player.Controller.ChangeState("Normal");
						}
						Singleton<Map>.Instance.Player.Controller.ChangeState("Idle");
						GC.Collect();
						bool miniMap = Config.GameData.MiniMap;
						if (<OpenModeMenu>c__AnonStorey.prevMapVisibleMode == 0 && miniMap)
						{
							Singleton<MapUIContainer>.Instance.MinimapUI.OpenMiniMap();
						}
					}, delegate
					{
						if (prevStateName == "Onbu")
						{
							Singleton<Map>.Instance.Player.Controller.ChangeState("Onbu");
						}
						else
						{
							Singleton<Map>.Instance.Player.Controller.ChangeState("Normal");
						}
						Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.Action);
						Singleton<Manager.Input>.Instance.SetupState();
						Singleton<Map>.Instance.Player.SetScheduledInteractionState(true);
						Singleton<Map>.Instance.Player.ReleaseInteraction();
					});
					Singleton<MapUIContainer>.Instance.MinimapUI.WarpProc = null;
				};
				break;
			case SystemMenuUI.MenuMode.Camera:
				this.Visible = false;
				MapUIContainer.SetActiveCraftUI(true);
				MapUIContainer.CraftUI.Observer = this;
				MapUIContainer.CraftUI.OnClose = delegate()
				{
					<OpenModeMenu>c__AnonStorey.$this.Visible = true;
					<OpenModeMenu>c__AnonStorey.$this.OpenModeMenu(SystemMenuUI.MenuMode.Home);
					MapUIContainer.CraftUI.OnClose = null;
				};
				break;
			case SystemMenuUI.MenuMode.Call:
			{
				PlayerActor player = Singleton<Map>.Instance.Player;
				player.PlayerController.ChangeState("Photo");
				Action onClose = this.OnClose;
				if (onClose != null)
				{
					onClose();
				}
				break;
			}
			case SystemMenuUI.MenuMode.Help:
			{
				PlayerActor player2 = Singleton<Map>.Instance.Player;
				player2.CallProc();
				Action onClose2 = this.OnClose;
				if (onClose2 != null)
				{
					onClose2();
				}
				break;
			}
			case SystemMenuUI.MenuMode.Log:
				this.Visible = false;
				MapUIContainer.TutorialUI.ClosedEvent = delegate()
				{
					<OpenModeMenu>c__AnonStorey.$this.Visible = true;
					MapUIContainer.SetVisibleHUDExceptStoryUI(true);
					<OpenModeMenu>c__AnonStorey.$this.OpenModeMenu(SystemMenuUI.MenuMode.Home);
				};
				MapUIContainer.SetVisibleHUDExceptStoryUI(false);
				MapUIContainer.TutorialUI.SetCondition(-1, true);
				MapUIContainer.SetActiveTutorialUI(true);
				break;
			case SystemMenuUI.MenuMode.Save:
				this.Visible = false;
				MapUIContainer.GameLogUI.IsActiveControl = true;
				MapUIContainer.GameLogUI.OnClosed = delegate()
				{
					<OpenModeMenu>c__AnonStorey.$this.Visible = true;
					<OpenModeMenu>c__AnonStorey.$this.OpenModeMenu(SystemMenuUI.MenuMode.Home);
					MapUIContainer.GameLogUI.OnClosed = null;
				};
				break;
			case SystemMenuUI.MenuMode.InventoryEnter:
				this.Visible = false;
				if (this._charasEntry == null || this._charasEntry.Length != Config.GraphicData.CharasEntry.Length)
				{
					this._charasEntry = new bool[Config.GraphicData.CharasEntry.Length];
				}
				for (int i = 0; i < this._charasEntry.Length; i++)
				{
					this._charasEntry[i] = Config.GraphicData.CharasEntry[i];
				}
				ConfigWindow.UnLoadAction = delegate()
				{
					if (!MapScene.EqualsSequence(<OpenModeMenu>c__AnonStorey.$this._charasEntry, Config.GraphicData.CharasEntry))
					{
						bool interactable = <OpenModeMenu>c__AnonStorey.$this._canvasGroup.interactable;
						<OpenModeMenu>c__AnonStorey.$this._canvasGroup.interactable = true;
						Singleton<Map>.Instance.ApplyConfig(null, delegate
						{
							<OpenModeMenu>c__AnonStorey._canvasGroup.interactable = interactable;
							<OpenModeMenu>c__AnonStorey.Visible = true;
							<OpenModeMenu>c__AnonStorey.OpenModeMenu(SystemMenuUI.MenuMode.Home);
						});
					}
					else
					{
						<OpenModeMenu>c__AnonStorey.$this.Visible = true;
						<OpenModeMenu>c__AnonStorey.$this.OpenModeMenu(SystemMenuUI.MenuMode.Home);
					}
				};
				ConfigWindow.TitleChangeAction = delegate()
				{
					ConfigWindow.UnLoadAction = null;
					Singleton<Game>.Instance.Config.timeScaleChange = 1f;
					Singleton<Game>.Instance.Dialog.TimeScale = 1f;
				};
				Singleton<Game>.Instance.LoadConfig();
				break;
			case (SystemMenuUI.MenuMode)11:
				this._inventoryEnterUI.IsActiveControl = true;
				break;
			}
		}

		// Token: 0x06008940 RID: 35136 RVA: 0x00391C3C File Offset: 0x0039003C
		public void ChangeBackground(int id)
		{
			IDisposable[] array;
			if ((array = this._backgroundDisposables) == null)
			{
				array = (this._backgroundDisposables = new IDisposable[this._backgrounds.Count]);
			}
			foreach (IDisposable disposable in array)
			{
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			IObservable<TimeInterval<float>> source = ObservableEasing.Linear(0.3f, true).FrameTimeInterval(true);
			int num = 0;
			using (Dictionary<int, CanvasGroup>.Enumerator enumerator = this._backgrounds.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					SystemMenuUI.<ChangeBackground>c__AnonStorey9 <ChangeBackground>c__AnonStorey = new SystemMenuUI.<ChangeBackground>c__AnonStorey9();
					<ChangeBackground>c__AnonStorey.kvp = enumerator.Current;
					float startAlpha = <ChangeBackground>c__AnonStorey.kvp.Value.alpha;
					int destAlpha = (<ChangeBackground>c__AnonStorey.kvp.Key != id) ? 0 : 1;
					this._backgroundDisposables[num++] = source.Subscribe(delegate(TimeInterval<float> x)
					{
						CanvasGroup value = <ChangeBackground>c__AnonStorey.kvp.Value;
						value.alpha = Mathf.Lerp(startAlpha, (float)destAlpha, x.Value);
					});
				}
			}
		}

		// Token: 0x04006F0B RID: 28427
		[SerializeField]
		[DisableInEditorMode]
		[DisableInPlayMode]
		private BoolReactiveProperty _isActive = new BoolReactiveProperty(false);

		// Token: 0x04006F0C RID: 28428
		private IConnectableObservable<bool> _activeChanged;

		// Token: 0x04006F0D RID: 28429
		private bool _visible = true;

		// Token: 0x04006F0E RID: 28430
		private IDisposable _visibleChangeDisposable;

		// Token: 0x04006F0F RID: 28431
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x04006F10 RID: 28432
		[SerializeField]
		private RectTransform _uiRoot;

		// Token: 0x04006F11 RID: 28433
		[SerializeField]
		private Button _closeButton;

		// Token: 0x04006F12 RID: 28434
		private bool _activeCloseButton;

		// Token: 0x04006F13 RID: 28435
		private CanvasGroup _closeButtonCanvasGroup;

		// Token: 0x04006F14 RID: 28436
		private IDisposable _activeCloseDisposable;

		// Token: 0x04006F15 RID: 28437
		[SerializeField]
		private NotifyingUI _notifyingUI;

		// Token: 0x04006F16 RID: 28438
		[SerializeField]
		private Dictionary<int, CanvasGroup> _backgrounds = new Dictionary<int, CanvasGroup>();

		// Token: 0x04006F17 RID: 28439
		[SerializeField]
		[DisableInEditorMode]
		[DisableInPlayMode]
		private HomeMenu _homeMenu;

		// Token: 0x04006F18 RID: 28440
		[SerializeField]
		[DisableInEditorMode]
		[DisableInPlayMode]
		private StatusUI _statusUI;

		// Token: 0x04006F19 RID: 28441
		[SerializeField]
		[DisableInEditorMode]
		[DisableInPlayMode]
		private InventoryUIController _inventoryUI;

		// Token: 0x04006F1A RID: 28442
		[SerializeField]
		[DisableInEditorMode]
		[DisableInPlayMode]
		private InventoryUIController _inventoryEnterUI;

		// Token: 0x04006F1C RID: 28444
		private SystemMenuUI.MenuMode? _reservedMode;

		// Token: 0x04006F1D RID: 28445
		private IConnectableObservable<Unit> _fadeStream;

		// Token: 0x04006F1E RID: 28446
		private IDisposable _fadeDisposable;

		// Token: 0x04006F1F RID: 28447
		private bool[] _charasEntry;

		// Token: 0x04006F20 RID: 28448
		private IDisposable[] _backgroundDisposables;

		// Token: 0x02000FEE RID: 4078
		public enum MenuMode
		{
			// Token: 0x04006F27 RID: 28455
			Home = -1,
			// Token: 0x04006F28 RID: 28456
			Status,
			// Token: 0x04006F29 RID: 28457
			Inventory,
			// Token: 0x04006F2A RID: 28458
			Map,
			// Token: 0x04006F2B RID: 28459
			Craft,
			// Token: 0x04006F2C RID: 28460
			Camera,
			// Token: 0x04006F2D RID: 28461
			Call,
			// Token: 0x04006F2E RID: 28462
			Help,
			// Token: 0x04006F2F RID: 28463
			Log,
			// Token: 0x04006F30 RID: 28464
			Save,
			// Token: 0x04006F31 RID: 28465
			Config,
			// Token: 0x04006F32 RID: 28466
			InventoryEnter
		}
	}
}
