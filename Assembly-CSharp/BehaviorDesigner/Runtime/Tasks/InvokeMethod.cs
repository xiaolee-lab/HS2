using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
	// Token: 0x020000D4 RID: 212
	[TaskDescription("Invokes the specified method with the specified parameters. Can optionally store the return value. Returns success if the method was invoked.")]
	[TaskCategory("Reflection")]
	[TaskIcon("{SkinColor}ReflectionIcon.png")]
	public class InvokeMethod : Action
	{
		// Token: 0x060004BA RID: 1210 RVA: 0x0001D5A0 File Offset: 0x0001B9A0
		public override TaskStatus OnUpdate()
		{
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
			List<object> list = new List<object>();
			List<Type> list2 = new List<Type>();
			for (int i = 0; i < 4; i++)
			{
				FieldInfo field = base.GetType().GetField("parameter" + (i + 1));
				SharedVariable sharedVariable;
				if ((sharedVariable = (field.GetValue(this) as SharedVariable)) == null)
				{
					break;
				}
				list.Add(sharedVariable.GetValue());
				list2.Add(sharedVariable.GetType().GetProperty("Value").PropertyType);
			}
			MethodInfo method = component.GetType().GetMethod(this.methodName.Value, list2.ToArray());
			if (method == null)
			{
				return TaskStatus.Failure;
			}
			object value = method.Invoke(component, list.ToArray());
			if (this.storeResult != null)
			{
				this.storeResult.SetValue(value);
			}
			return TaskStatus.Success;
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0001D6D5 File Offset: 0x0001BAD5
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.componentName = null;
			this.methodName = null;
			this.parameter1 = null;
			this.parameter2 = null;
			this.parameter3 = null;
			this.parameter4 = null;
			this.storeResult = null;
		}

		// Token: 0x0400040C RID: 1036
		[Tooltip("The GameObject to invoke the method on")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400040D RID: 1037
		[Tooltip("The component to invoke the method on")]
		public SharedString componentName;

		// Token: 0x0400040E RID: 1038
		[Tooltip("The name of the method")]
		public SharedString methodName;

		// Token: 0x0400040F RID: 1039
		[Tooltip("The first parameter of the method")]
		public SharedVariable parameter1;

		// Token: 0x04000410 RID: 1040
		[Tooltip("The second parameter of the method")]
		public SharedVariable parameter2;

		// Token: 0x04000411 RID: 1041
		[Tooltip("The third parameter of the method")]
		public SharedVariable parameter3;

		// Token: 0x04000412 RID: 1042
		[Tooltip("The fourth parameter of the method")]
		public SharedVariable parameter4;

		// Token: 0x04000413 RID: 1043
		[Tooltip("Store the result of the invoke call")]
		public SharedVariable storeResult;
	}
}
