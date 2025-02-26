using System;
using System.Collections.Generic;
using AIProject;
using AIProject.SaveData;
using Manager;

namespace ADV.Commands.MapScene
{
	// Token: 0x02000749 RID: 1865
	internal static class Extension
	{
		// Token: 0x06002C0B RID: 11275 RVA: 0x000FDC8C File Offset: 0x000FC08C
		public static bool AddItemVars(this TextScenario self, StuffItem Item)
		{
			Dictionary<string, ValData> vars = self.Vars;
			string text = "Item";
			StuffItemInfo stuffItemInfo = null;
			if (Item != null && Singleton<Resources>.IsInstance())
			{
				stuffItemInfo = Singleton<Resources>.Instance.GameInfo.GetItem(Item.CategoryID, Item.ID);
			}
			vars[text] = new ValData(((stuffItemInfo != null) ? stuffItemInfo.Name : null) ?? string.Empty);
			Dictionary<string, ValData> dictionary = vars;
			string key = string.Format("{0}.Hash", text);
			int? num = (stuffItemInfo != null) ? new int?(stuffItemInfo.nameHash) : null;
			dictionary[key] = new ValData((num == null) ? -1 : num.Value);
			if (Item == null)
			{
				return false;
			}
			vars[string.Format("{0}.{1}", text, "CategoryID")] = new ValData(Item.CategoryID);
			vars[string.Format("{0}.{1}", text, "ID")] = new ValData(Item.ID);
			vars[string.Format("{0}.{1}", text, "Count")] = new ValData(Item.Count);
			return true;
		}

		// Token: 0x06002C0C RID: 11276 RVA: 0x000FDDC8 File Offset: 0x000FC1C8
		public static bool AddItemVars(this TextScenario self, StuffItemInfo Item, int Count)
		{
			Dictionary<string, ValData> vars = self.Vars;
			string text = "Item";
			vars[text] = new ValData(((Item != null) ? Item.Name : null) ?? string.Empty);
			Dictionary<string, ValData> dictionary = vars;
			string key = string.Format("{0}.Hash", text);
			int? num = (Item != null) ? new int?(Item.nameHash) : null;
			dictionary[key] = new ValData((num == null) ? -1 : num.Value);
			if (Item == null)
			{
				return false;
			}
			vars[string.Format("{0}.{1}", text, "CategoryID")] = new ValData(Item.CategoryID);
			vars[string.Format("{0}.{1}", text, "ID")] = new ValData(Item.ID);
			vars[string.Format("{0}.{1}", text, "Count")] = new ValData(Count);
			return true;
		}

		// Token: 0x06002C0D RID: 11277 RVA: 0x000FDED0 File Offset: 0x000FC2D0
		public static CharaData GetChara(this CommandBase self, ref int cnt)
		{
			int num = cnt++;
			if (self.args.IsNullOrEmpty(num))
			{
				return null;
			}
			CharaData result = null;
			int no;
			if (!int.TryParse(self.args[num], out no) || (result = self.scenario.commandController.GetChara(no)) == null)
			{
			}
			return result;
		}
	}
}
