using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector3
{
	// Token: 0x020002C3 RID: 707
	[TaskCategory("Unity/Vector3")]
	[TaskDescription("Sets the X, Y, and Z values of the Vector3.")]
	public class SetXYZ : Action
	{
		// Token: 0x06000C06 RID: 3078 RVA: 0x0002E9D4 File Offset: 0x0002CDD4
		public override TaskStatus OnUpdate()
		{
			Vector3 value = this.vector3Variable.Value;
			if (!this.xValue.IsNone)
			{
				value.x = this.xValue.Value;
			}
			if (!this.yValue.IsNone)
			{
				value.y = this.yValue.Value;
			}
			if (!this.zValue.IsNone)
			{
				value.z = this.zValue.Value;
			}
			this.vector3Variable.Value = value;
			return TaskStatus.Success;
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x0002EA60 File Offset: 0x0002CE60
		public override void OnReset()
		{
			this.vector3Variable = Vector3.zero;
			this.xValue = (this.yValue = (this.zValue = 0f));
		}

		// Token: 0x04000ADA RID: 2778
		[Tooltip("The Vector3 to set the values of")]
		public SharedVector3 vector3Variable;

		// Token: 0x04000ADB RID: 2779
		[Tooltip("The X value. Set to None to have the value ignored")]
		public SharedFloat xValue;

		// Token: 0x04000ADC RID: 2780
		[Tooltip("The Y value. Set to None to have the value ignored")]
		public SharedFloat yValue;

		// Token: 0x04000ADD RID: 2781
		[Tooltip("The Z value. Set to None to have the value ignored")]
		public SharedFloat zValue;
	}
}
