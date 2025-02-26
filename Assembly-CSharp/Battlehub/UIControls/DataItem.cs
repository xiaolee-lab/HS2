using System;
using System.Collections.Generic;

namespace Battlehub.UIControls
{
	// Token: 0x02000098 RID: 152
	public class DataItem
	{
		// Token: 0x0600026D RID: 621 RVA: 0x0001036B File Offset: 0x0000E76B
		public DataItem(string name)
		{
			this.Name = name;
			this.Children = new List<DataItem>();
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00010385 File Offset: 0x0000E785
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x040002A0 RID: 672
		public string Name;

		// Token: 0x040002A1 RID: 673
		public DataItem Parent;

		// Token: 0x040002A2 RID: 674
		public List<DataItem> Children;
	}
}
