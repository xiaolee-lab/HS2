using System;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine.AI;

namespace AIProject
{
	// Token: 0x02000CA3 RID: 3235
	[TaskCategory("")]
	public class OpenDoor : AgentStateAction
	{
		// Token: 0x06006942 RID: 26946 RVA: 0x002CC39C File Offset: 0x002CA79C
		public override void OnStart()
		{
			this._unchangeParamState = true;
			AgentActor agent = base.Agent;
			this._prevEventKey = agent.EventKey;
			agent.EventKey = EventType.DoorOpen;
			OffMeshLinkData currentOffMeshLinkData = agent.NavMeshAgent.currentOffMeshLinkData;
			DoorPoint component = currentOffMeshLinkData.offMeshLink.GetComponent<DoorPoint>();
			this._prevTargetPoint = agent.TargetInSightActionPoint;
			this._isDoorOpen = (!currentOffMeshLinkData.activated || component == null || component.IsOpen);
			if (this._isDoorOpen)
			{
				agent.EventKey = this._prevEventKey;
				return;
			}
			agent.TargetInSightActionPoint = component;
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

		// Token: 0x06006943 RID: 26947 RVA: 0x002CC4EC File Offset: 0x002CA8EC
		public override TaskStatus OnUpdate()
		{
			if (this._isDoorOpen)
			{
				NavMeshAgent navMeshAgent = base.Agent.NavMeshAgent;
				if (navMeshAgent != null && navMeshAgent.isActiveAndEnabled)
				{
					navMeshAgent.ResetPath();
				}
				return TaskStatus.Success;
			}
			return base.OnUpdate();
		}

		// Token: 0x06006944 RID: 26948 RVA: 0x002CC535 File Offset: 0x002CA935
		protected override void Complete()
		{
			base.Complete();
			base.Agent.EventKey = this._prevEventKey;
			base.Agent.TargetInSightActionPoint = this._prevTargetPoint;
		}

		// Token: 0x06006945 RID: 26949 RVA: 0x002CC560 File Offset: 0x002CA960
		protected override void OnCompletedStateTask()
		{
			DoorPoint doorPoint = base.Agent.CurrentPoint as DoorPoint;
			if (doorPoint == null)
			{
				return;
			}
			if (doorPoint.OpenType == DoorPoint.OpenTypeState.Right || doorPoint.OpenType == DoorPoint.OpenTypeState.Right90)
			{
				doorPoint.SetOpenState(DoorPoint.OpenPattern.OpenRight, true);
			}
			else
			{
				doorPoint.SetOpenState(DoorPoint.OpenPattern.OpenLeft, true);
			}
		}

		// Token: 0x04005997 RID: 22935
		private EventType _prevEventKey;

		// Token: 0x04005998 RID: 22936
		private ActionPoint _prevTargetPoint;

		// Token: 0x04005999 RID: 22937
		private bool _isDoorOpen;
	}
}
