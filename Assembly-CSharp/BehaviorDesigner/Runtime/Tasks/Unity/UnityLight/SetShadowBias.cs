using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityLight
{
	// Token: 0x0200019B RID: 411
	[TaskCategory("Unity/Light")]
	[TaskDescription("Sets the shadow bias of the light.")]
	public class SetShadowBias : Action
	{
		// Token: 0x06000809 RID: 2057 RVA: 0x00025484 File Offset: 0x00023884
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x000254C7 File Offset: 0x000238C7
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				return TaskStatus.Failure;
			}
			this.light.shadowBias = this.shadowBias.Value;
			return TaskStatus.Success;
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x000254F3 File Offset: 0x000238F3
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.shadowBias = 0f;
		}

		// Token: 0x040006EC RID: 1772
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040006ED RID: 1773
		[Tooltip("The shadow bias to set")]
		public SharedFloat shadowBias;

		// Token: 0x040006EE RID: 1774
		private Light light;

		// Token: 0x040006EF RID: 1775
		private GameObject prevGameObject;
	}
}
