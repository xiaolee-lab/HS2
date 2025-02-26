using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x02000112 RID: 274
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Converts the state name to its corresponding hash code. Returns Success.")]
	public class GetStringToHash : Action
	{
		// Token: 0x06000602 RID: 1538 RVA: 0x00020A24 File Offset: 0x0001EE24
		public override TaskStatus OnUpdate()
		{
			this.storeValue.Value = Animator.StringToHash(this.stateName.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00020A42 File Offset: 0x0001EE42
		public override void OnReset()
		{
			this.stateName = string.Empty;
			this.storeValue = 0;
		}

		// Token: 0x040004FE RID: 1278
		[Tooltip("The name of the state to convert to a hash code")]
		public SharedString stateName;

		// Token: 0x040004FF RID: 1279
		[Tooltip("The hash value")]
		[RequiredField]
		public SharedInt storeValue;
	}
}
