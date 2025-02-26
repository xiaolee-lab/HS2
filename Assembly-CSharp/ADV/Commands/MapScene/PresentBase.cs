using System;
using AIChara;
using AIProject;
using Manager;

namespace ADV.Commands.MapScene
{
	// Token: 0x02000754 RID: 1876
	public abstract class PresentBase : CommandBase
	{
		// Token: 0x06002C3D RID: 11325
		protected abstract Resources.GameInfoTables.AdvPresentItemInfo GetAdvPresentItemInfo(ChaControl chaCtrl);

		// Token: 0x06002C3E RID: 11326 RVA: 0x000FE7F0 File Offset: 0x000FCBF0
		protected void SetPresentInfo(ref int cnt)
		{
			CharaData chara = this.GetChara(ref cnt);
			if (chara == null)
			{
				return;
			}
			Resources.GameInfoTables.AdvPresentItemInfo advPresentItemInfo = this.GetAdvPresentItemInfo(chara.chaCtrl);
			if (advPresentItemInfo == null)
			{
				return;
			}
			int eventItemID = advPresentItemInfo.eventItemID;
			string name = advPresentItemInfo.itemInfo.Name;
			int nameHash = advPresentItemInfo.itemInfo.nameHash;
			PlayState autoPlayState = chara.GetAutoPlayState();
			for (int i = 0; i < autoPlayState.ItemInfoCount; i++)
			{
				ActionItemInfo eventItemInfo;
				if (Singleton<Resources>.Instance.Map.EventItemList.TryGetValue(eventItemID, out eventItemInfo))
				{
					chara.data.actor.LoadEventItem(eventItemID, autoPlayState.GetItemInfo(i), eventItemInfo);
				}
			}
			base.scenario.Vars["Item"] = new ValData(name);
			base.scenario.Vars[string.Format("{0}.{1}", "Item", "Hash")] = new ValData(nameHash);
		}
	}
}
