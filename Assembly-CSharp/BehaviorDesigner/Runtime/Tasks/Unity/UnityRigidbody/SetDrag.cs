using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x02000220 RID: 544
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Sets the drag of the Rigidbody. Returns Success.")]
	public class SetDrag : Action
	{
		// Token: 0x060009D3 RID: 2515 RVA: 0x00029B8C File Offset: 0x00027F8C
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x00029BCF File Offset: 0x00027FCF
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				return TaskStatus.Failure;
			}
			this.rigidbody.drag = this.drag.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x00029BFB File Offset: 0x00027FFB
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.drag = 0f;
		}

		// Token: 0x040008D9 RID: 2265
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040008DA RID: 2266
		[Tooltip("The drag of the Rigidbody")]
		public SharedFloat drag;

		// Token: 0x040008DB RID: 2267
		private Rigidbody rigidbody;

		// Token: 0x040008DC RID: 2268
		private GameObject prevGameObject;
	}
}
