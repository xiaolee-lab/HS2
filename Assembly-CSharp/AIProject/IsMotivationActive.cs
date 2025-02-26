using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000D62 RID: 3426
	[TaskCategory("")]
	public class IsMotivationActive : AgentConditional
	{
		// Token: 0x06006C29 RID: 27689 RVA: 0x002E5B90 File Offset: 0x002E3F90
		public override TaskStatus OnUpdate()
		{
			int desireKey = Desire.GetDesireKey(this._key);
			float? motivation = base.Agent.GetMotivation(desireKey);
			if (motivation == null)
			{
				return TaskStatus.Failure;
			}
			if (motivation.Value >= Singleton<Manager.Resources>.Instance.AgentProfile.ActiveMotivationBorder)
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}

		// Token: 0x04005ADA RID: 23258
		[SerializeField]
		private Desire.Type _key;
	}
}
