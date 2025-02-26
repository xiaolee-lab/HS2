using System;

namespace UnityEngine.UI.ProceduralImage
{
	// Token: 0x0200062F RID: 1583
	public struct ProceduralImageInfo
	{
		// Token: 0x060025AC RID: 9644 RVA: 0x000D7358 File Offset: 0x000D5758
		public ProceduralImageInfo(float width, float height, float fallOffDistance, float pixelSize, Vector4 radius, float borderWidth)
		{
			this.width = Mathf.Abs(width);
			this.height = Mathf.Abs(height);
			this.fallOffDistance = Mathf.Max(0f, fallOffDistance);
			this.radius = radius;
			this.borderWidth = Mathf.Max(borderWidth, 0f);
			this.pixelSize = Mathf.Max(0f, pixelSize);
		}

		// Token: 0x04002566 RID: 9574
		public float width;

		// Token: 0x04002567 RID: 9575
		public float height;

		// Token: 0x04002568 RID: 9576
		public float fallOffDistance;

		// Token: 0x04002569 RID: 9577
		public Vector4 radius;

		// Token: 0x0400256A RID: 9578
		public float borderWidth;

		// Token: 0x0400256B RID: 9579
		public float pixelSize;
	}
}
