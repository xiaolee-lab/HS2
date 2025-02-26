using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimation
{
	// Token: 0x02000102 RID: 258
	[TaskCategory("Unity/Animation")]
	[TaskDescription("Plays an animation after previous animations has finished playing. Returns Success.")]
	public class PlayQueued : Action
	{
		// Token: 0x060005C2 RID: 1474 RVA: 0x0002000C File Offset: 0x0001E40C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animation = defaultGameObject.GetComponent<Animation>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x0002004F File Offset: 0x0001E44F
		public override TaskStatus OnUpdate()
		{
			if (this.animation == null)
			{
				return TaskStatus.Failure;
			}
			this.animation.PlayQueued(this.animationName.Value, this.queue, this.playMode);
			return TaskStatus.Success;
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00020088 File Offset: 0x0001E488
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.animationName.Value = string.Empty;
			this.queue = QueueMode.CompleteOthers;
			this.playMode = PlayMode.StopSameLayer;
		}

		// Token: 0x040004B6 RID: 1206
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040004B7 RID: 1207
		[Tooltip("The name of the animation")]
		public SharedString animationName;

		// Token: 0x040004B8 RID: 1208
		[Tooltip("Specifies when the animation should start playing")]
		public QueueMode queue;

		// Token: 0x040004B9 RID: 1209
		[Tooltip("The play mode of the animation")]
		public PlayMode playMode;

		// Token: 0x040004BA RID: 1210
		private Animation animation;

		// Token: 0x040004BB RID: 1211
		private GameObject prevGameObject;
	}
}
