using System;
using UnityEngine;

namespace Battlehub.UIControls
{
	// Token: 0x02000092 RID: 146
	[RequireComponent(typeof(RectTransform))]
	public class TreeViewDropMarker : ItemDropMarker
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600022B RID: 555 RVA: 0x0000EE89 File Offset: 0x0000D289
		// (set) Token: 0x0600022C RID: 556 RVA: 0x0000EE91 File Offset: 0x0000D291
		public override ItemDropAction Action
		{
			get
			{
				return base.Action;
			}
			set
			{
				base.Action = value;
				this.ChildGraphics.SetActive(base.Action == ItemDropAction.SetLastChild);
				this.SiblingGraphics.SetActive(base.Action != ItemDropAction.SetLastChild);
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000EEC5 File Offset: 0x0000D2C5
		protected override void AwakeOverride()
		{
			base.AwakeOverride();
			this.m_treeView = base.GetComponentInParent<TreeView>();
			this.m_siblingGraphicsRectTransform = this.SiblingGraphics.GetComponent<RectTransform>();
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000EEEC File Offset: 0x0000D2EC
		public override void SetTraget(ItemContainer item)
		{
			base.SetTraget(item);
			if (item == null)
			{
				return;
			}
			TreeViewItem treeViewItem = (TreeViewItem)item;
			if (treeViewItem != null)
			{
				this.m_siblingGraphicsRectTransform.offsetMin = new Vector2((float)treeViewItem.Indent, this.m_siblingGraphicsRectTransform.offsetMin.y);
			}
			else
			{
				this.m_siblingGraphicsRectTransform.offsetMin = new Vector2(0f, this.m_siblingGraphicsRectTransform.offsetMin.y);
			}
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000EF78 File Offset: 0x0000D378
		public override void SetPosition(Vector2 position)
		{
			if (base.Item == null)
			{
				return;
			}
			if (!this.m_treeView.CanReparent)
			{
				base.SetPosition(position);
				return;
			}
			RectTransform rectTransform = base.Item.RectTransform;
			TreeViewItem treeViewItem = (TreeViewItem)base.Item;
			Camera cam = null;
			if (base.ParentCanvas.renderMode == RenderMode.WorldSpace || base.ParentCanvas.renderMode == RenderMode.ScreenSpaceCamera)
			{
				cam = this.m_treeView.Camera;
			}
			Vector2 vector;
			if (!this.m_treeView.CanReorder)
			{
				if (!treeViewItem.CanDrop)
				{
					return;
				}
				this.Action = ItemDropAction.SetLastChild;
				base.RectTransform.position = rectTransform.position;
			}
			else if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, position, cam, out vector))
			{
				if (vector.y > -rectTransform.rect.height / 4f)
				{
					this.Action = ItemDropAction.SetPrevSibling;
					base.RectTransform.position = rectTransform.position;
				}
				else if (vector.y < rectTransform.rect.height / 4f - rectTransform.rect.height && !treeViewItem.HasChildren)
				{
					this.Action = ItemDropAction.SetNextSibling;
					base.RectTransform.position = rectTransform.position;
					base.RectTransform.localPosition = base.RectTransform.localPosition - new Vector3(0f, rectTransform.rect.height * base.ParentCanvas.scaleFactor, 0f);
				}
				else
				{
					if (!treeViewItem.CanDrop)
					{
						return;
					}
					this.Action = ItemDropAction.SetLastChild;
					base.RectTransform.position = rectTransform.position;
				}
			}
		}

		// Token: 0x0400028B RID: 651
		private TreeView m_treeView;

		// Token: 0x0400028C RID: 652
		private RectTransform m_siblingGraphicsRectTransform;

		// Token: 0x0400028D RID: 653
		public GameObject ChildGraphics;
	}
}
