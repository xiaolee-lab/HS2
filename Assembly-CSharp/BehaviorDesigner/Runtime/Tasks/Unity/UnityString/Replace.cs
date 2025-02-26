using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityString
{
	// Token: 0x02000273 RID: 627
	[TaskCategory("Unity/String")]
	[TaskDescription("Replaces a string with the new string")]
	public class Replace : Action
	{
		// Token: 0x06000AF7 RID: 2807 RVA: 0x0002C305 File Offset: 0x0002A705
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.targetString.Value.Replace(this.oldString.Value, this.newString.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x0002C33C File Offset: 0x0002A73C
		public override void OnReset()
		{
			this.targetString = string.Empty;
			this.oldString = string.Empty;
			this.newString = string.Empty;
			this.storeResult = string.Empty;
		}

		// Token: 0x040009CE RID: 2510
		[Tooltip("The target string")]
		public SharedString targetString;

		// Token: 0x040009CF RID: 2511
		[Tooltip("The old replace")]
		public SharedString oldString;

		// Token: 0x040009D0 RID: 2512
		[Tooltip("The new string")]
		public SharedString newString;

		// Token: 0x040009D1 RID: 2513
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedString storeResult;
	}
}
