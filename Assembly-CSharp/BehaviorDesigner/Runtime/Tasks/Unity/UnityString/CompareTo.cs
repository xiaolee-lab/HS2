using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityString
{
	// Token: 0x0200026D RID: 621
	[TaskCategory("Unity/String")]
	[TaskDescription("Compares the first string to the second string. Returns an int which indicates whether the first string precedes, matches, or follows the second string.")]
	public class CompareTo : Action
	{
		// Token: 0x06000AE4 RID: 2788 RVA: 0x0002C04D File Offset: 0x0002A44D
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.firstString.Value.CompareTo(this.secondString.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x0002C076 File Offset: 0x0002A476
		public override void OnReset()
		{
			this.firstString = string.Empty;
			this.secondString = string.Empty;
			this.storeResult = 0;
		}

		// Token: 0x040009BE RID: 2494
		[Tooltip("The string to compare")]
		public SharedString firstString;

		// Token: 0x040009BF RID: 2495
		[Tooltip("The string to compare to")]
		public SharedString secondString;

		// Token: 0x040009C0 RID: 2496
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedInt storeResult;
	}
}
