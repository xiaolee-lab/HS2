using System;
using System.Runtime.CompilerServices;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000D94 RID: 3476
	public abstract class AgentDecorator : Decorator
	{
		// Token: 0x17001523 RID: 5411
		// (get) Token: 0x06006C92 RID: 27794 RVA: 0x002E6958 File Offset: 0x002E4D58
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

		// Token: 0x04005AEB RID: 23275
		private AgentActor _agent;
	}
}
