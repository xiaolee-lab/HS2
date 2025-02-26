using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityLight
{
	// Token: 0x02000192 RID: 402
	[TaskCategory("Unity/Light")]
	[TaskDescription("Stores the shadow bias of the light.")]
	public class GetShadowBias : Action
	{
		// Token: 0x060007E5 RID: 2021 RVA: 0x00024F84 File Offset: 0x00023384
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00024FC7 File Offset: 0x000233C7
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue = this.light.shadowBias;
			return TaskStatus.Success;
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x00024FF3 File Offset: 0x000233F3
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x040006C8 RID: 1736
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040006C9 RID: 1737
		[RequiredField]
		[Tooltip("The shadow bias to store")]
		public SharedFloat storeValue;

		// Token: 0x040006CA RID: 1738
		private Light light;

		// Token: 0x040006CB RID: 1739
		private GameObject prevGameObject;
	}
}
