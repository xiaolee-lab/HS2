using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SuperScrollView
{
	// Token: 0x020005C7 RID: 1479
	public class ListItem9 : MonoBehaviour
	{
		// Token: 0x0600220A RID: 8714 RVA: 0x000BB63C File Offset: 0x000B9A3C
		public void Init()
		{
			ClickEventListener clickEventListener = ClickEventListener.Get(this.mStarIcon.gameObject);
			clickEventListener.SetClickEventHandler(new Action<GameObject>(this.OnStarClicked));
			this.mToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnToggleValueChanged));
		}

		// Token: 0x0600220B RID: 8715 RVA: 0x000BB688 File Offset: 0x000B9A88
		private void OnToggleValueChanged(bool check)
		{
			ItemData itemDataByIndex = DataSourceMgr.Get.GetItemDataByIndex(this.mItemDataIndex);
			if (itemDataByIndex == null)
			{
				return;
			}
			itemDataByIndex.mChecked = check;
		}

		// Token: 0x0600220C RID: 8716 RVA: 0x000BB6B4 File Offset: 0x000B9AB4
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

		// Token: 0x0600220D RID: 8717 RVA: 0x000BB70C File Offset: 0x000B9B0C
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

		// Token: 0x0600220E RID: 8718 RVA: 0x000BB768 File Offset: 0x000B9B68
		public void SetItemData(ItemData itemData, int itemIndex)
		{
			this.mItemDataIndex = itemIndex;
			this.mNameText.text = itemData.mName;
			this.mDescText.text = itemData.mFileSize.ToString() + "KB";
			this.mIcon.sprite = ResManager.Get.GetSpriteByName(itemData.mIcon);
			this.SetStarCount(itemData.mStarCount);
			this.mToggle.isOn = itemData.mChecked;
		}

		// Token: 0x040021BA RID: 8634
		public Text mNameText;

		// Token: 0x040021BB RID: 8635
		public Image mIcon;

		// Token: 0x040021BC RID: 8636
		public Image mStarIcon;

		// Token: 0x040021BD RID: 8637
		public Text mStarCount;

		// Token: 0x040021BE RID: 8638
		public Text mDescText;

		// Token: 0x040021BF RID: 8639
		public Color32 mRedStarColor = new Color32(236, 217, 103, byte.MaxValue);

		// Token: 0x040021C0 RID: 8640
		public Color32 mGrayStarColor = new Color32(215, 215, 215, byte.MaxValue);

		// Token: 0x040021C1 RID: 8641
		public Toggle mToggle;

		// Token: 0x040021C2 RID: 8642
		private int mItemDataIndex = -1;
	}
}
