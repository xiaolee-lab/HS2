using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityLight
{
	// Token: 0x02000193 RID: 403
	[TaskCategory("Unity/Light")]
	[TaskDescription("Stores the color of the light.")]
	public class GetShadowStrength : Action
	{
		// Token: 0x060007E9 RID: 2025 RVA: 0x00025014 File Offset: 0x00023414
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x00025057 File Offset: 0x00023457
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue = this.light.shadowStrength;
			return TaskStatus.Success;
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x00025083 File Offset: 0x00023483
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x040006CC RID: 1740
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040006CD RID: 1741
		[RequiredField]
		[Tooltip("The color to store")]
		public SharedFloat storeValue;

		// Token: 0x040006CE RID: 1742
		private Light light;

		// Token: 0x040006CF RID: 1743
		private GameObject prevGameObject;
	}
}
