using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityString
{
	// Token: 0x02000274 RID: 628
	[TaskCategory("Unity/String")]
	[TaskDescription("Sets the variable string to the value string.")]
	public class SetString : Action
	{
		// Token: 0x06000AFA RID: 2810 RVA: 0x0002C391 File Offset: 0x0002A791
		public override TaskStatus OnUpdate()
		{
			this.variable.Value = this.value.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x0002C3AA File Offset: 0x0002A7AA
		public override void OnReset()
		{
			this.variable = string.Empty;
			this.value = string.Empty;
		}

		// Token: 0x040009D2 RID: 2514
		[Tooltip("The target string")]
		[RequiredField]
		public SharedString variable;

		// Token: 0x040009D3 RID: 2515
		[Tooltip("The value string")]
		public SharedString value;
	}
}
