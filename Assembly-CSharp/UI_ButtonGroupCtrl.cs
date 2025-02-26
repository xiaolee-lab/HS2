using System;
using System.Linq;
using Illusion.Extensions;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000A23 RID: 2595
public class UI_ButtonGroupCtrl : MonoBehaviour
{
	// Token: 0x06004D28 RID: 19752 RVA: 0x001D9D00 File Offset: 0x001D8100
	public virtual void Start()
	{
		if (this.items.Any<UI_ButtonGroupCtrl.ItemInfo>())
		{
			(from item in this.items.Select((UI_ButtonGroupCtrl.ItemInfo val, int idx) => new
			{
				val,
				idx
			})
			where item.val != null && item.val.btnItem != null
			select item).ToList().ForEach(delegate(item)
			{
				item.val.btnItem.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					foreach (var <>__AnonType in this.items.Select((UI_ButtonGroupCtrl.ItemInfo v, int i) => new
					{
						v,
						i
					}))
					{
						if (<>__AnonType.i != item.idx && <>__AnonType.v != null)
						{
							CanvasGroup cgItem = <>__AnonType.v.cgItem;
							if (null != cgItem)
							{
								cgItem.Enable(false, false);
							}
						}
					}
					if (null != item.val.cgItem)
					{
						item.val.cgItem.Enable(true, false);
					}
				});
			});
		}
	}

	// Token: 0x04004690 RID: 18064
	public UI_ButtonGroupCtrl.ItemInfo[] items;

	// Token: 0x02000A24 RID: 2596
	[Serializable]
	public class ItemInfo
	{
		// Token: 0x04004693 RID: 18067
		public Button btnItem;

		// Token: 0x04004694 RID: 18068
		public CanvasGroup cgItem;
	}
}
