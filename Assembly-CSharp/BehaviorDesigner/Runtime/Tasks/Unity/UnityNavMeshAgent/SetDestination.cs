using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityNavMeshAgent
{
	// Token: 0x020001C6 RID: 454
	[TaskCategory("Unity/NavMeshAgent")]
	[TaskDescription("Sets the destination of the agent in world-space units. Returns Success if the destination is valid.")]
	public class SetDestination : Action
	{
		// Token: 0x0600088C RID: 2188 RVA: 0x000269A8 File Offset: 0x00024DA8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x000269EB File Offset: 0x00024DEB
		public override TaskStatus OnUpdate()
		{
			if (this.navMeshAgent == null)
			{
				return TaskStatus.Failure;
			}
			return (!this.navMeshAgent.SetDestination(this.destination.Value)) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x00026A22 File Offset: 0x00024E22
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.destination = Vector3.zero;
		}

		// Token: 0x04000786 RID: 1926
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000787 RID: 1927
		[SharedRequired]
		[Tooltip("The NavMeshAgent destination")]
		public SharedVector3 destination;

		// Token: 0x04000788 RID: 1928
		private NavMeshAgent navMeshAgent;

		// Token: 0x04000789 RID: 1929
		private GameObject prevGameObject;
	}
}
