using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityLight
{
	// Token: 0x0200018E RID: 398
	[TaskCategory("Unity/Light")]
	[TaskDescription("Stores the color of the light.")]
	public class GetColor : Action
	{
		// Token: 0x060007D5 RID: 2005 RVA: 0x00024D44 File Offset: 0x00023144
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x00024D87 File Offset: 0x00023187
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue = this.light.color;
			return TaskStatus.Success;
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x00024DB3 File Offset: 0x000231B3
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Color.white;
		}

		// Token: 0x040006B8 RID: 1720
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040006B9 RID: 1721
		[RequiredField]
		[Tooltip("The color to store")]
		public SharedColor storeValue;

		// Token: 0x040006BA RID: 1722
		private Light light;

		// Token: 0x040006BB RID: 1723
		private GameObject prevGameObject;
	}
}
