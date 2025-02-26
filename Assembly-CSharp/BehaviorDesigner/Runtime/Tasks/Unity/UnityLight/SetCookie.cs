using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityLight
{
	// Token: 0x02000196 RID: 406
	[TaskCategory("Unity/Light")]
	[TaskDescription("Sets the cookie of the light.")]
	public class SetCookie : Action
	{
		// Token: 0x060007F5 RID: 2037 RVA: 0x000251C4 File Offset: 0x000235C4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x00025207 File Offset: 0x00023607
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				return TaskStatus.Failure;
			}
			this.light.cookie = this.cookie;
			return TaskStatus.Success;
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x0002522E File Offset: 0x0002362E
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.cookie = null;
		}

		// Token: 0x040006D8 RID: 1752
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040006D9 RID: 1753
		[Tooltip("The cookie to set")]
		public Texture2D cookie;

		// Token: 0x040006DA RID: 1754
		private Light light;

		// Token: 0x040006DB RID: 1755
		private GameObject prevGameObject;
	}
}
