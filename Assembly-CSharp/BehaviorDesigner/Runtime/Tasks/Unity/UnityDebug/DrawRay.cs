using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityDebug
{
	// Token: 0x0200016D RID: 365
	[TaskCategory("Unity/Debug")]
	[TaskDescription("Draws a debug ray")]
	public class DrawRay : Action
	{
		// Token: 0x06000770 RID: 1904 RVA: 0x00024255 File Offset: 0x00022655
		public override TaskStatus OnUpdate()
		{
			return TaskStatus.Success;
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x00024258 File Offset: 0x00022658
		public override void OnReset()
		{
			this.start = Vector3.zero;
			this.direction = Vector3.zero;
			this.color = Color.white;
		}

		// Token: 0x04000676 RID: 1654
		[Tooltip("The position")]
		public SharedVector3 start;

		// Token: 0x04000677 RID: 1655
		[Tooltip("The direction")]
		public SharedVector3 direction;

		// Token: 0x04000678 RID: 1656
		[Tooltip("The color")]
		public SharedColor color = Color.white;
	}
}
