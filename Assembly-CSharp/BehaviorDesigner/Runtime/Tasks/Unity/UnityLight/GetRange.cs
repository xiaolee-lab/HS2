using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityLight
{
	// Token: 0x02000191 RID: 401
	[TaskCategory("Unity/Light")]
	[TaskDescription("Stores the range of the light.")]
	public class GetRange : Action
	{
		// Token: 0x060007E1 RID: 2017 RVA: 0x00024EF4 File Offset: 0x000232F4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x00024F37 File Offset: 0x00023337
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue = this.light.range;
			return TaskStatus.Success;
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x00024F63 File Offset: 0x00023363
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x040006C4 RID: 1732
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040006C5 RID: 1733
		[RequiredField]
		[Tooltip("The range to store")]
		public SharedFloat storeValue;

		// Token: 0x040006C6 RID: 1734
		private Light light;

		// Token: 0x040006C7 RID: 1735
		private GameObject prevGameObject;
	}
}
