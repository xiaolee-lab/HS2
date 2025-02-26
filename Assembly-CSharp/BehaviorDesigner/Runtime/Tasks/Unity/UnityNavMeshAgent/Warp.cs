using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityNavMeshAgent
{
	// Token: 0x020001CA RID: 458
	[TaskCategory("Unity/NavMeshAgent")]
	[TaskDescription("Warps agent to the provided position. Returns Success.")]
	public class Warp : Action
	{
		// Token: 0x0600089C RID: 2204 RVA: 0x00026BCC File Offset: 0x00024FCC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x00026C0F File Offset: 0x0002500F
		public override TaskStatus OnUpdate()
		{
			if (this.navMeshAgent == null)
			{
				return TaskStatus.Failure;
			}
			this.navMeshAgent.Warp(this.newPosition.Value);
			return TaskStatus.Success;
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x00026C3C File Offset: 0x0002503C
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.newPosition = Vector3.zero;
		}

		// Token: 0x04000795 RID: 1941
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000796 RID: 1942
		[Tooltip("The position to warp to")]
		public SharedVector3 newPosition;

		// Token: 0x04000797 RID: 1943
		private NavMeshAgent navMeshAgent;

		// Token: 0x04000798 RID: 1944
		private GameObject prevGameObject;
	}
}
