using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
	// Token: 0x020000C5 RID: 197
	[TaskDescription("Rotates towards the specified rotation. The rotation can either be specified by a transform or rotation. If the transform is used then the rotation will not be used.")]
	[TaskCategory("Movement")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/Movement/documentation.php?id=2")]
	[TaskIcon("Assets/Behavior Designer Movement/Editor/Icons/{SkinColor}RotateTowardsIcon.png")]
	public class RotateTowards : Action
	{
		// Token: 0x06000491 RID: 1169 RVA: 0x0001C82C File Offset: 0x0001AC2C
		public override TaskStatus OnUpdate()
		{
			Quaternion quaternion = this.Target();
			if (Quaternion.Angle(this.transform.rotation, quaternion) < this.rotationEpsilon.Value)
			{
				return TaskStatus.Success;
			}
			this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, quaternion, this.maxLookAtRotationDelta.Value);
			return TaskStatus.Running;
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0001C88C File Offset: 0x0001AC8C
		private Quaternion Target()
		{
			if (this.target == null || this.target.Value == null)
			{
				return Quaternion.Euler(this.targetRotation.Value);
			}
			Vector3 forward = this.target.Value.transform.position - this.transform.position;
			if (this.onlyY.Value)
			{
				forward.y = 0f;
			}
			if (this.usePhysics2D)
			{
				float angle = Mathf.Atan2(forward.y, forward.x) * 57.29578f;
				return Quaternion.AngleAxis(angle, Vector3.forward);
			}
			return Quaternion.LookRotation(forward);
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0001C944 File Offset: 0x0001AD44
		public override void OnReset()
		{
			this.usePhysics2D = false;
			this.rotationEpsilon = 0.5f;
			this.maxLookAtRotationDelta = 1f;
			this.onlyY = false;
			this.target = null;
			this.targetRotation = Vector3.zero;
		}

		// Token: 0x040003CC RID: 972
		[Tooltip("Should the 2D version be used?")]
		public bool usePhysics2D;

		// Token: 0x040003CD RID: 973
		[Tooltip("The agent is done rotating when the angle is less than this value")]
		public SharedFloat rotationEpsilon = 0.5f;

		// Token: 0x040003CE RID: 974
		[Tooltip("The maximum number of angles the agent can rotate in a single tick")]
		public SharedFloat maxLookAtRotationDelta = 1f;

		// Token: 0x040003CF RID: 975
		[Tooltip("Should the rotation only affect the Y axis?")]
		public SharedBool onlyY;

		// Token: 0x040003D0 RID: 976
		[Tooltip("The GameObject that the agent is rotating towards")]
		public SharedGameObject target;

		// Token: 0x040003D1 RID: 977
		[Tooltip("If target is null then use the target rotation")]
		public SharedVector3 targetRotation;
	}
}
