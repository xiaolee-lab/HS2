using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector2
{
	// Token: 0x020002A7 RID: 679
	[TaskCategory("Unity/Vector2")]
	[TaskDescription("Stores the X and Y values of the Vector2.")]
	public class GetXY : Action
	{
		// Token: 0x06000BB8 RID: 3000 RVA: 0x0002DE3C File Offset: 0x0002C23C
		public override TaskStatus OnUpdate()
		{
			this.storeX.Value = this.vector2Variable.Value.x;
			this.storeY.Value = this.vector2Variable.Value.y;
			return TaskStatus.Success;
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x0002DE88 File Offset: 0x0002C288
		public override void OnReset()
		{
			this.vector2Variable = Vector2.zero;
			this.storeX = (this.storeY = 0f);
		}

		// Token: 0x04000A88 RID: 2696
		[Tooltip("The Vector2 to get the values of")]
		public SharedVector2 vector2Variable;

		// Token: 0x04000A89 RID: 2697
		[Tooltip("The X value")]
		[RequiredField]
		public SharedFloat storeX;

		// Token: 0x04000A8A RID: 2698
		[Tooltip("The Y value")]
		[RequiredField]
		public SharedFloat storeY;
	}
}
