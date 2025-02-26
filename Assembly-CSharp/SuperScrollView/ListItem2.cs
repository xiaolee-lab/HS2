using System;
using UnityEngine;
using UnityEngine.UI;

namespace SuperScrollView
{
	// Token: 0x020005C0 RID: 1472
	public class ListItem2 : MonoBehaviour
	{
		// Token: 0x060021EB RID: 8683 RVA: 0x000BAAD8 File Offset: 0x000B8ED8
		public void Init()
		{
			for (int i = 0; i < this.mStarArray.Length; i++)
			{
				int index = i;
				ClickEventListener clickEventListener = ClickEventListener.Get(this.mStarArray[i].gameObject);
				clickEventListener.SetClickEventHandler(delegate(GameObject obj)
				{
					this.OnStarClicked(index);
				});
			}
		}

		// Token: 0x060021EC RID: 8684 RVA: 0x000BAB38 File Offset: 0x000B8F38
		private void OnStarClicked(int index)
		{
			ItemData itemDataByIndex = DataSourceMgr.Get.GetItemDataByIndex(this.mItemDataIndex);
			if (itemDataByIndex == null)
			{
				return;
			}
			if (index == 0 && itemDataByIndex.mStarCount == 1)
			{
				itemDataByIndex.mStarCount = 0;
			}
			else
			{
				itemDataByIndex.mStarCount = index + 1;
			}
			this.SetStarCount(itemDataByIndex.mStarCount);
		}

		// Token: 0x060021ED RID: 8685 RVA: 0x000BAB90 File Offset: 0x000B8F90
		public void SetStarCount(int count)
		{
			int i;
			for (i = 0; i < count; i++)
			{
				this.mStarArray[i].color = this.mRedStarColor;
			}
			while (i < this.mStarArray.Length)
			{
				this.mStarArray[i].color = this.mGrayStarColor;
				i++;
			}
		}

		// Token: 0x060021EE RID: 8686 RVA: 0x000BABF8 File Offset: 0x000B8FF8
		public void SetItemData(ItemData itemData, int itemIndex)
		{
			this.mItemDataIndex = itemIndex;
			this.mNameText.text = itemData.mName;
			this.mDescText.text = itemData.mFileSize.ToString() + "KB";
			this.mDescText2.text = itemData.mDesc;
			this.mIcon.sprite = ResManager.Get.GetSpriteByName(itemData.mIcon);
			this.SetStarCount(itemData.mStarCount);
		}

		// Token: 0x0400218D RID: 8589
		public Text mNameText;

		// Token: 0x0400218E RID: 8590
		public Image mIcon;

		// Token: 0x0400218F RID: 8591
		public Image[] mStarArray;

		// Token: 0x04002190 RID: 8592
		public Text mDescText;

		// Token: 0x04002191 RID: 8593
		public Text mDescText2;

		// Token: 0x04002192 RID: 8594
		public Color32 mRedStarColor = new Color32(249, 227, 101, byte.MaxValue);

		// Token: 0x04002193 RID: 8595
		public Color32 mGrayStarColor = new Color32(215, 215, 215, byte.MaxValue);

		// Token: 0x04002194 RID: 8596
		public GameObject mContentRootObj;

		// Token: 0x04002195 RID: 8597
		private int mItemDataIndex = -1;

		// Token: 0x04002196 RID: 8598
		public LoopListView2 mLoopListView;
	}
}
