using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x02000262 RID: 610
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Sets the SharedVector3 variable to the specified object. Returns Success.")]
	public class SetSharedVector3 : Action
	{
		// Token: 0x06000ABD RID: 2749 RVA: 0x0002BB5E File Offset: 0x00029F5E
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x0002BB77 File Offset: 0x00029F77
		public override void OnReset()
		{
			this.targetValue = Vector3.zero;
			this.targetVariable = Vector3.zero;
		}

		// Token: 0x040009A0 RID: 2464
		[Tooltip("The value to set the SharedVector3 to")]
		public SharedVector3 targetValue;

		// Token: 0x040009A1 RID: 2465
		[RequiredField]
		[Tooltip("The SharedVector3 to set")]
		public SharedVector3 targetVariable;
	}
}
