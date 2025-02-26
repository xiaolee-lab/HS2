using System;
using MessagePack;
using UnityEngine;

namespace Housing
{
	// Token: 0x02000890 RID: 2192
	[Union(0, typeof(OIItem))]
	[Union(1, typeof(OIFolder))]
	public interface IObjectInfo
	{
		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x060038E6 RID: 14566
		int Kind { get; }

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x060038E7 RID: 14567
		// (set) Token: 0x060038E8 RID: 14568
		Vector3 Pos { get; set; }

		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x060038E9 RID: 14569
		// (set) Token: 0x060038EA RID: 14570
		Vector3 Rot { get; set; }
	}
}
