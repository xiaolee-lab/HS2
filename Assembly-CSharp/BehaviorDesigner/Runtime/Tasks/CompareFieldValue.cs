using System;
using System.Reflection;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000F0 RID: 240
	[TaskDescription("Compares the field value to the value specified. Returns success if the values are the same.")]
	[TaskCategory("Reflection")]
	[TaskIcon("{SkinColor}ReflectionIcon.png")]
	public class CompareFieldValue : Conditional
	{
		// Token: 0x0600056E RID: 1390 RVA: 0x0001F500 File Offset: 0x0001D900
		public override TaskStatus OnUpdate()
		{
			if (this.compareValue == null)
			{
				return TaskStatus.Failure;
			}
			Type typeWithinAssembly = TaskUtility.GetTypeWithinAssembly(this.componentName.Value);
			if (typeWithinAssembly == null)
			{
				return TaskStatus.Failure;
			}
			Component component = base.GetDefaultGameObject(this.targetGameObject.Value).GetComponent(typeWithinAssembly);
			if (component == null)
			{
				return TaskStatus.Failure;
			}
			FieldInfo field = component.GetType().GetField(this.fieldName.Value);
			object value = field.GetValue(component);
			if (value == null && this.compareValue.GetValue() == null)
			{
				return TaskStatus.Success;
			}
			return (!value.Equals(this.compareValue.GetValue())) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0001F5B3 File Offset: 0x0001D9B3
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.componentName = null;
			this.fieldName = null;
			this.compareValue = null;
		}

		// Token: 0x04000478 RID: 1144
		[Tooltip("The GameObject to compare the field on")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000479 RID: 1145
		[Tooltip("The component to compare the field on")]
		public SharedString componentName;

		// Token: 0x0400047A RID: 1146
		[Tooltip("The name of the field")]
		public SharedString fieldName;

		// Token: 0x0400047B RID: 1147
		[Tooltip("The value to compare to")]
		public SharedVariable compareValue;
	}
}
