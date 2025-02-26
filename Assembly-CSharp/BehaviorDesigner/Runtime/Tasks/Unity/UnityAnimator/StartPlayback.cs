using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x02000122 RID: 290
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Sets the animator in playback mode.")]
	public class StartPlayback : Action
	{
		// Token: 0x06000646 RID: 1606 RVA: 0x00021858 File Offset: 0x0001FC58
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0002189B File Offset: 0x0001FC9B
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				return TaskStatus.Failure;
			}
			this.animator.StartPlayback();
			return TaskStatus.Success;
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x000218BC File Offset: 0x0001FCBC
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04000555 RID: 1365
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000556 RID: 1366
		private Animator animator;

		// Token: 0x04000557 RID: 1367
		private GameObject prevGameObject;
	}
}
