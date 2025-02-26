using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityNavMeshAgent
{
	// Token: 0x020001C5 RID: 453
	[TaskCategory("Unity/NavMeshAgent")]
	[TaskDescription("Sets the maximum turning speed in (deg/s) while following a path. Returns Success.")]
	public class SetAngularSpeed : Action
	{
		// Token: 0x06000888 RID: 2184 RVA: 0x00026918 File Offset: 0x00024D18
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0002695B File Offset: 0x00024D5B
		public override TaskStatus OnUpdate()
		{
			if (this.navMeshAgent == null)
			{
				return TaskStatus.Failure;
			}
			this.navMeshAgent.angularSpeed = this.angularSpeed.Value;
			return TaskStatus.Success;
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00026987 File Offset: 0x00024D87
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.angularSpeed = 0f;
		}

		// Token: 0x04000782 RID: 1922
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000783 RID: 1923
		[Tooltip("The NavMeshAgent angular speed")]
		public SharedFloat angularSpeed;

		// Token: 0x04000784 RID: 1924
		private NavMeshAgent navMeshAgent;

		// Token: 0x04000785 RID: 1925
		private GameObject prevGameObject;
	}
}
