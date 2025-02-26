using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
	// Token: 0x02000207 RID: 519
	[TaskCategory("Unity/Rigidbody")]
	[TaskDescription("Applies a force to the rigidbody that simulates explosion effects. Returns Success.")]
	public class AddExplosionForce : Action
	{
		// Token: 0x0600096F RID: 2415 RVA: 0x00028CE8 File Offset: 0x000270E8
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.rigidbody = defaultGameObject.GetComponent<Rigidbody>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x00028D2C File Offset: 0x0002712C
		public override TaskStatus OnUpdate()
		{
			if (this.rigidbody == null)
			{
				return TaskStatus.Failure;
			}
			this.rigidbody.AddExplosionForce(this.explosionForce.Value, this.explosionPosition.Value, this.explosionRadius.Value, this.upwardsModifier, this.forceMode);
			return TaskStatus.Success;
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x00028D88 File Offset: 0x00027188
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.explosionForce = 0f;
			this.explosionPosition = Vector3.zero;
			this.explosionRadius = 0f;
			this.upwardsModifier = 0f;
			this.forceMode = ForceMode.Force;
		}

		// Token: 0x0400086D RID: 2157
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400086E RID: 2158
		[Tooltip("The force of the explosion")]
		public SharedFloat explosionForce;

		// Token: 0x0400086F RID: 2159
		[Tooltip("The position of the explosion")]
		public SharedVector3 explosionPosition;

		// Token: 0x04000870 RID: 2160
		[Tooltip("The radius of the explosion")]
		public SharedFloat explosionRadius;

		// Token: 0x04000871 RID: 2161
		[Tooltip("Applies the force as if it was applied from beneath the object")]
		public float upwardsModifier;

		// Token: 0x04000872 RID: 2162
		[Tooltip("The type of force")]
		public ForceMode forceMode;

		// Token: 0x04000873 RID: 2163
		private Rigidbody rigidbody;

		// Token: 0x04000874 RID: 2164
		private GameObject prevGameObject;
	}
}
