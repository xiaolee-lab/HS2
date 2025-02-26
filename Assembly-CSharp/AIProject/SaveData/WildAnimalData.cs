using System;
using MessagePack;

namespace AIProject.SaveData
{
	// Token: 0x0200098A RID: 2442
	[MessagePackObject(false)]
	public class WildAnimalData
	{
		// Token: 0x0600460F RID: 17935 RVA: 0x001AD7D1 File Offset: 0x001ABBD1
		public WildAnimalData()
		{
		}

		// Token: 0x06004610 RID: 17936 RVA: 0x001AD7D9 File Offset: 0x001ABBD9
		public WildAnimalData(WildAnimalData data)
		{
			this.Copy(data);
		}

		// Token: 0x17000DA0 RID: 3488
		// (get) Token: 0x06004611 RID: 17937 RVA: 0x001AD7E8 File Offset: 0x001ABBE8
		// (set) Token: 0x06004612 RID: 17938 RVA: 0x001AD7F0 File Offset: 0x001ABBF0
		[Key(0)]
		public float CoolTime { get; set; }

		// Token: 0x17000DA1 RID: 3489
		// (get) Token: 0x06004613 RID: 17939 RVA: 0x001AD7F9 File Offset: 0x001ABBF9
		// (set) Token: 0x06004614 RID: 17940 RVA: 0x001AD801 File Offset: 0x001ABC01
		[Key(1)]
		public bool IsAdded { get; set; }

		// Token: 0x06004615 RID: 17941 RVA: 0x001AD80A File Offset: 0x001ABC0A
		public void Copy(WildAnimalData data)
		{
			if (data == null)
			{
				return;
			}
			this.CoolTime = data.CoolTime;
			this.IsAdded = data.IsAdded;
		}
	}
}
