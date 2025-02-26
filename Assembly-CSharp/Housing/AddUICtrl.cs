using System;
using System.Collections.Generic;
using System.Linq;
using AIProject;
using AIProject.SaveData;
using AIProject.Scene;
using Housing.Add;
using Housing.Command;
using Illusion.Extensions;
using Manager;
using SuperScrollView;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Housing
{
	// Token: 0x020008A5 RID: 2213
	[Serializable]
	public class AddUICtrl : UIDerived
	{
		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x0600396F RID: 14703 RVA: 0x00152392 File Offset: 0x00150792
		// (set) Token: 0x06003970 RID: 14704 RVA: 0x0015239F File Offset: 0x0015079F
		public bool Active
		{
			get
			{
				return this.activeReactive.Value;
			}
			set
			{
				this.activeReactive.Value = value;
			}
		}

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x06003971 RID: 14705 RVA: 0x001523AD File Offset: 0x001507AD
		// (set) Token: 0x06003972 RID: 14706 RVA: 0x001523BA File Offset: 0x001507BA
		private int Category
		{
			get
			{
				return this.categoryReactive.Value;
			}
			set
			{
				this.categoryReactive.Value = value;
			}
		}

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x06003973 RID: 14707 RVA: 0x001523C8 File Offset: 0x001507C8
		// (set) Token: 0x06003974 RID: 14708 RVA: 0x001523D5 File Offset: 0x001507D5
		private int Select
		{
			get
			{
				return this.selectReactive.Value;
			}
			set
			{
				this.selectReactive.Value = value;
			}
		}

		// Token: 0x17000A59 RID: 2649
		// (set) Token: 0x06003975 RID: 14709 RVA: 0x001523E3 File Offset: 0x001507E3
		private bool ButtonAddEnable
		{
			set
			{
				this.buttonAdd.gameObject.SetActiveIfDifferent(value);
			}
		}

		// Token: 0x17000A5A RID: 2650
		// (set) Token: 0x06003976 RID: 14710 RVA: 0x001523F7 File Offset: 0x001507F7
		private bool Visible
		{
			set
			{
				this.canvasGroup.Enable(value, false);
			}
		}

		// Token: 0x06003977 RID: 14711 RVA: 0x00152408 File Offset: 0x00150808
		public override void Init(UICtrl _uiCtrl, bool _tutorial)
		{
			base.Init(_uiCtrl, _tutorial);
			foreach (KeyValuePair<int, Housing.CategoryInfo> keyValuePair in Singleton<Housing>.Instance.dicCategoryInfo)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.objCategory, this.togglesRoot);
				gameObject.SetActive(true);
				TabUI tab = gameObject.GetComponent<TabUI>();
				RawImage rawImage = tab.rawImage;
				rawImage.texture = keyValuePair.Value.Thumbnail;
				Toggle toggleSelect = tab.toggleSelect;
				toggleSelect.isOn = false;
				int c = keyValuePair.Key;
				(from _b in toggleSelect.OnValueChangedAsObservable()
				where _b
				select _b).Subscribe(delegate(bool _)
				{
					this.Category = c;
				});
				toggleSelect.OnPointerEnterAsObservable().TakeUntilDestroy(tab.imageCursor).Subscribe(delegate(PointerEventData _)
				{
					tab.imageCursor.enabled = true;
				}).AddTo(gameObject);
				toggleSelect.OnPointerExitAsObservable().TakeUntilDestroy(tab.imageCursor).Subscribe(delegate(PointerEventData _)
				{
					tab.imageCursor.enabled = false;
				}).AddTo(gameObject);
			}
			this.buttonClose.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.Active = false;
			});
			this.buttonAdd.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				AddUICtrl.FileInfo fileInfo = this.fileInfos.SafeGet(this.Select);
				if (fileInfo != null)
				{
					Singleton<UndoRedoManager>.Instance.Do(new AddItemCommand(fileInfo.no));
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_L);
				}
				this.Active = false;
			});
			this.buttonActivate.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				ConfirmScene.Sentence = "作成しますか？";
				ConfirmScene.OnClickedYes = delegate()
				{
					AddUICtrl.FileInfo fileInfo = this.fileInfos.SafeGet(this.Select);
					if (fileInfo == null)
					{
						return;
					}
					fileInfo.SetUnlock();
					this.Visible = true;
					this.selectReactive.SetValueAndForceNotify(this.Select);
					this.view.RefreshAllShownItem();
					Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Craft);
				};
				ConfirmScene.OnClickedNo = delegate()
				{
					this.Visible = true;
				};
				Singleton<Game>.Instance.LoadDialog();
				this.Visible = false;
			});
			this.activeReactive.Subscribe(delegate(bool _b)
			{
				this.Visible = _b;
				if (_b)
				{
					this.isForceUpdate = true;
					this.selectReactive.SetValueAndForceNotify(-1);
					this.buttonClose.ClearState();
				}
			});
			this.categoryReactive.Subscribe(delegate(int _i)
			{
				this.CreateList(_i);
				Housing.CategoryInfo categoryInfo = null;
				if (Singleton<Housing>.Instance.dicCategoryInfo.TryGetValue(_i, out categoryInfo))
				{
					this.textCategory.text = categoryInfo.name;
					this.imageCategory.texture = categoryInfo.Thumbnail;
				}
			});
			this.selectReactive.Subscribe(delegate(int _i)
			{
				AddUICtrl.FileInfo fileInfo = this.fileInfos.SafeGet(_i);
				if (fileInfo != null)
				{
					if (fileInfo.Unlock)
					{
						this.cgCraft.Enable(false, false);
						bool flag = !Singleton<Housing>.Instance.CheckLimitNum(fileInfo.no);
						this.ButtonAddEnable = !flag;
						this.textItemLimit.enabled = flag;
					}
					else
					{
						this.cgCraft.Enable(true, false);
						this.buttonActivate.interactable = this.materialUI.UpdateUI(fileInfo.loadInfo);
						this.ButtonAddEnable = false;
						this.textItemLimit.enabled = false;
					}
					this.textName.text = fileInfo.loadInfo.name;
					this.textText.text = fileInfo.loadInfo.text;
					this.imagesInfo[0].enabled = fileInfo.loadInfo.isAccess;
					this.imagesInfo[1].enabled = fileInfo.loadInfo.isAction;
					this.imagesInfo[2].enabled = fileInfo.loadInfo.isHPoint;
					this.cgInfo.Enable(true, false);
				}
				else
				{
					this.cgInfo.Enable(false, false);
					this.cgCraft.Enable(false, false);
					this.ButtonAddEnable = false;
					this.textItemLimit.enabled = false;
					if (this.isForceUpdate)
					{
						this.view.RefreshAllShownItem();
						this.isForceUpdate = false;
					}
				}
			});
			this.categoryReactive.SetValueAndForceNotify(-1);
		}

		// Token: 0x06003978 RID: 14712 RVA: 0x00152630 File Offset: 0x00150A30
		public override void UpdateUI()
		{
		}

		// Token: 0x06003979 RID: 14713 RVA: 0x00152632 File Offset: 0x00150A32
		public void Reselect()
		{
			this.selectReactive.SetValueAndForceNotify(this.selectReactive.Value);
		}

		// Token: 0x0600397A RID: 14714 RVA: 0x0015264C File Offset: 0x00150A4C
		private void CreateList(int _category)
		{
			int filter = (_category < 0) ? -1 : (1 << _category);
			this.fileInfos = (from v in Singleton<Housing>.Instance.dicLoadInfo
			where (v.Value.Category & filter) > 0
			where Singleton<Housing>.Instance.CheckSize(v.Value.size)
			select new AddUICtrl.FileInfo
			{
				no = v.Key,
				loadInfo = v.Value
			}).ToArray<AddUICtrl.FileInfo>();
			this.selectReactive.SetValueAndForceNotify(-1);
			int num = (!this.fileInfos.IsNullOrEmpty<AddUICtrl.FileInfo>()) ? (this.fileInfos.Length / this.countPerRow) : 0;
			if (!this.fileInfos.IsNullOrEmpty<AddUICtrl.FileInfo>() && this.fileInfos.Length % this.countPerRow > 0)
			{
				num++;
			}
			if (!this.view.IsInit)
			{
				this.view.InitListView(num, (LoopListView2 _view, int _index) => this.OnUpdate(_view, _index), null);
			}
			else if (!this.view.SetListItemCount(num, true))
			{
				this.view.RefreshAllShownItem();
			}
		}

		// Token: 0x0600397B RID: 14715 RVA: 0x00152790 File Offset: 0x00150B90
		private LoopListViewItem2 OnUpdate(LoopListView2 _view, int _index)
		{
			if (_index < 0)
			{
				return null;
			}
			LoopListViewItem2 loopListViewItem = _view.NewListViewItem(this.original.name);
			ItemRow itemRow = loopListViewItem as ItemRow;
			if (itemRow)
			{
				for (int i = 0; i < this.countPerRow; i++)
				{
					int index = _index * this.countPerRow + i;
					itemRow.SetData(i, this.fileInfos.SafeGet(index), delegate
					{
						this.Select = index;
					}, this.Select == index);
				}
			}
			return loopListViewItem;
		}

		// Token: 0x04003917 RID: 14615
		[SerializeField]
		private CanvasGroup canvasGroup;

		// Token: 0x04003918 RID: 14616
		[SerializeField]
		[Header("一覧関係")]
		private LoopListView2 view;

		// Token: 0x04003919 RID: 14617
		[SerializeField]
		private GameObject original;

		// Token: 0x0400391A RID: 14618
		[SerializeField]
		private int countPerRow = 6;

		// Token: 0x0400391B RID: 14619
		[SerializeField]
		[Header("フィルター関係")]
		private Transform togglesRoot;

		// Token: 0x0400391C RID: 14620
		[SerializeField]
		private GameObject objCategory;

		// Token: 0x0400391D RID: 14621
		[SerializeField]
		private RawImage imageCategory;

		// Token: 0x0400391E RID: 14622
		[SerializeField]
		private Text textCategory;

		// Token: 0x0400391F RID: 14623
		[SerializeField]
		[Header("動作関係")]
		private Button buttonAdd;

		// Token: 0x04003920 RID: 14624
		[SerializeField]
		private ButtonEx buttonClose;

		// Token: 0x04003921 RID: 14625
		[SerializeField]
		private Text textItemLimit;

		// Token: 0x04003922 RID: 14626
		[SerializeField]
		[Header("情報関係")]
		private CanvasGroup cgInfo;

		// Token: 0x04003923 RID: 14627
		[SerializeField]
		private Text textName;

		// Token: 0x04003924 RID: 14628
		[SerializeField]
		private Text textText;

		// Token: 0x04003925 RID: 14629
		[SerializeField]
		private Image[] imagesInfo;

		// Token: 0x04003926 RID: 14630
		[SerializeField]
		[Header("ロック関係")]
		private CanvasGroup cgCraft;

		// Token: 0x04003927 RID: 14631
		[SerializeField]
		private MaterialUI materialUI;

		// Token: 0x04003928 RID: 14632
		[SerializeField]
		private Button buttonActivate;

		// Token: 0x04003929 RID: 14633
		private AddUICtrl.FileInfo[] fileInfos;

		// Token: 0x0400392A RID: 14634
		private bool isForceUpdate;

		// Token: 0x0400392B RID: 14635
		private BoolReactiveProperty activeReactive = new BoolReactiveProperty(false);

		// Token: 0x0400392C RID: 14636
		private IntReactiveProperty categoryReactive = new IntReactiveProperty(-1);

		// Token: 0x0400392D RID: 14637
		private IntReactiveProperty selectReactive = new IntReactiveProperty(-1);

		// Token: 0x020008A6 RID: 2214
		public class FileInfo
		{
			// Token: 0x17000A5B RID: 2651
			// (get) Token: 0x06003988 RID: 14728 RVA: 0x00152B84 File Offset: 0x00150F84
			public bool Unlock
			{
				get
				{
					Dictionary<int, Dictionary<int, bool>> unlock = Singleton<Game>.Instance.WorldData.HousingData.Unlock;
					if (!unlock.ContainsKey(this.loadInfo.category))
					{
						return true;
					}
					bool flag = false;
					return !unlock[this.loadInfo.category].TryGetValue(this.no, out flag) || flag;
				}
			}

			// Token: 0x06003989 RID: 14729 RVA: 0x00152BEC File Offset: 0x00150FEC
			public void SetUnlock()
			{
				foreach (Housing.RequiredMaterial requiredMaterial in this.loadInfo.requiredMaterials)
				{
					StuffItem item = new StuffItem(requiredMaterial.category, requiredMaterial.no, requiredMaterial.num);
					StuffItem.RemoveStorages(item, new List<StuffItem>[]
					{
						Singleton<Map>.Instance.Player.PlayerData.ItemList,
						Singleton<Game>.Instance.Environment.ItemListInStorage
					});
				}
				Singleton<Game>.Instance.WorldData.HousingData.Unlock[this.loadInfo.category][this.no] = true;
			}

			// Token: 0x04003931 RID: 14641
			public int no;

			// Token: 0x04003932 RID: 14642
			public Housing.LoadInfo loadInfo;
		}
	}
}
