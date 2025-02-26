using System;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000DDA RID: 3546
	[RequireComponent(typeof(ActorLocomotion))]
	public class MerchantController : ActorController
	{
		// Token: 0x06006DAD RID: 28077 RVA: 0x002EB138 File Offset: 0x002E9538
		protected override void Start()
		{
			base.Start();
			(from _ in Observable.EveryUpdate().TakeUntilDestroy(base.gameObject)
			where base.isActiveAndEnabled
			select _).Subscribe(delegate(long _)
			{
				this.OnUpdate();
			});
		}

		// Token: 0x06006DAE RID: 28078 RVA: 0x002EB173 File Offset: 0x002E9573
		private void OnDisable()
		{
			if (this._character != null)
			{
				this._character.enabled = false;
			}
		}

		// Token: 0x06006DAF RID: 28079 RVA: 0x002EB192 File Offset: 0x002E9592
		public override void StartBehavior()
		{
			if (this._character == null)
			{
				this._character = base.GetComponent<ActorLocomotionMerchant>();
			}
		}

		// Token: 0x06006DB0 RID: 28080 RVA: 0x002EB1B4 File Offset: 0x002E95B4
		private void OnUpdate()
		{
			if (this._actor == null || !this._actor.IsInit)
			{
				return;
			}
			if (this._character != null)
			{
				this._character.Move(Vector3.zero);
			}
			if (Singleton<Scene>.Instance.IsNowLoadingFade)
			{
				return;
			}
		}

		// Token: 0x06006DB1 RID: 28081 RVA: 0x002EB210 File Offset: 0x002E9610
		protected override void SubFixedUpdate()
		{
			if (this._character != null)
			{
				this._character.UpdateState();
			}
		}

		// Token: 0x06006DB2 RID: 28082 RVA: 0x002EB22A File Offset: 0x002E962A
		public override void ChangeState(string stateName)
		{
		}

		// Token: 0x04005B4D RID: 23373
		[SerializeField]
		private ActorLocomotionMerchant _character;
	}
}
