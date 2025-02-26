using System;
using System.Linq;
using Illusion.Extensions;
using UniRx;
using UnityEngine;

// Token: 0x02000A21 RID: 2593
public class UI_ButtonExGroupCtrl : MonoBehaviour
{
	// Token: 0x06004D22 RID: 19746 RVA: 0x001D9B1C File Offset: 0x001D7F1C
	public virtual void Start()
	{
		if (this.items.Any<UI_ButtonExGroupCtrl.ItemInfo>())
		{
			(from item in this.items.Select((UI_ButtonExGroupCtrl.ItemInfo val, int idx) => new
			{
				val,
				idx
			})
			where item.val != null && item.val.btnItem != null
			select item).ToList().ForEach(delegate(item)
			{
				item.val.btnItem.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					foreach (UI_ButtonExGroupCtrl.ItemInfo itemInfo in this.items)
					{
						if (itemInfo != null && itemInfo.btnItem != item.val.btnItem)
						{
							foreach (CanvasGroup canvasGroup in itemInfo.cgItem)
							{
								if (null != canvasGroup)
								{
									canvasGroup.Enable(false, false);
								}
							}
						}
					}
					foreach (CanvasGroup canvasGroup2 in item.val.cgItem)
					{
						if (null != canvasGroup2)
						{
							canvasGroup2.Enable(true, false);
						}
					}
				});
			});
		}
	}

	// Token: 0x0400468B RID: 18059
	public UI_ButtonExGroupCtrl.ItemInfo[] items;

	// Token: 0x02000A22 RID: 2594
	[Serializable]
	public class ItemInfo
	{
		// Token: 0x0400468E RID: 18062
		public UI_ButtonEx btnItem;

		// Token: 0x0400468F RID: 18063
		public CanvasGroup[] cgItem;
	}
}
