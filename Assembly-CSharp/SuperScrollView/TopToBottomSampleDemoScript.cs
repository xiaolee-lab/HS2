using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SuperScrollView
{
	// Token: 0x020005E6 RID: 1510
	public class TopToBottomSampleDemoScript : MonoBehaviour
	{
		// Token: 0x060022FA RID: 8954 RVA: 0x000C144C File Offset: 0x000BF84C
		private void Start()
		{
			this.InitData();
			this.mLoopListView.InitListView(this.mDataList.Count, new Func<LoopListView2, int, LoopListViewItem2>(this.OnGetItemByIndex), null);
			this.mScrollToButton = GameObject.Find("ButtonPanel/buttonGroup2/ScrollToButton").GetComponent<Button>();
			this.mAppendItemButton = GameObject.Find("ButtonPanel/buttonGroup3/AppendItemButton").GetComponent<Button>();
			this.mInsertItemButton = GameObject.Find("ButtonPanel/buttonGroup3/InsertItemButton").GetComponent<Button>();
			this.mScrollToInput = GameObject.Find("ButtonPanel/buttonGroup2/ScrollToInputField").GetComponent<InputField>();
			this.mScrollToButton.onClick.AddListener(new UnityAction(this.OnJumpBtnClicked));
			this.mAppendItemButton.onClick.AddListener(new UnityAction(this.OnAppendItemBtnClicked));
			this.mInsertItemButton.onClick.AddListener(new UnityAction(this.OnInsertItemBtnClicked));
		}

		// Token: 0x060022FB RID: 8955 RVA: 0x000C152C File Offset: 0x000BF92C
		private void InitData()
		{
			this.mDataList = new List<CustomData>();
			int num = 100;
			for (int i = 0; i < num; i++)
			{
				CustomData customData = new CustomData();
				customData.mContent = "Item" + i;
				this.mDataList.Add(customData);
			}
		}

		// Token: 0x060022FC RID: 8956 RVA: 0x000C1584 File Offset: 0x000BF984
		private void AppendOneData()
		{
			CustomData customData = new CustomData();
			customData.mContent = "Item" + this.mDataList.Count;
			this.mDataList.Add(customData);
		}

		// Token: 0x060022FD RID: 8957 RVA: 0x000C15C4 File Offset: 0x000BF9C4
		private void InsertOneData()
		{
			this.mTotalInsertedCount++;
			CustomData customData = new CustomData();
			customData.mContent = "Item(-" + this.mTotalInsertedCount + ")";
			this.mDataList.Insert(0, customData);
		}

		// Token: 0x060022FE RID: 8958 RVA: 0x000C1614 File Offset: 0x000BFA14
		private LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int index)
		{
			if (index < 0 || index >= this.mDataList.Count)
			{
				return null;
			}
			CustomData customData = this.mDataList[index];
			if (customData == null)
			{
				return null;
			}
			LoopListViewItem2 loopListViewItem = listView.NewListViewItem("ItemPrefab1");
			ListItem16 component = loopListViewItem.GetComponent<ListItem16>();
			if (!loopListViewItem.IsInitHandlerCalled)
			{
				loopListViewItem.IsInitHandlerCalled = true;
				component.Init();
			}
			component.mNameText.text = customData.mContent;
			return loopListViewItem;
		}

		// Token: 0x060022FF RID: 8959 RVA: 0x000C1690 File Offset: 0x000BFA90
		private void OnJumpBtnClicked()
		{
			int itemIndex = 0;
			if (!int.TryParse(this.mScrollToInput.text, out itemIndex))
			{
				return;
			}
			this.mLoopListView.MovePanelToItemIndex(itemIndex, 0f);
		}

		// Token: 0x06002300 RID: 8960 RVA: 0x000C16C8 File Offset: 0x000BFAC8
		private void OnAppendItemBtnClicked()
		{
			this.AppendOneData();
			this.mLoopListView.SetListItemCount(this.mDataList.Count, false);
			this.mLoopListView.RefreshAllShownItem();
		}

		// Token: 0x06002301 RID: 8961 RVA: 0x000C16F3 File Offset: 0x000BFAF3
		private void OnInsertItemBtnClicked()
		{
			this.InsertOneData();
			this.mLoopListView.SetListItemCount(this.mDataList.Count, false);
			this.mLoopListView.RefreshAllShownItem();
		}

		// Token: 0x04002287 RID: 8839
		public LoopListView2 mLoopListView;

		// Token: 0x04002288 RID: 8840
		private Button mScrollToButton;

		// Token: 0x04002289 RID: 8841
		private Button mAppendItemButton;

		// Token: 0x0400228A RID: 8842
		private Button mInsertItemButton;

		// Token: 0x0400228B RID: 8843
		private InputField mScrollToInput;

		// Token: 0x0400228C RID: 8844
		private List<CustomData> mDataList;

		// Token: 0x0400228D RID: 8845
		private int mTotalInsertedCount;
	}
}
