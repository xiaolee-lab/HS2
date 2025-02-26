using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject
{
	// Token: 0x02000176 RID: 374
	[TaskCategory("Unity/GameObject")]
	[TaskDescription("Finds a GameObject by name. Returns Success.")]
	public class Find : Action
	{
		// Token: 0x0600078D RID: 1933 RVA: 0x000245D7 File Offset: 0x000229D7
		public override TaskStatus OnUpdate()
		{
			this.storeValue.Value = GameObject.Find(this.gameObjectName.Value);
			return TaskStatus.Success;
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x000245F5 File Offset: 0x000229F5
		public override void OnReset()
		{
			this.gameObjectName = null;
			this.storeValue = null;
		}

		// Token: 0x04000689 RID: 1673
		[Tooltip("The GameObject name to find")]
		public SharedString gameObjectName;

		// Token: 0x0400068A RID: 1674
		[Tooltip("The object found by name")]
		[RequiredField]
		public SharedGameObject storeValue;
	}
}
