using System;
using UnityEngine;
using UnityEngine.UI;

namespace SuperScrollView
{
	// Token: 0x020005C5 RID: 1477
	public class ListItem7 : MonoBehaviour
	{
		// Token: 0x060021FF RID: 8703 RVA: 0x000BB2BC File Offset: 0x000B96BC
		public void Init()
		{
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06002200 RID: 8704 RVA: 0x000BB2BE File Offset: 0x000B96BE
		// (set) Token: 0x06002201 RID: 8705 RVA: 0x000BB2C6 File Offset: 0x000B96C6
		public int Value
		{
			get
			{
				return this.mValue;
			}
			set
			{
				this.mValue = value;
			}
		}

		// Token: 0x040021AD RID: 8621
		public Text mText;

		// Token: 0x040021AE RID: 8622
		public int mValue;
	}
}
