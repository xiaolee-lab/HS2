using System;
using AIProject.Definitions;
using AIProject.SaveData;
using BehaviorDesigner.Runtime.Tasks;
using Manager;

namespace AIProject
{
	// Token: 0x02000C9F RID: 3231
	[TaskCategory("")]
	public class Drink : AgentStateAction
	{
		// Token: 0x06006930 RID: 26928 RVA: 0x002CB6F8 File Offset: 0x002C9AF8
		public override void OnStart()
		{
			AgentActor agent = base.Agent;
			agent.EventKey = EventType.Drink;
			this._targetItem = agent.SelectDrinkItem();
			base.OnStart();
		}

		// Token: 0x06006931 RID: 26929 RVA: 0x002CB72C File Offset: 0x002C9B2C
		protected override void OnCompletedStateTask()
		{
			int desireKey = Desire.GetDesireKey(Desire.Type.Drink);
			base.Agent.SetDesire(desireKey, 0f);
			AgentActor agent = base.Agent;
			agent.ApplyDrinkParameter(this._targetItem);
			ItemIDKeyPair coconutDrinkID = Singleton<Resources>.Instance.AgentProfile.CoconutDrinkID;
			if (this._targetItem.CategoryID == coconutDrinkID.categoryID && this._targetItem.ID == coconutDrinkID.itemID)
			{
				agent.SetStatus(0, 50f);
			}
			this._targetItem = null;
		}

		// Token: 0x04005990 RID: 22928
		private StuffItem _targetItem;
	}
}
