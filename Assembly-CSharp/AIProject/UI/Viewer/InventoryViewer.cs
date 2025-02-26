using System;
using System.Collections;
using System.Runtime.CompilerServices;
using AIProject.ColorDefine;
using AIProject.Scene;
using Manager;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UnityEx;

namespace AIProject.UI.Viewer
{
	// Token: 0x02000E80 RID: 3712
	public class InventoryViewer : MonoBehaviour
	{
		// Token: 0x17001717 RID: 5911
		// (get) Token: 0x06007682 RID: 30338 RVA: 0x00323942 File Offset: 0x00321D42
		public MenuUIBehaviour[] MenuUIList
		{
			get
			{
				return this.GetCache(ref this._menuUIList, () => new MenuUIBehaviour[]
				{
					this._categoryUI,
					this._sortUI,
					this._itemListUI
				});
			}
		}

		// Token: 0x17001718 RID: 5912
		// (get) Token: 0x06007683 RID: 30339 RVA: 0x0032395C File Offset: 0x00321D5C
		public ItemFilterCategoryUI categoryUI
		{
			[CompilerGenerated]
			get
			{
				return this._categoryUI;
			}
		}

		// Token: 0x17001719 RID: 5913
		// (get) Token: 0x06007684 RID: 30340 RVA: 0x00323964 File Offset: 0x00321D64
		public ItemListUI itemListUI
		{
			[CompilerGenerated]
			get
			{
				return this._itemListUI;
			}
		}

		// Token: 0x1700171A RID: 5914
		// (get) Token: 0x06007685 RID: 30341 RVA: 0x0032396C File Offset: 0x00321D6C
		public ItemSortUI sortUI
		{
			[CompilerGenerated]
			get
			{
				return this._sortUI;
			}
		}

		// Token: 0x1700171B RID: 5915
		// (get) Token: 0x06007686 RID: 30342 RVA: 0x00323974 File Offset: 0x00321D74
		public Button sortButton
		{
			[CompilerGenerated]
			get
			{
				return this._sortButton;
			}
		}

		// Token: 0x1700171C RID: 5916
		// (get) Token: 0x06007687 RID: 30343 RVA: 0x0032397C File Offset: 0x00321D7C
		public Toggle sorter
		{
			[CompilerGenerated]
			get
			{
				return this._sorter;
			}
		}

		// Token: 0x1700171D RID: 5917
		// (get) Token: 0x06007688 RID: 30344 RVA: 0x00323984 File Offset: 0x00321D84
		public Image cursor
		{
			[CompilerGenerated]
			get
			{
				return this._cursor;
			}
		}

		// Token: 0x1700171E RID: 5918
		// (get) Token: 0x06007689 RID: 30345 RVA: 0x0032398C File Offset: 0x00321D8C
		public ConditionalTextXtoYViewer slotCounter
		{
			[CompilerGenerated]
			get
			{
				return this._slotCounter;
			}
		}

		// Token: 0x1700171F RID: 5919
		// (get) Token: 0x0600768A RID: 30346 RVA: 0x00323994 File Offset: 0x00321D94
		public Text emptyText
		{
			[CompilerGenerated]
			get
			{
				return this._emptyText;
			}
		}

		// Token: 0x17001720 RID: 5920
		// (get) Token: 0x0600768B RID: 30347 RVA: 0x0032399C File Offset: 0x00321D9C
		// (set) Token: 0x0600768C RID: 30348 RVA: 0x003239A4 File Offset: 0x00321DA4
		public bool isAutoEmptyText { get; set; }

		// Token: 0x0600768D RID: 30349 RVA: 0x003239AD File Offset: 0x00321DAD
		public void SortUIBind(ItemSortUI sortUI)
		{
			this._sortUI = sortUI;
		}

		// Token: 0x17001721 RID: 5921
		// (get) Token: 0x0600768E RID: 30350 RVA: 0x003239B6 File Offset: 0x00321DB6
		private InventoryViewer.SortData sortData { get; } = new InventoryViewer.SortData();

		// Token: 0x0600768F RID: 30351 RVA: 0x003239BE File Offset: 0x00321DBE
		public void SetParentMenuUI(MenuUIBehaviour parentMenuUI)
		{
			this.parentMenuUI = parentMenuUI;
		}

		// Token: 0x17001722 RID: 5922
		// (get) Token: 0x06007690 RID: 30352 RVA: 0x003239C7 File Offset: 0x00321DC7
		public bool IsActiveControl
		{
			[CompilerGenerated]
			get
			{
				return this.parentMenuUI == null || this.parentMenuUI.IsActiveControl;
			}
		}

		// Token: 0x17001723 RID: 5923
		// (get) Token: 0x06007691 RID: 30353 RVA: 0x003239EB File Offset: 0x00321DEB
		// (set) Token: 0x06007692 RID: 30354 RVA: 0x003239F3 File Offset: 0x00321DF3
		public Action<int> setFocusLevel { get; set; }

		// Token: 0x17001724 RID: 5924
		// (get) Token: 0x06007693 RID: 30355 RVA: 0x003239FC File Offset: 0x00321DFC
		// (set) Token: 0x06007694 RID: 30356 RVA: 0x00323A04 File Offset: 0x00321E04
		public MenuUIBehaviour parentMenuUI { get; private set; }

		// Token: 0x06007695 RID: 30357 RVA: 0x00323A0D File Offset: 0x00321E0D
		public void ForceSortType(int type)
		{
			this.sortData.ForceSortType(type);
		}

		// Token: 0x17001725 RID: 5925
		// (get) Token: 0x06007696 RID: 30358 RVA: 0x00323A1B File Offset: 0x00321E1B
		// (set) Token: 0x06007697 RID: 30359 RVA: 0x00323A28 File Offset: 0x00321E28
		public int SortType
		{
			get
			{
				return this.sortData.type;
			}
			set
			{
				this.sortData.type = value;
			}
		}

		// Token: 0x06007698 RID: 30360 RVA: 0x00323A36 File Offset: 0x00321E36
		public void ForceSortAscending(bool ascending)
		{
			this.sortData.ForceSortAscending(ascending);
		}

		// Token: 0x17001726 RID: 5926
		// (get) Token: 0x06007699 RID: 30361 RVA: 0x00323A44 File Offset: 0x00321E44
		// (set) Token: 0x0600769A RID: 30362 RVA: 0x00323A51 File Offset: 0x00321E51
		public bool SortAscending
		{
			get
			{
				return this.sortData.ascending;
			}
			set
			{
				this.sortData.ascending = value;
			}
		}

		// Token: 0x0600769B RID: 30363 RVA: 0x00323A5F File Offset: 0x00321E5F
		public void SortItemList()
		{
			this._itemListUI.SortItems(this.sortData.type, this.sortData.ascending);
		}

		// Token: 0x17001727 RID: 5927
		// (get) Token: 0x0600769C RID: 30364 RVA: 0x00323A82 File Offset: 0x00321E82
		// (set) Token: 0x0600769D RID: 30365 RVA: 0x00323A8A File Offset: 0x00321E8A
		public bool initialized { get; private set; }

		// Token: 0x17001728 RID: 5928
		// (get) Token: 0x0600769E RID: 30366 RVA: 0x00323A93 File Offset: 0x00321E93
		// (set) Token: 0x0600769F RID: 30367 RVA: 0x00323A9B File Offset: 0x00321E9B
		public bool categoryInitialized { get; private set; }

		// Token: 0x17001729 RID: 5929
		// (get) Token: 0x060076A0 RID: 30368 RVA: 0x00323AA4 File Offset: 0x00321EA4
		// (set) Token: 0x060076A1 RID: 30369 RVA: 0x00323AAC File Offset: 0x00321EAC
		public bool sortUIInitialized { get; private set; }

		// Token: 0x060076A2 RID: 30370 RVA: 0x00323AB8 File Offset: 0x00321EB8
		public IEnumerator CategoryButtonAddEvent(Action<int> action)
		{
			while (!this._categoryUI.SetButtonListener(delegate(int i)
			{
				action(i);
			}))
			{
				yield return null;
			}
			this.categoryInitialized = true;
			yield break;
		}

		// Token: 0x060076A3 RID: 30371 RVA: 0x00323ADA File Offset: 0x00321EDA
		public void ChangeTitleIcon(Sprite sprite)
		{
			this._iconText.icon.sprite = sprite;
		}

		// Token: 0x060076A4 RID: 30372 RVA: 0x00323AED File Offset: 0x00321EED
		public void ChangeTitleText(string text)
		{
			this._iconText.text.text = text;
		}

		// Token: 0x060076A5 RID: 30373 RVA: 0x00323B00 File Offset: 0x00321F00
		public void SetFocusLevel(int level)
		{
			Singleton<Manager.Input>.Instance.FocusLevel = level;
			this._categoryUI.EnabledInput = (level == this._categoryUI.FocusLevel);
			this._itemListUI.EnabledInput = (level == this._itemListUI.FocusLevel);
			if (this._sortUI != null)
			{
				this._sortUI.EnabledInput = (level == this._sortUI.FocusLevel);
			}
			Action<int> setFocusLevel = this.setFocusLevel;
			if (setFocusLevel != null)
			{
				setFocusLevel(level);
			}
		}

		// Token: 0x060076A6 RID: 30374 RVA: 0x00323B8C File Offset: 0x00321F8C
		private IEnumerator Start()
		{
			this._slotCounter.Initialize();
			if (this._slotCounter.xText != null)
			{
				IObservable<Colors> source = from i in this._slotCounter.X
				select (i >= this._slotCounter.y) ? Colors.Red : Colors.White;
				if (InventoryViewer.<>f__mg$cache0 == null)
				{
					InventoryViewer.<>f__mg$cache0 = new Func<Colors, Color>(Define.Get);
				}
				source.Select(InventoryViewer.<>f__mg$cache0).Subscribe(delegate(Color color)
				{
					this._slotCounter.xText.color = color;
				}).AddTo(this._slotCounter.xText);
			}
			this.sortData.Type.Subscribe(delegate(int type)
			{
				this._itemListUI.SortItems(type, this.sortData.ascending);
			});
			this.sortData.Ascending.Subscribe(delegate(bool ascending)
			{
				this._itemListUI.SortItems(this.sortData.type, ascending);
			});
			if (this._sorter != null)
			{
				this._sorter.isOn = this.sortData.ascending;
				this._sorter.onValueChanged.AsObservable<bool>().Subscribe(delegate(bool isOn)
				{
					this.sortData.ascending = isOn;
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
				});
				if (this._sorter.targetGraphic != null)
				{
					this._sorter.OnValueChangedAsObservable().Subscribe(delegate(bool isOn)
					{
						this._sorter.targetGraphic.enabled = !isOn;
					});
				}
			}
			if (this._sortButton != null)
			{
				this._sortButton.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
				});
			}
			this.initialized = true;
			yield break;
		}

		// Token: 0x060076A7 RID: 30375 RVA: 0x00323BA8 File Offset: 0x00321FA8
		public static IEnumerator Load(Transform viewerParent, Action<InventoryViewer> onComplete)
		{
			while (!Singleton<Manager.Resources>.IsInstance())
			{
				yield return null;
			}
			string bundle = Singleton<Manager.Resources>.Instance.DefinePack.ABPaths.MapScenePrefab;
			GameObject asset = CommonLib.LoadAsset<GameObject>(bundle, "InventoryViewer", false, string.Empty);
			if (!MapScene.AssetBundlePaths.Exists((UnityEx.ValueTuple<string, string> x) => x.Item1 == bundle))
			{
				MapScene.AssetBundlePaths.Add(new UnityEx.ValueTuple<string, string>(bundle, string.Empty));
			}
			onComplete.Call(UnityEngine.Object.Instantiate<GameObject>(asset, viewerParent, false).GetComponent<InventoryViewer>());
			yield break;
		}

		// Token: 0x060076A8 RID: 30376 RVA: 0x00323BCC File Offset: 0x00321FCC
		public IEnumerator LoadSortUI(Action<ItemSortUI> onComplete)
		{
			if (!(this._sortUI != null))
			{
				while (!Singleton<Manager.Resources>.IsInstance())
				{
					yield return null;
				}
				string bundle = Singleton<Manager.Resources>.Instance.DefinePack.ABPaths.MapScenePrefab;
				GameObject asset = CommonLib.LoadAsset<GameObject>(bundle, "SortUI", false, string.Empty);
				if (asset != null)
				{
					this._sortUI = UnityEngine.Object.Instantiate<GameObject>(asset, base.transform, false).GetComponent<ItemSortUI>();
					if (!MapScene.AssetBundlePaths.Exists((UnityEx.ValueTuple<string, string> x) => x.Item1 == bundle))
					{
						MapScene.AssetBundlePaths.Add(new UnityEx.ValueTuple<string, string>(bundle, string.Empty));
					}
				}
			}
			onComplete.Call(this._sortUI);
			if (this._sortUI != null)
			{
				this._sortUI.TypeChanged += delegate(int n)
				{
					this.sortData.type = n;
				};
			}
			this.sortUIInitialized = true;
			yield break;
		}

		// Token: 0x060076A9 RID: 30377 RVA: 0x00323BF0 File Offset: 0x00321FF0
		public void SetCursorFocus(Selectable selectable)
		{
			this._categoryUI.useCursor = false;
			CursorFrame.Set(this._cursor.rectTransform, selectable.GetComponent<RectTransform>(), null);
			this._cursor.enabled = true;
			if (Singleton<Manager.Input>.Instance.FocusLevel != this._categoryUI.FocusLevel)
			{
				Singleton<Manager.Input>.Instance.FocusLevel = this._categoryUI.FocusLevel;
			}
			if (!this._categoryUI.EnabledInput)
			{
				this._categoryUI.EnabledInput = true;
			}
		}

		// Token: 0x0400606E RID: 24686
		private MenuUIBehaviour[] _menuUIList;

		// Token: 0x04006070 RID: 24688
		[SerializeField]
		private InventoryViewer.IconText _iconText;

		// Token: 0x04006071 RID: 24689
		[SerializeField]
		private ItemFilterCategoryUI _categoryUI;

		// Token: 0x04006072 RID: 24690
		[SerializeField]
		private ItemListUI _itemListUI;

		// Token: 0x04006073 RID: 24691
		[SerializeField]
		private ItemSortUI _sortUI;

		// Token: 0x04006074 RID: 24692
		[SerializeField]
		private Button _sortButton;

		// Token: 0x04006075 RID: 24693
		[SerializeField]
		private Toggle _sorter;

		// Token: 0x04006076 RID: 24694
		[SerializeField]
		private Image _cursor;

		// Token: 0x04006077 RID: 24695
		[SerializeField]
		private ConditionalTextXtoYViewer _slotCounter;

		// Token: 0x04006078 RID: 24696
		[SerializeField]
		private Text _emptyText;

		// Token: 0x0400607F RID: 24703
		[CompilerGenerated]
		private static Func<Colors, Color> <>f__mg$cache0;

		// Token: 0x02000E81 RID: 3713
		[Serializable]
		private class IconText
		{
			// Token: 0x1700172A RID: 5930
			// (get) Token: 0x060076AC RID: 30380 RVA: 0x00323CA2 File Offset: 0x003220A2
			public Image icon
			{
				[CompilerGenerated]
				get
				{
					return this._icon;
				}
			}

			// Token: 0x1700172B RID: 5931
			// (get) Token: 0x060076AD RID: 30381 RVA: 0x00323CAA File Offset: 0x003220AA
			public Text text
			{
				[CompilerGenerated]
				get
				{
					return this._text;
				}
			}

			// Token: 0x04006080 RID: 24704
			[SerializeField]
			private Image _icon;

			// Token: 0x04006081 RID: 24705
			[SerializeField]
			private Text _text;
		}

		// Token: 0x02000E82 RID: 3714
		private class SortData
		{
			// Token: 0x1700172C RID: 5932
			// (get) Token: 0x060076AF RID: 30383 RVA: 0x00323CD2 File Offset: 0x003220D2
			public IObservable<int> Type
			{
				[CompilerGenerated]
				get
				{
					return this._type;
				}
			}

			// Token: 0x1700172D RID: 5933
			// (get) Token: 0x060076B0 RID: 30384 RVA: 0x00323CDA File Offset: 0x003220DA
			public IObservable<bool> Ascending
			{
				[CompilerGenerated]
				get
				{
					return this._ascending;
				}
			}

			// Token: 0x060076B1 RID: 30385 RVA: 0x00323CE2 File Offset: 0x003220E2
			public void ForceSortType(int type)
			{
				this._type.SetValueAndForceNotify(type);
			}

			// Token: 0x060076B2 RID: 30386 RVA: 0x00323CF0 File Offset: 0x003220F0
			public void ForceSortAscending(bool ascending)
			{
				this._ascending.SetValueAndForceNotify(ascending);
			}

			// Token: 0x1700172E RID: 5934
			// (get) Token: 0x060076B3 RID: 30387 RVA: 0x00323CFE File Offset: 0x003220FE
			// (set) Token: 0x060076B4 RID: 30388 RVA: 0x00323D0B File Offset: 0x0032210B
			public int type
			{
				get
				{
					return this._type.Value;
				}
				set
				{
					this._type.Value = value;
				}
			}

			// Token: 0x1700172F RID: 5935
			// (get) Token: 0x060076B5 RID: 30389 RVA: 0x00323D19 File Offset: 0x00322119
			// (set) Token: 0x060076B6 RID: 30390 RVA: 0x00323D26 File Offset: 0x00322126
			public bool ascending
			{
				get
				{
					return this._ascending.Value;
				}
				set
				{
					this._ascending.Value = value;
				}
			}

			// Token: 0x17001730 RID: 5936
			// (get) Token: 0x060076B7 RID: 30391 RVA: 0x00323D34 File Offset: 0x00322134
			private IntReactiveProperty _type { get; } = new IntReactiveProperty(0);

			// Token: 0x17001731 RID: 5937
			// (get) Token: 0x060076B8 RID: 30392 RVA: 0x00323D3C File Offset: 0x0032213C
			private BoolReactiveProperty _ascending { get; } = new BoolReactiveProperty(true);
		}
	}
}
