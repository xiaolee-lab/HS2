using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityLight
{
	// Token: 0x02000190 RID: 400
	[TaskCategory("Unity/Light")]
	[TaskDescription("Stores the intensity of the light.")]
	public class GetIntensity : Action
	{
		// Token: 0x060007DD RID: 2013 RVA: 0x00024E64 File Offset: 0x00023264
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00024EA7 File Offset: 0x000232A7
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue = this.light.intensity;
			return TaskStatus.Success;
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x00024ED3 File Offset: 0x000232D3
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x040006C0 RID: 1728
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040006C1 RID: 1729
		[RequiredField]
		[Tooltip("The intensity to store")]
		public SharedFloat storeValue;

		// Token: 0x040006C2 RID: 1730
		private Light light;

		// Token: 0x040006C3 RID: 1731
		private GameObject prevGameObject;
	}
}
