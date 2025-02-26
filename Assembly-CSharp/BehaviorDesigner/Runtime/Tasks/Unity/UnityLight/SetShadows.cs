using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityLight
{
	// Token: 0x0200019C RID: 412
	[TaskCategory("Unity/Light")]
	[TaskDescription("Sets the shadow type of the light.")]
	public class SetShadows : Action
	{
		// Token: 0x0600080D RID: 2061 RVA: 0x00025514 File Offset: 0x00023914
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x00025557 File Offset: 0x00023957
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				return TaskStatus.Failure;
			}
			this.light.shadows = this.shadows;
			return TaskStatus.Success;
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x0002557E File Offset: 0x0002397E
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x040006F0 RID: 1776
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040006F1 RID: 1777
		[Tooltip("The shadow type to set")]
		public LightShadows shadows;

		// Token: 0x040006F2 RID: 1778
		private Light light;

		// Token: 0x040006F3 RID: 1779
		private GameObject prevGameObject;
	}
}
