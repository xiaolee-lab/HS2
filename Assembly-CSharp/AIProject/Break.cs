using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000C99 RID: 3225
	[TaskCategory("")]
	public class Break : AgentStateAction
	{
		// Token: 0x0600691B RID: 26907 RVA: 0x002CB382 File Offset: 0x002C9782
		public override void OnStart()
		{
			base.Agent.EventKey = EventType.Break;
			base.OnStart();
		}

		// Token: 0x0600691C RID: 26908 RVA: 0x002CB398 File Offset: 0x002C9798
		protected override void OnCompletedStateTask()
		{
			if (this._unchangeParamState)
			{
				return;
			}
			AgentActor agent = base.Agent;
			int desireKey = Desire.GetDesireKey(Desire.Type.Break);
			agent.SetDesire(desireKey, 0f);
			if (UnityEngine.Random.Range(0, 20) < 1)
			{
				agent.AgentData.SickState.ID = -1;
			}
		}
	}
}
