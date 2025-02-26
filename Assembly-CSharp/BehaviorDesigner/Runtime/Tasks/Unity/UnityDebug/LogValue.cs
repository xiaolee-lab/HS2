using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityDebug
{
	// Token: 0x0200016F RID: 367
	[TaskCategory("Unity/Debug")]
	[TaskDescription("Log a variable value.")]
	public class LogValue : Action
	{
		// Token: 0x06000778 RID: 1912 RVA: 0x00024426 File Offset: 0x00022826
		public override TaskStatus OnUpdate()
		{
			return TaskStatus.Success;
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x00024429 File Offset: 0x00022829
		public override void OnReset()
		{
			this.variable = null;
		}

		// Token: 0x0400067F RID: 1663
		[Tooltip("The variable to output")]
		public SharedGenericVariable variable;
	}
}
