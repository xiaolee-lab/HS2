using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x02000562 RID: 1378
	[AddComponentMenu("")]
	public class UIControl : MonoBehaviour
	{
		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06001D02 RID: 7426 RVA: 0x000AB1C5 File Offset: 0x000A95C5
		public int id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x06001D03 RID: 7427 RVA: 0x000AB1CD File Offset: 0x000A95CD
		private void Awake()
		{
			this._id = UIControl.GetNextUid();
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06001D04 RID: 7428 RVA: 0x000AB1DA File Offset: 0x000A95DA
		// (set) Token: 0x06001D05 RID: 7429 RVA: 0x000AB1E2 File Offset: 0x000A95E2
		public bool showTitle
		{
			get
			{
				return this._showTitle;
			}
			set
			{
				if (this.title == null)
				{
					return;
				}
				this.title.gameObject.SetActive(value);
				this._showTitle = value;
			}
		}

		// Token: 0x06001D06 RID: 7430 RVA: 0x000AB20E File Offset: 0x000A960E
		public virtual void SetCancelCallback(Action cancelCallback)
		{
		}

		// Token: 0x06001D07 RID: 7431 RVA: 0x000AB210 File Offset: 0x000A9610
		private static int GetNextUid()
		{
			if (UIControl._uidCounter == 2147483647)
			{
				UIControl._uidCounter = 0;
			}
			int uidCounter = UIControl._uidCounter;
			UIControl._uidCounter++;
			return uidCounter;
		}

		// Token: 0x04001E23 RID: 7715
		public Text title;

		// Token: 0x04001E24 RID: 7716
		private int _id;

		// Token: 0x04001E25 RID: 7717
		private bool _showTitle;

		// Token: 0x04001E26 RID: 7718
		private static int _uidCounter;
	}
}
