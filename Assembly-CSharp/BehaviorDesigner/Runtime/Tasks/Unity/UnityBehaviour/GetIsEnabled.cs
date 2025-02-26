using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityBehaviour
{
	// Token: 0x0200014A RID: 330
	[TaskCategory("Unity/Behaviour")]
	[TaskDescription("Stores the enabled state of the object. Returns Success.")]
	public class GetIsEnabled : Action
	{
		// Token: 0x060006E7 RID: 1767 RVA: 0x00022ECC File Offset: 0x000212CC
		public override TaskStatus OnUpdate()
		{
			if (this.specifiedObject == null && !(this.specifiedObject.Value is Behaviour))
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = (this.specifiedObject.Value as Behaviour).enabled;
			return TaskStatus.Success;
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00022F1C File Offset: 0x0002131C
		public override void OnReset()
		{
			if (this.specifiedObject != null)
			{
				this.specifiedObject.Value = null;
			}
			this.storeValue = false;
		}

		// Token: 0x040005F1 RID: 1521
		[Tooltip("The Object to use")]
		public SharedObject specifiedObject;

		// Token: 0x040005F2 RID: 1522
		[Tooltip("The enabled/disabled state")]
		[RequiredField]
		public SharedBool storeValue;
	}
}
