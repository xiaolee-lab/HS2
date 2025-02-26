using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000D7 RID: 215
	[TaskDescription("Restarts a behavior tree, returns success after it has been restarted.")]
	[TaskIcon("{SkinColor}RestartBehaviorTreeIcon.png")]
	public class RestartBehaviorTree : Action
	{
		// Token: 0x060004C3 RID: 1219 RVA: 0x0001D878 File Offset: 0x0001BC78
		public override void OnAwake()
		{
			Behavior[] components = base.GetDefaultGameObject(this.behaviorGameObject.Value).GetComponents<Behavior>();
			if (components.Length == 1)
			{
				this.behavior = components[0];
			}
			else if (components.Length > 1)
			{
				for (int i = 0; i < components.Length; i++)
				{
					if (components[i].Group == this.group.Value)
					{
						this.behavior = components[i];
						break;
					}
				}
				if (this.behavior == null)
				{
					this.behavior = components[0];
				}
			}
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0001D910 File Offset: 0x0001BD10
		public override TaskStatus OnUpdate()
		{
			if (this.behavior == null)
			{
				return TaskStatus.Failure;
			}
			this.behavior.DisableBehavior();
			this.behavior.EnableBehavior();
			return TaskStatus.Success;
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0001D93C File Offset: 0x0001BD3C
		public override void OnReset()
		{
			this.behavior = null;
		}

		// Token: 0x0400041C RID: 1052
		[Tooltip("The GameObject of the behavior tree that should be restarted. If null use the current behavior")]
		public SharedGameObject behaviorGameObject;

		// Token: 0x0400041D RID: 1053
		[Tooltip("The group of the behavior tree that should be restarted")]
		public SharedInt group;

		// Token: 0x0400041E RID: 1054
		private Behavior behavior;
	}
}
