using System;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D96 RID: 3478
	public abstract class AgentAnimatorAction : AgentAction
	{
		// Token: 0x06006C96 RID: 27798 RVA: 0x002CA66E File Offset: 0x002C8A6E
		public override void OnStart()
		{
			base.OnStart();
			this._animator = base.Agent.Animation.Animator;
		}

		// Token: 0x04005AED RID: 23277
		protected Animator _animator;
	}
}
