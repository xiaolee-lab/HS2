using System;

// Token: 0x02000A54 RID: 2644
public interface MetaballCellClusterInterface
{
	// Token: 0x17000EA7 RID: 3751
	// (get) Token: 0x06004E7D RID: 20093
	float BaseRadius { get; }

	// Token: 0x06004E7E RID: 20094
	void DoForeachCell(ForeachCellDeleg deleg);

	// Token: 0x17000EA8 RID: 3752
	// (get) Token: 0x06004E7F RID: 20095
	int CellCount { get; }
}
