using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityNavMeshAgent
{
	// Token: 0x020001C8 RID: 456
	[TaskCategory("Unity/NavMeshAgent")]
	[TaskDescription("Sets the maximum movement speed when following a path. Returns Success.")]
	public class SetSpeed : Action
	{
		// Token: 0x06000894 RID: 2196 RVA: 0x00026AC4 File Offset: 0x00024EC4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x00026B07 File Offset: 0x00024F07
		public override TaskStatus OnUpdate()
		{
			if (this.navMeshAgent == null)
			{
				return TaskStatus.Failure;
			}
			this.navMeshAgent.speed = this.speed.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x00026B33 File Offset: 0x00024F33
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.speed = 0f;
		}

		// Token: 0x0400078E RID: 1934
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400078F RID: 1935
		[Tooltip("The NavMeshAgent speed")]
		public SharedFloat speed;

		// Token: 0x04000790 RID: 1936
		private NavMeshAgent navMeshAgent;

		// Token: 0x04000791 RID: 1937
		private GameObject prevGameObject;
	}
}
