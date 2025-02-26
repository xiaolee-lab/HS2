using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SuperScrollView
{
	// Token: 0x020005BA RID: 1466
	public class ListItem14 : MonoBehaviour
	{
		// Token: 0x060021D9 RID: 8665 RVA: 0x000BA5AC File Offset: 0x000B89AC
		public void Init()
		{
			int childCount = base.transform.childCount;
			for (int i = 0; i < childCount; i++)
			{
				Transform child = base.transform.GetChild(i);
				ListItem14Elem listItem14Elem = new ListItem14Elem();
				listItem14Elem.mRootObj = child.gameObject;
				listItem14Elem.mIcon = child.Find("ItemIcon").GetComponent<Image>();
				listItem14Elem.mName = child.Find("ItemIcon/name").GetComponent<Text>();
				this.mElemItemList.Add(listItem14Elem);
			}
		}

		// Token: 0x04002171 RID: 8561
		public List<ListItem14Elem> mElemItemList = new List<ListItem14Elem>();
	}
}
