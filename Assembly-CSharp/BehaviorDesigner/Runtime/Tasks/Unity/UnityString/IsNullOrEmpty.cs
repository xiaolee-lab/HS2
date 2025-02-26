using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityString
{
	// Token: 0x02000272 RID: 626
	[TaskCategory("Unity/String")]
	[TaskDescription("Returns success if the string is null or empty")]
	public class IsNullOrEmpty : Conditional
	{
		// Token: 0x06000AF4 RID: 2804 RVA: 0x0002C2CD File Offset: 0x0002A6CD
		public override TaskStatus OnUpdate()
		{
			return (!string.IsNullOrEmpty(this.targetString.Value)) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x0002C2EB File Offset: 0x0002A6EB
		public override void OnReset()
		{
			this.targetString = string.Empty;
		}

		// Token: 0x040009CD RID: 2509
		[Tooltip("The target string")]
		public SharedString targetString;
	}
}
