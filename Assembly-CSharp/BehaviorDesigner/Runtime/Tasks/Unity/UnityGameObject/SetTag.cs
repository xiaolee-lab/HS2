using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject
{
	// Token: 0x0200017E RID: 382
	[TaskCategory("Unity/GameObject")]
	[TaskDescription("Sets the GameObject tag. Returns Success.")]
	public class SetTag : Action
	{
		// Token: 0x060007A5 RID: 1957 RVA: 0x000248F4 File Offset: 0x00022CF4
		public override TaskStatus OnUpdate()
		{
			base.GetDefaultGameObject(this.targetGameObject.Value).tag = this.tag.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x00024918 File Offset: 0x00022D18
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.tag = string.Empty;
		}

		// Token: 0x0400069E RID: 1694
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400069F RID: 1695
		[Tooltip("The GameObject tag")]
		public SharedString tag;
	}
}
