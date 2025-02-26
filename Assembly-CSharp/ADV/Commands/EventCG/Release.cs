using System;
using System.Collections.Generic;

namespace ADV.Commands.EventCG
{
	// Token: 0x02000711 RID: 1809
	public class Release : CommandBase
	{
		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06002B2F RID: 11055 RVA: 0x000FA5E1 File Offset: 0x000F89E1
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"isMotionContinue"
				};
			}
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06002B30 RID: 11056 RVA: 0x000FA5F1 File Offset: 0x000F89F1
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					bool.FalseString
				};
			}
		}

		// Token: 0x06002B31 RID: 11057 RVA: 0x000FA604 File Offset: 0x000F8A04
		public override void Do()
		{
			base.Do();
			if (!Common.Release(base.scenario))
			{
				return;
			}
			foreach (KeyValuePair<int, CharaData> keyValuePair in base.scenario.commandController.Characters)
			{
				keyValuePair.Value.backup.Repair();
			}
			base.scenario.commandController.useCorrectCamera = true;
		}
	}
}
