using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject
{
	// Token: 0x0200017B RID: 379
	[TaskCategory("Unity/GameObject")]
	[TaskDescription("Instantiates a new GameObject. Returns Success.")]
	public class Instantiate : Action
	{
		// Token: 0x0600079C RID: 1948 RVA: 0x000247B2 File Offset: 0x00022BB2
		public override TaskStatus OnUpdate()
		{
			this.storeResult.Value = UnityEngine.Object.Instantiate<GameObject>(this.targetGameObject.Value, this.position.Value, this.rotation.Value);
			return TaskStatus.Success;
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x000247E6 File Offset: 0x00022BE6
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.position = Vector3.zero;
			this.rotation = Quaternion.identity;
		}

		// Token: 0x04000695 RID: 1685
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000696 RID: 1686
		[Tooltip("The position of the new GameObject")]
		public SharedVector3 position;

		// Token: 0x04000697 RID: 1687
		[Tooltip("The rotation of the new GameObject")]
		public SharedQuaternion rotation = Quaternion.identity;

		// Token: 0x04000698 RID: 1688
		[SharedRequired]
		[Tooltip("The instantiated GameObject")]
		public SharedGameObject storeResult;
	}
}
