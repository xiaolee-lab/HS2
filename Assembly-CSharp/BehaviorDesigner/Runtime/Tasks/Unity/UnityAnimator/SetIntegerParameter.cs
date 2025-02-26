using System;
using System.Collections;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x0200011C RID: 284
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Sets the int parameter on an animator. Returns Success.")]
	public class SetIntegerParameter : Action
	{
		// Token: 0x0600062B RID: 1579 RVA: 0x000212DC File Offset: 0x0001F6DC
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x00021320 File Offset: 0x0001F720
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				return TaskStatus.Failure;
			}
			this.hashID = Animator.StringToHash(this.paramaterName.Value);
			int integer = this.animator.GetInteger(this.hashID);
			this.animator.SetInteger(this.hashID, this.intValue.Value);
			if (this.setOnce)
			{
				base.StartCoroutine(this.ResetValue(integer));
			}
			return TaskStatus.Success;
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x000213A0 File Offset: 0x0001F7A0
		public IEnumerator ResetValue(int origVale)
		{
			yield return null;
			this.animator.SetInteger(this.hashID, origVale);
			yield break;
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x000213C2 File Offset: 0x0001F7C2
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.paramaterName = string.Empty;
			this.intValue = 0;
		}

		// Token: 0x04000533 RID: 1331
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x04000534 RID: 1332
		[Tooltip("The name of the parameter")]
		public SharedString paramaterName;

		// Token: 0x04000535 RID: 1333
		[Tooltip("The value of the int parameter")]
		public SharedInt intValue;

		// Token: 0x04000536 RID: 1334
		[Tooltip("Should the value be reverted back to its original value after it has been set?")]
		public bool setOnce;

		// Token: 0x04000537 RID: 1335
		private int hashID;

		// Token: 0x04000538 RID: 1336
		private Animator animator;

		// Token: 0x04000539 RID: 1337
		private GameObject prevGameObject;
	}
}
