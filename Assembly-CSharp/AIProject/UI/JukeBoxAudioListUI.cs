using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using AIProject.Animal;
using Illusion.Game;
using Illusion.Game.Extensions;
using Manager;
using ReMotion;
using uAudio.uAudio_backend;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AIProject.UI
{
	// Token: 0x02000E97 RID: 3735
	[RequireComponent(typeof(CanvasGroup))]
	[RequireComponent(typeof(RectTransform))]
	public class JukeBoxAudioListUI : MenuUIBehaviour
	{
		// Token: 0x1700179C RID: 6044
		// (get) Token: 0x06007833 RID: 30771 RVA: 0x0032AE53 File Offset: 0x00329253
		public string NoneStr
		{
			[CompilerGenerated]
			get
			{
				return this._noneStr;
			}
		}

		// Token: 0x1700179D RID: 6045
		// (get) Token: 0x06007834 RID: 30772 RVA: 0x0032AE5B File Offset: 0x0032925B
		// (set) Token: 0x06007835 RID: 30773 RVA: 0x0032AE83 File Offset: 0x00329283
		public float CanvasAlpha
		{
			get
			{
				return (!(this._canvasGroup != null)) ? 0f : this._canvasGroup.alpha;
			}
			private set
			{
				if (this._canvasGroup != null)
				{
					this._canvasGroup.alpha = value;
				}
			}
		}

		// Token: 0x1700179E RID: 6046
		// (get) Token: 0x06007836 RID: 30774 RVA: 0x0032AEA2 File Offset: 0x003292A2
		public bool InputEnabled
		{
			[CompilerGenerated]
			get
			{
				return base.EnabledInput && this._focusLevel == Singleton<Manager.Input>.Instance.FocusLevel;
			}
		}

		// Token: 0x1700179F RID: 6047
		// (get) Token: 0x06007837 RID: 30775 RVA: 0x0032AEC4 File Offset: 0x003292C4
		public IReadOnlyList<JukeBoxAudioListUI.AudioData> ElementList
		{
			[CompilerGenerated]
			get
			{
				return this._elementList;
			}
		}

		// Token: 0x06007838 RID: 30776 RVA: 0x0032AECC File Offset: 0x003292CC
		protected override void Awake()
		{
			base.Awake();
			if (this._canvasGroup == null)
			{
				this._canvasGroup = base.GetComponent<CanvasGroup>();
			}
			if (this._rectTransform == null)
			{
				this._rectTransform = base.GetComponent<RectTransform>();
			}
		}

		// Token: 0x06007839 RID: 30777 RVA: 0x0032AF1C File Offset: 0x0032931C
		protected override void OnBeforeStart()
		{
			base.OnBeforeStart();
			base.OnActiveChangedAsObservable().TakeUntilDestroy(this).Subscribe(delegate(bool x)
			{
				this.SetActiveControl(x);
			});
			(from _ in this._closeButton.OnClickAsObservable()
			where this.InputEnabled
			where this.IsActiveControl
			select _).Subscribe(delegate(Unit _)
			{
				this.DoClose();
			});
			(from _ in this._sortButton.OnClickAsObservable()
			where this.InputEnabled
			select _).Subscribe(delegate(Unit _)
			{
				this._sortUI.IsActiveControl = !this._sortUI.IsActiveControl;
			});
			(from _ in this._setButton.OnClickAsObservable()
			where this.InputEnabled
			select _).Subscribe(delegate(Unit _)
			{
				this.DoSetAudio();
			});
			(from _ in this._rightButton.OnClickAsObservable()
			where this.InputEnabled
			select _).Subscribe(delegate(Unit _)
			{
				this.DoMove(1);
			});
			(from _ in this._leftButton.OnClickAsObservable()
			where this.InputEnabled
			select _).Subscribe(delegate(Unit _)
			{
				this.DoMove(-1);
			});
			(from _ in this._playButton.OnClickAsObservable()
			where this.InputEnabled
			select _).Subscribe(delegate(Unit _)
			{
				this.DoPlay();
			});
			(from _ in this._pauseButton.OnClickAsObservable()
			where this.InputEnabled
			select _).Subscribe(delegate(Unit _)
			{
				this.AudioStop();
			});
			(from _ in this._sortToggle.OnValueChangedAsObservable()
			where this.InputEnabled
			select _).Subscribe(delegate(bool _)
			{
				this.ListSort();
			});
			if (this._sortToggle.targetGraphic != null)
			{
				this._sortToggle.OnValueChangedAsObservable().Subscribe(delegate(bool isOn)
				{
					this._sortToggle.targetGraphic.enabled = !isOn;
				});
			}
			this._sortUI.ToggleIndexChanged = delegate(int x)
			{
				this.ListSort();
			};
			if (this._sortSelectedImage != null)
			{
				this._sortSelectedImage.enabled = false;
				this._sortToggle.OnPointerEnterAsObservable().Subscribe(delegate(PointerEventData _)
				{
					this._sortSelectedImage.enabled = true;
				});
				this._sortToggle.OnPointerExitAsObservable().Subscribe(delegate(PointerEventData _)
				{
					this._sortSelectedImage.enabled = false;
				});
			}
		}

		// Token: 0x0600783A RID: 30778 RVA: 0x0032B16F File Offset: 0x0032956F
		protected override void OnAfterStart()
		{
			base.OnAfterStart();
		}

		// Token: 0x0600783B RID: 30779 RVA: 0x0032B177 File Offset: 0x00329577
		public void Hide()
		{
			if (this._canvasGroup == null)
			{
				return;
			}
			this._canvasGroup.SetBlocksRaycasts(false);
			this._canvasGroup.SetInteractable(false);
			this._canvasGroup.alpha = 0f;
		}

		// Token: 0x170017A0 RID: 6048
		// (get) Token: 0x0600783C RID: 30780 RVA: 0x0032B1B5 File Offset: 0x003295B5
		public bool IsPlayingAudio
		{
			[CompilerGenerated]
			get
			{
				return this._currentPlayAudio != null && this._currentPlayAudio.gameObject != null && this._currentPlayAudio.isPlaying;
			}
		}

		// Token: 0x0600783D RID: 30781 RVA: 0x0032B1EC File Offset: 0x003295EC
		private void DoPlay()
		{
			if (this._lastAudioData != null && this._selectedElement == this._lastAudioData)
			{
				return;
			}
			this.AudioStop();
			if (this._selectedElement == null)
			{
				return;
			}
			if (this._uAudio != null)
			{
				this._uAudio.Dispose();
			}
			bool flag = false;
			AudioClip audioClip = SoundPlayer.LoadAudioClip(this._selectedElement.filePath, ref flag, this._uAudio);
			if (audioClip != null)
			{
				this._currentPlayAudio = Illusion.Game.Utils.Sound.Play(Sound.Type.BGM, audioClip, 0f);
				this._lastAudioData = this._selectedElement;
			}
			if (this._currentPlayAudio != null)
			{
				this._playButton.SetActiveSelf(false);
				this._pauseButton.SetActiveSelf(true);
			}
		}

		// Token: 0x0600783E RID: 30782 RVA: 0x0032B2AE File Offset: 0x003296AE
		public void DoPause()
		{
			this.AudioStop();
		}

		// Token: 0x0600783F RID: 30783 RVA: 0x0032B2B6 File Offset: 0x003296B6
		public void AudioStop()
		{
			if (this.IsPlayingAudio)
			{
				this._currentPlayAudio.Stop();
			}
			this._currentPlayAudio = null;
			this._playButton.SetActiveSelf(true);
			this._pauseButton.SetActiveSelf(false);
			this._lastAudioData = null;
		}

		// Token: 0x06007840 RID: 30784 RVA: 0x0032B2F4 File Offset: 0x003296F4
		private void DoMove(int move)
		{
			if (this._selectedElement != null || this._lastAudioData != null)
			{
				if (!this._elementList.IsNullOrEmpty<JukeBoxAudioListUI.AudioData>() && 2 <= this._elementList.Count)
				{
					JukeBoxAudioListUI.AudioData audioData = this._selectedElement ?? this._lastAudioData;
					int num = audioData.index + move;
					if (this._elementList.Count <= num)
					{
						num = 0;
					}
					else if (num < 0)
					{
						num = this._elementList.Count - 1;
					}
					this.ChangeSelectedElement(this._elementList.GetElement(num));
				}
			}
			else
			{
				this.ChangeSelectedElement(this._elementList.GetElement(0));
			}
		}

		// Token: 0x06007841 RID: 30785 RVA: 0x0032B3AB File Offset: 0x003297AB
		private void DoSetAudio()
		{
			if (this._selectedElement == null)
			{
				return;
			}
			this._mainUI.SetAudioName(this._selectedElement.text.text, this._selectedElement.fileName);
			this.PlaySE(SoundPack.SystemSE.OK_L);
		}

		// Token: 0x06007842 RID: 30786 RVA: 0x0032B3E6 File Offset: 0x003297E6
		private void DoClose()
		{
			this.IsActiveControl = false;
		}

		// Token: 0x06007843 RID: 30787 RVA: 0x0032B3F0 File Offset: 0x003297F0
		private void SetActiveControl(bool active)
		{
			this._allDisposable.Clear();
			IEnumerator coroutine = (!active) ? this.CloseCoroutine() : this.OpenCoroutine();
			Observable.FromCoroutine(() => coroutine, false).TakeUntilDestroy(this).Subscribe<Unit>().AddTo(this._allDisposable);
		}

		// Token: 0x06007844 RID: 30788 RVA: 0x0032B454 File Offset: 0x00329854
		private IEnumerator OpenCoroutine()
		{
			if (Singleton<SoundPlayer>.IsInstance())
			{
				this._uAudio = Singleton<SoundPlayer>.Instance.uAudio;
			}
			else if (this._uAudio == null)
			{
				this._uAudio = new uAudio();
			}
			if (this._uAudio != null)
			{
				this._uAudio.Volume = 1f;
				this._uAudio.CurrentTime = TimeSpan.Zero;
			}
			base.gameObject.SetActiveSelf(true);
			this._canvasGroup.SetBlocksRaycasts(true);
			this._canvasGroup.SetInteractable(false);
			this.ChangeSelectedElement(null);
			this.UISetting();
			float startAlpha = this.CanvasAlpha;
			IConnectableObservable<TimeInterval<float>> stream = ObservableEasing.Linear(this._alphaAccelerationTime, true).FrameTimeInterval(true).TakeUntilDestroy(this).Publish<TimeInterval<float>>();
			stream.Connect().AddTo(this._allDisposable);
			stream.Subscribe(delegate(TimeInterval<float> x)
			{
				this.CanvasAlpha = Mathf.Lerp(startAlpha, 1f, x.Value);
			});
			yield return stream.ToYieldInstruction<TimeInterval<float>>().AddTo(this._allDisposable);
			this._canvasGroup.SetInteractable(true);
			base.EnabledInput = true;
			yield break;
		}

		// Token: 0x06007845 RID: 30789 RVA: 0x0032B470 File Offset: 0x00329870
		private IEnumerator CloseCoroutine()
		{
			base.EnabledInput = false;
			this._canvasGroup.SetInteractable(false);
			float startAlpha = this.CanvasAlpha;
			IConnectableObservable<TimeInterval<float>> stream = ObservableEasing.Linear(this._alphaAccelerationTime, true).FrameTimeInterval(true).TakeUntilDestroy(this).Publish<TimeInterval<float>>();
			stream.Connect().AddTo(this._allDisposable);
			stream.Subscribe(delegate(TimeInterval<float> x)
			{
				this.CanvasAlpha = Mathf.Lerp(startAlpha, 0f, x.Value);
			});
			yield return stream.ToYieldInstruction<TimeInterval<float>>().AddTo(this._allDisposable);
			this.AudioStop();
			this.Release();
			this._canvasGroup.SetBlocksRaycasts(false);
			base.gameObject.SetActiveSelf(false);
			yield break;
		}

		// Token: 0x06007846 RID: 30790 RVA: 0x0032B48C File Offset: 0x0032988C
		public void UISetting()
		{
			this.Release();
			if (this._noneData == null)
			{
				this._noneData = this.GetElement();
				this._noneData.text.text = (this._noneData.fileName = this._noneStr);
				this._noneData.filePath = string.Empty;
			}
			this._playButton.SetActiveSelf(true);
			this._pauseButton.SetActiveSelf(false);
			this._filePathList.Clear();
			if (!this._extensionList.IsNullOrEmpty<string>())
			{
				foreach (string text in this._extensionList)
				{
					if (!text.IsNullOrEmpty())
					{
						string[] files = Directory.GetFiles(SoundPlayer.Directory.AudioFile, string.Format("*.{0}", text));
						if (!files.IsNullOrEmpty<string>())
						{
							this._filePathList.AddRange(files);
						}
					}
				}
			}
			if (!this._filePathList.IsNullOrEmpty<string>())
			{
				foreach (string text2 in this._filePathList)
				{
					if (!text2.IsNullOrEmpty())
					{
						JukeBoxAudioListUI.AudioData element = this.GetElement();
						element.filePath = text2;
						element.fileName = Path.GetFileName(text2);
						element.text.text = Path.GetFileNameWithoutExtension(text2);
						element.dateTime = File.GetLastWriteTime(text2);
						this._elementList.Add(element);
					}
				}
			}
			this._elementList.Insert(0, this._noneData);
			this.ListSort();
			using (List<JukeBoxAudioListUI.AudioData>.Enumerator enumerator3 = this._elementList.GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					JukeBoxAudioListUI.AudioData elm = enumerator3.Current;
					JukeBoxAudioListUI $this = this;
					elm.button.SetActiveSelf(true);
					elm.ClickAction = delegate(JukeBoxAudioListUI.AudioData x)
					{
						if ($this.InputEnabled)
						{
							$this.ChangeSelectedElement(elm);
						}
					};
					elm.button.onClick.AddListener(delegate()
					{
						Action<JukeBoxAudioListUI.AudioData> clickAction = elm.ClickAction;
						if (clickAction != null)
						{
							clickAction(elm);
						}
					});
					IDisposable doubleClickDisposable = elm.doubleClickDisposable;
					if (doubleClickDisposable != null)
					{
						doubleClickDisposable.Dispose();
					}
					elm.doubleClickDisposable = elm.button.onClick.AsObservable().DoubleInterval(250f, false).Subscribe(delegate(IList<double> _)
					{
						$this.OnDoubleClick(elm);
					}).AddTo(elm.button);
				}
			}
		}

		// Token: 0x06007847 RID: 30791 RVA: 0x0032B780 File Offset: 0x00329B80
		private void ListSort()
		{
			if (this._elementList.IsNullOrEmpty<JukeBoxAudioListUI.AudioData>() || this._elementList.Count <= 2)
			{
				return;
			}
			if (this._noneData != null && this._elementList.Contains(this._noneData))
			{
				this._elementList.Remove(this._noneData);
			}
			int num = this._sortUI.SortIndex();
			if (num != 0)
			{
				if (num == 1)
				{
					if (this._sortToggle.isOn)
					{
						this._elementList.Sort((JukeBoxAudioListUI.AudioData a, JukeBoxAudioListUI.AudioData b) => a.fileName.CompareTo(b.fileName));
					}
					else
					{
						this._elementList.Sort((JukeBoxAudioListUI.AudioData a, JukeBoxAudioListUI.AudioData b) => b.fileName.CompareTo(a.fileName));
					}
				}
			}
			else if (this._sortToggle.isOn)
			{
				this._elementList.Sort((JukeBoxAudioListUI.AudioData a, JukeBoxAudioListUI.AudioData b) => a.dateTime.CompareTo(b.dateTime));
			}
			else
			{
				this._elementList.Sort((JukeBoxAudioListUI.AudioData a, JukeBoxAudioListUI.AudioData b) => b.dateTime.CompareTo(a.dateTime));
			}
			if (this._noneData != null)
			{
				this._elementList.Insert(0, this._noneData);
			}
			for (int i = 0; i < this._elementList.Count; i++)
			{
				JukeBoxAudioListUI.AudioData audioData = this._elementList[i];
				audioData.index = i;
				audioData.button.transform.SetAsLastSibling();
			}
		}

		// Token: 0x06007848 RID: 30792 RVA: 0x0032B934 File Offset: 0x00329D34
		private void ChangeSelectedElement(JukeBoxAudioListUI.AudioData data)
		{
			if (data == null && this._selectedElement != null)
			{
				this._selectedElement.text.color = this._whiteColor;
				this._selectedElement = null;
				this.AudioStop();
				return;
			}
			if (data != null && data == this._selectedElement)
			{
				this._selectedElement.text.color = this._whiteColor;
				this._selectedElement = null;
				return;
			}
			if (data == null)
			{
				return;
			}
			if (this._selectedElement != null)
			{
				this._selectedElement.text.color = this._whiteColor;
			}
			this._selectedElement = data;
			data.text.color = this._yellowColor;
			if (this.IsPlayingAudio)
			{
				this.DoPlay();
			}
		}

		// Token: 0x06007849 RID: 30793 RVA: 0x0032B9F8 File Offset: 0x00329DF8
		private void OnDoubleClick(JukeBoxAudioListUI.AudioData data)
		{
			if (data == null)
			{
				return;
			}
			bool isPlayingAudio = this.IsPlayingAudio;
			if (this._selectedElement == data)
			{
				this.DoSetAudio();
				return;
			}
			if (this._selectedElement != null && this._selectedElement != data)
			{
				this._selectedElement.text.color = this._whiteColor;
				this._selectedElement = null;
				this.AudioStop();
			}
			this._selectedElement = data;
			data.text.color = this._yellowColor;
			this.DoSetAudio();
		}

		// Token: 0x0600784A RID: 30794 RVA: 0x0032BA80 File Offset: 0x00329E80
		private JukeBoxAudioListUI.AudioData GetElement()
		{
			JukeBoxAudioListUI.AudioData audioData = this._elementPool.PopFront<JukeBoxAudioListUI.AudioData>();
			if (audioData == null)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this._elementPrefab, this._elementRoot, false);
				Button button = (gameObject != null) ? gameObject.GetComponent<Button>() : null;
				Text text = (button != null) ? button.GetComponentInChildren<Text>() : null;
				if (button != null && text != null)
				{
					audioData = new JukeBoxAudioListUI.AudioData
					{
						button = button,
						text = text
					};
				}
			}
			return audioData;
		}

		// Token: 0x0600784B RID: 30795 RVA: 0x0032BB08 File Offset: 0x00329F08
		private void ReturnElement(JukeBoxAudioListUI.AudioData elm)
		{
			if (elm == null || this._elementPool.Contains(elm))
			{
				return;
			}
			if (elm.text != null)
			{
				elm.text.color = this._whiteColor;
			}
			elm.Clear();
			elm.Deactivate();
			this._elementPool.Add(elm);
		}

		// Token: 0x0600784C RID: 30796 RVA: 0x0032BB68 File Offset: 0x00329F68
		private void Release()
		{
			foreach (JukeBoxAudioListUI.AudioData audioData in this._elementList)
			{
				if (audioData != null)
				{
					this.ReturnElement(audioData);
				}
			}
			this._elementList.Clear();
			this._noneData = null;
			this._selectedElement = null;
		}

		// Token: 0x0600784D RID: 30797 RVA: 0x0032BBE4 File Offset: 0x00329FE4
		private void PlaySE(SoundPack.SystemSE se)
		{
			SoundPack soundPack = (!Singleton<Manager.Resources>.IsInstance()) ? null : Singleton<Manager.Resources>.Instance.SoundPack;
			if (soundPack != null)
			{
				soundPack.Play(se);
			}
		}

		// Token: 0x0400614C RID: 24908
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x0400614D RID: 24909
		[SerializeField]
		private RectTransform _rectTransform;

		// Token: 0x0400614E RID: 24910
		[SerializeField]
		private Button _closeButton;

		// Token: 0x0400614F RID: 24911
		[SerializeField]
		private Toggle _sortToggle;

		// Token: 0x04006150 RID: 24912
		[SerializeField]
		private Image _sortSelectedImage;

		// Token: 0x04006151 RID: 24913
		[SerializeField]
		private Button _sortButton;

		// Token: 0x04006152 RID: 24914
		[SerializeField]
		private Transform _elementRoot;

		// Token: 0x04006153 RID: 24915
		[SerializeField]
		private GameObject _elementPrefab;

		// Token: 0x04006154 RID: 24916
		[SerializeField]
		private string _noneStr = string.Empty;

		// Token: 0x04006155 RID: 24917
		[SerializeField]
		private List<string> _extensionList = new List<string>();

		// Token: 0x04006156 RID: 24918
		[SerializeField]
		private Button _setButton;

		// Token: 0x04006157 RID: 24919
		[SerializeField]
		private Button _leftButton;

		// Token: 0x04006158 RID: 24920
		[SerializeField]
		private Button _rightButton;

		// Token: 0x04006159 RID: 24921
		[SerializeField]
		private Button _playButton;

		// Token: 0x0400615A RID: 24922
		[SerializeField]
		private Button _pauseButton;

		// Token: 0x0400615B RID: 24923
		[SerializeField]
		private Color _whiteColor = Color.white;

		// Token: 0x0400615C RID: 24924
		[SerializeField]
		private Color _yellowColor = Color.yellow;

		// Token: 0x0400615D RID: 24925
		[SerializeField]
		private JukeBoxUI _mainUI;

		// Token: 0x0400615E RID: 24926
		[SerializeField]
		private AudioSortUI _sortUI;

		// Token: 0x0400615F RID: 24927
		private Button _noneButton;

		// Token: 0x04006160 RID: 24928
		private JukeBoxAudioListUI.AudioData _noneData;

		// Token: 0x04006161 RID: 24929
		private List<JukeBoxAudioListUI.AudioData> _elementPool = new List<JukeBoxAudioListUI.AudioData>();

		// Token: 0x04006162 RID: 24930
		private List<string> _filePathList = new List<string>();

		// Token: 0x04006163 RID: 24931
		private List<JukeBoxAudioListUI.AudioData> _elementList = new List<JukeBoxAudioListUI.AudioData>();

		// Token: 0x04006164 RID: 24932
		private JukeBoxAudioListUI.AudioData _selectedElement;

		// Token: 0x04006165 RID: 24933
		private AudioSource _currentPlayAudio;

		// Token: 0x04006166 RID: 24934
		private uAudio _uAudio;

		// Token: 0x04006167 RID: 24935
		private JukeBoxAudioListUI.AudioData _lastAudioData;

		// Token: 0x04006168 RID: 24936
		private CompositeDisposable _allDisposable = new CompositeDisposable();

		// Token: 0x02000E98 RID: 3736
		public class AudioData
		{
			// Token: 0x170017A1 RID: 6049
			// (get) Token: 0x06007869 RID: 30825 RVA: 0x0032BD77 File Offset: 0x0032A177
			// (set) Token: 0x0600786A RID: 30826 RVA: 0x0032BD7F File Offset: 0x0032A17F
			public Action<JukeBoxAudioListUI.AudioData> ClickAction { get; set; }

			// Token: 0x0600786B RID: 30827 RVA: 0x0032BD88 File Offset: 0x0032A188
			public void Clear()
			{
				this.index = -1;
				this.ClickAction = null;
				this.dateTime = DateTime.MinValue;
				this.button.onClick.RemoveAllListeners();
				this.text.text = string.Empty;
				this.filePath = string.Empty;
				this.fileName = string.Empty;
			}

			// Token: 0x0600786C RID: 30828 RVA: 0x0032BDE4 File Offset: 0x0032A1E4
			public void Activate()
			{
				this.button.SetActiveSelf(true);
			}

			// Token: 0x0600786D RID: 30829 RVA: 0x0032BDF2 File Offset: 0x0032A1F2
			public void Deactivate()
			{
				this.button.SetActiveSelf(false);
			}

			// Token: 0x0400616D RID: 24941
			public Button button;

			// Token: 0x0400616E RID: 24942
			public Text text;

			// Token: 0x0400616F RID: 24943
			public int index = -1;

			// Token: 0x04006171 RID: 24945
			public DateTime dateTime = DateTime.MinValue;

			// Token: 0x04006172 RID: 24946
			public IDisposable doubleClickDisposable;

			// Token: 0x04006173 RID: 24947
			public string filePath = string.Empty;

			// Token: 0x04006174 RID: 24948
			public string fileName = string.Empty;
		}
	}
}
