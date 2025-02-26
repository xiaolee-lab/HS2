using System;
using System.Runtime.CompilerServices;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DC7 RID: 3527
	[TaskCategory("商人")]
	public class Walk : MerchantAction
	{
		// Token: 0x1700153E RID: 5438
		// (get) Token: 0x06006D6F RID: 28015 RVA: 0x002EA719 File Offset: 0x002E8B19
		private NavMeshAgent Agent
		{
			[CompilerGenerated]
			get
			{
				return base.Merchant.NavMeshAgent;
			}
		}

		// Token: 0x1700153F RID: 5439
		// (get) Token: 0x06006D70 RID: 28016 RVA: 0x002EA726 File Offset: 0x002E8B26
		private bool AgentActive
		{
			[CompilerGenerated]
			get
			{
				return this.Agent.isActiveAndEnabled && this.Agent.isOnNavMesh;
			}
		}

		// Token: 0x06006D71 RID: 28017 RVA: 0x002EA746 File Offset: 0x002E8B46
		public override void OnStart()
		{
			base.OnStart();
			this._prevActiveEncount = base.Merchant.ActiveEncount;
			if (!this._prevActiveEncount)
			{
				base.Merchant.ActiveEncount = true;
			}
			this.StartWalk();
			this._walkActive = true;
		}

		// Token: 0x06006D72 RID: 28018 RVA: 0x002EA783 File Offset: 0x002E8B83
		private void StartWalk()
		{
			if (base.TargetInSightMerchantPoint == null)
			{
				return;
			}
			base.Merchant.StartLocomotion(base.TargetInSightMerchantPoint.Destination);
		}

		// Token: 0x06006D73 RID: 28019 RVA: 0x002EA7AD File Offset: 0x002E8BAD
		private void StopWalk()
		{
			if (!this.AgentActive)
			{
				return;
			}
			base.Merchant.StopNavMeshAgent();
		}

		// Token: 0x06006D74 RID: 28020 RVA: 0x002EA7C6 File Offset: 0x002E8BC6
		public override void OnPause(bool paused)
		{
			if (!this._walkActive)
			{
				return;
			}
			base.OnPause(paused);
			if (paused)
			{
				this.StopWalk();
			}
			else
			{
				this.StartWalk();
			}
		}

		// Token: 0x06006D75 RID: 28021 RVA: 0x002EA7F4 File Offset: 0x002E8BF4
		public override TaskStatus OnUpdate()
		{
			if (base.TargetInSightMerchantPoint == null)
			{
				return TaskStatus.Failure;
			}
			if (this.AgentActive && !base.Merchant.NavMeshAgent.isStopped && !base.Merchant.NavMeshAgent.pathPending)
			{
				base.Merchant.NavMeshAgent.SetDestination(base.TargetInSightMerchantPoint.Destination);
			}
			return TaskStatus.Running;
		}

		// Token: 0x06006D76 RID: 28022 RVA: 0x002EA866 File Offset: 0x002E8C66
		public override void OnEnd()
		{
			this._walkActive = false;
			this.StopWalk();
			if (!this._prevActiveEncount)
			{
				base.Merchant.ActiveEncount = false;
			}
			base.OnEnd();
		}

		// Token: 0x06006D77 RID: 28023 RVA: 0x002EA892 File Offset: 0x002E8C92
		public override void OnBehaviorComplete()
		{
			this._walkActive = false;
			base.OnBehaviorComplete();
		}

		// Token: 0x04005B3E RID: 23358
		private bool _prevActiveEncount;

		// Token: 0x04005B3F RID: 23359
		private bool _walkActive;
	}
}
