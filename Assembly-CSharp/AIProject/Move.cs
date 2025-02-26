using System;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject
{
	// Token: 0x02000CA2 RID: 3234
	[TaskCategory("")]
	public class Move : AgentStateAction
	{
		// Token: 0x0600693E RID: 26942 RVA: 0x002CC284 File Offset: 0x002CA684
		public override void OnStart()
		{
			AgentActor agent = base.Agent;
			this._prevEventKey = agent.EventKey;
			agent.EventKey = EventType.Move;
			ActionPoint component = base.Agent.NavMeshAgent.currentOffMeshLinkData.offMeshLink.GetComponent<ActionPoint>();
			this._prevTargetPoint = agent.TargetInSightActionPoint;
			agent.TargetInSightActionPoint = component;
			base.OnStart();
		}

		// Token: 0x0600693F RID: 26943 RVA: 0x002CC2E8 File Offset: 0x002CA6E8
		public override void OnEnd()
		{
			base.OnEnd();
			AgentActor agent = base.Agent;
			if (agent.CurrentPoint != null)
			{
				agent.CurrentPoint.RemoveBooking(agent);
				agent.CurrentPoint.SetActiveMapItemObjs(true);
				agent.CurrentPoint.CreateByproduct(agent.ActionID, agent.PoseID);
				agent.CurrentPoint.ReleaseSlot(agent);
				agent.CurrentPoint = null;
				agent.Animation.StopAllAnimCoroutine();
				agent.ActivateTransfer(false);
			}
		}

		// Token: 0x06006940 RID: 26944 RVA: 0x002CC368 File Offset: 0x002CA768
		protected override void Complete()
		{
			base.Complete();
			base.Agent.EventKey = this._prevEventKey;
			base.Agent.TargetInSightActionPoint = this._prevTargetPoint;
		}

		// Token: 0x04005995 RID: 22933
		private EventType _prevEventKey;

		// Token: 0x04005996 RID: 22934
		private ActionPoint _prevTargetPoint;
	}
}
