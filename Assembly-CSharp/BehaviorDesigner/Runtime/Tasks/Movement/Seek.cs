using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
	// Token: 0x020000C7 RID: 199
	[TaskDescription("Seek the target specified using the Unity NavMesh.")]
	[TaskCategory("Movement")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/Movement/documentation.php?id=3")]
	[TaskIcon("Assets/Behavior Designer Movement/Editor/Icons/{SkinColor}SeekIcon.png")]
	public class Seek : NavMeshMovement
	{
		// Token: 0x06000499 RID: 1177 RVA: 0x0001CD6B File Offset: 0x0001B16B
		public override void OnStart()
		{
			base.OnStart();
			this.SetDestination(this.Target());
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0001CD80 File Offset: 0x0001B180
		public override TaskStatus OnUpdate()
		{
			if (this.HasArrived())
			{
				return TaskStatus.Success;
			}
			this.SetDestination(this.Target());
			return TaskStatus.Running;
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0001CD9D File Offset: 0x0001B19D
		private Vector3 Target()
		{
			if (this.target.Value != null)
			{
				return this.target.Value.transform.position;
			}
			return this.targetPosition.Value;
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0001CDD6 File Offset: 0x0001B1D6
		public override void OnReset()
		{
			base.OnReset();
			this.target = null;
			this.targetPosition = Vector3.zero;
		}

		// Token: 0x040003E6 RID: 998
		[Tooltip("The GameObject that the agent is seeking")]
		public SharedGameObject target;

		// Token: 0x040003E7 RID: 999
		[Tooltip("If target is null then use the target position")]
		public SharedVector3 targetPosition;
	}
}
