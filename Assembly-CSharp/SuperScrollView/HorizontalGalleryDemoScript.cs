using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SuperScrollView
{
	// Token: 0x020005D8 RID: 1496
	public class HorizontalGalleryDemoScript : MonoBehaviour
	{
		// Token: 0x0600227D RID: 8829 RVA: 0x000BD6C8 File Offset: 0x000BBAC8
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

		// Token: 0x0600227E RID: 8830 RVA: 0x000BD7FA File Offset: 0x000BBBFA
		private void OnBackBtnClicked()
		{
			SceneManager.LoadScene("Menu");
		}

		// Token: 0x0600227F RID: 8831 RVA: 0x000BD808 File Offset: 0x000BBC08
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

		// Token: 0x06002280 RID: 8832 RVA: 0x000BD878 File Offset: 0x000BBC78
		private void LateUpdate()
		{
			this.mLoopListView.UpdateAllShownItemSnapData();
			int shownItemCount = this.mLoopListView.ShownItemCount;
			for (int i = 0; i < shownItemCount; i++)
			{
				LoopListViewItem2 shownItemByIndex = this.mLoopListView.GetShownItemByIndex(i);
				ListItem5 component = shownItemByIndex.GetComponent<ListItem5>();
				float num = 1f - Mathf.Abs(shownItemByIndex.DistanceWithViewPortSnapCenter) / 700f;
				num = Mathf.Clamp(num, 0.4f, 1f);
				component.mContentRootObj.GetComponent<CanvasGroup>().alpha = num;
				component.mContentRootObj.transform.localScale = new Vector3(num, num, 1f);
			}
		}

		// Token: 0x06002281 RID: 8833 RVA: 0x000BD920 File Offset: 0x000BBD20
		private void OnJumpBtnClicked()
		{
			int num = 0;
			if (!int.TryParse(this.mScrollToInput.text, out num))
			{
				return;
			}
			num -= 2;
			if (num < 0)
			{
				num = 0;
			}
			this.mLoopListView.MovePanelToItemIndex(num, 0f);
			this.mLoopListView.FinishSnapImmediately();
		}

		// Token: 0x06002282 RID: 8834 RVA: 0x000BD970 File Offset: 0x000BBD70
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

		// Token: 0x06002283 RID: 8835 RVA: 0x000BD9E0 File Offset: 0x000BBDE0
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

		// Token: 0x04002218 RID: 8728
		public LoopListView2 mLoopListView;

		// Token: 0x04002219 RID: 8729
		private Button mScrollToButton;

		// Token: 0x0400221A RID: 8730
		private Button mAddItemButton;

		// Token: 0x0400221B RID: 8731
		private Button mSetCountButton;

		// Token: 0x0400221C RID: 8732
		private InputField mScrollToInput;

		// Token: 0x0400221D RID: 8733
		private InputField mAddItemInput;

		// Token: 0x0400221E RID: 8734
		private InputField mSetCountInput;

		// Token: 0x0400221F RID: 8735
		private Button mBackButton;
	}
}
