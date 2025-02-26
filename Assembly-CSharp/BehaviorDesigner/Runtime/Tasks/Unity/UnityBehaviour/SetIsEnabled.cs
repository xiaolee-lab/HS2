using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityBehaviour
{
	// Token: 0x0200014C RID: 332
	[TaskCategory("Unity/Behaviour")]
	[TaskDescription("Enables/Disables the object. Returns Success.")]
	public class SetIsEnabled : Action
	{
		// Token: 0x060006ED RID: 1773 RVA: 0x00022FC0 File Offset: 0x000213C0
		public override TaskStatus OnUpdate()
		{
			if (this.specifiedObject == null && !(this.specifiedObject.Value is Behaviour))
			{
				return TaskStatus.Failure;
			}
			(this.specifiedObject.Value as Behaviour).enabled = this.enabled.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x00023010 File Offset: 0x00021410
		public override void OnReset()
		{
			if (this.specifiedObject != null)
			{
				this.specifiedObject.Value = null;
			}
			this.enabled = false;
		}

		// Token: 0x040005F4 RID: 1524
		[Tooltip("The Object to use")]
		public SharedObject specifiedObject;

		// Token: 0x040005F5 RID: 1525
		[Tooltip("The enabled/disabled state")]
		public SharedBool enabled;
	}
}
