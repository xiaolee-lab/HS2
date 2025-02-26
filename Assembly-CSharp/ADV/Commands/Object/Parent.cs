using System;
using UnityEngine;

namespace ADV.Commands.Object
{
	// Token: 0x02000769 RID: 1897
	public class Parent : CommandBase
	{
		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x06002C8D RID: 11405 RVA: 0x000FF8F8 File Offset: 0x000FDCF8
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"Name",
					"FindType",
					"ChildName",
					"RootName"
				};
			}
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x06002C8E RID: 11406 RVA: 0x000FF920 File Offset: 0x000FDD20
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					string.Empty,
					string.Empty,
					string.Empty,
					string.Empty
				};
			}
		}

		// Token: 0x06002C8F RID: 11407 RVA: 0x000FF948 File Offset: 0x000FDD48
		public override void Do()
		{
			base.Do();
			int num = 0;
			string key = this.args[num++];
			string findType = this.args[num++];
			string childName = this.args[num++];
			string otherRootName = this.args[num++];
			Transform parent = ObjectEx.FindGet(findType, childName, otherRootName, base.scenario.commandController);
			base.scenario.commandController.Objects[key].transform.SetParent(parent, false);
		}
	}
}
