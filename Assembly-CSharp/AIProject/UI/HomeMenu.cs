using System;
using System.Collections;
using AIProject.Scene;
using Manager;
using ReMotion;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace AIProject.UI
{
	// Token: 0x02000FEB RID: 4075
	public class HomeMenu : MenuUIBehaviour
	{
		// Token: 0x17001E01 RID: 7681
		// (get) Token: 0x060088FE RID: 35070 RVA: 0x00390039 File Offset: 0x0038E439
		// (set) Token: 0x060088FF RID: 35071 RVA: 0x00390041 File Offset: 0x0038E441
		public SystemMenuUI Observer { get; set; }

		// Token: 0x17001E02 RID: 7682
		// (get) Token: 0x06008900 RID: 35072 RVA: 0x0039004A File Offset: 0x0038E44A
		// (set) Token: 0x06008901 RID: 35073 RVA: 0x00390052 File Offset: 0x0038E452
		public Action OnClose { get; set; }

		// Token: 0x06008902 RID: 35074 RVA: 0x0039005C File Offset: 0x0038E45C
		protected override void OnBeforeStart()
		{
			base.OnActiveChangedAsObservable().Subscribe(delegate(bool x)
			{
				this.SetActiveControl(x);
			});
			(from _ in Observable.EveryUpdate().TakeUntilDestroy(base.gameObject)
			where base.isActiveAndEnabled && this.IsActiveControl
			select _).OnErrorRetry<long>().Subscribe(delegate(long _)
			{
				this.OnUpdate();
			});
			this._statsText.text = "ステータス";
			this._inventoryText.text = "ポーチ";
			this._mapText.text = "マップ";
			this._craftText.text = "クラフト";
			this._cameraText.text = "カメラ";
			this._callText.text = "コール";
			this._helpText.text = "ヘルプ";
			this._logText.text = "ログ";
			this._saveText.text = "セーブ";
			this._optionText.text = "オプション";
			if (this._statsButton)
			{
				this._statsButton.onClick.AddListener(delegate()
				{
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
					this.OpenWindow(0);
				});
			}
			if (this._inventoryButton)
			{
				this._inventoryButton.onClick.AddListener(delegate()
				{
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
					SystemMenuUI systemUI = MapUIContainer.SystemMenuUI;
					InventoryUIController inventoryUI = systemUI.InventoryUI;
					inventoryUI.OnClose = delegate()
					{
						inventoryUI.IsActiveControl = false;
						systemUI.OpenModeMenu(SystemMenuUI.MenuMode.Home);
						inventoryUI.OnClose = null;
					};
					this.OpenWindow(1);
				});
			}
			if (this._mapButton)
			{
				this._mapButton.onClick.AddListener(delegate()
				{
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
					this.OpenWindow(2);
				});
			}
			if (this._craftButton)
			{
				this._craftButton.onClick.AddListener(delegate()
				{
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
					this.OpenWindow(3);
				});
			}
			if (this._cameraButton)
			{
				this._cameraButton.onClick.AddListener(delegate()
				{
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
					this.OpenWindow(4);
				});
			}
			if (this._callButton)
			{
				this._callButton.onClick.AddListener(delegate()
				{
					this.OpenWindow(5);
				});
			}
			if (this._helpButton)
			{
				this._helpButton.onClick.AddListener(delegate()
				{
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
					this.OpenWindow(6);
				});
			}
			if (this._logButton)
			{
				this._logButton.onClick.AddListener(delegate()
				{
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
					this.OpenWindow(7);
				});
			}
			if (this._saveButton)
			{
				this._saveButton.onClick.AddListener(delegate()
				{
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
					ConfirmScene.Sentence = "セーブしますか？";
					ConfirmScene.OnClickedYes = delegate()
					{
						Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Save);
						Singleton<MapScene>.Instance.SaveProfile(false);
						if (Singleton<Game>.IsInstance())
						{
							Singleton<Game>.Instance.SaveGlobalData();
						}
						MapUIContainer.AddNotify("セーブしました");
					};
					ConfirmScene.OnClickedNo = delegate()
					{
						Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
					};
					Singleton<Game>.Instance.LoadDialog();
				});
			}
			if (this._optionButton)
			{
				this._optionButton.onClick.AddListener(delegate()
				{
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
					this.OpenWindow(9);
				});
			}
			KeyCodeDownCommand keyCodeDownCommand = new KeyCodeDownCommand
			{
				KeyCode = KeyCode.Mouse1
			};
			keyCodeDownCommand.TriggerEvent.AddListener(delegate()
			{
				this.Close();
			});
			this._keyCommands.Add(keyCodeDownCommand);
			base.EnabledInput = false;
		}

		// Token: 0x06008903 RID: 35075 RVA: 0x00390360 File Offset: 0x0038E760
		private void SetActiveControl(bool active)
		{
			Manager.Input instance = Singleton<Manager.Input>.Instance;
			IEnumerator coroutine;
			if (active)
			{
				Time.timeScale = 0f;
				Singleton<Map>.Instance.Player.SetScheduledInteractionState(false);
				Singleton<Map>.Instance.Player.ReleaseInteraction();
				instance.FocusLevel = 0;
				instance.MenuElements = this.MenuUIList;
				coroutine = this.DoOpen();
			}
			else
			{
				instance.ClearMenuElements();
				instance.FocusLevel = -1;
				coroutine = this.DoClose();
			}
			if (this._fadeSubscriber != null)
			{
				this._fadeSubscriber.Dispose();
			}
			this._fadeSubscriber = Observable.FromCoroutine(() => coroutine, false).Subscribe(delegate(Unit _)
			{
			}, delegate(Exception ex)
			{
			});
		}

		// Token: 0x17001E03 RID: 7683
		// (get) Token: 0x06008904 RID: 35076 RVA: 0x00390454 File Offset: 0x0038E854
		public MenuUIBehaviour[] MenuUIList
		{
			get
			{
				MenuUIBehaviour[] result;
				if ((result = this._menuUIList) == null)
				{
					result = (this._menuUIList = new MenuUIBehaviour[]
					{
						this
					});
				}
				return result;
			}
		}

		// Token: 0x06008905 RID: 35077 RVA: 0x00390484 File Offset: 0x0038E884
		private void OnUpdate()
		{
			if (!Singleton<Map>.IsInstance())
			{
				return;
			}
			if (Singleton<Map>.Instance.Simulator == null)
			{
				return;
			}
			EnvironmentSimulator simulator = Singleton<Map>.Instance.Simulator;
			DateTime now = simulator.Now;
			if (!this.EqualsTime(now))
			{
				this._hour = now.Hour;
				this._minute = now.Minute;
				this._timeLabel.text = string.Format("{0:00}：{1:00}", this._hour, this._minute);
			}
			int num = (int)simulator.TemperatureValue;
			if (this._temperature != num)
			{
				this._temperature = num;
				this._temperatureLabel.text = string.Format("{0:0}℃", this._temperature);
			}
			if (this._weather != simulator.Weather || this._weatherIconID == -1)
			{
				this._weather = simulator.Weather;
				switch (this._weather)
				{
				case Weather.Clear:
				case Weather.Cloud1:
				case Weather.Cloud2:
					this._weatherIconID = 0;
					break;
				case Weather.Cloud3:
				case Weather.Cloud4:
					this._weatherIconID = 1;
					break;
				case Weather.Fog:
					this._weatherIconID = 4;
					break;
				case Weather.Rain:
					this._weatherIconID = 2;
					break;
				case Weather.Storm:
					this._weatherIconID = 3;
					break;
				}
				this._weatherImage.sprite = null;
				this._weatherImage.sprite = Singleton<Manager.Resources>.Instance.itemIconTables.WeatherIconTable[this._weatherIconID];
			}
		}

		// Token: 0x06008906 RID: 35078 RVA: 0x00390618 File Offset: 0x0038EA18
		private bool EqualsTime(DateTime time)
		{
			return this._hour == time.Hour && this._minute == time.Minute;
		}

		// Token: 0x06008907 RID: 35079 RVA: 0x0039063E File Offset: 0x0038EA3E
		private void OpenWindow(int id)
		{
			if (this.Observer != null)
			{
				this.IsActiveControl = false;
				this.Observer.OpenModeMenu((SystemMenuUI.MenuMode)id);
			}
		}

		// Token: 0x06008908 RID: 35080 RVA: 0x00390664 File Offset: 0x0038EA64
		private void Close()
		{
			Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
			Action onClose = this.OnClose;
			if (onClose != null)
			{
				onClose();
			}
		}

		// Token: 0x06008909 RID: 35081 RVA: 0x0039068C File Offset: 0x0038EA8C
		private IEnumerator DoOpen()
		{
			if (this.Observer != null)
			{
				this.Observer.OnClose = delegate()
				{
					this.Close();
				};
			}
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.UI);
			Singleton<Manager.Input>.Instance.SetupState();
			if (this._canvasGroup.blocksRaycasts)
			{
				this._canvasGroup.blocksRaycasts = false;
			}
			float startAlpha = this._canvasGroup.alpha;
			IConnectableObservable<TimeInterval<float>> fadeInStream = ObservableEasing.Linear(0.3f, true).FrameTimeInterval(true).Publish<TimeInterval<float>>();
			fadeInStream.Subscribe(delegate(TimeInterval<float> x)
			{
				this._canvasGroup.alpha = Mathf.Lerp(startAlpha, 1f, x.Value);
			});
			fadeInStream.Connect();
			yield return fadeInStream.ToYieldInstruction<TimeInterval<float>>();
			this._canvasGroup.blocksRaycasts = true;
			base.EnabledInput = true;
			this.Observer.ActiveCloseButton = true;
			yield break;
		}

		// Token: 0x0600890A RID: 35082 RVA: 0x003906A8 File Offset: 0x0038EAA8
		private IEnumerator DoClose()
		{
			this.Observer.ActiveCloseButton = false;
			base.EnabledInput = false;
			this._canvasGroup.blocksRaycasts = false;
			float startAlpha = this._canvasGroup.alpha;
			IConnectableObservable<TimeInterval<float>> fadeOutStream = ObservableEasing.Linear(0.3f, true).FrameTimeInterval(true).Publish<TimeInterval<float>>();
			fadeOutStream.Subscribe(delegate(TimeInterval<float> x)
			{
				this._canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, x.Value);
			});
			fadeOutStream.Connect();
			yield return fadeOutStream.ToYieldInstruction<TimeInterval<float>>();
			yield break;
		}

		// Token: 0x0600890B RID: 35083 RVA: 0x003906C4 File Offset: 0x0038EAC4
		public void UsageRestriction()
		{
			bool tutorialMode = Map.TutorialMode;
			int tutorialProgress = Map.GetTutorialProgress();
			bool flag = true;
			PlayerActor player = Map.GetPlayer();
			if (player != null)
			{
				flag = (player.PlayerController.PrevStateName != "Onbu");
			}
			bool flag2 = !tutorialMode;
			this.SetInteractable(this._inventoryButton, flag2);
			this.SetInteractable(this._cameraButton, flag2 && flag);
			this.SetInteractable(this._logButton, flag2);
			this.SetInteractable(this._mapButton, flag2);
			this.SetInteractable(this._callButton, flag2);
			if (tutorialMode)
			{
				this.SetInteractable(this._statsButton, 5 <= tutorialProgress);
				this.SetInteractable(this._craftButton, 4 <= tutorialProgress);
			}
			else
			{
				this.SetInteractable(this._craftButton, true);
				this.SetInteractable(this._statsButton, true);
			}
		}

		// Token: 0x0600890C RID: 35084 RVA: 0x003907B0 File Offset: 0x0038EBB0
		private bool SetInteractable(Selectable tar, bool active)
		{
			if (tar == null)
			{
				return false;
			}
			if (tar.interactable != active)
			{
				tar.interactable = active;
				return true;
			}
			return false;
		}

		// Token: 0x04006EE0 RID: 28384
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x04006EE1 RID: 28385
		[SerializeField]
		private Text _timeLabel;

		// Token: 0x04006EE2 RID: 28386
		[SerializeField]
		private Image _weatherImage;

		// Token: 0x04006EE3 RID: 28387
		[SerializeField]
		private Text _temperatureLabel;

		// Token: 0x04006EE4 RID: 28388
		[Header("IconLabel")]
		[SerializeField]
		private Text _statsText;

		// Token: 0x04006EE5 RID: 28389
		[SerializeField]
		private Text _inventoryText;

		// Token: 0x04006EE6 RID: 28390
		[SerializeField]
		private Text _mapText;

		// Token: 0x04006EE7 RID: 28391
		[SerializeField]
		private Text _craftText;

		// Token: 0x04006EE8 RID: 28392
		[SerializeField]
		private Text _cameraText;

		// Token: 0x04006EE9 RID: 28393
		[SerializeField]
		private Text _callText;

		// Token: 0x04006EEA RID: 28394
		[SerializeField]
		private Text _helpText;

		// Token: 0x04006EEB RID: 28395
		[SerializeField]
		private Text _logText;

		// Token: 0x04006EEC RID: 28396
		[SerializeField]
		private Text _saveText;

		// Token: 0x04006EED RID: 28397
		[SerializeField]
		private Text _optionText;

		// Token: 0x04006EEE RID: 28398
		[Header("Icon (Button)")]
		[SerializeField]
		private Button _statsButton;

		// Token: 0x04006EEF RID: 28399
		[SerializeField]
		private Button _inventoryButton;

		// Token: 0x04006EF0 RID: 28400
		[SerializeField]
		private Button _mapButton;

		// Token: 0x04006EF1 RID: 28401
		[SerializeField]
		private Button _craftButton;

		// Token: 0x04006EF2 RID: 28402
		[SerializeField]
		private Button _cameraButton;

		// Token: 0x04006EF3 RID: 28403
		[SerializeField]
		private Button _callButton;

		// Token: 0x04006EF4 RID: 28404
		[SerializeField]
		private Button _helpButton;

		// Token: 0x04006EF5 RID: 28405
		[SerializeField]
		private Button _logButton;

		// Token: 0x04006EF6 RID: 28406
		[SerializeField]
		private Button _saveButton;

		// Token: 0x04006EF7 RID: 28407
		[SerializeField]
		private Button _optionButton;

		// Token: 0x04006EF8 RID: 28408
		private IDisposable _fadeSubscriber;

		// Token: 0x04006EF9 RID: 28409
		private MenuUIBehaviour[] _menuUIList;

		// Token: 0x04006EFA RID: 28410
		private int _hour;

		// Token: 0x04006EFB RID: 28411
		private int _minute;

		// Token: 0x04006EFC RID: 28412
		private int _temperature;

		// Token: 0x04006EFD RID: 28413
		private Weather _weather;

		// Token: 0x04006EFE RID: 28414
		private int _weatherIconID = -1;
	}
}
