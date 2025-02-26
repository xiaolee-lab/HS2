using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector2
{
	// Token: 0x020002AA RID: 682
	[TaskCategory("Unity/Vector2")]
	[TaskDescription("Multiply the Vector2 by a float.")]
	public class Multiply : Action
	{
		// Token: 0x06000BC1 RID: 3009 RVA: 0x0002DFC7 File Offset: 0x0002C3C7
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.vector2Variable.Value * this.multiplyBy.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x0002DFF0 File Offset: 0x0002C3F0
		public override void OnReset()
		{
			this.vector2Variable = (this.storeResult = Vector2.zero);
			this.multiplyBy = 0f;
		}

		// Token: 0x04000A93 RID: 2707
		[Tooltip("The Vector2 to multiply of")]
		public SharedVector2 vector2Variable;

		// Token: 0x04000A94 RID: 2708
		[Tooltip("The value to multiply the Vector2 of")]
		public SharedFloat multiplyBy;

		// Token: 0x04000A95 RID: 2709
		[Tooltip("The multiplication resut")]
		[RequiredField]
		public SharedVector2 storeResult;
	}
}
