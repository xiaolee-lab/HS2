using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DD3 RID: 3539
	[TaskCategory("商人")]
	public class IsMatchEventType : MerchantConditional
	{
		// Token: 0x06006D94 RID: 28052 RVA: 0x002EAC27 File Offset: 0x002E9027
		public override void OnAwake()
		{
			base.OnAwake();
			if (this.navMeshPath == null)
			{
				this.navMeshPath = new NavMeshPath();
			}
		}

		// Token: 0x06006D95 RID: 28053 RVA: 0x002EAC48 File Offset: 0x002E9048
		public override void OnStart()
		{
			if ((this.targetPoint = base.TargetInSightMerchantPoint) == null)
			{
				this.targetPoint = base.CurrentMerchantPoint;
			}
			this.mainTargetPoint = base.MainTargetInsightMerchantPoint;
		}

		// Token: 0x06006D96 RID: 28054 RVA: 0x002EAC88 File Offset: 0x002E9088
		public override TaskStatus OnUpdate()
		{
			if (this.targetPoint == null)
			{
				return TaskStatus.Failure;
			}
			MerchantPoint merchantPoint = (!(this.mainTargetPoint != null)) ? this.targetPoint : this.mainTargetPoint;
			if (base.Merchant.OpenAreaID < merchantPoint.AreaID)
			{
				return TaskStatus.Failure;
			}
			Merchant.EventType eventType = merchantPoint.EventType;
			if (!NavMesh.CalculatePath(base.Merchant.Position, this.targetPoint.Destination, base.Merchant.NavMeshAgent.areaMask, this.navMeshPath) || this.navMeshPath.status != NavMeshPathStatus.PathComplete)
			{
				return TaskStatus.Failure;
			}
			return ((eventType & this._matchType) != this._matchType) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x04005B43 RID: 23363
		[SerializeField]
		private Merchant.EventType _matchType;

		// Token: 0x04005B44 RID: 23364
		private MerchantPoint targetPoint;

		// Token: 0x04005B45 RID: 23365
		private MerchantPoint mainTargetPoint;

		// Token: 0x04005B46 RID: 23366
		private NavMeshPath navMeshPath;
	}
}
