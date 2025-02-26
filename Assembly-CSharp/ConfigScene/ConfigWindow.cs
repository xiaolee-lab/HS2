using System;
using System.Collections;
using System.Linq;
using AIProject;
using AIProject.Scene;
using Illusion.Extensions;
using Manager;
using ReMotion;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ConfigScene
{
	// Token: 0x0200085C RID: 2140
	[DefaultExecutionOrder(100)]
	public class ConfigWindow : BaseLoader
	{
		// Token: 0x170009C8 RID: 2504
		// (set) Token: 0x06003680 RID: 13952 RVA: 0x00141FCE File Offset: 0x001403CE
		public float timeScaleChange
		{
			set
			{
				this.timeScale = value;
			}
		}

		// Token: 0x06003681 RID: 13953 RVA: 0x00141FD7 File Offset: 0x001403D7
		public void Unload()
		{
			if (!ConfigWindow.IsConfig)
			{
				return;
			}
			this.Save();
			if (ConfigWindow.UnLoadAction != null)
			{
				ConfigWindow.UnLoadAction();
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x06003682 RID: 13954 RVA: 0x0014200B File Offset: 0x0014040B
		private void PlaySE(SoundPack.SystemSE se = SoundPack.SystemSE.OK_S)
		{
			Singleton<Manager.Resources>.Instance.SoundPack.Play(se);
		}

		// Token: 0x06003683 RID: 13955 RVA: 0x00142020 File Offset: 0x00140420
		protected override void Awake()
		{
			base.Awake();
			if (Singleton<Game>.IsInstance())
			{
				if (Singleton<Game>.Instance.Config != null)
				{
					UnityEngine.Object.Destroy(Singleton<Game>.Instance.Config.gameObject);
					Singleton<Game>.Instance.Config = null;
					GC.Collect();
					UnityEngine.Resources.UnloadUnusedAssets();
				}
				Singleton<Game>.Instance.Config = this;
			}
			this.timeScale = Time.timeScale;
			Time.timeScale = 0f;
			if (Singleton<Manager.Input>.IsInstance())
			{
				this._validType = Singleton<Manager.Input>.Instance.State;
				Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.UI);
				Singleton<Manager.Input>.Instance.SetupState();
			}
		}

		// Token: 0x06003684 RID: 13956 RVA: 0x001420CC File Offset: 0x001404CC
		protected void OnDisable()
		{
			if (Singleton<Game>.IsInstance())
			{
				Singleton<Game>.Instance.Config = null;
			}
			Time.timeScale = this.timeScale;
			if (Singleton<Manager.Input>.IsInstance())
			{
				Singleton<Manager.Input>.Instance.ReserveState(this._validType);
				Singleton<Manager.Input>.Instance.SetupState();
			}
		}

		// Token: 0x06003685 RID: 13957 RVA: 0x00142120 File Offset: 0x00140520
		private IEnumerator Start()
		{
			this.Open();
			foreach (ConfigWindow.ShortCutGroup shortCutGroup in this.shortCuts)
			{
				shortCutGroup.VisibleUpdate();
			}
			var shortCutList = this.shortCuts.Select(delegate(ConfigWindow.ShortCutGroup p, int i)
			{
				Button button = UnityEngine.Object.Instantiate<Button>(this.shortCutButtonPrefab, this.shortCutButtonBackGround, false);
				Text componentInChildren = button.GetComponentInChildren<Text>(true);
				componentInChildren.text = p.title;
				button.transform.SetSiblingIndex(i);
				ConfigSubWindowComponent comp = button.GetComponentInChildren<ConfigSubWindowComponent>(true);
				comp.selectAction.listActionEnter.Add(delegate
				{
					comp.objSelect.SetActiveIfDifferent(true);
					this.PlaySE(SoundPack.SystemSE.Select);
				});
				comp.selectAction.listActionExit.Add(delegate
				{
					comp.objSelect.SetActiveIfDifferent(false);
				});
				return new
				{
					bt = button,
					name = p.name
				};
			}).ToList();
			this.settings = (from p in this.shortCuts
			select p.trans.GetComponent<BaseSetting>()).ToArray<BaseSetting>();
			while (!Config.initialized)
			{
				yield return null;
			}
			if (this.imgBackGroud)
			{
				this.imgBackGroud.color = ConfigWindow.backGroundColor;
			}
			foreach (BaseSetting baseSetting in this.settings)
			{
				baseSetting.Init();
				baseSetting.UIPresenter();
			}
			float spacing = this.mainWindow.GetComponent<VerticalLayoutGroup>().spacing;
			shortCutList.ForEach(delegate(sc)
			{
				sc.bt.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					ConfigWindow.ShortCutGroup[] array3 = this.shortCuts.TakeWhile((ConfigWindow.ShortCutGroup p) => p.name != sc.name).ToArray<ConfigWindow.ShortCutGroup>();
					float y = Mathf.Min(array3.Sum((ConfigWindow.ShortCutGroup p) => p.trans.sizeDelta.y) + spacing * (float)array3.Length, this.mainWindow.rect.height - (this.mainWindow.parent as RectTransform).rect.height);
					Vector2 anchoredPosition = this.mainWindow.anchoredPosition;
					anchoredPosition.y = y;
					this.mainWindow.anchoredPosition = anchoredPosition;
				});
			});
			this.graphicSetting = (from sc in this.shortCuts
			select sc.trans.GetComponent<GraphicSetting>() into sc
			where sc != null
			select sc).FirstOrDefault<GraphicSetting>();
			shortCutList.ForEach(delegate(sc)
			{
				sc.bt.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					this.PlaySE(SoundPack.SystemSE.OK_S);
				});
			});
			this.buttons.ToList<Button>().ForEach(delegate(Button bt)
			{
				this.$this.ObserveEveryValueChanged((ConfigWindow _) => !Singleton<Scene>.Instance.IsNowLoadingFade, FrameCountType.Update, false).SubscribeToInteractable(bt);
				bt.OnPointerEnterAsObservable().Subscribe(delegate(PointerEventData _)
				{
					this.PlaySE(SoundPack.SystemSE.Select);
				});
				ConfigSubWindowComponent comp = bt.GetComponentInChildren<ConfigSubWindowComponent>(true);
				if (comp)
				{
					comp.selectAction.listActionEnter.Add(delegate
					{
						comp.objSelect.SetActiveIfDifferent(true);
					});
					comp.selectAction.listActionExit.Add(delegate
					{
						comp.objSelect.SetActiveIfDifferent(false);
					});
				}
			});
			Button backButton = this.buttons.FirstOrDefault((Button p) => p.name == "btnClose");
			(from _ in this.UpdateAsObservable()
			where Singleton<Game>.Instance.ExitScene == null && Singleton<Game>.Instance.Dialog == null
			where UnityEngine.Input.GetMouseButtonDown(1)
			where ConfigWindow.IsConfig
			where !Singleton<Scene>.Instance.IsOverlap && backButton.interactable
			select _).Take(1).Subscribe(delegate(Unit _)
			{
				this.OnBack();
			});
			(from _ in this.UpdateAsObservable()
			where !Singleton<Scene>.Instance.IsFadeNow
			where Singleton<Game>.Instance.ExitScene == null && Singleton<Game>.Instance.Dialog == null
			select _).Subscribe(delegate(Unit _)
			{
				if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
				{
					Singleton<Game>.Instance.LoadExit();
				}
			});
			if (Singleton<GameCursor>.IsInstance())
			{
				Singleton<GameCursor>.Instance.SetCursorLock(false);
			}
			ConfigWindow.IsConfig = true;
			yield break;
		}

		// Token: 0x06003686 RID: 13958 RVA: 0x0014213B File Offset: 0x0014053B
		private void OnDestroy()
		{
			ConfigWindow.IsConfig = false;
			ConfigWindow.UnLoadAction = null;
			ConfigWindow.TitleChangeAction = null;
		}

		// Token: 0x06003687 RID: 13959 RVA: 0x00142150 File Offset: 0x00140550
		public void OnDefault()
		{
			this.PlaySE(SoundPack.SystemSE.OK_S);
			ConfirmScene.Sentence = this.localizeIsInit[Singleton<GameSystem>.Instance.languageInt];
			ConfirmScene.OnClickedYes = delegate()
			{
				this.PlaySE(SoundPack.SystemSE.OK_L);
				Singleton<Voice>.Instance.Reset();
				Singleton<Config>.Instance.Reset();
				foreach (BaseSetting baseSetting in this.settings)
				{
					baseSetting.UIPresenter();
				}
			};
			ConfirmScene.OnClickedNo = delegate()
			{
				this.PlaySE(SoundPack.SystemSE.Cancel);
			};
			Singleton<Game>.Instance.LoadDialog();
		}

		// Token: 0x06003688 RID: 13960 RVA: 0x001421A8 File Offset: 0x001405A8
		public void OnTitle()
		{
			this.PlaySE(SoundPack.SystemSE.OK_S);
			bool flag = false;
			string b = "Title";
			bool flag2 = Singleton<Scene>.Instance.NowSceneNames[0] == b;
			if (flag2)
			{
				flag = true;
			}
			if (!flag)
			{
				ConfirmScene.Sentence = this.localizeIsTitle[Singleton<GameSystem>.Instance.languageInt];
				ConfirmScene.OnClickedYes = delegate()
				{
					this.Save();
					if (ConfigWindow.TitleChangeAction != null)
					{
						ConfigWindow.TitleChangeAction();
					}
					if (ConfigWindow.UnLoadAction != null)
					{
						ConfigWindow.UnLoadAction();
					}
					Singleton<Game>.Instance.Dialog.TimeScale = 1f;
					this.PlaySE(SoundPack.SystemSE.OK_L);
					Scene.Data data = new Scene.Data
					{
						levelName = "Title",
						isFade = true,
						onLoad = delegate
						{
							if (ConfigWindow.IsConfig)
							{
								UnityEngine.Object.Destroy(base.gameObject);
								Singleton<Game>.Instance.WorldData = null;
							}
						}
					};
					Singleton<Scene>.Instance.LoadReserve(data, true);
				};
				ConfirmScene.OnClickedNo = delegate()
				{
					this.PlaySE(SoundPack.SystemSE.Cancel);
				};
				Singleton<Game>.Instance.LoadDialog();
			}
			else
			{
				this.Close(delegate
				{
					this.Unload();
				});
			}
		}

		// Token: 0x06003689 RID: 13961 RVA: 0x00142242 File Offset: 0x00140642
		public void OnGameEnd()
		{
			this.PlaySE(SoundPack.SystemSE.OK_S);
		}

		// Token: 0x0600368A RID: 13962 RVA: 0x0014224C File Offset: 0x0014064C
		public void OnBack()
		{
			if (Singleton<Game>.Instance.ExitScene != null || Singleton<Game>.Instance.Dialog != null)
			{
				return;
			}
			this.PlaySE(SoundPack.SystemSE.Cancel);
			this.Close(delegate
			{
				this.Unload();
			});
		}

		// Token: 0x0600368B RID: 13963 RVA: 0x0014229D File Offset: 0x0014069D
		private void Save()
		{
			Singleton<Voice>.Instance.Save();
			Singleton<Config>.Instance.Save();
		}

		// Token: 0x0600368C RID: 13964 RVA: 0x001422B4 File Offset: 0x001406B4
		private void Open()
		{
			this.canvasGroup.blocksRaycasts = false;
			ObservableEasing.Linear(0.2f, true).FrameTimeInterval(true).Subscribe(delegate(TimeInterval<float> x)
			{
				this.canvasGroup.alpha = x.Value;
			}, delegate(Exception ex)
			{
			}, delegate()
			{
				this.canvasGroup.blocksRaycasts = true;
			});
		}

		// Token: 0x0600368D RID: 13965 RVA: 0x0014231C File Offset: 0x0014071C
		private void Close(Action onCompleted)
		{
			this.canvasGroup.blocksRaycasts = false;
			ObservableEasing.Linear(0.2f, true).FrameTimeInterval(true).Subscribe(delegate(TimeInterval<float> x)
			{
				this.canvasGroup.alpha = 1f - x.Value;
			}, delegate(Exception ex)
			{
			}, onCompleted);
		}

		// Token: 0x0600368E RID: 13966 RVA: 0x00142376 File Offset: 0x00140776
		public void CharaEntryInteractable(bool _interactable)
		{
			if (!this.graphicSetting)
			{
				return;
			}
			this.graphicSetting.EntryInteractable(_interactable);
		}

		// Token: 0x040036F8 RID: 14072
		[SerializeField]
		private CanvasGroup canvasGroup;

		// Token: 0x040036F9 RID: 14073
		[Label("コンフィグ項目を写す場所")]
		[SerializeField]
		private RectTransform mainWindow;

		// Token: 0x040036FA RID: 14074
		[Label("ショートカットボタンの場所")]
		[SerializeField]
		private RectTransform shortCutButtonBackGround;

		// Token: 0x040036FB RID: 14075
		[Label("ショートカットボタンのプレファブ")]
		[SerializeField]
		private Button shortCutButtonPrefab;

		// Token: 0x040036FC RID: 14076
		[Tooltip("ショートカットのリンク")]
		[SerializeField]
		private ConfigWindow.ShortCutGroup[] shortCuts;

		// Token: 0x040036FD RID: 14077
		[Tooltip("初めに設定されているボタン")]
		[SerializeField]
		private Button[] buttons;

		// Token: 0x040036FE RID: 14078
		[Tooltip("背景")]
		[SerializeField]
		private Image imgBackGroud;

		// Token: 0x040036FF RID: 14079
		private BaseSetting[] settings;

		// Token: 0x04003700 RID: 14080
		private float timeScale = 1f;

		// Token: 0x04003701 RID: 14081
		private Manager.Input.ValidType _validType;

		// Token: 0x04003702 RID: 14082
		public static Action UnLoadAction = null;

		// Token: 0x04003703 RID: 14083
		public static Action TitleChangeAction = null;

		// Token: 0x04003704 RID: 14084
		public static Color backGroundColor = new Color(0f, 0f, 0f, 0f);

		// Token: 0x04003705 RID: 14085
		private readonly string[] localizeIsInit = new string[]
		{
			"設定を初期化しますか？",
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty
		};

		// Token: 0x04003706 RID: 14086
		private readonly string[] localizeIsTitle = new string[]
		{
			"タイトルに戻りますか？",
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty
		};

		// Token: 0x04003707 RID: 14087
		public static bool IsConfig = false;

		// Token: 0x04003708 RID: 14088
		private GraphicSetting graphicSetting;

		// Token: 0x0200085D RID: 2141
		[Serializable]
		public class ShortCutGroup
		{
			// Token: 0x170009C9 RID: 2505
			// (get) Token: 0x0600369D RID: 13981 RVA: 0x00142535 File Offset: 0x00140935
			public RectTransform trans
			{
				get
				{
					return this._trans[this.visibleNo];
				}
			}

			// Token: 0x0600369E RID: 13982 RVA: 0x00142544 File Offset: 0x00140944
			public void VisibleUpdate()
			{
				foreach (RectTransform rectTransform in this._trans)
				{
					rectTransform.gameObject.SetActiveIfDifferent(rectTransform == this.trans);
				}
			}

			// Token: 0x0400370B RID: 14091
			public string title;

			// Token: 0x0400370C RID: 14092
			public string name;

			// Token: 0x0400370D RID: 14093
			public int visibleNo;

			// Token: 0x0400370E RID: 14094
			[SerializeField]
			private RectTransform[] _trans;
		}
	}
}
