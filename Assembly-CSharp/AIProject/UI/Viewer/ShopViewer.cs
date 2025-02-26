using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using AIProject.SaveData;
using Illusion.Game.Extensions;
using UniRx;
using UnityEngine;

namespace AIProject.UI.Viewer
{
	// Token: 0x02000EB8 RID: 3768
	public class ShopViewer : MonoBehaviour
	{
		// Token: 0x1700184A RID: 6218
		// (get) Token: 0x06007B46 RID: 31558 RVA: 0x0033E4BC File Offset: 0x0033C8BC
		public ItemListUI normals
		{
			[CompilerGenerated]
			get
			{
				return this._normalItemListUI;
			}
		}

		// Token: 0x1700184B RID: 6219
		// (get) Token: 0x06007B47 RID: 31559 RVA: 0x0033E4C4 File Offset: 0x0033C8C4
		public ItemListUI specials
		{
			[CompilerGenerated]
			get
			{
				return this._specialItemListUI;
			}
		}

		// Token: 0x1700184C RID: 6220
		// (get) Token: 0x06007B48 RID: 31560 RVA: 0x0033E4CC File Offset: 0x0033C8CC
		public ShopViewer.ItemListController[] controllers { get; } = new ShopViewer.ItemListController[2];

		// Token: 0x1700184D RID: 6221
		// (get) Token: 0x06007B49 RID: 31561 RVA: 0x0033E4D4 File Offset: 0x0033C8D4
		// (set) Token: 0x06007B4A RID: 31562 RVA: 0x0033E4DC File Offset: 0x0033C8DC
		public bool initialized { get; private set; }

		// Token: 0x06007B4B RID: 31563 RVA: 0x0033E4E8 File Offset: 0x0033C8E8
		private void Awake()
		{
			ItemListUI[] array = new ItemListUI[]
			{
				this._normalItemListUI,
				this._specialItemListUI
			};
			for (int i = 0; i < this.controllers.Length; i++)
			{
				ShopViewer.ItemListController itemListController = new ShopViewer.ItemListController((i != 0) ? ShopViewer.ItemListController.Mode.VendorSpecial : ShopViewer.ItemListController.Mode.Vendor);
				itemListController.Bind(array[i]);
				this.controllers[i] = itemListController;
			}
		}

		// Token: 0x06007B4C RID: 31564 RVA: 0x0033E54C File Offset: 0x0033C94C
		private IEnumerator Start()
		{
			this.initialized = true;
			yield break;
		}

		// Token: 0x040062CE RID: 25294
		[Header("Normal")]
		[SerializeField]
		private ItemListUI _normalItemListUI;

		// Token: 0x040062CF RID: 25295
		[Header("Special")]
		[SerializeField]
		private ItemListUI _specialItemListUI;

		// Token: 0x02000EB9 RID: 3769
		public class ExtraPadding : ItemNodeUI.ExtraData
		{
			// Token: 0x06007B4D RID: 31565 RVA: 0x0033E567 File Offset: 0x0033C967
			public ExtraPadding(StuffItem item, ShopViewer.ItemListController source)
			{
				this.item = item;
				this.source = source;
			}

			// Token: 0x1700184E RID: 6222
			// (get) Token: 0x06007B4E RID: 31566 RVA: 0x0033E57D File Offset: 0x0033C97D
			public StuffItem item { get; }

			// Token: 0x1700184F RID: 6223
			// (get) Token: 0x06007B4F RID: 31567 RVA: 0x0033E585 File Offset: 0x0033C985
			public ShopViewer.ItemListController source { get; }
		}

		// Token: 0x02000EBA RID: 3770
		public class ItemListController
		{
			// Token: 0x06007B50 RID: 31568 RVA: 0x0033E58D File Offset: 0x0033C98D
			public ItemListController()
			{
			}

			// Token: 0x06007B51 RID: 31569 RVA: 0x0033E595 File Offset: 0x0033C995
			public ItemListController(ShopViewer.ItemListController.Mode mode)
			{
				this.mode = mode;
			}

			// Token: 0x140000CE RID: 206
			// (add) Token: 0x06007B52 RID: 31570 RVA: 0x0033E5A4 File Offset: 0x0033C9A4
			// (remove) Token: 0x06007B53 RID: 31571 RVA: 0x0033E5DC File Offset: 0x0033C9DC
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			public event Action<int, ItemNodeUI> DoubleClick;

			// Token: 0x17001850 RID: 6224
			// (get) Token: 0x06007B54 RID: 31572 RVA: 0x0033E612 File Offset: 0x0033CA12
			public ItemListUI itemListUI
			{
				[CompilerGenerated]
				get
				{
					return this._itemListUI;
				}
			}

			// Token: 0x17001851 RID: 6225
			// (get) Token: 0x06007B55 RID: 31573 RVA: 0x0033E61A File Offset: 0x0033CA1A
			public ShopViewer.ItemListController.Mode mode { get; }

			// Token: 0x06007B56 RID: 31574 RVA: 0x0033E622 File Offset: 0x0033CA22
			public void Bind(ItemListUI itemListUI)
			{
				this._itemListUI = itemListUI;
			}

			// Token: 0x06007B57 RID: 31575 RVA: 0x0033E62B File Offset: 0x0033CA2B
			public void Clear()
			{
				this._itemListUI.ClearItems();
			}

			// Token: 0x06007B58 RID: 31576 RVA: 0x0033E638 File Offset: 0x0033CA38
			public void Create(IReadOnlyCollection<StuffItem> itemCollection)
			{
				this._itemListUI.ClearItems();
				foreach (StuffItem item in itemCollection)
				{
					this.ItemListAddNode(item, new ShopViewer.ExtraPadding(item, this));
				}
			}

			// Token: 0x06007B59 RID: 31577 RVA: 0x0033E6A0 File Offset: 0x0033CAA0
			public void AddItem(StuffItem item, ShopViewer.ExtraPadding padding)
			{
				if (this.mode == ShopViewer.ItemListController.Mode.Normal)
				{
					StuffItem stuffItem = null;
					foreach (KeyValuePair<int, ItemNodeUI> keyValuePair in this._itemListUI.optionTable)
					{
						ShopViewer.ExtraPadding extraPadding = keyValuePair.Value.extraData as ShopViewer.ExtraPadding;
						if (extraPadding != null && extraPadding.item == padding.item)
						{
							stuffItem = keyValuePair.Value.Item;
							break;
						}
					}
					if (stuffItem != null)
					{
						stuffItem.Count += item.Count;
					}
					else
					{
						this.ItemListAddNode(item, padding);
					}
				}
				else
				{
					padding.item.Count += item.Count;
				}
				this._itemListUI.Refresh();
			}

			// Token: 0x06007B5A RID: 31578 RVA: 0x0033E790 File Offset: 0x0033CB90
			public void RemoveItem(int sel, StuffItem item)
			{
				ItemNodeUI node = this._itemListUI.GetNode(sel);
				node.Item.Count -= item.Count;
				if (node.Item.Count <= 0)
				{
					if (this.mode == ShopViewer.ItemListController.Mode.Normal)
					{
						this._itemListUI.RemoveItemNode(sel);
					}
					this._itemListUI.ForceSetNonSelect();
				}
				this._itemListUI.Refresh();
			}

			// Token: 0x06007B5B RID: 31579 RVA: 0x0033E804 File Offset: 0x0033CC04
			private ItemNodeUI ItemListAddNode(StuffItem item, ShopViewer.ExtraPadding padding)
			{
				int index = this._itemListUI.SearchNotUsedIndex;
				ItemNodeUI node = this._itemListUI.AddItemNode(index, item);
				if (node != null)
				{
					node.extraData = padding;
					node.OnClick.AsObservable().DoubleInterval(250f, false).Subscribe(delegate(IList<double> _)
					{
						if (this.DoubleClick != null)
						{
							this.DoubleClick(index, node);
						}
					}).AddTo(node);
				}
				return node;
			}

			// Token: 0x040062D5 RID: 25301
			private ItemListUI _itemListUI;

			// Token: 0x02000EBB RID: 3771
			public enum Mode
			{
				// Token: 0x040062D8 RID: 25304
				Normal,
				// Token: 0x040062D9 RID: 25305
				Vendor,
				// Token: 0x040062DA RID: 25306
				VendorSpecial
			}
		}
	}
}
