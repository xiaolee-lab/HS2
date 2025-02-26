using System;
using UnityEngine;
using UnityEngine.UI.ProceduralImage;

// Token: 0x0200062C RID: 1580
[ModifierID("Round")]
public class RoundModifier : ProceduralImageModifier
{
	// Token: 0x06002597 RID: 9623 RVA: 0x000D6E38 File Offset: 0x000D5238
	public override Vector4 CalculateRadius(Rect imageRect)
	{
		float num = Mathf.Min(imageRect.width, imageRect.height) * 0.5f;
		return new Vector4(num, num, num, num);
	}
}
