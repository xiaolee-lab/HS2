using System;
using System.Linq;
using Illusion.Extensions;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CharaCustom
{
	// Token: 0x02000A0D RID: 2573
	public class CvsSelectWindow : MonoBehaviour
	{
		// Token: 0x06004CA0 RID: 19616 RVA: 0x001D733C File Offset: 0x001D573C
		public virtual void Start()
		{
			if (this.items.Any<CvsSelectWindow.ItemInfo>())
			{
				(from item in this.items.Select((CvsSelectWindow.ItemInfo val, int idx) => new
				{
					val,
					idx
				})
				where item.val != null && item.val.btnItem != null
				select item).ToList().ForEach(delegate(item)
				{
					item.val.btnItem.OnClickAsObservable().Subscribe(delegate(Unit _)
					{
						foreach (CvsSelectWindow.ItemInfo itemInfo in this.items)
						{
							if (itemInfo != null && itemInfo.btnItem != item.val.btnItem)
							{
								foreach (CanvasGroup canvasGroup in itemInfo.cgItem)
								{
									if (canvasGroup)
									{
										canvasGroup.Enable(false, false);
									}
								}
							}
						}
						foreach (CanvasGroup canvasGroup2 in item.val.cgItem)
						{
							if (canvasGroup2)
							{
								canvasGroup2.Enable(true, false);
							}
						}
						if (this.cgBaseWindow)
						{
							this.cgBaseWindow.Enable(true, false);
						}
						if (this.backSelect != item.idx)
						{
							if (item.val.cvsBase)
							{
								this.titleText = item.val.btnItem.GetComponentInChildren<Text>();
								if (this.titleText && item.val.cvsBase.titleText)
								{
									item.val.cvsBase.titleText.text = this.titleText.text;
								}
								item.val.cvsBase.SNo = item.val.No;
								item.val.cvsBase.UpdateCustomUI();
								item.val.cvsBase.ChangeMenuFunc();
							}
							CustomColorCtrl customColorCtrl = Singleton<CustomBase>.Instance.customColorCtrl;
							if (customColorCtrl)
							{
								customColorCtrl.Close();
							}
							this.backSelect = item.idx;
						}
					});
				});
			}
			if (Singleton<CustomBase>.Instance.modeNew)
			{
				if (this.btnNewFirst != null)
				{
					this.btnNewFirst.onClick.Invoke();
				}
			}
			else if (this.btnEditFirst != null)
			{
				this.btnEditFirst.onClick.Invoke();
			}
		}

		// Token: 0x0400463C RID: 17980
		public CanvasGroup cgBaseWindow;

		// Token: 0x0400463D RID: 17981
		public UI_ButtonEx btnNewFirst;

		// Token: 0x0400463E RID: 17982
		public UI_ButtonEx btnEditFirst;

		// Token: 0x0400463F RID: 17983
		public CvsSelectWindow.ItemInfo[] items;

		// Token: 0x04004640 RID: 17984
		private int backSelect = -1;

		// Token: 0x04004641 RID: 17985
		private Text titleText;

		// Token: 0x02000A0E RID: 2574
		[Serializable]
		public class ItemInfo
		{
			// Token: 0x04004644 RID: 17988
			public UI_ButtonEx btnItem;

			// Token: 0x04004645 RID: 17989
			public CanvasGroup[] cgItem;

			// Token: 0x04004646 RID: 17990
			public CvsBase cvsBase;

			// Token: 0x04004647 RID: 17991
			public int No;
		}
	}
}
