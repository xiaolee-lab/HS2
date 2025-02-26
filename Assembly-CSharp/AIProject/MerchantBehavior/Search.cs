using System;
using BehaviorDesigner.Runtime.Tasks;
using UniRx;
using UnityEngine;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DA6 RID: 3494
	[TaskCategory("商人")]
	public class Search : MerchantStateAction
	{
		// Token: 0x06006D08 RID: 27912 RVA: 0x002E7E83 File Offset: 0x002E6283
		public override void OnStart()
		{
			this.counter = 0f;
			base.OnStart();
			this.searchSecondTime = (float)UnityEngine.Random.Range(this.animInfo.loopMinTime, this.animInfo.loopMaxTime);
		}

		// Token: 0x06006D09 RID: 27913 RVA: 0x002E7EB8 File Offset: 0x002E62B8
		public override TaskStatus OnUpdate()
		{
			if (base.Merchant.Animation.PlayingInAnimation)
			{
				return TaskStatus.Running;
			}
			if (!base.Merchant.ElapsedDay)
			{
				if (this.animInfo.isLoop)
				{
					if (this.searchSecondTime < this.counter)
					{
						if (this.onEndAction != null)
						{
							this.onEndAction.OnNext(Unit.Default);
						}
						if (!base.Merchant.Animation.PlayingOutAnimation)
						{
							base.Complete();
							return TaskStatus.Success;
						}
					}
					else
					{
						this.counter += Time.deltaTime;
					}
				}
				else
				{
					if (this.onEndAction != null)
					{
						this.onEndAction.OnNext(Unit.Default);
					}
					if (!base.Merchant.Animation.PlayingOutAnimation)
					{
						base.Complete();
						return TaskStatus.Success;
					}
				}
				return TaskStatus.Running;
			}
			if (this.onEndAction != null)
			{
				this.onEndAction.OnNext(Unit.Default);
			}
			if (base.Merchant.Animation.PlayingOutAnimation)
			{
				return TaskStatus.Running;
			}
			base.Merchant.ElapsedDay = false;
			base.Complete();
			base.Merchant.ChangeNextSchedule();
			base.Merchant.SetCurrentSchedule();
			return TaskStatus.Success;
		}

		// Token: 0x04005B1F RID: 23327
		private float counter;

		// Token: 0x04005B20 RID: 23328
		private float searchSecondTime;
	}
}
