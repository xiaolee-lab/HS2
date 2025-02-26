using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityLight
{
	// Token: 0x0200018F RID: 399
	[TaskCategory("Unity/Light")]
	[TaskDescription("Stores the light's cookie size.")]
	public class GetCookieSize : Action
	{
		// Token: 0x060007D9 RID: 2009 RVA: 0x00024DD4 File Offset: 0x000231D4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x00024E17 File Offset: 0x00023217
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue = this.light.cookieSize;
			return TaskStatus.Success;
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x00024E43 File Offset: 0x00023243
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x040006BC RID: 1724
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040006BD RID: 1725
		[RequiredField]
		[Tooltip("The size to store")]
		public SharedFloat storeValue;

		// Token: 0x040006BE RID: 1726
		private Light light;

		// Token: 0x040006BF RID: 1727
		private GameObject prevGameObject;
	}
}
