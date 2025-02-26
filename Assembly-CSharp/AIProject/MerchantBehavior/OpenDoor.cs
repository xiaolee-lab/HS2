using System;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UnityEngine.AI;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DBD RID: 3517
	[TaskCategory("商人")]
	public class OpenDoor : MerchantMoveAction
	{
		// Token: 0x06006D55 RID: 27989 RVA: 0x002E8C2C File Offset: 0x002E702C
		public override void OnStart()
		{
			this.prevEventKey = base.Merchant.EventKey;
			base.Merchant.EventKey = EventType.DoorOpen;
			OffMeshLinkData currentOffMeshLinkData = base.Merchant.NavMeshAgent.currentOffMeshLinkData;
			DoorPoint component = currentOffMeshLinkData.offMeshLink.GetComponent<DoorPoint>();
			this.isDoorOpen = (!currentOffMeshLinkData.activated || component == null || component.IsOpen);
			base.CurrentPoint = ((!this.isDoorOpen) ? component : null);
			if (this.isDoorOpen)
			{
				base.Merchant.EventKey = this.prevEventKey;
			}
			base.OnStart();
			if (base.CurrentPoint != null)
			{
				DoorAnimation component2 = base.CurrentPoint.GetComponent<DoorAnimation>();
				if (component2 != null)
				{
					ActionPointInfo actionPointInfo;
					base.CurrentPoint.TryGetAgentActionPointInfo(EventType.DoorOpen, out actionPointInfo);
					PlayState playState = Singleton<Resources>.Instance.Animation.MerchantCommonActionAnimStateTable[actionPointInfo.eventID][actionPointInfo.poseID];
					component2.Load(playState.MainStateInfo.InStateInfo.StateInfos);
					component2.PlayAnimation(this.animInfo.inEnableBlend, this.animInfo.inBlendSec, this.animInfo.inFadeOutTime, this.animInfo.layer);
				}
			}
		}

		// Token: 0x06006D56 RID: 27990 RVA: 0x002E8D88 File Offset: 0x002E7188
		public override TaskStatus OnUpdate()
		{
			if (this.isDoorOpen)
			{
				NavMeshAgent navMeshAgent = base.Merchant.NavMeshAgent;
				if (navMeshAgent != null && navMeshAgent.isActiveAndEnabled)
				{
					navMeshAgent.ResetPath();
				}
				return TaskStatus.Success;
			}
			return base.OnUpdate();
		}

		// Token: 0x06006D57 RID: 27991 RVA: 0x002E8DD4 File Offset: 0x002E71D4
		protected override void OnCompletedStateTask()
		{
			if (base.CurrentPoint == null)
			{
				return;
			}
			DoorPoint doorPoint = base.CurrentPoint as DoorPoint;
			if (doorPoint.OpenType == DoorPoint.OpenTypeState.Right)
			{
				doorPoint.SetOpenState(DoorPoint.OpenPattern.OpenRight, true);
			}
			else
			{
				doorPoint.SetOpenState(DoorPoint.OpenPattern.OpenLeft, true);
			}
		}

		// Token: 0x04005B3A RID: 23354
		private EventType prevEventKey;

		// Token: 0x04005B3B RID: 23355
		private bool isDoorOpen;
	}
}
