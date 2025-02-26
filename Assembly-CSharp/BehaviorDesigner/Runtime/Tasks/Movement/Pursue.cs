using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
	// Token: 0x020000C3 RID: 195
	[TaskDescription("Pursue the target specified using the Unity NavMesh.")]
	[TaskCategory("Movement")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/Movement/documentation.php?id=5")]
	[TaskIcon("Assets/Behavior Designer Movement/Editor/Icons/{SkinColor}PursueIcon.png")]
	public class Pursue : NavMeshMovement
	{
		// Token: 0x06000487 RID: 1159 RVA: 0x0001C425 File Offset: 0x0001A825
		public override void OnStart()
		{
			base.OnStart();
			this.targetPosition = this.target.Value.transform.position;
			this.SetDestination(this.Target());
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0001C455 File Offset: 0x0001A855
		public override TaskStatus OnUpdate()
		{
			if (this.HasArrived())
			{
				return TaskStatus.Success;
			}
			this.SetDestination(this.Target());
			return TaskStatus.Running;
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0001C474 File Offset: 0x0001A874
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
			return this.targetPosition + (this.targetPosition - b) * d;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0001C53D File Offset: 0x0001A93D
		public override void OnReset()
		{
			base.OnReset();
			this.targetDistPrediction = 20f;
			this.targetDistPredictionMult = 20f;
			this.target = null;
		}

		// Token: 0x040003C2 RID: 962
		[Tooltip("How far to predict the distance ahead of the target. Lower values indicate less distance should be predicated")]
		public SharedFloat targetDistPrediction = 20f;

		// Token: 0x040003C3 RID: 963
		[Tooltip("Multiplier for predicting the look ahead distance")]
		public SharedFloat targetDistPredictionMult = 20f;

		// Token: 0x040003C4 RID: 964
		[Tooltip("The GameObject that the agent is pursuing")]
		public SharedGameObject target;

		// Token: 0x040003C5 RID: 965
		private Vector3 targetPosition;
	}
}
