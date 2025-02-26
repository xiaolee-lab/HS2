using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x0200028F RID: 655
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Returns Success if the transform is a child of the specified GameObject.")]
	public class IsChildOf : Conditional
	{
		// Token: 0x06000B60 RID: 2912 RVA: 0x0002D1E8 File Offset: 0x0002B5E8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x0002D22B File Offset: 0x0002B62B
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				return TaskStatus.Failure;
			}
			return (!this.targetTransform.IsChildOf(this.transformName.Value)) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x0002D262 File Offset: 0x0002B662
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.transformName = null;
		}

		// Token: 0x04000A31 RID: 2609
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000A32 RID: 2610
		[Tooltip("The interested transform")]
		public SharedTransform transformName;

		// Token: 0x04000A33 RID: 2611
		private Transform targetTransform;

		// Token: 0x04000A34 RID: 2612
		private GameObject prevGameObject;
	}
}
