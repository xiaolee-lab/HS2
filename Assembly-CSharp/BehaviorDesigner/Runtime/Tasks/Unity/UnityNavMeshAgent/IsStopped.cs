using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityNavMeshAgent
{
	// Token: 0x020001C0 RID: 448
	[TaskCategory("Unity/NavMeshAgent")]
	[TaskDescription("Is the agent stopped?")]
	public class IsStopped : Conditional
	{
		// Token: 0x06000874 RID: 2164 RVA: 0x00026688 File Offset: 0x00024A88
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x000266CB File Offset: 0x00024ACB
		public override TaskStatus OnUpdate()
		{
			if (this.navMeshAgent == null)
			{
				return TaskStatus.Failure;
			}
			return (!this.navMeshAgent.isStopped) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x000266F7 File Offset: 0x00024AF7
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04000771 RID: 1905
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000772 RID: 1906
		private NavMeshAgent navMeshAgent;

		// Token: 0x04000773 RID: 1907
		private GameObject prevGameObject;
	}
}
