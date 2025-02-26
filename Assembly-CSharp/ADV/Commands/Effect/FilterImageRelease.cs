using System;

namespace ADV.Commands.Effect
{
	// Token: 0x02000739 RID: 1849
	public class FilterImageRelease : CommandBase
	{
		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06002BCC RID: 11212 RVA: 0x000FD140 File Offset: 0x000FB540
		public override string[] ArgsLabel
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06002BCD RID: 11213 RVA: 0x000FD143 File Offset: 0x000FB543
		public override string[] ArgsDefault
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002BCE RID: 11214 RVA: 0x000FD146 File Offset: 0x000FB546
		public override void Do()
		{
			base.Do();
			base.scenario.FilterImage.enabled = false;
			base.scenario.FilterImage.sprite = null;
		}
	}
}
