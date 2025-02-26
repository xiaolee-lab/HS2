using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimation
{
	// Token: 0x02000105 RID: 261
	[TaskCategory("Unity/Animation")]
	[TaskDescription("Sets animate physics to the specified value. Returns Success.")]
	public class SetAnimatePhysics : Action
	{
		// Token: 0x060005CE RID: 1486 RVA: 0x000201F4 File Offset: 0x0001E5F4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animation = defaultGameObject.GetComponent<Animation>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00020237 File Offset: 0x0001E637
		public override TaskStatus OnUpdate()
		{
			if (this.animation == null)
			{
				return TaskStatus.Failure;
			}
			this.animation.animatePhysics = this.animatePhysics.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00020263 File Offset: 0x0001E663
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.animatePhysics.Value = false;
		}

		// Token: 0x040004C3 RID: 1219
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040004C4 RID: 1220
		[Tooltip("Are animations executed in the physics loop?")]
		public SharedBool animatePhysics;

		// Token: 0x040004C5 RID: 1221
		private Animation animation;

		// Token: 0x040004C6 RID: 1222
		private GameObject prevGameObject;
	}
}
