using System;
using Illusion;
using Illusion.Extensions;
using UnityEngine;

namespace ADV.Commands.Object
{
	// Token: 0x0200076A RID: 1898
	public class Component : CommandBase
	{
		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06002C91 RID: 11409 RVA: 0x000FF9D3 File Offset: 0x000FDDD3
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"Type",
					"ComponentType",
					"FindType",
					"ChildName",
					"RootName"
				};
			}
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06002C92 RID: 11410 RVA: 0x000FFA04 File Offset: 0x000FDE04
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					Component.Type.Add.ToString(),
					string.Empty,
					string.Empty,
					string.Empty,
					string.Empty
				};
			}
		}

		// Token: 0x06002C93 RID: 11411 RVA: 0x000FFA4C File Offset: 0x000FDE4C
		public override void Do()
		{
			base.Do();
			int num = 0;
			string self = this.args[num++];
			int num2 = self.Check(true, Enum.GetNames(typeof(Component.Type)));
			string typeName = this.args[num++];
			string findType = this.args[num++];
			string childName = this.args[num++];
			string otherRootName = this.args[num++];
			GameObject gameObject = ObjectEx.FindGet(findType, childName, otherRootName, base.scenario.commandController).gameObject;
			System.Type type = Illusion.Utils.Type.Get(typeName);
			Component.Type type2 = (Component.Type)num2;
			if (type2 != Component.Type.Add)
			{
				if (type2 == Component.Type.Sub)
				{
					UnityEngine.Object.Destroy(gameObject.GetComponent(type));
				}
			}
			else
			{
				gameObject.AddComponent(type);
			}
		}

		// Token: 0x0200076B RID: 1899
		private enum Type
		{
			// Token: 0x04002B62 RID: 11106
			Add,
			// Token: 0x04002B63 RID: 11107
			Sub
		}
	}
}
