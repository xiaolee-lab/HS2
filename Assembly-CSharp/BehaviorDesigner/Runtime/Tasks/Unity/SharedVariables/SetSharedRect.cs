using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x0200025D RID: 605
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Sets the SharedRect variable to the specified object. Returns Success.")]
	public class SetSharedRect : Action
	{
		// Token: 0x06000AAE RID: 2734 RVA: 0x0002B9FE File Offset: 0x00029DFE
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x0002BA18 File Offset: 0x00029E18
		public override void OnReset()
		{
			this.targetValue = default(Rect);
			this.targetVariable = default(Rect);
		}

		// Token: 0x04000996 RID: 2454
		[Tooltip("The value to set the SharedRect to")]
		public SharedRect targetValue;

		// Token: 0x04000997 RID: 2455
		[RequiredField]
		[Tooltip("The SharedRect to set")]
		public SharedRect targetVariable;
	}
}
