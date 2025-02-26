using System;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000C61 RID: 3169
	public abstract class ActorAnimationThirdPerson : ActorAnimation
	{
		// Token: 0x06006671 RID: 26225 RVA: 0x002B7F6B File Offset: 0x002B636B
		public virtual void UpdateState(ActorLocomotion.AnimationState state)
		{
		}

		// Token: 0x06006672 RID: 26226 RVA: 0x002B7F6D File Offset: 0x002B636D
		public override Vector3 GetPivotPoint()
		{
			return base.transform.position;
		}

		// Token: 0x06006673 RID: 26227 RVA: 0x002B7F7A File Offset: 0x002B637A
		protected virtual void OnLateUpdate()
		{
			base.Follow();
		}

		// Token: 0x0400584D RID: 22605
		protected Vector3 _lastForward = Vector3.zero;
	}
}
