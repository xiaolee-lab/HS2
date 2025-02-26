using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AIProject.ColorDefine;
using AIProject.SaveData;
using Manager;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AIProject.UI.Recycling
{
	// Token: 0x02000EA1 RID: 3745
	public class RecyclingDecidedItemSlotUI : UIBehaviour
	{
		// Token: 0x170017CF RID: 6095
		// (get) Token: 0x06007953 RID: 31059 RVA: 0x00331606 File Offset: 0x0032FA06
		// (set) Token: 0x06007954 RID: 31060 RVA: 0x0033160E File Offset: 0x0032FA0E
		public List<StuffItem> ItemList { get; private set; } = new List<StuffItem>();

		// Token: 0x170017D0 RID: 6096
		// (get) Token: 0x06007955 RID: 31061 RVA: 0x00331617 File Offset: 0x0032FA17
		public Subject<StuffItem> CreateEvent { get; } = new Subject<StuffItem>();

		// Token: 0x170017D1 RID: 6097
		// (get) Token: 0x06007956 RID: 31062 RVA: 0x0033161F File Offset: 0x0032FA1F
		public Subject<StuffItem> DeleteEvent { get; } = new Subject<StuffItem>();

		// Token: 0x170017D2 RID: 6098
		// (get) Token: 0x06007957 RID: 31063 RVA: 0x00331627 File Offset: 0x0032FA27
		public ItemListUI ItemListUI
		{
			[CompilerGenerated]
			get
			{
				return this._itemListUI;
			}
		}

		// Token: 0x170017D3 RID: 6099
		// (get) Token: 0x06007958 RID: 31064 RVA: 0x0033162F File Offset: 0x0032FA2F
		public ItemListController ListController { get; } = new ItemListController(PanelType.DecidedItem);

		// Token: 0x170017D4 RID: 6100
		// (get) Token: 0x06007959 RID: 31065 RVA: 0x00331637 File Offset: 0x0032FA37
		public BoolReactiveProperty IsCreate { get; } = new BoolReactiveProperty(false);

		// Token: 0x170017D5 RID: 6101
		// (get) Token: 0x0600795A RID: 31066 RVA: 0x0033163F File Offset: 0x0032FA3F
		private List<ItemNodeUI> ItemNodeUIList { get; } = new List<ItemNodeUI>();

		// Token: 0x170017D6 RID: 6102
		// (get) Token: 0x0600795B RID: 31067 RVA: 0x00331647 File Offset: 0x0032FA47
		// (set) Token: 0x0600795C RID: 31068 RVA: 0x0033164F File Offset: 0x0032FA4F
		public int SlotMaxNum { get; private set; }

		// Token: 0x170017D7 RID: 6103
		// (get) Token: 0x0600795D RID: 31069 RVA: 0x00331658 File Offset: 0x0032FA58
		// (set) Token: 0x0600795E RID: 31070 RVA: 0x00331660 File Offset: 0x0032FA60
		public float CountLimit { get; private set; }

		// Token: 0x170017D8 RID: 6104
		// (get) Token: 0x0600795F RID: 31071 RVA: 0x00331669 File Offset: 0x0032FA69
		// (set) Token: 0x06007960 RID: 31072 RVA: 0x00331671 File Offset: 0x0032FA71
		public int CraftPointID { get; set; } = -1;

		// Token: 0x170017D9 RID: 6105
		// (get) Token: 0x06007961 RID: 31073 RVA: 0x0033167A File Offset: 0x0032FA7A
		// (set) Token: 0x06007962 RID: 31074 RVA: 0x00331682 File Offset: 0x0032FA82
		public int CreatedItemSlotMaxNum { get; private set; }

		// Token: 0x170017DA RID: 6106
		// (get) Token: 0x06007963 RID: 31075 RVA: 0x0033168B File Offset: 0x0032FA8B
		// (set) Token: 0x06007964 RID: 31076 RVA: 0x00331693 File Offset: 0x0032FA93
		public int NeedNumber { get; private set; }

		// Token: 0x170017DB RID: 6107
		// (get) Token: 0x06007965 RID: 31077 RVA: 0x0033169C File Offset: 0x0032FA9C
		public bool IsCreateable
		{
			get
			{
				RecyclingData recyclingData = this._recyclingUI.RecyclingData;
				return recyclingData != null && recyclingData.CreatedItemList.Count < this.CreatedItemSlotMaxNum && this.NeedNumber <= this.ItemCount();
			}
		}

		// Token: 0x06007966 RID: 31078 RVA: 0x003316E7 File Offset: 0x0032FAE7
		protected override void Awake()
		{
			base.Awake();
			if (this._recyclingUI == null)
			{
				this._recyclingUI = base.GetComponentInParent<RecyclingUI>();
			}
			this.ListController.Bind(this._itemListUI);
		}

		// Token: 0x06007967 RID: 31079 RVA: 0x00331720 File Offset: 0x0032FB20
		protected override void Start()
		{
			base.Start();
			ItemListController listController = this.ListController;
			listController.RefreshEvent = (Action)Delegate.Combine(listController.RefreshEvent, new Action(delegate()
			{
				this.RefreshUI();
			}));
			if (this._updateDisposable != null)
			{
				this._updateDisposable.Clear();
			}
			this._updateDisposable = new CompositeDisposable();
			if (Singleton<Manager.Resources>.IsInstance())
			{
				DefinePack.RecyclingSetting recycling = Singleton<Manager.Resources>.Instance.DefinePack.Recycling;
				this.CountLimit = recycling.ItemCreateTime;
				this.SlotMaxNum = recycling.DecidedItemCapacity;
				this.CreatedItemSlotMaxNum = recycling.CreateItemCapacity;
				this.NeedNumber = recycling.NeedNumber;
			}
			else
			{
				this.CountLimit = 0f;
				this.SlotMaxNum = 0;
				this.CreatedItemSlotMaxNum = 0;
				this.NeedNumber = 0;
			}
			Observable.EveryUpdate().TakeUntilDestroy(base.gameObject).Subscribe(delegate(long _)
			{
				this.OnRecyclingDataUpdate();
			}).AddTo(this._updateDisposable);
			IObservable<Unit> source = this._createButton.OnClickAsObservable();
			IObservable<Unit> source2 = this._cancelButton.OnClickAsObservable();
			Observable.Merge<Unit>(new IObservable<Unit>[]
			{
				source.Do(delegate(Unit _)
				{
					this.OnStartClick();
				}),
				source2.Do(delegate(Unit _)
				{
					this.OnCancelClick();
				})
			}).Subscribe(delegate(Unit _)
			{
				this._recyclingUI.PlaySE(SoundPack.SystemSE.OK_S);
			}).AddTo(this);
			RecyclingCreateItemStockUI createItemStockUI = this._recyclingUI.CreateItemStockUI;
			createItemStockUI.ChangeCreateableEvent = (Action<bool>)Delegate.Combine(createItemStockUI.ChangeCreateableEvent, new Action<bool>(delegate(bool flag)
			{
				this._createButton.interactable = flag;
			}));
			this.IsCreate.Subscribe(delegate(bool flag)
			{
				this.SetActive(this._cancelButton, flag);
				this.SetActive(this._createButton, !flag);
				this.SetActive(this._playNowText, flag);
			}).AddTo(this._updateDisposable);
		}

		// Token: 0x06007968 RID: 31080 RVA: 0x003318CD File Offset: 0x0032FCCD
		protected override void OnDestroy()
		{
			if (this._updateDisposable != null)
			{
				this._updateDisposable.Clear();
			}
			base.OnDestroy();
		}

		// Token: 0x06007969 RID: 31081 RVA: 0x003318F0 File Offset: 0x0032FCF0
		public void SettingUI(List<StuffItem> itemList)
		{
			this.ItemList = itemList;
			this.ListController.SetItemList(itemList);
			this.ListController.Create(itemList);
			this._maxCountText.text = string.Format("{0}", this.SlotMaxNum);
			RecyclingData recyclingData = this._recyclingUI.RecyclingData;
			if (recyclingData != null)
			{
				this.IsCreate.Value = recyclingData.CreateCountEnabled;
			}
		}

		// Token: 0x0600796A RID: 31082 RVA: 0x00331960 File Offset: 0x0032FD60
		public void RefreshCountText()
		{
			int num = this.ItemCount();
			this._countText.text = num.ToString();
			Colors colors = (this.SlotMaxNum > num) ? Colors.White : Colors.Red;
			this._countText.color = Define.Get(colors);
		}

		// Token: 0x0600796B RID: 31083 RVA: 0x003319B4 File Offset: 0x0032FDB4
		public void RefreshButtonUI()
		{
			RecyclingData recyclingData = this._recyclingUI.RecyclingData;
			if (recyclingData != null)
			{
				bool createCountEnabled = recyclingData.CreateCountEnabled;
				this.SetActive(this._createButton, !createCountEnabled);
				this.SetActive(this._cancelButton, createCountEnabled);
				if (!createCountEnabled)
				{
					recyclingData.CreateCounter = 0f;
				}
				this._createButton.interactable = this.IsCreateable;
			}
		}

		// Token: 0x0600796C RID: 31084 RVA: 0x00331A1C File Offset: 0x0032FE1C
		public void RefreshCreatePanelUI()
		{
			RecyclingData recyclingData = this._recyclingUI.RecyclingData;
			if (recyclingData != null)
			{
				if (recyclingData.CreateCountEnabled)
				{
					recyclingData.CreateCountEnabled = this.CheckCreateable(recyclingData);
				}
				if (!recyclingData.CreateCountEnabled)
				{
					recyclingData.CreateCounter = 0f;
				}
				bool flag = this.NeedNumber <= this.ItemCount(recyclingData);
				if (flag)
				{
					this._createPanelUI.DoOpen();
				}
				else
				{
					this._createPanelUI.DoClose();
				}
				this.IsCreate.Value = recyclingData.CreateCountEnabled;
			}
			else
			{
				this._createPanelUI.DoForceClose();
				this.IsCreate.Value = false;
			}
		}

		// Token: 0x0600796D RID: 31085 RVA: 0x00331ACA File Offset: 0x0032FECA
		public void RefreshUI()
		{
			this._itemListUI.Refresh();
			this.RefreshCountText();
			this.RefreshCreatePanelUI();
			this.RefreshButtonUI();
		}

		// Token: 0x0600796E RID: 31086 RVA: 0x00331AEC File Offset: 0x0032FEEC
		public int ItemCount()
		{
			if (this.ItemList.IsNullOrEmpty<StuffItem>())
			{
				return 0;
			}
			int num = 0;
			foreach (StuffItem stuffItem in this.ItemList)
			{
				if (stuffItem != null)
				{
					num += stuffItem.Count;
				}
			}
			return num;
		}

		// Token: 0x0600796F RID: 31087 RVA: 0x00331B68 File Offset: 0x0032FF68
		public int ItemCount(RecyclingData data)
		{
			if (data == null || data.DecidedItemList.IsNullOrEmpty<StuffItem>())
			{
				return 0;
			}
			int num = 0;
			foreach (StuffItem stuffItem in data.DecidedItemList)
			{
				if (stuffItem != null)
				{
					num += stuffItem.Count;
				}
			}
			return num;
		}

		// Token: 0x06007970 RID: 31088 RVA: 0x00331BE8 File Offset: 0x0032FFE8
		public int FreeCount()
		{
			int num = 0;
			this.ItemNodeUIList.RemoveAll((ItemNodeUI x) => x == null);
			foreach (ItemNodeUI itemNodeUI in this.ItemNodeUIList)
			{
				if (!(itemNodeUI == null) && itemNodeUI.Item != null)
				{
					num += itemNodeUI.Item.Count;
				}
			}
			return Mathf.Max(0, this.SlotMaxNum - num);
		}

		// Token: 0x06007971 RID: 31089 RVA: 0x00331CA0 File Offset: 0x003300A0
		private void OnStartClick()
		{
			RecyclingData recyclingData = this._recyclingUI.RecyclingData;
			if (recyclingData == null)
			{
				return;
			}
			if (this.ItemCount(recyclingData) < this.NeedNumber)
			{
				return;
			}
			if (!this._recyclingUI.CreateItemStockUI.IsCreateable)
			{
				return;
			}
			this.IsCreate.Value = true;
			recyclingData.CreateCountEnabled = true;
			recyclingData.CreateCounter = 0f;
		}

		// Token: 0x06007972 RID: 31090 RVA: 0x00331D08 File Offset: 0x00330108
		private void OnCancelClick()
		{
			RecyclingData recyclingData = this._recyclingUI.RecyclingData;
			if (recyclingData == null)
			{
				return;
			}
			this.IsCreate.Value = false;
			recyclingData.CreateCountEnabled = false;
			recyclingData.CreateCounter = 0f;
		}

		// Token: 0x06007973 RID: 31091 RVA: 0x00331D48 File Offset: 0x00330148
		private void OnRecyclingDataUpdate()
		{
			if (!Singleton<Game>.IsInstance())
			{
				return;
			}
			AIProject.SaveData.Environment environment = Singleton<Game>.Instance.Environment;
			Dictionary<int, RecyclingData> dictionary = (environment == null) ? null : environment.RecyclingDataTable;
			if (dictionary.IsNullOrEmpty<int, RecyclingData>())
			{
				return;
			}
			foreach (KeyValuePair<int, RecyclingData> keyValuePair in dictionary)
			{
				RecyclingData value = keyValuePair.Value;
				if (value != null)
				{
					if (!value.CreateCountEnabled)
					{
						value.CreateCounter = 0f;
					}
					else if (this.CountLimit <= value.CreateCounter)
					{
						this.CreateItem(keyValuePair.Key, value);
					}
				}
			}
		}

		// Token: 0x06007974 RID: 31092 RVA: 0x00331E20 File Offset: 0x00330220
		private void RemoveItem(int pointID, RecyclingData data)
		{
			if (data == null || data.DecidedItemList.IsNullOrEmpty<StuffItem>())
			{
				return;
			}
			if (pointID == this._recyclingUI.CraftPointID)
			{
				this.RemoveItem();
			}
			else
			{
				int num = this.NeedNumber;
				for (int i = 0; i < data.DecidedItemList.Count; i++)
				{
					StuffItem element = data.DecidedItemList.GetElement(i);
					if (element == null)
					{
						data.DecidedItemList.RemoveAt(i);
						i--;
					}
					else
					{
						int num2 = Mathf.Min(element.Count, num);
						num -= num2;
						element.Count -= num2;
						if (element.Count <= 0)
						{
							data.DecidedItemList.RemoveAt(i);
							i--;
						}
						if (num <= 0)
						{
							break;
						}
					}
				}
			}
		}

		// Token: 0x06007975 RID: 31093 RVA: 0x00331EF4 File Offset: 0x003302F4
		private void RemoveItem()
		{
			if (this.ItemList.IsNullOrEmpty<StuffItem>() && this.ItemCount() < this.NeedNumber)
			{
				return;
			}
			int num = this.NeedNumber;
			for (int i = 0; i < this.ItemList.Count; i++)
			{
				StuffItem element = this.ItemList.GetElement(i);
				if (element == null)
				{
					this.ItemList.RemoveAt(i);
					i--;
				}
				else
				{
					int num2 = Mathf.Min(num, element.Count);
					num -= num2;
					element.Count -= num2;
					if (element.Count <= 0)
					{
						this.ItemList.Remove(element);
						i--;
						int num3 = this._itemListUI.SearchIndex(element);
						if (num3 != -1)
						{
							this._itemListUI.RemoveItemNode(num3);
						}
					}
					this.DeleteEvent.OnNext(element);
					if (num <= 0)
					{
						break;
					}
				}
			}
			Action refreshEvent = this.ListController.RefreshEvent;
			if (refreshEvent != null)
			{
				refreshEvent();
			}
		}

		// Token: 0x06007976 RID: 31094 RVA: 0x00332000 File Offset: 0x00330400
		private void CreateItem(int pointID, RecyclingData data)
		{
			if (data == null)
			{
				return;
			}
			data.CreateCounter = 0f;
			bool flag = this.CheckCreateable(data);
			if (flag)
			{
				this.RemoveItem(pointID, data);
				StuffItem randomCreate = this.GetRandomCreate();
				if (randomCreate != null)
				{
					if (this._recyclingUI.CraftPointID == pointID)
					{
						this.CreateEvent.OnNext(randomCreate);
					}
					else
					{
						data.CreatedItemList.AddItem(randomCreate);
					}
				}
			}
			if (data.CreateCountEnabled)
			{
				data.CreateCountEnabled = this.CheckCreateable(data);
			}
			if (this._recyclingUI.CraftPointID == pointID)
			{
				this.IsCreate.Value = data.CreateCountEnabled;
			}
		}

		// Token: 0x06007977 RID: 31095 RVA: 0x003320AB File Offset: 0x003304AB
		public bool CheckCreateable(RecyclingData data)
		{
			return data != null && data.CreatedItemList.Count < this.CreatedItemSlotMaxNum && this.NeedNumber <= this.ItemCount(data);
		}

		// Token: 0x06007978 RID: 31096 RVA: 0x003320E0 File Offset: 0x003304E0
		private StuffItem GetRandomCreate()
		{
			if (!Singleton<Manager.Resources>.IsInstance())
			{
				return null;
			}
			Manager.Resources instance = Singleton<Manager.Resources>.Instance;
			Manager.Resources.GameInfoTables gameInfo = instance.GameInfo;
			List<RecyclingItemInfo> list = (gameInfo != null) ? gameInfo.RecyclingCreateableItemList : null;
			if (list.IsNullOrEmpty<RecyclingItemInfo>())
			{
				return null;
			}
			int index = UnityEngine.Random.Range(0, list.Count);
			RecyclingItemInfo element = list.GetElement(index);
			return (instance.GameInfo.GetItem(element.CategoryID, element.ItemID) != null) ? new StuffItem(element.CategoryID, element.ItemID, 1) : null;
		}

		// Token: 0x06007979 RID: 31097 RVA: 0x00332173 File Offset: 0x00330573
		private void SetActive(GameObject obj, bool flag)
		{
			if (obj != null && obj.activeSelf != flag)
			{
				obj.SetActive(flag);
			}
		}

		// Token: 0x0600797A RID: 31098 RVA: 0x00332194 File Offset: 0x00330594
		private void SetActive(Component com, bool flag)
		{
			if (com != null && com.gameObject != null && com.gameObject.activeSelf != flag)
			{
				com.gameObject.SetActive(flag);
			}
		}

		// Token: 0x040061DE RID: 25054
		[SerializeField]
		private RecyclingUI _recyclingUI;

		// Token: 0x040061DF RID: 25055
		[SerializeField]
		private RecyclingCreatePanelUI _createPanelUI;

		// Token: 0x040061E3 RID: 25059
		[SerializeField]
		private Text _countText;

		// Token: 0x040061E4 RID: 25060
		[SerializeField]
		private Text _maxCountText;

		// Token: 0x040061E5 RID: 25061
		[SerializeField]
		private Text _playNowText;

		// Token: 0x040061E6 RID: 25062
		[SerializeField]
		private Image _countBarImage;

		// Token: 0x040061E7 RID: 25063
		[SerializeField]
		private Button _createButton;

		// Token: 0x040061E8 RID: 25064
		[SerializeField]
		private Button _cancelButton;

		// Token: 0x040061E9 RID: 25065
		[SerializeField]
		private ItemListUI _itemListUI;

		// Token: 0x040061F2 RID: 25074
		private CompositeDisposable _updateDisposable;
	}
}
