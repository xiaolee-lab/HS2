using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x0200010F RID: 271
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Stores the integer parameter on an animator. Returns Success.")]
	public class GetIntegerParameter : Action
	{
		// Token: 0x060005F6 RID: 1526 RVA: 0x00020844 File Offset: 0x0001EC44
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00020887 File Offset: 0x0001EC87
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.animator.GetInteger(this.paramaterName.Value);
			return TaskStatus.Success;
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x000208BE File Offset: 0x0001ECBE
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.paramaterName = string.Empty;
			this.storeValue = 0;
		}

		// Token: 0x040004F0 RID: 1264
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040004F1 RID: 1265
		[Tooltip("The name of the parameter")]
		public SharedString paramaterName;

		// Token: 0x040004F2 RID: 1266
		[Tooltip("The value of the integer parameter")]
		[RequiredField]
		public SharedInt storeValue;

		// Token: 0x040004F3 RID: 1267
		private Animator animator;

		// Token: 0x040004F4 RID: 1268
		private GameObject prevGameObject;
	}
}
