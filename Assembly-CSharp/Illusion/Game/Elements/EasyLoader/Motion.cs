using System;
using UnityEngine;

namespace Illusion.Game.Elements.EasyLoader
{
	// Token: 0x020007A3 RID: 1955
	[Serializable]
	public class Motion : BaseMotion
	{
		// Token: 0x06002E4A RID: 11850 RVA: 0x00105FED File Offset: 0x001043ED
		public Motion()
		{
		}

		// Token: 0x06002E4B RID: 11851 RVA: 0x00105FF5 File Offset: 0x001043F5
		public Motion(string bundle, string asset) : base(bundle, asset)
		{
		}

		// Token: 0x06002E4C RID: 11852 RVA: 0x00105FFF File Offset: 0x001043FF
		public Motion(string bundle, string asset, string state) : base(bundle, asset, state)
		{
		}

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x06002E4D RID: 11853 RVA: 0x0010600A File Offset: 0x0010440A
		private static int[] defaultLayers { get; } = new int[1];

		// Token: 0x06002E4E RID: 11854 RVA: 0x00106011 File Offset: 0x00104411
		public virtual bool Setting(Animator animator)
		{
			return this.Setting(animator, this.bundle, this.asset, this.state, false);
		}

		// Token: 0x06002E4F RID: 11855 RVA: 0x00106030 File Offset: 0x00104430
		public virtual bool Setting(Animator animator, string bundle, string asset, string state, bool isCheck)
		{
			if (isCheck && !base.Check(bundle, asset, state))
			{
				return false;
			}
			bool result = false;
			if (!asset.IsNullOrEmpty())
			{
				this.asset = asset;
				if (!bundle.IsNullOrEmpty())
				{
					this.bundle = bundle;
				}
				this.LoadAnimator(animator);
				result = true;
			}
			if (!state.IsNullOrEmpty())
			{
				this.state = state;
				result = true;
			}
			return result;
		}

		// Token: 0x06002E50 RID: 11856 RVA: 0x0010609B File Offset: 0x0010449B
		public void LoadAnimator(Animator animator, string bundle, string asset)
		{
			new Motion(bundle, asset).LoadAnimator(animator);
		}

		// Token: 0x06002E51 RID: 11857 RVA: 0x001060AA File Offset: 0x001044AA
		public void LoadAnimator(Animator animator)
		{
			if (this.isEmpty)
			{
				return;
			}
			animator.runtimeAnimatorController = this.GetAsset<RuntimeAnimatorController>();
			this.UnloadBundle(false, false);
		}

		// Token: 0x06002E52 RID: 11858 RVA: 0x001060CC File Offset: 0x001044CC
		public void Play(Animator animator)
		{
			if (!animator.gameObject.activeInHierarchy)
			{
				return;
			}
			int[] array = (!this.layers.IsNullOrEmpty<int>()) ? this.layers : Motion.defaultLayers;
			if (!this.isCrossFade)
			{
				foreach (int layer in array)
				{
					animator.Play(this.state, layer, this.normalizedTime);
				}
			}
			else
			{
				foreach (int layer2 in array)
				{
					animator.CrossFade(this.state, this.transitionDuration, layer2, this.normalizedTime);
				}
			}
		}

		// Token: 0x04002D37 RID: 11575
		public bool isCrossFade;

		// Token: 0x04002D38 RID: 11576
		public float transitionDuration;

		// Token: 0x04002D39 RID: 11577
		public float normalizedTime;

		// Token: 0x04002D3A RID: 11578
		public int[] layers;
	}
}
