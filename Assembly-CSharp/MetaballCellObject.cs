using System;
using UnityEngine;

// Token: 0x02000A55 RID: 2645
public class MetaballCellObject : MonoBehaviour
{
	// Token: 0x17000EA9 RID: 3753
	// (get) Token: 0x06004E81 RID: 20097 RVA: 0x001E2068 File Offset: 0x001E0468
	public MetaballCell Cell
	{
		get
		{
			return this._cell;
		}
	}

	// Token: 0x06004E82 RID: 20098 RVA: 0x001E2070 File Offset: 0x001E0470
	public virtual void Setup(MetaballCell cell)
	{
		this._cell = cell;
	}

	// Token: 0x0400478B RID: 18315
	protected MetaballCell _cell;
}
