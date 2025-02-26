using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimation
{
	// Token: 0x020000FE RID: 254
	[TaskCategory("Unity/Animation")]
	[TaskDescription("Cross fades an animation after previous animations has finished playing. Returns Success.")]
	public class CrossFadeQueued : Action
	{
		// Token: 0x060005B2 RID: 1458 RVA: 0x0001FD20 File Offset: 0x0001E120
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animation = defaultGameObject.GetComponent<Animation>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0001FD63 File Offset: 0x0001E163
		public override TaskStatus OnUpdate()
		{
			if (this.animation == null)
			{
				return TaskStatus.Failure;
			}
			this.animation.CrossFadeQueued(this.animationName.Value, this.fadeLength, this.queue, this.playMode);
			return TaskStatus.Success;
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x0001FDA2 File Offset: 0x0001E1A2
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.animationName.Value = string.Empty;
			this.fadeLength = 0.3f;
			this.queue = QueueMode.CompleteOthers;
			this.playMode = PlayMode.StopSameLayer;
		}

		// Token: 0x040004A2 RID: 1186
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040004A3 RID: 1187
		[Tooltip("The name of the animation")]
		public SharedString animationName;

		// Token: 0x040004A4 RID: 1188
		[Tooltip("The amount of time it takes to blend")]
		public float fadeLength = 0.3f;

		// Token: 0x040004A5 RID: 1189
		[Tooltip("Specifies when the animation should start playing")]
		public QueueMode queue;

		// Token: 0x040004A6 RID: 1190
		[Tooltip("The play mode of the animation")]
		public PlayMode playMode;

		// Token: 0x040004A7 RID: 1191
		private Animation animation;

		// Token: 0x040004A8 RID: 1192
		private GameObject prevGameObject;
	}
}
