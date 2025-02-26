using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityBehaviour
{
	// Token: 0x0200014B RID: 331
	[TaskCategory("Unity/Behaviour")]
	[TaskDescription("Returns Success if the object is enabled, otherwise Failure.")]
	public class IsEnabled : Conditional
	{
		// Token: 0x060006EA RID: 1770 RVA: 0x00022F4C File Offset: 0x0002134C
		public override TaskStatus OnUpdate()
		{
			if (this.specifiedObject == null && !(this.specifiedObject.Value is Behaviour))
			{
				return TaskStatus.Failure;
			}
			return (!(this.specifiedObject.Value as Behaviour).enabled) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x00022F9C File Offset: 0x0002139C
		public override void OnReset()
		{
			if (this.specifiedObject != null)
			{
				this.specifiedObject.Value = null;
			}
		}

		// Token: 0x040005F3 RID: 1523
		[Tooltip("The Object to use")]
		public SharedObject specifiedObject;
	}
}
