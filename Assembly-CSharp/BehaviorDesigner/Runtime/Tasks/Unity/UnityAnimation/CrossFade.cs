using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimation
{
	// Token: 0x020000FD RID: 253
	[TaskCategory("Unity/Animation")]
	[TaskDescription("Fades the animation over a period of time and fades other animations out. Returns Success.")]
	public class CrossFade : Action
	{
		// Token: 0x060005AE RID: 1454 RVA: 0x0001FC64 File Offset: 0x0001E064
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animation = defaultGameObject.GetComponent<Animation>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0001FCA7 File Offset: 0x0001E0A7
		public override TaskStatus OnUpdate()
		{
			if (this.animation == null)
			{
				return TaskStatus.Failure;
			}
			this.animation.CrossFade(this.animationName.Value, this.fadeLength, this.playMode);
			return TaskStatus.Success;
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0001FCDF File Offset: 0x0001E0DF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.animationName.Value = string.Empty;
			this.fadeLength = 0.3f;
			this.playMode = PlayMode.StopSameLayer;
		}

		// Token: 0x0400049C RID: 1180
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400049D RID: 1181
		[Tooltip("The name of the animation")]
		public SharedString animationName;

		// Token: 0x0400049E RID: 1182
		[Tooltip("The amount of time it takes to blend")]
		public float fadeLength = 0.3f;

		// Token: 0x0400049F RID: 1183
		[Tooltip("The play mode of the animation")]
		public PlayMode playMode;

		// Token: 0x040004A0 RID: 1184
		private Animation animation;

		// Token: 0x040004A1 RID: 1185
		private GameObject prevGameObject;
	}
}
