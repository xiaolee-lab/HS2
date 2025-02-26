using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
	// Token: 0x020000BE RID: 190
	[TaskDescription("Follow the leader using the Unity NavMesh.")]
	[TaskCategory("Movement")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/Movement/documentation.php?id=14")]
	[TaskIcon("Assets/Behavior Designer Movement/Editor/Icons/{SkinColor}LeaderFollowIcon.png")]
	public class LeaderFollow : NavMeshGroupMovement
	{
		// Token: 0x06000463 RID: 1123 RVA: 0x0001BDAB File Offset: 0x0001A1AB
		public override void OnStart()
		{
			this.leaderTransform = this.leader.Value.transform;
			this.leaderAgent = this.leader.Value.GetComponent<NavMeshAgent>();
			base.OnStart();
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0001BDE0 File Offset: 0x0001A1E0
		public override TaskStatus OnUpdate()
		{
			Vector3 a = this.LeaderBehindPosition();
			for (int i = 0; i < this.agents.Length; i++)
			{
				if (this.LeaderLookingAtAgent(i) && Vector3.Magnitude(this.leaderTransform.position - this.transforms[i].position) < this.aheadDistance.Value)
				{
					this.SetDestination(i, this.transforms[i].position + (this.transforms[i].position - this.leaderTransform.position).normalized * this.aheadDistance.Value);
				}
				else
				{
					this.SetDestination(i, a + this.DetermineSeparation(i));
				}
			}
			return TaskStatus.Running;
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0001BEB8 File Offset: 0x0001A2B8
		private Vector3 LeaderBehindPosition()
		{
			return this.leaderTransform.position + (-this.leaderAgent.velocity).normalized * this.leaderBehindDistance.Value;
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0001BF00 File Offset: 0x0001A300
		private Vector3 DetermineSeparation(int agentIndex)
		{
			Vector3 a = Vector3.zero;
			int num = 0;
			Transform transform = this.transforms[agentIndex];
			for (int i = 0; i < this.agents.Length; i++)
			{
				if (agentIndex != i && Vector3.SqrMagnitude(this.transforms[i].position - transform.position) < this.neighborDistance.Value)
				{
					a += this.transforms[i].position - transform.position;
					num++;
				}
			}
			if (num == 0)
			{
				return Vector3.zero;
			}
			return (a / (float)num * -1f).normalized * this.separationDistance.Value;
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0001BFC5 File Offset: 0x0001A3C5
		public bool LeaderLookingAtAgent(int agentIndex)
		{
			return Vector3.Dot(this.leaderTransform.forward, this.transforms[agentIndex].forward) < -0.5f;
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0001BFEC File Offset: 0x0001A3EC
		public override void OnReset()
		{
			base.OnReset();
			this.neighborDistance = 10f;
			this.leaderBehindDistance = 2f;
			this.separationDistance = 2f;
			this.aheadDistance = 2f;
			this.leader = null;
		}

		// Token: 0x040003A5 RID: 933
		[Tooltip("Agents less than this distance apart are neighbors")]
		public SharedFloat neighborDistance = 10f;

		// Token: 0x040003A6 RID: 934
		[Tooltip("How far behind the leader the agents should follow the leader")]
		public SharedFloat leaderBehindDistance = 2f;

		// Token: 0x040003A7 RID: 935
		[Tooltip("The distance that the agents should be separated")]
		public SharedFloat separationDistance = 2f;

		// Token: 0x040003A8 RID: 936
		[Tooltip("The agent is getting too close to the front of the leader if they are within the aheadDistance")]
		public SharedFloat aheadDistance = 2f;

		// Token: 0x040003A9 RID: 937
		[Tooltip("The leader to follow")]
		public SharedGameObject leader;

		// Token: 0x040003AA RID: 938
		private Transform leaderTransform;

		// Token: 0x040003AB RID: 939
		private NavMeshAgent leaderAgent;
	}
}
