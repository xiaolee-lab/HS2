using System;
using UnityEngine;
using UnityEngine.UI;

namespace SuperScrollView
{
	// Token: 0x020005B8 RID: 1464
	public class ListItem13 : MonoBehaviour
	{
		// Token: 0x060021D3 RID: 8659 RVA: 0x000BA3C4 File Offset: 0x000B87C4
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

		// Token: 0x060021D4 RID: 8660 RVA: 0x000BA424 File Offset: 0x000B8824
		private void OnStarClicked(int index)
		{
			ItemData itemChildDataByIndex = TreeViewDataSourceMgr.Get.GetItemChildDataByIndex(this.mItemDataIndex, this.mChildDataIndex);
			if (itemChildDataByIndex == null)
			{
				return;
			}
			if (index == 0 && itemChildDataByIndex.mStarCount == 1)
			{
				itemChildDataByIndex.mStarCount = 0;
			}
			else
			{
				itemChildDataByIndex.mStarCount = index + 1;
			}
			this.SetStarCount(itemChildDataByIndex.mStarCount);
		}

		// Token: 0x060021D5 RID: 8661 RVA: 0x000BA484 File Offset: 0x000B8884
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

		// Token: 0x060021D6 RID: 8662 RVA: 0x000BA4EC File Offset: 0x000B88EC
		public void SetItemData(ItemData itemData, int itemIndex, int childIndex)
		{
			this.mItemDataIndex = itemIndex;
			this.mChildDataIndex = childIndex;
			this.mNameText.text = itemData.mName;
			this.mDescText.text = itemData.mFileSize.ToString() + "KB";
			this.mDescText2.text = itemData.mDesc;
			this.mIcon.sprite = ResManager.Get.GetSpriteByName(itemData.mIcon);
			this.SetStarCount(itemData.mStarCount);
		}

		// Token: 0x04002164 RID: 8548
		public Text mNameText;

		// Token: 0x04002165 RID: 8549
		public Image mIcon;

		// Token: 0x04002166 RID: 8550
		public Image[] mStarArray;

		// Token: 0x04002167 RID: 8551
		public Text mDescText;

		// Token: 0x04002168 RID: 8552
		public Text mDescText2;

		// Token: 0x04002169 RID: 8553
		public Color32 mRedStarColor = new Color32(249, 227, 101, byte.MaxValue);

		// Token: 0x0400216A RID: 8554
		public Color32 mGrayStarColor = new Color32(215, 215, 215, byte.MaxValue);

		// Token: 0x0400216B RID: 8555
		public GameObject mContentRootObj;

		// Token: 0x0400216C RID: 8556
		private int mItemDataIndex = -1;

		// Token: 0x0400216D RID: 8557
		private int mChildDataIndex = -1;
	}
}
