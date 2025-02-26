using System;
using System.Linq;
using UnityEx;

namespace AIProject
{
	// Token: 0x02000F0D RID: 3853
	public class Recipe
	{
		// Token: 0x170018E7 RID: 6375
		// (get) Token: 0x06007E1F RID: 32287 RVA: 0x0035BD2A File Offset: 0x0035A12A
		// (set) Token: 0x06007E20 RID: 32288 RVA: 0x0035BD32 File Offset: 0x0035A132
		public int ID { get; set; }

		// Token: 0x170018E8 RID: 6376
		// (get) Token: 0x06007E21 RID: 32289 RVA: 0x0035BD3B File Offset: 0x0035A13B
		// (set) Token: 0x06007E22 RID: 32290 RVA: 0x0035BD43 File Offset: 0x0035A143
		public int CategoryID { get; set; }

		// Token: 0x170018E9 RID: 6377
		// (get) Token: 0x06007E23 RID: 32291 RVA: 0x0035BD4C File Offset: 0x0035A14C
		// (set) Token: 0x06007E24 RID: 32292 RVA: 0x0035BD54 File Offset: 0x0035A154
		public int ItemID { get; set; }

		// Token: 0x170018EA RID: 6378
		// (get) Token: 0x06007E25 RID: 32293 RVA: 0x0035BD5D File Offset: 0x0035A15D
		// (set) Token: 0x06007E26 RID: 32294 RVA: 0x0035BD65 File Offset: 0x0035A165
		public string Name { get; set; }

		// Token: 0x170018EB RID: 6379
		// (get) Token: 0x06007E27 RID: 32295 RVA: 0x0035BD6E File Offset: 0x0035A16E
		// (set) Token: 0x06007E28 RID: 32296 RVA: 0x0035BD76 File Offset: 0x0035A176
		public int Priority { get; set; }

		// Token: 0x170018EC RID: 6380
		// (get) Token: 0x06007E29 RID: 32297 RVA: 0x0035BD7F File Offset: 0x0035A17F
		// (set) Token: 0x06007E2A RID: 32298 RVA: 0x0035BD87 File Offset: 0x0035A187
		public float Rate { get; set; }

		// Token: 0x170018ED RID: 6381
		// (get) Token: 0x06007E2B RID: 32299 RVA: 0x0035BD90 File Offset: 0x0035A190
		// (set) Token: 0x06007E2C RID: 32300 RVA: 0x0035BD98 File Offset: 0x0035A198
		public bool IsVisible { get; set; }

		// Token: 0x170018EE RID: 6382
		// (get) Token: 0x06007E2D RID: 32301 RVA: 0x0035BDA1 File Offset: 0x0035A1A1
		// (set) Token: 0x06007E2E RID: 32302 RVA: 0x0035BDAE File Offset: 0x0035A1AE
		public UnityEx.ValueTuple<int, int, int>[] Materials
		{
			get
			{
				return this._materials.ToArray<UnityEx.ValueTuple<int, int, int>>();
			}
			set
			{
				this._materials = value.ToArray<UnityEx.ValueTuple<int, int, int>>();
			}
		}

		// Token: 0x040065D6 RID: 26070
		private UnityEx.ValueTuple<int, int, int>[] _materials;
	}
}
