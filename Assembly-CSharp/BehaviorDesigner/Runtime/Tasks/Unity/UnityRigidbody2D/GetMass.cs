using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
	// Token: 0x02000233 RID: 563
	[TaskCategory("Unity/Rigidbody2D")]
	[TaskDescription("Stores the mass of the Rigidbody2D. Returns Success.")]
	public class GetMass : Action
	{
		// Token: 0x06000A1F RID: 2591 RVA: 0x0002A608 File Offset: 0x00028A08
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody2D = defaultGameObject.GetComponent<Rigidbody2D>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x0002A64B File Offset: 0x00028A4B
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody2D == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.rigidbody2D.mass;
			return TaskStatus.Success;
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0002A677 File Offset: 0x00028A77
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04000923 RID: 2339
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000924 RID: 2340
		[Tooltip("The mass of the Rigidbody2D")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x04000925 RID: 2341
		private Rigidbody2D rigidbody2D;

		// Token: 0x04000926 RID: 2342
		private GameObject prevGameObject;
	}
}
