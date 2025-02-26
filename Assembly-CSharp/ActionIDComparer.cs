using System;
using System.Collections.Generic;

// Token: 0x0200082F RID: 2095
public class ActionIDComparer : IEqualityComparer<ActionID>
{
	// Token: 0x06003546 RID: 13638 RVA: 0x0013A7D2 File Offset: 0x00138BD2
	public bool Equals(ActionID _id0, ActionID _id1)
	{
		return _id0 == _id1;
	}

	// Token: 0x06003547 RID: 13639 RVA: 0x0013A7D8 File Offset: 0x00138BD8
	public int GetHashCode(ActionID _id)
	{
		return (int)_id;
	}
}
