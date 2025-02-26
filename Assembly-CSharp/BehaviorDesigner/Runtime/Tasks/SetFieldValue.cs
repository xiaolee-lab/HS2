using System;
using System.Reflection;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000D5 RID: 213
	[TaskDescription("Sets the field to the value specified. Returns success if the field was set.")]
	[TaskCategory("Reflection")]
	[TaskIcon("{SkinColor}ReflectionIcon.png")]
	public class SetFieldValue : Action
	{
		// Token: 0x060004BD RID: 1213 RVA: 0x0001D718 File Offset: 0x0001BB18
		public override TaskStatus OnUpdate()
		{
			if (this.fieldValue == null)
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
			field.SetValue(component, this.fieldValue.GetValue());
			return TaskStatus.Success;
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0001D7A1 File Offset: 0x0001BBA1
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.componentName = null;
			this.fieldName = null;
			this.fieldValue = null;
		}

		// Token: 0x04000414 RID: 1044
		[Tooltip("The GameObject to set the field on")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000415 RID: 1045
		[Tooltip("The component to set the field on")]
		public SharedString componentName;

		// Token: 0x04000416 RID: 1046
		[Tooltip("The name of the field")]
		public SharedString fieldName;

		// Token: 0x04000417 RID: 1047
		[Tooltip("The value to set")]
		public SharedVariable fieldValue;
	}
}
