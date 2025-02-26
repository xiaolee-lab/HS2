using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimation
{
	// Token: 0x02000107 RID: 263
	[TaskCategory("Unity/Animation")]
	[TaskDescription("Stops an animation. Stops all animations if animationName is blank. Returns Success.")]
	public class Stop : Action
	{
		// Token: 0x060005D6 RID: 1494 RVA: 0x00020304 File Offset: 0x0001E704
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animation = defaultGameObject.GetComponent<Animation>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x00020348 File Offset: 0x0001E748
		public override TaskStatus OnUpdate()
		{
			if (this.animation == null)
			{
				return TaskStatus.Failure;
			}
			if (string.IsNullOrEmpty(this.animationName.Value))
			{
				this.animation.Stop();
			}
			else
			{
				this.animation.Stop(this.animationName.Value);
			}
			return TaskStatus.Success;
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x000203A4 File Offset: 0x0001E7A4
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.animationName = string.Empty;
		}

		// Token: 0x040004CB RID: 1227
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040004CC RID: 1228
		[Tooltip("The name of the animation")]
		public SharedString animationName;

		// Token: 0x040004CD RID: 1229
		private Animation animation;

		// Token: 0x040004CE RID: 1230
		private GameObject prevGameObject;
	}
}
