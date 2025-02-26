using System;

namespace ADV.Commands.Base
{
	// Token: 0x020006F8 RID: 1784
	public class Tag : CommandBase
	{
		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06002A7B RID: 10875 RVA: 0x000F6E81 File Offset: 0x000F5281
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"Label"
				};
			}
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06002A7C RID: 10876 RVA: 0x000F6E91 File Offset: 0x000F5291
		public override string[] ArgsDefault
		{
			get
			{
				return null;
			}
		}
	}
}
