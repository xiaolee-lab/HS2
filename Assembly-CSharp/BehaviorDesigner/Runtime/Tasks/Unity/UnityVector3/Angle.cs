using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector3
{
	// Token: 0x020002B0 RID: 688
	[TaskCategory("Unity/Vector3")]
	[TaskDescription("Returns the angle between two Vector3s.")]
	public class Angle : Action
	{
		// Token: 0x06000BD0 RID: 3024 RVA: 0x0002E26E File Offset: 0x0002C66E
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector3.Angle(this.firstVector3.Value, this.secondVector3.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x0002E298 File Offset: 0x0002C698
		public override void OnReset()
		{
			this.firstVector3 = (this.secondVector3 = Vector3.zero);
			this.storeResult = 0f;
		}

		// Token: 0x04000AA5 RID: 2725
		[Tooltip("The first Vector3")]
		public SharedVector3 firstVector3;

		// Token: 0x04000AA6 RID: 2726
		[Tooltip("The second Vector3")]
		public SharedVector3 secondVector3;

		// Token: 0x04000AA7 RID: 2727
		[Tooltip("The angle")]
		[RequiredField]
		public SharedFloat storeResult;
	}
}
