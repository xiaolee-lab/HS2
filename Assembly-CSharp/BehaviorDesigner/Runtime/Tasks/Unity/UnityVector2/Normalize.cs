using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector2
{
	// Token: 0x020002AB RID: 683
	[TaskCategory("Unity/Vector2")]
	[TaskDescription("Normalize the Vector2.")]
	public class Normalize : Action
	{
		// Token: 0x06000BC4 RID: 3012 RVA: 0x0002E030 File Offset: 0x0002C430
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.vector2Variable.Value.normalized;
			return TaskStatus.Success;
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x0002E05C File Offset: 0x0002C45C
		public override void OnReset()
		{
			this.vector2Variable = (this.storeResult = Vector2.zero);
		}

		// Token: 0x04000A96 RID: 2710
		[Tooltip("The Vector2 to normalize")]
		public SharedVector2 vector2Variable;

		// Token: 0x04000A97 RID: 2711
		[Tooltip("The normalized resut")]
		[RequiredField]
		public SharedVector2 storeResult;
	}
}
