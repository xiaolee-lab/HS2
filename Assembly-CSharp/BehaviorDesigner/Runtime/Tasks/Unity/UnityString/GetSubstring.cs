using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityString
{
	// Token: 0x02000271 RID: 625
	[TaskCategory("Unity/String")]
	[TaskDescription("Stores a substring of the target string")]
	public class GetSubstring : Action
	{
		// Token: 0x06000AF1 RID: 2801 RVA: 0x0002C210 File Offset: 0x0002A610
		public override TaskStatus OnUpdate()
		{
			if (this.length.Value != -1)
			{
				this.storeResult.Value = this.targetString.Value.Substring(this.startIndex.Value, this.length.Value);
			}
			else
			{
				this.storeResult.Value = this.targetString.Value.Substring(this.startIndex.Value);
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x0002C28B File Offset: 0x0002A68B
		public override void OnReset()
		{
			this.targetString = string.Empty;
			this.startIndex = 0;
			this.length = -1;
			this.storeResult = string.Empty;
		}

		// Token: 0x040009C9 RID: 2505
		[Tooltip("The target string")]
		public SharedString targetString;

		// Token: 0x040009CA RID: 2506
		[Tooltip("The start substring index")]
		public SharedInt startIndex = 0;

		// Token: 0x040009CB RID: 2507
		[Tooltip("The length of the substring. Don't use if -1")]
		public SharedInt length = -1;

		// Token: 0x040009CC RID: 2508
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedString storeResult;
	}
}
