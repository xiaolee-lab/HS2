using System;

namespace UnityEngine.UI.ProceduralImage
{
	// Token: 0x02000630 RID: 1584
	[DisallowMultipleComponent]
	public abstract class ProceduralImageModifier : MonoBehaviour
	{
		// Token: 0x060025AE RID: 9646
		public abstract Vector4 CalculateRadius(Rect imageRect);
	}
}
