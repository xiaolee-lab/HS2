using System;
using BehaviorDesigner.Runtime.Tasks;
using Manager;
using UniRx;
using UnityEngine;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DBA RID: 3514
	[TaskCategory("商人")]
	public class DelayActivateObstacle : MerchantAction
	{
		// Token: 0x06006D47 RID: 27975 RVA: 0x002E88E0 File Offset: 0x002E6CE0
		public override void OnStart()
		{
			base.OnStart();
			this.possible = false;
			this.unnecessary = base.Merchant.IsActiveObstacle;
			if (this.checkUnnecessary && this.unnecessary)
			{
				return;
			}
			this.disposable = Observable.Timer(TimeSpan.FromSeconds((double)Singleton<Manager.Resources>.Instance.MerchantProfile.ActivateNavMeshElementDelayTime)).Subscribe(delegate(long _)
			{
				this.possible = true;
			}, delegate()
			{
				this.possible = true;
			});
		}

		// Token: 0x06006D48 RID: 27976 RVA: 0x002E895F File Offset: 0x002E6D5F
		public override TaskStatus OnUpdate()
		{
			if (this.checkUnnecessary && this.unnecessary)
			{
				return TaskStatus.Success;
			}
			if (this.possible)
			{
				base.Merchant.ActivateNavMeshObstacle(base.Merchant.Position);
				return TaskStatus.Success;
			}
			return TaskStatus.Running;
		}

		// Token: 0x06006D49 RID: 27977 RVA: 0x002E899D File Offset: 0x002E6D9D
		public override void OnEnd()
		{
			if (this.disposable != null)
			{
				this.disposable.Dispose();
			}
			base.OnEnd();
		}

		// Token: 0x04005B30 RID: 23344
		[SerializeField]
		private bool checkUnnecessary = true;

		// Token: 0x04005B31 RID: 23345
		private bool possible;

		// Token: 0x04005B32 RID: 23346
		private bool unnecessary;

		// Token: 0x04005B33 RID: 23347
		private IDisposable disposable;
	}
}
