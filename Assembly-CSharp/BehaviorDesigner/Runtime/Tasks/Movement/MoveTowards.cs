using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
	// Token: 0x020000BF RID: 191
	[TaskDescription("Move towards the specified position. The position can either be specified by a transform or position. If the transform is used then the position will not be used.")]
	[TaskCategory("Movement")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/Movement/documentation.php?id=1")]
	[TaskIcon("Assets/Behavior Designer Movement/Editor/Icons/{SkinColor}MoveTowardsIcon.png")]
	public class MoveTowards : Action
	{
		// Token: 0x0600046A RID: 1130 RVA: 0x0001C06C File Offset: 0x0001A46C
		public override TaskStatus OnUpdate()
		{
			Vector3 vector = this.Target();
			if (Vector3.Magnitude(this.transform.position - vector) < this.arriveDistance.Value)
			{
				return TaskStatus.Success;
			}
			this.transform.position = Vector3.MoveTowards(this.transform.position, vector, this.speed.Value * Time.deltaTime);
			if (this.lookAtTarget.Value && (vector - this.transform.position).sqrMagnitude > 0.01f)
			{
				this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, Quaternion.LookRotation(vector - this.transform.position), this.maxLookAtRotationDelta.Value);
			}
			return TaskStatus.Running;
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0001C148 File Offset: 0x0001A548
		private Vector3 Target()
		{
			if (this.target == null || this.target.Value == null)
			{
				return this.targetPosition.Value;
			}
			return this.target.Value.transform.position;
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0001C197 File Offset: 0x0001A597
		public override void OnReset()
		{
			this.arriveDistance = 0.1f;
			this.lookAtTarget = true;
		}

		// Token: 0x040003AC RID: 940
		[Tooltip("The speed of the agent")]
		public SharedFloat speed;

		// Token: 0x040003AD RID: 941
		[Tooltip("The agent has arrived when the magnitude is less than this value")]
		public SharedFloat arriveDistance = 0.1f;

		// Token: 0x040003AE RID: 942
		[Tooltip("Should the agent be looking at the target position?")]
		public SharedBool lookAtTarget = true;

		// Token: 0x040003AF RID: 943
		[Tooltip("Max rotation delta if lookAtTarget is enabled")]
		public SharedFloat maxLookAtRotationDelta;

		// Token: 0x040003B0 RID: 944
		[Tooltip("The GameObject that the agent is moving towards")]
		public SharedGameObject target;

		// Token: 0x040003B1 RID: 945
		[Tooltip("If target is null then use the target position")]
		public SharedVector3 targetPosition;
	}
}
