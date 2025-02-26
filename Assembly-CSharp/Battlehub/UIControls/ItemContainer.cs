using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Battlehub.UIControls
{
	// Token: 0x0200007F RID: 127
	[RequireComponent(typeof(RectTransform), typeof(LayoutElement))]
	public class ItemContainer : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IDropHandler, IEndDragHandler, IEventSystemHandler
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600011C RID: 284 RVA: 0x0000A7D4 File Offset: 0x00008BD4
		// (remove) Token: 0x0600011D RID: 285 RVA: 0x0000A808 File Offset: 0x00008C08
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event EventHandler Selected;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600011E RID: 286 RVA: 0x0000A83C File Offset: 0x00008C3C
		// (remove) Token: 0x0600011F RID: 287 RVA: 0x0000A870 File Offset: 0x00008C70
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event EventHandler Unselected;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000120 RID: 288 RVA: 0x0000A8A4 File Offset: 0x00008CA4
		// (remove) Token: 0x06000121 RID: 289 RVA: 0x0000A8D8 File Offset: 0x00008CD8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event ItemEventHandler PointerDown;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000122 RID: 290 RVA: 0x0000A90C File Offset: 0x00008D0C
		// (remove) Token: 0x06000123 RID: 291 RVA: 0x0000A940 File Offset: 0x00008D40
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event ItemEventHandler PointerUp;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000124 RID: 292 RVA: 0x0000A974 File Offset: 0x00008D74
		// (remove) Token: 0x06000125 RID: 293 RVA: 0x0000A9A8 File Offset: 0x00008DA8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event ItemEventHandler DoubleClick;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000126 RID: 294 RVA: 0x0000A9DC File Offset: 0x00008DDC
		// (remove) Token: 0x06000127 RID: 295 RVA: 0x0000AA10 File Offset: 0x00008E10
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event ItemEventHandler PointerEnter;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000128 RID: 296 RVA: 0x0000AA44 File Offset: 0x00008E44
		// (remove) Token: 0x06000129 RID: 297 RVA: 0x0000AA78 File Offset: 0x00008E78
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event ItemEventHandler PointerExit;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x0600012A RID: 298 RVA: 0x0000AAAC File Offset: 0x00008EAC
		// (remove) Token: 0x0600012B RID: 299 RVA: 0x0000AAE0 File Offset: 0x00008EE0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event ItemEventHandler BeginDrag;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x0600012C RID: 300 RVA: 0x0000AB14 File Offset: 0x00008F14
		// (remove) Token: 0x0600012D RID: 301 RVA: 0x0000AB48 File Offset: 0x00008F48
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event ItemEventHandler Drag;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x0600012E RID: 302 RVA: 0x0000AB7C File Offset: 0x00008F7C
		// (remove) Token: 0x0600012F RID: 303 RVA: 0x0000ABB0 File Offset: 0x00008FB0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event ItemEventHandler Drop;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000130 RID: 304 RVA: 0x0000ABE4 File Offset: 0x00008FE4
		// (remove) Token: 0x06000131 RID: 305 RVA: 0x0000AC18 File Offset: 0x00009018
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event ItemEventHandler EndDrag;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000132 RID: 306 RVA: 0x0000AC4C File Offset: 0x0000904C
		// (remove) Token: 0x06000133 RID: 307 RVA: 0x0000AC80 File Offset: 0x00009080
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event EventHandler BeginEdit;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06000134 RID: 308 RVA: 0x0000ACB4 File Offset: 0x000090B4
		// (remove) Token: 0x06000135 RID: 309 RVA: 0x0000ACE8 File Offset: 0x000090E8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event EventHandler EndEdit;

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000136 RID: 310 RVA: 0x0000AD1C File Offset: 0x0000911C
		public LayoutElement LayoutElement
		{
			get
			{
				return this.m_layoutElement;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000137 RID: 311 RVA: 0x0000AD24 File Offset: 0x00009124
		public RectTransform RectTransform
		{
			get
			{
				return this.m_rectTransform;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000138 RID: 312 RVA: 0x0000AD2C File Offset: 0x0000912C
		// (set) Token: 0x06000139 RID: 313 RVA: 0x0000AD34 File Offset: 0x00009134
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
						if (ItemContainer.Selected != null)
						{
							ItemContainer.Selected(this, EventArgs.Empty);
						}
					}
					else if (ItemContainer.Unselected != null)
					{
						ItemContainer.Unselected(this, EventArgs.Empty);
					}
				}
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600013A RID: 314 RVA: 0x0000AD98 File Offset: 0x00009198
		// (set) Token: 0x0600013B RID: 315 RVA: 0x0000ADA0 File Offset: 0x000091A0
		public bool IsEditing
		{
			get
			{
				return this.m_isEditing;
			}
			set
			{
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
						if (ItemContainer.BeginEdit != null)
						{
							ItemContainer.BeginEdit(this, EventArgs.Empty);
						}
					}
					else if (ItemContainer.EndEdit != null)
					{
						ItemContainer.EndEdit(this, EventArgs.Empty);
					}
				}
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600013C RID: 316 RVA: 0x0000AE7A File Offset: 0x0000927A
		private ItemsControl ItemsControl
		{
			get
			{
				if (this.m_itemsControl == null)
				{
					this.m_itemsControl = base.GetComponentInParent<ItemsControl>();
				}
				return this.m_itemsControl;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600013D RID: 317 RVA: 0x0000AE9F File Offset: 0x0000929F
		// (set) Token: 0x0600013E RID: 318 RVA: 0x0000AEA7 File Offset: 0x000092A7
		public object Item { get; set; }

		// Token: 0x0600013F RID: 319 RVA: 0x0000AEB0 File Offset: 0x000092B0
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

		// Token: 0x06000140 RID: 320 RVA: 0x0000AF15 File Offset: 0x00009315
		private void Start()
		{
			this.StartOverride();
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000AF1D File Offset: 0x0000931D
		private void OnDestroy()
		{
			base.StopAllCoroutines();
			this.m_coBeginEdit = null;
			this.OnDestroyOverride();
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0000AF32 File Offset: 0x00009332
		protected virtual void AwakeOverride()
		{
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000AF34 File Offset: 0x00009334
		protected virtual void StartOverride()
		{
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000AF36 File Offset: 0x00009336
		protected virtual void OnDestroyOverride()
		{
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000AF38 File Offset: 0x00009338
		public virtual void Clear()
		{
			this.m_isEditing = false;
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
			this.m_isSelected = false;
			this.Item = null;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000AFB8 File Offset: 0x000093B8
		private IEnumerator CoBeginEdit()
		{
			yield return new WaitForSeconds(0.5f);
			this.m_coBeginEdit = null;
			this.IsEditing = this.CanEdit;
			yield break;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000AFD4 File Offset: 0x000093D4
		void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
		{
			this.m_canBeginEdit = (this.m_isSelected && this.ItemsControl != null && this.ItemsControl.SelectedItemsCount == 1 && this.ItemsControl.CanEdit);
			if (ItemContainer.PointerDown != null)
			{
				ItemContainer.PointerDown(this, eventData);
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000B038 File Offset: 0x00009438
		void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
		{
			if (eventData.clickCount == 2)
			{
				if (ItemContainer.DoubleClick != null)
				{
					ItemContainer.DoubleClick(this, eventData);
				}
				if (this.CanEdit && this.m_coBeginEdit != null)
				{
					base.StopCoroutine(this.m_coBeginEdit);
					this.m_coBeginEdit = null;
				}
			}
			else
			{
				if (this.m_canBeginEdit && this.m_coBeginEdit == null)
				{
					this.m_coBeginEdit = this.CoBeginEdit();
					base.StartCoroutine(this.m_coBeginEdit);
				}
				if (ItemContainer.PointerUp != null)
				{
					ItemContainer.PointerUp(this, eventData);
				}
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000B0DC File Offset: 0x000094DC
		void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
		{
			if (!this.CanDrag)
			{
				ExecuteEvents.ExecuteHierarchy<IBeginDragHandler>(base.transform.parent.gameObject, eventData, ExecuteEvents.beginDragHandler);
				return;
			}
			this.m_canBeginEdit = false;
			if (ItemContainer.BeginDrag != null)
			{
				ItemContainer.BeginDrag(this, eventData);
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000B12E File Offset: 0x0000952E
		void IDropHandler.OnDrop(PointerEventData eventData)
		{
			if (ItemContainer.Drop != null)
			{
				ItemContainer.Drop(this, eventData);
			}
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000B146 File Offset: 0x00009546
		void IDragHandler.OnDrag(PointerEventData eventData)
		{
			if (!this.CanDrag)
			{
				ExecuteEvents.ExecuteHierarchy<IDragHandler>(base.transform.parent.gameObject, eventData, ExecuteEvents.dragHandler);
				return;
			}
			if (ItemContainer.Drag != null)
			{
				ItemContainer.Drag(this, eventData);
			}
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000B186 File Offset: 0x00009586
		void IEndDragHandler.OnEndDrag(PointerEventData eventData)
		{
			if (!this.CanDrag)
			{
				ExecuteEvents.ExecuteHierarchy<IEndDragHandler>(base.transform.parent.gameObject, eventData, ExecuteEvents.endDragHandler);
				return;
			}
			if (ItemContainer.EndDrag != null)
			{
				ItemContainer.EndDrag(this, eventData);
			}
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000B1C6 File Offset: 0x000095C6
		void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
		{
			if (ItemContainer.PointerEnter != null)
			{
				ItemContainer.PointerEnter(this, eventData);
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000B1DE File Offset: 0x000095DE
		void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
		{
			if (ItemContainer.PointerExit != null)
			{
				ItemContainer.PointerExit(this, eventData);
			}
		}

		// Token: 0x04000205 RID: 517
		public bool CanDrag = true;

		// Token: 0x04000206 RID: 518
		public bool CanEdit = true;

		// Token: 0x04000207 RID: 519
		public bool CanDrop = true;

		// Token: 0x04000215 RID: 533
		public GameObject ItemPresenter;

		// Token: 0x04000216 RID: 534
		public GameObject EditorPresenter;

		// Token: 0x04000217 RID: 535
		private LayoutElement m_layoutElement;

		// Token: 0x04000218 RID: 536
		private RectTransform m_rectTransform;

		// Token: 0x04000219 RID: 537
		protected bool m_isSelected;

		// Token: 0x0400021A RID: 538
		[SerializeField]
		private bool m_isEditing;

		// Token: 0x0400021B RID: 539
		private ItemsControl m_itemsControl;

		// Token: 0x0400021D RID: 541
		private bool m_canBeginEdit;

		// Token: 0x0400021E RID: 542
		private IEnumerator m_coBeginEdit;
	}
}
