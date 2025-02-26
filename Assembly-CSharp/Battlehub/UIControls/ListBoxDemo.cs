using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Battlehub.UIControls
{
	// Token: 0x0200007B RID: 123
	public class ListBoxDemo : MonoBehaviour
	{
		// Token: 0x060000F9 RID: 249 RVA: 0x00009CF8 File Offset: 0x000080F8
		public static bool IsPrefab(Transform This)
		{
			if (Application.isEditor && !Application.isPlaying)
			{
				throw new InvalidOperationException("Does not work in edit mode");
			}
			return This.gameObject.scene.buildIndex < 0;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00009D3C File Offset: 0x0000813C
		private void Start()
		{
			if (!this.ListBox)
			{
				return;
			}
			this.ListBox.ItemDataBinding += this.OnItemDataBinding;
			this.ListBox.SelectionChanged += this.OnSelectionChanged;
			this.ListBox.ItemsRemoved += this.OnItemsRemoved;
			this.ListBox.ItemBeginDrag += this.OnItemBeginDrag;
			this.ListBox.ItemDrop += this.OnItemDrop;
			this.ListBox.ItemEndDrag += this.OnItemEndDrag;
			IEnumerable<GameObject> source = from go in Resources.FindObjectsOfTypeAll<GameObject>()
			where !ListBoxDemo.IsPrefab(go.transform) && go.transform.parent == null
			select go;
			this.ListBox.Items = from t in source
			orderby t.transform.GetSiblingIndex()
			select t;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00009E3C File Offset: 0x0000823C
		private void OnDestroy()
		{
			if (!this.ListBox)
			{
				return;
			}
			this.ListBox.ItemDataBinding -= this.OnItemDataBinding;
			this.ListBox.SelectionChanged -= this.OnSelectionChanged;
			this.ListBox.ItemsRemoved -= this.OnItemsRemoved;
			this.ListBox.ItemBeginDrag -= this.OnItemBeginDrag;
			this.ListBox.ItemDrop -= this.OnItemDrop;
			this.ListBox.ItemEndDrag -= this.OnItemEndDrag;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00009EE4 File Offset: 0x000082E4
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

		// Token: 0x060000FD RID: 253 RVA: 0x00009F4E File Offset: 0x0000834E
		private void OnSelectionChanged(object sender, SelectionChangedArgs e)
		{
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00009F50 File Offset: 0x00008350
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

		// Token: 0x060000FF RID: 255 RVA: 0x00009F98 File Offset: 0x00008398
		private void OnItemDataBinding(object sender, ItemDataBindingArgs e)
		{
			GameObject gameObject = e.Item as GameObject;
			if (gameObject != null)
			{
				Text componentInChildren = e.ItemPresenter.GetComponentInChildren<Text>(true);
				componentInChildren.text = gameObject.name;
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00009FD6 File Offset: 0x000083D6
		private void OnItemBeginDrag(object sender, ItemArgs e)
		{
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00009FD8 File Offset: 0x000083D8
		private void OnItemDrop(object sender, ItemDropArgs e)
		{
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
				for (int j = 0; j < e.DragItems.Length; j++)
				{
					Transform transform3 = ((GameObject)e.DragItems[j]).transform;
					if (transform3.parent != transform.parent)
					{
						transform3.SetParent(transform.parent, true);
					}
					int siblingIndex = transform.GetSiblingIndex();
					transform3.SetSiblingIndex(siblingIndex + 1);
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
					int siblingIndex2 = transform.GetSiblingIndex();
					transform4.SetSiblingIndex(siblingIndex2);
				}
			}
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000A12B File Offset: 0x0000852B
		private void OnItemEndDrag(object sender, ItemArgs e)
		{
		}

		// Token: 0x040001FD RID: 509
		public ListBox ListBox;
	}
}
