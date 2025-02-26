using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject
{
	// Token: 0x02000171 RID: 369
	[TaskCategory("Unity/GameObject")]
	[TaskDescription("Returns Success if the GameObject is active in the hierarchy, otherwise Failure.")]
	public class ActiveSelf : Conditional
	{
		// Token: 0x0600077E RID: 1918 RVA: 0x0002446F File Offset: 0x0002286F
		public override TaskStatus OnUpdate()
		{
			return (!base.GetDefaultGameObject(this.targetGameObject.Value).activeSelf) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x00024493 File Offset: 0x00022893
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04000681 RID: 1665
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;
	}
}
