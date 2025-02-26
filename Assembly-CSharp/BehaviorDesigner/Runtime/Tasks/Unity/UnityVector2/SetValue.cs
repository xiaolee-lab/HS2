using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector2
{
	// Token: 0x020002AE RID: 686
	[TaskCategory("Unity/Vector2")]
	[TaskDescription("Sets the value of the Vector2.")]
	public class SetValue : Action
	{
		// Token: 0x06000BCA RID: 3018 RVA: 0x0002E17A File Offset: 0x0002C57A
		public override TaskStatus OnUpdate()
		{
			this.vector2Variable.Value = this.vector2Value.Value;
			return TaskStatus.Success;
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x0002E194 File Offset: 0x0002C594
		public override void OnReset()
		{
			this.vector2Value = (this.vector2Variable = Vector2.zero);
		}

		// Token: 0x04000AA0 RID: 2720
		[Tooltip("The Vector2 to get the values of")]
		public SharedVector2 vector2Value;

		// Token: 0x04000AA1 RID: 2721
		[Tooltip("The Vector2 to set the values of")]
		public SharedVector2 vector2Variable;
	}
}
