using System;
using BehaviorDesigner.Runtime.Tasks;
using Manager;

namespace AIProject
{
	// Token: 0x02000CCC RID: 3276
	[TaskCategory("")]
	public class StartH : AgentAction
	{
		// Token: 0x060069FE RID: 27134 RVA: 0x002D2258 File Offset: 0x002D0658
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			PlayerActor player = Singleton<Map>.Instance.Player;
			if (agent.FromFemale)
			{
				if (player.ChaControl.sex == 1 && !player.ChaControl.fileParam.futanari)
				{
					Singleton<HSceneManager>.Instance.nInvitePtn = 0;
					agent.InitiateHScene(HSceneManager.HEvent.Normal);
				}
				else
				{
					agent.InitiateHScene(HSceneManager.HEvent.FromFemale);
				}
			}
			else
			{
				Singleton<HSceneManager>.Instance.nInvitePtn = 0;
				agent.InitiateHScene(HSceneManager.HEvent.Normal);
			}
			return TaskStatus.Success;
		}
	}
}
