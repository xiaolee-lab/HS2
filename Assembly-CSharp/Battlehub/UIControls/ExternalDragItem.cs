using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Battlehub.UIControls
{
	// Token: 0x0200007A RID: 122
	public class ExternalDragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IDropHandler, IEndDragHandler, IEventSystemHandler
	{
		// Token: 0x060000F4 RID: 244 RVA: 0x00009B5A File Offset: 0x00007F5A
		void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
		{
			this.TreeView.ExternalBeginDrag(eventData.position);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00009B72 File Offset: 0x00007F72
		void IDragHandler.OnDrag(PointerEventData eventData)
		{
			this.TreeView.ExternalItemDrag(eventData.position);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00009B8A File Offset: 0x00007F8A
		void IDropHandler.OnDrop(PointerEventData eventData)
		{
			if (this.TreeView.DropTarget != null)
			{
				this.TreeView.AddChild(this.TreeView.DropTarget, new GameObject());
			}
			this.TreeView.ExternalItemDrop();
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00009BC4 File Offset: 0x00007FC4
		void IEndDragHandler.OnEndDrag(PointerEventData eventData)
		{
			if (this.TreeView.DropTarget != null)
			{
				GameObject gameObject = (GameObject)this.TreeView.DropTarget;
				GameObject gameObject2 = new GameObject();
				TreeViewItem treeViewItem = (TreeViewItem)this.TreeView.GetItemContainer(this.TreeView.DropTarget);
				if (this.TreeView.DropAction == ItemDropAction.SetLastChild)
				{
					gameObject2.transform.SetParent(gameObject.transform);
					this.TreeView.AddChild(this.TreeView.DropTarget, gameObject2);
					treeViewItem.CanExpand = true;
					treeViewItem.IsExpanded = true;
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
					TreeViewItem treeViewItem2 = (TreeViewItem)this.TreeView.Insert(num, gameObject2);
					treeViewItem2.Parent = treeViewItem.Parent;
				}
			}
			this.TreeView.ExternalItemDrop();
		}

		// Token: 0x040001FC RID: 508
		public TreeView TreeView;
	}
}
