using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x02000290 RID: 656
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Rotates the transform so the forward vector points at worldPosition. Returns Success.")]
	public class LookAt : Action
	{
		// Token: 0x06000B64 RID: 2916 RVA: 0x0002D27C File Offset: 0x0002B67C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x0002D2C0 File Offset: 0x0002B6C0
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				return TaskStatus.Failure;
			}
			if (this.targetLookAt.Value != null)
			{
				this.targetTransform.LookAt(this.targetLookAt.Value.transform);
			}
			else
			{
				this.targetTransform.LookAt(this.worldPosition.Value, this.worldUp);
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x0002D333 File Offset: 0x0002B733
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.targetLookAt = null;
			this.worldPosition = Vector3.up;
			this.worldUp = Vector3.up;
		}

		// Token: 0x04000A35 RID: 2613
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000A36 RID: 2614
		[Tooltip("The GameObject to look at. If null the world position will be used.")]
		public SharedGameObject targetLookAt;

		// Token: 0x04000A37 RID: 2615
		[Tooltip("Point to look at")]
		public SharedVector3 worldPosition;

		// Token: 0x04000A38 RID: 2616
		[Tooltip("Vector specifying the upward direction")]
		public Vector3 worldUp;

		// Token: 0x04000A39 RID: 2617
		private Transform targetTransform;

		// Token: 0x04000A3A RID: 2618
		private GameObject prevGameObject;
	}
}
