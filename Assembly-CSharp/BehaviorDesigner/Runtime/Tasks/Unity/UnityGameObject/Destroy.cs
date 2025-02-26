using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject
{
	// Token: 0x02000174 RID: 372
	[TaskCategory("Unity/GameObject")]
	[TaskDescription("Destorys the specified GameObject. Returns Success.")]
	public class Destroy : Action
	{
		// Token: 0x06000787 RID: 1927 RVA: 0x0002453C File Offset: 0x0002293C
		public override TaskStatus OnUpdate()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (this.time == 0f)
			{
				UnityEngine.Object.Destroy(defaultGameObject);
			}
			else
			{
				UnityEngine.Object.Destroy(defaultGameObject, this.time);
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x00024583 File Offset: 0x00022983
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.time = 0f;
		}

		// Token: 0x04000686 RID: 1670
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000687 RID: 1671
		[Tooltip("Time to destroy the GameObject in")]
		public float time;
	}
}
