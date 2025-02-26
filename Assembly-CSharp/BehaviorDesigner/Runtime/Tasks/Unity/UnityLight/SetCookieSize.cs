using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityLight
{
	// Token: 0x02000197 RID: 407
	[TaskCategory("Unity/Light")]
	[TaskDescription("Sets the light's cookie size.")]
	public class SetCookieSize : Action
	{
		// Token: 0x060007F9 RID: 2041 RVA: 0x00025248 File Offset: 0x00023648
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x0002528B File Offset: 0x0002368B
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				return TaskStatus.Failure;
			}
			this.light.cookieSize = this.cookieSize.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x000252B7 File Offset: 0x000236B7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.cookieSize = 0f;
		}

		// Token: 0x040006DC RID: 1756
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040006DD RID: 1757
		[Tooltip("The size to set")]
		public SharedFloat cookieSize;

		// Token: 0x040006DE RID: 1758
		private Light light;

		// Token: 0x040006DF RID: 1759
		private GameObject prevGameObject;
	}
}
