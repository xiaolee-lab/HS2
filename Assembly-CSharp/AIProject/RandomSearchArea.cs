using System;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Manager;

namespace AIProject
{
	// Token: 0x02000D06 RID: 3334
	public class RandomSearchArea : AgentAction
	{
		// Token: 0x06006B09 RID: 27401 RVA: 0x002DBD48 File Offset: 0x002DA148
		public override TaskStatus OnUpdate()
		{
			AgentActor agent = base.Agent;
			List<SearchAreaProbabilities.AddProb> list = ListPool<SearchAreaProbabilities.AddProb>.Get();
			StatusProfile statusProfile = Singleton<Resources>.Instance.StatusProfile;
			if (agent.ChaControl.fileGameInfo.normalSkill.ContainsValue(10))
			{
				list.Add(new SearchAreaProbabilities.AddProb(6, statusProfile.FishingSearchProbBuff));
			}
			if (agent.ChaControl.fileGameInfo.normalSkill.ContainsValue(18))
			{
				list.Add(new SearchAreaProbabilities.AddProb(0, statusProfile.HandSearchProbBuff));
			}
			if (agent.ChaControl.fileGameInfo.normalSkill.ContainsValue(42))
			{
				list.Add(new SearchAreaProbabilities.AddProb(4, statusProfile.PickelSearchProbBuff));
			}
			if (agent.ChaControl.fileGameInfo.normalSkill.ContainsValue(47))
			{
				list.Add(new SearchAreaProbabilities.AddProb(3, statusProfile.ShovelSearchProbBuff));
			}
			if (agent.ChaControl.fileGameInfo.normalSkill.ContainsValue(48))
			{
				list.Add(new SearchAreaProbabilities.AddProb(5, statusProfile.NetSearchProbBuff));
			}
			Dictionary<int, bool> dictionary = DictionaryPool<int, bool>.Get();
			dictionary[0] = true;
			dictionary[3] = (agent.AgentData.EquipedShovelItem.ID > -1);
			dictionary[4] = (agent.AgentData.EquipedPickelItem.ID > -1);
			dictionary[5] = (agent.AgentData.EquipedNetItem.ID > -1);
			dictionary[6] = (agent.AgentData.EquipedFishingItem.ID > -1);
			if (list.Count > 0)
			{
				agent.SearchAreaID = Singleton<Resources>.Instance.CommonDefine.ProbSearchAreaProfile.Lottery(dictionary, list);
			}
			else
			{
				agent.SearchAreaID = Singleton<Resources>.Instance.CommonDefine.ProbSearchAreaProfile.Lottery(dictionary, null);
			}
			DictionaryPool<int, bool>.Release(dictionary);
			ListPool<SearchAreaProbabilities.AddProb>.Release(list);
			return TaskStatus.Success;
		}
	}
}
