using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector2
{
	// Token: 0x020002A9 RID: 681
	[TaskCategory("Unity/Vector2")]
	[TaskDescription("Move from the current position to the target position.")]
	public class MoveTowards : Action
	{
		// Token: 0x06000BBE RID: 3006 RVA: 0x0002DF43 File Offset: 0x0002C343
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector2.MoveTowards(this.currentPosition.Value, this.targetPosition.Value, this.speed.Value * Time.deltaTime);
			return TaskStatus.Success;
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x0002DF80 File Offset: 0x0002C380
		public override void OnReset()
		{
			this.currentPosition = (this.targetPosition = (this.storeResult = Vector2.zero));
			this.speed = 0f;
		}

		// Token: 0x04000A8F RID: 2703
		[Tooltip("The current position")]
		public SharedVector2 currentPosition;

		// Token: 0x04000A90 RID: 2704
		[Tooltip("The target position")]
		public SharedVector2 targetPosition;

		// Token: 0x04000A91 RID: 2705
		[Tooltip("The movement speed")]
		public SharedFloat speed;

		// Token: 0x04000A92 RID: 2706
		[Tooltip("The move resut")]
		[RequiredField]
		public SharedVector2 storeResult;
	}
}
