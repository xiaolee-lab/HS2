using System;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000D0 RID: 208
	[TaskDescription("Log is a simple task which will output the specified text and return success. It can be used for debugging.")]
	[TaskIcon("{SkinColor}LogIcon.png")]
	public class Log : Action
	{
		// Token: 0x060004AE RID: 1198 RVA: 0x0001D39A File Offset: 0x0001B79A
		public override TaskStatus OnUpdate()
		{
			if (this.logError.Value)
			{
			}
			return TaskStatus.Success;
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0001D3B2 File Offset: 0x0001B7B2
		public override void OnReset()
		{
			this.text = string.Empty;
			this.logError = false;
		}

		// Token: 0x04000400 RID: 1024
		[Tooltip("Text to output to the log")]
		public SharedString text;

		// Token: 0x04000401 RID: 1025
		[Tooltip("Is this text an error?")]
		public SharedBool logError;
	}
}
