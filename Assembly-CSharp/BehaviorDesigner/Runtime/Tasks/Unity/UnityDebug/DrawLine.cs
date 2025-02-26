using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityDebug
{
	// Token: 0x0200016C RID: 364
	[TaskCategory("Unity/Debug")]
	[TaskDescription("Draws a debug line")]
	public class DrawLine : Action
	{
		// Token: 0x0600076D RID: 1901 RVA: 0x000241E0 File Offset: 0x000225E0
		public override TaskStatus OnUpdate()
		{
			return TaskStatus.Success;
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x000241E4 File Offset: 0x000225E4
		public override void OnReset()
		{
			this.start = Vector3.zero;
			this.end = Vector3.zero;
			this.color = Color.white;
			this.duration = 0f;
			this.depthTest = true;
		}

		// Token: 0x04000671 RID: 1649
		[Tooltip("The start position")]
		public SharedVector3 start;

		// Token: 0x04000672 RID: 1650
		[Tooltip("The end position")]
		public SharedVector3 end;

		// Token: 0x04000673 RID: 1651
		[Tooltip("The color")]
		public SharedColor color = Color.white;

		// Token: 0x04000674 RID: 1652
		[Tooltip("Duration the line will be visible for in seconds.\nDefault: 0 means 1 frame.")]
		public SharedFloat duration;

		// Token: 0x04000675 RID: 1653
		[Tooltip("Whether the line should show through world geometry.")]
		public SharedBool depthTest = true;
	}
}
