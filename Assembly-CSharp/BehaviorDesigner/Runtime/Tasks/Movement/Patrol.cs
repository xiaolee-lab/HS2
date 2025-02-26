using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
	// Token: 0x020000C2 RID: 194
	[TaskDescription("Patrol around the specified waypoints using the Unity NavMesh.")]
	[TaskCategory("Movement")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/Movement/documentation.php?id=7")]
	[TaskIcon("Assets/Behavior Designer Movement/Editor/Icons/{SkinColor}PatrolIcon.png")]
	public class Patrol : NavMeshMovement
	{
		// Token: 0x06000481 RID: 1153 RVA: 0x0001C1DC File Offset: 0x0001A5DC
		public override void OnStart()
		{
			base.OnStart();
			float num = float.PositiveInfinity;
			for (int i = 0; i < this.waypoints.Value.Count; i++)
			{
				float num2;
				if ((num2 = Vector3.Magnitude(this.transform.position - this.waypoints.Value[i].transform.position)) < num)
				{
					num = num2;
					this.waypointIndex = i;
				}
			}
			this.waypointReachedTime = -1f;
			this.SetDestination(this.Target());
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0001C270 File Offset: 0x0001A670
		public override TaskStatus OnUpdate()
		{
			if (this.waypoints.Value.Count == 0)
			{
				return TaskStatus.Failure;
			}
			if (this.HasArrived())
			{
				if (this.waypointReachedTime == -1f)
				{
					this.waypointReachedTime = Time.time;
				}
				if (this.waypointReachedTime + this.waypointPauseDuration.Value <= Time.time)
				{
					if (this.randomPatrol.Value)
					{
						if (this.waypoints.Value.Count == 1)
						{
							this.waypointIndex = 0;
						}
						else
						{
							int num;
							for (num = this.waypointIndex; num == this.waypointIndex; num = UnityEngine.Random.Range(0, this.waypoints.Value.Count))
							{
							}
							this.waypointIndex = num;
						}
					}
					else
					{
						this.waypointIndex = (this.waypointIndex + 1) % this.waypoints.Value.Count;
					}
					this.SetDestination(this.Target());
					this.waypointReachedTime = -1f;
				}
			}
			return TaskStatus.Running;
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0001C37C File Offset: 0x0001A77C
		private Vector3 Target()
		{
			if (this.waypointIndex >= this.waypoints.Value.Count)
			{
				return this.transform.position;
			}
			return this.waypoints.Value[this.waypointIndex].transform.position;
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0001C3D0 File Offset: 0x0001A7D0
		public override void OnReset()
		{
			base.OnReset();
			this.randomPatrol = false;
			this.waypointPauseDuration = 0f;
			this.waypoints = null;
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0001C3FB File Offset: 0x0001A7FB
		public override void OnDrawGizmos()
		{
		}

		// Token: 0x040003BD RID: 957
		[Tooltip("Should the agent patrol the waypoints randomly?")]
		public SharedBool randomPatrol = false;

		// Token: 0x040003BE RID: 958
		[Tooltip("The length of time that the agent should pause when arriving at a waypoint")]
		public SharedFloat waypointPauseDuration = 0f;

		// Token: 0x040003BF RID: 959
		[Tooltip("The waypoints to move to")]
		public SharedGameObjectList waypoints;

		// Token: 0x040003C0 RID: 960
		private int waypointIndex;

		// Token: 0x040003C1 RID: 961
		private float waypointReachedTime;
	}
}
