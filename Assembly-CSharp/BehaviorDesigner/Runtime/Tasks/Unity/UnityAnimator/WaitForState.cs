using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x02000126 RID: 294
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Waits for the Animator to reach the specified state.")]
	public class WaitForState : Action
	{
		// Token: 0x06000656 RID: 1622 RVA: 0x00021A4D File Offset: 0x0001FE4D
		public override void OnAwake()
		{
			this.stateHash = Animator.StringToHash(this.stateName.Value);
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x00021A68 File Offset: 0x0001FE68
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
				if (!this.animator.HasState(this.layer.Value, this.stateHash))
				{
				}
			}
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00021ACC File Offset: 0x0001FECC
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				return TaskStatus.Failure;
			}
			if (this.animator.GetCurrentAnimatorStateInfo(this.layer.Value).fullPathHash == this.stateHash)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Running;
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00021B18 File Offset: 0x0001FF18
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.stateName = string.Empty;
			this.layer = -1;
		}

		// Token: 0x04000562 RID: 1378
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000563 RID: 1379
		[Tooltip("The name of the state")]
		public SharedString stateName;

		// Token: 0x04000564 RID: 1380
		[Tooltip("The layer where the state is")]
		public SharedInt layer = -1;

		// Token: 0x04000565 RID: 1381
		private Animator animator;

		// Token: 0x04000566 RID: 1382
		private GameObject prevGameObject;

		// Token: 0x04000567 RID: 1383
		private int stateHash;
	}
}
