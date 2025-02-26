using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.SharedVariables
{
	// Token: 0x02000265 RID: 613
	[TaskCategory("Unity/SharedVariable")]
	[TaskDescription("Gets the Transform from the GameObject. Returns Success.")]
	public class SharedGameObjectToTransform : Action
	{
		// Token: 0x06000AC7 RID: 2759 RVA: 0x0002BC7E File Offset: 0x0002A07E
		public override TaskStatus OnUpdate()
		{
			if (this.sharedGameObject.Value == null)
			{
				return TaskStatus.Failure;
			}
			this.sharedTransform.Value = this.sharedGameObject.Value.GetComponent<Transform>();
			return TaskStatus.Success;
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x0002BCB4 File Offset: 0x0002A0B4
		public override void OnReset()
		{
			this.sharedGameObject = null;
			this.sharedTransform = null;
		}

		// Token: 0x040009A6 RID: 2470
		[Tooltip("The GameObject to get the Transform of")]
		public SharedGameObject sharedGameObject;

		// Token: 0x040009A7 RID: 2471
		[RequiredField]
		[Tooltip("The Transform to set")]
		public SharedTransform sharedTransform;
	}
}
