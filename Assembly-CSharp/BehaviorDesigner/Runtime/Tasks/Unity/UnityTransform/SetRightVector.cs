using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x0200029B RID: 667
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Sets the right vector of the Transform. Returns Success.")]
	public class SetRightVector : Action
	{
		// Token: 0x06000B90 RID: 2960 RVA: 0x0002D95C File Offset: 0x0002BD5C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x0002D99F File Offset: 0x0002BD9F
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				return TaskStatus.Failure;
			}
			this.targetTransform.right = this.position.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x0002D9CB File Offset: 0x0002BDCB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.position = Vector3.zero;
		}

		// Token: 0x04000A66 RID: 2662
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000A67 RID: 2663
		[Tooltip("The position of the Transform")]
		public SharedVector3 position;

		// Token: 0x04000A68 RID: 2664
		private Transform targetTransform;

		// Token: 0x04000A69 RID: 2665
		private GameObject prevGameObject;
	}
}
