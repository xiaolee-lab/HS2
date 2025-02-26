using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
	// Token: 0x020000C8 RID: 200
	[TaskDescription("Wander using the Unity NavMesh.")]
	[TaskCategory("Movement")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/Movement/documentation.php?id=9")]
	[TaskIcon("Assets/Behavior Designer Movement/Editor/Icons/{SkinColor}WanderIcon.png")]
	public class Wander : NavMeshMovement
	{
		// Token: 0x0600049E RID: 1182 RVA: 0x0001CE68 File Offset: 0x0001B268
		public override TaskStatus OnUpdate()
		{
			if (this.HasArrived())
			{
				if (this.maxPauseDuration.Value > 0f)
				{
					if (this.destinationReachTime == -1f)
					{
						this.destinationReachTime = Time.time;
						this.pauseTime = UnityEngine.Random.Range(this.minPauseDuration.Value, this.maxPauseDuration.Value);
					}
					if (this.destinationReachTime + this.pauseTime <= Time.time && this.TrySetTarget())
					{
						this.destinationReachTime = -1f;
					}
				}
				else
				{
					this.TrySetTarget();
				}
			}
			return TaskStatus.Running;
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0001CF0C File Offset: 0x0001B30C
		private bool TrySetTarget()
		{
			Vector3 a = this.transform.forward;
			bool flag = false;
			int num = this.targetRetries.Value;
			Vector3 vector = this.transform.position;
			while (!flag && num > 0)
			{
				a += UnityEngine.Random.insideUnitSphere * this.wanderRate.Value;
				vector = this.transform.position + a.normalized * UnityEngine.Random.Range(this.minWanderDistance.Value, this.maxWanderDistance.Value);
				flag = base.SamplePosition(vector);
				num--;
			}
			if (flag)
			{
				this.SetDestination(vector);
			}
			return flag;
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0001CFC0 File Offset: 0x0001B3C0
		public override void OnReset()
		{
			this.minWanderDistance = 20f;
			this.maxWanderDistance = 20f;
			this.wanderRate = 2f;
			this.minPauseDuration = 0f;
			this.maxPauseDuration = 0f;
			this.targetRetries = 1;
		}

		// Token: 0x040003E8 RID: 1000
		[Tooltip("Minimum distance ahead of the current position to look ahead for a destination")]
		public SharedFloat minWanderDistance = 20f;

		// Token: 0x040003E9 RID: 1001
		[Tooltip("Maximum distance ahead of the current position to look ahead for a destination")]
		public SharedFloat maxWanderDistance = 20f;

		// Token: 0x040003EA RID: 1002
		[Tooltip("The amount that the agent rotates direction")]
		public SharedFloat wanderRate = 2f;

		// Token: 0x040003EB RID: 1003
		[Tooltip("The minimum length of time that the agent should pause at each destination")]
		public SharedFloat minPauseDuration = 0f;

		// Token: 0x040003EC RID: 1004
		[Tooltip("The maximum length of time that the agent should pause at each destination (zero to disable)")]
		public SharedFloat maxPauseDuration = 0f;

		// Token: 0x040003ED RID: 1005
		[Tooltip("The maximum number of retries per tick (set higher if using a slow tick time)")]
		public SharedInt targetRetries = 1;

		// Token: 0x040003EE RID: 1006
		private float pauseTime;

		// Token: 0x040003EF RID: 1007
		private float destinationReachTime;
	}
}
