using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020010D7 RID: 4311
public class ExcelData : ScriptableObject
{
	// Token: 0x17001F0A RID: 7946
	public string this[int cell, int row]
	{
		get
		{
			return this.Get(cell, row);
		}
	}

	// Token: 0x17001F0B RID: 7947
	// (get) Token: 0x06008F6A RID: 36714 RVA: 0x003BA61A File Offset: 0x003B8A1A
	public int MaxCell
	{
		get
		{
			return this.list.Count;
		}
	}

	// Token: 0x06008F6B RID: 36715 RVA: 0x003BA627 File Offset: 0x003B8A27
	public string Get(ExcelData.Specify specify)
	{
		return this.Get(specify.cell, specify.row);
	}

	// Token: 0x06008F6C RID: 36716 RVA: 0x003BA63C File Offset: 0x003B8A3C
	public string Get(int cell, int row)
	{
		string result = string.Empty;
		if (cell < this.list.Count && row < this.list[cell].list.Count)
		{
			result = this.list[cell].list[row];
		}
		return result;
	}

	// Token: 0x06008F6D RID: 36717 RVA: 0x003BA698 File Offset: 0x003B8A98
	public List<string> GetCell(int row)
	{
		List<string> list = new List<string>();
		foreach (ExcelData.Param param in this.list)
		{
			if (row < param.list.Count)
			{
				list.Add(param.list[row]);
			}
		}
		return list;
	}

	// Token: 0x06008F6E RID: 36718 RVA: 0x003BA718 File Offset: 0x003B8B18
	public List<string> GetCell(int rowStart, int rowEnd)
	{
		List<string> list = new List<string>();
		if ((ulong)rowStart > (ulong)((long)rowEnd))
		{
			return list;
		}
		foreach (ExcelData.Param param in this.list)
		{
			int num = rowStart;
			while (num < param.list.Count && num <= rowEnd)
			{
				list.Add(param.list[num]);
				num++;
			}
		}
		return list;
	}

	// Token: 0x06008F6F RID: 36719 RVA: 0x003BA7B4 File Offset: 0x003B8BB4
	public List<string> GetRow(int cell)
	{
		List<string> list = new List<string>();
		if (cell < this.list.Count)
		{
			foreach (string item in this.list[cell].list)
			{
				list.Add(item);
			}
		}
		return list;
	}

	// Token: 0x06008F70 RID: 36720 RVA: 0x003BA834 File Offset: 0x003B8C34
	public List<string> GetRow(int cellStart, int cellEnd)
	{
		List<string> list = new List<string>();
		if ((ulong)cellStart > (ulong)((long)cellEnd))
		{
			return list;
		}
		int num = cellStart;
		while (num < this.list.Count && num <= cellEnd)
		{
			foreach (string item in this.list[num].list)
			{
				list.Add(item);
			}
			num++;
		}
		return list;
	}

	// Token: 0x06008F71 RID: 36721 RVA: 0x003BA8D0 File Offset: 0x003B8CD0
	public List<ExcelData.Param> Get(ExcelData.Specify start, ExcelData.Specify end)
	{
		List<ExcelData.Param> list = new List<ExcelData.Param>();
		if ((ulong)start.cell > (ulong)((long)end.cell) || (ulong)start.row > (ulong)((long)end.row))
		{
			return list;
		}
		if (start.cell < this.list.Count)
		{
			int num = start.cell;
			while (num < this.list.Count && num <= end.cell)
			{
				ExcelData.Param param = new ExcelData.Param();
				if (start.row < this.list[num].list.Count)
				{
					param.list = new List<string>();
					int num2 = start.row;
					while (num2 < this.list[num].list.Count && num2 <= end.row)
					{
						param.list.Add(this.list[num].list[num2]);
						num2++;
					}
				}
				list.Add(param);
				num++;
			}
		}
		return list;
	}

	// Token: 0x06008F72 RID: 36722 RVA: 0x003BA9E4 File Offset: 0x003B8DE4
	public List<ExcelData.Param> Get(ExcelData.Specify start, ExcelData.Specify end, string find)
	{
		List<ExcelData.Param> list = new List<ExcelData.Param>();
		list = this.Get(start, end);
		foreach (ExcelData.Param param in list)
		{
			foreach (string b in param.list)
			{
				if (find == b)
				{
					ExcelData.Param param2 = new ExcelData.Param();
					param2.list = new List<string>();
					foreach (string item in param.list)
					{
						param2.list.Add(item);
					}
					list.Add(param2);
					break;
				}
			}
		}
		return list;
	}

	// Token: 0x06008F73 RID: 36723 RVA: 0x003BAB0C File Offset: 0x003B8F0C
	public List<ExcelData.Param> Find(string find)
	{
		List<ExcelData.Param> list = new List<ExcelData.Param>();
		foreach (ExcelData.Param param in this.list)
		{
			foreach (string b in param.list)
			{
				if (find == b)
				{
					ExcelData.Param param2 = new ExcelData.Param();
					param2.list = new List<string>();
					foreach (string item in param.list)
					{
						param2.list.Add(item);
					}
					list.Add(param2);
					break;
				}
			}
		}
		return list;
	}

	// Token: 0x06008F74 RID: 36724 RVA: 0x003BAC30 File Offset: 0x003B9030
	public List<ExcelData.Param> FindArea(string find, ExcelData.Specify spe)
	{
		List<ExcelData.Param> list = new List<ExcelData.Param>();
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < this.list.Count; i++)
		{
			for (int j = 0; j < this.list[i].list.Count; j++)
			{
				if (find == this.list[i].list[j])
				{
					num = i + 1;
					num2 = j;
					break;
				}
			}
		}
		int num3 = num;
		while (num3 < this.list.Count && num3 < num + spe.cell)
		{
			ExcelData.Param param = new ExcelData.Param();
			int num4 = num2;
			while (num4 < this.list[num3].list.Count && num4 < num2 + spe.row)
			{
				param.list = new List<string>();
				param.list.Add(this.list[num3].list[num4]);
				list.Add(param);
				num4++;
			}
			num3++;
		}
		return list;
	}

	// Token: 0x06008F75 RID: 36725 RVA: 0x003BAD68 File Offset: 0x003B9168
	public List<ExcelData.Param> FindArea(string find)
	{
		List<ExcelData.Param> list = new List<ExcelData.Param>();
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < this.list.Count; i++)
		{
			for (int j = 0; j < this.list[i].list.Count; j++)
			{
				if (find == this.list[i].list[j])
				{
					num = i + 1;
					num2 = j;
					break;
				}
			}
		}
		for (int k = num; k < this.list.Count; k++)
		{
			ExcelData.Param param = new ExcelData.Param();
			for (int l = num2; l < this.list[k].list.Count; l++)
			{
				param.list = new List<string>();
				param.list.Add(this.list[k].list[l]);
				list.Add(param);
			}
		}
		return list;
	}

	// Token: 0x040073EA RID: 29674
	public List<ExcelData.Param> list = new List<ExcelData.Param>();

	// Token: 0x020010D8 RID: 4312
	public class Specify
	{
		// Token: 0x06008F76 RID: 36726 RVA: 0x003BAE82 File Offset: 0x003B9282
		public Specify(int cell, int row)
		{
			this.cell = cell;
			this.row = row;
		}

		// Token: 0x06008F77 RID: 36727 RVA: 0x003BAE98 File Offset: 0x003B9298
		public Specify()
		{
		}

		// Token: 0x040073EB RID: 29675
		public int cell;

		// Token: 0x040073EC RID: 29676
		public int row;
	}

	// Token: 0x020010D9 RID: 4313
	[Serializable]
	public class Param
	{
		// Token: 0x040073ED RID: 29677
		public List<string> list = new List<string>();
	}
}
