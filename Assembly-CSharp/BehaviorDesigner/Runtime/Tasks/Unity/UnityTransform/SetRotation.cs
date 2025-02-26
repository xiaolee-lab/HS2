using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x0200029C RID: 668
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Sets the rotation of the Transform. Returns Success.")]
	public class SetRotation : Action
	{
		// Token: 0x06000B94 RID: 2964 RVA: 0x0002D9EC File Offset: 0x0002BDEC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x0002DA2F File Offset: 0x0002BE2F
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				return TaskStatus.Failure;
			}
			this.targetTransform.rotation = this.rotation.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x0002DA5B File Offset: 0x0002BE5B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.rotation = Quaternion.identity;
		}

		// Token: 0x04000A6A RID: 2666
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000A6B RID: 2667
		[Tooltip("The rotation of the Transform")]
		public SharedQuaternion rotation;

		// Token: 0x04000A6C RID: 2668
		private Transform targetTransform;

		// Token: 0x04000A6D RID: 2669
		private GameObject prevGameObject;
	}
}
