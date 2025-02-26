using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector2
{
	// Token: 0x020002A6 RID: 678
	[TaskCategory("Unity/Vector2")]
	[TaskDescription("Stores the Vector3 value of the Vector2.")]
	public class GetVector3 : Action
	{
		// Token: 0x06000BB5 RID: 2997 RVA: 0x0002DDF3 File Offset: 0x0002C1F3
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = this.vector3Variable.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0002DE11 File Offset: 0x0002C211
		public override void OnReset()
		{
			this.vector3Variable = Vector2.zero;
			this.storeResult = Vector3.zero;
		}

		// Token: 0x04000A86 RID: 2694
		[Tooltip("The Vector2 to get the Vector3 value of")]
		public SharedVector2 vector3Variable;

		// Token: 0x04000A87 RID: 2695
		[Tooltip("The Vector3 value")]
		[RequiredField]
		public SharedVector3 storeResult;
	}
}
