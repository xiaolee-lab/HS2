using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x02000286 RID: 646
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Stores the local euler angles of the Transform. Returns Success.")]
	public class GetLocalEulerAngles : Action
	{
		// Token: 0x06000B3C RID: 2876 RVA: 0x0002CCE0 File Offset: 0x0002B0E0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x0002CD23 File Offset: 0x0002B123
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.targetTransform.localEulerAngles;
			return TaskStatus.Success;
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x0002CD4F File Offset: 0x0002B14F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04000A0D RID: 2573
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000A0E RID: 2574
		[Tooltip("The local euler angles of the Transform")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x04000A0F RID: 2575
		private Transform targetTransform;

		// Token: 0x04000A10 RID: 2576
		private GameObject prevGameObject;
	}
}
