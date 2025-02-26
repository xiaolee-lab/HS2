using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityLight
{
	// Token: 0x0200019F RID: 415
	[TaskCategory("Unity/Light")]
	[TaskDescription("Sets the type of the light.")]
	public class SetType : Action
	{
		// Token: 0x06000819 RID: 2073 RVA: 0x000256B0 File Offset: 0x00023AB0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x000256F3 File Offset: 0x00023AF3
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				return TaskStatus.Failure;
			}
			this.light.type = this.type;
			return TaskStatus.Success;
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x0002571A File Offset: 0x00023B1A
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x040006FC RID: 1788
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040006FD RID: 1789
		[Tooltip("The type to set")]
		public LightType type;

		// Token: 0x040006FE RID: 1790
		private Light light;

		// Token: 0x040006FF RID: 1791
		private GameObject prevGameObject;
	}
}
