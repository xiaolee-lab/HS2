using System;

// Token: 0x020010DA RID: 4314
public class ExcelDataSeek
{
	// Token: 0x06008F79 RID: 36729 RVA: 0x003BAEB3 File Offset: 0x003B92B3
	public ExcelDataSeek(ExcelData data)
	{
		this.data = data;
		this.cell = 0;
		this.row = 0;
	}

	// Token: 0x17001F0C RID: 7948
	// (get) Token: 0x06008F7A RID: 36730 RVA: 0x003BAED0 File Offset: 0x003B92D0
	// (set) Token: 0x06008F7B RID: 36731 RVA: 0x003BAED8 File Offset: 0x003B92D8
	public ExcelData data { get; private set; }

	// Token: 0x17001F0D RID: 7949
	// (get) Token: 0x06008F7C RID: 36732 RVA: 0x003BAEE1 File Offset: 0x003B92E1
	// (set) Token: 0x06008F7D RID: 36733 RVA: 0x003BAEE9 File Offset: 0x003B92E9
	public int cell { get; private set; }

	// Token: 0x17001F0E RID: 7950
	// (get) Token: 0x06008F7E RID: 36734 RVA: 0x003BAEF2 File Offset: 0x003B92F2
	// (set) Token: 0x06008F7F RID: 36735 RVA: 0x003BAEFA File Offset: 0x003B92FA
	public int row { get; private set; }

	// Token: 0x17001F0F RID: 7951
	// (get) Token: 0x06008F80 RID: 36736 RVA: 0x003BAF03 File Offset: 0x003B9303
	public bool isEndCell
	{
		get
		{
			return this.cell >= this.data.list.Count;
		}
	}

	// Token: 0x17001F10 RID: 7952
	// (get) Token: 0x06008F81 RID: 36737 RVA: 0x003BAF20 File Offset: 0x003B9320
	public bool isEndRow
	{
		get
		{
			bool result = true;
			if (this.cell < this.data.list.Count && this.row < this.data.list[this.cell].list.Count)
			{
				result = false;
			}
			return result;
		}
	}

	// Token: 0x06008F82 RID: 36738 RVA: 0x003BAF78 File Offset: 0x003B9378
	public int SetCell(int set)
	{
		this.cell = set;
		return set;
	}

	// Token: 0x06008F83 RID: 36739 RVA: 0x003BAF90 File Offset: 0x003B9390
	public int SetRow(int set)
	{
		this.row = set;
		return set;
	}

	// Token: 0x06008F84 RID: 36740 RVA: 0x003BAFA8 File Offset: 0x003B93A8
	public int NextCell(int next)
	{
		return this.cell += next;
	}

	// Token: 0x06008F85 RID: 36741 RVA: 0x003BAFC8 File Offset: 0x003B93C8
	public int NextRow(int next)
	{
		return this.row += next;
	}

	// Token: 0x06008F86 RID: 36742 RVA: 0x003BAFE8 File Offset: 0x003B93E8
	public bool SearchCell(int next = 0, bool isErrorCheck = false)
	{
		bool result = false;
		this.cell += next;
		while (this.cell < this.data.list.Count)
		{
			if (this.row < this.data.list[this.cell].list.Count && this.data.list[this.cell].list[this.row] != string.Empty)
			{
				result = true;
				break;
			}
			this.cell++;
		}
		return result;
	}

	// Token: 0x06008F87 RID: 36743 RVA: 0x003BB09C File Offset: 0x003B949C
	public bool SearchRow(int next = 0, bool isErrorCheck = false)
	{
		bool result = false;
		this.row += next;
		if (this.cell < this.data.list.Count)
		{
			while (this.row < this.data.list[this.cell].list.Count)
			{
				if (this.data.list[this.cell].list[this.row] != string.Empty)
				{
					result = true;
					break;
				}
				this.row++;
			}
		}
		return result;
	}
}
