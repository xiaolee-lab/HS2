using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x0200020F RID: 527
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Stores the center of mass of the Rigidbody. Returns Success.")]
	public class GetCenterOfMass : Action
	{
		// Token: 0x0600098F RID: 2447 RVA: 0x00029234 File Offset: 0x00027634
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x00029277 File Offset: 0x00027677
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.rigidbody.centerOfMass;
			return TaskStatus.Success;
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x000292A3 File Offset: 0x000276A3
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04000897 RID: 2199
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000898 RID: 2200
		[Tooltip("The center of mass of the Rigidbody")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x04000899 RID: 2201
		private Rigidbody rigidbody;

		// Token: 0x0400089A RID: 2202
		private GameObject prevGameObject;
	}
}
