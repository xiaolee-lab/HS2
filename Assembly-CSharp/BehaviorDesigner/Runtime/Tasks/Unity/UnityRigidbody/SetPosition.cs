using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x02000224 RID: 548
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Sets the position of the Rigidbody. Returns Success.")]
	public class SetPosition : Action
	{
		// Token: 0x060009E3 RID: 2531 RVA: 0x00029DC4 File Offset: 0x000281C4
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x00029E07 File Offset: 0x00028207
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				return TaskStatus.Failure;
			}
			this.rigidbody.position = this.position.Value;
			return TaskStatus.Success;
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x00029E33 File Offset: 0x00028233
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.position = Vector3.zero;
		}

		// Token: 0x040008E9 RID: 2281
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x040008EA RID: 2282
		[Tooltip("The position of the Rigidbody")]
		public SharedVector3 position;

		// Token: 0x040008EB RID: 2283
		private Rigidbody rigidbody;

		// Token: 0x040008EC RID: 2284
		private GameObject prevGameObject;
	}
}
