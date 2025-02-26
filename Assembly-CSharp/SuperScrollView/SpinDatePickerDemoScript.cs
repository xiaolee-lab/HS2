using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SuperScrollView
{
	// Token: 0x020005E3 RID: 1507
	public class SpinDatePickerDemoScript : MonoBehaviour
	{
		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x060022E3 RID: 8931 RVA: 0x000C0D2A File Offset: 0x000BF12A
		public int CurSelectedMonth
		{
			get
			{
				return this.mCurSelectedMonth;
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x060022E4 RID: 8932 RVA: 0x000C0D32 File Offset: 0x000BF132
		public int CurSelectedDay
		{
			get
			{
				return this.mCurSelectedDay;
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x060022E5 RID: 8933 RVA: 0x000C0D3A File Offset: 0x000BF13A
		public int CurSelectedHour
		{
			get
			{
				return this.mCurSelectedHour;
			}
		}

		// Token: 0x060022E6 RID: 8934 RVA: 0x000C0D44 File Offset: 0x000BF144
		private void Start()
		{
			this.mLoopListViewMonth.mOnSnapNearestChanged = new Action<LoopListView2, LoopListViewItem2>(this.OnMonthSnapTargetChanged);
			this.mLoopListViewDay.mOnSnapNearestChanged = new Action<LoopListView2, LoopListViewItem2>(this.OnDaySnapTargetChanged);
			this.mLoopListViewHour.mOnSnapNearestChanged = new Action<LoopListView2, LoopListViewItem2>(this.OnHourSnapTargetChanged);
			this.mLoopListViewMonth.InitListView(-1, new Func<LoopListView2, int, LoopListViewItem2>(this.OnGetItemByIndexForMonth), null);
			this.mLoopListViewDay.InitListView(-1, new Func<LoopListView2, int, LoopListViewItem2>(this.OnGetItemByIndexForDay), null);
			this.mLoopListViewHour.InitListView(-1, new Func<LoopListView2, int, LoopListViewItem2>(this.OnGetItemByIndexForHour), null);
			this.mLoopListViewMonth.mOnSnapItemFinished = new Action<LoopListView2, LoopListViewItem2>(this.OnMonthSnapTargetFinished);
			this.mBackButton.onClick.AddListener(new UnityAction(this.OnBackBtnClicked));
		}

		// Token: 0x060022E7 RID: 8935 RVA: 0x000C0E14 File Offset: 0x000BF214
		private void OnBackBtnClicked()
		{
			SceneManager.LoadScene("Menu");
		}

		// Token: 0x060022E8 RID: 8936 RVA: 0x000C0E20 File Offset: 0x000BF220
		private LoopListViewItem2 OnGetItemByIndexForHour(LoopListView2 listView, int index)
		{
			LoopListViewItem2 loopListViewItem = listView.NewListViewItem("ItemPrefab1");
			ListItem7 component = loopListViewItem.GetComponent<ListItem7>();
			if (!loopListViewItem.IsInitHandlerCalled)
			{
				loopListViewItem.IsInitHandlerCalled = true;
				component.Init();
			}
			int num = 1;
			int num2 = 24;
			int num3;
			if (index >= 0)
			{
				num3 = index % num2;
			}
			else
			{
				num3 = num2 + (index + 1) % num2 - 1;
			}
			num3 += num;
			component.Value = num3;
			component.mText.text = num3.ToString();
			return loopListViewItem;
		}

		// Token: 0x060022E9 RID: 8937 RVA: 0x000C0EA4 File Offset: 0x000BF2A4
		private LoopListViewItem2 OnGetItemByIndexForMonth(LoopListView2 listView, int index)
		{
			LoopListViewItem2 loopListViewItem = listView.NewListViewItem("ItemPrefab1");
			ListItem7 component = loopListViewItem.GetComponent<ListItem7>();
			if (!loopListViewItem.IsInitHandlerCalled)
			{
				loopListViewItem.IsInitHandlerCalled = true;
				component.Init();
			}
			int num = 1;
			int num2 = 12;
			int num3;
			if (index >= 0)
			{
				num3 = index % num2;
			}
			else
			{
				num3 = num2 + (index + 1) % num2 - 1;
			}
			num3 += num;
			component.Value = num3;
			component.mText.text = SpinDatePickerDemoScript.mMonthNameArray[num3 - 1];
			return loopListViewItem;
		}

		// Token: 0x060022EA RID: 8938 RVA: 0x000C0F24 File Offset: 0x000BF324
		private LoopListViewItem2 OnGetItemByIndexForDay(LoopListView2 listView, int index)
		{
			LoopListViewItem2 loopListViewItem = listView.NewListViewItem("ItemPrefab1");
			ListItem7 component = loopListViewItem.GetComponent<ListItem7>();
			if (!loopListViewItem.IsInitHandlerCalled)
			{
				loopListViewItem.IsInitHandlerCalled = true;
				component.Init();
			}
			int num = 1;
			int num2 = SpinDatePickerDemoScript.mMonthDayCountArray[this.mCurSelectedMonth - 1];
			int num3;
			if (index >= 0)
			{
				num3 = index % num2;
			}
			else
			{
				num3 = num2 + (index + 1) % num2 - 1;
			}
			num3 += num;
			component.Value = num3;
			component.mText.text = num3.ToString();
			return loopListViewItem;
		}

		// Token: 0x060022EB RID: 8939 RVA: 0x000C0FB4 File Offset: 0x000BF3B4
		private void OnMonthSnapTargetChanged(LoopListView2 listView, LoopListViewItem2 item)
		{
			int indexInShownItemList = listView.GetIndexInShownItemList(item);
			if (indexInShownItemList < 0)
			{
				return;
			}
			ListItem7 component = item.GetComponent<ListItem7>();
			this.mCurSelectedMonth = component.Value;
			this.OnListViewSnapTargetChanged(listView, indexInShownItemList);
		}

		// Token: 0x060022EC RID: 8940 RVA: 0x000C0FEC File Offset: 0x000BF3EC
		private void OnDaySnapTargetChanged(LoopListView2 listView, LoopListViewItem2 item)
		{
			int indexInShownItemList = listView.GetIndexInShownItemList(item);
			if (indexInShownItemList < 0)
			{
				return;
			}
			ListItem7 component = item.GetComponent<ListItem7>();
			this.mCurSelectedDay = component.Value;
			this.OnListViewSnapTargetChanged(listView, indexInShownItemList);
		}

		// Token: 0x060022ED RID: 8941 RVA: 0x000C1024 File Offset: 0x000BF424
		private void OnHourSnapTargetChanged(LoopListView2 listView, LoopListViewItem2 item)
		{
			int indexInShownItemList = listView.GetIndexInShownItemList(item);
			if (indexInShownItemList < 0)
			{
				return;
			}
			ListItem7 component = item.GetComponent<ListItem7>();
			this.mCurSelectedHour = component.Value;
			this.OnListViewSnapTargetChanged(listView, indexInShownItemList);
		}

		// Token: 0x060022EE RID: 8942 RVA: 0x000C105C File Offset: 0x000BF45C
		private void OnMonthSnapTargetFinished(LoopListView2 listView, LoopListViewItem2 item)
		{
			LoopListViewItem2 shownItemByIndex = this.mLoopListViewDay.GetShownItemByIndex(0);
			ListItem7 component = shownItemByIndex.GetComponent<ListItem7>();
			int firstItemIndex = component.Value - 1;
			this.mLoopListViewDay.RefreshAllShownItemWithFirstIndex(firstItemIndex);
		}

		// Token: 0x060022EF RID: 8943 RVA: 0x000C1094 File Offset: 0x000BF494
		private void OnListViewSnapTargetChanged(LoopListView2 listView, int targetIndex)
		{
			int shownItemCount = listView.ShownItemCount;
			for (int i = 0; i < shownItemCount; i++)
			{
				LoopListViewItem2 shownItemByIndex = listView.GetShownItemByIndex(i);
				ListItem7 component = shownItemByIndex.GetComponent<ListItem7>();
				if (i == targetIndex)
				{
					component.mText.color = Color.red;
				}
				else
				{
					component.mText.color = Color.black;
				}
			}
		}

		// Token: 0x04002275 RID: 8821
		public LoopListView2 mLoopListViewMonth;

		// Token: 0x04002276 RID: 8822
		public LoopListView2 mLoopListViewDay;

		// Token: 0x04002277 RID: 8823
		public LoopListView2 mLoopListViewHour;

		// Token: 0x04002278 RID: 8824
		public Button mBackButton;

		// Token: 0x04002279 RID: 8825
		private static int[] mMonthDayCountArray = new int[]
		{
			31,
			28,
			31,
			30,
			31,
			30,
			31,
			31,
			30,
			31,
			30,
			31
		};

		// Token: 0x0400227A RID: 8826
		private static string[] mMonthNameArray = new string[]
		{
			"Jan.",
			"Feb.",
			"Mar.",
			"Apr.",
			"May.",
			"Jun.",
			"Jul.",
			"Aug.",
			"Sep.",
			"Oct.",
			"Nov.",
			"Dec."
		};

		// Token: 0x0400227B RID: 8827
		private int mCurSelectedMonth = 2;

		// Token: 0x0400227C RID: 8828
		private int mCurSelectedDay = 2;

		// Token: 0x0400227D RID: 8829
		private int mCurSelectedHour = 2;
	}
}
