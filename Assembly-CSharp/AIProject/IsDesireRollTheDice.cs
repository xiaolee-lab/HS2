using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D54 RID: 3412
	[TaskCategory("")]
	public class IsDesireRollTheDice : AgentConditional
	{
		// Token: 0x06006C0D RID: 27661 RVA: 0x002E55EC File Offset: 0x002E39EC
		public override TaskStatus OnUpdate()
		{
			int desireKey = Desire.GetDesireKey(this._key);
			float num = (float)Singleton<Manager.Resources>.Instance.GetDesireBorder(desireKey).Item2;
			float? desire = base.Agent.GetDesire(desireKey);
			if (desire == null)
			{
				return TaskStatus.Failure;
			}
			float num2 = UnityEngine.Random.Range(0f, num);
			if (!this._compareDowner)
			{
				if (Mathf.Approximately(desire.Value, 0f))
				{
					return TaskStatus.Failure;
				}
				return (num2 > desire.Value) ? TaskStatus.Failure : TaskStatus.Success;
			}
			else
			{
				if (Mathf.Approximately(desire.Value, num))
				{
					return TaskStatus.Failure;
				}
				return (desire.Value > num2) ? TaskStatus.Failure : TaskStatus.Success;
			}
		}

		// Token: 0x04005ACD RID: 23245
		[SerializeField]
		private Desire.Type _key = Desire.Type.Toilet;

		// Token: 0x04005ACE RID: 23246
		[SerializeField]
		private bool _compareDowner;
	}
}
