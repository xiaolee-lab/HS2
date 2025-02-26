using System;
using AIProject.Definitions;
using BehaviorDesigner.Runtime.Tasks;

namespace AIProject.MerchantBehavior
{
	// Token: 0x02000DC9 RID: 3529
	[TaskCategory("商人")]
	public class CanLesbianWithPartner : MerchantConditional
	{
		// Token: 0x06006D7E RID: 28030 RVA: 0x002EA990 File Offset: 0x002E8D90
		public override void OnAwake()
		{
			base.OnAwake();
			this._merchant = base.Merchant;
		}

		// Token: 0x06006D7F RID: 28031 RVA: 0x002EA9A4 File Offset: 0x002E8DA4
		public override TaskStatus OnUpdate()
		{
			if (this._merchant == null)
			{
				return TaskStatus.Failure;
			}
			Actor partner = this._merchant.Partner;
			AgentActor agentActor = partner as AgentActor;
			bool flag = agentActor != null;
			if (flag)
			{
				flag = (agentActor.Partner == this._merchant);
			}
			if (flag)
			{
				Desire.ActionType mode = agentActor.Mode;
				switch (mode)
				{
				case Desire.ActionType.GotoLesbianSpot:
				case Desire.ActionType.EndTaskLesbianH:
					break;
				default:
					if (mode != Desire.ActionType.EndTaskLesbianMerchantH)
					{
						flag = false;
						goto IL_83;
					}
					break;
				}
				flag = true;
			}
			IL_83:
			return (!flag) ? TaskStatus.Failure : TaskStatus.Success;
		}

		// Token: 0x04005B42 RID: 23362
		private MerchantActor _merchant;
	}
}
