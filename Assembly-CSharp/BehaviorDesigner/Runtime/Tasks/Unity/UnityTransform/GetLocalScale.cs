using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x02000289 RID: 649
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Stores the local scale of the Transform. Returns Success.")]
	public class GetLocalScale : Action
	{
		// Token: 0x06000B48 RID: 2888 RVA: 0x0002CE90 File Offset: 0x0002B290
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x0002CED3 File Offset: 0x0002B2D3
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.targetTransform.localScale;
			return TaskStatus.Success;
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x0002CEFF File Offset: 0x0002B2FF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04000A19 RID: 2585
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000A1A RID: 2586
		[Tooltip("The local scale of the Transform")]
		[RequiredField]
		public SharedVector3 storeValue;

		// Token: 0x04000A1B RID: 2587
		private Transform targetTransform;

		// Token: 0x04000A1C RID: 2588
		private GameObject prevGameObject;
	}
}
