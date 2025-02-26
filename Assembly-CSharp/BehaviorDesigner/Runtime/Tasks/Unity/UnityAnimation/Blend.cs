using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimation
{
	// Token: 0x020000FC RID: 252
	[TaskCategory("Unity/Animation")]
	[TaskDescription("Blends the animation. Returns Success.")]
	public class Blend : Action
	{
		// Token: 0x060005AA RID: 1450 RVA: 0x0001FBA4 File Offset: 0x0001DFA4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animation = defaultGameObject.GetComponent<Animation>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0001FBE7 File Offset: 0x0001DFE7
		public override TaskStatus OnUpdate()
		{
			if (this.animation == null)
			{
				return TaskStatus.Failure;
			}
			this.animation.Blend(this.animationName.Value, this.targetWeight, this.fadeLength);
			return TaskStatus.Success;
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0001FC1F File Offset: 0x0001E01F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.animationName = string.Empty;
			this.targetWeight = 1f;
			this.fadeLength = 0.3f;
		}

		// Token: 0x04000496 RID: 1174
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000497 RID: 1175
		[Tooltip("The name of the animation")]
		public SharedString animationName;

		// Token: 0x04000498 RID: 1176
		[Tooltip("The weight the animation should blend to")]
		public float targetWeight = 1f;

		// Token: 0x04000499 RID: 1177
		[Tooltip("The amount of time it takes to blend")]
		public float fadeLength = 0.3f;

		// Token: 0x0400049A RID: 1178
		private Animation animation;

		// Token: 0x0400049B RID: 1179
		private GameObject prevGameObject;
	}
}
