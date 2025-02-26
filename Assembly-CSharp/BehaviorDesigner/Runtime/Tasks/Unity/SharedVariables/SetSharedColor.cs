using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x02000255 RID: 597
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Sets the SharedColor variable to the specified object. Returns Success.")]
	public class SetSharedColor : Action
	{
		// Token: 0x06000A96 RID: 2710 RVA: 0x0002B7EC File Offset: 0x00029BEC
		public override TaskStatus OnUpdate()
		{
			this.targetVariable.Value = this.targetValue.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x0002B805 File Offset: 0x00029C05
		public override void OnReset()
		{
			this.targetValue = Color.black;
			this.targetVariable = Color.black;
		}

		// Token: 0x04000985 RID: 2437
		[Tooltip("The value to set the SharedColor to")]
		public SharedColor targetValue;

		// Token: 0x04000986 RID: 2438
		[RequiredField]
		[Tooltip("The SharedColor to set")]
		public SharedColor targetVariable;
	}
}
