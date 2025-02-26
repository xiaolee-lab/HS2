using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
	// Token: 0x020000BB RID: 187
	[TaskDescription("Flee from the target specified using the Unity NavMesh.")]
	[TaskCategory("Movement")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/Movement/documentation.php?id=4")]
	[TaskIcon("Assets/Behavior Designer Movement/Editor/Icons/{SkinColor}FleeIcon.png")]
	public class Flee : NavMeshMovement
	{
		// Token: 0x06000455 RID: 1109 RVA: 0x0001B61A File Offset: 0x00019A1A
		public override void OnStart()
		{
			base.OnStart();
			this.hasMoved = false;
			this.SetDestination(this.Target());
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0001B638 File Offset: 0x00019A38
		public override TaskStatus OnUpdate()
		{
			if (Vector3.Magnitude(this.transform.position - this.target.Value.transform.position) > this.fleedDistance.Value)
			{
				return TaskStatus.Success;
			}
			if (this.HasArrived())
			{
				if (!this.hasMoved)
				{
					return TaskStatus.Failure;
				}
				if (!this.SetDestination(this.Target()))
				{
					return TaskStatus.Failure;
				}
				this.hasMoved = false;
			}
			else
			{
				float sqrMagnitude = this.Velocity().sqrMagnitude;
				if (this.hasMoved && sqrMagnitude <= 0f)
				{
					return TaskStatus.Failure;
				}
				this.hasMoved = (sqrMagnitude > 0f);
			}
			return TaskStatus.Running;
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0001B6F0 File Offset: 0x00019AF0
		private Vector3 Target()
		{
			return this.transform.position + (this.transform.position - this.target.Value.transform.position).normalized * this.lookAheadDistance.Value;
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0001B74A File Offset: 0x00019B4A
		protected override bool SetDestination(Vector3 destination)
		{
			return base.SamplePosition(destination) && base.SetDestination(destination);
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0001B761 File Offset: 0x00019B61
		public override void OnReset()
		{
			base.OnReset();
			this.fleedDistance = 20f;
			this.lookAheadDistance = 5f;
			this.target = null;
		}

		// Token: 0x04000398 RID: 920
		[Tooltip("The agent has fleed when the magnitude is greater than this value")]
		public SharedFloat fleedDistance = 20f;

		// Token: 0x04000399 RID: 921
		[Tooltip("The distance to look ahead when fleeing")]
		public SharedFloat lookAheadDistance = 5f;

		// Token: 0x0400039A RID: 922
		[Tooltip("The GameObject that the agent is fleeing from")]
		public SharedGameObject target;

		// Token: 0x0400039B RID: 923
		private bool hasMoved;
	}
}
