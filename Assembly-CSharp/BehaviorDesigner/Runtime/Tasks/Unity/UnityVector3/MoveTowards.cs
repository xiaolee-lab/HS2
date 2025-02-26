using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector3
{
	// Token: 0x020002BC RID: 700
	[TaskCategory("Unity/Vector3")]
	[TaskDescription("Move from the current position to the target position.")]
	public class MoveTowards : Action
	{
		// Token: 0x06000BF4 RID: 3060 RVA: 0x0002E6B7 File Offset: 0x0002CAB7
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector3.MoveTowards(this.currentPosition.Value, this.targetPosition.Value, this.speed.Value * Time.deltaTime);
			return TaskStatus.Success;
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x0002E6F4 File Offset: 0x0002CAF4
		public override void OnReset()
		{
			this.currentPosition = (this.targetPosition = (this.storeResult = Vector3.zero));
			this.speed = 0f;
		}

		// Token: 0x04000AC2 RID: 2754
		[Tooltip("The current position")]
		public SharedVector3 currentPosition;

		// Token: 0x04000AC3 RID: 2755
		[Tooltip("The target position")]
		public SharedVector3 targetPosition;

		// Token: 0x04000AC4 RID: 2756
		[Tooltip("The movement speed")]
		public SharedFloat speed;

		// Token: 0x04000AC5 RID: 2757
		[Tooltip("The move resut")]
		[RequiredField]
		public SharedVector3 storeResult;
	}
}
