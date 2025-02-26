using System;
using System.Linq;
using Illusion.Extensions;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000A2E RID: 2606
public class UI_ToggleGroupCtrl : MonoBehaviour
{
	// Token: 0x06004D6B RID: 19819 RVA: 0x001D6BB8 File Offset: 0x001D4FB8
	public virtual void Start()
	{
		if (this.items.Any<UI_ToggleGroupCtrl.ItemInfo>())
		{
			(from item in this.items.Select((UI_ToggleGroupCtrl.ItemInfo val, int idx) => new
			{
				val,
				idx
			})
			where item.val != null && item.val.tglItem != null
			select item).ToList().ForEach(delegate(item)
			{
				(from isOn in item.val.tglItem.OnValueChangedAsObservable()
				where isOn
				select isOn).Subscribe(delegate(bool _)
				{
					foreach (var <>__AnonType in this.items.Select((UI_ToggleGroupCtrl.ItemInfo v, int i) => new
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

	// Token: 0x06004D6C RID: 19820 RVA: 0x001D6C38 File Offset: 0x001D5038
	public int GetSelectIndex()
	{
		var <>__AnonType = this.items.Select((UI_ToggleGroupCtrl.ItemInfo v, int i) => new
		{
			v,
			i
		}).FirstOrDefault(x => x.v.tglItem.isOn);
		if (<>__AnonType != null)
		{
			return <>__AnonType.i;
		}
		return -1;
	}

	// Token: 0x040046C8 RID: 18120
	public UI_ToggleGroupCtrl.ItemInfo[] items;

	// Token: 0x02000A2F RID: 2607
	[Serializable]
	public class ItemInfo
	{
		// Token: 0x040046CE RID: 18126
		public Toggle tglItem;

		// Token: 0x040046CF RID: 18127
		public CanvasGroup cgItem;
	}
}
