using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector2
{
	// Token: 0x020002A3 RID: 675
	[TaskCategory("Unity/Vector2")]
	[TaskDescription("Stores the right vector value.")]
	public class GetRightVector : Action
	{
		// Token: 0x06000BAC RID: 2988 RVA: 0x0002DD42 File Offset: 0x0002C142
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector2.right;
			return TaskStatus.Success;
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x0002DD55 File Offset: 0x0002C155
		public override void OnReset()
		{
			this.storeResult = Vector2.zero;
		}

		// Token: 0x04000A82 RID: 2690
		[Tooltip("The stored result")]
		[RequiredField]
		public SharedVector2 storeResult;
	}
}
