using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityNavMeshAgent
{
	// Token: 0x020001BE RID: 446
	[TaskCategory("Unity/NavMeshAgent")]
	[TaskDescription("Gets the distance between the agent's position and the destination on the current path. Returns Success.")]
	public class GetRemainingDistance : Action
	{
		// Token: 0x0600086C RID: 2156 RVA: 0x00026568 File Offset: 0x00024968
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x000265AB File Offset: 0x000249AB
		public override TaskStatus OnUpdate()
		{
			if (this.navMeshAgent == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.navMeshAgent.remainingDistance;
			return TaskStatus.Success;
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x000265D7 File Offset: 0x000249D7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04000769 RID: 1897
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400076A RID: 1898
		[SharedRequired]
		[Tooltip("The remaining distance")]
		public SharedFloat storeValue;

		// Token: 0x0400076B RID: 1899
		private NavMeshAgent navMeshAgent;

		// Token: 0x0400076C RID: 1900
		private GameObject prevGameObject;
	}
}
