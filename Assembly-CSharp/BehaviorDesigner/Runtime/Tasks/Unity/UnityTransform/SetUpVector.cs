using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x0200029D RID: 669
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Sets the up vector of the Transform. Returns Success.")]
	public class SetUpVector : Action
	{
		// Token: 0x06000B98 RID: 2968 RVA: 0x0002DA7C File Offset: 0x0002BE7C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x0002DABF File Offset: 0x0002BEBF
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				return TaskStatus.Failure;
			}
			this.targetTransform.up = this.position.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x0002DAEB File Offset: 0x0002BEEB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.position = Vector3.zero;
		}

		// Token: 0x04000A6E RID: 2670
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000A6F RID: 2671
		[Tooltip("The position of the Transform")]
		public SharedVector3 position;

		// Token: 0x04000A70 RID: 2672
		private Transform targetTransform;

		// Token: 0x04000A71 RID: 2673
		private GameObject prevGameObject;
	}
}
