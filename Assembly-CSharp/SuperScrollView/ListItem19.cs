using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SuperScrollView
{
	// Token: 0x020005BF RID: 1471
	public class ListItem19 : MonoBehaviour
	{
		// Token: 0x060021E5 RID: 8677 RVA: 0x000BA8C0 File Offset: 0x000B8CC0
		public void Init()
		{
			ClickEventListener clickEventListener = ClickEventListener.Get(this.mStarIcon.gameObject);
			clickEventListener.SetClickEventHandler(new Action<GameObject>(this.OnStarClicked));
			this.mToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnToggleValueChanged));
		}

		// Token: 0x060021E6 RID: 8678 RVA: 0x000BA90C File Offset: 0x000B8D0C
		private void OnToggleValueChanged(bool check)
		{
			ItemData itemDataByIndex = DataSourceMgr.Get.GetItemDataByIndex(this.mItemDataIndex);
			if (itemDataByIndex == null)
			{
				return;
			}
			itemDataByIndex.mChecked = check;
		}

		// Token: 0x060021E7 RID: 8679 RVA: 0x000BA938 File Offset: 0x000B8D38
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

		// Token: 0x060021E8 RID: 8680 RVA: 0x000BA990 File Offset: 0x000B8D90
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

		// Token: 0x060021E9 RID: 8681 RVA: 0x000BA9EC File Offset: 0x000B8DEC
		public void SetItemData(ItemData itemData, int itemIndex, int row, int column)
		{
			this.mItemDataIndex = itemIndex;
			this.mNameText.text = itemData.mName;
			this.mRowText.text = "Row: " + row;
			this.mColumnText.text = "Column: " + column;
			this.mIcon.sprite = ResManager.Get.GetSpriteByName(itemData.mIcon);
			this.SetStarCount(itemData.mStarCount);
			this.mToggle.isOn = itemData.mChecked;
		}

		// Token: 0x04002183 RID: 8579
		public Text mNameText;

		// Token: 0x04002184 RID: 8580
		public Image mIcon;

		// Token: 0x04002185 RID: 8581
		public Image mStarIcon;

		// Token: 0x04002186 RID: 8582
		public Text mStarCount;

		// Token: 0x04002187 RID: 8583
		public Text mRowText;

		// Token: 0x04002188 RID: 8584
		public Text mColumnText;

		// Token: 0x04002189 RID: 8585
		public Color32 mRedStarColor = new Color32(236, 217, 103, byte.MaxValue);

		// Token: 0x0400218A RID: 8586
		public Color32 mGrayStarColor = new Color32(215, 215, 215, byte.MaxValue);

		// Token: 0x0400218B RID: 8587
		public Toggle mToggle;

		// Token: 0x0400218C RID: 8588
		private int mItemDataIndex = -1;
	}
}
