using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D5A RID: 3418
	[TaskCategory("")]
	public class IsFlavorGreater : AgentConditional
	{
		// Token: 0x06006C19 RID: 27673 RVA: 0x002E5818 File Offset: 0x002E3C18
		public override TaskStatus OnUpdate()
		{
			int num;
			if (!base.Agent.ChaControl.fileGameInfo.flavorState.TryGetValue((int)this._key, out num))
			{
				return TaskStatus.Failure;
			}
			if (!this._compareDowner)
			{
				return (this._borderValue > (float)num) ? TaskStatus.Failure : TaskStatus.Success;
			}
			return ((float)num > this._borderValue) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x04005AD2 RID: 23250
		[SerializeField]
		private float _borderValue = 50f;

		// Token: 0x04005AD3 RID: 23251
		[SerializeField]
		private FlavorSkill.Type _key;

		// Token: 0x04005AD4 RID: 23252
		[SerializeField]
		private bool _compareDowner;
	}
}
