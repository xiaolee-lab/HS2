using System;
using UnityEngine;

namespace ADV.Commands.Base
{
	// Token: 0x02000709 RID: 1801
	public class Wait : CommandBase
	{
		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06002B0F RID: 11023 RVA: 0x000F9C9F File Offset: 0x000F809F
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"Time"
				};
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06002B10 RID: 11024 RVA: 0x000F9CAF File Offset: 0x000F80AF
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					"0"
				};
			}
		}

		// Token: 0x06002B11 RID: 11025 RVA: 0x000F9CBF File Offset: 0x000F80BF
		public override void Do()
		{
			base.Do();
			this.timer = 0f;
			float.TryParse(this.args[0], out this.time);
		}

		// Token: 0x06002B12 RID: 11026 RVA: 0x000F9CE6 File Offset: 0x000F80E6
		public override bool Process()
		{
			base.Process();
			this.timer += Time.deltaTime;
			return this.timer >= this.time;
		}

		// Token: 0x04002B17 RID: 11031
		private float time;

		// Token: 0x04002B18 RID: 11032
		private float timer;
	}
}
