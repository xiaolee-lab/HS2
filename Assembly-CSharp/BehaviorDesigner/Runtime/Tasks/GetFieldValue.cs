using System;
using System.Reflection;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000D2 RID: 210
	[TaskDescription("Gets the value from the field specified. Returns success if the field was retrieved.")]
	[TaskCategory("Reflection")]
	[TaskIcon("{SkinColor}ReflectionIcon.png")]
	public class GetFieldValue : Action
	{
		// Token: 0x060004B4 RID: 1204 RVA: 0x0001D440 File Offset: 0x0001B840
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
			this.fieldValue.SetValue(field.GetValue(component));
			return TaskStatus.Success;
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0001D4C9 File Offset: 0x0001B8C9
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.componentName = null;
			this.fieldName = null;
			this.fieldValue = null;
		}

		// Token: 0x04000404 RID: 1028
		[Tooltip("The GameObject to get the field on")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000405 RID: 1029
		[Tooltip("The component to get the field on")]
		public SharedString componentName;

		// Token: 0x04000406 RID: 1030
		[Tooltip("The name of the field")]
		public SharedString fieldName;

		// Token: 0x04000407 RID: 1031
		[Tooltip("The value of the field")]
		[RequiredField]
		public SharedVariable fieldValue;
	}
}
