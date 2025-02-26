using System;
using Illusion.Extensions;

namespace ADV
{
	// Token: 0x02000781 RID: 1921
	[Serializable]
	public class Regulate
	{
		// Token: 0x06002CF8 RID: 11512 RVA: 0x0010138F File Offset: 0x000FF78F
		public Regulate(TextScenario scenario)
		{
			this.scenario = scenario;
		}

		// Token: 0x06002CF9 RID: 11513 RVA: 0x0010139E File Offset: 0x000FF79E
		public void AddRegulate(Regulate.Control regulate)
		{
			this.control = (Regulate.Control)this.control.Add(regulate);
		}

		// Token: 0x06002CFA RID: 11514 RVA: 0x001013BD File Offset: 0x000FF7BD
		public void SubRegulate(Regulate.Control regulate)
		{
			this.control = (Regulate.Control)this.control.Sub(regulate);
		}

		// Token: 0x06002CFB RID: 11515 RVA: 0x001013DC File Offset: 0x000FF7DC
		public void SetRegulate(Regulate.Control regulate)
		{
			this.control = regulate;
			if (this.control == (Regulate.Control)0)
			{
				this.scenario.isSkip = false;
				this.scenario.isAuto = false;
			}
		}

		// Token: 0x04002B91 RID: 11153
		[EnumFlags]
		public Regulate.Control control;

		// Token: 0x04002B92 RID: 11154
		private TextScenario scenario;

		// Token: 0x02000782 RID: 1922
		public enum Control
		{
			// Token: 0x04002B94 RID: 11156
			Next = 1,
			// Token: 0x04002B95 RID: 11157
			ClickNext,
			// Token: 0x04002B96 RID: 11158
			Skip = 4,
			// Token: 0x04002B97 RID: 11159
			Auto = 8,
			// Token: 0x04002B98 RID: 11160
			AutoForce = 16
		}
	}
}
