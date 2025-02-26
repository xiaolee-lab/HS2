using System;
using System.Collections;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x0200011A RID: 282
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Sets the bool parameter on an animator. Returns Success.")]
	public class SetBoolParameter : Action
	{
		// Token: 0x06000621 RID: 1569 RVA: 0x00020F68 File Offset: 0x0001F368
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00020FAC File Offset: 0x0001F3AC
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				return TaskStatus.Failure;
			}
			this.hashID = Animator.StringToHash(this.paramaterName.Value);
			bool @bool = this.animator.GetBool(this.hashID);
			this.animator.SetBool(this.hashID, this.boolValue.Value);
			if (this.setOnce)
			{
				base.StartCoroutine(this.ResetValue(@bool));
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x0002102C File Offset: 0x0001F42C
		public IEnumerator ResetValue(bool origVale)
		{
			yield return null;
			this.animator.SetBool(this.hashID, origVale);
			yield break;
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x0002104E File Offset: 0x0001F44E
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.paramaterName = string.Empty;
			this.boolValue = false;
		}

		// Token: 0x04000525 RID: 1317
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000526 RID: 1318
		[Tooltip("The name of the parameter")]
		public SharedString paramaterName;

		// Token: 0x04000527 RID: 1319
		[Tooltip("The value of the bool parameter")]
		public SharedBool boolValue;

		// Token: 0x04000528 RID: 1320
		[Tooltip("Should the value be reverted back to its original value after it has been set?")]
		public bool setOnce;

		// Token: 0x04000529 RID: 1321
		private int hashID;

		// Token: 0x0400052A RID: 1322
		private Animator animator;

		// Token: 0x0400052B RID: 1323
		private GameObject prevGameObject;
	}
}
