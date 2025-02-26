using System;
using System.Runtime.CompilerServices;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000D97 RID: 3479
	public abstract class AgentConditional : Conditional
	{
		// Token: 0x17001525 RID: 5413
		// (get) Token: 0x06006C98 RID: 27800 RVA: 0x002E2B2C File Offset: 0x002E0F2C
		public AgentActor Agent
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

		// Token: 0x04005AEE RID: 23278
		private AgentActor _agent;
	}
}
