using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x02000221 RID: 545
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Sets the freeze rotation value of the Rigidbody. Returns Success.")]
	public class SetFreezeRotation : Action
	{
		// Token: 0x060009D7 RID: 2519 RVA: 0x00029C1C File Offset: 0x0002801C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x00029C5F File Offset: 0x0002805F
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				return TaskStatus.Failure;
			}
			this.rigidbody.freezeRotation = this.freezeRotation.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x00029C8B File Offset: 0x0002808B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.freezeRotation = false;
		}

		// Token: 0x040008DD RID: 2269
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040008DE RID: 2270
		[Tooltip("The freeze rotation value of the Rigidbody")]
		public SharedBool freezeRotation;

		// Token: 0x040008DF RID: 2271
		private Rigidbody rigidbody;

		// Token: 0x040008E0 RID: 2272
		private GameObject prevGameObject;
	}
}
