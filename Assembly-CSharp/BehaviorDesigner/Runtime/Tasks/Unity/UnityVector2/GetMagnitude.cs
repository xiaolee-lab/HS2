using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector2
{
	// Token: 0x020002A2 RID: 674
	[TaskCategory("Unity/Vector2")]
	[TaskDescription("Stores the magnitude of the Vector2.")]
	public class GetMagnitude : Action
	{
		// Token: 0x06000BA9 RID: 2985 RVA: 0x0002DCEC File Offset: 0x0002C0EC
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.vector2Variable.Value.magnitude;
			return TaskStatus.Success;
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x0002DD18 File Offset: 0x0002C118
		public override void OnReset()
		{
			this.vector2Variable = Vector2.zero;
			this.storeResult = 0f;
		}

		// Token: 0x04000A80 RID: 2688
		[Tooltip("The Vector2 to get the magnitude of")]
		public SharedVector2 vector2Variable;

		// Token: 0x04000A81 RID: 2689
		[Tooltip("The magnitude of the vector")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}
