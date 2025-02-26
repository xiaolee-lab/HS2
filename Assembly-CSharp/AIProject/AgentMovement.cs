using System;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D99 RID: 3481
	public abstract class AgentMovement : AgentAction
	{
		// Token: 0x06006C9F RID: 27807
		protected abstract bool SetDestination(Vector3 destination);

		// Token: 0x06006CA0 RID: 27808
		protected abstract void UpdateRotation(bool update);

		// Token: 0x06006CA1 RID: 27809
		protected abstract bool HasPath();

		// Token: 0x06006CA2 RID: 27810
		protected abstract Vector3 Velocity();

		// Token: 0x06006CA3 RID: 27811
		protected abstract bool HasArrived();

		// Token: 0x06006CA4 RID: 27812
		protected abstract void Stop();

		// Token: 0x04005AF0 RID: 23280
		protected Vector3 _moveDirection = Vector3.zero;

		// Token: 0x04005AF1 RID: 23281
		protected Vector3 _moveDirectionVelocity = Vector3.zero;
	}
}
