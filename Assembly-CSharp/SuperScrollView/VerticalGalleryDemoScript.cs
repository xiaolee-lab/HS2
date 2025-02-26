using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SuperScrollView
{
	// Token: 0x020005E9 RID: 1513
	public class VerticalGalleryDemoScript : MonoBehaviour
	{
		// Token: 0x06002315 RID: 8981 RVA: 0x000C22F8 File Offset: 0x000C06F8
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

		// Token: 0x06002316 RID: 8982 RVA: 0x000C242A File Offset: 0x000C082A
		private void OnBackBtnClicked()
		{
			SceneManager.LoadScene("Menu");
		}

		// Token: 0x06002317 RID: 8983 RVA: 0x000C2438 File Offset: 0x000C0838
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
			ListItem2 component = loopListViewItem.GetComponent<ListItem2>();
			if (!loopListViewItem.IsInitHandlerCalled)
			{
				loopListViewItem.IsInitHandlerCalled = true;
				component.Init();
			}
			component.SetItemData(itemDataByIndex, index);
			return loopListViewItem;
		}

		// Token: 0x06002318 RID: 8984 RVA: 0x000C24A8 File Offset: 0x000C08A8
		private void LateUpdate()
		{
			this.mLoopListView.UpdateAllShownItemSnapData();
			int shownItemCount = this.mLoopListView.ShownItemCount;
			for (int i = 0; i < shownItemCount; i++)
			{
				LoopListViewItem2 shownItemByIndex = this.mLoopListView.GetShownItemByIndex(i);
				ListItem2 component = shownItemByIndex.GetComponent<ListItem2>();
				float num = 1f - Mathf.Abs(shownItemByIndex.DistanceWithViewPortSnapCenter) / 700f;
				num = Mathf.Clamp(num, 0.4f, 1f);
				component.mContentRootObj.GetComponent<CanvasGroup>().alpha = num;
				component.mContentRootObj.transform.localScale = new Vector3(num, num, 1f);
			}
		}

		// Token: 0x06002319 RID: 8985 RVA: 0x000C2550 File Offset: 0x000C0950
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

		// Token: 0x0600231A RID: 8986 RVA: 0x000C25A0 File Offset: 0x000C09A0
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

		// Token: 0x0600231B RID: 8987 RVA: 0x000C2610 File Offset: 0x000C0A10
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

		// Token: 0x040022A1 RID: 8865
		public LoopListView2 mLoopListView;

		// Token: 0x040022A2 RID: 8866
		private Button mScrollToButton;

		// Token: 0x040022A3 RID: 8867
		private Button mAddItemButton;

		// Token: 0x040022A4 RID: 8868
		private Button mSetCountButton;

		// Token: 0x040022A5 RID: 8869
		private InputField mScrollToInput;

		// Token: 0x040022A6 RID: 8870
		private InputField mAddItemInput;

		// Token: 0x040022A7 RID: 8871
		private InputField mSetCountInput;

		// Token: 0x040022A8 RID: 8872
		private Button mBackButton;
	}
}
