using System;
using BehaviorDesigner.Runtime;

namespace AIProject
{
	// Token: 0x0200096D RID: 2413
	public static class BehaviorDesignerExtensions
	{
		// Token: 0x060042F7 RID: 17143 RVA: 0x001A5BBC File Offset: 0x001A3FBC
		public static T GetVariable<T>(this Behavior behavior, string name) where T : SharedVariable, new()
		{
			bool flag = behavior != null;
			if (flag)
			{
				return behavior.GetVariable(name) as T;
			}
			return (T)((object)null);
		}
	}
}
