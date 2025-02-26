using System;
using System.Collections.Generic;
using AIProject;
using Illusion.Component.UI;
using Illusion.Extensions;
using Manager;
using SceneAssist;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameLoadCharaFileSystem
{
	// Token: 0x0200087C RID: 2172
	public class GameLoadCharaWindow : MonoBehaviour
	{
		// Token: 0x06003771 RID: 14193 RVA: 0x00148A01 File Offset: 0x00146E01
		public GameLoadCharaWindow()
		{
			this.onChangeItemFunc = null;
		}

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x06003772 RID: 14194 RVA: 0x00148A2C File Offset: 0x00146E2C
		public bool useMale
		{
			get
			{
				return this._useMale;
			}
		}

		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x06003773 RID: 14195 RVA: 0x00148A34 File Offset: 0x00146E34
		public bool useFemale
		{
			get
			{
				return this._useFemale;
			}
		}

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x06003774 RID: 14196 RVA: 0x00148A3C File Offset: 0x00146E3C
		public bool useMyData
		{
			get
			{
				return this._useMyData;
			}
		}

		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x06003776 RID: 14198 RVA: 0x00148A4D File Offset: 0x00146E4D
		// (set) Token: 0x06003775 RID: 14197 RVA: 0x00148A44 File Offset: 0x00146E44
		public bool useDownload
		{
			get
			{
				return this._useDownload;
			}
			set
			{
				this._useDownload = value;
			}
		}

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x06003778 RID: 14200 RVA: 0x00148A5E File Offset: 0x00146E5E
		// (set) Token: 0x06003777 RID: 14199 RVA: 0x00148A55 File Offset: 0x00146E55
		public bool addFirstEmpty
		{
			get
			{
				return this._addFirstEmpty;
			}
			set
			{
				this._addFirstEmpty = value;
			}
		}

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x06003779 RID: 14201 RVA: 0x00148A66 File Offset: 0x00146E66
		public int windowType
		{
			get
			{
				return this._windowType;
			}
		}

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x0600377A RID: 14202 RVA: 0x00148A6E File Offset: 0x00146E6E
		public GameLoadCharaListCtrl listCtrl
		{
			get
			{
				return this._listCtrl;
			}
		}

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x0600377B RID: 14203 RVA: 0x00148A76 File Offset: 0x00146E76
		// (set) Token: 0x0600377C RID: 14204 RVA: 0x00148A7E File Offset: 0x00146E7E
		public bool hideClose
		{
			get
			{
				return this._hideClose;
			}
			set
			{
				if (this.btnClose)
				{
					this._hideClose = value;
					this.btnClose.gameObject.SetActiveIfDifferent(!value);
				}
			}
		}

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x0600377D RID: 14205 RVA: 0x00148AAC File Offset: 0x00146EAC
		// (set) Token: 0x0600377E RID: 14206 RVA: 0x00148AB4 File Offset: 0x00146EB4
		public bool hideCharacterCreation
		{
			get
			{
				return this._hideCharacterCreation;
			}
			set
			{
				if (this.btnCharacterCreation)
				{
					this._hideCharacterCreation = value;
					this.btnCharacterCreation.gameObject.SetActiveIfDifferent(!value);
				}
			}
		}

		// Token: 0x170009DB RID: 2523
		// (set) Token: 0x0600377F RID: 14207 RVA: 0x00148AE2 File Offset: 0x00146EE2
		public GameLoadCharaListCtrl.OnChangeItemFunc onChangeItemFunc
		{
			set
			{
				if (null != this.listCtrl)
				{
					this.listCtrl.onChangeItemFunc = value;
				}
			}
		}

		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x06003780 RID: 14208 RVA: 0x00148B01 File Offset: 0x00146F01
		// (set) Token: 0x06003781 RID: 14209 RVA: 0x00148B09 File Offset: 0x00146F09
		public bool IsStartUp { get; private set; }

		// Token: 0x06003782 RID: 14210 RVA: 0x00148B14 File Offset: 0x00146F14
		public void InitCharaList(bool enableFirstEmpty = true)
		{
			this.lstMaleCharaFileInfo = GameCharaFileInfoAssist.CreateCharaFileInfoList(this.useMale, false, this.useMyData, this.useDownload, enableFirstEmpty && this.addFirstEmpty);
			this.lstFemaleCharaFileInfo = GameCharaFileInfoAssist.CreateCharaFileInfoList(false, this.useFemale, this.useMyData, this.useDownload, enableFirstEmpty && this.addFirstEmpty);
		}

		// Token: 0x06003783 RID: 14211 RVA: 0x00148B7B File Offset: 0x00146F7B
		private void CreateCharaList(List<GameCharaFileInfo> _lst, bool _isSelectInfoClear = false)
		{
			this.listCtrl.ClearList();
			this.listCtrl.AddList(_lst);
			this.listCtrl.Create(_isSelectInfoClear);
		}

		// Token: 0x06003784 RID: 14212 RVA: 0x00148BA0 File Offset: 0x00146FA0
		private void CreateCharaListViewOnly(List<GameCharaFileInfo> _lst, bool _isSelectInfoClear = false)
		{
			this.listCtrl.ClearList();
			this.listCtrl.AddList(_lst);
			this.listCtrl.CreateListView(_isSelectInfoClear);
		}

		// Token: 0x06003785 RID: 14213 RVA: 0x00148BC8 File Offset: 0x00146FC8
		public void UpdateWindow(int _type, bool _isCreateList = true, bool _isSelectInfoClear = false)
		{
			this._windowType = _type;
			this.objEntrySelect.SetActiveIfDifferent(false);
			if (this.windowType == 0)
			{
				this.objPlayerTitleMenu.SetActiveIfDifferent(true);
				this.objFemaleTitleMenu.SetActiveIfDifferent(false);
				this.objSexSelect.SetActiveIfDifferent(true);
				this.objMaleSelect.SetActiveIfDifferent(false);
				this.objFemaleSelect.SetActiveIfDifferent(false);
				if (_isCreateList)
				{
					this.CreateCharaList((!this.tglMale.isOn) ? this.lstFemaleCharaFileInfo : this.lstMaleCharaFileInfo, _isSelectInfoClear);
				}
			}
			else if (this.windowType == 1)
			{
				this.selectSex = 1;
				this.objPlayerTitleMenu.SetActiveIfDifferent(false);
				this.objFemaleTitleMenu.SetActiveIfDifferent(true);
				this.sccFemaleTitleImageIcon.OnChangeValue(this.femaleNum);
				this.objSexSelect.SetActiveIfDifferent(false);
				if (_isCreateList)
				{
					this.CreateCharaList(this.lstFemaleCharaFileInfo, _isSelectInfoClear);
				}
			}
			if (_isSelectInfoClear && this.selectReactive != null)
			{
				this.selectReactive.Value = false;
			}
		}

		// Token: 0x06003786 RID: 14214 RVA: 0x00148CE4 File Offset: 0x001470E4
		public void UpdateWindow(bool _isCreateList = true, bool _isSelectInfoClear = false)
		{
			if (this.windowType == 0)
			{
				if (_isCreateList)
				{
					this.CreateCharaListViewOnly((!this.tglMale.isOn) ? this.lstFemaleCharaFileInfo : this.lstMaleCharaFileInfo, _isSelectInfoClear);
				}
			}
			else if (this.windowType == 1 && _isCreateList)
			{
				this.CreateCharaListViewOnly(this.lstFemaleCharaFileInfo, _isSelectInfoClear);
			}
			if (_isSelectInfoClear && this.selectReactive != null)
			{
				this.selectReactive.Value = false;
			}
		}

		// Token: 0x06003787 RID: 14215 RVA: 0x00148D6C File Offset: 0x0014716C
		public void ReCreateList(bool _isSelectInfoClear, bool enableFirstEmpty = true)
		{
			this.InitCharaList(enableFirstEmpty);
			this.tglMale.SetIsOnWithoutCallback(true);
			this.tglFemale.SetIsOnWithoutCallback(false);
			this.selectSex = 0;
			this._listCtrl.InitSort();
			int windowType = this.windowType;
			this.UpdateWindow(windowType, true, _isSelectInfoClear);
		}

		// Token: 0x06003788 RID: 14216 RVA: 0x00148DBC File Offset: 0x001471BC
		public void ReCreateListOnly(bool _isSelectInfoClear, bool enableFirstEmpty = true)
		{
			this.InitCharaList(enableFirstEmpty);
			this.UpdateWindow(true, _isSelectInfoClear);
		}

		// Token: 0x06003789 RID: 14217 RVA: 0x00148DDA File Offset: 0x001471DA
		public void Awake()
		{
		}

		// Token: 0x0600378A RID: 14218 RVA: 0x00148DDC File Offset: 0x001471DC
		public void Start()
		{
			if (this.IsStartUp)
			{
				return;
			}
			this.InitCharaList(true);
			if (this.btnEntry)
			{
				(from _ in this.btnEntry.OnClickAsObservable()
				where !Singleton<Scene>.Instance.IsNowLoadingFade
				select _).Subscribe(delegate(Unit _)
				{
					if (this.onLoadItemFunc != null)
					{
						this.onLoadItemFunc(this.listCtrl.GetNowSelectCard());
					}
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_L);
				});
			}
			this.actionEntry.listActionEnter.Add(delegate
			{
				if (!this.btnEntry)
				{
					return;
				}
				if (!this.btnEntry.interactable)
				{
					return;
				}
				this.objEntrySelect.SetActiveIfDifferent(true);
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
			});
			this.actionEntry.listActionExit.Add(delegate
			{
				if (!this.btnEntry)
				{
					return;
				}
				if (!this.btnEntry.interactable)
				{
					return;
				}
				this.objEntrySelect.SetActiveIfDifferent(false);
			});
			this.UpdateWindow(this.windowType, true, false);
			if (this.tglMale)
			{
				(from _ in this.tglMale.onValueChanged.AsObservable<bool>()
				where this.selectSex != 0
				select _).Subscribe(delegate(bool _isOn)
				{
					if (!_isOn)
					{
						return;
					}
					this.selectSex = 0;
					this.CreateCharaList(this.lstMaleCharaFileInfo, false);
					this.listCtrl.SetNowSelectToggle();
					GameCharaFileInfo nowSelectCard = this.listCtrl.GetNowSelectCard();
					if (nowSelectCard != null)
					{
						this.listCtrl.SetParameterWindowVisible(true);
					}
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
				});
				this.tglMale.OnPointerEnterAsObservable().Subscribe(delegate(PointerEventData _)
				{
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
				});
			}
			if (this.tglFemale)
			{
				(from _ in this.tglFemale.onValueChanged.AsObservable<bool>()
				where this.selectSex != 1
				select _).Subscribe(delegate(bool _isOn)
				{
					if (!_isOn)
					{
						return;
					}
					this.selectSex = 1;
					this.CreateCharaList(this.lstFemaleCharaFileInfo, false);
					this.listCtrl.SetNowSelectToggle();
					GameCharaFileInfo nowSelectCard = this.listCtrl.GetNowSelectCard();
					if (nowSelectCard != null)
					{
						this.listCtrl.SetParameterWindowVisible(true);
					}
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
				});
				this.tglFemale.OnPointerEnterAsObservable().Subscribe(delegate(PointerEventData _)
				{
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
				});
			}
			if (this.btnCharacterCreation)
			{
				(from _ in this.btnCharacterCreation.OnClickAsObservable()
				where !Singleton<Scene>.Instance.IsNowLoadingFade
				select _).Subscribe(delegate(Unit _)
				{
					if (this.onCharaCreateClickAction != null)
					{
						this.onCharaCreateClickAction(this.selectSex);
					}
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_L);
				});
				this.btnCharacterCreation.OnPointerEnterAsObservable().Subscribe(delegate(PointerEventData _)
				{
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
				});
				this.btnCharacterCreation.gameObject.SetActiveIfDifferent(!this._hideCharacterCreation);
			}
			this.actionFemaleSelect.listActionEnter.Add(delegate
			{
				this.objFemaleSelect.SetActiveIfDifferent(true);
			});
			this.actionFemaleSelect.listActionExit.Add(delegate
			{
				this.objFemaleSelect.SetActiveIfDifferent(false);
			});
			this.actionMaleSelect.listActionEnter.Add(delegate
			{
				this.objMaleSelect.SetActiveIfDifferent(true);
			});
			this.actionMaleSelect.listActionExit.Add(delegate
			{
				this.objMaleSelect.SetActiveIfDifferent(false);
			});
			this.selectReactive = new BoolReactiveProperty(false);
			this.selectReactive.SubscribeToInteractable(this.btnEntry);
			this.listCtrl.onChangeItem = delegate(bool _isOn)
			{
				this.selectReactive.Value = _isOn;
			};
			(from _ in this.UpdateAsObservable()
			where UnityEngine.Input.GetMouseButtonUp(1)
			where !Singleton<Scene>.Instance.IsNowLoadingFade
			select _).Subscribe(delegate(Unit _)
			{
				if (this.onClickRightFunc != null)
				{
					this.onClickRightFunc();
				}
			});
			if (this.btnClose)
			{
				this.btnClose.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
					if (this.onCloseWindowFunc != null)
					{
						this.onCloseWindowFunc();
					}
				});
				this.btnClose.OnPointerEnterAsObservable().Subscribe(delegate(PointerEventData _)
				{
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Select);
				});
				this.btnClose.gameObject.SetActiveIfDifferent(!this._hideClose);
			}
			this.IsStartUp = true;
		}

		// Token: 0x0400381C RID: 14364
		[SerializeField]
		private bool _useMale = true;

		// Token: 0x0400381D RID: 14365
		[SerializeField]
		private bool _useFemale = true;

		// Token: 0x0400381E RID: 14366
		[SerializeField]
		private bool _useMyData = true;

		// Token: 0x0400381F RID: 14367
		[SerializeField]
		private bool _useDownload = true;

		// Token: 0x04003820 RID: 14368
		[SerializeField]
		private bool _addFirstEmpty;

		// Token: 0x04003821 RID: 14369
		[SerializeField]
		private int _windowType;

		// Token: 0x04003822 RID: 14370
		[SerializeField]
		private GameLoadCharaListCtrl _listCtrl;

		// Token: 0x04003823 RID: 14371
		[SerializeField]
		private GameObject objPlayerTitleMenu;

		// Token: 0x04003824 RID: 14372
		[SerializeField]
		private GameObject objFemaleTitleMenu;

		// Token: 0x04003825 RID: 14373
		[SerializeField]
		private SpriteChangeCtrl sccFemaleTitleImageIcon;

		// Token: 0x04003826 RID: 14374
		[SerializeField]
		private GameObject objSexSelect;

		// Token: 0x04003827 RID: 14375
		[SerializeField]
		private Toggle tglMale;

		// Token: 0x04003828 RID: 14376
		[SerializeField]
		private GameObject objMaleSelect;

		// Token: 0x04003829 RID: 14377
		[SerializeField]
		private PointerEnterExitAction actionMaleSelect;

		// Token: 0x0400382A RID: 14378
		[SerializeField]
		private Toggle tglFemale;

		// Token: 0x0400382B RID: 14379
		[SerializeField]
		private GameObject objFemaleSelect;

		// Token: 0x0400382C RID: 14380
		[SerializeField]
		private PointerEnterExitAction actionFemaleSelect;

		// Token: 0x0400382D RID: 14381
		[SerializeField]
		private Button btnCharacterCreation;

		// Token: 0x0400382E RID: 14382
		[SerializeField]
		private Button btnEntry;

		// Token: 0x0400382F RID: 14383
		[SerializeField]
		private GameObject objEntrySelect;

		// Token: 0x04003830 RID: 14384
		[SerializeField]
		private PointerEnterExitAction actionEntry;

		// Token: 0x04003831 RID: 14385
		[SerializeField]
		private Button btnClose;

		// Token: 0x04003832 RID: 14386
		[SerializeField]
		private bool _hideClose;

		// Token: 0x04003833 RID: 14387
		[SerializeField]
		private bool _hideCharacterCreation;

		// Token: 0x04003834 RID: 14388
		public GameLoadCharaWindow.OnCloseWindowFunc onCloseWindowFunc;

		// Token: 0x04003835 RID: 14389
		public Action<GameCharaFileInfo> onLoadItemFunc;

		// Token: 0x04003836 RID: 14390
		public Action onClickRightFunc;

		// Token: 0x04003837 RID: 14391
		public int femaleNum;

		// Token: 0x04003838 RID: 14392
		public Action<int> onCharaCreateClickAction;

		// Token: 0x0400383A RID: 14394
		private List<GameCharaFileInfo> lstMaleCharaFileInfo;

		// Token: 0x0400383B RID: 14395
		private List<GameCharaFileInfo> lstFemaleCharaFileInfo;

		// Token: 0x0400383C RID: 14396
		private BoolReactiveProperty selectReactive;

		// Token: 0x0400383D RID: 14397
		private int selectSex;

		// Token: 0x0200087D RID: 2173
		// (Invoke) Token: 0x060037A3 RID: 14243
		public delegate void OnCloseWindowFunc();
	}
}
