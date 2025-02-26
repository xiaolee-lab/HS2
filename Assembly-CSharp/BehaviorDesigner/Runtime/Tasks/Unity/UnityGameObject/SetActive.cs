using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject
{
	// Token: 0x0200017D RID: 381
	[TaskCategory("Unity/GameObject")]
	[TaskDescription("Activates/Deactivates the GameObject. Returns Success.")]
	public class SetActive : Action
	{
		// Token: 0x060007A2 RID: 1954 RVA: 0x000248B3 File Offset: 0x00022CB3
		public override TaskStatus OnUpdate()
		{
			base.GetDefaultGameObject(this.targetGameObject.Value).SetActive(this.active.Value);
			return TaskStatus.Success;
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x000248D7 File Offset: 0x00022CD7
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.active = false;
		}

		// Token: 0x0400069C RID: 1692
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400069D RID: 1693
		[Tooltip("Active state of the GameObject")]
		public SharedBool active;
	}
}
