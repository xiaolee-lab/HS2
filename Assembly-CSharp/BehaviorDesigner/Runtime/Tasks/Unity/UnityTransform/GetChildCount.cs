using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x02000283 RID: 643
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Stores the number of children a Transform has. Returns Success.")]
	public class GetChildCount : Action
	{
		// Token: 0x06000B30 RID: 2864 RVA: 0x0002CB34 File Offset: 0x0002AF34
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x0002CB77 File Offset: 0x0002AF77
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.targetTransform.childCount;
			return TaskStatus.Success;
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x0002CBA3 File Offset: 0x0002AFA3
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0;
		}

		// Token: 0x04000A01 RID: 2561
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000A02 RID: 2562
		[Tooltip("The number of children")]
		[RequiredField]
		public SharedInt storeValue;

		// Token: 0x04000A03 RID: 2563
		private Transform targetTransform;

		// Token: 0x04000A04 RID: 2564
		private GameObject prevGameObject;
	}
}
