using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimation
{
	// Token: 0x020000FF RID: 255
	[TaskCategory("Unity/Animation")]
	[TaskDescription("Stores the animate physics value. Returns Success.")]
	public class GetAnimatePhysics : Action
	{
		// Token: 0x060005B6 RID: 1462 RVA: 0x0001FDDC File Offset: 0x0001E1DC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animation = defaultGameObject.GetComponent<Animation>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x0001FE1F File Offset: 0x0001E21F
		public override TaskStatus OnUpdate()
		{
			if (this.animation == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.animation.animatePhysics;
			return TaskStatus.Success;
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x0001FE4B File Offset: 0x0001E24B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue.Value = false;
		}

		// Token: 0x040004A9 RID: 1193
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040004AA RID: 1194
		[Tooltip("Are the if animations are executed in the physics loop?")]
		[RequiredField]
		public SharedBool storeValue;

		// Token: 0x040004AB RID: 1195
		private Animation animation;

		// Token: 0x040004AC RID: 1196
		private GameObject prevGameObject;
	}
}
