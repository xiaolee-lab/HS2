using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject
{
	// Token: 0x02000172 RID: 370
	[TaskCategory("Unity/GameObject")]
	[TaskDescription("Returns Success if the layermasks match, otherwise Failure.")]
	public class CompareLayerMask : Conditional
	{
		// Token: 0x06000781 RID: 1921 RVA: 0x000244A4 File Offset: 0x000228A4
		public override TaskStatus OnUpdate()
		{
			return ((1 << base.GetDefaultGameObject(this.targetGameObject.Value).layer & this.layermask.value) == 0) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x000244D9 File Offset: 0x000228D9
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04000682 RID: 1666
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000683 RID: 1667
		[Tooltip("The layermask to compare against")]
		public LayerMask layermask;
	}
}
