using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityNavMeshAgent
{
	// Token: 0x020001BA RID: 442
	[TaskCategory("Unity/NavMeshAgent")]
	[TaskDescription("Gets the maximum acceleration of an agent as it follows a path, given in units / sec^2.. Returns Success.")]
	public class GetAcceleration : Action
	{
		// Token: 0x0600085C RID: 2140 RVA: 0x00026330 File Offset: 0x00024730
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x00026373 File Offset: 0x00024773
		public override TaskStatus OnUpdate()
		{
			if (this.navMeshAgent == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.navMeshAgent.acceleration;
			return TaskStatus.Success;
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x0002639F File Offset: 0x0002479F
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x04000759 RID: 1881
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400075A RID: 1882
		[SharedRequired]
		[Tooltip("The NavMeshAgent acceleration")]
		public SharedFloat storeValue;

		// Token: 0x0400075B RID: 1883
		private NavMeshAgent navMeshAgent;

		// Token: 0x0400075C RID: 1884
		private GameObject prevGameObject;
	}
}
