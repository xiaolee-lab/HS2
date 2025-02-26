using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector3
{
	// Token: 0x020002C1 RID: 705
	[TaskCategory("Unity/Vector3")]
	[TaskDescription("Rotate the current rotation to the target rotation.")]
	public class RotateTowards : Action
	{
		// Token: 0x06000C00 RID: 3072 RVA: 0x0002E8E0 File Offset: 0x0002CCE0
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector3.RotateTowards(this.currentRotation.Value, this.targetRotation.Value, this.maxDegreesDelta.Value * 0.017453292f * Time.deltaTime, this.maxMagnitudeDelta.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x0002E938 File Offset: 0x0002CD38
		public override void OnReset()
		{
			this.currentRotation = (this.targetRotation = (this.storeResult = Vector3.zero));
			this.maxDegreesDelta = (this.maxMagnitudeDelta = 0f);
		}

		// Token: 0x04000AD3 RID: 2771
		[Tooltip("The current rotation in euler angles")]
		public SharedVector3 currentRotation;

		// Token: 0x04000AD4 RID: 2772
		[Tooltip("The target rotation in euler angles")]
		public SharedVector3 targetRotation;

		// Token: 0x04000AD5 RID: 2773
		[Tooltip("The maximum delta of the degrees")]
		public SharedFloat maxDegreesDelta;

		// Token: 0x04000AD6 RID: 2774
		[Tooltip("The maximum delta of the magnitude")]
		public SharedFloat maxMagnitudeDelta;

		// Token: 0x04000AD7 RID: 2775
		[Tooltip("The rotation resut")]
		[RequiredField]
		public SharedVector3 storeResult;
	}
}
