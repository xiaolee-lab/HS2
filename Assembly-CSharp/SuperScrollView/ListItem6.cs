using System;
using System.Collections.Generic;
using UnityEngine;

namespace SuperScrollView
{
	// Token: 0x020005C4 RID: 1476
	public class ListItem6 : MonoBehaviour
	{
		// Token: 0x060021FD RID: 8701 RVA: 0x000BB258 File Offset: 0x000B9658
		public void Init()
		{
			foreach (ListItem5 listItem in this.mItemList)
			{
				listItem.Init();
			}
		}

		// Token: 0x040021AC RID: 8620
		public List<ListItem5> mItemList;
	}
}
