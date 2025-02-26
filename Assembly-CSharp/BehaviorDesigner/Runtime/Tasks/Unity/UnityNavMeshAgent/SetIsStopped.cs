using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityNavMeshAgent
{
	// Token: 0x020001C7 RID: 455
	[TaskCategory("Unity/NavMeshAgent")]
	[TaskDescription("Sets the stop status. Returns Success.")]
	public class SetIsStopped : Action
	{
		// Token: 0x06000890 RID: 2192 RVA: 0x00026A44 File Offset: 0x00024E44
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x00026A87 File Offset: 0x00024E87
		public override TaskStatus OnUpdate()
		{
			if (this.navMeshAgent == null)
			{
				return TaskStatus.Failure;
			}
			this.navMeshAgent.isStopped = this.isStopped.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x00026AB3 File Offset: 0x00024EB3
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x0400078A RID: 1930
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400078B RID: 1931
		[Tooltip("The stop status")]
		public SharedBool isStopped;

		// Token: 0x0400078C RID: 1932
		private NavMeshAgent navMeshAgent;

		// Token: 0x0400078D RID: 1933
		private GameObject prevGameObject;
	}
}
