using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SuperScrollView
{
	// Token: 0x020005EB RID: 1515
	internal class MenuSceneScript : MonoBehaviour
	{
		// Token: 0x0600231E RID: 8990 RVA: 0x000C27E4 File Offset: 0x000C0BE4
		private void Start()
		{
			this.CreateFpsDisplyObj();
			int childCount = this.mButtonPanelTf.childCount;
			int num = this.mSceneNameArray.Length;
			for (int i = 0; i < childCount; i++)
			{
				if (i >= num)
				{
					this.mButtonPanelTf.GetChild(i).gameObject.SetActive(false);
				}
				else
				{
					this.mButtonPanelTf.GetChild(i).gameObject.SetActive(true);
					SceneNameInfo info = this.mSceneNameArray[i];
					Button component = this.mButtonPanelTf.GetChild(i).GetComponent<Button>();
					component.onClick.AddListener(delegate()
					{
						SceneManager.LoadScene(info.mSceneName);
					});
					Text component2 = component.transform.Find("Text").GetComponent<Text>();
					component2.text = info.mName;
				}
			}
		}

		// Token: 0x0600231F RID: 8991 RVA: 0x000C28C4 File Offset: 0x000C0CC4
		private void CreateFpsDisplyObj()
		{
			FPSDisplay x = UnityEngine.Object.FindObjectOfType<FPSDisplay>();
			if (x != null)
			{
				return;
			}
			GameObject gameObject = new GameObject();
			gameObject.name = "FPSDisplay";
			gameObject.AddComponent<FPSDisplay>();
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
		}

		// Token: 0x040022AB RID: 8875
		public Transform mButtonPanelTf;

		// Token: 0x040022AC RID: 8876
		private SceneNameInfo[] mSceneNameArray = new SceneNameInfo[]
		{
			new SceneNameInfo("Staggered GridView1", "StaggeredGridView_TopToBottom"),
			new SceneNameInfo("Staggered GridView2", "StaggeredGridView_LeftToRight"),
			new SceneNameInfo("Chat Message List", "ChatMsgListViewDemo"),
			new SceneNameInfo("Horizontal Gallery", "HorizontalGalleryDemo"),
			new SceneNameInfo("Vertical Gallery", "VerticalGalleryDemo"),
			new SceneNameInfo("GridView", "GridView_TopLeftToBottomRight"),
			new SceneNameInfo("PageView", "PageViewDemo"),
			new SceneNameInfo("TreeView", "TreeViewDemo"),
			new SceneNameInfo("Spin Date Picker", "SpinDatePickerDemo"),
			new SceneNameInfo("Pull Down To Refresh", "PullAndRefreshDemo"),
			new SceneNameInfo("TreeView\nWith Sticky Head", "TreeViewWithStickyHeadDemo"),
			new SceneNameInfo("Change Item Height", "ChangeItemHeightDemo"),
			new SceneNameInfo("Pull Up To Load More", "PullAndLoadMoreDemo"),
			new SceneNameInfo("Click Load More", "ClickAndLoadMoreDemo"),
			new SceneNameInfo("Select And Delete", "DeleteItemDemo"),
			new SceneNameInfo("GridView Select Delete ", "GridViewDeleteItemDemo"),
			new SceneNameInfo("Responsive GridView", "ResponsiveGridViewDemo"),
			new SceneNameInfo("TreeView\nWith Children Indent", "TreeViewWithChildrenIndentDemo")
		};
	}
}
