using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityString
{
	// Token: 0x02000270 RID: 624
	[TaskCategory("Unity/String")]
	[TaskDescription("Randomly selects a string from the array of strings.")]
	public class GetRandomString : Action
	{
		// Token: 0x06000AEE RID: 2798 RVA: 0x0002C1B8 File Offset: 0x0002A5B8
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.source[UnityEngine.Random.Range(0, this.source.Length)].Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x0002C1E0 File Offset: 0x0002A5E0
		public override void OnReset()
		{
			this.source = null;
			this.storeResult = null;
		}

		// Token: 0x040009C7 RID: 2503
		[Tooltip("The array of strings")]
		public SharedString[] source;

		// Token: 0x040009C8 RID: 2504
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedString storeResult;
	}
}
