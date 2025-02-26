using System;
using UnityEngine;

namespace Battlehub.UIControls
{
	// Token: 0x02000081 RID: 129
	[RequireComponent(typeof(RectTransform))]
	public class ItemDropMarker : MonoBehaviour
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000150 RID: 336 RVA: 0x0000B2AD File Offset: 0x000096AD
		protected Canvas ParentCanvas
		{
			get
			{
				return this.m_parentCanvas;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000151 RID: 337 RVA: 0x0000B2B5 File Offset: 0x000096B5
		// (set) Token: 0x06000152 RID: 338 RVA: 0x0000B2BD File Offset: 0x000096BD
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

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000153 RID: 339 RVA: 0x0000B2C6 File Offset: 0x000096C6
		public RectTransform RectTransform
		{
			get
			{
				return this.m_rectTransform;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000154 RID: 340 RVA: 0x0000B2CE File Offset: 0x000096CE
		protected ItemContainer Item
		{
			get
			{
				return this.m_item;
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000B2D6 File Offset: 0x000096D6
		private void Awake()
		{
			this.m_rectTransform = base.GetComponent<RectTransform>();
			this.SiblingGraphics.SetActive(true);
			this.m_parentCanvas = base.GetComponentInParent<Canvas>();
			this.m_itemsControl = base.GetComponentInParent<ItemsControl>();
			this.AwakeOverride();
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000B30E File Offset: 0x0000970E
		protected virtual void AwakeOverride()
		{
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000B310 File Offset: 0x00009710
		public virtual void SetTraget(ItemContainer item)
		{
			base.gameObject.SetActive(item != null);
			this.m_item = item;
			if (this.m_item == null)
			{
				this.Action = ItemDropAction.None;
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000B344 File Offset: 0x00009744
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

		// Token: 0x04000224 RID: 548
		private Canvas m_parentCanvas;

		// Token: 0x04000225 RID: 549
		private ItemsControl m_itemsControl;

		// Token: 0x04000226 RID: 550
		public GameObject SiblingGraphics;

		// Token: 0x04000227 RID: 551
		private ItemDropAction m_action;

		// Token: 0x04000228 RID: 552
		private RectTransform m_rectTransform;

		// Token: 0x04000229 RID: 553
		private ItemContainer m_item;
	}
}
