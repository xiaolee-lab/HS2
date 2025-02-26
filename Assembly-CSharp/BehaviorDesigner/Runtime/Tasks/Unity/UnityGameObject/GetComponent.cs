using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject
{
	// Token: 0x02000179 RID: 377
	[TaskCategory("Unity/GameObject")]
	[TaskDescription("Returns the component of Type type if the game object has one attached, null if it doesn't. Returns Success.")]
	public class GetComponent : Action
	{
		// Token: 0x06000796 RID: 1942 RVA: 0x00024701 File Offset: 0x00022B01
		public override TaskStatus OnUpdate()
		{
			this.storeValue.Value = base.GetDefaultGameObject(this.targetGameObject.Value).GetComponent(this.type.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x00024730 File Offset: 0x00022B30
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.type.Value = string.Empty;
			this.storeValue.Value = null;
		}

		// Token: 0x04000690 RID: 1680
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000691 RID: 1681
		[Tooltip("The type of component")]
		public SharedString type;

		// Token: 0x04000692 RID: 1682
		[Tooltip("The component")]
		[RequiredField]
		public SharedObject storeValue;
	}
}
