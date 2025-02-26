using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityLight
{
	// Token: 0x0200019A RID: 410
	[TaskCategory("Unity/Light")]
	[TaskDescription("Sets the range of the light.")]
	public class SetRange : Action
	{
		// Token: 0x06000805 RID: 2053 RVA: 0x000253F4 File Offset: 0x000237F4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x00025437 File Offset: 0x00023837
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				return TaskStatus.Failure;
			}
			this.light.range = this.range.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x00025463 File Offset: 0x00023863
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.range = 0f;
		}

		// Token: 0x040006E8 RID: 1768
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040006E9 RID: 1769
		[Tooltip("The range to set")]
		public SharedFloat range;

		// Token: 0x040006EA RID: 1770
		private Light light;

		// Token: 0x040006EB RID: 1771
		private GameObject prevGameObject;
	}
}
