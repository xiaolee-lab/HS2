using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SuperScrollView
{
	// Token: 0x020005C6 RID: 1478
	public class ListItem8 : MonoBehaviour
	{
		// Token: 0x06002203 RID: 8707 RVA: 0x000BB328 File Offset: 0x000B9728
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
			this.mExpandBtn.onClick.AddListener(new UnityAction(this.OnExpandBtnClicked));
		}

		// Token: 0x06002204 RID: 8708 RVA: 0x000BB3A4 File Offset: 0x000B97A4
		public void OnExpandChanged()
		{
			RectTransform component = base.gameObject.GetComponent<RectTransform>();
			if (this.mIsExpand)
			{
				component.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 284f);
				this.mExpandContentRoot.SetActive(true);
				this.mClickTip.text = "Shrink";
			}
			else
			{
				component.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 143f);
				this.mExpandContentRoot.SetActive(false);
				this.mClickTip.text = "Expand";
			}
		}

		// Token: 0x06002205 RID: 8709 RVA: 0x000BB420 File Offset: 0x000B9820
		private void OnExpandBtnClicked()
		{
			ItemData itemDataByIndex = DataSourceMgr.Get.GetItemDataByIndex(this.mItemDataIndex);
			if (itemDataByIndex == null)
			{
				return;
			}
			this.mIsExpand = !this.mIsExpand;
			itemDataByIndex.mIsExpand = this.mIsExpand;
			this.OnExpandChanged();
			LoopListViewItem2 component = base.gameObject.GetComponent<LoopListViewItem2>();
			component.ParentListView.OnItemSizeChanged(component.ItemIndex);
		}

		// Token: 0x06002206 RID: 8710 RVA: 0x000BB484 File Offset: 0x000B9884
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

		// Token: 0x06002207 RID: 8711 RVA: 0x000BB4DC File Offset: 0x000B98DC
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

		// Token: 0x06002208 RID: 8712 RVA: 0x000BB544 File Offset: 0x000B9944
		public void SetItemData(ItemData itemData, int itemIndex)
		{
			this.mItemDataIndex = itemIndex;
			this.mNameText.text = itemData.mName;
			this.mDescText.text = itemData.mFileSize.ToString() + "KB";
			this.mIcon.sprite = ResManager.Get.GetSpriteByName(itemData.mIcon);
			this.SetStarCount(itemData.mStarCount);
			this.mIsExpand = itemData.mIsExpand;
			this.OnExpandChanged();
		}

		// Token: 0x040021AF RID: 8623
		public Text mNameText;

		// Token: 0x040021B0 RID: 8624
		public Image mIcon;

		// Token: 0x040021B1 RID: 8625
		public Image[] mStarArray;

		// Token: 0x040021B2 RID: 8626
		public Text mDescText;

		// Token: 0x040021B3 RID: 8627
		public GameObject mExpandContentRoot;

		// Token: 0x040021B4 RID: 8628
		public Text mClickTip;

		// Token: 0x040021B5 RID: 8629
		public Button mExpandBtn;

		// Token: 0x040021B6 RID: 8630
		public Color32 mRedStarColor = new Color32(249, 227, 101, byte.MaxValue);

		// Token: 0x040021B7 RID: 8631
		public Color32 mGrayStarColor = new Color32(215, 215, 215, byte.MaxValue);

		// Token: 0x040021B8 RID: 8632
		private int mItemDataIndex = -1;

		// Token: 0x040021B9 RID: 8633
		private bool mIsExpand;
	}
}
