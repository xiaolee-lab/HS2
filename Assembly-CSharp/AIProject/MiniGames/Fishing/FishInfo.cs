using System;
using System.Runtime.CompilerServices;

namespace AIProject.MiniGames.Fishing
{
	// Token: 0x02000F1E RID: 3870
	public struct FishInfo
	{
		// Token: 0x06007F5A RID: 32602 RVA: 0x0036352C File Offset: 0x0036192C
		public FishInfo(int _categoryID, int _itemID, string _itemName, int _sizeID, int _modelID, int _heartPoint, int _minExPoint, int _maxExPoint, int _tankPointID, float _nicknameHeightOffset)
		{
			this.CategoryID = _categoryID;
			this.ItemID = _itemID;
			this.ItemName = _itemName;
			this.SizeID = _sizeID;
			this.ModelID = _modelID;
			this.HeartPoint = _heartPoint;
			this.MinExPoint = _minExPoint;
			this.MaxExPoint = _maxExPoint;
			this.TankPointID = _tankPointID;
			this.NicknameHeightOffset = _nicknameHeightOffset;
		}

		// Token: 0x17001933 RID: 6451
		// (get) Token: 0x06007F5B RID: 32603 RVA: 0x00363586 File Offset: 0x00361986
		// (set) Token: 0x06007F5C RID: 32604 RVA: 0x0036358E File Offset: 0x0036198E
		public int CategoryID { get; private set; }

		// Token: 0x17001934 RID: 6452
		// (get) Token: 0x06007F5D RID: 32605 RVA: 0x00363597 File Offset: 0x00361997
		// (set) Token: 0x06007F5E RID: 32606 RVA: 0x0036359F File Offset: 0x0036199F
		public int ItemID { get; private set; }

		// Token: 0x17001935 RID: 6453
		// (get) Token: 0x06007F5F RID: 32607 RVA: 0x003635A8 File Offset: 0x003619A8
		// (set) Token: 0x06007F60 RID: 32608 RVA: 0x003635B0 File Offset: 0x003619B0
		public string ItemName { get; private set; }

		// Token: 0x17001936 RID: 6454
		// (get) Token: 0x06007F61 RID: 32609 RVA: 0x003635B9 File Offset: 0x003619B9
		// (set) Token: 0x06007F62 RID: 32610 RVA: 0x003635C1 File Offset: 0x003619C1
		public int SizeID { get; private set; }

		// Token: 0x17001937 RID: 6455
		// (get) Token: 0x06007F63 RID: 32611 RVA: 0x003635CA File Offset: 0x003619CA
		// (set) Token: 0x06007F64 RID: 32612 RVA: 0x003635D2 File Offset: 0x003619D2
		public int ModelID { get; private set; }

		// Token: 0x17001938 RID: 6456
		// (get) Token: 0x06007F65 RID: 32613 RVA: 0x003635DB File Offset: 0x003619DB
		// (set) Token: 0x06007F66 RID: 32614 RVA: 0x003635E3 File Offset: 0x003619E3
		public int HeartPoint { get; private set; }

		// Token: 0x17001939 RID: 6457
		// (get) Token: 0x06007F67 RID: 32615 RVA: 0x003635EC File Offset: 0x003619EC
		// (set) Token: 0x06007F68 RID: 32616 RVA: 0x003635F4 File Offset: 0x003619F4
		public int MinExPoint { get; private set; }

		// Token: 0x1700193A RID: 6458
		// (get) Token: 0x06007F69 RID: 32617 RVA: 0x003635FD File Offset: 0x003619FD
		// (set) Token: 0x06007F6A RID: 32618 RVA: 0x00363605 File Offset: 0x00361A05
		public int MaxExPoint { get; private set; }

		// Token: 0x1700193B RID: 6459
		// (get) Token: 0x06007F6B RID: 32619 RVA: 0x0036360E File Offset: 0x00361A0E
		// (set) Token: 0x06007F6C RID: 32620 RVA: 0x00363616 File Offset: 0x00361A16
		public int TankPointID { get; private set; }

		// Token: 0x1700193C RID: 6460
		// (get) Token: 0x06007F6D RID: 32621 RVA: 0x0036361F File Offset: 0x00361A1F
		// (set) Token: 0x06007F6E RID: 32622 RVA: 0x00363627 File Offset: 0x00361A27
		public float NicknameHeightOffset { get; private set; }

		// Token: 0x1700193D RID: 6461
		// (get) Token: 0x06007F6F RID: 32623 RVA: 0x00363630 File Offset: 0x00361A30
		public bool IsActive
		{
			[CompilerGenerated]
			get
			{
				return 0 < this.HeartPoint && !this.ItemName.IsNullOrEmpty();
			}
		}
	}
}
