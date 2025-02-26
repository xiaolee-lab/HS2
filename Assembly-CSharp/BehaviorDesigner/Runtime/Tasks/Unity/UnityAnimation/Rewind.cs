using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimation
{
	// Token: 0x02000103 RID: 259
	[TaskCategory("Unity/Animation")]
	[TaskDescription("Rewinds an animation. Rewinds all animations if animationName is blank. Returns Success.")]
	public class Rewind : Action
	{
		// Token: 0x060005C6 RID: 1478 RVA: 0x000200B8 File Offset: 0x0001E4B8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animation = defaultGameObject.GetComponent<Animation>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x000200FC File Offset: 0x0001E4FC
		public override TaskStatus OnUpdate()
		{
			if (this.animation == null)
			{
				return TaskStatus.Failure;
			}
			if (string.IsNullOrEmpty(this.animationName.Value))
			{
				this.animation.Rewind();
			}
			else
			{
				this.animation.Rewind(this.animationName.Value);
			}
			return TaskStatus.Success;
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00020158 File Offset: 0x0001E558
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.animationName.Value = string.Empty;
		}

		// Token: 0x040004BC RID: 1212
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040004BD RID: 1213
		[Tooltip("The name of the animation")]
		public SharedString animationName;

		// Token: 0x040004BE RID: 1214
		private Animation animation;

		// Token: 0x040004BF RID: 1215
		private GameObject prevGameObject;
	}
}
