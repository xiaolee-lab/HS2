using System;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x02000117 RID: 279
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Automatically adjust the gameobject position and rotation so that the AvatarTarget reaches the matchPosition when the current state is at the specified progress. Returns Success.")]
	public class MatchTarget : Action
	{
		// Token: 0x06000615 RID: 1557 RVA: 0x00020CFC File Offset: 0x0001F0FC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00020D40 File Offset: 0x0001F140
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				return TaskStatus.Failure;
			}
			this.animator.MatchTarget(this.matchPosition.Value, this.matchRotation.Value, this.targetBodyPart, new MatchTargetWeightMask(this.weightMaskPosition, this.weightMaskRotation), this.startNormalizedTime, this.targetNormalizedTime);
			return TaskStatus.Success;
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x00020DA8 File Offset: 0x0001F1A8
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.matchPosition = Vector3.zero;
			this.matchRotation = Quaternion.identity;
			this.targetBodyPart = AvatarTarget.Root;
			this.weightMaskPosition = Vector3.zero;
			this.weightMaskRotation = 0f;
			this.startNormalizedTime = 0f;
			this.targetNormalizedTime = 1f;
		}

		// Token: 0x04000511 RID: 1297
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000512 RID: 1298
		[Tooltip("The position we want the body part to reach")]
		public SharedVector3 matchPosition;

		// Token: 0x04000513 RID: 1299
		[Tooltip("The rotation in which we want the body part to be")]
		public SharedQuaternion matchRotation;

		// Token: 0x04000514 RID: 1300
		[Tooltip("The body part that is involved in the match")]
		public AvatarTarget targetBodyPart;

		// Token: 0x04000515 RID: 1301
		[Tooltip("Weights for matching position")]
		public Vector3 weightMaskPosition;

		// Token: 0x04000516 RID: 1302
		[Tooltip("Weights for matching rotation")]
		public float weightMaskRotation;

		// Token: 0x04000517 RID: 1303
		[Tooltip("Start time within the animation clip")]
		public float startNormalizedTime;

		// Token: 0x04000518 RID: 1304
		[Tooltip("End time within the animation clip")]
		public float targetNormalizedTime = 1f;

		// Token: 0x04000519 RID: 1305
		private Animator animator;

		// Token: 0x0400051A RID: 1306
		private GameObject prevGameObject;
	}
}
