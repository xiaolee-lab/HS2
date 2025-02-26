using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000C9B RID: 3227
	[TaskCategory("")]
	public class CommonThink : AgentStateAction
	{
		// Token: 0x06006921 RID: 26913 RVA: 0x002CB475 File Offset: 0x002C9875
		public override void OnStart()
		{
			base.OnStart();
		}

		// Token: 0x06006922 RID: 26914 RVA: 0x002CB47D File Offset: 0x002C987D
		public override TaskStatus OnUpdate()
		{
			return base.OnUpdate();
		}

		// Token: 0x06006923 RID: 26915 RVA: 0x002CB485 File Offset: 0x002C9885
		protected override void OnCompletedStateTask()
		{
			base.OnCompletedStateTask();
		}
	}
}
