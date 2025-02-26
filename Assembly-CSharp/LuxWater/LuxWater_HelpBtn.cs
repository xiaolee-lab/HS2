using System;
using UnityEngine;

namespace LuxWater
{
	// Token: 0x020003D8 RID: 984
	public class LuxWater_HelpBtn : PropertyAttribute
	{
		// Token: 0x06001174 RID: 4468 RVA: 0x00066C8C File Offset: 0x0006508C
		public LuxWater_HelpBtn(string URL)
		{
			this.URL = URL;
		}

		// Token: 0x0400134B RID: 4939
		public string URL;
	}
}
