using System;
using UnityEngine;
using UnityEngine.UI;

namespace SuperScrollView
{
	// Token: 0x020005BE RID: 1470
	public class ListItem18 : MonoBehaviour
	{
		// Token: 0x060021E0 RID: 8672 RVA: 0x000BA700 File Offset: 0x000B8B00
		public void Init()
		{
			ClickEventListener clickEventListener = ClickEventListener.Get(this.mStarIcon.gameObject);
			clickEventListener.SetClickEventHandler(new Action<GameObject>(this.OnStarClicked));
		}

		// Token: 0x060021E1 RID: 8673 RVA: 0x000BA730 File Offset: 0x000B8B30
		private void OnStarClicked(GameObject obj)
		{
			ItemData itemDataByIndex = DataSourceMgr.Get.GetItemDataByIndex(this.mItemDataIndex);
			if (itemDataByIndex == null)
			{
				return;
			}
			if (itemDataByIndex.mStarCount == 5)
			{
				itemDataByIndex.mStarCount = 0;
			}
			else
			{
				itemDataByIndex.mStarCount++;
			}
			this.SetStarCount(itemDataByIndex.mStarCount);
		}

		// Token: 0x060021E2 RID: 8674 RVA: 0x000BA788 File Offset: 0x000B8B88
		public void SetStarCount(int count)
		{
			this.mStarCount.text = count.ToString();
			if (count == 0)
			{
				this.mStarIcon.color = this.mGrayStarColor;
			}
			else
			{
				this.mStarIcon.color = this.mRedStarColor;
			}
		}

		// Token: 0x060021E3 RID: 8675 RVA: 0x000BA7E4 File Offset: 0x000B8BE4
		public void SetItemData(ItemData itemData, int itemIndex, int row, int column)
		{
			this.mItemDataIndex = itemIndex;
			this.mNameText.text = itemData.mName;
			this.mRowText.text = "Row: " + row;
			this.mColumnText.text = "Column: " + column;
			this.mIcon.sprite = ResManager.Get.GetSpriteByName(itemData.mIcon);
			this.SetStarCount(itemData.mStarCount);
		}

		// Token: 0x04002179 RID: 8569
		public Text mNameText;

		// Token: 0x0400217A RID: 8570
		public Image mIcon;

		// Token: 0x0400217B RID: 8571
		public Image mStarIcon;

		// Token: 0x0400217C RID: 8572
		public Text mStarCount;

		// Token: 0x0400217D RID: 8573
		public Text mRowText;

		// Token: 0x0400217E RID: 8574
		public Text mColumnText;

		// Token: 0x0400217F RID: 8575
		public Color32 mRedStarColor = new Color32(236, 217, 103, byte.MaxValue);

		// Token: 0x04002180 RID: 8576
		public Color32 mGrayStarColor = new Color32(215, 215, 215, byte.MaxValue);

		// Token: 0x04002181 RID: 8577
		private int mItemDataIndex = -1;

		// Token: 0x04002182 RID: 8578
		public GameObject mContentRootObj;
	}
}
