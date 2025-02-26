using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x02000110 RID: 272
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Stores the layer's weight. Returns Success.")]
	public class GetLayerWeight : Action
	{
		// Token: 0x060005FA RID: 1530 RVA: 0x000208EC File Offset: 0x0001ECEC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0002092F File Offset: 0x0001ED2F
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.animator.GetLayerWeight(this.index.Value);
			return TaskStatus.Success;
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00020966 File Offset: 0x0001ED66
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.index = 0;
			this.storeValue = 0f;
		}

		// Token: 0x040004F5 RID: 1269
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040004F6 RID: 1270
		[Tooltip("The index of the layer")]
		public SharedInt index;

		// Token: 0x040004F7 RID: 1271
		[Tooltip("The value of the float parameter")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x040004F8 RID: 1272
		private Animator animator;

		// Token: 0x040004F9 RID: 1273
		private GameObject prevGameObject;
	}
}
