using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x02000223 RID: 547
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Sets the mass of the Rigidbody. Returns Success.")]
	public class SetMass : Action
	{
		// Token: 0x060009DF RID: 2527 RVA: 0x00029D34 File Offset: 0x00028134
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x00029D77 File Offset: 0x00028177
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				return TaskStatus.Failure;
			}
			this.rigidbody.mass = this.mass.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x00029DA3 File Offset: 0x000281A3
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.mass = 0f;
		}

		// Token: 0x040008E5 RID: 2277
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040008E6 RID: 2278
		[Tooltip("The mass of the Rigidbody")]
		public SharedFloat mass;

		// Token: 0x040008E7 RID: 2279
		private Rigidbody rigidbody;

		// Token: 0x040008E8 RID: 2280
		private GameObject prevGameObject;
	}
}
