using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x0200029A RID: 666
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Sets the position of the Transform. Returns Success.")]
	public class SetPosition : Action
	{
		// Token: 0x06000B8C RID: 2956 RVA: 0x0002D8CC File Offset: 0x0002BCCC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x0002D90F File Offset: 0x0002BD0F
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				return TaskStatus.Failure;
			}
			this.targetTransform.position = this.position.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x0002D93B File Offset: 0x0002BD3B
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.position = Vector3.zero;
		}

		// Token: 0x04000A62 RID: 2658
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000A63 RID: 2659
		[Tooltip("The position of the Transform")]
		public SharedVector3 position;

		// Token: 0x04000A64 RID: 2660
		private Transform targetTransform;

		// Token: 0x04000A65 RID: 2661
		private GameObject prevGameObject;
	}
}
