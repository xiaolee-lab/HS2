using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x02000111 RID: 273
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Stores the playback speed of the animator. 1 is normal playback speed. Returns Success.")]
	public class GetSpeed : Action
	{
		// Token: 0x060005FE RID: 1534 RVA: 0x00020994 File Offset: 0x0001ED94
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x000209D7 File Offset: 0x0001EDD7
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.animator.speed;
			return TaskStatus.Success;
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00020A03 File Offset: 0x0001EE03
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x040004FA RID: 1274
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040004FB RID: 1275
		[Tooltip("The playback speed of the Animator")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x040004FC RID: 1276
		private Animator animator;

		// Token: 0x040004FD RID: 1277
		private GameObject prevGameObject;
	}
}
