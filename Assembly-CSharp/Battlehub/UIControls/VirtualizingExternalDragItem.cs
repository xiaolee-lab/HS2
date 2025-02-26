using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Battlehub.UIControls
{
	// Token: 0x02000096 RID: 150
	public class VirtualizingExternalDragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IDropHandler, IEndDragHandler, IEventSystemHandler
	{
		// Token: 0x0600025C RID: 604 RVA: 0x0000FC7E File Offset: 0x0000E07E
		void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
		{
			this.TreeView.ExternalBeginDrag(eventData.position);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000FC96 File Offset: 0x0000E096
		void IDragHandler.OnDrag(PointerEventData eventData)
		{
			this.TreeView.ExternalItemDrag(eventData.position);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000FCAE File Offset: 0x0000E0AE
		void IDropHandler.OnDrop(PointerEventData eventData)
		{
			if (this.TreeView.DropTarget != null)
			{
				this.TreeView.AddChild(this.TreeView.DropTarget, new GameObject());
			}
			this.TreeView.ExternalItemDrop();
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000FCE8 File Offset: 0x0000E0E8
		void IEndDragHandler.OnEndDrag(PointerEventData eventData)
		{
			if (this.TreeView.DropTarget != null)
			{
				GameObject gameObject = (GameObject)this.TreeView.DropTarget;
				GameObject gameObject2 = new GameObject();
				VirtualizingTreeViewItem virtualizingTreeViewItem = (VirtualizingTreeViewItem)this.TreeView.GetItemContainer(this.TreeView.DropTarget);
				if (this.TreeView.DropAction == ItemDropAction.SetLastChild)
				{
					gameObject2.transform.SetParent(gameObject.transform);
					this.TreeView.AddChild(this.TreeView.DropTarget, gameObject2);
					virtualizingTreeViewItem.CanExpand = true;
					virtualizingTreeViewItem.IsExpanded = true;
				}
				else if (this.TreeView.DropAction != ItemDropAction.None)
				{
					int num;
					if (this.TreeView.DropAction == ItemDropAction.SetNextSibling)
					{
						num = this.TreeView.IndexOf(gameObject) + 1;
					}
					else
					{
						num = this.TreeView.IndexOf(gameObject);
					}
					gameObject2.transform.SetParent(gameObject.transform.parent);
					gameObject2.transform.SetSiblingIndex(num);
					TreeViewItemContainerData treeViewItemContainerData = (TreeViewItemContainerData)this.TreeView.Insert(num, gameObject2);
					VirtualizingTreeViewItem virtualizingTreeViewItem2 = (VirtualizingTreeViewItem)this.TreeView.GetItemContainer(gameObject2);
					if (virtualizingTreeViewItem2 != null)
					{
						virtualizingTreeViewItem2.Parent = virtualizingTreeViewItem.Parent;
					}
					else
					{
						treeViewItemContainerData.Parent = virtualizingTreeViewItem.Parent;
					}
				}
			}
			this.TreeView.ExternalItemDrop();
		}

		// Token: 0x0400029D RID: 669
		public VirtualizingTreeView TreeView;
	}
}
