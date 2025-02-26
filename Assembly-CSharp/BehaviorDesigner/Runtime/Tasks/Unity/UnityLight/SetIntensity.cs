using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityLight
{
	// Token: 0x02000199 RID: 409
	[TaskCategory("Unity/Light")]
	[TaskDescription("Sets the intensity of the light.")]
	public class SetIntensity : Action
	{
		// Token: 0x06000801 RID: 2049 RVA: 0x00025364 File Offset: 0x00023764
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x000253A7 File Offset: 0x000237A7
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				return TaskStatus.Failure;
			}
			this.light.intensity = this.intensity.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x000253D3 File Offset: 0x000237D3
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.intensity = 0f;
		}

		// Token: 0x040006E4 RID: 1764
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040006E5 RID: 1765
		[Tooltip("The intensity to set")]
		public SharedFloat intensity;

		// Token: 0x040006E6 RID: 1766
		private Light light;

		// Token: 0x040006E7 RID: 1767
		private GameObject prevGameObject;
	}
}
