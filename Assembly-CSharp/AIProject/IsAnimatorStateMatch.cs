using System;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D4D RID: 3405
	[TaskCategory("")]
	public class IsAnimatorStateMatch : AgentConditional
	{
		// Token: 0x06006BFC RID: 27644 RVA: 0x002E52F3 File Offset: 0x002E36F3
		public override void OnStart()
		{
			base.OnStart();
			this._stateNameHash = Animator.StringToHash(this._stateName);
		}

		// Token: 0x06006BFD RID: 27645 RVA: 0x002E530C File Offset: 0x002E370C
		public override TaskStatus OnUpdate()
		{
			if (base.Agent.Animation.Animator.GetCurrentAnimatorStateInfo(0).shortNameHash == this._stateNameHash)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}

		// Token: 0x04005AC3 RID: 23235
		[SerializeField]
		public string _stateName = string.Empty;

		// Token: 0x04005AC4 RID: 23236
		private int _stateNameHash = -1;
	}
}
