using System;
using UnityEngine;
using UnityEngine.UI;

namespace SuperScrollView
{
	// Token: 0x020005C2 RID: 1474
	public class ListItem4 : MonoBehaviour
	{
		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x060021F4 RID: 8692 RVA: 0x000BAD61 File Offset: 0x000B9161
		public int ItemIndex
		{
			get
			{
				return this.mItemIndex;
			}
		}

		// Token: 0x060021F5 RID: 8693 RVA: 0x000BAD69 File Offset: 0x000B9169
		public void Init()
		{
		}

		// Token: 0x060021F6 RID: 8694 RVA: 0x000BAD6C File Offset: 0x000B916C
		public void SetItemData(ChatMsg itemData, int itemIndex)
		{
			this.mIndexText.text = itemIndex.ToString();
			PersonInfo personInfo = ChatMsgDataSourceMgr.Get.GetPersonInfo(itemData.mPersonId);
			this.mItemIndex = itemIndex;
			if (itemData.mMsgType == MsgTypeEnum.Str)
			{
				this.mMsgPic.gameObject.SetActive(false);
				this.mMsgText.gameObject.SetActive(true);
				this.mMsgText.text = itemData.mSrtMsg;
				this.mMsgText.GetComponent<ContentSizeFitter>().SetLayoutVertical();
				this.mIcon.sprite = ResManager.Get.GetSpriteByName(personInfo.mHeadIcon);
				Vector2 sizeDelta = this.mItemBg.GetComponent<RectTransform>().sizeDelta;
				sizeDelta.x = this.mMsgText.GetComponent<RectTransform>().sizeDelta.x + 20f;
				sizeDelta.y = this.mMsgText.GetComponent<RectTransform>().sizeDelta.y + 20f;
				this.mItemBg.GetComponent<RectTransform>().sizeDelta = sizeDelta;
				if (personInfo.mId == 0)
				{
					this.mItemBg.color = new Color32(160, 231, 90, byte.MaxValue);
					this.mArrow.color = this.mItemBg.color;
				}
				else
				{
					this.mItemBg.color = Color.white;
					this.mArrow.color = this.mItemBg.color;
				}
				RectTransform component = base.gameObject.GetComponent<RectTransform>();
				float num = sizeDelta.y;
				if (num < 75f)
				{
					num = 75f;
				}
				component.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, num);
			}
			else
			{
				this.mMsgPic.gameObject.SetActive(true);
				this.mMsgText.gameObject.SetActive(false);
				this.mMsgPic.sprite = ResManager.Get.GetSpriteByName(itemData.mPicMsgSpriteName);
				this.mMsgPic.SetNativeSize();
				this.mIcon.sprite = ResManager.Get.GetSpriteByName(personInfo.mHeadIcon);
				Vector2 sizeDelta2 = this.mItemBg.GetComponent<RectTransform>().sizeDelta;
				sizeDelta2.x = this.mMsgPic.GetComponent<RectTransform>().sizeDelta.x + 20f;
				sizeDelta2.y = this.mMsgPic.GetComponent<RectTransform>().sizeDelta.y + 20f;
				this.mItemBg.GetComponent<RectTransform>().sizeDelta = sizeDelta2;
				if (personInfo.mId == 0)
				{
					this.mItemBg.color = new Color32(160, 231, 90, byte.MaxValue);
					this.mArrow.color = this.mItemBg.color;
				}
				else
				{
					this.mItemBg.color = Color.white;
					this.mArrow.color = this.mItemBg.color;
				}
				RectTransform component2 = base.gameObject.GetComponent<RectTransform>();
				float num2 = sizeDelta2.y;
				if (num2 < 75f)
				{
					num2 = 75f;
				}
				component2.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, num2);
			}
		}

		// Token: 0x0400219C RID: 8604
		public Text mMsgText;

		// Token: 0x0400219D RID: 8605
		public Image mMsgPic;

		// Token: 0x0400219E RID: 8606
		public Image mIcon;

		// Token: 0x0400219F RID: 8607
		public Image mItemBg;

		// Token: 0x040021A0 RID: 8608
		public Image mArrow;

		// Token: 0x040021A1 RID: 8609
		public Text mIndexText;

		// Token: 0x040021A2 RID: 8610
		private int mItemIndex = -1;
	}
}
