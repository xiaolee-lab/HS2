using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x02000296 RID: 662
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Sets the local position of the Transform. Returns Success.")]
	public class SetLocalPosition : Action
	{
		// Token: 0x06000B7C RID: 2940 RVA: 0x0002D694 File Offset: 0x0002BA94
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0002D6D7 File Offset: 0x0002BAD7
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				return TaskStatus.Failure;
			}
			this.targetTransform.localPosition = this.localPosition.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x0002D703 File Offset: 0x0002BB03
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.localPosition = Vector3.zero;
		}

		// Token: 0x04000A52 RID: 2642
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000A53 RID: 2643
		[Tooltip("The local position of the Transform")]
		public SharedVector3 localPosition;

		// Token: 0x04000A54 RID: 2644
		private Transform targetTransform;

		// Token: 0x04000A55 RID: 2645
		private GameObject prevGameObject;
	}
}
