using System;
using System.Reflection;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000D6 RID: 214
	[TaskDescription("Sets the property to the value specified. Returns success if the property was set.")]
	[TaskCategory("Reflection")]
	[TaskIcon("{SkinColor}ReflectionIcon.png")]
	public class SetPropertyValue : Action
	{
		// Token: 0x060004C0 RID: 1216 RVA: 0x0001D7C8 File Offset: 0x0001BBC8
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
			property.SetValue(component, this.propertyValue.GetValue(), null);
			return TaskStatus.Success;
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0001D852 File Offset: 0x0001BC52
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.componentName = null;
			this.propertyName = null;
			this.propertyValue = null;
		}

		// Token: 0x04000418 RID: 1048
		[Tooltip("The GameObject to set the property on")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000419 RID: 1049
		[Tooltip("The component to set the property on")]
		public SharedString componentName;

		// Token: 0x0400041A RID: 1050
		[Tooltip("The name of the property")]
		public SharedString propertyName;

		// Token: 0x0400041B RID: 1051
		[Tooltip("The value to set")]
		public SharedVariable propertyValue;
	}
}
