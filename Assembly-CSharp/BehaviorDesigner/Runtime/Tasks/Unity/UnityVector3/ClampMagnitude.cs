using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector3
{
	// Token: 0x020002B1 RID: 689
	[TaskCategory("Unity/Vector3")]
	[TaskDescription("Clamps the magnitude of the Vector3.")]
	public class ClampMagnitude : Action
	{
		// Token: 0x06000BD3 RID: 3027 RVA: 0x0002E2D6 File Offset: 0x0002C6D6
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector3.ClampMagnitude(this.vector3Variable.Value, this.maxLength.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x0002E300 File Offset: 0x0002C700
		public override void OnReset()
		{
			this.vector3Variable = (this.storeResult = Vector3.zero);
			this.maxLength = 0f;
		}

		// Token: 0x04000AA8 RID: 2728
		[Tooltip("The Vector3 to clamp the magnitude of")]
		public SharedVector3 vector3Variable;

		// Token: 0x04000AA9 RID: 2729
		[Tooltip("The max length of the magnitude")]
		public SharedFloat maxLength;

		// Token: 0x04000AAA RID: 2730
		[Tooltip("The clamp magnitude resut")]
		[RequiredField]
		public SharedVector3 storeResult;
	}
}
