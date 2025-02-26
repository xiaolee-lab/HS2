using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityDebug
{
	// Token: 0x0200016E RID: 366
	[TaskDescription("LogFormat is analgous to Debug.LogFormat().\nIt takes format string, substitutes arguments supplied a '{0-4}' and returns success.\nAny fields or arguments not supplied are ignored.It can be used for debugging.")]
	[TaskIcon("{SkinColor}LogIcon.png")]
	public class LogFormat : Action
	{
		// Token: 0x06000773 RID: 1907 RVA: 0x00024294 File Offset: 0x00022694
		public override TaskStatus OnUpdate()
		{
			object[] array = this.buildParamsArray();
			if (this.logError.Value)
			{
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x000242C0 File Offset: 0x000226C0
		private object[] buildParamsArray()
		{
			object[] array;
			if (this.isValid(this.arg3))
			{
				array = new object[]
				{
					null,
					null,
					null,
					this.arg3.GetValue()
				};
				array[2] = this.arg2.GetValue();
				array[1] = this.arg1.GetValue();
				array[0] = this.arg0.GetValue();
			}
			else if (this.isValid(this.arg2))
			{
				array = new object[]
				{
					null,
					null,
					this.arg2.GetValue()
				};
				array[1] = this.arg1.GetValue();
				array[0] = this.arg0.GetValue();
			}
			else if (this.isValid(this.arg1))
			{
				array = new object[]
				{
					null,
					this.arg1.GetValue()
				};
				array[0] = this.arg0.GetValue();
			}
			else
			{
				if (!this.isValid(this.arg0))
				{
					return null;
				}
				array = new object[]
				{
					this.arg0.GetValue()
				};
			}
			return array;
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x000243D0 File Offset: 0x000227D0
		private bool isValid(SharedVariable sv)
		{
			return sv != null && !sv.IsNone;
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x000243E4 File Offset: 0x000227E4
		public override void OnReset()
		{
			this.textFormat = string.Empty;
			this.logError = false;
			this.arg0 = null;
			this.arg1 = null;
			this.arg2 = null;
			this.arg3 = null;
		}

		// Token: 0x04000679 RID: 1657
		[Tooltip("Text format with {0}, {1}, etc")]
		public SharedString textFormat;

		// Token: 0x0400067A RID: 1658
		[Tooltip("Is this text an error?")]
		public SharedBool logError;

		// Token: 0x0400067B RID: 1659
		public SharedVariable arg0;

		// Token: 0x0400067C RID: 1660
		public SharedVariable arg1;

		// Token: 0x0400067D RID: 1661
		public SharedVariable arg2;

		// Token: 0x0400067E RID: 1662
		public SharedVariable arg3;
	}
}
