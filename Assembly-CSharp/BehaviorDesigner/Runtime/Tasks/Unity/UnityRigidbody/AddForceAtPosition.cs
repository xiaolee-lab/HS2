using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x02000209 RID: 521
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Applies a force at the specified position to the rigidbody. Returns Success.")]
	public class AddForceAtPosition : Action
	{
		// Token: 0x06000977 RID: 2423 RVA: 0x00028E90 File Offset: 0x00027290
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x00028ED3 File Offset: 0x000272D3
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				return TaskStatus.Failure;
			}
			this.rigidbody.AddForceAtPosition(this.force.Value, this.position.Value, this.forceMode);
			return TaskStatus.Success;
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x00028F10 File Offset: 0x00027310
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.force = Vector3.zero;
			this.position = Vector3.zero;
			this.forceMode = ForceMode.Force;
		}

		// Token: 0x0400087A RID: 2170
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400087B RID: 2171
		[Tooltip("The amount of force to apply")]
		public SharedVector3 force;

		// Token: 0x0400087C RID: 2172
		[Tooltip("The position of the force")]
		public SharedVector3 position;

		// Token: 0x0400087D RID: 2173
		[Tooltip("The type of force")]
		public ForceMode forceMode;

		// Token: 0x0400087E RID: 2174
		private Rigidbody rigidbody;

		// Token: 0x0400087F RID: 2175
		private GameObject prevGameObject;
	}
}
