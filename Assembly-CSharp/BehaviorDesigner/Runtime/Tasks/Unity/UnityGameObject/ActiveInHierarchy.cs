using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject
{
	// Token: 0x02000170 RID: 368
	[TaskCategory("Unity/GameObject")]
	[TaskDescription("Returns Success if the GameObject is active in the hierarchy, otherwise Failure.")]
	public class ActiveInHierarchy : Conditional
	{
		// Token: 0x0600077B RID: 1915 RVA: 0x0002443A File Offset: 0x0002283A
		public override TaskStatus OnUpdate()
		{
			return (!base.GetDefaultGameObject(this.targetGameObject.Value).activeInHierarchy) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0002445E File Offset: 0x0002285E
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04000680 RID: 1664
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;
	}
}
