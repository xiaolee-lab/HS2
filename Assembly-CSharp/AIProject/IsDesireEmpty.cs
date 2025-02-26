using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D53 RID: 3411
	[TaskCategory("")]
	public class IsDesireEmpty : AgentConditional
	{
		// Token: 0x06006C0A RID: 27658 RVA: 0x002E5581 File Offset: 0x002E3981
		public override void OnStart()
		{
			base.OnStart();
			this._desireKey = Desire.GetDesireKey(this._type);
		}

		// Token: 0x06006C0B RID: 27659 RVA: 0x002E559C File Offset: 0x002E399C
		public override TaskStatus OnUpdate()
		{
			float? desire = base.Agent.GetDesire(this._desireKey);
			if (desire == null)
			{
				return TaskStatus.Failure;
			}
			if (desire.Value <= 0f)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}

		// Token: 0x04005ACB RID: 23243
		[SerializeField]
		private Desire.Type _type = Desire.Type.Toilet;

		// Token: 0x04005ACC RID: 23244
		private int _desireKey;
	}
}
