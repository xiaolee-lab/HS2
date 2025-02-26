using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x02000284 RID: 644
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Stores the euler angles of the Transform. Returns Success.")]
	public class GetEulerAngles : Action
	{
		// Token: 0x06000B34 RID: 2868 RVA: 0x0002CBC0 File Offset: 0x0002AFC0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x0002CC03 File Offset: 0x0002B003
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.targetTransform.eulerAngles;
			return TaskStatus.Success;
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x0002CC2F File Offset: 0x0002B02F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04000A05 RID: 2565
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000A06 RID: 2566
		[Tooltip("The euler angles of the Transform")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x04000A07 RID: 2567
		private Transform targetTransform;

		// Token: 0x04000A08 RID: 2568
		private GameObject prevGameObject;
	}
}
