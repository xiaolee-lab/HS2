using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ADV;
using Illusion.Extensions;
using Manager;
using ReMotion;
using UniRx;
using UniRx.Toolkit;
using UnityEngine;
using UnityEngine.UI;

namespace AIProject.UI
{
	// Token: 0x02000F13 RID: 3859
	public class GameLog : MenuUIBehaviour
	{
		// Token: 0x170018FF RID: 6399
		// (get) Token: 0x06007E50 RID: 32336 RVA: 0x0035C190 File Offset: 0x0035A590
		private MenuUIBehaviour[] MenuUIElements
		{
			[CompilerGenerated]
			get
			{
				MenuUIBehaviour[] result;
				if ((result = this._menuUIElements) == null)
				{
					result = (this._menuUIElements = new MenuUIBehaviour[]
					{
						this
					});
				}
				return result;
			}
		}

		// Token: 0x06007E51 RID: 32337 RVA: 0x0035C1C0 File Offset: 0x0035A5C0
		protected override void OnBeforeStart()
		{
			this._elementPool = new GameLog.GameLogElementPool
			{
				Source = this._node
			};
			this._lerpStream = ObservableEasing.Linear(0.1f, true).FrameTimeInterval(true);
			base.OnActiveChangedAsObservable().Subscribe(delegate(bool x)
			{
				this.SetActiveControl(x);
			});
			if (this._closeButton != null)
			{
				this._closeButton.onClick.AddListener(delegate()
				{
					this.IsActiveControl = false;
				});
			}
			KeyCodeDownCommand keyCodeDownCommand = new KeyCodeDownCommand
			{
				KeyCode = KeyCode.Mouse1
			};
			keyCodeDownCommand.TriggerEvent.AddListener(delegate()
			{
				this.IsActiveControl = false;
			});
			this._keyCommands.Add(keyCodeDownCommand);
		}

		// Token: 0x06007E52 RID: 32338 RVA: 0x0035C278 File Offset: 0x0035A678
		private void Update()
		{
			if (this._guideRoot != null)
			{
				this._guideRoot.gameObject.SetActiveIfDifferent(Config.GameData.ActionGuide);
			}
		}

		// Token: 0x06007E53 RID: 32339 RVA: 0x0035C2A8 File Offset: 0x0035A6A8
		private void SetActiveControl(bool active)
		{
			IEnumerator coroutine = (!active) ? this.Close() : this.Open();
			if (this._fadeSubscriber != null)
			{
				this._fadeSubscriber.Dispose();
			}
			this._fadeSubscriber = Observable.FromCoroutine(() => coroutine, false).Subscribe(delegate(Unit _)
			{
			}, delegate(Exception ex)
			{
				if (global::Debug.isDebugBuild)
				{
				}
			});
		}

		// Token: 0x06007E54 RID: 32340 RVA: 0x0035C348 File Offset: 0x0035A748
		private IEnumerator Open()
		{
			base.gameObject.SetActive(true);
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.UI);
			Singleton<Manager.Input>.Instance.SetupState();
			if (this._canvasGroup.blocksRaycasts)
			{
				this._canvasGroup.blocksRaycasts = false;
			}
			this._scrollRect.verticalScrollbar.value = 1f;
			IConnectableObservable<TimeInterval<float>> stream = this._lerpStream.Publish<TimeInterval<float>>();
			stream.Subscribe(delegate(TimeInterval<float> x)
			{
				this._canvasGroup.alpha = x.Value;
			});
			stream.Connect();
			yield return stream.ToYieldInstruction<TimeInterval<float>>();
			Singleton<Manager.Input>.Instance.MenuElements = this.MenuUIElements;
			Singleton<Manager.Input>.Instance.FocusLevel = 0;
			this._canvasGroup.blocksRaycasts = true;
			base.EnabledInput = true;
			yield break;
		}

		// Token: 0x06007E55 RID: 32341 RVA: 0x0035C364 File Offset: 0x0035A764
		private IEnumerator Close()
		{
			Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
			Singleton<Manager.Input>.Instance.ClearMenuElements();
			base.EnabledInput = false;
			this._canvasGroup.blocksRaycasts = false;
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.Action);
			float startAlpha = this._canvasGroup.alpha;
			IConnectableObservable<TimeInterval<float>> stream = this._lerpStream.Publish<TimeInterval<float>>();
			stream.Subscribe(delegate(TimeInterval<float> x)
			{
				this._canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, x.Value);
			});
			stream.Connect();
			yield return stream.ToYieldInstruction<TimeInterval<float>>();
			Singleton<Manager.Input>.Instance.SetupState();
			Action onClosed = this.OnClosed;
			if (onClosed != null)
			{
				onClosed();
			}
			base.gameObject.SetActive(false);
			yield break;
		}

		// Token: 0x17001900 RID: 6400
		// (get) Token: 0x06007E56 RID: 32342 RVA: 0x0035C37F File Offset: 0x0035A77F
		// (set) Token: 0x06007E57 RID: 32343 RVA: 0x0035C387 File Offset: 0x0035A787
		public Action OnClosed { get; set; }

		// Token: 0x06007E58 RID: 32344 RVA: 0x0035C390 File Offset: 0x0035A790
		public void AddLog(string name, string message, IReadOnlyCollection<TextScenario.IVoice[]> voices)
		{
			if (this._logs.Count > 50)
			{
				GameLogElement instance = this._logs.Dequeue();
				this._elementPool.Return(instance);
			}
			GameLogElement gameLogElement = this._elementPool.Rent();
			gameLogElement.transform.SetParent(this._scrollRect.content, false);
			gameLogElement.transform.localScale = Vector3.one;
			gameLogElement.transform.SetAsFirstSibling();
			gameLogElement.Add(name, message, voices);
			this._logs.Enqueue(gameLogElement);
		}

		// Token: 0x06007E59 RID: 32345 RVA: 0x0035C41A File Offset: 0x0035A81A
		private void EnableRaycastTargetIfInvisible(bool isInvisible)
		{
			if (isInvisible)
			{
				this._canvasGroup.blocksRaycasts = true;
			}
		}

		// Token: 0x040065EA RID: 26090
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x040065EB RID: 26091
		[SerializeField]
		private ScrollRect _scrollRect;

		// Token: 0x040065EC RID: 26092
		[SerializeField]
		private Button _closeButton;

		// Token: 0x040065ED RID: 26093
		[SerializeField]
		private GameLogElement _node;

		// Token: 0x040065EE RID: 26094
		[SerializeField]
		private RectTransform _guideRoot;

		// Token: 0x040065EF RID: 26095
		private Queue<GameLogElement> _logs = new Queue<GameLogElement>();

		// Token: 0x040065F0 RID: 26096
		private GameLog.GameLogElementPool _elementPool = new GameLog.GameLogElementPool();

		// Token: 0x040065F1 RID: 26097
		private const float _alphaFadeDuration = 0.1f;

		// Token: 0x040065F2 RID: 26098
		private IObservable<TimeInterval<float>> _lerpStream;

		// Token: 0x040065F3 RID: 26099
		private MenuUIBehaviour[] _menuUIElements;

		// Token: 0x040065F4 RID: 26100
		private IDisposable _fadeSubscriber;

		// Token: 0x02000F14 RID: 3860
		public class GameLogElementPool : ObjectPool<GameLogElement>
		{
			// Token: 0x17001901 RID: 6401
			// (get) Token: 0x06007E60 RID: 32352 RVA: 0x0035C45F File Offset: 0x0035A85F
			// (set) Token: 0x06007E61 RID: 32353 RVA: 0x0035C467 File Offset: 0x0035A867
			public GameLogElement Source { get; set; }

			// Token: 0x17001902 RID: 6402
			// (get) Token: 0x06007E62 RID: 32354 RVA: 0x0035C470 File Offset: 0x0035A870
			// (set) Token: 0x06007E63 RID: 32355 RVA: 0x0035C478 File Offset: 0x0035A878
			public Transform Parent { get; set; }

			// Token: 0x06007E64 RID: 32356 RVA: 0x0035C484 File Offset: 0x0035A884
			protected override GameLogElement CreateInstance()
			{
				return UnityEngine.Object.Instantiate<GameLogElement>(this.Source).GetComponent<GameLogElement>();
			}

			// Token: 0x06007E65 RID: 32357 RVA: 0x0035C4A3 File Offset: 0x0035A8A3
			protected override void OnBeforeReturn(GameLogElement instance)
			{
				if (instance == null)
				{
					return;
				}
				instance.transform.SetParent(this.Parent);
				base.OnBeforeReturn(instance);
			}
		}
	}
}
