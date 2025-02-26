using System;
using Manager;
using UnityEngine;

namespace ADV.Commands.Sound.Base
{
	// Token: 0x02000770 RID: 1904
	public abstract class Stop : CommandBase
	{
		// Token: 0x06002CA1 RID: 11425 RVA: 0x0010037C File Offset: 0x000FE77C
		public Stop(Sound.Type type)
		{
			this.type = type;
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06002CA2 RID: 11426 RVA: 0x0010038B File Offset: 0x000FE78B
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

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06002CA3 RID: 11427 RVA: 0x0010039B File Offset: 0x000FE79B
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

		// Token: 0x06002CA4 RID: 11428 RVA: 0x001003AB File Offset: 0x000FE7AB
		public override void Do()
		{
			base.Do();
			float.TryParse(this.args.SafeGet(0), out this.stopTime);
		}

		// Token: 0x06002CA5 RID: 11429 RVA: 0x001003CB File Offset: 0x000FE7CB
		public override bool Process()
		{
			base.Process();
			if (this.timer >= this.stopTime)
			{
				return true;
			}
			this.timer += Time.deltaTime;
			return false;
		}

		// Token: 0x06002CA6 RID: 11430 RVA: 0x001003FA File Offset: 0x000FE7FA
		public override void Result(bool processEnd)
		{
			base.Result(processEnd);
			Singleton<Sound>.Instance.Stop(this.type);
		}

		// Token: 0x04002B72 RID: 11122
		private Sound.Type type;

		// Token: 0x04002B73 RID: 11123
		private float stopTime;

		// Token: 0x04002B74 RID: 11124
		private float timer;
	}
}
