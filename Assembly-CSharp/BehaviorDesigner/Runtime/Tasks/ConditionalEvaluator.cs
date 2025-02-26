using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000F2 RID: 242
	[TaskDescription("Evaluates the specified conditional task. If the conditional task returns success then the child task is run and the child status is returned. If the conditional task does not return success then the child task is not run and a failure status is immediately returned.")]
	[TaskIcon("{SkinColor}ConditionalEvaluatorIcon.png")]
	public class ConditionalEvaluator : Decorator
	{
		// Token: 0x06000574 RID: 1396 RVA: 0x0001F6C0 File Offset: 0x0001DAC0
		public override void OnAwake()
		{
			if (this.conditionalTask != null)
			{
				this.conditionalTask.Owner = base.Owner;
				this.conditionalTask.GameObject = this.gameObject;
				this.conditionalTask.Transform = this.transform;
				this.conditionalTask.OnAwake();
			}
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0001F716 File Offset: 0x0001DB16
		public override void OnStart()
		{
			if (this.conditionalTask != null)
			{
				this.conditionalTask.OnStart();
			}
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0001F72E File Offset: 0x0001DB2E
		public override bool CanExecute()
		{
			if (this.checkConditionalTask)
			{
				this.checkConditionalTask = false;
				this.OnUpdate();
			}
			return !this.conditionalTaskFailed && (this.executionStatus == TaskStatus.Inactive || this.executionStatus == TaskStatus.Running);
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0001F76D File Offset: 0x0001DB6D
		public override bool CanReevaluate()
		{
			return this.reevaluate.Value;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0001F77C File Offset: 0x0001DB7C
		public override TaskStatus OnUpdate()
		{
			TaskStatus taskStatus = this.conditionalTask.OnUpdate();
			this.conditionalTaskFailed = (this.conditionalTask == null || taskStatus == TaskStatus.Failure);
			return taskStatus;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0001F7AE File Offset: 0x0001DBAE
		public override void OnChildExecuted(TaskStatus childStatus)
		{
			this.executionStatus = childStatus;
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0001F7B7 File Offset: 0x0001DBB7
		public override TaskStatus OverrideStatus()
		{
			return TaskStatus.Failure;
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x0001F7BA File Offset: 0x0001DBBA
		public override TaskStatus OverrideStatus(TaskStatus status)
		{
			if (this.conditionalTaskFailed)
			{
				return TaskStatus.Failure;
			}
			return status;
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0001F7CA File Offset: 0x0001DBCA
		public override void OnEnd()
		{
			this.executionStatus = TaskStatus.Inactive;
			this.checkConditionalTask = true;
			this.conditionalTaskFailed = false;
			if (this.conditionalTask != null)
			{
				this.conditionalTask.OnEnd();
			}
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0001F7F7 File Offset: 0x0001DBF7
		public override void OnReset()
		{
			this.conditionalTask = null;
		}

		// Token: 0x04000480 RID: 1152
		[Tooltip("Should the conditional task be reevaluated every tick?")]
		public SharedBool reevaluate;

		// Token: 0x04000481 RID: 1153
		[InspectTask]
		[Tooltip("The conditional task to evaluate")]
		public Conditional conditionalTask;

		// Token: 0x04000482 RID: 1154
		private TaskStatus executionStatus;

		// Token: 0x04000483 RID: 1155
		private bool checkConditionalTask = true;

		// Token: 0x04000484 RID: 1156
		private bool conditionalTaskFailed;
	}
}
