using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x02000280 RID: 640
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Finds a transform by name. Returns Success.")]
	public class Find : Action
	{
		// Token: 0x06000B24 RID: 2852 RVA: 0x0002C8A0 File Offset: 0x0002ACA0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x0002C8E3 File Offset: 0x0002ACE3
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.targetTransform.Find(this.transformName.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x0002C91A File Offset: 0x0002AD1A
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.transformName = null;
			this.storeValue = null;
		}

		// Token: 0x040009F0 RID: 2544
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040009F1 RID: 2545
		[Tooltip("The transform name to find")]
		public SharedString transformName;

		// Token: 0x040009F2 RID: 2546
		[Tooltip("The object found by name")]
		[RequiredField]
		public SharedTransform storeValue;

		// Token: 0x040009F3 RID: 2547
		private Transform targetTransform;

		// Token: 0x040009F4 RID: 2548
		private GameObject prevGameObject;
	}
}
