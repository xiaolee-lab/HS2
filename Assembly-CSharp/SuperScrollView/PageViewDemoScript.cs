using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SuperScrollView
{
	// Token: 0x020005DB RID: 1499
	public class PageViewDemoScript : MonoBehaviour
	{
		// Token: 0x0600228D RID: 8845 RVA: 0x000BDD00 File Offset: 0x000BC100
		private void Start()
		{
			this.InitDots();
			LoopListViewInitParam loopListViewInitParam = LoopListViewInitParam.CopyDefaultInitParam();
			loopListViewInitParam.mSnapVecThreshold = 99999f;
			this.mLoopListView.mOnBeginDragAction = new Action(this.OnBeginDrag);
			this.mLoopListView.mOnDragingAction = new Action(this.OnDraging);
			this.mLoopListView.mOnEndDragAction = new Action(this.OnEndDrag);
			this.mLoopListView.mOnSnapNearestChanged = new Action<LoopListView2, LoopListViewItem2>(this.OnSnapNearestChanged);
			this.mLoopListView.InitListView(this.mPageCount, new Func<LoopListView2, int, LoopListViewItem2>(this.OnGetItemByIndex), loopListViewInitParam);
			this.mBackButton = GameObject.Find("ButtonPanel/BackButton").GetComponent<Button>();
			this.mBackButton.onClick.AddListener(new UnityAction(this.OnBackBtnClicked));
		}

		// Token: 0x0600228E RID: 8846 RVA: 0x000BDDD0 File Offset: 0x000BC1D0
		private void InitDots()
		{
			int childCount = this.mDotsRootObj.childCount;
			for (int i = 0; i < childCount; i++)
			{
				Transform child = this.mDotsRootObj.GetChild(i);
				DotElem dotElem = new DotElem();
				dotElem.mDotElemRoot = child.gameObject;
				dotElem.mDotSmall = child.Find("dotSmall").gameObject;
				dotElem.mDotBig = child.Find("dotBig").gameObject;
				ClickEventListener clickEventListener = ClickEventListener.Get(dotElem.mDotElemRoot);
				int index = i;
				clickEventListener.SetClickEventHandler(delegate(GameObject obj)
				{
					this.OnDotClicked(index);
				});
				this.mDotElemList.Add(dotElem);
			}
		}

		// Token: 0x0600228F RID: 8847 RVA: 0x000BDE8C File Offset: 0x000BC28C
		private void OnDotClicked(int index)
		{
			int curSnapNearestItemIndex = this.mLoopListView.CurSnapNearestItemIndex;
			if (curSnapNearestItemIndex < 0 || curSnapNearestItemIndex >= this.mPageCount)
			{
				return;
			}
			if (index == curSnapNearestItemIndex)
			{
				return;
			}
			if (index > curSnapNearestItemIndex)
			{
				this.mLoopListView.SetSnapTargetItemIndex(curSnapNearestItemIndex + 1);
			}
			else
			{
				this.mLoopListView.SetSnapTargetItemIndex(curSnapNearestItemIndex - 1);
			}
		}

		// Token: 0x06002290 RID: 8848 RVA: 0x000BDEEC File Offset: 0x000BC2EC
		private void UpdateAllDots()
		{
			int curSnapNearestItemIndex = this.mLoopListView.CurSnapNearestItemIndex;
			if (curSnapNearestItemIndex < 0 || curSnapNearestItemIndex >= this.mPageCount)
			{
				return;
			}
			int count = this.mDotElemList.Count;
			if (curSnapNearestItemIndex >= count)
			{
				return;
			}
			for (int i = 0; i < count; i++)
			{
				DotElem dotElem = this.mDotElemList[i];
				if (i != curSnapNearestItemIndex)
				{
					dotElem.mDotSmall.SetActive(true);
					dotElem.mDotBig.SetActive(false);
				}
				else
				{
					dotElem.mDotSmall.SetActive(false);
					dotElem.mDotBig.SetActive(true);
				}
			}
		}

		// Token: 0x06002291 RID: 8849 RVA: 0x000BDF88 File Offset: 0x000BC388
		private void OnSnapNearestChanged(LoopListView2 listView, LoopListViewItem2 item)
		{
			this.UpdateAllDots();
		}

		// Token: 0x06002292 RID: 8850 RVA: 0x000BDF90 File Offset: 0x000BC390
		private void OnBackBtnClicked()
		{
			SceneManager.LoadScene("Menu");
		}

		// Token: 0x06002293 RID: 8851 RVA: 0x000BDF9C File Offset: 0x000BC39C
		private LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int pageIndex)
		{
			if (pageIndex < 0 || pageIndex >= this.mPageCount)
			{
				return null;
			}
			LoopListViewItem2 loopListViewItem = listView.NewListViewItem("ItemPrefab1");
			ListItem14 component = loopListViewItem.GetComponent<ListItem14>();
			if (!loopListViewItem.IsInitHandlerCalled)
			{
				loopListViewItem.IsInitHandlerCalled = true;
				component.Init();
			}
			List<ListItem14Elem> mElemItemList = component.mElemItemList;
			int count = mElemItemList.Count;
			int num = pageIndex * count;
			int i;
			for (i = 0; i < count; i++)
			{
				ItemData itemDataByIndex = DataSourceMgr.Get.GetItemDataByIndex(num + i);
				if (itemDataByIndex == null)
				{
					break;
				}
				ListItem14Elem listItem14Elem = mElemItemList[i];
				listItem14Elem.mRootObj.SetActive(true);
				listItem14Elem.mIcon.sprite = ResManager.Get.GetSpriteByName(itemDataByIndex.mIcon);
				listItem14Elem.mName.text = itemDataByIndex.mName;
			}
			if (i < count)
			{
				while (i < count)
				{
					mElemItemList[i].mRootObj.SetActive(false);
					i++;
				}
			}
			return loopListViewItem;
		}

		// Token: 0x06002294 RID: 8852 RVA: 0x000BE0A5 File Offset: 0x000BC4A5
		private void OnBeginDrag()
		{
		}

		// Token: 0x06002295 RID: 8853 RVA: 0x000BE0A7 File Offset: 0x000BC4A7
		private void OnDraging()
		{
		}

		// Token: 0x06002296 RID: 8854 RVA: 0x000BE0AC File Offset: 0x000BC4AC
		private void OnEndDrag()
		{
			float x = this.mLoopListView.ScrollRect.velocity.x;
			int curSnapNearestItemIndex = this.mLoopListView.CurSnapNearestItemIndex;
			LoopListViewItem2 shownItemByItemIndex = this.mLoopListView.GetShownItemByItemIndex(curSnapNearestItemIndex);
			if (shownItemByItemIndex == null)
			{
				this.mLoopListView.ClearSnapData();
				return;
			}
			if (Mathf.Abs(x) < 50f)
			{
				this.mLoopListView.SetSnapTargetItemIndex(curSnapNearestItemIndex);
				return;
			}
			Vector3 itemCornerPosInViewPort = this.mLoopListView.GetItemCornerPosInViewPort(shownItemByItemIndex, ItemCornerEnum.LeftTop);
			if (itemCornerPosInViewPort.x > 0f)
			{
				if (x > 0f)
				{
					this.mLoopListView.SetSnapTargetItemIndex(curSnapNearestItemIndex - 1);
				}
				else
				{
					this.mLoopListView.SetSnapTargetItemIndex(curSnapNearestItemIndex);
				}
			}
			else if (itemCornerPosInViewPort.x < 0f)
			{
				if (x > 0f)
				{
					this.mLoopListView.SetSnapTargetItemIndex(curSnapNearestItemIndex);
				}
				else
				{
					this.mLoopListView.SetSnapTargetItemIndex(curSnapNearestItemIndex + 1);
				}
			}
			else if (x > 0f)
			{
				this.mLoopListView.SetSnapTargetItemIndex(curSnapNearestItemIndex - 1);
			}
			else
			{
				this.mLoopListView.SetSnapTargetItemIndex(curSnapNearestItemIndex + 1);
			}
		}

		// Token: 0x0400222B RID: 8747
		public LoopListView2 mLoopListView;

		// Token: 0x0400222C RID: 8748
		private Button mBackButton;

		// Token: 0x0400222D RID: 8749
		private int mPageCount = 5;

		// Token: 0x0400222E RID: 8750
		public Transform mDotsRootObj;

		// Token: 0x0400222F RID: 8751
		private List<DotElem> mDotElemList = new List<DotElem>();
	}
}
