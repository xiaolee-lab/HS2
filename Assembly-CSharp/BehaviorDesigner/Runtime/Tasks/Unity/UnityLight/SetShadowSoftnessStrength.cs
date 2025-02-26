using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityLight
{
	// Token: 0x0200019D RID: 413
	[TaskCategory("Unity/Light")]
	[TaskDescription("Sets the shadow strength of the light.")]
	public class SetShadowSoftnessStrength : Action
	{
		// Token: 0x06000811 RID: 2065 RVA: 0x00025590 File Offset: 0x00023990
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x000255D3 File Offset: 0x000239D3
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				return TaskStatus.Failure;
			}
			this.light.shadowStrength = this.shadowStrength.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x000255FF File Offset: 0x000239FF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.shadowStrength = 0f;
		}

		// Token: 0x040006F4 RID: 1780
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040006F5 RID: 1781
		[Tooltip("The shadow strength to set")]
		public SharedFloat shadowStrength;

		// Token: 0x040006F6 RID: 1782
		private Light light;

		// Token: 0x040006F7 RID: 1783
		private GameObject prevGameObject;
	}
}
