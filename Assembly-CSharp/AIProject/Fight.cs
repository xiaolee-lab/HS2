using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000CC0 RID: 3264
	[TaskCategory("")]
	public class Fight : AgentAction
	{
		// Token: 0x060069D0 RID: 27088 RVA: 0x002D0EA2 File Offset: 0x002CF2A2
		public override void OnStart()
		{
			base.OnStart();
			base.Agent.RuntimeDesire = Desire.Type.Lonely;
			base.Agent.StartTalkSequence(base.Agent.CommandPartner);
		}

		// Token: 0x060069D1 RID: 27089 RVA: 0x002D0ED0 File Offset: 0x002CF2D0
		public override TaskStatus OnUpdate()
		{
			if (base.Agent.LivesTalkSequence)
			{
				return TaskStatus.Running;
			}
			int desireKey = Desire.GetDesireKey(base.Agent.RuntimeDesire);
			if (desireKey != -1)
			{
				base.Agent.SetDesire(desireKey, 0f);
			}
			base.Agent.RuntimeDesire = Desire.Type.None;
			return TaskStatus.Success;
		}
	}
}
