using System;
using Illusion.Extensions;
using UnityEngine;

namespace ADV.Commands.Effect
{
	// Token: 0x02000733 RID: 1843
	public class Fade : CommandBase
	{
		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06002BB1 RID: 11185 RVA: 0x000FCBC2 File Offset: 0x000FAFC2
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"Fade",
					"Time",
					"Color",
					"Type",
					"isInit"
				};
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06002BB2 RID: 11186 RVA: 0x000FCBF2 File Offset: 0x000FAFF2
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					"in",
					"0",
					"clear",
					"front",
					bool.TrueString
				};
			}
		}

		// Token: 0x06002BB3 RID: 11187 RVA: 0x000FCC24 File Offset: 0x000FB024
		public override void Do()
		{
			base.Do();
			this.timer = 0f;
			int num = 0;
			this.fadeIn = this.args[num++].Compare("in", true);
			this.time = float.Parse(this.args[num++]);
			this.color = this.args[num++].GetColor();
			this.isFront = this.args[num++].Compare("front", true);
			if (this.fadeIn)
			{
				base.scenario.FadeIn(this.time, true);
			}
			else
			{
				base.scenario.FadeOut(this.time, true);
			}
		}

		// Token: 0x06002BB4 RID: 11188 RVA: 0x000FCCE0 File Offset: 0x000FB0E0
		public override bool Process()
		{
			base.Process();
			return !base.scenario.Fading;
		}

		// Token: 0x06002BB5 RID: 11189 RVA: 0x000FCCF8 File Offset: 0x000FB0F8
		public override void Result(bool processEnd)
		{
			base.Result(processEnd);
			if (!processEnd)
			{
				if (this.fadeIn)
				{
					base.scenario.FadeIn(0f, true);
				}
				else
				{
					base.scenario.FadeOut(0f, true);
				}
			}
		}

		// Token: 0x04002B2D RID: 11053
		private Color color = Color.clear;

		// Token: 0x04002B2E RID: 11054
		private float time;

		// Token: 0x04002B2F RID: 11055
		private float timer;

		// Token: 0x04002B30 RID: 11056
		private bool fadeIn = true;

		// Token: 0x04002B31 RID: 11057
		private bool isFront = true;

		// Token: 0x04002B32 RID: 11058
		private bool isInitColorSet;

		// Token: 0x04002B33 RID: 11059
		private ADVFade advFade;
	}
}
