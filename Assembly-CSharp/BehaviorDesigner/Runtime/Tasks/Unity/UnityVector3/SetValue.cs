using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector3
{
	// Token: 0x020002C2 RID: 706
	[TaskCategory("Unity/Vector3")]
	[TaskDescription("Sets the value of the Vector3.")]
	public class SetValue : Action
	{
		// Token: 0x06000C03 RID: 3075 RVA: 0x0002E988 File Offset: 0x0002CD88
		public override TaskStatus OnUpdate()
		{
			this.vector3Variable.Value = this.vector3Value.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x0002E9A4 File Offset: 0x0002CDA4
		public override void OnReset()
		{
			this.vector3Value = (this.vector3Variable = Vector3.zero);
		}

		// Token: 0x04000AD8 RID: 2776
		[Tooltip("The Vector3 to get the values of")]
		public SharedVector3 vector3Value;

		// Token: 0x04000AD9 RID: 2777
		[Tooltip("The Vector3 to set the values of")]
		public SharedVector3 vector3Variable;
	}
}
