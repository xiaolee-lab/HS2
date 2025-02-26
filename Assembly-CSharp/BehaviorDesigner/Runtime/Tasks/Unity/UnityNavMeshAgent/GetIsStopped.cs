using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityNavMeshAgent
{
	// Token: 0x020001BD RID: 445
	[TaskCategory("Unity/NavMeshAgent")]
	[TaskDescription("Gets the stop status. Returns Success.")]
	public class GetIsStopped : Action
	{
		// Token: 0x06000868 RID: 2152 RVA: 0x000264E0 File Offset: 0x000248E0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x00026523 File Offset: 0x00024923
		public override TaskStatus OnUpdate()
		{
			if (this.navMeshAgent == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.navMeshAgent.isStopped;
			return TaskStatus.Success;
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x0002654F File Offset: 0x0002494F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = null;
		}

		// Token: 0x04000765 RID: 1893
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000766 RID: 1894
		[SharedRequired]
		[Tooltip("The stop status")]
		public SharedBool storeValue;

		// Token: 0x04000767 RID: 1895
		private NavMeshAgent navMeshAgent;

		// Token: 0x04000768 RID: 1896
		private GameObject prevGameObject;
	}
}
