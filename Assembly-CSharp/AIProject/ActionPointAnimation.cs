using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000BD2 RID: 3026
	public class ActionPointAnimation : ActionPointComponentBase
	{
		// Token: 0x170011A8 RID: 4520
		// (get) Token: 0x06005CC0 RID: 23744 RVA: 0x002748DC File Offset: 0x00272CDC
		public Animator Animator
		{
			[CompilerGenerated]
			get
			{
				return this._animator;
			}
		}

		// Token: 0x06005CC1 RID: 23745 RVA: 0x002748E4 File Offset: 0x00272CE4
		protected override void OnStart()
		{
			this._animator = ActionPointAnimData.AnimationItemTable[this._id];
		}

		// Token: 0x06005CC2 RID: 23746 RVA: 0x002748FC File Offset: 0x00272CFC
		protected void PlayAnimation(string stateName, int layer, float normalizedTime)
		{
			if (global::Debug.isDebugBuild)
			{
			}
			this.Animator.Play(stateName, layer, normalizedTime);
		}

		// Token: 0x06005CC3 RID: 23747 RVA: 0x00274916 File Offset: 0x00272D16
		protected void CrossFadeAnimation(string stateName, float fadeTime, int layer, float fixedTimeOffset)
		{
			if (global::Debug.isDebugBuild)
			{
			}
			this.Animator.CrossFadeInFixedTime(stateName, fadeTime, layer, fixedTimeOffset);
		}

		// Token: 0x04005358 RID: 21336
		[SerializeField]
		protected int _id;

		// Token: 0x04005359 RID: 21337
		[SerializeField]
		protected Animator _animator;
	}
}
