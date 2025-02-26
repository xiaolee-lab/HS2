using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SuperScrollView
{
	// Token: 0x020005D9 RID: 1497
	public class LeftToRightDemoScript : MonoBehaviour
	{
		// Token: 0x06002285 RID: 8837 RVA: 0x000BDA38 File Offset: 0x000BBE38
		private void Start()
		{
			this.mLoopListView.InitListView(DataSourceMgr.Get.TotalItemCount, new Func<LoopListView2, int, LoopListViewItem2>(this.OnGetItemByIndex), null);
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

		// Token: 0x06002286 RID: 8838 RVA: 0x000BDB6A File Offset: 0x000BBF6A
		private void OnBackBtnClicked()
		{
			SceneManager.LoadScene("Menu");
		}

		// Token: 0x06002287 RID: 8839 RVA: 0x000BDB78 File Offset: 0x000BBF78
		private LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int index)
		{
			if (index < 0 || index >= DataSourceMgr.Get.TotalItemCount)
			{
				return null;
			}
			ItemData itemDataByIndex = DataSourceMgr.Get.GetItemDataByIndex(index);
			if (itemDataByIndex == null)
			{
				return null;
			}
			LoopListViewItem2 loopListViewItem = listView.NewListViewItem("ItemPrefab1");
			ListItem5 component = loopListViewItem.GetComponent<ListItem5>();
			if (!loopListViewItem.IsInitHandlerCalled)
			{
				loopListViewItem.IsInitHandlerCalled = true;
				component.Init();
			}
			component.SetItemData(itemDataByIndex, index);
			return loopListViewItem;
		}

		// Token: 0x06002288 RID: 8840 RVA: 0x000BDBE8 File Offset: 0x000BBFE8
		private void OnJumpBtnClicked()
		{
			int itemIndex = 0;
			if (!int.TryParse(this.mScrollToInput.text, out itemIndex))
			{
				return;
			}
			this.mLoopListView.MovePanelToItemIndex(itemIndex, 0f);
		}

		// Token: 0x06002289 RID: 8841 RVA: 0x000BDC20 File Offset: 0x000BC020
		private void OnAddItemBtnClicked()
		{
			if (this.mLoopListView.ItemTotalCount < 0)
			{
				return;
			}
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

		// Token: 0x0600228A RID: 8842 RVA: 0x000BDC90 File Offset: 0x000BC090
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

		// Token: 0x04002220 RID: 8736
		public LoopListView2 mLoopListView;

		// Token: 0x04002221 RID: 8737
		private Button mScrollToButton;

		// Token: 0x04002222 RID: 8738
		private Button mAddItemButton;

		// Token: 0x04002223 RID: 8739
		private Button mSetCountButton;

		// Token: 0x04002224 RID: 8740
		private InputField mScrollToInput;

		// Token: 0x04002225 RID: 8741
		private InputField mAddItemInput;

		// Token: 0x04002226 RID: 8742
		private InputField mSetCountInput;

		// Token: 0x04002227 RID: 8743
		private Button mBackButton;
	}
}
