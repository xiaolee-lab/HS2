using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityNavMeshAgent
{
	// Token: 0x020001C1 RID: 449
	[TaskCategory("Unity/NavMeshAgent")]
	[TaskDescription("Apply relative movement to the current position. Returns Success.")]
	public class Move : Action
	{
		// Token: 0x06000878 RID: 2168 RVA: 0x00026708 File Offset: 0x00024B08
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x0002674B File Offset: 0x00024B4B
		public override TaskStatus OnUpdate()
		{
			if (this.navMeshAgent == null)
			{
				return TaskStatus.Failure;
			}
			this.navMeshAgent.Move(this.offset.Value);
			return TaskStatus.Success;
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00026777 File Offset: 0x00024B77
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.offset = Vector3.zero;
		}

		// Token: 0x04000774 RID: 1908
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000775 RID: 1909
		[Tooltip("The relative movement vector")]
		public SharedVector3 offset;

		// Token: 0x04000776 RID: 1910
		private NavMeshAgent navMeshAgent;

		// Token: 0x04000777 RID: 1911
		private GameObject prevGameObject;
	}
}
