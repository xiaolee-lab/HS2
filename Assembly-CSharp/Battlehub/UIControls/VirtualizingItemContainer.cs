using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Battlehub.UIControls
{
	// Token: 0x0200009C RID: 156
	[RequireComponent(typeof(RectTransform), typeof(LayoutElement))]
	public class VirtualizingItemContainer : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IDropHandler, IEndDragHandler, IPointerClickHandler, IEventSystemHandler
	{
		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06000293 RID: 659 RVA: 0x00010B6C File Offset: 0x0000EF6C
		// (remove) Token: 0x06000294 RID: 660 RVA: 0x00010BA0 File Offset: 0x0000EFA0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event EventHandler Selected;

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06000295 RID: 661 RVA: 0x00010BD4 File Offset: 0x0000EFD4
		// (remove) Token: 0x06000296 RID: 662 RVA: 0x00010C08 File Offset: 0x0000F008
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event EventHandler Unselected;

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x06000297 RID: 663 RVA: 0x00010C3C File Offset: 0x0000F03C
		// (remove) Token: 0x06000298 RID: 664 RVA: 0x00010C70 File Offset: 0x0000F070
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event VirtualizingItemEventHandler PointerDown;

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06000299 RID: 665 RVA: 0x00010CA4 File Offset: 0x0000F0A4
		// (remove) Token: 0x0600029A RID: 666 RVA: 0x00010CD8 File Offset: 0x0000F0D8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event VirtualizingItemEventHandler PointerUp;

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x0600029B RID: 667 RVA: 0x00010D0C File Offset: 0x0000F10C
		// (remove) Token: 0x0600029C RID: 668 RVA: 0x00010D40 File Offset: 0x0000F140
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event VirtualizingItemEventHandler DoubleClick;

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x0600029D RID: 669 RVA: 0x00010D74 File Offset: 0x0000F174
		// (remove) Token: 0x0600029E RID: 670 RVA: 0x00010DA8 File Offset: 0x0000F1A8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event VirtualizingItemEventHandler Click;

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x0600029F RID: 671 RVA: 0x00010DDC File Offset: 0x0000F1DC
		// (remove) Token: 0x060002A0 RID: 672 RVA: 0x00010E10 File Offset: 0x0000F210
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event VirtualizingItemEventHandler PointerEnter;

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x060002A1 RID: 673 RVA: 0x00010E44 File Offset: 0x0000F244
		// (remove) Token: 0x060002A2 RID: 674 RVA: 0x00010E78 File Offset: 0x0000F278
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event VirtualizingItemEventHandler PointerExit;

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x060002A3 RID: 675 RVA: 0x00010EAC File Offset: 0x0000F2AC
		// (remove) Token: 0x060002A4 RID: 676 RVA: 0x00010EE0 File Offset: 0x0000F2E0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event VirtualizingItemEventHandler BeginDrag;

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x060002A5 RID: 677 RVA: 0x00010F14 File Offset: 0x0000F314
		// (remove) Token: 0x060002A6 RID: 678 RVA: 0x00010F48 File Offset: 0x0000F348
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event VirtualizingItemEventHandler Drag;

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x060002A7 RID: 679 RVA: 0x00010F7C File Offset: 0x0000F37C
		// (remove) Token: 0x060002A8 RID: 680 RVA: 0x00010FB0 File Offset: 0x0000F3B0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event VirtualizingItemEventHandler Drop;

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x060002A9 RID: 681 RVA: 0x00010FE4 File Offset: 0x0000F3E4
		// (remove) Token: 0x060002AA RID: 682 RVA: 0x00011018 File Offset: 0x0000F418
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event VirtualizingItemEventHandler EndDrag;

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x060002AB RID: 683 RVA: 0x0001104C File Offset: 0x0000F44C
		// (remove) Token: 0x060002AC RID: 684 RVA: 0x00011080 File Offset: 0x0000F480
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event EventHandler BeginEdit;

		// Token: 0x1400002B RID: 43
		// (add) Token: 0x060002AD RID: 685 RVA: 0x000110B4 File Offset: 0x0000F4B4
		// (remove) Token: 0x060002AE RID: 686 RVA: 0x000110E8 File Offset: 0x0000F4E8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event EventHandler EndEdit;

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0001111C File Offset: 0x0000F51C
		public LayoutElement LayoutElement
		{
			get
			{
				return this.m_layoutElement;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x00011124 File Offset: 0x0000F524
		public RectTransform RectTransform
		{
			get
			{
				return this.m_rectTransform;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x0001112C File Offset: 0x0000F52C
		// (set) Token: 0x060002B2 RID: 690 RVA: 0x00011134 File Offset: 0x0000F534
		public virtual bool IsSelected
		{
			get
			{
				return this.m_isSelected;
			}
			set
			{
				if (this.m_isSelected != value)
				{
					this.m_isSelected = value;
					if (this.m_isSelected)
					{
						if (VirtualizingItemContainer.Selected != null)
						{
							VirtualizingItemContainer.Selected(this, EventArgs.Empty);
						}
					}
					else if (VirtualizingItemContainer.Unselected != null)
					{
						VirtualizingItemContainer.Unselected(this, EventArgs.Empty);
					}
				}
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x00011198 File Offset: 0x0000F598
		// (set) Token: 0x060002B4 RID: 692 RVA: 0x000111A0 File Offset: 0x0000F5A0
		public bool IsEditing
		{
			get
			{
				return this.m_isEditing;
			}
			set
			{
				if (this.Item == null)
				{
					return;
				}
				if (this.m_isEditing != value && this.m_isSelected)
				{
					this.m_isEditing = (value && this.m_isSelected);
					if (this.EditorPresenter != this.ItemPresenter)
					{
						if (this.EditorPresenter != null)
						{
							this.EditorPresenter.SetActive(this.m_isEditing);
						}
						if (this.ItemPresenter != null)
						{
							this.ItemPresenter.SetActive(!this.m_isEditing);
						}
					}
					if (this.m_isEditing)
					{
						if (VirtualizingItemContainer.BeginEdit != null)
						{
							VirtualizingItemContainer.BeginEdit(this, EventArgs.Empty);
						}
					}
					else if (VirtualizingItemContainer.EndEdit != null)
					{
						VirtualizingItemContainer.EndEdit(this, EventArgs.Empty);
					}
				}
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x00011288 File Offset: 0x0000F688
		protected VirtualizingItemsControl ItemsControl
		{
			get
			{
				if (this.m_itemsControl == null)
				{
					this.m_itemsControl = base.GetComponentInParent<VirtualizingItemsControl>();
					if (this.m_itemsControl == null)
					{
						Transform parent = base.transform.parent;
						while (parent != null)
						{
							this.m_itemsControl = parent.GetComponent<VirtualizingItemsControl>();
							if (this.m_itemsControl != null)
							{
								break;
							}
							parent = parent.parent;
						}
					}
				}
				return this.m_itemsControl;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0001130F File Offset: 0x0000F70F
		// (set) Token: 0x060002B7 RID: 695 RVA: 0x00011318 File Offset: 0x0000F718
		public virtual object Item
		{
			get
			{
				return this.m_item;
			}
			set
			{
				this.m_item = value;
				if (this.m_isEditing)
				{
					if (this.EditorPresenter != null)
					{
						this.EditorPresenter.SetActive(this.m_item != null);
					}
				}
				else if (this.ItemPresenter != null)
				{
					this.ItemPresenter.SetActive(this.m_item != null);
				}
				if (this.m_item == null)
				{
					this.IsSelected = false;
				}
			}
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x000113A0 File Offset: 0x0000F7A0
		private void Awake()
		{
			this.m_rectTransform = base.GetComponent<RectTransform>();
			this.m_layoutElement = base.GetComponent<LayoutElement>();
			if (this.ItemPresenter == null)
			{
				this.ItemPresenter = base.gameObject;
			}
			if (this.EditorPresenter == null)
			{
				this.EditorPresenter = base.gameObject;
			}
			this.AwakeOverride();
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00011405 File Offset: 0x0000F805
		private void Start()
		{
			this.StartOverride();
			this.ItemsControl.UpdateContainerSize(this);
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00011419 File Offset: 0x0000F819
		private void OnDestroy()
		{
			base.StopAllCoroutines();
			this.m_coBeginEdit = null;
			this.OnDestroyOverride();
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0001142E File Offset: 0x0000F82E
		protected virtual void AwakeOverride()
		{
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00011430 File Offset: 0x0000F830
		protected virtual void StartOverride()
		{
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00011432 File Offset: 0x0000F832
		protected virtual void OnDestroyOverride()
		{
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00011434 File Offset: 0x0000F834
		public virtual void Clear()
		{
			this.m_isEditing = false;
			if (this.EditorPresenter != this.ItemPresenter)
			{
				if (this.EditorPresenter != null)
				{
					this.EditorPresenter.SetActive(this.m_item != null && this.m_isEditing);
				}
				if (this.ItemPresenter != null)
				{
					this.ItemPresenter.SetActive(this.m_item != null && !this.m_isEditing);
				}
			}
			if (this.m_item == null)
			{
				this.IsSelected = false;
			}
		}

		// Token: 0x060002BF RID: 703 RVA: 0x000114D4 File Offset: 0x0000F8D4
		private IEnumerator CoBeginEdit()
		{
			yield return new WaitForSeconds(0.5f);
			this.m_coBeginEdit = null;
			if (this.m_itemsControl.IsSelected && this.m_itemsControl.IsFocused)
			{
				this.IsEditing = this.CanEdit;
			}
			yield break;
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x000114F0 File Offset: 0x0000F8F0
		void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
		{
			this.m_canBeginEdit = (this.m_isSelected && this.ItemsControl != null && this.ItemsControl.SelectedItemsCount == 1 && this.ItemsControl.CanEdit);
			if (!this.CanSelect)
			{
				return;
			}
			if (VirtualizingItemContainer.PointerDown != null)
			{
				VirtualizingItemContainer.PointerDown(this, eventData);
			}
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00011560 File Offset: 0x0000F960
		void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
		{
			if (eventData.clickCount == 2)
			{
				if (VirtualizingItemContainer.DoubleClick != null)
				{
					VirtualizingItemContainer.DoubleClick(this, eventData);
				}
				if (this.CanEdit && eventData.button == PointerEventData.InputButton.Left && this.m_coBeginEdit != null)
				{
					base.StopCoroutine(this.m_coBeginEdit);
					this.m_coBeginEdit = null;
				}
			}
			else
			{
				if (this.m_canBeginEdit && eventData.button == PointerEventData.InputButton.Left && this.m_coBeginEdit == null)
				{
					this.m_coBeginEdit = this.CoBeginEdit();
					base.StartCoroutine(this.m_coBeginEdit);
				}
				if (!this.CanSelect)
				{
					return;
				}
				if (VirtualizingItemContainer.PointerUp != null)
				{
					VirtualizingItemContainer.PointerUp(this, eventData);
				}
			}
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00011624 File Offset: 0x0000FA24
		void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
		{
			if (!this.CanDrag)
			{
				ExecuteEvents.ExecuteHierarchy<IBeginDragHandler>(base.transform.parent.gameObject, eventData, ExecuteEvents.beginDragHandler);
				return;
			}
			this.m_canBeginEdit = false;
			if (VirtualizingItemContainer.BeginDrag != null)
			{
				VirtualizingItemContainer.BeginDrag(this, eventData);
			}
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00011676 File Offset: 0x0000FA76
		void IDropHandler.OnDrop(PointerEventData eventData)
		{
			if (VirtualizingItemContainer.Drop != null)
			{
				VirtualizingItemContainer.Drop(this, eventData);
			}
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0001168E File Offset: 0x0000FA8E
		void IDragHandler.OnDrag(PointerEventData eventData)
		{
			if (!this.CanDrag)
			{
				ExecuteEvents.ExecuteHierarchy<IDragHandler>(base.transform.parent.gameObject, eventData, ExecuteEvents.dragHandler);
				return;
			}
			if (VirtualizingItemContainer.Drag != null)
			{
				VirtualizingItemContainer.Drag(this, eventData);
			}
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x000116CE File Offset: 0x0000FACE
		void IEndDragHandler.OnEndDrag(PointerEventData eventData)
		{
			if (!this.CanDrag)
			{
				ExecuteEvents.ExecuteHierarchy<IEndDragHandler>(base.transform.parent.gameObject, eventData, ExecuteEvents.endDragHandler);
				return;
			}
			if (VirtualizingItemContainer.EndDrag != null)
			{
				VirtualizingItemContainer.EndDrag(this, eventData);
			}
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0001170E File Offset: 0x0000FB0E
		void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
		{
			if (VirtualizingItemContainer.PointerEnter != null)
			{
				VirtualizingItemContainer.PointerEnter(this, eventData);
			}
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00011726 File Offset: 0x0000FB26
		void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
		{
			if (VirtualizingItemContainer.PointerExit != null)
			{
				VirtualizingItemContainer.PointerExit(this, eventData);
			}
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0001173E File Offset: 0x0000FB3E
		void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
		{
			if (VirtualizingItemContainer.Click != null)
			{
				VirtualizingItemContainer.Click(this, eventData);
			}
		}

		// Token: 0x040002AD RID: 685
		[HideInInspector]
		public bool CanDrag = true;

		// Token: 0x040002AE RID: 686
		[HideInInspector]
		public bool CanEdit = true;

		// Token: 0x040002AF RID: 687
		[HideInInspector]
		public bool CanBeParent = true;

		// Token: 0x040002B0 RID: 688
		[HideInInspector]
		public bool CanSelect = true;

		// Token: 0x040002BF RID: 703
		public GameObject ItemPresenter;

		// Token: 0x040002C0 RID: 704
		public GameObject EditorPresenter;

		// Token: 0x040002C1 RID: 705
		private LayoutElement m_layoutElement;

		// Token: 0x040002C2 RID: 706
		private RectTransform m_rectTransform;

		// Token: 0x040002C3 RID: 707
		protected bool m_isSelected;

		// Token: 0x040002C4 RID: 708
		private bool m_isEditing;

		// Token: 0x040002C5 RID: 709
		private VirtualizingItemsControl m_itemsControl;

		// Token: 0x040002C6 RID: 710
		private object m_item;

		// Token: 0x040002C7 RID: 711
		private bool m_canBeginEdit;

		// Token: 0x040002C8 RID: 712
		private IEnumerator m_coBeginEdit;
	}
}
