using System;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DB9 RID: 3513
	[TaskCategory("商人")]
	public class DelayActivateAgent : MerchantAction
	{
		// Token: 0x06006D41 RID: 27969 RVA: 0x002E87E0 File Offset: 0x002E6BE0
		public override void OnStart()
		{
			base.OnStart();
			this.possible = false;
			this.unnecessary = base.Merchant.IsActiveAgnet;
			if (this.checkUnnecessary && this.unnecessary)
			{
				return;
			}
			base.Merchant.DeactivateNavMeshObjstacle();
			this.disposable = Observable.Timer(TimeSpan.FromSeconds((double)Singleton<Manager.Resources>.Instance.MerchantProfile.ActivateNavMeshElementDelayTime)).Subscribe(delegate(long _)
			{
				this.possible = true;
			}, delegate()
			{
				this.possible = true;
			});
		}

		// Token: 0x06006D42 RID: 27970 RVA: 0x002E886A File Offset: 0x002E6C6A
		public override TaskStatus OnUpdate()
		{
			if (this.checkUnnecessary && this.unnecessary)
			{
				return TaskStatus.Success;
			}
			if (this.possible)
			{
				base.Merchant.ActivateNavMeshAgent();
				return TaskStatus.Success;
			}
			return TaskStatus.Running;
		}

		// Token: 0x06006D43 RID: 27971 RVA: 0x002E889D File Offset: 0x002E6C9D
		public override void OnEnd()
		{
			if (this.disposable != null)
			{
				this.disposable.Dispose();
			}
			base.OnEnd();
		}

		// Token: 0x04005B2C RID: 23340
		[SerializeField]
		private bool checkUnnecessary = true;

		// Token: 0x04005B2D RID: 23341
		private bool possible;

		// Token: 0x04005B2E RID: 23342
		private bool unnecessary;

		// Token: 0x04005B2F RID: 23343
		private IDisposable disposable;
	}
}
