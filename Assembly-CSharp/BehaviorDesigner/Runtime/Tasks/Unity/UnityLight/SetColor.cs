using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityLight
{
	// Token: 0x02000195 RID: 405
	[TaskCategory("Unity/Light")]
	[TaskDescription("Sets the color of the light.")]
	public class SetColor : Action
	{
		// Token: 0x060007F1 RID: 2033 RVA: 0x00025134 File Offset: 0x00023534
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.light = defaultGameObject.GetComponent<Light>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x00025177 File Offset: 0x00023577
		public override TaskStatus OnUpdate()
		{
			if (this.light == null)
			{
				return TaskStatus.Failure;
			}
			this.light.color = this.color.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x000251A3 File Offset: 0x000235A3
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.color = Color.white;
		}

		// Token: 0x040006D4 RID: 1748
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040006D5 RID: 1749
		[Tooltip("The color to set")]
		public SharedColor color;

		// Token: 0x040006D6 RID: 1750
		private Light light;

		// Token: 0x040006D7 RID: 1751
		private GameObject prevGameObject;
	}
}
