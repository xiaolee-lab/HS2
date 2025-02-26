using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x02000297 RID: 663
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Sets the local rotation of the Transform. Returns Success.")]
	public class SetLocalRotation : Action
	{
		// Token: 0x06000B80 RID: 2944 RVA: 0x0002D724 File Offset: 0x0002BB24
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0002D767 File Offset: 0x0002BB67
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				return TaskStatus.Failure;
			}
			this.targetTransform.localRotation = this.localRotation.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x0002D793 File Offset: 0x0002BB93
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.localRotation = Quaternion.identity;
		}

		// Token: 0x04000A56 RID: 2646
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000A57 RID: 2647
		[Tooltip("The local rotation of the Transform")]
		public SharedQuaternion localRotation;

		// Token: 0x04000A58 RID: 2648
		private Transform targetTransform;

		// Token: 0x04000A59 RID: 2649
		private GameObject prevGameObject;
	}
}
