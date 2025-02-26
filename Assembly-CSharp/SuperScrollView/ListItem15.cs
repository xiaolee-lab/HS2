using System;
using System.Collections.Generic;
using UnityEngine;

namespace SuperScrollView
{
	// Token: 0x020005BB RID: 1467
	public class ListItem15 : MonoBehaviour
	{
		// Token: 0x060021DB RID: 8667 RVA: 0x000BA638 File Offset: 0x000B8A38
		public void Init()
		{
			foreach (ListItem16 listItem in this.mItemList)
			{
				listItem.Init();
			}
		}

		// Token: 0x04002172 RID: 8562
		public List<ListItem16> mItemList;
	}
}
