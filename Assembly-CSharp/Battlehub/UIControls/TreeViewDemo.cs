using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Battlehub.UIControls
{
	// Token: 0x0200007D RID: 125
	public class TreeViewDemo : MonoBehaviour
	{
		// Token: 0x06000109 RID: 265 RVA: 0x0000A1E8 File Offset: 0x000085E8
		public static bool IsPrefab(Transform This)
		{
			if (Application.isEditor && !Application.isPlaying)
			{
				throw new InvalidOperationException("Does not work in edit mode");
			}
			return This.gameObject.scene.buildIndex < 0;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0000A22C File Offset: 0x0000862C
		private void Start()
		{
			if (!this.TreeView)
			{
				return;
			}
			IEnumerable<GameObject> items = from go in Resources.FindObjectsOfTypeAll<GameObject>()
			where !TreeViewDemo.IsPrefab(go.transform) && go.transform.parent == null
			select go into t
			orderby t.transform.GetSiblingIndex()
			select t;
			this.TreeView.ItemDataBinding += this.OnItemDataBinding;
			this.TreeView.SelectionChanged += this.OnSelectionChanged;
			this.TreeView.ItemsRemoved += this.OnItemsRemoved;
			this.TreeView.ItemExpanding += this.OnItemExpanding;
			this.TreeView.ItemBeginDrag += this.OnItemBeginDrag;
			this.TreeView.ItemDrop += this.OnItemDrop;
			this.TreeView.ItemBeginDrop += this.OnItemBeginDrop;
			this.TreeView.ItemEndDrag += this.OnItemEndDrag;
			this.TreeView.Items = items;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000A358 File Offset: 0x00008758
		private void OnItemBeginDrop(object sender, ItemDropCancelArgs e)
		{
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000A35C File Offset: 0x0000875C
		private void OnDestroy()
		{
			if (!this.TreeView)
			{
				return;
			}
			this.TreeView.ItemDataBinding -= this.OnItemDataBinding;
			this.TreeView.SelectionChanged -= this.OnSelectionChanged;
			this.TreeView.ItemsRemoved -= this.OnItemsRemoved;
			this.TreeView.ItemExpanding -= this.OnItemExpanding;
			this.TreeView.ItemBeginDrag -= this.OnItemBeginDrag;
			this.TreeView.ItemBeginDrop -= this.OnItemBeginDrop;
			this.TreeView.ItemDrop -= this.OnItemDrop;
			this.TreeView.ItemEndDrag -= this.OnItemEndDrag;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000A434 File Offset: 0x00008834
		private void OnItemExpanding(object sender, ItemExpandingArgs e)
		{
			GameObject gameObject = (GameObject)e.Item;
			if (gameObject.transform.childCount > 0)
			{
				GameObject[] array = new GameObject[gameObject.transform.childCount];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = gameObject.transform.GetChild(i).gameObject;
				}
				e.Children = array;
			}
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000A49E File Offset: 0x0000889E
		private void OnSelectionChanged(object sender, SelectionChangedArgs e)
		{
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000A4A0 File Offset: 0x000088A0
		private void OnItemsRemoved(object sender, ItemsRemovedArgs e)
		{
			for (int i = 0; i < e.Items.Length; i++)
			{
				GameObject gameObject = (GameObject)e.Items[i];
				if (gameObject != null)
				{
					UnityEngine.Object.Destroy(gameObject);
				}
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000A4E8 File Offset: 0x000088E8
		private void OnItemDataBinding(object sender, TreeViewItemDataBindingArgs e)
		{
			GameObject gameObject = e.Item as GameObject;
			if (gameObject != null)
			{
				Text componentInChildren = e.ItemPresenter.GetComponentInChildren<Text>(true);
				componentInChildren.text = gameObject.name;
				Image image = e.ItemPresenter.GetComponentsInChildren<Image>()[4];
				image.sprite = Resources.Load<Sprite>("cube");
				if (gameObject.name != "TreeView")
				{
					e.HasChildren = (gameObject.transform.childCount > 0);
				}
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000A56D File Offset: 0x0000896D
		private void OnItemBeginDrag(object sender, ItemArgs e)
		{
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000A570 File Offset: 0x00008970
		private void OnItemDrop(object sender, ItemDropArgs e)
		{
			if (e.DropTarget == null)
			{
				return;
			}
			Transform transform = ((GameObject)e.DropTarget).transform;
			if (e.Action == ItemDropAction.SetLastChild)
			{
				for (int i = 0; i < e.DragItems.Length; i++)
				{
					Transform transform2 = ((GameObject)e.DragItems[i]).transform;
					transform2.SetParent(transform, true);
					transform2.SetAsLastSibling();
				}
			}
			else if (e.Action == ItemDropAction.SetNextSibling)
			{
				for (int j = e.DragItems.Length - 1; j >= 0; j--)
				{
					Transform transform3 = ((GameObject)e.DragItems[j]).transform;
					int siblingIndex = transform.GetSiblingIndex();
					if (transform3.parent != transform.parent)
					{
						transform3.SetParent(transform.parent, true);
						transform3.SetSiblingIndex(siblingIndex + 1);
					}
					else
					{
						int siblingIndex2 = transform3.GetSiblingIndex();
						if (siblingIndex < siblingIndex2)
						{
							transform3.SetSiblingIndex(siblingIndex + 1);
						}
						else
						{
							transform3.SetSiblingIndex(siblingIndex);
						}
					}
				}
			}
			else if (e.Action == ItemDropAction.SetPrevSibling)
			{
				for (int k = 0; k < e.DragItems.Length; k++)
				{
					Transform transform4 = ((GameObject)e.DragItems[k]).transform;
					if (transform4.parent != transform.parent)
					{
						transform4.SetParent(transform.parent, true);
					}
					int siblingIndex3 = transform.GetSiblingIndex();
					int siblingIndex4 = transform4.GetSiblingIndex();
					if (siblingIndex3 > siblingIndex4)
					{
						transform4.SetSiblingIndex(siblingIndex3 - 1);
					}
					else
					{
						transform4.SetSiblingIndex(siblingIndex3);
					}
				}
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000A723 File Offset: 0x00008B23
		private void OnItemEndDrag(object sender, ItemArgs e)
		{
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000A728 File Offset: 0x00008B28
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.J))
			{
				this.TreeView.SelectedItems = this.TreeView.Items.OfType<object>().Take(5).ToArray<object>();
			}
			else if (Input.GetKeyDown(KeyCode.K))
			{
				this.TreeView.SelectedItem = null;
			}
		}

		// Token: 0x04000202 RID: 514
		public TreeView TreeView;
	}
}
