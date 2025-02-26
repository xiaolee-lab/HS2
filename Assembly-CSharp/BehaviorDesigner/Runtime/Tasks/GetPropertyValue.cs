using System;
using System.Reflection;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000D3 RID: 211
	[TaskDescription("Gets the value from the property specified. Returns success if the property was retrieved.")]
	[TaskCategory("Reflection")]
	[TaskIcon("{SkinColor}ReflectionIcon.png")]
	public class GetPropertyValue : Action
	{
		// Token: 0x060004B7 RID: 1207 RVA: 0x0001D4F0 File Offset: 0x0001B8F0
		public override TaskStatus OnUpdate()
		{
			if (this.propertyValue == null)
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
			PropertyInfo property = component.GetType().GetProperty(this.propertyName.Value);
			this.propertyValue.SetValue(property.GetValue(component, null));
			return TaskStatus.Success;
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0001D57A File Offset: 0x0001B97A
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.componentName = null;
			this.propertyName = null;
			this.propertyValue = null;
		}

		// Token: 0x04000408 RID: 1032
		[Tooltip("The GameObject to get the property of")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000409 RID: 1033
		[Tooltip("The component to get the property of")]
		public SharedString componentName;

		// Token: 0x0400040A RID: 1034
		[Tooltip("The name of the property")]
		public SharedString propertyName;

		// Token: 0x0400040B RID: 1035
		[Tooltip("The value of the property")]
		[RequiredField]
		public SharedVariable propertyValue;
	}
}
