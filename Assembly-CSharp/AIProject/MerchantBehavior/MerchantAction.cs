using System;
using System.Runtime.CompilerServices;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000D9D RID: 3485
	public abstract class MerchantAction : BehaviorDesigner.Runtime.Tasks.Action
	{
		// Token: 0x17001529 RID: 5417
		// (get) Token: 0x06006CBF RID: 27839 RVA: 0x002E6994 File Offset: 0x002E4D94
		protected MerchantActor Merchant
		{
			[CompilerGenerated]
			get
			{
				MerchantActor result;
				if ((result = this._merchant) == null)
				{
					result = (this._merchant = (base.Owner as MerchantBehaviorTree).SourceMerchant);
				}
				return result;
			}
		}

		// Token: 0x1700152A RID: 5418
		// (get) Token: 0x06006CC0 RID: 27840 RVA: 0x002E69C7 File Offset: 0x002E4DC7
		// (set) Token: 0x06006CC1 RID: 27841 RVA: 0x002E69D4 File Offset: 0x002E4DD4
		protected MerchantPoint CurrentMerchantPoint
		{
			get
			{
				return this.Merchant.CurrentMerchantPoint;
			}
			set
			{
				this.Merchant.CurrentMerchantPoint = value;
			}
		}

		// Token: 0x1700152B RID: 5419
		// (get) Token: 0x06006CC2 RID: 27842 RVA: 0x002E69E2 File Offset: 0x002E4DE2
		// (set) Token: 0x06006CC3 RID: 27843 RVA: 0x002E69EF File Offset: 0x002E4DEF
		protected MerchantPoint TargetInSightMerchantPoint
		{
			get
			{
				return this.Merchant.TargetInSightMerchantPoint;
			}
			set
			{
				this.Merchant.TargetInSightMerchantPoint = value;
			}
		}

		// Token: 0x1700152C RID: 5420
		// (get) Token: 0x06006CC4 RID: 27844 RVA: 0x002E69FD File Offset: 0x002E4DFD
		// (set) Token: 0x06006CC5 RID: 27845 RVA: 0x002E6A0A File Offset: 0x002E4E0A
		protected MerchantPoint MainTargetInSightMerchantPoint
		{
			get
			{
				return this.Merchant.MainTargetInSightMerchantPoint;
			}
			set
			{
				this.Merchant.MainTargetInSightMerchantPoint = value;
			}
		}

		// Token: 0x1700152D RID: 5421
		// (get) Token: 0x06006CC6 RID: 27846 RVA: 0x002E6A18 File Offset: 0x002E4E18
		// (set) Token: 0x06006CC7 RID: 27847 RVA: 0x002E6A25 File Offset: 0x002E4E25
		protected MerchantPoint PrevMerchantPoint
		{
			get
			{
				return this.Merchant.PrevMerchantPoint;
			}
			set
			{
				this.Merchant.PrevMerchantPoint = value;
			}
		}

		// Token: 0x1700152E RID: 5422
		// (get) Token: 0x06006CC8 RID: 27848 RVA: 0x002E6A33 File Offset: 0x002E4E33
		protected MerchantActor.MerchantSchedule CurrentSchedule
		{
			[CompilerGenerated]
			get
			{
				return this.Merchant.CurrentSchedule;
			}
		}

		// Token: 0x1700152F RID: 5423
		// (get) Token: 0x06006CC9 RID: 27849 RVA: 0x002E6A40 File Offset: 0x002E4E40
		protected MerchantActor.MerchantSchedule PrevSchedule
		{
			[CompilerGenerated]
			get
			{
				return this.Merchant.PrevSchedule;
			}
		}

		// Token: 0x04005B04 RID: 23300
		private MerchantActor _merchant;
	}
}
