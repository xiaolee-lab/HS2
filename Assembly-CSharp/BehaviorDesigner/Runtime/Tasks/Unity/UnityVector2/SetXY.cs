using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector2
{
	// Token: 0x020002AF RID: 687
	[TaskCategory("Unity/Vector2")]
	[TaskDescription("Sets the X and Y values of the Vector2.")]
	public class SetXY : Action
	{
		// Token: 0x06000BCD RID: 3021 RVA: 0x0002E1C4 File Offset: 0x0002C5C4
		public override TaskStatus OnUpdate()
		{
			Vector2 value = this.vector2Variable.Value;
			if (!this.xValue.IsNone)
			{
				value.x = this.xValue.Value;
			}
			if (!this.yValue.IsNone)
			{
				value.y = this.yValue.Value;
			}
			this.vector2Variable.Value = value;
			return TaskStatus.Success;
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x0002E230 File Offset: 0x0002C630
		public override void OnReset()
		{
			this.vector2Variable = Vector2.zero;
			this.xValue = (this.yValue = 0f);
		}

		// Token: 0x04000AA2 RID: 2722
		[Tooltip("The Vector2 to set the values of")]
		public SharedVector2 vector2Variable;

		// Token: 0x04000AA3 RID: 2723
		[Tooltip("The X value. Set to None to have the value ignored")]
		public SharedFloat xValue;

		// Token: 0x04000AA4 RID: 2724
		[Tooltip("The Y value. Set to None to have the value ignored")]
		public SharedFloat yValue;
	}
}
