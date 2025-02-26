using System;
using UnityEngine;

namespace ADV.Commands.Chara
{
	// Token: 0x0200071B RID: 1819
	public class LookEyesTarget : CommandBase
	{
		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06002B56 RID: 11094 RVA: 0x000FAFC7 File Offset: 0x000F93C7
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"No",
					"Type",
					"Target",
					"Rate",
					"Deg",
					"Range",
					"Dis"
				};
			}
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06002B57 RID: 11095 RVA: 0x000FB008 File Offset: 0x000F9408
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					int.MaxValue.ToString(),
					"0",
					string.Empty,
					string.Empty,
					string.Empty,
					string.Empty,
					string.Empty
				};
			}
		}

		// Token: 0x06002B58 RID: 11096 RVA: 0x000FB064 File Offset: 0x000F9464
		public override void Do()
		{
			base.Do();
			int num = 0;
			int no = int.Parse(this.args[num++]);
			int targetType = int.Parse(this.args[num++]);
			CharaData chara = base.scenario.commandController.GetChara(no);
			GameObject obj = null;
			this.args.SafeProc(num++, delegate(string s)
			{
				obj = GameObject.Find(s);
			});
			Transform trfTarg = (!(obj == null)) ? obj.transform : null;
			float rate = 0.5f;
			this.args.SafeProc(num++, delegate(string s)
			{
				rate = float.Parse(s);
			});
			float rotDeg = 0f;
			this.args.SafeProc(num++, delegate(string s)
			{
				rotDeg = float.Parse(s);
			});
			float range = 1f;
			this.args.SafeProc(num++, delegate(string s)
			{
				range = float.Parse(s);
			});
			float dis = 2f;
			this.args.SafeProc(num++, delegate(string s)
			{
				dis = float.Parse(s);
			});
			chara.chaCtrl.ChangeLookEyesTarget(targetType, trfTarg, rate, rotDeg, range, dis);
		}
	}
}
