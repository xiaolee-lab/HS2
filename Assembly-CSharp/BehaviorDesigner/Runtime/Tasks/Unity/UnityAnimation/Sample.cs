using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimation
{
	// Token: 0x02000104 RID: 260
	[TaskCategory("Unity/Animation")]
	[TaskDescription("Samples animations at the current state. Returns Success.")]
	public class Sample : Action
	{
		// Token: 0x060005CA RID: 1482 RVA: 0x0002017C File Offset: 0x0001E57C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animation = defaultGameObject.GetComponent<Animation>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x000201BF File Offset: 0x0001E5BF
		public override TaskStatus OnUpdate()
		{
			if (this.animation == null)
			{
				return TaskStatus.Failure;
			}
			this.animation.Sample();
			return TaskStatus.Success;
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x000201E0 File Offset: 0x0001E5E0
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x040004C0 RID: 1216
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040004C1 RID: 1217
		private Animation animation;

		// Token: 0x040004C2 RID: 1218
		private GameObject prevGameObject;
	}
}
