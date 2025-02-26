using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x0200024D RID: 589
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Returns success if the variable value is equal to the compareTo value.")]
	public class CompareSharedRect : Conditional
	{
		// Token: 0x06000A7E RID: 2686 RVA: 0x0002B438 File Offset: 0x00029838
		public override TaskStatus OnUpdate()
		{
			return (!this.variable.Value.Equals(this.compareTo.Value)) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x0002B470 File Offset: 0x00029870
		public override void OnReset()
		{
			this.variable = default(Rect);
			this.compareTo = default(Rect);
		}

		// Token: 0x04000975 RID: 2421
		[Tooltip("The first variable to compare")]
		public SharedRect variable;

		// Token: 0x04000976 RID: 2422
		[Tooltip("The variable to compare to")]
		public SharedRect compareTo;
	}
}
