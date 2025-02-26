using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x0200010D RID: 269
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Stores the float parameter on an animator. Returns Success.")]
	public class GetFloatParameter : Action
	{
		// Token: 0x060005EE RID: 1518 RVA: 0x00020708 File Offset: 0x0001EB08
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0002074B File Offset: 0x0001EB4B
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.animator.GetFloat(this.paramaterName.Value);
			return TaskStatus.Success;
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00020782 File Offset: 0x0001EB82
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.paramaterName = string.Empty;
			this.storeValue = 0f;
		}

		// Token: 0x040004E7 RID: 1255
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040004E8 RID: 1256
		[Tooltip("The name of the parameter")]
		public SharedString paramaterName;

		// Token: 0x040004E9 RID: 1257
		[Tooltip("The value of the float parameter")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x040004EA RID: 1258
		private Animator animator;

		// Token: 0x040004EB RID: 1259
		private GameObject prevGameObject;
	}
}
