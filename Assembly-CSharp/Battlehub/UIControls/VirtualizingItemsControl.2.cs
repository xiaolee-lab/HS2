using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Battlehub.UIControls
{
	// Token: 0x020000A1 RID: 161
	public class VirtualizingItemsControl : Selectable, IDropHandler, IUpdateSelectedHandler, IUpdateFocusedHandler, IPointerClickHandler, IEventSystemHandler
	{
		// Token: 0x1400002F RID: 47
		// (add) Token: 0x060002E7 RID: 743 RVA: 0x00011A98 File Offset: 0x0000FE98
		// (remove) Token: 0x060002E8 RID: 744 RVA: 0x00011AD0 File Offset: 0x0000FED0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<ItemArgs> ItemBeginDrag;

		// Token: 0x14000030 RID: 48
		// (add) Token: 0x060002E9 RID: 745 RVA: 0x00011B08 File Offset: 0x0000FF08
		// (remove) Token: 0x060002EA RID: 746 RVA: 0x00011B40 File Offset: 0x0000FF40
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<ItemDropCancelArgs> ItemBeginDrop;

		// Token: 0x14000031 RID: 49
		// (add) Token: 0x060002EB RID: 747 RVA: 0x00011B78 File Offset: 0x0000FF78
		// (remove) Token: 0x060002EC RID: 748 RVA: 0x00011BB0 File Offset: 0x0000FFB0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<ItemDropCancelArgs> ItemDragEnter;

		// Token: 0x14000032 RID: 50
		// (add) Token: 0x060002ED RID: 749 RVA: 0x00011BE8 File Offset: 0x0000FFE8
		// (remove) Token: 0x060002EE RID: 750 RVA: 0x00011C20 File Offset: 0x00010020
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler ItemDragExit;

		// Token: 0x14000033 RID: 51
		// (add) Token: 0x060002EF RID: 751 RVA: 0x00011C58 File Offset: 0x00010058
		// (remove) Token: 0x060002F0 RID: 752 RVA: 0x00011C90 File Offset: 0x00010090
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<ItemArgs> ItemDrag;

		// Token: 0x14000034 RID: 52
		// (add) Token: 0x060002F1 RID: 753 RVA: 0x00011CC8 File Offset: 0x000100C8
		// (remove) Token: 0x060002F2 RID: 754 RVA: 0x00011D00 File Offset: 0x00010100
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<ItemDropArgs> ItemDrop;

		// Token: 0x14000035 RID: 53
		// (add) Token: 0x060002F3 RID: 755 RVA: 0x00011D38 File Offset: 0x00010138
		// (remove) Token: 0x060002F4 RID: 756 RVA: 0x00011D70 File Offset: 0x00010170
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<ItemArgs> ItemEndDrag;

		// Token: 0x14000036 RID: 54
		// (add) Token: 0x060002F5 RID: 757 RVA: 0x00011DA8 File Offset: 0x000101A8
		// (remove) Token: 0x060002F6 RID: 758 RVA: 0x00011DE0 File Offset: 0x000101E0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<SelectionChangedArgs> SelectionChanged;

		// Token: 0x14000037 RID: 55
		// (add) Token: 0x060002F7 RID: 759 RVA: 0x00011E18 File Offset: 0x00010218
		// (remove) Token: 0x060002F8 RID: 760 RVA: 0x00011E50 File Offset: 0x00010250
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<ItemArgs> ItemDoubleClick;

		// Token: 0x14000038 RID: 56
		// (add) Token: 0x060002F9 RID: 761 RVA: 0x00011E88 File Offset: 0x00010288
		// (remove) Token: 0x060002FA RID: 762 RVA: 0x00011EC0 File Offset: 0x000102C0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<ItemArgs> ItemClick;

		// Token: 0x14000039 RID: 57
		// (add) Token: 0x060002FB RID: 763 RVA: 0x00011EF8 File Offset: 0x000102F8
		// (remove) Token: 0x060002FC RID: 764 RVA: 0x00011F30 File Offset: 0x00010330
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<ItemsCancelArgs> ItemsRemoving;

		// Token: 0x1400003A RID: 58
		// (add) Token: 0x060002FD RID: 765 RVA: 0x00011F68 File Offset: 0x00010368
		// (remove) Token: 0x060002FE RID: 766 RVA: 0x00011FA0 File Offset: 0x000103A0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<ItemsRemovedArgs> ItemsRemoved;

		// Token: 0x1400003B RID: 59
		// (add) Token: 0x060002FF RID: 767 RVA: 0x00011FD8 File Offset: 0x000103D8
		// (remove) Token: 0x06000300 RID: 768 RVA: 0x00012010 File Offset: 0x00010410
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler IsFocusedChanged;

		// Token: 0x1400003C RID: 60
		// (add) Token: 0x06000301 RID: 769 RVA: 0x00012048 File Offset: 0x00010448
		// (remove) Token: 0x06000302 RID: 770 RVA: 0x00012080 File Offset: 0x00010480
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler Submit;

		// Token: 0x1400003D RID: 61
		// (add) Token: 0x06000303 RID: 771 RVA: 0x000120B8 File Offset: 0x000104B8
		// (remove) Token: 0x06000304 RID: 772 RVA: 0x000120F0 File Offset: 0x000104F0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler Cancel;

		// Token: 0x1400003E RID: 62
		// (add) Token: 0x06000305 RID: 773 RVA: 0x00012128 File Offset: 0x00010528
		// (remove) Token: 0x06000306 RID: 774 RVA: 0x00012160 File Offset: 0x00010560
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<PointerEventArgs> Click;

		// Token: 0x1400003F RID: 63
		// (add) Token: 0x06000307 RID: 775 RVA: 0x00012198 File Offset: 0x00010598
		// (remove) Token: 0x06000308 RID: 776 RVA: 0x000121D0 File Offset: 0x000105D0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<PointerEventArgs> PointerEnter;

		// Token: 0x14000040 RID: 64
		// (add) Token: 0x06000309 RID: 777 RVA: 0x00012208 File Offset: 0x00010608
		// (remove) Token: 0x0600030A RID: 778 RVA: 0x00012240 File Offset: 0x00010640
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<PointerEventArgs> PointerExit;

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600030B RID: 779 RVA: 0x00012276 File Offset: 0x00010676
		protected virtual bool CanScroll
		{
			get
			{
				return this.CanReorder;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600030C RID: 780 RVA: 0x0001227E File Offset: 0x0001067E
		protected bool IsDropInProgress
		{
			get
			{
				return this.m_isDropInProgress;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600030D RID: 781 RVA: 0x00012286 File Offset: 0x00010686
		// (set) Token: 0x0600030E RID: 782 RVA: 0x00012290 File Offset: 0x00010690
		public bool IsFocused
		{
			get
			{
				return this.m_isFocused;
			}
			set
			{
				if (this.m_isFocused != value)
				{
					this.m_isFocused = value;
					if (this.IsFocusedChanged != null)
					{
						this.IsFocusedChanged(this, EventArgs.Empty);
					}
					if (this.m_isFocused && this.SelectedIndex == -1 && this.m_scrollRect.ItemsCount > 0 && !this.CanUnselectAll)
					{
						this.SelectedIndex = 0;
					}
				}
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600030F RID: 783 RVA: 0x00012306 File Offset: 0x00010706
		// (set) Token: 0x06000310 RID: 784 RVA: 0x00012310 File Offset: 0x00010710
		public bool IsSelected
		{
			get
			{
				return this.m_isSelected;
			}
			protected set
			{
				if (this.m_isSelected != value)
				{
					this.m_isSelected = value;
					if (this.m_isSelected)
					{
						this.m_selectionBackup = this.m_selectedItems;
					}
					else
					{
						this.m_selectionBackup = null;
					}
					if (!this.m_isSelected)
					{
						this.IsFocused = false;
					}
				}
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000311 RID: 785 RVA: 0x00012365 File Offset: 0x00010765
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

		// Token: 0x06000312 RID: 786 RVA: 0x00012385 File Offset: 0x00010785
		public void ClearTarget()
		{
			this.m_dropMarker.SetTarget(null);
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000313 RID: 787 RVA: 0x00012393 File Offset: 0x00010793
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

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000314 RID: 788 RVA: 0x000123B3 File Offset: 0x000107B3
		public object[] DragItems
		{
			get
			{
				return this.m_dragItems;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000315 RID: 789 RVA: 0x000123BB File Offset: 0x000107BB
		protected VirtualizingItemDropMarker DropMarker
		{
			get
			{
				return this.m_dropMarker;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000316 RID: 790 RVA: 0x000123C3 File Offset: 0x000107C3
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

		// Token: 0x06000317 RID: 791 RVA: 0x000123DD File Offset: 0x000107DD
		public bool IsItemSelected(object obj)
		{
			return this.m_selectedItemsHS != null && this.m_selectedItemsHS.Contains(obj);
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000318 RID: 792 RVA: 0x000123F8 File Offset: 0x000107F8
		// (set) Token: 0x06000319 RID: 793 RVA: 0x00012400 File Offset: 0x00010800
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
						ItemContainerData itemContainerData;
						if (this.m_itemContainerData.TryGetValue(obj, out itemContainerData))
						{
							itemContainerData.IsSelected = true;
						}
						VirtualizingItemContainer itemContainer = this.GetItemContainer(obj);
						if (itemContainer != null)
						{
							itemContainer.IsSelected = true;
						}
					}
					if (this.m_selectedItems.Count == 0)
					{
						this.m_selectedIndex = -1;
					}
					else
					{
						this.m_selectedIndex = this.IndexOf(this.m_selectedItems[0]);
					}
				}
				else
				{
					this.m_selectedItems = null;
					this.m_selectedItemsHS = null;
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
							ItemContainerData itemContainerData2;
							if (this.m_itemContainerData.TryGetValue(obj2, out itemContainerData2))
							{
								itemContainerData2.IsSelected = false;
							}
							list.Add(obj2);
							VirtualizingItemContainer itemContainer2 = this.GetItemContainer(obj2);
							if (itemContainer2 != null)
							{
								itemContainer2.IsSelected = false;
							}
						}
					}
				}
				bool flag = (selectedItems == null && this.m_selectedItems != null) || (selectedItems != null && this.m_selectedItems == null) || (selectedItems != null && this.m_selectedItems != null && selectedItems.Count != this.m_selectedItems.Count);
				if (!flag && selectedItems != null)
				{
					for (int k = 0; k < selectedItems.Count; k++)
					{
						if (this.m_selectedItems[k] != selectedItems[k])
						{
							flag = true;
							break;
						}
					}
				}
				if (flag && this.SelectionChanged != null)
				{
					object[] newItems = (this.m_selectedItems != null) ? this.m_selectedItems.ToArray() : new object[0];
					this.SelectionChanged(this, new SelectionChangedArgs(list.ToArray(), newItems));
				}
				this.m_selectionLocked = false;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600031A RID: 794 RVA: 0x00012684 File Offset: 0x00010A84
		// (set) Token: 0x0600031B RID: 795 RVA: 0x000126AF File Offset: 0x00010AAF
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

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600031C RID: 796 RVA: 0x000126BE File Offset: 0x00010ABE
		// (set) Token: 0x0600031D RID: 797 RVA: 0x000126D4 File Offset: 0x00010AD4
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
				ItemContainerData itemContainerData;
				if (this.SelectedItem != null && this.m_itemContainerData.TryGetValue(this.SelectedItem, out itemContainerData))
				{
					itemContainerData.IsSelected = false;
				}
				VirtualizingItemContainer itemContainer = this.GetItemContainer(this.SelectedItem);
				if (itemContainer != null)
				{
					itemContainer.IsSelected = false;
				}
				this.m_selectedIndex = value;
				object obj = null;
				if (this.m_selectedIndex >= 0 && this.m_selectedIndex < this.m_scrollRect.ItemsCount)
				{
					obj = this.m_scrollRect.Items[this.m_selectedIndex];
					ItemContainerData itemContainerData2;
					if (obj != null && this.m_itemContainerData.TryGetValue(obj, out itemContainerData2))
					{
						itemContainerData2.IsSelected = true;
					}
					VirtualizingItemContainer itemContainer2 = this.GetItemContainer(obj);
					if (itemContainer2 != null)
					{
						itemContainer2.IsSelected = true;
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
					ItemContainerData itemContainerData3;
					if (obj2 != null && this.m_itemContainerData.TryGetValue(obj2, out itemContainerData3))
					{
						itemContainerData3.IsSelected = false;
					}
					VirtualizingItemContainer itemContainer3 = this.GetItemContainer(obj2);
					if (itemContainer3 != null)
					{
						itemContainer3.IsSelected = false;
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

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600031E RID: 798 RVA: 0x000128B9 File Offset: 0x00010CB9
		public int ItemsCount
		{
			get
			{
				return this.m_scrollRect.ItemsCount;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600031F RID: 799 RVA: 0x000128C6 File Offset: 0x00010CC6
		// (set) Token: 0x06000320 RID: 800 RVA: 0x000128D3 File Offset: 0x00010CD3
		public IEnumerable Items
		{
			get
			{
				return this.m_scrollRect.Items;
			}
			set
			{
				this.SetItems(value, true);
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000321 RID: 801 RVA: 0x000128DD File Offset: 0x00010CDD
		public VirtualizingScrollRect VirtualizingScrollRect
		{
			[CompilerGenerated]
			get
			{
				return this.m_scrollRect;
			}
		}

		// Token: 0x06000322 RID: 802 RVA: 0x000128E8 File Offset: 0x00010CE8
		public void SetItems(IEnumerable value, bool updateSelection)
		{
			if (value == null)
			{
				this.SelectedItems = null;
				this.m_scrollRect.Items = null;
				this.m_scrollRect.verticalNormalizedPosition = 1f;
				this.m_scrollRect.horizontalNormalizedPosition = 0f;
				this.m_itemContainerData = new Dictionary<object, ItemContainerData>();
			}
			else
			{
				List<object> list = value.OfType<object>().ToList<object>();
				if (updateSelection && this.m_selectedItemsHS != null)
				{
					this.m_selectedItems = (from item in list
					where this.m_selectedItemsHS.Contains(item)
					select item).ToList<object>();
				}
				this.m_itemContainerData = new Dictionary<object, ItemContainerData>();
				for (int i = 0; i < list.Count; i++)
				{
					this.m_itemContainerData.Add(list[i], this.InstantiateItemContainerData(list[i]));
				}
				this.m_scrollRect.Items = list;
				if (updateSelection && this.IsFocused && this.SelectedIndex == -1)
				{
					this.SelectedIndex = 0;
				}
			}
		}

		// Token: 0x06000323 RID: 803 RVA: 0x000129EC File Offset: 0x00010DEC
		protected virtual ItemContainerData InstantiateItemContainerData(object item)
		{
			return new ItemContainerData
			{
				Item = item
			};
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00012A08 File Offset: 0x00010E08
		protected override void Awake()
		{
			base.Awake();
			this.m_scrollRect = base.GetComponent<VirtualizingScrollRect>();
			if (this.m_scrollRect == null)
			{
			}
			this.m_scrollRect.ItemDataBinding += this.OnScrollRectItemDataBinding;
			this.m_dropMarker = base.GetComponentInChildren<VirtualizingItemDropMarker>(true);
			this.m_rtcListener = base.GetComponentInChildren<RectTransformChangeListener>();
			if (this.m_rtcListener != null)
			{
				this.m_rtcListener.RectTransformChanged += this.OnViewportRectTransformChanged;
			}
			this.m_pointerEventsListener = base.GetComponentInChildren<PointerEnterExitListener>();
			if (this.m_pointerEventsListener != null)
			{
				this.m_pointerEventsListener.PointerEnter += this.OnViewportPointerEnter;
				this.m_pointerEventsListener.PointerExit += this.OnViewportPointerExit;
			}
			if (this.Camera == null)
			{
				Canvas componentInParent = base.GetComponentInParent<Canvas>();
				if (componentInParent != null)
				{
					this.Camera = componentInParent.worldCamera;
				}
			}
			this.m_prevCanDrag = this.CanDrag;
			this.OnCanDragChanged();
			this.AwakeOverride();
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00012B24 File Offset: 0x00010F24
		protected override void Start()
		{
			base.Start();
			if (this.m_eventSystem == null)
			{
				this.m_eventSystem = EventSystem.current;
			}
			if (this.InputProvider == null)
			{
				this.InputProvider = base.GetComponent<InputProvider>();
				if (this.InputProvider == null)
				{
					this.InputProvider = this.CreateInputProviderOverride();
				}
			}
			this.m_canvas = base.GetComponentInParent<Canvas>();
			this.StartOverride();
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00012B9F File Offset: 0x00010F9F
		protected virtual InputProvider CreateInputProviderOverride()
		{
			return base.gameObject.AddComponent<VirtualizingItemsControlInputProvider>();
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00012BAC File Offset: 0x00010FAC
		private void Update()
		{
			if (this.m_scrollDir != VirtualizingItemsControl.ScrollDir.None)
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
				if (this.m_scrollDir == VirtualizingItemsControl.ScrollDir.Up)
				{
					this.m_scrollRect.verticalNormalizedPosition += num2;
					if (this.m_scrollRect.verticalNormalizedPosition > 1f)
					{
						this.m_scrollRect.verticalNormalizedPosition = 1f;
						this.m_scrollDir = VirtualizingItemsControl.ScrollDir.None;
					}
				}
				else if (this.m_scrollDir == VirtualizingItemsControl.ScrollDir.Down)
				{
					this.m_scrollRect.verticalNormalizedPosition -= num2;
					if (this.m_scrollRect.verticalNormalizedPosition < 0f)
					{
						this.m_scrollRect.verticalNormalizedPosition = 0f;
						this.m_scrollDir = VirtualizingItemsControl.ScrollDir.None;
					}
				}
				else if (this.m_scrollDir == VirtualizingItemsControl.ScrollDir.Left)
				{
					this.m_scrollRect.horizontalNormalizedPosition -= num4;
					if (this.m_scrollRect.horizontalNormalizedPosition < 0f)
					{
						this.m_scrollRect.horizontalNormalizedPosition = 0f;
						this.m_scrollDir = VirtualizingItemsControl.ScrollDir.None;
					}
				}
				if (this.m_scrollDir == VirtualizingItemsControl.ScrollDir.Right)
				{
					this.m_scrollRect.horizontalNormalizedPosition += num4;
					if (this.m_scrollRect.horizontalNormalizedPosition > 1f)
					{
						this.m_scrollRect.horizontalNormalizedPosition = 1f;
						this.m_scrollDir = VirtualizingItemsControl.ScrollDir.None;
					}
				}
			}
			if (this.InputProvider != null && this.IsSelected)
			{
				if (this.InputProvider.IsDeleteButtonDown && this.CanRemove)
				{
					this.RemoveSelectedItems();
				}
				else if (this.InputProvider.IsSelectAllButtonDown && this.InputProvider.IsFunctionalButtonPressed && this.CanSelectAll)
				{
					this.SelectedItems = this.m_scrollRect.Items;
				}
				else if (this.InputProvider.IsSubmitButtonDown)
				{
					this.IsFocused = !this.IsFocused;
					if (!this.IsFocused && this.Submit != null)
					{
						this.Submit(this, EventArgs.Empty);
					}
				}
				else if (this.InputProvider.IsCancelButtonDown)
				{
					this.SelectedItems = this.m_selectionBackup;
					this.IsFocused = false;
					if (this.Cancel != null)
					{
						this.Cancel(this, EventArgs.Empty);
					}
				}
				if (this.IsFocused)
				{
					if (!Mathf.Approximately(this.InputProvider.VerticalAxis, 0f))
					{
						if (this.InputProvider.IsVerticalButtonDown)
						{
							this.m_repeater = new Repeater(Time.time, 0f, 0.4f, 0.05f, delegate()
							{
								float verticalAxis = this.InputProvider.VerticalAxis;
								if (verticalAxis < 0f)
								{
									if (this.m_scrollRect.Index + this.m_scrollRect.VisibleItemsCount > this.SelectedIndex + this.m_scrollRect.ContainersPerGroup)
									{
										if (this.SelectedIndex + this.m_scrollRect.ContainersPerGroup < this.m_scrollRect.ItemsCount)
										{
											this.SelectedIndex += this.m_scrollRect.ContainersPerGroup;
										}
									}
									else
									{
										this.m_scrollRect.Index += this.m_scrollRect.ContainersPerGroup;
										if (this.m_scrollRect.Index + this.m_scrollRect.VisibleItemsCount > this.SelectedIndex + this.m_scrollRect.ContainersPerGroup && this.SelectedIndex + this.m_scrollRect.ContainersPerGroup < this.m_scrollRect.ItemsCount)
										{
											this.SelectedIndex += this.m_scrollRect.ContainersPerGroup;
										}
									}
								}
								else if (verticalAxis > 0f)
								{
									if (this.m_scrollRect.Index < this.SelectedIndex - (this.m_scrollRect.ContainersPerGroup - 1))
									{
										this.SelectedIndex -= this.m_scrollRect.ContainersPerGroup;
									}
									else
									{
										this.m_scrollRect.Index -= this.m_scrollRect.ContainersPerGroup;
										if (this.m_scrollRect.Index < this.SelectedIndex - (this.m_scrollRect.ContainersPerGroup - 1))
										{
											this.SelectedIndex -= this.m_scrollRect.ContainersPerGroup;
										}
									}
								}
							});
						}
						if (this.m_repeater != null)
						{
							this.m_repeater.Repeat(Time.time);
						}
					}
					else if (!Mathf.Approximately(this.InputProvider.HorizontalAxis, 0f))
					{
						if (this.m_scrollRect.UseGrid)
						{
							if (this.InputProvider.IsHorizontalButtonDown)
							{
								this.m_repeater = new Repeater(Time.time, 0f, 0.4f, 0.05f, delegate()
								{
									float horizontalAxis = this.InputProvider.HorizontalAxis;
									if (horizontalAxis > 0f)
									{
										if (this.m_scrollRect.Index + this.m_scrollRect.VisibleItemsCount > this.SelectedIndex + this.m_scrollRect.ContainersPerGroup)
										{
											this.SelectedIndex++;
										}
										else
										{
											this.m_scrollRect.Index++;
											if (this.m_scrollRect.Index + this.m_scrollRect.VisibleItemsCount > this.SelectedIndex + 1 && this.SelectedIndex < this.m_scrollRect.ItemsCount - 1)
											{
												this.SelectedIndex++;
											}
										}
									}
									else if (horizontalAxis < 0f)
									{
										if (this.m_scrollRect.Index < this.SelectedIndex)
										{
											this.SelectedIndex--;
										}
										else
										{
											this.m_scrollRect.Index--;
											if (this.m_scrollRect.Index < this.SelectedIndex)
											{
												this.SelectedIndex--;
											}
										}
									}
								});
							}
							this.m_repeater.Repeat(Time.time);
						}
					}
					else
					{
						this.m_repeater = null;
					}
				}
			}
			if (this.m_prevCanDrag != this.CanDrag)
			{
				this.OnCanDragChanged();
				this.m_prevCanDrag = this.CanDrag;
			}
			this.UpdateOverride();
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00012FEC File Offset: 0x000113EC
		protected override void OnEnable()
		{
			base.OnEnable();
			VirtualizingItemContainer.Selected += this.OnItemSelected;
			VirtualizingItemContainer.Unselected += this.OnItemUnselected;
			VirtualizingItemContainer.PointerUp += this.OnItemPointerUp;
			VirtualizingItemContainer.PointerDown += this.OnItemPointerDown;
			VirtualizingItemContainer.PointerEnter += this.OnItemPointerEnter;
			VirtualizingItemContainer.PointerExit += this.OnItemPointerExit;
			VirtualizingItemContainer.DoubleClick += this.OnItemDoubleClick;
			VirtualizingItemContainer.Click += this.OnItemClick;
			VirtualizingItemContainer.BeginEdit += this.OnItemBeginEdit;
			VirtualizingItemContainer.EndEdit += this.OnItemEndEdit;
			VirtualizingItemContainer.BeginDrag += this.OnItemBeginDrag;
			VirtualizingItemContainer.Drag += this.OnItemDrag;
			VirtualizingItemContainer.Drop += this.OnItemDrop;
			VirtualizingItemContainer.EndDrag += this.OnItemEndDrag;
			this.OnEnableOverride();
		}

		// Token: 0x06000329 RID: 809 RVA: 0x000130F8 File Offset: 0x000114F8
		protected override void OnDisable()
		{
			base.OnDisable();
			VirtualizingItemContainer.Selected -= this.OnItemSelected;
			VirtualizingItemContainer.Unselected -= this.OnItemUnselected;
			VirtualizingItemContainer.PointerUp -= this.OnItemPointerUp;
			VirtualizingItemContainer.PointerDown -= this.OnItemPointerDown;
			VirtualizingItemContainer.PointerEnter -= this.OnItemPointerEnter;
			VirtualizingItemContainer.PointerExit -= this.OnItemPointerExit;
			VirtualizingItemContainer.DoubleClick -= this.OnItemDoubleClick;
			VirtualizingItemContainer.Click -= this.OnItemClick;
			VirtualizingItemContainer.BeginEdit -= this.OnItemBeginEdit;
			VirtualizingItemContainer.EndEdit -= this.OnItemEndEdit;
			VirtualizingItemContainer.BeginDrag -= this.OnItemBeginDrag;
			VirtualizingItemContainer.Drag -= this.OnItemDrag;
			VirtualizingItemContainer.Drop -= this.OnItemDrop;
			VirtualizingItemContainer.EndDrag -= this.OnItemEndDrag;
			this.IsFocused = false;
			this.OnDisableOverride();
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00013208 File Offset: 0x00011608
		protected override void OnDestroy()
		{
			base.OnDestroy();
			if (this.m_scrollRect != null)
			{
				this.m_scrollRect.ItemDataBinding -= this.OnScrollRectItemDataBinding;
			}
			if (this.m_rtcListener != null)
			{
				this.m_rtcListener.RectTransformChanged -= this.OnViewportRectTransformChanged;
			}
			if (this.m_pointerEventsListener != null)
			{
				this.m_pointerEventsListener.PointerEnter -= this.OnViewportPointerEnter;
				this.m_pointerEventsListener.PointerExit -= this.OnViewportPointerExit;
			}
			this.OnDestroyOverride();
		}

		// Token: 0x0600032B RID: 811 RVA: 0x000132B0 File Offset: 0x000116B0
		protected virtual void AwakeOverride()
		{
		}

		// Token: 0x0600032C RID: 812 RVA: 0x000132B2 File Offset: 0x000116B2
		protected virtual void StartOverride()
		{
		}

		// Token: 0x0600032D RID: 813 RVA: 0x000132B4 File Offset: 0x000116B4
		protected virtual void UpdateOverride()
		{
		}

		// Token: 0x0600032E RID: 814 RVA: 0x000132B6 File Offset: 0x000116B6
		protected virtual void OnEnableOverride()
		{
		}

		// Token: 0x0600032F RID: 815 RVA: 0x000132B8 File Offset: 0x000116B8
		protected virtual void OnDisableOverride()
		{
		}

		// Token: 0x06000330 RID: 816 RVA: 0x000132BA File Offset: 0x000116BA
		protected virtual void OnDestroyOverride()
		{
		}

		// Token: 0x06000331 RID: 817 RVA: 0x000132BC File Offset: 0x000116BC
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
			VirtualizingItemContainer.Unselected -= this.OnItemUnselected;
			if (this.InputProvider.IsFunctionalButtonPressed)
			{
				IList list = (this.m_selectedItems == null) ? new List<object>() : this.m_selectedItems.ToList<object>();
				list.Add(((VirtualizingItemContainer)sender).Item);
				this.SelectedItems = list;
			}
			else if (this.InputProvider.IsFunctional2ButtonPressed)
			{
				this.SelectRange((VirtualizingItemContainer)sender);
			}
			else
			{
				this.SelectedIndex = this.IndexOf(((VirtualizingItemContainer)sender).Item);
			}
			VirtualizingItemContainer.Unselected += this.OnItemUnselected;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0001338C File Offset: 0x0001178C
		private void SelectRange(VirtualizingItemContainer itemContainer)
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
					list.Add(this.m_scrollRect.Items[i]);
				}
				for (int j = num + 1; j <= num4; j++)
				{
					list.Add(this.m_scrollRect.Items[j]);
				}
				this.SelectedItems = list;
			}
			else
			{
				this.SelectedIndex = this.IndexOf(itemContainer.Item);
			}
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00013480 File Offset: 0x00011880
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
			list.Remove(((VirtualizingItemContainer)sender).Item);
			this.SelectedItems = list;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x000134E0 File Offset: 0x000118E0
		private void TryToSelect(VirtualizingItemContainer sender)
		{
			if (this.InputProvider.IsFunctional2ButtonPressed)
			{
				if (sender.Item != null)
				{
					this.SelectRange(sender);
				}
			}
			else if (this.InputProvider.IsFunctionalButtonPressed)
			{
				if (sender.Item != null)
				{
					sender.IsSelected = !sender.IsSelected;
				}
			}
			else if (sender.Item != null)
			{
				sender.IsSelected = true;
			}
			else if (this.CanUnselectAll)
			{
				this.SelectedIndex = -1;
			}
			this.m_eventSystem.SetSelectedGameObject(base.gameObject);
			this.IsFocused = true;
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00013584 File Offset: 0x00011984
		private void OnItemPointerDown(VirtualizingItemContainer sender, PointerEventData eventData)
		{
			if (!this.CanHandleEvent(sender))
			{
				return;
			}
			if (this.m_externalDragOperation)
			{
				return;
			}
			this.m_dropMarker.SetTarget(null);
			this.m_dragItems = null;
			this.m_dragItemsData = null;
			this.m_isDropInProgress = false;
			if (sender.IsSelected && eventData.button == PointerEventData.InputButton.Right)
			{
				return;
			}
			if (!this.SelectOnPointerUp)
			{
				this.TryToSelect(sender);
			}
		}

		// Token: 0x06000336 RID: 822 RVA: 0x000135F8 File Offset: 0x000119F8
		private void OnItemPointerUp(VirtualizingItemContainer sender, PointerEventData eventData)
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
			if (sender.IsSelected && eventData.button == PointerEventData.InputButton.Right)
			{
				return;
			}
			if (this.SelectOnPointerUp && !this.m_isDropInProgress)
			{
				this.TryToSelect(sender);
			}
			if (!this.InputProvider.IsFunctional2ButtonPressed && !this.InputProvider.IsFunctionalButtonPressed && this.m_selectedItems != null && this.m_selectedItems.Count > 1)
			{
				if (this.SelectedItem == sender.Item)
				{
					this.SelectedItem = null;
				}
				this.SelectedItem = sender.Item;
			}
		}

		// Token: 0x06000337 RID: 823 RVA: 0x000136C0 File Offset: 0x00011AC0
		private void OnItemPointerEnter(VirtualizingItemContainer sender, PointerEventData eventData)
		{
			if (!this.CanHandleEvent(sender))
			{
				return;
			}
			this.m_dropTarget = sender;
			ItemDropCancelArgs itemDropCancelArgs = null;
			if (this.m_dragItems != null && this.m_dragItems.Length > 0)
			{
				itemDropCancelArgs = new ItemDropCancelArgs(this.m_dragItemsData, this.m_dropTarget.Item, this.m_dropMarker.Action, this.m_externalDragOperation, eventData);
				if (this.ItemDragEnter != null)
				{
					this.ItemDragEnter(this, itemDropCancelArgs);
				}
			}
			if ((this.m_dragItems != null || this.m_externalDragOperation) && this.m_scrollDir == VirtualizingItemsControl.ScrollDir.None)
			{
				if (itemDropCancelArgs == null || !itemDropCancelArgs.Cancel)
				{
					this.m_dropMarker.SetTarget(this.m_dropTarget);
				}
				else
				{
					this.m_dropMarker.SetTarget(null);
				}
			}
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00013794 File Offset: 0x00011B94
		private void OnItemPointerExit(VirtualizingItemContainer sender, PointerEventData eventData)
		{
			if (!this.CanHandleEvent(sender))
			{
				return;
			}
			this.m_dropTarget = null;
			if (this.m_dragItems != null || this.m_externalDragOperation)
			{
				this.m_dropMarker.SetTarget(null);
			}
			if (this.m_dragItems != null && this.ItemDragExit != null)
			{
				this.ItemDragExit(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00013800 File Offset: 0x00011C00
		private void OnItemDoubleClick(VirtualizingItemContainer sender, PointerEventData eventData)
		{
			if (!this.CanHandleEvent(sender))
			{
				return;
			}
			if (sender.Item != null && this.ItemDoubleClick != null)
			{
				this.ItemDoubleClick(this, new ItemArgs(new object[]
				{
					sender.Item
				}, eventData));
			}
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00013854 File Offset: 0x00011C54
		private void OnItemClick(VirtualizingItemContainer sender, PointerEventData eventData)
		{
			if (!this.CanHandleEvent(sender))
			{
				return;
			}
			if (sender.Item == null)
			{
				if (this.Click != null)
				{
					this.Click(this, new PointerEventArgs(eventData));
				}
			}
			else if (this.ItemClick != null)
			{
				this.ItemClick(this, new ItemArgs(new object[]
				{
					sender.Item
				}, eventData));
			}
		}

		// Token: 0x0600033B RID: 827 RVA: 0x000138C8 File Offset: 0x00011CC8
		private void OnItemBeginDrag(VirtualizingItemContainer sender, PointerEventData eventData)
		{
			if (!this.CanHandleEvent(sender))
			{
				return;
			}
			if (this.m_selectedItems != null && this.m_selectedItems.Contains(sender.Item))
			{
				this.m_dragItems = this.GetDragItems();
			}
			else
			{
				this.m_dragItems = new ItemContainerData[]
				{
					this.m_itemContainerData[sender.Item]
				};
			}
			this.m_dragItemsData = (from di in this.m_dragItems
			select di.Item).ToArray<object>();
			if (this.ItemBeginDrag != null)
			{
				this.ItemBeginDrag(this, new ItemArgs(this.m_dragItemsData, eventData));
			}
			if (this.m_dropTarget != null)
			{
				ItemDropCancelArgs itemDropCancelArgs = new ItemDropCancelArgs(this.m_dragItemsData, this.m_dropTarget.Item, this.m_dropMarker.Action, this.m_externalDragOperation, eventData);
				if (this.ItemDragEnter != null)
				{
					this.ItemDragEnter(this, itemDropCancelArgs);
				}
				if (!itemDropCancelArgs.Cancel)
				{
					this.m_dropMarker.SetTarget(this.m_dropTarget);
					this.m_dropMarker.SetPosition(eventData.position);
				}
			}
			else
			{
				this.m_dropMarker.SetTarget(null);
			}
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00013A1C File Offset: 0x00011E1C
		private void OnItemDrag(VirtualizingItemContainer sender, PointerEventData eventData)
		{
			if (!this.CanHandleEvent(sender))
			{
				return;
			}
			this.ExternalItemDrag(eventData.position);
			if (this.ItemDrag != null)
			{
				this.ItemDrag(this, new ItemArgs(this.m_dragItemsData, eventData));
			}
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
					if (vector.y > 0f && vector.y < this.ScrollMargin.y)
					{
						this.m_scrollDir = VirtualizingItemsControl.ScrollDir.Up;
						this.m_dropMarker.SetTarget(null);
					}
					else if (vector.y < -height && vector.y > -(height + this.ScrollMargin.w))
					{
						this.m_scrollDir = VirtualizingItemsControl.ScrollDir.Down;
						this.m_dropMarker.SetTarget(null);
					}
					else if (vector.x < 0f && vector.x >= -this.ScrollMargin.x)
					{
						this.m_scrollDir = VirtualizingItemsControl.ScrollDir.Left;
					}
					else if (vector.x >= width && vector.x < width + this.ScrollMargin.z)
					{
						this.m_scrollDir = VirtualizingItemsControl.ScrollDir.Right;
					}
					else
					{
						this.m_scrollDir = VirtualizingItemsControl.ScrollDir.None;
					}
				}
			}
			else
			{
				this.m_scrollDir = VirtualizingItemsControl.ScrollDir.None;
			}
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00013BF0 File Offset: 0x00011FF0
		private void OnItemDrop(VirtualizingItemContainer sender, PointerEventData eventData)
		{
			if (!this.CanHandleEvent(sender))
			{
				return;
			}
			if (this.m_dragItems == null)
			{
				return;
			}
			this.m_isDropInProgress = true;
			try
			{
				if (this.m_dropTarget != null && this.CanDrop(this.m_dragItems, this.GetItemContainerData(this.m_dropTarget.Item)))
				{
					bool flag = false;
					if (this.ItemBeginDrop != null)
					{
						ItemDropCancelArgs itemDropCancelArgs = new ItemDropCancelArgs(this.m_dragItemsData, this.m_dropTarget.Item, this.m_dropMarker.Action, false, eventData);
						if (this.ItemBeginDrop != null)
						{
							this.ItemBeginDrop(this, itemDropCancelArgs);
							flag = itemDropCancelArgs.Cancel;
						}
					}
					if (!flag)
					{
						object[] array = (this.m_dragItems == null) ? null : this.m_dragItemsData;
						object obj = (!(this.m_dropTarget != null)) ? null : this.m_dropTarget.Item;
						ItemContainerData itemContainerData = this.GetItemContainerData(obj);
						this.Drop(this.m_dragItems, itemContainerData, this.m_dropMarker.Action);
						if (this.ItemDrop != null && array != null && obj != null && this.m_dropMarker != null)
						{
							this.ItemDrop(this, new ItemDropArgs(array, obj, this.m_dropMarker.Action, false, eventData));
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

		// Token: 0x0600033E RID: 830 RVA: 0x00013D7C File Offset: 0x0001217C
		private void OnItemEndDrag(VirtualizingItemContainer sender, PointerEventData eventData)
		{
			if (this.m_dropTarget != null)
			{
				this.OnItemDrop(sender, eventData);
			}
			else
			{
				if (!this.CanHandleEvent(sender))
				{
					return;
				}
				this.RaiseEndDrag(eventData);
			}
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00013DB0 File Offset: 0x000121B0
		private void RaiseEndDrag(PointerEventData eventData)
		{
			if (this.m_dragItems != null)
			{
				if (this.ItemEndDrag != null)
				{
					this.ItemEndDrag(this, new ItemArgs(this.m_dragItemsData, eventData));
				}
				this.m_dropMarker.SetTarget(null);
				this.m_dragItems = null;
				this.m_dragItemsData = null;
				this.m_scrollDir = VirtualizingItemsControl.ScrollDir.None;
			}
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00013E0C File Offset: 0x0001220C
		private void OnViewportRectTransformChanged()
		{
			if (this.ExpandChildrenHeight || this.ExpandChildrenWidth)
			{
				Rect rect = this.m_scrollRect.viewport.rect;
				if (rect.width != this.m_width || rect.height != this.m_height)
				{
					this.m_width = rect.width;
					this.m_height = rect.height;
					this.SetContainersSize();
				}
			}
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00013E84 File Offset: 0x00012284
		private void OnViewportPointerEnter(object sender, PointerEventArgs e)
		{
			if (this.PointerEnter != null)
			{
				this.PointerEnter(this, e);
			}
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00013E9E File Offset: 0x0001229E
		private void OnViewportPointerExit(object sender, PointerEventArgs e)
		{
			if (this.PointerExit != null)
			{
				this.PointerExit(this, e);
			}
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00013EB8 File Offset: 0x000122B8
		private void SetContainersSize()
		{
			this.m_scrollRect.ForEachContainer(delegate(RectTransform c)
			{
				VirtualizingItemContainer component = c.GetComponent<VirtualizingItemContainer>();
				this.UpdateContainerSize(component);
			});
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00013ED4 File Offset: 0x000122D4
		public void UpdateContainerSize(VirtualizingItemContainer container)
		{
			if (container != null && container.LayoutElement != null)
			{
				if (this.ExpandChildrenWidth)
				{
					container.LayoutElement.minWidth = this.m_width;
				}
				if (this.ExpandChildrenHeight)
				{
					container.LayoutElement.minHeight = this.m_height;
				}
			}
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00013F36 File Offset: 0x00012336
		private void OnCanDragChanged()
		{
			this.m_scrollRect.ForEachContainer(delegate(RectTransform c)
			{
				VirtualizingItemContainer component = c.GetComponent<VirtualizingItemContainer>();
				if (component != null)
				{
					component.CanDrag = this.CanDrag;
				}
			});
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00013F50 File Offset: 0x00012350
		protected bool CanHandleEvent(object sender)
		{
			if (sender is ItemContainerData)
			{
				ItemContainerData itemContainerData = (ItemContainerData)sender;
				ItemContainerData itemContainerData2;
				return this.m_itemContainerData.TryGetValue(itemContainerData.Item, out itemContainerData2) && itemContainerData == itemContainerData2;
			}
			VirtualizingItemContainer virtualizingItemContainer = sender as VirtualizingItemContainer;
			return virtualizingItemContainer && this.m_scrollRect.IsParentOf(virtualizingItemContainer.transform);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00013FB4 File Offset: 0x000123B4
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
			if (this.m_scrollRect.ItemsCount > 0)
			{
				RectTransform rectTransform = this.m_scrollRect.LastContainer();
				if (rectTransform != null)
				{
					this.m_dropTarget = rectTransform.GetComponent<VirtualizingItemContainer>();
					if (this.m_dropTarget.Item == this.m_scrollRect.Items[this.m_scrollRect.Items.Count - 1])
					{
						this.m_dropMarker.Action = ItemDropAction.SetNextSibling;
					}
					else
					{
						this.m_dropTarget = null;
					}
				}
			}
			if (this.m_dropTarget != null)
			{
				this.m_isDropInProgress = true;
				try
				{
					ItemContainerData itemContainerData = this.GetItemContainerData(this.m_dropTarget.Item);
					if (this.CanDrop(this.m_dragItems, itemContainerData))
					{
						this.Drop(this.m_dragItems, itemContainerData, this.m_dropMarker.Action);
						if (this.ItemDrop != null)
						{
							this.ItemDrop(this, new ItemDropArgs(this.m_dragItemsData, this.m_dropTarget.Item, this.m_dropMarker.Action, false, eventData));
						}
					}
					this.m_dropMarker.SetTarget(null);
					this.m_dragItems = null;
					this.m_dragItemsData = null;
				}
				finally
				{
					this.m_isDropInProgress = false;
				}
			}
		}

		// Token: 0x06000348 RID: 840 RVA: 0x00014184 File Offset: 0x00012584
		public override void OnPointerDown(PointerEventData eventData)
		{
			base.OnPointerDown(eventData);
			if (this.CanUnselectAll)
			{
				this.SelectedIndex = -1;
			}
			this.m_eventSystem.SetSelectedGameObject(base.gameObject);
			this.IsFocused = true;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x000141B7 File Offset: 0x000125B7
		protected virtual void OnItemBeginEdit(object sender, EventArgs e)
		{
		}

		// Token: 0x0600034A RID: 842 RVA: 0x000141B9 File Offset: 0x000125B9
		protected virtual void OnItemEndEdit(object sender, EventArgs e)
		{
		}

		// Token: 0x0600034B RID: 843 RVA: 0x000141BC File Offset: 0x000125BC
		public virtual void DataBindItem(object item)
		{
			VirtualizingItemContainer itemContainer = this.GetItemContainer(item);
			if (itemContainer != null)
			{
				this.DataBindItem(item, itemContainer);
			}
		}

		// Token: 0x0600034C RID: 844 RVA: 0x000141E5 File Offset: 0x000125E5
		public virtual void DataBindItem(object item, VirtualizingItemContainer itemContainer)
		{
		}

		// Token: 0x0600034D RID: 845 RVA: 0x000141E8 File Offset: 0x000125E8
		private void OnScrollRectItemDataBinding(RectTransform container, object item)
		{
			VirtualizingItemContainer component = container.GetComponent<VirtualizingItemContainer>();
			component.Item = item;
			if (item != null)
			{
				this.m_selectionLocked = true;
				ItemContainerData itemContainerData = this.m_itemContainerData[item];
				itemContainerData.IsSelected = this.IsItemSelected(item);
				component.IsSelected = itemContainerData.IsSelected;
				component.CanDrag = this.CanDrag;
				this.m_selectionLocked = false;
			}
			this.DataBindItem(item, component);
			if (this.m_scrollRect.ItemsCount == 1)
			{
				this.SetContainersSize();
			}
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00014268 File Offset: 0x00012668
		public int IndexOf(object obj)
		{
			if (this.m_scrollRect.Items == null)
			{
				return -1;
			}
			if (obj == null)
			{
				return -1;
			}
			return this.m_scrollRect.Items.IndexOf(obj);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00014298 File Offset: 0x00012698
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
			if (num < newIndex)
			{
				this.m_scrollRect.SetNextSibling(this.GetItemAt(newIndex), obj);
			}
			else
			{
				this.m_scrollRect.SetPrevSibling(this.GetItemAt(newIndex), obj);
			}
		}

		// Token: 0x06000350 RID: 848 RVA: 0x000142FC File Offset: 0x000126FC
		public ItemContainerData LastItemContainerData()
		{
			if (this.m_scrollRect.Items == null || this.m_scrollRect.ItemsCount == 0)
			{
				return null;
			}
			return this.GetItemContainerData(this.m_scrollRect.Items[this.m_scrollRect.ItemsCount - 1]);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00014350 File Offset: 0x00012750
		public VirtualizingItemContainer GetItemContainer(object item)
		{
			if (item == null)
			{
				return null;
			}
			RectTransform container = this.m_scrollRect.GetContainer(item);
			if (container == null)
			{
				return null;
			}
			return container.GetComponent<VirtualizingItemContainer>();
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00014388 File Offset: 0x00012788
		public ItemContainerData GetItemContainerData(object item)
		{
			if (item == null)
			{
				return null;
			}
			ItemContainerData result = null;
			this.m_itemContainerData.TryGetValue(item, out result);
			return result;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x000143B0 File Offset: 0x000127B0
		public ItemContainerData GetItemContainerData(int siblingIndex)
		{
			if (siblingIndex < 0 || this.m_scrollRect.Items.Count <= siblingIndex)
			{
				return null;
			}
			object key = this.m_scrollRect.Items[siblingIndex];
			return this.m_itemContainerData[key];
		}

		// Token: 0x06000354 RID: 852 RVA: 0x000143FA File Offset: 0x000127FA
		protected virtual bool CanDrop(ItemContainerData[] dragItems, ItemContainerData dropTarget)
		{
			return dropTarget == null || (dragItems != null && !dragItems.Contains(dropTarget.Item));
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00014420 File Offset: 0x00012820
		protected ItemContainerData[] GetDragItems()
		{
			ItemContainerData[] array = new ItemContainerData[this.m_selectedItems.Count];
			if (this.m_selectedItems != null)
			{
				for (int i = 0; i < this.m_selectedItems.Count; i++)
				{
					array[i] = this.m_itemContainerData[this.m_selectedItems[i]];
				}
			}
			return (from di in array
			orderby this.IndexOf(di.Item)
			select di).ToArray<ItemContainerData>();
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00014498 File Offset: 0x00012898
		protected virtual void Drop(ItemContainerData[] dragItems, ItemContainerData dropTargetData, ItemDropAction action)
		{
			if (action == ItemDropAction.SetPrevSibling)
			{
				foreach (ItemContainerData prevSibling in dragItems)
				{
					this.SetPrevSiblingInternal(dropTargetData, prevSibling);
				}
			}
			else if (action == ItemDropAction.SetNextSibling)
			{
				foreach (ItemContainerData nextSibling in dragItems)
				{
					this.SetNextSiblingInternal(dropTargetData, nextSibling);
				}
			}
			this.UpdateSelectedItemIndex();
		}

		// Token: 0x06000357 RID: 855 RVA: 0x000144FE File Offset: 0x000128FE
		protected virtual void SetNextSiblingInternal(ItemContainerData sibling, ItemContainerData nextSibling)
		{
			this.m_scrollRect.SetNextSibling(sibling.Item, nextSibling.Item);
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00014517 File Offset: 0x00012917
		protected virtual void SetPrevSiblingInternal(ItemContainerData sibling, ItemContainerData prevSibling)
		{
			this.m_scrollRect.SetPrevSibling(sibling.Item, prevSibling.Item);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00014530 File Offset: 0x00012930
		protected void UpdateSelectedItemIndex()
		{
			this.m_selectedIndex = this.IndexOf(this.SelectedItem);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00014544 File Offset: 0x00012944
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
			if (this.m_dragItems != null || this.m_externalDragOperation)
			{
				if (this.m_scrollDir == VirtualizingItemsControl.ScrollDir.None)
				{
					this.m_dropMarker.SetTarget(this.m_dropTarget);
				}
				else
				{
					this.m_dropMarker.SetTarget(null);
				}
			}
		}

		// Token: 0x0600035B RID: 859 RVA: 0x000145B9 File Offset: 0x000129B9
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

		// Token: 0x0600035C RID: 860 RVA: 0x000145E9 File Offset: 0x000129E9
		public void ExternalItemDrop()
		{
			if (!this.CanDrag)
			{
				return;
			}
			this.m_externalDragOperation = false;
			this.m_dropMarker.SetTarget(null);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0001460C File Offset: 0x00012A0C
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
			this.DestroyItems(array, true);
			if (this.ItemsRemoved != null)
			{
				this.ItemsRemoved(this, new ItemsRemovedArgs(array));
			}
		}

		// Token: 0x0600035E RID: 862 RVA: 0x000146B0 File Offset: 0x00012AB0
		protected virtual void DestroyItems(object[] items, bool unselect)
		{
			if (unselect)
			{
				foreach (object item2 in items)
				{
					if (this.m_selectedItems != null && this.m_selectedItems.Contains(item2))
					{
						this.m_selectedItems.Remove(item2);
						this.m_selectedItemsHS.Remove(item2);
						if (this.m_selectedItems.Count == 0)
						{
							this.m_selectedIndex = -1;
						}
						else
						{
							this.m_selectedIndex = this.IndexOf(this.m_selectedItems[0]);
						}
					}
				}
			}
			this.m_scrollRect.RemoveItems((from item in items
			select this.IndexOf(item)).ToArray<int>(), true);
			foreach (object key in items)
			{
				this.m_itemContainerData.Remove(key);
			}
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0001478D File Offset: 0x00012B8D
		public ItemContainerData Add(object item)
		{
			return this.Insert(this.m_scrollRect.ItemsCount, item);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x000147A4 File Offset: 0x00012BA4
		public virtual ItemContainerData Insert(int index, object item)
		{
			if (this.m_itemContainerData.ContainsKey(item))
			{
				return this.m_itemContainerData[item];
			}
			ItemContainerData itemContainerData = this.InstantiateItemContainerData(item);
			this.m_itemContainerData.Add(item, itemContainerData);
			this.m_scrollRect.InsertItem(index, item, true);
			return itemContainerData;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x000147F4 File Offset: 0x00012BF4
		public void SetNextSibling(object sibling, object nextSibling)
		{
			ItemContainerData itemContainerData = this.GetItemContainerData(sibling);
			if (itemContainerData == null)
			{
				return;
			}
			ItemContainerData itemContainerData2 = this.GetItemContainerData(nextSibling);
			if (itemContainerData2 == null)
			{
				return;
			}
			this.Drop(new ItemContainerData[]
			{
				itemContainerData2
			}, itemContainerData, ItemDropAction.SetNextSibling);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00014834 File Offset: 0x00012C34
		public void SetPrevSibling(object sibling, object prevSibling)
		{
			ItemContainerData itemContainerData = this.GetItemContainerData(sibling);
			if (itemContainerData == null)
			{
				return;
			}
			ItemContainerData itemContainerData2 = this.GetItemContainerData(prevSibling);
			if (itemContainerData2 == null)
			{
				return;
			}
			this.Drop(new ItemContainerData[]
			{
				itemContainerData2
			}, itemContainerData, ItemDropAction.SetPrevSibling);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00014874 File Offset: 0x00012C74
		protected virtual void Remove(object[] items)
		{
			items = (from item in items
			where this.m_scrollRect.Items.Contains(item)
			select item).ToArray<object>();
			if (items.Length == 0)
			{
				return;
			}
			if (this.ItemsRemoving != null)
			{
				ItemsCancelArgs itemsCancelArgs = new ItemsCancelArgs(items.ToList<object>());
				this.ItemsRemoving(this, itemsCancelArgs);
				if (itemsCancelArgs.Items == null)
				{
					items = new object[0];
				}
				else
				{
					items = itemsCancelArgs.Items.ToArray();
				}
			}
			if (items.Length == 0)
			{
				return;
			}
			this.DestroyItems(items, true);
			if (this.ItemsRemoved != null)
			{
				this.ItemsRemoved(this, new ItemsRemovedArgs(items));
			}
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0001491A File Offset: 0x00012D1A
		public virtual void Remove(object item)
		{
			this.Remove(new object[]
			{
				item
			});
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0001492C File Offset: 0x00012D2C
		public object GetItemAt(int index)
		{
			if (index < 0 || index >= this.m_scrollRect.Items.Count)
			{
				return null;
			}
			return this.m_scrollRect.Items[index];
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0001495E File Offset: 0x00012D5E
		public override void OnSelect(BaseEventData eventData)
		{
			base.OnSelect(eventData);
			this.IsSelected = true;
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0001496E File Offset: 0x00012D6E
		public override void OnDeselect(BaseEventData eventData)
		{
			base.OnDeselect(eventData);
			this.IsSelected = false;
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0001497E File Offset: 0x00012D7E
		public void OnUpdateSelected(BaseEventData eventData)
		{
			if (!this.IsFocused)
			{
				return;
			}
			eventData.Use();
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00014992 File Offset: 0x00012D92
		public void OnUpdateFocused(BaseEventData eventData)
		{
			if (!this.IsFocused)
			{
				return;
			}
			eventData.Use();
		}

		// Token: 0x0600036A RID: 874 RVA: 0x000149A6 File Offset: 0x00012DA6
		void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
		{
			if (this.Click != null)
			{
				this.Click(this, new PointerEventArgs(eventData));
			}
		}

		// Token: 0x0600036B RID: 875 RVA: 0x000149C5 File Offset: 0x00012DC5
		public void Refresh()
		{
			this.m_scrollRect.Refresh();
		}

		// Token: 0x040002E7 RID: 743
		[SerializeField]
		public EventSystem m_eventSystem;

		// Token: 0x040002E8 RID: 744
		public InputProvider InputProvider;

		// Token: 0x040002E9 RID: 745
		public bool SelectOnPointerUp;

		// Token: 0x040002EA RID: 746
		public bool CanUnselectAll = true;

		// Token: 0x040002EB RID: 747
		public bool CanSelectAll = true;

		// Token: 0x040002EC RID: 748
		public bool CanEdit = true;

		// Token: 0x040002ED RID: 749
		public bool CanRemove = true;

		// Token: 0x040002EE RID: 750
		private bool m_prevCanDrag;

		// Token: 0x040002EF RID: 751
		public bool CanDrag = true;

		// Token: 0x040002F0 RID: 752
		public bool CanReorder = true;

		// Token: 0x040002F1 RID: 753
		public bool ExpandChildrenWidth = true;

		// Token: 0x040002F2 RID: 754
		public bool ExpandChildrenHeight;

		// Token: 0x040002F3 RID: 755
		private bool m_isDropInProgress;

		// Token: 0x040002F4 RID: 756
		private List<object> m_selectionBackup;

		// Token: 0x040002F5 RID: 757
		private bool m_isFocused;

		// Token: 0x040002F6 RID: 758
		private bool m_isSelected;

		// Token: 0x040002F7 RID: 759
		private Canvas m_canvas;

		// Token: 0x040002F8 RID: 760
		public Camera Camera;

		// Token: 0x040002F9 RID: 761
		public float ScrollSpeed = 100f;

		// Token: 0x040002FA RID: 762
		public Vector4 ScrollMargin = new Vector4(float.MaxValue, float.MaxValue, float.MaxValue, float.MaxValue);

		// Token: 0x040002FB RID: 763
		private VirtualizingItemsControl.ScrollDir m_scrollDir;

		// Token: 0x040002FC RID: 764
		private PointerEnterExitListener m_pointerEventsListener;

		// Token: 0x040002FD RID: 765
		private RectTransformChangeListener m_rtcListener;

		// Token: 0x040002FE RID: 766
		private float m_width;

		// Token: 0x040002FF RID: 767
		private float m_height;

		// Token: 0x04000300 RID: 768
		private VirtualizingItemDropMarker m_dropMarker;

		// Token: 0x04000301 RID: 769
		private Repeater m_repeater;

		// Token: 0x04000302 RID: 770
		private bool m_externalDragOperation;

		// Token: 0x04000303 RID: 771
		private VirtualizingItemContainer m_dropTarget;

		// Token: 0x04000304 RID: 772
		private ItemContainerData[] m_dragItems;

		// Token: 0x04000305 RID: 773
		private object[] m_dragItemsData;

		// Token: 0x04000306 RID: 774
		private bool m_selectionLocked;

		// Token: 0x04000307 RID: 775
		private List<object> m_selectedItems;

		// Token: 0x04000308 RID: 776
		private HashSet<object> m_selectedItemsHS;

		// Token: 0x04000309 RID: 777
		private int m_selectedIndex = -1;

		// Token: 0x0400030A RID: 778
		private Dictionary<object, ItemContainerData> m_itemContainerData = new Dictionary<object, ItemContainerData>();

		// Token: 0x0400030B RID: 779
		private VirtualizingScrollRect m_scrollRect;

		// Token: 0x020000A2 RID: 162
		private enum ScrollDir
		{
			// Token: 0x0400030E RID: 782
			None,
			// Token: 0x0400030F RID: 783
			Up,
			// Token: 0x04000310 RID: 784
			Down,
			// Token: 0x04000311 RID: 785
			Left,
			// Token: 0x04000312 RID: 786
			Right
		}
	}
}
