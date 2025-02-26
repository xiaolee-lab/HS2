using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A52 RID: 2642
public class MetaballCellCluster : MetaballCellClusterInterface
{
	// Token: 0x17000EA5 RID: 3749
	// (get) Token: 0x06004E6D RID: 20077 RVA: 0x001E1CBA File Offset: 0x001E00BA
	// (set) Token: 0x06004E6E RID: 20078 RVA: 0x001E1CC2 File Offset: 0x001E00C2
	public float BaseRadius
	{
		get
		{
			return this._baseRadius;
		}
		set
		{
			this._baseRadius = value;
		}
	}

	// Token: 0x06004E6F RID: 20079 RVA: 0x001E1CCC File Offset: 0x001E00CC
	public void DoForeachCell(ForeachCellDeleg deleg)
	{
		foreach (MetaballCell c in this._cells)
		{
			deleg(c);
		}
	}

	// Token: 0x06004E70 RID: 20080 RVA: 0x001E1D28 File Offset: 0x001E0128
	public MetaballCell GetCell(int index)
	{
		return this._cells[index];
	}

	// Token: 0x06004E71 RID: 20081 RVA: 0x001E1D38 File Offset: 0x001E0138
	public int FindCell(MetaballCell cell)
	{
		for (int i = 0; i < this._cells.Count; i++)
		{
			if (this._cells[i] == cell)
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x17000EA6 RID: 3750
	// (get) Token: 0x06004E72 RID: 20082 RVA: 0x001E1D76 File Offset: 0x001E0176
	public int CellCount
	{
		get
		{
			return this._cells.Count;
		}
	}

	// Token: 0x06004E73 RID: 20083 RVA: 0x001E1D84 File Offset: 0x001E0184
	public MetaballCell AddCell(Vector3 position, float minDistanceCoef = 1f, float? radius = null, string tag = null)
	{
		MetaballCell cell = new MetaballCell();
		cell.baseColor = this._baseColor;
		cell.radius = ((radius != null) ? radius.Value : this._baseRadius);
		cell.modelPosition = position;
		cell.tag = tag;
		bool bFail = false;
		this.DoForeachCell(delegate(MetaballCell c)
		{
			if ((cell.modelPosition - c.modelPosition).sqrMagnitude < cell.radius * cell.radius * minDistanceCoef * minDistanceCoef)
			{
				bFail = true;
			}
		});
		if (!bFail)
		{
			this._cells.Add(cell);
		}
		return (!bFail) ? cell : null;
	}

	// Token: 0x06004E74 RID: 20084 RVA: 0x001E1E4B File Offset: 0x001E024B
	public void RemoveCell(MetaballCell cell)
	{
		this._cells.Remove(cell);
	}

	// Token: 0x06004E75 RID: 20085 RVA: 0x001E1E5A File Offset: 0x001E025A
	public void ClearCells()
	{
		this._cells.Clear();
	}

	// Token: 0x06004E76 RID: 20086 RVA: 0x001E1E68 File Offset: 0x001E0268
	public string GetPositionsString()
	{
		string text = string.Empty;
		foreach (MetaballCell metaballCell in this._cells)
		{
			text += metaballCell.modelPosition.ToString("F3");
			text += ";";
		}
		if (text[text.Length - 1] == ';')
		{
			text = text.Substring(0, text.Length - 1);
		}
		return text;
	}

	// Token: 0x06004E77 RID: 20087 RVA: 0x001E1F0C File Offset: 0x001E030C
	public void ReadPositionsString(string positions)
	{
		this.ClearCells();
		string[] array = positions.Split(new char[]
		{
			';'
		});
		if (array.Length == 0)
		{
			throw new UnityException("invalid input positions data :" + positions);
		}
		for (int i = 0; i < array.Length; i++)
		{
			Vector3 position = MetaballCellCluster.ParseVector3(array[i]);
			this.AddCell(position, 0f, null, null);
		}
	}

	// Token: 0x06004E78 RID: 20088 RVA: 0x001E1F80 File Offset: 0x001E0380
	private static Vector3 ParseVector3(string data)
	{
		int num = data.IndexOf('(');
		int num2 = data.IndexOf(')');
		string text = data.Substring(num + 1, num2 - num - 1);
		string[] array = text.Split(new char[]
		{
			','
		});
		Vector3 zero = Vector3.zero;
		int num3 = 0;
		while (num3 < 3 && num3 < array.Length)
		{
			zero[num3] = float.Parse(array[num3]);
			num3++;
		}
		return zero;
	}

	// Token: 0x04004788 RID: 18312
	private List<MetaballCell> _cells = new List<MetaballCell>();

	// Token: 0x04004789 RID: 18313
	private float _baseRadius;

	// Token: 0x0400478A RID: 18314
	private Vector3 _baseColor = Vector3.one;
}
