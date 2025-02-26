using System;

namespace UnityEngine.UI.ProceduralImage
{
	// Token: 0x02000627 RID: 1575
	[AttributeUsage(AttributeTargets.Class)]
	public class ModifierID : Attribute
	{
		// Token: 0x06002587 RID: 9607 RVA: 0x000D6C42 File Offset: 0x000D5042
		public ModifierID(string name)
		{
			this.name = name;
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06002588 RID: 9608 RVA: 0x000D6C51 File Offset: 0x000D5051
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04002557 RID: 9559
		private string name;
	}
}
