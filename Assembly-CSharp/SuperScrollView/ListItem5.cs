using System;
using UnityEngine;
using UnityEngine.UI;

namespace SuperScrollView
{
	// Token: 0x020005C3 RID: 1475
	public class ListItem5 : MonoBehaviour
	{
		// Token: 0x060021F8 RID: 8696 RVA: 0x000BB0F8 File Offset: 0x000B94F8
		public void Init()
		{
			ClickEventListener clickEventListener = ClickEventListener.Get(this.mStarIcon.gameObject);
			clickEventListener.SetClickEventHandler(new Action<GameObject>(this.OnStarClicked));
		}

		// Token: 0x060021F9 RID: 8697 RVA: 0x000BB128 File Offset: 0x000B9528
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

		// Token: 0x060021FA RID: 8698 RVA: 0x000BB180 File Offset: 0x000B9580
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

		// Token: 0x060021FB RID: 8699 RVA: 0x000BB1DC File Offset: 0x000B95DC
		public void SetItemData(ItemData itemData, int itemIndex)
		{
			this.mItemDataIndex = itemIndex;
			this.mNameText.text = itemData.mName;
			this.mDescText.text = itemData.mFileSize.ToString() + "KB";
			this.mIcon.sprite = ResManager.Get.GetSpriteByName(itemData.mIcon);
			this.SetStarCount(itemData.mStarCount);
		}

		// Token: 0x040021A3 RID: 8611
		public Text mNameText;

		// Token: 0x040021A4 RID: 8612
		public Image mIcon;

		// Token: 0x040021A5 RID: 8613
		public Image mStarIcon;

		// Token: 0x040021A6 RID: 8614
		public Text mStarCount;

		// Token: 0x040021A7 RID: 8615
		public Text mDescText;

		// Token: 0x040021A8 RID: 8616
		public Color32 mRedStarColor = new Color32(236, 217, 103, byte.MaxValue);

		// Token: 0x040021A9 RID: 8617
		public Color32 mGrayStarColor = new Color32(215, 215, 215, byte.MaxValue);

		// Token: 0x040021AA RID: 8618
		private int mItemDataIndex = -1;

		// Token: 0x040021AB RID: 8619
		public GameObject mContentRootObj;
	}
}
