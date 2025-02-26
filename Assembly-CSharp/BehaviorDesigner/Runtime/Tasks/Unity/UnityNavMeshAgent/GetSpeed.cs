using System;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityNavMeshAgent
{
	// Token: 0x020001BF RID: 447
	[TaskCategory("Unity/NavMeshAgent")]
	[TaskDescription("Gets the maximum movement speed when following a path. Returns Success.")]
	public class GetSpeed : Action
	{
		// Token: 0x06000870 RID: 2160 RVA: 0x000265F8 File Offset: 0x000249F8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.navMeshAgent = defaultGameObject.GetComponent<NavMeshAgent>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0002663B File Offset: 0x00024A3B
		public override TaskStatus OnUpdate()
		{
			if (this.navMeshAgent == null)
			{
				return TaskStatus.Failure;
			}
			this.storeValue.Value = this.navMeshAgent.speed;
			return TaskStatus.Success;
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x00026667 File Offset: 0x00024A67
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = 0f;
		}

		// Token: 0x0400076D RID: 1901
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400076E RID: 1902
		[SharedRequired]
		[Tooltip("The NavMeshAgent speed")]
		public SharedFloat storeValue;

		// Token: 0x0400076F RID: 1903
		private NavMeshAgent navMeshAgent;

		// Token: 0x04000770 RID: 1904
		private GameObject prevGameObject;
	}
}
