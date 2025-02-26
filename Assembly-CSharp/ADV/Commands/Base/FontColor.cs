using System;
using Illusion.Extensions;
using UnityEngine;

namespace ADV.Commands.Base
{
	// Token: 0x02000706 RID: 1798
	public class FontColor : CommandBase
	{
		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06002AF9 RID: 11001 RVA: 0x000F913A File Offset: 0x000F753A
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"Color",
					"Target"
				};
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06002AFA RID: 11002 RVA: 0x000F9152 File Offset: 0x000F7552
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					"white",
					string.Empty
				};
			}
		}

		// Token: 0x06002AFB RID: 11003 RVA: 0x000F916C File Offset: 0x000F756C
		public override void Do()
		{
			base.Do();
			int num = 0;
			string text = this.args[num++];
			string target = string.Empty;
			this.args.SafeProc(num++, delegate(string s)
			{
				target = s;
			});
			Color? colorCheck = text.GetColorCheck();
			if (colorCheck != null)
			{
				base.scenario.commandController.fontColor.Set(target, colorCheck.Value);
				return;
			}
			string text2 = text.ToLower();
			if (text2 != null)
			{
				int configIndex;
				if (!(text2 == "color0"))
				{
					if (!(text2 == "color1") && !(text2 == "color2"))
					{
						return;
					}
					configIndex = 1;
				}
				else
				{
					configIndex = 0;
				}
				base.scenario.commandController.fontColor.Set(target, configIndex);
				return;
			}
		}
	}
}
