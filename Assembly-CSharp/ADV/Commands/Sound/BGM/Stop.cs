using System;
using Manager;
using UnityEngine;

namespace ADV.Commands.Sound.BGM
{
	// Token: 0x02000772 RID: 1906
	public class Stop : CommandBase
	{
		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06002CAC RID: 11436 RVA: 0x001006A1 File Offset: 0x000FEAA1
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"Time",
					"Fade"
				};
			}
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06002CAD RID: 11437 RVA: 0x001006B9 File Offset: 0x000FEAB9
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					"0",
					"0.8"
				};
			}
		}

		// Token: 0x06002CAE RID: 11438 RVA: 0x001006D4 File Offset: 0x000FEAD4
		public override void Do()
		{
			base.Do();
			int num = 0;
			this.stopTime = float.Parse(this.args[num++]);
			this.fadeTime = float.Parse(this.args[num++]);
		}

		// Token: 0x06002CAF RID: 11439 RVA: 0x00100717 File Offset: 0x000FEB17
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

		// Token: 0x06002CB0 RID: 11440 RVA: 0x00100746 File Offset: 0x000FEB46
		public override void Result(bool processEnd)
		{
			base.Result(processEnd);
			Singleton<Sound>.Instance.StopBGM(this.fadeTime);
		}

		// Token: 0x04002B75 RID: 11125
		private float stopTime;

		// Token: 0x04002B76 RID: 11126
		private float fadeTime;

		// Token: 0x04002B77 RID: 11127
		private float timer;
	}
}
