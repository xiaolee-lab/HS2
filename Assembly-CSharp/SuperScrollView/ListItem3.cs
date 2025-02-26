using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SuperScrollView
{
	// Token: 0x020005C1 RID: 1473
	public class ListItem3 : MonoBehaviour
	{
		// Token: 0x060021F0 RID: 8688 RVA: 0x000BACA5 File Offset: 0x000B90A5
		public void Init()
		{
			this.mToggle.onValueChanged.AddListener(new UnityAction<bool>(this.OnToggleValueChanged));
		}

		// Token: 0x060021F1 RID: 8689 RVA: 0x000BACC4 File Offset: 0x000B90C4
		private void OnToggleValueChanged(bool check)
		{
			ItemData itemDataByIndex = DataSourceMgr.Get.GetItemDataByIndex(this.mItemIndex);
			if (itemDataByIndex == null)
			{
				return;
			}
			itemDataByIndex.mChecked = check;
		}

		// Token: 0x060021F2 RID: 8690 RVA: 0x000BACF0 File Offset: 0x000B90F0
		public void SetItemData(ItemData itemData, int itemIndex)
		{
			this.mItemIndex = itemIndex;
			this.mNameText.text = itemData.mName;
			this.mDescText.text = itemData.mDesc;
			this.mIcon.sprite = ResManager.Get.GetSpriteByName(itemData.mIcon);
			this.mToggle.isOn = itemData.mChecked;
		}

		// Token: 0x04002197 RID: 8599
		public Text mNameText;

		// Token: 0x04002198 RID: 8600
		public Image mIcon;

		// Token: 0x04002199 RID: 8601
		public Text mDescText;

		// Token: 0x0400219A RID: 8602
		private int mItemIndex = -1;

		// Token: 0x0400219B RID: 8603
		public Toggle mToggle;
	}
}
