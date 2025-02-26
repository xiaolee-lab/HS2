using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityNavMeshAgent
{
	// Token: 0x020001BB RID: 443
	[TaskCategory("Unity/NavMeshAgent")]
	[TaskDescription("Gets the maximum turning speed in (deg/s) while following a path.. Returns Success.")]
	public class GetAngularSpeed : Action
	{
		// Token: 0x06000860 RID: 2144 RVA: 0x000263C0 File Offset: 0x000247C0
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x00026403 File Offset: 0x00024803
		public override TaskStatus OnUpdate()
		{
			if (this.navMeshAgent == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.navMeshAgent.angularSpeed;
			return TaskStatus.Success;
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x0002642F File Offset: 0x0002482F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x0400075D RID: 1885
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400075E RID: 1886
		[SharedRequired]
		[Tooltip("The NavMeshAgent angular speed")]
		public SharedFloat storeValue;

		// Token: 0x0400075F RID: 1887
		private NavMeshAgent navMeshAgent;

		// Token: 0x04000760 RID: 1888
		private GameObject prevGameObject;
	}
}
