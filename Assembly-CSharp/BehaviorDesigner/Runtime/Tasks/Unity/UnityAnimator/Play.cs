using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x02000118 RID: 280
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Plays an animator state. Returns Success.")]
	public class Play : Action
	{
		// Token: 0x06000619 RID: 1561 RVA: 0x00020E2C File Offset: 0x0001F22C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00020E6F File Offset: 0x0001F26F
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				return TaskStatus.Failure;
			}
			this.animator.Play(this.stateName.Value, this.layer, this.normalizedTime);
			return TaskStatus.Success;
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00020EA7 File Offset: 0x0001F2A7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.stateName = string.Empty;
			this.layer = -1;
			this.normalizedTime = float.NegativeInfinity;
		}

		// Token: 0x0400051B RID: 1307
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400051C RID: 1308
		[Tooltip("The name of the state")]
		public SharedString stateName;

		// Token: 0x0400051D RID: 1309
		[Tooltip("The layer where the state is")]
		public int layer = -1;

		// Token: 0x0400051E RID: 1310
		[Tooltip("The normalized time at which the state will play")]
		public float normalizedTime = float.NegativeInfinity;

		// Token: 0x0400051F RID: 1311
		private Animator animator;

		// Token: 0x04000520 RID: 1312
		private GameObject prevGameObject;
	}
}
