using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SuperScrollView
{
	// Token: 0x020005EC RID: 1516
	public class StaggeredGridView_LeftToRightDemoScript : MonoBehaviour
	{
		// Token: 0x06002321 RID: 8993 RVA: 0x000C2924 File Offset: 0x000C0D24
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
			gridViewLayoutParam.mColumnOrRowCount = 2;
			gridViewLayoutParam.mItemWidthOrHeight = 219f;
			this.mLoopListView.InitListView(DataSourceMgr.Get.TotalItemCount, gridViewLayoutParam, new Func<LoopStaggeredGridView, int, LoopStaggeredGridViewItem>(this.OnGetItemByIndex), null);
		}

		// Token: 0x06002322 RID: 8994 RVA: 0x000C2A8C File Offset: 0x000C0E8C
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
			LoopStaggeredGridViewItem loopStaggeredGridViewItem = listView.NewListViewItem("ItemPrefab1");
			ListItem5 component = loopStaggeredGridViewItem.GetComponent<ListItem5>();
			if (!loopStaggeredGridViewItem.IsInitHandlerCalled)
			{
				loopStaggeredGridViewItem.IsInitHandlerCalled = true;
				component.Init();
			}
			component.SetItemData(itemDataByIndex, index);
			float size = 390f + (float)this.mItemWidthArrayForDemo[index % this.mItemWidthArrayForDemo.Length] * 10f;
			loopStaggeredGridViewItem.CachedRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size);
			return loopStaggeredGridViewItem;
		}

		// Token: 0x06002323 RID: 8995 RVA: 0x000C2B18 File Offset: 0x000C0F18
		private void InitItemHeightArrayForDemo()
		{
			this.mItemWidthArrayForDemo = new int[100];
			for (int i = 0; i < this.mItemWidthArrayForDemo.Length; i++)
			{
				this.mItemWidthArrayForDemo[i] = UnityEngine.Random.Range(0, 20);
			}
		}

		// Token: 0x06002324 RID: 8996 RVA: 0x000C2B5B File Offset: 0x000C0F5B
		private void OnBackBtnClicked()
		{
			SceneManager.LoadScene("Menu");
		}

		// Token: 0x06002325 RID: 8997 RVA: 0x000C2B68 File Offset: 0x000C0F68
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

		// Token: 0x06002326 RID: 8998 RVA: 0x000C2BAC File Offset: 0x000C0FAC
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

		// Token: 0x06002327 RID: 8999 RVA: 0x000C2C08 File Offset: 0x000C1008
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

		// Token: 0x040022AD RID: 8877
		public LoopStaggeredGridView mLoopListView;

		// Token: 0x040022AE RID: 8878
		private Button mScrollToButton;

		// Token: 0x040022AF RID: 8879
		private Button mAddItemButton;

		// Token: 0x040022B0 RID: 8880
		private Button mSetCountButton;

		// Token: 0x040022B1 RID: 8881
		private InputField mScrollToInput;

		// Token: 0x040022B2 RID: 8882
		private InputField mAddItemInput;

		// Token: 0x040022B3 RID: 8883
		private InputField mSetCountInput;

		// Token: 0x040022B4 RID: 8884
		private Button mBackButton;

		// Token: 0x040022B5 RID: 8885
		private int[] mItemWidthArrayForDemo;
	}
}
