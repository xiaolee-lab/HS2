using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x02000263 RID: 611
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Sets the SharedVector4 variable to the specified object. Returns Success.")]
	public class SetSharedVector4 : Action
	{
		// Token: 0x06000AC0 RID: 2752 RVA: 0x0002BBA1 File Offset: 0x00029FA1
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x0002BBBA File Offset: 0x00029FBA
		public override void OnReset()
		{
			this.targetValue = Vector4.zero;
			this.targetVariable = Vector4.zero;
		}

		// Token: 0x040009A2 RID: 2466
		[Tooltip("The value to set the SharedVector4 to")]
		public SharedVector4 targetValue;

		// Token: 0x040009A3 RID: 2467
		[RequiredField]
		[Tooltip("The SharedVector4 to set")]
		public SharedVector4 targetVariable;
	}
}
