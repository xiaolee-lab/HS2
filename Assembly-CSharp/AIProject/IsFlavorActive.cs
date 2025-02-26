using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D59 RID: 3417
	[TaskCategory("")]
	public class IsFlavorActive : AgentConditional
	{
		// Token: 0x06006C17 RID: 27671 RVA: 0x002E5794 File Offset: 0x002E3B94
		public override TaskStatus OnUpdate()
		{
			bool flag = false;
			int num;
			if (!base.Agent.ChaControl.fileGameInfo.flavorState.TryGetValue((int)this._target, out num))
			{
				return TaskStatus.Failure;
			}
			if (this._compareDowner)
			{
				flag |= (num <= this._borderLevel);
			}
			else
			{
				flag |= (num >= this._borderLevel);
			}
			if (flag)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}

		// Token: 0x04005ACF RID: 23247
		[SerializeField]
		private int _borderLevel = 1;

		// Token: 0x04005AD0 RID: 23248
		[SerializeField]
		private bool _compareDowner;

		// Token: 0x04005AD1 RID: 23249
		[SerializeField]
		private FlavorSkill.Type _target;
	}
}
