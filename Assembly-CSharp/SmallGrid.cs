using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000EE9 RID: 3817
public struct SmallGrid
{
	// Token: 0x04006490 RID: 25744
	public int m_state;

	// Token: 0x04006491 RID: 25745
	public int m_canRoof;

	// Token: 0x04006492 RID: 25746
	public int[] m_itemkind;

	// Token: 0x04006493 RID: 25747
	public int[] m_itemstackwall;

	// Token: 0x04006494 RID: 25748
	public int[] m_itemdupulication;

	// Token: 0x04006495 RID: 25749
	public bool m_inRoom;

	// Token: 0x04006496 RID: 25750
	public bool m_UnderCarsol;

	// Token: 0x04006497 RID: 25751
	public int m_PutFloatingPartsHeight;

	// Token: 0x04006498 RID: 25752
	public List<int> m_PutElement;

	// Token: 0x04006499 RID: 25753
	public Renderer m_color;
}
