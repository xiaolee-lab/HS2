using System;
using UnityEngine;

namespace ADV.Commands.Object
{
	// Token: 0x02000762 RID: 1890
	public class Delete : CommandBase
	{
		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x06002C7D RID: 11389 RVA: 0x000FF49B File Offset: 0x000FD89B
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"Name"
				};
			}
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06002C7E RID: 11390 RVA: 0x000FF4AB File Offset: 0x000FD8AB
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					string.Empty
				};
			}
		}

		// Token: 0x06002C7F RID: 11391 RVA: 0x000FF4BC File Offset: 0x000FD8BC
		public override void Do()
		{
			base.Do();
			string key = this.args[0];
			GameObject obj = base.scenario.commandController.Objects[key];
			UnityEngine.Object.Destroy(obj);
			base.scenario.commandController.Objects.Remove(key);
		}
	}
}
