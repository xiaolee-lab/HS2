using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
	// Token: 0x020000B5 RID: 181
	public abstract class Movement : Action
	{
		// Token: 0x0600042C RID: 1068
		protected abstract bool SetDestination(Vector3 destination);

		// Token: 0x0600042D RID: 1069
		protected abstract void UpdateRotation(bool update);

		// Token: 0x0600042E RID: 1070
		protected abstract bool HasPath();

		// Token: 0x0600042F RID: 1071
		protected abstract Vector3 Velocity();

		// Token: 0x06000430 RID: 1072
		protected abstract bool HasArrived();

		// Token: 0x06000431 RID: 1073
		protected abstract void Stop();
	}
}
