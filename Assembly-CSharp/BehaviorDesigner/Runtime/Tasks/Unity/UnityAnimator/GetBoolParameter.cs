using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x0200010A RID: 266
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Stores the bool parameter on an animator. Returns Success.")]
	public class GetBoolParameter : Action
	{
		// Token: 0x060005E2 RID: 1506 RVA: 0x0002053C File Offset: 0x0001E93C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0002057F File Offset: 0x0001E97F
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.animator.GetBool(this.paramaterName.Value);
			return TaskStatus.Success;
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x000205B6 File Offset: 0x0001E9B6
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.paramaterName = string.Empty;
			this.storeValue = false;
		}

		// Token: 0x040004DA RID: 1242
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040004DB RID: 1243
		[Tooltip("The name of the parameter")]
		public SharedString paramaterName;

		// Token: 0x040004DC RID: 1244
		[Tooltip("The value of the bool parameter")]
		[RequiredField]
		public SharedBool storeValue;

		// Token: 0x040004DD RID: 1245
		private Animator animator;

		// Token: 0x040004DE RID: 1246
		private GameObject prevGameObject;
	}
}
