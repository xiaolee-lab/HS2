using System;
using System.Linq;
using Illusion.Extensions;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CharaCustom
{
	// Token: 0x02000A0F RID: 2575
	public class CvsSettingWindow : MonoBehaviour
	{
		// Token: 0x06004CA6 RID: 19622 RVA: 0x001D782C File Offset: 0x001D5C2C
		public virtual void Start()
		{
			if (this.btnClose.Any<Button>())
			{
				(from item in this.btnClose
				where null != item
				select item).ToList<Button>().ForEach(delegate(Button item)
				{
					item.OnClickAsObservable().Subscribe(delegate(Unit _)
					{
						if (this.cgbaseWindow)
						{
							this.cgbaseWindow.Enable(false, false);
						}
					});
				});
			}
		}

		// Token: 0x04004648 RID: 17992
		public CanvasGroup cgbaseWindow;

		// Token: 0x04004649 RID: 17993
		public Button[] btnClose;
	}
}
