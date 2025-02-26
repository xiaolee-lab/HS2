using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000D8 RID: 216
	[TaskDescription("Sends an event to the behavior tree, returns success after sending the event.")]
	[HelpURL("https://www.opsive.com/support/documentation/behavior-designer/events/")]
	[TaskIcon("{SkinColor}SendEventIcon.png")]
	public class SendEvent : Action
	{
		// Token: 0x060004C7 RID: 1223 RVA: 0x0001D950 File Offset: 0x0001BD50
		public override void OnStart()
		{
			BehaviorTree[] components = base.GetDefaultGameObject(this.targetGameObject.Value).GetComponents<BehaviorTree>();
			if (components.Length == 1)
			{
				this.behaviorTree = components[0];
			}
			else if (components.Length > 1)
			{
				for (int i = 0; i < components.Length; i++)
				{
					if (components[i].Group == this.group.Value)
					{
						this.behaviorTree = components[i];
						break;
					}
				}
				if (this.behaviorTree == null)
				{
					this.behaviorTree = components[0];
				}
			}
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0001D9E8 File Offset: 0x0001BDE8
		public override TaskStatus OnUpdate()
		{
			if (this.argument1 == null || this.argument1.IsNone)
			{
				this.behaviorTree.SendEvent(this.eventName.Value);
			}
			else if (this.argument2 == null || this.argument2.IsNone)
			{
				this.behaviorTree.SendEvent<object>(this.eventName.Value, this.argument1.GetValue());
			}
			else if (this.argument3 == null || this.argument3.IsNone)
			{
				this.behaviorTree.SendEvent<object, object>(this.eventName.Value, this.argument1.GetValue(), this.argument2.GetValue());
			}
			else
			{
				this.behaviorTree.SendEvent<object, object, object>(this.eventName.Value, this.argument1.GetValue(), this.argument2.GetValue(), this.argument3.GetValue());
			}
			return TaskStatus.Success;
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0001DAF0 File Offset: 0x0001BEF0
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.eventName = string.Empty;
		}

		// Token: 0x0400041F RID: 1055
		[Tooltip("The GameObject of the behavior tree that should have the event sent to it. If null use the current behavior")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000420 RID: 1056
		[Tooltip("The event to send")]
		public SharedString eventName;

		// Token: 0x04000421 RID: 1057
		[Tooltip("The group of the behavior tree that the event should be sent to")]
		public SharedInt group;

		// Token: 0x04000422 RID: 1058
		[Tooltip("Optionally specify a first argument to send")]
		[SharedRequired]
		public SharedVariable argument1;

		// Token: 0x04000423 RID: 1059
		[Tooltip("Optionally specify a second argument to send")]
		[SharedRequired]
		public SharedVariable argument2;

		// Token: 0x04000424 RID: 1060
		[Tooltip("Optionally specify a third argument to send")]
		[SharedRequired]
		public SharedVariable argument3;

		// Token: 0x04000425 RID: 1061
		private BehaviorTree behaviorTree;
	}
}
