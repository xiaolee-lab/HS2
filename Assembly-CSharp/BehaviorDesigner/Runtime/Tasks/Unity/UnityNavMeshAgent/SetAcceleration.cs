using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityNavMeshAgent
{
	// Token: 0x020001C4 RID: 452
	[TaskCategory("Unity/NavMeshAgent")]
	[TaskDescription("Sets the maximum acceleration of an agent as it follows a path, given in units / sec^2. Returns Success.")]
	public class SetAcceleration : Action
	{
		// Token: 0x06000884 RID: 2180 RVA: 0x00026888 File Offset: 0x00024C88
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x000268CB File Offset: 0x00024CCB
		public override TaskStatus OnUpdate()
		{
			if (this.navMeshAgent == null)
			{
				return TaskStatus.Failure;
			}
			this.navMeshAgent.acceleration = this.acceleration.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x000268F7 File Offset: 0x00024CF7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.acceleration = 0f;
		}

		// Token: 0x0400077E RID: 1918
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400077F RID: 1919
		[Tooltip("The NavMeshAgent acceleration")]
		public SharedFloat acceleration;

		// Token: 0x04000780 RID: 1920
		private NavMeshAgent navMeshAgent;

		// Token: 0x04000781 RID: 1921
		private GameObject prevGameObject;
	}
}
