using System;
using AIProject.SaveData;
using Manager;

namespace AIProject.UI
{
	// Token: 0x02000E89 RID: 3721
	public class ItemInfoRemoveUI : ItemInfoUI
	{
		// Token: 0x06007732 RID: 30514 RVA: 0x00327E54 File Offset: 0x00326254
		public override void Refresh(StuffItem item)
		{
			StuffItemInfo item2 = Singleton<Resources>.Instance.GameInfo.GetItem(item.CategoryID, item.ID);
			this._itemName.text = item2.Name;
			this._flavorText.text = item2.Explanation;
			if (this._infoLayout != null)
			{
				this._infoLayout.SetActive(item2.isTrash);
			}
			this.Refresh(item.Count);
		}
	}
}
