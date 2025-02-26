using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimation
{
	// Token: 0x02000106 RID: 262
	[TaskCategory("Unity/Animation")]
	[TaskDescription("Sets the wrap mode to the specified value. Returns Success.")]
	public class SetWrapMode : Action
	{
		// Token: 0x060005D2 RID: 1490 RVA: 0x00020280 File Offset: 0x0001E680
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animation = defaultGameObject.GetComponent<Animation>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x000202C3 File Offset: 0x0001E6C3
		public override TaskStatus OnUpdate()
		{
			if (this.animation == null)
			{
				return TaskStatus.Failure;
			}
			this.animation.wrapMode = this.wrapMode;
			return TaskStatus.Success;
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x000202EA File Offset: 0x0001E6EA
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.wrapMode = WrapMode.Default;
		}

		// Token: 0x040004C7 RID: 1223
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040004C8 RID: 1224
		[Tooltip("How should time beyond the playback range of the clip be treated?")]
		public WrapMode wrapMode;

		// Token: 0x040004C9 RID: 1225
		private Animation animation;

		// Token: 0x040004CA RID: 1226
		private GameObject prevGameObject;
	}
}
