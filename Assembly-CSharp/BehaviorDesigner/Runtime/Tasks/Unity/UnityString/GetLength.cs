using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityString
{
	// Token: 0x0200026F RID: 623
	[TaskCategory("Unity/String")]
	[TaskDescription("Stores the length of the string")]
	public class GetLength : Action
	{
		// Token: 0x06000AEB RID: 2795 RVA: 0x0002C174 File Offset: 0x0002A574
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.targetString.Value.Length;
			return TaskStatus.Success;
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x0002C192 File Offset: 0x0002A592
		public override void OnReset()
		{
			this.targetString = string.Empty;
			this.storeResult = 0;
		}

		// Token: 0x040009C5 RID: 2501
		[Tooltip("The target string")]
		public SharedString targetString;

		// Token: 0x040009C6 RID: 2502
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedInt storeResult;
	}
}
