using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MessagePack;
using UnityEngine;

namespace Housing
{
	// Token: 0x02000891 RID: 2193
	[MessagePackObject(false)]
	public class OIFolder : IObjectInfo
	{
		// Token: 0x060038EB RID: 14571 RVA: 0x0015093D File Offset: 0x0014ED3D
		public OIFolder()
		{
		}

		// Token: 0x060038EC RID: 14572 RVA: 0x00150978 File Offset: 0x0014ED78
		public OIFolder(OIFolder _src)
		{
			this.Pos = _src.Pos;
			this.Rot = _src.Rot;
			this.Name = _src.Name;
			this.Expand = _src.Expand;
			foreach (IObjectInfo objectInfo in _src.Child)
			{
				int kind = objectInfo.Kind;
				if (kind != 0)
				{
					if (kind == 1)
					{
						this.Child.Add(new OIFolder(objectInfo as OIFolder));
					}
				}
				else
				{
					this.Child.Add(new OIItem(objectInfo as OIItem));
				}
			}
		}

		// Token: 0x060038ED RID: 14573 RVA: 0x00150A88 File Offset: 0x0014EE88
		public OIFolder(OIFolder _src, bool _idCopy)
		{
			this.Pos = _src.Pos;
			this.Rot = _src.Rot;
			this.Name = _src.Name;
			this.Expand = _src.Expand;
			foreach (IObjectInfo objectInfo in _src.Child)
			{
				int kind = objectInfo.Kind;
				if (kind != 0)
				{
					if (kind == 1)
					{
						this.Child.Add(new OIFolder(objectInfo as OIFolder, _idCopy));
					}
				}
				else
				{
					this.Child.Add(new OIItem(objectInfo as OIItem, _idCopy));
				}
			}
		}

		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x060038EE RID: 14574 RVA: 0x00150B98 File Offset: 0x0014EF98
		[IgnoreMember]
		public int Kind
		{
			[CompilerGenerated]
			get
			{
				return 1;
			}
		}

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x060038EF RID: 14575 RVA: 0x00150B9B File Offset: 0x0014EF9B
		// (set) Token: 0x060038F0 RID: 14576 RVA: 0x00150BA3 File Offset: 0x0014EFA3
		[Key(0)]
		public Vector3 Pos { get; set; } = Vector3.zero;

		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x060038F1 RID: 14577 RVA: 0x00150BAC File Offset: 0x0014EFAC
		// (set) Token: 0x060038F2 RID: 14578 RVA: 0x00150BB4 File Offset: 0x0014EFB4
		[Key(1)]
		public Vector3 Rot { get; set; } = Vector3.zero;

		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x060038F3 RID: 14579 RVA: 0x00150BBD File Offset: 0x0014EFBD
		// (set) Token: 0x060038F4 RID: 14580 RVA: 0x00150BC5 File Offset: 0x0014EFC5
		[Key(2)]
		public string Name { get; set; } = "フォルダー";

		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x060038F5 RID: 14581 RVA: 0x00150BCE File Offset: 0x0014EFCE
		// (set) Token: 0x060038F6 RID: 14582 RVA: 0x00150BD6 File Offset: 0x0014EFD6
		[Key(3)]
		public List<IObjectInfo> Child { get; set; } = new List<IObjectInfo>();

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x060038F7 RID: 14583 RVA: 0x00150BDF File Offset: 0x0014EFDF
		// (set) Token: 0x060038F8 RID: 14584 RVA: 0x00150BE7 File Offset: 0x0014EFE7
		[Key(4)]
		public bool Expand { get; set; } = true;
	}
}
