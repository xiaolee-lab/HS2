using System;
using System.Runtime.CompilerServices;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000D95 RID: 3477
	public abstract class AgentAction : BehaviorDesigner.Runtime.Tasks.Action
	{
		// Token: 0x17001524 RID: 5412
		// (get) Token: 0x06006C94 RID: 27796 RVA: 0x002C9FE0 File Offset: 0x002C83E0
		protected AgentActor Agent
		{
			[CompilerGenerated]
			get
			{
				AgentActor result;
				if ((result = this._agent) == null)
				{
					result = (this._agent = (base.Owner as AgentBehaviorTree).SourceAgent);
				}
				return result;
			}
		}

		// Token: 0x04005AEC RID: 23276
		private AgentActor _agent;
	}
}
