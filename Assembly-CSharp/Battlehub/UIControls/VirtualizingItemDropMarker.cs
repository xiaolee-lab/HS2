using System;
using UnityEngine;

namespace Battlehub.UIControls
{
	// Token: 0x0200009D RID: 157
	[RequireComponent(typeof(RectTransform))]
	public class VirtualizingItemDropMarker : MonoBehaviour
	{
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060002CA RID: 714 RVA: 0x00011837 File Offset: 0x0000FC37
		protected Canvas ParentCanvas
		{
			get
			{
				return this.m_parentCanvas;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060002CB RID: 715 RVA: 0x0001183F File Offset: 0x0000FC3F
		// (set) Token: 0x060002CC RID: 716 RVA: 0x00011847 File Offset: 0x0000FC47
		public virtual ItemDropAction Action
		{
			get
			{
				return this.m_action;
			}
			set
			{
				this.m_action = value;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060002CD RID: 717 RVA: 0x00011850 File Offset: 0x0000FC50
		public RectTransform RectTransform
		{
			get
			{
				return this.m_rectTransform;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060002CE RID: 718 RVA: 0x00011858 File Offset: 0x0000FC58
		protected VirtualizingItemContainer Item
		{
			get
			{
				return this.m_item;
			}
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00011860 File Offset: 0x0000FC60
		private void Awake()
		{
			this.m_rectTransform = base.GetComponent<RectTransform>();
			this.SiblingGraphics.SetActive(true);
			this.m_parentCanvas = base.GetComponentInParent<Canvas>();
			this.m_itemsControl = base.GetComponentInParent<VirtualizingItemsControl>();
			this.AwakeOverride();
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00011898 File Offset: 0x0000FC98
		protected virtual void AwakeOverride()
		{
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0001189A File Offset: 0x0000FC9A
		public virtual void SetTarget(VirtualizingItemContainer item)
		{
			base.gameObject.SetActive(item != null);
			this.m_item = item;
			if (this.m_item == null)
			{
				this.Action = ItemDropAction.None;
			}
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x000118D0 File Offset: 0x0000FCD0
		public virtual void SetPosition(Vector2 position)
		{
			if (this.m_item == null)
			{
				return;
			}
			if (!this.m_itemsControl.CanReorder)
			{
				return;
			}
			RectTransform rectTransform = this.Item.RectTransform;
			Vector2 sizeDelta = this.m_rectTransform.sizeDelta;
			sizeDelta.y = rectTransform.rect.height;
			this.m_rectTransform.sizeDelta = sizeDelta;
			Camera cam = null;
			if (this.ParentCanvas.renderMode == RenderMode.WorldSpace || this.ParentCanvas.renderMode == RenderMode.ScreenSpaceCamera)
			{
				cam = this.m_itemsControl.Camera;
			}
			Vector2 vector;
			if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, position, cam, out vector))
			{
				if (vector.y > -rectTransform.rect.height / 2f)
				{
					this.Action = ItemDropAction.SetPrevSibling;
					this.RectTransform.position = rectTransform.position;
				}
				else
				{
					this.Action = ItemDropAction.SetNextSibling;
					this.RectTransform.position = rectTransform.position;
					this.RectTransform.localPosition = this.RectTransform.localPosition - new Vector3(0f, rectTransform.rect.height * this.ParentCanvas.scaleFactor, 0f);
				}
			}
		}

		// Token: 0x040002C9 RID: 713
		private VirtualizingItemsControl m_itemsControl;

		// Token: 0x040002CA RID: 714
		private Canvas m_parentCanvas;

		// Token: 0x040002CB RID: 715
		public GameObject SiblingGraphics;

		// Token: 0x040002CC RID: 716
		private ItemDropAction m_action;

		// Token: 0x040002CD RID: 717
		protected RectTransform m_rectTransform;

		// Token: 0x040002CE RID: 718
		private VirtualizingItemContainer m_item;
	}
}
