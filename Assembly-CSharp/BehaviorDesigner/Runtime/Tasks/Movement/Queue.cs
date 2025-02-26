using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
	// Token: 0x020000C4 RID: 196
	[TaskDescription("Queue in a line using the Unity NavMesh.")]
	[TaskCategory("Movement")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/Movement/documentation.php?id=15")]
	[TaskIcon("Assets/Behavior Designer Movement/Editor/Icons/{SkinColor}QueueIcon.png")]
	public class Queue : NavMeshGroupMovement
	{
		// Token: 0x0600048C RID: 1164 RVA: 0x0001C5D0 File Offset: 0x0001A9D0
		public override TaskStatus OnUpdate()
		{
			for (int i = 0; i < this.agents.Length; i++)
			{
				if (this.AgentAhead(i))
				{
					this.SetDestination(i, this.transforms[i].position + this.transforms[i].forward * this.slowDownSpeed.Value + this.DetermineSeparation(i));
				}
				else
				{
					this.SetDestination(i, this.target.Value.transform.position);
				}
			}
			return TaskStatus.Running;
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0001C668 File Offset: 0x0001AA68
		private bool AgentAhead(int index)
		{
			Vector3 a = this.Velocity(index) * this.maxQueueAheadDistance.Value;
			for (int i = 0; i < this.agents.Length; i++)
			{
				if (index != i && Vector3.SqrMagnitude(a - this.transforms[i].position) < this.maxQueueRadius.Value)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0001C6D8 File Offset: 0x0001AAD8
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

		// Token: 0x0600048F RID: 1167 RVA: 0x0001C7A0 File Offset: 0x0001ABA0
		public override void OnReset()
		{
			base.OnReset();
			this.neighborDistance = 10f;
			this.separationDistance = 2f;
			this.maxQueueAheadDistance = 2f;
			this.maxQueueRadius = 20f;
			this.slowDownSpeed = 0.15f;
		}

		// Token: 0x040003C6 RID: 966
		[Tooltip("Agents less than this distance apart are neighbors")]
		public SharedFloat neighborDistance = 10f;

		// Token: 0x040003C7 RID: 967
		[Tooltip("The distance that the agents should be separated")]
		public SharedFloat separationDistance = 2f;

		// Token: 0x040003C8 RID: 968
		[Tooltip("The distance the the agent should look ahead to see if another agent is in the way")]
		public SharedFloat maxQueueAheadDistance = 2f;

		// Token: 0x040003C9 RID: 969
		[Tooltip("The radius that the agent should check to see if another agent is in the way")]
		public SharedFloat maxQueueRadius = 20f;

		// Token: 0x040003CA RID: 970
		[Tooltip("The multiplier to slow down if an agent is in front of the current agent")]
		public SharedFloat slowDownSpeed = 0.15f;

		// Token: 0x040003CB RID: 971
		[Tooltip("The target to seek towards")]
		public SharedGameObject target;
	}
}
