using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x02000287 RID: 647
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Stores the local position of the Transform. Returns Success.")]
	public class GetLocalPosition : Action
	{
		// Token: 0x06000B40 RID: 2880 RVA: 0x0002CD70 File Offset: 0x0002B170
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x0002CDB3 File Offset: 0x0002B1B3
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.targetTransform.localPosition;
			return TaskStatus.Success;
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x0002CDDF File Offset: 0x0002B1DF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04000A11 RID: 2577
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000A12 RID: 2578
		[Tooltip("The local position of the Transform")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x04000A13 RID: 2579
		private Transform targetTransform;

		// Token: 0x04000A14 RID: 2580
		private GameObject prevGameObject;
	}
}
