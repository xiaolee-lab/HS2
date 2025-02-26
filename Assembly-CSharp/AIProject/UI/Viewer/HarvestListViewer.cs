using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using AIProject.SaveData;
using AIProject.Scene;
using Manager;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;
using UnityEx;

namespace AIProject.UI.Viewer
{
	// Token: 0x02000E76 RID: 3702
	public class HarvestListViewer : MonoBehaviour
	{
		// Token: 0x140000C2 RID: 194
		// (add) Token: 0x060075E0 RID: 30176 RVA: 0x0031FF40 File Offset: 0x0031E340
		// (remove) Token: 0x060075E1 RID: 30177 RVA: 0x0031FF78 File Offset: 0x0031E378
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<IReadOnlyCollection<StuffItem>> GetItemAction;

		// Token: 0x140000C3 RID: 195
		// (add) Token: 0x060075E2 RID: 30178 RVA: 0x0031FFB0 File Offset: 0x0031E3B0
		// (remove) Token: 0x060075E3 RID: 30179 RVA: 0x0031FFE8 File Offset: 0x0031E3E8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action AllGetListAction;

		// Token: 0x140000C4 RID: 196
		// (add) Token: 0x060075E4 RID: 30180 RVA: 0x00320020 File Offset: 0x0031E420
		// (remove) Token: 0x060075E5 RID: 30181 RVA: 0x00320058 File Offset: 0x0031E458
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<IReadOnlyCollection<StuffItem>> SyncListAction;

		// Token: 0x170016E6 RID: 5862
		// (get) Token: 0x060075E6 RID: 30182 RVA: 0x0032008E File Offset: 0x0031E48E
		public ICollection<StuffItem> AddFailedList { get; } = new List<StuffItem>();

		// Token: 0x170016E7 RID: 5863
		// (get) Token: 0x060075E7 RID: 30183 RVA: 0x00320096 File Offset: 0x0031E496
		// (set) Token: 0x060075E8 RID: 30184 RVA: 0x0032009E File Offset: 0x0031E49E
		public bool initialized { get; private set; }

		// Token: 0x060075E9 RID: 30185 RVA: 0x003200A7 File Offset: 0x0031E4A7
		public void Refresh()
		{
			this._itemListUI.ForceSetNonSelect();
		}

		// Token: 0x060075EA RID: 30186 RVA: 0x003200B4 File Offset: 0x0031E4B4
		public void AddList(StuffItem item)
		{
			this.harvestList.AddItem(item);
			this._itemListUI.AddItemNode(this._itemListUI.SearchNotUsedIndex, item);
		}

		// Token: 0x060075EB RID: 30187 RVA: 0x003200DB File Offset: 0x0031E4DB
		public void ClearList()
		{
			this.harvestList.Clear();
			this._itemListUI.ClearItems();
		}

		// Token: 0x060075EC RID: 30188 RVA: 0x003200F4 File Offset: 0x0031E4F4
		public bool InStorageCheck(StuffItem item)
		{
			IReadOnlyCollection<StuffItem> storage = this.storage;
			int capacity = this.capacity;
			int? num = (item != null) ? new int?(item.Count) : null;
			int num2;
			bool result = storage.CanAddItem(capacity, item, (num == null) ? 0 : num.Value, out num2);
			if (item != null)
			{
				this.storage.CanAddItem(this.capacity, null, 0, out num2);
			}
			this.inStorage.Value = (num2 > 0);
			return result;
		}

		// Token: 0x170016E8 RID: 5864
		// (get) Token: 0x060075ED RID: 30189 RVA: 0x00320178 File Offset: 0x0031E578
		public ItemListUI itemListUI
		{
			[CompilerGenerated]
			get
			{
				return this._itemListUI;
			}
		}

		// Token: 0x170016E9 RID: 5865
		// (get) Token: 0x060075EE RID: 30190 RVA: 0x00320180 File Offset: 0x0031E580
		private ReactiveCollection<StuffItem> harvestList { get; } = new ReactiveCollection<StuffItem>();

		// Token: 0x170016EA RID: 5866
		// (get) Token: 0x060075EF RID: 30191 RVA: 0x00320188 File Offset: 0x0031E588
		private int capacity
		{
			[CompilerGenerated]
			get
			{
				return Singleton<Map>.Instance.Player.PlayerData.InventorySlotMax;
			}
		}

		// Token: 0x170016EB RID: 5867
		// (get) Token: 0x060075F0 RID: 30192 RVA: 0x0032019E File Offset: 0x0031E59E
		private List<StuffItem> storage
		{
			[CompilerGenerated]
			get
			{
				return Singleton<Map>.Instance.Player.PlayerData.ItemList;
			}
		}

		// Token: 0x170016EC RID: 5868
		// (get) Token: 0x060075F1 RID: 30193 RVA: 0x003201B4 File Offset: 0x0031E5B4
		private BoolReactiveProperty inStorage { get; } = new BoolReactiveProperty();

		// Token: 0x170016ED RID: 5869
		// (get) Token: 0x060075F2 RID: 30194 RVA: 0x003201BC File Offset: 0x0031E5BC
		private CompositeDisposable disposables { get; } = new CompositeDisposable();

		// Token: 0x060075F3 RID: 30195 RVA: 0x003201C4 File Offset: 0x0031E5C4
		private IEnumerator Start()
		{
			while (!Singleton<Manager.Resources>.IsInstance())
			{
				yield return null;
			}
			while (!Singleton<Game>.IsInstance() || Singleton<Game>.Instance.Environment == null)
			{
				yield return null;
			}
			if (!this._itemListUI.isOptionNode)
			{
				string bundle = Singleton<Manager.Resources>.Instance.DefinePack.ABPaths.MapScenePrefab;
				GameObject gameObject = CommonLib.LoadAsset<GameObject>(bundle, "ItemOption_new", false, string.Empty);
				if (gameObject == null)
				{
					yield break;
				}
				if (!MapScene.AssetBundlePaths.Exists((UnityEx.ValueTuple<string, string> x) => x.Item1 == bundle))
				{
					MapScene.AssetBundlePaths.Add(new UnityEx.ValueTuple<string, string>(bundle, string.Empty));
				}
				this._itemListUI.SetOptionNode(gameObject);
			}
			while (!Singleton<Map>.IsInstance() || Singleton<Map>.Instance.Player == null || Singleton<Map>.Instance.Player.PlayerData == null)
			{
				yield return null;
			}
			this._itemListUI.CurrentChanged += delegate(int _, ItemNodeUI option)
			{
				bool flag = option != null;
				StuffItem stuffItem = flag ? option.Item : null;
				flag &= this.InStorageCheck(stuffItem);
				if (stuffItem != null)
				{
					flag |= (Singleton<Manager.Resources>.Instance.GameInfo.GetItem(stuffItem.CategoryID, stuffItem.ID) == null);
				}
				this._getButton.interactable = flag;
			};
			this.inStorage.Subscribe(delegate(bool success)
			{
				this._infoText.enabled = !success;
			}).AddTo(this.disposables);
			this._allGetButton.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
				if (this.GetItemAction != null)
				{
					this.GetItemAction(this.harvestList);
				}
				foreach (StuffItem item in this.harvestList.ToList<StuffItem>())
				{
					if (!this.AddFailedList.Contains(item))
					{
						this.harvestList.Remove(item);
						this._itemListUI.RemoveItemNode(this._itemListUI.SearchIndex(item));
					}
				}
				this._itemListUI.Refresh();
				if (this._itemListUI.SearchIndex(this._itemListUI.CurrentOption) == -1)
				{
					this._itemListUI.ForceSetNonSelect();
				}
				this.AddFailedList.Clear();
				if (this.SyncListAction != null)
				{
					this.SyncListAction(this.harvestList);
				}
			}).AddTo(this.disposables);
			(from _ in this._getButton.OnClickAsObservable()
			where this._itemListUI.CurrentOption != null
			select _).Subscribe(delegate(Unit _)
			{
				Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.OK_S);
				StuffItem item = this._itemListUI.CurrentOption.Item;
				if (this.GetItemAction != null)
				{
					this.GetItemAction(new StuffItem[]
					{
						item
					});
				}
				if (!this.AddFailedList.Contains(item))
				{
					this.harvestList.Remove(item);
					this._itemListUI.RemoveItemNode(this._itemListUI.CurrentID);
					this._itemListUI.ForceSetNonSelect();
				}
				this.AddFailedList.Clear();
				if (this.SyncListAction != null)
				{
					this.SyncListAction(this.harvestList);
				}
			}).AddTo(this.disposables);
			(from _ in this.harvestList.ObserveRemove()
			where !this.harvestList.Any<StuffItem>()
			select _).Subscribe(delegate(CollectionRemoveEvent<StuffItem> _)
			{
				if (this.AllGetListAction != null)
				{
					this.AllGetListAction();
				}
			});
			this.UpdateAsObservable().Subscribe(delegate(Unit _)
			{
				this._allGetButton.interactable = this.harvestList.Any<StuffItem>();
			}).AddTo(this.disposables);
			this.initialized = true;
			yield break;
		}

		// Token: 0x060075F4 RID: 30196 RVA: 0x003201DF File Offset: 0x0031E5DF
		private void OnDestroy()
		{
			this.disposables.Clear();
		}

		// Token: 0x0400600D RID: 24589
		[SerializeField]
		private ItemListUI _itemListUI;

		// Token: 0x0400600E RID: 24590
		[SerializeField]
		private Text _infoText;

		// Token: 0x0400600F RID: 24591
		[SerializeField]
		private Button _allGetButton;

		// Token: 0x04006010 RID: 24592
		[SerializeField]
		private Button _getButton;

		// Token: 0x04006011 RID: 24593
		[SerializeField]
		private Image _cursor;
	}
}
