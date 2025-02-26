using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
	// Token: 0x020000BA RID: 186
	[TaskDescription("Evade the target specified using the Unity NavMesh.")]
	[TaskCategory("Movement")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/Movement/documentation.php?id=6")]
	[TaskIcon("Assets/Behavior Designer Movement/Editor/Icons/{SkinColor}EvadeIcon.png")]
	public class Evade : NavMeshMovement
	{
		// Token: 0x06000450 RID: 1104 RVA: 0x0001B407 File Offset: 0x00019807
		public override void OnStart()
		{
			base.OnStart();
			this.targetPosition = this.target.Value.transform.position;
			this.SetDestination(this.Target());
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0001B438 File Offset: 0x00019838
		public override TaskStatus OnUpdate()
		{
			if (Vector3.Magnitude(this.transform.position - this.target.Value.transform.position) > this.evadeDistance.Value)
			{
				return TaskStatus.Success;
			}
			this.SetDestination(this.Target());
			return TaskStatus.Running;
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0001B490 File Offset: 0x00019890
		private Vector3 Target()
		{
			float magnitude = (this.target.Value.transform.position - this.transform.position).magnitude;
			float magnitude2 = this.Velocity().magnitude;
			float d;
			if (magnitude2 <= magnitude / this.targetDistPrediction.Value)
			{
				d = this.targetDistPrediction.Value;
			}
			else
			{
				d = magnitude / magnitude2 * this.targetDistPredictionMult.Value;
			}
			Vector3 b = this.targetPosition;
			this.targetPosition = this.target.Value.transform.position;
			Vector3 b2 = this.targetPosition + (this.targetPosition - b) * d;
			return this.transform.position + (this.transform.position - b2).normalized * this.lookAheadDistance.Value;
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0001B598 File Offset: 0x00019998
		public override void OnReset()
		{
			base.OnReset();
			this.evadeDistance = 10f;
			this.lookAheadDistance = 5f;
			this.targetDistPrediction = 20f;
			this.targetDistPredictionMult = 20f;
			this.target = null;
		}

		// Token: 0x04000392 RID: 914
		[Tooltip("The agent has evaded when the magnitude is greater than this value")]
		public SharedFloat evadeDistance = 10f;

		// Token: 0x04000393 RID: 915
		[Tooltip("The distance to look ahead when evading")]
		public SharedFloat lookAheadDistance = 5f;

		// Token: 0x04000394 RID: 916
		[Tooltip("How far to predict the distance ahead of the target. Lower values indicate less distance should be predicated")]
		public SharedFloat targetDistPrediction = 20f;

		// Token: 0x04000395 RID: 917
		[Tooltip("Multiplier for predicting the look ahead distance")]
		public SharedFloat targetDistPredictionMult = 20f;

		// Token: 0x04000396 RID: 918
		[Tooltip("The GameObject that the agent is evading")]
		public SharedGameObject target;

		// Token: 0x04000397 RID: 919
		private Vector3 targetPosition;
	}
}
