using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRenderer
{
	// Token: 0x02000205 RID: 517
	[TaskCategory("Unity/Renderer")]
	[TaskDescription("Returns Success if the Renderer is visible, otherwise Failure.")]
	public class IsVisible : Conditional
	{
		// Token: 0x06000967 RID: 2407 RVA: 0x00028BE0 File Offset: 0x00026FE0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.renderer = defaultGameObject.GetComponent<Renderer>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x00028C23 File Offset: 0x00027023
		public override TaskStatus OnUpdate()
		{
			if (this.renderer == null)
			{
				return TaskStatus.Failure;
			}
			return (!this.renderer.isVisible) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x00028C4F File Offset: 0x0002704F
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04000866 RID: 2150
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000867 RID: 2151
		private Renderer renderer;

		// Token: 0x04000868 RID: 2152
		private GameObject prevGameObject;
	}
}
