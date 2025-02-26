using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Battlehub.UIControls
{
	// Token: 0x0200008A RID: 138
	public abstract class ItemsControl : MonoBehaviour, IPointerDownHandler, IDropHandler, IEventSystemHandler
	{
		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06000193 RID: 403 RVA: 0x0000B714 File Offset: 0x00009B14
		// (remove) Token: 0x06000194 RID: 404 RVA: 0x0000B74C File Offset: 0x00009B4C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<ItemArgs> ItemBeginDrag;

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06000195 RID: 405 RVA: 0x0000B784 File Offset: 0x00009B84
		// (remove) Token: 0x06000196 RID: 406 RVA: 0x0000B7BC File Offset: 0x00009BBC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<ItemDropCancelArgs> ItemBeginDrop;

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000197 RID: 407 RVA: 0x0000B7F4 File Offset: 0x00009BF4
		// (remove) Token: 0x06000198 RID: 408 RVA: 0x0000B82C File Offset: 0x00009C2C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<ItemDropArgs> ItemDrop;

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06000199 RID: 409 RVA: 0x0000B864 File Offset: 0x00009C64
		// (remove) Token: 0x0600019A RID: 410 RVA: 0x0000B89C File Offset: 0x00009C9C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<ItemArgs> ItemEndDrag;

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x0600019B RID: 411 RVA: 0x0000B8D4 File Offset: 0x00009CD4
		// (remove) Token: 0x0600019C RID: 412 RVA: 0x0000B90C File Offset: 0x00009D0C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<SelectionChangedArgs> SelectionChanged;

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x0600019D RID: 413 RVA: 0x0000B944 File Offset: 0x00009D44
		// (remove) Token: 0x0600019E RID: 414 RVA: 0x0000B97C File Offset: 0x00009D7C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<ItemArgs> ItemDoubleClick;

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x0600019F RID: 415 RVA: 0x0000B9B4 File Offset: 0x00009DB4
		// (remove) Token: 0x060001A0 RID: 416 RVA: 0x0000B9EC File Offset: 0x00009DEC
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<ItemsCancelArgs> ItemsRemoving;

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x060001A1 RID: 417 RVA: 0x0000BA24 File Offset: 0x00009E24
		// (remove) Token: 0x060001A2 RID: 418 RVA: 0x0000BA5C File Offset: 0x00009E5C
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<ItemsRemovedArgs> ItemsRemoved;

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x0000BA92 File Offset: 0x00009E92
		protected virtual bool CanScroll
		{
			get
			{
				return this.CanReorder;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x0000BA9A File Offset: 0x00009E9A
		protected bool IsDropInProgress
		{
			get
			{
				return this.m_isDropInProgress;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x0000BAA2 File Offset: 0x00009EA2
		public object DropTarget
		{
			get
			{
				if (this.m_dropTarget == null)
				{
					return null;
				}
				return this.m_dropTarget.Item;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x0000BAC2 File Offset: 0x00009EC2
		public ItemDropAction DropAction
		{
			get
			{
				if (this.m_dropMarker == null)
				{
					return ItemDropAction.None;
				}
				return this.m_dropMarker.Action;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x0000BAE2 File Offset: 0x00009EE2
		protected ItemDropMarker DropMarker
		{
			get
			{
				return this.m_dropMarker;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x0000BAEA File Offset: 0x00009EEA
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x0000BAF4 File Offset: 0x00009EF4
		public IEnumerable Items
		{
			get
			{
				return this.m_items;
			}
			set
			{
				if (value == null)
				{
					this.m_items = null;
					this.m_scrollRect.verticalNormalizedPosition = 1f;
					this.m_scrollRect.horizontalNormalizedPosition = 0f;
				}
				else
				{
					this.m_items = value.OfType<object>().ToList<object>();
				}
				this.DataBind();
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001AA RID: 426 RVA: 0x0000BB4A File Offset: 0x00009F4A
		public int ItemsCount
		{
			get
			{
				if (this.m_items == null)
				{
					return 0;
				}
				return this.m_items.Count;
			}
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000BB64 File Offset: 0x00009F64
		protected void RemoveItemAt(int index)
		{
			this.m_items.RemoveAt(index);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000BB72 File Offset: 0x00009F72
		protected void RemoveItemContainerAt(int index)
		{
			this.m_itemContainers.RemoveAt(index);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000BB80 File Offset: 0x00009F80
		protected void InsertItem(int index, object value)
		{
			this.m_items.Insert(index, value);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000BB8F File Offset: 0x00009F8F
		protected void InsertItemContainerAt(int index, ItemContainer container)
		{
			this.m_itemContainers.Insert(index, container);
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001AF RID: 431 RVA: 0x0000BB9E File Offset: 0x00009F9E
		public int SelectedItemsCount
		{
			get
			{
				if (this.m_selectedItems == null)
				{
					return 0;
				}
				return this.m_selectedItems.Count;
			}
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000BBB8 File Offset: 0x00009FB8
		public bool IsItemSelected(object obj)
		{
			return this.m_selectedItemsHS != null && this.m_selectedItemsHS.Contains(obj);
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x0000BBD3 File Offset: 0x00009FD3
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x0000BBDC File Offset: 0x00009FDC
		public virtual IEnumerable SelectedItems
		{
			get
			{
				return this.m_selectedItems;
			}
			set
			{
				if (this.m_selectionLocked)
				{
					return;
				}
				this.m_selectionLocked = true;
				IList selectedItems = this.m_selectedItems;
				if (value != null)
				{
					this.m_selectedItems = value.OfType<object>().ToList<object>();
					this.m_selectedItemsHS = new HashSet<object>(this.m_selectedItems);
					for (int i = this.m_selectedItems.Count - 1; i >= 0; i--)
					{
						object obj = this.m_selectedItems[i];
						ItemContainer itemContainer = this.GetItemContainer(obj);
						if (itemContainer != null)
						{
							itemContainer.IsSelected = true;
						}
					}
					if (this.m_selectedItems.Count == 0)
					{
						this.m_selectedItemContainer = null;
						this.m_selectedIndex = -1;
					}
					else
					{
						this.m_selectedItemContainer = this.GetItemContainer(this.m_selectedItems[0]);
						this.m_selectedIndex = this.IndexOf(this.m_selectedItems[0]);
					}
				}
				else
				{
					this.m_selectedItems = null;
					this.m_selectedItemsHS = null;
					this.m_selectedItemContainer = null;
					this.m_selectedIndex = -1;
				}
				List<object> list = new List<object>();
				if (selectedItems != null)
				{
					for (int j = 0; j < selectedItems.Count; j++)
					{
						object obj2 = selectedItems[j];
						if (this.m_selectedItemsHS == null || !this.m_selectedItemsHS.Contains(obj2))
						{
							list.Add(obj2);
							ItemContainer itemContainer2 = this.GetItemContainer(obj2);
							if (itemContainer2 != null)
							{
								itemContainer2.IsSelected = false;
							}
						}
					}
				}
				if (this.SelectionChanged != null)
				{
					object[] newItems = (this.m_selectedItems != null) ? this.m_selectedItems.ToArray() : new object[0];
					this.SelectionChanged(this, new SelectionChangedArgs(list.ToArray(), newItems));
				}
				this.m_selectionLocked = false;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x0000BDAB File Offset: 0x0000A1AB
		// (set) Token: 0x060001B4 RID: 436 RVA: 0x0000BDD6 File Offset: 0x0000A1D6
		public object SelectedItem
		{
			get
			{
				if (this.m_selectedItems == null || this.m_selectedItems.Count == 0)
				{
					return null;
				}
				return this.m_selectedItems[0];
			}
			set
			{
				this.SelectedIndex = this.IndexOf(value);
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x0000BDE5 File Offset: 0x0000A1E5
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x0000BDFC File Offset: 0x0000A1FC
		public int SelectedIndex
		{
			get
			{
				if (this.SelectedItem == null)
				{
					return -1;
				}
				return this.m_selectedIndex;
			}
			set
			{
				if (this.m_selectedIndex == value)
				{
					return;
				}
				if (this.m_selectionLocked)
				{
					return;
				}
				this.m_selectionLocked = true;
				ItemContainer selectedItemContainer = this.m_selectedItemContainer;
				if (selectedItemContainer != null)
				{
					selectedItemContainer.IsSelected = false;
				}
				this.m_selectedIndex = value;
				object obj = null;
				if (this.m_selectedIndex >= 0 && this.m_selectedIndex < this.m_items.Count)
				{
					obj = this.m_items[this.m_selectedIndex];
					this.m_selectedItemContainer = this.GetItemContainer(obj);
					if (this.m_selectedItemContainer != null)
					{
						this.m_selectedItemContainer.IsSelected = true;
					}
				}
				object[] array;
				if (obj != null)
				{
					(array = new object[1])[0] = obj;
				}
				else
				{
					array = new object[0];
				}
				object[] array2 = array;
				foreach (object obj2 in (this.m_selectedItems != null) ? this.m_selectedItems.Except(array2).ToArray<object>() : new object[0])
				{
					ItemContainer itemContainer = this.GetItemContainer(obj2);
					if (itemContainer != null)
					{
						itemContainer.IsSelected = false;
					}
				}
				this.m_selectedItems = array2.ToList<object>();
				this.m_selectedItemsHS = new HashSet<object>(this.m_selectedItems);
				if (this.SelectionChanged != null)
				{
					object[] array3;
					this.SelectionChanged(this, new SelectionChangedArgs(array3, array2));
				}
				this.m_selectionLocked = false;
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000BF6D File Offset: 0x0000A36D
		public int IndexOf(object obj)
		{
			if (this.m_items == null)
			{
				return -1;
			}
			if (obj == null)
			{
				return -1;
			}
			return this.m_items.IndexOf(obj);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000BF90 File Offset: 0x0000A390
		public virtual void SetIndex(object obj, int newIndex)
		{
			int num = this.IndexOf(obj);
			if (num == -1)
			{
				return;
			}
			if (num == this.m_selectedIndex)
			{
				this.m_selectedIndex = newIndex;
			}
			this.m_items.RemoveAt(num);
			this.m_items.Insert(newIndex, obj);
			ItemContainer itemContainer = this.m_itemContainers[num];
			this.m_itemContainers.RemoveAt(num);
			this.m_itemContainers.Insert(newIndex, itemContainer);
			itemContainer.transform.SetSiblingIndex(newIndex);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000C00C File Offset: 0x0000A40C
		public ItemContainer GetItemContainer(object obj)
		{
			return (from ic in this.m_itemContainers
			where ic.Item == obj
			select ic).FirstOrDefault<ItemContainer>();
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000C042 File Offset: 0x0000A442
		public ItemContainer LastItemContainer()
		{
			if (this.m_itemContainers == null || this.m_itemContainers.Count == 0)
			{
				return null;
			}
			return this.m_itemContainers[this.m_itemContainers.Count - 1];
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000C079 File Offset: 0x0000A479
		public ItemContainer GetItemContainer(int siblingIndex)
		{
			if (siblingIndex < 0 || siblingIndex >= this.m_itemContainers.Count)
			{
				return null;
			}
			return this.m_itemContainers[siblingIndex];
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000C0A4 File Offset: 0x0000A4A4
		public void ExternalBeginDrag(Vector3 position)
		{
			if (!this.CanDrag)
			{
				return;
			}
			this.m_externalDragOperation = true;
			if (this.m_dropTarget == null)
			{
				return;
			}
			if ((this.m_dragItems != null || this.m_externalDragOperation) && this.m_scrollDir == ItemsControl.ScrollDir.None)
			{
				this.m_dropMarker.SetTraget(this.m_dropTarget);
			}
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000C108 File Offset: 0x0000A508
		public void ExternalItemDrag(Vector3 position)
		{
			if (!this.CanDrag)
			{
				return;
			}
			if (this.m_dropTarget != null)
			{
				this.m_dropMarker.SetPosition(position);
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000C138 File Offset: 0x0000A538
		public void ExternalItemDrop()
		{
			if (!this.CanDrag)
			{
				return;
			}
			this.m_externalDragOperation = false;
			this.m_dropMarker.SetTraget(null);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000C159 File Offset: 0x0000A559
		public ItemContainer Add(object item)
		{
			if (this.m_items == null)
			{
				this.m_items = new List<object>();
				this.m_itemContainers = new List<ItemContainer>();
			}
			return this.Insert(this.m_items.Count, item);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000C190 File Offset: 0x0000A590
		public virtual ItemContainer Insert(int index, object item)
		{
			if (this.m_items == null)
			{
				this.m_items = new List<object>();
				this.m_itemContainers = new List<ItemContainer>();
			}
			object obj = this.m_items.ElementAtOrDefault(index);
			ItemContainer itemContainer = this.GetItemContainer(obj);
			int siblingIndex;
			if (itemContainer != null)
			{
				siblingIndex = this.m_itemContainers.IndexOf(itemContainer);
			}
			else
			{
				siblingIndex = this.m_itemContainers.Count;
			}
			this.m_items.Insert(index, item);
			itemContainer = this.InstantiateItemContainer(siblingIndex);
			if (itemContainer != null)
			{
				itemContainer.Item = item;
				this.DataBindItem(item, itemContainer);
			}
			return itemContainer;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000C230 File Offset: 0x0000A630
		public void SetNextSibling(object sibling, object nextSibling)
		{
			ItemContainer itemContainer = this.GetItemContainer(sibling);
			if (itemContainer == null)
			{
				return;
			}
			ItemContainer itemContainer2 = this.GetItemContainer(nextSibling);
			if (itemContainer2 == null)
			{
				return;
			}
			this.Drop(new ItemContainer[]
			{
				itemContainer2
			}, itemContainer, ItemDropAction.SetNextSibling);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000C27C File Offset: 0x0000A67C
		public void SetPrevSibling(object sibling, object prevSibling)
		{
			ItemContainer itemContainer = this.GetItemContainer(sibling);
			if (itemContainer == null)
			{
				return;
			}
			ItemContainer itemContainer2 = this.GetItemContainer(prevSibling);
			if (itemContainer2 == null)
			{
				return;
			}
			this.Drop(new ItemContainer[]
			{
				itemContainer2
			}, itemContainer, ItemDropAction.SetPrevSibling);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000C2C5 File Offset: 0x0000A6C5
		public virtual void Remove(object item)
		{
			if (item == null)
			{
				return;
			}
			if (this.m_items == null)
			{
				return;
			}
			if (!this.m_items.Contains(item))
			{
				return;
			}
			this.DestroyItem(item);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000C2F4 File Offset: 0x0000A6F4
		public void RemoveAt(int index)
		{
			if (this.m_items == null)
			{
				return;
			}
			if (index >= this.m_items.Count || index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			this.Remove(this.m_items[index]);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000C344 File Offset: 0x0000A744
		private void Awake()
		{
			if (this.Panel == null)
			{
				this.Panel = base.transform;
			}
			this.m_itemContainers = base.GetComponentsInChildren<ItemContainer>().ToList<ItemContainer>();
			this.m_rtcListener = base.GetComponentInChildren<RectTransformChangeListener>();
			if (this.m_rtcListener != null)
			{
				this.m_rtcListener.RectTransformChanged += this.OnViewportRectTransformChanged;
			}
			this.m_dropMarker = base.GetComponentInChildren<ItemDropMarker>(true);
			this.m_scrollRect = base.GetComponent<ScrollRect>();
			if (this.Camera == null)
			{
				this.Camera = Camera.main;
			}
			this.m_prevCanDrag = this.CanDrag;
			this.OnCanDragChanged();
			this.AwakeOverride();
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000C400 File Offset: 0x0000A800
		private void Start()
		{
			this.m_canvas = base.GetComponentInParent<Canvas>();
			this.StartOverride();
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000C414 File Offset: 0x0000A814
		private void Update()
		{
			if (this.m_scrollDir != ItemsControl.ScrollDir.None)
			{
				float num = this.m_scrollRect.content.rect.height - this.m_scrollRect.viewport.rect.height;
				float num2 = 0f;
				if (num > 0f)
				{
					num2 = this.ScrollSpeed / 10f * (1f / num);
				}
				float num3 = this.m_scrollRect.content.rect.width - this.m_scrollRect.viewport.rect.width;
				float num4 = 0f;
				if (num3 > 0f)
				{
					num4 = this.ScrollSpeed / 10f * (1f / num3);
				}
				if (this.m_scrollDir == ItemsControl.ScrollDir.Up)
				{
					this.m_scrollRect.verticalNormalizedPosition += num2;
					if (this.m_scrollRect.verticalNormalizedPosition > 1f)
					{
						this.m_scrollRect.verticalNormalizedPosition = 1f;
						this.m_scrollDir = ItemsControl.ScrollDir.None;
					}
				}
				else if (this.m_scrollDir == ItemsControl.ScrollDir.Down)
				{
					this.m_scrollRect.verticalNormalizedPosition -= num2;
					if (this.m_scrollRect.verticalNormalizedPosition < 0f)
					{
						this.m_scrollRect.verticalNormalizedPosition = 0f;
						this.m_scrollDir = ItemsControl.ScrollDir.None;
					}
				}
				else if (this.m_scrollDir == ItemsControl.ScrollDir.Left)
				{
					this.m_scrollRect.horizontalNormalizedPosition -= num4;
					if (this.m_scrollRect.horizontalNormalizedPosition < 0f)
					{
						this.m_scrollRect.horizontalNormalizedPosition = 0f;
						this.m_scrollDir = ItemsControl.ScrollDir.None;
					}
				}
				if (this.m_scrollDir == ItemsControl.ScrollDir.Right)
				{
					this.m_scrollRect.horizontalNormalizedPosition += num4;
					if (this.m_scrollRect.horizontalNormalizedPosition > 1f)
					{
						this.m_scrollRect.horizontalNormalizedPosition = 1f;
						this.m_scrollDir = ItemsControl.ScrollDir.None;
					}
				}
			}
			if (Input.GetKeyDown(this.RemoveKey))
			{
				this.RemoveSelectedItems();
			}
			if (Input.GetKeyDown(this.SelectAllKey) && Input.GetKey(this.RangeselectKey))
			{
				this.SelectedItems = this.m_items;
			}
			if (this.m_prevCanDrag != this.CanDrag)
			{
				this.OnCanDragChanged();
				this.m_prevCanDrag = this.CanDrag;
			}
			this.UpdateOverride();
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000C688 File Offset: 0x0000AA88
		private void OnEnable()
		{
			ItemContainer.Selected += this.OnItemSelected;
			ItemContainer.Unselected += this.OnItemUnselected;
			ItemContainer.PointerUp += this.OnItemPointerUp;
			ItemContainer.PointerDown += this.OnItemPointerDown;
			ItemContainer.PointerEnter += this.OnItemPointerEnter;
			ItemContainer.PointerExit += this.OnItemPointerExit;
			ItemContainer.DoubleClick += this.OnItemDoubleClick;
			ItemContainer.BeginEdit += this.OnItemBeginEdit;
			ItemContainer.EndEdit += this.OnItemEndEdit;
			ItemContainer.BeginDrag += this.OnItemBeginDrag;
			ItemContainer.Drag += this.OnItemDrag;
			ItemContainer.Drop += this.OnItemDrop;
			ItemContainer.EndDrag += this.OnItemEndDrag;
			this.OnEnableOverride();
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000C77C File Offset: 0x0000AB7C
		private void OnDisable()
		{
			ItemContainer.Selected -= this.OnItemSelected;
			ItemContainer.Unselected -= this.OnItemUnselected;
			ItemContainer.PointerUp -= this.OnItemPointerUp;
			ItemContainer.PointerDown -= this.OnItemPointerDown;
			ItemContainer.PointerEnter -= this.OnItemPointerEnter;
			ItemContainer.PointerExit -= this.OnItemPointerExit;
			ItemContainer.DoubleClick -= this.OnItemDoubleClick;
			ItemContainer.BeginEdit -= this.OnItemBeginEdit;
			ItemContainer.EndEdit -= this.OnItemEndEdit;
			ItemContainer.BeginDrag -= this.OnItemBeginDrag;
			ItemContainer.Drag -= this.OnItemDrag;
			ItemContainer.Drop -= this.OnItemDrop;
			ItemContainer.EndDrag -= this.OnItemEndDrag;
			this.OnDisableOverride();
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000C86E File Offset: 0x0000AC6E
		private void OnDestroy()
		{
			if (this.m_rtcListener != null)
			{
				this.m_rtcListener.RectTransformChanged -= this.OnViewportRectTransformChanged;
			}
			this.OnDestroyOverride();
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000C89E File Offset: 0x0000AC9E
		protected virtual void AwakeOverride()
		{
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000C8A0 File Offset: 0x0000ACA0
		protected virtual void StartOverride()
		{
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000C8A2 File Offset: 0x0000ACA2
		protected virtual void UpdateOverride()
		{
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000C8A4 File Offset: 0x0000ACA4
		protected virtual void OnEnableOverride()
		{
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000C8A6 File Offset: 0x0000ACA6
		protected virtual void OnDisableOverride()
		{
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000C8A8 File Offset: 0x0000ACA8
		protected virtual void OnDestroyOverride()
		{
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000C8AC File Offset: 0x0000ACAC
		private void OnViewportRectTransformChanged()
		{
			if (this.ExpandChildrenHeight || this.ExpandChildrenWidth)
			{
				Rect rect = this.m_scrollRect.viewport.rect;
				if (rect.width != this.m_width || rect.height != this.m_height)
				{
					this.m_width = rect.width;
					this.m_height = rect.height;
					if (this.m_itemContainers != null)
					{
						for (int i = 0; i < this.m_itemContainers.Count; i++)
						{
							ItemContainer itemContainer = this.m_itemContainers[i];
							if (itemContainer != null)
							{
								if (this.ExpandChildrenWidth)
								{
									itemContainer.LayoutElement.minWidth = this.m_width;
								}
								if (this.ExpandChildrenHeight)
								{
									itemContainer.LayoutElement.minHeight = this.m_height;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000C998 File Offset: 0x0000AD98
		private void OnCanDragChanged()
		{
			for (int i = 0; i < this.m_itemContainers.Count; i++)
			{
				ItemContainer itemContainer = this.m_itemContainers[i];
				if (itemContainer != null)
				{
					itemContainer.CanDrag = this.CanDrag;
				}
			}
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000C9E8 File Offset: 0x0000ADE8
		protected bool CanHandleEvent(object sender)
		{
			ItemContainer itemContainer = sender as ItemContainer;
			return itemContainer && itemContainer.transform.IsChildOf(this.Panel);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000CA1C File Offset: 0x0000AE1C
		private void OnItemSelected(object sender, EventArgs e)
		{
			if (this.m_selectionLocked)
			{
				return;
			}
			if (!this.CanHandleEvent(sender))
			{
				return;
			}
			ItemContainer.Unselected -= this.OnItemUnselected;
			if (Input.GetKey(this.MultiselectKey))
			{
				IList list = (this.m_selectedItems == null) ? new List<object>() : this.m_selectedItems.ToList<object>();
				list.Add(((ItemContainer)sender).Item);
				this.SelectedItems = list;
			}
			else if (Input.GetKey(this.RangeselectKey))
			{
				this.SelectRange((ItemContainer)sender);
			}
			else
			{
				this.SelectedIndex = this.IndexOf(((ItemContainer)sender).Item);
			}
			ItemContainer.Unselected += this.OnItemUnselected;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000CAEC File Offset: 0x0000AEEC
		private void SelectRange(ItemContainer itemContainer)
		{
			if (this.m_selectedItems != null && this.m_selectedItems.Count > 0)
			{
				List<object> list = new List<object>();
				int num = this.IndexOf(this.m_selectedItems[0]);
				object item = itemContainer.Item;
				int num2 = this.IndexOf(item);
				int num3 = Mathf.Min(num, num2);
				int num4 = Math.Max(num, num2);
				list.Add(this.m_selectedItems[0]);
				for (int i = num3; i < num; i++)
				{
					list.Add(this.m_items[i]);
				}
				for (int j = num + 1; j <= num4; j++)
				{
					list.Add(this.m_items[j]);
				}
				this.SelectedItems = list;
			}
			else
			{
				this.SelectedIndex = this.IndexOf(itemContainer.Item);
			}
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000CBD8 File Offset: 0x0000AFD8
		private void OnItemUnselected(object sender, EventArgs e)
		{
			if (this.m_selectionLocked)
			{
				return;
			}
			if (!this.CanHandleEvent(sender))
			{
				return;
			}
			IList list = (this.m_selectedItems == null) ? new List<object>() : this.m_selectedItems.ToList<object>();
			list.Remove(((ItemContainer)sender).Item);
			this.SelectedItems = list;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000CC38 File Offset: 0x0000B038
		private void OnItemPointerDown(ItemContainer sender, PointerEventData e)
		{
			if (!this.CanHandleEvent(sender))
			{
				return;
			}
			if (this.m_externalDragOperation)
			{
				return;
			}
			this.m_dropMarker.SetTraget(null);
			this.m_dragItems = null;
			this.m_isDropInProgress = false;
			if (!this.SelectOnPointerUp)
			{
				if (Input.GetKey(this.RangeselectKey))
				{
					this.SelectRange(sender);
				}
				else if (Input.GetKey(this.MultiselectKey))
				{
					sender.IsSelected = !sender.IsSelected;
				}
				else
				{
					sender.IsSelected = true;
				}
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000CCCC File Offset: 0x0000B0CC
		private void OnItemPointerUp(ItemContainer sender, PointerEventData e)
		{
			if (!this.CanHandleEvent(sender))
			{
				return;
			}
			if (this.m_externalDragOperation)
			{
				return;
			}
			if (this.m_dragItems != null)
			{
				return;
			}
			if (this.SelectOnPointerUp)
			{
				if (!this.m_isDropInProgress)
				{
					if (Input.GetKey(this.RangeselectKey))
					{
						this.SelectRange(sender);
					}
					else if (Input.GetKey(this.MultiselectKey))
					{
						sender.IsSelected = !sender.IsSelected;
					}
					else
					{
						sender.IsSelected = true;
					}
				}
			}
			else if (!Input.GetKey(this.MultiselectKey) && !Input.GetKey(this.RangeselectKey) && this.m_selectedItems != null && this.m_selectedItems.Count > 1)
			{
				if (this.SelectedItem == sender.Item)
				{
					this.SelectedItem = null;
				}
				this.SelectedItem = sender.Item;
			}
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000CDC0 File Offset: 0x0000B1C0
		private void OnItemPointerEnter(ItemContainer sender, PointerEventData eventData)
		{
			if (!this.CanHandleEvent(sender))
			{
				return;
			}
			this.m_dropTarget = sender;
			if ((this.m_dragItems != null || this.m_externalDragOperation) && this.m_scrollDir == ItemsControl.ScrollDir.None)
			{
				this.m_dropMarker.SetTraget(this.m_dropTarget);
			}
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000CE13 File Offset: 0x0000B213
		private void OnItemPointerExit(ItemContainer sender, PointerEventData eventData)
		{
			if (!this.CanHandleEvent(sender))
			{
				return;
			}
			this.m_dropTarget = null;
			if (this.m_dragItems != null || this.m_externalDragOperation)
			{
				this.m_dropMarker.SetTraget(null);
			}
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000CE4B File Offset: 0x0000B24B
		private void OnItemDoubleClick(ItemContainer sender, PointerEventData eventData)
		{
			if (!this.CanHandleEvent(sender))
			{
				return;
			}
			if (this.ItemDoubleClick != null)
			{
				this.ItemDoubleClick(this, new ItemArgs(new object[]
				{
					sender.Item
				}, eventData));
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000CE86 File Offset: 0x0000B286
		protected virtual void OnItemBeginEdit(object sender, EventArgs e)
		{
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000CE88 File Offset: 0x0000B288
		protected virtual void OnItemEndEdit(object sender, EventArgs e)
		{
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000CE8C File Offset: 0x0000B28C
		private void OnItemBeginDrag(ItemContainer sender, PointerEventData eventData)
		{
			if (!this.CanHandleEvent(sender))
			{
				return;
			}
			if (this.m_dropTarget != null)
			{
				this.m_dropMarker.SetTraget(this.m_dropTarget);
				this.m_dropMarker.SetPosition(eventData.position);
			}
			if (this.m_selectedItems != null && this.m_selectedItems.Contains(sender.Item))
			{
				this.m_dragItems = this.GetDragItems();
			}
			else
			{
				this.m_dragItems = new ItemContainer[]
				{
					sender
				};
			}
			if (this.ItemBeginDrag != null)
			{
				this.ItemBeginDrag(this, new ItemArgs((from di in this.m_dragItems
				select di.Item).ToArray<object>(), eventData));
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000CF68 File Offset: 0x0000B368
		private void OnItemDrag(ItemContainer sender, PointerEventData eventData)
		{
			if (!this.CanHandleEvent(sender))
			{
				return;
			}
			this.ExternalItemDrag(eventData.position);
			float height = this.m_scrollRect.viewport.rect.height;
			float width = this.m_scrollRect.viewport.rect.width;
			Camera cam = null;
			if (this.m_canvas.renderMode == RenderMode.WorldSpace || this.m_canvas.renderMode == RenderMode.ScreenSpaceCamera)
			{
				cam = this.Camera;
			}
			if (this.CanScroll)
			{
				Vector2 vector;
				if (RectTransformUtility.ScreenPointToLocalPointInRectangle(this.m_scrollRect.viewport, eventData.position, cam, out vector))
				{
					if (vector.y >= 0f)
					{
						this.m_scrollDir = ItemsControl.ScrollDir.Up;
						this.m_dropMarker.SetTraget(null);
					}
					else if (vector.y < -height)
					{
						this.m_scrollDir = ItemsControl.ScrollDir.Down;
						this.m_dropMarker.SetTraget(null);
					}
					else if (vector.x <= 0f)
					{
						this.m_scrollDir = ItemsControl.ScrollDir.Left;
					}
					else if (vector.x >= width)
					{
						this.m_scrollDir = ItemsControl.ScrollDir.Right;
					}
					else
					{
						this.m_scrollDir = ItemsControl.ScrollDir.None;
					}
				}
			}
			else
			{
				this.m_scrollDir = ItemsControl.ScrollDir.None;
			}
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000D0B4 File Offset: 0x0000B4B4
		private void OnItemDrop(ItemContainer sender, PointerEventData eventData)
		{
			if (!this.CanHandleEvent(sender))
			{
				return;
			}
			this.m_isDropInProgress = true;
			try
			{
				if (this.CanDrop(this.m_dragItems, this.m_dropTarget))
				{
					bool flag = false;
					if (this.ItemBeginDrop != null)
					{
						ItemDropCancelArgs itemDropCancelArgs = new ItemDropCancelArgs((from di in this.m_dragItems
						select di.Item).ToArray<object>(), this.m_dropTarget.Item, this.m_dropMarker.Action, false, eventData);
						if (this.ItemBeginDrop != null)
						{
							this.ItemBeginDrop(this, itemDropCancelArgs);
							flag = itemDropCancelArgs.Cancel;
						}
					}
					if (!flag)
					{
						this.Drop(this.m_dragItems, this.m_dropTarget, this.m_dropMarker.Action);
						if (this.ItemDrop != null)
						{
							if (this.m_dragItems == null)
							{
							}
							if (this.m_dropTarget == null)
							{
							}
							if (this.m_dropMarker == null)
							{
							}
							if (this.m_dragItems != null && this.m_dropTarget != null && this.m_dropMarker != null)
							{
								this.ItemDrop(this, new ItemDropArgs((from di in this.m_dragItems
								select di.Item).ToArray<object>(), this.m_dropTarget.Item, this.m_dropMarker.Action, false, eventData));
							}
						}
					}
				}
				this.RaiseEndDrag(eventData);
			}
			finally
			{
				this.m_isDropInProgress = false;
			}
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000D274 File Offset: 0x0000B674
		private void OnItemEndDrag(ItemContainer sender, PointerEventData eventData)
		{
			if (!this.CanHandleEvent(sender))
			{
				return;
			}
			this.RaiseEndDrag(eventData);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000D28C File Offset: 0x0000B68C
		private void RaiseEndDrag(PointerEventData pointerEventData)
		{
			if (this.m_dragItems != null)
			{
				if (this.ItemEndDrag != null)
				{
					this.ItemEndDrag(this, new ItemArgs((from di in this.m_dragItems
					select di.Item).ToArray<object>(), pointerEventData));
				}
				this.m_dropMarker.SetTraget(null);
				this.m_dragItems = null;
				this.m_scrollDir = ItemsControl.ScrollDir.None;
			}
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000D308 File Offset: 0x0000B708
		void IDropHandler.OnDrop(PointerEventData eventData)
		{
			if (!this.CanReorder)
			{
				return;
			}
			if (this.m_dragItems == null)
			{
				GameObject pointerDrag = eventData.pointerDrag;
				if (pointerDrag != null)
				{
					ItemContainer component = pointerDrag.GetComponent<ItemContainer>();
					if (component != null && component.Item != null)
					{
						object item = component.Item;
						if (this.ItemDrop != null)
						{
							this.ItemDrop(this, new ItemDropArgs(new object[]
							{
								item
							}, null, ItemDropAction.SetLastChild, true, eventData));
						}
					}
				}
				return;
			}
			if (this.m_itemContainers != null && this.m_itemContainers.Count > 0)
			{
				this.m_dropTarget = this.m_itemContainers.Last<ItemContainer>();
				this.m_dropMarker.Action = ItemDropAction.SetNextSibling;
			}
			this.m_isDropInProgress = true;
			try
			{
				if (this.CanDrop(this.m_dragItems, this.m_dropTarget))
				{
					this.Drop(this.m_dragItems, this.m_dropTarget, this.m_dropMarker.Action);
					if (this.ItemDrop != null)
					{
						this.ItemDrop(this, new ItemDropArgs((from di in this.m_dragItems
						select di.Item).ToArray<object>(), this.m_dropTarget.Item, this.m_dropMarker.Action, false, eventData));
					}
				}
				this.m_dropMarker.SetTraget(null);
				this.m_dragItems = null;
			}
			finally
			{
				this.m_isDropInProgress = false;
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000D494 File Offset: 0x0000B894
		void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
		{
			if (this.CanUnselectAll)
			{
				this.SelectedIndex = -1;
			}
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000D4A8 File Offset: 0x0000B8A8
		protected virtual bool CanDrop(ItemContainer[] dragItems, ItemContainer dropTarget)
		{
			return dropTarget == null || (dragItems != null && !dragItems.Contains(dropTarget.Item));
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000D4D4 File Offset: 0x0000B8D4
		protected ItemContainer[] GetDragItems()
		{
			ItemContainer[] array = new ItemContainer[this.m_selectedItems.Count];
			if (this.m_selectedItems != null)
			{
				for (int i = 0; i < this.m_selectedItems.Count; i++)
				{
					array[i] = this.GetItemContainer(this.m_selectedItems[i]);
				}
			}
			return (from di in array
			orderby di.transform.GetSiblingIndex()
			select di).ToArray<ItemContainer>();
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000D558 File Offset: 0x0000B958
		protected virtual void SetNextSibling(ItemContainer sibling, ItemContainer nextSibling)
		{
			int num = sibling.transform.GetSiblingIndex();
			int siblingIndex = nextSibling.transform.GetSiblingIndex();
			this.RemoveItemContainerAt(siblingIndex);
			this.RemoveItemAt(siblingIndex);
			if (siblingIndex > num)
			{
				num++;
			}
			nextSibling.transform.SetSiblingIndex(num);
			this.InsertItemContainerAt(num, nextSibling);
			this.InsertItem(num, nextSibling.Item);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000D5B8 File Offset: 0x0000B9B8
		protected virtual void SetPrevSibling(ItemContainer sibling, ItemContainer prevSibling)
		{
			int num = sibling.transform.GetSiblingIndex();
			int siblingIndex = prevSibling.transform.GetSiblingIndex();
			this.RemoveItemContainerAt(siblingIndex);
			this.RemoveItemAt(siblingIndex);
			if (siblingIndex < num)
			{
				num--;
			}
			prevSibling.transform.SetSiblingIndex(num);
			this.InsertItemContainerAt(num, prevSibling);
			this.InsertItem(num, prevSibling.Item);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000D618 File Offset: 0x0000BA18
		protected virtual void Drop(ItemContainer[] dragItems, ItemContainer dropTarget, ItemDropAction action)
		{
			if (action == ItemDropAction.SetPrevSibling)
			{
				foreach (ItemContainer prevSibling in dragItems)
				{
					this.SetPrevSibling(dropTarget, prevSibling);
				}
			}
			else if (action == ItemDropAction.SetNextSibling)
			{
				foreach (ItemContainer nextSibling in dragItems)
				{
					this.SetNextSibling(dropTarget, nextSibling);
				}
			}
			this.UpdateSelectedItemIndex();
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000D67E File Offset: 0x0000BA7E
		protected void UpdateSelectedItemIndex()
		{
			this.m_selectedIndex = this.IndexOf(this.SelectedItem);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000D694 File Offset: 0x0000BA94
		protected virtual void DataBind()
		{
			this.m_itemContainers = base.GetComponentsInChildren<ItemContainer>().ToList<ItemContainer>();
			if (this.m_items == null)
			{
				for (int i = 0; i < this.m_itemContainers.Count; i++)
				{
					UnityEngine.Object.DestroyImmediate(this.m_itemContainers[i].gameObject);
				}
			}
			else
			{
				int num = this.m_items.Count - this.m_itemContainers.Count;
				if (num > 0)
				{
					for (int j = 0; j < num; j++)
					{
						this.InstantiateItemContainer(this.m_itemContainers.Count);
					}
				}
				else
				{
					int num2 = this.m_itemContainers.Count + num;
					for (int k = this.m_itemContainers.Count - 1; k >= num2; k--)
					{
						this.DestroyItemContainer(k);
					}
				}
			}
			for (int l = 0; l < this.m_itemContainers.Count; l++)
			{
				ItemContainer itemContainer = this.m_itemContainers[l];
				if (itemContainer != null)
				{
					itemContainer.Clear();
				}
			}
			if (this.m_items != null)
			{
				for (int m = 0; m < this.m_items.Count; m++)
				{
					object item = this.m_items[m];
					ItemContainer itemContainer2 = this.m_itemContainers[m];
					itemContainer2.CanDrag = this.CanDrag;
					if (itemContainer2 != null)
					{
						itemContainer2.Item = item;
						this.DataBindItem(item, itemContainer2);
					}
				}
			}
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000D82D File Offset: 0x0000BC2D
		public virtual void DataBindItem(object item, ItemContainer itemContainer)
		{
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000D830 File Offset: 0x0000BC30
		protected ItemContainer InstantiateItemContainer(int siblingIndex)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ItemContainerPrefab);
			gameObject.name = "ItemContainer";
			gameObject.transform.SetParent(this.Panel, false);
			gameObject.transform.SetSiblingIndex(siblingIndex);
			ItemContainer itemContainer = this.InstantiateItemContainerOverride(gameObject);
			itemContainer.CanDrag = this.CanDrag;
			if (this.ExpandChildrenWidth)
			{
				itemContainer.LayoutElement.minWidth = this.m_width;
			}
			if (this.ExpandChildrenHeight)
			{
				itemContainer.LayoutElement.minHeight = this.m_height;
			}
			this.m_itemContainers.Insert(siblingIndex, itemContainer);
			return itemContainer;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000D8CC File Offset: 0x0000BCCC
		protected void DestroyItemContainer(int siblingIndex)
		{
			if (this.m_itemContainers == null)
			{
				return;
			}
			if (siblingIndex >= 0 && siblingIndex < this.m_itemContainers.Count)
			{
				UnityEngine.Object.DestroyImmediate(this.m_itemContainers[siblingIndex].gameObject);
				this.m_itemContainers.RemoveAt(siblingIndex);
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000D920 File Offset: 0x0000BD20
		protected virtual ItemContainer InstantiateItemContainerOverride(GameObject container)
		{
			ItemContainer itemContainer = container.GetComponent<ItemContainer>();
			if (itemContainer == null)
			{
				itemContainer = container.AddComponent<ItemContainer>();
			}
			return itemContainer;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000D948 File Offset: 0x0000BD48
		public void RemoveSelectedItems()
		{
			if (this.m_selectedItems == null)
			{
				return;
			}
			object[] array;
			if (this.ItemsRemoving != null)
			{
				ItemsCancelArgs itemsCancelArgs = new ItemsCancelArgs(this.m_selectedItems.ToList<object>());
				this.ItemsRemoving(this, itemsCancelArgs);
				if (itemsCancelArgs.Items == null)
				{
					array = new object[0];
				}
				else
				{
					array = itemsCancelArgs.Items.ToArray();
				}
			}
			else
			{
				array = this.m_selectedItems.ToArray();
			}
			if (array.Length == 0)
			{
				return;
			}
			this.SelectedItems = null;
			foreach (object item in array)
			{
				this.DestroyItem(item);
			}
			if (this.ItemsRemoved != null)
			{
				this.ItemsRemoved(this, new ItemsRemovedArgs(array));
			}
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000DA0C File Offset: 0x0000BE0C
		protected virtual void DestroyItem(object item)
		{
			if (this.m_selectedItems != null && this.m_selectedItems.Contains(item))
			{
				this.m_selectedItems.Remove(item);
				this.m_selectedItemsHS.Remove(item);
				if (this.m_selectedItems.Count == 0)
				{
					this.m_selectedItemContainer = null;
					this.m_selectedIndex = -1;
				}
				else
				{
					this.m_selectedItemContainer = this.GetItemContainer(this.m_selectedItems[0]);
					this.m_selectedIndex = this.IndexOf(this.m_selectedItemContainer.Item);
				}
			}
			ItemContainer itemContainer = this.GetItemContainer(item);
			if (itemContainer != null)
			{
				int siblingIndex = itemContainer.transform.GetSiblingIndex();
				this.DestroyItemContainer(siblingIndex);
				this.m_items.Remove(item);
			}
		}

		// Token: 0x04000247 RID: 583
		public KeyCode MultiselectKey = KeyCode.LeftControl;

		// Token: 0x04000248 RID: 584
		public KeyCode RangeselectKey = KeyCode.LeftShift;

		// Token: 0x04000249 RID: 585
		public KeyCode SelectAllKey = KeyCode.A;

		// Token: 0x0400024A RID: 586
		public KeyCode RemoveKey = KeyCode.Delete;

		// Token: 0x0400024B RID: 587
		public bool SelectOnPointerUp;

		// Token: 0x0400024C RID: 588
		public bool CanUnselectAll = true;

		// Token: 0x0400024D RID: 589
		public bool CanEdit = true;

		// Token: 0x0400024E RID: 590
		private bool m_prevCanDrag;

		// Token: 0x0400024F RID: 591
		public bool CanDrag = true;

		// Token: 0x04000250 RID: 592
		public bool CanReorder = true;

		// Token: 0x04000251 RID: 593
		public bool ExpandChildrenWidth = true;

		// Token: 0x04000252 RID: 594
		public bool ExpandChildrenHeight;

		// Token: 0x04000253 RID: 595
		private bool m_isDropInProgress;

		// Token: 0x04000254 RID: 596
		[SerializeField]
		private GameObject ItemContainerPrefab;

		// Token: 0x04000255 RID: 597
		[SerializeField]
		protected Transform Panel;

		// Token: 0x04000256 RID: 598
		private Canvas m_canvas;

		// Token: 0x04000257 RID: 599
		public Camera Camera;

		// Token: 0x04000258 RID: 600
		public float ScrollSpeed = 100f;

		// Token: 0x04000259 RID: 601
		private ItemsControl.ScrollDir m_scrollDir;

		// Token: 0x0400025A RID: 602
		private ScrollRect m_scrollRect;

		// Token: 0x0400025B RID: 603
		private RectTransformChangeListener m_rtcListener;

		// Token: 0x0400025C RID: 604
		private float m_width;

		// Token: 0x0400025D RID: 605
		private float m_height;

		// Token: 0x0400025E RID: 606
		private List<ItemContainer> m_itemContainers;

		// Token: 0x0400025F RID: 607
		private ItemDropMarker m_dropMarker;

		// Token: 0x04000260 RID: 608
		private bool m_externalDragOperation;

		// Token: 0x04000261 RID: 609
		private ItemContainer m_dropTarget;

		// Token: 0x04000262 RID: 610
		private ItemContainer[] m_dragItems;

		// Token: 0x04000263 RID: 611
		private IList<object> m_items;

		// Token: 0x04000264 RID: 612
		private bool m_selectionLocked;

		// Token: 0x04000265 RID: 613
		private List<object> m_selectedItems;

		// Token: 0x04000266 RID: 614
		private HashSet<object> m_selectedItemsHS;

		// Token: 0x04000267 RID: 615
		private ItemContainer m_selectedItemContainer;

		// Token: 0x04000268 RID: 616
		private int m_selectedIndex = -1;

		// Token: 0x0200008B RID: 139
		private enum ScrollDir
		{
			// Token: 0x04000270 RID: 624
			None,
			// Token: 0x04000271 RID: 625
			Up,
			// Token: 0x04000272 RID: 626
			Down,
			// Token: 0x04000273 RID: 627
			Left,
			// Token: 0x04000274 RID: 628
			Right
		}
	}
}
