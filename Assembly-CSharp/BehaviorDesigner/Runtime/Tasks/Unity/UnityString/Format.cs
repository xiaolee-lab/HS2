using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityString
{
	// Token: 0x0200026E RID: 622
	[TaskCategory("Unity/String")]
	[TaskDescription("Stores a string with the specified format.")]
	public class Format : Action
	{
		// Token: 0x06000AE7 RID: 2791 RVA: 0x0002C0AC File Offset: 0x0002A4AC
		public override void OnAwake()
		{
			this.variableValues = new object[this.variables.Length];
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x0002C0C4 File Offset: 0x0002A4C4
		public override TaskStatus OnUpdate()
		{
			for (int i = 0; i < this.variableValues.Length; i++)
			{
				this.variableValues[i] = this.variables[i].Value.value.GetValue();
			}
			try
			{
				this.storeResult.Value = string.Format(this.format.Value, this.variableValues);
			}
			catch (Exception ex)
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x0002C14C File Offset: 0x0002A54C
		public override void OnReset()
		{
			this.format = string.Empty;
			this.variables = null;
			this.storeResult = null;
		}

		// Token: 0x040009C1 RID: 2497
		[Tooltip("The format of the string")]
		public SharedString format;

		// Token: 0x040009C2 RID: 2498
		[Tooltip("Any variables to appear in the string")]
		public SharedGenericVariable[] variables;

		// Token: 0x040009C3 RID: 2499
		[Tooltip("The result of the format")]
		[RequiredField]
		public SharedString storeResult;

		// Token: 0x040009C4 RID: 2500
		private object[] variableValues;
	}
}
