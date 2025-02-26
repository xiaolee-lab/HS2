using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x02000299 RID: 665
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Sets the parent of the Transform. Returns Success.")]
	public class SetParent : Action
	{
		// Token: 0x06000B88 RID: 2952 RVA: 0x0002D844 File Offset: 0x0002BC44
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x0002D887 File Offset: 0x0002BC87
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				return TaskStatus.Failure;
			}
			this.targetTransform.parent = this.parent.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x0002D8B3 File Offset: 0x0002BCB3
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.parent = null;
		}

		// Token: 0x04000A5E RID: 2654
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000A5F RID: 2655
		[Tooltip("The parent of the Transform")]
		public SharedTransform parent;

		// Token: 0x04000A60 RID: 2656
		private Transform targetTransform;

		// Token: 0x04000A61 RID: 2657
		private GameObject prevGameObject;
	}
}
