using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.Math
{
	// Token: 0x020001B1 RID: 433
	[TaskCategory("Unity/Math")]
	[TaskDescription("Is the int a positive value?")]
	public class IsIntPositive : Conditional
	{
		// Token: 0x06000841 RID: 2113 RVA: 0x00025FD6 File Offset: 0x000243D6
		public override TaskStatus OnUpdate()
		{
			return (this.intVariable.Value <= 0) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x00025FF0 File Offset: 0x000243F0
		public override void OnReset()
		{
			this.intVariable = 0;
		}

		// Token: 0x04000741 RID: 1857
		[Tooltip("The int to check if positive")]
		public SharedInt intVariable;
	}
}
