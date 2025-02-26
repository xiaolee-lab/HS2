using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Illusion.Extensions;
using Manager;
using ReMotion;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AIProject.UI
{
	// Token: 0x02000E4C RID: 3660
	public class ClosetUI : MenuUIBehaviour
	{
		// Token: 0x06007334 RID: 29492 RVA: 0x00311A47 File Offset: 0x0030FE47
		public ClosetUI()
		{
			this.OnChangeFolderItemFunc = null;
			this.OnChangeSavedataItemFunc = null;
		}

		// Token: 0x17001627 RID: 5671
		// (get) Token: 0x06007335 RID: 29493 RVA: 0x00311A68 File Offset: 0x0030FE68
		public CoordinateListUI FolderCoordinateListUI
		{
			[CompilerGenerated]
			get
			{
				return this._folderCoordinateListUI;
			}
		}

		// Token: 0x17001628 RID: 5672
		// (get) Token: 0x06007336 RID: 29494 RVA: 0x00311A70 File Offset: 0x0030FE70
		public CoordinateListUI SaveDataCoordinateListUI
		{
			[CompilerGenerated]
			get
			{
				return this._savedataCoordinateListUI;
			}
		}

		// Token: 0x17001629 RID: 5673
		// (get) Token: 0x06007337 RID: 29495 RVA: 0x00311A78 File Offset: 0x0030FE78
		// (set) Token: 0x06007338 RID: 29496 RVA: 0x00311A80 File Offset: 0x0030FE80
		public bool HideClose
		{
			get
			{
				return this._hideClose;
			}
			set
			{
				if (this._buttonClose)
				{
					this._hideClose = value;
					this._buttonClose.gameObject.SetActiveIfDifferent(!value);
				}
			}
		}

		// Token: 0x1700162A RID: 5674
		// (set) Token: 0x06007339 RID: 29497 RVA: 0x00311AAE File Offset: 0x0030FEAE
		public CoordinateListUI.ChangeItemFunc OnChangeFolderItemFunc
		{
			set
			{
				if (this._folderCoordinateListUI != null)
				{
					this._folderCoordinateListUI.OnChangeItemFunc = value;
				}
			}
		}

		// Token: 0x1700162B RID: 5675
		// (set) Token: 0x0600733A RID: 29498 RVA: 0x00311ACD File Offset: 0x0030FECD
		public CoordinateListUI.ChangeItemFunc OnChangeSavedataItemFunc
		{
			set
			{
				if (this._savedataCoordinateListUI != null)
				{
					this._savedataCoordinateListUI.OnChangeItemFunc = value;
				}
			}
		}

		// Token: 0x1700162C RID: 5676
		// (get) Token: 0x0600733B RID: 29499 RVA: 0x00311AEC File Offset: 0x0030FEEC
		// (set) Token: 0x0600733C RID: 29500 RVA: 0x00311AF4 File Offset: 0x0030FEF4
		public Action OnClickRightFunc { get; set; }

		// Token: 0x1700162D RID: 5677
		// (get) Token: 0x0600733D RID: 29501 RVA: 0x00311AFD File Offset: 0x0030FEFD
		// (set) Token: 0x0600733E RID: 29502 RVA: 0x00311B05 File Offset: 0x0030FF05
		public List<string> CoordinateFilterSource { get; set; }

		// Token: 0x1700162E RID: 5678
		// (get) Token: 0x0600733F RID: 29503 RVA: 0x00311B0E File Offset: 0x0030FF0E
		public MenuUIBehaviour[] MenuUIList
		{
			get
			{
				if (this._menuUIList == null)
				{
					this._menuUIList = new MenuUIBehaviour[]
					{
						this,
						this._folderCoordinateListUI,
						this._savedataCoordinateListUI
					};
				}
				return this._menuUIList;
			}
		}

		// Token: 0x06007340 RID: 29504 RVA: 0x00311B43 File Offset: 0x0030FF43
		public IObservable<bool> OnActiveParameterChangedAsObservable()
		{
			if (this._activeParameterChange == null)
			{
				this._activeParameterChange = this._isActiveParameter.TakeUntilDestroy(base.gameObject).Publish<bool>();
				this._activeParameterChange.Connect();
			}
			return this._activeParameterChange;
		}

		// Token: 0x06007341 RID: 29505 RVA: 0x00311B7E File Offset: 0x0030FF7E
		public void InitCoordinateList()
		{
			this._folderCoordinateFileInfo = GameCoordinateFileInfoAssist.CreateCoordinateFileInfoList(false, true, this.CoordinateFilterSource);
			this._savedataCoordinateFileInfo = GameCoordinateFileInfoAssist.CreateCoordinateFileInfoQueryList(this.CoordinateFilterSource);
		}

		// Token: 0x06007342 RID: 29506 RVA: 0x00311BA4 File Offset: 0x0030FFA4
		private void CreateCoordinateList(List<GameCoordinateFileInfo> list, List<GameCoordinateFileInfo> saveList, bool isSelectInfoClear = false)
		{
			this._folderCoordinateListUI.ClearList();
			this._folderCoordinateListUI.AddList(list);
			this._folderCoordinateListUI.Create(isSelectInfoClear);
			this._savedataCoordinateListUI.ClearList();
			this._savedataCoordinateListUI.AddList(saveList);
			this._savedataCoordinateListUI.Create(isSelectInfoClear);
		}

		// Token: 0x06007343 RID: 29507 RVA: 0x00311BF7 File Offset: 0x0030FFF7
		public void ReCreateList(bool isSelectInfoClear)
		{
			this.InitCoordinateList();
			this._folderCoordinateListUI.InitSort();
			this._savedataCoordinateListUI.InitSort();
			this.CreateCoordinateList(this._folderCoordinateFileInfo, this._savedataCoordinateFileInfo, false);
		}

		// Token: 0x06007344 RID: 29508 RVA: 0x00311C28 File Offset: 0x00310028
		private void SetParameter(GameCoordinateFileInfo data, bool fromFolder)
		{
			bool activeSelf = this._objParameterWindow.activeSelf;
			this._objParameterWindow.SetActiveIfDifferent(true);
			this._rawImageCoordinateCard.texture = PngAssist.ChangeTextureFromByte(PngFile.LoadPngBytes(data.FullPath), 0, 0, TextureFormat.ARGB32, false);
			this._buttonInput.gameObject.SetActiveIfDifferent(fromFolder);
			this._buttonOutput.gameObject.SetActiveIfDifferent(!fromFolder);
		}

		// Token: 0x06007345 RID: 29509 RVA: 0x00311C94 File Offset: 0x00310094
		protected override void OnBeforeStart()
		{
			base.OnActiveChangedAsObservable().Subscribe(delegate(bool active)
			{
				this.SetActiveControl(active);
			});
			this.OnActiveParameterChangedAsObservable().Subscribe(delegate(bool active)
			{
				this.SetActiveParameter(active);
			});
			this._selectFolder = new BoolReactiveProperty();
			this._selectFolder.Subscribe(delegate(bool x)
			{
				if (x)
				{
					this._buttonInput.gameObject.SetActiveIfDifferent(true);
					this._buttonOutput.gameObject.SetActiveIfDifferent(false);
					this._isActiveParameter.Value = true;
				}
				else
				{
					this._isActiveParameter.Value = false;
				}
			});
			this._selectSaveData = new BoolReactiveProperty();
			this._selectSaveData.Subscribe(delegate(bool x)
			{
				if (x)
				{
					this._buttonInput.gameObject.SetActiveIfDifferent(false);
					this._buttonOutput.gameObject.SetActiveIfDifferent(true);
					this._isActiveParameter.Value = true;
				}
				else
				{
					this._isActiveParameter.Value = false;
				}
			});
			this._folderCoordinateListUI.OnChangeItemFunc = delegate(GameCoordinateFileInfo dat)
			{
				this._selectInfo = dat;
				if (dat != null)
				{
					this._rawImageCoordinateCard.texture = PngAssist.ChangeTextureFromByte(PngFile.LoadPngBytes(dat.FullPath), 0, 0, TextureFormat.ARGB32, false);
				}
			};
			this._savedataCoordinateListUI.OnChangeItemFunc = delegate(GameCoordinateFileInfo dat)
			{
				this._selectInfo = dat;
				if (dat != null)
				{
					this._rawImageCoordinateCard.texture = PngAssist.ChangeTextureFromByte(PngFile.LoadPngBytes(dat.FullPath), 0, 0, TextureFormat.ARGB32, false);
				}
			};
			this._folderCoordinateListUI.OnChangeItem = delegate(bool isOn)
			{
				if (isOn)
				{
					this._selectSaveData.Value = !isOn;
					this._savedataCoordinateListUI.SelectDataClear();
					this._savedataCoordinateListUI.SetNowSelectToggle();
					this._selectFolder.Value = isOn;
				}
				else
				{
					this._selectFolder.Value = false;
					this._selectSaveData.Value = false;
				}
			};
			this._savedataCoordinateListUI.OnChangeItem = delegate(bool isOn)
			{
				if (isOn)
				{
					this._selectFolder.Value = !isOn;
					this._folderCoordinateListUI.SelectDataClear();
					this._folderCoordinateListUI.SetNowSelectToggle();
					this._selectSaveData.Value = isOn;
				}
				else
				{
					this._selectFolder.Value = false;
					this._selectSaveData.Value = false;
				}
			};
			if (this._buttonInput)
			{
				this._buttonInput.onClick.AddListener(delegate()
				{
					if (!this.CoordinateFilterSource.Contains(this._selectInfo.FileName))
					{
						this.CoordinateFilterSource.Add(this._selectInfo.FileName);
					}
					this._isActiveParameter.Value = false;
					this._selectFolder.Value = false;
					this.ReCreateList(true);
				});
			}
			if (this._buttonOutput)
			{
				this._buttonOutput.onClick.AddListener(delegate()
				{
					this.CoordinateFilterSource.Remove(this._selectInfo.FileName);
					this._isActiveParameter.Value = false;
					this._selectSaveData.Value = false;
					this.ReCreateList(true);
				});
			}
			KeyCodeDownCommand keyCodeDownCommand = new KeyCodeDownCommand
			{
				KeyCode = KeyCode.Mouse1
			};
			keyCodeDownCommand.TriggerEvent.AddListener(delegate()
			{
				this.Close();
				Action onClickRightFunc = this.OnClickRightFunc;
				if (onClickRightFunc != null)
				{
					onClickRightFunc();
				}
			});
			this._keyCommands.Add(keyCodeDownCommand);
			if (this._buttonClose)
			{
				this._buttonClose.onClick.AddListener(delegate()
				{
					this.Close();
				});
				this._buttonClose.OnPointerEnterAsObservable().Subscribe(delegate(PointerEventData _)
				{
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
				});
				this._buttonClose.gameObject.SetActiveIfDifferent(!this._hideClose);
			}
		}

		// Token: 0x06007346 RID: 29510 RVA: 0x00311E75 File Offset: 0x00310275
		private void Close()
		{
			Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
			this.IsActiveControl = false;
		}

		// Token: 0x06007347 RID: 29511 RVA: 0x00311E90 File Offset: 0x00310290
		private void SetActiveControl(bool active)
		{
			Manager.Input instance = Singleton<Manager.Input>.Instance;
			IEnumerator coroutine;
			if (active)
			{
				Time.timeScale = 0f;
				this.InitCoordinateList();
				this.CreateCoordinateList(this._folderCoordinateFileInfo, this._savedataCoordinateFileInfo, false);
				instance.FocusLevel = 0;
				instance.MenuElements = this.MenuUIList;
				coroutine = this.OpenCoroutine();
			}
			else
			{
				instance.ClearMenuElements();
				instance.FocusLevel = -1;
				coroutine = this.CloseCoroutine();
			}
			if (this._fadeDisposable != null)
			{
				this._fadeDisposable.Dispose();
			}
			this._fadeDisposable = Observable.FromCoroutine(() => coroutine, false).Subscribe(delegate(Unit _)
			{
			}, delegate(Exception ex)
			{
			});
		}

		// Token: 0x06007348 RID: 29512 RVA: 0x00311F80 File Offset: 0x00310380
		private IEnumerator OpenCoroutine()
		{
			yield return MapUIContainer.DrawOnceTutorialUI(14, null);
			Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.UI);
			Singleton<Manager.Input>.Instance.SetupState();
			if (this._canvasGroup.blocksRaycasts)
			{
				this._canvasGroup.blocksRaycasts = false;
			}
			float startAlpha = this._canvasGroup.alpha;
			IObservable<TimeInterval<float>> fadeInStream = ObservableEasing.Linear(0.3f, true).FrameTimeInterval(true).Do(delegate(TimeInterval<float> x)
			{
				this._canvasGroup.alpha = Mathf.Lerp(startAlpha, 1f, x.Value);
			});
			yield return fadeInStream.ToYieldInstruction<TimeInterval<float>>();
			this._canvasGroup.blocksRaycasts = true;
			base.EnabledInput = true;
			yield break;
		}

		// Token: 0x06007349 RID: 29513 RVA: 0x00311F9C File Offset: 0x0031039C
		private IEnumerator CloseCoroutine()
		{
			base.EnabledInput = false;
			this._canvasGroup.blocksRaycasts = false;
			float startAlpha = this._canvasGroup.alpha;
			IObservable<TimeInterval<float>> fadeOutStream = ObservableEasing.Linear(0.3f, true).FrameTimeInterval(true).Do(delegate(TimeInterval<float> x)
			{
				this._canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, x.Value);
			});
			yield return fadeOutStream.ToYieldInstruction<TimeInterval<float>>();
			Time.timeScale = 1f;
			yield break;
		}

		// Token: 0x0600734A RID: 29514 RVA: 0x00311FB8 File Offset: 0x003103B8
		private void SetActiveParameter(bool active)
		{
			Manager.Input instance = Singleton<Manager.Input>.Instance;
			IEnumerator coroutine;
			if (active)
			{
				coroutine = this.OpenParameterCoroutine();
			}
			else
			{
				coroutine = this.CloseParameterCoroutine();
			}
			if (this._fadeParameterDisposable != null)
			{
				this._fadeParameterDisposable.Dispose();
			}
			this._fadeParameterDisposable = Observable.FromCoroutine(() => coroutine, false).Subscribe(delegate(Unit _)
			{
			}, delegate(Exception ex)
			{
			});
		}

		// Token: 0x0600734B RID: 29515 RVA: 0x00312064 File Offset: 0x00310464
		private IEnumerator OpenParameterCoroutine()
		{
			this._objParameterWindow.SetActiveIfDifferent(true);
			this._canvasGroupParameter.blocksRaycasts = true;
			float startAlpha = this._canvasGroupParameter.alpha;
			IObservable<TimeInterval<float>> stream = ObservableEasing.Linear(0.2f, true).FrameTimeInterval(true).Do(delegate(TimeInterval<float> x)
			{
				this._canvasGroupParameter.alpha = Mathf.Lerp(startAlpha, 1f, x.Value);
			});
			yield return stream.ToYieldInstruction<TimeInterval<float>>();
			yield break;
		}

		// Token: 0x0600734C RID: 29516 RVA: 0x00312080 File Offset: 0x00310480
		private IEnumerator CloseParameterCoroutine()
		{
			this._canvasGroupParameter.blocksRaycasts = false;
			float startAlpha = this._canvasGroupParameter.alpha;
			IObservable<TimeInterval<float>> stream = ObservableEasing.Linear(0.2f, true).FrameTimeInterval(true).Do(delegate(TimeInterval<float> x)
			{
				this._canvasGroupParameter.alpha = Mathf.Lerp(startAlpha, 0f, x.Value);
			});
			yield return stream.ToYieldInstruction<TimeInterval<float>>();
			this._objParameterWindow.SetActiveIfDifferent(false);
			yield break;
		}

		// Token: 0x04005E3A RID: 24122
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x04005E3B RID: 24123
		[SerializeField]
		private CoordinateListUI _folderCoordinateListUI;

		// Token: 0x04005E3C RID: 24124
		[SerializeField]
		private CoordinateListUI _savedataCoordinateListUI;

		// Token: 0x04005E3D RID: 24125
		[SerializeField]
		private Button _buttonClose;

		// Token: 0x04005E3E RID: 24126
		[SerializeField]
		private bool _hideClose;

		// Token: 0x04005E3F RID: 24127
		[SerializeField]
		private CanvasGroup _canvasGroupParameter;

		// Token: 0x04005E40 RID: 24128
		[SerializeField]
		private GameObject _objParameterWindow;

		// Token: 0x04005E41 RID: 24129
		[SerializeField]
		private RawImage _rawImageCoordinateCard;

		// Token: 0x04005E42 RID: 24130
		[SerializeField]
		private Button _buttonInput;

		// Token: 0x04005E43 RID: 24131
		[SerializeField]
		private Button _buttonOutput;

		// Token: 0x04005E45 RID: 24133
		private int _femaleID;

		// Token: 0x04005E46 RID: 24134
		private List<GameCoordinateFileInfo> _folderCoordinateFileInfo;

		// Token: 0x04005E47 RID: 24135
		private List<GameCoordinateFileInfo> _savedataCoordinateFileInfo;

		// Token: 0x04005E49 RID: 24137
		private BoolReactiveProperty _selectFolder;

		// Token: 0x04005E4A RID: 24138
		private BoolReactiveProperty _selectSaveData;

		// Token: 0x04005E4B RID: 24139
		private GameCoordinateFileInfo _selectInfo;

		// Token: 0x04005E4C RID: 24140
		private MenuUIBehaviour[] _menuUIList;

		// Token: 0x04005E4D RID: 24141
		private BoolReactiveProperty _isActiveParameter = new BoolReactiveProperty();

		// Token: 0x04005E4E RID: 24142
		private IConnectableObservable<bool> _activeParameterChange;

		// Token: 0x04005E4F RID: 24143
		private IDisposable _fadeDisposable;

		// Token: 0x04005E50 RID: 24144
		private IDisposable _fadeParameterDisposable;
	}
}
