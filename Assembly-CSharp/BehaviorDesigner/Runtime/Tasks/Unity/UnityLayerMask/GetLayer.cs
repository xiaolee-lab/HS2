using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityLayerMask
{
	// Token: 0x0200018C RID: 396
	[TaskCategory("Unity/LayerMask")]
	[TaskDescription("Gets the layer of a GameObject.")]
	public class GetLayer : Action
	{
		// Token: 0x060007CF RID: 1999 RVA: 0x00024C84 File Offset: 0x00023084
		public override TaskStatus OnUpdate()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			this.storeResult.Value = LayerMask.LayerToName(defaultGameObject.layer);
			return TaskStatus.Success;
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x00024CBA File Offset: 0x000230BA
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.storeResult = string.Empty;
		}

		// Token: 0x040006B4 RID: 1716
		[Tooltip("The GameObject to set the layer of")]
		public SharedGameObject targetGameObject;

		// Token: 0x040006B5 RID: 1717
		[Tooltip("The name of the layer to get")]
		[RequiredField]
		public SharedString storeResult;
	}
}
