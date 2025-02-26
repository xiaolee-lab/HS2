using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x02000108 RID: 264
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Creates a dynamic transition between the current state and the destination state. Returns Success.")]
	public class CrossFade : Action
	{
		// Token: 0x060005DA RID: 1498 RVA: 0x000203D8 File Offset: 0x0001E7D8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0002041C File Offset: 0x0001E81C
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				return TaskStatus.Failure;
			}
			this.animator.CrossFade(this.stateName.Value, this.transitionDuration.Value, this.layer, this.normalizedTime);
			return TaskStatus.Success;
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0002046A File Offset: 0x0001E86A
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.stateName = string.Empty;
			this.transitionDuration = 0f;
			this.layer = -1;
			this.normalizedTime = float.NegativeInfinity;
		}

		// Token: 0x040004CF RID: 1231
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040004D0 RID: 1232
		[Tooltip("The name of the state")]
		public SharedString stateName;

		// Token: 0x040004D1 RID: 1233
		[Tooltip("The duration of the transition. Value is in source state normalized time")]
		public SharedFloat transitionDuration;

		// Token: 0x040004D2 RID: 1234
		[Tooltip("The layer where the state is")]
		public int layer = -1;

		// Token: 0x040004D3 RID: 1235
		[Tooltip("The normalized time at which the state will play")]
		public float normalizedTime = float.NegativeInfinity;

		// Token: 0x040004D4 RID: 1236
		private Animator animator;

		// Token: 0x040004D5 RID: 1237
		private GameObject prevGameObject;
	}
}
