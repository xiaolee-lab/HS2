using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine;
using UnityEx;

namespace AIProject
{
	// Token: 0x02000D51 RID: 3409
	[TaskCategory("")]
	public class IsDesireActive : AgentConditional
	{
		// Token: 0x06006C05 RID: 27653 RVA: 0x002E541C File Offset: 0x002E381C
		public override void OnStart()
		{
			base.OnStart();
			int desireKey = Desire.GetDesireKey(this._key);
			UnityEx.ValueTuple<int, int> desireBorder = Singleton<Manager.Resources>.Instance.GetDesireBorder(desireKey);
			if (this._checkLimit)
			{
				this._migrationBorder = desireBorder.Item2;
			}
			else
			{
				this._migrationBorder = desireBorder.Item1;
			}
		}

		// Token: 0x06006C06 RID: 27654 RVA: 0x002E5474 File Offset: 0x002E3874
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			int desireKey = Desire.GetDesireKey(this._key);
			float? desire = agent.GetDesire(desireKey);
			if (desire == null)
			{
				return TaskStatus.Failure;
			}
			if (desire.Value >= (float)this._migrationBorder)
			{
				float? motivation = agent.GetMotivation(desireKey);
				if (motivation != null)
				{
					if (motivation.Value < Singleton<Manager.Resources>.Instance.AgentProfile.ActiveMotivationBorder)
					{
						agent.SetDesire(desireKey, 0f);
						return TaskStatus.Failure;
					}
					return TaskStatus.Success;
				}
			}
			return TaskStatus.Failure;
		}

		// Token: 0x04005AC6 RID: 23238
		[SerializeField]
		private Desire.Type _key = Desire.Type.Hunt;

		// Token: 0x04005AC7 RID: 23239
		private int _migrationBorder;

		// Token: 0x04005AC8 RID: 23240
		[SerializeField]
		private bool _checkLimit;
	}
}
