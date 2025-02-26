using System;
using UnityEngine;

namespace ADV.Commands.Chara
{
	// Token: 0x0200071E RID: 1822
	public class LookNeckTarget : CommandBase
	{
		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06002B62 RID: 11106 RVA: 0x000FB44B File Offset: 0x000F984B
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

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06002B63 RID: 11107 RVA: 0x000FB48C File Offset: 0x000F988C
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

		// Token: 0x06002B64 RID: 11108 RVA: 0x000FB4E8 File Offset: 0x000F98E8
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
			float dis = 0.8f;
			this.args.SafeProc(num++, delegate(string s)
			{
				dis = float.Parse(s);
			});
			chara.chaCtrl.ChangeLookNeckTarget(targetType, trfTarg, rate, rotDeg, range, dis);
		}
	}
}
