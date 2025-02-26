using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityNavMeshAgent
{
	// Token: 0x020001C2 RID: 450
	[TaskCategory("Unity/NavMeshAgent")]
	[TaskDescription("Clears the current path. Returns Success.")]
	public class ResetPath : Action
	{
		// Token: 0x0600087C RID: 2172 RVA: 0x00026798 File Offset: 0x00024B98
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x000267DB File Offset: 0x00024BDB
		public override TaskStatus OnUpdate()
		{
			if (this.navMeshAgent == null)
			{
				return TaskStatus.Failure;
			}
			this.navMeshAgent.ResetPath();
			return TaskStatus.Success;
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x000267FC File Offset: 0x00024BFC
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04000778 RID: 1912
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000779 RID: 1913
		private NavMeshAgent navMeshAgent;

		// Token: 0x0400077A RID: 1914
		private GameObject prevGameObject;
	}
}
