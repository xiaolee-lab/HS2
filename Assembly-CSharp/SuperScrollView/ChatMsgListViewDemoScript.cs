using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SuperScrollView
{
	// Token: 0x020005D2 RID: 1490
	public class ChatMsgListViewDemoScript : MonoBehaviour
	{
		// Token: 0x06002255 RID: 8789 RVA: 0x000BC8FC File Offset: 0x000BACFC
		private void Start()
		{
			this.mLoopListView.InitListView(ChatMsgDataSourceMgr.Get.TotalItemCount, new Func<LoopListView2, int, LoopListViewItem2>(this.OnGetItemByIndex), null);
			this.mScrollToButton = GameObject.Find("ButtonPanel/buttonGroup2/ScrollToButton").GetComponent<Button>();
			this.mScrollToInput = GameObject.Find("ButtonPanel/buttonGroup2/ScrollToInputField").GetComponent<InputField>();
			this.mScrollToButton.onClick.AddListener(new UnityAction(this.OnJumpBtnClicked));
			this.mBackButton = GameObject.Find("ButtonPanel/BackButton").GetComponent<Button>();
			this.mBackButton.onClick.AddListener(new UnityAction(this.OnBackBtnClicked));
			this.mAppendMsgButton = GameObject.Find("ButtonPanel/buttonGroup1/AppendButton").GetComponent<Button>();
			this.mAppendMsgButton.onClick.AddListener(new UnityAction(this.OnAppendMsgBtnClicked));
		}

		// Token: 0x06002256 RID: 8790 RVA: 0x000BC9D3 File Offset: 0x000BADD3
		private void OnBackBtnClicked()
		{
			SceneManager.LoadScene("Menu");
		}

		// Token: 0x06002257 RID: 8791 RVA: 0x000BC9DF File Offset: 0x000BADDF
		private void OnAppendMsgBtnClicked()
		{
			ChatMsgDataSourceMgr.Get.AppendOneMsg();
			this.mLoopListView.SetListItemCount(ChatMsgDataSourceMgr.Get.TotalItemCount, false);
			this.mLoopListView.MovePanelToItemIndex(ChatMsgDataSourceMgr.Get.TotalItemCount - 1, 0f);
		}

		// Token: 0x06002258 RID: 8792 RVA: 0x000BCA20 File Offset: 0x000BAE20
		private void OnJumpBtnClicked()
		{
			int num = 0;
			if (!int.TryParse(this.mScrollToInput.text, out num))
			{
				return;
			}
			if (num < 0)
			{
				return;
			}
			this.mLoopListView.MovePanelToItemIndex(num, 0f);
		}

		// Token: 0x06002259 RID: 8793 RVA: 0x000BCA60 File Offset: 0x000BAE60
		private LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int index)
		{
			if (index < 0 || index >= ChatMsgDataSourceMgr.Get.TotalItemCount)
			{
				return null;
			}
			ChatMsg chatMsgByIndex = ChatMsgDataSourceMgr.Get.GetChatMsgByIndex(index);
			if (chatMsgByIndex == null)
			{
				return null;
			}
			LoopListViewItem2 loopListViewItem;
			if (chatMsgByIndex.mPersonId == 0)
			{
				loopListViewItem = listView.NewListViewItem("ItemPrefab1");
			}
			else
			{
				loopListViewItem = listView.NewListViewItem("ItemPrefab2");
			}
			ListItem4 component = loopListViewItem.GetComponent<ListItem4>();
			if (!loopListViewItem.IsInitHandlerCalled)
			{
				loopListViewItem.IsInitHandlerCalled = true;
				component.Init();
			}
			component.SetItemData(chatMsgByIndex, index);
			return loopListViewItem;
		}

		// Token: 0x040021F4 RID: 8692
		public LoopListView2 mLoopListView;

		// Token: 0x040021F5 RID: 8693
		private Button mScrollToButton;

		// Token: 0x040021F6 RID: 8694
		private InputField mScrollToInput;

		// Token: 0x040021F7 RID: 8695
		private Button mBackButton;

		// Token: 0x040021F8 RID: 8696
		private Button mAppendMsgButton;
	}
}
