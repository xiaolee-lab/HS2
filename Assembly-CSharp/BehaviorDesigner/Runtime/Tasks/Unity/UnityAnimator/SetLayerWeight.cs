using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x0200011D RID: 285
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Sets the layer's current weight. Returns Success.")]
	public class SetLayerWeight : Action
	{
		// Token: 0x06000630 RID: 1584 RVA: 0x00021494 File Offset: 0x0001F894
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x000214D7 File Offset: 0x0001F8D7
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				return TaskStatus.Failure;
			}
			this.animator.SetLayerWeight(this.index.Value, this.weight.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x0002150E File Offset: 0x0001F90E
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.index = 0;
			this.weight = 0f;
		}

		// Token: 0x0400053A RID: 1338
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400053B RID: 1339
		[Tooltip("The layer's index")]
		public SharedInt index;

		// Token: 0x0400053C RID: 1340
		[Tooltip("The weight of the layer")]
		public SharedFloat weight;

		// Token: 0x0400053D RID: 1341
		private Animator animator;

		// Token: 0x0400053E RID: 1342
		private GameObject prevGameObject;
	}
}
