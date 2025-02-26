using System;
using UnityEngine;
using UnityEngine.UI;

namespace Housing.SaveLoad
{
	// Token: 0x020008AD RID: 2221
	public class SizeTab : MonoBehaviour
	{
		// Token: 0x17000A6A RID: 2666
		// (set) Token: 0x060039D9 RID: 14809 RVA: 0x00153D33 File Offset: 0x00152133
		public string Text
		{
			set
			{
				this.text.text = value;
			}
		}

		// Token: 0x04003955 RID: 14677
		public Toggle toggle;

		// Token: 0x04003956 RID: 14678
		public Text text;
	}
}
