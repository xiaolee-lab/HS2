using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject
{
	// Token: 0x0200017A RID: 378
	[TaskCategory("Unity/GameObject")]
	[TaskDescription("Stores the GameObject tag. Returns Success.")]
	public class GetTag : Action
	{
		// Token: 0x06000799 RID: 1945 RVA: 0x0002475D File Offset: 0x00022B5D
		public override TaskStatus OnUpdate()
		{
			this.storeValue.Value = base.GetDefaultGameObject(this.targetGameObject.Value).tag;
			return TaskStatus.Success;
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x00024781 File Offset: 0x00022B81
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeValue = string.Empty;
		}

		// Token: 0x04000693 RID: 1683
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000694 RID: 1684
		[Tooltip("Active state of the GameObject")]
		[RequiredField]
		public SharedString storeValue;
	}
}
