using System;
using Illusion;
using UnityEngine;

namespace ADV.Commands.Object
{
	// Token: 0x02000760 RID: 1888
	public class Create : CommandBase
	{
		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06002C75 RID: 11381 RVA: 0x000FF2EA File Offset: 0x000FD6EA
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"Name",
					"Component"
				};
			}
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06002C76 RID: 11382 RVA: 0x000FF302 File Offset: 0x000FD702
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					string.Empty,
					string.Empty
				};
			}
		}

		// Token: 0x06002C77 RID: 11383 RVA: 0x000FF31C File Offset: 0x000FD71C
		public override void Do()
		{
			base.Do();
			int cnt = 0;
			GameObject gameObject = new GameObject(this.args[cnt++]);
			foreach (string typeName in CommandBase.RemoveArgsEmpty(base.GetArgToSplitLast(cnt)))
			{
				gameObject.AddComponent(Illusion.Utils.Type.Get(typeName));
			}
			base.scenario.commandController.SetObject(gameObject);
		}
	}
}
