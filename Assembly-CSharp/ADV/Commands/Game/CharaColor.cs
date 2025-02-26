using System;
using AIChara;
using Illusion.Extensions;
using UnityEngine;

namespace ADV.Commands.Game
{
	// Token: 0x02000742 RID: 1858
	public class CharaColor : CommandBase
	{
		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x06002BEE RID: 11246 RVA: 0x000FD869 File Offset: 0x000FBC69
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"No",
					"isActive",
					"Color"
				};
			}
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06002BEF RID: 11247 RVA: 0x000FD88C File Offset: 0x000FBC8C
		public override string[] ArgsDefault
		{
			get
			{
				string[] array = new string[3];
				array[0] = int.MaxValue.ToString();
				array[1] = bool.FalseString;
				return array;
			}
		}

		// Token: 0x06002BF0 RID: 11248 RVA: 0x000FD8C0 File Offset: 0x000FBCC0
		public override void Do()
		{
			base.Do();
			int num = 0;
			int no = int.Parse(this.args[num++]);
			CharaData chara = base.scenario.commandController.GetChara(no);
			ChaControl chaCtrl = chara.chaCtrl;
			ChaFileStatus status = chaCtrl.fileStatus;
			status.visibleSimple = bool.Parse(this.args[num++]);
			this.args.SafeProc(num++, delegate(string colorStr)
			{
				Color? colorCheck = colorStr.GetColorCheck();
				status.simpleColor = colorCheck.Value;
			});
		}
	}
}
