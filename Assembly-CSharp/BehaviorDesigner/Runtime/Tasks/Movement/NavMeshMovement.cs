using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
	// Token: 0x020000C1 RID: 193
	public abstract class NavMeshMovement : Movement
	{
		// Token: 0x06000474 RID: 1140 RVA: 0x0001AEFB File Offset: 0x000192FB
		public override void OnAwake()
		{
			this.navMeshAgent = base.GetComponent<NavMeshAgent>();
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0001AF0C File Offset: 0x0001930C
		public override void OnStart()
		{
			this.navMeshAgent.speed = this.speed.Value;
			this.navMeshAgent.angularSpeed = this.angularSpeed.Value;
			this.navMeshAgent.isStopped = false;
			if (!this.updateRotation.Value)
			{
				this.UpdateRotation(true);
			}
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0001AF68 File Offset: 0x00019368
		protected override bool SetDestination(Vector3 destination)
		{
			this.navMeshAgent.isStopped = false;
			return this.navMeshAgent.SetDestination(destination);
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0001AF82 File Offset: 0x00019382
		protected override void UpdateRotation(bool update)
		{
			this.navMeshAgent.updateRotation = update;
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0001AF90 File Offset: 0x00019390
		protected override bool HasPath()
		{
			return this.navMeshAgent.hasPath && this.navMeshAgent.remainingDistance > this.arriveDistance.Value;
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0001AFBD File Offset: 0x000193BD
		protected override Vector3 Velocity()
		{
			return this.navMeshAgent.velocity;
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0001AFCC File Offset: 0x000193CC
		protected bool SamplePosition(Vector3 position)
		{
			NavMeshHit navMeshHit;
			return NavMesh.SamplePosition(position, out navMeshHit, float.MaxValue, -1);
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0001AFE8 File Offset: 0x000193E8
		protected override bool HasArrived()
		{
			float num;
			if (this.navMeshAgent.pathPending)
			{
				num = float.PositiveInfinity;
			}
			else
			{
				num = this.navMeshAgent.remainingDistance;
			}
			return num <= this.arriveDistance.Value;
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0001B02D File Offset: 0x0001942D
		protected override void Stop()
		{
			this.UpdateRotation(this.updateRotation.Value);
			if (this.navMeshAgent.hasPath)
			{
				this.navMeshAgent.isStopped = true;
			}
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x0001B05C File Offset: 0x0001945C
		public override void OnEnd()
		{
			if (this.stopOnTaskEnd.Value)
			{
				this.Stop();
			}
			else
			{
				this.UpdateRotation(this.updateRotation.Value);
			}
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0001B08A File Offset: 0x0001948A
		public override void OnBehaviorComplete()
		{
			this.Stop();
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0001B092 File Offset: 0x00019492
		public override void OnReset()
		{
			this.speed = 10f;
			this.angularSpeed = 120f;
			this.arriveDistance = 1f;
			this.stopOnTaskEnd = true;
		}

		// Token: 0x040003B7 RID: 951
		[Tooltip("The speed of the agent")]
		public SharedFloat speed = 10f;

		// Token: 0x040003B8 RID: 952
		[Tooltip("The angular speed of the agent")]
		public SharedFloat angularSpeed = 120f;

		// Token: 0x040003B9 RID: 953
		[Tooltip("The agent has arrived when the destination is less than the specified amount")]
		public SharedFloat arriveDistance = 0.2f;

		// Token: 0x040003BA RID: 954
		[Tooltip("Should the NavMeshAgent be stopped when the task ends?")]
		public SharedBool stopOnTaskEnd = true;

		// Token: 0x040003BB RID: 955
		[Tooltip("Should the NavMeshAgent rotation be updated when the task ends?")]
		public SharedBool updateRotation = true;

		// Token: 0x040003BC RID: 956
		protected NavMeshAgent navMeshAgent;
	}
}
