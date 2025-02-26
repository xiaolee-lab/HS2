﻿using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SuperScrollView
{
	// Token: 0x020005E0 RID: 1504
	public class ResponsiveGridViewDemoScript : MonoBehaviour
	{
		// Token: 0x060022BF RID: 8895 RVA: 0x000BF71C File Offset: 0x000BDB1C
		private void Start()
		{
			this.mListItemTotalCount = DataSourceMgr.Get.TotalItemCount;
			int num = this.mListItemTotalCount / this.mItemCountPerRow;
			if (this.mListItemTotalCount % this.mItemCountPerRow > 0)
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
			this.mDragChangSizeScript.mOnDragEndAction = new Action(this.OnViewPortSizeChanged);
			this.OnViewPortSizeChanged();
		}

		// Token: 0x060022C0 RID: 8896 RVA: 0x000BF897 File Offset: 0x000BDC97
		private void OnBackBtnClicked()
		{
			SceneManager.LoadScene("Menu");
		}

		// Token: 0x060022C1 RID: 8897 RVA: 0x000BF8A4 File Offset: 0x000BDCA4
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
			int num = this.mListItemTotalCount / this.mItemCountPerRow;
			if (this.mListItemTotalCount % this.mItemCountPerRow > 0)
			{
				num++;
			}
			this.mLoopListView.SetListItemCount(num, false);
			this.mLoopListView.RefreshAllShownItem();
		}

		// Token: 0x060022C2 RID: 8898 RVA: 0x000BF930 File Offset: 0x000BDD30
		private void UpdateItemPrefab()
		{
			ItemPrefabConfData itemPrefabConfData = this.mLoopListView.GetItemPrefabConfData("ItemPrefab1");
			GameObject mItemPrefab = itemPrefabConfData.mItemPrefab;
			RectTransform component = mItemPrefab.GetComponent<RectTransform>();
			ListItem6 component2 = mItemPrefab.GetComponent<ListItem6>();
			float viewPortWidth = this.mLoopListView.ViewPortWidth;
			int count = component2.mItemList.Count;
			GameObject gameObject = component2.mItemList[0].gameObject;
			RectTransform component3 = gameObject.GetComponent<RectTransform>();
			float width = component3.rect.width;
			int num = Mathf.FloorToInt(viewPortWidth / width);
			if (num == 0)
			{
				num = 1;
			}
			this.mItemCountPerRow = num;
			float num2 = (viewPortWidth - width * (float)num) / (float)(num + 1);
			if (num2 < 0f)
			{
				num2 = 0f;
			}
			component.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, viewPortWidth);
			if (num > count)
			{
				int num3 = num - count;
				for (int i = 0; i < num3; i++)
				{
					GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject, Vector3.zero, Quaternion.identity, component);
					RectTransform component4 = gameObject2.GetComponent<RectTransform>();
					component4.localScale = Vector3.one;
					component4.anchoredPosition3D = Vector3.zero;
					component4.rotation = Quaternion.identity;
					ListItem5 component5 = gameObject2.GetComponent<ListItem5>();
					component2.mItemList.Add(component5);
				}
			}
			else if (num < count)
			{
				int num4 = count - num;
				for (int j = 0; j < num4; j++)
				{
					ListItem5 listItem = component2.mItemList[component2.mItemList.Count - 1];
					component2.mItemList.RemoveAt(component2.mItemList.Count - 1);
					UnityEngine.Object.DestroyImmediate(listItem.gameObject);
				}
			}
			float num5 = num2;
			for (int k = 0; k < component2.mItemList.Count; k++)
			{
				GameObject gameObject3 = component2.mItemList[k].gameObject;
				gameObject3.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(num5, 0f, 0f);
				num5 = num5 + width + num2;
			}
			this.mLoopListView.OnItemPrefabChanged("ItemPrefab1");
		}

		// Token: 0x060022C3 RID: 8899 RVA: 0x000BFB4D File Offset: 0x000BDF4D
		private void OnViewPortSizeChanged()
		{
			this.UpdateItemPrefab();
			this.SetListItemTotalCount(this.mListItemTotalCount);
		}

		// Token: 0x060022C4 RID: 8900 RVA: 0x000BFB64 File Offset: 0x000BDF64
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
			for (int i = 0; i < this.mItemCountPerRow; i++)
			{
				int num = index * this.mItemCountPerRow + i;
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

		// Token: 0x060022C5 RID: 8901 RVA: 0x000BFC4C File Offset: 0x000BE04C
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
			int num2 = num / this.mItemCountPerRow;
			if (num % this.mItemCountPerRow > 0)
			{
				num2++;
			}
			if (num2 > 0)
			{
				num2--;
			}
			this.mLoopListView.MovePanelToItemIndex(num2, 0f);
		}

		// Token: 0x060022C6 RID: 8902 RVA: 0x000BFCB8 File Offset: 0x000BE0B8
		private void OnAddItemBtnClicked()
		{
			int num = 0;
			if (!int.TryParse(this.mAddItemInput.text, out num))
			{
				return;
			}
			this.SetListItemTotalCount(this.mListItemTotalCount + num);
		}

		// Token: 0x060022C7 RID: 8903 RVA: 0x000BFCF0 File Offset: 0x000BE0F0
		private void OnSetItemCountBtnClicked()
		{
			int listItemTotalCount = 0;
			if (!int.TryParse(this.mSetCountInput.text, out listItemTotalCount))
			{
				return;
			}
			this.SetListItemTotalCount(listItemTotalCount);
		}

		// Token: 0x04002256 RID: 8790
		public LoopListView2 mLoopListView;

		// Token: 0x04002257 RID: 8791
		private Button mScrollToButton;

		// Token: 0x04002258 RID: 8792
		private Button mAddItemButton;

		// Token: 0x04002259 RID: 8793
		private Button mSetCountButton;

		// Token: 0x0400225A RID: 8794
		private InputField mScrollToInput;

		// Token: 0x0400225B RID: 8795
		private InputField mAddItemInput;

		// Token: 0x0400225C RID: 8796
		private InputField mSetCountInput;

		// Token: 0x0400225D RID: 8797
		private Button mBackButton;

		// Token: 0x0400225E RID: 8798
		private int mItemCountPerRow = 3;

		// Token: 0x0400225F RID: 8799
		private int mListItemTotalCount;

		// Token: 0x04002260 RID: 8800
		public DragChangSizeScript mDragChangSizeScript;
	}
}
