using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector2
{
	// Token: 0x020002A4 RID: 676
	[TaskCategory("Unity/Vector2")]
	[TaskDescription("Stores the square magnitude of the Vector2.")]
	public class GetSqrMagnitude : Action
	{
		// Token: 0x06000BAF RID: 2991 RVA: 0x0002DD70 File Offset: 0x0002C170
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.vector2Variable.Value.sqrMagnitude;
			return TaskStatus.Success;
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0002DD9C File Offset: 0x0002C19C
		public override void OnReset()
		{
			this.vector2Variable = Vector2.zero;
			this.storeResult = 0f;
		}

		// Token: 0x04000A83 RID: 2691
		[Tooltip("The Vector2 to get the square magnitude of")]
		public SharedVector2 vector2Variable;

		// Token: 0x04000A84 RID: 2692
		[Tooltip("The square magnitude of the vector")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}
