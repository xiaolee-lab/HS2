using System;
using UnityEngine;

namespace Battlehub.UIControls
{
	// Token: 0x020000AE RID: 174
	[RequireComponent(typeof(RectTransform))]
	public class VirtualizingTreeViewDropMarker : VirtualizingItemDropMarker
	{
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x00017F20 File Offset: 0x00016320
		// (set) Token: 0x060003FB RID: 1019 RVA: 0x00017F28 File Offset: 0x00016328
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

		// Token: 0x060003FC RID: 1020 RVA: 0x00017F5C File Offset: 0x0001635C
		protected override void AwakeOverride()
		{
			base.AwakeOverride();
			this.m_treeView = base.GetComponentInParent<VirtualizingTreeView>();
			this.m_siblingGraphicsRectTransform = this.SiblingGraphics.GetComponent<RectTransform>();
			RectTransform rectTransform = (RectTransform)base.transform;
			VirtualizingScrollRect componentInChildren = this.m_treeView.GetComponentInChildren<VirtualizingScrollRect>();
			if (componentInChildren != null && componentInChildren.UseGrid)
			{
				this.m_useGrid = true;
				rectTransform.anchorMin = Vector2.zero;
				rectTransform.anchorMax = Vector2.zero;
			}
			else
			{
				rectTransform.anchorMin = Vector2.zero;
				rectTransform.anchorMax = new Vector2(1f, 0f);
			}
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00018000 File Offset: 0x00016400
		public override void SetTarget(VirtualizingItemContainer item)
		{
			base.SetTarget(item);
			if (item == null)
			{
				return;
			}
			VirtualizingTreeViewItem virtualizingTreeViewItem = (VirtualizingTreeViewItem)item;
			if (virtualizingTreeViewItem != null)
			{
				this.m_siblingGraphicsRectTransform.offsetMin = new Vector2(virtualizingTreeViewItem.Indent, this.m_siblingGraphicsRectTransform.offsetMin.y);
			}
			else
			{
				this.m_siblingGraphicsRectTransform.offsetMin = new Vector2(0f, this.m_siblingGraphicsRectTransform.offsetMin.y);
			}
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0001808C File Offset: 0x0001648C
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
			VirtualizingTreeViewItem virtualizingTreeViewItem = (VirtualizingTreeViewItem)base.Item;
			Vector2 sizeDelta = this.m_rectTransform.sizeDelta;
			sizeDelta.y = rectTransform.rect.height;
			if (this.m_useGrid)
			{
				sizeDelta.x = rectTransform.rect.width;
			}
			this.m_rectTransform.sizeDelta = sizeDelta;
			Camera cam = null;
			if (base.ParentCanvas.renderMode == RenderMode.WorldSpace || base.ParentCanvas.renderMode == RenderMode.ScreenSpaceCamera)
			{
				cam = this.m_treeView.Camera;
			}
			Vector2 vector;
			if (!this.m_treeView.CanReorder)
			{
				if (!virtualizingTreeViewItem.CanBeParent)
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
				else if (vector.y < rectTransform.rect.height / 4f - rectTransform.rect.height && !virtualizingTreeViewItem.HasChildren)
				{
					this.Action = ItemDropAction.SetNextSibling;
					base.RectTransform.position = rectTransform.TransformPoint(Vector3.down * rectTransform.rect.height);
				}
				else
				{
					if (!virtualizingTreeViewItem.CanBeParent)
					{
						return;
					}
					this.Action = ItemDropAction.SetLastChild;
					base.RectTransform.position = rectTransform.position;
				}
			}
		}

		// Token: 0x0400033B RID: 827
		private bool m_useGrid;

		// Token: 0x0400033C RID: 828
		private VirtualizingTreeView m_treeView;

		// Token: 0x0400033D RID: 829
		private RectTransform m_siblingGraphicsRectTransform;

		// Token: 0x0400033E RID: 830
		public GameObject ChildGraphics;
	}
}
