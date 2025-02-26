using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityVector2
{
	// Token: 0x0200029F RID: 671
	[TaskCategory("Unity/Vector2")]
	[TaskDescription("Clamps the magnitude of the Vector2.")]
	public class ClampMagnitude : Action
	{
		// Token: 0x06000BA0 RID: 2976 RVA: 0x0002DBB1 File Offset: 0x0002BFB1
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = Vector2.ClampMagnitude(this.vector2Variable.Value, this.maxLength.Value);
			return TaskStatus.Success;
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x0002DBDC File Offset: 0x0002BFDC
		public override void OnReset()
		{
			this.vector2Variable = (this.storeResult = Vector2.zero);
			this.maxLength = 0f;
		}

		// Token: 0x04000A77 RID: 2679
		[Tooltip("The Vector2 to clamp the magnitude of")]
		public SharedVector2 vector2Variable;

		// Token: 0x04000A78 RID: 2680
		[Tooltip("The max length of the magnitude")]
		public SharedFloat maxLength;

		// Token: 0x04000A79 RID: 2681
		[Tooltip("The clamp magnitude resut")]
		[RequiredField]
		public SharedVector2 storeResult;
	}
}
