using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector3
{
	// Token: 0x020002B9 RID: 697
	[TaskCategory("Unity/Vector3")]
	[TaskDescription("Stores the Vector2 value of the Vector3.")]
	public class GetVector2 : Action
	{
		// Token: 0x06000BEB RID: 3051 RVA: 0x0002E543 File Offset: 0x0002C943
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.vector3Variable.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x0002E561 File Offset: 0x0002C961
		public override void OnReset()
		{
			this.vector3Variable = Vector3.zero;
			this.storeResult = Vector2.zero;
		}

		// Token: 0x04000AB8 RID: 2744
		[Tooltip("The Vector3 to get the Vector2 value of")]
		public SharedVector3 vector3Variable;

		// Token: 0x04000AB9 RID: 2745
		[Tooltip("The Vector2 value")]
		[RequiredField]
		public SharedVector2 storeResult;
	}
}
