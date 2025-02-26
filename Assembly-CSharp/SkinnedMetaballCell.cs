using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A56 RID: 2646
public class SkinnedMetaballCell : MetaballCell, MetaballCellClusterInterface
{
	// Token: 0x17000EAA RID: 3754
	// (get) Token: 0x06004E84 RID: 20100 RVA: 0x001E2097 File Offset: 0x001E0497
	public float BaseRadius
	{
		get
		{
			return this.radius;
		}
	}

	// Token: 0x17000EAB RID: 3755
	// (get) Token: 0x06004E85 RID: 20101 RVA: 0x001E209F File Offset: 0x001E049F
	public bool IsRoot
	{
		get
		{
			return this.parent == null;
		}
	}

	// Token: 0x17000EAC RID: 3756
	// (get) Token: 0x06004E86 RID: 20102 RVA: 0x001E20AA File Offset: 0x001E04AA
	public bool IsTerminal
	{
		get
		{
			return this.children.Count == 0;
		}
	}

	// Token: 0x17000EAD RID: 3757
	// (get) Token: 0x06004E87 RID: 20103 RVA: 0x001E20BA File Offset: 0x001E04BA
	public bool IsBranch
	{
		get
		{
			return this.IsRoot || this.children.Count > 1;
		}
	}

	// Token: 0x17000EAE RID: 3758
	// (get) Token: 0x06004E88 RID: 20104 RVA: 0x001E20D8 File Offset: 0x001E04D8
	public SkinnedMetaballCell Root
	{
		get
		{
			if (this.IsRoot)
			{
				return this;
			}
			return this.parent.Root;
		}
	}

	// Token: 0x17000EAF RID: 3759
	// (get) Token: 0x06004E89 RID: 20105 RVA: 0x001E20F4 File Offset: 0x001E04F4
	public int CellCount
	{
		get
		{
			int num = 1;
			foreach (SkinnedMetaballCell skinnedMetaballCell in this.children)
			{
				num += skinnedMetaballCell.CellCount;
			}
			return num;
		}
	}

	// Token: 0x06004E8A RID: 20106 RVA: 0x001E2158 File Offset: 0x001E0558
	public void DoForeachSkinnedCell(SkinnedMetaballCell.ForeachSkinnedCellDeleg deleg)
	{
		deleg(this);
		foreach (SkinnedMetaballCell skinnedMetaballCell in this.children)
		{
			skinnedMetaballCell.DoForeachSkinnedCell(deleg);
		}
	}

	// Token: 0x06004E8B RID: 20107 RVA: 0x001E21BC File Offset: 0x001E05BC
	public void DoForeachCell(ForeachCellDeleg deleg)
	{
		deleg(this);
		foreach (SkinnedMetaballCell skinnedMetaballCell in this.children)
		{
			skinnedMetaballCell.DoForeachCell(deleg);
		}
	}

	// Token: 0x17000EB0 RID: 3760
	// (get) Token: 0x06004E8C RID: 20108 RVA: 0x001E2220 File Offset: 0x001E0620
	public int DistanceFromBranch
	{
		get
		{
			if (this.IsBranch)
			{
				return 0;
			}
			int distanceFromLastBranch = this.DistanceFromLastBranch;
			int distanceToNextBranch = this.DistanceToNextBranch;
			return Mathf.Min(distanceFromLastBranch, distanceToNextBranch);
		}
	}

	// Token: 0x17000EB1 RID: 3761
	// (get) Token: 0x06004E8D RID: 20109 RVA: 0x001E224F File Offset: 0x001E064F
	public int DistanceFromLastLink
	{
		get
		{
			if (this.IsRoot || this.children.Count > 1 || this.links.Count > 0)
			{
				return 0;
			}
			return this.parent.DistanceFromLastLink + 1;
		}
	}

	// Token: 0x17000EB2 RID: 3762
	// (get) Token: 0x06004E8E RID: 20110 RVA: 0x001E228D File Offset: 0x001E068D
	private int DistanceFromLastBranch
	{
		get
		{
			if (this.IsBranch)
			{
				return 0;
			}
			return 1 + this.parent.DistanceFromLastBranch;
		}
	}

	// Token: 0x17000EB3 RID: 3763
	// (get) Token: 0x06004E8F RID: 20111 RVA: 0x001E22AC File Offset: 0x001E06AC
	private int DistanceToNextBranch
	{
		get
		{
			if (this.IsBranch)
			{
				return 0;
			}
			int num = int.MaxValue;
			foreach (SkinnedMetaballCell skinnedMetaballCell in this.children)
			{
				int distanceToNextBranch = skinnedMetaballCell.DistanceToNextBranch;
				if (distanceToNextBranch < num)
				{
					num = distanceToNextBranch;
				}
			}
			return num;
		}
	}

	// Token: 0x06004E90 RID: 20112 RVA: 0x001E2328 File Offset: 0x001E0728
	public SkinnedMetaballCell AddChild(Vector3 position, float in_radius, float minDistanceCoef = 1f)
	{
		SkinnedMetaballCell child = new SkinnedMetaballCell();
		child.baseColor = this.baseColor;
		child.radius = in_radius;
		child.distanceFromRoot = this.distanceFromRoot + 1;
		child.modelPosition = position;
		child.parent = this;
		this.children.Add(child);
		bool bFail = false;
		this.Root.DoForeachSkinnedCell(delegate(SkinnedMetaballCell c)
		{
			if (c != child && (child.modelPosition - c.modelPosition).sqrMagnitude < child.radius * child.radius * minDistanceCoef * minDistanceCoef)
			{
				bFail = true;
			}
		});
		if (bFail)
		{
			this.children.Remove(child);
			return null;
		}
		child.CalcRotation();
		return child;
	}

	// Token: 0x06004E91 RID: 20113 RVA: 0x001E23F4 File Offset: 0x001E07F4
	public void CalcRotation()
	{
		if (this.IsRoot)
		{
			this.modelRotation = Quaternion.FromToRotation(Vector3.forward, Vector3.up);
		}
		else
		{
			Vector3 fromDirection = this.parent.modelRotation * Vector3.forward;
			Vector3 toDirection = this.modelPosition - this.parent.modelPosition;
			this.modelRotation = Quaternion.FromToRotation(fromDirection, toDirection) * this.parent.modelRotation;
		}
	}

	// Token: 0x06004E92 RID: 20114 RVA: 0x001E2470 File Offset: 0x001E0870
	public string GetStringExpression()
	{
		string text = string.Empty;
		text += this.modelPosition.ToString("F3");
		text += ";";
		foreach (SkinnedMetaballCell skinnedMetaballCell in this.children)
		{
			text += skinnedMetaballCell.GetStringExpression();
			text += ";";
		}
		if (text[text.Length - 1] == ';')
		{
			text = text.Substring(0, text.Length - 1);
		}
		return text;
	}

	// Token: 0x06004E93 RID: 20115 RVA: 0x001E2530 File Offset: 0x001E0930
	public static SkinnedMetaballCell ConstructFromString(string data, float radius)
	{
		string[] array = data.Split(new char[]
		{
			';'
		});
		if (array.Length == 0)
		{
			throw new UnityException("invalid input data :" + data);
		}
		SkinnedMetaballCell skinnedMetaballCell = new SkinnedMetaballCell();
		skinnedMetaballCell.parent = null;
		skinnedMetaballCell.modelPosition = SkinnedMetaballCell.ParseVector3(array[0]);
		skinnedMetaballCell.radius = radius;
		skinnedMetaballCell.baseColor = Vector3.zero;
		skinnedMetaballCell.CalcRotation();
		for (int i = 1; i < array.Length; i++)
		{
			Vector3 position = SkinnedMetaballCell.ParseVector3(array[i]);
			skinnedMetaballCell.AddChild(position, radius, 0f);
		}
		return skinnedMetaballCell;
	}

	// Token: 0x06004E94 RID: 20116 RVA: 0x001E25C8 File Offset: 0x001E09C8
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

	// Token: 0x0400478C RID: 18316
	public SkinnedMetaballCell parent;

	// Token: 0x0400478D RID: 18317
	public List<SkinnedMetaballCell> children = new List<SkinnedMetaballCell>();

	// Token: 0x0400478E RID: 18318
	public List<SkinnedMetaballCell> links = new List<SkinnedMetaballCell>();

	// Token: 0x0400478F RID: 18319
	public int distanceFromRoot;

	// Token: 0x02000A57 RID: 2647
	// (Invoke) Token: 0x06004E96 RID: 20118
	public delegate void ForeachSkinnedCellDeleg(SkinnedMetaballCell c);
}
