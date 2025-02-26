using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
	// Token: 0x020000B9 RID: 185
	[TaskDescription("Find a place to hide and move to it using the Unity NavMesh.")]
	[TaskCategory("Movement")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/Movement/documentation.php?id=8")]
	[TaskIcon("Assets/Behavior Designer Movement/Editor/Icons/{SkinColor}CoverIcon.png")]
	public class Cover : NavMeshMovement
	{
		// Token: 0x0600044C RID: 1100 RVA: 0x0001B13C File Offset: 0x0001953C
		public override void OnStart()
		{
			int i = 0;
			Vector3 direction = this.transform.forward;
			float num = 0f;
			this.coverTarget = this.transform.position;
			this.foundCover = false;
			while (i < this.maxRaycasts.Value)
			{
				Ray ray = new Ray(this.transform.position, direction);
				RaycastHit raycastHit;
				if (Physics.Raycast(ray, out raycastHit, this.maxCoverDistance.Value, this.availableLayerCovers.value) && raycastHit.collider.Raycast(new Ray(raycastHit.point - raycastHit.normal * this.maxCoverDistance.Value, raycastHit.normal), out raycastHit, float.PositiveInfinity))
				{
					this.coverPoint = raycastHit.point;
					this.coverTarget = raycastHit.point + raycastHit.normal * this.coverOffset.Value;
					this.foundCover = true;
					break;
				}
				num += this.rayStep.Value;
				direction = Quaternion.Euler(0f, this.transform.eulerAngles.y + num, 0f) * Vector3.forward;
				i++;
			}
			if (this.foundCover)
			{
				this.SetDestination(this.coverTarget);
			}
			base.OnStart();
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0001B2AC File Offset: 0x000196AC
		public override TaskStatus OnUpdate()
		{
			if (!this.foundCover)
			{
				return TaskStatus.Failure;
			}
			if (this.HasArrived())
			{
				Quaternion quaternion = Quaternion.LookRotation(this.coverPoint - this.transform.position);
				if (!this.lookAtCoverPoint.Value || Quaternion.Angle(this.transform.rotation, quaternion) < this.rotationEpsilon.Value)
				{
					return TaskStatus.Success;
				}
				this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, quaternion, this.maxLookAtRotationDelta.Value);
			}
			return TaskStatus.Running;
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0001B348 File Offset: 0x00019748
		public override void OnReset()
		{
			base.OnStart();
			this.maxCoverDistance = 1000f;
			this.maxRaycasts = 100;
			this.rayStep = 1f;
			this.coverOffset = 2f;
			this.lookAtCoverPoint = false;
			this.rotationEpsilon = 0.5f;
		}

		// Token: 0x04000387 RID: 903
		[Tooltip("The distance to search for cover")]
		public SharedFloat maxCoverDistance = 1000f;

		// Token: 0x04000388 RID: 904
		[Tooltip("The layermask of the available cover positions")]
		public LayerMask availableLayerCovers;

		// Token: 0x04000389 RID: 905
		[Tooltip("The maximum number of raycasts that should be fired before the agent gives up looking for an agent to find cover behind")]
		public SharedInt maxRaycasts = 100;

		// Token: 0x0400038A RID: 906
		[Tooltip("How large the step should be between raycasts")]
		public SharedFloat rayStep = 1f;

		// Token: 0x0400038B RID: 907
		[Tooltip("Once a cover point has been found, multiply this offset by the normal to prevent the agent from hugging the wall")]
		public SharedFloat coverOffset = 2f;

		// Token: 0x0400038C RID: 908
		[Tooltip("Should the agent look at the cover point after it has arrived?")]
		public SharedBool lookAtCoverPoint = false;

		// Token: 0x0400038D RID: 909
		[Tooltip("The agent is done rotating to the cover point when the square magnitude is less than this value")]
		public SharedFloat rotationEpsilon = 0.5f;

		// Token: 0x0400038E RID: 910
		[Tooltip("Max rotation delta if lookAtCoverPoint")]
		public SharedFloat maxLookAtRotationDelta;

		// Token: 0x0400038F RID: 911
		private Vector3 coverPoint;

		// Token: 0x04000390 RID: 912
		private Vector3 coverTarget;

		// Token: 0x04000391 RID: 913
		private bool foundCover;
	}
}
