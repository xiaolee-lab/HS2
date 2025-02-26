using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x02000261 RID: 609
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Sets the SharedVector2 variable to the specified object. Returns Success.")]
	public class SetSharedVector2 : Action
	{
		// Token: 0x06000ABA RID: 2746 RVA: 0x0002BB1B File Offset: 0x00029F1B
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x0002BB34 File Offset: 0x00029F34
		public override void OnReset()
		{
			this.targetValue = Vector2.zero;
			this.targetVariable = Vector2.zero;
		}

		// Token: 0x0400099E RID: 2462
		[Tooltip("The value to set the SharedVector2 to")]
		public SharedVector2 targetValue;

		// Token: 0x0400099F RID: 2463
		[RequiredField]
		[Tooltip("The SharedVector2 to set")]
		public SharedVector2 targetVariable;
	}
}
