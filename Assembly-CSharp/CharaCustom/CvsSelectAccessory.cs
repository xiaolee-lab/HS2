using System;
using System.Linq;
using UniRx;

namespace CharaCustom
{
	// Token: 0x02000A0C RID: 2572
	public class CvsSelectAccessory : CvsSelectWindow
	{
		// Token: 0x06004C9B RID: 19611 RVA: 0x001D76CC File Offset: 0x001D5ACC
		public override void Start()
		{
			base.Start();
			Singleton<CustomBase>.Instance.ChangeAcsSlotColor(0);
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
						int idx = item.idx;
						if (idx < 0 || 19 < idx)
						{
						}
						Singleton<CustomBase>.Instance.ChangeAcsSlotColor(item.idx);
					});
				});
			}
		}
	}
}
