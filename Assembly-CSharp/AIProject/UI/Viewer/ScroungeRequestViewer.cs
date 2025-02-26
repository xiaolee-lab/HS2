using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using AIProject.SaveData;
using UnityEngine;

namespace AIProject.UI.Viewer
{
	// Token: 0x02000EAE RID: 3758
	public class ScroungeRequestViewer : MonoBehaviour
	{
		// Token: 0x17001814 RID: 6164
		// (get) Token: 0x06007A8A RID: 31370 RVA: 0x00338095 File Offset: 0x00336495
		public ItemListUI itemListUI
		{
			[CompilerGenerated]
			get
			{
				return this._itemListUI;
			}
		}

		// Token: 0x17001815 RID: 6165
		// (get) Token: 0x06007A8B RID: 31371 RVA: 0x0033809D File Offset: 0x0033649D
		public ShopViewer.ItemListController controller { get; } = new ShopViewer.ItemListController();

		// Token: 0x17001816 RID: 6166
		// (get) Token: 0x06007A8C RID: 31372 RVA: 0x003380A5 File Offset: 0x003364A5
		// (set) Token: 0x06007A8D RID: 31373 RVA: 0x003380AD File Offset: 0x003364AD
		public AgentActor agent { get; set; }

		// Token: 0x17001817 RID: 6167
		// (get) Token: 0x06007A8E RID: 31374 RVA: 0x003380B6 File Offset: 0x003364B6
		public ItemScrounge itemScrounge
		{
			[CompilerGenerated]
			get
			{
				return this.agent.AgentData.ItemScrounge;
			}
		}

		// Token: 0x06007A8F RID: 31375 RVA: 0x003380C8 File Offset: 0x003364C8
		public bool Check(IReadOnlyCollection<StuffItem> itemList)
		{
			if (!itemList.Any<StuffItem>())
			{
				return false;
			}
			List<StuffItem> list = (from item in itemList
			select new StuffItem(item)).ToList<StuffItem>();
			foreach (StuffItem item3 in this.itemScrounge.ItemList)
			{
				list.RemoveItem(item3);
			}
			if (list.Any<StuffItem>())
			{
				return false;
			}
			List<StuffItem> list2 = (from item in this.itemScrounge.ItemList
			select new StuffItem(item)).ToList<StuffItem>();
			foreach (StuffItem item2 in itemList)
			{
				list2.RemoveItem(item2);
			}
			return !list2.Any<StuffItem>();
		}

		// Token: 0x17001818 RID: 6168
		// (get) Token: 0x06007A90 RID: 31376 RVA: 0x003381FC File Offset: 0x003365FC
		// (set) Token: 0x06007A91 RID: 31377 RVA: 0x00338204 File Offset: 0x00336604
		public bool initialized { get; private set; }

		// Token: 0x06007A92 RID: 31378 RVA: 0x0033820D File Offset: 0x0033660D
		private void Awake()
		{
			this.controller.Bind(this._itemListUI);
		}

		// Token: 0x06007A93 RID: 31379 RVA: 0x00338220 File Offset: 0x00336620
		private IEnumerator Start()
		{
			this.initialized = true;
			yield break;
		}

		// Token: 0x04006269 RID: 25193
		[SerializeField]
		private ItemListUI _itemListUI;
	}
}
