using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
	// Token: 0x020000BD RID: 189
	[TaskDescription("Follows the specified target using the Unity NavMesh.")]
	[TaskCategory("Movement")]
	[HelpURL("http://www.opsive.com/assets/BehaviorDesigner/Movement/documentation.php?id=23")]
	[TaskIcon("Assets/Behavior Designer Movement/Editor/Icons/{SkinColor}FollowIcon.png")]
	public class Follow : NavMeshMovement
	{
		// Token: 0x0600045F RID: 1119 RVA: 0x0001BC1C File Offset: 0x0001A01C
		public override void OnStart()
		{
			base.OnStart();
			this.lastTargetPosition = this.target.Value.transform.position + Vector3.one * (this.moveDistance.Value + 1f);
			this.hasMoved = false;
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0001BC74 File Offset: 0x0001A074
		public override TaskStatus OnUpdate()
		{
			if (this.target.Value == null)
			{
				return TaskStatus.Failure;
			}
			Vector3 position = this.target.Value.transform.position;
			if ((position - this.lastTargetPosition).magnitude >= this.moveDistance.Value)
			{
				this.SetDestination(position);
				this.lastTargetPosition = position;
				this.hasMoved = true;
			}
			else if (this.hasMoved && (position - this.transform.position).magnitude < this.moveDistance.Value)
			{
				this.Stop();
				this.hasMoved = false;
				this.lastTargetPosition = position;
			}
			return TaskStatus.Running;
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0001BD37 File Offset: 0x0001A137
		public override void OnReset()
		{
			base.OnReset();
			this.target = null;
			this.moveDistance = 2f;
		}

		// Token: 0x040003A1 RID: 929
		[Tooltip("The GameObject that the agent is following")]
		public SharedGameObject target;

		// Token: 0x040003A2 RID: 930
		[Tooltip("Start moving towards the target if the target is further than the specified distance")]
		public SharedFloat moveDistance = 2f;

		// Token: 0x040003A3 RID: 931
		private Vector3 lastTargetPosition;

		// Token: 0x040003A4 RID: 932
		private bool hasMoved;
	}
}
