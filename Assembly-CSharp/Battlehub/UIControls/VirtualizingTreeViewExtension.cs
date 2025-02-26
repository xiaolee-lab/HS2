using System;
using System.Collections;

namespace Battlehub.UIControls
{
	// Token: 0x020000AD RID: 173
	public static class VirtualizingTreeViewExtension
	{
		// Token: 0x060003F5 RID: 1013 RVA: 0x00017B9C File Offset: 0x00015F9C
		public static void ExpandTo<T>(this VirtualizingTreeView treeView, T item, Func<T, T> getParent)
		{
			if (item == null)
			{
				return;
			}
			if (treeView.GetItemContainerData(item) == null)
			{
				treeView.ExpandTo(getParent(item), getParent);
				treeView.Expand(item);
			}
			else
			{
				treeView.Expand(item);
			}
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00017BF8 File Offset: 0x00015FF8
		public static void ExpandChildren<T>(this VirtualizingTreeView treeView, T item, Func<T, IEnumerable> getChildren)
		{
			IEnumerable enumerable = getChildren(item);
			if (enumerable != null)
			{
				treeView.Expand(item);
				IEnumerator enumerator = enumerable.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						T item2 = (T)((object)obj);
						treeView.ExpandChildren(item2, getChildren);
					}
				}
				finally
				{
					IDisposable disposable;
					if ((disposable = (enumerator as IDisposable)) != null)
					{
						disposable.Dispose();
					}
				}
			}
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00017C74 File Offset: 0x00016074
		public static void ExpandAll<T>(this VirtualizingTreeView treeView, T item, Func<T, T> getParent, Func<T, IEnumerable> getChildren)
		{
			treeView.ExpandTo(getParent(item), getParent);
			treeView.ExpandChildren(item, getChildren);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00017C8C File Offset: 0x0001608C
		public static void ItemDropStdHandler<T>(this VirtualizingTreeView treeView, ItemDropArgs e, Func<T, T> getParent, Action<T, T> setParent, Func<T, T, int> indexOfChild, Action<T, T> removeChild, Action<T, T, int> insertChild) where T : class
		{
			T t = (T)((object)e.DropTarget);
			if (e.Action == ItemDropAction.SetLastChild)
			{
				for (int i = 0; i < e.DragItems.Length; i++)
				{
					T t2 = (T)((object)e.DragItems[i]);
					removeChild(t2, getParent(t2));
					setParent(t2, t);
					insertChild(t2, getParent(t2), 0);
				}
			}
			else if (e.Action == ItemDropAction.SetNextSibling)
			{
				for (int j = e.DragItems.Length - 1; j >= 0; j--)
				{
					T t3 = (T)((object)e.DragItems[j]);
					int num = indexOfChild(t, getParent(t));
					if (getParent(t3) != getParent(t))
					{
						removeChild(t3, getParent(t3));
						setParent(t3, getParent(t));
						insertChild(t3, getParent(t3), num + 1);
					}
					else
					{
						int num2 = indexOfChild(t3, getParent(t3));
						if (num < num2)
						{
							removeChild(t3, getParent(t3));
							insertChild(t3, getParent(t3), num + 1);
						}
						else
						{
							removeChild(t3, getParent(t3));
							insertChild(t3, getParent(t3), num);
						}
					}
				}
			}
			else if (e.Action == ItemDropAction.SetPrevSibling)
			{
				for (int k = 0; k < e.DragItems.Length; k++)
				{
					T t4 = (T)((object)e.DragItems[k]);
					if (getParent(t4) != getParent(t))
					{
						removeChild(t4, getParent(t4));
						setParent(t4, getParent(t));
						insertChild(t4, getParent(t4), 0);
					}
					int num3 = indexOfChild(t, getParent(t));
					int num4 = indexOfChild(t4, getParent(t4));
					if (num3 > num4)
					{
						removeChild(t4, getParent(t4));
						insertChild(t4, getParent(t4), num3 - 1);
					}
					else
					{
						removeChild(t4, getParent(t4));
						insertChild(t4, getParent(t4), num3);
					}
				}
			}
		}
	}
}
