using System;
using BehaviorDesigner.Runtime.Tasks;
using Manager;

namespace AIProject
{
	// Token: 0x02000CF2 RID: 3314
	[TaskCategory("")]
	public class TutorialOpenDoor : AgentTutorialMoveAction
	{
		// Token: 0x06006AC1 RID: 27329 RVA: 0x002D986C File Offset: 0x002D7C6C
		public override void OnStart()
		{
			AgentActor agent = base.Agent;
			agent.EventKey = EventType.DoorOpen;
			DoorPoint component = agent.NavMeshAgent.currentOffMeshLinkData.offMeshLink.GetComponent<DoorPoint>();
			agent.TargetInSightActionPoint = component;
			this._actionMotion = new PoseKeyPair
			{
				postureID = -1,
				poseID = -1
			};
			base.OnStart();
			if (agent.CurrentPoint != null)
			{
				DoorAnimation component2 = agent.CurrentPoint.GetComponent<DoorAnimation>();
				if (component2 != null)
				{
					ActionPointInfo actionPointInfo = agent.Animation.ActionPointInfo;
					PlayState playState = Singleton<Resources>.Instance.Animation.AgentActionAnimTable[actionPointInfo.eventID][actionPointInfo.poseID];
					component2.Load(playState.MainStateInfo.InStateInfo.StateInfos);
					ActorAnimInfo animInfo = agent.Animation.AnimInfo;
					component2.PlayAnimation(animInfo.inEnableBlend, animInfo.inBlendSec, playState.MainStateInfo.FadeOutTime, animInfo.layer);
				}
			}
		}

		// Token: 0x06006AC2 RID: 27330 RVA: 0x002D9980 File Offset: 0x002D7D80
		protected override void OnCompletedStateTask()
		{
			DoorPoint doorPoint = base.Agent.CurrentPoint as DoorPoint;
			if (doorPoint.OpenType == DoorPoint.OpenTypeState.Right || doorPoint.OpenType == DoorPoint.OpenTypeState.Right90)
			{
				doorPoint.SetOpenState(DoorPoint.OpenPattern.OpenRight, true);
			}
			else
			{
				doorPoint.SetOpenState(DoorPoint.OpenPattern.OpenLeft, true);
			}
		}
	}
}
