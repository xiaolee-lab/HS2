using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityLight
{
	// Token: 0x0200019E RID: 414
	[TaskCategory("Unity/Light")]
	[TaskDescription("Sets the spot angle of the light.")]
	public class SetSpotAngle : Action
	{
		// Token: 0x06000815 RID: 2069 RVA: 0x00025620 File Offset: 0x00023A20
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x00025663 File Offset: 0x00023A63
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				return TaskStatus.Failure;
			}
			this.light.spotAngle = this.spotAngle.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0002568F File Offset: 0x00023A8F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.spotAngle = 0f;
		}

		// Token: 0x040006F8 RID: 1784
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040006F9 RID: 1785
		[Tooltip("The spot angle to set")]
		public SharedFloat spotAngle;

		// Token: 0x040006FA RID: 1786
		private Light light;

		// Token: 0x040006FB RID: 1787
		private GameObject prevGameObject;
	}
}
