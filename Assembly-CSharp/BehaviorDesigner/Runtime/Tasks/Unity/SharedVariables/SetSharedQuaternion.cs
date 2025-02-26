using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x0200025C RID: 604
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Sets the SharedQuaternion variable to the specified object. Returns Success.")]
	public class SetSharedQuaternion : Action
	{
		// Token: 0x06000AAB RID: 2731 RVA: 0x0002B9BB File Offset: 0x00029DBB
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x0002B9D4 File Offset: 0x00029DD4
		public override void OnReset()
		{
			this.targetValue = Quaternion.identity;
			this.targetVariable = Quaternion.identity;
		}

		// Token: 0x04000994 RID: 2452
		[Tooltip("The value to set the SharedQuaternion to")]
		public SharedQuaternion targetValue;

		// Token: 0x04000995 RID: 2453
		[RequiredField]
		[Tooltip("The SharedQuaternion to set")]
		public SharedQuaternion targetVariable;
	}
}
