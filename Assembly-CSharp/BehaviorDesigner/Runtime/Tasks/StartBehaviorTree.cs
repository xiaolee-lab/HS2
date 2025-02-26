using System;
using System.Collections.Generic;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000D9 RID: 217
	[TaskDescription("Start a new behavior tree and return success after it has been started.")]
	[TaskIcon("{SkinColor}StartBehaviorTreeIcon.png")]
	public class StartBehaviorTree : Action
	{
		// Token: 0x060004CB RID: 1227 RVA: 0x0001DB20 File Offset: 0x0001BF20
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
			if (this.behavior != null)
			{
				List<SharedVariable> allVariables = base.Owner.GetAllVariables();
				if (allVariables != null && this.synchronizeVariables.Value)
				{
					for (int j = 0; j < allVariables.Count; j++)
					{
						this.behavior.SetVariableValue(allVariables[j].Name, allVariables[j]);
					}
				}
				this.behavior.EnableBehavior();
				if (this.waitForCompletion.Value)
				{
					this.behaviorComplete = false;
					this.behavior.OnBehaviorEnd += this.BehaviorEnded;
				}
			}
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0001DC59 File Offset: 0x0001C059
		public override TaskStatus OnUpdate()
		{
			if (this.behavior == null)
			{
				return TaskStatus.Failure;
			}
			if (this.waitForCompletion.Value && !this.behaviorComplete)
			{
				return TaskStatus.Running;
			}
			return TaskStatus.Success;
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0001DC8C File Offset: 0x0001C08C
		private void BehaviorEnded(Behavior behavior)
		{
			this.behaviorComplete = true;
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0001DC95 File Offset: 0x0001C095
		public override void OnEnd()
		{
			if (this.behavior != null && this.waitForCompletion.Value)
			{
				this.behavior.OnBehaviorEnd -= this.BehaviorEnded;
			}
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0001DCCF File Offset: 0x0001C0CF
		public override void OnReset()
		{
			this.behaviorGameObject = null;
			this.group = 0;
			this.waitForCompletion = false;
			this.synchronizeVariables = false;
		}

		// Token: 0x04000426 RID: 1062
		[Tooltip("The GameObject of the behavior tree that should be started. If null use the current behavior")]
		public SharedGameObject behaviorGameObject;

		// Token: 0x04000427 RID: 1063
		[Tooltip("The group of the behavior tree that should be started")]
		public SharedInt group;

		// Token: 0x04000428 RID: 1064
		[Tooltip("Should this task wait for the behavior tree to complete?")]
		public SharedBool waitForCompletion = false;

		// Token: 0x04000429 RID: 1065
		[Tooltip("Should the variables be synchronized?")]
		public SharedBool synchronizeVariables;

		// Token: 0x0400042A RID: 1066
		private bool behaviorComplete;

		// Token: 0x0400042B RID: 1067
		private Behavior behavior;
	}
}
