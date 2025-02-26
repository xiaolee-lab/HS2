using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
	// Token: 0x020000C0 RID: 192
	public abstract class NavMeshGroupMovement : GroupMovement
	{
		// Token: 0x0600046E RID: 1134 RVA: 0x0001B7B8 File Offset: 0x00019BB8
		public override void OnStart()
		{
			this.navMeshAgents = new NavMeshAgent[this.agents.Length];
			this.transforms = new Transform[this.agents.Length];
			for (int i = 0; i < this.agents.Length; i++)
			{
				this.transforms[i] = this.agents[i].Value.transform;
				this.navMeshAgents[i] = this.agents[i].Value.GetComponent<NavMeshAgent>();
				this.navMeshAgents[i].speed = this.speed.Value;
				this.navMeshAgents[i].angularSpeed = this.angularSpeed.Value;
				this.navMeshAgents[i].isStopped = false;
			}
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0001B876 File Offset: 0x00019C76
		protected override bool SetDestination(int index, Vector3 target)
		{
			return this.navMeshAgents[index].destination == target || this.navMeshAgents[index].SetDestination(target);
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0001B8A0 File Offset: 0x00019CA0
		protected override Vector3 Velocity(int index)
		{
			return this.navMeshAgents[index].velocity;
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0001B8B0 File Offset: 0x00019CB0
		public override void OnEnd()
		{
			for (int i = 0; i < this.navMeshAgents.Length; i++)
			{
				if (this.navMeshAgents[i] != null)
				{
					this.navMeshAgents[i].isStopped = true;
				}
			}
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0001B8F7 File Offset: 0x00019CF7
		public override void OnReset()
		{
			this.agents = null;
		}

		// Token: 0x040003B2 RID: 946
		[Tooltip("All of the agents")]
		public SharedGameObject[] agents;

		// Token: 0x040003B3 RID: 947
		[Tooltip("The speed of the agents")]
		public SharedFloat speed = 10f;

		// Token: 0x040003B4 RID: 948
		[Tooltip("The angular speed of the agents")]
		public SharedFloat angularSpeed = 120f;

		// Token: 0x040003B5 RID: 949
		private NavMeshAgent[] navMeshAgents;

		// Token: 0x040003B6 RID: 950
		protected Transform[] transforms;
	}
}
