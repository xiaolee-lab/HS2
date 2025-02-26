using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimation
{
	// Token: 0x02000101 RID: 257
	[TaskCategory("Unity/Animation")]
	[TaskDescription("Plays animation without any blending. Returns Success.")]
	public class Play : Action
	{
		// Token: 0x060005BE RID: 1470 RVA: 0x0001FF3C File Offset: 0x0001E33C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animation = defaultGameObject.GetComponent<Animation>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0001FF80 File Offset: 0x0001E380
		public override TaskStatus OnUpdate()
		{
			if (this.animation == null)
			{
				return TaskStatus.Failure;
			}
			if (string.IsNullOrEmpty(this.animationName.Value))
			{
				this.animation.Play();
			}
			else
			{
				this.animation.Play(this.animationName.Value, this.playMode);
			}
			return TaskStatus.Success;
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x0001FFE4 File Offset: 0x0001E3E4
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.animationName.Value = string.Empty;
			this.playMode = PlayMode.StopSameLayer;
		}

		// Token: 0x040004B1 RID: 1201
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040004B2 RID: 1202
		[Tooltip("The name of the animation")]
		public SharedString animationName;

		// Token: 0x040004B3 RID: 1203
		[Tooltip("The play mode of the animation")]
		public PlayMode playMode;

		// Token: 0x040004B4 RID: 1204
		private Animation animation;

		// Token: 0x040004B5 RID: 1205
		private GameObject prevGameObject;
	}
}
