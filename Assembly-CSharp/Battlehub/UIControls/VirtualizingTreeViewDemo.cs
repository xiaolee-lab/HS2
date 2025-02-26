using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Battlehub.UIControls
{
	// Token: 0x02000097 RID: 151
	public class VirtualizingTreeViewDemo : MonoBehaviour
	{
		// Token: 0x06000261 RID: 609 RVA: 0x0000FE5C File Offset: 0x0000E25C
		public static bool IsPrefab(Transform This)
		{
			if (Application.isEditor && !Application.isPlaying)
			{
				throw new InvalidOperationException("Does not work in edit mode");
			}
			return This.gameObject.scene.buildIndex < 0;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000FEA0 File Offset: 0x0000E2A0
		private void Awake()
		{
			for (int i = 0; i < this.GameObjectNum; i++)
			{
				GameObject gameObject = new GameObject();
				gameObject.name = "Instantiated GameObject" + i;
			}
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000FEE0 File Offset: 0x0000E2E0
		private void Start()
		{
			this.TreeView.ItemDataBinding += this.OnItemDataBinding;
			this.TreeView.SelectionChanged += this.OnSelectionChanged;
			this.TreeView.ItemsRemoved += this.OnItemsRemoved;
			this.TreeView.ItemExpanding += this.OnItemExpanding;
			this.TreeView.ItemBeginDrag += this.OnItemBeginDrag;
			this.TreeView.ItemDrop += this.OnItemDrop;
			this.TreeView.ItemBeginDrop += this.OnItemBeginDrop;
			this.TreeView.ItemEndDrag += this.OnItemEndDrag;
			GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
			this.TreeView.Items = rootGameObjects;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000FFC0 File Offset: 0x0000E3C0
		private void OnDestroy()
		{
			this.TreeView.ItemDataBinding -= this.OnItemDataBinding;
			this.TreeView.SelectionChanged -= this.OnSelectionChanged;
			this.TreeView.ItemsRemoved -= this.OnItemsRemoved;
			this.TreeView.ItemExpanding -= this.OnItemExpanding;
			this.TreeView.ItemBeginDrag -= this.OnItemBeginDrag;
			this.TreeView.ItemBeginDrop -= this.OnItemBeginDrop;
			this.TreeView.ItemDrop -= this.OnItemDrop;
			this.TreeView.ItemEndDrag -= this.OnItemEndDrag;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00010088 File Offset: 0x0000E488
		private void OnItemExpanding(object sender, VirtualizingItemExpandingArgs e)
		{
			GameObject gameObject = (GameObject)e.Item;
			if (gameObject.transform.childCount > 0)
			{
				List<GameObject> list = new List<GameObject>();
				for (int i = 0; i < gameObject.transform.childCount; i++)
				{
					GameObject gameObject2 = gameObject.transform.GetChild(i).gameObject;
					list.Add(gameObject2);
				}
				e.Children = list;
			}
		}

		// Token: 0x06000266 RID: 614 RVA: 0x000100F4 File Offset: 0x0000E4F4
		private void OnSelectionChanged(object sender, SelectionChangedArgs e)
		{
		}

		// Token: 0x06000267 RID: 615 RVA: 0x000100F8 File Offset: 0x0000E4F8
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

		// Token: 0x06000268 RID: 616 RVA: 0x00010140 File Offset: 0x0000E540
		private void OnItemDataBinding(object sender, VirtualizingTreeViewItemDataBindingArgs e)
		{
			GameObject gameObject = e.Item as GameObject;
			if (gameObject != null)
			{
				Text componentInChildren = e.ItemPresenter.GetComponentInChildren<Text>(true);
				componentInChildren.text = gameObject.name;
				Image image = e.ItemPresenter.GetComponentsInChildren<Image>()[4];
				image.sprite = Resources.Load<Sprite>("IconNew");
				e.HasChildren = (gameObject.transform.childCount > 0);
			}
		}

		// Token: 0x06000269 RID: 617 RVA: 0x000101B0 File Offset: 0x0000E5B0
		private void OnItemBeginDrag(object sender, ItemArgs e)
		{
		}

		// Token: 0x0600026A RID: 618 RVA: 0x000101B2 File Offset: 0x0000E5B2
		private void OnItemBeginDrop(object sender, ItemDropCancelArgs e)
		{
		}

		// Token: 0x0600026B RID: 619 RVA: 0x000101B4 File Offset: 0x0000E5B4
		private void OnItemEndDrag(object sender, ItemArgs e)
		{
		}

		// Token: 0x0600026C RID: 620 RVA: 0x000101B8 File Offset: 0x0000E5B8
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

		// Token: 0x0400029E RID: 670
		public VirtualizingTreeView TreeView;

		// Token: 0x0400029F RID: 671
		public int GameObjectNum = 1000;
	}
}
