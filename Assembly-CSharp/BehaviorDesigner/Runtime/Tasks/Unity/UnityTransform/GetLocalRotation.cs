using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x02000288 RID: 648
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Stores the local rotation of the Transform. Returns Success.")]
	public class GetLocalRotation : Action
	{
		// Token: 0x06000B44 RID: 2884 RVA: 0x0002CE00 File Offset: 0x0002B200
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x0002CE43 File Offset: 0x0002B243
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.targetTransform.localRotation;
			return TaskStatus.Success;
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x0002CE6F File Offset: 0x0002B26F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Quaternion.identity;
		}

		// Token: 0x04000A15 RID: 2581
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000A16 RID: 2582
		[Tooltip("The local rotation of the Transform")]
		[RequiredField]
		public SharedQuaternion storeValue;

		// Token: 0x04000A17 RID: 2583
		private Transform targetTransform;

		// Token: 0x04000A18 RID: 2584
		private GameObject prevGameObject;
	}
}
