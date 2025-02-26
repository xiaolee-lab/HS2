using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityString
{
	// Token: 0x0200026C RID: 620
	[TaskCategory("Unity/String")]
	[TaskDescription("Creates a string from multiple other strings.")]
	public class BuildString : Action
	{
		// Token: 0x06000AE1 RID: 2785 RVA: 0x0002BFF0 File Offset: 0x0002A3F0
		public override TaskStatus OnUpdate()
		{
			for (int i = 0; i < this.source.Length; i++)
			{
				SharedString sharedString = this.storeResult;
				sharedString.Value += this.source[i];
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x0002C035 File Offset: 0x0002A435
		public override void OnReset()
		{
			this.source = null;
			this.storeResult = null;
		}

		// Token: 0x040009BC RID: 2492
		[Tooltip("The array of strings")]
		public SharedString[] source;

		// Token: 0x040009BD RID: 2493
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedString storeResult;
	}
}
