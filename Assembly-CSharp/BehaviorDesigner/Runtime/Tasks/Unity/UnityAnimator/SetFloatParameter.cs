using System;
using System.Collections;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
	// Token: 0x0200011B RID: 283
	[TaskCategory("Unity/Animator")]
	[TaskDescription("Sets the float parameter on an animator. Returns Success.")]
	public class SetFloatParameter : Action
	{
		// Token: 0x06000626 RID: 1574 RVA: 0x00021120 File Offset: 0x0001F520
		public override void OnStart()
		{
			GameObject defaultGameObject = base.GetDefaultGameObject(this.targetGameObject.Value);
			if (defaultGameObject != this.prevGameObject)
			{
				this.animator = defaultGameObject.GetComponent<Animator>();
				this.prevGameObject = defaultGameObject;
			}
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00021164 File Offset: 0x0001F564
		public override TaskStatus OnUpdate()
		{
			if (this.animator == null)
			{
				return TaskStatus.Failure;
			}
			this.hashID = Animator.StringToHash(this.paramaterName.Value);
			float @float = this.animator.GetFloat(this.hashID);
			this.animator.SetFloat(this.hashID, this.floatValue.Value);
			if (this.setOnce)
			{
				base.StartCoroutine(this.ResetValue(@float));
			}
			return TaskStatus.Success;
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x000211E4 File Offset: 0x0001F5E4
		public IEnumerator ResetValue(float origVale)
		{
			yield return null;
			this.animator.SetFloat(this.hashID, origVale);
			yield break;
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x00021206 File Offset: 0x0001F606
		public override void OnReset()
		{
			this.targetGameObject = null;
			this.paramaterName = string.Empty;
			this.floatValue = 0f;
		}

		// Token: 0x0400052C RID: 1324
		[Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
		public SharedGameObject targetGameObject;

		// Token: 0x0400052D RID: 1325
		[Tooltip("The name of the parameter")]
		public SharedString paramaterName;

		// Token: 0x0400052E RID: 1326
		[Tooltip("The value of the float parameter")]
		public SharedFloat floatValue;

		// Token: 0x0400052F RID: 1327
		[Tooltip("Should the value be reverted back to its original value after it has been set?")]
		public bool setOnce;

		// Token: 0x04000530 RID: 1328
		private int hashID;

		// Token: 0x04000531 RID: 1329
		private Animator animator;

		// Token: 0x04000532 RID: 1330
		private GameObject prevGameObject;
	}
}
