using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AIProject.SaveData;
using BehaviorDesigner.Runtime.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000D9C RID: 3484
	public abstract class MerchantSetPoint : MerchantAction
	{
		// Token: 0x17001526 RID: 5414
		// (get) Token: 0x06006CB2 RID: 27826 RVA: 0x002E6A6C File Offset: 0x002E4E6C
		// (set) Token: 0x06006CB3 RID: 27827 RVA: 0x002E6A74 File Offset: 0x002E4E74
		private protected MerchantActor.MerchantSchedule schedule { protected get; private set; }

		// Token: 0x17001527 RID: 5415
		// (get) Token: 0x06006CB4 RID: 27828 RVA: 0x002E6A7D File Offset: 0x002E4E7D
		// (set) Token: 0x06006CB5 RID: 27829 RVA: 0x002E6A85 File Offset: 0x002E4E85
		private protected bool ChangedSchedule { protected get; private set; }

		// Token: 0x17001528 RID: 5416
		// (get) Token: 0x06006CB6 RID: 27830 RVA: 0x002E6A8E File Offset: 0x002E4E8E
		protected bool ActiveAgent
		{
			[CompilerGenerated]
			get
			{
				return base.Merchant.NavMeshAgent.isActiveAndEnabled && base.Merchant.NavMeshAgent.isOnNavMesh;
			}
		}

		// Token: 0x06006CB7 RID: 27831 RVA: 0x002E6AB8 File Offset: 0x002E4EB8
		public override void OnAwake()
		{
			base.OnAwake();
			this.navMeshPath = new NavMeshPath();
		}

		// Token: 0x06006CB8 RID: 27832 RVA: 0x002E6ACC File Offset: 0x002E4ECC
		public override void OnStart()
		{
			base.OnStart();
			this.Success = false;
			this.EndFlag.Value = false;
			this.StopSetting();
			this.merchantPoints.Clear();
			if (this.ActiveAgent)
			{
				base.Merchant.NavMeshAgent.ResetPath();
			}
			if (base.Merchant.MerchantPoints.IsNullOrEmpty<MerchantPoint>())
			{
				return;
			}
			this.ChangedSchedule = (base.Merchant.MerchantData.CurrentSchedule != this.schedule);
			this.schedule = base.Merchant.MerchantData.CurrentSchedule;
			IEnumerator _coroutine = this.NextPointSettingCoroutine();
			this.nextPointSettingDisposable = Observable.FromCoroutine(() => _coroutine, false).TakeUntilDestroy(base.Merchant.gameObject).Subscribe(delegate(Unit _)
			{
			}, delegate()
			{
				this.EndFlag.Value = true;
			});
		}

		// Token: 0x06006CB9 RID: 27833
		protected abstract IEnumerator NextPointSettingCoroutine();

		// Token: 0x06006CBA RID: 27834 RVA: 0x002E6BDC File Offset: 0x002E4FDC
		public override TaskStatus OnUpdate()
		{
			if (!this.Success && !this.EndFlag.Value && this.nextPointSettingDisposable != null)
			{
				return TaskStatus.Running;
			}
			if (this.Success)
			{
				MerchantData merchantData = base.Merchant.MerchantData;
				merchantData.PointPosition = ((!(base.TargetInSightMerchantPoint != null)) ? new Vector3(-999f, -999f, -999f) : base.TargetInSightMerchantPoint.transform.position);
				merchantData.MainPointPosition = ((!(base.MainTargetInSightMerchantPoint != null)) ? new Vector3(-999f, -999f, -999f) : base.MainTargetInSightMerchantPoint.transform.position);
				return TaskStatus.Success;
			}
			return TaskStatus.Failure;
		}

		// Token: 0x06006CBB RID: 27835 RVA: 0x002E6CAB File Offset: 0x002E50AB
		protected void StopSetting()
		{
			if (this.nextPointSettingDisposable != null)
			{
				this.nextPointSettingDisposable.Dispose();
			}
			this.nextPointSettingDisposable = null;
		}

		// Token: 0x06006CBC RID: 27836 RVA: 0x002E6CCC File Offset: 0x002E50CC
		public override void OnEnd()
		{
			this.StopSetting();
			base.OnEnd();
		}

		// Token: 0x04005AFC RID: 23292
		private IDisposable nextPointSettingDisposable;

		// Token: 0x04005AFD RID: 23293
		protected bool Success;

		// Token: 0x04005AFE RID: 23294
		protected List<MerchantPoint> merchantPoints = new List<MerchantPoint>();

		// Token: 0x04005AFF RID: 23295
		protected NavMeshPath navMeshPath;

		// Token: 0x04005B00 RID: 23296
		protected ReactiveProperty<bool> EndFlag = new ReactiveProperty<bool>(false);
	}
}
