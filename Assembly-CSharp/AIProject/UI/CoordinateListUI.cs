using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Illusion.Extensions;
using Manager;
using ReMotion;
using SceneAssist;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AIProject.UI
{
	// Token: 0x02000E56 RID: 3670
	public class CoordinateListUI : MenuUIBehaviour
	{
		// Token: 0x060073F9 RID: 29689 RVA: 0x003164A1 File Offset: 0x003148A1
		public List<GameCoordinateFileInfo> GetListCharaFileInfo()
		{
			return this._listFileInfo;
		}

		// Token: 0x1700165E RID: 5726
		// (get) Token: 0x060073FA RID: 29690 RVA: 0x003164A9 File Offset: 0x003148A9
		// (set) Token: 0x060073FB RID: 29691 RVA: 0x003164B1 File Offset: 0x003148B1
		[HideInInspector]
		public bool UpdateCategory { get; set; }

		// Token: 0x1700165F RID: 5727
		// (get) Token: 0x060073FC RID: 29692 RVA: 0x003164BA File Offset: 0x003148BA
		// (set) Token: 0x060073FD RID: 29693 RVA: 0x003164C2 File Offset: 0x003148C2
		public CoordinateListUI.ChangeItemFunc OnChangeItemFunc { get; set; }

		// Token: 0x17001660 RID: 5728
		// (get) Token: 0x060073FE RID: 29694 RVA: 0x003164CB File Offset: 0x003148CB
		// (set) Token: 0x060073FF RID: 29695 RVA: 0x003164D3 File Offset: 0x003148D3
		public Action<bool> OnChangeItem { get; set; }

		// Token: 0x06007400 RID: 29696 RVA: 0x003164DC File Offset: 0x003148DC
		public IObservable<bool> OnActiveSortWindowChangedAsObservable()
		{
			if (this._activeChangeSortWindow == null)
			{
				this._activeChangeSortWindow = (from _ in this._isActiveSortWindow.TakeUntilDestroy(base.gameObject)
				where base.isActiveAndEnabled
				select _).Publish<bool>();
				this._activeChangeSortWindow.Connect();
			}
			return this._activeChangeSortWindow;
		}

		// Token: 0x06007401 RID: 29697 RVA: 0x00316534 File Offset: 0x00314934
		protected override void OnBeforeStart()
		{
			this._toggleOrder.onValueChanged.AsObservable<bool>().Subscribe(delegate(bool isOn)
			{
				this._imageOrder.enabled = !isOn;
				if (this._toggleSortDay.isOn)
				{
					this.SortDate(isOn);
				}
				else
				{
					this.SortFileName(isOn);
				}
				this._scrollData.Init(this._listFileInfo);
				this._scrollData.SetNowSelectToggle();
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
			});
			this._imageOrder.enabled = !this._toggleOrder.isOn;
			this._objOrderSelect.SetActiveIfDifferent(false);
			this._actionOrderSelect.listActionEnter.Add(delegate
			{
				this._objOrderSelect.SetActiveIfDifferent(true);
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
			});
			this._actionOrderSelect.listActionExit.Add(delegate
			{
				this._objOrderSelect.SetActiveIfDifferent(false);
			});
			this._buttonSortWindowOpen.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this._isActiveSortWindow.Value = true;
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
			});
			this._buttonSortWindowOpen.OnPointerEnterAsObservable().Subscribe(delegate(PointerEventData _)
			{
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
			});
			this._scrollData.OnSelect = delegate(GameCoordinateFileInfo data)
			{
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
				CoordinateListUI.ChangeItemFunc onChangeItemFunc = this.OnChangeItemFunc;
				if (onChangeItemFunc != null)
				{
					onChangeItemFunc(data);
				}
				Action<bool> onChangeItem = this.OnChangeItem;
				if (onChangeItem != null)
				{
					onChangeItem(true);
				}
			};
			this._scrollData.OnDeselect = delegate()
			{
				Action<bool> onChangeItem = this.OnChangeItem;
				if (onChangeItem != null)
				{
					onChangeItem(false);
				}
			};
			this._objSortWindow.SetActiveIfDifferent(false);
			this._buttonSortWindowClose.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this._isActiveSortWindow.Value = false;
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
			});
			this._buttonSortWindowClose.OnPointerEnterAsObservable().Subscribe(delegate(PointerEventData _)
			{
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
			});
			this._actionSortDay.listActionEnter.Add(delegate
			{
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
				this._objSortDaySelect.SetActiveIfDifferent(true);
			});
			this._actionSortDay.listActionExit.Add(delegate
			{
				this._objSortDaySelect.SetActiveIfDifferent(false);
			});
			this._actionSortName.listActionEnter.Add(delegate
			{
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
				this._objSortNameSelect.SetActiveIfDifferent(true);
			});
			this._actionSortName.listActionExit.Add(delegate
			{
				this._objSortNameSelect.SetActiveIfDifferent(false);
			});
			this._objSortDaySelect.SetActiveIfDifferent(false);
			this._objSortNameSelect.SetActiveIfDifferent(false);
			if (this._toggleSortDay)
			{
				(from _ in this._toggleSortDay.onValueChanged.AsObservable<bool>()
				where this._sortSelectNum != 0
				select _).Subscribe(delegate(bool isOn)
				{
					if (!isOn)
					{
						return;
					}
					this._sortSelectNum = 0;
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
					this.SortDate(this._toggleOrder.isOn);
					this._scrollData.Init(this._listFileInfo);
					this._scrollData.SetNowSelectToggle();
				});
			}
			if (this._toggleSortName)
			{
				(from _ in this._toggleSortName.onValueChanged.AsObservable<bool>()
				where this._sortSelectNum != 1
				select _).Subscribe(delegate(bool isOn)
				{
					if (!isOn)
					{
						return;
					}
					this._sortSelectNum = 1;
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
					this.SortFileName(this._toggleOrder.isOn);
					this._scrollData.Init(this._listFileInfo);
					this._scrollData.SetNowSelectToggle();
				});
			}
			this.OnActiveSortWindowChangedAsObservable().Subscribe(delegate(bool active)
			{
				this.SetActiveSortWindow(active);
			});
		}

		// Token: 0x06007402 RID: 29698 RVA: 0x003167BC File Offset: 0x00314BBC
		public void SortDate(bool ascend)
		{
			if (this._listFileInfo.Count == 0)
			{
				return;
			}
			using (new GameSystem.CultureScope())
			{
				if (ascend)
				{
					this._listFileInfo = (from n in this._listFileInfo
					orderby n.Time, n.FileName
					select n).ToList<GameCoordinateFileInfo>();
				}
				else
				{
					this._listFileInfo = (from n in this._listFileInfo
					orderby n.Time descending, n.FileName descending
					select n).ToList<GameCoordinateFileInfo>();
				}
			}
		}

		// Token: 0x06007403 RID: 29699 RVA: 0x003168B8 File Offset: 0x00314CB8
		public void SortFileName(bool ascend)
		{
			if (this._listFileInfo.Count == 0)
			{
				return;
			}
			using (new GameSystem.CultureScope())
			{
				if (ascend)
				{
					this._listFileInfo = (from n in this._listFileInfo
					orderby n.FileName, n.Time
					select n).ToList<GameCoordinateFileInfo>();
				}
				else
				{
					this._listFileInfo = (from n in this._listFileInfo
					orderby n.FileName descending, n.Time descending
					select n).ToList<GameCoordinateFileInfo>();
				}
			}
		}

		// Token: 0x06007404 RID: 29700 RVA: 0x003169B4 File Offset: 0x00314DB4
		public void SelectDataClear()
		{
			if (this._scrollData != null)
			{
				this._scrollData.SelectDataClear();
			}
		}

		// Token: 0x06007405 RID: 29701 RVA: 0x003169CE File Offset: 0x00314DCE
		public void SetNowSelectToggle()
		{
			if (this._scrollData != null)
			{
				this._scrollData.SetNowSelectToggle();
			}
		}

		// Token: 0x06007406 RID: 29702 RVA: 0x003169E8 File Offset: 0x00314DE8
		public void ClearList()
		{
			this._listFileInfo.Clear();
		}

		// Token: 0x06007407 RID: 29703 RVA: 0x003169F5 File Offset: 0x00314DF5
		public void AddList(List<GameCoordinateFileInfo> list)
		{
			this._listFileInfo = new List<GameCoordinateFileInfo>(list);
		}

		// Token: 0x06007408 RID: 29704 RVA: 0x00316A03 File Offset: 0x00314E03
		public GameCoordinateFileInfo GetNowSelectCard()
		{
			GameCoordinateFileScrollInfo.ScrollData selectData = this._scrollData.SelectData;
			return (selectData != null) ? selectData.info : null;
		}

		// Token: 0x06007409 RID: 29705 RVA: 0x00316A1F File Offset: 0x00314E1F
		public void InitSort()
		{
			this._toggleOrder.SetIsOnWithoutCallback(true);
			this._imageOrder.enabled = !this._toggleOrder.isOn;
			this._sortSelectNum = 0;
			this._toggleSortDay.SetIsOnWithoutCallback(true);
		}

		// Token: 0x0600740A RID: 29706 RVA: 0x00316A5C File Offset: 0x00314E5C
		public void Create(bool isSelectInfoClear)
		{
			if (this._toggleSortDay.isOn)
			{
				this.SortDate(this._toggleOrder.isOn);
			}
			else
			{
				this.SortFileName(this._toggleOrder.isOn);
			}
			if (isSelectInfoClear)
			{
				this._scrollData.SelectDataClear();
				this._scrollData.SetTopLine();
			}
			this._scrollData.Init(this._listFileInfo);
			this._isActiveSortWindow.Value = false;
			this._objSortWindow.SetActiveIfDifferent(false);
		}

		// Token: 0x0600740B RID: 29707 RVA: 0x00316AE8 File Offset: 0x00314EE8
		private void SetActiveSortWindow(bool active)
		{
			Manager.Input instance = Singleton<Manager.Input>.Instance;
			IEnumerator coroutine;
			if (active)
			{
				coroutine = this.OpenSortWindowCoroutine();
			}
			else
			{
				coroutine = this.CloseSortWindowCoroutine();
			}
			if (this._fadeSortWindowDisposable != null)
			{
				this._fadeSortWindowDisposable.Dispose();
			}
			this._fadeSortWindowDisposable = Observable.FromCoroutine(() => coroutine, false).Subscribe(delegate(Unit _)
			{
			}, delegate(Exception ex)
			{
			});
		}

		// Token: 0x0600740C RID: 29708 RVA: 0x00316B94 File Offset: 0x00314F94
		private IEnumerator OpenSortWindowCoroutine()
		{
			this._objSortWindow.SetActiveIfDifferent(true);
			this._canvasGroupSortWindow.blocksRaycasts = true;
			float startAlpha = this._canvasGroupSortWindow.alpha;
			IObservable<TimeInterval<float>> stream = ObservableEasing.Linear(0.2f, true).FrameTimeInterval(true).Do(delegate(TimeInterval<float> x)
			{
				this._canvasGroupSortWindow.alpha = Mathf.Lerp(startAlpha, 1f, x.Value);
			});
			yield return stream.ToYieldInstruction<TimeInterval<float>>();
			yield break;
		}

		// Token: 0x0600740D RID: 29709 RVA: 0x00316BB0 File Offset: 0x00314FB0
		private IEnumerator CloseSortWindowCoroutine()
		{
			this._canvasGroupSortWindow.blocksRaycasts = false;
			float startAlpha = this._canvasGroupSortWindow.alpha;
			IObservable<TimeInterval<float>> stream = ObservableEasing.Linear(0.2f, true).FrameTimeInterval(true).Do(delegate(TimeInterval<float> x)
			{
				this._canvasGroupSortWindow.alpha = Mathf.Lerp(startAlpha, 0f, x.Value);
			});
			yield return stream.ToYieldInstruction<TimeInterval<float>>();
			this._objSortWindow.SetActiveIfDifferent(false);
			yield break;
		}

		// Token: 0x04005ED7 RID: 24279
		[SerializeField]
		private ClosetUI _closetUI;

		// Token: 0x04005ED8 RID: 24280
		[SerializeField]
		private Toggle _toggleOrder;

		// Token: 0x04005ED9 RID: 24281
		[SerializeField]
		private Image _imageOrder;

		// Token: 0x04005EDA RID: 24282
		[SerializeField]
		private PointerEnterExitAction _actionOrderSelect;

		// Token: 0x04005EDB RID: 24283
		[SerializeField]
		private GameObject _objOrderSelect;

		// Token: 0x04005EDC RID: 24284
		[SerializeField]
		private Button _buttonSortWindowOpen;

		// Token: 0x04005EDD RID: 24285
		[SerializeField]
		private GameCoordinateFileScrollInfo _scrollData = new GameCoordinateFileScrollInfo();

		// Token: 0x04005EDE RID: 24286
		[SerializeField]
		private GameObject _objSortWindow;

		// Token: 0x04005EDF RID: 24287
		[SerializeField]
		private CanvasGroup _canvasGroupSortWindow;

		// Token: 0x04005EE0 RID: 24288
		[SerializeField]
		private Button _buttonSortWindowClose;

		// Token: 0x04005EE1 RID: 24289
		[SerializeField]
		private Toggle _toggleSortDay;

		// Token: 0x04005EE2 RID: 24290
		[SerializeField]
		private Toggle _toggleSortName;

		// Token: 0x04005EE3 RID: 24291
		[SerializeField]
		private PointerEnterExitAction _actionSortDay;

		// Token: 0x04005EE4 RID: 24292
		[SerializeField]
		private PointerEnterExitAction _actionSortName;

		// Token: 0x04005EE5 RID: 24293
		[SerializeField]
		private GameObject _objSortDaySelect;

		// Token: 0x04005EE6 RID: 24294
		[SerializeField]
		private GameObject _objSortNameSelect;

		// Token: 0x04005EE7 RID: 24295
		private List<GameCoordinateFileInfo> _listFileInfo = new List<GameCoordinateFileInfo>();

		// Token: 0x04005EEB RID: 24299
		private int _sortSelectNum;

		// Token: 0x04005EEC RID: 24300
		private int femaleParameterSelectNum;

		// Token: 0x04005EED RID: 24301
		private BoolReactiveProperty _isActiveSortWindow = new BoolReactiveProperty(false);

		// Token: 0x04005EEE RID: 24302
		private IConnectableObservable<bool> _activeChangeSortWindow;

		// Token: 0x04005EEF RID: 24303
		private IDisposable _fadeSortWindowDisposable;

		// Token: 0x02000E57 RID: 3671
		// (Invoke) Token: 0x0600742C RID: 29740
		public delegate void ChangeItemFunc(GameCoordinateFileInfo info);
	}
}
