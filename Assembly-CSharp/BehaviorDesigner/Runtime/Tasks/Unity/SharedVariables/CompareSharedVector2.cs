using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x02000251 RID: 593
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedVector2 : Conditional
	{
		// Token: 0x06000A8A RID: 2698 RVA: 0x0002B688 File Offset: 0x00029A88
		public override TaskStatus OnUpdate()
		{
			return (!this.variable.Value.Equals(this.compareTo.Value)) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x0002B6BF File Offset: 0x00029ABF
		public override void OnReset()
		{
			this.variable = Vector2.zero;
			this.compareTo = Vector2.zero;
		}

		// Token: 0x0400097D RID: 2429
		[Tooltip("The first variable to compare")]
		public SharedVector2 variable;

		// Token: 0x0400097E RID: 2430
		[Tooltip("The variable to compare to")]
		public SharedVector2 compareTo;
	}
}
