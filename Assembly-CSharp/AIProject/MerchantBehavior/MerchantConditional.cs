using System;
using System.Runtime.CompilerServices;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000D9E RID: 3486
	public abstract class MerchantConditional : Conditional
	{
		// Token: 0x17001530 RID: 5424
		// (get) Token: 0x06006CCB RID: 27851 RVA: 0x002E6D08 File Offset: 0x002E5108
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

		// Token: 0x17001531 RID: 5425
		// (get) Token: 0x06006CCC RID: 27852 RVA: 0x002E6D3B File Offset: 0x002E513B
		// (set) Token: 0x06006CCD RID: 27853 RVA: 0x002E6D48 File Offset: 0x002E5148
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

		// Token: 0x17001532 RID: 5426
		// (get) Token: 0x06006CCE RID: 27854 RVA: 0x002E6D56 File Offset: 0x002E5156
		// (set) Token: 0x06006CCF RID: 27855 RVA: 0x002E6D63 File Offset: 0x002E5163
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

		// Token: 0x17001533 RID: 5427
		// (get) Token: 0x06006CD0 RID: 27856 RVA: 0x002E6D71 File Offset: 0x002E5171
		// (set) Token: 0x06006CD1 RID: 27857 RVA: 0x002E6D7E File Offset: 0x002E517E
		protected MerchantPoint MainTargetInsightMerchantPoint
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

		// Token: 0x17001534 RID: 5428
		// (get) Token: 0x06006CD2 RID: 27858 RVA: 0x002E6D8C File Offset: 0x002E518C
		// (set) Token: 0x06006CD3 RID: 27859 RVA: 0x002E6D99 File Offset: 0x002E5199
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

		// Token: 0x17001535 RID: 5429
		// (get) Token: 0x06006CD4 RID: 27860 RVA: 0x002E6DA7 File Offset: 0x002E51A7
		protected MerchantActor.MerchantSchedule CurrentSchedule
		{
			[CompilerGenerated]
			get
			{
				return this.Merchant.CurrentSchedule;
			}
		}

		// Token: 0x17001536 RID: 5430
		// (get) Token: 0x06006CD5 RID: 27861 RVA: 0x002E6DB4 File Offset: 0x002E51B4
		protected MerchantActor.MerchantSchedule PrevSchedule
		{
			[CompilerGenerated]
			get
			{
				return this.Merchant.PrevSchedule;
			}
		}

		// Token: 0x04005B05 RID: 23301
		private MerchantActor _merchant;
	}
}
