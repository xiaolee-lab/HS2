using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
	// Token: 0x020000C6 RID: 198
	[TaskDescription("Search for a target by combining the wander, within hearing range, and the within seeing range tasks using the Unity NavMesh.")]
	[TaskCategory("Movement")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/Movement/documentation.php?id=10")]
	[TaskIcon("Assets/Behavior Designer Movement/Editor/Icons/{SkinColor}SearchIcon.png")]
	public class Search : NavMeshMovement
	{
		// Token: 0x06000495 RID: 1173 RVA: 0x0001CA74 File Offset: 0x0001AE74
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
			this.returnedObject.Value = MovementUtility.WithinSight(this.transform, this.offset.Value, this.fieldOfViewAngle.Value, this.viewDistance.Value, this.objectLayerMask, this.targetOffset.Value, this.ignoreLayerMask, this.useTargetBone.Value, this.targetBone);
			if (this.returnedObject.Value != null)
			{
				return TaskStatus.Success;
			}
			if (this.senseAudio.Value)
			{
				this.returnedObject.Value = MovementUtility.WithinHearingRange(this.transform, this.offset.Value, this.audibilityThreshold.Value, this.hearingRadius.Value, this.objectLayerMask);
				if (this.returnedObject.Value != null)
				{
					return TaskStatus.Success;
				}
			}
			return TaskStatus.Running;
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0001CBF4 File Offset: 0x0001AFF4
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

		// Token: 0x06000497 RID: 1175 RVA: 0x0001CCA8 File Offset: 0x0001B0A8
		public override void OnReset()
		{
			base.OnReset();
			this.minWanderDistance = 20f;
			this.maxWanderDistance = 20f;
			this.wanderRate = 2f;
			this.minPauseDuration = 0f;
			this.maxPauseDuration = 0f;
			this.targetRetries = 1;
			this.fieldOfViewAngle = 90f;
			this.viewDistance = 30f;
			this.senseAudio = true;
			this.hearingRadius = 30f;
			this.audibilityThreshold = 0.05f;
		}

		// Token: 0x040003D2 RID: 978
		[Tooltip("Minimum distance ahead of the current position to look ahead for a destination")]
		public SharedFloat minWanderDistance = 20f;

		// Token: 0x040003D3 RID: 979
		[Tooltip("Maximum distance ahead of the current position to look ahead for a destination")]
		public SharedFloat maxWanderDistance = 20f;

		// Token: 0x040003D4 RID: 980
		[Tooltip("The amount that the agent rotates direction")]
		public SharedFloat wanderRate = 1f;

		// Token: 0x040003D5 RID: 981
		[Tooltip("The minimum length of time that the agent should pause at each destination")]
		public SharedFloat minPauseDuration = 0f;

		// Token: 0x040003D6 RID: 982
		[Tooltip("The maximum length of time that the agent should pause at each destination (zero to disable)")]
		public SharedFloat maxPauseDuration = 0f;

		// Token: 0x040003D7 RID: 983
		[Tooltip("The maximum number of retries per tick (set higher if using a slow tick time)")]
		public SharedInt targetRetries = 1;

		// Token: 0x040003D8 RID: 984
		[Tooltip("The field of view angle of the agent (in degrees)")]
		public SharedFloat fieldOfViewAngle = 90f;

		// Token: 0x040003D9 RID: 985
		[Tooltip("The distance that the agent can see")]
		public SharedFloat viewDistance = 30f;

		// Token: 0x040003DA RID: 986
		[Tooltip("The LayerMask of the objects to ignore when performing the line of sight check")]
		public LayerMask ignoreLayerMask = 1 << LayerMask.NameToLayer("Ignore Raycast");

		// Token: 0x040003DB RID: 987
		[Tooltip("Should the search end if audio was heard?")]
		public SharedBool senseAudio = true;

		// Token: 0x040003DC RID: 988
		[Tooltip("How far away the unit can hear")]
		public SharedFloat hearingRadius = 30f;

		// Token: 0x040003DD RID: 989
		[Tooltip("The raycast offset relative to the pivot position")]
		public SharedVector3 offset;

		// Token: 0x040003DE RID: 990
		[Tooltip("The target raycast offset relative to the pivot position")]
		public SharedVector3 targetOffset;

		// Token: 0x040003DF RID: 991
		[Tooltip("The LayerMask of the objects that we are searching for")]
		public LayerMask objectLayerMask;

		// Token: 0x040003E0 RID: 992
		[Tooltip("Should the target bone be used?")]
		public SharedBool useTargetBone;

		// Token: 0x040003E1 RID: 993
		[Tooltip("The target's bone if the target is a humanoid")]
		public HumanBodyBones targetBone;

		// Token: 0x040003E2 RID: 994
		[Tooltip("The further away a sound source is the less likely the agent will be able to hear it. Set a threshold for the the minimum audibility level that the agent can hear")]
		public SharedFloat audibilityThreshold = 0.05f;

		// Token: 0x040003E3 RID: 995
		[Tooltip("The object that is found")]
		public SharedGameObject returnedObject;

		// Token: 0x040003E4 RID: 996
		private float pauseTime;

		// Token: 0x040003E5 RID: 997
		private float destinationReachTime;
	}
}
