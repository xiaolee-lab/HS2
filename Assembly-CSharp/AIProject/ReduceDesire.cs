using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D07 RID: 3335
	[TaskCategory("")]
	public class ReduceDesire : AgentAction
	{
		// Token: 0x06006B0B RID: 27403 RVA: 0x002DBF2E File Offset: 0x002DA32E
		public override void OnStart()
		{
			base.OnStart();
			this._desireKey = Desire.GetDesireKey(this._type);
		}

		// Token: 0x06006B0C RID: 27404 RVA: 0x002DBF48 File Offset: 0x002DA348
		public override TaskStatus OnUpdate()
		{
			float? desire = base.Agent.GetDesire(this._desireKey);
			if (desire == null)
			{
				return TaskStatus.Failure;
			}
			if (desire.Value <= 0f)
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Running;
		}

		// Token: 0x04005A54 RID: 23124
		[SerializeField]
		private Desire.Type _type = Desire.Type.Toilet;

		// Token: 0x04005A55 RID: 23125
		private int _desireKey;
	}
}
