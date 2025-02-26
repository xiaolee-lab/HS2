using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform
{
	// Token: 0x02000281 RID: 641
	[TaskCategory("Unity/Transform")]
	[TaskDescription("Gets the Angle between a GameObject's forward direction and a target. Returns Success.")]
	public class GetAngleToTarget : Action
	{
		// Token: 0x06000B28 RID: 2856 RVA: 0x0002C948 File Offset: 0x0002AD48
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.targetTransform = defaultGameObject.GetComponent<Transform>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x0002C98C File Offset: 0x0002AD8C
		public override TaskStatus OnUpdate()
		{
			if (this.targetTransform == null)
			{
				return TaskStatus.Failure;
			}
			Vector3 a;
			if (this.targetObject.Value != null)
			{
				a = this.targetObject.Value.transform.InverseTransformPoint(this.targetPosition.Value);
			}
			else
			{
				a = this.targetPosition.Value;
			}
			if (this.ignoreHeight.Value)
			{
				a.y = this.targetTransform.position.y;
			}
			Vector3 from = a - this.targetTransform.position;
			this.storeValue.Value = Vector3.Angle(from, this.targetTransform.forward);
			return TaskStatus.Success;
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x0002CA4D File Offset: 0x0002AE4D
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.targetObject = null;
			this.targetPosition = Vector3.zero;
			this.ignoreHeight = true;
			this.storeValue = 0f;
		}

		// Token: 0x040009F5 RID: 2549
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040009F6 RID: 2550
		[Tooltip("The target object to measure the angle to. If null the targetPosition will be used.")]
		public SharedGameObject targetObject;

		// Token: 0x040009F7 RID: 2551
		[Tooltip("The world position to measure an angle to. If the targetObject is also not null, this value is used as an offset from that object's position.")]
		public SharedVector3 targetPosition;

		// Token: 0x040009F8 RID: 2552
		[Tooltip("Ignore height differences when calculating the angle?")]
		public SharedBool ignoreHeight = true;

		// Token: 0x040009F9 RID: 2553
		[Tooltip("The angle to the target")]
		[RequiredField]
		public SharedFloat storeValue;

		// Token: 0x040009FA RID: 2554
		private Transform targetTransform;

		// Token: 0x040009FB RID: 2555
		private GameObject prevGameObject;
	}
}
