using System;
using UnityEngine;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DA0 RID: 3488
	public abstract class MerchantMovement : MerchantAction
	{
		// Token: 0x06006CE2 RID: 27874
		protected abstract bool SetDestination(Vector3 destination);

		// Token: 0x06006CE3 RID: 27875
		protected abstract void UpdateRotation(bool update);

		// Token: 0x06006CE4 RID: 27876
		protected abstract bool HasPath();

		// Token: 0x06006CE5 RID: 27877
		protected abstract Vector3 Velocity();

		// Token: 0x06006CE6 RID: 27878
		protected abstract bool HasArrived();

		// Token: 0x06006CE7 RID: 27879
		protected abstract void Stop();

		// Token: 0x04005B0D RID: 23309
		protected Vector3 _moveDirection = Vector3.zero;

		// Token: 0x04005B0E RID: 23310
		protected Vector3 _moveDirectionVelocity = Vector3.zero;
	}
}
