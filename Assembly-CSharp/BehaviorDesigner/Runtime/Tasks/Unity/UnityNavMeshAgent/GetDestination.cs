using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityNavMeshAgent
{
	// Token: 0x020001BC RID: 444
	[TaskCategory("Unity/NavMeshAgent")]
	[TaskDescription("Gets the destination of the agent in world-space units. Returns Success.")]
	public class GetDestination : Action
	{
		// Token: 0x06000864 RID: 2148 RVA: 0x00026450 File Offset: 0x00024850
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x00026493 File Offset: 0x00024893
		public override TaskStatus OnUpdate()
		{
			if (this.navMeshAgent == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.navMeshAgent.destination;
			return TaskStatus.Success;
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x000264BF File Offset: 0x000248BF
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = Vector3.zero;
		}

		// Token: 0x04000761 RID: 1889
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000762 RID: 1890
		[SharedRequired]
		[Tooltip("The NavMeshAgent destination")]
		public SharedVector3 storeValue;

		// Token: 0x04000763 RID: 1891
		private NavMeshAgent navMeshAgent;

		// Token: 0x04000764 RID: 1892
		private GameObject prevGameObject;
	}
}
