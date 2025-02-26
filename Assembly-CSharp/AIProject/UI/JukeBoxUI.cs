using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using AIProject.Animal;
using AIProject.SaveData;
using Manager;
using ReMotion;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace AIProject.UI
{
	// Token: 0x02000E99 RID: 3737
	[RequireComponent(typeof(RectTransform))]
	[RequireComponent(typeof(CanvasGroup))]
	public class JukeBoxUI : MenuUIBehaviour
	{
		// Token: 0x170017A2 RID: 6050
		// (get) Token: 0x0600786F RID: 30831 RVA: 0x0032C293 File Offset: 0x0032A693
		public RectTransform MyTransform
		{
			[CompilerGenerated]
			get
			{
				return this._myTransform;
			}
		}

		// Token: 0x170017A3 RID: 6051
		// (get) Token: 0x06007870 RID: 30832 RVA: 0x0032C29B File Offset: 0x0032A69B
		public CanvasGroup MyCanvas
		{
			[CompilerGenerated]
			get
			{
				return this._myCanvas;
			}
		}

		// Token: 0x170017A4 RID: 6052
		// (get) Token: 0x06007871 RID: 30833 RVA: 0x0032C2A3 File Offset: 0x0032A6A3
		public CanvasGroup BackCanvasGroup
		{
			[CompilerGenerated]
			get
			{
				return this._backCanvasGroup;
			}
		}

		// Token: 0x170017A5 RID: 6053
		// (get) Token: 0x06007872 RID: 30834 RVA: 0x0032C2AB File Offset: 0x0032A6AB
		// (set) Token: 0x06007873 RID: 30835 RVA: 0x0032C2D3 File Offset: 0x0032A6D3
		public float CanvasAlpha
		{
			get
			{
				return (!(this._canvasGroup != null)) ? 0f : this._canvasGroup.alpha;
			}
			set
			{
				if (this._canvasGroup != null)
				{
					this._canvasGroup.alpha = value;
				}
			}
		}

		// Token: 0x170017A6 RID: 6054
		// (get) Token: 0x06007874 RID: 30836 RVA: 0x0032C2F2 File Offset: 0x0032A6F2
		// (set) Token: 0x06007875 RID: 30837 RVA: 0x0032C31A File Offset: 0x0032A71A
		public float MyCanvasAlpha
		{
			get
			{
				return (!(this._myCanvas != null)) ? 0f : this._myCanvas.alpha;
			}
			set
			{
				if (this._myCanvas != null)
				{
					this._myCanvas.alpha = value;
				}
			}
		}

		// Token: 0x170017A7 RID: 6055
		// (get) Token: 0x06007876 RID: 30838 RVA: 0x0032C339 File Offset: 0x0032A739
		public bool InputEnabled
		{
			[CompilerGenerated]
			get
			{
				return base.EnabledInput && this._focusLevel == Singleton<Manager.Input>.Instance.FocusLevel;
			}
		}

		// Token: 0x170017A8 RID: 6056
		// (get) Token: 0x06007877 RID: 30839 RVA: 0x0032C35B File Offset: 0x0032A75B
		// (set) Token: 0x06007878 RID: 30840 RVA: 0x0032C363 File Offset: 0x0032A763
		public Action ClosedAction { get; set; }

		// Token: 0x170017A9 RID: 6057
		// (get) Token: 0x06007879 RID: 30841 RVA: 0x0032C36C File Offset: 0x0032A76C
		// (set) Token: 0x0600787A RID: 30842 RVA: 0x0032C374 File Offset: 0x0032A774
		public JukePoint CurrentJukePoint { get; private set; }

		// Token: 0x170017AA RID: 6058
		// (get) Token: 0x0600787B RID: 30843 RVA: 0x0032C37D File Offset: 0x0032A77D
		public MenuUIBehaviour[] MenuUIBehaviors
		{
			get
			{
				if (this._menuUIBehaviors == null)
				{
					this._menuUIBehaviors = new MenuUIBehaviour[]
					{
						this,
						this._listUI,
						this._sortUI
					};
				}
				return this._menuUIBehaviors;
			}
		}

		// Token: 0x0600787C RID: 30844 RVA: 0x0032C3B4 File Offset: 0x0032A7B4
		protected override void Awake()
		{
			base.Awake();
			if (this._myCanvas == null)
			{
				this._myCanvas = base.GetComponent<CanvasGroup>();
			}
			if (this._myTransform == null)
			{
				this._myTransform = base.GetComponent<RectTransform>();
			}
			if (JukeBoxUI.First)
			{
				if (!Directory.Exists(SoundPlayer.Directory.AudioFile))
				{
					Directory.CreateDirectory(SoundPlayer.Directory.AudioFile);
				}
				JukeBoxUI.First = false;
			}
		}

		// Token: 0x0600787D RID: 30845 RVA: 0x0032C42C File Offset: 0x0032A82C
		protected override void OnBeforeStart()
		{
			base.OnBeforeStart();
			base.OnActiveChangedAsObservable().Subscribe(delegate(bool x)
			{
				this.SetActiveControl(x);
			});
			ActionIDDownCommand actionIDDownCommand = new ActionIDDownCommand
			{
				ActionID = ActionID.Cancel
			};
			actionIDDownCommand.TriggerEvent.AddListener(delegate()
			{
				this.DoClose();
			});
			this._actionCommands.Add(actionIDDownCommand);
			KeyCodeDownCommand keyCodeDownCommand = new KeyCodeDownCommand
			{
				KeyCode = KeyCode.Mouse1
			};
			keyCodeDownCommand.TriggerEvent.AddListener(delegate()
			{
				this.DoClose();
			});
			this._keyCommands.Add(keyCodeDownCommand);
			(from _ in this._closeButton.OnClickAsObservable()
			where this.InputEnabled
			select _).Subscribe(delegate(Unit _)
			{
				this.DoClose();
			});
			(from _ in this._changeButton.OnClickAsObservable()
			where this.InputEnabled
			select _).Subscribe(delegate(Unit _)
			{
				this._listUI.IsActiveControl = !this._listUI.IsActiveControl;
			});
		}

		// Token: 0x0600787E RID: 30846 RVA: 0x0032C51C File Offset: 0x0032A91C
		protected override void OnAfterStart()
		{
			base.OnAfterStart();
			this.Off();
		}

		// Token: 0x0600787F RID: 30847 RVA: 0x0032C52C File Offset: 0x0032A92C
		private void SetActiveControl(bool active)
		{
			IEnumerator coroutine = (!active) ? this.CloseCoroutine() : this.OpenCoroutine();
			if (this._fadeDisposable != null)
			{
				this._fadeDisposable.Dispose();
			}
			if (this._allDisposable != null)
			{
				this._allDisposable.Clear();
			}
			this._fadeDisposable = Observable.FromCoroutine(() => coroutine, false).Subscribe<Unit>().AddTo(this._allDisposable);
		}

		// Token: 0x06007880 RID: 30848 RVA: 0x0032C5B4 File Offset: 0x0032A9B4
		private void DoClose()
		{
			this.PlaySystemSE(SoundPack.SystemSE.Cancel);
			this.IsActiveControl = false;
		}

		// Token: 0x06007881 RID: 30849 RVA: 0x0032C5C4 File Offset: 0x0032A9C4
		private void Off()
		{
			this.SetBlockRaycast(false);
			this.SetInteractable(false);
			this.SetAllEnableInput(false);
			this.SetAllFocusLevel(99);
			base.gameObject.SetActiveSelf(false);
		}

		// Token: 0x06007882 RID: 30850 RVA: 0x0032C5F0 File Offset: 0x0032A9F0
		private IEnumerator OpenCoroutine()
		{
			JukePoint currentJukePoint;
			if (Singleton<Map>.IsInstance())
			{
				PlayerActor player = Singleton<Map>.Instance.Player;
				currentJukePoint = ((player != null) ? player.CurrentjukePoint : null);
			}
			else
			{
				currentJukePoint = null;
			}
			this.CurrentJukePoint = currentJukePoint;
			int mapID = (!Singleton<Map>.IsInstance()) ? 0 : Singleton<Map>.Instance.MapID;
			if (Singleton<SoundPlayer>.IsInstance())
			{
				Singleton<SoundPlayer>.Instance.StopMapBGM(0f);
				Singleton<SoundPlayer>.Instance.ForcedMuteHousingAreaBGM(0f, true);
				if (this.CurrentJukePoint != null)
				{
					Singleton<SoundPlayer>.Instance.StopHousingAreaBGM(mapID, this.CurrentJukePoint.AreaID, 0f, true);
				}
			}
			base.gameObject.SetActiveSelf(true);
			this.SetBlockRaycast(true);
			this.SetInteractable(false);
			this.SetAllEnableInput(false);
			this._canvasGroup.SetInteractable(false);
			this._canvasGroup.SetBlocksRaycasts(true);
			this._prevTimeScale = Time.timeScale;
			Time.timeScale = 0f;
			this._listUI.UISetting();
			string audioName = null;
			if (this.CurrentJukePoint != null)
			{
				int areaID = this.CurrentJukePoint.AreaID;
				Dictionary<int, string> dictionary = null;
				if (Singleton<Game>.IsInstance())
				{
					if (mapID == 0)
					{
						AIProject.SaveData.Environment environment = Singleton<Game>.Instance.Environment;
						dictionary = ((environment != null) ? environment.JukeBoxAudioNameTable : null);
					}
					else
					{
						AIProject.SaveData.Environment environment2 = Singleton<Game>.Instance.Environment;
						Dictionary<int, Dictionary<int, string>> dictionary2 = (environment2 != null) ? environment2.AnotherJukeBoxAudioNameTable : null;
						if (dictionary2 != null && (!dictionary2.TryGetValue(mapID, out dictionary) || dictionary == null))
						{
							Dictionary<int, string> dictionary3 = new Dictionary<int, string>();
							dictionary2[mapID] = dictionary3;
							dictionary = dictionary3;
						}
					}
				}
				string text = null;
				if (!dictionary.IsNullOrEmpty<int, string>() && dictionary.TryGetValue(areaID, out text) && !text.IsNullOrEmpty())
				{
					audioName = Path.GetFileNameWithoutExtension(text);
				}
				if (!dictionary.IsNullOrEmpty<int, string>() && dictionary.ContainsKey(areaID) && audioName == null)
				{
					dictionary.Remove(areaID);
				}
			}
			this._audioNameText.text = (audioName ?? this._listUI.NoneStr);
			Manager.Input input = Singleton<Manager.Input>.Instance;
			this._prevFocusLevel = input.FocusLevel;
			input.FocusLevel++;
			input.MenuElements = this.MenuUIBehaviors;
			this.SetAllFocusLevel(input.FocusLevel);
			float startAlpha = this.MyCanvasAlpha;
			IConnectableObservable<TimeInterval<float>> stream = ObservableEasing.Linear(this._alphaAccelerationTime, true).FrameTimeInterval(true).TakeUntilDestroy(this).Publish<TimeInterval<float>>();
			stream.Connect().AddTo(this._allDisposable);
			stream.Subscribe(delegate(TimeInterval<float> x)
			{
				this.MyCanvasAlpha = Mathf.Lerp(startAlpha, 1f, x.Value);
			}).AddTo(this._allDisposable);
			yield return stream.ToYieldInstruction<TimeInterval<float>>().AddTo(this._allDisposable);
			input.ReserveState(Manager.Input.ValidType.UI);
			input.SetupState();
			this._canvasGroup.SetInteractable(true);
			this.SetInteractable(true);
			this.SetAllEnableInput(true);
			yield break;
		}

		// Token: 0x06007883 RID: 30851 RVA: 0x0032C60C File Offset: 0x0032AA0C
		private IEnumerator CloseCoroutine()
		{
			this.SetAllEnableInput(false);
			this.SetInteractable(false);
			this._canvasGroup.SetInteractable(false);
			Manager.Input input = Singleton<Manager.Input>.Instance;
			input.ClearMenuElements();
			this._listUI.AudioStop();
			float startAlpha = this.MyCanvasAlpha;
			IConnectableObservable<TimeInterval<float>> stream = ObservableEasing.Linear(this._alphaAccelerationTime, true).FrameTimeInterval(true).TakeUntilDestroy(this).Publish<TimeInterval<float>>();
			stream.Connect().AddTo(this._allDisposable);
			stream.Subscribe(delegate(TimeInterval<float> x)
			{
				this.MyCanvasAlpha = Mathf.Lerp(startAlpha, 0f, x.Value);
			}).AddTo(this._allDisposable);
			yield return stream.ToYieldInstruction<TimeInterval<float>>().AddTo(this._allDisposable);
			MenuUIBehaviour listUI = this._listUI;
			bool isActiveControl = false;
			this._sortUI.IsActiveControl = isActiveControl;
			listUI.IsActiveControl = isActiveControl;
			input.FocusLevel = this._prevFocusLevel;
			this._canvasGroup.SetBlocksRaycasts(false);
			this.SetBlockRaycast(false);
			base.gameObject.SetActiveSelf(false);
			Time.timeScale = this._prevTimeScale;
			if (Singleton<SoundPlayer>.IsInstance())
			{
				Singleton<SoundPlayer>.Instance.ActivateMapBGM();
			}
			Action closedAction = this.ClosedAction;
			if (closedAction != null)
			{
				closedAction();
			}
			yield break;
		}

		// Token: 0x06007884 RID: 30852 RVA: 0x0032C628 File Offset: 0x0032AA28
		public void SetAudioName(string fileName, string fileNameWithExtension)
		{
			this._audioNameText.text = (fileName ?? this._listUI.NoneStr);
			int num = (!Singleton<Map>.IsInstance()) ? 0 : Singleton<Map>.Instance.MapID;
			JukePoint currentJukePoint = this.CurrentJukePoint;
			int? num2 = (currentJukePoint != null) ? new int?(currentJukePoint.AreaID) : null;
			int num3 = (num2 == null) ? -1 : num2.Value;
			Dictionary<int, string> dictionary = null;
			if (Singleton<Game>.IsInstance())
			{
				if (num == 0)
				{
					AIProject.SaveData.Environment environment = Singleton<Game>.Instance.Environment;
					dictionary = ((environment != null) ? environment.JukeBoxAudioNameTable : null);
				}
				else
				{
					AIProject.SaveData.Environment environment2 = Singleton<Game>.Instance.Environment;
					Dictionary<int, Dictionary<int, string>> dictionary2 = (environment2 != null) ? environment2.AnotherJukeBoxAudioNameTable : null;
					if (dictionary2 != null && (!dictionary2.TryGetValue(num, out dictionary) || dictionary == null))
					{
						Dictionary<int, string> dictionary3 = new Dictionary<int, string>();
						dictionary2[num] = dictionary3;
						dictionary = dictionary3;
					}
				}
			}
			if (dictionary == null)
			{
				return;
			}
			bool flag = false;
			if (fileNameWithExtension.IsNullOrEmpty())
			{
				if (dictionary.ContainsKey(num3))
				{
					dictionary.Remove(num3);
					flag = true;
				}
			}
			else
			{
				flag = (!dictionary.ContainsKey(num3) || dictionary[num3] != fileNameWithExtension);
				dictionary[num3] = fileNameWithExtension;
			}
			if (flag && Singleton<SoundPlayer>.IsInstance())
			{
				Singleton<SoundPlayer>.Instance.RemoveAreaAudioClip(num, num3);
			}
		}

		// Token: 0x06007885 RID: 30853 RVA: 0x0032C79B File Offset: 0x0032AB9B
		private void SetBlockRaycast(bool active)
		{
			this._myCanvas.SetBlocksRaycasts(active);
			this._backCanvasGroup.SetBlocksRaycasts(active);
		}

		// Token: 0x06007886 RID: 30854 RVA: 0x0032C7B7 File Offset: 0x0032ABB7
		private void SetInteractable(bool active)
		{
			this._myCanvas.SetInteractable(active);
			this._backCanvasGroup.SetInteractable(active);
		}

		// Token: 0x06007887 RID: 30855 RVA: 0x0032C7D4 File Offset: 0x0032ABD4
		private void SetAllEnableInput(bool active)
		{
			MenuUIBehaviour[] menuUIBehaviors = this.MenuUIBehaviors;
			foreach (MenuUIBehaviour menuUIBehaviour in menuUIBehaviors)
			{
				menuUIBehaviour.EnabledInput = active;
			}
		}

		// Token: 0x06007888 RID: 30856 RVA: 0x0032C80C File Offset: 0x0032AC0C
		private void SetAllFocusLevel(int level)
		{
			MenuUIBehaviour[] menuUIBehaviors = this.MenuUIBehaviors;
			foreach (MenuUIBehaviour menuUIBehaviour in menuUIBehaviors)
			{
				menuUIBehaviour.SetFocusLevel(level);
			}
		}

		// Token: 0x06007889 RID: 30857 RVA: 0x0032C844 File Offset: 0x0032AC44
		private void PlaySystemSE(SoundPack.SystemSE se)
		{
			SoundPack soundPack = (!Singleton<Manager.Resources>.IsInstance()) ? null : Singleton<Manager.Resources>.Instance.SoundPack;
			if (soundPack != null)
			{
				soundPack.Play(se);
			}
		}

		// Token: 0x04006175 RID: 24949
		[SerializeField]
		private RectTransform _myTransform;

		// Token: 0x04006176 RID: 24950
		[SerializeField]
		private CanvasGroup _myCanvas;

		// Token: 0x04006177 RID: 24951
		[SerializeField]
		private CanvasGroup _backCanvasGroup;

		// Token: 0x04006178 RID: 24952
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x04006179 RID: 24953
		[SerializeField]
		private RectTransform _rectTransform;

		// Token: 0x0400617A RID: 24954
		[SerializeField]
		private Button _closeButton;

		// Token: 0x0400617B RID: 24955
		[SerializeField]
		private Text _audioNameText;

		// Token: 0x0400617C RID: 24956
		[SerializeField]
		private Button _changeButton;

		// Token: 0x0400617D RID: 24957
		[SerializeField]
		private JukeBoxAudioListUI _listUI;

		// Token: 0x0400617E RID: 24958
		[SerializeField]
		private AudioSortUI _sortUI;

		// Token: 0x04006180 RID: 24960
		private int _prevFocusLevel = -1;

		// Token: 0x04006182 RID: 24962
		private float _prevTimeScale = 1f;

		// Token: 0x04006183 RID: 24963
		private MenuUIBehaviour[] _menuUIBehaviors;

		// Token: 0x04006184 RID: 24964
		private static bool First = true;

		// Token: 0x04006185 RID: 24965
		private CompositeDisposable _allDisposable = new CompositeDisposable();

		// Token: 0x04006186 RID: 24966
		private IDisposable _fadeDisposable;
	}
}
