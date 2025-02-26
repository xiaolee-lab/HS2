using System;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000C90 RID: 3216
	[TaskCategory("")]
	public class CrossFadeAndWaitFinish : AgentAnimatorAction
	{
		// Token: 0x06006901 RID: 26881 RVA: 0x002CA6A4 File Offset: 0x002C8AA4
		public override TaskStatus OnUpdate()
		{
			if (this._animator == null)
			{
				return TaskStatus.Failure;
			}
			if (!this._fadesSameState && this._animator.GetCurrentAnimatorStateInfo(this._layer).IsName(this._stateName.Value))
			{
				return TaskStatus.Success;
			}
			this._animator.CrossFade(this._stateName.Value, this._transitionDuration.Value, this._layer, this._normalizedTime);
			return TaskStatus.Success;
		}

		// Token: 0x06006902 RID: 26882 RVA: 0x002CA728 File Offset: 0x002C8B28
		public override void OnReset()
		{
		}

		// Token: 0x04005989 RID: 22921
		[SerializeField]
		private SharedString _stateName;

		// Token: 0x0400598A RID: 22922
		[SerializeField]
		private SharedFloat _transitionDuration = 0f;

		// Token: 0x0400598B RID: 22923
		[SerializeField]
		private int _layer;

		// Token: 0x0400598C RID: 22924
		[SerializeField]
		private float _normalizedTime;

		// Token: 0x0400598D RID: 22925
		[SerializeField]
		private bool _fadesSameState;
	}
}
