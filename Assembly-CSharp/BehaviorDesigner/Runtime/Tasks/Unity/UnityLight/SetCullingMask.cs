using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityLight
{
	// Token: 0x02000198 RID: 408
	[TaskCategory("Unity/Light")]
	[TaskDescription("Sets the culling mask of the light.")]
	public class SetCullingMask : Action
	{
		// Token: 0x060007FD RID: 2045 RVA: 0x000252D8 File Offset: 0x000236D8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x0002531B File Offset: 0x0002371B
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				return TaskStatus.Failure;
			}
			this.light.cullingMask = this.cullingMask.value;
			return TaskStatus.Success;
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x00025347 File Offset: 0x00023747
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.cullingMask = -1;
		}

		// Token: 0x040006E0 RID: 1760
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040006E1 RID: 1761
		[Tooltip("The culling mask to set")]
		public LayerMask cullingMask;

		// Token: 0x040006E2 RID: 1762
		private Light light;

		// Token: 0x040006E3 RID: 1763
		private GameObject prevGameObject;
	}
}
