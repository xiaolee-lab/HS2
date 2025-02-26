using System;
using System.Reflection;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000F1 RID: 241
	[TaskDescription("Compares the property value to the value specified. Returns success if the values are the same.")]
	[TaskCategory("Reflection")]
	[TaskIcon("{SkinColor}ReflectionIcon.png")]
	public class ComparePropertyValue : Conditional
	{
		// Token: 0x06000571 RID: 1393 RVA: 0x0001F5DC File Offset: 0x0001D9DC
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
			PropertyInfo property = component.GetType().GetProperty(this.propertyName.Value);
			object value = property.GetValue(component, null);
			if (value == null && this.compareValue.GetValue() == null)
			{
				return TaskStatus.Success;
			}
			return (!value.Equals(this.compareValue.GetValue())) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0001F690 File Offset: 0x0001DA90
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.componentName = null;
			this.propertyName = null;
			this.compareValue = null;
		}

		// Token: 0x0400047C RID: 1148
		[Tooltip("The GameObject to compare the property of")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400047D RID: 1149
		[Tooltip("The component to compare the property of")]
		public SharedString componentName;

		// Token: 0x0400047E RID: 1150
		[Tooltip("The name of the property")]
		public SharedString propertyName;

		// Token: 0x0400047F RID: 1151
		[Tooltip("The value to compare to")]
		public SharedVariable compareValue;
	}
}
