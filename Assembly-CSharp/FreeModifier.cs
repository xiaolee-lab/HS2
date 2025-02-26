using System;
using UnityEngine;
using UnityEngine.UI.ProceduralImage;

// Token: 0x02000629 RID: 1577
[ModifierID("Free")]
public class FreeModifier : ProceduralImageModifier
{
	// Token: 0x17000582 RID: 1410
	// (get) Token: 0x0600258C RID: 9612 RVA: 0x000D6CA4 File Offset: 0x000D50A4
	// (set) Token: 0x0600258D RID: 9613 RVA: 0x000D6CAC File Offset: 0x000D50AC
	public Vector4 Radius
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

	// Token: 0x0600258E RID: 9614 RVA: 0x000D6CB5 File Offset: 0x000D50B5
	public override Vector4 CalculateRadius(Rect imageRect)
	{
		return this.radius;
	}

	// Token: 0x0600258F RID: 9615 RVA: 0x000D6CC0 File Offset: 0x000D50C0
	protected void OnValidate()
	{
		this.radius.x = Mathf.Max(0f, this.radius.x);
		this.radius.y = Mathf.Max(0f, this.radius.y);
		this.radius.z = Mathf.Max(0f, this.radius.z);
		this.radius.w = Mathf.Max(0f, this.radius.w);
	}

	// Token: 0x04002559 RID: 9561
	[SerializeField]
	private Vector4 radius;
}
