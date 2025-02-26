using System;
using System.Collections.Generic;
using AIProject.SaveData;
using AIProject.Scene;
using Manager;

namespace AIProject.UI
{
	// Token: 0x02000E7F RID: 3711
	public class InventoryUIController : InventoryFilterUIController
	{
		// Token: 0x17001715 RID: 5909
		// (get) Token: 0x0600767B RID: 30331 RVA: 0x003236F4 File Offset: 0x00321AF4
		// (set) Token: 0x0600767C RID: 30332 RVA: 0x003236FC File Offset: 0x00321AFC
		public Action<StuffItem> OnSubmit { get; set; }

		// Token: 0x17001716 RID: 5910
		// (get) Token: 0x0600767D RID: 30333 RVA: 0x00323705 File Offset: 0x00321B05
		// (set) Token: 0x0600767E RID: 30334 RVA: 0x0032370D File Offset: 0x00321B0D
		public bool isConfirm { get; set; } = true;

		// Token: 0x0600767F RID: 30335 RVA: 0x00323718 File Offset: 0x00321B18
		protected override void ItemInfoEvent()
		{
			int selectIndex = -1;
			ItemNodeUI selectItem = null;
			base.itemListUI.CurrentChanged += delegate(int i, ItemNodeUI option)
			{
				if (option == null)
				{
					return;
				}
				selectIndex = i;
				selectItem = option;
				this.SelectItem(selectItem.Item);
			};
			this._itemInfoUI.OnSubmit += delegate()
			{
				if (!this.isConfirm || selectItem.isNone)
				{
					this.EnterItem(selectIndex, selectItem);
				}
				else
				{
					ConfirmScene.Sentence = this._itemInfoUI.ConfirmLabel;
					ConfirmScene.OnClickedYes = delegate()
					{
						this.EnterItem(selectIndex, selectItem);
						this.playSE.Play(SoundPack.SystemSE.OK_L);
					};
					ConfirmScene.OnClickedNo = delegate()
					{
						this.playSE.Play(SoundPack.SystemSE.Cancel);
					};
					Singleton<Game>.Instance.LoadDialog();
				}
			};
		}

		// Token: 0x06007680 RID: 30336 RVA: 0x00323770 File Offset: 0x00321B70
		private void EnterItem(int selectIndex, ItemNodeUI selectItem)
		{
			StuffItem item = selectItem.Item;
			item.Count -= this._itemInfoUI.Count;
			Action<StuffItem> onSubmit = this.OnSubmit;
			if (onSubmit != null)
			{
				onSubmit(new StuffItem(item.CategoryID, item.ID, this._itemInfoUI.Count));
			}
			bool flag = item.Count <= 0;
			this._itemInfoUI.Refresh(item);
			List<StuffItem> list = base.itemList();
			if (flag)
			{
				list.Remove(item);
				base.itemListUI.RemoveItemNode(selectIndex);
				base.itemListUI.ForceSetNonSelect();
				this._itemInfoUI.Close();
			}
			this._inventoryUI.Refresh();
		}
	}
}
