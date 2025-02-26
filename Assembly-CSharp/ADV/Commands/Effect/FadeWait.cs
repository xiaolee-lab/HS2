using System;
using Illusion;
using Illusion.Extensions;

namespace ADV.Commands.Effect
{
	// Token: 0x02000736 RID: 1846
	public class FadeWait : CommandBase
	{
		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06002BC2 RID: 11202 RVA: 0x000FCF77 File Offset: 0x000FB377
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"Type"
				};
			}
		}

		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x06002BC3 RID: 11203 RVA: 0x000FCF88 File Offset: 0x000FB388
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					FadeWait.Type.Scene.ToString()
				};
			}
		}

		// Token: 0x06002BC4 RID: 11204 RVA: 0x000FCFAD File Offset: 0x000FB3AD
		public override void Do()
		{
			base.Do();
			this.type = Illusion.Utils.Enum<FadeWait.Type>.Cast(this.args[0].Check(true, Illusion.Utils.Enum<FadeWait.Type>.Names));
		}

		// Token: 0x06002BC5 RID: 11205 RVA: 0x000FCFD3 File Offset: 0x000FB3D3
		public override bool Process()
		{
			base.Process();
			return !base.scenario.Fading;
		}

		// Token: 0x06002BC6 RID: 11206 RVA: 0x000FCFEA File Offset: 0x000FB3EA
		public override void Result(bool processEnd)
		{
		}

		// Token: 0x04002B37 RID: 11063
		private FadeWait.Type type;

		// Token: 0x02000737 RID: 1847
		private enum Type
		{
			// Token: 0x04002B39 RID: 11065
			Scene,
			// Token: 0x04002B3A RID: 11066
			Filter
		}
	}
}
