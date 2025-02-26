using System;
using UnityEngine;

namespace SuperScrollView
{
	// Token: 0x020005B5 RID: 1461
	public class ListItem10 : MonoBehaviour
	{
		// Token: 0x060021C9 RID: 8649 RVA: 0x000BA260 File Offset: 0x000B8660
		public void Init()
		{
			foreach (ListItem9 listItem in this.mItemList)
			{
				listItem.Init();
			}
		}

		// Token: 0x0400215B RID: 8539
		public ListItem9[] mItemList;
	}
}
