using System;
using Manager;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000BDD RID: 3037
	[RequireComponent(typeof(ActionPoint))]
	public class HousingChestAnimation : ChestAnimation
	{
		// Token: 0x06005CFA RID: 23802 RVA: 0x00275B24 File Offset: 0x00273F24
		protected override void OnStart()
		{
			if (this._animator != null)
			{
				int chestAnimatorID = Singleton<Manager.Resources>.Instance.CommonDefine.ItemAnims.ChestAnimatorID;
				this._animator.runtimeAnimatorController = Singleton<Manager.Resources>.Instance.Animation.GetItemAnimator(chestAnimatorID);
				string chestDefaultState = Singleton<Manager.Resources>.Instance.CommonDefine.ItemAnims.ChestDefaultState;
				this._animator.Play(chestDefaultState);
			}
		}
	}
}
