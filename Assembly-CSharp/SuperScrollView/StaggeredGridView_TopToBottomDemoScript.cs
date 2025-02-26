using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SuperScrollView
{
	// Token: 0x020005ED RID: 1517
	public class StaggeredGridView_TopToBottomDemoScript : MonoBehaviour
	{
		// Token: 0x06002329 RID: 9001 RVA: 0x000C2C5C File Offset: 0x000C105C
		private void Start()
		{
			this.mSetCountButton = GameObject.Find("ButtonPanel/buttonGroup1/SetCountButton").GetComponent<Button>();
			this.mScrollToButton = GameObject.Find("ButtonPanel/buttonGroup2/ScrollToButton").GetComponent<Button>();
			this.mAddItemButton = GameObject.Find("ButtonPanel/buttonGroup3/AddItemButton").GetComponent<Button>();
			this.mSetCountInput = GameObject.Find("ButtonPanel/buttonGroup1/SetCountInputField").GetComponent<InputField>();
			this.mScrollToInput = GameObject.Find("ButtonPanel/buttonGroup2/ScrollToInputField").GetComponent<InputField>();
			this.mAddItemInput = GameObject.Find("ButtonPanel/buttonGroup3/AddItemInputField").GetComponent<InputField>();
			this.mScrollToButton.onClick.AddListener(new UnityAction(this.OnJumpBtnClicked));
			this.mAddItemButton.onClick.AddListener(new UnityAction(this.OnAddItemBtnClicked));
			this.mSetCountButton.onClick.AddListener(new UnityAction(this.OnSetItemCountBtnClicked));
			this.mBackButton = GameObject.Find("ButtonPanel/BackButton").GetComponent<Button>();
			this.mBackButton.onClick.AddListener(new UnityAction(this.OnBackBtnClicked));
			this.InitItemHeightArrayForDemo();
			GridViewLayoutParam gridViewLayoutParam = new GridViewLayoutParam();
			gridViewLayoutParam.mPadding1 = 10f;
			gridViewLayoutParam.mPadding2 = 10f;
			gridViewLayoutParam.mColumnOrRowCount = 3;
			gridViewLayoutParam.mItemWidthOrHeight = 217f;
			this.mLoopListView.InitListView(DataSourceMgr.Get.TotalItemCount, gridViewLayoutParam, new Func<LoopStaggeredGridView, int, LoopStaggeredGridViewItem>(this.OnGetItemByIndex), null);
		}

		// Token: 0x0600232A RID: 9002 RVA: 0x000C2DC4 File Offset: 0x000C11C4
		private LoopStaggeredGridViewItem OnGetItemByIndex(LoopStaggeredGridView listView, int index)
		{
			if (index < 0)
			{
				return null;
			}
			ItemData itemDataByIndex = DataSourceMgr.Get.GetItemDataByIndex(index);
			if (itemDataByIndex == null)
			{
				return null;
			}
			LoopStaggeredGridViewItem loopStaggeredGridViewItem = listView.NewListViewItem("ItemPrefab0");
			ListItem5 component = loopStaggeredGridViewItem.GetComponent<ListItem5>();
			if (!loopStaggeredGridViewItem.IsInitHandlerCalled)
			{
				loopStaggeredGridViewItem.IsInitHandlerCalled = true;
				component.Init();
			}
			component.SetItemData(itemDataByIndex, index);
			float size = 300f + (float)this.mItemHeightArrayForDemo[index % this.mItemHeightArrayForDemo.Length] * 10f;
			loopStaggeredGridViewItem.CachedRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);
			return loopStaggeredGridViewItem;
		}

		// Token: 0x0600232B RID: 9003 RVA: 0x000C2E50 File Offset: 0x000C1250
		private void InitItemHeightArrayForDemo()
		{
			this.mItemHeightArrayForDemo = new int[100];
			for (int i = 0; i < this.mItemHeightArrayForDemo.Length; i++)
			{
				this.mItemHeightArrayForDemo[i] = UnityEngine.Random.Range(0, 20);
			}
		}

		// Token: 0x0600232C RID: 9004 RVA: 0x000C2E93 File Offset: 0x000C1293
		private void OnBackBtnClicked()
		{
			SceneManager.LoadScene("Menu");
		}

		// Token: 0x0600232D RID: 9005 RVA: 0x000C2EA0 File Offset: 0x000C12A0
		private void OnJumpBtnClicked()
		{
			int num = 0;
			if (!int.TryParse(this.mScrollToInput.text, out num))
			{
				return;
			}
			if (num < 0)
			{
				num = 0;
			}
			this.mLoopListView.MovePanelToItemIndex(num, 0f);
		}

		// Token: 0x0600232E RID: 9006 RVA: 0x000C2EE4 File Offset: 0x000C12E4
		private void OnAddItemBtnClicked()
		{
			int num = 0;
			if (!int.TryParse(this.mAddItemInput.text, out num))
			{
				return;
			}
			num = this.mLoopListView.ItemTotalCount + num;
			if (num < 0 || num > DataSourceMgr.Get.TotalItemCount)
			{
				return;
			}
			this.mLoopListView.SetListItemCount(num, false);
		}

		// Token: 0x0600232F RID: 9007 RVA: 0x000C2F40 File Offset: 0x000C1340
		private void OnSetItemCountBtnClicked()
		{
			int num = 0;
			if (!int.TryParse(this.mSetCountInput.text, out num))
			{
				return;
			}
			if (num < 0 || num > DataSourceMgr.Get.TotalItemCount)
			{
				return;
			}
			this.mLoopListView.SetListItemCount(num, false);
		}

		// Token: 0x040022B6 RID: 8886
		public LoopStaggeredGridView mLoopListView;

		// Token: 0x040022B7 RID: 8887
		private Button mScrollToButton;

		// Token: 0x040022B8 RID: 8888
		private Button mAddItemButton;

		// Token: 0x040022B9 RID: 8889
		private Button mSetCountButton;

		// Token: 0x040022BA RID: 8890
		private InputField mScrollToInput;

		// Token: 0x040022BB RID: 8891
		private InputField mAddItemInput;

		// Token: 0x040022BC RID: 8892
		private InputField mSetCountInput;

		// Token: 0x040022BD RID: 8893
		private Button mBackButton;

		// Token: 0x040022BE RID: 8894
		private int[] mItemHeightArrayForDemo;
	}
}
