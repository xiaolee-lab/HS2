using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityNavMeshAgent
{
	// Token: 0x020001C3 RID: 451
	[TaskCategory("Unity/NavMeshAgent")]
	[TaskDescription("Resumes the movement along the current path after a pause. Returns Success.")]
	public class Resume : Action
	{
		// Token: 0x06000880 RID: 2176 RVA: 0x00026810 File Offset: 0x00024C10
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x00026853 File Offset: 0x00024C53
		public override TaskStatus OnUpdate()
		{
			if (this.navMeshAgent == null)
			{
				return TaskStatus.Failure;
			}
			this.navMeshAgent.isStopped = false;
			return TaskStatus.Success;
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x00026875 File Offset: 0x00024C75
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x0400077B RID: 1915
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400077C RID: 1916
		private NavMeshAgent navMeshAgent;

		// Token: 0x0400077D RID: 1917
		private GameObject prevGameObject;
	}
}
