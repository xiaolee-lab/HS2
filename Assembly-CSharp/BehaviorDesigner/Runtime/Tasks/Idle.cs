using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000CF RID: 207
	[TaskDescription("Returns a TaskStatus of running. Will only stop when interrupted or a conditional abort is triggered.")]
	[TaskIcon("{SkinColor}IdleIcon.png")]
	public class Idle : Action
	{
		// Token: 0x060004AC RID: 1196 RVA: 0x0001D38F File Offset: 0x0001B78F
		public override TaskStatus OnUpdate()
		{
			return TaskStatus.Running;
		}
	}
}
