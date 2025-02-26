using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000DA RID: 218
	[TaskDescription("Pause or disable a behavior tree and return success after it has been stopped.")]
	[TaskIcon("{SkinColor}StopBehaviorTreeIcon.png")]
	public class StopBehaviorTree : Action
	{
		// Token: 0x060004D1 RID: 1233 RVA: 0x0001DD10 File Offset: 0x0001C110
		public override void OnStart()
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

		// Token: 0x060004D2 RID: 1234 RVA: 0x0001DDA8 File Offset: 0x0001C1A8
		public override TaskStatus OnUpdate()
		{
			if (this.behavior == null)
			{
				return TaskStatus.Failure;
			}
			this.behavior.DisableBehavior(this.pauseBehavior.Value);
			return TaskStatus.Success;
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0001DDD4 File Offset: 0x0001C1D4
		public override void OnReset()
		{
			this.behaviorGameObject = null;
			this.group = 0;
			this.pauseBehavior = false;
		}

		// Token: 0x0400042C RID: 1068
		[Tooltip("The GameObject of the behavior tree that should be stopped. If null use the current behavior")]
		public SharedGameObject behaviorGameObject;

		// Token: 0x0400042D RID: 1069
		[Tooltip("The group of the behavior tree that should be stopped")]
		public SharedInt group;

		// Token: 0x0400042E RID: 1070
		[Tooltip("Should the behavior be paused or completely disabled")]
		public SharedBool pauseBehavior = false;

		// Token: 0x0400042F RID: 1071
		private Behavior behavior;
	}
}
