using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject
{
	// Token: 0x02000173 RID: 371
	[TaskCategory("Unity/GameObject")]
	[TaskDescription("Returns Success if tags match, otherwise Failure.")]
	public class CompareTag : Conditional
	{
		// Token: 0x06000784 RID: 1924 RVA: 0x000244EA File Offset: 0x000228EA
		public override TaskStatus OnUpdate()
		{
			return (!base.GetDefaultGameObject(this.targetGameObject.Value).CompareTag(this.tag.Value)) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x00024519 File Offset: 0x00022919
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.tag = string.Empty;
		}

		// Token: 0x04000684 RID: 1668
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000685 RID: 1669
		[Tooltip("The tag to compare against")]
		public SharedString tag;
	}
}
