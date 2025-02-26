using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject
{
	// Token: 0x0200017C RID: 380
	[TaskCategory("Unity/GameObject")]
	[TaskDescription("Sends a message to the target GameObject. Returns Success.")]
	public class SendMessage : Action
	{
		// Token: 0x0600079F RID: 1951 RVA: 0x00024818 File Offset: 0x00022C18
		public override TaskStatus OnUpdate()
		{
			if (this.value.Value != null)
			{
				base.GetDefaultGameObject(this.targetGameObject.Value).SendMessage(this.message.Value, this.value.Value.value.GetValue());
			}
			else
			{
				base.GetDefaultGameObject(this.targetGameObject.Value).SendMessage(this.message.Value);
			}
			return TaskStatus.Success;
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x00024892 File Offset: 0x00022C92
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.message = string.Empty;
		}

		// Token: 0x04000699 RID: 1689
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400069A RID: 1690
		[Tooltip("The message to send")]
		public SharedString message;

		// Token: 0x0400069B RID: 1691
		[Tooltip("The value to send")]
		public SharedGenericVariable value;
	}
}
