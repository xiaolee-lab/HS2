using System;

namespace Battlehub.UIControls
{
	// Token: 0x02000082 RID: 130
	public class SelectionChangedArgs : EventArgs
	{
		// Token: 0x06000159 RID: 345 RVA: 0x0000B45A File Offset: 0x0000985A
		public SelectionChangedArgs(object[] oldItems, object[] newItems)
		{
			this.OldItems = oldItems;
			this.NewItems = newItems;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000B470 File Offset: 0x00009870
		public SelectionChangedArgs(object oldItem, object newItem)
		{
			this.OldItems = new object[]
			{
				oldItem
			};
			this.NewItems = new object[]
			{
				newItem
			};
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600015B RID: 347 RVA: 0x0000B498 File Offset: 0x00009898
		// (set) Token: 0x0600015C RID: 348 RVA: 0x0000B4A0 File Offset: 0x000098A0
		public object[] OldItems { get; private set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600015D RID: 349 RVA: 0x0000B4A9 File Offset: 0x000098A9
		// (set) Token: 0x0600015E RID: 350 RVA: 0x0000B4B1 File Offset: 0x000098B1
		public object[] NewItems { get; private set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600015F RID: 351 RVA: 0x0000B4BA File Offset: 0x000098BA
		public object OldItem
		{
			get
			{
				if (this.OldItems == null)
				{
					return null;
				}
				if (this.OldItems.Length == 0)
				{
					return null;
				}
				return this.OldItems[0];
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000160 RID: 352 RVA: 0x0000B4E0 File Offset: 0x000098E0
		public object NewItem
		{
			get
			{
				if (this.NewItems == null)
				{
					return null;
				}
				if (this.NewItems.Length == 0)
				{
					return null;
				}
				return this.NewItems[0];
			}
		}
	}
}
