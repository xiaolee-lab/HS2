using System;
using UnityEngine;
using UnityEngine.UI.ProceduralImage;

// Token: 0x0200062D RID: 1581
[ModifierID("Uniform")]
public class UniformModifier : ProceduralImageModifier
{
	// Token: 0x17000585 RID: 1413
	// (get) Token: 0x06002599 RID: 9625 RVA: 0x000D6E70 File Offset: 0x000D5270
	// (set) Token: 0x0600259A RID: 9626 RVA: 0x000D6E78 File Offset: 0x000D5278
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

	// Token: 0x0600259B RID: 9627 RVA: 0x000D6E84 File Offset: 0x000D5284
	public override Vector4 CalculateRadius(Rect imageRect)
	{
		float num = this.radius;
		return new Vector4(num, num, num, num);
	}

	// Token: 0x04002561 RID: 9569
	[SerializeField]
	private float radius;
}
