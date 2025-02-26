using System;
using Manager;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000BDB RID: 3035
	public class HousingActionPointAnimation : ActionPointAnimation
	{
		// Token: 0x06005CF4 RID: 23796 RVA: 0x00275AAB File Offset: 0x00273EAB
		protected override void OnStart()
		{
			if (this._animator != null)
			{
				this._animator.runtimeAnimatorController = Singleton<Manager.Resources>.Instance.Animation.GetItemAnimator(this._animatorID);
			}
		}

		// Token: 0x04005370 RID: 21360
		[SerializeField]
		protected int _animatorID;
	}
}
