using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000CAF RID: 3247
	[TaskCategory("")]
	public class Call : AgentAction
	{
		// Token: 0x06006978 RID: 27000 RVA: 0x002CE9DB File Offset: 0x002CCDDB
		public override TaskStatus OnUpdate()
		{
			return TaskStatus.Running;
		}
	}
}
