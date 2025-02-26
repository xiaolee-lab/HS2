using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SuperScrollView
{
	// Token: 0x020005D6 RID: 1494
	public class GridViewDemoScript : MonoBehaviour
	{
		// Token: 0x06002272 RID: 8818 RVA: 0x000BD220 File Offset: 0x000BB620
		private void Start()
		{
			this.mListItemTotalCount = DataSourceMgr.Get.TotalItemCount;
			int num = this.mListItemTotalCount / 3;
			if (this.mListItemTotalCount % 3 > 0)
			{
				num++;
			}
			this.mLoopListView.InitListView(num, new Func<LoopListView2, int, LoopListViewItem2>(this.OnGetItemByIndex), null);
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
		}

		// Token: 0x06002273 RID: 8819 RVA: 0x000BD374 File Offset: 0x000BB774
		private void OnBackBtnClicked()
		{
			SceneManager.LoadScene("Menu");
		}

		// Token: 0x06002274 RID: 8820 RVA: 0x000BD380 File Offset: 0x000BB780
		private void SetListItemTotalCount(int count)
		{
			this.mListItemTotalCount = count;
			if (this.mListItemTotalCount < 0)
			{
				this.mListItemTotalCount = 0;
			}
			if (this.mListItemTotalCount > DataSourceMgr.Get.TotalItemCount)
			{
				this.mListItemTotalCount = DataSourceMgr.Get.TotalItemCount;
			}
			int num = this.mListItemTotalCount / 3;
			if (this.mListItemTotalCount % 3 > 0)
			{
				num++;
			}
			this.mLoopListView.SetListItemCount(num, false);
			this.mLoopListView.RefreshAllShownItem();
		}

		// Token: 0x06002275 RID: 8821 RVA: 0x000BD400 File Offset: 0x000BB800
		private LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int index)
		{
			if (index < 0)
			{
				return null;
			}
			LoopListViewItem2 loopListViewItem = listView.NewListViewItem("ItemPrefab1");
			ListItem6 component = loopListViewItem.GetComponent<ListItem6>();
			if (!loopListViewItem.IsInitHandlerCalled)
			{
				loopListViewItem.IsInitHandlerCalled = true;
				component.Init();
			}
			for (int i = 0; i < 3; i++)
			{
				int num = index * 3 + i;
				if (num >= this.mListItemTotalCount)
				{
					component.mItemList[i].gameObject.SetActive(false);
				}
				else
				{
					ItemData itemDataByIndex = DataSourceMgr.Get.GetItemDataByIndex(num);
					if (itemDataByIndex != null)
					{
						component.mItemList[i].gameObject.SetActive(true);
						component.mItemList[i].SetItemData(itemDataByIndex, num);
					}
					else
					{
						component.mItemList[i].gameObject.SetActive(false);
					}
				}
			}
			return loopListViewItem;
		}

		// Token: 0x06002276 RID: 8822 RVA: 0x000BD4E0 File Offset: 0x000BB8E0
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
			num++;
			int num2 = num / 3;
			if (num % 3 > 0)
			{
				num2++;
			}
			if (num2 > 0)
			{
				num2--;
			}
			this.mLoopListView.MovePanelToItemIndex(num2, 0f);
		}

		// Token: 0x06002277 RID: 8823 RVA: 0x000BD544 File Offset: 0x000BB944
		private void OnAddItemBtnClicked()
		{
			int num = 0;
			if (!int.TryParse(this.mAddItemInput.text, out num))
			{
				return;
			}
			this.SetListItemTotalCount(this.mListItemTotalCount + num);
		}

		// Token: 0x06002278 RID: 8824 RVA: 0x000BD57C File Offset: 0x000BB97C
		private void OnSetItemCountBtnClicked()
		{
			int listItemTotalCount = 0;
			if (!int.TryParse(this.mSetCountInput.text, out listItemTotalCount))
			{
				return;
			}
			this.SetListItemTotalCount(listItemTotalCount);
		}

		// Token: 0x0400220B RID: 8715
		public LoopListView2 mLoopListView;

		// Token: 0x0400220C RID: 8716
		private Button mScrollToButton;

		// Token: 0x0400220D RID: 8717
		private Button mAddItemButton;

		// Token: 0x0400220E RID: 8718
		private Button mSetCountButton;

		// Token: 0x0400220F RID: 8719
		private InputField mScrollToInput;

		// Token: 0x04002210 RID: 8720
		private InputField mAddItemInput;

		// Token: 0x04002211 RID: 8721
		private InputField mSetCountInput;

		// Token: 0x04002212 RID: 8722
		private Button mBackButton;

		// Token: 0x04002213 RID: 8723
		private const int mItemCountPerRow = 3;

		// Token: 0x04002214 RID: 8724
		private int mListItemTotalCount;
	}
}
