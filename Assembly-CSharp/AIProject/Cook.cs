using System;
using System.Collections.Generic;
using AIProject.Definitions;
using AIProject.SaveData;
using AIProject.UI;
using BehaviorDesigner.Runtime.Tasks;
using Manager;

namespace AIProject
{
	// Token: 0x02000C9C RID: 3228
	[TaskCategory("")]
	public class Cook : AgentStateAction
	{
		// Token: 0x06006925 RID: 26917 RVA: 0x002CB495 File Offset: 0x002C9895
		public override void OnStart()
		{
			base.Agent.EventKey = EventType.Cook;
			this._continueCookSeq = this.CalcTargetRecipe();
			if (!this._continueCookSeq)
			{
				return;
			}
			base.OnStart();
		}

		// Token: 0x06006926 RID: 26918 RVA: 0x002CB4C8 File Offset: 0x002C98C8
		public override TaskStatus OnUpdate()
		{
			if (!this._continueCookSeq)
			{
				int desireKey = Desire.GetDesireKey(Desire.Type.Cook);
				base.Agent.SetDesire(desireKey, 0f);
				return TaskStatus.Success;
			}
			return base.OnUpdate();
		}

		// Token: 0x06006927 RID: 26919 RVA: 0x002CB508 File Offset: 0x002C9908
		protected override void OnCompletedStateTask()
		{
			AgentActor agent = base.Agent;
			int desireKey = Desire.GetDesireKey(Desire.Type.Cook);
			agent.SetDesire(desireKey, 0f);
			if (this._createItem != null)
			{
				StuffItemInfo item = Singleton<Resources>.Instance.GameInfo.GetItem(this._createItem.item.CategoryID, this._createItem.item.ID);
				MapUIContainer.AddItemLog(agent, item, this._createItem.info.CreateSum, false);
				this._createItem = null;
			}
			agent.AgentData.SetAppendEventFlagCheck(4, true);
		}

		// Token: 0x06006928 RID: 26920 RVA: 0x002CB59C File Offset: 0x002C999C
		private bool CalcTargetRecipe()
		{
			AgentActor agent = base.Agent;
			List<StuffItem> itemList = agent.AgentData.ItemList;
			this._createItem = CraftUI.CreateCheck(Singleton<Resources>.Instance.GameInfo.recipe.cookTable, new List<StuffItem>[]
			{
				itemList,
				Singleton<Game>.Instance.Environment.ItemListInPantry
			});
			int num = agent.ChaControl.fileGameInfo.flavorState[0];
			bool chef = agent.ChaControl.fileGameInfo.normalSkill.ContainsValue(0);
			return CraftUI.CreateCooking(this._createItem, itemList, (float)num, chef);
		}

		// Token: 0x0400598E RID: 22926
		private bool _continueCookSeq;

		// Token: 0x0400598F RID: 22927
		private CraftUI.CreateItem _createItem;
	}
}
