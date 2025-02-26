using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x02000282 RID: 642
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Stores the transform child at the specified index. Returns Success.")]
	public class GetChild : Action
	{
		// Token: 0x06000B2C RID: 2860 RVA: 0x0002CA94 File Offset: 0x0002AE94
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x0002CAD7 File Offset: 0x0002AED7
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.targetTransform.GetChild(this.index.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x0002CB0E File Offset: 0x0002AF0E
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.index = 0;
			this.storeValue = null;
		}

		// Token: 0x040009FC RID: 2556
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040009FD RID: 2557
		[Tooltip("The index of the child")]
		public SharedInt index;

		// Token: 0x040009FE RID: 2558
		[Tooltip("The child of the Transform")]
		[RequiredField]
		public SharedTransform storeValue;

		// Token: 0x040009FF RID: 2559
		private Transform targetTransform;

		// Token: 0x04000A00 RID: 2560
		private GameObject prevGameObject;
	}
}
