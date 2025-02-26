using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimation
{
	// Token: 0x02000100 RID: 256
	[TaskCategory("Unity/Animation")]
	[TaskDescription("Returns Success if the animation is currently playing.")]
	public class IsPlaying : Conditional
	{
		// Token: 0x060005BA RID: 1466 RVA: 0x0001FE68 File Offset: 0x0001E268
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animation = defaultGameObject.GetComponent<Animation>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x0001FEAC File Offset: 0x0001E2AC
		public override TaskStatus OnUpdate()
		{
			if (this.animation == null)
			{
				return TaskStatus.Failure;
			}
			if (string.IsNullOrEmpty(this.animationName.Value))
			{
				return (!this.animation.isPlaying) ? TaskStatus.Failure : TaskStatus.Success;
			}
			return (!this.animation.IsPlaying(this.animationName.Value)) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0001FF1B File Offset: 0x0001E31B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.animationName.Value = string.Empty;
		}

		// Token: 0x040004AD RID: 1197
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040004AE RID: 1198
		[Tooltip("The name of the animation")]
		public SharedString animationName;

		// Token: 0x040004AF RID: 1199
		private Animation animation;

		// Token: 0x040004B0 RID: 1200
		private GameObject prevGameObject;
	}
}
