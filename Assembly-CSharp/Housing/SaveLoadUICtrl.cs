using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using AIProject.Scene;
using Housing.SaveLoad;
using Illusion.Extensions;
using Manager;
using SuperScrollView;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Housing
{
	// Token: 0x020008AE RID: 2222
	[Serializable]
	public class SaveLoadUICtrl : UIDerived
	{
		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x060039DB RID: 14811 RVA: 0x00153D9D File Offset: 0x0015219D
		// (set) Token: 0x060039DC RID: 14812 RVA: 0x00153DAA File Offset: 0x001521AA
		public bool Visible
		{
			get
			{
				return this.visibleReactive.Value;
			}
			set
			{
				this.visibleReactive.Value = value;
			}
		}

		// Token: 0x060039DD RID: 14813 RVA: 0x00153DB8 File Offset: 0x001521B8
		public override void Init(UICtrl _uiCtrl, bool _tutorial)
		{
			base.Init(_uiCtrl, _tutorial);
			this.buttonClose.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.Close();
			});
			foreach (KeyValuePair<int, Housing.AreaSizeInfo> keyValuePair in Singleton<Housing>.Instance.dicAreaSizeInfo)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.tabOriginal, this.tabRoot);
				gameObject.SetActive(true);
				SizeTab component = gameObject.GetComponent<SizeTab>();
				int type = keyValuePair.Value.no;
				component.Text = string.Format("{0}X{1}X{2}", keyValuePair.Value.limitSize.x, keyValuePair.Value.limitSize.y, keyValuePair.Value.limitSize.z);
				(from _b in component.toggle.OnValueChangedAsObservable()
				where _b
				select _b).Subscribe(delegate(bool _)
				{
					this.SelectTab(type, false);
				});
				this.dicTabInfo[type] = new SaveLoadUICtrl.TabInfo
				{
					gameObject = gameObject,
					sizeTab = component
				};
			}
			for (int i = 0; i < this.buttonsThumbnail.Length; i++)
			{
				int idx = i;
				this.buttonsThumbnail[i].OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					this.infoLoad.Setup(this.rawsThumbnail[idx].texture);
					this.select = this.thumbnailLimit * this.page + idx;
				});
			}
			this.infoLoad.buttonClose.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.infoLoad.Visible = false;
			});
			this.infoLoad.buttonDelete.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				ConfirmScene.Sentence = "データを消去しますか？";
				ConfirmScene.OnClickedYes = delegate()
				{
					File.Delete(this.listPath[this.select]);
					this.InitInfo(this.tab);
					this.SetPage(this.page, true);
					this.infoLoad.Visible = false;
				};
				ConfirmScene.OnClickedNo = delegate()
				{
				};
				Singleton<Game>.Instance.LoadDialog();
			});
			this.infoLoad.buttonLoad.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				ConfirmScene.Sentence = "データを読込みますか？\n" + "セットされたアイテムは削除されます。".Coloring("#DE4529FF").Size(24);
				ConfirmScene.OnClickedYes = delegate()
				{
					Singleton<Selection>.Instance.SetSelectObjects(null);
					base.UICtrl.ListUICtrl.ClearList();
					Singleton<UndoRedoManager>.Instance.Clear();
					this.Visible = false;
					Singleton<CraftScene>.Instance.WorkingUICtrl.Visible = true;
					Singleton<Housing>.Instance.LoadAsync(this.listPath[this.select], delegate(bool _b)
					{
						base.UICtrl.ListUICtrl.UpdateUI();
						Singleton<CraftScene>.Instance.WorkingUICtrl.Visible = false;
						this.Close();
					});
				};
				ConfirmScene.OnClickedNo = delegate()
				{
				};
				Singleton<Game>.Instance.LoadDialog();
			});
			this.visibleReactive.Subscribe(delegate(bool _b)
			{
				this.canvasGroup.Enable(_b, false);
				if (_b)
				{
					this.buttonClose.ClearState();
					this.infoLoad.Visible = false;
				}
			});
			int housingID = Singleton<CraftScene>.Instance.HousingID;
			Housing.AreaInfo areaInfo = null;
			if (Singleton<Housing>.Instance.dicAreaInfo.TryGetValue(housingID, out areaInfo))
			{
				Housing.AreaSizeInfo areaSizeInfo = null;
				if (Singleton<Housing>.Instance.dicAreaSizeInfo.TryGetValue(areaInfo.size, out areaSizeInfo))
				{
					foreach (KeyValuePair<int, SaveLoadUICtrl.TabInfo> keyValuePair2 in this.dicTabInfo)
					{
						keyValuePair2.Value.Interactable = areaSizeInfo.compatibility.Contains(keyValuePair2.Key);
					}
				}
			}
			this.Visible = false;
		}

		// Token: 0x060039DE RID: 14814 RVA: 0x001540D4 File Offset: 0x001524D4
		public override void UpdateUI()
		{
		}

		// Token: 0x060039DF RID: 14815 RVA: 0x001540D8 File Offset: 0x001524D8
		public void Open()
		{
			this.Visible = true;
			base.UICtrl.ListUICtrl.Visible = false;
			base.UICtrl.AddUICtrl.Active = false;
			this.SelectTab(Singleton<CraftScene>.Instance.HousingID, true);
			Housing.AreaInfo areaInfo = null;
			if (Singleton<Housing>.Instance.dicAreaInfo.TryGetValue(Singleton<CraftScene>.Instance.HousingID, out areaInfo))
			{
				SaveLoadUICtrl.TabInfo tabInfo = null;
				if (this.dicTabInfo.TryGetValue(areaInfo.size, out tabInfo))
				{
					tabInfo.IsOn = true;
				}
				this.SelectTab(areaInfo.size, false);
			}
		}

		// Token: 0x060039E0 RID: 14816 RVA: 0x0015416F File Offset: 0x0015256F
		public void Close()
		{
			this.Visible = false;
			base.UICtrl.ListUICtrl.Visible = true;
		}

		// Token: 0x060039E1 RID: 14817 RVA: 0x0015418C File Offset: 0x0015258C
		private void InitInfo(int _idx)
		{
			string path = UserData.Create(string.Format("{0}{1:00}/", "housing/", _idx + 1));
			List<KeyValuePair<DateTime, string>> list = (from s in Directory.EnumerateFiles(path, "*.png")
			select new SaveLoadUICtrl.FileInfo(s) into fi
			where fi.CraftInfo != null
			where fi.Check(_idx)
			select new KeyValuePair<DateTime, string>(File.GetLastWriteTime(fi.Path), fi.Path)).ToList<KeyValuePair<DateTime, string>>();
			CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
			Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("ja-JP");
			list.Sort((KeyValuePair<DateTime, string> a, KeyValuePair<DateTime, string> b) => b.Key.CompareTo(a.Key));
			Thread.CurrentThread.CurrentCulture = currentCulture;
			this.listPath = (from v in list
			select v.Value).ToList<string>();
			this.thumbnailNum = ((!this.listPath.IsNullOrEmpty<string>()) ? this.listPath.Count : 0);
			this.pageNum = this.thumbnailNum / this.thumbnailLimit + ((this.thumbnailNum % this.thumbnailLimit == 0) ? 0 : 1);
			this.pageNum = Mathf.Max(this.pageNum, 1);
			if (!this.view.IsInit)
			{
				this.view.InitListView(this.pageNum, (LoopListView2 _view, int _index) => this.OnUpdate(_view, _index), null);
			}
			else if (!this.view.SetListItemCount(this.pageNum, true))
			{
				this.view.RefreshAllShownItem();
			}
			foreach (PageButton pageButton in from _v in this.view.ItemList
			select _v as PageButton)
			{
				if (pageButton != null)
				{
					pageButton.Deselect();
				}
			}
		}

		// Token: 0x060039E2 RID: 14818 RVA: 0x00154410 File Offset: 0x00152810
		private void SelectTab(int _idx, bool _force = false)
		{
			if (!_force && this.tab == _idx)
			{
				return;
			}
			this.InitInfo(_idx);
			this.tab = _idx;
			this.SetPage(0, true);
		}

		// Token: 0x060039E3 RID: 14819 RVA: 0x0015443C File Offset: 0x0015283C
		private void SetPage(int _page, bool _force = false)
		{
			if (!_force && this.page == _page)
			{
				return;
			}
			_page = Mathf.Clamp(_page, 0, this.pageNum - 1);
			int num = this.thumbnailLimit * _page;
			for (int i = 0; i < this.thumbnailLimit; i++)
			{
				int num2 = num + i;
				if (!MathfEx.RangeEqualOn<int>(0, num2, this.thumbnailNum - 1))
				{
					if (_page == 0 && i == 0)
					{
						this.rawsThumbnail[i].texture = this.textureNoData;
						this.rawsThumbnail[i].enabled = true;
					}
					else
					{
						this.rawsThumbnail[i].enabled = false;
					}
					this.buttonsThumbnail[i].enabled = false;
				}
				else
				{
					this.rawsThumbnail[i].texture = PngAssist.LoadTexture(this.listPath[num2]);
					this.rawsThumbnail[i].enabled = true;
					this.buttonsThumbnail[i].enabled = true;
					this.buttonsThumbnail[i].interactable = true;
				}
			}
			this.page = _page;
			if (_force)
			{
				PageButton pageButton = this.view.ItemList.FirstOrDefault((LoopListViewItem2 _v) => _v.ItemIndex == _page) as PageButton;
				if (pageButton != null)
				{
					pageButton.Select();
				}
			}
			UnityEngine.Resources.UnloadUnusedAssets();
			GC.Collect();
		}

		// Token: 0x060039E4 RID: 14820 RVA: 0x001545B8 File Offset: 0x001529B8
		private LoopListViewItem2 OnUpdate(LoopListView2 _view, int _index)
		{
			if (_index < 0)
			{
				return null;
			}
			LoopListViewItem2 loopListViewItem = _view.NewListViewItem(this.original.name);
			PageButton pageButton = loopListViewItem as PageButton;
			if (pageButton != null)
			{
				pageButton.SetData(_index, this.page == _index, delegate(int _i)
				{
					this.SetPage(_i, false);
				});
			}
			return loopListViewItem;
		}

		// Token: 0x04003957 RID: 14679
		[SerializeField]
		private CanvasGroup canvasGroup;

		// Token: 0x04003958 RID: 14680
		[SerializeField]
		private ButtonEx buttonClose;

		// Token: 0x04003959 RID: 14681
		[SerializeField]
		private Transform tabRoot;

		// Token: 0x0400395A RID: 14682
		[SerializeField]
		private GameObject tabOriginal;

		// Token: 0x0400395B RID: 14683
		[SerializeField]
		private RawImage[] rawsThumbnail;

		// Token: 0x0400395C RID: 14684
		[SerializeField]
		private Button[] buttonsThumbnail;

		// Token: 0x0400395D RID: 14685
		[SerializeField]
		private Texture textureNoData;

		// Token: 0x0400395E RID: 14686
		[SerializeField]
		[Header("一覧関係")]
		private LoopListView2 view;

		// Token: 0x0400395F RID: 14687
		[SerializeField]
		private GameObject original;

		// Token: 0x04003960 RID: 14688
		[SerializeField]
		[Header("読込情報関係")]
		private SaveLoadUICtrl.Info infoLoad = new SaveLoadUICtrl.Info();

		// Token: 0x04003961 RID: 14689
		private BoolReactiveProperty visibleReactive = new BoolReactiveProperty(false);

		// Token: 0x04003962 RID: 14690
		private List<string> listPath;

		// Token: 0x04003963 RID: 14691
		private int thumbnailNum = -1;

		// Token: 0x04003964 RID: 14692
		private int thumbnailLimit = 16;

		// Token: 0x04003965 RID: 14693
		private int pageNum = -1;

		// Token: 0x04003966 RID: 14694
		private int tab = -1;

		// Token: 0x04003967 RID: 14695
		private int page;

		// Token: 0x04003968 RID: 14696
		private int select = -1;

		// Token: 0x04003969 RID: 14697
		private Dictionary<int, SaveLoadUICtrl.TabInfo> dicTabInfo = new Dictionary<int, SaveLoadUICtrl.TabInfo>();

		// Token: 0x020008AF RID: 2223
		[Serializable]
		private class Info
		{
			// Token: 0x17000A6C RID: 2668
			// (get) Token: 0x060039F8 RID: 14840 RVA: 0x00154867 File Offset: 0x00152C67
			// (set) Token: 0x060039F9 RID: 14841 RVA: 0x0015487E File Offset: 0x00152C7E
			public bool Visible
			{
				get
				{
					return this.canvasGroup.alpha != 0f;
				}
				set
				{
					this.canvasGroup.Enable(value, false);
				}
			}

			// Token: 0x060039FA RID: 14842 RVA: 0x0015488D File Offset: 0x00152C8D
			public void Setup(Texture _texture)
			{
				this.rawThumbnail.texture = _texture;
				this.Visible = true;
				this.buttonClose.ClearState();
				this.buttonDelete.ClearState();
				this.buttonLoad.ClearState();
			}

			// Token: 0x04003973 RID: 14707
			public CanvasGroup canvasGroup;

			// Token: 0x04003974 RID: 14708
			public ButtonEx buttonClose;

			// Token: 0x04003975 RID: 14709
			public RawImage rawThumbnail;

			// Token: 0x04003976 RID: 14710
			public ButtonEx buttonDelete;

			// Token: 0x04003977 RID: 14711
			public ButtonEx buttonLoad;
		}

		// Token: 0x020008B0 RID: 2224
		private class TabInfo
		{
			// Token: 0x17000A6D RID: 2669
			// (set) Token: 0x060039FC RID: 14844 RVA: 0x001548CB File Offset: 0x00152CCB
			public bool Interactable
			{
				set
				{
					this.sizeTab.toggle.interactable = value;
				}
			}

			// Token: 0x17000A6E RID: 2670
			// (set) Token: 0x060039FD RID: 14845 RVA: 0x001548DE File Offset: 0x00152CDE
			public bool IsOn
			{
				set
				{
					this.sizeTab.toggle.isOn = value;
				}
			}

			// Token: 0x04003978 RID: 14712
			public GameObject gameObject;

			// Token: 0x04003979 RID: 14713
			public SizeTab sizeTab;
		}

		// Token: 0x020008B1 RID: 2225
		private class FileInfo
		{
			// Token: 0x060039FE RID: 14846 RVA: 0x001548F1 File Offset: 0x00152CF1
			public FileInfo(string _path)
			{
				this.Path = _path;
				this.CraftInfo = CraftInfo.LoadStatic(_path);
			}

			// Token: 0x17000A6F RID: 2671
			// (get) Token: 0x060039FF RID: 14847 RVA: 0x0015490C File Offset: 0x00152D0C
			// (set) Token: 0x06003A00 RID: 14848 RVA: 0x00154914 File Offset: 0x00152D14
			public string Path { get; private set; }

			// Token: 0x17000A70 RID: 2672
			// (get) Token: 0x06003A01 RID: 14849 RVA: 0x0015491D File Offset: 0x00152D1D
			// (set) Token: 0x06003A02 RID: 14850 RVA: 0x00154925 File Offset: 0x00152D25
			public CraftInfo CraftInfo { get; private set; }

			// Token: 0x06003A03 RID: 14851 RVA: 0x0015492E File Offset: 0x00152D2E
			public bool Check(int _areaType)
			{
				return this.CraftInfo != null && _areaType == Singleton<Housing>.Instance.GetSizeType(this.CraftInfo.AreaNo);
			}
		}
	}
}
