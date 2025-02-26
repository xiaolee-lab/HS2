using System;
using System.Linq;
using Illusion.Extensions;
using UnityEngine;

namespace ADV.Commands.Chara
{
	// Token: 0x02000727 RID: 1831
	public class StandFindPosition : CommandBase
	{
		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06002B88 RID: 11144 RVA: 0x000FBDC7 File Offset: 0x000FA1C7
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"No",
					"Type",
					"Name",
					"Child"
				};
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06002B89 RID: 11145 RVA: 0x000FBDF0 File Offset: 0x000FA1F0
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					int.MaxValue.ToString(),
					StandFindPosition.Type.World.ToString(),
					string.Empty,
					string.Empty
				};
			}
		}

		// Token: 0x06002B8A RID: 11146 RVA: 0x000FBE3C File Offset: 0x000FA23C
		public override void Do()
		{
			base.Do();
			int num = 0;
			int no = int.Parse(this.args[num++]);
			string self = this.args[num++];
			int num2 = self.Check(true, Enum.GetNames(typeof(StandFindPosition.Type)));
			string findName = this.args[num++];
			string childName = string.Empty;
			this.args.SafeProc(num++, delegate(string s)
			{
				childName = s;
			});
			Transform transform = base.scenario.commandController.GetChara(no).transform;
			Transform stand = null;
			switch (num2)
			{
			case 0:
				GameObject.Find(findName).SafeProc(delegate(GameObject p)
				{
					stand = p.transform;
				});
				break;
			case 1:
			{
				GameObject gameObject = GameObject.FindGameObjectWithTag(findName);
				stand = gameObject.transform;
				if (!childName.IsNullOrEmpty())
				{
					stand = gameObject.GetComponentsInChildren<Transform>().FirstOrDefault((Transform t) => t.name.Compare(childName, true));
				}
				break;
			}
			case 2:
				stand = base.scenario.commandController.NullDic[findName];
				if (!childName.IsNullOrEmpty())
				{
					stand = stand.GetComponentsInChildren<Transform>().FirstOrDefault((Transform t) => t.name.Compare(childName, true));
				}
				break;
			case 3:
				stand = base.scenario.commandController.EventCGRoot.Children().Find((Transform p) => p.name.Compare(findName, true));
				if (!childName.IsNullOrEmpty())
				{
					stand = stand.Children().Find((Transform t) => t.name.Compare(childName, true));
				}
				break;
			}
			transform.SetPositionAndRotation(stand.position, stand.rotation);
		}

		// Token: 0x02000728 RID: 1832
		public enum Type
		{
			// Token: 0x04002B24 RID: 11044
			World,
			// Token: 0x04002B25 RID: 11045
			Tag,
			// Token: 0x04002B26 RID: 11046
			Null,
			// Token: 0x04002B27 RID: 11047
			EventCG
		}
	}
}
