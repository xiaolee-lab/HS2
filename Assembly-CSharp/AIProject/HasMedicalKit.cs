using System;
using AIProject.SaveData;
using BehaviorDesigner.Runtime.Tasks;
using Manager;

namespace AIProject
{
	// Token: 0x02000D7C RID: 3452
	[TaskCategory("")]
	public class HasMedicalKit : AgentConditional
	{
		// Token: 0x06006C5E RID: 27742 RVA: 0x002E6238 File Offset: 0x002E4638
		public override TaskStatus OnUpdate()
		{
			ItemIDKeyPair gauzeID = Singleton<Resources>.Instance.CommonDefine.ItemIDDefine.GauzeID;
			foreach (StuffItem stuffItem in base.Agent.AgentData.ItemList)
			{
				if (stuffItem.CategoryID == gauzeID.categoryID && stuffItem.ID == gauzeID.itemID)
				{
					return TaskStatus.Success;
				}
			}
			return TaskStatus.Failure;
		}
	}
}
