using System;
using AIProject.SaveData;

namespace AIProject.UI.Recycling
{
	// Token: 0x02000EAB RID: 3755
	public class ExtraPadding : ItemNodeUI.ExtraData
	{
		// Token: 0x06007A62 RID: 31330 RVA: 0x003374B3 File Offset: 0x003358B3
		public ExtraPadding(StuffItem item, ItemListController source)
		{
			this.item = item;
			this.source = source;
		}

		// Token: 0x1700180C RID: 6156
		// (get) Token: 0x06007A63 RID: 31331 RVA: 0x003374C9 File Offset: 0x003358C9
		public StuffItem item { get; }

		// Token: 0x1700180D RID: 6157
		// (get) Token: 0x06007A64 RID: 31332 RVA: 0x003374D1 File Offset: 0x003358D1
		public ItemListController source { get; }
	}
}
