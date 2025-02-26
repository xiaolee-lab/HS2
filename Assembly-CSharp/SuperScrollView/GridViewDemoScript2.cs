using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SuperScrollView
{
	// Token: 0x020005CF RID: 1487
	public class GridViewDemoScript2 : MonoBehaviour
	{
		// Token: 0x06002240 RID: 8768 RVA: 0x000BC124 File Offset: 0x000BA524
		private void Start()
		{
			this.mLoopGridView.InitGridView(DataSourceMgr.Get.TotalItemCount, new Func<LoopGridView, int, int, int, LoopGridViewItem>(this.OnGetItemByRowColumn), null, null);
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

		// Token: 0x06002241 RID: 8769 RVA: 0x000BC257 File Offset: 0x000BA657
		private void OnBackBtnClicked()
		{
			SceneManager.LoadScene("Menu");
		}

		// Token: 0x06002242 RID: 8770 RVA: 0x000BC264 File Offset: 0x000BA664
		private LoopGridViewItem OnGetItemByRowColumn(LoopGridView gridView, int itemIndex, int row, int column)
		{
			ItemData itemDataByIndex = DataSourceMgr.Get.GetItemDataByIndex(itemIndex);
			if (itemDataByIndex == null)
			{
				return null;
			}
			LoopGridViewItem loopGridViewItem = gridView.NewListViewItem("ItemPrefab0");
			ListItem18 component = loopGridViewItem.GetComponent<ListItem18>();
			if (!loopGridViewItem.IsInitHandlerCalled)
			{
				loopGridViewItem.IsInitHandlerCalled = true;
				component.Init();
			}
			component.SetItemData(itemDataByIndex, itemIndex, row, column);
			return loopGridViewItem;
		}

		// Token: 0x06002243 RID: 8771 RVA: 0x000BC2BC File Offset: 0x000BA6BC
		private void OnJumpBtnClicked()
		{
			int itemIndex = 0;
			if (!int.TryParse(this.mScrollToInput.text, out itemIndex))
			{
				return;
			}
			this.mLoopGridView.MovePanelToItemByIndex(itemIndex, 0f, 0f);
		}

		// Token: 0x06002244 RID: 8772 RVA: 0x000BC2FC File Offset: 0x000BA6FC
		private void OnAddItemBtnClicked()
		{
			int num = 0;
			if (!int.TryParse(this.mAddItemInput.text, out num))
			{
				return;
			}
			this.mLoopGridView.SetListItemCount(num + this.mLoopGridView.ItemTotalCount, false);
		}

		// Token: 0x06002245 RID: 8773 RVA: 0x000BC33C File Offset: 0x000BA73C
		private void OnSetItemCountBtnClicked()
		{
			int num = 0;
			if (!int.TryParse(this.mSetCountInput.text, out num))
			{
				return;
			}
			if (num > DataSourceMgr.Get.TotalItemCount)
			{
				num = DataSourceMgr.Get.TotalItemCount;
			}
			if (num < 0)
			{
				num = 0;
			}
			this.mLoopGridView.SetListItemCount(num, true);
		}

		// Token: 0x040021DC RID: 8668
		public LoopGridView mLoopGridView;

		// Token: 0x040021DD RID: 8669
		private Button mScrollToButton;

		// Token: 0x040021DE RID: 8670
		private Button mAddItemButton;

		// Token: 0x040021DF RID: 8671
		private Button mSetCountButton;

		// Token: 0x040021E0 RID: 8672
		private InputField mScrollToInput;

		// Token: 0x040021E1 RID: 8673
		private InputField mAddItemInput;

		// Token: 0x040021E2 RID: 8674
		private InputField mSetCountInput;

		// Token: 0x040021E3 RID: 8675
		private Button mBackButton;
	}
}
