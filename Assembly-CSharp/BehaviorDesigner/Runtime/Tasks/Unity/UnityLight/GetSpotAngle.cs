using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityLight
{
	// Token: 0x02000194 RID: 404
	[TaskCategory("Unity/Light")]
	[TaskDescription("Stores the spot angle of the light.")]
	public class GetSpotAngle : Action
	{
		// Token: 0x060007ED RID: 2029 RVA: 0x000250A4 File Offset: 0x000234A4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x000250E7 File Offset: 0x000234E7
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue = this.light.spotAngle;
			return TaskStatus.Success;
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x00025113 File Offset: 0x00023513
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x040006D0 RID: 1744
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040006D1 RID: 1745
		[RequiredField]
		[Tooltip("The spot angle to store")]
		public SharedFloat storeValue;

		// Token: 0x040006D2 RID: 1746
		private Light light;

		// Token: 0x040006D3 RID: 1747
		private GameObject prevGameObject;
	}
}
