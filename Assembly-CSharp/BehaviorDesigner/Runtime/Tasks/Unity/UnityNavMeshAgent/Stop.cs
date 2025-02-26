using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityNavMeshAgent
{
	// Token: 0x020001C9 RID: 457
	[TaskCategory("Unity/NavMeshAgent")]
	[TaskDescription("Stop movement of this agent along its current path. Returns Success.")]
	public class Stop : Action
	{
		// Token: 0x06000898 RID: 2200 RVA: 0x00026B54 File Offset: 0x00024F54
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x00026B97 File Offset: 0x00024F97
		public override TaskStatus OnUpdate()
		{
			if (this.navMeshAgent == null)
			{
				return TaskStatus.Failure;
			}
			this.navMeshAgent.isStopped = true;
			return TaskStatus.Success;
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x00026BB9 File Offset: 0x00024FB9
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04000792 RID: 1938
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000793 RID: 1939
		private NavMeshAgent navMeshAgent;

		// Token: 0x04000794 RID: 1940
		private GameObject prevGameObject;
	}
}
