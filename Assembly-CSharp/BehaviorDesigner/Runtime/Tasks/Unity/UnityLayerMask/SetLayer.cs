using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityLayerMask
{
	// Token: 0x0200018D RID: 397
	[TaskCategory("Unity/LayerMask")]
	[TaskDescription("Sets the layer of a GameObject.")]
	public class SetLayer : Action
	{
		// Token: 0x060007D2 RID: 2002 RVA: 0x00024CEC File Offset: 0x000230EC
		public override TaskStatus OnUpdate()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			defaultGameObject.layer = LayerMask.NameToLayer(this.layerName.Value);
			return TaskStatus.Success;
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x00024D22 File Offset: 0x00023122
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.layerName = "Default";
		}

		// Token: 0x040006B6 RID: 1718
		[Tooltip("The GameObject to set the layer of")]
		public SharedGameObject targetGameObject;

		// Token: 0x040006B7 RID: 1719
		[Tooltip("The name of the layer to set")]
		public SharedString layerName = "Default";
	}
}
