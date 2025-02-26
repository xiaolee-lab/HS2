using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject
{
	// Token: 0x02000175 RID: 373
	[TaskCategory("Unity/GameObject")]
	[TaskDescription("Destorys the specified GameObject immediately. Returns Success.")]
	public class DestroyImmediate : Action
	{
		// Token: 0x0600078A RID: 1930 RVA: 0x000245A0 File Offset: 0x000229A0
		public override TaskStatus OnUpdate()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			UnityEngine.Object.DestroyImmediate(defaultGameObject);
			return TaskStatus.Success;
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x000245C6 File Offset: 0x000229C6
		public override void OnReset()
		{
			this.targetGameObject = null;
		}

		// Token: 0x04000688 RID: 1672
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;
	}
}
