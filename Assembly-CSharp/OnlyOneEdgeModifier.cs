using System;
using UnityEngine;
using UnityEngine.UI.ProceduralImage;

// Token: 0x0200062A RID: 1578
[ModifierID("Only One Edge")]
public class OnlyOneEdgeModifier : ProceduralImageModifier
{
	// Token: 0x17000583 RID: 1411
	// (get) Token: 0x06002591 RID: 9617 RVA: 0x000D6D55 File Offset: 0x000D5155
	// (set) Token: 0x06002592 RID: 9618 RVA: 0x000D6D5D File Offset: 0x000D515D
	public float Radius
	{
		get
		{
			return this.radius;
		}
		set
		{
			this.radius = value;
		}
	}

	// Token: 0x17000584 RID: 1412
	// (get) Token: 0x06002593 RID: 9619 RVA: 0x000D6D66 File Offset: 0x000D5166
	// (set) Token: 0x06002594 RID: 9620 RVA: 0x000D6D6E File Offset: 0x000D516E
	public OnlyOneEdgeModifier.ProceduralImageEdge Side
	{
		get
		{
			return this.side;
		}
		set
		{
			this.side = value;
		}
	}

	// Token: 0x06002595 RID: 9621 RVA: 0x000D6D78 File Offset: 0x000D5178
	public override Vector4 CalculateRadius(Rect imageRect)
	{
		switch (this.side)
		{
		case OnlyOneEdgeModifier.ProceduralImageEdge.Top:
			return new Vector4(this.radius, this.radius, 0f, 0f);
		case OnlyOneEdgeModifier.ProceduralImageEdge.Bottom:
			return new Vector4(0f, 0f, this.radius, this.radius);
		case OnlyOneEdgeModifier.ProceduralImageEdge.Left:
			return new Vector4(this.radius, 0f, 0f, this.radius);
		case OnlyOneEdgeModifier.ProceduralImageEdge.Right:
			return new Vector4(0f, this.radius, this.radius, 0f);
		default:
			return new Vector4(0f, 0f, 0f, 0f);
		}
	}

	// Token: 0x0400255A RID: 9562
	[SerializeField]
	private float radius;

	// Token: 0x0400255B RID: 9563
	[SerializeField]
	private OnlyOneEdgeModifier.ProceduralImageEdge side;

	// Token: 0x0200062B RID: 1579
	public enum ProceduralImageEdge
	{
		// Token: 0x0400255D RID: 9565
		Top,
		// Token: 0x0400255E RID: 9566
		Bottom,
		// Token: 0x0400255F RID: 9567
		Left,
		// Token: 0x04002560 RID: 9568
		Right
	}
}
