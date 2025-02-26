using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
	// Token: 0x02000240 RID: 576
	[TaskCategory("Unity/Rigidbody2D")]
	[TaskDescription("Sets the mass of the Rigidbody2D. Returns Success.")]
	public class SetMass : Action
	{
		// Token: 0x06000A53 RID: 2643 RVA: 0x0002AD34 File Offset: 0x00029134
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x0002AD77 File Offset: 0x00029177
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				return TaskStatus.Failure;
			}
			this.rigidbody2D.mass = this.mass.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x0002ADA3 File Offset: 0x000291A3
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.mass = 0f;
		}

		// Token: 0x04000955 RID: 2389
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000956 RID: 2390
		[Tooltip("The mass of the Rigidbody2D")]
		public SharedFloat mass;

		// Token: 0x04000957 RID: 2391
		private Rigidbody2D rigidbody2D;

		// Token: 0x04000958 RID: 2392
		private GameObject prevGameObject;
	}
}
